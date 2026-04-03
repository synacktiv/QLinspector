
import java
import libs.generic.DangerousMethods
import libs.generic.Source


/* 
*
* Need some improvment with lambda expression,
* sometimes return unexpected results with lambda expressions
*
*/
class DangerousExpression extends Expr {
  DangerousExpression() {
    ( this instanceof Call and this.(Call).getCallee() instanceof DangerousMethod ) or
    ( this instanceof LambdaExpr and this.(LambdaExpr).getExprBody().(MethodCall).getMethod() instanceof DangerousMethod)
  }
}

class CallsDangerousMethod extends Callable {
  CallsDangerousMethod(){
    exists(DangerousExpression de | de.getEnclosingCallable() = this)
  }
}

class RecursiveCallToDangerousMethod extends Callable {
  RecursiveCallToDangerousMethod(){

    not this instanceof Sanitizer and


    /*
     can be commented for more results
    
    (
      getDeclaringType().getASupertype*() instanceof TypeSerializable or 
      
      // we don't want static call with no parameters as we won't control anything
      (this.isStatic() and not this.hasNoParameters())or 
      

      this instanceof Constructor
    ) 
    
    and*/
    
    (
      this instanceof CallsDangerousMethod or
      exists(RecursiveCallToDangerousMethod unsafe | this.polyCalls(unsafe))
    ) 
  }

    /*
     linking a RecursiveCallToDangerousMethod to a DangerousExpression
    */
    DangerousExpression getDangerousExpression(){
    exists(DangerousExpression de | de.getEnclosingCallable() = this and result = de ) or 
    exists(RecursiveCallToDangerousMethod unsafe | this.polyCalls(unsafe) and result = unsafe.(RecursiveCallToDangerousMethod).getDangerousExpression())
    }
}

/* 
*
* global filter to block function in the chain,
* method names can be added when you found a false positive
*
*/

class Sanitizer extends Callable {
  Sanitizer(){
    hasName([""]) 
  }
}

query predicate edges(Callable node1, Callable node2) {
  /*  
  (
      node1.(MethodCall).getMethod().getAPossibleImplementation() = node2 and
      node1.(MethodCall).getMethod() instanceof RecursiveCallToDangerousMethod and
      node2 instanceof RecursiveCallToDangerousMethod
    ) or (
      node1.(ConstructorCall).getConstructor() = node2 and
      node1.(ConstructorCall).getConstructor() instanceof RecursiveCallToDangerousMethod and
      node2 instanceof RecursiveCallToDangerousMethod
    ) or (
      node2.(Call).getEnclosingCallable() = node1 and
      node2.(Call).getEnclosingCallable() instanceof RecursiveCallToDangerousMethod and
      node1 instanceof RecursiveCallToDangerousMethod
    )*/

    (node1.(RecursiveCallToDangerousMethod).polyCalls(node2) and node2 instanceof RecursiveCallToDangerousMethod)
    /*
    (
      (
        node2.(Call).getEnclosingCallable() = node1 and
        hasCalls(node1, node2.(MethodCall).getMethod())
      ) or (
        node2.(Call).getEnclosingCallable() = node1 and
        hasCalls(node1, node2.(ConstructorCall).getConstructor())
      ) or (
        node1.(MethodCall).getMethod() = node2 and
        hasCalls(node1.(Call).getEnclosingCallable(), node2)
      ) or (
        node1.(ConstructorCall).getConstructor() = node2 and
        hasCalls(node1.(Call).getEnclosingCallable(), node2)
      )
    )
    */
}





predicate paramaterCall(Callable c0, Call c1){
  c1.getEnclosingCallable() = c0 and
  (
    exists(Parameter p | p = c0.getAParameter() and
      ( 
        /*
          check this:

            method(Param p):
              obj.sink(p)
              // or
              p.sink()

        */
        p.getAnAccess() = c1.getAnArgument() or
        c1.getQualifier() = p.getAnAccess()
      )
    )
  )
}

predicate callMethodOfSuperType(Callable c0, Call c1){
  c1.getEnclosingCallable() = c0 and
  /*
    here we search sink and method if the have a common supertype:

      method(Param p):
        this.sink()

  */
  c1.(MethodCall).getMethod().getDeclaringType().getASupertype*() = c0.getDeclaringType().getASupertype*()
}

predicate callToLocalVariable(Callable c0, Call c1){
  c1.getEnclosingCallable() = c0 and
  (
    /*
      find this pattern:

        CustomOBject customOBject = new CustomOBject()
        ...
        customOBject.dangerousMethod(...)

    */
    exists(LocalVariableDecl var | var.getEnclosingCallable() = c0 and
      var.getAnAccess() = c1.(MethodCall).getQualifier()
    ) 
    
    or

    /*
     if we have a new NonSerializableObject()
     TODO: check if we need to filter constructors with at least one parameter
    */
    c1 instanceof ClassInstanceExpr 

  )
  
}

predicate isValidCallToNonSerializableMethod(Callable c0, Callable c1){
  c0.polyCalls(c1) and
  exists(Call c | c.getEnclosingCallable() = c0 and
    c.getCallee() = c1
    and (
      paramaterCall(c0, c)
      //callToLocalVariable(c0, c)
      //callMethodOfSuperType(c0, c)
    )
  )
  
}

predicate isValidCallForGadgetChain(RecursiveCallToDangerousMethod c0, RecursiveCallToDangerousMethod c1){
  // If we call a method from a Serializable type it's ok
  c1.getDeclaringType().getASupertype*() instanceof TypeSerializable or 

  /*
    If we call a static method with some parameters it's ok
    we don't want static call with no parameters as we won't control anything
  */
  (c1.isStatic() and not c1.hasNoParameters() ) or
  
  // encapsulate additional checks
  isValidCallToNonSerializableMethod(c0, c1)
}

predicate hasCalls(RecursiveCallToDangerousMethod c0, RecursiveCallToDangerousMethod c1) {

    (
      isValidCallForGadgetChain(c0, c1) and
      c0.polyCalls(c1)
    ) 
    
    or 
    
    exists(RecursiveCallToDangerousMethod unsafe | 
      isValidCallForGadgetChain(c0, unsafe) and 
      c0.polyCalls(unsafe) and
      hasCalls(unsafe, c1)
    )
    
}

/*
  public void readObject(...)                => c0
    customObject.callMethod(...)           
      ...
        otherObject.sink(...)                => c1
          Runtime.getRuntime().exec(...)     => de
*/
predicate findGadgetChain(Callable c0, Callable c1, DangerousExpression de){
  c0 instanceof RecursiveCallToDangerousMethod and
  c1 instanceof RecursiveCallToDangerousMethod and
  (
    hasCalls(c0, c1) or 

    // check if c0 = c1 otherwise it won't be displayed
    c0 = c1
  ) and
  de.getEnclosingCallable() = c1
}

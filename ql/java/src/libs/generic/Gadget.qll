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
    /* can be commented for more results
    */
    (
      getDeclaringType().getASupertype*() instanceof TypeSerializable or 
      this.isStatic() 
    ) 
    
    and 
    
    (
      this instanceof CallsDangerousMethod or
      exists(RecursiveCallToDangerousMethod unsafe | this.polyCalls(unsafe))
    ) 
  }

    /*
    /* linking a RecursiveCallToDangerousMethod to a DangerousExpression
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


query predicate edges(ControlFlowNode node1, ControlFlowNode node2) {
    (node1.(MethodCall).getMethod().getAPossibleImplementation() = node2 and node2 instanceof RecursiveCallToDangerousMethod) or 
    (node2.(MethodCall).getEnclosingCallable() = node1 and node1 instanceof RecursiveCallToDangerousMethod)
}

predicate hasCalls(RecursiveCallToDangerousMethod c0, RecursiveCallToDangerousMethod c1) {
    c0.polyCalls(c1) or exists(RecursiveCallToDangerousMethod unsafe | c0.polyCalls(unsafe) and hasCalls(unsafe, c1))
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
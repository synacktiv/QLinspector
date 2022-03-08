import java
import semmle.code.java.security.DangerousMethods
import semmle.code.java.security.Source


/* 
*
* Need some improvment with lambda expression,
* sometimes return unexpected results with lambda expressions
*
*/
private class DangerousExpression extends Expr {
  DangerousExpression() {
    ( this instanceof Call and this.(Call).getCallee() instanceof DangerousMethod ) or
    ( this instanceof LambdaExpr and this.(LambdaExpr).getExprBody().(MethodAccess).getMethod() instanceof DangerousMethod)
  }
}

private class CallsDangerousMethod extends Callable {
  CallsDangerousMethod(){
    exists(DangerousExpression de | de.getEnclosingCallable() = this)
  }
}

private class RecursiveCallToDangerousMethod extends Callable {
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
private class Sanitizer extends Callable {
  Sanitizer(){
    hasName([""]) 
  }
}

/*
================ find sink  ================
from Callable c0,  DangerousExpression de
where c0 instanceof RecursiveCallToDangerousMethod and
de.getEnclosingCallable() = c0
select c0, de

================ find source  ===============
from Callable c0
where c0 instanceof RecursiveCallToDangerousMethod and
c0 instanceof Source
select c0


=======  link the source and the sink  ======
from Callable c0, Callable c1, Callable c2, Callable c3, Callable c4, Callable c5, DangerousExpression de
where c0 instanceof RecursiveCallToDangerousMethod and
de.getEnclosingCallable() = c0 and

c1.polyCalls(c0) and
c1 instanceof RecursiveCallToDangerousMethod and

c2.polyCalls(c1) and
c2 instanceof RecursiveCallToDangerousMethod and

c3.polyCalls(c2) and
c3 instanceof RecursiveCallToDangerousMethod and

c4.polyCalls(c3) and
c4 instanceof RecursiveCallToDangerousMethod and

c5.polyCalls(c4) and
c5 instanceof RecursiveCallToDangerousMethod and

c5 instanceof Source 

select c5, c4, c3, c2, c1, c0, de


========== check source to sink path  ==========
from Callable c0, MethodAccess ma , Callable c1
where c0 instanceof RecursiveCallToDangerousMethod and
ma = c0.(RecursiveCallToDangerousMethod).getDangerousExpression() and
ma.getMethod() instanceof ReflectionInvocationMethod and 
c0 instanceof Source 

select  c0, ma 
*/
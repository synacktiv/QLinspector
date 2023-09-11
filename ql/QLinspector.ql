/**
 * 
 * @name QLInspector
 * @kind path-problem
 * 
 */

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


query predicate edges(ControlFlowNode node1, ControlFlowNode node2) {
    (node1.(MethodAccess).getMethod().getAPossibleImplementation() = node2 and node2 instanceof RecursiveCallToDangerousMethod) or 
    (node2.(MethodAccess).getEnclosingCallable() = node1 and node1 instanceof RecursiveCallToDangerousMethod) 
}

predicate hasCalls(RecursiveCallToDangerousMethod c0, RecursiveCallToDangerousMethod c1) {
    c0.polyCalls(c1) or exists(RecursiveCallToDangerousMethod unsafe | c0.polyCalls(unsafe) and hasCalls(unsafe, c1))
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
from RecursiveCallToDangerousMethod c0,  RecursiveCallToDangerousMethod c1, DangerousExpression de
where de.getEnclosingCallable() = c1 and
c0 instanceof Source and
hasCalls(c0, c1)
select c0, c0, c1, "recursive call to dangerous expression $@", de, de.toString()


========== check source to sink path  ==========
from Callable c0, MethodAccess ma , Callable c1
where c0 instanceof RecursiveCallToDangerousMethod and
ma = c0.(RecursiveCallToDangerousMethod).getDangerousExpression() and
ma.getMethod() instanceof ReflectionInvocationMethod and 
c0 instanceof Source 

select  c0, ma 
*/

from RecursiveCallToDangerousMethod c0,  RecursiveCallToDangerousMethod c1, DangerousExpression de
where de.getEnclosingCallable() = c1 and
c0 instanceof Source and
hasCalls(c0, c1)
select c0, c0, c1, "recursive call to dangerous expression $@", de, de.toString()

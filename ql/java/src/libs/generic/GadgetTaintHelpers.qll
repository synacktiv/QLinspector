import java
import semmle.code.java.dataflow.TaintTracking


class GadgetAdditionalTaintStep extends Unit {
    /**
     * Holds if the step from `node1` to `node2` should be considered a taint
     * step for the `GadgetFinderConfig` configuration.
     */
    abstract predicate step(DataFlow::Node node1, DataFlow::Node node2);
}
  
/**
 * We want to propagate output of getters:
 * 
 *  Obj sink = taintedSource.getField();
 * 
 * In this example we add a taint on the sink object.
 */
class GetterTaintStep extends GadgetAdditionalTaintStep {
    override predicate step(DataFlow::Node fromNode, DataFlow::Node toNode) {
        exists(MethodCall ma, Method m | 
        
        ma.getMethod() = m and
        m.getName().matches("get%") and
        m.getNumberOfParameters() = 0 and
        
        fromNode.asExpr() = ma.getQualifier() and
        toNode.asExpr() = ma
        
        )
    }
}
  
/**
 * We want to propagate output of setters:
 * 
 *  Obj sink = new CustomObject();
 *  sink.setField(taintedSource);
 * 
 * In this example we add a taint on the sink object.
 */
class SetterTaintStep extends GadgetAdditionalTaintStep {
    override predicate step(DataFlow::Node fromNode, DataFlow::Node toNode) {
        exists(MethodCall ma, Method m | 
        
        ma.getMethod() = m and
        m.getName().matches("set%") and
        m.getNumberOfParameters() = 1 and
        
        fromNode.asExpr() = ma.getArgument(0) and
        toNode.asExpr() = ma.getQualifier()
        
        )
    }
}

/**
 * We want to taint constructed object where the constructor takes our sourceNode
 * as parameter:
 * 
 *  CustomObject toNode = new CustomObject(arg1, fromNode, arg3);
 */
class ConstructorTaintStep extends GadgetAdditionalTaintStep {
    override predicate step(DataFlow::Node fromNode, DataFlow::Node toNode) {
        exists(ConstructorCall ma | 
        
        fromNode.asExpr() = ma.getAnArgument() and
        toNode.asExpr() = ma
        
        )
    }
}
  
/**
 * We want to add additional taint step when the ObjectInputStream is accessed
 * Like this:
 * 
 *  private void readObject(ObjectInputStream fromNode) {
 *      ObjectInputStream.GetField gf = fromNode.readFields();
 *      Object toNode = gf.get("val", null);
 *      ...
 *      dangerousMethod(toNode)
 */
class ObjectInputStreamTaintStep extends GadgetAdditionalTaintStep {
    override predicate step(DataFlow::Node fromNode, DataFlow::Node toNode) {
      exists(MethodCall mc, Method m | 
        (
          (
            mc.getMethod() = m and
            m.getName().matches("read%") and
            m.getDeclaringType().hasQualifiedName("java.io", "ObjectInputStream")
          ) 
          or
          (
            mc.getMethod() = m and
            m.hasName("get") and
            m.getDeclaringType().getASupertype*().hasQualifiedName("java.io", "ObjectInputStream$GetField")
          )
        )
        
        and
        
        fromNode.asExpr() = mc.getQualifier() and
        toNode.asExpr() = mc
        
      )
    }
}

/**
 * We want to propagate the taint from an abstrat method call to an actual Lambda
 * expression call.
 * Like this:
 * 
 *  consumerFunc.accept(fromNode);
 * 
 * To an actual implementation of a lambda expression like this:
 * 
 *  (String toNode) -> {dangerousMethod(toNode);};
 * 
 * cf: https://github.com/artsploit/ysoserial/blob/scala1/src/main/java/ysoserial/payloads/Scala1.java
 */
class LambdaCallWithParameterStep extends GadgetAdditionalTaintStep {
  override predicate step(DataFlow::Node fromNode, DataFlow::Node toNode) {
    exists( LambdaExpr le, MethodCall mc  | 
        mc.getMethod().isAbstract() and
        mc.getMethod().getAPossibleImplementation() = le.asMethod() and
        le.asMethod().getDeclaringType().getASupertype*() instanceof TypeSerializable and

        fromNode.asExpr().(Argument).getCall() = mc and
        toNode.(DataFlow::ParameterNode).asParameter() = le.asMethod().getAParameter()
      )
  }
}

/**
 * We want to propagate the taint from an abstrat method call to an actual Lambda
 * expression call.
 * Like this:
 * 
 *  fromNode.run();
 * 
 * To an actual implementation of a lambda expression like this:
 * 
 *  SerializableRunnable runnable = () -> {dangerousMethod(toNode);};
 * 
 * cf: https://github.com/artsploit/ysoserial/blob/scala1/src/main/java/ysoserial/payloads/Scala1.java
 */
class LambdaCallQualifierStep extends GadgetAdditionalTaintStep {
  override predicate step(DataFlow::Node fromNode, DataFlow::Node toNode) {
    exists( LambdaExpr le, MethodCall mc  | 
        mc.getMethod().isAbstract() and
        mc.getMethod().getAPossibleImplementation() = le.asMethod() and
        le.asMethod().getDeclaringType().getASupertype*() instanceof TypeSerializable and

        fromNode.asExpr() = mc.getQualifier() and
        toNode.asExpr().(FieldAccess).getEnclosingCallable() = le.asMethod()
      )
  }
}

/**
 * Try to get a callable from a node.
 * 
 * If you add a new source type you might 
 * need to add logic here to see it in the result.
 * The last condition is however quite permissive.
 */
Callable getSourceCallable(DataFlow::Node n){
    result = n.asParameter().getCallable() or 
    exists(Call call |
      call.getAnArgument() = n.asExpr() |
      result = call.getCallee()
    ) or
    result = n.(DataFlow::InstanceParameterNode).getCallable() or
    result = n.asExpr().getEnclosingCallable()
}
  
/**
 * Just display location of a Node as a string with line an column info:
 *  readObject (Myclass:10:43)
 */
string getSourceLocationInfo(DataFlow::Node n){
    result = getSourceCallable(n) + " (" + n.getEnclosingCallable().getDeclaringType().toString() + ":" + n.getLocation().getStartLine() + ":" + n.getLocation().getStartColumn() + ")"
}

// https://codeql.github.com/docs/ql-language-reference/predicates/#binding-sets
bindingset[regExp]
predicate filterSourcePath(DataFlow::Node n, string regExp){
  n.getLocation().getFile().getAbsolutePath().regexpMatch(regExp)
}
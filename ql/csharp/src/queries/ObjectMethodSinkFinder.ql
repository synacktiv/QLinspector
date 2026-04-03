/**
 * @id synacktiv/csharp/objectmethodsinkfinder
 * @description find new sources for gadget chain based on overridable methods of the Object Type.
 * @name objectmethodsinkfinder
 * @kind path-problem
 * @problem.severity warning
 * @tags security
 */

import csharp
import semmle.code.csharp.dataflow.TaintTracking
private import semmle.code.csharp.security.dataflow.flowsinks.FlowSinks
import GadgetFinder::PathGraph
import libs.Sources as Sources
import libs.GadgetTaintHelpers
import libs.NewtonsoftJson

/**
 * A data flow sink for gadget.
 */
abstract class Sink extends ApiSinkExprNode { }

private module GadgetFinderConfig implements DataFlow::ConfigSig {
  
  predicate isSource(DataFlow::Node source) {
    source instanceof Sources::Source
  }

  predicate isSink(DataFlow::Node sink) {
    sink instanceof Sink
  }

  predicate isAdditionalFlowStep(DataFlow::Node node1, DataFlow::Node node2) {
    any(GadgetAdditionalTaintStep s).step(node1, node2)
  }

  /**
   * We stop return statement if the caller is the source
   * 
   * Thanks @aschackmull
   * cf: https://github.com/github/codeql/discussions/16973#discussioncomment-10050420
   */
  DataFlow::FlowFeature getAFeature() { 
    result instanceof DataFlow::FeatureHasSourceCallContext
  }
}

class ObjectMethodSink extends Sink {
    ObjectMethodSink(){
        exists(Method baseMethod, MethodCall mc |
            baseMethod.getDeclaringType() instanceof ObjectType and
            baseMethod.hasName(["GetHashCode", "ToString", "Equals"]) and
            //baseMethod.hasName("Equals") and
            baseMethod.getACall() = mc and
            mc.getRawArgument(0) = this.asExpr() and
            isGenericType(mc.getRawArgument(0).getType())
        ) or 
        exists(MethodCall mc |
          mc.getTarget().isStatic()  and
          mc.getTarget().getDeclaringType() instanceof ObjectType  and
          mc.getTarget().hasName("Equals") and
          mc.getArgument(1) = this.asExpr() and
          isGenericType(mc.getArgument(1).getType())
        )
    }
}

class IEqualityComparerSink extends Sink {
    IEqualityComparerSink(){
        exists(Method m, MethodCall mc  |
            getASuperType*(mc.getARuntimeTarget().getDeclaringType()).hasFullyQualifiedName(["System.Collections", "System.Collections.Generic"], ["IEqualityComparer", "IEqualityComparer<T>"]) and
            m.hasName(["GetHashCode", "Equals"]) and
            m.getACall() = mc and
            mc.getAnArgument() = this.asExpr() and
            isGenericType(this.asExpr().getType())
        )
    }
}

/*
class EqualOperatorSource extends Sources::Source {
    EqualOperatorSource(){
        exists(Operator o |
            o.getDeclaringType() instanceof JsonSerializableType and
            o.hasName("==") and
            o.getAParameter() = this.asParameter()
        )
    }
}
*/

module GadgetFinder = TaintTracking::Global<GadgetFinderConfig>;

from GadgetFinder::PathNode source, GadgetFinder::PathNode sink
where GadgetFinder::flowPath(source, sink)
select sink.getNode(), source, sink, "Gadget from $@", source.getNode(), getSourceLocationInfo(source.getNode())
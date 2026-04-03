import java
import semmle.code.java.dataflow.FlowSources
import libs.generic.GadgetTaintHelpers
import libs.generic.DangerousMethods as DangerousMethods

/**
 * An abstract class to be overridden by specific gadget hunters.
 */
abstract class GadgetEntryPoint extends DataFlow::Node { }

/**
 * The core logic module that works for ANY GadgetEntryPoint
 */
module GenericGadgetFinderConfig implements DataFlow::ConfigSig {
  predicate isSource(DataFlow::Node source) {
    source instanceof GadgetEntryPoint
  }

  predicate isSink(DataFlow::Node sink) {
    sink instanceof DangerousMethods::Sink
  }

  predicate isAdditionalFlowStep(DataFlow::Node node1, DataFlow::Node node2) {
    any(GadgetAdditionalTaintStep s).step(node1, node2)
  }

  DataFlow::FlowFeature getAFeature() { 
    result instanceof DataFlow::FeatureHasSourceCallContext
  }

  predicate isBarrier(DataFlow::Node node) {
    node instanceof GadgetSanitizer
  }
}

abstract class GadgetSanitizer extends DataFlow::Node { }

/**
 * placeholder for adding sanitizing steps
 * Put it in your ql query.
*/
/*
class GenericGadgetSanitizer extends GadgetSanitizer {
  GenericGadgetSanitizer() {
    none()
  }
}
  */

private class FieldInheritTaint extends DataFlow::FieldContent, TaintInheritingContent {
  FieldInheritTaint() { 
    this.getField().getDeclaringType().getASupertype*() instanceof TypeSerializable 
  }
}

// Instantiate the Global TaintTracking module once
module GadgetFinder = TaintTracking::Global<GenericGadgetFinderConfig>;
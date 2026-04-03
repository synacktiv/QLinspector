import java
import semmle.code.java.dataflow.FlowSinks
import semmle.code.java.security.UnsafeDeserializationQuery

/**
 * A data flow sink for ExpressionEvaluation operations.
 */
abstract class Sink extends ApiSinkNode { }

private class ObjectInputStreamReadObjectMethod extends Method {
  ObjectInputStreamReadObjectMethod() {
    this.getDeclaringType().getASourceSupertype*() instanceof TypeObjectInputStream and
    (this.hasName("readObject") or this.hasName("readUnshared"))
  }
}

/** A sink for unsafe deserialization. */
class UnsafeDeserializationSink extends Sink {
    UnsafeDeserializationSink() {
        exists(MethodCall mc |
            unsafeDeserialization(mc, this.asExpr()) and
            
            // don't want readObject too much false positive
            // keep the others to find bridge gadgets
            not mc.getMethod() instanceof ObjectInputStreamReadObjectMethod
        )
    }
}
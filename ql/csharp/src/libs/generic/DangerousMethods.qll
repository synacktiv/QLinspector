import csharp
import libs.generic.GadgetTaintHelpers
import libs.generic.sinks.RequestForgery as RequestForgery
import libs.generic.sinks.Reflection as Reflection
import libs.generic.sinks.FileOperation as FileOperation
private import semmle.code.csharp.security.dataflow.flowsinks.FlowSinks
private import semmle.code.csharp.dataflow.internal.ExternalFlow
import semmle.code.csharp.security.dataflow.UnsafeDeserializationQuery as UnsafeDeserialization
import semmle.code.csharp.security.dataflow.CodeInjectionQuery as CodeInjection 
import semmle.code.csharp.security.dataflow.CommandInjectionQuery as CommandInjection
import semmle.code.csharp.security.dataflow.TaintedPathQuery as TaintedPath
import semmle.code.csharp.security.dataflow.XMLEntityInjectionQuery as XmlEntityInjection
import semmle.code.csharp.security.dataflow.ResourceInjectionQuery as ResourceInjection

/**
 * A data flow sink for gadget.
 */
abstract class Sink extends ApiSinkExprNode { }

private class ExternalGadgetSink extends Sink {
  ExternalGadgetSink() { sinkNode(this, "gadget-sink") }
}

/**
 * A sink for delegate calls to find more `TypeConfuseDelegate` like gadgets.
 */
class DangerousDelegateSink extends Sink {

  DangerousDelegateSink() {
    exists(DelegateCall dc | this.getExpr() = dc.getARuntimeArgument() |
      dc.getNumberOfRuntimeArguments() > 0 and

      // Every parameter must be string or generic
      forall(Expr arg | arg = dc.getARuntimeArgument() |
        isStringOrGeneric(arg.getType())
      )
    )
  }
}

class DLLImport extends Method {
  DLLImport(){
    this.getAnAttribute().getType().hasName("DllImportAttribute")
  }

  string getLib(){
    exists(Attribute attr | this.getAnAttribute() = attr |
      attr.getConstructorArgument(0).(StringLiteral).getValue().toLowerCase() = result
    )
  }
}

private predicate isDLOpenCall(MethodCall c, string lib, string method) {
  exists(DLLImport m |
    c.getTarget() = m and
    m.getLib() = lib and
    m.getName() = method
  )
}

class LoadLibrarySink extends Sink {
  LoadLibrarySink() {
    exists(MethodCall c | c.getArgument(0) = this.asExpr() |
      (
        // Linux
        isDLOpenCall(c, "libdl.so", "dlopen") or
        
        // Mac
        isDLOpenCall(c, "libSystem.dylib", "dlopen") or
        
        // Windows
        isDLOpenCall(c, "kernel32.dll", "LoadLibrary")
      )
    )
  }
}

/**
 * Sinks stolen from other built-in queries.
 */
class ExternalDangerousSink extends Sink {
  ExternalDangerousSink(){
    this instanceof UnsafeDeserialization::Sink
    or this instanceof CodeInjection::Sink
    or this instanceof CommandInjection::Sink
    // replaced by DangerousFileOperationSink
    //or this instanceof TaintedPath::Sink
    or this instanceof XmlEntityInjection::Sink
    or this instanceof ResourceInjection::Sink
    
    // custom
    or this instanceof RequestForgery::Sink
    or this instanceof Reflection::Sink
    or this instanceof FileOperation::Sink
  }
}
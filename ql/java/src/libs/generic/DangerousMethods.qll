import java
import semmle.code.java.dataflow.FlowSinks
import semmle.code.java.dataflow.ExternalFlow
import semmle.code.java.security.CommandLineQuery as CommandLine
import semmle.code.java.security.LdapInjection as LdapInjection 
import semmle.code.java.security.MvelInjection as MvelInjection 
import semmle.code.java.security.RequestForgery as RequestForgery 
import semmle.code.java.security.TemplateInjection as TemplateInjection
import semmle.code.java.security.XsltInjection as XsltInjection 
import semmle.code.java.security.Xxe as Xxe
import semmle.code.java.security.JndiInjection as JndiInjection
import semmle.code.java.security.OgnlInjection as OgnlInjection
import semmle.code.java.security.QueryInjection as QueryInjection
import semmle.code.java.security.GroovyInjection as GroovyInjection
import semmle.code.java.security.JexlInjectionQuery as JexlInjection
import semmle.code.java.security.SpelInjection as SpelInjection
import semmle.code.java.security.TaintedPathQuery as TaintedPath
import libs.generic.sinks.Jakarta as Jakarta
import libs.generic.sinks.ScriptInjection as ScriptInjection
import libs.generic.sinks.UnsafeDeserialization as UnsafeDeserialization
import libs.generic.Types

/**
 * A data flow sink for gadget.
 */
abstract class Sink extends ApiSinkNode { }

class ExternalGadgetSink extends Sink {
  ExternalGadgetSink() { sinkNode(this, "gadget-sink") }
}

class BeanValidationSink extends Sink {
  BeanValidationSink() { sinkNode(this, "bean-validation") }
}

/**
 * Sinks stolen from other built-in queries.
 */
class ExternalDangerousSink extends Sink {
  ExternalDangerousSink(){
    this instanceof CommandLine::CommandInjectionSink
    or this instanceof LdapInjection::LdapInjectionSink
    or this instanceof MvelInjection::MvelEvaluationSink
    or this instanceof RequestForgery::RequestForgerySink
    or this instanceof TemplateInjection::TemplateInjectionSink
    or this instanceof XsltInjection::XsltInjectionSink
    or this instanceof Xxe::XxeSink
    or this instanceof JndiInjection::JndiInjectionSink
    or this instanceof OgnlInjection::OgnlInjectionSink
    or this instanceof QueryInjection::QueryInjectionSink
    or this instanceof GroovyInjection::GroovyInjectionSink
    or this instanceof JexlInjection::JexlEvaluationSink
    or this instanceof SpelInjection::SpelExpressionEvaluationSink
    or this instanceof TaintedPath::TaintedPathSink

    // custom
    or this instanceof Jakarta::Sink
    or this instanceof ScriptInjection::Sink
    or this instanceof UnsafeDeserialization::Sink
  }
}

/**
 * A call to `java.lang.reflect.Method.invoke`.
 */
class MethodInvokeCall extends MethodCall {
  MethodInvokeCall() { this.getMethod().hasQualifiedName("java.lang.reflect", "Method", "invoke") }
}

/**
 * Unsafe reflection sink (the qualifier or method arguments to `Constructor.newInstance(...)` or `Method.invoke(...)`)
 */
class UnsafeReflectionSink extends Sink {
  UnsafeReflectionSink() {
    exists(MethodCall mc |
      (
        mc.getMethod().hasQualifiedName("java.lang.reflect", "Constructor<>", "newInstance") or
        mc instanceof MethodInvokeCall
      ) and
      this.asExpr() = [mc.getQualifier(), mc.getAnArgument()]
    )
  }
}

class PropertySetterSink extends Sink {
  PropertySetterSink(){
    exists(MethodCall mc |  
      mc.getMethod().hasQualifiedName("java.lang", "System", ["setProperty", "setProperties"]) and
      this.asExpr() = mc.getAnArgument()
    )
  }
}

/**
 * Want to search for BCEL class loader but it's removed in recent java versions
 * https://www.leavesongs.com/penetration/where-is-bcel-classloader.html
 */
class ClassLoaderSink extends Sink {
  ClassLoaderSink(){
    exists(Call c |
      c.getCallee().getDeclaringType().getASupertype*() instanceof TypeClassLoader and
      this.asExpr() = c.getAnArgument() and (
        c instanceof ConstructorCall or
        c.(MethodCall).getMethod().hasName("defineClass")
      )
    )
  }
}

/*
class QuickTestMethods extends Sink {
  QuickTestMethods(){
    exists(MethodCall mc |  
      ma.getMethod().hasName("writeObject")
      this.asExpr() = ma.getAnArgument()
    )
  }
}

*/
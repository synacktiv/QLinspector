import java
import semmle.code.java.dataflow.FlowSinks

/**
 * A data flow sink for ExpressionEvaluation operations.
 */
abstract class Sink extends ApiSinkNode { }

class ExpressionEvaluationSink extends Sink {
  ExpressionEvaluationSink() {
    exists(MethodCall ma, Method m, Expr taintFrom |
      ma.getMethod() = m and taintFrom = this.asExpr()
    |
      m.getDeclaringType() instanceof ValueExpression and
      m.hasName(["getValue", "setValue"]) and
      ma.getQualifier() = taintFrom
      or
      m.getDeclaringType() instanceof MethodExpression and
      m.hasName("invoke") and
      ma.getQualifier() = taintFrom
      or
      m.getDeclaringType() instanceof LambdaExpression and
      m.hasName("invoke") and
      ma.getQualifier() = taintFrom
      or
      m.getDeclaringType() instanceof ELProcessor and
      m.hasName(["eval", "getValue", "setValue"]) and
      ma.getArgument(0) = taintFrom
      or
      m.getDeclaringType() instanceof ELProcessor and
      m.hasName("setVariable") and
      ma.getArgument(1) = taintFrom
    )
  }
}

private class JakartaType extends RefType {
  JakartaType() { this.getPackage().hasName(["javax.el", "jakarta.el"]) }
}

private class ELProcessor extends JakartaType {
  ELProcessor() { this.hasName("ELProcessor") }
}

private class ExpressionFactory extends JakartaType {
  ExpressionFactory() { this.hasName("ExpressionFactory") }
}

private class ValueExpression extends JakartaType {
  ValueExpression() { this.hasName("ValueExpression") }
}

private class MethodExpression extends JakartaType {
  MethodExpression() { this.hasName("MethodExpression") }
}

private class LambdaExpression extends JakartaType {
  LambdaExpression() { this.hasName("LambdaExpression") }
}

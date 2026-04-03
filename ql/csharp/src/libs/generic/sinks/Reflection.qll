/**
 * Provides classes and predicates for reasoning about reflection.
 */

import csharp
import libs.generic.GadgetTaintHelpers
private import semmle.code.csharp.security.dataflow.flowsinks.FlowSinks

/**
 * A data flow sink for reflection.
 */
abstract class Sink extends ApiSinkExprNode { }
/**
 * Sink for reflection operations including property and method calls.
 */
private class ReflectionSink extends Sink {
  ReflectionSink() {
    this.asExpr() = getAReflectionSinkArg()
  }
}

/**
 * Gets an argument that serves as a reflection sink.
 */
private Expr getAReflectionSinkArg() {
  result = getTypeReflectionArg() or
  result = getPropertyInfoArg() or
  result = getPropertyDescriptorCollectionArg() or
  result = getPropertyDescriptorArg() or
  result = getMethodBaseInvokeArg() or
  result = getInvokeMemberArg()
}

/**
 * System.Type.GetProperty/GetMethod/GetMember(string name)
 */
private Expr getTypeReflectionArg() {
  result = getArgFromMethod("System", "Type", ["GetProperty", "GetMethod", "GetMember"], 0)
}

/**
 * System.Reflection.PropertyInfo.GetValue(object obj)
 */
private Expr getPropertyInfoArg() {
  result = getArgFromMethod("System.Reflection", "PropertyInfo", "GetValue", 0)
}

/**
 * System.ComponentModel.PropertyDescriptorCollection.Find(string name, bool ignoreCase)
 */
private Expr getPropertyDescriptorCollectionArg() {
  result = getArgFromMethod("System.ComponentModel", "PropertyDescriptorCollection", "Find", 0)
}

/**
 * System.ComponentModel.PropertyDescriptor.GetValue(object component)
 */
private Expr getPropertyDescriptorArg() {
  result = getArgFromMethod("System.ComponentModel", "PropertyDescriptor", "GetValue", 0)
}

/**
 * System.Reflection.MethodBase.Invoke(object obj, object[] parameters)
 */
private Expr getMethodBaseInvokeArg() {
  result = getArgFromMethod("System.Reflection", "MethodBase", "Invoke", 0)
}

/**
 * System.Type.InvokeMember(string name, BindingFlags, Binder, object target, object[] args, ...)
 */
private Expr getInvokeMemberArg() {
  result = getNamedArgFromMethod("System", "Type", "InvokeMember", ["name", "target", "args"])
}
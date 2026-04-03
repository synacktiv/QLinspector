/**
 * Provides classes and predicates for reasoning about dangerous file operations.
 */

import csharp
import libs.generic.GadgetTaintHelpers
private import semmle.code.csharp.security.dataflow.flowsinks.FlowSinks

/**
 * A data flow sink for dangerous file operations.
 */
abstract class Sink extends ApiSinkExprNode { }

/**
 * Sink for dangerous file operations.
 */
private class DangerousFileOperationSink extends Sink {
  DangerousFileOperationSink() {
    this.asExpr() = getAFileOperationSinkArg()
  }
}

/**
 * Gets an argument that is a sink for file operations.
 */
private Expr getAFileOperationSinkArg() {
  result = getFileMethodSinkArg() or
  result = getFileConstructorSinkArg()
}

private class FileIOType extends RefType {
  FileIOType() {
    this.hasFullyQualifiedName("System.IO",
      ["FileStream", "Stream", "File", "Directory", "BinaryWriter", 
       "MemoryStream", "StreamWriter", "StringWriter", "TextWriter"]
    )
  }
}

/**
 * A method that performs dangerous file operations.
 */
private class DangerousFileMethod extends Method {
  DangerousFileMethod() {
    this.getDeclaringType().getASuperType*() instanceof FileIOType and
    this.getName().matches([
      "%Write%", "%Create%", "%Append%", "%Delete%", 
      "%Open%", "%Replace%", "%Move%", "%Copy%", "Exists"
    ])
  }
  
  /**
   * Gets a parameter name that can be a sink for this method.
   */
  string getASinkParameterName() {
    result = ["path", "buffer", "value", "content", "contents"]
  }
}

/**
 * Gets an argument from a dangerous file method call.
 */
private Expr getFileMethodSinkArg() {
  exists(DangerousFileMethod m |
    result = m.getACall().getArgumentForName(m.getASinkParameterName())
  )
}

/**
 * A FileStream constructor.
 */
private class FileStreamConstructor extends Constructor {
  FileStreamConstructor() {
    this.getDeclaringType().getASuperType*().hasFullyQualifiedName("System.IO", "FileStream")
  }
}

/**
 * Gets the path argument from a FileStream constructor call.
 */
private Expr getFileConstructorSinkArg() {
  exists(FileStreamConstructor ctor |
    result = ctor.getACall().getArgumentForName("path")
  )
}
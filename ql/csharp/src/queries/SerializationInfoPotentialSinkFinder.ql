/**
 * @id synacktiv/csharp/serializationinfopotentialsinkfinder
 * @description find access to potential interesting values in a SerializationInfo stream.
 * @name serializationinfopotentialsinkfinder
 * @kind problem
 * @problem.severity warning
 * @tags security
 */

import csharp

class SerializationInfoPotentialSink extends MethodCall {
  SerializationInfoPotentialSink(){
    exists(Method m, Expr arg |
      this.getTarget() = m and
      m.getName().matches("Get%") and
      m.getDeclaringType().hasFullyQualifiedName("System.Runtime.Serialization", "SerializationInfo") and

      arg = this.getArgumentForName("name") and
      arg.(StringLiteral).getValue().toLowerCase().matches(
        [
          "%path%", "%dir%", "%file%", "%url%", "%uri%", "%drive%",
          "%src%", "%dst%", "%resource%", "%xml%", "%xaml%","%64%",
          "%zip%", "%compressed%", "%code%", "%coding%", "%serial%",
          "%binary%", "%stream%", "%memory%", "%share%", "%conf%",
          "%assembly%"
        ])
    )
  }

  StringLiteral getArgName(){
    result = this.getArgumentForName("name")
  }
}

from SerializationInfoPotentialSink sink, StringLiteral arg
where arg = sink.getArgName()
select sink, "$@ to stream parameter named $@", sink, "Access", arg, sink.getArgName().getValue()
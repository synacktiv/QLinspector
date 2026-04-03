/**
 * @id synacktiv/csharp/sink-to-model
 * @description Transform a sink to a model
 * @name sink-to-model
 * @kind table
 */

import csharp
import libs.generic.DangerousMethods as DangerousMethods
private import semmle.code.csharp.security.dataflow.flowsinks.FlowSinks

class GadgetSink extends Callable {

    Parameter p;

    GadgetSink(){
        (
            this.getDeclaringType().hasFullyQualifiedName("", "") and
            this.hasName("") and
            p = this.getAParameter() and 
            p.hasName("")
        )or
        (
           this.getDeclaringType().hasFullyQualifiedName("", "") and
            this.hasName("") and
            p = this.getParameter(0)
        )
    }

    int getParamPosition(){
        p = this.getParameter(result)
    }

    string getSignature(){
        result = "(" + this.parameterTypesToString() + ")"
    }

    string generateModel(){
        result = "- [\"" +
        this.getDeclaringType().getNamespace().getFullName() + 
        "\", \"" +
        getTypeName(this.getDeclaringType()) +
        "\", True, \"" +
        this.getName() +
        "\", \"" +
        this.getSignature() +
        "\", \"\", " +
        "\"Argument[" + getParamPosition() + "]\", \"gadget-sink\", \"manual\"]"
    }

    private string parameterTypeToString(int i) {
        exists(Parameter param | 
            param = this.getParameter(i) and
            result = getFullTypeName(param.getType())
        )
    }

    language[monotonicAggregates]
    override string parameterTypesToString() {
        result =
        concat(int i | exists(this.getParameter(i)) | this.parameterTypeToString(i), "," order by i)
    }

    string getFullTypeName(ValueOrRefType t) {
      result = t.getNamespace().getFullName() + "." + getTypeName(t)
    }

    /**
     * Returns the type name and handle nested types.
     * Example: Outer+Inner
     */
    string getTypeName(ValueOrRefType t) {
      result = getTypeNameRec(t)
    }

    private string getTypeNameRec(ValueOrRefType t) {
      // Top-level type
      result = t.getName()
        and not t instanceof NestedType

      // Nested type: recurse and append
      or
      result = getTypeNameRec(t.getDeclaringType()) + "+" + t.getName()
        and t instanceof NestedType
    }

}

from GadgetSink s
select s.generateModel()
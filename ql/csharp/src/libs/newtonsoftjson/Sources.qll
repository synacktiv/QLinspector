import csharp
import libs.newtonsoftjson.NewtonsoftJson
import libs.generic.Sources
import semmle.code.csharp.dispatch.OverridableCallable

class NewtonsoftJsonGadgetSource extends GadgetSource {
  NewtonsoftJsonGadgetSource() {
    this = any(JsonSerializableType j).getAnAutomaticCallback()
  }
}

class TypeConverterSource extends Source {
    TypeConverterSource(){
      exists(Method inherited, SerializableType t |
        inherited = getTypeConverterConvertFrom().getInherited(t) and
        this.asParameter() = inherited.getParameter(2)
      )
    }
}

private OverridableCallable getTypeConverterConvertFrom() {
  result.getDeclaringType().hasFullyQualifiedName("System.ComponentModel", "TypeConverter") and
  result.hasName("ConvertFrom")
}
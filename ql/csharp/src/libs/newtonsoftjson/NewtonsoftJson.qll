import csharp
import semmle.code.csharp.serialization.Serialization
import libs.generic.GadgetTaintHelpers

/**
 * A type serializable by Newtonsoft.Json, considering typical Json.NET serialization
 * conventions: public parameterless constructor, [JsonConstructor], or single public constructor.
 */
class JsonSerializableType extends SerializableType {

    // Every type is serializable
    JsonSerializableType() { this.getAConstructor()  instanceof JsonConstructorSerilizationCallBack}
    //JsonSerializableType() { none()}

  /**
   * Json.NET deserialization callbacks, like methods marked with [OnDeserializing]/[OnDeserialized] or constructors.
   */
  override Callable getADeserializationCallback() {
    result instanceof JsonSerilizationCallBack and 
    result.getDeclaringType() = this
  }

  /**
   * Not working, the `SerializableType` type force the return value to be a `Field` 
   * but we can have `Property`.
   * 
   * Json.NET serialized members: properties or fields with common JSON attributes.
   */
  override Field getASerializedField() {
    result.getDeclaringType() = this and
    (
      // Has relevant Json.NET attributes
      result.getAnAttribute().getType().hasName([
        "JsonPropertyAttribute", "JsonDictionaryAttribute", "JsonRequiredAttribute",
        "JsonArrayAttribute", "JsonConverterAttribute", "JsonExtensionDataAttribute",
        "SerializableAttribute",
        "DataMemberAttribute"
      ])
      or
      // Public properties or fields without [JsonIgnore]
      (
        result.isPublic() and 
        not result.getAnAttribute().getType() instanceof NotSerializedAttributeClass
      )
    )
  }
}

/**
 * A constructor marked with the [JsonConstructor] attribute.
 */
class JsonConstructor extends Constructor {
  JsonConstructor() {
    this.(Attributable).getAnAttribute().getType().hasName("JsonConstructorAttribute")
  }
}

/**
 * A method that can be called during Json.net deserialization.
 */
abstract class JsonSerilizationCallBack extends Callable {}

class JsonConstructorSerilizationCallBack extends JsonSerilizationCallBack, Constructor {
    JsonConstructorSerilizationCallBack(){
      this instanceof JsonConstructor
      or
      (
        this.isPublic() and 
        (
          // Public parameterless constructor
          this.getNumberOfParameters() = 0 or

          // Single public constructor with parameters
          strictcount(this.getDeclaringType().getAConstructor()) = 1
        )
      )
    }
}

class JsonDecoratorSerilizationCallBack extends JsonSerilizationCallBack, Method {
    JsonDecoratorSerilizationCallBack(){
        this.(Attributable).getAnAttribute().getType().hasName(["OnDeserializedAttribute", "OnDeserializingAttribute"])
    }
}

class JsonSettersSerilizationCallBack extends JsonSerilizationCallBack, Setter {
    JsonSettersSerilizationCallBack(){
      not this.isStatic() and
      (
        this.isPublic() or
        this.getDeclaration().(Property).getAnAttribute().getType().hasName("JsonPropertyAttribute")
      )
    }
}

/**
 * TODO: is it needed ? I already have DefaultSerializableMember.
 */
class JsonNetSerializableMember extends SerializableMember {
    JsonNetSerializableMember() {
      // declaring type does not need to be serializable
      not this.(Attributable).getAnAttribute().getType() instanceof NotSerializedAttributeClass
    }
  }

/** Any attribute class that marks a member to not be serialized. */
class JsonNetNotSerializedAttributeClass extends NotSerializedAttributeClass {
    JsonNetNotSerializedAttributeClass() {
      this.hasName(["JsonIgnoreAttribute"])
    }
  }
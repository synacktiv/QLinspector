import csharp

/**
 * A concrete type implementing `System.Runtime.Serialization.ISerializationSurrogate`
 * (excludes the interface itself, which reflexively/vacuously matches its own base-type
 * closure and would otherwise show up as a fake "implementation" of itself).
 */
class SerializationSurrogateType extends ValueOrRefType {
  SerializationSurrogateType() {
    this.getABaseType*()
        .hasFullyQualifiedName("System.Runtime.Serialization", "ISerializationSurrogate") and
    not this instanceof Interface
  }
}

/**
 * A concrete type implementing `System.Runtime.Serialization.ISurrogateSelector`
 * (same reflexive-interface exclusion as `SerializationSurrogateType`).
 */
class SurrogateSelectorType extends ValueOrRefType {
  SurrogateSelectorType() {
    this.getABaseType*().hasFullyQualifiedName("System.Runtime.Serialization", "ISurrogateSelector") and
    not this instanceof Interface
  }
}

/**
 * The `SetObjectData` method of a surrogate - the sink a formatter invokes with the
 * attacker-controlled `SerializationInfo` in place of the target object's own
 * deserialization logic.
 */
class SurrogateSetObjectData extends Method {
  SurrogateSetObjectData() {
    this.hasName("SetObjectData") and
    this.getDeclaringType() instanceof SerializationSurrogateType
  }
}

/** A `new XSurrogateSelector(...)` call constructing a `SurrogateSelectorType`. */
class SurrogateSelectorInstantiation extends ObjectCreation {
  SurrogateSelectorInstantiation() { this.getType() instanceof SurrogateSelectorType }
}

import csharp

/**
 * A concrete type implementing `System.Runtime.Serialization.ISerializationSurrogate`
 * (excludes the interface itself, which reflexively/vacuously matches its own base-type
 * closure and would otherwise show up as a fake "implementation" of itself).
 *
 * This also catches the classic public `ActivitySurrogateSelector+ObjectSurrogate`
 * gadget structurally, with no special-casing needed - *if* it's ever resolvable as a
 * type in the database at all. Checked against this exact DB and it isn't (nothing in
 * Veeam's source touches it, so extraction never materializes the nested type as a
 * queryable entity) - see `/mnt/veeam/notes/activity-surrogate-selector-gadget.md`.
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
 *
 * This also catches the classic public `ActivitySurrogateSelector` gadget structurally -
 * confirmed against this exact DB: it resolves (pulled in transitively via a NuGet
 * reference in `Veeam.Common.SerializationRules`'s build) and its declared base types do
 * include `ISurrogateSelector`, so no name-based special case is needed for it either.
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

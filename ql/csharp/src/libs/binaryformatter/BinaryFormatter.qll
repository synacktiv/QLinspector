import csharp
import libs.generic.GadgetTaintHelpers

/** Any attribute class that marks a member to not be serialized. */
class BinaryFormatterNotSerializedAttributeClass extends NotSerializedAttributeClass {
    BinaryFormatterNotSerializedAttributeClass() {
      this.hasName(["NonSerializedAttribute"])
    }
  }
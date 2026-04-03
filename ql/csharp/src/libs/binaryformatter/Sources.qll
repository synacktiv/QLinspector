import csharp
import semmle.code.csharp.serialization.Serialization
import libs.generic.Sources

class BinaryFormatterGadgetSource extends GadgetSource {
  BinaryFormatterGadgetSource() {
    this = any(BinarySerializableType j).getAnAutomaticCallback()
  }
}
import java
import libs.generic.Types
import semmle.code.java.Maps
import semmle.code.java.dataflow.DataFlow


/**
 * A data flow source for a gadget.
 */
abstract class Source extends DataFlow::Node { }
abstract class BeanFactorySource extends DataFlow::Node { }
abstract class CommonsBeanutilsSource extends DataFlow::Node { }

/**
 * A Source method. It's must be implemented by the different Gadget Types (see BinaryFormatter and JSON.Net).
 */
abstract class GadgetSource extends Callable { }

abstract class BeanFactoryGadgetSource extends Callable { }

abstract class CommonsBeanutilsGadgetSource extends Callable { }

class SerilizableType extends Callable {
    SerilizableType(){
        this.getDeclaringType().getASupertype*() instanceof TypeSerializable
    }
}

class MapSource extends GadgetSource, Method, SerilizableType {
    MapSource() {
      this.getDeclaringType().getASupertype*() instanceof MapType and
      this.hasName(["get", "put"])
    }
}

class HashCodeSource extends GadgetSource, Method, SerilizableType {
  HashCodeSource(){this.hasName("hashCode")}
}

class EqualsSource extends GadgetSource, Method, SerilizableType {
  EqualsSource(){this.hasName("equals")}
}

class CompareSource extends GadgetSource, Method, SerilizableType {
    CompareSource() {
        this.getDeclaringType().getASupertype*() instanceof TypeComparator and
        this.hasName(["compare", "compareTo"])
    }
}

class SerializableMethodsSource extends GadgetSource, Method, SerilizableType {
    SerializableMethodsSource() {
        this.getDeclaringType().getASupertype*() instanceof TypeSerializable and
        this.hasName(["readObject", "readObjectNoData", "readResolve"])
    }
}

class ExternalizableMethodSource extends GadgetSource, SerilizableType {
    ExternalizableMethodSource(){
        this.getDeclaringType().getASupertype*() instanceof TypeExternalizable and
        this.hasName("readExternal")
    }
}

class ObjectInputValidationMethodSource extends GadgetSource, SerilizableType {
    ObjectInputValidationMethodSource(){
        this.getDeclaringType().getASupertype*() instanceof TypeObjectInputValidation and
        this.hasName("validateObject")
    }
}

class ObjectMethodSource extends GadgetSource, SerilizableType {
    ObjectMethodSource(){
        this.hasName("finalize") and
        this.getNumberOfParameters() = 0 and
        this.getAThrownExceptionType().hasQualifiedName("java.lang", "Throwable")
    }
}

// Cf CommonsCollections1
class InvocationHandlerMethodSource extends GadgetSource, SerilizableType {
    InvocationHandlerMethodSource(){
        this.getDeclaringType().getASupertype*() instanceof TypeInvocationHandler and
        this.hasName("invoke")
    }
}

class MethodHandlerMethodSource extends GadgetSource, SerilizableType {
    MethodHandlerMethodSource(){
        this.getDeclaringType().getASupertype*() instanceof TypeMethodHandler and
        this.hasName("invoke")
    }
}

class GroovyMethodSource extends GadgetSource, SerilizableType {
    GroovyMethodSource(){
        this.getDeclaringType().getASupertype*() instanceof TypeGroovy and
        this.hasName(["invokeMethod", "invokeConstructor", "invokeStaticMethod"])
    }
}

// from gadget inspector (not tested)

// we can call a getter thanks to CommonBeanUtils1
// https://mogwailabs.de/en/blog/2023/04/look-mama-no-templatesimpl/
class CustomGetterMethodSource extends CommonsBeanutilsGadgetSource, SerilizableType { 
    CustomGetterMethodSource(){
        this.hasNoParameters() and
        this.getName().matches("get%")
    }
}

// Search for new ObjectFactories to replace BeanFactory
class ObjectFactoryMethod extends Callable {
    ObjectFactoryMethod(){
        this.getDeclaringType().getASupertype*() instanceof TypeObjectFactory and
        this.hasName("getObjectInstance")
    }
}

predicate isParameterOf(DataFlow::Node node, Callable gadget) {
    // The 'this' instance inside the gadget method
    node.(DataFlow::InstanceParameterNode).getEnclosingCallable() = gadget
    or
    // Any argument passed to the gadget method (e.g., ObjectInputStream)
    node.asParameter().getCallable() = gadget
}

/**
   * We taint the object that is being deserialized and all it's fields.
   * 
   * In fact the tricks here is that the instance parameter (this) is implicitly
   * passed between method calls. 
   * 
   * Thanks @atorralba
   * cf: https://github.com/github/codeql/discussions/16474
   * 
   * We also taint all the objects that are passed as parameters of the source.
   *  readObject(ObjectInputStream taint){...}
   */
class ParameterSource extends Source {
    ParameterSource() {
        exists(GadgetSource g | isParameterOf(this, g))
    }
}

class BeanFactoryParameterSource extends BeanFactorySource {
    BeanFactoryParameterSource(){
        exists(BeanFactoryGadgetSource g | isParameterOf(this, g))
    }  
}

class CommonsBeanutilsParameterSource extends CommonsBeanutilsSource {
    CommonsBeanutilsParameterSource(){
        exists(CommonsBeanutilsGadgetSource g | isParameterOf(this, g))
    }  
}




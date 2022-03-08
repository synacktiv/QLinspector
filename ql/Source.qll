import java
import semmle.code.java.Maps

class InvocationHandler extends Interface {
  InvocationHandler() { this.hasQualifiedName("java.lang.reflect", "InvocationHandler") }
}

class Comparator extends Interface {
  Comparator() { 
      this.hasQualifiedName("java.util", "Comparator<>") or 
      this.hasQualifiedName("java.util", "Comparator") 
    }
}

class MapClass extends RefType{
    MapClass() {
        getASupertype+() instanceof MapType
    }
}

class ExternalizableType extends RefType {
  ExternalizableType() { hasName("java.io.Externalizable") }
}

class ObjectInputValidationType extends RefType {
  ObjectInputValidationType() { hasName("java.io.ObjectInputValidation") }
}

class MethodHandlerType extends RefType {
  MethodHandlerType() { 
      hasQualifiedName("javassist.util.proxy", "MethodHandler") or
      hasQualifiedName("org.jboss.weld.bean.proxy", "MethodHandler")
    }
}

// from gadget inspector (not tested)
class GroovyType extends RefType {
    GroovyType(){
        hasQualifiedName("org.codehaus.groovy.runtime", "InvokerHelper") or
        hasQualifiedName("groovy.lang", "MetaClass")
    }
}

class ObjectType extends RefType {
  ObjectType() { hasName("java.lang.Object") }
}

class MapSource extends Method {
    MapSource() {
      getDeclaringType().getASupertype*() instanceof MapClass and
      (
        hasName("get") or
        hasName("put")
      )
      
    }
}

class HashCode extends Method {
  HashCode(){
    hasName("hashCode")
  }
}

class Equals extends Method {
  Equals(){
    hasName("equals")
  }
}

class Compare extends Method {
    Compare() {
        getDeclaringType().getASupertype*() instanceof Comparator and
        hasName("compare")
    }
}

class SerializableMethods extends Method {
    SerializableMethods() {
        this.getDeclaringType().getASupertype*() instanceof TypeSerializable and (
            hasName("readObject") or
            hasName("readObjectNoData") or
            hasName("readResolve") 
        )
    }
}

class ExternalizableMethod extends Callable {
    ExternalizableMethod(){
        this.getDeclaringType().getASupertype*() instanceof ExternalizableType and
        hasName("readExternal")
    }
}

class ObjectInputValidationMethod extends Callable {
    ObjectInputValidationMethod(){
        this.getDeclaringType().getASupertype*() instanceof ObjectInputValidationType and
        hasName("validateObject")
    }
}

class ObjectMethod extends Callable {
    ObjectMethod(){
        this.getDeclaringType().getASupertype*() instanceof ObjectType and
        hasName("finalize")
    }
}

class InvocationHandlerMethod extends Callable {
    InvocationHandlerMethod(){
        this.getDeclaringType().getASupertype*() instanceof InvocationHandler and
        hasName("invoke")
    }
}

class MethodHandlerMethod extends Callable {
    MethodHandlerMethod(){
        this.getDeclaringType().getASupertype*() instanceof MethodHandlerType and
        hasName("invoke")
    }
}

// from gadget inspector (not tested)
class GroovyMethod extends Callable {
    GroovyMethod(){
        this.getDeclaringType().getASupertype*() instanceof GroovyType and (
            hasName("invokeMethod") or
            hasName("invokeConstructor") or 
            hasName("invokeStaticMethod")
        )
    }
}


class Source extends Callable{
    Source(){
        getDeclaringType().getASupertype*() instanceof TypeSerializable and (
            this instanceof MapSource or 
            this instanceof SerializableMethods or
            this instanceof Equals or
            this instanceof HashCode or
            this instanceof Compare or
            this instanceof ExternalizableMethod or 
            this instanceof ObjectInputValidationMethod or
            this instanceof InvocationHandlerMethod or
            this instanceof MethodHandlerMethod or
            this instanceof GroovyMethod
        )
    }
}
import java

class TypeComparator extends Interface {
  TypeComparator() { 
      this.hasQualifiedName("java.util", ["Comparator<>", "Comparator"])
    }
}

class TypeInvocationHandler extends Interface {
  TypeInvocationHandler() { this.hasQualifiedName("java.lang.reflect", "InvocationHandler") }
}

class TypeMap extends RefType {
  TypeMap() { this.hasQualifiedName("java.io", "ObjectOutputStream") }
}

class TypeExternalizable extends RefType {
  TypeExternalizable() { this.hasQualifiedName("java.io", "Externalizable") }
}

class TypeObjectInputValidation extends RefType {
  TypeObjectInputValidation() { this.hasQualifiedName("java.io", "ObjectInputValidation")}
}

class TypeMethodHandler extends RefType {
  TypeMethodHandler() { 
      this.hasQualifiedName("javassist.util.proxy", "MethodHandler") or
      this.hasQualifiedName("org.jboss.weld.bean.proxy", "MethodHandler")
    }
}

class TypeGroovy extends RefType {
    TypeGroovy(){
        this.hasQualifiedName("org.codehaus.groovy.runtime", "InvokerHelper") or
        this.hasQualifiedName("groovy.lang", "MetaClass")
    }
}

class TypeObjectFactory extends RefType {
    TypeObjectFactory() { this.hasQualifiedName("javax.naming.spi", "ObjectFactory")}
}

class TypeClassLoader extends RefType {
  TypeClassLoader(){ this.hasQualifiedName("java.lang", "ClassLoader")}
}
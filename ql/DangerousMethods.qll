import java

class JakartaType extends RefType {
  JakartaType() { getPackage().hasName(["javax.el", "jakarta.el"]) }
}

class ELProcessor extends JakartaType {
  ELProcessor() { hasName("ELProcessor") }
}

class NioFiles extends RefType {
  NioFiles() { hasName("java.nio.file.Files") }
}

class ExpressionFactory extends JakartaType {
  ExpressionFactory() { hasName("ExpressionFactory") }
}

class ValueExpression extends JakartaType {
  ValueExpression() { hasName("ValueExpression") }
}

class MethodExpression extends JakartaType {
  MethodExpression() { hasName("MethodExpression") }
}

class LambdaExpression extends JakartaType {
  LambdaExpression() { hasName("LambdaExpression") }
}

class ClassLoaderType extends RefType {
  ClassLoaderType(){ hasQualifiedName("java.lang", "ClassLoader")}
}

class URLClassLoader extends RefType {
  URLClassLoader(){ hasQualifiedName("java.net", "URLClassLoader")}
}

class NamingContext extends RefType {
  NamingContext(){hasQualifiedName("javax.naming", "Context")}
}

class LdapContext extends RefType {
  LdapContext(){hasQualifiedName("com.sun.jndi.ldap", "LdapCtx")}
}

class BeanFactory extends RefType {
  BeanFactory(){ hasQualifiedName("org.springframework.beans.factory", "BeanFactory")}
}

class OGNL extends RefType {
  OGNL(){hasQualifiedName("ognl", "Ognl")}
}

class OgnlValueStack extends RefType {
  OgnlValueStack(){ hasQualifiedName("com.opensymphony.xwork2.ognl", "OgnlValueStack")}
}

class DriverManager extends RefType {
  DriverManager(){hasQualifiedName("java.sql", "DriverManager")}
}


class ExpressionEvaluationMethod extends Method {
    ExpressionEvaluationMethod(){
        getDeclaringType().getASupertype*() instanceof ValueExpression and
        hasName(["getValue", "setValue"]) 
        or
        getDeclaringType().getASupertype*() instanceof MethodExpression and
        hasName("invoke") 
        or
        getDeclaringType().getASupertype*() instanceof LambdaExpression and
        hasName("invoke") 
        or
        getDeclaringType().getASupertype*() instanceof ELProcessor and
        hasName(["eval", "getValue", "setValue"]) 
        or
        getDeclaringType().getASupertype*() instanceof ELProcessor and
        hasName("setVariable")
    }
}

class ReflectionInvocationMethod extends Method {
    ReflectionInvocationMethod(){
        hasQualifiedName("java.lang.reflect", "Method", "invoke")
    }
}

class RuntimeExec extends Method {
  RuntimeExec(){
    hasQualifiedName("java.lang", "Runtime", "exec")
  }
}

class URL extends Method {
  URL(){
    hasQualifiedName("java.net", "URL", "openStream") or
    hasQualifiedName("java.net", "URLConnection", "connect")
  }
}

class ProcessBuilder extends Constructor {
  ProcessBuilder(){
    hasQualifiedName("java.lang", "ProcessBuilder", "ProcessBuilder")
  }
}

class Files extends Method {
  Files(){
    getDeclaringType().getASupertype*() instanceof NioFiles and (
      hasName("newInputStream") or 
      hasName("newOutputStream") or 
      hasName("newBufferedReader") or 
      hasName("newBufferedWriter")
    )

  }
}

class FileInputStream extends Constructor {
  FileInputStream(){
    hasQualifiedName("java.io", "FileInputStream", "FileInputStream")
  }
}

class FileOutputStream extends Constructor {
  FileOutputStream(){
    hasQualifiedName("java.io", "FileOutputStream", "FileOutputStream")
  }
}

// exploring new path, but yield too many false positive
class System extends Method {
  System(){
    hasQualifiedName("java.lang", "System", "setProperty") or
    hasQualifiedName("java.lang", "System", "setProperties")
  }
}

class EvalScriptEngine extends Method {
  EvalScriptEngine(){
    hasQualifiedName("javax.script", "ScriptEngine", "eval")
  }
}

class ClassLoaderMethods extends Callable {
  ClassLoaderMethods(){
    this.getDeclaringType().getASupertype*() instanceof ClassLoaderType and (
      hasName("ClassLoader") or
      hasName("defineClass") or
      hasName("loadClass")
    )
    
  }
}

// remote class loading with these methods can be interesting
class URLClassLoaderMethods extends Callable {
  URLClassLoaderMethods(){
    this.getDeclaringType().getASupertype*() instanceof URLClassLoader and (
        hasName("newInstance") or 
        hasName("URLClassLoader") or
        hasName("MLet")
      )
  }
}

class ClassLoader extends Callable {
  ClassLoader(){
    this instanceof ClassLoaderMethods or
    this instanceof URLClassLoaderMethods
  }
}

 class NamingContextLookup extends Callable {
  NamingContextLookup(){
    this.getDeclaringType().getASupertype*() instanceof NamingContext and (
      hasName("lookup")
    )
  }
}

// https://github.com/voidfyoo/rwctf-2021-old-system/tree/main/writeup
class LdapContextLookup extends Callable {
  LdapContextLookup(){
    this.getDeclaringType().getASupertype*() instanceof LdapContext and (
      hasName("c_lookup")
    )
  }
}

class ContextLookup extends Callable {
  ContextLookup(){
    this instanceof NamingContextLookup or
    this instanceof LdapContextLookup
  }
}

// fixed DefaultListableBeanFactory is not serializable a reference of the BeanFactory 
// is returned which is not known by the server 
class SpringBeansMethods extends Callable {
  SpringBeansMethods(){
    this.getDeclaringType().getASupertype*() instanceof BeanFactory and (
      ( this.hasName("getBean") and this.getNumberOfParameters() = 1)
    )
  }
}

class OGNLEvaluation extends Callable {
  OGNLEvaluation(){
     this.getDeclaringType().getASupertype*() instanceof OgnlValueStack and hasName("findValue") or 
     this.getDeclaringType().getASupertype*() instanceof OGNL and hasName("getValue")
  }
}

class DriverManagerMethods extends Callable {
  DriverManagerMethods(){
    this.getDeclaringType().getASupertype*() instanceof DriverManager and hasName("getConnection")
  }
}

class DangerousMethod extends Callable {
  DangerousMethod(){
    this instanceof ExpressionEvaluationMethod or
    this instanceof ReflectionInvocationMethod or
    this instanceof RuntimeExec or
    this instanceof URL or
    this instanceof ProcessBuilder or 
    this instanceof Files or
    this instanceof FileInputStream or 
    this instanceof FileOutputStream or
    this instanceof EvalScriptEngine or
    this instanceof ClassLoader or
    this instanceof ContextLookup or
    this instanceof OGNLEvaluation or
    this instanceof DriverManagerMethods
    
    //this instanceof SpringBeansMethods
    //this instanceof System
  }

}


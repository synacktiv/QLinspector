/**
 * Provides a taint-tracking configuration for reasoning about user input that is used to construct web queries.
 */

import csharp
private import semmle.code.csharp.security.dataflow.flowsinks.FlowSinks
private import semmle.code.csharp.frameworks.system.Net

/**
 * A data flow sink for server side request forgery vulnerabilities.
 */
abstract class Sink extends ApiSinkExprNode { }

/** The `System.Net.WebClient` class. */
class SystemNetHttpWebClientClass extends SystemNetClass {
  SystemNetHttpWebClientClass() { this.hasName("WebClient") }
}

/** The `System.Net.WebRequest` class. */
class SystemNetHttpWebRequestClass extends SystemNetClass {
  SystemNetHttpWebRequestClass() { this.hasName("WebRequest") }
}

/** The `System.Net.Http` namespace. */
class SystemNetHttpNamespace extends Namespace {
  SystemNetHttpNamespace() {
    this.getParentNamespace() instanceof SystemNetNamespace and
    this.hasName("Http")
  }
}

/** A class in the `System.Net.Http` namespace. */
class SystemNetHttpClass extends Class {
  SystemNetHttpClass() { this.getNamespace() instanceof SystemNetHttpNamespace }
}

/** The `System.Net.Http.HttpClient` class. */
class SystemNetHttpHttpClientClass extends SystemNetHttpClass {
  SystemNetHttpHttpClientClass() { this.hasName("HttpClient") }
}

/**
 * An argument to a `WebRequest.Create` call taken as a
 * sink for Server Side Request Forgery (SSRF) Vulnerabilities.
 */
private class SystemNetWebRequestCreateSink extends Sink {
  SystemNetWebRequestCreateSink() {
    exists(MethodCall call | call.getArgument(0) = this.asExpr() |
      call.getTarget().getDeclaringType() instanceof SystemNetHttpWebRequestClass and
      call.getTarget().hasName("Create")
    )
  }
}

/**
 * An argument to an HTTP Request call of a `System.Net.Http.HttpClient` object
 * taken as a sink for Server Side Request Forgery (SSRF) Vulnerabilities.
 */
private class SystemNetHttpClientSink extends Sink {
  SystemNetHttpClientSink() {
    exists(HttpClientMethod m |
      m.getACall().getArgument(0) = this.asExpr()
    )
  }
}

/**
 * A method on HttpClient that takes a URL as first argument.
 */
private class HttpClientMethod extends Method {
  HttpClientMethod() {
    this.getDeclaringType().getASuperType*() instanceof SystemNetHttpHttpClientClass and
    this.hasName([
      "DeleteAsync", "GetAsync", "GetByteArrayAsync", "GetStreamAsync", "GetStringAsync",
      "PatchAsync", "PostAsync", "PutAsync"
    ])
  }
}

/**
 * A property assignment for BaseAddress.
 */
private class SystemNetClientBaseAddressSink extends Sink {
  SystemNetClientBaseAddressSink() {
    exists(Property p | p.getAnAssignedValue() = this.asExpr() |
      p.hasName("BaseAddress") and
      p.getDeclaringType() instanceof SystemNetHttpClientType
    )
  }
}

/**
 * A type that has a BaseAddress property (WebClient or HttpClient).
 */
private class SystemNetHttpClientType extends Type {
  SystemNetHttpClientType() {
    this instanceof SystemNetHttpWebClientClass or
    this instanceof SystemNetHttpHttpClientClass
  }
}

/**
 * An argument to an HTTP Request call of a `System.Net.WebClient` object
 * taken as a sink for Server Side Request Forgery (SSRF) Vulnerabilities.
 */
private class SystemNetWebClientSink extends Sink {
  SystemNetWebClientSink() {
    exists(WebClientMethod m |
      m.getACall().getArgument(0) = this.asExpr()
    )
  }
}

/**
 * A method on WebClient that takes a URL as first argument.
 */
private class WebClientMethod extends Method {
  WebClientMethod() {
    this.getDeclaringType().getASuperType*() instanceof SystemNetHttpWebClientClass and
    this.hasName([
      "OpenRead", "OpenReadAsync", "DownloadData", "DownloadDataAsync", "DownloadFile",
      "DownloadFileAsync", "DownloadString", "DownloadStringAsync", "OpenWrite", 
      "OpenWriteAsync", "UploadData", "UploadDataAsync", "UploadFile", "UploadFileAsync",
      "UploadValues", "UploadValuesAsync", "UploadString", "UploadStringAsync"
    ])
  }
}
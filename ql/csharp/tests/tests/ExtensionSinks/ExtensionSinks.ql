/**
 * @id synacktiv/csharp/test-extension-sink
 * @description test-extension-sink
 * @name test-extension-sink
 * @kind problem
 */

import csharp

private import semmle.code.csharp.security.dataflow.flowsinks.FlowSinks
private import semmle.code.csharp.dataflow.internal.ExternalFlow

abstract class Sink extends ApiSinkExprNode { }

private class ExternalGadgetSink extends Sink {
  ExternalGadgetSink() { sinkNode(this, "gadget-sink") }
  
}

from ExternalGadgetSink s
select s
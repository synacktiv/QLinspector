/**
 * @id synacktiv/csharp/surrogateselectorinstantiationfinder
 * @description find every place a System.Runtime.Serialization.ISurrogateSelector
 *              implementation is instantiated - i.e. where a formatter's
 *              SurrogateSelector becomes wired up, reachable, and worth tracing to see
 *              what BinaryFormatter instance (if any) it ends up attached to.
 * @name surrogateselectorinstantiationfinder
 * @kind problem
 * @problem.severity warning
 * @tags security
 */

import csharp
import libs.generic.Surrogates

from SurrogateSelectorInstantiation instantiation, SurrogateSelectorType selectorType
where selectorType = instantiation.getType()
select instantiation, "Surrogate selector $@ instantiated here", selectorType,
  selectorType.getName()

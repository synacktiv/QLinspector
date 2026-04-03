/**
 * @id synacktiv/csharp/dangeroustypefinder
 * @description find types that are sub type of a dangerous type.
 * @name dangeroustypefinder
 * @kind problem
 * @problem.severity warning
 * @tags security
 */

import csharp
import libs.KnownGadgets
import libs.GadgetTaintHelpers

from RefType t, RefType dangerousType
where 
  dangerousType instanceof KnownDangerousType and
  dangerousType.getASubType() = t and
  t.getAnAttribute().getType() instanceof SerializedAttributeClass
select t, "Type $@ is a sub type of dangerous type $@", t, t.getName(), dangerousType, dangerousType.getName()
/**
 * @id synacktiv/java/qlinspectorold
 * @description find regular Java gadget chains
 * @name QLInspector
 * @kind path-problem
 * @problem.severity error
 * @tags security
 */

import java
import libs.generic.DangerousMethods
import libs.generic.Source
import libs.generic.GadgetHelpers


/*
================ find sink  ================
from Callable c0,  DangerousExpression de
where c0 instanceof RecursiveCallToDangerousMethod and
de.getEnclosingCallable() = c0
select c0, de

================ find source  ===============
from Callable c0
where c0 instanceof RecursiveCallToDangerousMethod and
c0 instanceof GadgetSource
select c0
*/

from Callable c0,  Callable c1, DangerousExpression de
where c0 instanceof GadgetSource and
findGadgetChain(c0, c1, de)
select c0, c0, c1, "recursive call to dangerous expression $@", de, de.toString()
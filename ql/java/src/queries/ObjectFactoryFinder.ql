/**
 * @id synacktiv/java/objectfactoryfinder
 * @description find new ObjectFactory for JNDI exploitation
 * @name ObjectFactoryFinder
 * @kind problem
 * @problem.severity warning
 * @tags security
 */

import java
import libs.generic.Source

from ObjectFactoryMethod objFactoryMethod
select objFactoryMethod, "Found ObjectFactory: " + objFactoryMethod.getDeclaringType().toString()
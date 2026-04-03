/**
 * @id synacktiv/java/shaded-xalan
 * @name Shaded Xalan classes finder
 * @description Identifies shaded copies of Xalan classes (like TemplatesImpl) 
 * that may bypass JPMS restrictions.
 * @kind problem
 * @problem.severity warning
 */

import java

predicate isXalanClassName(string name) {
  name in [
    "TemplatesImpl", 
    "AbstractTranslet", 
    "TransletClassLoader", 
    "TransformerFactoryImpl"
  ]
}

predicate isOriginalXalanPackage(Package p) {
  p.getName().matches("com.sun.org.apache.xalan.internal%")
}

from Class c
where
  // Look for the specific class names used in the gadget chain
  isXalanClassName(c.getName()) and
  
  // Ensure it's NOT the original JDK internal package
  not isOriginalXalanPackage(c.getPackage())
  
select c, "Found shaded Xalan class '" + c.getName() + "' in package '" + c.getPackage().getName() + "'. This may bypass JPMS gadget protections."
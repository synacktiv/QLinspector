/**
 * @id synacktiv/java/qlinspector-beanfactory
 * @description find sinks for BeanFactory gadget chain
 * @name BeanFactoryGadgetFinder
 * @kind path-problem
 * @problem.severity error
 * @tags security
 */

import java
import libs.generic.GenericGadgetFinderConfig
import libs.generic.Source as Sources
import GadgetFinder::PathGraph

class Source extends GadgetEntryPoint {
    Source() { this instanceof Sources::BeanFactorySource }
}

from GadgetFinder::PathNode source, GadgetFinder::PathNode sink
where GadgetFinder::flowPath(source, sink)
select sink.getNode(), source, sink, "Gadget from $@", source.getNode(), getSourceLocationInfo(source.getNode())
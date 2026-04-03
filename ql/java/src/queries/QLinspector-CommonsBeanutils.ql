/**
 * @id synacktiv/java/qlinspector-commonsbeanutils
 * @description find sinks for CommonsBeanutils gadget chain
 * @name CommonsBeanutilsGadgetFinder
 * @kind path-problem
 * @problem.severity error
 * @tags security
 */

import java
import libs.generic.GenericGadgetFinderConfig
import libs.generic.Source as Sources
import GadgetFinder::PathGraph

class Source extends GadgetEntryPoint {
    Source() { this instanceof Sources::CommonsBeanutilsSource }
}

from GadgetFinder::PathNode source, GadgetFinder::PathNode sink
where GadgetFinder::flowPath(source, sink)
select sink.getNode(), source, sink, "Gadget from $@", source.getNode(), getSourceLocationInfo(source.getNode())
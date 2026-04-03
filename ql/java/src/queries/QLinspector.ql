/**
 * @id synacktiv/java/qlinspector
 * @description find regular Java gadget chains
 * @name Java deserialization gadget finder
 * @kind path-problem
 * @problem.severity warning
 * @tags security
 */

import java
import libs.generic.GenericGadgetFinderConfig
import libs.generic.Source as Sources
import GadgetFinder::PathGraph

class Source extends GadgetEntryPoint {
    Source() { this instanceof Sources::Source }
}

from GadgetFinder::PathNode source, GadgetFinder::PathNode sink
where GadgetFinder::flowPath(source, sink)
select sink.getNode(), source, sink, "Gadget from $@", source.getNode(), getSourceLocationInfo(source.getNode())
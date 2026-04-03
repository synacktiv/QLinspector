/**
 * @id synacktiv/csharp/qlinspector-newtonsoftjson
 * @description find C# gadget chains for newtonsoftjson
 * @name qlinspector-newtonsoftjson
 * @kind path-problem
 * @problem.severity warning
 * @tags security
 */

import csharp
import libs.generic.GenericGadgetFinderConfig
import GadgetFinder::PathGraph
import libs.generic.Sources as Sources
import libs.newtonsoftjson.Sources

class Source extends GadgetEntryPoint {
    Source() { 
      this instanceof Sources::Source
      //and filterSourcePath(this, [".*/my/path/.*", ".*/second/path/"])
    }
}

from GadgetFinder::PathNode source, GadgetFinder::PathNode sink
where GadgetFinder::flowPath(source, sink)
select sink.getNode(), source, sink, "Gadget from $@", source.getNode(), getSourceLocationInfo(source.getNode())

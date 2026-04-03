import csharp
private import semmle.code.csharp.dataflow.internal.DataFlowPrivate as DataFlowPrivate
import semmle.code.csharp.dispatch.OverridableCallable
import semmle.code.csharp.serialization.Serialization

/**
 * A data flow source for a gadget.
 */
abstract class Source extends DataFlow::Node { }

/**
 * A Source method. It's must be implemented by the different Gadget Types (see BinaryFormatter and JSON.Net).
 */
abstract class GadgetSource extends Callable { }

class ParameterSource extends Source {
    ParameterSource(){
        this.asParameter().getCallable() instanceof GadgetSource or
        this.(DataFlowPrivate::InstanceParameterNode).getCallable(_) instanceof GadgetSource
    }  
}

/*
class TmpMemberAccess extends Source {
    TmpMemberAccess(){
        this.asExpr() instanceof GadgetSourceAssignableMemberAccess
    }  
}
*/

/**
 * In his research: https://github.com/thezdi/presentations/blob/main/2023_Hexacon/whitepaper-net-deser.pdf
 * @chudyPB has identified several deserialization gadgets that can be activated by an arbitrary getter call like:
 *  - SecurityException
 *  - SettingsPropertyValue
 */
/*
class GetterSource extends Source {
    GetterSource(){
        this.(DataFlowPrivate::InstanceParameterNode).getCallable(_) instanceof Getter
    }
}
*/

/**
 * For now it's ok since it's valid for JSON.Net and BinaryFormatter.
 */
class ObjectMethodSource extends Source {
    ObjectMethodSource(){
        exists(Callable inherited, SerializableType t |
            inherited = getObjectMethod().getInherited(t) and
            this.(DataFlowPrivate::InstanceParameterNode).getCallable(_) = inherited
        )
    }
}

private OverridableCallable getObjectMethod() {
  result.getDeclaringType() instanceof ObjectType and
  result.hasName(["ToString", "GetHashCode", "Equals"])
}
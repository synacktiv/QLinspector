/**
 * @id synacktiv/csharp/surrogatesetobjectdatafinder
 * @description find all ISerializationSurrogate implementations and report their
 *              SetObjectData method - the method a formatter calls instead of the
 *              target type's own (de)serialization logic once GetSurrogate selects it.
 * @name surrogatesetobjectdatafinder
 * @kind problem
 * @problem.severity warning
 * @tags security
 */

import csharp
import libs.generic.Surrogates

from SurrogateSetObjectData setObjectData, SerializationSurrogateType surrogate
where surrogate = setObjectData.getDeclaringType()
select setObjectData, "Surrogate class $@ implements ISerializationSurrogate.SetObjectData $@",
  surrogate, surrogate.getName(), setObjectData, setObjectData.getName()

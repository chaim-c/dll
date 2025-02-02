using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000047 RID: 71
	internal class SbyteBasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x06000261 RID: 609 RVA: 0x0000A99B File Offset: 0x00008B9B
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			writer.WriteSByte((sbyte)value);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A9A9 File Offset: 0x00008BA9
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			return reader.ReadSByte();
		}
	}
}

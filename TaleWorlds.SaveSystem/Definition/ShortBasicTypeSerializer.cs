using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000044 RID: 68
	internal class ShortBasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x06000258 RID: 600 RVA: 0x0000A932 File Offset: 0x00008B32
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			writer.WriteShort((short)value);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000A940 File Offset: 0x00008B40
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			return reader.ReadShort();
		}
	}
}

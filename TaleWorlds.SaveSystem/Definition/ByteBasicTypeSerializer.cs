using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000046 RID: 70
	internal class ByteBasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x0600025E RID: 606 RVA: 0x0000A978 File Offset: 0x00008B78
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			writer.WriteByte((byte)value);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000A986 File Offset: 0x00008B86
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			return reader.ReadByte();
		}
	}
}

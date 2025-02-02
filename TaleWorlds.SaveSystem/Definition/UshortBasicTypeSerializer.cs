using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000045 RID: 69
	internal class UshortBasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x0600025B RID: 603 RVA: 0x0000A955 File Offset: 0x00008B55
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			writer.WriteUShort((ushort)value);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000A963 File Offset: 0x00008B63
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			return reader.ReadUShort();
		}
	}
}

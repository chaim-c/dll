using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000043 RID: 67
	internal class UintBasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x06000255 RID: 597 RVA: 0x0000A90F File Offset: 0x00008B0F
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			writer.WriteUInt((uint)value);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000A91D File Offset: 0x00008B1D
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			return reader.ReadUInt();
		}
	}
}

using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x0200004B RID: 75
	internal class UlongBasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x0600026D RID: 621 RVA: 0x0000AA27 File Offset: 0x00008C27
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			writer.WriteULong((ulong)value);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000AA35 File Offset: 0x00008C35
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			return reader.ReadULong();
		}
	}
}

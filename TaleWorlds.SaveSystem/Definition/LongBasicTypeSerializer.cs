using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x0200004A RID: 74
	internal class LongBasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x0600026A RID: 618 RVA: 0x0000AA04 File Offset: 0x00008C04
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			writer.WriteLong((long)value);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000AA12 File Offset: 0x00008C12
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			return reader.ReadLong();
		}
	}
}

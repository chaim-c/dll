using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000056 RID: 86
	internal class StringSerializer : IBasicTypeSerializer
	{
		// Token: 0x0600028E RID: 654 RVA: 0x0000AD8F File Offset: 0x00008F8F
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000AD91 File Offset: 0x00008F91
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			return null;
		}
	}
}

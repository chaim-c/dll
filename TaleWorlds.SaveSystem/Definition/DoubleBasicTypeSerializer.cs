using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000049 RID: 73
	internal class DoubleBasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x06000267 RID: 615 RVA: 0x0000A9E1 File Offset: 0x00008BE1
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			writer.WriteDouble((double)value);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000A9EF File Offset: 0x00008BEF
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			return reader.ReadDouble();
		}
	}
}

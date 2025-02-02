using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000054 RID: 84
	internal class ColorBasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x06000288 RID: 648 RVA: 0x0000AD3C File Offset: 0x00008F3C
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			Color value2 = (Color)value;
			writer.WriteColor(value2);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000AD57 File Offset: 0x00008F57
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			return reader.ReadColor();
		}
	}
}

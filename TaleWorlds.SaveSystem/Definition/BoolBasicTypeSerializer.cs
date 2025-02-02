using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000055 RID: 85
	internal class BoolBasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x0600028B RID: 651 RVA: 0x0000AD6C File Offset: 0x00008F6C
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			writer.WriteBool((bool)value);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000AD7A File Offset: 0x00008F7A
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			return reader.ReadBool();
		}
	}
}

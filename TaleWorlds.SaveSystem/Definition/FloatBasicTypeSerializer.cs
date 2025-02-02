using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000048 RID: 72
	internal class FloatBasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x06000264 RID: 612 RVA: 0x0000A9BE File Offset: 0x00008BBE
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			writer.WriteFloat((float)value);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000A9CC File Offset: 0x00008BCC
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			return reader.ReadFloat();
		}
	}
}

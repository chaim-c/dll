using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000042 RID: 66
	internal class IntBasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x06000252 RID: 594 RVA: 0x0000A8EC File Offset: 0x00008AEC
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			writer.WriteInt((int)value);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000A8FA File Offset: 0x00008AFA
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			return reader.ReadInt();
		}
	}
}

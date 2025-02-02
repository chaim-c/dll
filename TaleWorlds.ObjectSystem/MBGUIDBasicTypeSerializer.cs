using System;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Definition;

namespace TaleWorlds.ObjectSystem
{
	// Token: 0x02000012 RID: 18
	internal class MBGUIDBasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x0600008A RID: 138 RVA: 0x0000450C File Offset: 0x0000270C
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			writer.WriteUInt(((MBGUID)value).InternalValue);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000452D File Offset: 0x0000272D
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			return new MBGUID(reader.ReadUInt());
		}
	}
}

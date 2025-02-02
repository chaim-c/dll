using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x0200004F RID: 79
	internal class Vec3iBasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x06000279 RID: 633 RVA: 0x0000AB0C File Offset: 0x00008D0C
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			Vec3i vec = (Vec3i)value;
			writer.WriteVec3Int(vec);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000AB27 File Offset: 0x00008D27
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			return reader.ReadVec3Int();
		}
	}
}

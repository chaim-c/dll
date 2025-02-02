using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x0200004E RID: 78
	internal class Vec3BasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x06000276 RID: 630 RVA: 0x0000AADC File Offset: 0x00008CDC
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			Vec3 vec = (Vec3)value;
			writer.WriteVec3(vec);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000AAF7 File Offset: 0x00008CF7
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			return reader.ReadVec3();
		}
	}
}

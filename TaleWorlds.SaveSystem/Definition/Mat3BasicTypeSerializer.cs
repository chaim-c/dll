using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000051 RID: 81
	internal class Mat3BasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x0600027F RID: 639 RVA: 0x0000ABB0 File Offset: 0x00008DB0
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			Mat3 mat = (Mat3)value;
			writer.WriteVec3(mat.s);
			writer.WriteVec3(mat.f);
			writer.WriteVec3(mat.u);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			Vec3 s = reader.ReadVec3();
			Vec3 f = reader.ReadVec3();
			Vec3 u = reader.ReadVec3();
			return new Mat3(s, f, u);
		}
	}
}

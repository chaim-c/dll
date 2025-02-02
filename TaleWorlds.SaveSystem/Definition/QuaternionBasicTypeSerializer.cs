using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000053 RID: 83
	internal class QuaternionBasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x06000285 RID: 645 RVA: 0x0000ACB8 File Offset: 0x00008EB8
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			Quaternion quaternion = (Quaternion)value;
			writer.WriteFloat(quaternion.X);
			writer.WriteFloat(quaternion.Y);
			writer.WriteFloat(quaternion.Z);
			writer.WriteFloat(quaternion.W);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000ACFC File Offset: 0x00008EFC
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			float x = reader.ReadFloat();
			float y = reader.ReadFloat();
			float z = reader.ReadFloat();
			float w = reader.ReadFloat();
			return new Quaternion(x, y, z, w);
		}
	}
}

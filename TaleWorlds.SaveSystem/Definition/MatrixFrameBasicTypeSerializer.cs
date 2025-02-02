using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000052 RID: 82
	internal class MatrixFrameBasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x06000282 RID: 642 RVA: 0x0000AC20 File Offset: 0x00008E20
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			MatrixFrame matrixFrame = (MatrixFrame)value;
			writer.WriteVec3(matrixFrame.origin);
			writer.WriteVec3(matrixFrame.rotation.s);
			writer.WriteVec3(matrixFrame.rotation.f);
			writer.WriteVec3(matrixFrame.rotation.u);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000AC74 File Offset: 0x00008E74
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			Vec3 o = reader.ReadVec3();
			Vec3 f = reader.ReadVec3();
			Vec3 s = reader.ReadVec3();
			Vec3 u = reader.ReadVec3();
			return new MatrixFrame(new Mat3(s, f, u), o);
		}
	}
}

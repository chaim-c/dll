using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000050 RID: 80
	internal class Mat2BasicTypeSerializer : IBasicTypeSerializer
	{
		// Token: 0x0600027C RID: 636 RVA: 0x0000AB3C File Offset: 0x00008D3C
		void IBasicTypeSerializer.Serialize(IWriter writer, object value)
		{
			Mat2 mat = (Mat2)value;
			writer.WriteVec2(mat.s);
			writer.WriteVec2(mat.f);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000AB68 File Offset: 0x00008D68
		object IBasicTypeSerializer.Deserialize(IReader reader)
		{
			Vec2 vec = reader.ReadVec2();
			Vec2 vec2 = reader.ReadVec2();
			return new Mat2(vec.x, vec.y, vec2.x, vec2.y);
		}
	}
}

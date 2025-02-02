using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000014 RID: 20
	public struct FogInformation
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002A65 File Offset: 0x00000C65
		public void DeserializeFrom(IReader reader)
		{
			this.Density = reader.ReadFloat();
			this.Color = reader.ReadVec3();
			this.Falloff = reader.ReadFloat();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A8B File Offset: 0x00000C8B
		public void SerializeTo(IWriter writer)
		{
			writer.WriteFloat(this.Density);
			writer.WriteVec3(this.Color);
			writer.WriteFloat(this.Falloff);
		}

		// Token: 0x04000038 RID: 56
		public float Density;

		// Token: 0x04000039 RID: 57
		public Vec3 Color;

		// Token: 0x0400003A RID: 58
		public float Falloff;
	}
}

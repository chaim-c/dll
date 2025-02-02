using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000012 RID: 18
	public struct AmbientInformation
	{
		// Token: 0x0600003E RID: 62 RVA: 0x000029E5 File Offset: 0x00000BE5
		public void DeserializeFrom(IReader reader)
		{
			this.EnvironmentMultiplier = reader.ReadFloat();
			this.AmbientColor = reader.ReadVec3();
			this.MieScatterStrength = reader.ReadFloat();
			this.RayleighConstant = reader.ReadFloat();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A17 File Offset: 0x00000C17
		public void SerializeTo(IWriter writer)
		{
			writer.WriteFloat(this.EnvironmentMultiplier);
			writer.WriteVec3(this.AmbientColor);
			writer.WriteFloat(this.MieScatterStrength);
			writer.WriteFloat(this.RayleighConstant);
		}

		// Token: 0x04000033 RID: 51
		public float EnvironmentMultiplier;

		// Token: 0x04000034 RID: 52
		public Vec3 AmbientColor;

		// Token: 0x04000035 RID: 53
		public float MieScatterStrength;

		// Token: 0x04000036 RID: 54
		public float RayleighConstant;
	}
}

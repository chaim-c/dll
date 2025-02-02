using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000017 RID: 23
	public struct PostProcessInformation
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002B61 File Offset: 0x00000D61
		public void DeserializeFrom(IReader reader)
		{
			this.MinExposure = reader.ReadFloat();
			this.MaxExposure = reader.ReadFloat();
			this.BrightpassThreshold = reader.ReadFloat();
			this.MiddleGray = reader.ReadFloat();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B93 File Offset: 0x00000D93
		public void SerializeTo(IWriter writer)
		{
			writer.WriteFloat(this.MinExposure);
			writer.WriteFloat(this.MaxExposure);
			writer.WriteFloat(this.BrightpassThreshold);
			writer.WriteFloat(this.MiddleGray);
		}

		// Token: 0x04000042 RID: 66
		public float MinExposure;

		// Token: 0x04000043 RID: 67
		public float MaxExposure;

		// Token: 0x04000044 RID: 68
		public float BrightpassThreshold;

		// Token: 0x04000045 RID: 69
		public float MiddleGray;
	}
}

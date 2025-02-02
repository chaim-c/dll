using System;
using System.Runtime.InteropServices;

namespace TaleWorlds.Library
{
	// Token: 0x02000018 RID: 24
	public struct AtmosphereInfo
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002BC5 File Offset: 0x00000DC5
		public bool IsValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.AtmosphereName);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public static AtmosphereInfo GetInvalidAtmosphereInfo()
		{
			return new AtmosphereInfo
			{
				AtmosphereName = ""
			};
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002BFC File Offset: 0x00000DFC
		public void DeserializeFrom(IReader reader)
		{
			this.SunInfo.DeserializeFrom(reader);
			this.RainInfo.DeserializeFrom(reader);
			this.SnowInfo.DeserializeFrom(reader);
			this.AmbientInfo.DeserializeFrom(reader);
			this.FogInfo.DeserializeFrom(reader);
			this.SkyInfo.DeserializeFrom(reader);
			this.TimeInfo.DeserializeFrom(reader);
			this.AreaInfo.DeserializeFrom(reader);
			this.PostProInfo.DeserializeFrom(reader);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002C78 File Offset: 0x00000E78
		public void SerializeTo(IWriter writer)
		{
			this.SunInfo.SerializeTo(writer);
			this.RainInfo.SerializeTo(writer);
			this.SnowInfo.SerializeTo(writer);
			this.AmbientInfo.SerializeTo(writer);
			this.FogInfo.SerializeTo(writer);
			this.SkyInfo.SerializeTo(writer);
			this.TimeInfo.SerializeTo(writer);
			this.AreaInfo.SerializeTo(writer);
			this.PostProInfo.SerializeTo(writer);
		}

		// Token: 0x04000046 RID: 70
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string AtmosphereName;

		// Token: 0x04000047 RID: 71
		public SunInformation SunInfo;

		// Token: 0x04000048 RID: 72
		public RainInformation RainInfo;

		// Token: 0x04000049 RID: 73
		public SnowInformation SnowInfo;

		// Token: 0x0400004A RID: 74
		public AmbientInformation AmbientInfo;

		// Token: 0x0400004B RID: 75
		public FogInformation FogInfo;

		// Token: 0x0400004C RID: 76
		public SkyInformation SkyInfo;

		// Token: 0x0400004D RID: 77
		public TimeInformation TimeInfo;

		// Token: 0x0400004E RID: 78
		public AreaInformation AreaInfo;

		// Token: 0x0400004F RID: 79
		public PostProcessInformation PostProInfo;

		// Token: 0x04000050 RID: 80
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string InterpolatedAtmosphereName;
	}
}

using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000016 RID: 22
	public struct AreaInformation
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002B2D File Offset: 0x00000D2D
		public void DeserializeFrom(IReader reader)
		{
			this.Temperature = reader.ReadFloat();
			this.Humidity = reader.ReadFloat();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B47 File Offset: 0x00000D47
		public void SerializeTo(IWriter writer)
		{
			writer.WriteFloat(this.Temperature);
			writer.WriteFloat(this.Humidity);
		}

		// Token: 0x04000040 RID: 64
		public float Temperature;

		// Token: 0x04000041 RID: 65
		public float Humidity;
	}
}

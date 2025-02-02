using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000011 RID: 17
	public struct SnowInformation
	{
		// Token: 0x0600003C RID: 60 RVA: 0x000029C9 File Offset: 0x00000BC9
		public void DeserializeFrom(IReader reader)
		{
			this.Density = reader.ReadFloat();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000029D7 File Offset: 0x00000BD7
		public void SerializeTo(IWriter writer)
		{
			writer.WriteFloat(this.Density);
		}

		// Token: 0x04000032 RID: 50
		public float Density;
	}
}

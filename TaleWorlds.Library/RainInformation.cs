using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000010 RID: 16
	public struct RainInformation
	{
		// Token: 0x0600003A RID: 58 RVA: 0x000029AD File Offset: 0x00000BAD
		public void DeserializeFrom(IReader reader)
		{
			this.Density = reader.ReadFloat();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000029BB File Offset: 0x00000BBB
		public void SerializeTo(IWriter writer)
		{
			writer.WriteFloat(this.Density);
		}

		// Token: 0x04000031 RID: 49
		public float Density;
	}
}

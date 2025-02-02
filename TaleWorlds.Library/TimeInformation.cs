using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000015 RID: 21
	public struct TimeInformation
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002AB1 File Offset: 0x00000CB1
		public void DeserializeFrom(IReader reader)
		{
			this.TimeOfDay = reader.ReadFloat();
			this.NightTimeFactor = reader.ReadFloat();
			this.DrynessFactor = reader.ReadFloat();
			this.WinterTimeFactor = reader.ReadFloat();
			this.Season = reader.ReadInt();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002AEF File Offset: 0x00000CEF
		public void SerializeTo(IWriter writer)
		{
			writer.WriteFloat(this.TimeOfDay);
			writer.WriteFloat(this.NightTimeFactor);
			writer.WriteFloat(this.DrynessFactor);
			writer.WriteFloat(this.WinterTimeFactor);
			writer.WriteInt(this.Season);
		}

		// Token: 0x0400003B RID: 59
		public float TimeOfDay;

		// Token: 0x0400003C RID: 60
		public float NightTimeFactor;

		// Token: 0x0400003D RID: 61
		public float DrynessFactor;

		// Token: 0x0400003E RID: 62
		public float WinterTimeFactor;

		// Token: 0x0400003F RID: 63
		public int Season;
	}
}

using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000013 RID: 19
	public struct SkyInformation
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002A49 File Offset: 0x00000C49
		public void DeserializeFrom(IReader reader)
		{
			this.Brightness = reader.ReadFloat();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A57 File Offset: 0x00000C57
		public void SerializeTo(IWriter writer)
		{
			writer.WriteFloat(this.Brightness);
		}

		// Token: 0x04000037 RID: 55
		public float Brightness;
	}
}

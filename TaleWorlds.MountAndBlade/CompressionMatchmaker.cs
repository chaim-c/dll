using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002F0 RID: 752
	public static class CompressionMatchmaker
	{
		// Token: 0x04000F6F RID: 3951
		public static CompressionInfo.Integer KillDeathAssistCountCompressionInfo = new CompressionInfo.Integer(-1000, 100000, true);

		// Token: 0x04000F70 RID: 3952
		public static CompressionInfo.Float MissionTimeCompressionInfo = new CompressionInfo.Float(-5f, 86400f, 20);

		// Token: 0x04000F71 RID: 3953
		public static CompressionInfo.Float MissionTimeLowPrecisionCompressionInfo = new CompressionInfo.Float(-5f, 12, 4f);

		// Token: 0x04000F72 RID: 3954
		public static CompressionInfo.Integer MissionCurrentStateCompressionInfo = new CompressionInfo.Integer(0, 6);

		// Token: 0x04000F73 RID: 3955
		public static CompressionInfo.Integer ScoreCompressionInfo = new CompressionInfo.Integer(-1000000, 21);
	}
}

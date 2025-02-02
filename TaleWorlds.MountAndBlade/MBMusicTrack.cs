using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001D2 RID: 466
	public struct MBMusicTrack
	{
		// Token: 0x06001A8C RID: 6796 RVA: 0x0005CF03 File Offset: 0x0005B103
		public MBMusicTrack(MBMusicTrack obj)
		{
			this.index = obj.index;
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x0005CF11 File Offset: 0x0005B111
		internal MBMusicTrack(int i)
		{
			this.index = i;
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001A8E RID: 6798 RVA: 0x0005CF1A File Offset: 0x0005B11A
		private bool IsValid
		{
			get
			{
				return this.index >= 0;
			}
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x0005CF28 File Offset: 0x0005B128
		public bool Equals(MBMusicTrack obj)
		{
			return this.index == obj.index;
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0005CF38 File Offset: 0x0005B138
		public override int GetHashCode()
		{
			return this.index;
		}

		// Token: 0x04000845 RID: 2117
		private int index;
	}
}

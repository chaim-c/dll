using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001D9 RID: 473
	public struct MBSoundTrack
	{
		// Token: 0x06001AA8 RID: 6824 RVA: 0x0005D177 File Offset: 0x0005B377
		internal MBSoundTrack(int i)
		{
			this.index = i;
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x0005D180 File Offset: 0x0005B380
		public bool Equals(MBSoundTrack a)
		{
			return this.index == a.index;
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0005D190 File Offset: 0x0005B390
		public override int GetHashCode()
		{
			return this.index;
		}

		// Token: 0x0400085D RID: 2141
		private int index;
	}
}

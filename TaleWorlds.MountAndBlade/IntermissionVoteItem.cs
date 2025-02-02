using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002F2 RID: 754
	public class IntermissionVoteItem
	{
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060028EB RID: 10475 RVA: 0x0009D7AB File Offset: 0x0009B9AB
		// (set) Token: 0x060028EC RID: 10476 RVA: 0x0009D7B3 File Offset: 0x0009B9B3
		public int VoteCount { get; private set; }

		// Token: 0x060028ED RID: 10477 RVA: 0x0009D7BC File Offset: 0x0009B9BC
		public IntermissionVoteItem(string id, int index)
		{
			this.Id = id;
			this.Index = index;
			this.VoteCount = 0;
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x0009D7D9 File Offset: 0x0009B9D9
		public void SetVoteCount(int voteCount)
		{
			this.VoteCount = voteCount;
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x0009D7E2 File Offset: 0x0009B9E2
		public void IncreaseVoteCount(int incrementAmount)
		{
			this.VoteCount += incrementAmount;
		}

		// Token: 0x04000FBE RID: 4030
		public readonly string Id;

		// Token: 0x04000FBF RID: 4031
		public readonly int Index;
	}
}

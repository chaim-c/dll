using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Conversation.Persuasion
{
	// Token: 0x02000254 RID: 596
	public class PersuasionTask
	{
		// Token: 0x06001F68 RID: 8040 RVA: 0x00089DFB File Offset: 0x00087FFB
		public PersuasionTask(int reservationType)
		{
			this.Options = new MBList<PersuasionOptionArgs>();
			this.ReservationType = reservationType;
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x00089E15 File Offset: 0x00088015
		public void AddOptionToTask(PersuasionOptionArgs option)
		{
			this.Options.Add(option);
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x00089E24 File Offset: 0x00088024
		public void BlockAllOptions()
		{
			foreach (PersuasionOptionArgs persuasionOptionArgs in this.Options)
			{
				persuasionOptionArgs.BlockTheOption(true);
			}
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x00089E78 File Offset: 0x00088078
		public void UnblockAllOptions()
		{
			foreach (PersuasionOptionArgs persuasionOptionArgs in this.Options)
			{
				persuasionOptionArgs.BlockTheOption(false);
			}
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x00089ECC File Offset: 0x000880CC
		public void ApplyEffects(float moveToNextStageChance, float blockRandomOptionChance)
		{
			if (moveToNextStageChance > MBRandom.RandomFloat)
			{
				this.BlockAllOptions();
				return;
			}
			if (blockRandomOptionChance > MBRandom.RandomFloat)
			{
				PersuasionOptionArgs randomElementWithPredicate = this.Options.GetRandomElementWithPredicate((PersuasionOptionArgs x) => !x.IsBlocked);
				if (randomElementWithPredicate == null)
				{
					return;
				}
				randomElementWithPredicate.BlockTheOption(true);
			}
		}

		// Token: 0x04000A06 RID: 2566
		public readonly MBList<PersuasionOptionArgs> Options;

		// Token: 0x04000A07 RID: 2567
		public TextObject SpokenLine;

		// Token: 0x04000A08 RID: 2568
		public TextObject ImmediateFailLine;

		// Token: 0x04000A09 RID: 2569
		public TextObject FinalFailLine;

		// Token: 0x04000A0A RID: 2570
		public TextObject TryLaterLine;

		// Token: 0x04000A0B RID: 2571
		public readonly int ReservationType;
	}
}

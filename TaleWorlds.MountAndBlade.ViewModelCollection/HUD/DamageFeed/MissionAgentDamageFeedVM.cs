using System;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD.DamageFeed
{
	// Token: 0x02000056 RID: 86
	public class MissionAgentDamageFeedVM : ViewModel
	{
		// Token: 0x060006C1 RID: 1729 RVA: 0x0001B093 File Offset: 0x00019293
		public MissionAgentDamageFeedVM()
		{
			this._takenDamageText = new TextObject("{=meFS5F4V}-{DAMAGE}", null);
			this.FeedList = new MBBindingList<MissionAgentDamageFeedItemVM>();
			CombatLogManager.OnGenerateCombatLog += this.CombatLogManagerOnPrintCombatLog;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001B0C8 File Offset: 0x000192C8
		public override void OnFinalize()
		{
			CombatLogManager.OnGenerateCombatLog -= this.CombatLogManagerOnPrintCombatLog;
			base.OnFinalize();
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001B0E4 File Offset: 0x000192E4
		private void CombatLogManagerOnPrintCombatLog(CombatLogData logData)
		{
			if (!logData.IsVictimAgentMine || logData.TotalDamage <= 0)
			{
				return;
			}
			this._takenDamageText.SetTextVariable("DAMAGE", logData.TotalDamage);
			MissionAgentDamageFeedItemVM item = new MissionAgentDamageFeedItemVM(this._takenDamageText.ToString(), new Action<MissionAgentDamageFeedItemVM>(this.RemoveItem));
			this.FeedList.Add(item);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001B145 File Offset: 0x00019345
		private void RemoveItem(MissionAgentDamageFeedItemVM item)
		{
			this.FeedList.Remove(item);
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001B154 File Offset: 0x00019354
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x0001B15C File Offset: 0x0001935C
		[DataSourceProperty]
		public MBBindingList<MissionAgentDamageFeedItemVM> FeedList
		{
			get
			{
				return this._feedList;
			}
			set
			{
				if (value != this._feedList)
				{
					this._feedList = value;
					base.OnPropertyChangedWithValue<MBBindingList<MissionAgentDamageFeedItemVM>>(value, "FeedList");
				}
			}
		}

		// Token: 0x04000339 RID: 825
		private readonly TextObject _takenDamageText;

		// Token: 0x0400033A RID: 826
		private MBBindingList<MissionAgentDamageFeedItemVM> _feedList;
	}
}

using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD.KillFeed.General;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD.KillFeed.Personal;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD.KillFeed
{
	// Token: 0x0200004C RID: 76
	public class SPKillFeedVM : ViewModel
	{
		// Token: 0x06000647 RID: 1607 RVA: 0x00019A5F File Offset: 0x00017C5F
		public SPKillFeedVM()
		{
			this.GeneralCasualty = new SPGeneralKillNotificationVM();
			this.PersonalFeed = new SPPersonalKillNotificationVM();
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00019A7D File Offset: 0x00017C7D
		public void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, bool isHeadshot)
		{
			this.GeneralCasualty.OnAgentRemoved(affectedAgent, affectorAgent, null, isHeadshot);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00019A8E File Offset: 0x00017C8E
		public void OnPersonalKill(int damageAmount, bool isMountDamage, bool isFriendlyFire, bool isHeadshot, string killedAgentName, bool isUnconscious)
		{
			this.PersonalFeed.OnPersonalKill(damageAmount, isMountDamage, isFriendlyFire, isHeadshot, killedAgentName, isUnconscious);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00019AA4 File Offset: 0x00017CA4
		public void OnPersonalDamage(int totalDamage, bool isVictimAgentMount, bool isFriendlyFire, string victimAgentName)
		{
			this.PersonalFeed.OnPersonalHit(totalDamage, isVictimAgentMount, isFriendlyFire, victimAgentName);
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x00019AB6 File Offset: 0x00017CB6
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x00019ABE File Offset: 0x00017CBE
		[DataSourceProperty]
		public SPGeneralKillNotificationVM GeneralCasualty
		{
			get
			{
				return this._generalCasualty;
			}
			set
			{
				if (value != this._generalCasualty)
				{
					this._generalCasualty = value;
					base.OnPropertyChangedWithValue<SPGeneralKillNotificationVM>(value, "GeneralCasualty");
				}
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x00019ADC File Offset: 0x00017CDC
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x00019AE4 File Offset: 0x00017CE4
		[DataSourceProperty]
		public SPPersonalKillNotificationVM PersonalFeed
		{
			get
			{
				return this._personalFeed;
			}
			set
			{
				if (value != this._personalFeed)
				{
					this._personalFeed = value;
					base.OnPropertyChangedWithValue<SPPersonalKillNotificationVM>(value, "PersonalFeed");
				}
			}
		}

		// Token: 0x040002FC RID: 764
		private SPGeneralKillNotificationVM _generalCasualty;

		// Token: 0x040002FD RID: 765
		private SPPersonalKillNotificationVM _personalFeed;
	}
}

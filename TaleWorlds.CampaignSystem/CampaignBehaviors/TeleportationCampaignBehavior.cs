using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Party.PartyComponents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003D9 RID: 985
	public class TeleportationCampaignBehavior : CampaignBehaviorBase, ITeleportationCampaignBehavior, ICampaignBehavior
	{
		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06003D08 RID: 15624 RVA: 0x0012999E File Offset: 0x00127B9E
		private TextObject _partyLeaderChangeNotificationText
		{
			get
			{
				return new TextObject("{=QSaufZ9i}One of your parties has lost its leader. It will disband after a day has passed. You can assign a new clan member to lead it, if you wish to keep the party.", null);
			}
		}

		// Token: 0x06003D09 RID: 15625 RVA: 0x001299AC File Offset: 0x00127BAC
		public override void RegisterEvents()
		{
			CampaignEvents.HourlyTickEvent.AddNonSerializedListener(this, new Action(this.HourlyTick));
			CampaignEvents.DailyTickPartyEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.DailyTickParty));
			CampaignEvents.HeroComesOfAgeEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnHeroComesOfAge));
			CampaignEvents.MobilePartyDestroyed.AddNonSerializedListener(this, new Action<MobileParty, PartyBase>(this.OnMobilePartyDestroyed));
			CampaignEvents.OnSettlementOwnerChangedEvent.AddNonSerializedListener(this, new Action<Settlement, bool, Hero, Hero, Hero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail>(this.OnSettlementOwnerChanged));
			CampaignEvents.HeroKilledEvent.AddNonSerializedListener(this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.OnHeroKilled));
			CampaignEvents.OnPartyDisbandStartedEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.OnPartyDisbandStarted));
			CampaignEvents.OnGovernorChangedEvent.AddNonSerializedListener(this, new Action<Town, Hero, Hero>(this.OnGovernorChanged));
			CampaignEvents.OnHeroTeleportationRequestedEvent.AddNonSerializedListener(this, new Action<Hero, Settlement, MobileParty, TeleportHeroAction.TeleportationDetail>(this.OnHeroTeleportationRequested));
			CampaignEvents.OnPartyDisbandedEvent.AddNonSerializedListener(this, new Action<MobileParty, Settlement>(this.OnPartyDisbanded));
			CampaignEvents.OnClanLeaderChangedEvent.AddNonSerializedListener(this, new Action<Hero, Hero>(this.OnClanLeaderChanged));
		}

		// Token: 0x06003D0A RID: 15626 RVA: 0x00129AB6 File Offset: 0x00127CB6
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<List<TeleportationCampaignBehavior.TeleportationData>>("_teleportationList", ref this._teleportationList);
		}

		// Token: 0x06003D0B RID: 15627 RVA: 0x00129ACC File Offset: 0x00127CCC
		public bool GetTargetOfTeleportingHero(Hero teleportingHero, out bool isGovernor, out bool isPartyLeader, out IMapPoint target)
		{
			isGovernor = false;
			isPartyLeader = false;
			target = null;
			for (int i = 0; i < this._teleportationList.Count; i++)
			{
				TeleportationCampaignBehavior.TeleportationData teleportationData = this._teleportationList[i];
				if (teleportationData.TeleportingHero == teleportingHero)
				{
					if (teleportationData.TargetSettlement != null)
					{
						isGovernor = teleportationData.IsGovernor;
						target = teleportationData.TargetSettlement;
						return true;
					}
					if (teleportationData.TargetParty != null)
					{
						isPartyLeader = teleportationData.IsPartyLeader;
						target = teleportationData.TargetParty;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003D0C RID: 15628 RVA: 0x00129B48 File Offset: 0x00127D48
		public CampaignTime GetHeroArrivalTimeToDestination(Hero teleportingHero)
		{
			TeleportationCampaignBehavior.TeleportationData teleportationData = this._teleportationList.FirstOrDefaultQ((TeleportationCampaignBehavior.TeleportationData x) => x.TeleportingHero == teleportingHero);
			if (teleportationData != null)
			{
				return teleportationData.TeleportationTime;
			}
			return CampaignTime.Never;
		}

		// Token: 0x06003D0D RID: 15629 RVA: 0x00129B8C File Offset: 0x00127D8C
		private void HourlyTick()
		{
			for (int i = this._teleportationList.Count - 1; i >= 0; i--)
			{
				TeleportationCampaignBehavior.TeleportationData teleportationData = this._teleportationList[i];
				if (teleportationData.TeleportationTime.IsPast && this.CanApplyImmediateTeleportation(teleportationData))
				{
					TeleportationCampaignBehavior.TeleportationData data = teleportationData;
					this.RemoveTeleportationData(teleportationData, false, true);
					this.ApplyImmediateTeleport(data);
				}
			}
		}

		// Token: 0x06003D0E RID: 15630 RVA: 0x00129BE8 File Offset: 0x00127DE8
		private void DailyTickParty(MobileParty mobileParty)
		{
			if (mobileParty.IsActive && mobileParty.Army == null && mobileParty.MapEvent == null && mobileParty.LeaderHero != null && mobileParty.LeaderHero.IsNoncombatant && mobileParty.ActualClan != null && mobileParty.ActualClan != Clan.PlayerClan && mobileParty.ActualClan.Leader != mobileParty.LeaderHero)
			{
				MBList<Hero> mblist = mobileParty.ActualClan.Heroes.WhereQ((Hero h) => h.IsActive && h.IsCommander && h.PartyBelongedTo == null).ToMBList<Hero>();
				if (!mblist.IsEmpty<Hero>())
				{
					Hero leaderHero = mobileParty.LeaderHero;
					mobileParty.RemovePartyLeader();
					MakeHeroFugitiveAction.Apply(leaderHero);
					TeleportHeroAction.ApplyDelayedTeleportToPartyAsPartyLeader(mblist.GetRandomElementInefficiently<Hero>(), mobileParty);
				}
			}
		}

		// Token: 0x06003D0F RID: 15631 RVA: 0x00129CB4 File Offset: 0x00127EB4
		private void OnHeroComesOfAge(Hero hero)
		{
			if (hero.Clan != Clan.PlayerClan && !hero.IsNoncombatant)
			{
				foreach (WarPartyComponent warPartyComponent in hero.Clan.WarPartyComponents)
				{
					MobileParty mobileParty = warPartyComponent.MobileParty;
					if (mobileParty != null && mobileParty.Army == null && mobileParty.MapEvent == null && mobileParty.LeaderHero != null && mobileParty.LeaderHero.IsNoncombatant)
					{
						Hero leaderHero = mobileParty.LeaderHero;
						mobileParty.RemovePartyLeader();
						MakeHeroFugitiveAction.Apply(leaderHero);
						TeleportHeroAction.ApplyDelayedTeleportToPartyAsPartyLeader(hero, warPartyComponent.Party.MobileParty);
						break;
					}
				}
			}
		}

		// Token: 0x06003D10 RID: 15632 RVA: 0x00129D74 File Offset: 0x00127F74
		private void OnMobilePartyDestroyed(MobileParty mobileParty, PartyBase destroyerParty)
		{
			for (int i = this._teleportationList.Count - 1; i >= 0; i--)
			{
				TeleportationCampaignBehavior.TeleportationData teleportationData = this._teleportationList[i];
				if (teleportationData.TargetParty != null && teleportationData.TargetParty == mobileParty)
				{
					this.RemoveTeleportationData(teleportationData, true, true);
				}
			}
		}

		// Token: 0x06003D11 RID: 15633 RVA: 0x00129DC0 File Offset: 0x00127FC0
		private void OnSettlementOwnerChanged(Settlement settlement, bool openToClaim, Hero newOwner, Hero oldOwner, Hero capturerHero, ChangeOwnerOfSettlementAction.ChangeOwnerOfSettlementDetail detail)
		{
			for (int i = this._teleportationList.Count - 1; i >= 0; i--)
			{
				TeleportationCampaignBehavior.TeleportationData teleportationData = this._teleportationList[i];
				if (teleportationData.TargetSettlement != null && teleportationData.TargetSettlement == settlement && newOwner.Clan != teleportationData.TeleportingHero.Clan)
				{
					this.RemoveTeleportationData(teleportationData, true, true);
				}
			}
		}

		// Token: 0x06003D12 RID: 15634 RVA: 0x00129E20 File Offset: 0x00128020
		private void OnHeroKilled(Hero victim, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification = true)
		{
			for (int i = this._teleportationList.Count - 1; i >= 0; i--)
			{
				TeleportationCampaignBehavior.TeleportationData teleportationData = this._teleportationList[i];
				if (teleportationData.TeleportingHero == victim)
				{
					this.RemoveTeleportationData(teleportationData, true, true);
				}
			}
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x00129E64 File Offset: 0x00128064
		private void OnPartyDisbandStarted(MobileParty disbandParty)
		{
			if (disbandParty.ActualClan == Clan.PlayerClan && disbandParty.LeaderHero == null && (disbandParty.IsLordParty || disbandParty.IsCaravan))
			{
				Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new PartyLeaderChangeNotification(disbandParty, this._partyLeaderChangeNotificationText));
			}
			for (int i = this._teleportationList.Count - 1; i >= 0; i--)
			{
				TeleportationCampaignBehavior.TeleportationData teleportationData = this._teleportationList[i];
				if (teleportationData.TargetParty != null && teleportationData.TargetParty == disbandParty)
				{
					this.RemoveTeleportationData(teleportationData, true, false);
				}
			}
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x00129EF0 File Offset: 0x001280F0
		private void OnGovernorChanged(Town fortification, Hero oldGovernor, Hero newGovernor)
		{
			for (int i = this._teleportationList.Count - 1; i >= 0; i--)
			{
				TeleportationCampaignBehavior.TeleportationData teleportationData = this._teleportationList[i];
				if (teleportationData.TeleportingHero != newGovernor && teleportationData.IsGovernor && teleportationData.TargetSettlement.Town == fortification)
				{
					teleportationData.IsGovernor = false;
				}
			}
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x00129F48 File Offset: 0x00128148
		private void OnHeroTeleportationRequested(Hero hero, Settlement targetSettlement, MobileParty targetParty, TeleportHeroAction.TeleportationDetail detail)
		{
			switch (detail)
			{
			case TeleportHeroAction.TeleportationDetail.ImmediateTeleportToSettlement:
				for (int i = this._teleportationList.Count - 1; i >= 0; i--)
				{
					TeleportationCampaignBehavior.TeleportationData teleportationData = this._teleportationList[i];
					if (hero == teleportationData.TeleportingHero && teleportationData.TargetSettlement == targetSettlement)
					{
						this.RemoveTeleportationData(teleportationData, true, false);
					}
				}
				break;
			case TeleportHeroAction.TeleportationDetail.ImmediateTeleportToParty:
			case TeleportHeroAction.TeleportationDetail.ImmediateTeleportToPartyAsPartyLeader:
				break;
			case TeleportHeroAction.TeleportationDetail.DelayedTeleportToSettlement:
			case TeleportHeroAction.TeleportationDetail.DelayedTeleportToSettlementAsGovernor:
				this._teleportationList.Add(new TeleportationCampaignBehavior.TeleportationData(hero, targetSettlement, detail == TeleportHeroAction.TeleportationDetail.DelayedTeleportToSettlementAsGovernor));
				return;
			case TeleportHeroAction.TeleportationDetail.DelayedTeleportToParty:
			case TeleportHeroAction.TeleportationDetail.DelayedTeleportToPartyAsPartyLeader:
				if (detail == TeleportHeroAction.TeleportationDetail.DelayedTeleportToPartyAsPartyLeader)
				{
					for (int j = this._teleportationList.Count - 1; j >= 0; j--)
					{
						TeleportationCampaignBehavior.TeleportationData teleportationData2 = this._teleportationList[j];
						if (teleportationData2.TargetParty == targetParty && teleportationData2.IsPartyLeader)
						{
							this.RemoveTeleportationData(teleportationData2, true, false);
						}
					}
				}
				this._teleportationList.Add(new TeleportationCampaignBehavior.TeleportationData(hero, targetParty, detail == TeleportHeroAction.TeleportationDetail.DelayedTeleportToPartyAsPartyLeader));
				return;
			default:
				return;
			}
		}

		// Token: 0x06003D16 RID: 15638 RVA: 0x0012A030 File Offset: 0x00128230
		private void OnPartyDisbanded(MobileParty disbandParty, Settlement relatedSettlement)
		{
			for (int i = this._teleportationList.Count - 1; i >= 0; i--)
			{
				TeleportationCampaignBehavior.TeleportationData teleportationData = this._teleportationList[i];
				if (teleportationData.TargetParty != null && teleportationData.TargetParty == disbandParty)
				{
					this.RemoveTeleportationData(teleportationData, true, true);
				}
			}
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x0012A07C File Offset: 0x0012827C
		private void OnClanLeaderChanged(Hero oldLeader, Hero newLeader)
		{
			for (int i = this._teleportationList.Count - 1; i >= 0; i--)
			{
				TeleportationCampaignBehavior.TeleportationData teleportationData = this._teleportationList[i];
				if (teleportationData.TeleportingHero == newLeader && !teleportationData.IsPartyLeader)
				{
					this.RemoveTeleportationData(teleportationData, true, true);
				}
			}
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x0012A0C8 File Offset: 0x001282C8
		private void RemoveTeleportationData(TeleportationCampaignBehavior.TeleportationData data, bool isCanceled, bool disbandTargetParty = true)
		{
			this._teleportationList.Remove(data);
			if (isCanceled)
			{
				if (data.TeleportingHero.IsTraveling && data.TeleportingHero.DeathMark == KillCharacterAction.KillCharacterActionDetail.None)
				{
					MakeHeroFugitiveAction.Apply(data.TeleportingHero);
				}
				if (data.TargetParty != null)
				{
					if (data.TargetParty.ActualClan == Clan.PlayerClan)
					{
						CampaignEventDispatcher.Instance.OnPartyLeaderChangeOfferCanceled(data.TargetParty);
					}
					if (disbandTargetParty && data.TargetParty.IsActive && data.IsPartyLeader)
					{
						IDisbandPartyCampaignBehavior behavior = Campaign.Current.CampaignBehaviorManager.GetBehavior<IDisbandPartyCampaignBehavior>();
						if (behavior != null && !behavior.IsPartyWaitingForDisband(data.TargetParty))
						{
							DisbandPartyAction.StartDisband(data.TargetParty);
						}
					}
				}
			}
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x0012A17C File Offset: 0x0012837C
		private bool CanApplyImmediateTeleportation(TeleportationCampaignBehavior.TeleportationData data)
		{
			return (data.TargetSettlement != null && !data.TargetSettlement.IsUnderSiege && !data.TargetSettlement.IsUnderRaid) || (data.TargetParty != null && data.TargetParty.MapEvent == null && !data.TargetParty.IsCurrentlyEngagingParty);
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x0012A1D0 File Offset: 0x001283D0
		private void ApplyImmediateTeleport(TeleportationCampaignBehavior.TeleportationData data)
		{
			if (data.TargetSettlement == null)
			{
				if (data.TargetParty != null)
				{
					if (data.IsPartyLeader)
					{
						TeleportHeroAction.ApplyImmediateTeleportToPartyAsPartyLeader(data.TeleportingHero, data.TargetParty);
						return;
					}
					TeleportHeroAction.ApplyImmediateTeleportToParty(data.TeleportingHero, data.TargetParty);
				}
				return;
			}
			if (data.IsGovernor)
			{
				data.TargetSettlement.Town.Governor = data.TeleportingHero;
				TeleportHeroAction.ApplyImmediateTeleportToSettlement(data.TeleportingHero, data.TargetSettlement);
				return;
			}
			TeleportHeroAction.ApplyImmediateTeleportToSettlement(data.TeleportingHero, data.TargetSettlement);
		}

		// Token: 0x04001221 RID: 4641
		private List<TeleportationCampaignBehavior.TeleportationData> _teleportationList = new List<TeleportationCampaignBehavior.TeleportationData>();

		// Token: 0x0200073D RID: 1853
		public class TeleportationCampaignBehaviorTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x0600597F RID: 22911 RVA: 0x0018403C File Offset: 0x0018223C
			public TeleportationCampaignBehaviorTypeDefiner() : base(151000)
			{
			}

			// Token: 0x06005980 RID: 22912 RVA: 0x00184049 File Offset: 0x00182249
			protected override void DefineClassTypes()
			{
				base.AddClassDefinition(typeof(TeleportationCampaignBehavior.TeleportationData), 1, null);
			}

			// Token: 0x06005981 RID: 22913 RVA: 0x0018405D File Offset: 0x0018225D
			protected override void DefineContainerDefinitions()
			{
				base.ConstructContainerDefinition(typeof(List<TeleportationCampaignBehavior.TeleportationData>));
			}
		}

		// Token: 0x0200073E RID: 1854
		internal class TeleportationData
		{
			// Token: 0x06005982 RID: 22914 RVA: 0x00184070 File Offset: 0x00182270
			public TeleportationData(Hero teleportingHero, Settlement targetSettlement, bool isGovernor)
			{
				this.TeleportingHero = teleportingHero;
				this.TeleportationTime = CampaignTime.HoursFromNow(Campaign.Current.Models.DelayedTeleportationModel.GetTeleportationDelayAsHours(teleportingHero, targetSettlement.Party).ResultNumber);
				this.TargetSettlement = targetSettlement;
				this.IsGovernor = isGovernor;
				this.TargetParty = null;
				this.IsPartyLeader = false;
			}

			// Token: 0x06005983 RID: 22915 RVA: 0x001840D4 File Offset: 0x001822D4
			public TeleportationData(Hero teleportingHero, MobileParty targetParty, bool isPartyLeader)
			{
				this.TeleportingHero = teleportingHero;
				this.TeleportationTime = CampaignTime.HoursFromNow(Campaign.Current.Models.DelayedTeleportationModel.GetTeleportationDelayAsHours(teleportingHero, targetParty.Party).ResultNumber);
				this.TargetParty = targetParty;
				this.IsPartyLeader = isPartyLeader;
				this.TargetSettlement = null;
				this.IsGovernor = false;
			}

			// Token: 0x06005984 RID: 22916 RVA: 0x00184138 File Offset: 0x00182338
			internal static void AutoGeneratedStaticCollectObjectsTeleportationData(object o, List<object> collectedObjects)
			{
				((TeleportationCampaignBehavior.TeleportationData)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06005985 RID: 22917 RVA: 0x00184146 File Offset: 0x00182346
			protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				collectedObjects.Add(this.TeleportingHero);
				CampaignTime.AutoGeneratedStaticCollectObjectsCampaignTime(this.TeleportationTime, collectedObjects);
				collectedObjects.Add(this.TargetSettlement);
				collectedObjects.Add(this.TargetParty);
			}

			// Token: 0x06005986 RID: 22918 RVA: 0x0018417D File Offset: 0x0018237D
			internal static object AutoGeneratedGetMemberValueTeleportingHero(object o)
			{
				return ((TeleportationCampaignBehavior.TeleportationData)o).TeleportingHero;
			}

			// Token: 0x06005987 RID: 22919 RVA: 0x0018418A File Offset: 0x0018238A
			internal static object AutoGeneratedGetMemberValueTeleportationTime(object o)
			{
				return ((TeleportationCampaignBehavior.TeleportationData)o).TeleportationTime;
			}

			// Token: 0x06005988 RID: 22920 RVA: 0x0018419C File Offset: 0x0018239C
			internal static object AutoGeneratedGetMemberValueTargetSettlement(object o)
			{
				return ((TeleportationCampaignBehavior.TeleportationData)o).TargetSettlement;
			}

			// Token: 0x06005989 RID: 22921 RVA: 0x001841A9 File Offset: 0x001823A9
			internal static object AutoGeneratedGetMemberValueIsGovernor(object o)
			{
				return ((TeleportationCampaignBehavior.TeleportationData)o).IsGovernor;
			}

			// Token: 0x0600598A RID: 22922 RVA: 0x001841BB File Offset: 0x001823BB
			internal static object AutoGeneratedGetMemberValueTargetParty(object o)
			{
				return ((TeleportationCampaignBehavior.TeleportationData)o).TargetParty;
			}

			// Token: 0x0600598B RID: 22923 RVA: 0x001841C8 File Offset: 0x001823C8
			internal static object AutoGeneratedGetMemberValueIsPartyLeader(object o)
			{
				return ((TeleportationCampaignBehavior.TeleportationData)o).IsPartyLeader;
			}

			// Token: 0x04001E77 RID: 7799
			[SaveableField(1)]
			public Hero TeleportingHero;

			// Token: 0x04001E78 RID: 7800
			[SaveableField(2)]
			public CampaignTime TeleportationTime;

			// Token: 0x04001E79 RID: 7801
			[SaveableField(3)]
			public Settlement TargetSettlement;

			// Token: 0x04001E7A RID: 7802
			[SaveableField(4)]
			public bool IsGovernor;

			// Token: 0x04001E7B RID: 7803
			[SaveableField(5)]
			public MobileParty TargetParty;

			// Token: 0x04001E7C RID: 7804
			[SaveableField(6)]
			public bool IsPartyLeader;
		}
	}
}

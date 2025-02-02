﻿using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.MapEvents
{
	// Token: 0x020002BB RID: 699
	public class ForceSuppliesEventComponent : MapEventComponent
	{
		// Token: 0x06002982 RID: 10626 RVA: 0x000B25E3 File Offset: 0x000B07E3
		internal static void AutoGeneratedStaticCollectObjectsForceSuppliesEventComponent(object o, List<object> collectedObjects)
		{
			((ForceSuppliesEventComponent)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x000B25F1 File Offset: 0x000B07F1
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x000B25FA File Offset: 0x000B07FA
		protected ForceSuppliesEventComponent(MapEvent mapEvent) : base(mapEvent)
		{
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x000B2604 File Offset: 0x000B0804
		public static ForceSuppliesEventComponent CreateForceSuppliesEvent(PartyBase attackerParty, PartyBase defenderParty)
		{
			MapEvent mapEvent = new MapEvent();
			ForceSuppliesEventComponent forceSuppliesEventComponent = new ForceSuppliesEventComponent(mapEvent);
			mapEvent.Initialize(attackerParty, defenderParty, forceSuppliesEventComponent, MapEvent.BattleTypes.IsForcingSupplies);
			Settlement settlement = defenderParty.Settlement;
			if (((settlement != null) ? settlement.MilitiaPartyComponent : null) != null)
			{
				defenderParty.Settlement.MilitiaPartyComponent.MobileParty.MapEventSide = mapEvent.DefenderSide;
			}
			Campaign.Current.MapEventManager.OnMapEventCreated(mapEvent);
			return forceSuppliesEventComponent;
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x000B2668 File Offset: 0x000B0868
		public static ForceSuppliesEventComponent CreateComponentForOldSaves(MapEvent mapEvent)
		{
			return new ForceSuppliesEventComponent(mapEvent);
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x000B2670 File Offset: 0x000B0870
		protected override void OnInitialize()
		{
			ChangeVillageStateAction.ApplyBySettingToBeingForcedForSupplies(base.MapEvent.MapEventSettlement, base.MapEvent.AttackerSide.LeaderParty.MobileParty);
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x000B2697 File Offset: 0x000B0897
		protected override void OnFinalize()
		{
			CampaignEventDispatcher.Instance.ForceSuppliesCompleted((base.MapEvent.BattleState == BattleState.AttackerVictory) ? BattleSideEnum.Attacker : BattleSideEnum.Defender, this);
			ChangeVillageStateAction.ApplyBySettingToNormal(base.MapEvent.MapEventSettlement);
		}
	}
}

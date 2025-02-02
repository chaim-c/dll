﻿using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.BarterSystem.Barterables
{
	// Token: 0x0200041A RID: 1050
	public class MercenaryJoinKingdomBarterable : Barterable
	{
		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06003FC7 RID: 16327 RVA: 0x0013AD0D File Offset: 0x00138F0D
		public override string StringID
		{
			get
			{
				return "mercenary_join_faction_barterable";
			}
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x0013AD14 File Offset: 0x00138F14
		public MercenaryJoinKingdomBarterable(Hero owner, PartyBase ownerParty, Kingdom targetKingdom) : base(owner, ownerParty)
		{
			this._targetKingdom = targetKingdom;
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06003FC9 RID: 16329 RVA: 0x0013AD25 File Offset: 0x00138F25
		public override TextObject Name
		{
			get
			{
				TextObject textObject = new TextObject("{=PaG0Blui}Become a mercenary for {TARGET_FACTION}", null);
				textObject.SetTextVariable("TARGET_FACTION", this._targetKingdom.Name);
				return textObject;
			}
		}

		// Token: 0x06003FCA RID: 16330 RVA: 0x0013AD4C File Offset: 0x00138F4C
		public override int GetUnitValueForFaction(IFaction faction)
		{
			float num = 0f;
			if (this._targetKingdom == faction.MapFaction)
			{
				num += Campaign.Current.Models.DiplomacyModel.GetScoreOfKingdomToHireMercenary(this._targetKingdom, base.OriginalOwner.Clan);
			}
			else if (faction == base.OriginalOwner.Clan)
			{
				if (base.OriginalOwner.Clan.Kingdom != null)
				{
					num += Campaign.Current.Models.DiplomacyModel.GetScoreOfMercenaryToLeaveKingdom(base.OriginalOwner.Clan, base.OriginalOwner.Clan.Kingdom);
				}
				num += Campaign.Current.Models.DiplomacyModel.GetScoreOfMercenaryToJoinKingdom(base.OriginalOwner.Clan, this._targetKingdom);
			}
			return (int)num;
		}

		// Token: 0x06003FCB RID: 16331 RVA: 0x0013AE13 File Offset: 0x00139013
		public override void CheckBarterLink(Barterable linkedBarterable)
		{
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x0013AE15 File Offset: 0x00139015
		public override ImageIdentifier GetVisualIdentifier()
		{
			return null;
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x0013AE18 File Offset: 0x00139018
		public override string GetEncyclopediaLink()
		{
			return this._targetKingdom.EncyclopediaLink;
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x0013AE25 File Offset: 0x00139025
		public override void Apply()
		{
			ChangeKingdomAction.ApplyByJoinFactionAsMercenary(base.OriginalOwner.Clan, this._targetKingdom, Campaign.Current.Models.MinorFactionsModel.GetMercenaryAwardFactorToJoinKingdom(base.OriginalOwner.Clan, this._targetKingdom, false), true);
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x0013AE64 File Offset: 0x00139064
		internal static void AutoGeneratedStaticCollectObjectsMercenaryJoinKingdomBarterable(object o, List<object> collectedObjects)
		{
			((MercenaryJoinKingdomBarterable)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x06003FD0 RID: 16336 RVA: 0x0013AE72 File Offset: 0x00139072
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			collectedObjects.Add(this._targetKingdom);
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x0013AE87 File Offset: 0x00139087
		internal static object AutoGeneratedGetMemberValue_targetKingdom(object o)
		{
			return ((MercenaryJoinKingdomBarterable)o)._targetKingdom;
		}

		// Token: 0x040012A0 RID: 4768
		[SaveableField(700)]
		private readonly Kingdom _targetKingdom;
	}
}

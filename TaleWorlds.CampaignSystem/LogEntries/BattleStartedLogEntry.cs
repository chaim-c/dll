﻿using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.LogEntries
{
	// Token: 0x020002C8 RID: 712
	public class BattleStartedLogEntry : LogEntry, IEncyclopediaLog, IChatNotification
	{
		// Token: 0x060029FE RID: 10750 RVA: 0x000B3DEC File Offset: 0x000B1FEC
		internal static void AutoGeneratedStaticCollectObjectsBattleStartedLogEntry(object o, List<object> collectedObjects)
		{
			((BattleStartedLogEntry)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x000B3DFC File Offset: 0x000B1FFC
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			collectedObjects.Add(this._settlement);
			collectedObjects.Add(this._attackerName);
			collectedObjects.Add(this._defenderName);
			collectedObjects.Add(this._attackerFaction);
			collectedObjects.Add(this._attackerLord);
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x000B3E4C File Offset: 0x000B204C
		internal static object AutoGeneratedGetMemberValue_notificationTextColor(object o)
		{
			return ((BattleStartedLogEntry)o)._notificationTextColor;
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x000B3E5E File Offset: 0x000B205E
		internal static object AutoGeneratedGetMemberValue_settlement(object o)
		{
			return ((BattleStartedLogEntry)o)._settlement;
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x000B3E6B File Offset: 0x000B206B
		internal static object AutoGeneratedGetMemberValue_attackerName(object o)
		{
			return ((BattleStartedLogEntry)o)._attackerName;
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x000B3E78 File Offset: 0x000B2078
		internal static object AutoGeneratedGetMemberValue_defenderName(object o)
		{
			return ((BattleStartedLogEntry)o)._defenderName;
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x000B3E85 File Offset: 0x000B2085
		internal static object AutoGeneratedGetMemberValue_isVisibleNotification(object o)
		{
			return ((BattleStartedLogEntry)o)._isVisibleNotification;
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x000B3E97 File Offset: 0x000B2097
		internal static object AutoGeneratedGetMemberValue_battleDetail(object o)
		{
			return ((BattleStartedLogEntry)o)._battleDetail;
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x000B3EA9 File Offset: 0x000B20A9
		internal static object AutoGeneratedGetMemberValue_attackerFaction(object o)
		{
			return ((BattleStartedLogEntry)o)._attackerFaction;
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x000B3EB6 File Offset: 0x000B20B6
		internal static object AutoGeneratedGetMemberValue_attackerLord(object o)
		{
			return ((BattleStartedLogEntry)o)._attackerLord;
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x000B3EC3 File Offset: 0x000B20C3
		internal static object AutoGeneratedGetMemberValue_attackerPartyHasArmy(object o)
		{
			return ((BattleStartedLogEntry)o)._attackerPartyHasArmy;
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06002A09 RID: 10761 RVA: 0x000B3ED5 File Offset: 0x000B20D5
		public bool IsVisibleNotification
		{
			get
			{
				return this._isVisibleNotification;
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06002A0A RID: 10762 RVA: 0x000B3EDD File Offset: 0x000B20DD
		public override ChatNotificationType NotificationType
		{
			get
			{
				return base.AdversityNotification(null, null);
			}
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x000B3EE8 File Offset: 0x000B20E8
		public BattleStartedLogEntry(PartyBase attackerParty, PartyBase defenderParty, object subject)
		{
			this._notificationTextColor = defenderParty.MapFaction.LabelColor;
			this._attackerName = ((attackerParty.IsMobile && attackerParty.MobileParty.Army != null) ? attackerParty.MobileParty.ArmyName : attackerParty.Name);
			this._defenderName = ((defenderParty.IsMobile && defenderParty.MobileParty.Army != null) ? defenderParty.MobileParty.ArmyName : defenderParty.Name);
			this._attackerFaction = attackerParty.MapFaction;
			this._attackerPartyHasArmy = (attackerParty.IsMobile && attackerParty.MobileParty.Army != null && attackerParty.MobileParty.LeaderHero != null);
			if (attackerParty.IsMobile && attackerParty.MobileParty.LeaderHero != null)
			{
				this._attackerLord = attackerParty.MobileParty.LeaderHero;
			}
			this._settlement = (subject as Settlement);
			if (this._settlement != null)
			{
				this._isVisibleNotification = true;
				if (this._settlement.IsVillage)
				{
					this._battleDetail = (this._settlement.MapFaction.IsAtWarWith(attackerParty.MapFaction) ? 1U : 0U);
					return;
				}
				if (this._settlement.IsFortification)
				{
					this._battleDetail = (this._settlement.MapFaction.IsAtWarWith(attackerParty.MapFaction) ? 3U : 2U);
					return;
				}
			}
			else if (subject is Army)
			{
				this._battleDetail = 4U;
			}
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x000B4050 File Offset: 0x000B2250
		public override string ToString()
		{
			return this.GetEncyclopediaText().ToString();
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x000B405D File Offset: 0x000B225D
		public TextObject GetNotificationText()
		{
			return this.GetEncyclopediaText();
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x000B4065 File Offset: 0x000B2265
		public bool IsVisibleInEncyclopediaPageOf<T>(T obj) where T : MBObjectBase
		{
			return obj == this._settlement;
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x000B4078 File Offset: 0x000B2278
		public TextObject GetEncyclopediaText()
		{
			TextObject textObject = TextObject.Empty;
			TextObject variable = (this._settlement != null) ? this._settlement.EncyclopediaLinkWithName : TextObject.Empty;
			switch (this._battleDetail)
			{
			case 0U:
				textObject = GameTexts.FindText("str_army_defend_from_raiding_news", null);
				textObject.SetTextVariable("PARTY", this._attackerName);
				textObject.SetTextVariable("SETTLEMENT", variable);
				break;
			case 1U:
				if (this._attackerFaction == null || this._attackerLord == null)
				{
					textObject = new TextObject("{=xss7eP0f}{PARTY} is raiding {SETTLEMENT}", null);
					textObject.SetTextVariable("PARTY", this._attackerName);
					textObject.SetTextVariable("SETTLEMENT", variable);
				}
				else
				{
					if (this._attackerPartyHasArmy)
					{
						textObject = GameTexts.FindText("str_army_raiding_news", null);
					}
					else
					{
						textObject = GameTexts.FindText("str_party_raiding_news", null);
					}
					StringHelpers.SetCharacterProperties("LORD", this._attackerLord.CharacterObject, textObject, false);
					textObject.SetTextVariable("FACTION_NAME", this._attackerFaction.EncyclopediaLinkWithName);
					textObject.SetTextVariable("SETTLEMENT", variable);
				}
				break;
			case 2U:
				textObject = GameTexts.FindText("str_defend_from_assault_news", null);
				textObject.SetTextVariable("PARTY", this._attackerName);
				textObject.SetTextVariable("SETTLEMENT", variable);
				break;
			case 3U:
				textObject = GameTexts.FindText("str_assault_news", null);
				textObject.SetTextVariable("PARTY", this._attackerName);
				textObject.SetTextVariable("SETTLEMENT", variable);
				break;
			case 4U:
				textObject = GameTexts.FindText("str_army_battle_news", null);
				textObject.SetTextVariable("PARTY1", this._attackerName);
				textObject.SetTextVariable("PARTY2", this._defenderName);
				break;
			}
			return textObject;
		}

		// Token: 0x04000C9E RID: 3230
		[SaveableField(40)]
		private readonly uint _notificationTextColor;

		// Token: 0x04000C9F RID: 3231
		[SaveableField(41)]
		private readonly Settlement _settlement;

		// Token: 0x04000CA0 RID: 3232
		[SaveableField(42)]
		private readonly TextObject _attackerName;

		// Token: 0x04000CA1 RID: 3233
		[SaveableField(43)]
		private readonly TextObject _defenderName;

		// Token: 0x04000CA2 RID: 3234
		[SaveableField(44)]
		private readonly bool _isVisibleNotification;

		// Token: 0x04000CA3 RID: 3235
		[SaveableField(45)]
		private readonly uint _battleDetail;

		// Token: 0x04000CA4 RID: 3236
		[SaveableField(46)]
		private readonly IFaction _attackerFaction;

		// Token: 0x04000CA5 RID: 3237
		[SaveableField(47)]
		private readonly Hero _attackerLord;

		// Token: 0x04000CA6 RID: 3238
		[SaveableField(48)]
		private readonly bool _attackerPartyHasArmy;

		// Token: 0x020005EE RID: 1518
		private enum BattleDetail
		{
			// Token: 0x04001869 RID: 6249
			AttackerPartyDefendingVillage,
			// Token: 0x0400186A RID: 6250
			AttackerPartyRaidingVillage,
			// Token: 0x0400186B RID: 6251
			AttackerPartyDefendingFortification,
			// Token: 0x0400186C RID: 6252
			AttackerPartyBesiegingFortification,
			// Token: 0x0400186D RID: 6253
			ArmyBattle
		}
	}
}

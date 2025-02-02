﻿using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.LogEntries
{
	// Token: 0x020002E3 RID: 739
	public class SettlementClaimedLogEntry : LogEntry, IChatNotification
	{
		// Token: 0x06002B45 RID: 11077 RVA: 0x000B7976 File Offset: 0x000B5B76
		internal static void AutoGeneratedStaticCollectObjectsSettlementClaimedLogEntry(object o, List<object> collectedObjects)
		{
			((SettlementClaimedLogEntry)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x000B7984 File Offset: 0x000B5B84
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			collectedObjects.Add(this.Claimant);
			collectedObjects.Add(this.Settlement);
			collectedObjects.Add(this._settlementClan);
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x000B79B1 File Offset: 0x000B5BB1
		internal static object AutoGeneratedGetMemberValueClaimant(object o)
		{
			return ((SettlementClaimedLogEntry)o).Claimant;
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x000B79BE File Offset: 0x000B5BBE
		internal static object AutoGeneratedGetMemberValueSettlement(object o)
		{
			return ((SettlementClaimedLogEntry)o).Settlement;
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x000B79CB File Offset: 0x000B5BCB
		internal static object AutoGeneratedGetMemberValue_settlementClan(object o)
		{
			return ((SettlementClaimedLogEntry)o)._settlementClan;
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06002B4A RID: 11082 RVA: 0x000B79D8 File Offset: 0x000B5BD8
		public bool IsVisibleNotification
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06002B4B RID: 11083 RVA: 0x000B79DB File Offset: 0x000B5BDB
		public override CampaignTime KeepInHistoryTime
		{
			get
			{
				return CampaignTime.Weeks(240f);
			}
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000B79E7 File Offset: 0x000B5BE7
		public SettlementClaimedLogEntry(Hero claimant, Settlement settlement)
		{
			this.Claimant = claimant;
			this.Settlement = settlement;
			this._settlementClan = settlement.OwnerClan;
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x000B7A09 File Offset: 0x000B5C09
		public override string ToString()
		{
			return this.GetNotificationText().ToString();
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x000B7A18 File Offset: 0x000B5C18
		public TextObject GetNotificationText()
		{
			TextObject textObject = new TextObject("{=ARNYrXln}{TROOP.NAME} has claimed {SIDE1_PARTY} currently held by the {SIDE2_FACTION}", null);
			StringHelpers.SetCharacterProperties("TROOP", this.Claimant.CharacterObject, textObject, false);
			textObject.SetTextVariable("SIDE1_PARTY", this.Settlement.Name);
			textObject.SetTextVariable("SIDE2_FACTION", this._settlementClan.Name);
			return textObject;
		}

		// Token: 0x04000CF9 RID: 3321
		[SaveableField(320)]
		public readonly Hero Claimant;

		// Token: 0x04000CFA RID: 3322
		[SaveableField(321)]
		public readonly Settlement Settlement;

		// Token: 0x04000CFB RID: 3323
		[SaveableField(322)]
		private readonly Clan _settlementClan;
	}
}

﻿using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.Party.PartyComponents
{
	// Token: 0x020002AD RID: 685
	public class GarrisonPartyComponent : PartyComponent
	{
		// Token: 0x060027BC RID: 10172 RVA: 0x000AA588 File Offset: 0x000A8788
		internal static void AutoGeneratedStaticCollectObjectsGarrisonPartyComponent(object o, List<object> collectedObjects)
		{
			((GarrisonPartyComponent)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x000AA596 File Offset: 0x000A8796
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			collectedObjects.Add(this.Settlement);
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x000AA5AB File Offset: 0x000A87AB
		internal static object AutoGeneratedGetMemberValueSettlement(object o)
		{
			return ((GarrisonPartyComponent)o).Settlement;
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x000AA5B8 File Offset: 0x000A87B8
		public static MobileParty CreateGarrisonParty(string stringId, Settlement settlement, bool isInitialGarrison)
		{
			return MobileParty.CreateParty(stringId, new GarrisonPartyComponent(settlement), delegate(MobileParty mobileParty)
			{
				(mobileParty.PartyComponent as GarrisonPartyComponent).InitializeGarrisonPartyProperties(mobileParty, isInitialGarrison);
			});
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060027C0 RID: 10176 RVA: 0x000AA5EA File Offset: 0x000A87EA
		public override Hero PartyOwner
		{
			get
			{
				return this.Settlement.OwnerClan.Leader;
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060027C1 RID: 10177 RVA: 0x000AA5FC File Offset: 0x000A87FC
		public override TextObject Name
		{
			get
			{
				if (this._cachedName == null)
				{
					this._cachedName = GameTexts.FindText("str_garrison_party_name", null);
					this._cachedName.SetTextVariable("MAJOR_PARTY_LEADER", this.Settlement.Name);
				}
				return this._cachedName;
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060027C2 RID: 10178 RVA: 0x000AA639 File Offset: 0x000A8839
		public override Settlement HomeSettlement
		{
			get
			{
				return this.Settlement;
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x060027C3 RID: 10179 RVA: 0x000AA641 File Offset: 0x000A8841
		public override int WagePaymentLimit
		{
			get
			{
				return this.Settlement.GarrisonWagePaymentLimit;
			}
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x000AA64E File Offset: 0x000A884E
		public override void SetWagePaymentLimit(int newLimit)
		{
			this.Settlement.SetGarrisonWagePaymentLimit(newLimit);
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x060027C5 RID: 10181 RVA: 0x000AA65C File Offset: 0x000A885C
		// (set) Token: 0x060027C6 RID: 10182 RVA: 0x000AA664 File Offset: 0x000A8864
		[SaveableProperty(1)]
		public Settlement Settlement { get; private set; }

		// Token: 0x060027C7 RID: 10183 RVA: 0x000AA66D File Offset: 0x000A886D
		protected internal GarrisonPartyComponent(Settlement settlement)
		{
			this.Settlement = settlement;
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x000AA67C File Offset: 0x000A887C
		protected override void OnInitialize()
		{
			this.Settlement.Town.GarrisonPartyComponent = this;
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x000AA68F File Offset: 0x000A888F
		protected override void OnFinalize()
		{
			this.Settlement.Town.GarrisonPartyComponent = null;
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x000AA6A2 File Offset: 0x000A88A2
		public override void ClearCachedName()
		{
			this._cachedName = null;
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x000AA6AC File Offset: 0x000A88AC
		private void InitializeGarrisonPartyProperties(MobileParty mobileParty, bool isInitialGarrison)
		{
			PartyTemplateObject defaultPartyTemplate = this.Settlement.Culture.DefaultPartyTemplate;
			mobileParty.CurrentSettlement = this.Settlement;
			int troopNumberLimit = isInitialGarrison ? ((int)(this.Settlement.Town.Prosperity * 0.04f + (float)(this.Settlement.IsTown ? 40 : 0) + 80f)) : 0;
			mobileParty.InitializeMobilePartyAtPosition(defaultPartyTemplate, this.Settlement.GatePosition, troopNumberLimit);
			mobileParty.Party.SetVisualAsDirty();
			mobileParty.InitializePartyTrade(Campaign.Current.Models.ClanFinanceModel.PartyGoldLowerThreshold);
			mobileParty.Ai.DisableAi();
			mobileParty.Aggressiveness = 0f;
		}

		// Token: 0x04000C24 RID: 3108
		[CachedData]
		private TextObject _cachedName;
	}
}

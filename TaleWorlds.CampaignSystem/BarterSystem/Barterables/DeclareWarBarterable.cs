using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.BarterSystem.Barterables
{
	// Token: 0x02000413 RID: 1043
	public class DeclareWarBarterable : Barterable
	{
		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06003F73 RID: 16243 RVA: 0x00139A9A File Offset: 0x00137C9A
		public override string StringID
		{
			get
			{
				return "declare_war_barterable";
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06003F74 RID: 16244 RVA: 0x00139AA1 File Offset: 0x00137CA1
		// (set) Token: 0x06003F75 RID: 16245 RVA: 0x00139AA9 File Offset: 0x00137CA9
		public IFaction DeclaringFaction { get; private set; }

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06003F76 RID: 16246 RVA: 0x00139AB2 File Offset: 0x00137CB2
		// (set) Token: 0x06003F77 RID: 16247 RVA: 0x00139ABA File Offset: 0x00137CBA
		public IFaction OtherFaction { get; private set; }

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06003F78 RID: 16248 RVA: 0x00139AC3 File Offset: 0x00137CC3
		public override TextObject Name
		{
			get
			{
				TextObject textObject = new TextObject("{=GZwNgIon}Declare war against {OTHER_FACTION}", null);
				textObject.SetTextVariable("OTHER_FACTION", this.OtherFaction.Name);
				return textObject;
			}
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x00139AE7 File Offset: 0x00137CE7
		public DeclareWarBarterable(IFaction declaringFaction, IFaction otherFaction) : base(declaringFaction.Leader, null)
		{
			this.DeclaringFaction = declaringFaction;
			this.OtherFaction = otherFaction;
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x00139B04 File Offset: 0x00137D04
		public override void Apply()
		{
			DeclareWarAction.ApplyByDefault(base.OriginalOwner.MapFaction, this.OtherFaction.MapFaction);
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x00139B24 File Offset: 0x00137D24
		public override int GetUnitValueForFaction(IFaction faction)
		{
			int result = 0;
			Clan evaluatingFaction = (faction is Clan) ? ((Clan)faction) : ((Kingdom)faction).RulingClan;
			if (faction.MapFaction == base.OriginalOwner.MapFaction)
			{
				TextObject textObject;
				result = (int)Campaign.Current.Models.DiplomacyModel.GetScoreOfDeclaringWar(base.OriginalOwner.MapFaction, this.OtherFaction.MapFaction, evaluatingFaction, out textObject);
			}
			else if (faction.MapFaction == this.OtherFaction.MapFaction)
			{
				TextObject textObject;
				result = (int)Campaign.Current.Models.DiplomacyModel.GetScoreOfDeclaringWar(this.OtherFaction.MapFaction, base.OriginalOwner.MapFaction, evaluatingFaction, out textObject);
			}
			return result;
		}

		// Token: 0x06003F7C RID: 16252 RVA: 0x00139BD6 File Offset: 0x00137DD6
		public override ImageIdentifier GetVisualIdentifier()
		{
			return null;
		}
	}
}

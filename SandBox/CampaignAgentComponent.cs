using System;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.LinQuick;
using TaleWorlds.MountAndBlade;

namespace SandBox
{
	// Token: 0x0200001E RID: 30
	public class CampaignAgentComponent : AgentComponent
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00005A0C File Offset: 0x00003C0C
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00005A14 File Offset: 0x00003C14
		public AgentNavigator AgentNavigator { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00005A1D File Offset: 0x00003C1D
		public PartyBase OwnerParty
		{
			get
			{
				IAgentOriginBase origin = this.Agent.Origin;
				return (PartyBase)((origin != null) ? origin.BattleCombatant : null);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005A3B File Offset: 0x00003C3B
		public CampaignAgentComponent(Agent agent) : base(agent)
		{
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005A44 File Offset: 0x00003C44
		public AgentNavigator CreateAgentNavigator(LocationCharacter locationCharacter)
		{
			this.AgentNavigator = new AgentNavigator(this.Agent, locationCharacter);
			return this.AgentNavigator;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005A5E File Offset: 0x00003C5E
		public AgentNavigator CreateAgentNavigator()
		{
			this.AgentNavigator = new AgentNavigator(this.Agent);
			return this.AgentNavigator;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005A77 File Offset: 0x00003C77
		public void OnAgentRemoved(Agent agent)
		{
			AgentNavigator agentNavigator = this.AgentNavigator;
			if (agentNavigator == null)
			{
				return;
			}
			agentNavigator.OnAgentRemoved(agent);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005A8A File Offset: 0x00003C8A
		public override void OnTickAsAI(float dt)
		{
			AgentNavigator agentNavigator = this.AgentNavigator;
			if (agentNavigator == null)
			{
				return;
			}
			agentNavigator.Tick(dt, false);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005AA0 File Offset: 0x00003CA0
		public override float GetMoraleDecreaseConstant()
		{
			PartyBase ownerParty = this.OwnerParty;
			if (((ownerParty != null) ? ownerParty.MapEvent : null) == null || !this.OwnerParty.MapEvent.IsSiegeAssault)
			{
				return 1f;
			}
			if (this.OwnerParty.MapEvent.AttackerSide.Parties.FindIndexQ((MapEventParty p) => p.Party == this.OwnerParty) < 0)
			{
				return 0.5f;
			}
			return 0.33f;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005B10 File Offset: 0x00003D10
		public override float GetMoraleAddition()
		{
			float num = 0f;
			PartyBase ownerParty = this.OwnerParty;
			if (((ownerParty != null) ? ownerParty.MapEvent : null) != null)
			{
				float num2;
				float num3;
				this.OwnerParty.MapEvent.GetStrengthsRelativeToParty(this.OwnerParty.Side, out num2, out num3);
				if (this.OwnerParty.IsMobile)
				{
					float num4 = (this.OwnerParty.MobileParty.Morale - 50f) / 2f;
					num += num4;
				}
				float num5 = num2 / (num2 + num3) * 10f - 5f;
				num += num5;
			}
			return num;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005B9D File Offset: 0x00003D9D
		public override void OnStopUsingGameObject()
		{
			if (this.Agent.IsAIControlled)
			{
				AgentNavigator agentNavigator = this.AgentNavigator;
				if (agentNavigator == null)
				{
					return;
				}
				agentNavigator.OnStopUsingGameObject();
			}
		}
	}
}

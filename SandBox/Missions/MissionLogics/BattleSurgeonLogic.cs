using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000049 RID: 73
	public class BattleSurgeonLogic : MissionLogic
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x00011880 File Offset: 0x0000FA80
		protected override void OnGetAgentState(Agent agent, bool usedSurgery)
		{
			if (usedSurgery)
			{
				PartyBase ownerParty = agent.GetComponent<CampaignAgentComponent>().OwnerParty;
				Agent agent2;
				if (ownerParty != null && this._surgeonAgents.TryGetValue(ownerParty.Id, out agent2) && agent2.State == AgentState.Active)
				{
					SkillLevelingManager.OnSurgeryApplied(ownerParty.MobileParty, true, ((CharacterObject)agent.Character).Tier);
				}
			}
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000118DC File Offset: 0x0000FADC
		public override void OnAgentCreated(Agent agent)
		{
			base.OnAgentCreated(agent);
			CharacterObject characterObject = (CharacterObject)agent.Character;
			bool flag;
			if (characterObject == null)
			{
				flag = (null != null);
			}
			else
			{
				Hero heroObject = characterObject.HeroObject;
				flag = (((heroObject != null) ? heroObject.PartyBelongedTo : null) != null);
			}
			if (flag && characterObject.HeroObject == characterObject.HeroObject.PartyBelongedTo.EffectiveSurgeon)
			{
				string id = characterObject.HeroObject.PartyBelongedTo.Party.Id;
				if (this._surgeonAgents.ContainsKey(id))
				{
					this._surgeonAgents.Remove(id);
				}
				this._surgeonAgents.Add(id, agent);
			}
		}

		// Token: 0x04000146 RID: 326
		private Dictionary<string, Agent> _surgeonAgents = new Dictionary<string, Agent>();
	}
}

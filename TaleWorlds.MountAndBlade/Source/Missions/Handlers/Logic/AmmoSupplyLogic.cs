using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.Source.Missions.Handlers.Logic
{
	// Token: 0x020003BC RID: 956
	public class AmmoSupplyLogic : MissionLogic
	{
		// Token: 0x0600330B RID: 13067 RVA: 0x000D47A6 File Offset: 0x000D29A6
		public AmmoSupplyLogic(List<BattleSideEnum> sideList)
		{
			this._sideList = sideList;
			this._checkTimer = new BasicMissionTimer();
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x000D47C0 File Offset: 0x000D29C0
		public bool IsAgentEligibleForAmmoSupply(Agent agent)
		{
			if (agent.IsAIControlled && this._sideList.Contains(agent.Team.Side))
			{
				for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
				{
					if (!agent.Equipment[equipmentIndex].IsEmpty && agent.Equipment[equipmentIndex].IsAnyAmmo())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x000D4828 File Offset: 0x000D2A28
		public override void OnMissionTick(float dt)
		{
			if (this._checkTimer.ElapsedTime > 3f)
			{
				this._checkTimer.Reset();
				foreach (Team team in base.Mission.Teams)
				{
					if (this._sideList.IndexOf(team.Side) >= 0)
					{
						foreach (Agent agent in team.ActiveAgents)
						{
							for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
							{
								if (agent.IsAIControlled && !agent.Equipment[equipmentIndex].IsEmpty && agent.Equipment[equipmentIndex].IsAnyAmmo())
								{
									short modifiedMaxAmount = agent.Equipment[equipmentIndex].ModifiedMaxAmount;
									short amount = agent.Equipment[equipmentIndex].Amount;
									short num = modifiedMaxAmount;
									if (modifiedMaxAmount > 1)
									{
										num = modifiedMaxAmount - 1;
									}
									if (amount < num)
									{
										agent.SetWeaponAmountInSlot(equipmentIndex, num, false);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x04001629 RID: 5673
		private const float CheckTimePeriod = 3f;

		// Token: 0x0400162A RID: 5674
		private readonly List<BattleSideEnum> _sideList;

		// Token: 0x0400162B RID: 5675
		private readonly BasicMissionTimer _checkTimer;
	}
}

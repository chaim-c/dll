using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002CF RID: 719
	public class MPPerksAgentComponent : AgentComponent
	{
		// Token: 0x060027B7 RID: 10167 RVA: 0x00099294 File Offset: 0x00097494
		public MPPerksAgentComponent(Agent agent) : base(agent)
		{
			this.Agent.OnAgentHealthChanged += this.OnHealthChanged;
			if (this.Agent.HasMount)
			{
				this.Agent.MountAgent.OnAgentHealthChanged += this.OnMountHealthChanged;
			}
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x000992E8 File Offset: 0x000974E8
		public override void OnMount(Agent mount)
		{
			mount.OnAgentHealthChanged += this.OnMountHealthChanged;
			mount.UpdateAgentProperties();
			MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(this.Agent);
			if (perkHandler == null)
			{
				return;
			}
			perkHandler.OnEvent(this.Agent, MPPerkCondition.PerkEventFlags.MountChange);
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x00099322 File Offset: 0x00097522
		public override void OnDismount(Agent mount)
		{
			mount.OnAgentHealthChanged -= this.OnMountHealthChanged;
			mount.UpdateAgentProperties();
			MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(this.Agent);
			if (perkHandler == null)
			{
				return;
			}
			perkHandler.OnEvent(this.Agent, MPPerkCondition.PerkEventFlags.MountChange);
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x0009935C File Offset: 0x0009755C
		public override void OnItemPickup(SpawnedItemEntity item)
		{
			if (!item.WeaponCopy.IsEmpty && item.WeaponCopy.Item.ItemType == ItemObject.ItemTypeEnum.Banner)
			{
				MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(this.Agent);
				if (perkHandler == null)
				{
					return;
				}
				perkHandler.OnEvent(MPPerkCondition.PerkEventFlags.BannerPickUp);
			}
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x000993A7 File Offset: 0x000975A7
		public override void OnWeaponDrop(MissionWeapon droppedWeapon)
		{
			if (!droppedWeapon.IsEmpty && droppedWeapon.Item.ItemType == ItemObject.ItemTypeEnum.Banner)
			{
				MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(this.Agent);
				if (perkHandler == null)
				{
					return;
				}
				perkHandler.OnEvent(MPPerkCondition.PerkEventFlags.BannerDrop);
			}
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x000993DC File Offset: 0x000975DC
		public override void OnAgentRemoved()
		{
			if (this.Agent.HasMount)
			{
				this.Agent.MountAgent.OnAgentHealthChanged -= this.OnMountHealthChanged;
				this.Agent.MountAgent.UpdateAgentProperties();
			}
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x00099417 File Offset: 0x00097617
		private void OnHealthChanged(Agent agent, float oldHealth, float newHealth)
		{
			MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(this.Agent);
			if (perkHandler == null)
			{
				return;
			}
			perkHandler.OnEvent(agent, MPPerkCondition.PerkEventFlags.HealthChange);
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x00099430 File Offset: 0x00097630
		private void OnMountHealthChanged(Agent agent, float oldHealth, float newHealth)
		{
			if (!this.Agent.IsActive() || this.Agent.MountAgent != agent)
			{
				agent.OnAgentHealthChanged -= this.OnMountHealthChanged;
				return;
			}
			MPPerkObject.MPPerkHandler perkHandler = MPPerkObject.GetPerkHandler(this.Agent);
			if (perkHandler == null)
			{
				return;
			}
			perkHandler.OnEvent(this.Agent, MPPerkCondition.PerkEventFlags.MountHealthChange);
		}
	}
}

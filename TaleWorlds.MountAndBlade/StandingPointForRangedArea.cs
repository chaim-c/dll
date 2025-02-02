using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000347 RID: 839
	public class StandingPointForRangedArea : StandingPoint
	{
		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06002E08 RID: 11784 RVA: 0x000BBA65 File Offset: 0x000B9C65
		public override Agent.AIScriptedFrameFlags DisableScriptedFrameFlags
		{
			get
			{
				return Agent.AIScriptedFrameFlags.NoAttack | Agent.AIScriptedFrameFlags.ConsiderRotation;
			}
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x000BBA68 File Offset: 0x000B9C68
		protected internal override void OnInit()
		{
			base.OnInit();
			this.AutoSheathWeapons = false;
			this.LockUserFrames = false;
			this.LockUserPositions = true;
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x000BBA94 File Offset: 0x000B9C94
		public override bool IsDisabledForAgent(Agent agent)
		{
			EquipmentIndex wieldedItemIndex = agent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
			if (wieldedItemIndex == EquipmentIndex.None)
			{
				return true;
			}
			WeaponComponentData currentUsageItem = agent.Equipment[wieldedItemIndex].CurrentUsageItem;
			if (currentUsageItem == null || !currentUsageItem.IsRangedWeapon)
			{
				return true;
			}
			if (wieldedItemIndex == EquipmentIndex.ExtraWeaponSlot)
			{
				return this.ThrowingValueMultiplier <= 0f || base.IsDisabledForAgent(agent);
			}
			return this.RangedWeaponValueMultiplier <= 0f || base.IsDisabledForAgent(agent);
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x000BBB04 File Offset: 0x000B9D04
		public override float GetUsageScoreForAgent(Agent agent)
		{
			EquipmentIndex wieldedItemIndex = agent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
			float num = 0f;
			if (wieldedItemIndex != EquipmentIndex.None && agent.Equipment[wieldedItemIndex].CurrentUsageItem.IsRangedWeapon)
			{
				num = ((wieldedItemIndex == EquipmentIndex.ExtraWeaponSlot) ? this.ThrowingValueMultiplier : this.RangedWeaponValueMultiplier);
			}
			return base.GetUsageScoreForAgent(agent) + num;
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x000BBB5A File Offset: 0x000B9D5A
		public override bool HasAlternative()
		{
			return true;
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x000BBB5D File Offset: 0x000B9D5D
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (base.HasUser)
			{
				return base.GetTickRequirement() | ScriptComponentBehavior.TickRequirement.TickParallel2;
			}
			return base.GetTickRequirement();
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x000BBB76 File Offset: 0x000B9D76
		protected internal override void OnTickParallel2(float dt)
		{
			base.OnTickParallel2(dt);
			if (base.HasUser && this.IsDisabledForAgent(base.UserAgent))
			{
				base.UserAgent.StopUsingGameObjectMT(false, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
			}
		}

		// Token: 0x04001330 RID: 4912
		public float ThrowingValueMultiplier = 5f;

		// Token: 0x04001331 RID: 4913
		public float RangedWeaponValueMultiplier = 2f;
	}
}

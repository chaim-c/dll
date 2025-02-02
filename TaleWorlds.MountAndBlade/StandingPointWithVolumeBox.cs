using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200034A RID: 842
	public class StandingPointWithVolumeBox : StandingPointWithWeaponRequirement
	{
		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x000BBC55 File Offset: 0x000B9E55
		public override Agent.AIScriptedFrameFlags DisableScriptedFrameFlags
		{
			get
			{
				return Agent.AIScriptedFrameFlags.NoAttack;
			}
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x000BBC58 File Offset: 0x000B9E58
		public override bool IsDisabledForAgent(Agent agent)
		{
			return base.IsDisabledForAgent(agent) || MathF.Abs(agent.Position.z - base.GameEntity.GlobalPosition.z) > 2f || agent.Position.DistanceSquared(base.GameEntity.GlobalPosition) > 100f;
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x000BBCB8 File Offset: 0x000B9EB8
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			MBEditor.IsEntitySelected(base.GameEntity);
		}

		// Token: 0x04001334 RID: 4916
		private const float MaxUserAgentDistance = 10f;

		// Token: 0x04001335 RID: 4917
		private const float MaxUserAgentElevation = 2f;

		// Token: 0x04001336 RID: 4918
		public string VolumeBoxTag = "volumebox";
	}
}

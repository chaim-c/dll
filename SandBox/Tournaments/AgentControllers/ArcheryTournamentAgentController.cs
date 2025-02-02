using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Tournaments.MissionLogics;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Tournaments.AgentControllers
{
	// Token: 0x02000032 RID: 50
	public class ArcheryTournamentAgentController : AgentController
	{
		// Token: 0x060001AB RID: 427 RVA: 0x0000B8EE File Offset: 0x00009AEE
		public override void OnInitialize()
		{
			this._missionController = Mission.Current.GetMissionBehavior<TournamentArcheryMissionController>();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000B900 File Offset: 0x00009B00
		public void OnTick()
		{
			if (!base.Owner.IsAIControlled)
			{
				return;
			}
			this.UpdateTarget();
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000B916 File Offset: 0x00009B16
		public void SetTargets(List<DestructableComponent> targetList)
		{
			this._targetList = targetList;
			this._target = null;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000B926 File Offset: 0x00009B26
		private void UpdateTarget()
		{
			if (this._target == null || this._target.IsDestroyed)
			{
				this.SelectNewTarget();
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000B944 File Offset: 0x00009B44
		private void SelectNewTarget()
		{
			List<KeyValuePair<float, DestructableComponent>> list = new List<KeyValuePair<float, DestructableComponent>>();
			foreach (DestructableComponent destructableComponent in this._targetList)
			{
				float score = this.GetScore(destructableComponent);
				if (score > 0f)
				{
					list.Add(new KeyValuePair<float, DestructableComponent>(score, destructableComponent));
				}
			}
			if (list.Count == 0)
			{
				this._target = null;
				base.Owner.DisableScriptedCombatMovement();
				WorldPosition worldPosition = base.Owner.GetWorldPosition();
				base.Owner.SetScriptedPosition(ref worldPosition, false, Agent.AIScriptedFrameFlags.None);
			}
			else
			{
				List<KeyValuePair<float, DestructableComponent>> list2 = (from x in list
				orderby x.Key descending
				select x).ToList<KeyValuePair<float, DestructableComponent>>();
				int maxValue = MathF.Min(list2.Count, 5);
				this._target = list2[MBRandom.RandomInt(maxValue)].Value;
			}
			if (this._target != null)
			{
				base.Owner.SetScriptedTargetEntityAndPosition(this._target.GameEntity, base.Owner.GetWorldPosition(), Agent.AISpecialCombatModeFlags.None, false);
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000BA70 File Offset: 0x00009C70
		private float GetScore(DestructableComponent target)
		{
			if (!target.IsDestroyed)
			{
				return 1f / base.Owner.Position.DistanceSquared(target.GameEntity.GlobalPosition);
			}
			return 0f;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000BAAF File Offset: 0x00009CAF
		public void OnTargetHit(Agent agent, DestructableComponent target)
		{
			if (agent == base.Owner || target == this._target)
			{
				this.SelectNewTarget();
			}
		}

		// Token: 0x0400009D RID: 157
		private List<DestructableComponent> _targetList;

		// Token: 0x0400009E RID: 158
		private DestructableComponent _target;

		// Token: 0x0400009F RID: 159
		private TournamentArcheryMissionController _missionController;
	}
}

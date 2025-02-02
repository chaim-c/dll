using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Missions.Handlers
{
	// Token: 0x020003C0 RID: 960
	public class BattleDeploymentHandler : DeploymentHandler
	{
		// Token: 0x1400009F RID: 159
		// (add) Token: 0x0600331D RID: 13085 RVA: 0x000D4D64 File Offset: 0x000D2F64
		// (remove) Token: 0x0600331E RID: 13086 RVA: 0x000D4D9C File Offset: 0x000D2F9C
		public event Action OnDeploymentReady;

		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x0600331F RID: 13087 RVA: 0x000D4DD4 File Offset: 0x000D2FD4
		// (remove) Token: 0x06003320 RID: 13088 RVA: 0x000D4E0C File Offset: 0x000D300C
		public event Action OnAIDeploymentReady;

		// Token: 0x06003321 RID: 13089 RVA: 0x000D4E41 File Offset: 0x000D3041
		public BattleDeploymentHandler(bool isPlayerAttacker) : base(isPlayerAttacker)
		{
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x000D4E4A File Offset: 0x000D304A
		public override void OnTeamDeployed(Team team)
		{
			if (team.IsPlayerTeam)
			{
				Action onDeploymentReady = this.OnDeploymentReady;
				if (onDeploymentReady == null)
				{
					return;
				}
				onDeploymentReady();
				return;
			}
			else
			{
				Action onAIDeploymentReady = this.OnAIDeploymentReady;
				if (onAIDeploymentReady == null)
				{
					return;
				}
				onAIDeploymentReady();
				return;
			}
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x000D4E75 File Offset: 0x000D3075
		public override void FinishDeployment()
		{
			base.FinishDeployment();
			Mission mission = base.Mission ?? Mission.Current;
			mission.GetMissionBehavior<DeploymentMissionController>().FinishDeployment();
			mission.IsTeleportingAgents = false;
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x000D4EA0 File Offset: 0x000D30A0
		public Vec2 GetEstimatedAverageDefenderPosition()
		{
			WorldPosition worldPosition;
			Vec2 vec;
			base.Mission.GetFormationSpawnFrame(BattleSideEnum.Defender, FormationClass.Infantry, false, out worldPosition, out vec);
			return worldPosition.AsVec2;
		}
	}
}

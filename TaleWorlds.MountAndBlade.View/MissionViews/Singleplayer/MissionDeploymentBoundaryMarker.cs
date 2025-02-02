using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer
{
	// Token: 0x02000068 RID: 104
	public class MissionDeploymentBoundaryMarker : MissionView
	{
		// Token: 0x06000420 RID: 1056 RVA: 0x00022A33 File Offset: 0x00020C33
		public MissionDeploymentBoundaryMarker(IEntityFactory entityFactory, float markerInterval = 2f)
		{
			this.entityFactory = entityFactory;
			this.MarkerInterval = Math.Max(markerInterval, 0.0001f);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00022A68 File Offset: 0x00020C68
		public override void AfterStart()
		{
			base.AfterStart();
			for (int i = 0; i < 2; i++)
			{
				this.boundaryMarkersPerSide[i] = new Dictionary<string, List<GameEntity>>();
			}
			this._boundaryMarkersRemoved = false;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00022A9B File Offset: 0x00020C9B
		protected override void OnEndMission()
		{
			base.OnEndMission();
			this.TryRemoveBoundaryMarkers();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00022AAC File Offset: 0x00020CAC
		public override void OnInitialDeploymentPlanMadeForSide(BattleSideEnum side, bool isFirstPlan)
		{
			bool flag = base.Mission.DeploymentPlan.HasDeploymentBoundaries(side);
			if (isFirstPlan && flag)
			{
				foreach (ValueTuple<string, List<Vec2>> valueTuple in base.Mission.DeploymentPlan.GetDeploymentBoundaries(side))
				{
					this.AddBoundaryMarkerForSide(side, new KeyValuePair<string, ICollection<Vec2>>(valueTuple.Item1, valueTuple.Item2));
				}
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00022B34 File Offset: 0x00020D34
		public override void OnRemoveBehavior()
		{
			this.TryRemoveBoundaryMarkers();
			base.OnRemoveBehavior();
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00022B44 File Offset: 0x00020D44
		private void AddBoundaryMarkerForSide(BattleSideEnum side, KeyValuePair<string, ICollection<Vec2>> boundary)
		{
			string key = boundary.Key;
			if (!this.boundaryMarkersPerSide[(int)side].ContainsKey(key))
			{
				Banner banner = (side == BattleSideEnum.Attacker) ? base.Mission.AttackerTeam.Banner : ((side == BattleSideEnum.Defender) ? base.Mission.DefenderTeam.Banner : null);
				List<GameEntity> list = new List<GameEntity>();
				List<Vec2> list2 = boundary.Value.ToList<Vec2>();
				for (int i = 0; i < list2.Count; i++)
				{
					this.MarkLine(new Vec3(list2[i], 0f, -1f), new Vec3(list2[(i + 1) % list2.Count], 0f, -1f), list, banner);
				}
				this.boundaryMarkersPerSide[(int)side][key] = list;
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00022C18 File Offset: 0x00020E18
		private void TryRemoveBoundaryMarkers()
		{
			if (!this._boundaryMarkersRemoved)
			{
				for (int i = 0; i < 2; i++)
				{
					foreach (string boundaryName in this.boundaryMarkersPerSide[i].Keys.ToList<string>())
					{
						this.RemoveBoundaryMarker(boundaryName, (BattleSideEnum)i);
					}
				}
				this._boundaryMarkersRemoved = true;
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00022C94 File Offset: 0x00020E94
		private void RemoveBoundaryMarker(string boundaryName, BattleSideEnum side)
		{
			List<GameEntity> list;
			if (this.boundaryMarkersPerSide[(int)side].TryGetValue(boundaryName, out list))
			{
				foreach (GameEntity gameEntity in list)
				{
					gameEntity.Remove(103);
				}
				this.boundaryMarkersPerSide[(int)side].Remove(boundaryName);
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00022D04 File Offset: 0x00020F04
		protected void MarkLine(Vec3 startPoint, Vec3 endPoint, List<GameEntity> boundary, Banner banner = null)
		{
			Scene scene = base.Mission.Scene;
			Vec3 vec = endPoint - startPoint;
			float length = vec.Length;
			Vec3 vec2 = vec;
			vec2.Normalize();
			vec2 *= this.MarkerInterval;
			for (float num = 0f; num < length; num += this.MarkerInterval)
			{
				MatrixFrame identity = MatrixFrame.Identity;
				identity.rotation.RotateAboutUp(vec.RotationZ + 1.5707964f);
				identity.origin = startPoint;
				if (!scene.GetHeightAtPoint(identity.origin.AsVec2, BodyFlags.CommonCollisionExcludeFlagsForCombat, ref identity.origin.z))
				{
					identity.origin.z = 0f;
				}
				identity.origin.z = identity.origin.z - 0.5f;
				identity.Scale(Vec3.One * 0.4f);
				GameEntity gameEntity = this.entityFactory.MakeEntity(new object[]
				{
					banner
				});
				gameEntity.SetFrame(ref identity);
				boundary.Add(gameEntity);
				startPoint += vec2;
			}
		}

		// Token: 0x040002AC RID: 684
		public const string AttackerStaticDeploymentBoundaryName = "walk_area";

		// Token: 0x040002AD RID: 685
		public const string DefenderStaticDeploymentBoundaryName = "deployment_castle_boundary";

		// Token: 0x040002AE RID: 686
		public readonly float MarkerInterval;

		// Token: 0x040002AF RID: 687
		private readonly Dictionary<string, List<GameEntity>>[] boundaryMarkersPerSide = new Dictionary<string, List<GameEntity>>[2];

		// Token: 0x040002B0 RID: 688
		private readonly IEntityFactory entityFactory;

		// Token: 0x040002B1 RID: 689
		private bool _boundaryMarkersRemoved = true;
	}
}

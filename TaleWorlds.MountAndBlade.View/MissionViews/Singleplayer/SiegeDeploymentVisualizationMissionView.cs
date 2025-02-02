using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer
{
	// Token: 0x02000075 RID: 117
	public class SiegeDeploymentVisualizationMissionView : MissionView
	{
		// Token: 0x0600043F RID: 1087 RVA: 0x000230A8 File Offset: 0x000212A8
		public override void AfterStart()
		{
			base.AfterStart();
			this._deploymentPoints = (from dp in Mission.Current.ActiveMissionObjects.FindAllWithType<DeploymentPoint>()
			where !dp.IsDisabled
			select dp).ToList<DeploymentPoint>();
			foreach (DeploymentPoint deploymentPoint in this._deploymentPoints)
			{
				deploymentPoint.OnDeploymentPointTypeDetermined += this.OnDeploymentPointStateSet;
				deploymentPoint.OnDeploymentStateChanged += this.OnDeploymentStateChanged;
			}
			this._deploymentPointsVisible = true;
			Mission.Current.GetMissionBehavior<SiegeDeploymentMissionController>();
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0002316C File Offset: 0x0002136C
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			this._deploymentMissionView = base.Mission.GetMissionBehavior<DeploymentMissionView>();
			DeploymentMissionView deploymentMissionView = this._deploymentMissionView;
			deploymentMissionView.OnDeploymentFinish = (OnPlayerDeploymentFinishDelegate)Delegate.Combine(deploymentMissionView.OnDeploymentFinish, new OnPlayerDeploymentFinishDelegate(this.OnDeploymentFinish));
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000231AC File Offset: 0x000213AC
		public void OnDeploymentFinish()
		{
			DeploymentMissionView deploymentMissionView = this._deploymentMissionView;
			deploymentMissionView.OnDeploymentFinish = (OnPlayerDeploymentFinishDelegate)Delegate.Remove(deploymentMissionView.OnDeploymentFinish, new OnPlayerDeploymentFinishDelegate(this.OnDeploymentFinish));
			this._deploymentMissionView = null;
			this.TryRemoveDeploymentVisibilities();
			Mission.Current.RemoveMissionBehavior(this);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000231F8 File Offset: 0x000213F8
		protected override void OnEndMission()
		{
			base.OnEndMission();
			this.TryRemoveDeploymentVisibilities();
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00023206 File Offset: 0x00021406
		public override void OnRemoveBehavior()
		{
			base.OnRemoveBehavior();
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00023210 File Offset: 0x00021410
		private void TryRemoveDeploymentVisibilities()
		{
			if (this._deploymentPointsVisible)
			{
				foreach (DeploymentPoint deploymentPoint in this._deploymentPoints)
				{
					this.RemoveDeploymentVisibility(deploymentPoint);
					deploymentPoint.OnDeploymentStateChanged -= this.OnDeploymentStateChanged;
				}
				this._deploymentPointsVisible = false;
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00023284 File Offset: 0x00021484
		private void RemoveDeploymentVisibility(DeploymentPoint deploymentPoint)
		{
			switch (deploymentPoint.GetDeploymentPointType())
			{
			case DeploymentPoint.DeploymentPointType.BatteringRam:
				this.HideDeploymentBanners(deploymentPoint, true);
				this.SetGhostVisibility(deploymentPoint, false);
				return;
			case DeploymentPoint.DeploymentPointType.TowerLadder:
				this.HideDeploymentBanners(deploymentPoint, true);
				this.SetGhostVisibility(deploymentPoint, false);
				this.SetDeploymentTargetContourState(deploymentPoint, false);
				this.SetLaddersUpState(deploymentPoint, false);
				this.SetLightState(deploymentPoint, false);
				return;
			case DeploymentPoint.DeploymentPointType.Breach:
				this.HideDeploymentBanners(deploymentPoint, true);
				this.SetDeploymentTargetContourState(deploymentPoint, false);
				this.SetLightState(deploymentPoint, false);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00023300 File Offset: 0x00021500
		private static string GetSelectorStateDescription()
		{
			string text = "";
			for (int i = 1; i < 1023; i *= 2)
			{
				if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & i) > 0)
				{
					string str = text;
					string str2 = " ";
					SiegeDeploymentVisualizationMissionView.DeploymentVisualizationPreference deploymentVisualizationPreference = (SiegeDeploymentVisualizationMissionView.DeploymentVisualizationPreference)i;
					text = str + str2 + deploymentVisualizationPreference.ToString();
				}
			}
			return text;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00023349 File Offset: 0x00021549
		[CommandLineFunctionality.CommandLineArgumentFunction("set_deployment_visualization_selector", "mission")]
		public static string SetDeploymentVisualizationSelector(List<string> strings)
		{
			if (strings.Count == 1 && int.TryParse(strings[0], out SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector))
			{
				return "Enabled deployment visualization options are:" + SiegeDeploymentVisualizationMissionView.GetSelectorStateDescription();
			}
			return "Format is \"mission.set_deployment_visualization_selector [integer > 0]\".";
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0002337C File Offset: 0x0002157C
		private void OnDeploymentStateChanged(DeploymentPoint deploymentPoint, SynchedMissionObject targetObject)
		{
			this.OnDeploymentPointStateSet(deploymentPoint);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00023388 File Offset: 0x00021588
		private void OnDeploymentPointStateSet(DeploymentPoint deploymentPoint)
		{
			switch (deploymentPoint.GetDeploymentPointState())
			{
			case DeploymentPoint.DeploymentPointState.NotDeployed:
				if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 1) > 0)
				{
					if (deploymentPoint.GetDeploymentPointType() == DeploymentPoint.DeploymentPointType.BatteringRam)
					{
						if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 2) > 0)
						{
							this.ShowLineFromDeploymentPointToTarget(deploymentPoint);
						}
						if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 4) > 0)
						{
							this.CreateArcPoints(deploymentPoint);
						}
						if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 8) > 0)
						{
							this.ShowDeploymentBanners(deploymentPoint);
						}
						if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 16) > 0)
						{
							this.ShowPath(deploymentPoint);
						}
						if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 32) > 0)
						{
							this.SetGhostVisibility(deploymentPoint, true);
							return;
						}
					}
				}
				else if (deploymentPoint.GetDeploymentPointType() == DeploymentPoint.DeploymentPointType.BatteringRam)
				{
					this.HideDeploymentBanners(deploymentPoint, false);
				}
				break;
			case DeploymentPoint.DeploymentPointState.BatteringRam:
			case DeploymentPoint.DeploymentPointState.SiegeTower:
				if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 2) > 0)
				{
					this.ShowLineFromDeploymentPointToTarget(deploymentPoint);
				}
				if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 4) > 0)
				{
					this.CreateArcPoints(deploymentPoint);
				}
				if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 8) > 0)
				{
					this.ShowDeploymentBanners(deploymentPoint);
				}
				if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 16) > 0)
				{
					this.ShowPath(deploymentPoint);
				}
				if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 32) > 0)
				{
					this.SetGhostVisibility(deploymentPoint, true);
				}
				this.SetLaddersUpState(deploymentPoint, false);
				this.SetLightState(deploymentPoint, false);
				return;
			case DeploymentPoint.DeploymentPointState.SiegeLadder:
				if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 2) > 0)
				{
					this.ShowLineFromDeploymentPointToTarget(deploymentPoint);
				}
				if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 4) > 0)
				{
					this.CreateArcPoints(deploymentPoint);
				}
				if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 8) > 0)
				{
					this.ShowDeploymentBanners(deploymentPoint);
				}
				if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 64) > 0)
				{
					this.SetDeploymentTargetContourState(deploymentPoint, true);
				}
				if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 128) > 0)
				{
					this.SetLaddersUpState(deploymentPoint, true);
				}
				if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 256) > 0)
				{
					this.SetLightState(deploymentPoint, true);
					return;
				}
				break;
			case DeploymentPoint.DeploymentPointState.Breach:
				if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 2) > 0)
				{
					this.ShowLineFromDeploymentPointToTarget(deploymentPoint);
				}
				if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 4) > 0)
				{
					this.CreateArcPoints(deploymentPoint);
				}
				if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 8) > 0)
				{
					this.ShowDeploymentBanners(deploymentPoint);
				}
				if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 64) > 0)
				{
					this.SetDeploymentTargetContourState(deploymentPoint, true);
				}
				if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 256) > 0)
				{
					this.SetLightState(deploymentPoint, true);
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00023578 File Offset: 0x00021778
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			foreach (DeploymentPoint deploymentPoint in this._deploymentPoints)
			{
				switch (deploymentPoint.GetDeploymentPointState())
				{
				case DeploymentPoint.DeploymentPointState.NotDeployed:
					if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 1) > 0 && deploymentPoint.GetDeploymentPointType() == DeploymentPoint.DeploymentPointType.BatteringRam)
					{
						if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 2) > 0)
						{
							this.ShowLineFromDeploymentPointToTarget(deploymentPoint);
						}
						if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 4) > 0)
						{
							this.ShowArcFromDeploymentPointToTarget(deploymentPoint);
						}
						if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 16) > 0)
						{
							this.ShowPath(deploymentPoint);
						}
					}
					break;
				case DeploymentPoint.DeploymentPointState.BatteringRam:
				case DeploymentPoint.DeploymentPointState.SiegeTower:
					if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 2) > 0)
					{
						this.ShowLineFromDeploymentPointToTarget(deploymentPoint);
					}
					if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 4) > 0)
					{
						this.ShowArcFromDeploymentPointToTarget(deploymentPoint);
					}
					if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 16) > 0)
					{
						this.ShowPath(deploymentPoint);
					}
					break;
				case DeploymentPoint.DeploymentPointState.SiegeLadder:
					if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 2) > 0)
					{
						this.ShowLineFromDeploymentPointToTarget(deploymentPoint);
					}
					if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 4) > 0)
					{
						this.ShowArcFromDeploymentPointToTarget(deploymentPoint);
					}
					break;
				case DeploymentPoint.DeploymentPointState.Breach:
					if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 2) > 0)
					{
						this.ShowLineFromDeploymentPointToTarget(deploymentPoint);
					}
					if ((SiegeDeploymentVisualizationMissionView.deploymentVisualizerSelector & 4) > 0)
					{
						this.ShowArcFromDeploymentPointToTarget(deploymentPoint);
					}
					break;
				}
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x000236D0 File Offset: 0x000218D0
		private void ShowLineFromDeploymentPointToTarget(DeploymentPoint deploymentPoint)
		{
			deploymentPoint.GetDeploymentOrigin();
			Vec3 deploymentTargetPosition = deploymentPoint.DeploymentTargetPosition;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000236E0 File Offset: 0x000218E0
		private List<Vec3> CreateArcPoints(DeploymentPoint deploymentPoint)
		{
			Vec3 deploymentOrigin = deploymentPoint.GetDeploymentOrigin();
			Vec3 deploymentTargetPosition = deploymentPoint.DeploymentTargetPosition;
			float num = (deploymentTargetPosition - deploymentOrigin).Length / 3f;
			List<Vec3> list = new List<Vec3>();
			int num2 = 0;
			while ((float)num2 < num)
			{
				Vec3 item = MBMath.Lerp(deploymentOrigin, deploymentTargetPosition, (float)num2 / num, 0f);
				float num3 = 8f - MathF.Pow(MathF.Abs((float)num2 - num * 0.5f) / (num * 0.5f), 1.2f) * 8f;
				item.z += num3;
				list.Add(item);
				num2++;
			}
			list.Add(deploymentTargetPosition);
			return list;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0002378C File Offset: 0x0002198C
		private void ShowArcFromDeploymentPointToTarget(DeploymentPoint deploymentPoint)
		{
			Vec3 deploymentTargetPosition = deploymentPoint.DeploymentTargetPosition;
			List<Vec3> list;
			this._deploymentArcs.TryGetValue(deploymentPoint, out list);
			if (list == null || list[list.Count - 1] != deploymentTargetPosition)
			{
				list = this.CreateArcPoints(deploymentPoint);
			}
			Vec3 vec = Vec3.Invalid;
			foreach (Vec3 vec2 in list)
			{
				bool isValid = vec.IsValid;
				vec = vec2;
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00023818 File Offset: 0x00021A18
		private void ShowDeploymentBanners(DeploymentPoint deploymentPoint)
		{
			Vec3 deploymentOrigin = deploymentPoint.GetDeploymentOrigin();
			Vec3 deploymentTargetPosition = deploymentPoint.DeploymentTargetPosition;
			ValueTuple<GameEntity, GameEntity> valueTuple;
			this._deploymentBanners.TryGetValue(deploymentPoint, out valueTuple);
			if (valueTuple.Item1 == null || valueTuple.Item2 == null)
			{
				valueTuple = this.CreateBanners(deploymentPoint);
			}
			GameEntity item = this._deploymentBanners[deploymentPoint].Item1;
			MatrixFrame identity = MatrixFrame.Identity;
			identity.origin = deploymentOrigin;
			identity.origin.z = identity.origin.z + 7.5f;
			identity.rotation.ApplyScaleLocal(10f);
			MatrixFrame matrixFrame = identity;
			item.SetFrame(ref matrixFrame);
			item.SetVisibilityExcludeParents(true);
			item.SetAlpha(1f);
			GameEntity item2 = this._deploymentBanners[deploymentPoint].Item2;
			identity = MatrixFrame.Identity;
			identity.origin = deploymentTargetPosition;
			identity.origin.z = identity.origin.z + 7.5f;
			identity.rotation.ApplyScaleLocal(10f);
			MatrixFrame matrixFrame2 = identity;
			item2.SetFrame(ref matrixFrame2);
			item2.SetVisibilityExcludeParents(true);
			item2.SetAlpha(1f);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00023928 File Offset: 0x00021B28
		private void HideDeploymentBanners(DeploymentPoint deploymentPoint, bool isRemoving = false)
		{
			ValueTuple<GameEntity, GameEntity> valueTuple;
			this._deploymentBanners.TryGetValue(deploymentPoint, out valueTuple);
			if (valueTuple.Item1 != null && valueTuple.Item2 != null)
			{
				if (isRemoving)
				{
					valueTuple.Item1.Remove(104);
					valueTuple.Item2.Remove(105);
					return;
				}
				valueTuple.Item1.SetVisibilityExcludeParents(false);
				valueTuple.Item2.SetVisibilityExcludeParents(false);
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00023998 File Offset: 0x00021B98
		private ValueTuple<GameEntity, GameEntity> CreateBanners(DeploymentPoint deploymentPoint)
		{
			GameEntity gameEntity = this.CreateBannerEntity(false);
			gameEntity.SetVisibilityExcludeParents(false);
			GameEntity gameEntity2 = this.CreateBannerEntity(true);
			gameEntity2.SetVisibilityExcludeParents(false);
			ValueTuple<GameEntity, GameEntity> valueTuple = new ValueTuple<GameEntity, GameEntity>(gameEntity, gameEntity2);
			this._deploymentBanners.Add(deploymentPoint, valueTuple);
			return valueTuple;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000239DC File Offset: 0x00021BDC
		private GameEntity CreateBannerEntity(bool isTargetEntity)
		{
			GameEntity gameEntity = GameEntity.CreateEmpty(Mission.Current.Scene, true);
			gameEntity.EntityFlags |= EntityFlags.NoOcclusionCulling;
			uint color = 4278190080U;
			uint color2;
			if (!isTargetEntity)
			{
				color2 = 2141323264U;
			}
			else
			{
				color2 = 2131100887U;
			}
			gameEntity.AddMultiMesh(MetaMesh.GetCopy("billboard_unit_mesh", true, false), true);
			gameEntity.GetFirstMesh().Color = uint.MaxValue;
			Material material = Material.GetFromResource("formation_icon").CreateCopy();
			if (isTargetEntity)
			{
				Texture fromResource = Texture.GetFromResource("plain_yellow");
				material.SetTexture(Material.MBTextureType.DiffuseMap2, fromResource);
			}
			else
			{
				Texture fromResource2 = Texture.GetFromResource("plain_blue");
				material.SetTexture(Material.MBTextureType.DiffuseMap2, fromResource2);
			}
			gameEntity.GetFirstMesh().SetMaterial(material);
			gameEntity.GetFirstMesh().Color = color2;
			gameEntity.GetFirstMesh().Color2 = color;
			gameEntity.GetFirstMesh().SetVectorArgument(0f, 1f, 0f, 1f);
			return gameEntity;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00023AC1 File Offset: 0x00021CC1
		private void ShowPath(DeploymentPoint deploymentPoint)
		{
			(deploymentPoint.GetWeaponsUnder().FirstOrDefault((SynchedMissionObject wu) => wu is IMoveableSiegeWeapon) as IMoveableSiegeWeapon).HighlightPath();
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00023AF7 File Offset: 0x00021CF7
		private void SetGhostVisibility(DeploymentPoint deploymentPoint, bool isVisible)
		{
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00023AFC File Offset: 0x00021CFC
		private void SetDeploymentTargetContourState(DeploymentPoint deploymentPoint, bool isHighlighted)
		{
			DeploymentPoint.DeploymentPointState deploymentPointState = deploymentPoint.GetDeploymentPointState();
			if (deploymentPointState == DeploymentPoint.DeploymentPointState.SiegeLadder)
			{
				using (List<SiegeLadder>.Enumerator enumerator = deploymentPoint.GetAssociatedSiegeLadders().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SiegeLadder siegeLadder = enumerator.Current;
						if (isHighlighted)
						{
							siegeLadder.GameEntity.SetContourColor(new uint?(4289622555U), true);
						}
						else
						{
							siegeLadder.GameEntity.SetContourColor(null, true);
						}
					}
					return;
				}
			}
			if (deploymentPointState == DeploymentPoint.DeploymentPointState.Breach)
			{
				if (isHighlighted)
				{
					deploymentPoint.AssociatedWallSegment.GameEntity.SetContourColor(new uint?(4289622555U), true);
					return;
				}
				deploymentPoint.AssociatedWallSegment.GameEntity.SetContourColor(null, true);
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00023BC0 File Offset: 0x00021DC0
		private void SetLaddersUpState(DeploymentPoint deploymentPoint, bool isRaised)
		{
			foreach (SiegeLadder siegeLadder in deploymentPoint.GetAssociatedSiegeLadders())
			{
				siegeLadder.SetUpStateVisibility(isRaised);
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00023C14 File Offset: 0x00021E14
		private void SetLightState(DeploymentPoint deploymentPoint, bool isVisible)
		{
			GameEntity gameEntity;
			this._deploymentLights.TryGetValue(deploymentPoint, out gameEntity);
			if (gameEntity != null)
			{
				gameEntity.SetVisibilityExcludeParents(isVisible);
				return;
			}
			if (isVisible)
			{
				this.CreateLight(deploymentPoint);
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00023C4C File Offset: 0x00021E4C
		private void CreateLight(DeploymentPoint deploymentPoint)
		{
			MatrixFrame identity = MatrixFrame.Identity;
			identity.origin = deploymentPoint.DeploymentTargetPosition + new Vec3(0f, 0f, (deploymentPoint.GetDeploymentPointType() == DeploymentPoint.DeploymentPointType.TowerLadder) ? 10f : 3f, -1f);
			identity.rotation.RotateAboutSide(1.5707964f);
			identity.Scale(new Vec3(5f, 5f, 5f, -1f));
			GameEntity value = GameEntity.Instantiate(Mission.Current.Scene, "aserai_keep_interior_a_light_shaft_a", identity);
			this._deploymentLights.Add(deploymentPoint, value);
		}

		// Token: 0x040002B4 RID: 692
		private static int deploymentVisualizerSelector;

		// Token: 0x040002B5 RID: 693
		private List<DeploymentPoint> _deploymentPoints;

		// Token: 0x040002B6 RID: 694
		private bool _deploymentPointsVisible;

		// Token: 0x040002B7 RID: 695
		private Dictionary<DeploymentPoint, List<Vec3>> _deploymentArcs = new Dictionary<DeploymentPoint, List<Vec3>>();

		// Token: 0x040002B8 RID: 696
		private Dictionary<DeploymentPoint, ValueTuple<GameEntity, GameEntity>> _deploymentBanners = new Dictionary<DeploymentPoint, ValueTuple<GameEntity, GameEntity>>();

		// Token: 0x040002B9 RID: 697
		private Dictionary<DeploymentPoint, GameEntity> _deploymentLights = new Dictionary<DeploymentPoint, GameEntity>();

		// Token: 0x040002BA RID: 698
		private DeploymentMissionView _deploymentMissionView;

		// Token: 0x040002BB RID: 699
		private const uint EntityHighlightColor = 4289622555U;

		// Token: 0x020000B8 RID: 184
		public enum DeploymentVisualizationPreference
		{
			// Token: 0x040003A7 RID: 935
			ShowUndeployed = 1,
			// Token: 0x040003A8 RID: 936
			Line,
			// Token: 0x040003A9 RID: 937
			Arc = 4,
			// Token: 0x040003AA RID: 938
			Banner = 8,
			// Token: 0x040003AB RID: 939
			Path = 16,
			// Token: 0x040003AC RID: 940
			Ghost = 32,
			// Token: 0x040003AD RID: 941
			Contour = 64,
			// Token: 0x040003AE RID: 942
			LiftLadders = 128,
			// Token: 0x040003AF RID: 943
			Light = 256,
			// Token: 0x040003B0 RID: 944
			AllEnabled = 1023
		}
	}
}

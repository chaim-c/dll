using System;
using System.Collections.Generic;
using System.Diagnostics;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.MissionViews
{
	// Token: 0x0200004D RID: 77
	public class MissionFormationTargetSelectionHandler : MissionView
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600034B RID: 843 RVA: 0x0001CFC0 File Offset: 0x0001B1C0
		// (remove) Token: 0x0600034C RID: 844 RVA: 0x0001CFF8 File Offset: 0x0001B1F8
		public event Action<MBReadOnlyList<Formation>> OnFormationFocused;

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0001D02D File Offset: 0x0001B22D
		private Camera ActiveCamera
		{
			get
			{
				return base.MissionScreen.CustomCamera ?? base.MissionScreen.CombatCamera;
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0001D04C File Offset: 0x0001B24C
		public MissionFormationTargetSelectionHandler()
		{
			this._distanceCache = new List<ValueTuple<Formation, float>>();
			this._focusedFormationCache = new MBList<Formation>();
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0001D0B0 File Offset: 0x0001B2B0
		public override void OnPreDisplayMissionTick(float dt)
		{
			base.OnPreDisplayMissionTick(dt);
			this._distanceCache.Clear();
			this._focusedFormationCache.Clear();
			Mission mission = base.Mission;
			if (((mission != null) ? mission.Teams : null) != null)
			{
				Vec3 position = this.ActiveCamera.Position;
				this._centerOfScreen.x = Screen.RealScreenResolutionWidth / 2f;
				this._centerOfScreen.y = Screen.RealScreenResolutionHeight / 2f;
				for (int i = 0; i < base.Mission.Teams.Count; i++)
				{
					Team team = base.Mission.Teams[i];
					if (!team.IsPlayerAlly)
					{
						for (int j = 0; j < team.FormationsIncludingEmpty.Count; j++)
						{
							Formation formation = team.FormationsIncludingEmpty[j];
							if (formation.CountOfUnits > 0)
							{
								float formationDistanceToCenter = this.GetFormationDistanceToCenter(formation, position);
								this._distanceCache.Add(new ValueTuple<Formation, float>(formation, formationDistanceToCenter));
							}
						}
					}
				}
				if (this._distanceCache.Count == 0)
				{
					Action<MBReadOnlyList<Formation>> onFormationFocused = this.OnFormationFocused;
					if (onFormationFocused == null)
					{
						return;
					}
					onFormationFocused(null);
					return;
				}
				else
				{
					Formation formation2 = null;
					float num = this.MaxDistanceToCenterForFocus;
					for (int k = 0; k < this._distanceCache.Count; k++)
					{
						ValueTuple<Formation, float> valueTuple = this._distanceCache[k];
						if (valueTuple.Item2 == 0f)
						{
							this._focusedFormationCache.Add(valueTuple.Item1);
						}
						else if (valueTuple.Item2 < num)
						{
							num = valueTuple.Item2;
							formation2 = valueTuple.Item1;
						}
					}
					if (formation2 != null)
					{
						this._focusedFormationCache.Add(formation2);
					}
					Action<MBReadOnlyList<Formation>> onFormationFocused2 = this.OnFormationFocused;
					if (onFormationFocused2 == null)
					{
						return;
					}
					onFormationFocused2(this._focusedFormationCache);
				}
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0001D264 File Offset: 0x0001B464
		private float GetFormationDistanceToCenter(Formation formation, Vec3 cameraPosition)
		{
			WorldPosition medianPosition = formation.QuerySystem.MedianPosition;
			medianPosition.SetVec2(formation.QuerySystem.AveragePosition);
			float num = formation.QuerySystem.AveragePosition.Distance(cameraPosition.AsVec2);
			if (num >= 1000f)
			{
				return 2.1474836E+09f;
			}
			if (num <= 10f)
			{
				return 0f;
			}
			float a = 0f;
			float b = 0f;
			float num2 = 0f;
			MBWindowManager.WorldToScreenInsideUsableArea(this.ActiveCamera, medianPosition.GetGroundVec3() + new Vec3(0f, 0f, 3f, -1f), ref a, ref b, ref num2);
			if (num2 <= 0f)
			{
				return 2.1474836E+09f;
			}
			return new Vec2(a, b).Distance(this._centerOfScreen);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0001D337 File Offset: 0x0001B537
		public override void OnRemoveBehavior()
		{
			this._distanceCache.Clear();
			this._focusedFormationCache.Clear();
			this.OnFormationFocused = null;
			base.OnRemoveBehavior();
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0001D35C File Offset: 0x0001B55C
		[Conditional("DEBUG")]
		public void TickDebug()
		{
		}

		// Token: 0x04000249 RID: 585
		public const float MaxDistanceForFocusCheck = 1000f;

		// Token: 0x0400024A RID: 586
		public const float MinDistanceForFocusCheck = 10f;

		// Token: 0x0400024B RID: 587
		public readonly float MaxDistanceToCenterForFocus = 70f * (Screen.RealScreenResolutionHeight / 1080f);

		// Token: 0x0400024C RID: 588
		private readonly List<ValueTuple<Formation, float>> _distanceCache;

		// Token: 0x0400024D RID: 589
		private readonly MBList<Formation> _focusedFormationCache;

		// Token: 0x0400024E RID: 590
		private Vec2 _centerOfScreen = new Vec2(Screen.RealScreenResolutionWidth / 2f, Screen.RealScreenResolutionHeight / 2f);
	}
}

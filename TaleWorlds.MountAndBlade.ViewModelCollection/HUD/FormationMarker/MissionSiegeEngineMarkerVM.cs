using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD.FormationMarker
{
	// Token: 0x02000053 RID: 83
	public class MissionSiegeEngineMarkerVM : ViewModel
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0001AA03 File Offset: 0x00018C03
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x0001AA0B File Offset: 0x00018C0B
		public bool IsInitialized { get; private set; }

		// Token: 0x0600069E RID: 1694 RVA: 0x0001AA14 File Offset: 0x00018C14
		public MissionSiegeEngineMarkerVM(Mission mission, Camera missionCamera)
		{
			this._mission = mission;
			this._missionCamera = missionCamera;
			this._comparer = new MissionSiegeEngineMarkerVM.SiegeEngineMarkerDistanceComparer();
			this.Targets = new MBBindingList<MissionSiegeEngineMarkerTargetVM>();
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001AA6C File Offset: 0x00018C6C
		public void InitializeWith(List<SiegeWeapon> siegeEngines)
		{
			this._siegeEngines = siegeEngines;
			for (int i = 0; i < this._siegeEngines.Count; i++)
			{
				SiegeWeapon engine = this._siegeEngines[i];
				BattleSideEnum side = this._mission.PlayerTeam.Side;
				if (!this.Targets.Any((MissionSiegeEngineMarkerTargetVM t) => t.Engine == engine))
				{
					MissionSiegeEngineMarkerTargetVM missionSiegeEngineMarkerTargetVM = new MissionSiegeEngineMarkerTargetVM(engine, engine.Side != side);
					this.Targets.Add(missionSiegeEngineMarkerTargetVM);
					missionSiegeEngineMarkerTargetVM.IsEnabled = this.IsEnabled;
				}
			}
			this.IsInitialized = true;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001AB18 File Offset: 0x00018D18
		public void Tick(float dt)
		{
			if (this._siegeEngines != null)
			{
				if (this.IsEnabled)
				{
					this.RefreshSiegeEngineList();
					this.RefreshSiegeEnginePositions();
					this.RefreshSiegeEngineItemProperties();
					this.SortMarkersInList();
					this._fadeOutTimerStarted = false;
					this._fadeOutTimer = 0f;
					this._prevIsEnabled = this.IsEnabled;
				}
				else
				{
					if (this._prevIsEnabled)
					{
						this._fadeOutTimerStarted = true;
					}
					if (this._fadeOutTimerStarted)
					{
						this._fadeOutTimer += dt;
					}
					if (this._fadeOutTimer < 2f)
					{
						this.RefreshSiegeEnginePositions();
					}
					else
					{
						this._fadeOutTimerStarted = false;
					}
				}
				this._prevIsEnabled = this.IsEnabled;
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001ABC0 File Offset: 0x00018DC0
		private void RefreshSiegeEngineList()
		{
			bool isDefender = this._mission.PlayerTeam.IsDefender;
			for (int i = this._siegeEngines.Count - 1; i >= 0; i--)
			{
				SiegeWeapon engine = this._siegeEngines[i];
				if (engine.DestructionComponent.IsDestroyed)
				{
					this._siegeEngines.RemoveAt(i);
					MissionSiegeEngineMarkerTargetVM item = this.Targets.SingleOrDefault((MissionSiegeEngineMarkerTargetVM t) => t.Engine == engine);
					this.Targets.Remove(item);
				}
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001AC54 File Offset: 0x00018E54
		private void RefreshSiegeEnginePositions()
		{
			foreach (MissionSiegeEngineMarkerTargetVM missionSiegeEngineMarkerTargetVM in this.Targets)
			{
				float num = 0f;
				float num2 = 0f;
				float num3 = 0f;
				Vec3 globalPosition = missionSiegeEngineMarkerTargetVM.Engine.GameEntity.GlobalPosition;
				MBWindowManager.WorldToScreenInsideUsableArea(this._missionCamera, globalPosition + this._heightOffset, ref num, ref num2, ref num3);
				if (num3 < 0f || !MathF.IsValidValue(num) || !MathF.IsValidValue(num2))
				{
					num = -10000f;
					num2 = -10000f;
					num3 = 0f;
				}
				if (this._prevIsEnabled && this.IsEnabled)
				{
					missionSiegeEngineMarkerTargetVM.ScreenPosition = Vec2.Lerp(missionSiegeEngineMarkerTargetVM.ScreenPosition, new Vec2(num, num2), 0.9f);
				}
				else
				{
					missionSiegeEngineMarkerTargetVM.ScreenPosition = new Vec2(num, num2);
				}
				MissionSiegeEngineMarkerTargetVM missionSiegeEngineMarkerTargetVM2 = missionSiegeEngineMarkerTargetVM;
				Agent main = Agent.Main;
				missionSiegeEngineMarkerTargetVM2.Distance = ((main != null && main.IsActive()) ? Agent.Main.Position.Distance(globalPosition) : num3);
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001AD80 File Offset: 0x00018F80
		private void SortMarkersInList()
		{
			this.Targets.Sort(this._comparer);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001AD94 File Offset: 0x00018F94
		private void RefreshSiegeEngineItemProperties()
		{
			foreach (MissionSiegeEngineMarkerTargetVM missionSiegeEngineMarkerTargetVM in this.Targets)
			{
				missionSiegeEngineMarkerTargetVM.Refresh();
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001ADE0 File Offset: 0x00018FE0
		private void UpdateTargetStates(bool isEnabled)
		{
			foreach (MissionSiegeEngineMarkerTargetVM missionSiegeEngineMarkerTargetVM in this.Targets)
			{
				missionSiegeEngineMarkerTargetVM.IsEnabled = isEnabled;
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001AE2C File Offset: 0x0001902C
		public override void OnFinalize()
		{
			base.OnFinalize();
			List<SiegeWeapon> siegeEngines = this._siegeEngines;
			if (siegeEngines != null)
			{
				siegeEngines.Clear();
			}
			this._siegeEngines = null;
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001AE4C File Offset: 0x0001904C
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x0001AE54 File Offset: 0x00019054
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
					this.UpdateTargetStates(value);
				}
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001AE79 File Offset: 0x00019079
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x0001AE81 File Offset: 0x00019081
		[DataSourceProperty]
		public MBBindingList<MissionSiegeEngineMarkerTargetVM> Targets
		{
			get
			{
				return this._targets;
			}
			set
			{
				if (value != this._targets)
				{
					this._targets = value;
					base.OnPropertyChangedWithValue<MBBindingList<MissionSiegeEngineMarkerTargetVM>>(value, "Targets");
				}
			}
		}

		// Token: 0x04000324 RID: 804
		private Mission _mission;

		// Token: 0x04000325 RID: 805
		private Camera _missionCamera;

		// Token: 0x04000326 RID: 806
		private List<SiegeWeapon> _siegeEngines;

		// Token: 0x04000327 RID: 807
		private Vec3 _heightOffset = new Vec3(0f, 0f, 3f, -1f);

		// Token: 0x04000328 RID: 808
		private bool _prevIsEnabled;

		// Token: 0x04000329 RID: 809
		private MissionSiegeEngineMarkerVM.SiegeEngineMarkerDistanceComparer _comparer;

		// Token: 0x0400032A RID: 810
		private bool _fadeOutTimerStarted;

		// Token: 0x0400032B RID: 811
		private float _fadeOutTimer;

		// Token: 0x0400032D RID: 813
		private bool _isEnabled;

		// Token: 0x0400032E RID: 814
		private MBBindingList<MissionSiegeEngineMarkerTargetVM> _targets;

		// Token: 0x020000D7 RID: 215
		public class SiegeEngineMarkerDistanceComparer : IComparer<MissionSiegeEngineMarkerTargetVM>
		{
			// Token: 0x06000B95 RID: 2965 RVA: 0x00028EE0 File Offset: 0x000270E0
			public int Compare(MissionSiegeEngineMarkerTargetVM x, MissionSiegeEngineMarkerTargetVM y)
			{
				return y.Distance.CompareTo(x.Distance);
			}
		}
	}
}

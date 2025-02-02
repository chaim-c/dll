using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.MountAndBlade.ViewModelCollection.Order;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD.FormationMarker
{
	// Token: 0x02000052 RID: 82
	public class MissionFormationMarkerVM : ViewModel
	{
		// Token: 0x0600068D RID: 1677 RVA: 0x0001A3A8 File Offset: 0x000185A8
		public MissionFormationMarkerVM(Mission mission, Camera missionCamera)
		{
			this._mission = mission;
			this._missionCamera = missionCamera;
			this._comparer = new MissionFormationMarkerVM.FormationMarkerDistanceComparer();
			this.Targets = new MBBindingList<MissionFormationMarkerTargetVM>();
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001A400 File Offset: 0x00018600
		public void Tick(float dt)
		{
			if (this.IsEnabled)
			{
				this.RefreshFormationListInMission();
				this.RefreshFormationPositions();
				this.RefreshFormationItemProperties();
				this.SortMarkersInList();
				this.RefreshTargetProperties();
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
					this.RefreshFormationPositions();
				}
				else
				{
					this._fadeOutTimerStarted = false;
				}
			}
			this._prevIsEnabled = this.IsEnabled;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001A4A0 File Offset: 0x000186A0
		private void RefreshFormationListInMission()
		{
			IEnumerable<Formation> formationList = this._mission.Teams.SelectMany((Team t) => t.FormationsIncludingEmpty.WhereQ((Formation f) => f.CountOfUnits > 0));
			using (IEnumerator<Formation> enumerator = formationList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation formation = enumerator.Current;
					if (this.Targets.All((MissionFormationMarkerTargetVM t) => t.Formation != formation))
					{
						MissionFormationMarkerTargetVM missionFormationMarkerTargetVM = new MissionFormationMarkerTargetVM(formation);
						this.Targets.Add(missionFormationMarkerTargetVM);
						missionFormationMarkerTargetVM.IsEnabled = this.IsEnabled;
						missionFormationMarkerTargetVM.IsFormationTargetRelevant = this.IsFormationTargetRelevant;
					}
				}
			}
			if (formationList.CountQ<Formation>() < this.Targets.Count)
			{
				foreach (MissionFormationMarkerTargetVM item in this.Targets.WhereQ((MissionFormationMarkerTargetVM t) => !formationList.Contains(t.Formation)).ToList<MissionFormationMarkerTargetVM>())
				{
					this.Targets.Remove(item);
				}
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001A5F0 File Offset: 0x000187F0
		private void RefreshFormationPositions()
		{
			for (int i = 0; i < this.Targets.Count; i++)
			{
				MissionFormationMarkerTargetVM missionFormationMarkerTargetVM = this.Targets[i];
				float num = 0f;
				float num2 = 0f;
				float num3 = 0f;
				WorldPosition medianPosition = missionFormationMarkerTargetVM.Formation.QuerySystem.MedianPosition;
				medianPosition.SetVec2(missionFormationMarkerTargetVM.Formation.QuerySystem.AveragePosition);
				if (medianPosition.IsValid)
				{
					MBWindowManager.WorldToScreen(this._missionCamera, medianPosition.GetGroundVec3() + this._heightOffset, ref num, ref num2, ref num3);
					missionFormationMarkerTargetVM.IsInsideScreenBoundaries = (num <= Screen.RealScreenResolutionWidth && num2 <= Screen.RealScreenResolutionHeight && num + 200f >= 0f && num2 + 100f >= 0f);
					missionFormationMarkerTargetVM.WSign = ((num3 < 0f) ? -1 : 1);
				}
				if (!missionFormationMarkerTargetVM.IsTargetingAFormation && (!medianPosition.IsValid || num3 < 0f || !MathF.IsValidValue(num) || !MathF.IsValidValue(num2)))
				{
					num = -10000f;
					num2 = -10000f;
					num3 = 0f;
				}
				if (this._prevIsEnabled && this.IsEnabled)
				{
					missionFormationMarkerTargetVM.ScreenPosition = Vec2.Lerp(missionFormationMarkerTargetVM.ScreenPosition, new Vec2(num, num2), 0.9f);
				}
				else
				{
					missionFormationMarkerTargetVM.ScreenPosition = new Vec2(num, num2);
				}
				MissionFormationMarkerTargetVM missionFormationMarkerTargetVM2 = missionFormationMarkerTargetVM;
				Agent main = Agent.Main;
				missionFormationMarkerTargetVM2.Distance = ((main != null && main.IsActive()) ? Agent.Main.Position.Distance(medianPosition.GetGroundVec3()) : num3);
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001A78C File Offset: 0x0001898C
		private void RefreshTargetProperties()
		{
			List<Formation> list = new List<Formation>();
			Agent main = Agent.Main;
			MBReadOnlyList<Formation> mbreadOnlyList;
			if (main == null)
			{
				mbreadOnlyList = null;
			}
			else
			{
				OrderController playerOrderController = main.Team.PlayerOrderController;
				mbreadOnlyList = ((playerOrderController != null) ? playerOrderController.SelectedFormations : null);
			}
			MBReadOnlyList<Formation> mbreadOnlyList2 = mbreadOnlyList;
			if (mbreadOnlyList2 != null)
			{
				for (int i = 0; i < mbreadOnlyList2.Count; i++)
				{
					if (mbreadOnlyList2[i].TargetFormation != null && OrderUIHelper.CanOrderHaveTarget(OrderUIHelper.GetActiveMovementOrderOfFormation(mbreadOnlyList2[i])))
					{
						list.Add(mbreadOnlyList2[i].TargetFormation);
					}
				}
			}
			for (int j = 0; j < this.Targets.Count; j++)
			{
				MissionFormationMarkerTargetVM missionFormationMarkerTargetVM = this.Targets[j];
				if (missionFormationMarkerTargetVM.TeamType == 2)
				{
					bool isTargetingAFormation = list.Contains(missionFormationMarkerTargetVM.Formation);
					MissionFormationMarkerTargetVM missionFormationMarkerTargetVM2 = missionFormationMarkerTargetVM;
					MBReadOnlyList<Formation> focusedFormations = this._focusedFormations;
					missionFormationMarkerTargetVM2.SetTargetedState(focusedFormations != null && focusedFormations.Contains(missionFormationMarkerTargetVM.Formation), isTargetingAFormation);
				}
			}
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001A868 File Offset: 0x00018A68
		private void SortMarkersInList()
		{
			this.Targets.Sort(this._comparer);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001A87C File Offset: 0x00018A7C
		private void RefreshFormationItemProperties()
		{
			foreach (MissionFormationMarkerTargetVM missionFormationMarkerTargetVM in this.Targets)
			{
				missionFormationMarkerTargetVM.Refresh();
			}
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001A8C8 File Offset: 0x00018AC8
		private void UpdateTargetStates(bool isEnabled, bool isFormationTargetRelevant)
		{
			foreach (MissionFormationMarkerTargetVM missionFormationMarkerTargetVM in this.Targets)
			{
				missionFormationMarkerTargetVM.IsEnabled = isEnabled;
				missionFormationMarkerTargetVM.IsFormationTargetRelevant = isFormationTargetRelevant;
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0001A91C File Offset: 0x00018B1C
		public void SetFocusedFormations(MBReadOnlyList<Formation> focusedFormations)
		{
			this._focusedFormations = focusedFormations;
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0001A925 File Offset: 0x00018B25
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x0001A930 File Offset: 0x00018B30
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
					for (int i = 0; i < this.Targets.Count; i++)
					{
						this.Targets[i].IsEnabled = value;
					}
				}
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0001A981 File Offset: 0x00018B81
		// (set) Token: 0x06000699 RID: 1689 RVA: 0x0001A98C File Offset: 0x00018B8C
		[DataSourceProperty]
		public bool IsFormationTargetRelevant
		{
			get
			{
				return this._isFormationTargetRelevant;
			}
			set
			{
				if (value != this._isFormationTargetRelevant)
				{
					this._isFormationTargetRelevant = value;
					base.OnPropertyChangedWithValue(value, "IsFormationTargetRelevant");
					for (int i = 0; i < this.Targets.Count; i++)
					{
						this.Targets[i].IsFormationTargetRelevant = value;
					}
				}
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001A9DD File Offset: 0x00018BDD
		// (set) Token: 0x0600069B RID: 1691 RVA: 0x0001A9E5 File Offset: 0x00018BE5
		[DataSourceProperty]
		public MBBindingList<MissionFormationMarkerTargetVM> Targets
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
					base.OnPropertyChangedWithValue<MBBindingList<MissionFormationMarkerTargetVM>>(value, "Targets");
				}
			}
		}

		// Token: 0x04000319 RID: 793
		private readonly Mission _mission;

		// Token: 0x0400031A RID: 794
		private readonly Camera _missionCamera;

		// Token: 0x0400031B RID: 795
		private readonly MissionFormationMarkerVM.FormationMarkerDistanceComparer _comparer;

		// Token: 0x0400031C RID: 796
		private readonly Vec3 _heightOffset = new Vec3(0f, 0f, 3f, -1f);

		// Token: 0x0400031D RID: 797
		private bool _prevIsEnabled;

		// Token: 0x0400031E RID: 798
		private bool _fadeOutTimerStarted;

		// Token: 0x0400031F RID: 799
		private float _fadeOutTimer;

		// Token: 0x04000320 RID: 800
		private MBReadOnlyList<Formation> _focusedFormations;

		// Token: 0x04000321 RID: 801
		private bool _isEnabled;

		// Token: 0x04000322 RID: 802
		private bool _isFormationTargetRelevant;

		// Token: 0x04000323 RID: 803
		private MBBindingList<MissionFormationMarkerTargetVM> _targets;

		// Token: 0x020000D3 RID: 211
		public class FormationMarkerDistanceComparer : IComparer<MissionFormationMarkerTargetVM>
		{
			// Token: 0x06000B8B RID: 2955 RVA: 0x00028E30 File Offset: 0x00027030
			public int Compare(MissionFormationMarkerTargetVM x, MissionFormationMarkerTargetVM y)
			{
				return y.Distance.CompareTo(x.Distance);
			}
		}
	}
}

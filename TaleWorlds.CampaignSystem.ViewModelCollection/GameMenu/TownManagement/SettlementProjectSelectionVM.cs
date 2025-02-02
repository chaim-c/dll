using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Buildings;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TownManagement
{
	// Token: 0x02000095 RID: 149
	public class SettlementProjectSelectionVM : ViewModel
	{
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0003A1F6 File Offset: 0x000383F6
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x0003A1FE File Offset: 0x000383FE
		public List<Building> LocalDevelopmentList { get; private set; }

		// Token: 0x06000E7B RID: 3707 RVA: 0x0003A207 File Offset: 0x00038407
		public SettlementProjectSelectionVM(Settlement settlement, Action onAnyChangeInQueue)
		{
			this._settlement = settlement;
			this._town = settlement.Town;
			this._onAnyChangeInQueue = onAnyChangeInQueue;
			this.RefreshValues();
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0003A230 File Offset: 0x00038430
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ProjectsText = new TextObject("{=LpsoPtOo}Projects", null).ToString();
			this.DailyDefaultsText = GameTexts.FindText("str_town_management_daily_defaults", null).ToString();
			this.DailyDefaultsExplanationText = GameTexts.FindText("str_town_management_daily_defaults_explanation", null).ToString();
			this.QueueText = GameTexts.FindText("str_town_management_queue", null).ToString();
			this.Refresh();
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0003A2A4 File Offset: 0x000384A4
		public void Refresh()
		{
			this.AvailableProjects = new MBBindingList<SettlementBuildingProjectVM>();
			this.DailyDefaultList = new MBBindingList<SettlementDailyProjectVM>();
			this.LocalDevelopmentList = new List<Building>();
			this.CurrentDevelopmentQueue = new MBBindingList<SettlementBuildingProjectVM>();
			this.AvailableProjects.Clear();
			for (int i = 0; i < this._town.Buildings.Count; i++)
			{
				Building building = this._town.Buildings[i];
				if (building.BuildingType.BuildingLocation != BuildingLocation.Daily)
				{
					SettlementBuildingProjectVM settlementBuildingProjectVM = new SettlementBuildingProjectVM(new Action<SettlementProjectVM, bool>(this.OnCurrentProjectSelection), new Action<SettlementProjectVM>(this.OnCurrentProjectSet), new Action(this.OnResetCurrentProject), building, this._settlement);
					this.AvailableProjects.Add(settlementBuildingProjectVM);
					if (settlementBuildingProjectVM.Building == this._town.CurrentBuilding)
					{
						this.CurrentSelectedProject = settlementBuildingProjectVM;
					}
				}
				else
				{
					SettlementDailyProjectVM settlementDailyProjectVM = new SettlementDailyProjectVM(new Action<SettlementProjectVM, bool>(this.OnCurrentProjectSelection), new Action<SettlementProjectVM>(this.OnCurrentProjectSet), new Action(this.OnResetCurrentProject), building, this._settlement);
					this.DailyDefaultList.Add(settlementDailyProjectVM);
					if (settlementDailyProjectVM.Building == this._town.CurrentDefaultBuilding)
					{
						this.CurrentDailyDefault = settlementDailyProjectVM;
						this.CurrentDailyDefault.IsDefault = true;
						settlementDailyProjectVM.IsDefault = true;
					}
				}
			}
			foreach (Building item in this._town.BuildingsInProgress)
			{
				this.LocalDevelopmentList.Add(item);
			}
			this.RefreshDevelopmentsQueueIndex();
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0003A448 File Offset: 0x00038648
		private void OnCurrentProjectSet(SettlementProjectVM selectedItem)
		{
			if (selectedItem != this.CurrentSelectedProject)
			{
				this.CurrentSelectedProject = selectedItem;
				this.CurrentSelectedProject.RefreshProductionText();
			}
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0003A468 File Offset: 0x00038668
		private void OnCurrentProjectSelection(SettlementProjectVM selectedItem, bool isSetAsActiveDevelopment)
		{
			if (!selectedItem.IsDaily)
			{
				if (isSetAsActiveDevelopment)
				{
					this.LocalDevelopmentList.Clear();
					this.LocalDevelopmentList.Add(selectedItem.Building);
				}
				else if (this.LocalDevelopmentList.Exists((Building d) => d == selectedItem.Building))
				{
					this.LocalDevelopmentList.Remove(selectedItem.Building);
				}
				else
				{
					this.LocalDevelopmentList.Add(selectedItem.Building);
				}
			}
			else
			{
				this.CurrentDailyDefault.IsDefault = false;
				this.CurrentDailyDefault = (selectedItem as SettlementDailyProjectVM);
				(selectedItem as SettlementDailyProjectVM).IsDefault = true;
			}
			this.RefreshDevelopmentsQueueIndex();
			if (this.LocalDevelopmentList.Count == 0)
			{
				this.CurrentSelectedProject = this.CurrentDailyDefault;
			}
			else if (selectedItem != this.CurrentSelectedProject)
			{
				this.CurrentSelectedProject = selectedItem;
			}
			Action onAnyChangeInQueue = this._onAnyChangeInQueue;
			if (onAnyChangeInQueue == null)
			{
				return;
			}
			onAnyChangeInQueue();
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0003A578 File Offset: 0x00038778
		private void OnResetCurrentProject()
		{
			this.CurrentSelectedProject = ((this.LocalDevelopmentList.Count > 0) ? this.AvailableProjects.First((SettlementBuildingProjectVM p) => p.Building == this.LocalDevelopmentList[0]) : this.CurrentDailyDefault);
			this.CurrentSelectedProject.RefreshProductionText();
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0003A5B8 File Offset: 0x000387B8
		private void RefreshDevelopmentsQueueIndex()
		{
			this.CurrentSelectedProject = null;
			this.CurrentDevelopmentQueue = new MBBindingList<SettlementBuildingProjectVM>();
			using (IEnumerator<SettlementBuildingProjectVM> enumerator = this.AvailableProjects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SettlementBuildingProjectVM item = enumerator.Current;
					item.DevelopmentQueueIndex = -1;
					item.IsInQueue = this.LocalDevelopmentList.Any((Building d) => d.BuildingType == item.Building.BuildingType);
					item.IsCurrentActiveProject = false;
					if (item.IsInQueue)
					{
						int num = this.LocalDevelopmentList.IndexOf(item.Building);
						item.DevelopmentQueueIndex = num;
						if (num == 0)
						{
							this.CurrentSelectedProject = item;
							item.IsCurrentActiveProject = true;
						}
						this.CurrentDevelopmentQueue.Add(item);
					}
					Comparer<SettlementBuildingProjectVM> comparer = Comparer<SettlementBuildingProjectVM>.Create((SettlementBuildingProjectVM s1, SettlementBuildingProjectVM s2) => s1.DevelopmentQueueIndex.CompareTo(s2.DevelopmentQueueIndex));
					this.CurrentDevelopmentQueue.Sort(comparer);
					item.RefreshProductionText();
				}
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x0003A6F4 File Offset: 0x000388F4
		// (set) Token: 0x06000E83 RID: 3715 RVA: 0x0003A6FC File Offset: 0x000388FC
		[DataSourceProperty]
		public string ProjectsText
		{
			get
			{
				return this._projectsText;
			}
			set
			{
				if (value != this._projectsText)
				{
					this._projectsText = value;
					base.OnPropertyChangedWithValue<string>(value, "ProjectsText");
				}
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x0003A71F File Offset: 0x0003891F
		// (set) Token: 0x06000E85 RID: 3717 RVA: 0x0003A727 File Offset: 0x00038927
		[DataSourceProperty]
		public string QueueText
		{
			get
			{
				return this._queueText;
			}
			set
			{
				if (value != this._queueText)
				{
					this._queueText = value;
					base.OnPropertyChangedWithValue<string>(value, "QueueText");
				}
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x0003A74A File Offset: 0x0003894A
		// (set) Token: 0x06000E87 RID: 3719 RVA: 0x0003A752 File Offset: 0x00038952
		[DataSourceProperty]
		public string DailyDefaultsText
		{
			get
			{
				return this._dailyDefaultsText;
			}
			set
			{
				if (value != this._dailyDefaultsText)
				{
					this._dailyDefaultsText = value;
					base.OnPropertyChangedWithValue<string>(value, "DailyDefaultsText");
				}
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x0003A775 File Offset: 0x00038975
		// (set) Token: 0x06000E89 RID: 3721 RVA: 0x0003A77D File Offset: 0x0003897D
		[DataSourceProperty]
		public string DailyDefaultsExplanationText
		{
			get
			{
				return this._dailyDefaultsExplanationText;
			}
			set
			{
				if (value != this._dailyDefaultsExplanationText)
				{
					this._dailyDefaultsExplanationText = value;
					base.OnPropertyChangedWithValue<string>(value, "DailyDefaultsExplanationText");
				}
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x0003A7A0 File Offset: 0x000389A0
		// (set) Token: 0x06000E8B RID: 3723 RVA: 0x0003A7A8 File Offset: 0x000389A8
		[DataSourceProperty]
		public SettlementProjectVM CurrentSelectedProject
		{
			get
			{
				return this._currentSelectedProject;
			}
			set
			{
				if (value != this._currentSelectedProject)
				{
					this._currentSelectedProject = value;
					base.OnPropertyChangedWithValue<SettlementProjectVM>(value, "CurrentSelectedProject");
				}
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x0003A7C6 File Offset: 0x000389C6
		// (set) Token: 0x06000E8D RID: 3725 RVA: 0x0003A7CE File Offset: 0x000389CE
		[DataSourceProperty]
		public SettlementDailyProjectVM CurrentDailyDefault
		{
			get
			{
				return this._currentDailyDefault;
			}
			set
			{
				if (value != this._currentDailyDefault)
				{
					this._currentDailyDefault = value;
					base.OnPropertyChangedWithValue<SettlementDailyProjectVM>(value, "CurrentDailyDefault");
				}
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x0003A7EC File Offset: 0x000389EC
		// (set) Token: 0x06000E8F RID: 3727 RVA: 0x0003A7F4 File Offset: 0x000389F4
		[DataSourceProperty]
		public MBBindingList<SettlementBuildingProjectVM> AvailableProjects
		{
			get
			{
				return this._availableProjects;
			}
			set
			{
				if (value != this._availableProjects)
				{
					this._availableProjects = value;
					base.OnPropertyChangedWithValue<MBBindingList<SettlementBuildingProjectVM>>(value, "AvailableProjects");
				}
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x0003A812 File Offset: 0x00038A12
		// (set) Token: 0x06000E91 RID: 3729 RVA: 0x0003A81A File Offset: 0x00038A1A
		[DataSourceProperty]
		public MBBindingList<SettlementBuildingProjectVM> CurrentDevelopmentQueue
		{
			get
			{
				return this._currentDevelopmentQueue;
			}
			set
			{
				if (value != this._currentDevelopmentQueue)
				{
					this._currentDevelopmentQueue = value;
					base.OnPropertyChangedWithValue<MBBindingList<SettlementBuildingProjectVM>>(value, "CurrentDevelopmentQueue");
				}
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0003A838 File Offset: 0x00038A38
		// (set) Token: 0x06000E93 RID: 3731 RVA: 0x0003A840 File Offset: 0x00038A40
		[DataSourceProperty]
		public MBBindingList<SettlementDailyProjectVM> DailyDefaultList
		{
			get
			{
				return this._dailyDefaultList;
			}
			set
			{
				if (value != this._dailyDefaultList)
				{
					this._dailyDefaultList = value;
					base.OnPropertyChangedWithValue<MBBindingList<SettlementDailyProjectVM>>(value, "DailyDefaultList");
				}
			}
		}

		// Token: 0x040006B5 RID: 1717
		private readonly Town _town;

		// Token: 0x040006B6 RID: 1718
		private readonly Settlement _settlement;

		// Token: 0x040006B7 RID: 1719
		private readonly Action _onAnyChangeInQueue;

		// Token: 0x040006B9 RID: 1721
		private SettlementDailyProjectVM _currentDailyDefault;

		// Token: 0x040006BA RID: 1722
		private SettlementProjectVM _currentSelectedProject;

		// Token: 0x040006BB RID: 1723
		private MBBindingList<SettlementDailyProjectVM> _dailyDefaultList;

		// Token: 0x040006BC RID: 1724
		private MBBindingList<SettlementBuildingProjectVM> _currentDevelopmentQueue;

		// Token: 0x040006BD RID: 1725
		private MBBindingList<SettlementBuildingProjectVM> _availableProjects;

		// Token: 0x040006BE RID: 1726
		private string _projectsText;

		// Token: 0x040006BF RID: 1727
		private string _queueText;

		// Token: 0x040006C0 RID: 1728
		private string _dailyDefaultsText;

		// Token: 0x040006C1 RID: 1729
		private string _dailyDefaultsExplanationText;
	}
}

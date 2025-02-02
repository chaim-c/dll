using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD
{
	// Token: 0x02000040 RID: 64
	public class MissionAgentLockVisualizerVM : ViewModel
	{
		// Token: 0x0600058C RID: 1420 RVA: 0x00017535 File Offset: 0x00015735
		public MissionAgentLockVisualizerVM()
		{
			this._allTrackedAgentsSet = new Dictionary<Agent, MissionAgentLockItemVM>();
			this.AllTrackedAgents = new MBBindingList<MissionAgentLockItemVM>();
			this.IsEnabled = true;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001755C File Offset: 0x0001575C
		public void OnActiveLockAgentChange(Agent oldAgent, Agent newAgent)
		{
			if (oldAgent != null && this._allTrackedAgentsSet.ContainsKey(oldAgent))
			{
				this.AllTrackedAgents.Remove(this._allTrackedAgentsSet[oldAgent]);
				this._allTrackedAgentsSet.Remove(oldAgent);
			}
			if (newAgent != null)
			{
				if (this._allTrackedAgentsSet.ContainsKey(newAgent))
				{
					this._allTrackedAgentsSet[newAgent].SetLockState(MissionAgentLockItemVM.LockStates.Active);
					return;
				}
				MissionAgentLockItemVM missionAgentLockItemVM = new MissionAgentLockItemVM(newAgent, MissionAgentLockItemVM.LockStates.Active);
				this._allTrackedAgentsSet.Add(newAgent, missionAgentLockItemVM);
				this.AllTrackedAgents.Add(missionAgentLockItemVM);
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000175E4 File Offset: 0x000157E4
		public void OnPossibleLockAgentChange(Agent oldPossibleAgent, Agent newPossibleAgent)
		{
			if (oldPossibleAgent != null && this._allTrackedAgentsSet.ContainsKey(oldPossibleAgent))
			{
				this.AllTrackedAgents.Remove(this._allTrackedAgentsSet[oldPossibleAgent]);
				this._allTrackedAgentsSet.Remove(oldPossibleAgent);
			}
			if (newPossibleAgent != null)
			{
				if (this._allTrackedAgentsSet.ContainsKey(newPossibleAgent))
				{
					this._allTrackedAgentsSet[newPossibleAgent].SetLockState(MissionAgentLockItemVM.LockStates.Possible);
					return;
				}
				MissionAgentLockItemVM missionAgentLockItemVM = new MissionAgentLockItemVM(newPossibleAgent, MissionAgentLockItemVM.LockStates.Possible);
				this._allTrackedAgentsSet.Add(newPossibleAgent, missionAgentLockItemVM);
				this.AllTrackedAgents.Add(missionAgentLockItemVM);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0001766C File Offset: 0x0001586C
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x00017674 File Offset: 0x00015874
		[DataSourceProperty]
		public MBBindingList<MissionAgentLockItemVM> AllTrackedAgents
		{
			get
			{
				return this._allTrackedAgents;
			}
			set
			{
				if (value != this._allTrackedAgents)
				{
					this._allTrackedAgents = value;
					base.OnPropertyChangedWithValue<MBBindingList<MissionAgentLockItemVM>>(value, "AllTrackedAgents");
				}
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00017692 File Offset: 0x00015892
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x0001769A File Offset: 0x0001589A
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
					if (!value)
					{
						this.AllTrackedAgents.Clear();
						this._allTrackedAgentsSet.Clear();
					}
				}
			}
		}

		// Token: 0x040002A8 RID: 680
		private readonly Dictionary<Agent, MissionAgentLockItemVM> _allTrackedAgentsSet;

		// Token: 0x040002A9 RID: 681
		private MBBindingList<MissionAgentLockItemVM> _allTrackedAgents;

		// Token: 0x040002AA RID: 682
		private bool _isEnabled;
	}
}

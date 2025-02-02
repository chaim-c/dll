using System;
using System.Collections.ObjectModel;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD
{
	// Token: 0x02000042 RID: 66
	public class MissionAgentTakenDamageVM : ViewModel
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x00017762 File Offset: 0x00015962
		public MissionAgentTakenDamageVM(Camera missionCamera)
		{
			this._missionCamera = missionCamera;
			this.TakenDamageList = new MBBindingList<MissionAgentTakenDamageItemVM>();
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001777C File Offset: 0x0001597C
		public void SetIsEnabled(bool isEnabled)
		{
			this._isEnabled = isEnabled;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00017788 File Offset: 0x00015988
		internal void Tick(float dt)
		{
			if (this._isEnabled)
			{
				for (int i = 0; i < this.TakenDamageList.Count; i++)
				{
					this.TakenDamageList[i].Update();
				}
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000177C4 File Offset: 0x000159C4
		internal void OnMainAgentHit(int damage, float distance)
		{
			if (this._isEnabled && damage > 0)
			{
				Collection<MissionAgentTakenDamageItemVM> takenDamageList = this.TakenDamageList;
				Camera missionCamera = this._missionCamera;
				Agent main = Agent.Main;
				takenDamageList.Add(new MissionAgentTakenDamageItemVM(missionCamera, (main != null) ? main.Position : default(Vec3), damage, false, new Action<MissionAgentTakenDamageItemVM>(this.OnRemoveDamageItem)));
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001781A File Offset: 0x00015A1A
		private void OnRemoveDamageItem(MissionAgentTakenDamageItemVM item)
		{
			this.TakenDamageList.Remove(item);
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00017829 File Offset: 0x00015A29
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x00017831 File Offset: 0x00015A31
		[DataSourceProperty]
		public MBBindingList<MissionAgentTakenDamageItemVM> TakenDamageList
		{
			get
			{
				return this._takenDamageList;
			}
			set
			{
				if (value != this._takenDamageList)
				{
					this._takenDamageList = value;
					base.OnPropertyChangedWithValue<MBBindingList<MissionAgentTakenDamageItemVM>>(value, "TakenDamageList");
				}
			}
		}

		// Token: 0x040002AE RID: 686
		private Camera _missionCamera;

		// Token: 0x040002AF RID: 687
		private bool _isEnabled;

		// Token: 0x040002B0 RID: 688
		private MBBindingList<MissionAgentTakenDamageItemVM> _takenDamageList;
	}
}

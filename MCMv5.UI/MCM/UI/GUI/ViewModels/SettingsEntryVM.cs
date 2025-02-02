using System;
using System.Runtime.CompilerServices;
using MCM.Abstractions;
using TaleWorlds.Library;

namespace MCM.UI.GUI.ViewModels
{
	// Token: 0x0200001E RID: 30
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SettingsEntryVM : ViewModel
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005B0A File Offset: 0x00003D0A
		public string Id
		{
			get
			{
				UnavailableSetting unavailableSetting = this.UnavailableSetting;
				string result;
				if ((result = ((unavailableSetting != null) ? unavailableSetting.Id : null)) == null)
				{
					SettingsVM settingsVM = this.SettingsVM;
					result = (((settingsVM != null) ? settingsVM.SettingsDefinition.SettingsId : null) ?? "ERROR");
				}
				return result;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00005B42 File Offset: 0x00003D42
		[DataSourceProperty]
		public string DisplayName
		{
			get
			{
				UnavailableSetting unavailableSetting = this.UnavailableSetting;
				string result;
				if ((result = ((unavailableSetting != null) ? unavailableSetting.DisplayName : null)) == null)
				{
					SettingsVM settingsVM = this.SettingsVM;
					result = (((settingsVM != null) ? settingsVM.DisplayName : null) ?? "ERROR");
				}
				return result;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00005B78 File Offset: 0x00003D78
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00005BB0 File Offset: 0x00003DB0
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				bool? isSelected = this._isSelected;
				bool result;
				if (isSelected == null)
				{
					SettingsVM settingsVM = this.SettingsVM;
					result = (settingsVM != null && settingsVM.IsSelected);
				}
				else
				{
					result = isSelected.GetValueOrDefault();
				}
				return result;
			}
			set
			{
				bool flag = this.SettingsVM != null;
				if (flag)
				{
					this.SettingsVM.IsSelected = value;
				}
				else
				{
					this._isSelected = new bool?(value);
				}
				base.OnPropertyChanged("IsSelected");
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00005BF9 File Offset: 0x00003DF9
		[Nullable(2)]
		public SettingsVM SettingsVM { [NullableContext(2)] get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00005C01 File Offset: 0x00003E01
		[Nullable(2)]
		public UnavailableSetting UnavailableSetting { [NullableContext(2)] get; }

		// Token: 0x060000EE RID: 238 RVA: 0x00005C09 File Offset: 0x00003E09
		public SettingsEntryVM(UnavailableSetting unavailableSetting, Action<SettingsEntryVM> command)
		{
			this.UnavailableSetting = unavailableSetting;
			this._isSelected = new bool?(false);
			this._executeSelect = command;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005C2D File Offset: 0x00003E2D
		public SettingsEntryVM(SettingsVM settingsVM, Action<SettingsEntryVM> command)
		{
			this.SettingsVM = settingsVM;
			this._executeSelect = command;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005C45 File Offset: 0x00003E45
		public void ExecuteSelect()
		{
			this._executeSelect(this);
		}

		// Token: 0x04000040 RID: 64
		private readonly Action<SettingsEntryVM> _executeSelect;

		// Token: 0x04000041 RID: 65
		private bool? _isSelected;
	}
}

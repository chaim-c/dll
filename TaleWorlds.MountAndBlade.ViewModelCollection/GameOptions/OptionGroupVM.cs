using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine.Options;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions
{
	// Token: 0x02000062 RID: 98
	public class OptionGroupVM : ViewModel
	{
		// Token: 0x060007A2 RID: 1954 RVA: 0x0001CCF0 File Offset: 0x0001AEF0
		public OptionGroupVM(TextObject groupName, OptionsVM optionsBase, IEnumerable<IOptionData> optionsList)
		{
			this._groupName = groupName;
			this.Options = new MBBindingList<GenericOptionDataVM>();
			foreach (IOptionData option in optionsList)
			{
				this.Options.Add(optionsBase.GetOptionItem(option));
			}
			this.RefreshValues();
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001CD64 File Offset: 0x0001AF64
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this._groupName.ToString();
			this.Options.ApplyActionOnAllItems(delegate(GenericOptionDataVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001CDB4 File Offset: 0x0001AFB4
		internal List<IOptionData> GetManagedOptions()
		{
			List<IOptionData> list = new List<IOptionData>();
			foreach (GenericOptionDataVM genericOptionDataVM in this.Options)
			{
				if (!genericOptionDataVM.IsNative)
				{
					list.Add(genericOptionDataVM.GetOptionData());
				}
			}
			return list;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001CE18 File Offset: 0x0001B018
		internal bool IsChanged()
		{
			return this.Options.Any((GenericOptionDataVM o) => o.IsChanged());
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001CE44 File Offset: 0x0001B044
		internal void Cancel()
		{
			this.Options.ApplyActionOnAllItems(delegate(GenericOptionDataVM o)
			{
				o.Cancel();
			});
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001CE70 File Offset: 0x0001B070
		internal void InitializeDependentConfigs(Action<IOptionData, float> updateDependentConfigs)
		{
			this.Options.ApplyActionOnAllItems(delegate(GenericOptionDataVM o)
			{
				updateDependentConfigs(o.GetOptionData(), o.GetOptionData().GetValue(false));
			});
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0001CEA1 File Offset: 0x0001B0A1
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x0001CEA9 File Offset: 0x0001B0A9
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0001CECC File Offset: 0x0001B0CC
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x0001CED4 File Offset: 0x0001B0D4
		[DataSourceProperty]
		public MBBindingList<GenericOptionDataVM> Options
		{
			get
			{
				return this._options;
			}
			set
			{
				if (value != this._options)
				{
					this._options = value;
					base.OnPropertyChangedWithValue<MBBindingList<GenericOptionDataVM>>(value, "Options");
				}
			}
		}

		// Token: 0x04000393 RID: 915
		private readonly TextObject _groupName;

		// Token: 0x04000394 RID: 916
		private const string ControllerIdentificationModifier = "_controller";

		// Token: 0x04000395 RID: 917
		private string _name;

		// Token: 0x04000396 RID: 918
		private MBBindingList<GenericOptionDataVM> _options;
	}
}

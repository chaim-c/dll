using System;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.Refinement
{
	// Token: 0x020000F6 RID: 246
	public class RefinementActionItemVM : ViewModel
	{
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x00056684 File Offset: 0x00054884
		public Crafting.RefiningFormula RefineFormula { get; }

		// Token: 0x06001742 RID: 5954 RVA: 0x0005668C File Offset: 0x0005488C
		public RefinementActionItemVM(Crafting.RefiningFormula refineFormula, Action<RefinementActionItemVM> onSelect)
		{
			this._onSelect = onSelect;
			this.RefineFormula = refineFormula;
			this.InputMaterials = new MBBindingList<CraftingResourceItemVM>();
			this.OutputMaterials = new MBBindingList<CraftingResourceItemVM>();
			SmithingModel smithingModel = Campaign.Current.Models.SmithingModel;
			if (this.RefineFormula.Input1Count > 0)
			{
				this.InputMaterials.Add(new CraftingResourceItemVM(this.RefineFormula.Input1, this.RefineFormula.Input1Count, 0));
			}
			if (this.RefineFormula.Input2Count > 0)
			{
				this.InputMaterials.Add(new CraftingResourceItemVM(this.RefineFormula.Input2, this.RefineFormula.Input2Count, 0));
			}
			if (this.RefineFormula.OutputCount > 0)
			{
				this.OutputMaterials.Add(new CraftingResourceItemVM(this.RefineFormula.Output, this.RefineFormula.OutputCount, 0));
			}
			if (this.RefineFormula.Output2Count > 0)
			{
				this.OutputMaterials.Add(new CraftingResourceItemVM(this.RefineFormula.Output2, this.RefineFormula.Output2Count, 0));
			}
			this.RefreshDynamicProperties();
			this.RefreshValues();
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x000567B4 File Offset: 0x000549B4
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.InputMaterials.ApplyActionOnAllItems(delegate(CraftingResourceItemVM m)
			{
				m.RefreshValues();
			});
			this.OutputMaterials.ApplyActionOnAllItems(delegate(CraftingResourceItemVM m)
			{
				m.RefreshValues();
			});
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x0005681B File Offset: 0x00054A1B
		public void RefreshDynamicProperties()
		{
			this.IsEnabled = this.CheckInputsAvailable();
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0005682C File Offset: 0x00054A2C
		private bool CheckInputsAvailable()
		{
			ItemRoster itemRoster = MobileParty.MainParty.ItemRoster;
			foreach (CraftingResourceItemVM craftingResourceItemVM in this.InputMaterials)
			{
				if (itemRoster.GetItemNumber(craftingResourceItemVM.ResourceItem) < craftingResourceItemVM.ResourceAmount)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00056898 File Offset: 0x00054A98
		public void ExecuteSelectAction()
		{
			this._onSelect(this);
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x000568A6 File Offset: 0x00054AA6
		// (set) Token: 0x06001748 RID: 5960 RVA: 0x000568AE File Offset: 0x00054AAE
		[DataSourceProperty]
		public MBBindingList<CraftingResourceItemVM> InputMaterials
		{
			get
			{
				return this._inputMaterials;
			}
			set
			{
				if (value != this._inputMaterials)
				{
					this._inputMaterials = value;
					base.OnPropertyChangedWithValue<MBBindingList<CraftingResourceItemVM>>(value, "InputMaterials");
				}
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x000568CC File Offset: 0x00054ACC
		// (set) Token: 0x0600174A RID: 5962 RVA: 0x000568D4 File Offset: 0x00054AD4
		[DataSourceProperty]
		public MBBindingList<CraftingResourceItemVM> OutputMaterials
		{
			get
			{
				return this._outputMaterials;
			}
			set
			{
				if (value != this._outputMaterials)
				{
					this._outputMaterials = value;
					base.OnPropertyChangedWithValue<MBBindingList<CraftingResourceItemVM>>(value, "OutputMaterials");
				}
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x000568F2 File Offset: 0x00054AF2
		// (set) Token: 0x0600174C RID: 5964 RVA: 0x000568FA File Offset: 0x00054AFA
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x00056918 File Offset: 0x00054B18
		// (set) Token: 0x0600174E RID: 5966 RVA: 0x00056920 File Offset: 0x00054B20
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
				}
			}
		}

		// Token: 0x04000ADB RID: 2779
		private readonly Action<RefinementActionItemVM> _onSelect;

		// Token: 0x04000ADD RID: 2781
		private MBBindingList<CraftingResourceItemVM> _inputMaterials;

		// Token: 0x04000ADE RID: 2782
		private MBBindingList<CraftingResourceItemVM> _outputMaterials;

		// Token: 0x04000ADF RID: 2783
		private bool _isSelected;

		// Token: 0x04000AE0 RID: 2784
		private bool _isEnabled;
	}
}

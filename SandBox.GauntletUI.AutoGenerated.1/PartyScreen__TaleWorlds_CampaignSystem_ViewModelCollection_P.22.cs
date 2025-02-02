﻿using System;
using System.ComponentModel;
using TaleWorlds.CampaignSystem.ViewModelCollection.Party.PartyTroopManagerPopUp;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x02000157 RID: 343
	public class PartyScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Party_PartyVM_Dependency_21_ItemTemplate : PartyScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Party_PartyVM_Dependency_27_PartyTroopUpgradeItem__InheritedPrefab
	{
		// Token: 0x0600684E RID: 26702 RVA: 0x0033E3A7 File Offset: 0x0033C5A7
		public PartyScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Party_PartyVM_Dependency_21_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x0600684F RID: 26703 RVA: 0x0033E3B0 File Offset: 0x0033C5B0
		public override void CreateWidgets()
		{
			base.CreateWidgets();
			this._widget = this;
		}

		// Token: 0x06006850 RID: 26704 RVA: 0x0033E3BF File Offset: 0x0033C5BF
		public override void SetIds()
		{
			base.SetIds();
		}

		// Token: 0x06006851 RID: 26705 RVA: 0x0033E3C7 File Offset: 0x0033C5C7
		public override void SetAttributes()
		{
			base.SetAttributes();
		}

		// Token: 0x06006852 RID: 26706 RVA: 0x0033E3D0 File Offset: 0x0033C5D0
		public override void DestroyDataSource()
		{
			base.DestroyDataSource();
			if (this._datasource_Root != null)
			{
				this._datasource_Root.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root;
				this._datasource_Root = null;
			}
		}

		// Token: 0x06006853 RID: 26707 RVA: 0x0033E4C4 File Offset: 0x0033C6C4
		public override void SetDataSource(PartyTroopManagerItemVM dataSource)
		{
			base.SetDataSource(dataSource);
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06006854 RID: 26708 RVA: 0x0033E4D4 File Offset: 0x0033C6D4
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06006855 RID: 26709 RVA: 0x0033E4E2 File Offset: 0x0033C6E2
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06006856 RID: 26710 RVA: 0x0033E4F0 File Offset: 0x0033C6F0
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06006857 RID: 26711 RVA: 0x0033E4FE File Offset: 0x0033C6FE
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06006858 RID: 26712 RVA: 0x0033E50C File Offset: 0x0033C70C
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06006859 RID: 26713 RVA: 0x0033E51A File Offset: 0x0033C71A
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600685A RID: 26714 RVA: 0x0033E528 File Offset: 0x0033C728
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600685B RID: 26715 RVA: 0x0033E536 File Offset: 0x0033C736
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600685C RID: 26716 RVA: 0x0033E544 File Offset: 0x0033C744
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600685D RID: 26717 RVA: 0x0033E552 File Offset: 0x0033C752
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
		}

		// Token: 0x0600685E RID: 26718 RVA: 0x0033E554 File Offset: 0x0033C754
		private void RefreshDataSource_datasource_Root(PartyTroopManagerItemVM newDataSource)
		{
			if (this._datasource_Root != null)
			{
				this._datasource_Root.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root;
				this._datasource_Root = null;
			}
			this._datasource_Root = newDataSource;
			if (this._datasource_Root != null)
			{
				this._datasource_Root.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root;
			}
		}

		// Token: 0x0400150E RID: 5390
		private PartyScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Party_PartyVM_Dependency_27_PartyTroopUpgradeItem__InheritedPrefab _widget;

		// Token: 0x0400150F RID: 5391
		private PartyTroopManagerItemVM _datasource_Root;
	}
}

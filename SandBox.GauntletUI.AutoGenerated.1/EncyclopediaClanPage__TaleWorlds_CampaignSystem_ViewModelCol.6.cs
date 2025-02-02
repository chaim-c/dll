﻿using System;
using System.ComponentModel;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x020000BE RID: 190
	public class EncyclopediaClanPage__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaClanPageVM_Dependency_5_ItemTemplate : EncyclopediaClanPage__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaClanPageVM_Dependency_15_EncyclopediaSubPageHistoryElement__InheritedPrefab
	{
		// Token: 0x060038E5 RID: 14565 RVA: 0x001C7D07 File Offset: 0x001C5F07
		public EncyclopediaClanPage__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaClanPageVM_Dependency_5_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x001C7D10 File Offset: 0x001C5F10
		public override void CreateWidgets()
		{
			base.CreateWidgets();
			this._widget = this;
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x001C7D1F File Offset: 0x001C5F1F
		public override void SetIds()
		{
			base.SetIds();
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x001C7D27 File Offset: 0x001C5F27
		public override void SetAttributes()
		{
			base.SetAttributes();
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x001C7D30 File Offset: 0x001C5F30
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

		// Token: 0x060038EA RID: 14570 RVA: 0x001C7E24 File Offset: 0x001C6024
		public override void SetDataSource(EncyclopediaHistoryEventVM dataSource)
		{
			base.SetDataSource(dataSource);
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x001C7E34 File Offset: 0x001C6034
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x001C7E42 File Offset: 0x001C6042
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x001C7E50 File Offset: 0x001C6050
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x001C7E5E File Offset: 0x001C605E
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x001C7E6C File Offset: 0x001C606C
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x001C7E7A File Offset: 0x001C607A
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x001C7E88 File Offset: 0x001C6088
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x001C7E96 File Offset: 0x001C6096
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x001C7EA4 File Offset: 0x001C60A4
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x001C7EB2 File Offset: 0x001C60B2
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x001C7EB4 File Offset: 0x001C60B4
		private void RefreshDataSource_datasource_Root(EncyclopediaHistoryEventVM newDataSource)
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

		// Token: 0x04000BBF RID: 3007
		private EncyclopediaClanPage__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaClanPageVM_Dependency_15_EncyclopediaSubPageHistoryElement__InheritedPrefab _widget;

		// Token: 0x04000BC0 RID: 3008
		private EncyclopediaHistoryEventVM _datasource_Root;
	}
}

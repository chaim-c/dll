﻿using System;
using System.ComponentModel;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.AutoGenerated0
{
	// Token: 0x02000028 RID: 40
	public class EncyclopediaFactionPage__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaFactionPageVM_Dependency_3_ItemTemplate : EncyclopediaFactionPage__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaFactionPageVM_Dependency_11_EncyclopediaClanSubPageElement__InheritedPrefab
	{
		// Token: 0x06000B8D RID: 2957 RVA: 0x00057430 File Offset: 0x00055630
		public EncyclopediaFactionPage__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaFactionPageVM_Dependency_3_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00057439 File Offset: 0x00055639
		public override void CreateWidgets()
		{
			base.CreateWidgets();
			this._widget = this;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00057448 File Offset: 0x00055648
		public override void SetIds()
		{
			base.SetIds();
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00057450 File Offset: 0x00055650
		public override void SetAttributes()
		{
			base.SetAttributes();
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00057458 File Offset: 0x00055658
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

		// Token: 0x06000B92 RID: 2962 RVA: 0x0005754C File Offset: 0x0005574C
		public override void SetDataSource(EncyclopediaFactionVM dataSource)
		{
			base.SetDataSource(dataSource);
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0005755C File Offset: 0x0005575C
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0005756A File Offset: 0x0005576A
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00057578 File Offset: 0x00055778
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00057586 File Offset: 0x00055786
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00057594 File Offset: 0x00055794
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x000575A2 File Offset: 0x000557A2
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x000575B0 File Offset: 0x000557B0
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x000575BE File Offset: 0x000557BE
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x000575CC File Offset: 0x000557CC
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x000575DA File Offset: 0x000557DA
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x000575DC File Offset: 0x000557DC
		private void RefreshDataSource_datasource_Root(EncyclopediaFactionVM newDataSource)
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

		// Token: 0x0400024F RID: 591
		private EncyclopediaFactionPage__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaFactionPageVM_Dependency_11_EncyclopediaClanSubPageElement__InheritedPrefab _widget;

		// Token: 0x04000250 RID: 592
		private EncyclopediaFactionVM _datasource_Root;
	}
}

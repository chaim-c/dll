﻿using System;
using System.ComponentModel;
using TaleWorlds.CampaignSystem.ViewModelCollection.Barter;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x0200000B RID: 11
	public class BarterScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Barter_BarterVM_Dependency_5_ItemTemplate : BarterScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Barter_BarterVM_Dependency_27_BarterItemTuple__InheritedPrefab
	{
		// Token: 0x06000244 RID: 580 RVA: 0x000180B7 File Offset: 0x000162B7
		public BarterScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Barter_BarterVM_Dependency_5_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000180C0 File Offset: 0x000162C0
		private VisualDefinition CreateVisualDefinitionLeftMenu()
		{
			VisualDefinition visualDefinition = new VisualDefinition("LeftMenu", 0.2f, 0f, false);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionXOffset = 0f
			});
			return visualDefinition;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00018100 File Offset: 0x00016300
		private VisualDefinition CreateVisualDefinitionRightMenu()
		{
			VisualDefinition visualDefinition = new VisualDefinition("RightMenu", 0.2f, 0f, false);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionXOffset = 0f
			});
			return visualDefinition;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00018140 File Offset: 0x00016340
		private VisualDefinition CreateVisualDefinitionTopMenu()
		{
			VisualDefinition visualDefinition = new VisualDefinition("TopMenu", 0.2f, 0f, false);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionYOffset = 0f
			});
			return visualDefinition;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00018180 File Offset: 0x00016380
		private VisualDefinition CreateVisualDefinitionBottomMenu()
		{
			VisualDefinition visualDefinition = new VisualDefinition("BottomMenu", 0.2f, 0f, false);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionYOffset = 19f
			});
			return visualDefinition;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000181BF File Offset: 0x000163BF
		public override void CreateWidgets()
		{
			base.CreateWidgets();
			this._widget = this;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000181CE File Offset: 0x000163CE
		public override void SetIds()
		{
			base.SetIds();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000181D6 File Offset: 0x000163D6
		public override void SetAttributes()
		{
			base.SetAttributes();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000181E0 File Offset: 0x000163E0
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

		// Token: 0x0600024D RID: 589 RVA: 0x000182D4 File Offset: 0x000164D4
		public override void SetDataSource(BarterItemVM dataSource)
		{
			base.SetDataSource(dataSource);
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000182E4 File Offset: 0x000164E4
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000182F2 File Offset: 0x000164F2
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00018300 File Offset: 0x00016500
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0001830E File Offset: 0x0001650E
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0001831C File Offset: 0x0001651C
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0001832A File Offset: 0x0001652A
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00018338 File Offset: 0x00016538
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00018346 File Offset: 0x00016546
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00018354 File Offset: 0x00016554
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00018362 File Offset: 0x00016562
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00018364 File Offset: 0x00016564
		private void RefreshDataSource_datasource_Root(BarterItemVM newDataSource)
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

		// Token: 0x040000AD RID: 173
		private BarterScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Barter_BarterVM_Dependency_27_BarterItemTuple__InheritedPrefab _widget;

		// Token: 0x040000AE RID: 174
		private BarterItemVM _datasource_Root;
	}
}

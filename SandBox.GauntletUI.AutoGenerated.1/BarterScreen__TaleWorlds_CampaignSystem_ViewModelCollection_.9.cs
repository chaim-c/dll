﻿using System;
using System.ComponentModel;
using TaleWorlds.CampaignSystem.ViewModelCollection.Barter;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x0200000E RID: 14
	public class BarterScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Barter_BarterVM_Dependency_8_ItemTemplate : BarterScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Barter_BarterVM_Dependency_29_BarterOfferItemTuple__InheritedPrefab
	{
		// Token: 0x06000283 RID: 643 RVA: 0x00018E2B File Offset: 0x0001702B
		public BarterScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Barter_BarterVM_Dependency_8_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00018E34 File Offset: 0x00017034
		private VisualDefinition CreateVisualDefinitionLeftMenu()
		{
			VisualDefinition visualDefinition = new VisualDefinition("LeftMenu", 0.2f, 0f, false);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionXOffset = 0f
			});
			return visualDefinition;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00018E74 File Offset: 0x00017074
		private VisualDefinition CreateVisualDefinitionRightMenu()
		{
			VisualDefinition visualDefinition = new VisualDefinition("RightMenu", 0.2f, 0f, false);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionXOffset = 0f
			});
			return visualDefinition;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00018EB4 File Offset: 0x000170B4
		private VisualDefinition CreateVisualDefinitionTopMenu()
		{
			VisualDefinition visualDefinition = new VisualDefinition("TopMenu", 0.2f, 0f, false);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionYOffset = 0f
			});
			return visualDefinition;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00018EF4 File Offset: 0x000170F4
		private VisualDefinition CreateVisualDefinitionBottomMenu()
		{
			VisualDefinition visualDefinition = new VisualDefinition("BottomMenu", 0.2f, 0f, false);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionYOffset = 19f
			});
			return visualDefinition;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00018F33 File Offset: 0x00017133
		public override void CreateWidgets()
		{
			base.CreateWidgets();
			this._widget = this;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00018F42 File Offset: 0x00017142
		public override void SetIds()
		{
			base.SetIds();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00018F4A File Offset: 0x0001714A
		public override void SetAttributes()
		{
			base.SetAttributes();
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00018F54 File Offset: 0x00017154
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

		// Token: 0x0600028C RID: 652 RVA: 0x00019048 File Offset: 0x00017248
		public override void SetDataSource(BarterItemVM dataSource)
		{
			base.SetDataSource(dataSource);
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00019058 File Offset: 0x00017258
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00019066 File Offset: 0x00017266
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00019074 File Offset: 0x00017274
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00019082 File Offset: 0x00017282
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00019090 File Offset: 0x00017290
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0001909E File Offset: 0x0001729E
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000190AC File Offset: 0x000172AC
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000190BA File Offset: 0x000172BA
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000190C8 File Offset: 0x000172C8
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000190D6 File Offset: 0x000172D6
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000190D8 File Offset: 0x000172D8
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

		// Token: 0x040000B3 RID: 179
		private BarterScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Barter_BarterVM_Dependency_29_BarterOfferItemTuple__InheritedPrefab _widget;

		// Token: 0x040000B4 RID: 180
		private BarterItemVM _datasource_Root;
	}
}

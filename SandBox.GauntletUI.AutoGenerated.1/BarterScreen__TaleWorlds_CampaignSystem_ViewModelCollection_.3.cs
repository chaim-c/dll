﻿using System;
using System.ComponentModel;
using TaleWorlds.CampaignSystem.ViewModelCollection.Barter;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x02000008 RID: 8
	public class BarterScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Barter_BarterVM_Dependency_2_ItemTemplate : BarterScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Barter_BarterVM_Dependency_27_BarterItemTuple__InheritedPrefab
	{
		// Token: 0x06000205 RID: 517 RVA: 0x00017343 File Offset: 0x00015543
		public BarterScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Barter_BarterVM_Dependency_2_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0001734C File Offset: 0x0001554C
		private VisualDefinition CreateVisualDefinitionLeftMenu()
		{
			VisualDefinition visualDefinition = new VisualDefinition("LeftMenu", 0.2f, 0f, false);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionXOffset = 0f
			});
			return visualDefinition;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0001738C File Offset: 0x0001558C
		private VisualDefinition CreateVisualDefinitionRightMenu()
		{
			VisualDefinition visualDefinition = new VisualDefinition("RightMenu", 0.2f, 0f, false);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionXOffset = 0f
			});
			return visualDefinition;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000173CC File Offset: 0x000155CC
		private VisualDefinition CreateVisualDefinitionTopMenu()
		{
			VisualDefinition visualDefinition = new VisualDefinition("TopMenu", 0.2f, 0f, false);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionYOffset = 0f
			});
			return visualDefinition;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0001740C File Offset: 0x0001560C
		private VisualDefinition CreateVisualDefinitionBottomMenu()
		{
			VisualDefinition visualDefinition = new VisualDefinition("BottomMenu", 0.2f, 0f, false);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionYOffset = 19f
			});
			return visualDefinition;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0001744B File Offset: 0x0001564B
		public override void CreateWidgets()
		{
			base.CreateWidgets();
			this._widget = this;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0001745A File Offset: 0x0001565A
		public override void SetIds()
		{
			base.SetIds();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00017462 File Offset: 0x00015662
		public override void SetAttributes()
		{
			base.SetAttributes();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0001746C File Offset: 0x0001566C
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

		// Token: 0x0600020E RID: 526 RVA: 0x00017560 File Offset: 0x00015760
		public override void SetDataSource(BarterItemVM dataSource)
		{
			base.SetDataSource(dataSource);
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00017570 File Offset: 0x00015770
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0001757E File Offset: 0x0001577E
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0001758C File Offset: 0x0001578C
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0001759A File Offset: 0x0001579A
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000175A8 File Offset: 0x000157A8
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000175B6 File Offset: 0x000157B6
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000175C4 File Offset: 0x000157C4
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000175D2 File Offset: 0x000157D2
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000175E0 File Offset: 0x000157E0
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000175EE File Offset: 0x000157EE
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000175F0 File Offset: 0x000157F0
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

		// Token: 0x040000A7 RID: 167
		private BarterScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Barter_BarterVM_Dependency_27_BarterItemTuple__InheritedPrefab _widget;

		// Token: 0x040000A8 RID: 168
		private BarterItemVM _datasource_Root;
	}
}

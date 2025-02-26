﻿using System;
using System.ComponentModel;
using SandBox.ViewModelCollection.SaveLoad;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x02000179 RID: 377
	public class SaveLoadScreen__SandBox_ViewModelCollection_SaveLoad_SaveLoadVM_Dependency_1_ItemTemplate : SaveLoadScreen__SandBox_ViewModelCollection_SaveLoad_SaveLoadVM_Dependency_4_SavedGameGroup__InheritedPrefab
	{
		// Token: 0x060073C2 RID: 29634 RVA: 0x0039AF22 File Offset: 0x00399122
		public SaveLoadScreen__SandBox_ViewModelCollection_SaveLoad_SaveLoadVM_Dependency_1_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x060073C3 RID: 29635 RVA: 0x0039AF2C File Offset: 0x0039912C
		private VisualDefinition CreateVisualDefinitionLeftPanel()
		{
			VisualDefinition visualDefinition = new VisualDefinition("LeftPanel", 0.45f, 0f, true);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionXOffset = -6f
			});
			return visualDefinition;
		}

		// Token: 0x060073C4 RID: 29636 RVA: 0x0039AF6C File Offset: 0x0039916C
		private VisualDefinition CreateVisualDefinitionRightPanel()
		{
			VisualDefinition visualDefinition = new VisualDefinition("RightPanel", 0.45f, 0f, true);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionXOffset = 0f
			});
			return visualDefinition;
		}

		// Token: 0x060073C5 RID: 29637 RVA: 0x0039AFAC File Offset: 0x003991AC
		private VisualDefinition CreateVisualDefinitionBottomPanel()
		{
			VisualDefinition visualDefinition = new VisualDefinition("BottomPanel", 0.45f, 0f, true);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionYOffset = 0f
			});
			return visualDefinition;
		}

		// Token: 0x060073C6 RID: 29638 RVA: 0x0039AFEC File Offset: 0x003991EC
		private VisualDefinition CreateVisualDefinitionTopPanel()
		{
			VisualDefinition visualDefinition = new VisualDefinition("TopPanel", 0.45f, 0f, true);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionYOffset = 0f
			});
			return visualDefinition;
		}

		// Token: 0x060073C7 RID: 29639 RVA: 0x0039B02B File Offset: 0x0039922B
		public override void CreateWidgets()
		{
			base.CreateWidgets();
			this._widget = this;
		}

		// Token: 0x060073C8 RID: 29640 RVA: 0x0039B03A File Offset: 0x0039923A
		public override void SetIds()
		{
			base.SetIds();
		}

		// Token: 0x060073C9 RID: 29641 RVA: 0x0039B042 File Offset: 0x00399242
		public override void SetAttributes()
		{
			base.SetAttributes();
			base.DoNotUseCustomScaleAndChildren = true;
		}

		// Token: 0x060073CA RID: 29642 RVA: 0x0039B054 File Offset: 0x00399254
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

		// Token: 0x060073CB RID: 29643 RVA: 0x0039B148 File Offset: 0x00399348
		public override void SetDataSource(SavedGameGroupVM dataSource)
		{
			base.SetDataSource(dataSource);
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x060073CC RID: 29644 RVA: 0x0039B158 File Offset: 0x00399358
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060073CD RID: 29645 RVA: 0x0039B166 File Offset: 0x00399366
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060073CE RID: 29646 RVA: 0x0039B174 File Offset: 0x00399374
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060073CF RID: 29647 RVA: 0x0039B182 File Offset: 0x00399382
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060073D0 RID: 29648 RVA: 0x0039B190 File Offset: 0x00399390
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060073D1 RID: 29649 RVA: 0x0039B19E File Offset: 0x0039939E
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060073D2 RID: 29650 RVA: 0x0039B1AC File Offset: 0x003993AC
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060073D3 RID: 29651 RVA: 0x0039B1BA File Offset: 0x003993BA
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060073D4 RID: 29652 RVA: 0x0039B1C8 File Offset: 0x003993C8
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060073D5 RID: 29653 RVA: 0x0039B1D6 File Offset: 0x003993D6
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
		}

		// Token: 0x060073D6 RID: 29654 RVA: 0x0039B1D8 File Offset: 0x003993D8
		private void RefreshDataSource_datasource_Root(SavedGameGroupVM newDataSource)
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

		// Token: 0x04001733 RID: 5939
		private SaveLoadScreen__SandBox_ViewModelCollection_SaveLoad_SaveLoadVM_Dependency_4_SavedGameGroup__InheritedPrefab _widget;

		// Token: 0x04001734 RID: 5940
		private SavedGameGroupVM _datasource_Root;
	}
}

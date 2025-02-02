﻿using System;
using System.ComponentModel;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD;

namespace TaleWorlds.MountAndBlade.GauntletUI.AutoGenerated0
{
	// Token: 0x0200000B RID: 11
	public class MainAgentCheerBarkController__TaleWorlds_MountAndBlade_ViewModelCollection_HUD_MissionMainAgentCheerBarkControllerVM_Dependency_5_ItemTemplate : MainAgentCheerBarkController__TaleWorlds_MountAndBlade_ViewModelCollection_HUD_MissionMainAgentCheerBarkControllerVM_Dependency_3_MainAgentCheerBarkNodeCircle__InheritedPrefab
	{
		// Token: 0x06000197 RID: 407 RVA: 0x0000C3A3 File Offset: 0x0000A5A3
		public MainAgentCheerBarkController__TaleWorlds_MountAndBlade_ViewModelCollection_HUD_MissionMainAgentCheerBarkControllerVM_Dependency_5_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000C3AC File Offset: 0x0000A5AC
		private VisualDefinition CreateVisualDefinitionCircleBackground()
		{
			VisualDefinition visualDefinition = new VisualDefinition("CircleBackground", 0.15f, 0f, false);
			visualDefinition.AddVisualState(new VisualState("DisabledSelected")
			{
				SuggestedHeight = 84f,
				SuggestedWidth = 85f
			});
			visualDefinition.AddVisualState(new VisualState("Selected")
			{
				SuggestedHeight = 84f,
				SuggestedWidth = 85f
			});
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				SuggestedHeight = 74f,
				SuggestedWidth = 75f
			});
			visualDefinition.AddVisualState(new VisualState("Pressed")
			{
				SuggestedHeight = 74f,
				SuggestedWidth = 75f
			});
			visualDefinition.AddVisualState(new VisualState("Hovered")
			{
				SuggestedHeight = 74f,
				SuggestedWidth = 75f
			});
			visualDefinition.AddVisualState(new VisualState("Disabled")
			{
				SuggestedHeight = 74f,
				SuggestedWidth = 75f
			});
			return visualDefinition;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000C4C8 File Offset: 0x0000A6C8
		private VisualDefinition CreateVisualDefinitionCircleGlow()
		{
			VisualDefinition visualDefinition = new VisualDefinition("CircleGlow", 0.15f, 0f, false);
			visualDefinition.AddVisualState(new VisualState("DisabledSelected")
			{
				SuggestedHeight = 125f,
				SuggestedWidth = 127f
			});
			visualDefinition.AddVisualState(new VisualState("Selected")
			{
				SuggestedHeight = 125f,
				SuggestedWidth = 127f
			});
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				SuggestedHeight = 115f,
				SuggestedWidth = 117f
			});
			visualDefinition.AddVisualState(new VisualState("Pressed")
			{
				SuggestedHeight = 115f,
				SuggestedWidth = 117f
			});
			visualDefinition.AddVisualState(new VisualState("Hovered")
			{
				SuggestedHeight = 115f,
				SuggestedWidth = 117f
			});
			visualDefinition.AddVisualState(new VisualState("Disabled")
			{
				SuggestedHeight = 115f,
				SuggestedWidth = 117f
			});
			return visualDefinition;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000C5E2 File Offset: 0x0000A7E2
		public override void CreateWidgets()
		{
			base.CreateWidgets();
			this._widget = this;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000C5F1 File Offset: 0x0000A7F1
		public override void SetIds()
		{
			base.SetIds();
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000C5F9 File Offset: 0x0000A7F9
		public override void SetAttributes()
		{
			base.SetAttributes();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000C604 File Offset: 0x0000A804
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

		// Token: 0x0600019E RID: 414 RVA: 0x0000C6F8 File Offset: 0x0000A8F8
		public override void SetDataSource(CheerBarkNodeItemVM dataSource)
		{
			base.SetDataSource(dataSource);
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000C708 File Offset: 0x0000A908
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000C716 File Offset: 0x0000A916
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000C724 File Offset: 0x0000A924
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000C732 File Offset: 0x0000A932
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000C740 File Offset: 0x0000A940
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000C74E File Offset: 0x0000A94E
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000C75C File Offset: 0x0000A95C
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000C76A File Offset: 0x0000A96A
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000C778 File Offset: 0x0000A978
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000C786 File Offset: 0x0000A986
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000C788 File Offset: 0x0000A988
		private void RefreshDataSource_datasource_Root(CheerBarkNodeItemVM newDataSource)
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

		// Token: 0x04000043 RID: 67
		private MainAgentCheerBarkController__TaleWorlds_MountAndBlade_ViewModelCollection_HUD_MissionMainAgentCheerBarkControllerVM_Dependency_3_MainAgentCheerBarkNodeCircle__InheritedPrefab _widget;

		// Token: 0x04000044 RID: 68
		private CheerBarkNodeItemVM _datasource_Root;
	}
}

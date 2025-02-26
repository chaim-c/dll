﻿using System;
using System.ComponentModel;
using System.Numerics;
using SandBox.ViewModelCollection.SaveLoad;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x0200017A RID: 378
	public class SaveLoadScreen__SandBox_ViewModelCollection_SaveLoad_SaveLoadVM_Dependency_2_ItemTemplate : Widget
	{
		// Token: 0x060073D7 RID: 29655 RVA: 0x0039B3A7 File Offset: 0x003995A7
		public SaveLoadScreen__SandBox_ViewModelCollection_SaveLoad_SaveLoadVM_Dependency_2_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x060073D8 RID: 29656 RVA: 0x0039B3B0 File Offset: 0x003995B0
		private VisualDefinition CreateVisualDefinitionLeftPanel()
		{
			VisualDefinition visualDefinition = new VisualDefinition("LeftPanel", 0.45f, 0f, true);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionXOffset = -6f
			});
			return visualDefinition;
		}

		// Token: 0x060073D9 RID: 29657 RVA: 0x0039B3F0 File Offset: 0x003995F0
		private VisualDefinition CreateVisualDefinitionRightPanel()
		{
			VisualDefinition visualDefinition = new VisualDefinition("RightPanel", 0.45f, 0f, true);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionXOffset = 0f
			});
			return visualDefinition;
		}

		// Token: 0x060073DA RID: 29658 RVA: 0x0039B430 File Offset: 0x00399630
		private VisualDefinition CreateVisualDefinitionBottomPanel()
		{
			VisualDefinition visualDefinition = new VisualDefinition("BottomPanel", 0.45f, 0f, true);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionYOffset = 0f
			});
			return visualDefinition;
		}

		// Token: 0x060073DB RID: 29659 RVA: 0x0039B470 File Offset: 0x00399670
		private VisualDefinition CreateVisualDefinitionTopPanel()
		{
			VisualDefinition visualDefinition = new VisualDefinition("TopPanel", 0.45f, 0f, true);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionYOffset = 0f
			});
			return visualDefinition;
		}

		// Token: 0x060073DC RID: 29660 RVA: 0x0039B4B0 File Offset: 0x003996B0
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new ListPanel(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_0_0 = new SelectedStateBrushWidget(base.Context);
			this._widget_0.AddChild(this._widget_0_0);
			this._widget_0_1 = new TextWidget(base.Context);
			this._widget_0.AddChild(this._widget_0_1);
			this._widget_1 = new HintWidget(base.Context);
			this._widget.AddChild(this._widget_1);
		}

		// Token: 0x060073DD RID: 29661 RVA: 0x0039B54C File Offset: 0x0039974C
		public void SetIds()
		{
		}

		// Token: 0x060073DE RID: 29662 RVA: 0x0039B550 File Offset: 0x00399750
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.Fixed;
			base.HeightSizePolicy = SizePolicy.Fixed;
			base.SuggestedWidth = 100f;
			base.SuggestedHeight = 55f;
			base.VerticalAlignment = VerticalAlignment.Bottom;
			base.MarginBottom = 13f;
			base.MarginLeft = 5f;
			base.MarginRight = 5f;
			base.UpdateChildrenStates = true;
			base.DoNotPassEventsToChildren = true;
			this._widget_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.StackLayout.LayoutMethod = LayoutMethod.VerticalBottomToTop;
			this._widget_0_0.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0_0.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_0_0.SuggestedWidth = 40f;
			this._widget_0_0.SuggestedHeight = 40f;
			this._widget_0_0.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_0_0.VerticalAlignment = VerticalAlignment.Top;
			this._widget_0_0.MarginTop = 4f;
			this._widget_0_0.Brush = base.Context.GetBrush("SaveLoad.Property.Icon");
			this._widget_0_1.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0_1.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_1.Brush = base.Context.GetBrush("Stage.Selection.Description.Text");
			this._widget_1.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_1.HeightSizePolicy = SizePolicy.CoverChildren;
		}

		// Token: 0x060073DF RID: 29663 RVA: 0x0039B6A8 File Offset: 0x003998A8
		public void DestroyDataSource()
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
				this._widget_0_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0_0;
				this._widget_0_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0_1;
				if (this._datasource_Root_Hint != null)
				{
					this._datasource_Root_Hint.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Hint;
					this._widget_1.EventFire -= this.EventListenerOf_widget_1;
					this._datasource_Root_Hint = null;
				}
				this._datasource_Root = null;
			}
		}

		// Token: 0x060073E0 RID: 29664 RVA: 0x0039BA2C File Offset: 0x00399C2C
		public void SetDataSource(SavedGamePropertyVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x060073E1 RID: 29665 RVA: 0x0039BA35 File Offset: 0x00399C35
		private void EventListenerOf_widget_1(Widget widget, string commandName, object[] args)
		{
			if (commandName == "HoverBegin")
			{
				this._datasource_Root_Hint.ExecuteBeginHint();
			}
			if (commandName == "HoverEnd")
			{
				this._datasource_Root_Hint.ExecuteEndHint();
			}
		}

		// Token: 0x060073E2 RID: 29666 RVA: 0x0039BA67 File Offset: 0x00399C67
		private void PropertyChangedListenerOf_widget_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0(propertyName);
		}

		// Token: 0x060073E3 RID: 29667 RVA: 0x0039BA70 File Offset: 0x00399C70
		private void boolPropertyChangedListenerOf_widget_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0(propertyName);
		}

		// Token: 0x060073E4 RID: 29668 RVA: 0x0039BA79 File Offset: 0x00399C79
		private void floatPropertyChangedListenerOf_widget_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0(propertyName);
		}

		// Token: 0x060073E5 RID: 29669 RVA: 0x0039BA82 File Offset: 0x00399C82
		private void Vec2PropertyChangedListenerOf_widget_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0(propertyName);
		}

		// Token: 0x060073E6 RID: 29670 RVA: 0x0039BA8B File Offset: 0x00399C8B
		private void Vector2PropertyChangedListenerOf_widget_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0(propertyName);
		}

		// Token: 0x060073E7 RID: 29671 RVA: 0x0039BA94 File Offset: 0x00399C94
		private void doublePropertyChangedListenerOf_widget_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0(propertyName);
		}

		// Token: 0x060073E8 RID: 29672 RVA: 0x0039BA9D File Offset: 0x00399C9D
		private void intPropertyChangedListenerOf_widget_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0(propertyName);
		}

		// Token: 0x060073E9 RID: 29673 RVA: 0x0039BAA6 File Offset: 0x00399CA6
		private void uintPropertyChangedListenerOf_widget_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0(propertyName);
		}

		// Token: 0x060073EA RID: 29674 RVA: 0x0039BAAF File Offset: 0x00399CAF
		private void ColorPropertyChangedListenerOf_widget_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0(propertyName);
		}

		// Token: 0x060073EB RID: 29675 RVA: 0x0039BAB8 File Offset: 0x00399CB8
		private void HandleWidgetPropertyChangeOf_widget_0_0(string propertyName)
		{
			if (propertyName == "SelectedState")
			{
				this._datasource_Root.PropertyType = this._widget_0_0.SelectedState;
				return;
			}
		}

		// Token: 0x060073EC RID: 29676 RVA: 0x0039BADE File Offset: 0x00399CDE
		private void PropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060073ED RID: 29677 RVA: 0x0039BAE7 File Offset: 0x00399CE7
		private void boolPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060073EE RID: 29678 RVA: 0x0039BAF0 File Offset: 0x00399CF0
		private void floatPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060073EF RID: 29679 RVA: 0x0039BAF9 File Offset: 0x00399CF9
		private void Vec2PropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060073F0 RID: 29680 RVA: 0x0039BB02 File Offset: 0x00399D02
		private void Vector2PropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060073F1 RID: 29681 RVA: 0x0039BB0B File Offset: 0x00399D0B
		private void doublePropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060073F2 RID: 29682 RVA: 0x0039BB14 File Offset: 0x00399D14
		private void intPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060073F3 RID: 29683 RVA: 0x0039BB1D File Offset: 0x00399D1D
		private void uintPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060073F4 RID: 29684 RVA: 0x0039BB26 File Offset: 0x00399D26
		private void ColorPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060073F5 RID: 29685 RVA: 0x0039BB2F File Offset: 0x00399D2F
		private void HandleWidgetPropertyChangeOf_widget_0_1(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.Value = this._widget_0_1.Text;
				return;
			}
		}

		// Token: 0x060073F6 RID: 29686 RVA: 0x0039BB55 File Offset: 0x00399D55
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060073F7 RID: 29687 RVA: 0x0039BB63 File Offset: 0x00399D63
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060073F8 RID: 29688 RVA: 0x0039BB71 File Offset: 0x00399D71
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060073F9 RID: 29689 RVA: 0x0039BB7F File Offset: 0x00399D7F
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060073FA RID: 29690 RVA: 0x0039BB8D File Offset: 0x00399D8D
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060073FB RID: 29691 RVA: 0x0039BB9B File Offset: 0x00399D9B
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060073FC RID: 29692 RVA: 0x0039BBA9 File Offset: 0x00399DA9
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060073FD RID: 29693 RVA: 0x0039BBB7 File Offset: 0x00399DB7
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060073FE RID: 29694 RVA: 0x0039BBC5 File Offset: 0x00399DC5
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060073FF RID: 29695 RVA: 0x0039BBD4 File Offset: 0x00399DD4
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "Hint")
			{
				this.RefreshDataSource_datasource_Root_Hint(this._datasource_Root.Hint);
				return;
			}
			if (propertyName == "PropertyType")
			{
				this._widget_0_0.SelectedState = this._datasource_Root.PropertyType;
				return;
			}
			if (propertyName == "Value")
			{
				this._widget_0_1.Text = this._datasource_Root.Value;
				return;
			}
		}

		// Token: 0x06007400 RID: 29696 RVA: 0x0039BC48 File Offset: 0x00399E48
		private void ViewModelPropertyChangedListenerOf_datasource_Root_Hint(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06007401 RID: 29697 RVA: 0x0039BC56 File Offset: 0x00399E56
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06007402 RID: 29698 RVA: 0x0039BC64 File Offset: 0x00399E64
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06007403 RID: 29699 RVA: 0x0039BC72 File Offset: 0x00399E72
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06007404 RID: 29700 RVA: 0x0039BC80 File Offset: 0x00399E80
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06007405 RID: 29701 RVA: 0x0039BC8E File Offset: 0x00399E8E
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06007406 RID: 29702 RVA: 0x0039BC9C File Offset: 0x00399E9C
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06007407 RID: 29703 RVA: 0x0039BCAA File Offset: 0x00399EAA
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06007408 RID: 29704 RVA: 0x0039BCB8 File Offset: 0x00399EB8
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06007409 RID: 29705 RVA: 0x0039BCC6 File Offset: 0x00399EC6
		private void HandleViewModelPropertyChangeOf_datasource_Root_Hint(string propertyName)
		{
		}

		// Token: 0x0600740A RID: 29706 RVA: 0x0039BCC8 File Offset: 0x00399EC8
		private void RefreshDataSource_datasource_Root(SavedGamePropertyVM newDataSource)
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
				this._widget_0_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0_0;
				this._widget_0_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0_1;
				if (this._datasource_Root_Hint != null)
				{
					this._datasource_Root_Hint.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Hint;
					this._widget_1.EventFire -= this.EventListenerOf_widget_1;
					this._datasource_Root_Hint = null;
				}
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
				this._widget_0_0.SelectedState = this._datasource_Root.PropertyType;
				this._widget_0_0.PropertyChanged += this.PropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.intPropertyChanged += this.intPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_0_0;
				this._widget_0_1.Text = this._datasource_Root.Value;
				this._widget_0_1.PropertyChanged += this.PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.intPropertyChanged += this.intPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_0_1;
				this._datasource_Root_Hint = this._datasource_Root.Hint;
				if (this._datasource_Root_Hint != null)
				{
					this._datasource_Root_Hint.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Hint;
					this._widget_1.EventFire += this.EventListenerOf_widget_1;
				}
			}
		}

		// Token: 0x0600740B RID: 29707 RVA: 0x0039C3FC File Offset: 0x0039A5FC
		private void RefreshDataSource_datasource_Root_Hint(HintViewModel newDataSource)
		{
			if (this._datasource_Root_Hint != null)
			{
				this._datasource_Root_Hint.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Hint;
				this._widget_1.EventFire -= this.EventListenerOf_widget_1;
				this._datasource_Root_Hint = null;
			}
			this._datasource_Root_Hint = newDataSource;
			this._datasource_Root_Hint = this._datasource_Root.Hint;
			if (this._datasource_Root_Hint != null)
			{
				this._datasource_Root_Hint.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Hint;
				this._widget_1.EventFire += this.EventListenerOf_widget_1;
			}
		}

		// Token: 0x04001735 RID: 5941
		private Widget _widget;

		// Token: 0x04001736 RID: 5942
		private ListPanel _widget_0;

		// Token: 0x04001737 RID: 5943
		private SelectedStateBrushWidget _widget_0_0;

		// Token: 0x04001738 RID: 5944
		private TextWidget _widget_0_1;

		// Token: 0x04001739 RID: 5945
		private HintWidget _widget_1;

		// Token: 0x0400173A RID: 5946
		private SavedGamePropertyVM _datasource_Root;

		// Token: 0x0400173B RID: 5947
		private HintViewModel _datasource_Root_Hint;
	}
}

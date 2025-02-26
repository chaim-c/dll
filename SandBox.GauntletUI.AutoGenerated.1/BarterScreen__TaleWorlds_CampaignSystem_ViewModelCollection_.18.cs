﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.CampaignSystem.ViewModelCollection.Barter;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Party;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x02000017 RID: 23
	public class BarterScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Barter_BarterVM_Dependency_17_BarterToggleTuple__DependendPrefab : PartyHeaderToggleWidget
	{
		// Token: 0x06000354 RID: 852 RVA: 0x0001C5C5 File Offset: 0x0001A7C5
		public BarterScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Barter_BarterVM_Dependency_17_BarterToggleTuple__DependendPrefab(UIContext context) : base(context)
		{
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0001C5D0 File Offset: 0x0001A7D0
		private VisualDefinition CreateVisualDefinitionToggle()
		{
			VisualDefinition visualDefinition = new VisualDefinition("Toggle", 0.045f, 0f, false);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				SuggestedWidth = 465f
			});
			visualDefinition.AddVisualState(new VisualState("Pressed")
			{
				SuggestedWidth = 465f
			});
			visualDefinition.AddVisualState(new VisualState("Selected")
			{
				SuggestedWidth = 465f
			});
			visualDefinition.AddVisualState(new VisualState("Hovered")
			{
				SuggestedWidth = 465f
			});
			return visualDefinition;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0001C668 File Offset: 0x0001A868
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new ListPanel(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_0_0 = new BrushWidget(base.Context);
			this._widget_0.AddChild(this._widget_0_0);
			this._widget_0_1 = new RichTextWidget(base.Context);
			this._widget_0.AddChild(this._widget_0_1);
			this._widget_0_2 = new RichTextWidget(base.Context);
			this._widget_0.AddChild(this._widget_0_2);
			this._widget_1 = new ButtonWidget(base.Context);
			this._widget.AddChild(this._widget_1);
			this._widget_1_0 = new HintWidget(base.Context);
			this._widget_1.AddChild(this._widget_1_0);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0001C748 File Offset: 0x0001A948
		public void SetIds()
		{
			this._widget_0.Id = "Description";
			this._widget_0_0.Id = "CollapseIndicator";
			this._widget_1.Id = "TransferAll";
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0001C77C File Offset: 0x0001A97C
		public void SetAttributes()
		{
			base.VisualDefinition = this.CreateVisualDefinitionToggle();
			base.WidthSizePolicy = SizePolicy.Fixed;
			base.HeightSizePolicy = SizePolicy.Fixed;
			base.SuggestedWidth = 465f;
			base.SuggestedHeight = 73f;
			base.VerticalAlignment = VerticalAlignment.Top;
			base.MarginTop = 4f;
			base.Brush = base.Context.GetBrush("Barter.LeftPanel.Toggle");
			base.CollapseIndicator = this._widget_0_0;
			base.UpdateChildrenStates = true;
			this._widget_0.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_0.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_0.VerticalAlignment = VerticalAlignment.Center;
			this._widget_0.MarginBottom = 10f;
			this._widget_0.IsDisabled = true;
			this._widget_0.StackLayout.LayoutMethod = LayoutMethod.HorizontalLeftToRight;
			this._widget_0.UpdateChildrenStates = true;
			this._widget_0.DoNotPassEventsToChildren = true;
			this._widget_0_0.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0_0.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_0_0.SuggestedWidth = 40f;
			this._widget_0_0.SuggestedHeight = 40f;
			this._widget_0_0.VerticalAlignment = VerticalAlignment.Center;
			this._widget_0_0.PositionYOffset = 5f;
			this._widget_0_0.MarginRight = 5f;
			this._widget_0_0.Brush = base.Context.GetBrush("Party.Toggle.ExpandIndicator");
			this._widget_0_1.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_1.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_0_1.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_0_1.VerticalAlignment = VerticalAlignment.Center;
			this._widget_0_1.MarginRight = 5f;
			this._widget_0_1.Brush = base.Context.GetBrush("Party.Text.Toggle");
			this._widget_0_2.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_2.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_0_2.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_0_2.VerticalAlignment = VerticalAlignment.Center;
			this._widget_0_2.MarginLeft = 5f;
			this._widget_0_2.Brush = base.Context.GetBrush("Party.Text.Toggle");
			this._widget_0_2.Text = "";
			this._widget_1.DoNotPassEventsToChildren = true;
			this._widget_1.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_1.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_1.SuggestedWidth = 42f;
			this._widget_1.SuggestedHeight = 42f;
			this._widget_1.HorizontalAlignment = HorizontalAlignment.Right;
			this._widget_1.VerticalAlignment = VerticalAlignment.Center;
			this._widget_1.MarginRight = 5f;
			this._widget_1.MarginLeft = 5f;
			this._widget_1.Brush = base.Context.GetBrush("ButtonLeftDoubleArrowBrush1");
			this._widget_1_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_1_0.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_1_0.IsDisabled = true;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0001CA6C File Offset: 0x0001AC6C
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
				this._widget.EventFire -= this.EventListenerOf_widget;
				this._widget_0_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0_1;
				this._widget_1.EventFire -= this.EventListenerOf_widget_1;
				this._widget_1_0.EventFire -= this.EventListenerOf_widget_1_0;
				this._datasource_Root = null;
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0001CC6E File Offset: 0x0001AE6E
		public void SetDataSource(BarterVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0001CC77 File Offset: 0x0001AE77
		private void EventListenerOf_widget(Widget widget, string commandName, object[] args)
		{
			commandName == "Drop";
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001CC85 File Offset: 0x0001AE85
		private void EventListenerOf_widget_1(Widget widget, string commandName, object[] args)
		{
			if (commandName == "Click")
			{
				this._datasource_Root.ExecuteTransferAllLeftItem();
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001CC9F File Offset: 0x0001AE9F
		private void EventListenerOf_widget_1_0(Widget widget, string commandName, object[] args)
		{
			commandName == "HoverBegin";
			commandName == "HoverEnd";
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0001CCB9 File Offset: 0x0001AEB9
		private void PropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001CCC2 File Offset: 0x0001AEC2
		private void boolPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001CCCB File Offset: 0x0001AECB
		private void floatPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001CCD4 File Offset: 0x0001AED4
		private void Vec2PropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001CCDD File Offset: 0x0001AEDD
		private void Vector2PropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001CCE6 File Offset: 0x0001AEE6
		private void doublePropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001CCEF File Offset: 0x0001AEEF
		private void intPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001CCF8 File Offset: 0x0001AEF8
		private void uintPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001CD01 File Offset: 0x0001AF01
		private void ColorPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001CD0A File Offset: 0x0001AF0A
		private void HandleWidgetPropertyChangeOf_widget_0_1(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.ItemLbl = this._widget_0_1.Text;
				return;
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0001CD30 File Offset: 0x0001AF30
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001CD3E File Offset: 0x0001AF3E
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0001CD4C File Offset: 0x0001AF4C
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0001CD5A File Offset: 0x0001AF5A
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001CD68 File Offset: 0x0001AF68
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001CD76 File Offset: 0x0001AF76
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001CD84 File Offset: 0x0001AF84
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001CD92 File Offset: 0x0001AF92
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001CDA0 File Offset: 0x0001AFA0
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001CDAE File Offset: 0x0001AFAE
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "ItemLbl")
			{
				this._widget_0_1.Text = this._datasource_Root.ItemLbl;
				return;
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001CDD4 File Offset: 0x0001AFD4
		private void RefreshDataSource_datasource_Root(BarterVM newDataSource)
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
				this._widget.EventFire -= this.EventListenerOf_widget;
				this._widget_0_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0_1;
				this._widget_1.EventFire -= this.EventListenerOf_widget_1;
				this._widget_1_0.EventFire -= this.EventListenerOf_widget_1_0;
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
				this._widget.EventFire += this.EventListenerOf_widget;
				this._widget_0_1.Text = this._datasource_Root.ItemLbl;
				this._widget_0_1.PropertyChanged += this.PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.intPropertyChanged += this.intPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_0_1;
				this._widget_1.EventFire += this.EventListenerOf_widget_1;
				this._widget_1_0.EventFire += this.EventListenerOf_widget_1_0;
			}
		}

		// Token: 0x040000D1 RID: 209
		private PartyHeaderToggleWidget _widget;

		// Token: 0x040000D2 RID: 210
		private ListPanel _widget_0;

		// Token: 0x040000D3 RID: 211
		private BrushWidget _widget_0_0;

		// Token: 0x040000D4 RID: 212
		private RichTextWidget _widget_0_1;

		// Token: 0x040000D5 RID: 213
		private RichTextWidget _widget_0_2;

		// Token: 0x040000D6 RID: 214
		private ButtonWidget _widget_1;

		// Token: 0x040000D7 RID: 215
		private HintWidget _widget_1_0;

		// Token: 0x040000D8 RID: 216
		private BarterVM _datasource_Root;
	}
}

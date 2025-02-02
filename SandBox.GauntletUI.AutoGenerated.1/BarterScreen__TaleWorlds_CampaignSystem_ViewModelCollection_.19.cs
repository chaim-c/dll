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
	// Token: 0x02000018 RID: 24
	public class BarterScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Barter_BarterVM_Dependency_18_BarterToggleTuple__DependendPrefab : PartyHeaderToggleWidget
	{
		// Token: 0x06000373 RID: 883 RVA: 0x0001D1E1 File Offset: 0x0001B3E1
		public BarterScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Barter_BarterVM_Dependency_18_BarterToggleTuple__DependendPrefab(UIContext context) : base(context)
		{
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0001D1EC File Offset: 0x0001B3EC
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

		// Token: 0x06000375 RID: 885 RVA: 0x0001D284 File Offset: 0x0001B484
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

		// Token: 0x06000376 RID: 886 RVA: 0x0001D364 File Offset: 0x0001B564
		public void SetIds()
		{
			this._widget_0.Id = "Description";
			this._widget_0_0.Id = "CollapseIndicator";
			this._widget_1.Id = "TransferAll";
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001D398 File Offset: 0x0001B598
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

		// Token: 0x06000378 RID: 888 RVA: 0x0001D688 File Offset: 0x0001B888
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

		// Token: 0x06000379 RID: 889 RVA: 0x0001D88A File Offset: 0x0001BA8A
		public void SetDataSource(BarterVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0001D893 File Offset: 0x0001BA93
		private void EventListenerOf_widget(Widget widget, string commandName, object[] args)
		{
			commandName == "Drop";
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001D8A1 File Offset: 0x0001BAA1
		private void EventListenerOf_widget_1(Widget widget, string commandName, object[] args)
		{
			commandName == "Click";
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001D8AF File Offset: 0x0001BAAF
		private void EventListenerOf_widget_1_0(Widget widget, string commandName, object[] args)
		{
			commandName == "HoverBegin";
			commandName == "HoverEnd";
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001D8C9 File Offset: 0x0001BAC9
		private void PropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001D8D2 File Offset: 0x0001BAD2
		private void boolPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001D8DB File Offset: 0x0001BADB
		private void floatPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001D8E4 File Offset: 0x0001BAE4
		private void Vec2PropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001D8ED File Offset: 0x0001BAED
		private void Vector2PropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001D8F6 File Offset: 0x0001BAF6
		private void doublePropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001D8FF File Offset: 0x0001BAFF
		private void intPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001D908 File Offset: 0x0001BB08
		private void uintPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001D911 File Offset: 0x0001BB11
		private void ColorPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001D91A File Offset: 0x0001BB1A
		private void HandleWidgetPropertyChangeOf_widget_0_1(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.DiplomaticLbl = this._widget_0_1.Text;
				return;
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0001D940 File Offset: 0x0001BB40
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001D94E File Offset: 0x0001BB4E
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001D95C File Offset: 0x0001BB5C
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001D96A File Offset: 0x0001BB6A
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001D978 File Offset: 0x0001BB78
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001D986 File Offset: 0x0001BB86
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001D994 File Offset: 0x0001BB94
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001D9A2 File Offset: 0x0001BBA2
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001D9B0 File Offset: 0x0001BBB0
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001D9BE File Offset: 0x0001BBBE
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "DiplomaticLbl")
			{
				this._widget_0_1.Text = this._datasource_Root.DiplomaticLbl;
				return;
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001D9E4 File Offset: 0x0001BBE4
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
				this._widget_0_1.Text = this._datasource_Root.DiplomaticLbl;
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

		// Token: 0x040000D9 RID: 217
		private PartyHeaderToggleWidget _widget;

		// Token: 0x040000DA RID: 218
		private ListPanel _widget_0;

		// Token: 0x040000DB RID: 219
		private BrushWidget _widget_0_0;

		// Token: 0x040000DC RID: 220
		private RichTextWidget _widget_0_1;

		// Token: 0x040000DD RID: 221
		private RichTextWidget _widget_0_2;

		// Token: 0x040000DE RID: 222
		private ButtonWidget _widget_1;

		// Token: 0x040000DF RID: 223
		private HintWidget _widget_1_0;

		// Token: 0x040000E0 RID: 224
		private BarterVM _datasource_Root;
	}
}

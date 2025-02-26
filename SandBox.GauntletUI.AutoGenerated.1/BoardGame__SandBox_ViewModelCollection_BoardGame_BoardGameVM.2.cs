﻿using System;
using System.ComponentModel;
using System.Numerics;
using SandBox.ViewModelCollection.BoardGame;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.BoardGame;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x02000027 RID: 39
	public class BoardGame__SandBox_ViewModelCollection_BoardGame_BoardGameVM_Dependency_1_ItemTemplate : Widget
	{
		// Token: 0x060006F8 RID: 1784 RVA: 0x00036807 File Offset: 0x00034A07
		public BoardGame__SandBox_ViewModelCollection_BoardGame_BoardGameVM_Dependency_1_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00036810 File Offset: 0x00034A10
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new TextWidget(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_1 = new ListPanel(base.Context);
			this._widget.AddChild(this._widget_1);
			this._widget_1_0 = new BoardGameInstructionVisualWidget(base.Context);
			this._widget_1.AddChild(this._widget_1_0);
			this._widget_1_1 = new TextWidget(base.Context);
			this._widget_1.AddChild(this._widget_1_1);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x000368AC File Offset: 0x00034AAC
		public void SetIds()
		{
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x000368B0 File Offset: 0x00034AB0
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.StretchToParent;
			base.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_0.Brush = base.Context.GetBrush("BoardGame.InstructionsTitle.Text");
			this._widget_1.StackLayout.LayoutMethod = LayoutMethod.VerticalBottomToTop;
			this._widget_1.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_1.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_1.MarginTop = 50f;
			this._widget_1.VerticalAlignment = VerticalAlignment.Center;
			this._widget_1_0.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_1_0.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_1_0.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_1_0.MarginTop = 30f;
			this._widget_1_1.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_1_1.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_1_1.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_1_1.MarginTop = 30f;
			this._widget_1_1.MarginBottom = 30f;
			this._widget_1_1.Brush = base.Context.GetBrush("BoardGame.Instructions.Text");
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x000369E4 File Offset: 0x00034BE4
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
				this._widget.PropertyChanged -= this.PropertyChangedListenerOf_widget;
				this._widget.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget;
				this._widget.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget;
				this._widget.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget;
				this._widget.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget;
				this._widget.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget;
				this._widget.intPropertyChanged -= this.intPropertyChangedListenerOf_widget;
				this._widget.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget;
				this._widget.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget;
				this._widget_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0;
				this._widget_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0;
				this._widget_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0;
				this._widget_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0;
				this._widget_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0;
				this._widget_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0;
				this._widget_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0;
				this._widget_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0;
				this._widget_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0;
				this._widget_1_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1_0;
				this._widget_1_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1_1;
				this._datasource_Root = null;
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00036E0E File Offset: 0x0003500E
		public void SetDataSource(BoardGameInstructionVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00036E17 File Offset: 0x00035017
		private void PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00036E20 File Offset: 0x00035020
		private void boolPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00036E29 File Offset: 0x00035029
		private void floatPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00036E32 File Offset: 0x00035032
		private void Vec2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00036E3B File Offset: 0x0003503B
		private void Vector2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00036E44 File Offset: 0x00035044
		private void doublePropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00036E4D File Offset: 0x0003504D
		private void intPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00036E56 File Offset: 0x00035056
		private void uintPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00036E5F File Offset: 0x0003505F
		private void ColorPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00036E68 File Offset: 0x00035068
		private void HandleWidgetPropertyChangeOf_widget(string propertyName)
		{
			if (propertyName == "IsVisible")
			{
				this._datasource_Root.IsEnabled = this._widget.IsVisible;
				return;
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00036E8E File Offset: 0x0003508E
		private void PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00036E97 File Offset: 0x00035097
		private void boolPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00036EA0 File Offset: 0x000350A0
		private void floatPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00036EA9 File Offset: 0x000350A9
		private void Vec2PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00036EB2 File Offset: 0x000350B2
		private void Vector2PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00036EBB File Offset: 0x000350BB
		private void doublePropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00036EC4 File Offset: 0x000350C4
		private void intPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00036ECD File Offset: 0x000350CD
		private void uintPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00036ED6 File Offset: 0x000350D6
		private void ColorPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00036EDF File Offset: 0x000350DF
		private void HandleWidgetPropertyChangeOf_widget_0(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.TitleText = this._widget_0.Text;
				return;
			}
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00036F05 File Offset: 0x00035105
		private void PropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00036F0E File Offset: 0x0003510E
		private void boolPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00036F17 File Offset: 0x00035117
		private void floatPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00036F20 File Offset: 0x00035120
		private void Vec2PropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00036F29 File Offset: 0x00035129
		private void Vector2PropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00036F32 File Offset: 0x00035132
		private void doublePropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00036F3B File Offset: 0x0003513B
		private void intPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00036F44 File Offset: 0x00035144
		private void uintPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00036F4D File Offset: 0x0003514D
		private void ColorPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00036F56 File Offset: 0x00035156
		private void HandleWidgetPropertyChangeOf_widget_1_0(string propertyName)
		{
			if (propertyName == "GameType")
			{
				this._datasource_Root.GameType = this._widget_1_0.GameType;
				return;
			}
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00036F7C File Offset: 0x0003517C
		private void PropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00036F85 File Offset: 0x00035185
		private void boolPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00036F8E File Offset: 0x0003518E
		private void floatPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00036F97 File Offset: 0x00035197
		private void Vec2PropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00036FA0 File Offset: 0x000351A0
		private void Vector2PropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00036FA9 File Offset: 0x000351A9
		private void doublePropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00036FB2 File Offset: 0x000351B2
		private void intPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00036FBB File Offset: 0x000351BB
		private void uintPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00036FC4 File Offset: 0x000351C4
		private void ColorPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00036FCD File Offset: 0x000351CD
		private void HandleWidgetPropertyChangeOf_widget_1_1(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.DescriptionText = this._widget_1_1.Text;
				return;
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00036FF3 File Offset: 0x000351F3
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00037001 File Offset: 0x00035201
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0003700F File Offset: 0x0003520F
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0003701D File Offset: 0x0003521D
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0003702B File Offset: 0x0003522B
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00037039 File Offset: 0x00035239
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00037047 File Offset: 0x00035247
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00037055 File Offset: 0x00035255
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00037063 File Offset: 0x00035263
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00037074 File Offset: 0x00035274
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "IsEnabled")
			{
				this._widget.IsVisible = this._datasource_Root.IsEnabled;
				return;
			}
			if (propertyName == "TitleText")
			{
				this._widget_0.Text = this._datasource_Root.TitleText;
				return;
			}
			if (propertyName == "GameType")
			{
				this._widget_1_0.GameType = this._datasource_Root.GameType;
				return;
			}
			if (propertyName == "DescriptionText")
			{
				this._widget_1_1.Text = this._datasource_Root.DescriptionText;
				return;
			}
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00037114 File Offset: 0x00035314
		private void RefreshDataSource_datasource_Root(BoardGameInstructionVM newDataSource)
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
				this._widget.PropertyChanged -= this.PropertyChangedListenerOf_widget;
				this._widget.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget;
				this._widget.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget;
				this._widget.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget;
				this._widget.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget;
				this._widget.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget;
				this._widget.intPropertyChanged -= this.intPropertyChangedListenerOf_widget;
				this._widget.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget;
				this._widget.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget;
				this._widget_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0;
				this._widget_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0;
				this._widget_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0;
				this._widget_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0;
				this._widget_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0;
				this._widget_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0;
				this._widget_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0;
				this._widget_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0;
				this._widget_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0;
				this._widget_1_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1_0;
				this._widget_1_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1_1;
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
				this._widget.IsVisible = this._datasource_Root.IsEnabled;
				this._widget.PropertyChanged += this.PropertyChangedListenerOf_widget;
				this._widget.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget;
				this._widget.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget;
				this._widget.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget;
				this._widget.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget;
				this._widget.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget;
				this._widget.intPropertyChanged += this.intPropertyChangedListenerOf_widget;
				this._widget.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget;
				this._widget.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget;
				this._widget_0.Text = this._datasource_Root.TitleText;
				this._widget_0.PropertyChanged += this.PropertyChangedListenerOf_widget_0;
				this._widget_0.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_0;
				this._widget_0.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_0;
				this._widget_0.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_0;
				this._widget_0.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_0;
				this._widget_0.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_0;
				this._widget_0.intPropertyChanged += this.intPropertyChangedListenerOf_widget_0;
				this._widget_0.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_0;
				this._widget_0.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_0;
				this._widget_1_0.GameType = this._datasource_Root.GameType;
				this._widget_1_0.PropertyChanged += this.PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.intPropertyChanged += this.intPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_1_0;
				this._widget_1_1.Text = this._datasource_Root.DescriptionText;
				this._widget_1_1.PropertyChanged += this.PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.intPropertyChanged += this.intPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_1_1;
			}
		}

		// Token: 0x040001AE RID: 430
		private Widget _widget;

		// Token: 0x040001AF RID: 431
		private TextWidget _widget_0;

		// Token: 0x040001B0 RID: 432
		private ListPanel _widget_1;

		// Token: 0x040001B1 RID: 433
		private BoardGameInstructionVisualWidget _widget_1_0;

		// Token: 0x040001B2 RID: 434
		private TextWidget _widget_1_1;

		// Token: 0x040001B3 RID: 435
		private BoardGameInstructionVM _datasource_Root;
	}
}

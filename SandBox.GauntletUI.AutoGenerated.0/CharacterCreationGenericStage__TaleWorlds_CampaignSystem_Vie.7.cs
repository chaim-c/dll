﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.AutoGenerated0
{
	// Token: 0x0200001B RID: 27
	public class CharacterCreationGenericStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationGenericStageVM_Dependency_6_ItemTemplate : Widget
	{
		// Token: 0x06000806 RID: 2054 RVA: 0x0003D8EB File Offset: 0x0003BAEB
		public CharacterCreationGenericStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationGenericStageVM_Dependency_6_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0003D8F4 File Offset: 0x0003BAF4
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new Widget(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_1 = new Widget(base.Context);
			this._widget.AddChild(this._widget_1);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0003D94C File Offset: 0x0003BB4C
		public void SetIds()
		{
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0003D950 File Offset: 0x0003BB50
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.Fixed;
			base.HeightSizePolicy = SizePolicy.Fixed;
			base.SuggestedWidth = 6f;
			base.SuggestedHeight = 24f;
			base.MarginLeft = 5f;
			base.MarginRight = 1f;
			base.VerticalAlignment = VerticalAlignment.Center;
			base.DoNotUseCustomScaleAndChildren = true;
			this._widget_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.Sprite = base.Context.SpriteData.GetSprite("BlankWhiteSquare");
			this._widget_0.Color = new Color(0.6f, 0.8627452f, 0.1882353f, 1f);
			this._widget_1.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_1.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_1.Sprite = base.Context.SpriteData.GetSprite("BlankWhiteSquare");
			this._widget_1.Color = new Color(0.6f, 0.8627452f, 0.1882353f, 1f);
			this._widget_1.ColorFactor = 0.5f;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0003DA70 File Offset: 0x0003BC70
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
				this._widget_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0;
				this._widget_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0;
				this._widget_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0;
				this._widget_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0;
				this._widget_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0;
				this._widget_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0;
				this._widget_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0;
				this._widget_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0;
				this._widget_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0;
				this._widget_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_1;
				this._widget_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1;
				this._widget_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1;
				this._widget_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1;
				this._widget_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1;
				this._widget_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1;
				this._widget_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1;
				this._widget_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1;
				this._widget_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1;
				this._datasource_Root = null;
			}
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0003DCFC File Offset: 0x0003BEFC
		public void SetDataSource(BoolItemWithActionVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0003DD05 File Offset: 0x0003BF05
		private void PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0003DD0E File Offset: 0x0003BF0E
		private void boolPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0003DD17 File Offset: 0x0003BF17
		private void floatPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0003DD20 File Offset: 0x0003BF20
		private void Vec2PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0003DD29 File Offset: 0x0003BF29
		private void Vector2PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0003DD32 File Offset: 0x0003BF32
		private void doublePropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0003DD3B File Offset: 0x0003BF3B
		private void intPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0003DD44 File Offset: 0x0003BF44
		private void uintPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0003DD4D File Offset: 0x0003BF4D
		private void ColorPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0003DD56 File Offset: 0x0003BF56
		private void HandleWidgetPropertyChangeOf_widget_0(string propertyName)
		{
			if (propertyName == "IsVisible")
			{
				this._datasource_Root.IsActive = this._widget_0.IsVisible;
				return;
			}
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0003DD7C File Offset: 0x0003BF7C
		private void PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0003DD85 File Offset: 0x0003BF85
		private void boolPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0003DD8E File Offset: 0x0003BF8E
		private void floatPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0003DD97 File Offset: 0x0003BF97
		private void Vec2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0003DDA0 File Offset: 0x0003BFA0
		private void Vector2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0003DDA9 File Offset: 0x0003BFA9
		private void doublePropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0003DDB2 File Offset: 0x0003BFB2
		private void intPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0003DDBB File Offset: 0x0003BFBB
		private void uintPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0003DDC4 File Offset: 0x0003BFC4
		private void ColorPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0003DDCD File Offset: 0x0003BFCD
		private void HandleWidgetPropertyChangeOf_widget_1(string propertyName)
		{
			if (propertyName == "IsHidden")
			{
				this._datasource_Root.IsActive = this._widget_1.IsHidden;
				return;
			}
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0003DDF3 File Offset: 0x0003BFF3
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0003DE01 File Offset: 0x0003C001
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0003DE0F File Offset: 0x0003C00F
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0003DE1D File Offset: 0x0003C01D
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0003DE2B File Offset: 0x0003C02B
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0003DE39 File Offset: 0x0003C039
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0003DE47 File Offset: 0x0003C047
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0003DE55 File Offset: 0x0003C055
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0003DE63 File Offset: 0x0003C063
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0003DE71 File Offset: 0x0003C071
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "IsActive")
			{
				this._widget_0.IsVisible = this._datasource_Root.IsActive;
				this._widget_1.IsHidden = this._datasource_Root.IsActive;
				return;
			}
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0003DEB0 File Offset: 0x0003C0B0
		private void RefreshDataSource_datasource_Root(BoolItemWithActionVM newDataSource)
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
				this._widget_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0;
				this._widget_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0;
				this._widget_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0;
				this._widget_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0;
				this._widget_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0;
				this._widget_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0;
				this._widget_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0;
				this._widget_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0;
				this._widget_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0;
				this._widget_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_1;
				this._widget_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1;
				this._widget_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1;
				this._widget_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1;
				this._widget_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1;
				this._widget_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1;
				this._widget_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1;
				this._widget_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1;
				this._widget_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1;
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
				this._widget_0.IsVisible = this._datasource_Root.IsActive;
				this._widget_0.PropertyChanged += this.PropertyChangedListenerOf_widget_0;
				this._widget_0.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_0;
				this._widget_0.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_0;
				this._widget_0.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_0;
				this._widget_0.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_0;
				this._widget_0.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_0;
				this._widget_0.intPropertyChanged += this.intPropertyChangedListenerOf_widget_0;
				this._widget_0.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_0;
				this._widget_0.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_0;
				this._widget_1.IsHidden = this._datasource_Root.IsActive;
				this._widget_1.PropertyChanged += this.PropertyChangedListenerOf_widget_1;
				this._widget_1.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_1;
				this._widget_1.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_1;
				this._widget_1.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_1;
				this._widget_1.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_1;
				this._widget_1.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_1;
				this._widget_1.intPropertyChanged += this.intPropertyChangedListenerOf_widget_1;
				this._widget_1.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_1;
				this._widget_1.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_1;
			}
		}

		// Token: 0x0400019B RID: 411
		private Widget _widget;

		// Token: 0x0400019C RID: 412
		private Widget _widget_0;

		// Token: 0x0400019D RID: 413
		private Widget _widget_1;

		// Token: 0x0400019E RID: 414
		private BoolItemWithActionVM _datasource_Root;
	}
}

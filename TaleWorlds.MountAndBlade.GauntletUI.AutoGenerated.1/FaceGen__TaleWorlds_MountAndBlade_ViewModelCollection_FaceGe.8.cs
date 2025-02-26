﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.GamepadNavigation;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;
using TaleWorlds.MountAndBlade.ViewModelCollection.FaceGenerator;

namespace TaleWorlds.MountAndBlade.GauntletUI.AutoGenerated1
{
	// Token: 0x0200000D RID: 13
	public class FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_7_FaceGenHair__DependendPrefab : ListPanel
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x000236BF File Offset: 0x000218BF
		public FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_7_FaceGenHair__DependendPrefab(UIContext context) : base(context)
		{
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000236C8 File Offset: 0x000218C8
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new NavigationScopeTargeter(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_1 = new FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_16_FaceGenColorSelector__DependendPrefab(base.Context);
			this._widget.AddChild(this._widget_1);
			this._widget_1.CreateWidgets();
			this._widget_2 = new Widget(base.Context);
			this._widget.AddChild(this._widget_2);
			this._widget_3 = new NavigationScopeTargeter(base.Context);
			this._widget.AddChild(this._widget_3);
			this._widget_4 = new FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_17_FaceGenGrid__DependendPrefab(base.Context);
			this._widget.AddChild(this._widget_4);
			this._widget_4.CreateWidgets();
			this._widget_5 = new Widget(base.Context);
			this._widget.AddChild(this._widget_5);
			this._widget_6 = new NavigationScopeTargeter(base.Context);
			this._widget.AddChild(this._widget_6);
			this._widget_7 = new FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_18_FaceGenGrid__DependendPrefab(base.Context);
			this._widget.AddChild(this._widget_7);
			this._widget_7.CreateWidgets();
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00023810 File Offset: 0x00021A10
		public void SetIds()
		{
			this._widget_1.SetIds();
			this._widget_1.Id = "HairColor";
			this._widget_4.SetIds();
			this._widget_4.Id = "HairTypes";
			this._widget_7.SetIds();
			this._widget_7.Id = "BeardTypes";
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00023870 File Offset: 0x00021A70
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.StretchToParent;
			base.HeightSizePolicy = SizePolicy.StretchToParent;
			base.StackLayout.LayoutMethod = LayoutMethod.VerticalBottomToTop;
			this._widget_0.ScopeID = "HairColorScope";
			this._widget_0.ScopeParent = this._widget_1;
			this._widget_0.ScopeMovements = GamepadNavigationTypes.Horizontal;
			this._widget_0.AlternateScopeMovements = GamepadNavigationTypes.Vertical;
			this._widget_0.AlternateMovementStepSize = 12;
			this._widget_0.HasCircularMovement = false;
			this._widget_1.SetAttributes();
			this._widget_1.MarginTop = 10f;
			this._widget_2.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_2.SuggestedWidth = 581f;
			this._widget_2.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_2.SuggestedHeight = 14f;
			this._widget_2.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_2.Sprite = base.Context.SpriteData.GetSprite("General\\CharacterCreation\\stone_divider");
			this._widget_3.ScopeID = "HairTypeScope";
			this._widget_3.ScopeParent = this._widget_4;
			this._widget_3.ScopeMovements = GamepadNavigationTypes.Horizontal;
			this._widget_3.AlternateScopeMovements = GamepadNavigationTypes.Vertical;
			this._widget_3.AlternateMovementStepSize = 5;
			this._widget_3.HasCircularMovement = false;
			this._widget_4.SetAttributes();
			this._widget_4.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_4.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_4.MarginRight = 3f;
			this._widget_5.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_5.SuggestedWidth = 580f;
			this._widget_5.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_5.SuggestedHeight = 11f;
			this._widget_5.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_5.Sprite = base.Context.SpriteData.GetSprite("General\\CharacterCreation\\stone_divider_thin");
			this._widget_5.ExtendTop = 19f;
			this._widget_5.ExtendBottom = 20f;
			this._widget_6.ScopeID = "BeardTypeScope";
			this._widget_6.ScopeParent = this._widget_7;
			this._widget_6.ScopeMovements = GamepadNavigationTypes.Horizontal;
			this._widget_6.AlternateScopeMovements = GamepadNavigationTypes.Vertical;
			this._widget_6.AlternateMovementStepSize = 5;
			this._widget_6.HasCircularMovement = false;
			this._widget_7.SetAttributes();
			this._widget_7.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_7.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_7.MarginRight = 3f;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00023AF8 File Offset: 0x00021CF8
		public void DestroyDataSource()
		{
			if (this._datasource_Root != null)
			{
				this._widget_4.DestroyDataSource();
				this._widget_7.DestroyDataSource();
				this._datasource_Root.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root;
				this._widget_7.PropertyChanged -= this.PropertyChangedListenerOf_widget_7;
				this._widget_7.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_7;
				this._widget_7.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_7;
				this._widget_7.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_7;
				this._widget_7.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_7;
				this._widget_7.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_7;
				this._widget_7.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_7;
				this._widget_7.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_7;
				this._widget_7.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_7;
				if (this._datasource_Root_HairColorSelector != null)
				{
					this._widget_1.DestroyDataSource();
					this._datasource_Root_HairColorSelector.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector = null;
				}
				this._datasource_Root = null;
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00023DB7 File Offset: 0x00021FB7
		public void SetDataSource(FaceGenVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00023DC0 File Offset: 0x00021FC0
		private void PropertyChangedListenerOf_widget_7(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_7(propertyName);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00023DC9 File Offset: 0x00021FC9
		private void boolPropertyChangedListenerOf_widget_7(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_7(propertyName);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00023DD2 File Offset: 0x00021FD2
		private void floatPropertyChangedListenerOf_widget_7(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_7(propertyName);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00023DDB File Offset: 0x00021FDB
		private void Vec2PropertyChangedListenerOf_widget_7(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_7(propertyName);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00023DE4 File Offset: 0x00021FE4
		private void Vector2PropertyChangedListenerOf_widget_7(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_7(propertyName);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00023DED File Offset: 0x00021FED
		private void doublePropertyChangedListenerOf_widget_7(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_7(propertyName);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00023DF6 File Offset: 0x00021FF6
		private void intPropertyChangedListenerOf_widget_7(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_7(propertyName);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00023DFF File Offset: 0x00021FFF
		private void uintPropertyChangedListenerOf_widget_7(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_7(propertyName);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00023E08 File Offset: 0x00022008
		private void ColorPropertyChangedListenerOf_widget_7(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_7(propertyName);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00023E11 File Offset: 0x00022011
		private void HandleWidgetPropertyChangeOf_widget_7(string propertyName)
		{
			propertyName == "IsHidden";
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00023E1F File Offset: 0x0002201F
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00023E2D File Offset: 0x0002202D
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00023E3B File Offset: 0x0002203B
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00023E49 File Offset: 0x00022049
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00023E57 File Offset: 0x00022057
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00023E65 File Offset: 0x00022065
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00023E73 File Offset: 0x00022073
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00023E81 File Offset: 0x00022081
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00023E8F File Offset: 0x0002208F
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00023EA0 File Offset: 0x000220A0
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "HairColorSelector")
			{
				this.RefreshDataSource_datasource_Root_HairColorSelector(this._datasource_Root.HairColorSelector);
				return;
			}
			if (propertyName == "IsFemale")
			{
				this._widget_7.IsHidden = this._datasource_Root.IsFemale;
				return;
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00023EF0 File Offset: 0x000220F0
		private void ViewModelPropertyChangedListenerOf_datasource_Root_HairColorSelector(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_HairColorSelector(e.PropertyName);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00023EFE File Offset: 0x000220FE
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root_HairColorSelector(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_HairColorSelector(e.PropertyName);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00023F0C File Offset: 0x0002210C
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_HairColorSelector(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_HairColorSelector(e.PropertyName);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00023F1A File Offset: 0x0002211A
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_HairColorSelector(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_HairColorSelector(e.PropertyName);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00023F28 File Offset: 0x00022128
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_HairColorSelector(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_HairColorSelector(e.PropertyName);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00023F36 File Offset: 0x00022136
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_HairColorSelector(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_HairColorSelector(e.PropertyName);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00023F44 File Offset: 0x00022144
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_HairColorSelector(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_HairColorSelector(e.PropertyName);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00023F52 File Offset: 0x00022152
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_HairColorSelector(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_HairColorSelector(e.PropertyName);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00023F60 File Offset: 0x00022160
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_HairColorSelector(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_HairColorSelector(e.PropertyName);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00023F6E File Offset: 0x0002216E
		private void HandleViewModelPropertyChangeOf_datasource_Root_HairColorSelector(string propertyName)
		{
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00023F70 File Offset: 0x00022170
		private void RefreshDataSource_datasource_Root(FaceGenVM newDataSource)
		{
			if (this._datasource_Root != null)
			{
				this._widget_4.SetDataSource(null);
				this._widget_7.SetDataSource(null);
				this._datasource_Root.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root;
				this._widget_7.PropertyChanged -= this.PropertyChangedListenerOf_widget_7;
				this._widget_7.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_7;
				this._widget_7.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_7;
				this._widget_7.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_7;
				this._widget_7.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_7;
				this._widget_7.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_7;
				this._widget_7.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_7;
				this._widget_7.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_7;
				this._widget_7.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_7;
				if (this._datasource_Root_HairColorSelector != null)
				{
					this._widget_1.SetDataSource(null);
					this._datasource_Root_HairColorSelector.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector = null;
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
				this._widget_7.IsHidden = this._datasource_Root.IsFemale;
				this._widget_7.PropertyChanged += this.PropertyChangedListenerOf_widget_7;
				this._widget_7.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_7;
				this._widget_7.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_7;
				this._widget_7.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_7;
				this._widget_7.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_7;
				this._widget_7.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_7;
				this._widget_7.intPropertyChanged += this.intPropertyChangedListenerOf_widget_7;
				this._widget_7.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_7;
				this._widget_7.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_7;
				this._datasource_Root_HairColorSelector = this._datasource_Root.HairColorSelector;
				if (this._datasource_Root_HairColorSelector != null)
				{
					this._datasource_Root_HairColorSelector.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_HairColorSelector;
					this._datasource_Root_HairColorSelector.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_HairColorSelector;
					this._widget_1.SetDataSource(this._datasource_Root_HairColorSelector);
				}
				this._widget_4.SetDataSource(this._datasource_Root);
				this._widget_7.SetDataSource(this._datasource_Root);
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00024518 File Offset: 0x00022718
		private void RefreshDataSource_datasource_Root_HairColorSelector(SelectorVM<SelectorItemVM> newDataSource)
		{
			if (this._datasource_Root_HairColorSelector != null)
			{
				this._widget_1.SetDataSource(null);
				this._datasource_Root_HairColorSelector.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_HairColorSelector;
				this._datasource_Root_HairColorSelector.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_HairColorSelector;
				this._datasource_Root_HairColorSelector.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_HairColorSelector;
				this._datasource_Root_HairColorSelector.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_HairColorSelector;
				this._datasource_Root_HairColorSelector.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_HairColorSelector;
				this._datasource_Root_HairColorSelector.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_HairColorSelector;
				this._datasource_Root_HairColorSelector.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_HairColorSelector;
				this._datasource_Root_HairColorSelector.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_HairColorSelector;
				this._datasource_Root_HairColorSelector.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_HairColorSelector;
				this._datasource_Root_HairColorSelector = null;
			}
			this._datasource_Root_HairColorSelector = newDataSource;
			this._datasource_Root_HairColorSelector = this._datasource_Root.HairColorSelector;
			if (this._datasource_Root_HairColorSelector != null)
			{
				this._datasource_Root_HairColorSelector.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root_HairColorSelector;
				this._datasource_Root_HairColorSelector.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_HairColorSelector;
				this._datasource_Root_HairColorSelector.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_HairColorSelector;
				this._datasource_Root_HairColorSelector.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_HairColorSelector;
				this._datasource_Root_HairColorSelector.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_HairColorSelector;
				this._datasource_Root_HairColorSelector.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_HairColorSelector;
				this._datasource_Root_HairColorSelector.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_HairColorSelector;
				this._datasource_Root_HairColorSelector.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_HairColorSelector;
				this._datasource_Root_HairColorSelector.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_HairColorSelector;
				this._widget_1.SetDataSource(this._datasource_Root_HairColorSelector);
			}
		}

		// Token: 0x04000108 RID: 264
		private ListPanel _widget;

		// Token: 0x04000109 RID: 265
		private NavigationScopeTargeter _widget_0;

		// Token: 0x0400010A RID: 266
		private FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_16_FaceGenColorSelector__DependendPrefab _widget_1;

		// Token: 0x0400010B RID: 267
		private Widget _widget_2;

		// Token: 0x0400010C RID: 268
		private NavigationScopeTargeter _widget_3;

		// Token: 0x0400010D RID: 269
		private FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_17_FaceGenGrid__DependendPrefab _widget_4;

		// Token: 0x0400010E RID: 270
		private Widget _widget_5;

		// Token: 0x0400010F RID: 271
		private NavigationScopeTargeter _widget_6;

		// Token: 0x04000110 RID: 272
		private FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_18_FaceGenGrid__DependendPrefab _widget_7;

		// Token: 0x04000111 RID: 273
		private FaceGenVM _datasource_Root;

		// Token: 0x04000112 RID: 274
		private SelectorVM<SelectorItemVM> _datasource_Root_HairColorSelector;
	}
}

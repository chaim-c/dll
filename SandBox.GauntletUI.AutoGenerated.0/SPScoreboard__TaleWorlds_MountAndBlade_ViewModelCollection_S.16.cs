﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;
using TaleWorlds.MountAndBlade.ViewModelCollection.Scoreboard;

namespace SandBox.GauntletUI.AutoGenerated0
{
	// Token: 0x020000B4 RID: 180
	public class SPScoreboard__TaleWorlds_MountAndBlade_ViewModelCollection_Scoreboard_CustomBattleScoreboardVM_Dependency_15_SPScoreboardSkillItem__InheritedPrefab : Widget
	{
		// Token: 0x06003006 RID: 12294 RVA: 0x001784A7 File Offset: 0x001766A7
		public SPScoreboard__TaleWorlds_MountAndBlade_ViewModelCollection_Scoreboard_CustomBattleScoreboardVM_Dependency_15_SPScoreboardSkillItem__InheritedPrefab(UIContext context) : base(context)
		{
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x001784B0 File Offset: 0x001766B0
		public virtual void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new SkillIconVisualWidget(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_1 = new TextWidget(base.Context);
			this._widget.AddChild(this._widget_1);
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x00178508 File Offset: 0x00176708
		public virtual void SetIds()
		{
			base.Id = "SkillItemWidget";
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x00178518 File Offset: 0x00176718
		public virtual void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.StretchToParent;
			base.HeightSizePolicy = SizePolicy.Fixed;
			base.SuggestedHeight = 50f;
			base.MarginTop = 5f;
			base.MarginBottom = 5f;
			base.Sprite = base.Context.SpriteData.GetSprite("StdAssets\\DropdownMenuItem@2x");
			this._widget_0.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_0.SuggestedWidth = 40f;
			this._widget_0.SuggestedHeight = 40f;
			this._widget_0.MarginTop = 5f;
			this._widget_0.MarginLeft = 5f;
			this._widget_1.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_1.VerticalAlignment = VerticalAlignment.Center;
			this._widget_1.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_1.MarginLeft = 55f;
			this._widget_1.Brush = base.Context.GetBrush("LeftAlignedFont");
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x00178618 File Offset: 0x00176818
		public virtual void DestroyDataSource()
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

		// Token: 0x0600300B RID: 12299 RVA: 0x001788A4 File Offset: 0x00176AA4
		public virtual void SetDataSource(SPScoreboardSkillItemVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x001788AD File Offset: 0x00176AAD
		private void PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x001788B6 File Offset: 0x00176AB6
		private void boolPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x001788BF File Offset: 0x00176ABF
		private void floatPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x001788C8 File Offset: 0x00176AC8
		private void Vec2PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x001788D1 File Offset: 0x00176AD1
		private void Vector2PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x001788DA File Offset: 0x00176ADA
		private void doublePropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x001788E3 File Offset: 0x00176AE3
		private void intPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x001788EC File Offset: 0x00176AEC
		private void uintPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x001788F5 File Offset: 0x00176AF5
		private void ColorPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x001788FE File Offset: 0x00176AFE
		private void HandleWidgetPropertyChangeOf_widget_0(string propertyName)
		{
			if (propertyName == "SkillId")
			{
				this._datasource_Root.SkillId = this._widget_0.SkillId;
				return;
			}
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x00178924 File Offset: 0x00176B24
		private void PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x0017892D File Offset: 0x00176B2D
		private void boolPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x00178936 File Offset: 0x00176B36
		private void floatPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x0017893F File Offset: 0x00176B3F
		private void Vec2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x00178948 File Offset: 0x00176B48
		private void Vector2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x00178951 File Offset: 0x00176B51
		private void doublePropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x0017895A File Offset: 0x00176B5A
		private void intPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x00178963 File Offset: 0x00176B63
		private void uintPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x0017896C File Offset: 0x00176B6C
		private void ColorPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x00178975 File Offset: 0x00176B75
		private void HandleWidgetPropertyChangeOf_widget_1(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.Description = this._widget_1.Text;
				return;
			}
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x0017899B File Offset: 0x00176B9B
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x001789A9 File Offset: 0x00176BA9
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x001789B7 File Offset: 0x00176BB7
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x001789C5 File Offset: 0x00176BC5
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x001789D3 File Offset: 0x00176BD3
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x001789E1 File Offset: 0x00176BE1
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x001789EF File Offset: 0x00176BEF
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x001789FD File Offset: 0x00176BFD
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x00178A0B File Offset: 0x00176C0B
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x00178A1C File Offset: 0x00176C1C
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "SkillId")
			{
				this._widget_0.SkillId = this._datasource_Root.SkillId;
				return;
			}
			if (propertyName == "Description")
			{
				this._widget_1.Text = this._datasource_Root.Description;
				return;
			}
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x00178A74 File Offset: 0x00176C74
		private void RefreshDataSource_datasource_Root(SPScoreboardSkillItemVM newDataSource)
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
				this._widget_0.SkillId = this._datasource_Root.SkillId;
				this._widget_0.PropertyChanged += this.PropertyChangedListenerOf_widget_0;
				this._widget_0.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_0;
				this._widget_0.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_0;
				this._widget_0.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_0;
				this._widget_0.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_0;
				this._widget_0.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_0;
				this._widget_0.intPropertyChanged += this.intPropertyChangedListenerOf_widget_0;
				this._widget_0.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_0;
				this._widget_0.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_0;
				this._widget_1.Text = this._datasource_Root.Description;
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

		// Token: 0x04000989 RID: 2441
		private Widget _widget;

		// Token: 0x0400098A RID: 2442
		private SkillIconVisualWidget _widget_0;

		// Token: 0x0400098B RID: 2443
		private TextWidget _widget_1;

		// Token: 0x0400098C RID: 2444
		private SPScoreboardSkillItemVM _datasource_Root;
	}
}

﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD;

namespace TaleWorlds.MountAndBlade.GauntletUI.AutoGenerated1
{
	// Token: 0x0200002A RID: 42
	public class MainAgentHUD__TaleWorlds_MountAndBlade_ViewModelCollection_MissionAgentStatusVM_Dependency_5_ItemTemplate : TakenDamageItemBrushWidget
	{
		// Token: 0x06000911 RID: 2321 RVA: 0x0004608A File Offset: 0x0004428A
		public MainAgentHUD__TaleWorlds_MountAndBlade_ViewModelCollection_MissionAgentStatusVM_Dependency_5_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00046094 File Offset: 0x00044294
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new BrushWidget(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_1 = new BrushWidget(base.Context);
			this._widget.AddChild(this._widget_1);
			this._widget_2 = new BrushWidget(base.Context);
			this._widget.AddChild(this._widget_2);
			this._widget_3 = new BrushWidget(base.Context);
			this._widget.AddChild(this._widget_3);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00046130 File Offset: 0x00044330
		public void SetIds()
		{
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00046134 File Offset: 0x00044334
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.StretchToParent;
			base.HeightSizePolicy = SizePolicy.StretchToParent;
			base.Brush.GlobalAlphaFactor = 0.5f;
			base.RangedOnScreenStayTime = 0.3f;
			base.MeleeOnScreenStayTime = 0.7f;
			this._widget_0.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.SuggestedWidth = 150f;
			this._widget_0.HorizontalAlignment = HorizontalAlignment.Left;
			this._widget_0.Brush = base.Context.GetBrush("Mission.TakenDamage.Left");
			this._widget_1.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_1.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_1.SuggestedWidth = 150f;
			this._widget_1.HorizontalAlignment = HorizontalAlignment.Right;
			this._widget_1.Brush = base.Context.GetBrush("Mission.TakenDamage.Right");
			this._widget_2.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_2.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_2.SuggestedHeight = 150f;
			this._widget_2.VerticalAlignment = VerticalAlignment.Top;
			this._widget_2.MarginLeft = 150f;
			this._widget_2.MarginRight = 150f;
			this._widget_2.Brush = base.Context.GetBrush("Mission.TakenDamage.Top");
			this._widget_3.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_3.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_3.SuggestedHeight = 150f;
			this._widget_3.VerticalAlignment = VerticalAlignment.Bottom;
			this._widget_3.MarginLeft = 150f;
			this._widget_3.MarginRight = 150f;
			this._widget_3.Brush = base.Context.GetBrush("Mission.TakenDamage.Bottom");
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x000462F4 File Offset: 0x000444F4
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
				this._widget.PropertyChanged -= this.PropertyChangedListenerOf_widget;
				this._widget.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget;
				this._widget.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget;
				this._widget.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget;
				this._widget.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget;
				this._widget.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget;
				this._widget.intPropertyChanged -= this.intPropertyChangedListenerOf_widget;
				this._widget.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget;
				this._widget.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget;
				this._datasource_Root = null;
			}
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000464C8 File Offset: 0x000446C8
		public void SetDataSource(MissionAgentTakenDamageItemVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000464D1 File Offset: 0x000446D1
		private void EventListenerOf_widget(Widget widget, string commandName, object[] args)
		{
			if (commandName == "OnRemove")
			{
				this._datasource_Root.ExecuteRemove();
			}
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x000464EB File Offset: 0x000446EB
		private void PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x000464F4 File Offset: 0x000446F4
		private void boolPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x000464FD File Offset: 0x000446FD
		private void floatPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00046506 File Offset: 0x00044706
		private void Vec2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0004650F File Offset: 0x0004470F
		private void Vector2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00046518 File Offset: 0x00044718
		private void doublePropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00046521 File Offset: 0x00044721
		private void intPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0004652A File Offset: 0x0004472A
		private void uintPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00046533 File Offset: 0x00044733
		private void ColorPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0004653C File Offset: 0x0004473C
		private void HandleWidgetPropertyChangeOf_widget(string propertyName)
		{
			if (propertyName == "DamageAmount")
			{
				this._datasource_Root.Damage = this._widget.DamageAmount;
				return;
			}
			if (propertyName == "IsBehind")
			{
				this._datasource_Root.IsBehind = this._widget.IsBehind;
				return;
			}
			if (propertyName == "IsRanged")
			{
				this._datasource_Root.IsRanged = this._widget.IsRanged;
				return;
			}
			if (propertyName == "ScreenPosOfAffectorAgent")
			{
				this._datasource_Root.ScreenPosOfAffectorAgent = this._widget.ScreenPosOfAffectorAgent;
				return;
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x000465D9 File Offset: 0x000447D9
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x000465E7 File Offset: 0x000447E7
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x000465F5 File Offset: 0x000447F5
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00046603 File Offset: 0x00044803
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00046611 File Offset: 0x00044811
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0004661F File Offset: 0x0004481F
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0004662D File Offset: 0x0004482D
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0004663B File Offset: 0x0004483B
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00046649 File Offset: 0x00044849
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00046658 File Offset: 0x00044858
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "Damage")
			{
				this._widget.DamageAmount = this._datasource_Root.Damage;
				return;
			}
			if (propertyName == "IsBehind")
			{
				this._widget.IsBehind = this._datasource_Root.IsBehind;
				return;
			}
			if (propertyName == "IsRanged")
			{
				this._widget.IsRanged = this._datasource_Root.IsRanged;
				return;
			}
			if (propertyName == "ScreenPosOfAffectorAgent")
			{
				this._widget.ScreenPosOfAffectorAgent = this._datasource_Root.ScreenPosOfAffectorAgent;
				return;
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x000466F8 File Offset: 0x000448F8
		private void RefreshDataSource_datasource_Root(MissionAgentTakenDamageItemVM newDataSource)
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
				this._widget.PropertyChanged -= this.PropertyChangedListenerOf_widget;
				this._widget.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget;
				this._widget.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget;
				this._widget.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget;
				this._widget.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget;
				this._widget.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget;
				this._widget.intPropertyChanged -= this.intPropertyChangedListenerOf_widget;
				this._widget.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget;
				this._widget.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget;
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
				this._widget.DamageAmount = this._datasource_Root.Damage;
				this._widget.IsBehind = this._datasource_Root.IsBehind;
				this._widget.IsRanged = this._datasource_Root.IsRanged;
				this._widget.ScreenPosOfAffectorAgent = this._datasource_Root.ScreenPosOfAffectorAgent;
				this._widget.EventFire += this.EventListenerOf_widget;
				this._widget.PropertyChanged += this.PropertyChangedListenerOf_widget;
				this._widget.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget;
				this._widget.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget;
				this._widget.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget;
				this._widget.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget;
				this._widget.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget;
				this._widget.intPropertyChanged += this.intPropertyChangedListenerOf_widget;
				this._widget.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget;
				this._widget.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget;
			}
		}

		// Token: 0x04000211 RID: 529
		private TakenDamageItemBrushWidget _widget;

		// Token: 0x04000212 RID: 530
		private BrushWidget _widget_0;

		// Token: 0x04000213 RID: 531
		private BrushWidget _widget_1;

		// Token: 0x04000214 RID: 532
		private BrushWidget _widget_2;

		// Token: 0x04000215 RID: 533
		private BrushWidget _widget_3;

		// Token: 0x04000216 RID: 534
		private MissionAgentTakenDamageItemVM _datasource_Root;
	}
}

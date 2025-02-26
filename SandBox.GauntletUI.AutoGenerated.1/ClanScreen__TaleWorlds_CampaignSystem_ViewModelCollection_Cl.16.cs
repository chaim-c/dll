﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x0200004E RID: 78
	public class ClanScreen__TaleWorlds_CampaignSystem_ViewModelCollection_ClanManagement_ClanManagementVM_Dependency_15_ItemTemplate : ListPanel
	{
		// Token: 0x060019A6 RID: 6566 RVA: 0x000DD274 File Offset: 0x000DB474
		public ClanScreen__TaleWorlds_CampaignSystem_ViewModelCollection_ClanManagement_ClanManagementVM_Dependency_15_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x000DD280 File Offset: 0x000DB480
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new TextWidget(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_1 = new TextWidget(base.Context);
			this._widget.AddChild(this._widget_1);
			this._widget_2 = new TextWidget(base.Context);
			this._widget.AddChild(this._widget_2);
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x000DD2FA File Offset: 0x000DB4FA
		public void SetIds()
		{
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x000DD2FC File Offset: 0x000DB4FC
		public void SetAttributes()
		{
			base.DoNotAcceptEvents = true;
			base.DoNotPassEventsToChildren = true;
			base.WidthSizePolicy = SizePolicy.StretchToParent;
			base.HeightSizePolicy = SizePolicy.CoverChildren;
			base.SuggestedHeight = 47f;
			base.MarginTop = 5f;
			base.MarginBottom = 5f;
			base.StackLayout.LayoutMethod = LayoutMethod.VerticalBottomToTop;
			base.UpdateChildrenStates = true;
			this._widget_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0.HorizontalAlignment = HorizontalAlignment.Left;
			this._widget_0.Brush = base.Context.GetBrush("Clan.Role.Title.Text");
			this._widget_0.Brush.TextHorizontalAlignment = TextHorizontalAlignment.Left;
			this._widget_0.Brush.TextVerticalAlignment = TextVerticalAlignment.Center;
			this._widget_1.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_1.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_1.HorizontalAlignment = HorizontalAlignment.Left;
			this._widget_1.Brush = base.Context.GetBrush("Clan.Role.Effects.Text");
			this._widget_1.Brush.TextHorizontalAlignment = TextHorizontalAlignment.Left;
			this._widget_1.Brush.TextVerticalAlignment = TextVerticalAlignment.Center;
			this._widget_2.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_2.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_2.HorizontalAlignment = HorizontalAlignment.Left;
			this._widget_2.Brush = base.Context.GetBrush("Clan.Role.Effects.Text");
			this._widget_2.Brush.TextHorizontalAlignment = TextHorizontalAlignment.Left;
			this._widget_2.Brush.TextVerticalAlignment = TextVerticalAlignment.Center;
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x000DD47C File Offset: 0x000DB67C
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
				this._widget_2.PropertyChanged -= this.PropertyChangedListenerOf_widget_2;
				this._widget_2.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_2;
				this._widget_2.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_2;
				this._widget_2.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_2;
				this._widget_2.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_2;
				this._widget_2.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_2;
				this._widget_2.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_2;
				this._widget_2.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_2;
				this._widget_2.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_2;
				this._datasource_Root = null;
			}
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x000DD7D7 File Offset: 0x000DB9D7
		public void SetDataSource(ClanRoleItemVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x000DD7E0 File Offset: 0x000DB9E0
		private void PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x000DD7E9 File Offset: 0x000DB9E9
		private void boolPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x000DD7F2 File Offset: 0x000DB9F2
		private void floatPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x000DD7FB File Offset: 0x000DB9FB
		private void Vec2PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x000DD804 File Offset: 0x000DBA04
		private void Vector2PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x000DD80D File Offset: 0x000DBA0D
		private void doublePropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x000DD816 File Offset: 0x000DBA16
		private void intPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x000DD81F File Offset: 0x000DBA1F
		private void uintPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x000DD828 File Offset: 0x000DBA28
		private void ColorPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x000DD831 File Offset: 0x000DBA31
		private void HandleWidgetPropertyChangeOf_widget_0(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.Name = this._widget_0.Text;
				return;
			}
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x000DD857 File Offset: 0x000DBA57
		private void PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x000DD860 File Offset: 0x000DBA60
		private void boolPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x000DD869 File Offset: 0x000DBA69
		private void floatPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x000DD872 File Offset: 0x000DBA72
		private void Vec2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x000DD87B File Offset: 0x000DBA7B
		private void Vector2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x000DD884 File Offset: 0x000DBA84
		private void doublePropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x000DD88D File Offset: 0x000DBA8D
		private void intPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x000DD896 File Offset: 0x000DBA96
		private void uintPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x000DD89F File Offset: 0x000DBA9F
		private void ColorPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x000DD8A8 File Offset: 0x000DBAA8
		private void HandleWidgetPropertyChangeOf_widget_1(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.AssignedMemberEffects = this._widget_1.Text;
				return;
			}
			if (propertyName == "IsVisible")
			{
				this._datasource_Root.HasEffects = this._widget_1.IsVisible;
				return;
			}
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x000DD8FD File Offset: 0x000DBAFD
		private void PropertyChangedListenerOf_widget_2(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2(propertyName);
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x000DD906 File Offset: 0x000DBB06
		private void boolPropertyChangedListenerOf_widget_2(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2(propertyName);
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x000DD90F File Offset: 0x000DBB0F
		private void floatPropertyChangedListenerOf_widget_2(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2(propertyName);
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x000DD918 File Offset: 0x000DBB18
		private void Vec2PropertyChangedListenerOf_widget_2(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2(propertyName);
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x000DD921 File Offset: 0x000DBB21
		private void Vector2PropertyChangedListenerOf_widget_2(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2(propertyName);
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x000DD92A File Offset: 0x000DBB2A
		private void doublePropertyChangedListenerOf_widget_2(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2(propertyName);
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x000DD933 File Offset: 0x000DBB33
		private void intPropertyChangedListenerOf_widget_2(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2(propertyName);
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x000DD93C File Offset: 0x000DBB3C
		private void uintPropertyChangedListenerOf_widget_2(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2(propertyName);
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x000DD945 File Offset: 0x000DBB45
		private void ColorPropertyChangedListenerOf_widget_2(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2(propertyName);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x000DD950 File Offset: 0x000DBB50
		private void HandleWidgetPropertyChangeOf_widget_2(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.NoEffectText = this._widget_2.Text;
				return;
			}
			if (propertyName == "IsHidden")
			{
				this._datasource_Root.HasEffects = this._widget_2.IsHidden;
				return;
			}
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x000DD9A5 File Offset: 0x000DBBA5
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x000DD9B3 File Offset: 0x000DBBB3
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x000DD9C1 File Offset: 0x000DBBC1
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x000DD9CF File Offset: 0x000DBBCF
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x000DD9DD File Offset: 0x000DBBDD
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x000DD9EB File Offset: 0x000DBBEB
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x000DD9F9 File Offset: 0x000DBBF9
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x000DDA07 File Offset: 0x000DBC07
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x000DDA15 File Offset: 0x000DBC15
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x000DDA24 File Offset: 0x000DBC24
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "Name")
			{
				this._widget_0.Text = this._datasource_Root.Name;
				return;
			}
			if (propertyName == "AssignedMemberEffects")
			{
				this._widget_1.Text = this._datasource_Root.AssignedMemberEffects;
				return;
			}
			if (propertyName == "HasEffects")
			{
				this._widget_1.IsVisible = this._datasource_Root.HasEffects;
				this._widget_2.IsHidden = this._datasource_Root.HasEffects;
				return;
			}
			if (propertyName == "NoEffectText")
			{
				this._widget_2.Text = this._datasource_Root.NoEffectText;
				return;
			}
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x000DDAD8 File Offset: 0x000DBCD8
		private void RefreshDataSource_datasource_Root(ClanRoleItemVM newDataSource)
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
				this._widget_2.PropertyChanged -= this.PropertyChangedListenerOf_widget_2;
				this._widget_2.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_2;
				this._widget_2.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_2;
				this._widget_2.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_2;
				this._widget_2.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_2;
				this._widget_2.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_2;
				this._widget_2.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_2;
				this._widget_2.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_2;
				this._widget_2.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_2;
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
				this._widget_0.Text = this._datasource_Root.Name;
				this._widget_0.PropertyChanged += this.PropertyChangedListenerOf_widget_0;
				this._widget_0.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_0;
				this._widget_0.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_0;
				this._widget_0.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_0;
				this._widget_0.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_0;
				this._widget_0.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_0;
				this._widget_0.intPropertyChanged += this.intPropertyChangedListenerOf_widget_0;
				this._widget_0.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_0;
				this._widget_0.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_0;
				this._widget_1.Text = this._datasource_Root.AssignedMemberEffects;
				this._widget_1.IsVisible = this._datasource_Root.HasEffects;
				this._widget_1.PropertyChanged += this.PropertyChangedListenerOf_widget_1;
				this._widget_1.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_1;
				this._widget_1.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_1;
				this._widget_1.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_1;
				this._widget_1.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_1;
				this._widget_1.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_1;
				this._widget_1.intPropertyChanged += this.intPropertyChangedListenerOf_widget_1;
				this._widget_1.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_1;
				this._widget_1.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_1;
				this._widget_2.Text = this._datasource_Root.NoEffectText;
				this._widget_2.IsHidden = this._datasource_Root.HasEffects;
				this._widget_2.PropertyChanged += this.PropertyChangedListenerOf_widget_2;
				this._widget_2.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_2;
				this._widget_2.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_2;
				this._widget_2.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_2;
				this._widget_2.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_2;
				this._widget_2.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_2;
				this._widget_2.intPropertyChanged += this.intPropertyChangedListenerOf_widget_2;
				this._widget_2.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_2;
				this._widget_2.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_2;
			}
		}

		// Token: 0x040005D1 RID: 1489
		private ListPanel _widget;

		// Token: 0x040005D2 RID: 1490
		private TextWidget _widget_0;

		// Token: 0x040005D3 RID: 1491
		private TextWidget _widget_1;

		// Token: 0x040005D4 RID: 1492
		private TextWidget _widget_2;

		// Token: 0x040005D5 RID: 1493
		private ClanRoleItemVM _datasource_Root;
	}
}

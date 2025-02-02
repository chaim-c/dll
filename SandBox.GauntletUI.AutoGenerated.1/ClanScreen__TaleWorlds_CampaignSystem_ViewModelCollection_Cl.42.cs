﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement;
using TaleWorlds.Core;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x02000068 RID: 104
	public class ClanScreen__TaleWorlds_CampaignSystem_ViewModelCollection_ClanManagement_ClanManagementVM_Dependency_41_ClanPartyMemberItem__InheritedPrefab : ButtonWidget
	{
		// Token: 0x06001E6B RID: 7787 RVA: 0x000FC318 File Offset: 0x000FA518
		public ClanScreen__TaleWorlds_CampaignSystem_ViewModelCollection_ClanManagement_ClanManagementVM_Dependency_41_ClanPartyMemberItem__InheritedPrefab(UIContext context) : base(context)
		{
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x000FC321 File Offset: 0x000FA521
		public virtual void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new ImageIdentifierWidget(base.Context);
			this._widget.AddChild(this._widget_0);
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x000FC34C File Offset: 0x000FA54C
		public virtual void SetIds()
		{
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x000FC34E File Offset: 0x000FA54E
		public virtual void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.StretchToParent;
			base.HeightSizePolicy = SizePolicy.StretchToParent;
			base.DoNotPassEventsToChildren = true;
			this._widget_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.HeightSizePolicy = SizePolicy.StretchToParent;
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x000FC380 File Offset: 0x000FA580
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
				this._widget.EventFire -= this.EventListenerOf_widget;
				if (this._datasource_Root_Visual != null)
				{
					this._datasource_Root_Visual.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Visual;
					this._widget_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0;
					this._widget_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0;
					this._widget_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0;
					this._widget_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0;
					this._widget_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0;
					this._widget_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0;
					this._widget_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0;
					this._widget_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0;
					this._widget_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0;
					this._datasource_Root_Visual = null;
				}
				this._datasource_Root = null;
			}
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x000FC635 File Offset: 0x000FA835
		public virtual void SetDataSource(ClanPartyMemberItemVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x000FC640 File Offset: 0x000FA840
		private void EventListenerOf_widget(Widget widget, string commandName, object[] args)
		{
			if (commandName == "HoverBegin")
			{
				this._datasource_Root.ExecuteBeginHint();
			}
			if (commandName == "HoverEnd")
			{
				this._datasource_Root.ExecuteEndHint();
			}
			if (commandName == "Click")
			{
				this._datasource_Root.ExecuteLink();
			}
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x000FC695 File Offset: 0x000FA895
		private void PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x000FC69E File Offset: 0x000FA89E
		private void boolPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x000FC6A7 File Offset: 0x000FA8A7
		private void floatPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x000FC6B0 File Offset: 0x000FA8B0
		private void Vec2PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x000FC6B9 File Offset: 0x000FA8B9
		private void Vector2PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x000FC6C2 File Offset: 0x000FA8C2
		private void doublePropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x000FC6CB File Offset: 0x000FA8CB
		private void intPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x000FC6D4 File Offset: 0x000FA8D4
		private void uintPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x000FC6DD File Offset: 0x000FA8DD
		private void ColorPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x000FC6E6 File Offset: 0x000FA8E6
		private void HandleWidgetPropertyChangeOf_widget_0(string propertyName)
		{
			if (propertyName == "AdditionalArgs")
			{
				return;
			}
			if (propertyName == "ImageId")
			{
				return;
			}
			propertyName == "ImageTypeCode";
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x000FC710 File Offset: 0x000FA910
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x000FC71E File Offset: 0x000FA91E
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x000FC72C File Offset: 0x000FA92C
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x000FC73A File Offset: 0x000FA93A
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x000FC748 File Offset: 0x000FA948
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x000FC756 File Offset: 0x000FA956
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x000FC764 File Offset: 0x000FA964
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x000FC772 File Offset: 0x000FA972
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x000FC780 File Offset: 0x000FA980
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x000FC78E File Offset: 0x000FA98E
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "Visual")
			{
				this.RefreshDataSource_datasource_Root_Visual(this._datasource_Root.Visual);
				return;
			}
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x000FC7AF File Offset: 0x000FA9AF
		private void ViewModelPropertyChangedListenerOf_datasource_Root_Visual(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x000FC7BD File Offset: 0x000FA9BD
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x000FC7CB File Offset: 0x000FA9CB
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x000FC7D9 File Offset: 0x000FA9D9
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x000FC7E7 File Offset: 0x000FA9E7
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x000FC7F5 File Offset: 0x000FA9F5
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x000FC803 File Offset: 0x000FAA03
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x000FC811 File Offset: 0x000FAA11
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x000FC81F File Offset: 0x000FAA1F
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x000FC830 File Offset: 0x000FAA30
		private void HandleViewModelPropertyChangeOf_datasource_Root_Visual(string propertyName)
		{
			if (propertyName == "AdditionalArgs")
			{
				this._widget_0.AdditionalArgs = this._datasource_Root_Visual.AdditionalArgs;
				return;
			}
			if (propertyName == "Id")
			{
				this._widget_0.ImageId = this._datasource_Root_Visual.Id;
				return;
			}
			if (propertyName == "ImageTypeCode")
			{
				this._widget_0.ImageTypeCode = this._datasource_Root_Visual.ImageTypeCode;
				return;
			}
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x000FC8AC File Offset: 0x000FAAAC
		private void RefreshDataSource_datasource_Root(ClanPartyMemberItemVM newDataSource)
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
				if (this._datasource_Root_Visual != null)
				{
					this._datasource_Root_Visual.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Visual;
					this._widget_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0;
					this._widget_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0;
					this._widget_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0;
					this._widget_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0;
					this._widget_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0;
					this._widget_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0;
					this._widget_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0;
					this._widget_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0;
					this._widget_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0;
					this._datasource_Root_Visual = null;
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
				this._widget.EventFire += this.EventListenerOf_widget;
				this._datasource_Root_Visual = this._datasource_Root.Visual;
				if (this._datasource_Root_Visual != null)
				{
					this._datasource_Root_Visual.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Visual;
					this._widget_0.AdditionalArgs = this._datasource_Root_Visual.AdditionalArgs;
					this._widget_0.ImageId = this._datasource_Root_Visual.Id;
					this._widget_0.ImageTypeCode = this._datasource_Root_Visual.ImageTypeCode;
					this._widget_0.PropertyChanged += this.PropertyChangedListenerOf_widget_0;
					this._widget_0.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_0;
					this._widget_0.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_0;
					this._widget_0.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_0;
					this._widget_0.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_0;
					this._widget_0.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_0;
					this._widget_0.intPropertyChanged += this.intPropertyChangedListenerOf_widget_0;
					this._widget_0.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_0;
					this._widget_0.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_0;
				}
			}
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x000FCE58 File Offset: 0x000FB058
		private void RefreshDataSource_datasource_Root_Visual(ImageIdentifierVM newDataSource)
		{
			if (this._datasource_Root_Visual != null)
			{
				this._datasource_Root_Visual.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Visual;
				this._widget_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0;
				this._widget_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0;
				this._widget_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0;
				this._widget_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0;
				this._widget_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0;
				this._widget_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0;
				this._widget_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0;
				this._widget_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0;
				this._widget_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0;
				this._datasource_Root_Visual = null;
			}
			this._datasource_Root_Visual = newDataSource;
			this._datasource_Root_Visual = this._datasource_Root.Visual;
			if (this._datasource_Root_Visual != null)
			{
				this._datasource_Root_Visual.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Visual;
				this._widget_0.AdditionalArgs = this._datasource_Root_Visual.AdditionalArgs;
				this._widget_0.ImageId = this._datasource_Root_Visual.Id;
				this._widget_0.ImageTypeCode = this._datasource_Root_Visual.ImageTypeCode;
				this._widget_0.PropertyChanged += this.PropertyChangedListenerOf_widget_0;
				this._widget_0.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_0;
				this._widget_0.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_0;
				this._widget_0.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_0;
				this._widget_0.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_0;
				this._widget_0.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_0;
				this._widget_0.intPropertyChanged += this.intPropertyChangedListenerOf_widget_0;
				this._widget_0.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_0;
				this._widget_0.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_0;
			}
		}

		// Token: 0x040006BA RID: 1722
		private ButtonWidget _widget;

		// Token: 0x040006BB RID: 1723
		private ImageIdentifierWidget _widget_0;

		// Token: 0x040006BC RID: 1724
		private ClanPartyMemberItemVM _datasource_Root;

		// Token: 0x040006BD RID: 1725
		private ImageIdentifierVM _datasource_Root_Visual;
	}
}

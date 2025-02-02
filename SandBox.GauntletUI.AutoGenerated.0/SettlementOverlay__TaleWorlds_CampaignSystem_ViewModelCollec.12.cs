﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.CampaignSystem.ViewModelCollection.Quests;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Quest;

namespace SandBox.GauntletUI.AutoGenerated0
{
	// Token: 0x020000A3 RID: 163
	public class SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_11_ItemTemplate : QuestMarkerBrushWidget
	{
		// Token: 0x060027F6 RID: 10230 RVA: 0x0012CBD8 File Offset: 0x0012ADD8
		public SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_11_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x0012CBE1 File Offset: 0x0012ADE1
		public void CreateWidgets()
		{
			this._widget = this;
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x0012CBEA File Offset: 0x0012ADEA
		public void SetIds()
		{
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x0012CBEC File Offset: 0x0012ADEC
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.Fixed;
			base.HeightSizePolicy = SizePolicy.Fixed;
			base.SuggestedHeight = 31f;
			base.Brush = base.Context.GetBrush("GameMenu.QuestMarker");
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x0012CC20 File Offset: 0x0012AE20
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
				this._datasource_Root = null;
			}
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x0012CDDD File Offset: 0x0012AFDD
		public void SetDataSource(QuestMarkerVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x0012CDE6 File Offset: 0x0012AFE6
		private void PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x0012CDEF File Offset: 0x0012AFEF
		private void boolPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x0012CDF8 File Offset: 0x0012AFF8
		private void floatPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x0012CE01 File Offset: 0x0012B001
		private void Vec2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x0012CE0A File Offset: 0x0012B00A
		private void Vector2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x0012CE13 File Offset: 0x0012B013
		private void doublePropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x0012CE1C File Offset: 0x0012B01C
		private void intPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x0012CE25 File Offset: 0x0012B025
		private void uintPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x0012CE2E File Offset: 0x0012B02E
		private void ColorPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x0012CE37 File Offset: 0x0012B037
		private void HandleWidgetPropertyChangeOf_widget(string propertyName)
		{
			if (propertyName == "QuestMarkerType")
			{
				this._datasource_Root.QuestMarkerType = this._widget.QuestMarkerType;
				return;
			}
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x0012CE5D File Offset: 0x0012B05D
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x0012CE6B File Offset: 0x0012B06B
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x0012CE79 File Offset: 0x0012B079
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x0012CE87 File Offset: 0x0012B087
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x0012CE95 File Offset: 0x0012B095
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x0012CEA3 File Offset: 0x0012B0A3
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x0012CEB1 File Offset: 0x0012B0B1
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x0012CEBF File Offset: 0x0012B0BF
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x0012CECD File Offset: 0x0012B0CD
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x0012CEDB File Offset: 0x0012B0DB
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "QuestMarkerType")
			{
				this._widget.QuestMarkerType = this._datasource_Root.QuestMarkerType;
				return;
			}
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x0012CF04 File Offset: 0x0012B104
		private void RefreshDataSource_datasource_Root(QuestMarkerVM newDataSource)
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
				this._widget.QuestMarkerType = this._datasource_Root.QuestMarkerType;
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

		// Token: 0x04000804 RID: 2052
		private QuestMarkerBrushWidget _widget;

		// Token: 0x04000805 RID: 2053
		private QuestMarkerVM _datasource_Root;
	}
}

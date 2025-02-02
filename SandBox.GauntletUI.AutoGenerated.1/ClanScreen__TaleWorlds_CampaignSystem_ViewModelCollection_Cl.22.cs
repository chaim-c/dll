﻿using System;
using System.ComponentModel;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Categories;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x02000054 RID: 84
	public class ClanScreen__TaleWorlds_CampaignSystem_ViewModelCollection_ClanManagement_ClanManagementVM_Dependency_21_Standard_VerticalScrollbar__DependendPrefab : Widget
	{
		// Token: 0x06001AA5 RID: 6821 RVA: 0x000E4040 File Offset: 0x000E2240
		public ClanScreen__TaleWorlds_CampaignSystem_ViewModelCollection_ClanManagement_ClanManagementVM_Dependency_21_Standard_VerticalScrollbar__DependendPrefab(UIContext context) : base(context)
		{
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x000E404C File Offset: 0x000E224C
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new Widget(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_1 = new Widget(base.Context);
			this._widget.AddChild(this._widget_1);
			this._widget_2 = new Widget(base.Context);
			this._widget.AddChild(this._widget_2);
			this._widget_3 = new ScrollbarWidget(base.Context);
			this._widget.AddChild(this._widget_3);
			this._widget_3_0 = new ImageWidget(base.Context);
			this._widget_3.AddChild(this._widget_3_0);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x000E410A File Offset: 0x000E230A
		public void SetIds()
		{
			this._widget_2.Id = "ScrollbarBlocker";
			this._widget_3.Id = "Scrollbar";
			this._widget_3_0.Id = "ScrollbarHandle";
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x000E413C File Offset: 0x000E233C
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.Fixed;
			base.HeightSizePolicy = SizePolicy.Fixed;
			base.SuggestedWidth = 22f;
			base.SuggestedHeight = 800f;
			this._widget_0.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_0.SuggestedWidth = 18f;
			this._widget_0.SuggestedHeight = 13f;
			this._widget_0.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_0.VerticalAlignment = VerticalAlignment.Top;
			this._widget_0.Sprite = base.Context.SpriteData.GetSprite("General\\Scrollbar.Vertical1\\scroller_stop_top");
			this._widget_0.ExtendTop = 22f;
			this._widget_0.ExtendBottom = 22f;
			this._widget_0.ExtendLeft = 22f;
			this._widget_0.ExtendRight = 22f;
			this._widget_0.IsDisabled = true;
			this._widget_1.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_1.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_1.SuggestedWidth = 18f;
			this._widget_1.SuggestedHeight = 13f;
			this._widget_1.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_1.VerticalAlignment = VerticalAlignment.Bottom;
			this._widget_1.Sprite = base.Context.SpriteData.GetSprite("General\\Scrollbar.Vertical1\\scroller_stop_bottom");
			this._widget_1.ExtendTop = 22f;
			this._widget_1.ExtendBottom = 22f;
			this._widget_1.ExtendLeft = 22f;
			this._widget_1.ExtendRight = 22f;
			this._widget_1.IsDisabled = true;
			this._widget_2.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_2.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_2.SuggestedWidth = 22f;
			this._widget_2.SuggestedHeight = 800f;
			this._widget_2.HorizontalAlignment = HorizontalAlignment.Left;
			this._widget_2.VerticalAlignment = VerticalAlignment.Bottom;
			this._widget_2.MarginTop = 13f;
			this._widget_2.MarginBottom = 13f;
			this._widget_2.Sprite = base.Context.SpriteData.GetSprite("General\\Scrollbar.Vertical1\\scroller_bed_noscroll");
			this._widget_2.ExtendLeft = 20f;
			this._widget_2.ExtendRight = 20f;
			this._widget_2.ExtendTop = 20f;
			this._widget_2.ExtendBottom = 20f;
			this._widget_3.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_3.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_3.SuggestedWidth = 22f;
			this._widget_3.SuggestedHeight = 800f;
			this._widget_3.HorizontalAlignment = HorizontalAlignment.Left;
			this._widget_3.VerticalAlignment = VerticalAlignment.Bottom;
			this._widget_3.MarginTop = 13f;
			this._widget_3.MarginBottom = 13f;
			this._widget_3.Brush = base.Context.GetBrush("Scrollbar.Vertical");
			this._widget_3.AlignmentAxis = AlignmentAxis.Vertical;
			this._widget_3.Handle = this._widget_3_0;
			this._widget_3.MaxValue = 100f;
			this._widget_3.MinValue = 0f;
			this._widget_3.UpdateChildrenStates = true;
			this._widget_3_0.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_3_0.SuggestedWidth = 20f;
			this._widget_3_0.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_3_0.VerticalAlignment = VerticalAlignment.Top;
			this._widget_3_0.Brush = base.Context.GetBrush("Scrollbar.Vertical.Handle");
			this._widget_3_0.MinHeight = 50f;
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x000E44E8 File Offset: 0x000E26E8
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
				this._datasource_Root = null;
			}
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x000E45D6 File Offset: 0x000E27D6
		public void SetDataSource(ClanPartiesVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x000E45DF File Offset: 0x000E27DF
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x000E45ED File Offset: 0x000E27ED
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x000E45FB File Offset: 0x000E27FB
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x000E4609 File Offset: 0x000E2809
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x000E4617 File Offset: 0x000E2817
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x000E4625 File Offset: 0x000E2825
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x000E4633 File Offset: 0x000E2833
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x000E4641 File Offset: 0x000E2841
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x000E464F File Offset: 0x000E284F
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x000E465D File Offset: 0x000E285D
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x000E4660 File Offset: 0x000E2860
		private void RefreshDataSource_datasource_Root(ClanPartiesVM newDataSource)
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
			}
		}

		// Token: 0x040005FC RID: 1532
		private Widget _widget;

		// Token: 0x040005FD RID: 1533
		private Widget _widget_0;

		// Token: 0x040005FE RID: 1534
		private Widget _widget_1;

		// Token: 0x040005FF RID: 1535
		private Widget _widget_2;

		// Token: 0x04000600 RID: 1536
		private ScrollbarWidget _widget_3;

		// Token: 0x04000601 RID: 1537
		private ImageWidget _widget_3_0;

		// Token: 0x04000602 RID: 1538
		private ClanPartiesVM _datasource_Root;
	}
}

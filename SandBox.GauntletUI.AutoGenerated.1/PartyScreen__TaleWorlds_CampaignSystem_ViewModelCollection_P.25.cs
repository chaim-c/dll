﻿using System;
using System.ComponentModel;
using TaleWorlds.CampaignSystem.ViewModelCollection.Party.PartyTroopManagerPopUp;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x0200015A RID: 346
	public class PartyScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Party_PartyVM_Dependency_24_Standard_VerticalScrollbar__DependendPrefab : Widget
	{
		// Token: 0x060068A1 RID: 26785 RVA: 0x0033FDA2 File Offset: 0x0033DFA2
		public PartyScreen__TaleWorlds_CampaignSystem_ViewModelCollection_Party_PartyVM_Dependency_24_Standard_VerticalScrollbar__DependendPrefab(UIContext context) : base(context)
		{
		}

		// Token: 0x060068A2 RID: 26786 RVA: 0x0033FDAC File Offset: 0x0033DFAC
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

		// Token: 0x060068A3 RID: 26787 RVA: 0x0033FE6A File Offset: 0x0033E06A
		public void SetIds()
		{
			this._widget_2.Id = "ScrollbarBlocker";
			this._widget_3.Id = "Scrollbar";
			this._widget_3_0.Id = "ScrollbarHandle";
		}

		// Token: 0x060068A4 RID: 26788 RVA: 0x0033FE9C File Offset: 0x0033E09C
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

		// Token: 0x060068A5 RID: 26789 RVA: 0x00340248 File Offset: 0x0033E448
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

		// Token: 0x060068A6 RID: 26790 RVA: 0x00340336 File Offset: 0x0033E536
		public void SetDataSource(PartyRecruitTroopVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x060068A7 RID: 26791 RVA: 0x0034033F File Offset: 0x0033E53F
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060068A8 RID: 26792 RVA: 0x0034034D File Offset: 0x0033E54D
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060068A9 RID: 26793 RVA: 0x0034035B File Offset: 0x0033E55B
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060068AA RID: 26794 RVA: 0x00340369 File Offset: 0x0033E569
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060068AB RID: 26795 RVA: 0x00340377 File Offset: 0x0033E577
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060068AC RID: 26796 RVA: 0x00340385 File Offset: 0x0033E585
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060068AD RID: 26797 RVA: 0x00340393 File Offset: 0x0033E593
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060068AE RID: 26798 RVA: 0x003403A1 File Offset: 0x0033E5A1
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060068AF RID: 26799 RVA: 0x003403AF File Offset: 0x0033E5AF
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060068B0 RID: 26800 RVA: 0x003403BD File Offset: 0x0033E5BD
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
		}

		// Token: 0x060068B1 RID: 26801 RVA: 0x003403C0 File Offset: 0x0033E5C0
		private void RefreshDataSource_datasource_Root(PartyRecruitTroopVM newDataSource)
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

		// Token: 0x04001517 RID: 5399
		private Widget _widget;

		// Token: 0x04001518 RID: 5400
		private Widget _widget_0;

		// Token: 0x04001519 RID: 5401
		private Widget _widget_1;

		// Token: 0x0400151A RID: 5402
		private Widget _widget_2;

		// Token: 0x0400151B RID: 5403
		private ScrollbarWidget _widget_3;

		// Token: 0x0400151C RID: 5404
		private ImageWidget _widget_3_0;

		// Token: 0x0400151D RID: 5405
		private PartyRecruitTroopVM _datasource_Root;
	}
}

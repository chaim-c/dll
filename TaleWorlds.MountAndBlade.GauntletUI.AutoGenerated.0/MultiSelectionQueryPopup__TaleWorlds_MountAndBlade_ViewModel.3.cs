﻿using System;
using System.ComponentModel;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ViewModelCollection.Inquiries;

namespace TaleWorlds.MountAndBlade.GauntletUI.AutoGenerated0
{
	// Token: 0x02000011 RID: 17
	public class MultiSelectionQueryPopup__TaleWorlds_MountAndBlade_ViewModelCollection_Inquiries_MultiSelectionQueryPopUpVM_Dependency_2_Standard_VerticalScrollbar__DependendPrefab : Widget
	{
		// Token: 0x06000376 RID: 886 RVA: 0x00019038 File Offset: 0x00017238
		public MultiSelectionQueryPopup__TaleWorlds_MountAndBlade_ViewModelCollection_Inquiries_MultiSelectionQueryPopUpVM_Dependency_2_Standard_VerticalScrollbar__DependendPrefab(UIContext context) : base(context)
		{
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00019044 File Offset: 0x00017244
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

		// Token: 0x06000378 RID: 888 RVA: 0x00019102 File Offset: 0x00017302
		public void SetIds()
		{
			this._widget_2.Id = "ScrollbarBlocker";
			this._widget_3.Id = "Scrollbar";
			this._widget_3_0.Id = "ScrollbarHandle";
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00019134 File Offset: 0x00017334
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

		// Token: 0x0600037A RID: 890 RVA: 0x000194E0 File Offset: 0x000176E0
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

		// Token: 0x0600037B RID: 891 RVA: 0x000195CE File Offset: 0x000177CE
		public void SetDataSource(MultiSelectionQueryPopUpVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000195D7 File Offset: 0x000177D7
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000195E5 File Offset: 0x000177E5
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000195F3 File Offset: 0x000177F3
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00019601 File Offset: 0x00017801
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001960F File Offset: 0x0001780F
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001961D File Offset: 0x0001781D
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001962B File Offset: 0x0001782B
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00019639 File Offset: 0x00017839
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00019647 File Offset: 0x00017847
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00019655 File Offset: 0x00017855
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00019658 File Offset: 0x00017858
		private void RefreshDataSource_datasource_Root(MultiSelectionQueryPopUpVM newDataSource)
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

		// Token: 0x0400008E RID: 142
		private Widget _widget;

		// Token: 0x0400008F RID: 143
		private Widget _widget_0;

		// Token: 0x04000090 RID: 144
		private Widget _widget_1;

		// Token: 0x04000091 RID: 145
		private Widget _widget_2;

		// Token: 0x04000092 RID: 146
		private ScrollbarWidget _widget_3;

		// Token: 0x04000093 RID: 147
		private ImageWidget _widget_3_0;

		// Token: 0x04000094 RID: 148
		private MultiSelectionQueryPopUpVM _datasource_Root;
	}
}

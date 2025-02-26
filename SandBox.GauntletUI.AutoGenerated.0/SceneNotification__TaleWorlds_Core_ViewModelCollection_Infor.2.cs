﻿using System;
using System.ComponentModel;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;

namespace SandBox.GauntletUI.AutoGenerated0
{
	// Token: 0x0200008C RID: 140
	public class SceneNotification__TaleWorlds_Core_ViewModelCollection_Information_SceneNotificationVM_Dependency_1_Standard_CircleLoadingWidget__DependendPrefab : Widget
	{
		// Token: 0x0600216D RID: 8557 RVA: 0x000FC0FA File Offset: 0x000FA2FA
		public SceneNotification__TaleWorlds_Core_ViewModelCollection_Information_SceneNotificationVM_Dependency_1_Standard_CircleLoadingWidget__DependendPrefab(UIContext context) : base(context)
		{
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x000FC104 File Offset: 0x000FA304
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new CircleLoadingAnimWidget(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_0_0 = new Widget(base.Context);
			this._widget_0.AddChild(this._widget_0_0);
			this._widget_0_1 = new Widget(base.Context);
			this._widget_0.AddChild(this._widget_0_1);
			this._widget_0_2 = new Widget(base.Context);
			this._widget_0.AddChild(this._widget_0_2);
			this._widget_0_3 = new Widget(base.Context);
			this._widget_0.AddChild(this._widget_0_3);
			this._widget_0_4 = new Widget(base.Context);
			this._widget_0.AddChild(this._widget_0_4);
			this._widget_0_5 = new Widget(base.Context);
			this._widget_0.AddChild(this._widget_0_5);
			this._widget_0_6 = new Widget(base.Context);
			this._widget_0.AddChild(this._widget_0_6);
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x000FC228 File Offset: 0x000FA428
		public void SetIds()
		{
			base.Id = "StandardCircleParent";
			this._widget_0.Id = "CircleLoadingWidget";
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x000FC248 File Offset: 0x000FA448
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.CoverChildren;
			base.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_0.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0.CircleRadius = 16f;
			this._widget_0.NumOfCirclesInASecond = 0.75f;
			this._widget_0.StaySeconds = -1f;
			this._widget_0_0.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0_0.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_0_0.SuggestedWidth = 12f;
			this._widget_0_0.SuggestedHeight = 12f;
			this._widget_0_0.Sprite = base.Context.SpriteData.GetSprite("MapMenuUnread@2x");
			this._widget_0_0.AlphaFactor = 0.6f;
			this._widget_0_0.Color = new Color(0.9490197f, 0.6862745f, 0.282353f, 1f);
			this._widget_0_1.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0_1.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_0_1.SuggestedWidth = 12f;
			this._widget_0_1.SuggestedHeight = 12f;
			this._widget_0_1.Sprite = base.Context.SpriteData.GetSprite("MapMenuUnread@2x");
			this._widget_0_1.AlphaFactor = 0.6f;
			this._widget_0_2.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0_2.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_0_2.SuggestedWidth = 12f;
			this._widget_0_2.SuggestedHeight = 12f;
			this._widget_0_2.Sprite = base.Context.SpriteData.GetSprite("MapMenuUnread@2x");
			this._widget_0_2.AlphaFactor = 0.6f;
			this._widget_0_3.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0_3.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_0_3.SuggestedWidth = 12f;
			this._widget_0_3.SuggestedHeight = 12f;
			this._widget_0_3.Sprite = base.Context.SpriteData.GetSprite("MapMenuUnread@2x");
			this._widget_0_3.AlphaFactor = 0.6f;
			this._widget_0_4.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0_4.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_0_4.SuggestedWidth = 12f;
			this._widget_0_4.SuggestedHeight = 12f;
			this._widget_0_4.Sprite = base.Context.SpriteData.GetSprite("MapMenuUnread@2x");
			this._widget_0_4.AlphaFactor = 0.6f;
			this._widget_0_5.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0_5.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_0_5.SuggestedWidth = 12f;
			this._widget_0_5.SuggestedHeight = 12f;
			this._widget_0_5.Sprite = base.Context.SpriteData.GetSprite("MapMenuUnread@2x");
			this._widget_0_5.AlphaFactor = 0.6f;
			this._widget_0_6.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0_6.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_0_6.SuggestedWidth = 12f;
			this._widget_0_6.SuggestedHeight = 12f;
			this._widget_0_6.Sprite = base.Context.SpriteData.GetSprite("MapMenuUnread@2x");
			this._widget_0_6.AlphaFactor = 0.6f;
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x000FC5A8 File Offset: 0x000FA7A8
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

		// Token: 0x06002172 RID: 8562 RVA: 0x000FC696 File Offset: 0x000FA896
		public void SetDataSource(SceneNotificationVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x000FC69F File Offset: 0x000FA89F
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x000FC6AD File Offset: 0x000FA8AD
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x000FC6BB File Offset: 0x000FA8BB
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x000FC6C9 File Offset: 0x000FA8C9
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x000FC6D7 File Offset: 0x000FA8D7
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x000FC6E5 File Offset: 0x000FA8E5
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x000FC6F3 File Offset: 0x000FA8F3
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x000FC701 File Offset: 0x000FA901
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x000FC70F File Offset: 0x000FA90F
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x000FC71D File Offset: 0x000FA91D
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x000FC720 File Offset: 0x000FA920
		private void RefreshDataSource_datasource_Root(SceneNotificationVM newDataSource)
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

		// Token: 0x040006DA RID: 1754
		private Widget _widget;

		// Token: 0x040006DB RID: 1755
		private CircleLoadingAnimWidget _widget_0;

		// Token: 0x040006DC RID: 1756
		private Widget _widget_0_0;

		// Token: 0x040006DD RID: 1757
		private Widget _widget_0_1;

		// Token: 0x040006DE RID: 1758
		private Widget _widget_0_2;

		// Token: 0x040006DF RID: 1759
		private Widget _widget_0_3;

		// Token: 0x040006E0 RID: 1760
		private Widget _widget_0_4;

		// Token: 0x040006E1 RID: 1761
		private Widget _widget_0_5;

		// Token: 0x040006E2 RID: 1762
		private Widget _widget_0_6;

		// Token: 0x040006E3 RID: 1763
		private SceneNotificationVM _datasource_Root;
	}
}

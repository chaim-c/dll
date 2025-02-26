﻿using System;
using System.ComponentModel;
using SandBox.ViewModelCollection.GameOver;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;

namespace SandBox.GauntletUI.AutoGenerated0
{
	// Token: 0x0200007A RID: 122
	public class GameOverScreen__SandBox_ViewModelCollection_GameOver_GameOverVM_Dependency_3_Standard_Background__DependendPrefab : ScreenBackgroundBrushWidget
	{
		// Token: 0x06001B67 RID: 7015 RVA: 0x000C96F3 File Offset: 0x000C78F3
		public GameOverScreen__SandBox_ViewModelCollection_GameOver_GameOverVM_Dependency_3_Standard_Background__DependendPrefab(UIContext context) : base(context)
		{
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x000C96FC File Offset: 0x000C78FC
		public void AddChildToLogicalLocation(Widget widget)
		{
			this._widget_0.AddChild(widget);
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x000C970C File Offset: 0x000C790C
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new Widget(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_1 = new Widget(base.Context);
			this._widget.AddChild(this._widget_1);
			this._widget_2 = new Widget(base.Context);
			this._widget.AddChild(this._widget_2);
			this._widget_3 = new Widget(base.Context);
			this._widget.AddChild(this._widget_3);
			this._widget_4 = new Widget(base.Context);
			this._widget.AddChild(this._widget_4);
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x000C97CC File Offset: 0x000C79CC
		public void SetIds()
		{
			this._widget_1.Id = "SmokeWidget1";
			this._widget_2.Id = "SmokeWidget2";
			this._widget_3.Id = "ParticleWidget1";
			this._widget_4.Id = "ParticleWidget2";
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x000C981C File Offset: 0x000C7A1C
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.StretchToParent;
			base.HeightSizePolicy = SizePolicy.StretchToParent;
			base.ParticleWidget1 = this._widget_3;
			base.ParticleWidget2 = this._widget_4;
			base.IsParticleVisible = true;
			base.IsSmokeVisible = true;
			base.SmokeWidget1 = this._widget_1;
			base.SmokeWidget2 = this._widget_2;
			base.AnimEnabled = true;
			base.SmokeSpeedModifier = 5f;
			base.ParticleSpeedModifier = 3f;
			base.IsFullscreenImageEnabled = true;
			base.Brush = base.Context.GetBrush("Standard.Background.Brush");
			base.DoNotUseCustomScaleAndChildren = true;
			this._widget_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_1.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_1.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_1.SuggestedWidth = 1920f;
			this._widget_1.SuggestedHeight = 700f;
			this._widget_1.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_1.VerticalAlignment = VerticalAlignment.Bottom;
			this._widget_1.Sprite = base.Context.SpriteData.GetSprite("fog_smoke");
			this._widget_1.ColorFactor = 1.1f;
			this._widget_2.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_2.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_2.SuggestedWidth = 1920f;
			this._widget_2.SuggestedHeight = 700f;
			this._widget_2.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_2.VerticalAlignment = VerticalAlignment.Bottom;
			this._widget_2.Sprite = base.Context.SpriteData.GetSprite("fog_smoke");
			this._widget_2.ColorFactor = 1.1f;
			this._widget_3.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_3.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_3.SuggestedWidth = 1920f;
			this._widget_3.SuggestedHeight = 1080f;
			this._widget_3.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_3.VerticalAlignment = VerticalAlignment.Center;
			this._widget_3.Sprite = base.Context.SpriteData.GetSprite("fog_particles");
			this._widget_3.AlphaFactor = 0.8f;
			this._widget_4.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_4.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_4.SuggestedWidth = 1920f;
			this._widget_4.SuggestedHeight = 1080f;
			this._widget_4.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_4.VerticalAlignment = VerticalAlignment.Center;
			this._widget_4.Sprite = base.Context.SpriteData.GetSprite("fog_particles");
			this._widget_4.AlphaFactor = 0.8f;
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x000C9AD0 File Offset: 0x000C7CD0
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

		// Token: 0x06001B6D RID: 7021 RVA: 0x000C9BBE File Offset: 0x000C7DBE
		public void SetDataSource(GameOverVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x000C9BC7 File Offset: 0x000C7DC7
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x000C9BD5 File Offset: 0x000C7DD5
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x000C9BE3 File Offset: 0x000C7DE3
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x000C9BF1 File Offset: 0x000C7DF1
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x000C9BFF File Offset: 0x000C7DFF
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x000C9C0D File Offset: 0x000C7E0D
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x000C9C1B File Offset: 0x000C7E1B
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x000C9C29 File Offset: 0x000C7E29
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x000C9C37 File Offset: 0x000C7E37
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x000C9C45 File Offset: 0x000C7E45
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x000C9C48 File Offset: 0x000C7E48
		private void RefreshDataSource_datasource_Root(GameOverVM newDataSource)
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

		// Token: 0x040005C2 RID: 1474
		private ScreenBackgroundBrushWidget _widget;

		// Token: 0x040005C3 RID: 1475
		private Widget _widget_0;

		// Token: 0x040005C4 RID: 1476
		private Widget _widget_1;

		// Token: 0x040005C5 RID: 1477
		private Widget _widget_2;

		// Token: 0x040005C6 RID: 1478
		private Widget _widget_3;

		// Token: 0x040005C7 RID: 1479
		private Widget _widget_4;

		// Token: 0x040005C8 RID: 1480
		private GameOverVM _datasource_Root;
	}
}

using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.HUD
{
	// Token: 0x020000BF RID: 191
	public class MoraleWidget : Widget
	{
		// Token: 0x060009F8 RID: 2552 RVA: 0x0001C576 File Offset: 0x0001A776
		public MoraleWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0001C57F File Offset: 0x0001A77F
		protected override void OnConnectedToRoot()
		{
			base.OnConnectedToRoot();
			this._moraleItemWidgets = this.CreateItemWidgets(this.ItemContainer);
			this.SetItemWidgetColors(this._teamColor);
			this.SetItemGlowWidgetColors(this._teamColorSecondary);
			this.RestartAnimations();
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0001C5B8 File Offset: 0x0001A7B8
		protected override void OnUpdate(float dt)
		{
			if (this._triggerAnimations)
			{
				if (this._animWaitFrame >= 1)
				{
					this.HandleAnimation();
					this._triggerAnimations = false;
					this._animWaitFrame = 0;
				}
				else
				{
					this._animWaitFrame++;
				}
			}
			if (!this._initialized)
			{
				this.FlowArrowWidget.LeftSideArrow = this.ExtendToLeft;
				this._initialized = true;
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0001C61A File Offset: 0x0001A81A
		private void RestartAnimations()
		{
			this._triggerAnimations = true;
			this._animWaitFrame = 0;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0001C62A File Offset: 0x0001A82A
		private void UpdateArrows(int flowLevel)
		{
			if (this.Container == null || this.FlowArrowWidget == null)
			{
				return;
			}
			this.FlowArrowWidget.SetFlowLevel(flowLevel);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0001C64C File Offset: 0x0001A84C
		private void UpdateMoraleMask()
		{
			int num = MathF.Floor((float)this.MoralePercentage / 100f * 10f);
			for (int i = 0; i < this._moraleItemWidgets.Length; i++)
			{
				MoraleWidget.MoraleItemWidget moraleItemWidget = this._moraleItemWidgets[i];
				float num2 = 0f;
				if (i < num)
				{
					num2 = 1f;
					if (!moraleItemWidget.ItemGlowWidget.IsVisible)
					{
						this.RestartAnimations();
					}
				}
				else if (i == num)
				{
					float num3 = 10f;
					num2 = ((float)this.MoralePercentage - (float)num * num3) / num3;
					if (!moraleItemWidget.ItemWidget.IsVisible && !MBMath.ApproximatelyEquals(num2, 0f, 1E-05f))
					{
						this.RestartAnimations();
					}
				}
				moraleItemWidget.SetFillAmount(num2, 12);
			}
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0001C700 File Offset: 0x0001A900
		private string GetCurrentStateName()
		{
			string result;
			if (this.MoralePercentage < 20)
			{
				result = "IsCriticalAnim";
			}
			else if (this.IncreaseLevel > 0)
			{
				result = "IncreaseAnim";
			}
			else
			{
				result = "Default";
			}
			return result;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0001C738 File Offset: 0x0001A938
		private void HandleAnimation()
		{
			for (int i = 0; i < this._moraleItemWidgets.Length; i++)
			{
				MoraleWidget.MoraleItemWidget moraleItemWidget = this._moraleItemWidgets[i];
				moraleItemWidget.ItemWidget.SetState(this._currentStateName);
				moraleItemWidget.ItemWidget.BrushRenderer.RestartAnimation();
				moraleItemWidget.ItemGlowWidget.SetState(this._currentStateName);
				moraleItemWidget.ItemGlowWidget.BrushRenderer.RestartAnimation();
			}
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0001C7A4 File Offset: 0x0001A9A4
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			this.UpdateMoraleMask();
			string currentStateName = this.GetCurrentStateName();
			if (this._currentStateName != currentStateName)
			{
				this._currentStateName = currentStateName;
				this.RestartAnimations();
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0001C7E0 File Offset: 0x0001A9E0
		private MoraleWidget.MoraleItemWidget[] CreateItemWidgets(Widget containerWidget)
		{
			MoraleWidget.MoraleItemWidget[] array = new MoraleWidget.MoraleItemWidget[10];
			for (int i = 0; i < 10; i++)
			{
				Widget widget = new Widget(base.Context);
				widget.UpdateChildrenStates = true;
				widget.WidthSizePolicy = SizePolicy.Fixed;
				widget.HeightSizePolicy = SizePolicy.Fixed;
				widget.SuggestedWidth = 39f;
				widget.SuggestedHeight = 38f;
				if (this.ExtendToLeft)
				{
					widget.HorizontalAlignment = HorizontalAlignment.Right;
					widget.MarginRight = (float)i * 28f;
				}
				else
				{
					widget.HorizontalAlignment = HorizontalAlignment.Left;
					widget.MarginLeft = (float)i * 28f;
				}
				widget.AddState("IncreaseAnim");
				widget.AddState("IsCriticalAnim");
				containerWidget.AddChild(widget);
				Widget widget2 = new Widget(base.Context);
				widget2.ClipContents = true;
				widget2.UpdateChildrenStates = true;
				widget2.WidthSizePolicy = SizePolicy.StretchToParent;
				widget2.HeightSizePolicy = SizePolicy.Fixed;
				widget2.VerticalAlignment = VerticalAlignment.Bottom;
				widget.AddChild(widget2);
				BrushWidget brushWidget = new BrushWidget(base.Context);
				brushWidget.WidthSizePolicy = SizePolicy.Fixed;
				brushWidget.HeightSizePolicy = SizePolicy.Fixed;
				brushWidget.VerticalAlignment = VerticalAlignment.Bottom;
				brushWidget.Brush = this.ItemGlowBrush;
				brushWidget.SuggestedWidth = 39f;
				brushWidget.SuggestedHeight = 38f;
				brushWidget.AddState("IncreaseAnim");
				brushWidget.AddState("IsCriticalAnim");
				widget2.AddChild(brushWidget);
				BrushWidget brushWidget2 = new BrushWidget(base.Context);
				brushWidget2.WidthSizePolicy = SizePolicy.StretchToParent;
				brushWidget2.HeightSizePolicy = SizePolicy.StretchToParent;
				brushWidget2.Brush = this.ItemBackgroundBrush;
				widget.AddChild(brushWidget2);
				BrushWidget brushWidget3 = new BrushWidget(base.Context);
				brushWidget3.WidthSizePolicy = SizePolicy.Fixed;
				brushWidget3.HeightSizePolicy = SizePolicy.Fixed;
				brushWidget3.VerticalAlignment = VerticalAlignment.Bottom;
				brushWidget3.Brush = this.ItemBrush;
				brushWidget3.SuggestedWidth = 39f;
				brushWidget3.SuggestedHeight = 38f;
				brushWidget3.AddState("IncreaseAnim");
				brushWidget3.AddState("IsCriticalAnim");
				widget2.AddChild(brushWidget3);
				array[i] = new MoraleWidget.MoraleItemWidget(widget, widget2, brushWidget3, brushWidget, brushWidget2);
			}
			return array;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0001C9E0 File Offset: 0x0001ABE0
		private void SetItemWidgetColors(Color color)
		{
			if (this._moraleItemWidgets != null)
			{
				foreach (MoraleWidget.MoraleItemWidget widget in this._moraleItemWidgets)
				{
					this.SetSingleItemWidgetColor(widget, color);
				}
			}
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0001CA18 File Offset: 0x0001AC18
		private void SetSingleItemWidgetColor(MoraleWidget.MoraleItemWidget widget, Color color)
		{
			widget.ItemWidget.Brush.Color = color;
			foreach (Style style in widget.ItemWidget.Brush.Styles)
			{
				StyleLayer[] layers = style.GetLayers();
				for (int i = 0; i < layers.Length; i++)
				{
					layers[i].Color = color;
				}
			}
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0001CA9C File Offset: 0x0001AC9C
		private void SetItemGlowWidgetColors(Color color)
		{
			if (this._moraleItemWidgets != null)
			{
				foreach (MoraleWidget.MoraleItemWidget widget in this._moraleItemWidgets)
				{
					this.SetSingleItemGlowWidgetColor(widget, color);
				}
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0001CAD4 File Offset: 0x0001ACD4
		private void SetSingleItemGlowWidgetColor(MoraleWidget.MoraleItemWidget widget, Color color)
		{
			widget.ItemGlowWidget.Brush.Color = color;
			foreach (Style style in widget.ItemGlowWidget.Brush.Styles)
			{
				StyleLayer[] layers = style.GetLayers();
				for (int i = 0; i < layers.Length; i++)
				{
					layers[i].Color = color;
				}
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0001CB58 File Offset: 0x0001AD58
		// (set) Token: 0x06000A07 RID: 2567 RVA: 0x0001CB60 File Offset: 0x0001AD60
		[DataSourceProperty]
		public int IncreaseLevel
		{
			get
			{
				return this._increaseLevel;
			}
			set
			{
				if (this._increaseLevel != value)
				{
					this._increaseLevel = value;
					base.OnPropertyChanged(value, "IncreaseLevel");
					this.UpdateArrows(this._increaseLevel);
				}
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0001CB8A File Offset: 0x0001AD8A
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x0001CB92 File Offset: 0x0001AD92
		[DataSourceProperty]
		public int MoralePercentage
		{
			get
			{
				return this._moralePercentage;
			}
			set
			{
				if (this._moralePercentage != value)
				{
					this._moralePercentage = value;
					base.OnPropertyChanged(value, "MoralePercentage");
				}
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0001CBB0 File Offset: 0x0001ADB0
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x0001CBB8 File Offset: 0x0001ADB8
		[DataSourceProperty]
		public Widget Container
		{
			get
			{
				return this._container;
			}
			set
			{
				if (this._container != value)
				{
					this._container = value;
					base.OnPropertyChanged<Widget>(value, "Container");
				}
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0001CBD6 File Offset: 0x0001ADD6
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x0001CBDE File Offset: 0x0001ADDE
		[DataSourceProperty]
		public Widget ItemContainer
		{
			get
			{
				return this._itemContainer;
			}
			set
			{
				if (this._itemContainer != value)
				{
					this._itemContainer = value;
					base.OnPropertyChanged<Widget>(value, "ItemContainer");
				}
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0001CBFC File Offset: 0x0001ADFC
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x0001CC04 File Offset: 0x0001AE04
		[DataSourceProperty]
		public Brush ItemBrush
		{
			get
			{
				return this._itemBrush;
			}
			set
			{
				if (this._itemBrush != value)
				{
					this._itemBrush = value;
					base.OnPropertyChanged<Brush>(value, "ItemBrush");
				}
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0001CC22 File Offset: 0x0001AE22
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x0001CC2A File Offset: 0x0001AE2A
		[DataSourceProperty]
		public Brush ItemGlowBrush
		{
			get
			{
				return this._itemGlowBrush;
			}
			set
			{
				if (this._itemGlowBrush != value)
				{
					this._itemGlowBrush = value;
					base.OnPropertyChanged<Brush>(value, "ItemGlowBrush");
				}
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0001CC48 File Offset: 0x0001AE48
		// (set) Token: 0x06000A13 RID: 2579 RVA: 0x0001CC50 File Offset: 0x0001AE50
		[DataSourceProperty]
		public Brush ItemBackgroundBrush
		{
			get
			{
				return this._itemBackgroundBrush;
			}
			set
			{
				if (this._itemBackgroundBrush != value)
				{
					this._itemBackgroundBrush = value;
					base.OnPropertyChanged<Brush>(value, "ItemBackgroundBrush");
				}
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0001CC6E File Offset: 0x0001AE6E
		// (set) Token: 0x06000A15 RID: 2581 RVA: 0x0001CC76 File Offset: 0x0001AE76
		[DataSourceProperty]
		public string TeamColorAsStr
		{
			get
			{
				return this._teamColorAsStr;
			}
			set
			{
				if (this._teamColorAsStr != value && value != null)
				{
					this._teamColorAsStr = value;
					base.OnPropertyChanged<string>(value, "TeamColorAsStr");
					this._teamColor = Color.ConvertStringToColor(value);
					this.SetItemWidgetColors(this._teamColor);
				}
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0001CCB4 File Offset: 0x0001AEB4
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x0001CCBC File Offset: 0x0001AEBC
		[DataSourceProperty]
		public string TeamColorAsStrSecondary
		{
			get
			{
				return this._teamColorAsStrSecondary;
			}
			set
			{
				if (this._teamColorAsStrSecondary != value && value != null)
				{
					this._teamColorAsStrSecondary = value;
					base.OnPropertyChanged<string>(value, "TeamColorAsStrSecondary");
					this._teamColorSecondary = Color.ConvertStringToColor(value);
					this.SetItemGlowWidgetColors(this._teamColorSecondary);
				}
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0001CCFA File Offset: 0x0001AEFA
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x0001CD02 File Offset: 0x0001AF02
		[DataSourceProperty]
		public MoraleArrowBrushWidget FlowArrowWidget
		{
			get
			{
				return this._flowArrowWidget;
			}
			set
			{
				if (this._flowArrowWidget != value)
				{
					this._flowArrowWidget = value;
					base.OnPropertyChanged<MoraleArrowBrushWidget>(value, "FlowArrowWidget");
				}
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0001CD20 File Offset: 0x0001AF20
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x0001CD28 File Offset: 0x0001AF28
		[DataSourceProperty]
		public bool ExtendToLeft
		{
			get
			{
				return this._extendToLeft;
			}
			set
			{
				if (this._extendToLeft != value)
				{
					this._extendToLeft = value;
					base.OnPropertyChanged(value, "ExtendToLeft");
				}
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0001CD46 File Offset: 0x0001AF46
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x0001CD4E File Offset: 0x0001AF4E
		[DataSourceProperty]
		public bool AreMoralesIndependent
		{
			get
			{
				return this._areMoralesIndependent;
			}
			set
			{
				if (this._areMoralesIndependent != value)
				{
					this._areMoralesIndependent = value;
					base.OnPropertyChanged(value, "AreMoralesIndependent");
				}
			}
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001CD6C File Offset: 0x0001AF6C
		private float PingPong(float min, float max, float time)
		{
			float num = max - min;
			bool flag = (int)(time / num) % 2 == 0;
			float num2 = time % num;
			if (!flag)
			{
				return max - num2;
			}
			return num2 + min;
		}

		// Token: 0x0400048D RID: 1165
		private const int ItemCount = 10;

		// Token: 0x0400048E RID: 1166
		private const float ItemDistance = 28f;

		// Token: 0x0400048F RID: 1167
		private const int ItemWidth = 39;

		// Token: 0x04000490 RID: 1168
		private const int ItemHeight = 38;

		// Token: 0x04000491 RID: 1169
		private const int FillMargin = 12;

		// Token: 0x04000492 RID: 1170
		private MoraleWidget.MoraleItemWidget[] _moraleItemWidgets;

		// Token: 0x04000493 RID: 1171
		private bool _initialized;

		// Token: 0x04000494 RID: 1172
		private bool _triggerAnimations;

		// Token: 0x04000495 RID: 1173
		private int _animWaitFrame;

		// Token: 0x04000496 RID: 1174
		private string _currentStateName;

		// Token: 0x04000497 RID: 1175
		private Color _teamColor;

		// Token: 0x04000498 RID: 1176
		private Color _teamColorSecondary;

		// Token: 0x04000499 RID: 1177
		private int _increaseLevel;

		// Token: 0x0400049A RID: 1178
		private int _moralePercentage;

		// Token: 0x0400049B RID: 1179
		private Widget _container;

		// Token: 0x0400049C RID: 1180
		private Widget _itemContainer;

		// Token: 0x0400049D RID: 1181
		private Brush _itemBrush;

		// Token: 0x0400049E RID: 1182
		private Brush _itemGlowBrush;

		// Token: 0x0400049F RID: 1183
		private Brush _itemBackgroundBrush;

		// Token: 0x040004A0 RID: 1184
		private MoraleArrowBrushWidget _flowArrowWidget;

		// Token: 0x040004A1 RID: 1185
		private bool _extendToLeft;

		// Token: 0x040004A2 RID: 1186
		private bool _areMoralesIndependent;

		// Token: 0x040004A3 RID: 1187
		private string _teamColorAsStr;

		// Token: 0x040004A4 RID: 1188
		private string _teamColorAsStrSecondary;

		// Token: 0x020001A9 RID: 425
		private class MoraleItemWidget
		{
			// Token: 0x1700071B RID: 1819
			// (get) Token: 0x06001474 RID: 5236 RVA: 0x00037E55 File Offset: 0x00036055
			// (set) Token: 0x06001475 RID: 5237 RVA: 0x00037E5D File Offset: 0x0003605D
			public Widget ParentWidget { get; private set; }

			// Token: 0x1700071C RID: 1820
			// (get) Token: 0x06001476 RID: 5238 RVA: 0x00037E66 File Offset: 0x00036066
			// (set) Token: 0x06001477 RID: 5239 RVA: 0x00037E6E File Offset: 0x0003606E
			public Widget MaskWidget { get; private set; }

			// Token: 0x1700071D RID: 1821
			// (get) Token: 0x06001478 RID: 5240 RVA: 0x00037E77 File Offset: 0x00036077
			// (set) Token: 0x06001479 RID: 5241 RVA: 0x00037E7F File Offset: 0x0003607F
			public BrushWidget ItemWidget { get; private set; }

			// Token: 0x1700071E RID: 1822
			// (get) Token: 0x0600147A RID: 5242 RVA: 0x00037E88 File Offset: 0x00036088
			// (set) Token: 0x0600147B RID: 5243 RVA: 0x00037E90 File Offset: 0x00036090
			public BrushWidget ItemGlowWidget { get; private set; }

			// Token: 0x1700071F RID: 1823
			// (get) Token: 0x0600147C RID: 5244 RVA: 0x00037E99 File Offset: 0x00036099
			// (set) Token: 0x0600147D RID: 5245 RVA: 0x00037EA1 File Offset: 0x000360A1
			public Widget ItemBackgroundWidget { get; private set; }

			// Token: 0x0600147E RID: 5246 RVA: 0x00037EAA File Offset: 0x000360AA
			public MoraleItemWidget(Widget parentWidget, Widget maskWidget, BrushWidget itemWidget, BrushWidget itemGlowWidget, Widget itemBackgroundWidget)
			{
				this.ParentWidget = parentWidget;
				this.MaskWidget = maskWidget;
				this.ItemWidget = itemWidget;
				this.ItemGlowWidget = itemGlowWidget;
				this.ItemBackgroundWidget = itemBackgroundWidget;
			}

			// Token: 0x0600147F RID: 5247 RVA: 0x00037ED8 File Offset: 0x000360D8
			public void SetFillAmount(float fill, int fillMargin)
			{
				bool flag = MBMath.ApproximatelyEquals(fill, 0f, 1E-05f);
				bool flag2 = MBMath.ApproximatelyEquals(fill, 1f, 1E-05f);
				if (flag)
				{
					this.MaskWidget.SuggestedHeight = 0f;
				}
				else if (flag2)
				{
					this.MaskWidget.SuggestedHeight = this.ItemWidget.SuggestedHeight;
				}
				else
				{
					int num = MathF.Floor(this.ItemWidget.SuggestedHeight - (float)(fillMargin * 2));
					this.MaskWidget.SuggestedHeight = (float)fillMargin + (float)num * fill;
				}
				this.ItemWidget.IsVisible = !flag;
				this.ItemGlowWidget.IsVisible = flag2;
			}
		}
	}
}

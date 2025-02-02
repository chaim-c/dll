using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission
{
	// Token: 0x020000D0 RID: 208
	public class CompassElementWidget : Widget
	{
		// Token: 0x06000AAA RID: 2730 RVA: 0x0001E2A8 File Offset: 0x0001C4A8
		public CompassElementWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0001E2BC File Offset: 0x0001C4BC
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			this.HandleDistanceFading(dt);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0001E2CC File Offset: 0x0001C4CC
		private void HandleDistanceFading(float dt)
		{
			if (this.Distance < 10)
			{
				this._alpha -= 2f * dt;
			}
			else
			{
				this._alpha += 2f * dt;
			}
			this._alpha = MBMath.ClampFloat(this._alpha, 0f, 1f);
			if (this.BannerWidget != null)
			{
				int childCount = this.BannerWidget.ChildCount;
				for (int i = 0; i < childCount; i++)
				{
					Widget child = this.FlagWidget.GetChild(i);
					Color color = child.Color;
					color.Alpha = this._alpha;
					child.Color = color;
				}
			}
			if (this.FlagWidget != null)
			{
				int childCount2 = this.FlagWidget.ChildCount;
				for (int j = 0; j < childCount2; j++)
				{
					Widget child2 = this.FlagWidget.GetChild(j);
					Color color2 = child2.Color;
					color2.Alpha = this._alpha;
					child2.Color = color2;
				}
			}
			base.IsVisible = (this._alpha > 1E-05f);
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x0001E3CE File Offset: 0x0001C5CE
		// (set) Token: 0x06000AAE RID: 2734 RVA: 0x0001E3D6 File Offset: 0x0001C5D6
		[DataSourceProperty]
		public float Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (Math.Abs(this._position - value) > 1E-45f)
				{
					this._position = value;
					base.OnPropertyChanged(value, "Position");
				}
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x0001E3FF File Offset: 0x0001C5FF
		// (set) Token: 0x06000AB0 RID: 2736 RVA: 0x0001E407 File Offset: 0x0001C607
		[DataSourceProperty]
		public int Distance
		{
			get
			{
				return this._distance;
			}
			set
			{
				if (this._distance != value)
				{
					this._distance = value;
					base.OnPropertyChanged(value, "Distance");
				}
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x0001E425 File Offset: 0x0001C625
		// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x0001E42D File Offset: 0x0001C62D
		[DataSourceProperty]
		public Widget BannerWidget
		{
			get
			{
				return this._bannerWidget;
			}
			set
			{
				if (this._bannerWidget != value)
				{
					this._bannerWidget = value;
					base.OnPropertyChanged<Widget>(value, "BannerWidget");
				}
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x0001E44B File Offset: 0x0001C64B
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x0001E453 File Offset: 0x0001C653
		[DataSourceProperty]
		public Widget FlagWidget
		{
			get
			{
				return this._flagWidget;
			}
			set
			{
				if (this._flagWidget != value)
				{
					this._flagWidget = value;
					base.OnPropertyChanged<Widget>(value, "FlagWidget");
				}
			}
		}

		// Token: 0x040004DF RID: 1247
		private float _alpha = 1f;

		// Token: 0x040004E0 RID: 1248
		private float _position;

		// Token: 0x040004E1 RID: 1249
		private int _distance;

		// Token: 0x040004E2 RID: 1250
		private Widget _bannerWidget;

		// Token: 0x040004E3 RID: 1251
		private Widget _flagWidget;
	}
}

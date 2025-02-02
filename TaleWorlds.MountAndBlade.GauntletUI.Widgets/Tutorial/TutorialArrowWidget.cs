using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Tutorial
{
	// Token: 0x02000044 RID: 68
	public class TutorialArrowWidget : Widget
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000B633 File Offset: 0x00009833
		// (set) Token: 0x06000395 RID: 917 RVA: 0x0000B63B File Offset: 0x0000983B
		public bool IsArrowEnabled { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000B644 File Offset: 0x00009844
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0000B64C File Offset: 0x0000984C
		public float FadeInTime { get; set; } = 1f;

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000B655 File Offset: 0x00009855
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0000B65D File Offset: 0x0000985D
		public float BigCircleRadius { get; set; } = 2f;

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000B666 File Offset: 0x00009866
		// (set) Token: 0x0600039B RID: 923 RVA: 0x0000B66E File Offset: 0x0000986E
		public float SmallCircleRadius { get; set; } = 2f;

		// Token: 0x0600039C RID: 924 RVA: 0x0000B677 File Offset: 0x00009877
		public TutorialArrowWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000B6A4 File Offset: 0x000098A4
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this.IsArrowEnabled)
			{
				base.IsVisible = false;
				this.SetGlobalAlphaRecursively(0f);
				return;
			}
			base.IsVisible = true;
			base.ScaledSuggestedWidth = this._localWidth;
			base.ScaledSuggestedHeight = this._localHeight;
			if (this._startTime > -1f)
			{
				float alphaFactor = Mathf.Lerp(0f, 1f, Mathf.Clamp((base.EventManager.Time - this._startTime) / this.FadeInTime, 0f, 1f));
				this.SetGlobalAlphaRecursively(alphaFactor);
				return;
			}
			this.SetGlobalAlphaRecursively(0f);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000B74C File Offset: 0x0000994C
		public void SetArrowProperties(float width, float height, bool isDirectionDown, bool isDirectionRight)
		{
			if (this._localWidth != width || this._localHeight != height || this._isDirectionDown != isDirectionDown || this._isDirectionRight != isDirectionRight)
			{
				base.RemoveAllChildren();
				float num = (float)Math.Sqrt((double)(width * width + height * height));
				float num2 = (this.BigCircleRadius + this.SmallCircleRadius) / 2f;
				int num3 = (int)(num / num2);
				float num4 = 0f;
				float start = 0f;
				float num5;
				float end;
				if (isDirectionDown)
				{
					num5 = width;
					end = height;
				}
				else
				{
					num5 = width;
					start = height;
					end = 0f;
				}
				float start2 = isDirectionRight ? this.BigCircleRadius : this.SmallCircleRadius;
				float end2 = isDirectionRight ? this.SmallCircleRadius : this.BigCircleRadius;
				for (int i = 0; i < num3; i++)
				{
					Widget defaultCircleWidgetTemplate = this.GetDefaultCircleWidgetTemplate();
					base.AddChild(defaultCircleWidgetTemplate);
					float amount = num2 * (float)i / MathF.Abs(num4 - num5);
					float num6 = Mathf.Lerp(start2, end2, amount);
					defaultCircleWidgetTemplate.PositionXOffset = Mathf.Lerp(num4, num5, amount);
					defaultCircleWidgetTemplate.PositionYOffset = Mathf.Lerp(start, end, amount);
					defaultCircleWidgetTemplate.SuggestedHeight = num6;
					defaultCircleWidgetTemplate.SuggestedWidth = num6;
				}
				this._localWidth = width;
				this._localHeight = height;
				this._isDirectionDown = isDirectionDown;
				this._isDirectionRight = isDirectionRight;
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000B895 File Offset: 0x00009A95
		public void ResetFade()
		{
			this._startTime = base.EventManager.Time;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000B8A8 File Offset: 0x00009AA8
		public void DisableFade()
		{
			this._startTime = base.EventManager.Time;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000B8BB File Offset: 0x00009ABB
		private Widget GetDefaultCircleWidgetTemplate()
		{
			return new Widget(base.Context)
			{
				WidthSizePolicy = SizePolicy.Fixed,
				HeightSizePolicy = SizePolicy.Fixed,
				Sprite = base.Context.SpriteData.GetSprite("BlankWhiteCircle"),
				IsEnabled = false
			};
		}

		// Token: 0x0400017C RID: 380
		private float _localWidth;

		// Token: 0x0400017D RID: 381
		private float _localHeight;

		// Token: 0x0400017E RID: 382
		private bool _isDirectionDown;

		// Token: 0x0400017F RID: 383
		private bool _isDirectionRight;

		// Token: 0x04000180 RID: 384
		private float _startTime;
	}
}

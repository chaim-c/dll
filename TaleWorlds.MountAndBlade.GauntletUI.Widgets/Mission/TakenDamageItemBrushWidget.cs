using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission
{
	// Token: 0x020000DA RID: 218
	public class TakenDamageItemBrushWidget : BrushWidget
	{
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x0001FC4A File Offset: 0x0001DE4A
		// (set) Token: 0x06000B59 RID: 2905 RVA: 0x0001FC52 File Offset: 0x0001DE52
		public float VerticalWidth { get; set; }

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0001FC5B File Offset: 0x0001DE5B
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x0001FC63 File Offset: 0x0001DE63
		public float VerticalHeight { get; set; }

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0001FC6C File Offset: 0x0001DE6C
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x0001FC74 File Offset: 0x0001DE74
		public float HorizontalWidth { get; set; }

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0001FC7D File Offset: 0x0001DE7D
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x0001FC85 File Offset: 0x0001DE85
		public float HorizontalHeight { get; set; }

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0001FC8E File Offset: 0x0001DE8E
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x0001FC96 File Offset: 0x0001DE96
		public float RangedOnScreenStayTime { get; set; } = 0.3f;

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0001FC9F File Offset: 0x0001DE9F
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x0001FCA7 File Offset: 0x0001DEA7
		public float MeleeOnScreenStayTime { get; set; } = 1f;

		// Token: 0x06000B64 RID: 2916 RVA: 0x0001FCB0 File Offset: 0x0001DEB0
		public TakenDamageItemBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0001FCD0 File Offset: 0x0001DED0
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initialized)
			{
				this.RegisterBrushStatesOfWidget();
				this._initialized = true;
				if (!this.IsRanged)
				{
					float num = (float)this.DamageAmount / 70f;
					num = MathF.Clamp(num, 0f, 1f);
					base.AlphaFactor = MathF.Lerp(0.3f, 1f, num, 1E-05f);
				}
			}
			this.UpdateAlpha(dt);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0001FD44 File Offset: 0x0001DF44
		private void UpdateAlpha(float dt)
		{
			if (base.AlphaFactor < 0.01f)
			{
				base.EventFired("OnRemove", Array.Empty<object>());
			}
			float num = this.IsRanged ? this.RangedOnScreenStayTime : this.MeleeOnScreenStayTime;
			this.SetGlobalAlphaRecursively(MathF.Lerp(base.AlphaFactor, 0f, dt / num, 1E-05f));
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0001FDA3 File Offset: 0x0001DFA3
		protected override void OnRender(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
		{
			if (base.AlphaFactor > 0f)
			{
				base.OnRender(twoDimensionContext, drawContext);
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0001FDBA File Offset: 0x0001DFBA
		// (set) Token: 0x06000B69 RID: 2921 RVA: 0x0001FDC2 File Offset: 0x0001DFC2
		[DataSourceProperty]
		public int DamageAmount
		{
			get
			{
				return this._damageAmount;
			}
			set
			{
				if (this._damageAmount != value)
				{
					this._damageAmount = value;
					base.OnPropertyChanged(value, "DamageAmount");
				}
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0001FDE0 File Offset: 0x0001DFE0
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x0001FDE8 File Offset: 0x0001DFE8
		[DataSourceProperty]
		public bool IsBehind
		{
			get
			{
				return this._isBehind;
			}
			set
			{
				if (this._isBehind != value)
				{
					this._isBehind = value;
					base.OnPropertyChanged(value, "IsBehind");
				}
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0001FE06 File Offset: 0x0001E006
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x0001FE0E File Offset: 0x0001E00E
		[DataSourceProperty]
		public bool IsRanged
		{
			get
			{
				return this._isRanged;
			}
			set
			{
				if (this._isRanged != value)
				{
					this._isRanged = value;
					base.OnPropertyChanged(value, "IsRanged");
				}
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0001FE2C File Offset: 0x0001E02C
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x0001FE34 File Offset: 0x0001E034
		[DataSourceProperty]
		public Vec2 ScreenPosOfAffectorAgent
		{
			get
			{
				return this._screenPosOfAffectorAgent;
			}
			set
			{
				if (this._screenPosOfAffectorAgent != value)
				{
					this._screenPosOfAffectorAgent = value;
					base.OnPropertyChanged(value, "ScreenPosOfAffectorAgent");
				}
			}
		}

		// Token: 0x0400052B RID: 1323
		private bool _initialized;

		// Token: 0x04000532 RID: 1330
		private int _damageAmount;

		// Token: 0x04000533 RID: 1331
		private Vec2 _screenPosOfAffectorAgent;

		// Token: 0x04000534 RID: 1332
		private bool _isBehind;

		// Token: 0x04000535 RID: 1333
		private bool _isRanged;
	}
}

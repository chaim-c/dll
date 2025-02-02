using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission
{
	// Token: 0x020000D7 RID: 215
	public class MissionLeaveBarSliderWidget : SliderWidget
	{
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0001F871 File Offset: 0x0001DA71
		private float CurrentAlpha
		{
			get
			{
				return base.ReadOnlyBrush.GlobalAlphaFactor;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0001F87E File Offset: 0x0001DA7E
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x0001F886 File Offset: 0x0001DA86
		public float FadeInMultiplier { get; set; } = 1f;

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0001F88F File Offset: 0x0001DA8F
		// (set) Token: 0x06000B39 RID: 2873 RVA: 0x0001F897 File Offset: 0x0001DA97
		public float FadeOutMultiplier { get; set; } = 1f;

		// Token: 0x06000B3A RID: 2874 RVA: 0x0001F8A0 File Offset: 0x0001DAA0
		public MissionLeaveBarSliderWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0001F8C0 File Offset: 0x0001DAC0
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initialized)
			{
				this.SetGlobalAlphaRecursively(0f);
				this._initialized = true;
			}
			float num = (base.ValueFloat > 0f) ? this.FadeInMultiplier : this.FadeOutMultiplier;
			float end = (float)((base.ValueFloat > 0f) ? 1 : 0);
			float alphaFactor = Mathf.Clamp(Mathf.Lerp(this.CurrentAlpha, end, num * 0.2f), 0f, 1f);
			this.SetGlobalAlphaRecursively(alphaFactor);
		}

		// Token: 0x04000519 RID: 1305
		private bool _initialized;
	}
}

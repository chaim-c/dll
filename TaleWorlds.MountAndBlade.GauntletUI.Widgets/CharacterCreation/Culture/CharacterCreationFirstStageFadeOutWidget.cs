using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.CharacterCreation.Culture
{
	// Token: 0x02000180 RID: 384
	public class CharacterCreationFirstStageFadeOutWidget : Widget
	{
		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x00036347 File Offset: 0x00034547
		// (set) Token: 0x060013D3 RID: 5075 RVA: 0x0003634F File Offset: 0x0003454F
		public float StayTime { get; set; } = 1.5f;

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00036358 File Offset: 0x00034558
		// (set) Token: 0x060013D5 RID: 5077 RVA: 0x00036360 File Offset: 0x00034560
		public float FadeOutTime { get; set; } = 1.5f;

		// Token: 0x060013D6 RID: 5078 RVA: 0x00036369 File Offset: 0x00034569
		public CharacterCreationFirstStageFadeOutWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x00036388 File Offset: 0x00034588
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._totalTime < this.StayTime)
			{
				this.SetGlobalAlphaRecursively(1f);
				base.IsEnabled = true;
			}
			else if (this._totalTime > this.StayTime && this._totalTime < this.StayTime + this.FadeOutTime)
			{
				float num = Mathf.Lerp(1f, 0f, (this._totalTime - this.StayTime) / this.FadeOutTime);
				this.SetGlobalAlphaRecursively(num);
				base.IsEnabled = (num > 0.2f);
			}
			else
			{
				this.SetGlobalAlphaRecursively(0f);
				base.IsEnabled = false;
			}
			this._totalTime += dt;
		}

		// Token: 0x04000909 RID: 2313
		private float _totalTime;
	}
}

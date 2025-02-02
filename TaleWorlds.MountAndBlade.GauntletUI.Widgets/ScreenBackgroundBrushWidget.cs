using System;
using System.Linq;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200003A RID: 58
	public class ScreenBackgroundBrushWidget : BrushWidget
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000A6C0 File Offset: 0x000088C0
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000A6C8 File Offset: 0x000088C8
		public bool IsParticleVisible { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000A6D1 File Offset: 0x000088D1
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000A6D9 File Offset: 0x000088D9
		public bool IsSmokeVisible { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000A6E2 File Offset: 0x000088E2
		// (set) Token: 0x0600033C RID: 828 RVA: 0x0000A6EA File Offset: 0x000088EA
		public bool IsFullscreenImageEnabled { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000A6F3 File Offset: 0x000088F3
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000A6FB File Offset: 0x000088FB
		public bool AnimEnabled { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000A704 File Offset: 0x00008904
		// (set) Token: 0x06000340 RID: 832 RVA: 0x0000A70C File Offset: 0x0000890C
		public Widget ParticleWidget1 { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000A715 File Offset: 0x00008915
		// (set) Token: 0x06000342 RID: 834 RVA: 0x0000A71D File Offset: 0x0000891D
		public Widget ParticleWidget2 { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000A726 File Offset: 0x00008926
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0000A72E File Offset: 0x0000892E
		public Widget SmokeWidget1 { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000A737 File Offset: 0x00008937
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000A73F File Offset: 0x0000893F
		public Widget SmokeWidget2 { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000A748 File Offset: 0x00008948
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000A750 File Offset: 0x00008950
		public float SmokeSpeedModifier { get; set; } = 1f;

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000A759 File Offset: 0x00008959
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000A761 File Offset: 0x00008961
		public float ParticleSpeedModifier { get; set; } = 1f;

		// Token: 0x0600034B RID: 843 RVA: 0x0000A76A File Offset: 0x0000896A
		public ScreenBackgroundBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000A790 File Offset: 0x00008990
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._firstFrame)
			{
				this.UpdateBackgroundImage();
				this._firstFrame = false;
			}
			this.ParticleWidget1.IsVisible = this.IsParticleVisible;
			this.ParticleWidget2.IsVisible = this.IsParticleVisible;
			this.SmokeWidget1.IsVisible = this.IsSmokeVisible;
			this.SmokeWidget2.IsVisible = this.IsSmokeVisible;
			if (this.AnimEnabled)
			{
				if (this.IsParticleVisible)
				{
					this.ParticleWidget1.PositionXOffset = this._totalParticleXOffset;
					this.ParticleWidget2.PositionXOffset = this.ParticleWidget1.PositionXOffset + this.ParticleWidget1.SuggestedWidth;
					this._totalParticleXOffset -= dt * 10f * this.ParticleSpeedModifier;
					if (Math.Abs(this._totalParticleXOffset) >= this.ParticleWidget1.SuggestedWidth)
					{
						this._totalParticleXOffset = 0f;
					}
				}
				if (this.IsSmokeVisible)
				{
					this.SmokeWidget1.PositionXOffset = this._totalSmokeXOffset;
					this.SmokeWidget2.PositionXOffset = this.SmokeWidget1.PositionXOffset - this.SmokeWidget1.SuggestedWidth;
					if (Math.Abs(this._totalSmokeXOffset) >= this.SmokeWidget1.SuggestedWidth)
					{
						this._totalSmokeXOffset = 0f;
					}
					this._totalSmokeXOffset += dt * 10f * this.SmokeSpeedModifier;
				}
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000A8FC File Offset: 0x00008AFC
		private void UpdateBackgroundImage()
		{
			if (this.IsFullscreenImageEnabled)
			{
				int index = base.Context.UIRandom.Next(base.Brush.Styles.Count);
				StyleLayer[] layers = base.ReadOnlyBrush.Styles.ElementAt(index).GetLayers();
				if (layers.Length != 0)
				{
					base.Brush.Sprite = layers[0].Sprite;
					return;
				}
			}
			else
			{
				base.Brush.Sprite = null;
			}
		}

		// Token: 0x0400015D RID: 349
		private bool _firstFrame = true;

		// Token: 0x0400015E RID: 350
		private float _totalSmokeXOffset;

		// Token: 0x0400015F RID: 351
		private float _totalParticleXOffset;
	}
}

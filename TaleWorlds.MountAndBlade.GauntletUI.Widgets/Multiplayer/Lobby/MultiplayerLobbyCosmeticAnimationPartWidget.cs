using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby
{
	// Token: 0x02000099 RID: 153
	public class MultiplayerLobbyCosmeticAnimationPartWidget : Widget
	{
		// Token: 0x06000834 RID: 2100 RVA: 0x00018009 File Offset: 0x00016209
		public MultiplayerLobbyCosmeticAnimationPartWidget(UIContext context) : base(context)
		{
			this.StopAnimation();
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00018018 File Offset: 0x00016218
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._isAnimationPlaying)
			{
				return;
			}
			if (this._alphaChangeTimeElapsed >= this._alphaChangeDuration)
			{
				this.InvertAnimationDirection();
				this.InitializeAnimationParameters();
			}
			this._currentAlpha = MathF.Lerp(this._currentAlpha, this._targetAlpha, this._alphaChangeTimeElapsed / this._alphaChangeDuration, 1E-05f);
			base.AlphaFactor = this._currentAlpha;
			this._alphaChangeTimeElapsed += dt;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00018092 File Offset: 0x00016292
		public void InitializeAnimationParameters()
		{
			this._currentAlpha = this._minAlpha;
			this._targetAlpha = this._maxAlpha;
			this._alphaChangeTimeElapsed = 0f;
			base.AlphaFactor = this._currentAlpha;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x000180C4 File Offset: 0x000162C4
		private void InvertAnimationDirection()
		{
			float minAlpha = this._minAlpha;
			this._minAlpha = this._maxAlpha;
			this._maxAlpha = minAlpha;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x000180EB File Offset: 0x000162EB
		public void StartAnimation(float alphaChangeDuration, float minAlpha, float maxAlpha)
		{
			this._alphaChangeDuration = alphaChangeDuration;
			this._minAlpha = minAlpha;
			this._maxAlpha = maxAlpha;
			this.InitializeAnimationParameters();
			this._isAnimationPlaying = true;
			base.IsVisible = true;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00018116 File Offset: 0x00016316
		public void StopAnimation()
		{
			this.InitializeAnimationParameters();
			this._isAnimationPlaying = false;
			base.IsVisible = false;
		}

		// Token: 0x040003BF RID: 959
		private float _alphaChangeDuration;

		// Token: 0x040003C0 RID: 960
		private float _minAlpha;

		// Token: 0x040003C1 RID: 961
		private float _maxAlpha;

		// Token: 0x040003C2 RID: 962
		private float _currentAlpha;

		// Token: 0x040003C3 RID: 963
		private float _targetAlpha;

		// Token: 0x040003C4 RID: 964
		private float _alphaChangeTimeElapsed;

		// Token: 0x040003C5 RID: 965
		private bool _isAnimationPlaying;
	}
}

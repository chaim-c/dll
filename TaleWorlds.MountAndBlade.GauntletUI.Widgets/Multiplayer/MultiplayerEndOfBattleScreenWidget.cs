using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer
{
	// Token: 0x0200007F RID: 127
	public class MultiplayerEndOfBattleScreenWidget : Widget
	{
		// Token: 0x0600071D RID: 1821 RVA: 0x0001522A File Offset: 0x0001342A
		public MultiplayerEndOfBattleScreenWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001524C File Offset: 0x0001344C
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._isAnimationStarted)
			{
				this.SetGlobalAlphaRecursively(MathF.Lerp(this._initialAlpha, this._targetAlpha, this._fadeInTimeElapsed / this.FadeInDuration, 1E-05f));
				this._fadeInTimeElapsed += dt;
				if (this._fadeInTimeElapsed >= this.FadeInDuration)
				{
					this._isAnimationStarted = false;
				}
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x000152B4 File Offset: 0x000134B4
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x000152BC File Offset: 0x000134BC
		[Editor(false)]
		public bool IsShown
		{
			get
			{
				return this._isShown;
			}
			set
			{
				if (value != this._isShown)
				{
					this._isShown = value;
					base.OnPropertyChanged(value, "IsShown");
					this.SetGlobalAlphaRecursively(0f);
					this._isAnimationStarted = value;
					base.IsVisible = value;
					this._fadeInTimeElapsed = 0f;
				}
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x00015309 File Offset: 0x00013509
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x00015311 File Offset: 0x00013511
		[Editor(false)]
		public float FadeInDuration
		{
			get
			{
				return this._fadeInDuration;
			}
			set
			{
				if (value != this._fadeInDuration)
				{
					this._fadeInDuration = value;
					base.OnPropertyChanged(value, "FadeInDuration");
				}
			}
		}

		// Token: 0x04000323 RID: 803
		private float _initialAlpha;

		// Token: 0x04000324 RID: 804
		private float _targetAlpha = 1f;

		// Token: 0x04000325 RID: 805
		private bool _isAnimationStarted;

		// Token: 0x04000326 RID: 806
		private float _fadeInTimeElapsed;

		// Token: 0x04000327 RID: 807
		private bool _isShown;

		// Token: 0x04000328 RID: 808
		private float _fadeInDuration = 0.3f;
	}
}

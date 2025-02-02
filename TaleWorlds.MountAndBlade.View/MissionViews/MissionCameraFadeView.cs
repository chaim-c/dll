using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.MissionViews
{
	// Token: 0x02000049 RID: 73
	[DefaultView]
	public class MissionCameraFadeView : MissionView
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0001C27B File Offset: 0x0001A47B
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0001C283 File Offset: 0x0001A483
		public float FadeAlpha { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0001C28C File Offset: 0x0001A48C
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0001C294 File Offset: 0x0001A494
		public MissionCameraFadeView.CameraFadeState FadeState { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0001C29D File Offset: 0x0001A49D
		public bool IsCameraFading
		{
			get
			{
				return this.FadeState == MissionCameraFadeView.CameraFadeState.FadingIn || this.FadeState == MissionCameraFadeView.CameraFadeState.FadingOut;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0001C2B3 File Offset: 0x0001A4B3
		public bool HasCameraFadeOut
		{
			get
			{
				return this.FadeState == MissionCameraFadeView.CameraFadeState.Black;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0001C2BE File Offset: 0x0001A4BE
		public bool HasCameraFadeIn
		{
			get
			{
				return this.FadeState == MissionCameraFadeView.CameraFadeState.White;
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0001C2C9 File Offset: 0x0001A4C9
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			this._stateDuration = 0f;
			this.FadeState = MissionCameraFadeView.CameraFadeState.White;
			this.FadeAlpha = 0f;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0001C2EE File Offset: 0x0001A4EE
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			if (base.Mission != null && base.MissionScreen.IsMissionTickable)
			{
				this.UpdateFadeState(dt);
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0001C314 File Offset: 0x0001A514
		protected void UpdateFadeState(float dt)
		{
			if (this.IsCameraFading)
			{
				this._stateDuration -= dt;
				if (this.FadeState == MissionCameraFadeView.CameraFadeState.FadingOut)
				{
					this.FadeAlpha = MathF.Min(1f - this._stateDuration / this._fadeOutTime, 1f);
					if (this._stateDuration < 0f)
					{
						this._stateDuration = this._blackTime;
						this.FadeState = MissionCameraFadeView.CameraFadeState.Black;
						return;
					}
				}
				else if (this.FadeState == MissionCameraFadeView.CameraFadeState.FadingIn)
				{
					this.FadeAlpha = MathF.Max(this._stateDuration / this._fadeInTime, 0f);
					if (this._stateDuration < 0f)
					{
						this._stateDuration = 0f;
						this.FadeState = MissionCameraFadeView.CameraFadeState.White;
						return;
					}
				}
			}
			else if (this.HasCameraFadeOut && this._autoFadeIn)
			{
				this._stateDuration -= dt;
				if (this._stateDuration < 0f)
				{
					this._stateDuration = this._fadeInTime;
					this.FadeState = MissionCameraFadeView.CameraFadeState.FadingIn;
					this._autoFadeIn = false;
				}
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0001C418 File Offset: 0x0001A618
		public void BeginFadeOutAndIn(float fadeOutTime, float blackTime, float fadeInTime)
		{
			if (base.Mission != null && base.MissionScreen.IsMissionTickable && this.FadeState == MissionCameraFadeView.CameraFadeState.White)
			{
				this._autoFadeIn = true;
				this._fadeOutTime = MathF.Max(fadeOutTime, 1E-05f);
				this._blackTime = MathF.Max(blackTime, 1E-05f);
				this._fadeInTime = MathF.Max(fadeInTime, 1E-05f);
				this._stateDuration = fadeOutTime;
				this.FadeAlpha = 0f;
				this.FadeState = MissionCameraFadeView.CameraFadeState.FadingOut;
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0001C498 File Offset: 0x0001A698
		public void BeginFadeOut(float fadeOutTime)
		{
			if (base.Mission != null && base.MissionScreen.IsMissionTickable && this.FadeState == MissionCameraFadeView.CameraFadeState.White)
			{
				this._autoFadeIn = false;
				this._fadeOutTime = MathF.Max(fadeOutTime, 1E-05f);
				this._blackTime = 0f;
				this._fadeInTime = 0f;
				this._stateDuration = fadeOutTime;
				this.FadeAlpha = 0f;
				this.FadeState = MissionCameraFadeView.CameraFadeState.FadingOut;
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0001C50C File Offset: 0x0001A70C
		public void BeginFadeIn(float fadeInTime)
		{
			if (base.Mission != null && base.MissionScreen.IsMissionTickable && this.FadeState == MissionCameraFadeView.CameraFadeState.Black && !this._autoFadeIn)
			{
				this._fadeOutTime = 0f;
				this._blackTime = 0f;
				this._fadeInTime = MathF.Max(fadeInTime, 1E-05f);
				this._stateDuration = fadeInTime;
				this.FadeAlpha = 1f;
				this.FadeState = MissionCameraFadeView.CameraFadeState.FadingIn;
			}
		}

		// Token: 0x0400023E RID: 574
		private bool _autoFadeIn;

		// Token: 0x0400023F RID: 575
		private float _fadeInTime = 0.5f;

		// Token: 0x04000240 RID: 576
		private float _blackTime = 0.25f;

		// Token: 0x04000241 RID: 577
		private float _fadeOutTime = 0.5f;

		// Token: 0x04000242 RID: 578
		private float _stateDuration;

		// Token: 0x020000AA RID: 170
		public enum CameraFadeState
		{
			// Token: 0x0400037D RID: 893
			White,
			// Token: 0x0400037E RID: 894
			FadingOut,
			// Token: 0x0400037F RID: 895
			Black,
			// Token: 0x04000380 RID: 896
			FadingIn
		}
	}
}

using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.Launcher.Library.CustomWidgets
{
	// Token: 0x02000020 RID: 32
	public class LauncherCircleLoadingAnimWidget : Widget
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00005BF5 File Offset: 0x00003DF5
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00005BFD File Offset: 0x00003DFD
		public float NumOfCirclesInASecond { get; set; } = 0.5f;

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00005C06 File Offset: 0x00003E06
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00005C0E File Offset: 0x00003E0E
		public float NormalAlpha { get; set; } = 0.5f;

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00005C17 File Offset: 0x00003E17
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00005C1F File Offset: 0x00003E1F
		public float FullAlpha { get; set; } = 1f;

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00005C28 File Offset: 0x00003E28
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00005C30 File Offset: 0x00003E30
		public float CircleRadius { get; set; } = 50f;

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00005C39 File Offset: 0x00003E39
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00005C41 File Offset: 0x00003E41
		public float StaySeconds { get; set; } = 2f;

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00005C4A File Offset: 0x00003E4A
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00005C52 File Offset: 0x00003E52
		public bool IsMovementEnabled { get; set; } = true;

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00005C5B File Offset: 0x00003E5B
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00005C63 File Offset: 0x00003E63
		public bool IsReverse { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00005C6C File Offset: 0x00003E6C
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00005C74 File Offset: 0x00003E74
		public float FadeInSeconds { get; set; } = 0.2f;

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00005C7D File Offset: 0x00003E7D
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00005C85 File Offset: 0x00003E85
		public float FadeOutSeconds { get; set; } = 0.2f;

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00005C8E File Offset: 0x00003E8E
		private float CurrentAlpha
		{
			get
			{
				return base.GetChild(0).AlphaFactor;
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005C9C File Offset: 0x00003E9C
		public LauncherCircleLoadingAnimWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005D04 File Offset: 0x00003F04
		protected override void OnParallelUpdate(float dt)
		{
			base.OnParallelUpdate(dt);
			this._totalTime += dt;
			if (!this._initialized)
			{
				this._visualState = LauncherCircleLoadingAnimWidget.VisualState.FadeIn;
				this.SetGlobalAlphaRecursively(0f);
				this._initialized = true;
			}
			if (this.IsMovementEnabled && base.IsVisible)
			{
				this.UpdateMovementValues(dt);
				this.UpdateAlphaValues(dt);
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005D68 File Offset: 0x00003F68
		private void UpdateMovementValues(float dt)
		{
			if (this.IsMovementEnabled)
			{
				float num = 360f / (float)base.ChildCount;
				float num2 = this._currentAngle;
				for (int i = 0; i < base.ChildCount; i++)
				{
					float num3 = (float)Math.Cos((double)(num2 * 0.017453292f)) * this.CircleRadius;
					float num4 = (float)Math.Sin((double)(num2 * 0.017453292f)) * this.CircleRadius;
					base.GetChild(i).PositionXOffset = (this.IsReverse ? num4 : num3);
					base.GetChild(i).PositionYOffset = (this.IsReverse ? num3 : num4);
					num2 += num;
					num2 %= 360f;
				}
				this._currentAngle += dt * 360f * this.NumOfCirclesInASecond;
				this._currentAngle %= 360f;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005E40 File Offset: 0x00004040
		private void UpdateAlphaValues(float dt)
		{
			float alphaFactor = 1f;
			if (this._visualState == LauncherCircleLoadingAnimWidget.VisualState.FadeIn)
			{
				alphaFactor = Mathf.Lerp(this.CurrentAlpha, 1f, dt / this.FadeInSeconds);
				if (this.CurrentAlpha >= 0.9f)
				{
					this._visualState = LauncherCircleLoadingAnimWidget.VisualState.Animating;
					this._stayStartTime = this._totalTime;
				}
			}
			else if (this._visualState == LauncherCircleLoadingAnimWidget.VisualState.Animating)
			{
				alphaFactor = 1f;
				if (this.StaySeconds != -1f && this._totalTime - this._stayStartTime > this.StaySeconds)
				{
					this._visualState = LauncherCircleLoadingAnimWidget.VisualState.FadeOut;
				}
			}
			else if (this._visualState == LauncherCircleLoadingAnimWidget.VisualState.FadeOut)
			{
				alphaFactor = Mathf.Lerp(this.CurrentAlpha, 0f, dt / this.FadeOutSeconds);
				if (this.CurrentAlpha <= 0.01f && this._totalTime - (this._stayStartTime + this.StaySeconds + this.FadeOutSeconds) > 3f)
				{
					this._visualState = LauncherCircleLoadingAnimWidget.VisualState.FadeIn;
				}
			}
			else
			{
				Debug.FailedAssert("This visual state is not enabled", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Launcher.Library\\CustomWidgets\\LauncherCircleLoadingAnimWidget.cs", "UpdateAlphaValues", 113);
			}
			this.SetGlobalAlphaRecursively(alphaFactor);
		}

		// Token: 0x040000A4 RID: 164
		private LauncherCircleLoadingAnimWidget.VisualState _visualState;

		// Token: 0x040000A5 RID: 165
		private float _stayStartTime;

		// Token: 0x040000A6 RID: 166
		private float _currentAngle;

		// Token: 0x040000A7 RID: 167
		private bool _initialized;

		// Token: 0x040000A8 RID: 168
		private float _totalTime;

		// Token: 0x02000044 RID: 68
		public enum VisualState
		{
			// Token: 0x040000F3 RID: 243
			FadeIn,
			// Token: 0x040000F4 RID: 244
			Animating,
			// Token: 0x040000F5 RID: 245
			FadeOut
		}
	}
}

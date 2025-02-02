using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200000D RID: 13
	public class CircleLoadingAnimWidget : Widget
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000336C File Offset: 0x0000156C
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003374 File Offset: 0x00001574
		public float NumOfCirclesInASecond { get; set; } = 0.5f;

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000337D File Offset: 0x0000157D
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003385 File Offset: 0x00001585
		public float NormalAlpha { get; set; } = 0.5f;

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000338E File Offset: 0x0000158E
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003396 File Offset: 0x00001596
		public float FullAlpha { get; set; } = 1f;

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000339F File Offset: 0x0000159F
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000033A7 File Offset: 0x000015A7
		public float CircleRadius { get; set; } = 50f;

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000033B0 File Offset: 0x000015B0
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000033B8 File Offset: 0x000015B8
		public float StaySeconds { get; set; } = 2f;

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000033C1 File Offset: 0x000015C1
		// (set) Token: 0x0600008B RID: 139 RVA: 0x000033C9 File Offset: 0x000015C9
		public bool IsMovementEnabled { get; set; } = true;

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000033D2 File Offset: 0x000015D2
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000033DA File Offset: 0x000015DA
		public bool IsReverse { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000033E3 File Offset: 0x000015E3
		// (set) Token: 0x0600008F RID: 143 RVA: 0x000033EB File Offset: 0x000015EB
		public float FadeInSeconds { get; set; } = 0.2f;

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000033F4 File Offset: 0x000015F4
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000033FC File Offset: 0x000015FC
		public float FadeOutSeconds { get; set; } = 0.2f;

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003405 File Offset: 0x00001605
		private float CurrentAlpha
		{
			get
			{
				return base.GetChild(0).AlphaFactor;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003414 File Offset: 0x00001614
		public CircleLoadingAnimWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000347C File Offset: 0x0000167C
		protected override void OnParallelUpdate(float dt)
		{
			base.OnParallelUpdate(dt);
			this._totalTime += dt;
			if (!this._initialized)
			{
				this._visualState = CircleLoadingAnimWidget.VisualState.FadeIn;
				this.SetGlobalAlphaRecursively(0f);
				this._initialized = true;
			}
			if (this.IsMovementEnabled && base.IsVisible)
			{
				Widget parentWidget = base.ParentWidget;
				if (parentWidget != null && parentWidget.IsVisible)
				{
					this.UpdateMovementValues(dt);
					this.UpdateAlphaValues(dt);
				}
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000034F4 File Offset: 0x000016F4
		private void UpdateMovementValues(float dt)
		{
			if (this.IsMovementEnabled)
			{
				float num = 360f / (float)base.ChildCount;
				float num2 = this._currentAngle;
				for (int i = 0; i < base.ChildCount; i++)
				{
					float num3 = MathF.Cos(num2 * 0.017453292f) * this.CircleRadius;
					float num4 = MathF.Sin(num2 * 0.017453292f) * this.CircleRadius;
					base.GetChild(i).PositionXOffset = (this.IsReverse ? num4 : num3);
					base.GetChild(i).PositionYOffset = (this.IsReverse ? num3 : num4);
					num2 += num;
					num2 %= 360f;
				}
				this._currentAngle += dt * 360f * this.NumOfCirclesInASecond;
				this._currentAngle %= 360f;
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000035C8 File Offset: 0x000017C8
		private void UpdateAlphaValues(float dt)
		{
			float alphaFactor = 1f;
			if (this._visualState == CircleLoadingAnimWidget.VisualState.FadeIn)
			{
				alphaFactor = Mathf.Lerp(this.CurrentAlpha, 1f, dt / this.FadeInSeconds);
				if (this.CurrentAlpha >= 0.9f)
				{
					this._visualState = CircleLoadingAnimWidget.VisualState.Animating;
					this._stayStartTime = this._totalTime;
				}
			}
			else if (this._visualState == CircleLoadingAnimWidget.VisualState.Animating)
			{
				alphaFactor = 1f;
				if (this.StaySeconds != -1f && this._totalTime - this._stayStartTime > this.StaySeconds)
				{
					this._visualState = CircleLoadingAnimWidget.VisualState.FadeOut;
				}
			}
			else if (this._visualState == CircleLoadingAnimWidget.VisualState.FadeOut)
			{
				alphaFactor = Mathf.Lerp(this.CurrentAlpha, 0f, dt / this.FadeOutSeconds);
				if (this.CurrentAlpha <= 0.01f && this._totalTime - (this._stayStartTime + this.StaySeconds + this.FadeOutSeconds) > 3f)
				{
					this._visualState = CircleLoadingAnimWidget.VisualState.FadeIn;
				}
			}
			else
			{
				Debug.FailedAssert("This visual state is not enabled", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\CircleLoadingAnimWidget.cs", "UpdateAlphaValues", 115);
			}
			this.SetGlobalAlphaRecursively(alphaFactor);
		}

		// Token: 0x04000040 RID: 64
		private CircleLoadingAnimWidget.VisualState _visualState;

		// Token: 0x04000041 RID: 65
		private float _stayStartTime;

		// Token: 0x04000042 RID: 66
		private float _currentAngle;

		// Token: 0x04000043 RID: 67
		private bool _initialized;

		// Token: 0x04000044 RID: 68
		private float _totalTime;

		// Token: 0x0200018B RID: 395
		public enum VisualState
		{
			// Token: 0x04000941 RID: 2369
			FadeIn,
			// Token: 0x04000942 RID: 2370
			Animating,
			// Token: 0x04000943 RID: 2371
			FadeOut
		}
	}
}

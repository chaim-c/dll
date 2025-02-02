using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.HUD
{
	// Token: 0x020000BD RID: 189
	public class HUDExtensionBrushWidget : BrushWidget
	{
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0001C1E7 File Offset: 0x0001A3E7
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0001C1EF File Offset: 0x0001A3EF
		public float AlphaChangeDuration { get; set; } = 0.15f;

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0001C1F8 File Offset: 0x0001A3F8
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x0001C200 File Offset: 0x0001A400
		public float OrderEnabledAlpha { get; set; } = 0.3f;

		// Token: 0x060009EA RID: 2538 RVA: 0x0001C209 File Offset: 0x0001A409
		public HUDExtensionBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0001C24C File Offset: 0x0001A44C
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._currentAlpha - this._targetAlpha > 1E-45f)
			{
				if (this._alphaChangeTimeElapsed < this.AlphaChangeDuration)
				{
					this._currentAlpha = MathF.Lerp(this._initialAlpha, this._targetAlpha, this._alphaChangeTimeElapsed / this.AlphaChangeDuration, 1E-05f);
					this.SetGlobalAlphaRecursively(this._currentAlpha);
					this._alphaChangeTimeElapsed += dt;
					return;
				}
			}
			else if (this._currentAlpha != this._targetAlpha)
			{
				this._currentAlpha = this._targetAlpha;
				this.SetGlobalAlphaRecursively(this._targetAlpha);
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0001C2EC File Offset: 0x0001A4EC
		private void OnIsOrderEnabledChanged()
		{
			this._alphaChangeTimeElapsed = 0f;
			this._targetAlpha = (this.IsOrderActive ? this.OrderEnabledAlpha : 1f);
			this._initialAlpha = this._currentAlpha;
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0001C320 File Offset: 0x0001A520
		// (set) Token: 0x060009EE RID: 2542 RVA: 0x0001C328 File Offset: 0x0001A528
		[Editor(false)]
		public bool IsOrderActive
		{
			get
			{
				return this._isOrderActive;
			}
			set
			{
				if (this._isOrderActive != value)
				{
					this._isOrderActive = value;
					base.OnPropertyChanged(value, "IsOrderActive");
					this.OnIsOrderEnabledChanged();
				}
			}
		}

		// Token: 0x04000482 RID: 1154
		private float _alphaChangeTimeElapsed;

		// Token: 0x04000483 RID: 1155
		private float _initialAlpha = 1f;

		// Token: 0x04000484 RID: 1156
		private float _targetAlpha = 1f;

		// Token: 0x04000485 RID: 1157
		private float _currentAlpha = 1f;

		// Token: 0x04000486 RID: 1158
		private bool _isOrderActive;
	}
}

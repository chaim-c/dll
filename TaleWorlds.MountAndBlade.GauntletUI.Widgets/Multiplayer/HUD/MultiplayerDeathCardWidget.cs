using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.HUD
{
	// Token: 0x020000C0 RID: 192
	public class MultiplayerDeathCardWidget : Widget
	{
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x0001CD93 File Offset: 0x0001AF93
		// (set) Token: 0x06000A20 RID: 2592 RVA: 0x0001CD9B File Offset: 0x0001AF9B
		public TextWidget WeaponTextWidget { get; set; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x0001CDA4 File Offset: 0x0001AFA4
		// (set) Token: 0x06000A22 RID: 2594 RVA: 0x0001CDAC File Offset: 0x0001AFAC
		public TextWidget TitleTextWidget { get; set; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0001CDB5 File Offset: 0x0001AFB5
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x0001CDBD File Offset: 0x0001AFBD
		public ScrollingRichTextWidget KillerNameTextWidget { get; set; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0001CDC6 File Offset: 0x0001AFC6
		// (set) Token: 0x06000A26 RID: 2598 RVA: 0x0001CDCE File Offset: 0x0001AFCE
		public Widget KillCountContainer { get; set; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0001CDD7 File Offset: 0x0001AFD7
		// (set) Token: 0x06000A28 RID: 2600 RVA: 0x0001CDDF File Offset: 0x0001AFDF
		public Brush SelfInflictedTitleBrush { get; set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0001CDE8 File Offset: 0x0001AFE8
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x0001CDF0 File Offset: 0x0001AFF0
		public Brush NormalBrushTitleBrush { get; set; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0001CDF9 File Offset: 0x0001AFF9
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x0001CE01 File Offset: 0x0001B001
		public float FadeInModifier { get; set; } = 2f;

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x0001CE0A File Offset: 0x0001B00A
		// (set) Token: 0x06000A2E RID: 2606 RVA: 0x0001CE12 File Offset: 0x0001B012
		public float FadeOutModifier { get; set; } = 10f;

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0001CE1B File Offset: 0x0001B01B
		// (set) Token: 0x06000A30 RID: 2608 RVA: 0x0001CE23 File Offset: 0x0001B023
		public float StayTime { get; set; } = 7f;

		// Token: 0x06000A31 RID: 2609 RVA: 0x0001CE2C File Offset: 0x0001B02C
		public MultiplayerDeathCardWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0001CE58 File Offset: 0x0001B058
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initialized)
			{
				this._initialized = true;
				base.IsEnabled = false;
				this._initAlpha = base.AlphaFactor;
				this.SetGlobalAlphaRecursively(this._targetAlpha);
			}
			if (Math.Abs(base.AlphaFactor - this._targetAlpha) > 1E-45f)
			{
				float num = (base.AlphaFactor > this._targetAlpha) ? this.FadeOutModifier : this.FadeInModifier;
				float alphaFactor = Mathf.Lerp(base.AlphaFactor, this._targetAlpha, dt * num);
				this.SetGlobalAlphaRecursively(alphaFactor);
			}
			if ((this.IsActive && base.AlphaFactor < 1E-45f) || base.Context.EventManager.Time - this._activeTimeStart > this.StayTime)
			{
				this.IsActive = false;
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0001CF28 File Offset: 0x0001B128
		private void HandleIsActiveToggle(bool isActive)
		{
			this._targetAlpha = (isActive ? 1f : 0f);
			if (isActive)
			{
				this._activeTimeStart = base.Context.EventManager.Time;
			}
			this.KillCountContainer.IsVisible = (!this.IsSelfInflicted && this.KillCountsEnabled);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0001CF80 File Offset: 0x0001B180
		private void HandleSelfInflictedToggle(bool isSelfInflicted)
		{
			this.TitleTextWidget.IsVisible = true;
			this.TitleTextWidget.Brush = (isSelfInflicted ? this.SelfInflictedTitleBrush : this.NormalBrushTitleBrush);
			this.KillerNameTextWidget.IsVisible = !isSelfInflicted;
			this.WeaponTextWidget.IsVisible = !isSelfInflicted;
			this.KillCountContainer.IsVisible = (!this.IsSelfInflicted && this.KillCountsEnabled);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0001CFEF File Offset: 0x0001B1EF
		private void HandleKillCountsEnabledSwitch(bool killCountsEnabled)
		{
			this.KillCountContainer.IsVisible = killCountsEnabled;
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x0001CFFD File Offset: 0x0001B1FD
		// (set) Token: 0x06000A37 RID: 2615 RVA: 0x0001D005 File Offset: 0x0001B205
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (value != this._isActive)
				{
					this._isActive = value;
					base.OnPropertyChanged(value, "IsActive");
					this.HandleIsActiveToggle(value);
				}
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0001D02A File Offset: 0x0001B22A
		// (set) Token: 0x06000A39 RID: 2617 RVA: 0x0001D032 File Offset: 0x0001B232
		public bool IsSelfInflicted
		{
			get
			{
				return this._isSelfInflicted;
			}
			set
			{
				if (value != this._isSelfInflicted)
				{
					this._isSelfInflicted = value;
					base.OnPropertyChanged(value, "IsSelfInflicted");
					this.HandleSelfInflictedToggle(value);
				}
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0001D057 File Offset: 0x0001B257
		// (set) Token: 0x06000A3B RID: 2619 RVA: 0x0001D05F File Offset: 0x0001B25F
		public bool KillCountsEnabled
		{
			get
			{
				return this._killCountsEnabled;
			}
			set
			{
				if (value != this._killCountsEnabled)
				{
					this._killCountsEnabled = value;
					base.OnPropertyChanged(value, "KillCountsEnabled");
					this.HandleKillCountsEnabledSwitch(value);
				}
			}
		}

		// Token: 0x040004AB RID: 1195
		private float _targetAlpha;

		// Token: 0x040004AC RID: 1196
		private float _initAlpha;

		// Token: 0x040004B0 RID: 1200
		private float _activeTimeStart;

		// Token: 0x040004B1 RID: 1201
		private bool _initialized;

		// Token: 0x040004B2 RID: 1202
		private bool _isActive;

		// Token: 0x040004B3 RID: 1203
		private bool _isSelfInflicted;

		// Token: 0x040004B4 RID: 1204
		private bool _killCountsEnabled;
	}
}

using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.DamageFeed
{
	// Token: 0x020000F4 RID: 244
	public class MissionAgentDamageFeedItemWidget : Widget
	{
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00023F91 File Offset: 0x00022191
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x00023F99 File Offset: 0x00022199
		public float FadeInTime { get; set; } = 0.1f;

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x00023FA2 File Offset: 0x000221A2
		// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x00023FAA File Offset: 0x000221AA
		public float StayTime { get; set; } = 1.5f;

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x00023FB3 File Offset: 0x000221B3
		// (set) Token: 0x06000CFB RID: 3323 RVA: 0x00023FBB File Offset: 0x000221BB
		public float FadeOutTime { get; set; } = 0.3f;

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00023FC4 File Offset: 0x000221C4
		private float CurrentAlpha
		{
			get
			{
				return base.AlphaFactor;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00023FCC File Offset: 0x000221CC
		// (set) Token: 0x06000CFE RID: 3326 RVA: 0x00023FD4 File Offset: 0x000221D4
		public float TimeSinceCreation { get; private set; }

		// Token: 0x06000CFF RID: 3327 RVA: 0x00023FDD File Offset: 0x000221DD
		public MissionAgentDamageFeedItemWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00024012 File Offset: 0x00022212
		public void ShowFeed()
		{
			this._isShown = true;
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0002401C File Offset: 0x0002221C
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._isInitialized)
			{
				this.SetGlobalAlphaRecursively(0f);
				this._isInitialized = true;
			}
			if (this._isShown)
			{
				this.TimeSinceCreation += dt * this._speedModifier;
				if (this.TimeSinceCreation <= this.FadeInTime)
				{
					this.SetGlobalAlphaRecursively(Mathf.Lerp(this.CurrentAlpha, 1f, this.TimeSinceCreation / this.FadeInTime));
					return;
				}
				if (this.TimeSinceCreation - this.FadeInTime <= this.StayTime)
				{
					this.SetGlobalAlphaRecursively(1f);
					return;
				}
				if (this.TimeSinceCreation - (this.FadeInTime + this.StayTime) <= this.FadeOutTime)
				{
					this.SetGlobalAlphaRecursively(Mathf.Lerp(this.CurrentAlpha, 0f, (this.TimeSinceCreation - (this.FadeInTime + this.StayTime)) / this.FadeOutTime));
					if (this.CurrentAlpha <= 0.1f)
					{
						base.EventFired("OnRemove", Array.Empty<object>());
						return;
					}
				}
				else
				{
					base.EventFired("OnRemove", Array.Empty<object>());
				}
			}
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00024138 File Offset: 0x00022338
		public void SetSpeedModifier(float newSpeed)
		{
			if (newSpeed > this._speedModifier)
			{
				this._speedModifier = newSpeed;
			}
		}

		// Token: 0x040005F8 RID: 1528
		private float _speedModifier = 1f;

		// Token: 0x040005FA RID: 1530
		private bool _isInitialized;

		// Token: 0x040005FB RID: 1531
		private bool _isShown;
	}
}

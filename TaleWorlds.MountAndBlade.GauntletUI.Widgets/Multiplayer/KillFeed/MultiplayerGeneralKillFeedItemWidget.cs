using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.KillFeed
{
	// Token: 0x020000B8 RID: 184
	public class MultiplayerGeneralKillFeedItemWidget : Widget
	{
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0001B585 File Offset: 0x00019785
		private float CurrentAlpha
		{
			get
			{
				return base.AlphaFactor;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0001B58D File Offset: 0x0001978D
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x0001B595 File Offset: 0x00019795
		public float TimeSinceCreation { get; private set; }

		// Token: 0x060009AC RID: 2476 RVA: 0x0001B59E File Offset: 0x0001979E
		public MultiplayerGeneralKillFeedItemWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0001B5B4 File Offset: 0x000197B4
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initialized)
			{
				this.SetGlobalAlphaRecursively(0f);
				this._initialized = true;
			}
			this.TimeSinceCreation += dt * this._speedModifier;
			if (this.TimeSinceCreation <= 0.15f)
			{
				this.SetGlobalAlphaRecursively(Mathf.Lerp(this.CurrentAlpha, 1f, this.TimeSinceCreation / 0.15f));
				return;
			}
			if (this.TimeSinceCreation - 0.15f <= 3.5f)
			{
				this.SetGlobalAlphaRecursively(1f);
				return;
			}
			if (this.TimeSinceCreation - 3.65f <= 1f)
			{
				this.SetGlobalAlphaRecursively(Mathf.Lerp(this.CurrentAlpha, 0f, (this.TimeSinceCreation - 3.65f) / 1f));
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

		// Token: 0x060009AE RID: 2478 RVA: 0x0001B6AF File Offset: 0x000198AF
		public void SetSpeedModifier(float newSpeed)
		{
			if (newSpeed > this._speedModifier)
			{
				this._speedModifier = newSpeed;
			}
		}

		// Token: 0x04000466 RID: 1126
		private const float FadeInTime = 0.15f;

		// Token: 0x04000467 RID: 1127
		private const float StayTime = 3.5f;

		// Token: 0x04000468 RID: 1128
		private const float FadeOutTime = 1f;

		// Token: 0x04000469 RID: 1129
		private float _speedModifier = 1f;

		// Token: 0x0400046B RID: 1131
		private bool _initialized;
	}
}

using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.KillFeed
{
	// Token: 0x020000BA RID: 186
	public class MultiplayerPersonalKillFeedItemWidget : Widget
	{
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0001B818 File Offset: 0x00019A18
		// (set) Token: 0x060009B7 RID: 2487 RVA: 0x0001B820 File Offset: 0x00019A20
		public Widget NotificationTypeIconWidget { get; set; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0001B829 File Offset: 0x00019A29
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x0001B831 File Offset: 0x00019A31
		public Widget NotificationBackgroundWidget { get; set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0001B83A File Offset: 0x00019A3A
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x0001B842 File Offset: 0x00019A42
		public TextWidget AmountTextWidget { get; set; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x0001B84B File Offset: 0x00019A4B
		// (set) Token: 0x060009BD RID: 2493 RVA: 0x0001B853 File Offset: 0x00019A53
		public RichTextWidget MessageTextWidget { get; set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x0001B85C File Offset: 0x00019A5C
		// (set) Token: 0x060009BF RID: 2495 RVA: 0x0001B864 File Offset: 0x00019A64
		public float FadeInTime { get; set; } = 1f;

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0001B86D File Offset: 0x00019A6D
		// (set) Token: 0x060009C1 RID: 2497 RVA: 0x0001B875 File Offset: 0x00019A75
		public float StayTime { get; set; } = 3f;

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0001B87E File Offset: 0x00019A7E
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x0001B886 File Offset: 0x00019A86
		public float FadeOutTime { get; set; } = 0.5f;

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0001B88F File Offset: 0x00019A8F
		private float CurrentAlpha
		{
			get
			{
				return base.AlphaFactor;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0001B897 File Offset: 0x00019A97
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x0001B89F File Offset: 0x00019A9F
		public float TimeSinceCreation { get; private set; }

		// Token: 0x060009C7 RID: 2503 RVA: 0x0001B8A8 File Offset: 0x00019AA8
		public MultiplayerPersonalKillFeedItemWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0001B905 File Offset: 0x00019B05
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (!this._initialized)
			{
				base.PositionYOffset = 0f;
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0001B924 File Offset: 0x00019B24
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initialized)
			{
				this.SetGlobalAlphaRecursively(0f);
				this.UpdateNotificationBackgroundWidget();
				this.UpdateNotificationTypeIconWidget();
				this.UpdateNotificationMessageWidget();
				this.UpdateNotificationAmountWidget();
				this.DetermineSoundEvent();
				this._initialized = true;
			}
			this.UpdateAlphaValues(dt);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0001B977 File Offset: 0x00019B77
		private void DetermineSoundEvent()
		{
			if (this.ItemType == 6)
			{
				base.Context.TwoDimensionContext.PlaySound(this._goldGainedSound);
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0001B998 File Offset: 0x00019B98
		private void UpdateAlphaValues(float dt)
		{
			float end = 0f;
			float amount = 0f;
			this.TimeSinceCreation += dt;
			if (this._maxTargetAlpha == 0f)
			{
				base.EventFired("OnRemove", Array.Empty<object>());
				return;
			}
			if (this.TimeSinceCreation <= this.FadeInTime)
			{
				end = MathF.Min(1f, this._maxTargetAlpha);
				amount = this.TimeSinceCreation / this.FadeInTime;
			}
			else if (this.TimeSinceCreation - this.FadeInTime <= this.StayTime)
			{
				end = MathF.Min(1f, this._maxTargetAlpha);
				amount = 1f;
			}
			else if (this.TimeSinceCreation - (this.FadeInTime + this.StayTime) <= this.FadeOutTime)
			{
				end = 0f;
				amount = (this.TimeSinceCreation - (this.FadeInTime + this.StayTime)) / this.FadeOutTime;
				if (this.CurrentAlpha <= 0.1f)
				{
					base.EventFired("OnRemove", Array.Empty<object>());
				}
			}
			else
			{
				base.EventFired("OnRemove", Array.Empty<object>());
			}
			this.SetGlobalAlphaRecursively(Mathf.Lerp(this.CurrentAlpha, end, amount));
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0001BABD File Offset: 0x00019CBD
		public void SetSpeedModifier(float newSpeed)
		{
			if (newSpeed > this._speedModifier)
			{
				this._speedModifier = newSpeed;
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0001BACF File Offset: 0x00019CCF
		public void SetMaxAlphaValue(float newMaxAlpha)
		{
			if (newMaxAlpha < this._maxTargetAlpha)
			{
				this._maxTargetAlpha = newMaxAlpha;
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0001BAE4 File Offset: 0x00019CE4
		private void UpdateNotificationTypeIconWidget()
		{
			if (this.ItemType == 0)
			{
				this.NotificationTypeIconWidget.IsVisible = false;
				return;
			}
			switch (this.ItemType)
			{
			case 1:
				this.NotificationTypeIconWidget.SetState("FriendlyFireDamage");
				return;
			case 2:
				this.NotificationTypeIconWidget.SetState("FriendlyFireKill");
				return;
			case 3:
				this.NotificationTypeIconWidget.SetState("MountDamage");
				return;
			case 4:
				this.NotificationTypeIconWidget.SetState("NormalKill");
				return;
			case 5:
				this.NotificationTypeIconWidget.SetState("Assist");
				return;
			case 6:
				this.NotificationTypeIconWidget.SetState("GoldChange");
				return;
			case 7:
				this.NotificationTypeIconWidget.SetState("NormalKillHeadshot");
				return;
			default:
				Debug.FailedAssert("Undefined personal feed notification type", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\Multiplayer\\KillFeed\\MultiplayerPersonalKillFeedItemWidget.cs", "UpdateNotificationTypeIconWidget", 177);
				this.NotificationTypeIconWidget.IsVisible = false;
				return;
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0001BBD0 File Offset: 0x00019DD0
		private void UpdateNotificationMessageWidget()
		{
			this.MessageTextWidget.Text = this.Message;
			if (string.IsNullOrEmpty(this.Message))
			{
				this.MessageTextWidget.IsVisible = false;
				return;
			}
			switch (this.ItemType)
			{
			case 0:
			case 3:
			case 4:
			case 5:
			case 7:
				this.MessageTextWidget.SetState("Normal");
				return;
			case 1:
			case 2:
				this.MessageTextWidget.SetState("FriendlyFire");
				return;
			case 6:
				if (this.Amount >= 0)
				{
					this.MessageTextWidget.SetState("GoldChangePositive");
					return;
				}
				this.MessageTextWidget.SetState("GoldChangeNegative");
				return;
			default:
				Debug.FailedAssert("Undefined personal feed notification type", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\Multiplayer\\KillFeed\\MultiplayerPersonalKillFeedItemWidget.cs", "UpdateNotificationMessageWidget", 218);
				this.MessageTextWidget.IsVisible = false;
				return;
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0001BCAC File Offset: 0x00019EAC
		private void UpdateNotificationAmountWidget()
		{
			if (this.ItemType != 6 && this.Amount == -1)
			{
				this.AmountTextWidget.IsVisible = false;
				return;
			}
			switch (this.ItemType)
			{
			case 0:
			case 3:
			case 4:
			case 7:
				this.AmountTextWidget.SetState("Normal");
				this.AmountTextWidget.IntText = this.Amount;
				return;
			case 1:
			case 2:
				this.AmountTextWidget.SetState("FriendlyFire");
				this.AmountTextWidget.IntText = this.Amount;
				return;
			case 5:
				this.AmountTextWidget.IsVisible = false;
				return;
			case 6:
				if (this.Amount >= 0)
				{
					this.AmountTextWidget.SetState("GoldChangePositive");
					this.AmountTextWidget.Text = "+" + this.Amount.ToString();
					return;
				}
				this.AmountTextWidget.SetState("GoldChangeNegative");
				this.AmountTextWidget.Text = this.Amount.ToString();
				return;
			default:
				Debug.FailedAssert("Undefined personal feed notification type", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\Multiplayer\\KillFeed\\MultiplayerPersonalKillFeedItemWidget.cs", "UpdateNotificationAmountWidget", 264);
				this.AmountTextWidget.IsVisible = false;
				return;
			}
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0001BDE8 File Offset: 0x00019FE8
		private void UpdateNotificationBackgroundWidget()
		{
			switch (this.ItemType)
			{
			case 0:
			case 1:
			case 3:
				this.NotificationBackgroundWidget.SetState("Hidden");
				return;
			case 2:
				this.NotificationBackgroundWidget.SetState("FriendlyFire");
				return;
			case 4:
			case 7:
				this.NotificationBackgroundWidget.SetState("Normal");
				return;
			case 5:
				break;
			case 6:
				if (this.Amount >= 0)
				{
					this.NotificationBackgroundWidget.SetState("GoldChangePositive");
					return;
				}
				this.NotificationBackgroundWidget.SetState("GoldChangeNegative");
				return;
			default:
				Debug.FailedAssert("Undefined personal feed notification type", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\Multiplayer\\KillFeed\\MultiplayerPersonalKillFeedItemWidget.cs", "UpdateNotificationBackgroundWidget", 300);
				this.NotificationBackgroundWidget.SetState("Hidden");
				break;
			}
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0001BEAC File Offset: 0x0001A0AC
		private float GetInitialVerticalPositionOfSelf()
		{
			float num = 0f;
			for (int i = 0; i < base.GetSiblingIndex(); i++)
			{
				num += base.ParentWidget.GetChild(i).Size.Y * base._inverseScaleToUse;
			}
			return num;
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0001BEF1 File Offset: 0x0001A0F1
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x0001BEF9 File Offset: 0x0001A0F9
		public bool IsDamage
		{
			get
			{
				return this._isDamage;
			}
			set
			{
				if (value != this._isDamage)
				{
					this._isDamage = value;
					base.OnPropertyChanged(value, "IsDamage");
				}
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x0001BF17 File Offset: 0x0001A117
		// (set) Token: 0x060009D6 RID: 2518 RVA: 0x0001BF1F File Offset: 0x0001A11F
		public string Message
		{
			get
			{
				return this._message;
			}
			set
			{
				if (value != this._message)
				{
					this._message = value;
					base.OnPropertyChanged<string>(value, "Message");
				}
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x0001BF42 File Offset: 0x0001A142
		// (set) Token: 0x060009D8 RID: 2520 RVA: 0x0001BF4A File Offset: 0x0001A14A
		public int ItemType
		{
			get
			{
				return this._itemType;
			}
			set
			{
				if (value != this._itemType)
				{
					this._itemType = value;
					base.OnPropertyChanged(value, "ItemType");
				}
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0001BF68 File Offset: 0x0001A168
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x0001BF70 File Offset: 0x0001A170
		public int Amount
		{
			get
			{
				return this._amount;
			}
			set
			{
				if (value != this._amount)
				{
					this._amount = value;
					base.OnPropertyChanged(value, "Amount");
				}
			}
		}

		// Token: 0x04000476 RID: 1142
		private float _speedModifier = 1f;

		// Token: 0x04000478 RID: 1144
		private float _maxTargetAlpha = 1f;

		// Token: 0x04000479 RID: 1145
		private bool _initialized;

		// Token: 0x0400047A RID: 1146
		private string _goldGainedSound = "multiplayer/coin_add";

		// Token: 0x0400047B RID: 1147
		private bool _isDamage;

		// Token: 0x0400047C RID: 1148
		private int _itemType;

		// Token: 0x0400047D RID: 1149
		private int _amount = -1;

		// Token: 0x0400047E RID: 1150
		private string _message;
	}
}

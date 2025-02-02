using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.KillFeed.Personal
{
	// Token: 0x020000EE RID: 238
	public class SingleplayerPersonalKillFeedItemWidget : Widget
	{
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0002277B File Offset: 0x0002097B
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x00022783 File Offset: 0x00020983
		public Widget NotificationTypeIconWidget { get; set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0002278C File Offset: 0x0002098C
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x00022794 File Offset: 0x00020994
		public Widget NotificationBackgroundWidget { get; set; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0002279D File Offset: 0x0002099D
		// (set) Token: 0x06000C85 RID: 3205 RVA: 0x000227A5 File Offset: 0x000209A5
		public TextWidget AmountTextWidget { get; set; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x000227AE File Offset: 0x000209AE
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x000227B6 File Offset: 0x000209B6
		public RichTextWidget MessageTextWidget { get; set; }

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x000227BF File Offset: 0x000209BF
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x000227C7 File Offset: 0x000209C7
		public float FadeInTime { get; set; } = 0.2f;

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x000227D0 File Offset: 0x000209D0
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x000227D8 File Offset: 0x000209D8
		public float StayTime { get; set; } = 2f;

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x000227E1 File Offset: 0x000209E1
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x000227E9 File Offset: 0x000209E9
		public float FadeOutTime { get; set; } = 0.2f;

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x000227F2 File Offset: 0x000209F2
		private float CurrentAlpha
		{
			get
			{
				return base.AlphaFactor;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x000227FA File Offset: 0x000209FA
		// (set) Token: 0x06000C90 RID: 3216 RVA: 0x00022802 File Offset: 0x00020A02
		public float TimeSinceCreation { get; private set; }

		// Token: 0x06000C91 RID: 3217 RVA: 0x0002280B File Offset: 0x00020A0B
		public SingleplayerPersonalKillFeedItemWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00022838 File Offset: 0x00020A38
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
				this.UpdateTroopTypeVisualWidget();
				this._initialized = true;
			}
			this.UpdateAlphaValues(dt);
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0002288C File Offset: 0x00020A8C
		private void UpdateAlphaValues(float dt)
		{
			this.TimeSinceCreation += dt;
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

		// Token: 0x06000C94 RID: 3220 RVA: 0x00022975 File Offset: 0x00020B75
		public void SetSpeedModifier(float newSpeed)
		{
			if (newSpeed > this._speedModifier)
			{
				this._speedModifier = newSpeed;
			}
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00022988 File Offset: 0x00020B88
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
				this.NotificationTypeIconWidget.SetState("MakeUnconscious");
				return;
			case 7:
				this.NotificationTypeIconWidget.SetState("NormalKillHeadshot");
				return;
			case 8:
				this.NotificationTypeIconWidget.SetState("MakeUnconsciousHeadshot");
				return;
			default:
				Debug.FailedAssert("Undefined personal feed notification type", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\Mission\\KillFeed\\Personal\\SingleplayerPersonalKillFeedItemWidget.cs", "UpdateNotificationTypeIconWidget", 126);
				this.NotificationTypeIconWidget.IsVisible = false;
				return;
			}
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00022A88 File Offset: 0x00020C88
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
			case 6:
			case 7:
			case 8:
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
			default:
				Debug.FailedAssert("Undefined personal feed notification type", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\Mission\\KillFeed\\Personal\\SingleplayerPersonalKillFeedItemWidget.cs", "UpdateNotificationAmountWidget", 163);
				this.AmountTextWidget.IsVisible = false;
				return;
			}
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00022B60 File Offset: 0x00020D60
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
			case 6:
			case 7:
			case 8:
				this.MessageTextWidget.SetState("Normal");
				return;
			case 1:
			case 2:
				this.MessageTextWidget.SetState("FriendlyFire");
				return;
			default:
				Debug.FailedAssert("Undefined personal feed notification type", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\Mission\\KillFeed\\Personal\\SingleplayerPersonalKillFeedItemWidget.cs", "UpdateNotificationMessageWidget", 196);
				this.MessageTextWidget.IsVisible = false;
				return;
			}
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00022C14 File Offset: 0x00020E14
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
			case 6:
			case 7:
			case 8:
				this.NotificationBackgroundWidget.SetState("Normal");
				return;
			case 5:
				break;
			default:
				Debug.FailedAssert("Undefined personal feed notification type", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\Mission\\KillFeed\\Personal\\SingleplayerPersonalKillFeedItemWidget.cs", "UpdateNotificationBackgroundWidget", 224);
				this.NotificationBackgroundWidget.SetState("Hidden");
				break;
			}
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00022CB0 File Offset: 0x00020EB0
		private void UpdateTroopTypeVisualWidget()
		{
			if (this._troopTypeWidget != null)
			{
				if (string.IsNullOrEmpty(this.TypeID))
				{
					this._troopTypeWidget.IsVisible = false;
					return;
				}
				this._troopTypeWidget.Sprite = this._troopTypeWidget.Context.SpriteData.GetSprite("General\\compass\\" + this._typeID);
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x00022D0F File Offset: 0x00020F0F
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x00022D17 File Offset: 0x00020F17
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

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00022D35 File Offset: 0x00020F35
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x00022D3D File Offset: 0x00020F3D
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

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x00022D5B File Offset: 0x00020F5B
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x00022D63 File Offset: 0x00020F63
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

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x00022D81 File Offset: 0x00020F81
		// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x00022D89 File Offset: 0x00020F89
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

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x00022DAC File Offset: 0x00020FAC
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x00022DB4 File Offset: 0x00020FB4
		public string TypeID
		{
			get
			{
				return this._typeID;
			}
			set
			{
				if (value != this._typeID)
				{
					this._typeID = value;
					base.OnPropertyChanged<string>(value, "TypeID");
				}
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x00022DD7 File Offset: 0x00020FD7
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x00022DE0 File Offset: 0x00020FE0
		public Widget TroopTypeWidget
		{
			get
			{
				return this._troopTypeWidget;
			}
			set
			{
				if (value != this._troopTypeWidget)
				{
					this._troopTypeWidget = value;
					if (!string.IsNullOrEmpty(this._typeID))
					{
						this._troopTypeWidget.Sprite = this._troopTypeWidget.Context.SpriteData.GetSprite("General\\compass\\" + this._typeID);
					}
				}
			}
		}

		// Token: 0x040005BC RID: 1468
		private bool _initialized;

		// Token: 0x040005BD RID: 1469
		private float _speedModifier;

		// Token: 0x040005BE RID: 1470
		private int _itemType;

		// Token: 0x040005BF RID: 1471
		private int _amount;

		// Token: 0x040005C0 RID: 1472
		private string _typeID;

		// Token: 0x040005C1 RID: 1473
		private string _message;

		// Token: 0x040005C2 RID: 1474
		private Widget _troopTypeWidget;

		// Token: 0x040005C3 RID: 1475
		private bool _isDamage;
	}
}

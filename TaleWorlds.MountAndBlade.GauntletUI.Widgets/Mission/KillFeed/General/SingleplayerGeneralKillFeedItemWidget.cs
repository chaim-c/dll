using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.KillFeed.General
{
	// Token: 0x020000F0 RID: 240
	public class SingleplayerGeneralKillFeedItemWidget : Widget
	{
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00022F68 File Offset: 0x00021168
		// (set) Token: 0x06000CAC RID: 3244 RVA: 0x00022F70 File Offset: 0x00021170
		public Widget MurdererTypeWidget { get; set; }

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00022F79 File Offset: 0x00021179
		// (set) Token: 0x06000CAE RID: 3246 RVA: 0x00022F81 File Offset: 0x00021181
		public Widget VictimTypeWidget { get; set; }

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x00022F8A File Offset: 0x0002118A
		// (set) Token: 0x06000CB0 RID: 3248 RVA: 0x00022F92 File Offset: 0x00021192
		public Widget ActionIconWidget { get; set; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00022F9B File Offset: 0x0002119B
		// (set) Token: 0x06000CB2 RID: 3250 RVA: 0x00022FA3 File Offset: 0x000211A3
		public Widget BackgroundWidget { get; set; }

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00022FAC File Offset: 0x000211AC
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x00022FB4 File Offset: 0x000211B4
		public AutoHideTextWidget VictimNameWidget { get; set; }

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00022FBD File Offset: 0x000211BD
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x00022FC5 File Offset: 0x000211C5
		public AutoHideTextWidget MurdererNameWidget { get; set; }

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x00022FCE File Offset: 0x000211CE
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x00022FD6 File Offset: 0x000211D6
		public float FadeInTime { get; set; } = 0.7f;

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00022FDF File Offset: 0x000211DF
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x00022FE7 File Offset: 0x000211E7
		public float StayTime { get; set; } = 3f;

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x00022FF0 File Offset: 0x000211F0
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x00022FF8 File Offset: 0x000211F8
		public float FadeOutTime { get; set; } = 0.7f;

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00023001 File Offset: 0x00021201
		private float CurrentAlpha
		{
			get
			{
				return base.AlphaFactor;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00023009 File Offset: 0x00021209
		// (set) Token: 0x06000CBF RID: 3263 RVA: 0x00023011 File Offset: 0x00021211
		public float TimeSinceCreation { get; private set; }

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0002301A File Offset: 0x0002121A
		public SingleplayerGeneralKillFeedItemWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00023050 File Offset: 0x00021250
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initialized)
			{
				this.MurdererTypeWidget.Sprite = this.MurdererTypeWidget.Context.SpriteData.GetSprite("General\\compass\\" + this._murdererType);
				this.VictimTypeWidget.Sprite = this.MurdererTypeWidget.Context.SpriteData.GetSprite("General\\compass\\" + this._victimType);
				this.ActionIconWidget.Sprite = this.ActionIconWidget.Context.SpriteData.GetSprite("General\\Mission\\PersonalKillfeed\\" + (this._isHeadshot ? "headshot_kill_icon" : "kill_feed_skull"));
				this.SetGlobalAlphaRecursively(0f);
				this.ActionIconWidget.Color = (this._isUnconscious ? new Color(1f, 1f, 1f, 1f) : new Color(1f, 0f, 0f, 1f));
				this.BackgroundWidget.Color = this._color;
				this.VictimNameWidget.Text = this._victimName;
				this.MurdererNameWidget.Text = this._murdererName;
				if (this._victimName.Length == 0)
				{
					this.ActionIconWidget.MarginRight = 0f;
					this.VictimTypeWidget.MarginLeft = 5f;
				}
				if (this._murdererName.Length == 0)
				{
					this.ActionIconWidget.MarginLeft = 0f;
					this.MurdererTypeWidget.MarginRight = 5f;
				}
				this._initialized = true;
			}
			this.TimeSinceCreation += dt * this._speedModifier;
			if (this.TimeSinceCreation <= this.FadeInTime)
			{
				this.SetGlobalAlphaRecursively(Mathf.Lerp(this.CurrentAlpha, 0.5f, this.TimeSinceCreation / this.FadeInTime));
				return;
			}
			if (this.TimeSinceCreation - this.FadeInTime <= this.StayTime)
			{
				this.SetGlobalAlphaRecursively(0.5f);
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

		// Token: 0x06000CC2 RID: 3266 RVA: 0x000232D6 File Offset: 0x000214D6
		public void SetSpeedModifier(float newSpeed)
		{
			if (newSpeed > this._speedModifier)
			{
				this._speedModifier = newSpeed;
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x000232E8 File Offset: 0x000214E8
		private void HandleMessage(string value)
		{
			string[] array = value.Split(new char[1]);
			this._murdererName = array[0];
			this._murdererType = array[1];
			this._victimName = array[2];
			this._victimType = array[3];
			this._isUnconscious = array[4].Equals("1");
			this._isHeadshot = array[5].Equals("1");
			this._color = Color.FromUint(uint.Parse(array[6]));
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0002335F File Offset: 0x0002155F
		// (set) Token: 0x06000CC5 RID: 3269 RVA: 0x00023367 File Offset: 0x00021567
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
					this.HandleMessage(value);
				}
			}
		}

		// Token: 0x040005C6 RID: 1478
		private const char _seperator = '\0';

		// Token: 0x040005CD RID: 1485
		private string _murdererType;

		// Token: 0x040005CE RID: 1486
		private string _victimType;

		// Token: 0x040005CF RID: 1487
		private string _murdererName;

		// Token: 0x040005D0 RID: 1488
		private string _victimName;

		// Token: 0x040005D1 RID: 1489
		private bool _isUnconscious;

		// Token: 0x040005D2 RID: 1490
		private bool _isHeadshot;

		// Token: 0x040005D3 RID: 1491
		private Color _color;

		// Token: 0x040005D7 RID: 1495
		private float _speedModifier = 1f;

		// Token: 0x040005D9 RID: 1497
		private bool _initialized;

		// Token: 0x040005DA RID: 1498
		private string _message;
	}
}

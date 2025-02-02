using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Information
{
	// Token: 0x02000139 RID: 313
	public class GameNotificationWidget : BrushWidget
	{
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x0002DDBA File Offset: 0x0002BFBA
		// (set) Token: 0x06001089 RID: 4233 RVA: 0x0002DDC2 File Offset: 0x0002BFC2
		public float RampUpInSeconds { get; set; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x0002DDCB File Offset: 0x0002BFCB
		// (set) Token: 0x0600108B RID: 4235 RVA: 0x0002DDD3 File Offset: 0x0002BFD3
		public float RampDownInSeconds { get; set; }

		// Token: 0x0600108C RID: 4236 RVA: 0x0002DDDC File Offset: 0x0002BFDC
		public GameNotificationWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x0002DDEC File Offset: 0x0002BFEC
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._textWidgetAlignmentDirty)
			{
				if (this.AnnouncerImageIdentifier.ImageTypeCode != 0)
				{
					this.TextWidget.Brush.TextHorizontalAlignment = TextHorizontalAlignment.Left;
				}
				else
				{
					this.TextWidget.Brush.TextHorizontalAlignment = TextHorizontalAlignment.Center;
				}
			}
			if (this.TotalTime > 0f && this._totalDt <= this.TotalTime)
			{
				if (this._totalDt <= this.RampUpInSeconds)
				{
					float alphaFactor = Mathf.Lerp(0f, 1f, this._totalDt / this.RampUpInSeconds);
					this.SetGlobalAlphaRecursively(alphaFactor);
				}
				else if (this._totalDt > this.RampUpInSeconds && this._totalDt < this.TotalTime - this.RampDownInSeconds)
				{
					this.SetGlobalAlphaRecursively(1f);
				}
				else if (this.TotalTime - this._totalDt < this.RampDownInSeconds)
				{
					float alphaFactor2 = Mathf.Lerp(1f, 0f, 1f - (this.TotalTime - this._totalDt) / this.RampUpInSeconds);
					this.SetGlobalAlphaRecursively(alphaFactor2);
				}
				else
				{
					this.SetGlobalAlphaRecursively(0f);
				}
				this._totalDt += dt;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x0002DF22 File Offset: 0x0002C122
		// (set) Token: 0x0600108F RID: 4239 RVA: 0x0002DF2A File Offset: 0x0002C12A
		public ImageIdentifierWidget AnnouncerImageIdentifier
		{
			get
			{
				return this._announcerImageIdentifier;
			}
			set
			{
				if (this._announcerImageIdentifier != value)
				{
					this._announcerImageIdentifier = value;
					base.OnPropertyChanged<ImageIdentifierWidget>(value, "AnnouncerImageIdentifier");
				}
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x0002DF48 File Offset: 0x0002C148
		// (set) Token: 0x06001091 RID: 4241 RVA: 0x0002DF50 File Offset: 0x0002C150
		public int NotificationId
		{
			get
			{
				return this._notificationId;
			}
			set
			{
				if (this._notificationId != value)
				{
					this._notificationId = value;
					base.OnPropertyChanged(value, "NotificationId");
					this._textWidgetAlignmentDirty = true;
					this._totalDt = 0f;
				}
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x0002DF80 File Offset: 0x0002C180
		// (set) Token: 0x06001093 RID: 4243 RVA: 0x0002DF88 File Offset: 0x0002C188
		public float TotalTime
		{
			get
			{
				return this._totalTime;
			}
			set
			{
				if (this._totalTime != value)
				{
					this._totalTime = value;
				}
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x0002DF9A File Offset: 0x0002C19A
		// (set) Token: 0x06001095 RID: 4245 RVA: 0x0002DFA2 File Offset: 0x0002C1A2
		public RichTextWidget TextWidget
		{
			get
			{
				return this._textWidget;
			}
			set
			{
				if (this._textWidget != value)
				{
					this._textWidget = value;
					base.OnPropertyChanged<RichTextWidget>(value, "TextWidget");
				}
			}
		}

		// Token: 0x04000788 RID: 1928
		private bool _textWidgetAlignmentDirty = true;

		// Token: 0x04000789 RID: 1929
		private float _totalDt;

		// Token: 0x0400078C RID: 1932
		private int _notificationId;

		// Token: 0x0400078D RID: 1933
		private RichTextWidget _textWidget;

		// Token: 0x0400078E RID: 1934
		private ImageIdentifierWidget _announcerImageIdentifier;

		// Token: 0x0400078F RID: 1935
		private float _totalTime;
	}
}

using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Quest
{
	// Token: 0x02000057 RID: 87
	public class QuestItemButtonWidget : ButtonWidget
	{
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000E705 File Offset: 0x0000C905
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x0000E70D File Offset: 0x0000C90D
		public Brush MainStoryLineItemBrush { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0000E716 File Offset: 0x0000C916
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x0000E71E File Offset: 0x0000C91E
		public Brush NormalItemBrush { get; set; }

		// Token: 0x0600049C RID: 1180 RVA: 0x0000E727 File Offset: 0x0000C927
		public QuestItemButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000E730 File Offset: 0x0000C930
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (!this._initialized)
			{
				base.Brush = (this.IsMainStoryLineQuest ? this.MainStoryLineItemBrush : this.NormalItemBrush);
				this._initialized = true;
			}
			if (this.QuestNameText != null && this.QuestDateText != null)
			{
				if (base.CurrentState == "Pressed")
				{
					this.QuestNameText.PositionYOffset = (float)this.QuestNameYOffset;
					this.QuestNameText.PositionXOffset = (float)this.QuestNameXOffset;
					this.QuestDateText.PositionYOffset = (float)this.QuestDateYOffset;
					this.QuestDateText.PositionXOffset = (float)this.QuestDateXOffset;
				}
				else
				{
					this.QuestNameText.PositionYOffset = 0f;
					this.QuestNameText.PositionXOffset = 0f;
					this.QuestDateText.PositionYOffset = 0f;
					this.QuestDateText.PositionXOffset = 0f;
				}
			}
			if (this.QuestDateText != null)
			{
				if (this.IsCompleted)
				{
					this.QuestDateText.IsVisible = false;
					return;
				}
				this.QuestDateText.IsHidden = this.IsRemainingDaysHidden;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000E84F File Offset: 0x0000CA4F
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x0000E857 File Offset: 0x0000CA57
		[Editor(false)]
		public bool IsCompleted
		{
			get
			{
				return this._isCompleted;
			}
			set
			{
				if (this._isCompleted != value)
				{
					this._isCompleted = value;
					base.OnPropertyChanged(value, "IsCompleted");
				}
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0000E875 File Offset: 0x0000CA75
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x0000E87D File Offset: 0x0000CA7D
		[Editor(false)]
		public bool IsMainStoryLineQuest
		{
			get
			{
				return this._isMainStoryLineQuest;
			}
			set
			{
				if (this._isMainStoryLineQuest != value)
				{
					this._isMainStoryLineQuest = value;
					base.OnPropertyChanged(value, "IsMainStoryLineQuest");
				}
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000E89B File Offset: 0x0000CA9B
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x0000E8A3 File Offset: 0x0000CAA3
		[Editor(false)]
		public bool IsRemainingDaysHidden
		{
			get
			{
				return this._isRemainingDaysHidden;
			}
			set
			{
				if (this._isRemainingDaysHidden != value)
				{
					this._isRemainingDaysHidden = value;
					base.OnPropertyChanged(value, "IsRemainingDaysHidden");
				}
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0000E8C1 File Offset: 0x0000CAC1
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x0000E8C9 File Offset: 0x0000CAC9
		[Editor(false)]
		public TextWidget QuestNameText
		{
			get
			{
				return this._questNameText;
			}
			set
			{
				if (this._questNameText != value)
				{
					this._questNameText = value;
					base.OnPropertyChanged<TextWidget>(value, "QuestNameText");
				}
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0000E8E7 File Offset: 0x0000CAE7
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x0000E8EF File Offset: 0x0000CAEF
		[Editor(false)]
		public TextWidget QuestDateText
		{
			get
			{
				return this._questDateText;
			}
			set
			{
				if (this._questDateText != value)
				{
					this._questDateText = value;
					base.OnPropertyChanged<TextWidget>(value, "QuestDateText");
				}
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000E90D File Offset: 0x0000CB0D
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x0000E915 File Offset: 0x0000CB15
		[Editor(false)]
		public int QuestNameYOffset
		{
			get
			{
				return this._questNameYOffset;
			}
			set
			{
				if (this._questNameYOffset != value)
				{
					this._questNameYOffset = value;
					base.OnPropertyChanged(value, "QuestNameYOffset");
				}
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000E933 File Offset: 0x0000CB33
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x0000E93B File Offset: 0x0000CB3B
		[Editor(false)]
		public int QuestNameXOffset
		{
			get
			{
				return this._questNameXOffset;
			}
			set
			{
				if (this._questNameXOffset != value)
				{
					this._questNameXOffset = value;
					base.OnPropertyChanged(value, "QuestNameXOffset");
				}
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000E959 File Offset: 0x0000CB59
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x0000E961 File Offset: 0x0000CB61
		[Editor(false)]
		public int QuestDateYOffset
		{
			get
			{
				return this._questDateYOffset;
			}
			set
			{
				if (this._questDateYOffset != value)
				{
					this._questDateYOffset = value;
					base.OnPropertyChanged(value, "QuestDateYOffset");
				}
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0000E97F File Offset: 0x0000CB7F
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x0000E987 File Offset: 0x0000CB87
		[Editor(false)]
		public int QuestDateXOffset
		{
			get
			{
				return this._questDateXOffset;
			}
			set
			{
				if (this._questDateXOffset != value)
				{
					this._questDateXOffset = value;
					base.OnPropertyChanged(value, "QuestDateXOffset");
				}
			}
		}

		// Token: 0x040001FE RID: 510
		private bool _initialized;

		// Token: 0x04000201 RID: 513
		private TextWidget _questNameText;

		// Token: 0x04000202 RID: 514
		private TextWidget _questDateText;

		// Token: 0x04000203 RID: 515
		private int _questNameYOffset;

		// Token: 0x04000204 RID: 516
		private int _questNameXOffset;

		// Token: 0x04000205 RID: 517
		private int _questDateYOffset;

		// Token: 0x04000206 RID: 518
		private int _questDateXOffset;

		// Token: 0x04000207 RID: 519
		private bool _isCompleted;

		// Token: 0x04000208 RID: 520
		private bool _isRemainingDaysHidden;

		// Token: 0x04000209 RID: 521
		private bool _isMainStoryLineQuest;
	}
}

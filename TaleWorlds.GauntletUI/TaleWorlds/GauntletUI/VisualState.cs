using System;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000034 RID: 52
	public class VisualState
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000EFE5 File Offset: 0x0000D1E5
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0000EFED File Offset: 0x0000D1ED
		public string State { get; private set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000EFF6 File Offset: 0x0000D1F6
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0000EFFE File Offset: 0x0000D1FE
		public float TransitionDuration
		{
			get
			{
				return this._transitionDuration;
			}
			set
			{
				this._transitionDuration = value;
				this.GotTransitionDuration = true;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000F00E File Offset: 0x0000D20E
		// (set) Token: 0x06000385 RID: 901 RVA: 0x0000F016 File Offset: 0x0000D216
		public float PositionXOffset
		{
			get
			{
				return this._positionXOffset;
			}
			set
			{
				this._positionXOffset = value;
				this.GotPositionXOffset = true;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000F026 File Offset: 0x0000D226
		// (set) Token: 0x06000387 RID: 903 RVA: 0x0000F02E File Offset: 0x0000D22E
		public float PositionYOffset
		{
			get
			{
				return this._positionYOffset;
			}
			set
			{
				this._positionYOffset = value;
				this.GotPositionYOffset = true;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000F03E File Offset: 0x0000D23E
		// (set) Token: 0x06000389 RID: 905 RVA: 0x0000F046 File Offset: 0x0000D246
		public float SuggestedWidth
		{
			get
			{
				return this._suggestedWidth;
			}
			set
			{
				this._suggestedWidth = value;
				this.GotSuggestedWidth = true;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000F056 File Offset: 0x0000D256
		// (set) Token: 0x0600038B RID: 907 RVA: 0x0000F05E File Offset: 0x0000D25E
		public float SuggestedHeight
		{
			get
			{
				return this._suggestedHeight;
			}
			set
			{
				this._suggestedHeight = value;
				this.GotSuggestedHeight = true;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000F06E File Offset: 0x0000D26E
		// (set) Token: 0x0600038D RID: 909 RVA: 0x0000F076 File Offset: 0x0000D276
		public float MarginTop
		{
			get
			{
				return this._marginTop;
			}
			set
			{
				this._marginTop = value;
				this.GotMarginTop = true;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000F086 File Offset: 0x0000D286
		// (set) Token: 0x0600038F RID: 911 RVA: 0x0000F08E File Offset: 0x0000D28E
		public float MarginBottom
		{
			get
			{
				return this._marginBottom;
			}
			set
			{
				this._marginBottom = value;
				this.GotMarginBottom = true;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000F09E File Offset: 0x0000D29E
		// (set) Token: 0x06000391 RID: 913 RVA: 0x0000F0A6 File Offset: 0x0000D2A6
		public float MarginLeft
		{
			get
			{
				return this._marginLeft;
			}
			set
			{
				this._marginLeft = value;
				this.GotMarginLeft = true;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000F0B6 File Offset: 0x0000D2B6
		// (set) Token: 0x06000393 RID: 915 RVA: 0x0000F0BE File Offset: 0x0000D2BE
		public float MarginRight
		{
			get
			{
				return this._marginRight;
			}
			set
			{
				this._marginRight = value;
				this.GotMarginRight = true;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		// (set) Token: 0x06000395 RID: 917 RVA: 0x0000F0D6 File Offset: 0x0000D2D6
		public bool GotTransitionDuration { get; private set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000F0DF File Offset: 0x0000D2DF
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0000F0E7 File Offset: 0x0000D2E7
		public bool GotPositionXOffset { get; private set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000F0F0 File Offset: 0x0000D2F0
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0000F0F8 File Offset: 0x0000D2F8
		public bool GotPositionYOffset { get; private set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000F101 File Offset: 0x0000D301
		// (set) Token: 0x0600039B RID: 923 RVA: 0x0000F109 File Offset: 0x0000D309
		public bool GotSuggestedWidth { get; private set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000F112 File Offset: 0x0000D312
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0000F11A File Offset: 0x0000D31A
		public bool GotSuggestedHeight { get; private set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000F123 File Offset: 0x0000D323
		// (set) Token: 0x0600039F RID: 927 RVA: 0x0000F12B File Offset: 0x0000D32B
		public bool GotMarginTop { get; private set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000F134 File Offset: 0x0000D334
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0000F13C File Offset: 0x0000D33C
		public bool GotMarginBottom { get; private set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000F145 File Offset: 0x0000D345
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x0000F14D File Offset: 0x0000D34D
		public bool GotMarginLeft { get; private set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000F156 File Offset: 0x0000D356
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x0000F15E File Offset: 0x0000D35E
		public bool GotMarginRight { get; private set; }

		// Token: 0x060003A6 RID: 934 RVA: 0x0000F167 File Offset: 0x0000D367
		public VisualState(string state)
		{
			this.State = state;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000F178 File Offset: 0x0000D378
		public void FillFromWidget(Widget widget)
		{
			this.PositionXOffset = widget.PositionXOffset;
			this.PositionYOffset = widget.PositionYOffset;
			this.SuggestedWidth = widget.SuggestedWidth;
			this.SuggestedHeight = widget.SuggestedHeight;
			this.MarginTop = widget.MarginTop;
			this.MarginBottom = widget.MarginBottom;
			this.MarginLeft = widget.MarginLeft;
			this.MarginRight = widget.MarginRight;
		}

		// Token: 0x040001B7 RID: 439
		private float _transitionDuration;

		// Token: 0x040001B8 RID: 440
		private float _positionXOffset;

		// Token: 0x040001B9 RID: 441
		private float _positionYOffset;

		// Token: 0x040001BA RID: 442
		private float _suggestedWidth;

		// Token: 0x040001BB RID: 443
		private float _suggestedHeight;

		// Token: 0x040001BC RID: 444
		private float _marginTop;

		// Token: 0x040001BD RID: 445
		private float _marginBottom;

		// Token: 0x040001BE RID: 446
		private float _marginLeft;

		// Token: 0x040001BF RID: 447
		private float _marginRight;
	}
}

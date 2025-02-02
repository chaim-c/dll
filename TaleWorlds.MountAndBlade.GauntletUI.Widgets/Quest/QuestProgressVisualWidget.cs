using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Quest
{
	// Token: 0x02000059 RID: 89
	public class QuestProgressVisualWidget : Widget
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000EA8D File Offset: 0x0000CC8D
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x0000EA95 File Offset: 0x0000CC95
		public Widget BarWidget { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000EA9E File Offset: 0x0000CC9E
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x0000EAA6 File Offset: 0x0000CCA6
		public Widget SliderWidget { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0000EAAF File Offset: 0x0000CCAF
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x0000EAB7 File Offset: 0x0000CCB7
		public Widget CheckboxVisualWidget { get; set; }

		// Token: 0x060004BA RID: 1210 RVA: 0x0000EAC0 File Offset: 0x0000CCC0
		public QuestProgressVisualWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000EACC File Offset: 0x0000CCCC
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initialized)
			{
				bool flag = this.CurrentProgress >= this.TargetProgress;
				base.IsVisible = (!flag && this.IsValid);
				this.CheckboxVisualWidget.IsVisible = (flag && this.IsValid);
				this.BarWidget.IsVisible = false;
				this.SliderWidget.IsVisible = false;
				if (base.IsVisible)
				{
					if (this.TargetProgress < 20)
					{
						for (int i = 0; i < this.TargetProgress; i++)
						{
							BrushWidget brushWidget = new BrushWidget(base.Context)
							{
								WidthSizePolicy = SizePolicy.Fixed,
								SuggestedWidth = this.ProgressStoneWidth,
								HeightSizePolicy = SizePolicy.Fixed,
								SuggestedHeight = this.ProgressStoneHeight,
								MarginRight = (float)this.HorizontalSpacingBetweenStones / 2f,
								MarginLeft = (float)this.HorizontalSpacingBetweenStones / 2f,
								IsEnabled = false
							};
							if (i < this.CurrentProgress)
							{
								brushWidget.Brush = base.Context.GetBrush("StageTask.ProgressStone");
								brushWidget.Brush.AlphaFactor = 0.8f;
							}
							this.BarWidget.AddChild(brushWidget);
						}
						this.BarWidget.IsVisible = true;
					}
					else if (this.TargetProgress >= 20)
					{
						this.SliderWidget.IsVisible = true;
						this.SliderWidget.IsDisabled = true;
					}
				}
				this._initialized = true;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0000EC3E File Offset: 0x0000CE3E
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x0000EC46 File Offset: 0x0000CE46
		public bool IsValid
		{
			get
			{
				return this._isValid;
			}
			set
			{
				if (this._isValid != value)
				{
					this._isValid = value;
				}
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0000EC58 File Offset: 0x0000CE58
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x0000EC60 File Offset: 0x0000CE60
		public float ProgressStoneWidth
		{
			get
			{
				return this._progressStoneWidth;
			}
			set
			{
				if (this._progressStoneWidth != value)
				{
					this._progressStoneWidth = value;
				}
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000EC72 File Offset: 0x0000CE72
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x0000EC7A File Offset: 0x0000CE7A
		public float ProgressStoneHeight
		{
			get
			{
				return this._progressStoneHeight;
			}
			set
			{
				if (this._progressStoneHeight != value)
				{
					this._progressStoneHeight = value;
				}
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0000EC8C File Offset: 0x0000CE8C
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0000EC94 File Offset: 0x0000CE94
		public int CurrentProgress
		{
			get
			{
				return this._currentProgress;
			}
			set
			{
				if (this._currentProgress != value)
				{
					this._currentProgress = value;
				}
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0000ECA6 File Offset: 0x0000CEA6
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x0000ECAE File Offset: 0x0000CEAE
		public int TargetProgress
		{
			get
			{
				return this._targetProgress;
			}
			set
			{
				if (this._targetProgress != value)
				{
					this._targetProgress = value;
				}
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0000ECC0 File Offset: 0x0000CEC0
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x0000ECC8 File Offset: 0x0000CEC8
		public int HorizontalSpacingBetweenStones
		{
			get
			{
				return this._horizontalSpacingBetweenStones;
			}
			set
			{
				if (this._horizontalSpacingBetweenStones != value)
				{
					this._horizontalSpacingBetweenStones = value;
				}
			}
		}

		// Token: 0x0400020B RID: 523
		private bool _initialized;

		// Token: 0x0400020F RID: 527
		private int _currentProgress;

		// Token: 0x04000210 RID: 528
		private int _targetProgress;

		// Token: 0x04000211 RID: 529
		private float _progressStoneWidth;

		// Token: 0x04000212 RID: 530
		private float _progressStoneHeight;

		// Token: 0x04000213 RID: 531
		private int _horizontalSpacingBetweenStones;

		// Token: 0x04000214 RID: 532
		private bool _isValid;
	}
}

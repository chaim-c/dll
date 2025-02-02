using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Conversation
{
	// Token: 0x02000166 RID: 358
	public class PersuasionResultChanceContainerListPanel : BrushListPanel
	{
		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x00033022 File Offset: 0x00031222
		// (set) Token: 0x060012AA RID: 4778 RVA: 0x0003302A File Offset: 0x0003122A
		public float StayTime { get; set; } = 1f;

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x060012AB RID: 4779 RVA: 0x00033033 File Offset: 0x00031233
		// (set) Token: 0x060012AC RID: 4780 RVA: 0x0003303B File Offset: 0x0003123B
		public Widget CritFailWidget { get; set; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x00033044 File Offset: 0x00031244
		// (set) Token: 0x060012AE RID: 4782 RVA: 0x0003304C File Offset: 0x0003124C
		public Widget FailWidget { get; set; }

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x00033055 File Offset: 0x00031255
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x0003305D File Offset: 0x0003125D
		public Widget SuccessWidget { get; set; }

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x00033066 File Offset: 0x00031266
		// (set) Token: 0x060012B2 RID: 4786 RVA: 0x0003306E File Offset: 0x0003126E
		public Widget CritSuccessWidget { get; set; }

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x00033077 File Offset: 0x00031277
		// (set) Token: 0x060012B4 RID: 4788 RVA: 0x0003307F File Offset: 0x0003127F
		public bool IsResultReady { get; set; }

		// Token: 0x060012B5 RID: 4789 RVA: 0x00033088 File Offset: 0x00031288
		public PersuasionResultChanceContainerListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x000330A8 File Offset: 0x000312A8
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this.IsResultReady)
			{
				if (this._delayStartTime == -1f && base.AlphaFactor <= 0.001f)
				{
					this._delayStartTime = base.EventManager.Time;
				}
				float alphaFactor = Mathf.Lerp(base.AlphaFactor, 0f, 0.35f);
				this.SetGlobalAlphaRecursively(alphaFactor);
				Widget resultVisualWidget = this._resultVisualWidget;
				if (resultVisualWidget != null)
				{
					resultVisualWidget.SetGlobalAlphaRecursively(1f);
				}
				if (this._delayStartTime != -1f && base.EventManager.Time - this._delayStartTime > this.StayTime)
				{
					base.EventFired("OnReadyToContinue", Array.Empty<object>());
				}
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x0003315C File Offset: 0x0003135C
		// (set) Token: 0x060012B8 RID: 4792 RVA: 0x00033164 File Offset: 0x00031364
		[Editor(false)]
		public int ResultIndex
		{
			get
			{
				return this._resultIndex;
			}
			set
			{
				if (value != this._resultIndex)
				{
					this._resultIndex = value;
					base.OnPropertyChanged(value, "ResultIndex");
					switch (value)
					{
					case 0:
						this._resultVisualWidget = this.CritFailWidget;
						this.SetState("CriticalFail");
						return;
					case 1:
						this._resultVisualWidget = this.FailWidget;
						this.SetState("Fail");
						return;
					case 2:
						this._resultVisualWidget = this.SuccessWidget;
						this.SetState("Success");
						return;
					case 3:
						this._resultVisualWidget = this.CritSuccessWidget;
						this.SetState("CriticalSuccess");
						break;
					default:
						return;
					}
				}
			}
		}

		// Token: 0x04000883 RID: 2179
		private Widget _resultVisualWidget;

		// Token: 0x04000885 RID: 2181
		private float _delayStartTime = -1f;

		// Token: 0x04000886 RID: 2182
		private int _resultIndex;
	}
}

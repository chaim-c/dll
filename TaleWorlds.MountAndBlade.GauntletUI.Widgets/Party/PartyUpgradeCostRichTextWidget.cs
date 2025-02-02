using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Party
{
	// Token: 0x02000065 RID: 101
	public class PartyUpgradeCostRichTextWidget : RichTextWidget
	{
		// Token: 0x06000573 RID: 1395 RVA: 0x000108EC File Offset: 0x0000EAEC
		public PartyUpgradeCostRichTextWidget(UIContext context) : base(context)
		{
			this.NormalColor = new Color(1f, 1f, 1f, 1f);
			this.InsufficientColor = new Color(0.753f, 0.071f, 0.098f, 1f);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00010945 File Offset: 0x0000EB45
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._requiresRefresh)
			{
				base.Brush.FontColor = (this.IsSufficient ? this.NormalColor : this.InsufficientColor);
				this._requiresRefresh = false;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0001097E File Offset: 0x0000EB7E
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x00010986 File Offset: 0x0000EB86
		[Editor(false)]
		public bool IsSufficient
		{
			get
			{
				return this._isSufficient;
			}
			set
			{
				if (value != this._isSufficient)
				{
					this._isSufficient = value;
					base.OnPropertyChanged(value, "IsSufficient");
					this._requiresRefresh = true;
				}
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x000109AB File Offset: 0x0000EBAB
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x000109B3 File Offset: 0x0000EBB3
		public Color NormalColor
		{
			get
			{
				return this._normalColor;
			}
			set
			{
				if (value != this._normalColor)
				{
					this._normalColor = value;
					this._requiresRefresh = true;
				}
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x000109D1 File Offset: 0x0000EBD1
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x000109D9 File Offset: 0x0000EBD9
		public Color InsufficientColor
		{
			get
			{
				return this._insufficientColor;
			}
			set
			{
				if (value != this._insufficientColor)
				{
					this._insufficientColor = value;
					this._requiresRefresh = true;
				}
			}
		}

		// Token: 0x0400025B RID: 603
		private bool _requiresRefresh = true;

		// Token: 0x0400025C RID: 604
		private bool _isSufficient;

		// Token: 0x0400025D RID: 605
		private Color _normalColor;

		// Token: 0x0400025E RID: 606
		private Color _insufficientColor;
	}
}

using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Options.Gamepad
{
	// Token: 0x02000074 RID: 116
	public class OptionsGamepadOptionItemListPanel : ListPanel
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000655 RID: 1621 RVA: 0x00012B14 File Offset: 0x00010D14
		// (remove) Token: 0x06000656 RID: 1622 RVA: 0x00012B4C File Offset: 0x00010D4C
		public event OptionsGamepadOptionItemListPanel.OnActionTextChangeEvent OnActionTextChanged;

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x00012B81 File Offset: 0x00010D81
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x00012B89 File Offset: 0x00010D89
		public OptionsGamepadKeyLocationWidget TargetKey { get; private set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00012B92 File Offset: 0x00010D92
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x00012B9A File Offset: 0x00010D9A
		public string ActionText
		{
			get
			{
				return this._actionText;
			}
			set
			{
				if (this._actionText != value)
				{
					this._actionText = value;
					OptionsGamepadOptionItemListPanel.OnActionTextChangeEvent onActionTextChanged = this.OnActionTextChanged;
					if (onActionTextChanged == null)
					{
						return;
					}
					onActionTextChanged();
				}
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00012BC1 File Offset: 0x00010DC1
		public OptionsGamepadOptionItemListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00012BCA File Offset: 0x00010DCA
		public void SetKeyProperties(OptionsGamepadKeyLocationWidget currentTarget, Widget parentAreaWidget)
		{
			this.TargetKey = currentTarget;
			this.TargetKey.SetKeyProperties(this.ActionText, parentAreaWidget);
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00012BE5 File Offset: 0x00010DE5
		// (set) Token: 0x0600065E RID: 1630 RVA: 0x00012BED File Offset: 0x00010DED
		public int KeyId
		{
			get
			{
				return this._keyId;
			}
			set
			{
				if (value != this._keyId)
				{
					this._keyId = value;
				}
			}
		}

		// Token: 0x040002C1 RID: 705
		private string _actionText;

		// Token: 0x040002C2 RID: 706
		private int _keyId;

		// Token: 0x0200019F RID: 415
		// (Invoke) Token: 0x06001460 RID: 5216
		public delegate void OnActionTextChangeEvent();
	}
}

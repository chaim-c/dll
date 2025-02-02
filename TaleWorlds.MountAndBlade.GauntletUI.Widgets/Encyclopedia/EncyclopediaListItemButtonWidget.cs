using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Encyclopedia
{
	// Token: 0x0200014E RID: 334
	public class EncyclopediaListItemButtonWidget : ButtonWidget
	{
		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060011AC RID: 4524 RVA: 0x0003111A File Offset: 0x0002F31A
		// (set) Token: 0x060011AD RID: 4525 RVA: 0x00031122 File Offset: 0x0002F322
		public TextWidget ListItemNameTextWidget { get; set; }

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x0003112B File Offset: 0x0002F32B
		// (set) Token: 0x060011AF RID: 4527 RVA: 0x00031133 File Offset: 0x0002F333
		public TextWidget ListComparedValueTextWidget { get; set; }

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0003113C File Offset: 0x0002F33C
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x00031144 File Offset: 0x0002F344
		public Brush InfoAvailableItemNameBrush { get; set; }

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0003114D File Offset: 0x0002F34D
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x00031155 File Offset: 0x0002F355
		public Brush InfoUnvailableItemNameBrush { get; set; }

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0003115E File Offset: 0x0002F35E
		// (set) Token: 0x060011B5 RID: 4533 RVA: 0x00031166 File Offset: 0x0002F366
		public bool IsInfoAvailable { get; set; }

		// Token: 0x060011B6 RID: 4534 RVA: 0x0003116F File Offset: 0x0002F36F
		public EncyclopediaListItemButtonWidget(UIContext context) : base(context)
		{
			base.EventManager.AddLateUpdateAction(this, new Action<float>(this.OnThisLateUpdate), 1);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00031194 File Offset: 0x0002F394
		public void OnThisLateUpdate(float dt)
		{
			this.ListItemNameTextWidget.Brush = (this.IsInfoAvailable ? this.InfoAvailableItemNameBrush : this.InfoUnvailableItemNameBrush);
			this.ListComparedValueTextWidget.Brush = (this.IsInfoAvailable ? this.InfoAvailableItemNameBrush : this.InfoUnvailableItemNameBrush);
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x000311E3 File Offset: 0x0002F3E3
		// (set) Token: 0x060011B9 RID: 4537 RVA: 0x000311EB File Offset: 0x0002F3EB
		[Editor(false)]
		public string ListItemId
		{
			get
			{
				return this._listItemId;
			}
			set
			{
				if (this._listItemId != value)
				{
					this._listItemId = value;
					base.OnPropertyChanged<string>(value, "ListItemId");
				}
			}
		}

		// Token: 0x04000818 RID: 2072
		private string _listItemId;
	}
}

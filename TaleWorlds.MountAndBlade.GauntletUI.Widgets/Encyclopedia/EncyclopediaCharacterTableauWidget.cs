using System;
using TaleWorlds.GauntletUI;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Encyclopedia
{
	// Token: 0x0200014A RID: 330
	public class EncyclopediaCharacterTableauWidget : CharacterTableauWidget
	{
		// Token: 0x06001196 RID: 4502 RVA: 0x00030DE8 File Offset: 0x0002EFE8
		public EncyclopediaCharacterTableauWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00030DF1 File Offset: 0x0002EFF1
		private void UpdateVisual(bool isDead)
		{
			base.Brush.SaturationFactor = (float)(isDead ? -100 : 0);
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x00030E07 File Offset: 0x0002F007
		// (set) Token: 0x06001199 RID: 4505 RVA: 0x00030E0F File Offset: 0x0002F00F
		[Editor(false)]
		public bool IsDead
		{
			get
			{
				return this._isDead;
			}
			set
			{
				if (this._isDead != value)
				{
					this._isDead = value;
					base.OnPropertyChanged(value, "IsDead");
					this.UpdateVisual(value);
				}
			}
		}

		// Token: 0x0400080E RID: 2062
		private bool _isDead;
	}
}

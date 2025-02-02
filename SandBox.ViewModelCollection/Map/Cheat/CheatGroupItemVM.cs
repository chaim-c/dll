using System;
using TaleWorlds.Localization;

namespace SandBox.ViewModelCollection.Map.Cheat
{
	// Token: 0x0200002F RID: 47
	public class CheatGroupItemVM : CheatItemBaseVM
	{
		// Token: 0x0600038C RID: 908 RVA: 0x00010EE8 File Offset: 0x0000F0E8
		public CheatGroupItemVM(GameplayCheatGroup cheatGroup, Action<CheatGroupItemVM> onSelectCheatGroup)
		{
			this.CheatGroup = cheatGroup;
			this._onSelectCheatGroup = onSelectCheatGroup;
			this.RefreshValues();
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00010F04 File Offset: 0x0000F104
		public override void RefreshValues()
		{
			base.RefreshValues();
			TextObject name = this.CheatGroup.GetName();
			base.Name = ((name != null) ? name.ToString() : null);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00010F29 File Offset: 0x0000F129
		public override void ExecuteAction()
		{
			Action<CheatGroupItemVM> onSelectCheatGroup = this._onSelectCheatGroup;
			if (onSelectCheatGroup == null)
			{
				return;
			}
			onSelectCheatGroup(this);
		}

		// Token: 0x040001DE RID: 478
		public readonly GameplayCheatGroup CheatGroup;

		// Token: 0x040001DF RID: 479
		private readonly Action<CheatGroupItemVM> _onSelectCheatGroup;
	}
}

using System;

namespace SandBox.ViewModelCollection.Map.Cheat
{
	// Token: 0x0200002E RID: 46
	public class CheatActionItemVM : CheatItemBaseVM
	{
		// Token: 0x06000389 RID: 905 RVA: 0x00010E83 File Offset: 0x0000F083
		public CheatActionItemVM(GameplayCheatItem cheat, Action<CheatActionItemVM> onCheatExecuted)
		{
			this._onCheatExecuted = onCheatExecuted;
			this.Cheat = cheat;
			this.RefreshValues();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00010E9F File Offset: 0x0000F09F
		public override void RefreshValues()
		{
			base.RefreshValues();
			GameplayCheatItem cheat = this.Cheat;
			base.Name = ((cheat != null) ? cheat.GetName().ToString() : null);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00010EC4 File Offset: 0x0000F0C4
		public override void ExecuteAction()
		{
			GameplayCheatItem cheat = this.Cheat;
			if (cheat != null)
			{
				cheat.ExecuteCheat();
			}
			Action<CheatActionItemVM> onCheatExecuted = this._onCheatExecuted;
			if (onCheatExecuted == null)
			{
				return;
			}
			onCheatExecuted(this);
		}

		// Token: 0x040001DC RID: 476
		public readonly GameplayCheatItem Cheat;

		// Token: 0x040001DD RID: 477
		private readonly Action<CheatActionItemVM> _onCheatExecuted;
	}
}

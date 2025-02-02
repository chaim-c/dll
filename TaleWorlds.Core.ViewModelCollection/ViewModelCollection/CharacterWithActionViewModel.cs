using System;

namespace TaleWorlds.Core.ViewModelCollection
{
	// Token: 0x0200000A RID: 10
	public class CharacterWithActionViewModel : CharacterViewModel
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public CharacterWithActionViewModel(Action onAction)
		{
			this._onAction = onAction;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002AE3 File Offset: 0x00000CE3
		private void ExecuteAction()
		{
			Action onAction = this._onAction;
			if (onAction == null)
			{
				return;
			}
			onAction();
		}

		// Token: 0x04000021 RID: 33
		private Action _onAction;
	}
}

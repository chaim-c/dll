using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.ClassLoadout
{
	// Token: 0x020000C5 RID: 197
	public class MultiplayerClassLoadoutItemTabListPanel : ListPanel
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000A5A RID: 2650 RVA: 0x0001D6D4 File Offset: 0x0001B8D4
		// (remove) Token: 0x06000A5B RID: 2651 RVA: 0x0001D70C File Offset: 0x0001B90C
		public event Action OnInitialized;

		// Token: 0x06000A5C RID: 2652 RVA: 0x0001D741 File Offset: 0x0001B941
		public MultiplayerClassLoadoutItemTabListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0001D74A File Offset: 0x0001B94A
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._isInitialized)
			{
				this._isInitialized = true;
				Action onInitialized = this.OnInitialized;
				if (onInitialized == null)
				{
					return;
				}
				onInitialized();
			}
		}

		// Token: 0x040004C0 RID: 1216
		private bool _isInitialized;
	}
}

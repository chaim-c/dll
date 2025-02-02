using System;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x02000344 RID: 836
	public class TutorialState : GameState
	{
		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06002F25 RID: 12069 RVA: 0x000C2688 File Offset: 0x000C0888
		public override bool IsMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x000C26AE File Offset: 0x000C08AE
		protected override void OnActivate()
		{
			base.OnActivate();
			this.MenuContext.Refresh();
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x000C26C1 File Offset: 0x000C08C1
		protected override void OnFinalize()
		{
			this.MenuContext.Destroy();
			this._objectManager.UnregisterObject(this.MenuContext);
			this.MenuContext = null;
			base.OnFinalize();
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x000C26EC File Offset: 0x000C08EC
		protected override void OnTick(float dt)
		{
			base.OnTick(dt);
			this.MenuContext.OnTick(dt);
		}

		// Token: 0x04000E04 RID: 3588
		private MBObjectManager _objectManager = MBObjectManager.Instance;

		// Token: 0x04000E05 RID: 3589
		public MenuContext MenuContext = MBObjectManager.Instance.CreateObject<MenuContext>();
	}
}

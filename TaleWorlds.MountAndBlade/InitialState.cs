using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000239 RID: 569
	public class InitialState : GameState
	{
		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001EF7 RID: 7927 RVA: 0x0006E468 File Offset: 0x0006C668
		public override bool IsMusicMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06001EF8 RID: 7928 RVA: 0x0006E46C File Offset: 0x0006C66C
		// (remove) Token: 0x06001EF9 RID: 7929 RVA: 0x0006E4A4 File Offset: 0x0006C6A4
		public event OnInitialMenuOptionInvokedDelegate OnInitialMenuOptionInvoked;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06001EFA RID: 7930 RVA: 0x0006E4DC File Offset: 0x0006C6DC
		// (remove) Token: 0x06001EFB RID: 7931 RVA: 0x0006E514 File Offset: 0x0006C714
		public event OnGameContentUpdatedDelegate OnGameContentUpdated;

		// Token: 0x06001EFC RID: 7932 RVA: 0x0006E549 File Offset: 0x0006C749
		protected override void OnActivate()
		{
			base.OnActivate();
			MBMusicManager mbmusicManager = MBMusicManager.Current;
			if (mbmusicManager == null)
			{
				return;
			}
			mbmusicManager.UnpauseMusicManagerSystem();
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x0006E560 File Offset: 0x0006C760
		protected override void OnTick(float dt)
		{
			base.OnTick(dt);
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x0006E569 File Offset: 0x0006C769
		public void OnExecutedInitialStateOption(InitialStateOption target)
		{
			OnInitialMenuOptionInvokedDelegate onInitialMenuOptionInvoked = this.OnInitialMenuOptionInvoked;
			if (onInitialMenuOptionInvoked == null)
			{
				return;
			}
			onInitialMenuOptionInvoked(target);
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x0006E57C File Offset: 0x0006C77C
		public void RefreshContentState()
		{
			OnGameContentUpdatedDelegate onGameContentUpdated = this.OnGameContentUpdated;
			if (onGameContentUpdated == null)
			{
				return;
			}
			onGameContentUpdated();
		}
	}
}

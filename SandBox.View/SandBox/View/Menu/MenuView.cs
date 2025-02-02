using System;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Overlay;

namespace SandBox.View.Menu
{
	// Token: 0x02000030 RID: 48
	public abstract class MenuView : SandboxView
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000118D7 File Offset: 0x0000FAD7
		// (set) Token: 0x06000173 RID: 371 RVA: 0x000118DF File Offset: 0x0000FADF
		internal bool Removed { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000118E8 File Offset: 0x0000FAE8
		public virtual bool ShouldUpdateMenuAfterRemoved
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000118EB File Offset: 0x0000FAEB
		// (set) Token: 0x06000176 RID: 374 RVA: 0x000118F3 File Offset: 0x0000FAF3
		public MenuViewContext MenuViewContext { get; internal set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000118FC File Offset: 0x0000FAFC
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00011904 File Offset: 0x0000FB04
		public MenuContext MenuContext { get; internal set; }

		// Token: 0x06000179 RID: 377 RVA: 0x0001190D File Offset: 0x0000FB0D
		protected internal virtual void OnMenuContextUpdated(MenuContext newMenuContext)
		{
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0001190F File Offset: 0x0000FB0F
		protected internal virtual void OnOverlayTypeChange(GameOverlays.MenuOverlayType newType)
		{
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00011911 File Offset: 0x0000FB11
		protected internal virtual void OnCharacterDeveloperOpened()
		{
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00011913 File Offset: 0x0000FB13
		protected internal virtual void OnCharacterDeveloperClosed()
		{
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00011915 File Offset: 0x0000FB15
		protected internal virtual void OnBackgroundMeshNameSet(string name)
		{
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00011917 File Offset: 0x0000FB17
		protected internal virtual void OnHourlyTick()
		{
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00011919 File Offset: 0x0000FB19
		protected internal virtual void OnResume()
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0001191B File Offset: 0x0000FB1B
		protected internal virtual void OnMapConversationActivated()
		{
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0001191D File Offset: 0x0000FB1D
		protected internal virtual void OnMapConversationDeactivated()
		{
		}

		// Token: 0x040000E3 RID: 227
		protected const float ContextAlphaModifier = 8.5f;
	}
}

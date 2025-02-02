using System;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Library;

namespace SandBox.View.Map
{
	// Token: 0x02000052 RID: 82
	public abstract class MapView : SandboxView
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0001CC34 File Offset: 0x0001AE34
		// (set) Token: 0x06000385 RID: 901 RVA: 0x0001CC3C File Offset: 0x0001AE3C
		public MapScreen MapScreen { get; internal set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0001CC45 File Offset: 0x0001AE45
		// (set) Token: 0x06000387 RID: 903 RVA: 0x0001CC4D File Offset: 0x0001AE4D
		public MapState MapState { get; internal set; }

		// Token: 0x06000388 RID: 904 RVA: 0x0001CC56 File Offset: 0x0001AE56
		protected internal virtual void CreateLayout()
		{
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001CC58 File Offset: 0x0001AE58
		protected internal virtual void OnResume()
		{
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001CC5A File Offset: 0x0001AE5A
		protected internal virtual void OnHourlyTick()
		{
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001CC5C File Offset: 0x0001AE5C
		protected internal virtual void OnStartWait(string waitMenuId)
		{
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001CC5E File Offset: 0x0001AE5E
		protected internal virtual void OnMainPartyEncounter()
		{
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001CC60 File Offset: 0x0001AE60
		protected internal virtual void OnDispersePlayerLeadedArmy()
		{
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001CC62 File Offset: 0x0001AE62
		protected internal virtual void OnArmyLeft()
		{
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001CC64 File Offset: 0x0001AE64
		protected internal virtual bool IsEscaped()
		{
			return false;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001CC67 File Offset: 0x0001AE67
		protected internal virtual bool IsOpeningEscapeMenuOnFocusChangeAllowed()
		{
			return true;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001CC6A File Offset: 0x0001AE6A
		protected internal virtual void OnOverlayCreated()
		{
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001CC6C File Offset: 0x0001AE6C
		protected internal virtual void OnOverlayClosed()
		{
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001CC6E File Offset: 0x0001AE6E
		protected internal virtual void OnMenuModeTick(float dt)
		{
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001CC70 File Offset: 0x0001AE70
		protected internal virtual void OnMapScreenUpdate(float dt)
		{
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001CC72 File Offset: 0x0001AE72
		protected internal virtual void OnIdleTick(float dt)
		{
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001CC74 File Offset: 0x0001AE74
		protected internal virtual void OnMapTerrainClick()
		{
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001CC76 File Offset: 0x0001AE76
		protected internal virtual void OnSiegeEngineClick(MatrixFrame siegeEngineFrame)
		{
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001CC78 File Offset: 0x0001AE78
		protected internal virtual void OnMapConversationStart()
		{
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001CC7A File Offset: 0x0001AE7A
		protected internal virtual void OnMapConversationUpdate(ConversationCharacterData playerConversationData, ConversationCharacterData partnerConversationData)
		{
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001CC7C File Offset: 0x0001AE7C
		protected internal virtual void OnMapConversationOver()
		{
		}

		// Token: 0x040001DB RID: 475
		protected const float ContextAlphaModifier = 8.5f;
	}
}

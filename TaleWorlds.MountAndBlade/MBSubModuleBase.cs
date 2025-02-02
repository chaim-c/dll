using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001DA RID: 474
	public abstract class MBSubModuleBase
	{
		// Token: 0x06001AAB RID: 6827 RVA: 0x0005D198 File Offset: 0x0005B398
		protected internal virtual void OnSubModuleLoad()
		{
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x0005D19A File Offset: 0x0005B39A
		protected internal virtual void OnSubModuleUnloaded()
		{
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x0005D19C File Offset: 0x0005B39C
		protected internal virtual void OnBeforeInitialModuleScreenSetAsRoot()
		{
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x0005D19E File Offset: 0x0005B39E
		public virtual void OnConfigChanged()
		{
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x0005D1A0 File Offset: 0x0005B3A0
		protected internal virtual void OnGameStart(Game game, IGameStarter gameStarterObject)
		{
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x0005D1A2 File Offset: 0x0005B3A2
		protected internal virtual void OnApplicationTick(float dt)
		{
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x0005D1A4 File Offset: 0x0005B3A4
		protected internal virtual void AfterAsyncTickTick(float dt)
		{
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x0005D1A6 File Offset: 0x0005B3A6
		protected internal virtual void InitializeGameStarter(Game game, IGameStarter starterObject)
		{
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x0005D1A8 File Offset: 0x0005B3A8
		public virtual void OnGameLoaded(Game game, object initializerObject)
		{
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x0005D1AA File Offset: 0x0005B3AA
		public virtual void OnNewGameCreated(Game game, object initializerObject)
		{
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x0005D1AC File Offset: 0x0005B3AC
		public virtual void BeginGameStart(Game game)
		{
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x0005D1AE File Offset: 0x0005B3AE
		public virtual void OnCampaignStart(Game game, object starterObject)
		{
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0005D1B0 File Offset: 0x0005B3B0
		public virtual void RegisterSubModuleObjects(bool isSavedCampaign)
		{
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x0005D1B2 File Offset: 0x0005B3B2
		public virtual void AfterRegisterSubModuleObjects(bool isSavedCampaign)
		{
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x0005D1B4 File Offset: 0x0005B3B4
		public virtual void OnMultiplayerGameStart(Game game, object starterObject)
		{
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x0005D1B6 File Offset: 0x0005B3B6
		public virtual void OnGameInitializationFinished(Game game)
		{
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x0005D1B8 File Offset: 0x0005B3B8
		public virtual void OnAfterGameInitializationFinished(Game game, object starterObject)
		{
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x0005D1BA File Offset: 0x0005B3BA
		public virtual bool DoLoading(Game game)
		{
			return true;
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x0005D1BD File Offset: 0x0005B3BD
		public virtual void OnGameEnd(Game game)
		{
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x0005D1BF File Offset: 0x0005B3BF
		public virtual void OnMissionBehaviorInitialize(Mission mission)
		{
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x0005D1C1 File Offset: 0x0005B3C1
		public virtual void OnBeforeMissionBehaviorInitialize(Mission mission)
		{
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x0005D1C3 File Offset: 0x0005B3C3
		public virtual void OnInitialState()
		{
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x0005D1C5 File Offset: 0x0005B3C5
		protected internal virtual void OnNetworkTick(float dt)
		{
		}
	}
}

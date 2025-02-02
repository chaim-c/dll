using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000266 RID: 614
	public class BannerBearerLogic : MissionLogic
	{
		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06002098 RID: 8344 RVA: 0x000749E4 File Offset: 0x00072BE4
		// (remove) Token: 0x06002099 RID: 8345 RVA: 0x00074A1C File Offset: 0x00072C1C
		public event Action<Formation> OnBannerBearersUpdated;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x0600209A RID: 8346 RVA: 0x00074A54 File Offset: 0x00072C54
		// (remove) Token: 0x0600209B RID: 8347 RVA: 0x00074A8C File Offset: 0x00072C8C
		public event Action<Agent, bool> OnBannerBearerAgentUpdated;

		// Token: 0x0600209C RID: 8348 RVA: 0x00074AC1 File Offset: 0x00072CC1
		public BannerBearerLogic()
		{
			this._bannerSearcherUpdateTimer = new BasicMissionTimer();
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x0600209D RID: 8349 RVA: 0x00074B00 File Offset: 0x00072D00
		// (set) Token: 0x0600209E RID: 8350 RVA: 0x00074B08 File Offset: 0x00072D08
		public MissionAgentSpawnLogic AgentSpawnLogic { get; private set; }

		// Token: 0x0600209F RID: 8351 RVA: 0x00074B14 File Offset: 0x00072D14
		public bool IsFormationBanner(Formation formation, SpawnedItemEntity spawnedItem)
		{
			if (!BannerBearerLogic.IsBannerItem(spawnedItem.WeaponCopy.Item))
			{
				return false;
			}
			BannerBearerLogic.FormationBannerController formationControllerFromBannerEntity = this.GetFormationControllerFromBannerEntity(spawnedItem.GameEntity);
			return formationControllerFromBannerEntity != null && formationControllerFromBannerEntity.Formation == formation;
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x00074B54 File Offset: 0x00072D54
		public bool HasBannerOnGround(Formation formation)
		{
			BannerBearerLogic.FormationBannerController formationControllerFromFormation = this.GetFormationControllerFromFormation(formation);
			return formationControllerFromFormation != null && formationControllerFromFormation.HasBannerOnGround();
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x00074B74 File Offset: 0x00072D74
		public BannerComponent GetActiveBanner(Formation formation)
		{
			BannerBearerLogic.FormationBannerController formationControllerFromFormation = this.GetFormationControllerFromFormation(formation);
			if (formationControllerFromFormation == null)
			{
				return null;
			}
			if (!formationControllerFromFormation.HasActiveBannerBearers())
			{
				return null;
			}
			return formationControllerFromFormation.BannerItem.BannerComponent;
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x00074BA4 File Offset: 0x00072DA4
		public List<Agent> GetFormationBannerBearers(Formation formation)
		{
			BannerBearerLogic.FormationBannerController formationControllerFromFormation = this.GetFormationControllerFromFormation(formation);
			if (formationControllerFromFormation != null)
			{
				return formationControllerFromFormation.BannerBearers;
			}
			return new List<Agent>();
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x00074BC8 File Offset: 0x00072DC8
		public ItemObject GetFormationBanner(Formation formation)
		{
			ItemObject result = null;
			BannerBearerLogic.FormationBannerController formationControllerFromFormation = this.GetFormationControllerFromFormation(formation);
			if (formationControllerFromFormation != null)
			{
				result = formationControllerFromFormation.BannerItem;
			}
			return result;
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x00074BEC File Offset: 0x00072DEC
		public bool IsBannerSearchingAgent(Agent agent)
		{
			if (agent.Formation != null)
			{
				BannerBearerLogic.FormationBannerController formationControllerFromFormation = this.GetFormationControllerFromFormation(agent.Formation);
				if (formationControllerFromFormation != null)
				{
					return formationControllerFromFormation.IsBannerSearchingAgent(agent);
				}
			}
			return false;
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x00074C1C File Offset: 0x00072E1C
		public int GetMissingBannerCount(Formation formation)
		{
			BannerBearerLogic.FormationBannerController formationControllerFromFormation = this.GetFormationControllerFromFormation(formation);
			if (formationControllerFromFormation == null || formationControllerFromFormation.BannerItem == null)
			{
				return 0;
			}
			int num = MissionGameModels.Current.BattleBannerBearersModel.GetDesiredNumberOfBannerBearersForFormation(formation) - formationControllerFromFormation.NumberOfBanners;
			if (num <= 0)
			{
				return 0;
			}
			return num;
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x00074C60 File Offset: 0x00072E60
		public Formation GetFormationFromBanner(SpawnedItemEntity spawnedItem)
		{
			GameEntity gameEntity = spawnedItem.GameEntity;
			gameEntity = ((gameEntity == null) ? spawnedItem.GameEntityWithWorldPosition.GameEntity : gameEntity);
			BannerBearerLogic.FormationBannerController formationControllerFromBannerEntity = this.GetFormationControllerFromBannerEntity(gameEntity);
			if (formationControllerFromBannerEntity == null)
			{
				return null;
			}
			return formationControllerFromBannerEntity.Formation;
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x00074CA0 File Offset: 0x00072EA0
		public void SetFormationBanner(Formation formation, ItemObject newBanner)
		{
			if (newBanner != null)
			{
				BannerBearerLogic.IsBannerItem(newBanner);
			}
			BannerBearerLogic.FormationBannerController formationBannerController = this.GetFormationControllerFromFormation(formation);
			if (formationBannerController != null)
			{
				if (formationBannerController.BannerItem != newBanner)
				{
					formationBannerController.SetBannerItem(newBanner);
				}
			}
			else
			{
				formationBannerController = new BannerBearerLogic.FormationBannerController(formation, newBanner, this, base.Mission);
				this._formationBannerData.Add(formation, formationBannerController);
			}
			formationBannerController.UpdateBannerBearersForDeployment();
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x00074CFC File Offset: 0x00072EFC
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			MissionGameModels.Current.BattleBannerBearersModel.InitializeModel(this);
			this.AgentSpawnLogic = base.Mission.GetMissionBehavior<MissionAgentSpawnLogic>();
			base.Mission.OnItemPickUp += this.OnItemPickup;
			base.Mission.OnItemDrop += this.OnItemDrop;
			this._initialSpawnEquipments.Clear();
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x00074D6C File Offset: 0x00072F6C
		protected override void OnEndMission()
		{
			base.OnEndMission();
			MissionGameModels.Current.BattleBannerBearersModel.FinalizeModel();
			base.Mission.OnItemPickUp -= this.OnItemPickup;
			base.Mission.OnItemDrop -= this.OnItemDrop;
			this.AgentSpawnLogic = null;
			this._isMissionEnded = true;
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x00074DCA File Offset: 0x00072FCA
		public override void OnDeploymentFinished()
		{
			this._initialSpawnEquipments.Clear();
			this._isMissionEnded = false;
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x00074DE0 File Offset: 0x00072FE0
		public override void OnMissionTick(float dt)
		{
			if (this._bannerSearcherUpdateTimer.ElapsedTime >= 3f)
			{
				foreach (BannerBearerLogic.FormationBannerController formationBannerController in this._formationBannerData.Values)
				{
					formationBannerController.UpdateBannerSearchers();
				}
				this._bannerSearcherUpdateTimer.Reset();
			}
			if (base.Mission.Mode == MissionMode.Deployment && !this._playerFormationsRequiringUpdate.IsEmpty<BannerBearerLogic.FormationBannerController>())
			{
				foreach (BannerBearerLogic.FormationBannerController formationBannerController2 in this._playerFormationsRequiringUpdate)
				{
					formationBannerController2.UpdateBannerBearersForDeployment();
				}
				this._playerFormationsRequiringUpdate.Clear();
			}
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x00074EB8 File Offset: 0x000730B8
		public void OnItemPickup(Agent agent, SpawnedItemEntity spawnedItem)
		{
			if (!BannerBearerLogic.IsBannerItem(spawnedItem.WeaponCopy.Item))
			{
				return;
			}
			GameEntity gameEntity = spawnedItem.GameEntityWithWorldPosition.GameEntity;
			BannerBearerLogic.FormationBannerController formationControllerFromBannerEntity = this.GetFormationControllerFromBannerEntity(gameEntity);
			if (formationControllerFromBannerEntity != null)
			{
				formationControllerFromBannerEntity.OnBannerEntityPickedUp(gameEntity, agent);
				formationControllerFromBannerEntity.UpdateAgentStats(false);
			}
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x00074F04 File Offset: 0x00073104
		public void OnItemDrop(Agent agent, SpawnedItemEntity spawnedItem)
		{
			if (!BannerBearerLogic.IsBannerItem(spawnedItem.WeaponCopy.Item))
			{
				return;
			}
			BannerBearerLogic.FormationBannerController formationControllerFromBannerEntity = this.GetFormationControllerFromBannerEntity(spawnedItem.GameEntity);
			if (formationControllerFromBannerEntity != null)
			{
				formationControllerFromBannerEntity.OnBannerEntityDropped(spawnedItem.GameEntity);
				formationControllerFromBannerEntity.UpdateAgentStats(false);
			}
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x00074F4A File Offset: 0x0007314A
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			if (affectedAgent.Banner != null && agentState == AgentState.Routed)
			{
				this.RemoveBannerOfAgent(affectedAgent);
			}
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x00074F5F File Offset: 0x0007315F
		public override void OnAgentPanicked(Agent affectedAgent)
		{
			if (affectedAgent.Banner != null)
			{
				BannerBearerLogic.ForceDropAgentBanner(affectedAgent);
			}
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x00074F70 File Offset: 0x00073170
		public void UpdateAgent(Agent agent, bool willBecomeBannerBearer)
		{
			if (willBecomeBannerBearer)
			{
				Formation formation = agent.Formation;
				BannerBearerLogic.FormationBannerController formationControllerFromFormation = this.GetFormationControllerFromFormation(formation);
				ItemObject bannerItem = formationControllerFromFormation.BannerItem;
				Equipment newSpawnEquipment = this.CreateBannerEquipmentForAgent(agent, bannerItem);
				agent.UpdateSpawnEquipmentAndRefreshVisuals(newSpawnEquipment);
				GameEntity weaponEntityFromEquipmentSlot = agent.GetWeaponEntityFromEquipmentSlot(EquipmentIndex.ExtraWeaponSlot);
				this.AddBannerEntity(formationControllerFromFormation, weaponEntityFromEquipmentSlot);
				formationControllerFromFormation.OnBannerEntityPickedUp(weaponEntityFromEquipmentSlot, agent);
			}
			else if (agent.Banner != null)
			{
				this.RemoveBannerOfAgent(agent);
				agent.UpdateSpawnEquipmentAndRefreshVisuals(this._initialSpawnEquipments[agent]);
			}
			agent.UpdateCachedAndFormationValues(false, false);
			agent.SetIsAIPaused(true);
			Action<Agent, bool> onBannerBearerAgentUpdated = this.OnBannerBearerAgentUpdated;
			if (onBannerBearerAgentUpdated == null)
			{
				return;
			}
			onBannerBearerAgentUpdated(agent, willBecomeBannerBearer);
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x00075008 File Offset: 0x00073208
		public Agent SpawnBannerBearer(IAgentOriginBase troopOrigin, bool isPlayerSide, Formation formation, bool spawnWithHorse, bool isReinforcement, int formationTroopCount, int formationTroopIndex, bool isAlarmed, bool wieldInitialWeapons, bool forceDismounted, Vec3? initialPosition, Vec2? initialDirection, string specialActionSetSuffix = null, bool useTroopClassForSpawn = false)
		{
			BannerBearerLogic.FormationBannerController formationControllerFromFormation = this.GetFormationControllerFromFormation(formation);
			ItemObject bannerItem = formationControllerFromFormation.BannerItem;
			Agent agent = base.Mission.SpawnTroop(troopOrigin, isPlayerSide, true, spawnWithHorse, isReinforcement, formationTroopCount, formationTroopIndex, isAlarmed, wieldInitialWeapons, forceDismounted, initialPosition, initialDirection, specialActionSetSuffix, bannerItem, formationControllerFromFormation.Formation.FormationIndex, useTroopClassForSpawn);
			agent.UpdateCachedAndFormationValues(false, false);
			GameEntity weaponEntityFromEquipmentSlot = agent.GetWeaponEntityFromEquipmentSlot(EquipmentIndex.ExtraWeaponSlot);
			this.AddBannerEntity(formationControllerFromFormation, weaponEntityFromEquipmentSlot);
			formationControllerFromFormation.OnBannerEntityPickedUp(weaponEntityFromEquipmentSlot, agent);
			return agent;
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x00075076 File Offset: 0x00073276
		public static bool IsBannerItem(ItemObject item)
		{
			return item != null && item.IsBannerItem && item.BannerComponent != null;
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x0007508E File Offset: 0x0007328E
		private void AddBannerEntity(BannerBearerLogic.FormationBannerController formationBannerController, GameEntity bannerEntity)
		{
			this._bannerToFormationMap.Add(bannerEntity.Pointer, formationBannerController);
			formationBannerController.AddBannerEntity(bannerEntity);
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000750A9 File Offset: 0x000732A9
		private void RemoveBannerEntity(BannerBearerLogic.FormationBannerController formationBannerController, GameEntity bannerEntity)
		{
			this._bannerToFormationMap.Remove(bannerEntity.Pointer);
			formationBannerController.RemoveBannerEntity(bannerEntity);
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x000750C4 File Offset: 0x000732C4
		private BannerBearerLogic.FormationBannerController GetFormationControllerFromFormation(Formation formation)
		{
			BannerBearerLogic.FormationBannerController result;
			if (!this._formationBannerData.TryGetValue(formation, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x000750E4 File Offset: 0x000732E4
		private BannerBearerLogic.FormationBannerController GetFormationControllerFromBannerEntity(GameEntity bannerEntity)
		{
			BannerBearerLogic.FormationBannerController result;
			if (this._bannerToFormationMap.TryGetValue(bannerEntity.Pointer, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x0007510C File Offset: 0x0007330C
		private Equipment CreateBannerEquipmentForAgent(Agent agent, ItemObject bannerItem)
		{
			Equipment spawnEquipment = agent.SpawnEquipment;
			if (!this._initialSpawnEquipments.ContainsKey(agent))
			{
				this._initialSpawnEquipments[agent] = spawnEquipment;
			}
			Equipment equipment = new Equipment(spawnEquipment);
			ItemObject bannerBearerReplacementWeapon = MissionGameModels.Current.BattleBannerBearersModel.GetBannerBearerReplacementWeapon(agent.Character);
			equipment[EquipmentIndex.WeaponItemBeginSlot] = new EquipmentElement(bannerBearerReplacementWeapon, null, null, false);
			for (int i = 1; i < 4; i++)
			{
				equipment[i] = default(EquipmentElement);
			}
			equipment[EquipmentIndex.ExtraWeaponSlot] = new EquipmentElement(bannerItem, null, null, false);
			return equipment;
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x00075198 File Offset: 0x00073398
		private void RemoveBannerOfAgent(Agent agent)
		{
			GameEntity weaponEntityFromEquipmentSlot = agent.GetWeaponEntityFromEquipmentSlot(EquipmentIndex.ExtraWeaponSlot);
			BannerBearerLogic.FormationBannerController formationControllerFromBannerEntity = this.GetFormationControllerFromBannerEntity(weaponEntityFromEquipmentSlot);
			if (formationControllerFromBannerEntity != null)
			{
				this.RemoveBannerEntity(formationControllerFromBannerEntity, weaponEntityFromEquipmentSlot);
				formationControllerFromBannerEntity.UpdateAgentStats(false);
			}
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x000751C7 File Offset: 0x000733C7
		private static void ForceDropAgentBanner(Agent agent)
		{
			if (agent != null)
			{
				ItemObject banner = agent.Banner;
			}
			agent.DropItem(EquipmentIndex.ExtraWeaponSlot, WeaponClass.Undefined);
		}

		// Token: 0x04000C0D RID: 3085
		public const float DefaultBannerBearerAgentDefensiveness = 1f;

		// Token: 0x04000C0E RID: 3086
		public const float BannerSearcherUpdatePeriod = 3f;

		// Token: 0x04000C12 RID: 3090
		private readonly Dictionary<UIntPtr, BannerBearerLogic.FormationBannerController> _bannerToFormationMap = new Dictionary<UIntPtr, BannerBearerLogic.FormationBannerController>();

		// Token: 0x04000C13 RID: 3091
		private readonly Dictionary<Formation, BannerBearerLogic.FormationBannerController> _formationBannerData = new Dictionary<Formation, BannerBearerLogic.FormationBannerController>();

		// Token: 0x04000C14 RID: 3092
		private readonly Dictionary<Agent, Equipment> _initialSpawnEquipments = new Dictionary<Agent, Equipment>();

		// Token: 0x04000C15 RID: 3093
		private readonly BasicMissionTimer _bannerSearcherUpdateTimer;

		// Token: 0x04000C16 RID: 3094
		private readonly List<BannerBearerLogic.FormationBannerController> _playerFormationsRequiringUpdate = new List<BannerBearerLogic.FormationBannerController>();

		// Token: 0x04000C17 RID: 3095
		private bool _isMissionEnded;

		// Token: 0x0200051B RID: 1307
		private class FormationBannerController
		{
			// Token: 0x17000979 RID: 2425
			// (get) Token: 0x06003868 RID: 14440 RVA: 0x000E1F3B File Offset: 0x000E013B
			// (set) Token: 0x06003869 RID: 14441 RVA: 0x000E1F43 File Offset: 0x000E0143
			public Formation Formation { get; private set; }

			// Token: 0x1700097A RID: 2426
			// (get) Token: 0x0600386A RID: 14442 RVA: 0x000E1F4C File Offset: 0x000E014C
			// (set) Token: 0x0600386B RID: 14443 RVA: 0x000E1F54 File Offset: 0x000E0154
			public ItemObject BannerItem { get; private set; }

			// Token: 0x1700097B RID: 2427
			// (get) Token: 0x0600386C RID: 14444 RVA: 0x000E1F5D File Offset: 0x000E015D
			public bool HasBanner
			{
				get
				{
					return this.BannerItem != null;
				}
			}

			// Token: 0x1700097C RID: 2428
			// (get) Token: 0x0600386D RID: 14445 RVA: 0x000E1F68 File Offset: 0x000E0168
			public List<Agent> BannerBearers
			{
				get
				{
					return (from instance in this._bannerInstances.Values
					where instance.IsOnAgent
					select instance.BannerBearer).ToList<Agent>();
				}
			}

			// Token: 0x1700097D RID: 2429
			// (get) Token: 0x0600386E RID: 14446 RVA: 0x000E1FD0 File Offset: 0x000E01D0
			public List<GameEntity> BannersOnGround
			{
				get
				{
					return (from instance in this._bannerInstances.Values
					where instance.IsOnGround
					select instance.Entity).ToList<GameEntity>();
				}
			}

			// Token: 0x1700097E RID: 2430
			// (get) Token: 0x0600386F RID: 14447 RVA: 0x000E2035 File Offset: 0x000E0235
			public int NumberOfBannerBearers
			{
				get
				{
					return this._bannerInstances.Values.Count((BannerBearerLogic.FormationBannerController.BannerInstance instance) => instance.IsOnAgent);
				}
			}

			// Token: 0x1700097F RID: 2431
			// (get) Token: 0x06003870 RID: 14448 RVA: 0x000E2066 File Offset: 0x000E0266
			public int NumberOfBanners
			{
				get
				{
					return this._bannerInstances.Count;
				}
			}

			// Token: 0x17000980 RID: 2432
			// (get) Token: 0x06003871 RID: 14449 RVA: 0x000E2073 File Offset: 0x000E0273
			public static float BannerSearchDistance
			{
				get
				{
					return 9f;
				}
			}

			// Token: 0x06003872 RID: 14450 RVA: 0x000E207C File Offset: 0x000E027C
			public FormationBannerController(Formation formation, ItemObject bannerItem, BannerBearerLogic bannerLogic, Mission mission)
			{
				this.Formation = formation;
				this.Formation.OnUnitAdded += this.OnAgentAdded;
				this.Formation.OnUnitRemoved += this.OnAgentRemoved;
				this.Formation.OnBeforeMovementOrderApplied += this.OnBeforeFormationMovementOrderApplied;
				this.Formation.OnAfterArrangementOrderApplied += this.OnAfterArrangementOrderApplied;
				this._bannerInstances = new Dictionary<UIntPtr, BannerBearerLogic.FormationBannerController.BannerInstance>();
				this._bannerSearchers = new Dictionary<Agent, ValueTuple<GameEntity, float>>();
				this._requiresAgentStatUpdate = false;
				this._lastActiveBannerBearerCount = 0;
				this._bannerLogic = bannerLogic;
				this._mission = mission;
				this.SetBannerItem(bannerItem);
			}

			// Token: 0x06003873 RID: 14451 RVA: 0x000E2137 File Offset: 0x000E0337
			public void SetBannerItem(ItemObject bannerItem)
			{
				if (bannerItem != null)
				{
					BannerBearerLogic.IsBannerItem(bannerItem);
				}
				this.BannerItem = bannerItem;
			}

			// Token: 0x06003874 RID: 14452 RVA: 0x000E214D File Offset: 0x000E034D
			public bool HasBannerEntity(GameEntity bannerEntity)
			{
				return bannerEntity != null && this._bannerInstances.Keys.Contains(bannerEntity.Pointer);
			}

			// Token: 0x06003875 RID: 14453 RVA: 0x000E2170 File Offset: 0x000E0370
			public bool HasBannerOnGround()
			{
				if (this.HasBanner)
				{
					return this._bannerInstances.Any((KeyValuePair<UIntPtr, BannerBearerLogic.FormationBannerController.BannerInstance> instance) => instance.Value.IsOnGround);
				}
				return false;
			}

			// Token: 0x06003876 RID: 14454 RVA: 0x000E21A6 File Offset: 0x000E03A6
			public bool HasActiveBannerBearers()
			{
				return this.GetNumberOfActiveBannerBearers() > 0;
			}

			// Token: 0x06003877 RID: 14455 RVA: 0x000E21B1 File Offset: 0x000E03B1
			public bool IsBannerSearchingAgent(Agent agent)
			{
				return this._bannerSearchers.Keys.Contains(agent);
			}

			// Token: 0x06003878 RID: 14456 RVA: 0x000E21C4 File Offset: 0x000E03C4
			public int GetNumberOfActiveBannerBearers()
			{
				int result = 0;
				if (this.HasBanner)
				{
					BattleBannerBearersModel bannerBearersModel = MissionGameModels.Current.BattleBannerBearersModel;
					result = this._bannerInstances.Values.Count((BannerBearerLogic.FormationBannerController.BannerInstance instance) => instance.IsOnAgent && bannerBearersModel.CanBannerBearerProvideEffectToFormation(instance.BannerBearer, this.Formation));
				}
				return result;
			}

			// Token: 0x06003879 RID: 14457 RVA: 0x000E2216 File Offset: 0x000E0416
			public void UpdateAgentStats(bool forceUpdate = false)
			{
				if (forceUpdate || this._requiresAgentStatUpdate)
				{
					this.Formation.ApplyActionOnEachUnit(delegate(Agent agent)
					{
						agent.UpdateAgentProperties();
						Agent mountAgent = agent.MountAgent;
						if (mountAgent != null)
						{
							mountAgent.UpdateAgentProperties();
						}
					}, null);
					this._requiresAgentStatUpdate = false;
				}
			}

			// Token: 0x0600387A RID: 14458 RVA: 0x000E2258 File Offset: 0x000E0458
			public unsafe void RepositionFormation()
			{
				this.Formation.SetMovementOrder(*this.Formation.GetReadonlyMovementOrderReference());
				this.Formation.ApplyActionOnEachUnit(delegate(Agent agent)
				{
					agent.UpdateCachedAndFormationValues(true, false);
				}, null);
			}

			// Token: 0x0600387B RID: 14459 RVA: 0x000E22AC File Offset: 0x000E04AC
			public void UpdateBannerSearchers()
			{
				List<GameEntity> bannersOnGround = this.BannersOnGround;
				if (!this._bannerSearchers.IsEmpty<KeyValuePair<Agent, ValueTuple<GameEntity, float>>>())
				{
					List<Agent> list = new List<Agent>();
					using (Dictionary<Agent, ValueTuple<GameEntity, float>>.Enumerator enumerator = this._bannerSearchers.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<Agent, ValueTuple<GameEntity, float>> searcherTuple = enumerator.Current;
							Agent key = searcherTuple.Key;
							if (key.IsActive())
							{
								if (!bannersOnGround.Any((GameEntity bannerEntity) => bannerEntity.Pointer == searcherTuple.Value.Item1.Pointer))
								{
									list.Add(key);
								}
							}
							else
							{
								list.Add(key);
							}
						}
					}
					foreach (Agent searcher in list)
					{
						this.RemoveBannerSearcher(searcher);
					}
				}
				using (List<GameEntity>.Enumerator enumerator3 = bannersOnGround.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						GameEntity banner = enumerator3.Current;
						bool flag = false;
						if (this._bannerSearchers.IsEmpty<KeyValuePair<Agent, ValueTuple<GameEntity, float>>>())
						{
							flag = true;
						}
						else
						{
							KeyValuePair<Agent, ValueTuple<GameEntity, float>> keyValuePair = this._bannerSearchers.FirstOrDefault(([TupleElementNames(new string[]
							{
								"bannerEntity",
								"lastDistance"
							})] KeyValuePair<Agent, ValueTuple<GameEntity, float>> tuple) => tuple.Value.Item1.Pointer == banner.Pointer);
							if (keyValuePair.Key == null)
							{
								flag = true;
							}
							else
							{
								Agent key2 = keyValuePair.Key;
								if (key2.IsActive())
								{
									GameEntity item = keyValuePair.Value.Item1;
									float item2 = keyValuePair.Value.Item2;
									float num = key2.Position.AsVec2.Distance(item.GlobalPosition.AsVec2);
									if (num <= item2 && num < BannerBearerLogic.FormationBannerController.BannerSearchDistance)
									{
										this._bannerSearchers[key2] = new ValueTuple<GameEntity, float>(item, num);
									}
									else
									{
										this.RemoveBannerSearcher(key2);
										flag = true;
									}
								}
								else
								{
									this.RemoveBannerSearcher(key2);
									flag = true;
								}
							}
						}
						if (flag)
						{
							float distance;
							Agent agent = this.FindBestSearcherForBanner(banner, out distance);
							if (agent != null)
							{
								this.AddBannerSearcher(agent, banner, distance);
							}
						}
					}
				}
			}

			// Token: 0x0600387C RID: 14460 RVA: 0x000E2510 File Offset: 0x000E0710
			public void UpdateBannerBearersForDeployment()
			{
				List<Agent> bannerBearers = this.BannerBearers;
				List<ValueTuple<Agent, bool>> list = new List<ValueTuple<Agent, bool>>();
				List<Agent> list2 = new List<Agent>();
				int num = 0;
				BattleBannerBearersModel battleBannerBearersModel = MissionGameModels.Current.BattleBannerBearersModel;
				if (battleBannerBearersModel.CanFormationDeployBannerBearers(this.Formation))
				{
					num = battleBannerBearersModel.GetDesiredNumberOfBannerBearersForFormation(this.Formation);
					using (List<Agent>.Enumerator enumerator = bannerBearers.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Agent agent = enumerator.Current;
							if (num > 0 && agent.Formation == this.Formation)
							{
								num--;
							}
							else
							{
								list2.Add(agent);
							}
						}
						goto IL_92;
					}
				}
				list2.AddRange(bannerBearers);
				IL_92:
				foreach (Agent item in list2)
				{
					bool item2 = false;
					if (num > 0)
					{
						item2 = true;
						num--;
					}
					list.Add(new ValueTuple<Agent, bool>(item, item2));
				}
				if (num > 0)
				{
					List<Agent> list3 = this.FindBannerBearableAgents(num);
					int num2 = 0;
					while (num2 < list3.Count && num > 0)
					{
						Agent agent2 = list3[num2];
						list2.Add(agent2);
						list.Add(new ValueTuple<Agent, bool>(agent2, true));
						num--;
						num2++;
					}
				}
				if (!list.IsEmpty<ValueTuple<Agent, bool>>())
				{
					BattleSideEnum side = this.Formation.Team.Side;
					this._bannerLogic.AgentSpawnLogic.GetSpawnHorses(side);
					BattleSideEnum side2 = this._mission.PlayerTeam.Side;
					foreach (ValueTuple<Agent, bool> valueTuple in list)
					{
						this._bannerLogic.UpdateAgent(valueTuple.Item1, valueTuple.Item2);
					}
				}
				this.UpdateAgentStats(false);
				this.RepositionFormation();
				Action<Formation> onBannerBearersUpdated = this._bannerLogic.OnBannerBearersUpdated;
				if (onBannerBearersUpdated == null)
				{
					return;
				}
				onBannerBearersUpdated(this.Formation);
			}

			// Token: 0x0600387D RID: 14461 RVA: 0x000E2718 File Offset: 0x000E0918
			public void AddBannerEntity(GameEntity entity)
			{
				if (!this._bannerInstances.ContainsKey(entity.Pointer))
				{
					this._bannerInstances.Add(entity.Pointer, new BannerBearerLogic.FormationBannerController.BannerInstance(null, entity, BannerBearerLogic.FormationBannerController.BannerState.Initialized));
				}
			}

			// Token: 0x0600387E RID: 14462 RVA: 0x000E2746 File Offset: 0x000E0946
			public void RemoveBannerEntity(GameEntity entity)
			{
				this._bannerInstances.Remove(entity.Pointer);
				this.UpdateBannerSearchers();
				this.CheckRequiresAgentStatUpdate();
			}

			// Token: 0x0600387F RID: 14463 RVA: 0x000E2766 File Offset: 0x000E0966
			public void OnBannerEntityPickedUp(GameEntity entity, Agent agent)
			{
				this._bannerInstances[entity.Pointer] = new BannerBearerLogic.FormationBannerController.BannerInstance(agent, entity, BannerBearerLogic.FormationBannerController.BannerState.OnAgent);
				if (agent.IsAIControlled)
				{
					agent.ResetEnemyCaches();
					agent.Defensiveness = 1f;
				}
				this.UpdateBannerSearchers();
				this.CheckRequiresAgentStatUpdate();
			}

			// Token: 0x06003880 RID: 14464 RVA: 0x000E27A6 File Offset: 0x000E09A6
			public void OnBannerEntityDropped(GameEntity entity)
			{
				this._bannerInstances[entity.Pointer] = new BannerBearerLogic.FormationBannerController.BannerInstance(null, entity, BannerBearerLogic.FormationBannerController.BannerState.OnGround);
				this.UpdateBannerSearchers();
				this.CheckRequiresAgentStatUpdate();
			}

			// Token: 0x06003881 RID: 14465 RVA: 0x000E27CD File Offset: 0x000E09CD
			public void OnBeforeFormationMovementOrderApplied(Formation formation, MovementOrder.MovementOrderEnum orderType)
			{
				if (formation == this.Formation)
				{
					this.UpdateBannerBearerArrangementPositions();
				}
			}

			// Token: 0x06003882 RID: 14466 RVA: 0x000E27DE File Offset: 0x000E09DE
			public void OnAfterArrangementOrderApplied(Formation formation, ArrangementOrder.ArrangementOrderEnum orderEnum)
			{
				if (formation == this.Formation)
				{
					this.UpdateBannerBearerArrangementPositions();
				}
			}

			// Token: 0x06003883 RID: 14467 RVA: 0x000E27F0 File Offset: 0x000E09F0
			private Agent FindBestSearcherForBanner(GameEntity banner, out float distance)
			{
				distance = float.MaxValue;
				Agent result = null;
				Vec2 asVec = banner.GlobalPosition.AsVec2;
				this._mission.GetNearbyAllyAgents(asVec, BannerBearerLogic.FormationBannerController.BannerSearchDistance, this.Formation.Team, this._nearbyAllyAgentsListCache);
				BattleBannerBearersModel battleBannerBearersModel = MissionGameModels.Current.BattleBannerBearersModel;
				foreach (Agent agent in this._nearbyAllyAgentsListCache)
				{
					if (agent.Formation == this.Formation && battleBannerBearersModel.CanAgentPickUpAnyBanner(agent))
					{
						float num = agent.Position.AsVec2.Distance(asVec);
						if (num < distance && !this._bannerSearchers.ContainsKey(agent))
						{
							result = agent;
							distance = num;
						}
					}
				}
				return result;
			}

			// Token: 0x06003884 RID: 14468 RVA: 0x000E28D8 File Offset: 0x000E0AD8
			private List<Agent> FindBannerBearableAgents(int count)
			{
				List<Agent> list = new List<Agent>();
				if (count > 0)
				{
					BattleBannerBearersModel bannerBearerModel = MissionGameModels.Current.BattleBannerBearersModel;
					using (List<IFormationUnit>.Enumerator enumerator = this.Formation.UnitsWithoutLooseDetachedOnes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Agent agent2;
							if ((agent2 = (enumerator.Current as Agent)) != null && agent2.Banner == null && bannerBearerModel.CanAgentBecomeBannerBearer(agent2))
							{
								list.Add(agent2);
							}
						}
					}
					list = (from agent in list
					orderby bannerBearerModel.GetAgentBannerBearingPriority(agent) descending
					select agent).ToList<Agent>();
				}
				return list;
			}

			// Token: 0x06003885 RID: 14469 RVA: 0x000E298C File Offset: 0x000E0B8C
			private void UpdateBannerBearerArrangementPositions()
			{
				List<Agent> list = (from instance in this._bannerInstances.Values
				where instance.IsOnAgent && instance.BannerBearer.Formation == this.Formation
				select instance.BannerBearer).ToList<Agent>();
				List<FormationArrangementModel.ArrangementPosition> bannerBearerPositions = MissionGameModels.Current.FormationArrangementsModel.GetBannerBearerPositions(this.Formation, list.Count);
				if (bannerBearerPositions == null || bannerBearerPositions.IsEmpty<FormationArrangementModel.ArrangementPosition>())
				{
					return;
				}
				int i = 0;
				foreach (Agent agent in list)
				{
					if (agent != null && agent.IsAIControlled && agent.Formation == this.Formation)
					{
						int num;
						int num2;
						agent.GetFormationFileAndRankInfo(out num, out num2);
						while (i < bannerBearerPositions.Count)
						{
							FormationArrangementModel.ArrangementPosition arrangementPosition = bannerBearerPositions[i];
							int fileIndex = arrangementPosition.FileIndex;
							int rankIndex = arrangementPosition.RankIndex;
							bool flag = num == fileIndex && num2 == rankIndex;
							if (!flag)
							{
								IFormationUnit unit = this.Formation.Arrangement.GetUnit(fileIndex, rankIndex);
								Agent agent2;
								if (unit != null && (agent2 = (unit as Agent)) != null)
								{
									if (agent2 == agent)
									{
										flag = true;
									}
									else if (agent2 != this.Formation.Captain)
									{
										this.Formation.SwitchUnitLocations(agent, agent2);
										flag = true;
									}
								}
							}
							if (flag)
							{
								i++;
								break;
							}
							i++;
						}
					}
				}
			}

			// Token: 0x06003886 RID: 14470 RVA: 0x000E2B1C File Offset: 0x000E0D1C
			private void OnAgentAdded(Formation formation, Agent agent)
			{
				if (this.Formation == formation)
				{
					if (!this._bannerLogic._isMissionEnded && this._mission.Mode == MissionMode.Deployment && formation.Team.IsPlayerTeam && MissionGameModels.Current.BattleInitializationModel.CanPlayerSideDeployWithOrderOfBattle())
					{
						int minimumFormationTroopCountToBearBanners = MissionGameModels.Current.BattleBannerBearersModel.GetMinimumFormationTroopCountToBearBanners();
						if (formation.CountOfUnits == minimumFormationTroopCountToBearBanners && !this._bannerLogic._playerFormationsRequiringUpdate.Contains(this))
						{
							this._bannerLogic._playerFormationsRequiringUpdate.Add(this);
							return;
						}
					}
					else
					{
						this.UpdateBannerSearchers();
					}
				}
			}

			// Token: 0x06003887 RID: 14471 RVA: 0x000E2BB0 File Offset: 0x000E0DB0
			private void OnAgentRemoved(Formation formation, Agent agent)
			{
				if (this.Formation == formation)
				{
					if (!this._bannerLogic._isMissionEnded && this._mission.Mode == MissionMode.Deployment && formation.Team.IsPlayerTeam && MissionGameModels.Current.BattleInitializationModel.CanPlayerSideDeployWithOrderOfBattle())
					{
						int minimumFormationTroopCountToBearBanners = MissionGameModels.Current.BattleBannerBearersModel.GetMinimumFormationTroopCountToBearBanners();
						if (formation.CountOfUnits == minimumFormationTroopCountToBearBanners - 1 && !this._bannerLogic._playerFormationsRequiringUpdate.Contains(this))
						{
							this._bannerLogic._playerFormationsRequiringUpdate.Add(this);
							return;
						}
					}
					else
					{
						this.UpdateBannerSearchers();
					}
				}
			}

			// Token: 0x06003888 RID: 14472 RVA: 0x000E2C48 File Offset: 0x000E0E48
			private void CheckRequiresAgentStatUpdate()
			{
				if (!this._requiresAgentStatUpdate)
				{
					int numberOfActiveBannerBearers = this.GetNumberOfActiveBannerBearers();
					if ((numberOfActiveBannerBearers > 0 && this._lastActiveBannerBearerCount == 0) || (numberOfActiveBannerBearers == 0 && this._lastActiveBannerBearerCount > 0))
					{
						this._requiresAgentStatUpdate = true;
						this._lastActiveBannerBearerCount = numberOfActiveBannerBearers;
					}
				}
			}

			// Token: 0x06003889 RID: 14473 RVA: 0x000E2C8A File Offset: 0x000E0E8A
			private void AddBannerSearcher(Agent searcher, GameEntity banner, float distance)
			{
				this._bannerSearchers.Add(searcher, new ValueTuple<GameEntity, float>(banner, distance));
				HumanAIComponent humanAIComponent = searcher.HumanAIComponent;
				if (humanAIComponent == null)
				{
					return;
				}
				humanAIComponent.DisablePickUpForAgentIfNeeded();
			}

			// Token: 0x0600388A RID: 14474 RVA: 0x000E2CAF File Offset: 0x000E0EAF
			private void RemoveBannerSearcher(Agent searcher)
			{
				this._bannerSearchers.Remove(searcher);
				if (searcher.IsActive())
				{
					HumanAIComponent humanAIComponent = searcher.HumanAIComponent;
					if (humanAIComponent == null)
					{
						return;
					}
					humanAIComponent.DisablePickUpForAgentIfNeeded();
				}
			}

			// Token: 0x04001C29 RID: 7209
			private int _lastActiveBannerBearerCount;

			// Token: 0x04001C2A RID: 7210
			private bool _requiresAgentStatUpdate;

			// Token: 0x04001C2B RID: 7211
			private BannerBearerLogic _bannerLogic;

			// Token: 0x04001C2C RID: 7212
			private Mission _mission;

			// Token: 0x04001C2D RID: 7213
			[TupleElementNames(new string[]
			{
				"bannerEntity",
				"lastDistance"
			})]
			private Dictionary<Agent, ValueTuple<GameEntity, float>> _bannerSearchers;

			// Token: 0x04001C2E RID: 7214
			private readonly Dictionary<UIntPtr, BannerBearerLogic.FormationBannerController.BannerInstance> _bannerInstances;

			// Token: 0x04001C2F RID: 7215
			private MBList<Agent> _nearbyAllyAgentsListCache = new MBList<Agent>();

			// Token: 0x0200067B RID: 1659
			public enum BannerState
			{
				// Token: 0x04002165 RID: 8549
				Initialized,
				// Token: 0x04002166 RID: 8550
				OnAgent,
				// Token: 0x04002167 RID: 8551
				OnGround
			}

			// Token: 0x0200067C RID: 1660
			public struct BannerInstance
			{
				// Token: 0x17000A37 RID: 2615
				// (get) Token: 0x06003DC9 RID: 15817 RVA: 0x000EDDE9 File Offset: 0x000EBFE9
				public bool IsOnGround
				{
					get
					{
						return this.State == BannerBearerLogic.FormationBannerController.BannerState.OnGround;
					}
				}

				// Token: 0x17000A38 RID: 2616
				// (get) Token: 0x06003DCA RID: 15818 RVA: 0x000EDDF4 File Offset: 0x000EBFF4
				public bool IsOnAgent
				{
					get
					{
						return this.State == BannerBearerLogic.FormationBannerController.BannerState.OnAgent;
					}
				}

				// Token: 0x06003DCB RID: 15819 RVA: 0x000EDDFF File Offset: 0x000EBFFF
				public BannerInstance(Agent bannerBearer, GameEntity entity, BannerBearerLogic.FormationBannerController.BannerState state)
				{
					this.BannerBearer = bannerBearer;
					this.Entity = entity;
					this.State = state;
				}

				// Token: 0x04002168 RID: 8552
				public readonly Agent BannerBearer;

				// Token: 0x04002169 RID: 8553
				public readonly GameEntity Entity;

				// Token: 0x0400216A RID: 8554
				private readonly BannerBearerLogic.FormationBannerController.BannerState State;
			}
		}
	}
}

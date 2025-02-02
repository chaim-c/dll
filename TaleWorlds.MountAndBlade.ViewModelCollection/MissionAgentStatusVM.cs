using System;
using System.ComponentModel;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD.DamageFeed;

namespace TaleWorlds.MountAndBlade.ViewModelCollection
{
	// Token: 0x02000005 RID: 5
	public class MissionAgentStatusVM : ViewModel
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022CE File Offset: 0x000004CE
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000022D6 File Offset: 0x000004D6
		public bool IsInDeployement { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022DF File Offset: 0x000004DF
		private MissionPeer _myMissionPeer
		{
			get
			{
				if (this._missionPeer != null)
				{
					return this._missionPeer;
				}
				if (GameNetwork.MyPeer != null)
				{
					this._missionPeer = GameNetwork.MyPeer.GetComponent<MissionPeer>();
				}
				return this._missionPeer;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002310 File Offset: 0x00000510
		public MissionAgentStatusVM(Mission mission, Camera missionCamera, Func<float> getCameraToggleProgress)
		{
			this.InteractionInterface = new AgentInteractionInterfaceVM(mission);
			this._mission = mission;
			this._missionCamera = missionCamera;
			this._getCameraToggleProgress = getCameraToggleProgress;
			this.PrimaryWeapon = new ImageIdentifierVM(ImageIdentifierType.Item);
			this.OffhandWeapon = new ImageIdentifierVM(ImageIdentifierType.Item);
			this.TakenDamageFeed = new MissionAgentDamageFeedVM();
			this.TakenDamageController = new MissionAgentTakenDamageVM(this._missionCamera);
			this.IsInteractionAvailable = true;
			this.RefreshValues();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000239A File Offset: 0x0000059A
		public void InitializeMainAgentPropterties()
		{
			Mission.Current.OnMainAgentChanged += this.OnMainAgentChanged;
			this.OnMainAgentChanged(this._mission, null);
			this.OnMainAgentWeaponChange();
			this._mpGameMode = Mission.Current.GetMissionBehavior<MissionMultiplayerGameModeBaseClient>();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023D5 File Offset: 0x000005D5
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.CameraToggleText = GameTexts.FindText("str_toggle_camera", null).ToString();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023F3 File Offset: 0x000005F3
		private void OnMainAgentChanged(object sender, PropertyChangedEventArgs e)
		{
			if (Agent.Main != null)
			{
				Agent main = Agent.Main;
				main.OnMainAgentWieldedItemChange = (Agent.OnMainAgentWieldedItemChangeDelegate)Delegate.Combine(main.OnMainAgentWieldedItemChange, new Agent.OnMainAgentWieldedItemChangeDelegate(this.OnMainAgentWeaponChange));
				this.OnMainAgentWeaponChange();
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002428 File Offset: 0x00000628
		public override void OnFinalize()
		{
			base.OnFinalize();
			Mission.Current.OnMainAgentChanged -= this.OnMainAgentChanged;
			this.TakenDamageFeed.OnFinalize();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002454 File Offset: 0x00000654
		public void Tick(float dt)
		{
			if (this._mission == null)
			{
				return;
			}
			this.CouchLanceState = this.GetCouchLanceState();
			this.SpearBraceState = this.GetSpearBraceState();
			Func<float> getCameraToggleProgress = this._getCameraToggleProgress;
			this.CameraToggleProgress = ((getCameraToggleProgress != null) ? getCameraToggleProgress() : 0f);
			if (this._mission.MainAgent != null && !this.IsInDeployement)
			{
				this.ShowAgentHealthBar = true;
				this.InteractionInterface.Tick();
				if (this._mission.Mode == MissionMode.Battle && !this._mission.IsFriendlyMission && this._myMissionPeer != null)
				{
					MissionPeer myMissionPeer = this._myMissionPeer;
					this.IsTroopsActive = (((myMissionPeer != null) ? myMissionPeer.ControlledFormation : null) != null);
					if (this.IsTroopsActive)
					{
						this.TroopCount = this._myMissionPeer.ControlledFormation.CountOfUnits;
						FormationClass defaultFormationGroup = (FormationClass)MultiplayerClassDivisions.GetMPHeroClassForPeer(this._myMissionPeer, false).TroopCharacter.DefaultFormationGroup;
						this.TroopsAmmoAvailable = (defaultFormationGroup == FormationClass.Ranged || defaultFormationGroup == FormationClass.HorseArcher);
						if (this.TroopsAmmoAvailable)
						{
							int totalCurrentAmmo = 0;
							int totalMaxAmmo = 0;
							this._myMissionPeer.ControlledFormation.ApplyActionOnEachUnit(delegate(Agent agent)
							{
								if (!agent.IsMainAgent)
								{
									int num;
									int num2;
									this.GetMaxAndCurrentAmmoOfAgent(agent, out num, out num2);
									totalCurrentAmmo += num;
									totalMaxAmmo += num2;
								}
							}, null);
							this.TroopsAmmoPercentage = (float)totalCurrentAmmo / (float)totalMaxAmmo;
						}
					}
				}
				this.UpdateWeaponStatuses();
				this.UpdateAgentAndMountStatuses();
				this.IsPlayerActive = true;
				this.IsCombatUIActive = true;
			}
			else
			{
				this.AgentHealth = 0;
				this.ShowMountHealthBar = false;
				this.ShowShieldHealthBar = false;
				if (this.IsCombatUIActive)
				{
					this._combatUIRemainTimer += dt;
					if (this._combatUIRemainTimer >= 3f)
					{
						this.IsCombatUIActive = false;
					}
				}
			}
			MissionMultiplayerGameModeBaseClient mpGameMode = this._mpGameMode;
			this.IsGoldActive = (mpGameMode != null && mpGameMode.IsGameModeUsingGold);
			if (this.IsGoldActive && this._myMissionPeer != null && this._myMissionPeer.GetNetworkPeer().IsSynchronized)
			{
				MissionMultiplayerGameModeBaseClient mpGameMode2 = this._mpGameMode;
				this.GoldAmount = ((mpGameMode2 != null) ? mpGameMode2.GetGoldAmount() : 0);
			}
			MissionAgentTakenDamageVM takenDamageController = this.TakenDamageController;
			if (takenDamageController == null)
			{
				return;
			}
			takenDamageController.Tick(dt);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002670 File Offset: 0x00000870
		private void UpdateWeaponStatuses()
		{
			bool isAmmoCountAlertEnabled = false;
			if (this._mission.MainAgent != null)
			{
				int ammoCount = -1;
				EquipmentIndex wieldedItemIndex = this._mission.MainAgent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
				EquipmentIndex wieldedItemIndex2 = this._mission.MainAgent.GetWieldedItemIndex(Agent.HandIndex.OffHand);
				if (wieldedItemIndex != EquipmentIndex.None && this._mission.MainAgent.Equipment[wieldedItemIndex].CurrentUsageItem != null)
				{
					if (this._mission.MainAgent.Equipment[wieldedItemIndex].CurrentUsageItem.IsRangedWeapon && this._mission.MainAgent.Equipment[wieldedItemIndex].CurrentUsageItem.IsConsumable)
					{
						int num;
						if (!this._mission.MainAgent.Equipment[wieldedItemIndex].Item.PrimaryWeapon.IsConsumable && this._mission.MainAgent.Equipment[wieldedItemIndex].CurrentUsageItem.IsConsumable)
						{
							num = 1;
						}
						else
						{
							num = this._mission.MainAgent.Equipment.GetAmmoAmount(wieldedItemIndex);
						}
						if (this._mission.MainAgent.Equipment[wieldedItemIndex].ModifiedMaxAmount == 1 || num > 0)
						{
							ammoCount = num;
						}
					}
					else if (this._mission.MainAgent.Equipment[wieldedItemIndex].CurrentUsageItem.IsRangedWeapon)
					{
						bool flag = this._mission.MainAgent.Equipment[wieldedItemIndex].CurrentUsageItem.WeaponClass == WeaponClass.Crossbow;
						ammoCount = this._mission.MainAgent.Equipment.GetAmmoAmount(wieldedItemIndex) + (int)(flag ? this._mission.MainAgent.Equipment[wieldedItemIndex].Ammo : 0);
					}
					if (!this._mission.MainAgent.Equipment[wieldedItemIndex].IsEmpty)
					{
						int num2;
						if (!this._mission.MainAgent.Equipment[wieldedItemIndex].Item.PrimaryWeapon.IsConsumable && this._mission.MainAgent.Equipment[wieldedItemIndex].CurrentUsageItem.IsConsumable)
						{
							num2 = 1;
						}
						else
						{
							num2 = this._mission.MainAgent.Equipment.GetMaxAmmo(wieldedItemIndex);
						}
						float f = (float)num2 * 0.2f;
						isAmmoCountAlertEnabled = (num2 != this.AmmoCount && this.AmmoCount <= MathF.Ceiling(f));
					}
				}
				if (wieldedItemIndex2 != EquipmentIndex.None && this._mission.MainAgent.Equipment[wieldedItemIndex2].CurrentUsageItem != null)
				{
					MissionWeapon missionWeapon = this._mission.MainAgent.Equipment[wieldedItemIndex2];
					this.ShowShieldHealthBar = missionWeapon.CurrentUsageItem.IsShield;
					if (this.ShowShieldHealthBar)
					{
						this.ShieldHealthMax = (int)missionWeapon.ModifiedMaxHitPoints;
						this.ShieldHealth = (int)missionWeapon.HitPoints;
					}
				}
				this.AmmoCount = ammoCount;
			}
			else
			{
				this.ShieldHealth = 0;
				this.AmmoCount = 0;
				this.ShowShieldHealthBar = false;
			}
			this.IsAmmoCountAlertEnabled = isAmmoCountAlertEnabled;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000029B3 File Offset: 0x00000BB3
		public void OnEquipmentInteractionViewToggled(bool isActive)
		{
			this.IsInteractionAvailable = !isActive;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000029C0 File Offset: 0x00000BC0
		private void UpdateAgentAndMountStatuses()
		{
			if (this._mission.MainAgent == null)
			{
				this.AgentHealthMax = 1;
				this.AgentHealth = (int)this._mission.MainAgent.Health;
				this.HorseHealthMax = 1;
				this.HorseHealth = 0;
				this.ShowMountHealthBar = false;
				return;
			}
			this.AgentHealthMax = (int)this._mission.MainAgent.HealthLimit;
			this.AgentHealth = (int)this._mission.MainAgent.Health;
			if (this._mission.MainAgent.MountAgent != null)
			{
				this.HorseHealthMax = (int)this._mission.MainAgent.MountAgent.HealthLimit;
				this.HorseHealth = (int)this._mission.MainAgent.MountAgent.Health;
				this.ShowMountHealthBar = true;
				return;
			}
			this.ShowMountHealthBar = false;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002A98 File Offset: 0x00000C98
		public void OnMainAgentWeaponChange()
		{
			if (this._mission.MainAgent == null)
			{
				return;
			}
			MissionWeapon missionWeapon = MissionWeapon.Invalid;
			MissionWeapon missionWeapon2 = MissionWeapon.Invalid;
			EquipmentIndex wieldedItemIndex = this._mission.MainAgent.GetWieldedItemIndex(Agent.HandIndex.OffHand);
			if (wieldedItemIndex > EquipmentIndex.None && wieldedItemIndex < EquipmentIndex.NumAllWeaponSlots)
			{
				missionWeapon = this._mission.MainAgent.Equipment[wieldedItemIndex];
			}
			wieldedItemIndex = this._mission.MainAgent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
			if (wieldedItemIndex > EquipmentIndex.None && wieldedItemIndex < EquipmentIndex.NumAllWeaponSlots)
			{
				missionWeapon2 = this._mission.MainAgent.Equipment[wieldedItemIndex];
			}
			WeaponComponentData currentUsageItem = missionWeapon.CurrentUsageItem;
			this.ShowShieldHealthBar = (currentUsageItem != null && currentUsageItem.IsShield);
			this.PrimaryWeapon = (missionWeapon2.IsEmpty ? new ImageIdentifierVM(ImageIdentifierType.Null) : new ImageIdentifierVM(missionWeapon2.Item, ""));
			this.OffhandWeapon = (missionWeapon.IsEmpty ? new ImageIdentifierVM(ImageIdentifierType.Null) : new ImageIdentifierVM(missionWeapon.Item, ""));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002B8A File Offset: 0x00000D8A
		public void OnAgentRemoved(Agent agent)
		{
			this.InteractionInterface.CheckAndClearFocusedAgent(agent);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002B98 File Offset: 0x00000D98
		public void OnAgentDeleted(Agent agent)
		{
			this.InteractionInterface.CheckAndClearFocusedAgent(agent);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002BA6 File Offset: 0x00000DA6
		public void OnMainAgentHit(int damage, float distance)
		{
			this.TakenDamageController.OnMainAgentHit(damage, distance);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002BB5 File Offset: 0x00000DB5
		public void OnFocusGained(Agent mainAgent, IFocusable focusableObject, bool isInteractable)
		{
			this.InteractionInterface.OnFocusGained(mainAgent, focusableObject, isInteractable);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002BC5 File Offset: 0x00000DC5
		public void OnFocusLost(Agent agent, IFocusable focusableObject)
		{
			this.InteractionInterface.OnFocusLost(agent, focusableObject);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public void OnSecondaryFocusGained(Agent agent, IFocusable focusableObject, bool isInteractable)
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002BD6 File Offset: 0x00000DD6
		public void OnSecondaryFocusLost(Agent agent, IFocusable focusableObject)
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public void OnAgentInteraction(Agent userAgent, Agent agent)
		{
			this.InteractionInterface.OnAgentInteraction(userAgent, agent);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002BE8 File Offset: 0x00000DE8
		private void GetMaxAndCurrentAmmoOfAgent(Agent agent, out int currentAmmo, out int maxAmmo)
		{
			currentAmmo = 0;
			maxAmmo = 0;
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.ExtraWeaponSlot; equipmentIndex++)
			{
				if (!agent.Equipment[equipmentIndex].IsEmpty && agent.Equipment[equipmentIndex].CurrentUsageItem.IsRangedWeapon)
				{
					currentAmmo = agent.Equipment.GetAmmoAmount(equipmentIndex);
					maxAmmo = agent.Equipment.GetMaxAmmo(equipmentIndex);
					return;
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002C58 File Offset: 0x00000E58
		private int GetCouchLanceState()
		{
			int result = 0;
			if (Agent.Main != null)
			{
				MissionWeapon wieldedWeapon = Agent.Main.WieldedWeapon;
				if (Agent.Main.HasMount && this.IsWeaponCouchable(wieldedWeapon))
				{
					if (this.IsPassiveUsageActiveWithCurrentWeapon(wieldedWeapon))
					{
						result = 3;
					}
					else if (this.IsConditionsMetForCouching())
					{
						result = 2;
					}
				}
			}
			return result;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002CA8 File Offset: 0x00000EA8
		private bool IsWeaponCouchable(MissionWeapon weapon)
		{
			if (weapon.IsEmpty)
			{
				return false;
			}
			foreach (WeaponComponentData weaponComponentData in weapon.Item.Weapons)
			{
				string weaponDescriptionId = weaponComponentData.WeaponDescriptionId;
				if (weaponDescriptionId != null && weaponDescriptionId.IndexOf("couch", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002D2C File Offset: 0x00000F2C
		private bool IsConditionsMetForCouching()
		{
			return Agent.Main.HasMount && Agent.Main.IsPassiveUsageConditionsAreMet;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002D48 File Offset: 0x00000F48
		private int GetSpearBraceState()
		{
			int result = 0;
			if (Agent.Main != null)
			{
				MissionWeapon wieldedWeapon = Agent.Main.WieldedWeapon;
				if (!Agent.Main.HasMount && Agent.Main.GetWieldedItemIndex(Agent.HandIndex.OffHand) == EquipmentIndex.None && this.IsWeaponBracable(wieldedWeapon))
				{
					if (this.IsPassiveUsageActiveWithCurrentWeapon(wieldedWeapon))
					{
						result = 3;
					}
					else if (this.IsConditionsMetForBracing())
					{
						result = 2;
					}
				}
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002DA4 File Offset: 0x00000FA4
		private bool IsWeaponBracable(MissionWeapon weapon)
		{
			if (weapon.IsEmpty)
			{
				return false;
			}
			foreach (WeaponComponentData weaponComponentData in weapon.Item.Weapons)
			{
				string weaponDescriptionId = weaponComponentData.WeaponDescriptionId;
				if (weaponDescriptionId != null && weaponDescriptionId.IndexOf("bracing", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002E28 File Offset: 0x00001028
		private bool IsConditionsMetForBracing()
		{
			return !Agent.Main.HasMount && !Agent.Main.WalkMode && Agent.Main.IsPassiveUsageConditionsAreMet;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002E4E File Offset: 0x0000104E
		private bool IsPassiveUsageActiveWithCurrentWeapon(MissionWeapon weapon)
		{
			return !weapon.IsEmpty && MBItem.GetItemIsPassiveUsage(weapon.CurrentUsageItem.ItemUsage);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002E6C File Offset: 0x0000106C
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002E74 File Offset: 0x00001074
		[DataSourceProperty]
		public MissionAgentTakenDamageVM TakenDamageController
		{
			get
			{
				return this._takenDamageController;
			}
			set
			{
				if (value != this._takenDamageController)
				{
					this._takenDamageController = value;
					base.OnPropertyChangedWithValue<MissionAgentTakenDamageVM>(value, "TakenDamageController");
				}
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002E92 File Offset: 0x00001092
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002E9A File Offset: 0x0000109A
		[DataSourceProperty]
		public AgentInteractionInterfaceVM InteractionInterface
		{
			get
			{
				return this._interactionInterface;
			}
			set
			{
				if (value != this._interactionInterface)
				{
					this._interactionInterface = value;
					base.OnPropertyChangedWithValue<AgentInteractionInterfaceVM>(value, "InteractionInterface");
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002EB8 File Offset: 0x000010B8
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002EC0 File Offset: 0x000010C0
		[DataSourceProperty]
		public int AgentHealth
		{
			get
			{
				return this._agentHealth;
			}
			set
			{
				if (value != this._agentHealth)
				{
					if (value <= 0)
					{
						this._agentHealth = 0;
						this.OffhandWeapon = new ImageIdentifierVM(ImageIdentifierType.Null);
						this.PrimaryWeapon = new ImageIdentifierVM(ImageIdentifierType.Null);
						this.AmmoCount = -1;
						this.ShieldHealth = 100;
						this.IsPlayerActive = false;
					}
					else
					{
						this._agentHealth = value;
					}
					base.OnPropertyChangedWithValue(value, "AgentHealth");
				}
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002F24 File Offset: 0x00001124
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002F2C File Offset: 0x0000112C
		[DataSourceProperty]
		public int AgentHealthMax
		{
			get
			{
				return this._agentHealthMax;
			}
			set
			{
				if (value != this._agentHealthMax)
				{
					this._agentHealthMax = value;
					base.OnPropertyChangedWithValue(value, "AgentHealthMax");
				}
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002F4A File Offset: 0x0000114A
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002F52 File Offset: 0x00001152
		[DataSourceProperty]
		public int HorseHealth
		{
			get
			{
				return this._horseHealth;
			}
			set
			{
				if (value != this._horseHealth)
				{
					this._horseHealth = value;
					base.OnPropertyChangedWithValue(value, "HorseHealth");
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002F70 File Offset: 0x00001170
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002F78 File Offset: 0x00001178
		[DataSourceProperty]
		public int HorseHealthMax
		{
			get
			{
				return this._horseHealthMax;
			}
			set
			{
				if (value != this._horseHealthMax)
				{
					this._horseHealthMax = value;
					base.OnPropertyChangedWithValue(value, "HorseHealthMax");
				}
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002F96 File Offset: 0x00001196
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002F9E File Offset: 0x0000119E
		[DataSourceProperty]
		public int ShieldHealth
		{
			get
			{
				return this._shieldHealth;
			}
			set
			{
				if (value != this._shieldHealth)
				{
					this._shieldHealth = value;
					base.OnPropertyChangedWithValue(value, "ShieldHealth");
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002FBC File Offset: 0x000011BC
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002FC4 File Offset: 0x000011C4
		[DataSourceProperty]
		public int ShieldHealthMax
		{
			get
			{
				return this._shieldHealthMax;
			}
			set
			{
				if (value != this._shieldHealthMax)
				{
					this._shieldHealthMax = value;
					base.OnPropertyChangedWithValue(value, "ShieldHealthMax");
				}
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002FE2 File Offset: 0x000011E2
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002FEA File Offset: 0x000011EA
		[DataSourceProperty]
		public bool IsPlayerActive
		{
			get
			{
				return this._isPlayerActive;
			}
			set
			{
				if (value != this._isPlayerActive)
				{
					this._isPlayerActive = value;
					base.OnPropertyChangedWithValue(value, "IsPlayerActive");
				}
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003008 File Offset: 0x00001208
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00003010 File Offset: 0x00001210
		public bool IsCombatUIActive
		{
			get
			{
				return this._isCombatUIActive;
			}
			set
			{
				if (value != this._isCombatUIActive)
				{
					this._isCombatUIActive = value;
					base.OnPropertyChangedWithValue(value, "IsCombatUIActive");
					this._combatUIRemainTimer = 0f;
				}
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003039 File Offset: 0x00001239
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00003041 File Offset: 0x00001241
		[DataSourceProperty]
		public bool ShowAgentHealthBar
		{
			get
			{
				return this._showAgentHealthBar;
			}
			set
			{
				if (value != this._showAgentHealthBar)
				{
					this._showAgentHealthBar = value;
					base.OnPropertyChangedWithValue(value, "ShowAgentHealthBar");
				}
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000305F File Offset: 0x0000125F
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00003067 File Offset: 0x00001267
		[DataSourceProperty]
		public bool ShowMountHealthBar
		{
			get
			{
				return this._showMountHealthBar;
			}
			set
			{
				if (value != this._showMountHealthBar)
				{
					this._showMountHealthBar = value;
					base.OnPropertyChangedWithValue(value, "ShowMountHealthBar");
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003085 File Offset: 0x00001285
		// (set) Token: 0x06000047 RID: 71 RVA: 0x0000308D File Offset: 0x0000128D
		[DataSourceProperty]
		public bool ShowShieldHealthBar
		{
			get
			{
				return this._showShieldHealthBar;
			}
			set
			{
				if (value != this._showShieldHealthBar)
				{
					this._showShieldHealthBar = value;
					base.OnPropertyChangedWithValue(value, "ShowShieldHealthBar");
				}
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000030AB File Offset: 0x000012AB
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000030B3 File Offset: 0x000012B3
		[DataSourceProperty]
		public bool IsInteractionAvailable
		{
			get
			{
				return this._isInteractionAvailable;
			}
			set
			{
				if (value != this._isInteractionAvailable)
				{
					this._isInteractionAvailable = value;
					base.OnPropertyChangedWithValue(value, "IsInteractionAvailable");
				}
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000030D1 File Offset: 0x000012D1
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000030D9 File Offset: 0x000012D9
		[DataSourceProperty]
		public bool IsAgentStatusAvailable
		{
			get
			{
				return this._isAgentStatusAvailable;
			}
			set
			{
				if (value != this._isAgentStatusAvailable)
				{
					this._isAgentStatusAvailable = value;
					base.OnPropertyChangedWithValue(value, "IsAgentStatusAvailable");
				}
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000030F7 File Offset: 0x000012F7
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000030FF File Offset: 0x000012FF
		[DataSourceProperty]
		public int CouchLanceState
		{
			get
			{
				return this._couchLanceState;
			}
			set
			{
				if (value != this._couchLanceState)
				{
					this._couchLanceState = value;
					base.OnPropertyChangedWithValue(value, "CouchLanceState");
				}
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000311D File Offset: 0x0000131D
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00003125 File Offset: 0x00001325
		[DataSourceProperty]
		public int SpearBraceState
		{
			get
			{
				return this._spearBraceState;
			}
			set
			{
				if (value != this._spearBraceState)
				{
					this._spearBraceState = value;
					base.OnPropertyChangedWithValue(value, "SpearBraceState");
				}
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003143 File Offset: 0x00001343
		// (set) Token: 0x06000051 RID: 81 RVA: 0x0000314B File Offset: 0x0000134B
		[DataSourceProperty]
		public int TroopCount
		{
			get
			{
				return this._troopCount;
			}
			set
			{
				if (value != this._troopCount)
				{
					this._troopCount = value;
					base.OnPropertyChangedWithValue(value, "TroopCount");
				}
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003169 File Offset: 0x00001369
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00003171 File Offset: 0x00001371
		[DataSourceProperty]
		public bool IsTroopsActive
		{
			get
			{
				return this._isTroopsActive;
			}
			set
			{
				if (value != this._isTroopsActive)
				{
					this._isTroopsActive = value;
					base.OnPropertyChangedWithValue(value, "IsTroopsActive");
				}
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000318F File Offset: 0x0000138F
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00003197 File Offset: 0x00001397
		[DataSourceProperty]
		public bool IsGoldActive
		{
			get
			{
				return this._isGoldActive;
			}
			set
			{
				if (value != this._isGoldActive)
				{
					this._isGoldActive = value;
					base.OnPropertyChangedWithValue(value, "IsGoldActive");
				}
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000031B5 File Offset: 0x000013B5
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000031BD File Offset: 0x000013BD
		[DataSourceProperty]
		public int GoldAmount
		{
			get
			{
				return this._goldAmount;
			}
			set
			{
				if (value != this._goldAmount)
				{
					this._goldAmount = value;
					base.OnPropertyChangedWithValue(value, "GoldAmount");
				}
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000031DB File Offset: 0x000013DB
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000031E3 File Offset: 0x000013E3
		[DataSourceProperty]
		public bool ShowAmmoCount
		{
			get
			{
				return this._showAmmoCount;
			}
			set
			{
				if (value != this._showAmmoCount)
				{
					this._showAmmoCount = value;
					base.OnPropertyChangedWithValue(value, "ShowAmmoCount");
				}
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003201 File Offset: 0x00001401
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003209 File Offset: 0x00001409
		[DataSourceProperty]
		public int AmmoCount
		{
			get
			{
				return this._ammoCount;
			}
			set
			{
				if (value != this._ammoCount)
				{
					this._ammoCount = value;
					base.OnPropertyChangedWithValue(value, "AmmoCount");
					this.ShowAmmoCount = (value >= 0);
				}
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003234 File Offset: 0x00001434
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000323C File Offset: 0x0000143C
		[DataSourceProperty]
		public float TroopsAmmoPercentage
		{
			get
			{
				return this._troopsAmmoPercentage;
			}
			set
			{
				if (value != this._troopsAmmoPercentage)
				{
					this._troopsAmmoPercentage = value;
					base.OnPropertyChangedWithValue(value, "TroopsAmmoPercentage");
				}
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000325A File Offset: 0x0000145A
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00003262 File Offset: 0x00001462
		[DataSourceProperty]
		public bool TroopsAmmoAvailable
		{
			get
			{
				return this._troopsAmmoAvailable;
			}
			set
			{
				if (value != this._troopsAmmoAvailable)
				{
					this._troopsAmmoAvailable = value;
					base.OnPropertyChangedWithValue(value, "TroopsAmmoAvailable");
				}
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003280 File Offset: 0x00001480
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00003288 File Offset: 0x00001488
		[DataSourceProperty]
		public bool IsAmmoCountAlertEnabled
		{
			get
			{
				return this._isAmmoCountAlertEnabled;
			}
			set
			{
				if (value != this._isAmmoCountAlertEnabled)
				{
					this._isAmmoCountAlertEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsAmmoCountAlertEnabled");
				}
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000032A6 File Offset: 0x000014A6
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000032AE File Offset: 0x000014AE
		[DataSourceProperty]
		public float CameraToggleProgress
		{
			get
			{
				return this._cameraToggleProgress;
			}
			set
			{
				if (value != this._cameraToggleProgress)
				{
					this._cameraToggleProgress = value;
					base.OnPropertyChangedWithValue(value, "CameraToggleProgress");
				}
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000032CC File Offset: 0x000014CC
		// (set) Token: 0x06000065 RID: 101 RVA: 0x000032D4 File Offset: 0x000014D4
		[DataSourceProperty]
		public string CameraToggleText
		{
			get
			{
				return this._cameraToggleText;
			}
			set
			{
				if (value != this._cameraToggleText)
				{
					this._cameraToggleText = value;
					base.OnPropertyChangedWithValue<string>(value, "CameraToggleText");
				}
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000032F7 File Offset: 0x000014F7
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000032FF File Offset: 0x000014FF
		[DataSourceProperty]
		public ImageIdentifierVM OffhandWeapon
		{
			get
			{
				return this._offhandWeapon;
			}
			set
			{
				if (value != this._offhandWeapon)
				{
					this._offhandWeapon = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "OffhandWeapon");
				}
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000331D File Offset: 0x0000151D
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003325 File Offset: 0x00001525
		[DataSourceProperty]
		public ImageIdentifierVM PrimaryWeapon
		{
			get
			{
				return this._primaryWeapon;
			}
			set
			{
				if (value != this._primaryWeapon)
				{
					this._primaryWeapon = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "PrimaryWeapon");
				}
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003343 File Offset: 0x00001543
		// (set) Token: 0x0600006B RID: 107 RVA: 0x0000334B File Offset: 0x0000154B
		[DataSourceProperty]
		public MissionAgentDamageFeedVM TakenDamageFeed
		{
			get
			{
				return this._takenDamageFeed;
			}
			set
			{
				if (value != this._takenDamageFeed)
				{
					this._takenDamageFeed = value;
					base.OnPropertyChangedWithValue<MissionAgentDamageFeedVM>(value, "TakenDamageFeed");
				}
			}
		}

		// Token: 0x0400000A RID: 10
		private const string _couchLanceUsageString = "couch";

		// Token: 0x0400000B RID: 11
		private const string _spearBraceUsageString = "spear";

		// Token: 0x0400000D RID: 13
		private readonly Mission _mission;

		// Token: 0x0400000E RID: 14
		private readonly Camera _missionCamera;

		// Token: 0x0400000F RID: 15
		private float _combatUIRemainTimer;

		// Token: 0x04000010 RID: 16
		private MissionPeer _missionPeer;

		// Token: 0x04000011 RID: 17
		private MissionMultiplayerGameModeBaseClient _mpGameMode;

		// Token: 0x04000012 RID: 18
		private readonly Func<float> _getCameraToggleProgress;

		// Token: 0x04000013 RID: 19
		private int _agentHealth;

		// Token: 0x04000014 RID: 20
		private int _agentHealthMax;

		// Token: 0x04000015 RID: 21
		private int _horseHealth;

		// Token: 0x04000016 RID: 22
		private int _horseHealthMax;

		// Token: 0x04000017 RID: 23
		private int _shieldHealth;

		// Token: 0x04000018 RID: 24
		private int _shieldHealthMax;

		// Token: 0x04000019 RID: 25
		private bool _isPlayerActive = true;

		// Token: 0x0400001A RID: 26
		private bool _isCombatUIActive;

		// Token: 0x0400001B RID: 27
		private bool _showAgentHealthBar;

		// Token: 0x0400001C RID: 28
		private bool _showMountHealthBar;

		// Token: 0x0400001D RID: 29
		private bool _showShieldHealthBar;

		// Token: 0x0400001E RID: 30
		private bool _troopsAmmoAvailable;

		// Token: 0x0400001F RID: 31
		private bool _isAgentStatusAvailable;

		// Token: 0x04000020 RID: 32
		private bool _isInteractionAvailable;

		// Token: 0x04000021 RID: 33
		private float _troopsAmmoPercentage;

		// Token: 0x04000022 RID: 34
		private int _troopCount;

		// Token: 0x04000023 RID: 35
		private int _goldAmount;

		// Token: 0x04000024 RID: 36
		private bool _isTroopsActive;

		// Token: 0x04000025 RID: 37
		private bool _isGoldActive;

		// Token: 0x04000026 RID: 38
		private AgentInteractionInterfaceVM _interactionInterface;

		// Token: 0x04000027 RID: 39
		private ImageIdentifierVM _offhandWeapon;

		// Token: 0x04000028 RID: 40
		private ImageIdentifierVM _primaryWeapon;

		// Token: 0x04000029 RID: 41
		private MissionAgentTakenDamageVM _takenDamageController;

		// Token: 0x0400002A RID: 42
		private MissionAgentDamageFeedVM _takenDamageFeed;

		// Token: 0x0400002B RID: 43
		private int _ammoCount;

		// Token: 0x0400002C RID: 44
		private int _couchLanceState = -1;

		// Token: 0x0400002D RID: 45
		private int _spearBraceState = -1;

		// Token: 0x0400002E RID: 46
		private bool _showAmmoCount;

		// Token: 0x0400002F RID: 47
		private bool _isAmmoCountAlertEnabled;

		// Token: 0x04000030 RID: 48
		private float _cameraToggleProgress;

		// Token: 0x04000031 RID: 49
		private string _cameraToggleText;

		// Token: 0x0200007A RID: 122
		private enum PassiveUsageStates
		{
			// Token: 0x040004DE RID: 1246
			NotPossible,
			// Token: 0x040004DF RID: 1247
			ConditionsNotMet,
			// Token: 0x040004E0 RID: 1248
			Possible,
			// Token: 0x040004E1 RID: 1249
			Active
		}
	}
}

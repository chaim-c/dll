using System;
using System.ComponentModel;
using NetworkMessages.FromClient;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.MountAndBlade.ViewModelCollection;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission
{
	// Token: 0x02000028 RID: 40
	[OverrideView(typeof(MissionMainAgentEquipDropView))]
	public class MissionGauntletMainAgentEquipDropView : MissionView
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000A1D1 File Offset: 0x000083D1
		private bool IsDisplayingADialog
		{
			get
			{
				IMissionScreen missionScreenAsInterface = this._missionScreenAsInterface;
				return (missionScreenAsInterface != null && missionScreenAsInterface.GetDisplayDialog()) || base.MissionScreen.IsRadialMenuActive || base.Mission.IsOrderMenuOpen;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000A201 File Offset: 0x00008401
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x0000A209 File Offset: 0x00008409
		private bool HoldHandled
		{
			get
			{
				return this._holdHandled;
			}
			set
			{
				this._holdHandled = value;
				MissionScreen missionScreen = base.MissionScreen;
				if (missionScreen == null)
				{
					return;
				}
				missionScreen.SetRadialMenuActiveState(value);
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000A223 File Offset: 0x00008423
		public MissionGauntletMainAgentEquipDropView()
		{
			this._missionScreenAsInterface = base.MissionScreen;
			this.HoldHandled = false;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000A240 File Offset: 0x00008440
		public override void EarlyStart()
		{
			base.EarlyStart();
			this._gauntletLayer = new GauntletLayer(3, "GauntletLayer", false);
			this._dataSource = new MissionMainAgentControllerEquipDropVM(new Action<EquipmentIndex>(this.OnToggleItem));
			this._missionMainAgentController = base.Mission.GetMissionBehavior<MissionMainAgentController>();
			this._missionControllerLeaveLogic = base.Mission.GetMissionBehavior<EquipmentControllerLeaveLogic>();
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("CombatHotKeyCategory"));
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.Invalid);
			this._gauntletLayer.LoadMovie("MainAgentControllerEquipDrop", this._dataSource);
			base.MissionScreen.AddLayer(this._gauntletLayer);
			base.Mission.OnMainAgentChanged += this.OnMainAgentChanged;
			TaleWorlds.InputSystem.Input.OnGamepadActiveStateChanged = (Action)Delegate.Combine(TaleWorlds.InputSystem.Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveChanged));
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000A329 File Offset: 0x00008529
		public override void AfterStart()
		{
			base.AfterStart();
			this._dataSource.InitializeMainAgentPropterties();
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000A33C File Offset: 0x0000853C
		public override void OnMissionScreenFinalize()
		{
			base.OnMissionScreenFinalize();
			TaleWorlds.InputSystem.Input.OnGamepadActiveStateChanged = (Action)Delegate.Remove(TaleWorlds.InputSystem.Input.OnGamepadActiveStateChanged, new Action(this.OnGamepadActiveChanged));
			base.Mission.OnMainAgentChanged -= this.OnMainAgentChanged;
			base.MissionScreen.RemoveLayer(this._gauntletLayer);
			this._gauntletLayer = null;
			this._dataSource.OnFinalize();
			this._dataSource = null;
			this._missionMainAgentController = null;
			this._missionControllerLeaveLogic = null;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000A3C0 File Offset: 0x000085C0
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			if (this._dataSource.IsActive && !this.IsMainAgentAvailable())
			{
				this.HandleClosingHold();
			}
			if (this.IsMainAgentAvailable() && (!base.MissionScreen.IsRadialMenuActive || this._dataSource.IsActive))
			{
				this.TickControls(dt);
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000A418 File Offset: 0x00008618
		private void OnMainAgentChanged(object sender, PropertyChangedEventArgs e)
		{
			if (base.Mission.MainAgent == null)
			{
				if (this.HoldHandled)
				{
					this.HoldHandled = false;
				}
				this._toggleHoldTime = 0f;
				this._dataSource.OnCancelHoldController();
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000A44C File Offset: 0x0000864C
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			if (affectedAgent == Agent.Main)
			{
				this.HandleClosingHold();
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000A45C File Offset: 0x0000865C
		private void TickControls(float dt)
		{
			if (base.MissionScreen.SceneLayer.Input.IsGameKeyDown(34) && !this.IsDisplayingADialog && this.IsMainAgentAvailable() && base.Mission.Mode != MissionMode.Deployment && base.Mission.Mode != MissionMode.CutScene && !base.MissionScreen.IsRadialMenuActive)
			{
				if (this._toggleHoldTime > 0.3f && !this.HoldHandled)
				{
					this.HandleOpeningHold();
					this.HoldHandled = true;
				}
				this._toggleHoldTime += dt;
				this._prevKeyDown = true;
			}
			else if (this._prevKeyDown && !base.MissionScreen.SceneLayer.Input.IsGameKeyDown(34))
			{
				if (this._toggleHoldTime < 0.3f)
				{
					this.HandleQuickRelease();
				}
				else
				{
					this.HandleClosingHold();
				}
				this.HoldHandled = false;
				this._toggleHoldTime = 0f;
				this._weaponDropHoldTime = 0f;
				this._prevKeyDown = false;
				this._weaponDropHandled = false;
			}
			if (this.HoldHandled)
			{
				int keyWeaponIndex = this.GetKeyWeaponIndex(false);
				int keyWeaponIndex2 = this.GetKeyWeaponIndex(true);
				this._dataSource.SetDropProgressForIndex(EquipmentIndex.None, this._weaponDropHoldTime / 0.5f);
				if (keyWeaponIndex != -1)
				{
					if (!this._weaponDropHandled)
					{
						int num = keyWeaponIndex;
						if (this._weaponDropHoldTime > 0.5f && !Agent.Main.Equipment[num].IsEmpty)
						{
							this.OnDropEquipment((EquipmentIndex)num);
							this._dataSource.OnWeaponDroppedAtIndex(keyWeaponIndex);
							this._weaponDropHandled = true;
						}
						this._dataSource.SetDropProgressForIndex((EquipmentIndex)num, this._weaponDropHoldTime / 0.5f);
					}
					this._weaponDropHoldTime += dt;
					return;
				}
				if (keyWeaponIndex2 != -1)
				{
					if (!this._weaponDropHandled)
					{
						int num2 = keyWeaponIndex2;
						if (!Agent.Main.Equipment[num2].IsEmpty)
						{
							this.OnToggleItem((EquipmentIndex)num2);
							this._dataSource.OnWeaponEquippedAtIndex(keyWeaponIndex2);
							this._weaponDropHandled = true;
						}
					}
					this._weaponDropHoldTime = 0f;
					return;
				}
				this._weaponDropHoldTime = 0f;
				this._weaponDropHandled = false;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000A668 File Offset: 0x00008868
		private void HandleOpeningHold()
		{
			MissionMainAgentControllerEquipDropVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.OnToggle(true);
			}
			base.MissionScreen.SetRadialMenuActiveState(true);
			EquipmentControllerLeaveLogic missionControllerLeaveLogic = this._missionControllerLeaveLogic;
			if (missionControllerLeaveLogic != null)
			{
				missionControllerLeaveLogic.SetIsEquipmentSelectionActive(true);
			}
			if (!GameNetwork.IsMultiplayer && !this._isSlowDownApplied)
			{
				base.Mission.AddTimeSpeedRequest(new Mission.TimeSpeedRequest(0.25f, 624));
				this._isSlowDownApplied = true;
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000A6D8 File Offset: 0x000088D8
		private void HandleClosingHold()
		{
			MissionMainAgentControllerEquipDropVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.OnToggle(false);
			}
			base.MissionScreen.SetRadialMenuActiveState(false);
			EquipmentControllerLeaveLogic missionControllerLeaveLogic = this._missionControllerLeaveLogic;
			if (missionControllerLeaveLogic != null)
			{
				missionControllerLeaveLogic.SetIsEquipmentSelectionActive(false);
			}
			if (!GameNetwork.IsMultiplayer && this._isSlowDownApplied)
			{
				base.Mission.RemoveTimeSpeedRequest(624);
				this._isSlowDownApplied = false;
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000A73B File Offset: 0x0000893B
		private void HandleQuickRelease()
		{
			this._missionMainAgentController.OnWeaponUsageToggleRequested();
			MissionMainAgentControllerEquipDropVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.OnToggle(false);
			}
			base.MissionScreen.SetRadialMenuActiveState(false);
			EquipmentControllerLeaveLogic missionControllerLeaveLogic = this._missionControllerLeaveLogic;
			if (missionControllerLeaveLogic == null)
			{
				return;
			}
			missionControllerLeaveLogic.SetIsEquipmentSelectionActive(false);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000A778 File Offset: 0x00008978
		private void OnToggleItem(EquipmentIndex indexToToggle)
		{
			bool flag = indexToToggle == Agent.Main.GetWieldedItemIndex(Agent.HandIndex.MainHand);
			bool flag2 = indexToToggle == Agent.Main.GetWieldedItemIndex(Agent.HandIndex.OffHand);
			if (flag || flag2)
			{
				Agent.Main.TryToSheathWeaponInHand(flag ? Agent.HandIndex.MainHand : Agent.HandIndex.OffHand, Agent.WeaponWieldActionType.WithAnimation);
				return;
			}
			Agent.Main.TryToWieldWeaponInSlot(indexToToggle, Agent.WeaponWieldActionType.WithAnimation, false);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000A7C8 File Offset: 0x000089C8
		private void OnDropEquipment(EquipmentIndex indexToDrop)
		{
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new DropWeapon(base.Input.IsGameKeyDown(10), indexToDrop));
				GameNetwork.EndModuleEventAsClient();
				return;
			}
			Agent.Main.HandleDropWeapon(base.Input.IsGameKeyDown(10), indexToDrop);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000A817 File Offset: 0x00008A17
		private bool IsMainAgentAvailable()
		{
			Agent main = Agent.Main;
			return main != null && main.IsActive();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000A829 File Offset: 0x00008A29
		public override void OnPhotoModeActivated()
		{
			base.OnPhotoModeActivated();
			this._gauntletLayer.UIContext.ContextAlpha = 0f;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000A846 File Offset: 0x00008A46
		public override void OnPhotoModeDeactivated()
		{
			base.OnPhotoModeDeactivated();
			this._gauntletLayer.UIContext.ContextAlpha = 1f;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000A863 File Offset: 0x00008A63
		private void OnGamepadActiveChanged()
		{
			this._dataSource.OnGamepadActiveChanged(TaleWorlds.InputSystem.Input.IsGamepadActive);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000A878 File Offset: 0x00008A78
		private int GetKeyWeaponIndex(bool isReleased)
		{
			Func<string, bool> func;
			Func<string, bool> func2;
			if (isReleased)
			{
				func = new Func<string, bool>(base.MissionScreen.SceneLayer.Input.IsHotKeyReleased);
				func2 = new Func<string, bool>(this._gauntletLayer.Input.IsHotKeyReleased);
			}
			else
			{
				func = new Func<string, bool>(base.MissionScreen.SceneLayer.Input.IsHotKeyDown);
				func2 = new Func<string, bool>(this._gauntletLayer.Input.IsHotKeyDown);
			}
			string text = string.Empty;
			if (func("ControllerEquipDropWeapon1") || func2("ControllerEquipDropWeapon1"))
			{
				text = "ControllerEquipDropWeapon1";
			}
			else if (func("ControllerEquipDropWeapon2") || func2("ControllerEquipDropWeapon2"))
			{
				text = "ControllerEquipDropWeapon2";
			}
			else if (func("ControllerEquipDropWeapon3") || func2("ControllerEquipDropWeapon3"))
			{
				text = "ControllerEquipDropWeapon3";
			}
			else if (func("ControllerEquipDropWeapon4") || func2("ControllerEquipDropWeapon4"))
			{
				text = "ControllerEquipDropWeapon4";
			}
			if (!string.IsNullOrEmpty(text))
			{
				for (int i = 0; i < this._dataSource.EquippedWeapons.Count; i++)
				{
					InputKeyItemVM shortcutKey = this._dataSource.EquippedWeapons[i].ShortcutKey;
					if (((shortcutKey != null) ? shortcutKey.HotKey.Id : null) == text)
					{
						return (int)this._dataSource.EquippedWeapons[i].Identifier;
					}
				}
			}
			return -1;
		}

		// Token: 0x040000EA RID: 234
		private const int _missionTimeSpeedRequestID = 624;

		// Token: 0x040000EB RID: 235
		private const float _slowDownAmountWhileRadialIsOpen = 0.25f;

		// Token: 0x040000EC RID: 236
		private bool _isSlowDownApplied;

		// Token: 0x040000ED RID: 237
		private GauntletLayer _gauntletLayer;

		// Token: 0x040000EE RID: 238
		private MissionMainAgentControllerEquipDropVM _dataSource;

		// Token: 0x040000EF RID: 239
		private MissionMainAgentController _missionMainAgentController;

		// Token: 0x040000F0 RID: 240
		private EquipmentControllerLeaveLogic _missionControllerLeaveLogic;

		// Token: 0x040000F1 RID: 241
		private const float _minOpenHoldTime = 0.3f;

		// Token: 0x040000F2 RID: 242
		private const float _minDropHoldTime = 0.5f;

		// Token: 0x040000F3 RID: 243
		private readonly IMissionScreen _missionScreenAsInterface;

		// Token: 0x040000F4 RID: 244
		private bool _holdHandled;

		// Token: 0x040000F5 RID: 245
		private float _toggleHoldTime;

		// Token: 0x040000F6 RID: 246
		private float _weaponDropHoldTime;

		// Token: 0x040000F7 RID: 247
		private bool _prevKeyDown;

		// Token: 0x040000F8 RID: 248
		private bool _weaponDropHandled;
	}
}

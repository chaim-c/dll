using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.MissionRepresentatives;

namespace TaleWorlds.MountAndBlade.ViewModelCollection
{
	// Token: 0x02000007 RID: 7
	public class AgentInteractionInterfaceVM : ViewModel
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003369 File Offset: 0x00001569
		private bool IsPlayerActive
		{
			get
			{
				Agent main = Agent.Main;
				return main != null && main.Health > 0f;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003382 File Offset: 0x00001582
		public AgentInteractionInterfaceVM(Mission mission)
		{
			this._mission = mission;
			this.IsActive = false;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003398 File Offset: 0x00001598
		internal void Tick()
		{
			if (this.IsActive && this._mission.Mode == MissionMode.StartUp && this._currentFocusedObject is Agent && ((Agent)this._currentFocusedObject).IsEnemyOf(this._mission.MainAgent))
			{
				this.IsActive = false;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000033EB File Offset: 0x000015EB
		internal void CheckAndClearFocusedAgent(Agent agent)
		{
			if (this._currentFocusedObject != null && this._currentFocusedObject as Agent == agent)
			{
				this.IsActive = false;
				this.ResetFocus();
				this.SecondaryInteractionMessage = "";
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000341B File Offset: 0x0000161B
		public void OnFocusedHealthChanged(IFocusable focusable, float healthPercentage, bool hideHealthbarWhenFull)
		{
			this.SetHealth(healthPercentage, hideHealthbarWhenFull);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003428 File Offset: 0x00001628
		internal void OnFocusGained(Agent mainAgent, IFocusable focusableObject, bool isInteractable)
		{
			if (this.IsPlayerActive && (this._currentFocusedObject != focusableObject || this._currentObjectInteractable != isInteractable))
			{
				this.ResetFocus();
				this._currentFocusedObject = focusableObject;
				this._currentObjectInteractable = isInteractable;
				Agent agent;
				UsableMissionObject usableMissionObject;
				if ((agent = (focusableObject as Agent)) != null)
				{
					if (agent.IsHuman)
					{
						this.SetAgent(mainAgent, agent, isInteractable);
						return;
					}
					this.SetMount(mainAgent, agent, isInteractable);
					return;
				}
				else if ((usableMissionObject = (focusableObject as UsableMissionObject)) != null)
				{
					SpawnedItemEntity spawnedItemEntity;
					if ((spawnedItemEntity = (usableMissionObject as SpawnedItemEntity)) != null)
					{
						bool canQuickPickup = Agent.Main.CanQuickPickUp(spawnedItemEntity);
						this.SetItem(spawnedItemEntity, canQuickPickup, isInteractable);
						return;
					}
					this.SetUsableMissionObject(usableMissionObject, isInteractable);
					return;
				}
				else
				{
					UsableMachine machine;
					if ((machine = (focusableObject as UsableMachine)) != null)
					{
						this.SetUsableMachine(machine, isInteractable);
						return;
					}
					DestructableComponent machine2;
					if ((machine2 = (focusableObject as DestructableComponent)) != null)
					{
						this.SetDestructibleComponent(machine2, false);
					}
				}
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000034EB File Offset: 0x000016EB
		internal void OnFocusLost(Agent agent, IFocusable focusableObject)
		{
			this.ResetFocus();
			this.IsActive = false;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000034FA File Offset: 0x000016FA
		internal void OnAgentInteraction(Agent userAgent, Agent agent)
		{
			if (this._mission.Mode == MissionMode.Stealth && agent.IsHuman && agent.IsActive() && !agent.IsEnemyOf(userAgent))
			{
				this.SetAgent(userAgent, agent, true);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000352C File Offset: 0x0000172C
		private void SetItem(SpawnedItemEntity item, bool canQuickPickup, bool isInteractable)
		{
			this.IsFocusedOnExit = false;
			EquipmentIndex equipmentIndex;
			ItemObject weaponToReplaceOnQuickAction = Agent.Main.GetWeaponToReplaceOnQuickAction(item, out equipmentIndex);
			bool fillUp = equipmentIndex != EquipmentIndex.None && !Agent.Main.Equipment[equipmentIndex].IsEmpty && Agent.Main.Equipment[equipmentIndex].IsAnyConsumable() && Agent.Main.Equipment[equipmentIndex].Amount < Agent.Main.Equipment[equipmentIndex].ModifiedMaxAmount;
			TextObject actionMessage = item.GetActionMessage(weaponToReplaceOnQuickAction, fillUp);
			TextObject descriptionMessage = item.GetDescriptionMessage(fillUp);
			if (!TextObject.IsNullOrEmpty(actionMessage) && !TextObject.IsNullOrEmpty(descriptionMessage))
			{
				this.FocusType = 0;
				if (isInteractable)
				{
					MBTextManager.SetTextVariable("USE_KEY", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("CombatHotKeyCategory", 13)), false);
					if (canQuickPickup)
					{
						Agent main = Agent.Main;
						if (main != null && main.CanInteractableWeaponBePickedUp(item))
						{
							this.PrimaryInteractionMessage = actionMessage.ToString();
							MBTextManager.SetTextVariable("KEY", GameTexts.FindText("str_ui_agent_interaction_use", null), false);
							MBTextManager.SetTextVariable("ACTION", GameTexts.FindText("str_select_item_to_replace", null), false);
							this.SecondaryInteractionMessage = GameTexts.FindText("str_hold_key_action", null).ToString() + this.GetWeaponSpecificText(item);
						}
						else
						{
							this.PrimaryInteractionMessage = actionMessage.ToString();
							MBTextManager.SetTextVariable("STR1", descriptionMessage, false);
							MBTextManager.SetTextVariable("STR2", this.GetWeaponSpecificText(item), false);
							this.SecondaryInteractionMessage = GameTexts.FindText("str_STR1_space_STR2", null).ToString();
						}
					}
					else
					{
						MBTextManager.SetTextVariable("KEY", GameTexts.FindText("str_ui_agent_interaction_use", null), false);
						MBTextManager.SetTextVariable("ACTION", GameTexts.FindText("str_select_item_to_replace", null), false);
						this.PrimaryInteractionMessage = GameTexts.FindText("str_hold_key_action", null).ToString();
						MBTextManager.SetTextVariable("STR1", descriptionMessage, false);
						MBTextManager.SetTextVariable("STR2", this.GetWeaponSpecificText(item), false);
						this.SecondaryInteractionMessage = GameTexts.FindText("str_STR1_space_STR2", null).ToString();
					}
				}
				else
				{
					this.PrimaryInteractionMessage = item.GetInfoTextForBeingNotInteractable(Agent.Main).ToString();
					MBTextManager.SetTextVariable("STR1", descriptionMessage, false);
					MBTextManager.SetTextVariable("STR2", this.GetWeaponSpecificText(item), false);
					this.SecondaryInteractionMessage = GameTexts.FindText("str_STR1_space_STR2", null).ToString();
				}
				this.IsActive = true;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000379C File Offset: 0x0000199C
		private void SetUsableMissionObject(UsableMissionObject usableObject, bool isInteractable)
		{
			this.FocusType = (int)usableObject.FocusableObjectType;
			this.IsFocusedOnExit = false;
			if (!string.IsNullOrEmpty(usableObject.ActionMessage.ToString()) && !string.IsNullOrEmpty(usableObject.DescriptionMessage.ToString()))
			{
				this.PrimaryInteractionMessage = (isInteractable ? usableObject.ActionMessage.ToString() : " ");
				this.SecondaryInteractionMessage = usableObject.DescriptionMessage.ToString();
				this.IsFocusedOnExit = (usableObject.FocusableObjectType == FocusableObjectType.Door || usableObject.FocusableObjectType == FocusableObjectType.Gate);
			}
			else
			{
				UsableMachine usableMachineFromPoint = this.GetUsableMachineFromPoint(usableObject);
				if (usableMachineFromPoint != null)
				{
					this.PrimaryInteractionMessage = (usableMachineFromPoint.GetDescriptionText(usableObject.GameEntity) ?? "");
					string secondaryInteractionMessage;
					if (!isInteractable)
					{
						secondaryInteractionMessage = "";
					}
					else
					{
						TextObject actionTextForStandingPoint = usableMachineFromPoint.GetActionTextForStandingPoint(usableObject);
						secondaryInteractionMessage = (((actionTextForStandingPoint != null) ? actionTextForStandingPoint.ToString() : null) ?? "");
					}
					this.SecondaryInteractionMessage = secondaryInteractionMessage;
				}
			}
			this.IsActive = true;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003884 File Offset: 0x00001A84
		private void SetUsableMachine(UsableMachine machine, bool isInteractable)
		{
			this.PrimaryInteractionMessage = (machine.GetDescriptionText(machine.GameEntity) ?? "");
			this.SecondaryInteractionMessage = " ";
			if (machine is CastleGate)
			{
				this.FocusType = 1;
			}
			if (machine.DestructionComponent != null)
			{
				this.TargetHealth = (int)(100f * machine.DestructionComponent.HitPoint / machine.DestructionComponent.MaxHitPoint);
				this.ShowHealthBar = true;
			}
			this.IsActive = true;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003900 File Offset: 0x00001B00
		private void SetDestructibleComponent(DestructableComponent machine, bool isInteractable)
		{
			string descriptionText = machine.GetDescriptionText(machine.GameEntity);
			bool flag = descriptionText != "" && descriptionText != null;
			this.PrimaryInteractionMessage = (flag ? descriptionText : "null");
			this.SecondaryInteractionMessage = " ";
			this.TargetHealth = (int)(100f * machine.HitPoint / machine.MaxHitPoint);
			this.ShowHealthBar = (machine.HitPoint < machine.MaxHitPoint);
			this.IsActive = flag;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003980 File Offset: 0x00001B80
		private void SetAgent(Agent mainAgent, Agent focusedAgent, bool isInteractable)
		{
			this.IsFocusedOnExit = false;
			bool isActive = true;
			this.FocusType = 3;
			if (focusedAgent.MissionPeer != null)
			{
				this.PrimaryInteractionMessage = focusedAgent.MissionPeer.DisplayedName;
			}
			else
			{
				this.PrimaryInteractionMessage = focusedAgent.Name.ToString();
			}
			if (isInteractable && (this._mission.Mode == MissionMode.StartUp || this._mission.Mode == MissionMode.Duel || this._mission.Mode == MissionMode.Battle || this._mission.Mode == MissionMode.Stealth) && focusedAgent.IsHuman)
			{
				MBTextManager.SetTextVariable("USE_KEY", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("CombatHotKeyCategory", 13)), false);
				if (focusedAgent.IsActive())
				{
					if (this._mission.Mode == MissionMode.Duel)
					{
						DuelMissionRepresentative duelMissionRepresentative = Agent.Main.MissionRepresentative as DuelMissionRepresentative;
						TextObject text = (duelMissionRepresentative != null && duelMissionRepresentative.CheckHasRequestFromAndRemoveRequestIfNeeded(focusedAgent.MissionPeer)) ? GameTexts.FindText("str_ui_respond", null) : GameTexts.FindText("str_ui_duel", null);
						MBTextManager.SetTextVariable("KEY", GameTexts.FindText("str_ui_agent_interaction_use", null), false);
						MBTextManager.SetTextVariable("ACTION", text, false);
						this.SecondaryInteractionMessage = GameTexts.FindText("str_key_action", null).ToString();
					}
					else if (this._mission.Mode == MissionMode.Stealth && !focusedAgent.IsEnemyOf(mainAgent))
					{
						MBTextManager.SetTextVariable("KEY", GameTexts.FindText("str_ui_agent_interaction_use", null), false);
						MBTextManager.SetTextVariable("ACTION", GameTexts.FindText("str_ui_prison_break", null), false);
						this.SecondaryInteractionMessage = GameTexts.FindText("str_key_action", null).ToString();
					}
					else if (focusedAgent.IsEnemyOf(mainAgent))
					{
						isActive = false;
					}
					else if (!Mission.Current.IsAgentInteractionAllowed())
					{
						isActive = false;
					}
					else if (this._mission.Mode != MissionMode.Battle)
					{
						MBTextManager.SetTextVariable("KEY", GameTexts.FindText("str_ui_agent_interaction_use", null), false);
						MBTextManager.SetTextVariable("ACTION", GameTexts.FindText("str_ui_talk", null), false);
						this.SecondaryInteractionMessage = GameTexts.FindText("str_key_action", null).ToString();
					}
					else
					{
						this.FocusType = -1;
					}
				}
				else if (this._mission.Mode != MissionMode.Battle)
				{
					MBTextManager.SetTextVariable("KEY", GameTexts.FindText("str_ui_agent_interaction_use", null), false);
					MBTextManager.SetTextVariable("ACTION", GameTexts.FindText("str_ui_search", null), false);
					this.SecondaryInteractionMessage = GameTexts.FindText("str_key_action", null).ToString();
				}
			}
			this.IsActive = isActive;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003BF8 File Offset: 0x00001DF8
		private void SetMount(Agent agent, Agent focusedAgent, bool isInteractable)
		{
			this.IsFocusedOnExit = false;
			if (focusedAgent.IsActive() && focusedAgent.IsMount)
			{
				string keyHyperlinkText = HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("CombatHotKeyCategory", 13));
				this.SecondaryInteractionMessage = focusedAgent.Name.ToString();
				this.FocusType = 2;
				if (focusedAgent.RiderAgent == null)
				{
					if ((float)MissionGameModels.Current.AgentStatCalculateModel.GetEffectiveSkill(agent, DefaultSkills.Riding) < focusedAgent.GetAgentDrivenPropertyValue(DrivenProperty.MountDifficulty))
					{
						this.PrimaryInteractionMessage = GameTexts.FindText("str_ui_riding_skill_not_adequate_to_mount", null).ToString();
					}
					else if ((agent.GetAgentFlags() & AgentFlag.CanRide) > AgentFlag.None)
					{
						MBTextManager.SetTextVariable("KEY", keyHyperlinkText, false);
						MBTextManager.SetTextVariable("ACTION", GameTexts.FindText("str_ui_mount", null), false);
						this.PrimaryInteractionMessage = GameTexts.FindText("str_key_action", null).ToString();
					}
					this.ShowHealthBar = false;
				}
				else if (focusedAgent.RiderAgent == agent)
				{
					MBTextManager.SetTextVariable("KEY", keyHyperlinkText, false);
					MBTextManager.SetTextVariable("ACTION", GameTexts.FindText("str_ui_dismount", null), false);
					this.PrimaryInteractionMessage = GameTexts.FindText("str_key_action", null).ToString();
				}
				this.IsActive = true;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003D27 File Offset: 0x00001F27
		private void SetHealth(float healthPercentage, bool hideHealthBarWhenFull)
		{
			this.TargetHealth = (int)(100f * healthPercentage);
			if (hideHealthBarWhenFull)
			{
				this.ShowHealthBar = (this.TargetHealth < 100);
				return;
			}
			this.ShowHealthBar = true;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003D52 File Offset: 0x00001F52
		public void ResetFocus()
		{
			this._currentFocusedObject = null;
			this.PrimaryInteractionMessage = "";
			this.FocusType = -1;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003D70 File Offset: 0x00001F70
		private UsableMachine GetUsableMachineFromPoint(UsableMissionObject standingPoint)
		{
			GameEntity gameEntity = standingPoint.GameEntity;
			while (gameEntity != null && !gameEntity.HasScriptOfType<UsableMachine>())
			{
				gameEntity = gameEntity.Parent;
			}
			UsableMachine result = null;
			if (gameEntity != null)
			{
				result = gameEntity.GetFirstScriptOfType<UsableMachine>();
			}
			return result;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003DAB File Offset: 0x00001FAB
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003DB3 File Offset: 0x00001FB3
		[DataSourceProperty]
		public bool IsFocusedOnExit
		{
			get
			{
				return this._isFocusedOnExit;
			}
			set
			{
				if (value != this._isFocusedOnExit)
				{
					this._isFocusedOnExit = value;
					base.OnPropertyChangedWithValue(value, "IsFocusedOnExit");
				}
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003DD1 File Offset: 0x00001FD1
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003DD9 File Offset: 0x00001FD9
		[DataSourceProperty]
		public int TargetHealth
		{
			get
			{
				return this._targetHealth;
			}
			set
			{
				if (value != this._targetHealth)
				{
					this._targetHealth = value;
					base.OnPropertyChangedWithValue(value, "TargetHealth");
				}
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003DF7 File Offset: 0x00001FF7
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00003DFF File Offset: 0x00001FFF
		[DataSourceProperty]
		public bool ShowHealthBar
		{
			get
			{
				return this._showHealthBar;
			}
			set
			{
				if (value != this._showHealthBar)
				{
					this._showHealthBar = value;
					base.OnPropertyChangedWithValue(value, "ShowHealthBar");
				}
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003E1D File Offset: 0x0000201D
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003E25 File Offset: 0x00002025
		[DataSourceProperty]
		public int FocusType
		{
			get
			{
				return this._focusType;
			}
			set
			{
				if (this._focusType != value)
				{
					this._focusType = value;
					base.OnPropertyChangedWithValue(value, "FocusType");
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003E43 File Offset: 0x00002043
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00003E4B File Offset: 0x0000204B
		[DataSourceProperty]
		public string PrimaryInteractionMessage
		{
			get
			{
				return this._primaryInteractionMessage;
			}
			set
			{
				if (this._primaryInteractionMessage != value)
				{
					this._primaryInteractionMessage = value;
					base.OnPropertyChangedWithValue<string>(value, "PrimaryInteractionMessage");
					if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
					{
						this.IsFocusedOnExit = false;
					}
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003E85 File Offset: 0x00002085
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00003E8D File Offset: 0x0000208D
		[DataSourceProperty]
		public string SecondaryInteractionMessage
		{
			get
			{
				return this._secondaryInteractionMessage;
			}
			set
			{
				if (this._secondaryInteractionMessage != value)
				{
					this._secondaryInteractionMessage = value;
					base.OnPropertyChangedWithValue<string>(value, "SecondaryInteractionMessage");
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003EB0 File Offset: 0x000020B0
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00003EB8 File Offset: 0x000020B8
		[DataSourceProperty]
		public string BackgroundColor
		{
			get
			{
				return this._backgroundColor;
			}
			set
			{
				if (this._backgroundColor != value)
				{
					this._backgroundColor = value;
					base.OnPropertyChangedWithValue<string>(value, "BackgroundColor");
				}
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003EDB File Offset: 0x000020DB
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003EE3 File Offset: 0x000020E3
		[DataSourceProperty]
		public string TextColor
		{
			get
			{
				return this._textColor;
			}
			set
			{
				if (this._textColor != value)
				{
					this._textColor = value;
					base.OnPropertyChangedWithValue<string>(value, "TextColor");
				}
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003F06 File Offset: 0x00002106
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00003F0E File Offset: 0x0000210E
		[DataSourceProperty]
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (value != this._isActive)
				{
					this._isActive = value;
					base.OnPropertyChangedWithValue(value, "IsActive");
					if (!value)
					{
						this.ShowHealthBar = false;
						this.PrimaryInteractionMessage = "";
						this.SecondaryInteractionMessage = "";
					}
				}
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003F4C File Offset: 0x0000214C
		private string GetWeaponSpecificText(SpawnedItemEntity spawnedItem)
		{
			MissionWeapon weaponCopy = spawnedItem.WeaponCopy;
			WeaponComponentData currentUsageItem = weaponCopy.CurrentUsageItem;
			if (currentUsageItem != null && currentUsageItem.IsShield)
			{
				MBTextManager.SetTextVariable("LEFT", (int)weaponCopy.HitPoints);
				MBTextManager.SetTextVariable("RIGHT", (int)weaponCopy.ModifiedMaxHitPoints);
				return GameTexts.FindText("str_LEFT_over_RIGHT_in_paranthesis", null).ToString();
			}
			WeaponComponentData currentUsageItem2 = weaponCopy.CurrentUsageItem;
			if (currentUsageItem2 != null && currentUsageItem2.IsAmmo && weaponCopy.ModifiedMaxAmount > 1 && !spawnedItem.IsStuckMissile())
			{
				MBTextManager.SetTextVariable("LEFT", (int)weaponCopy.Amount);
				MBTextManager.SetTextVariable("RIGHT", (int)weaponCopy.ModifiedMaxAmount);
				return GameTexts.FindText("str_LEFT_over_RIGHT_in_paranthesis", null).ToString();
			}
			return "";
		}

		// Token: 0x04000032 RID: 50
		private readonly Mission _mission;

		// Token: 0x04000033 RID: 51
		private bool _currentObjectInteractable;

		// Token: 0x04000034 RID: 52
		private IFocusable _currentFocusedObject;

		// Token: 0x04000035 RID: 53
		private bool _isActive;

		// Token: 0x04000036 RID: 54
		private string _secondaryInteractionMessage;

		// Token: 0x04000037 RID: 55
		private string _primaryInteractionMessage;

		// Token: 0x04000038 RID: 56
		private int _focusType;

		// Token: 0x04000039 RID: 57
		private int _targetHealth;

		// Token: 0x0400003A RID: 58
		private bool _showHealthBar;

		// Token: 0x0400003B RID: 59
		private bool _isFocusedOnExit;

		// Token: 0x0400003C RID: 60
		private string _backgroundColor;

		// Token: 0x0400003D RID: 61
		private string _textColor;
	}
}

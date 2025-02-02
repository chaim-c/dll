using System;
using System.ComponentModel;
using NetworkMessages.FromClient;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.MountAndBlade.ViewModelCollection;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission
{
	// Token: 0x02000027 RID: 39
	[OverrideView(typeof(MissionMainAgentCheerBarkControllerView))]
	public class MissionGauntletMainAgentCheerControllerView : MissionView
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00009B2D File Offset: 0x00007D2D
		private bool IsDisplayingADialog
		{
			get
			{
				IMissionScreen missionScreenAsInterface = this._missionScreenAsInterface;
				return (missionScreenAsInterface != null && missionScreenAsInterface.GetDisplayDialog()) || base.MissionScreen.IsRadialMenuActive || base.Mission.IsOrderMenuOpen;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00009B5D File Offset: 0x00007D5D
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00009B65 File Offset: 0x00007D65
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

		// Token: 0x06000193 RID: 403 RVA: 0x00009B7F File Offset: 0x00007D7F
		public MissionGauntletMainAgentCheerControllerView()
		{
			this._missionScreenAsInterface = base.MissionScreen;
			this.HoldHandled = false;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00009BAC File Offset: 0x00007DAC
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			this._gauntletLayer = new GauntletLayer(2, "GauntletLayer", false);
			this._missionMainAgentController = base.Mission.GetMissionBehavior<MissionMainAgentController>();
			this._dataSource = new MissionMainAgentCheerBarkControllerVM(new Action<int>(this.OnCheerSelect), new Action<int>(this.OnBarkSelect));
			this._gauntletLayer.LoadMovie("MainAgentCheerBarkController", this._dataSource);
			GameKeyContext category = HotKeyManager.GetCategory("CombatHotKeyCategory");
			if (this._missionMainAgentController != null)
			{
				InputContext inputContext = this._missionMainAgentController.Input as InputContext;
				if (inputContext != null && !inputContext.IsCategoryRegistered(category))
				{
					inputContext.RegisterHotKeyCategory(category);
				}
			}
			base.MissionScreen.AddLayer(this._gauntletLayer);
			base.Mission.OnMainAgentChanged += this.OnMainAgentChanged;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00009C7C File Offset: 0x00007E7C
		public override void OnMissionScreenFinalize()
		{
			base.OnMissionScreenFinalize();
			base.Mission.OnMainAgentChanged -= this.OnMainAgentChanged;
			base.MissionScreen.RemoveLayer(this._gauntletLayer);
			this._gauntletLayer = null;
			this._dataSource.OnFinalize();
			this._dataSource = null;
			this._missionMainAgentController = null;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00009CD7 File Offset: 0x00007ED7
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			if (this.IsMainAgentAvailable() && base.Mission.Mode != MissionMode.Deployment && (!base.MissionScreen.IsRadialMenuActive || this._dataSource.IsActive))
			{
				this.TickControls(dt);
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00009D17 File Offset: 0x00007F17
		private void OnMainAgentChanged(object sender, PropertyChangedEventArgs e)
		{
			if (base.Mission.MainAgent == null)
			{
				if (this.HoldHandled)
				{
					this.HoldHandled = false;
				}
				this._holdTime = 0f;
				this._dataSource.OnCancelHoldController();
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00009D4C File Offset: 0x00007F4C
		private void HandleNodeSelectionInput(CheerBarkNodeItemVM node, int nodeIndex, int parentNodeIndex = -1)
		{
			if (this._missionMainAgentController == null)
			{
				return;
			}
			IInputContext input = this._missionMainAgentController.Input;
			if (node.ShortcutKey != null)
			{
				if (input.IsHotKeyPressed(node.ShortcutKey.HotKey.Id))
				{
					if (parentNodeIndex != -1)
					{
						this._dataSource.SelectItem(parentNodeIndex, nodeIndex);
						return;
					}
					this._dataSource.SelectItem(nodeIndex, -1);
					this._isSelectingFromInput = node.HasSubNodes;
					return;
				}
				else if (input.IsHotKeyReleased(node.ShortcutKey.HotKey.Id))
				{
					if (!this._isSelectingFromInput)
					{
						this.HandleClosingHoldCheer();
						this._dataSource.Nodes.ApplyActionOnAllItems(delegate(CheerBarkNodeItemVM n)
						{
							n.ClearSelectionRecursive();
						});
					}
					this._isSelectingFromInput = false;
				}
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00009E1C File Offset: 0x0000801C
		private void TickControls(float dt)
		{
			if (this._missionMainAgentController == null)
			{
				return;
			}
			IInputContext input = this._missionMainAgentController.Input;
			if (GameNetwork.IsMultiplayer && this._cooldownTimeRemaining > 0f)
			{
				this._cooldownTimeRemaining -= dt;
				if (input.IsGameKeyDown(31))
				{
					if (!this._prevCheerKeyDown && (double)this._cooldownTimeRemaining >= 0.1)
					{
						this._cooldownInfoText.SetTextVariable("SECONDS", this._cooldownTimeRemaining.ToString("0.0"));
						InformationManager.DisplayMessage(new InformationMessage(this._cooldownInfoText.ToString()));
					}
					this._prevCheerKeyDown = true;
					return;
				}
				this._prevCheerKeyDown = false;
				return;
			}
			else
			{
				if (this.HoldHandled && this._dataSource.IsActive)
				{
					int num = -1;
					for (int i = 0; i < this._dataSource.Nodes.Count; i++)
					{
						if (this._dataSource.Nodes[i].IsSelected)
						{
							num = i;
							break;
						}
					}
					if (this._dataSource.IsNodesCategories)
					{
						if (num != -1)
						{
							for (int j = 0; j < this._dataSource.Nodes[num].SubNodes.Count; j++)
							{
								this.HandleNodeSelectionInput(this._dataSource.Nodes[num].SubNodes[j], j, num);
							}
						}
						else if (input.IsHotKeyReleased("CheerBarkSelectFirstCategory"))
						{
							this._dataSource.SelectItem(0, -1);
						}
						else if (input.IsHotKeyReleased("CheerBarkSelectSecondCategory"))
						{
							this._dataSource.SelectItem(1, -1);
						}
					}
					else
					{
						for (int k = 0; k < this._dataSource.Nodes.Count; k++)
						{
							this.HandleNodeSelectionInput(this._dataSource.Nodes[k], k, -1);
						}
					}
				}
				if (input.IsGameKeyDown(31) && !this.IsDisplayingADialog && !base.MissionScreen.IsRadialMenuActive)
				{
					if (this._holdTime > 0f && !this.HoldHandled)
					{
						this.HandleOpenHold();
						this.HoldHandled = true;
					}
					this._holdTime += dt;
					this._prevCheerKeyDown = true;
					return;
				}
				if (this._prevCheerKeyDown && !input.IsGameKeyDown(31))
				{
					if (this._holdTime < 0f)
					{
						this.HandleQuickReleaseCheer();
					}
					else
					{
						this.HandleClosingHoldCheer();
					}
					this.HoldHandled = false;
					this._holdTime = 0f;
					this._prevCheerKeyDown = false;
				}
				return;
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000A092 File Offset: 0x00008292
		private void HandleOpenHold()
		{
			MissionMainAgentCheerBarkControllerVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.OnSelectControllerToggle(true);
			}
			base.MissionScreen.SetRadialMenuActiveState(true);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000A0B2 File Offset: 0x000082B2
		private void HandleClosingHoldCheer()
		{
			MissionMainAgentCheerBarkControllerVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.OnSelectControllerToggle(false);
			}
			base.MissionScreen.SetRadialMenuActiveState(false);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000A0D2 File Offset: 0x000082D2
		private void HandleQuickReleaseCheer()
		{
			this.OnCheerSelect(-1);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000A0DC File Offset: 0x000082DC
		private void OnCheerSelect(int tauntIndex)
		{
			if (tauntIndex < 0)
			{
				return;
			}
			if (GameNetwork.IsClient)
			{
				TauntUsageManager.TauntUsage.TauntUsageFlag actionNotUsableReason = CosmeticsManagerHelper.GetActionNotUsableReason(Agent.Main, tauntIndex);
				if (actionNotUsableReason != TauntUsageManager.TauntUsage.TauntUsageFlag.None)
				{
					InformationManager.DisplayMessage(new InformationMessage(TauntUsageManager.GetActionDisabledReasonText(actionNotUsableReason)));
					return;
				}
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new TauntSelected(tauntIndex));
				GameNetwork.EndModuleEventAsClient();
			}
			else
			{
				Agent main = Agent.Main;
				if (main != null)
				{
					main.HandleTaunt(tauntIndex, true);
				}
			}
			this._cooldownTimeRemaining = 4f;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000A149 File Offset: 0x00008349
		private void OnBarkSelect(int indexOfBark)
		{
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new BarkSelected(indexOfBark));
				GameNetwork.EndModuleEventAsClient();
			}
			else
			{
				Agent main = Agent.Main;
				if (main != null)
				{
					main.HandleBark(indexOfBark);
				}
			}
			this._cooldownTimeRemaining = 2f;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000A185 File Offset: 0x00008385
		private bool IsMainAgentAvailable()
		{
			Agent main = Agent.Main;
			return main != null && main.IsActive();
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000A197 File Offset: 0x00008397
		public override void OnPhotoModeActivated()
		{
			base.OnPhotoModeActivated();
			this._gauntletLayer.UIContext.ContextAlpha = 0f;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000A1B4 File Offset: 0x000083B4
		public override void OnPhotoModeDeactivated()
		{
			base.OnPhotoModeDeactivated();
			this._gauntletLayer.UIContext.ContextAlpha = 1f;
		}

		// Token: 0x040000DD RID: 221
		private const float CooldownPeriodDurationAfterCheer = 4f;

		// Token: 0x040000DE RID: 222
		private const float CooldownPeriodDurationAfterBark = 2f;

		// Token: 0x040000DF RID: 223
		private const float _minHoldTime = 0f;

		// Token: 0x040000E0 RID: 224
		private readonly IMissionScreen _missionScreenAsInterface;

		// Token: 0x040000E1 RID: 225
		private MissionMainAgentController _missionMainAgentController;

		// Token: 0x040000E2 RID: 226
		private readonly TextObject _cooldownInfoText = new TextObject("{=aogZyZlR}You need to wait {SECONDS} seconds until you can cheer/shout again.", null);

		// Token: 0x040000E3 RID: 227
		private bool _holdHandled;

		// Token: 0x040000E4 RID: 228
		private float _holdTime;

		// Token: 0x040000E5 RID: 229
		private bool _prevCheerKeyDown;

		// Token: 0x040000E6 RID: 230
		private GauntletLayer _gauntletLayer;

		// Token: 0x040000E7 RID: 231
		private MissionMainAgentCheerBarkControllerVM _dataSource;

		// Token: 0x040000E8 RID: 232
		private float _cooldownTimeRemaining;

		// Token: 0x040000E9 RID: 233
		private bool _isSelectingFromInput;
	}
}

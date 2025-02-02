using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Options;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Order;
using TaleWorlds.MountAndBlade.ViewModelCollection;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.View.Screens
{
	// Token: 0x02000032 RID: 50
	[GameStateScreen(typeof(MissionState))]
	public class MissionScreen : ScreenBase, IMissionSystemHandler, IGameStateListener, IMissionScreen, IMissionListener
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000FDBB File Offset: 0x0000DFBB
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0000FDC3 File Offset: 0x0000DFC3
		public bool LockCameraMovement { get; private set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060001DE RID: 478 RVA: 0x0000FDCC File Offset: 0x0000DFCC
		// (remove) Token: 0x060001DF RID: 479 RVA: 0x0000FE04 File Offset: 0x0000E004
		public event MissionScreen.OnSpectateAgentDelegate OnSpectateAgentFocusIn;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060001E0 RID: 480 RVA: 0x0000FE3C File Offset: 0x0000E03C
		// (remove) Token: 0x060001E1 RID: 481 RVA: 0x0000FE74 File Offset: 0x0000E074
		public event MissionScreen.OnSpectateAgentDelegate OnSpectateAgentFocusOut;

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000FEA9 File Offset: 0x0000E0A9
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000FEB1 File Offset: 0x0000E0B1
		public OrderFlag OrderFlag { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000FEBA File Offset: 0x0000E0BA
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x0000FEC2 File Offset: 0x0000E0C2
		public Camera CombatCamera { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000FECB File Offset: 0x0000E0CB
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000FED3 File Offset: 0x0000E0D3
		public Camera CustomCamera
		{
			get
			{
				return this._customCamera;
			}
			set
			{
				this._customCamera = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000FEDC File Offset: 0x0000E0DC
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000FEE4 File Offset: 0x0000E0E4
		public float CameraBearing { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000FEED File Offset: 0x0000E0ED
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000FEF5 File Offset: 0x0000E0F5
		public float MaxCameraZoom { get; private set; } = 1f;

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000FEFE File Offset: 0x0000E0FE
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000FF06 File Offset: 0x0000E106
		public float CameraElevation { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000FF0F File Offset: 0x0000E10F
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000FF17 File Offset: 0x0000E117
		public float CameraResultDistanceToTarget { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000FF20 File Offset: 0x0000E120
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000FF28 File Offset: 0x0000E128
		public float CameraViewAngle { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000FF31 File Offset: 0x0000E131
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x0000FF39 File Offset: 0x0000E139
		public bool IsPhotoModeEnabled { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000FF42 File Offset: 0x0000E142
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000FF4A File Offset: 0x0000E14A
		public bool IsConversationActive { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000FF53 File Offset: 0x0000E153
		public bool IsDeploymentActive
		{
			get
			{
				return this.Mission.Mode == MissionMode.Deployment;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000FF63 File Offset: 0x0000E163
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000FF6B File Offset: 0x0000E16B
		public SceneLayer SceneLayer { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000FF74 File Offset: 0x0000E174
		public SceneView SceneView
		{
			get
			{
				SceneLayer sceneLayer = this.SceneLayer;
				if (sceneLayer == null)
				{
					return null;
				}
				return sceneLayer.SceneView;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000FF87 File Offset: 0x0000E187
		// (set) Token: 0x060001FB RID: 507 RVA: 0x0000FF8F File Offset: 0x0000E18F
		public Mission Mission { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000FF98 File Offset: 0x0000E198
		// (set) Token: 0x060001FD RID: 509 RVA: 0x0000FFA0 File Offset: 0x0000E1A0
		public bool IsCheatGhostMode { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000FFA9 File Offset: 0x0000E1A9
		// (set) Token: 0x060001FF RID: 511 RVA: 0x0000FFB1 File Offset: 0x0000E1B1
		public bool IsRadialMenuActive { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000FFBA File Offset: 0x0000E1BA
		public IInputContext InputManager
		{
			get
			{
				return this.Mission.InputManager;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000FFC7 File Offset: 0x0000E1C7
		private bool IsOrderMenuOpen
		{
			get
			{
				return this.Mission.IsOrderMenuOpen;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000FFD4 File Offset: 0x0000E1D4
		private bool IsTransferMenuOpen
		{
			get
			{
				return this.Mission.IsTransferMenuOpen;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000FFE1 File Offset: 0x0000E1E1
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000FFEC File Offset: 0x0000E1EC
		public Agent LastFollowedAgent
		{
			get
			{
				return this._lastFollowedAgent;
			}
			private set
			{
				if (this._lastFollowedAgent != value)
				{
					Agent lastFollowedAgent = this._lastFollowedAgent;
					this._lastFollowedAgent = value;
					NetworkCommunicator myPeer = GameNetwork.MyPeer;
					MissionPeer missionPeer = (myPeer != null) ? myPeer.GetComponent<MissionPeer>() : null;
					if (GameNetwork.IsMyPeerReady)
					{
						if (missionPeer != null)
						{
							missionPeer.FollowedAgent = this._lastFollowedAgent;
						}
						else
						{
							Debug.FailedAssert("MyPeer.IsSynchronized but myMissionPeer == null", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.View\\Screens\\MissionScreen.cs", "LastFollowedAgent", 218);
						}
					}
					if (lastFollowedAgent != null)
					{
						MissionScreen.OnSpectateAgentDelegate onSpectateAgentFocusOut = this.OnSpectateAgentFocusOut;
						if (onSpectateAgentFocusOut != null)
						{
							onSpectateAgentFocusOut(lastFollowedAgent);
						}
					}
					if (this._lastFollowedAgent != null)
					{
						if (this._lastFollowedAgent == this.Mission.MainAgent)
						{
							Agent mainAgent = this.Mission.MainAgent;
							mainAgent.OnMainAgentWieldedItemChange = (Agent.OnMainAgentWieldedItemChangeDelegate)Delegate.Combine(mainAgent.OnMainAgentWieldedItemChange, new Agent.OnMainAgentWieldedItemChangeDelegate(this.OnMainAgentWeaponChanged));
							this.ResetMaxCameraZoom();
						}
						MissionScreen.OnSpectateAgentDelegate onSpectateAgentFocusIn = this.OnSpectateAgentFocusIn;
						if (onSpectateAgentFocusIn != null)
						{
							onSpectateAgentFocusIn(this._lastFollowedAgent);
						}
					}
					if (this._lastFollowedAgent == this._agentToFollowOverride)
					{
						this._agentToFollowOverride = null;
					}
				}
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000100E4 File Offset: 0x0000E2E4
		// (set) Token: 0x06000206 RID: 518 RVA: 0x000100EC File Offset: 0x0000E2EC
		public IAgentVisual LastFollowedAgentVisuals { get; set; }

		// Token: 0x06000207 RID: 519 RVA: 0x000100F5 File Offset: 0x0000E2F5
		float IMissionScreen.GetCameraElevation()
		{
			return this.CameraElevation;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000100FD File Offset: 0x0000E2FD
		public void SetOrderFlagVisibility(bool value)
		{
			if (this.OrderFlag != null)
			{
				this.OrderFlag.IsVisible = value;
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00010113 File Offset: 0x0000E313
		public string GetFollowText()
		{
			if (this.LastFollowedAgent == null)
			{
				return "";
			}
			return this.LastFollowedAgent.Name.ToString();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00010134 File Offset: 0x0000E334
		public string GetFollowPartyText()
		{
			if (this.LastFollowedAgent != null)
			{
				TextObject textObject = new TextObject("{=xsC8Ierj}({BATTLE_COMBATANT})", null);
				textObject.SetTextVariable("BATTLE_COMBATANT", this.LastFollowedAgent.Origin.BattleCombatant.Name);
				return textObject.ToString();
			}
			return "";
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00010180 File Offset: 0x0000E380
		public bool SetDisplayDialog(bool value)
		{
			bool result = this._displayingDialog != value;
			this._displayingDialog = value;
			return result;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00010195 File Offset: 0x0000E395
		bool IMissionScreen.GetDisplayDialog()
		{
			return this._displayingDialog;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0001019D File Offset: 0x0000E39D
		public override bool MouseVisible
		{
			get
			{
				return ScreenManager.GetMouseVisibility();
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600020E RID: 526 RVA: 0x000101A4 File Offset: 0x0000E3A4
		public bool IsMissionTickable
		{
			get
			{
				return base.IsActive && this.Mission != null && (this.Mission.CurrentState == Mission.State.Continuing || this.Mission.MissionEnded);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000101D3 File Offset: 0x0000E3D3
		// (set) Token: 0x06000210 RID: 528 RVA: 0x000101DB File Offset: 0x0000E3DB
		public bool PhotoModeRequiresMouse { get; private set; }

		// Token: 0x06000211 RID: 529 RVA: 0x000101E4 File Offset: 0x0000E3E4
		public MissionScreen(MissionState missionState)
		{
			missionState.Handler = this;
			this._emptyUILayer = new SceneLayer("SceneLayer", true, true);
			((SceneLayer)this._emptyUILayer).SceneView.SetEnable(false);
			this._missionState = missionState;
			this.Mission = missionState.CurrentMission;
			this.CombatCamera = Camera.CreateCamera();
			this._missionViews = new List<MissionView>();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000102AC File Offset: 0x0000E4AC
		protected override void OnInitialize()
		{
			MBDebug.Print("-------MissionScreen-OnInitialize", 0, Debug.DebugColor.White, 17592186044416UL);
			base.OnInitialize();
			Module.CurrentModule.SkinsXMLHasChanged += this.OnSkinsXMLChanged;
			this.CameraViewAngle = 65f;
			this._cameraTarget = new Vec3(0f, 0f, 10f, -1f);
			this.CameraBearing = 0f;
			this.CameraElevation = -0.2f;
			this._cameraBearingDelta = 0f;
			this._cameraElevationDelta = 0f;
			this._cameraSpecialTargetAddedBearing = 0f;
			this._cameraSpecialCurrentAddedBearing = 0f;
			this._cameraSpecialTargetAddedElevation = 0f;
			this._cameraSpecialCurrentAddedElevation = 0f;
			this._cameraSpecialTargetPositionToAdd = Vec3.Zero;
			this._cameraSpecialCurrentPositionToAdd = Vec3.Zero;
			this._cameraSpecialTargetDistanceToAdd = 0f;
			this._cameraSpecialCurrentDistanceToAdd = 0f;
			this._cameraSpecialCurrentFOV = 65f;
			this._cameraSpecialTargetFOV = 65f;
			this._cameraAddedElevation = 0f;
			this._cameraTargetAddedHeight = 0f;
			this._cameraDeploymentHeightToAdd = 0f;
			this._lastCameraAddedDistance = 0f;
			this.CameraResultDistanceToTarget = 0f;
			this._cameraSpeed = Vec3.Zero;
			this._cameraSpeedMultiplier = 1f;
			this._cameraHeightLimit = 0f;
			this._cameraAddSpecialMovement = false;
			this._cameraAddSpecialPositionalMovement = false;
			this._cameraApplySpecialMovementsInstantly = false;
			this._currentViewBlockingBodyCoeff = 1f;
			this._targetViewBlockingBodyCoeff = 1f;
			this._cameraSmoothMode = false;
			this.CustomCamera = null;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00010440 File Offset: 0x0000E640
		private void InitializeMissionView()
		{
			this._missionState.Paused = false;
			this.SceneLayer = new SceneLayer("SceneLayer", true, true);
			this.SceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("Generic"));
			this.SceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this.SceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
			this.SceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("ScoreboardHotKeyCategory"));
			this.SceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("CombatHotKeyCategory"));
			this.SceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("Cheats"));
			this.Mission.InputManager = this.SceneLayer.Input;
			base.AddLayer(this.SceneLayer);
			this.SceneView.SetScene(this.Mission.Scene);
			this.SceneView.SetSceneUsesShadows(true);
			this.SceneView.SetAcceptGlobalDebugRenderObjects(true);
			this.SceneView.SetResolutionScaling(true);
			this._missionMainAgentController = this.Mission.GetMissionBehavior<MissionMainAgentController>();
			this._missionLobbyComponent = Mission.Current.GetMissionBehavior<MissionLobbyComponent>();
			this._missionCameraModeLogic = (this.Mission.MissionBehaviors.FirstOrDefault((MissionBehavior b) => b is ICameraModeLogic) as ICameraModeLogic);
			using (List<MissionBehavior>.Enumerator enumerator = this.Mission.MissionBehaviors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MissionView missionView;
					if ((missionView = (enumerator.Current as MissionView)) != null)
					{
						missionView.OnMissionScreenInitialize();
					}
				}
			}
			this.Mission.AgentVisualCreator = new AgentVisualsCreator();
			this._mpGameModeBase = this.Mission.GetMissionBehavior<MissionMultiplayerGameModeBaseClient>();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00010634 File Offset: 0x0000E834
		protected override void OnActivate()
		{
			base.OnActivate();
			this.ActivateLoadingScreen();
			if (this.Mission != null && this.Mission.MissionEnded)
			{
				MissionScreen missionScreen = ScreenManager.TopScreen as MissionScreen;
				if (missionScreen != null)
				{
					ScreenManager.TopScreen.DeactivateAllLayers();
					missionScreen.SceneView.SetEnable(false);
				}
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00010688 File Offset: 0x0000E888
		protected override void OnResume()
		{
			base.OnResume();
			if (this.Mission != null && this.Mission.MissionEnded)
			{
				MissionScreen missionScreen = ScreenManager.TopScreen as MissionScreen;
				if (missionScreen != null)
				{
					ScreenManager.TopScreen.DeactivateAllLayers();
					missionScreen.SceneView.SetEnable(false);
				}
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000106D4 File Offset: 0x0000E8D4
		public override void OnFocusChangeOnGameWindow(bool focusGained)
		{
			base.OnFocusChangeOnGameWindow(focusGained);
			if (!LoadingWindow.IsLoadingWindowActive)
			{
				Func<bool> isAnyInquiryActive = InformationManager.IsAnyInquiryActive;
				if (isAnyInquiryActive != null && !isAnyInquiryActive())
				{
					Mission mission = this.Mission;
					List<MissionBehavior> list;
					if (mission == null)
					{
						list = null;
					}
					else
					{
						List<MissionBehavior> missionBehaviors = mission.MissionBehaviors;
						if (missionBehaviors == null)
						{
							list = null;
						}
						else
						{
							list = (from v in missionBehaviors
							where v is MissionView
							orderby ((MissionView)v).ViewOrderPriority
							select v).ToList<MissionBehavior>();
						}
					}
					List<MissionBehavior> list2 = list;
					if (list2 != null)
					{
						for (int i = 0; i < list2.Count; i++)
						{
							(list2[i] as MissionView).OnFocusChangeOnGameWindow(focusGained);
						}
					}
				}
			}
			this.IsFocusLost = !focusGained;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000217 RID: 535 RVA: 0x000107A2 File Offset: 0x0000E9A2
		// (set) Token: 0x06000218 RID: 536 RVA: 0x000107AA File Offset: 0x0000E9AA
		public bool IsFocusLost { get; private set; }

		// Token: 0x06000219 RID: 537 RVA: 0x000107B4 File Offset: 0x0000E9B4
		public bool IsOpeningEscapeMenuOnFocusChangeAllowed()
		{
			Mission mission = this.Mission;
			List<MissionBehavior> list;
			if (mission == null)
			{
				list = null;
			}
			else
			{
				List<MissionBehavior> missionBehaviors = mission.MissionBehaviors;
				if (missionBehaviors == null)
				{
					list = null;
				}
				else
				{
					list = (from v in missionBehaviors
					where v is MissionView
					orderby ((MissionView)v).ViewOrderPriority
					select v).ToList<MissionBehavior>();
				}
			}
			List<MissionBehavior> list2 = list;
			if (list2 != null)
			{
				using (List<MissionBehavior>.Enumerator enumerator = list2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!(enumerator.Current as MissionView).IsOpeningEscapeMenuOnFocusChangeAllowed())
						{
							return false;
						}
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00010878 File Offset: 0x0000EA78
		public void SetExtraCameraParameters(bool newForceCanZoom, float newCameraRayCastStartingPointOffset)
		{
			this._forceCanZoom = newForceCanZoom;
			this._cameraRayCastOffset = newCameraRayCastStartingPointOffset;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00010888 File Offset: 0x0000EA88
		public void SetCustomAgentListToSpectateGatherer(MissionScreen.GatherCustomAgentListToSpectateDelegate gatherer)
		{
			this._gatherCustomAgentListToSpectate = gatherer;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00010894 File Offset: 0x0000EA94
		public void UpdateFreeCamera(MatrixFrame frame)
		{
			this.CombatCamera.Frame = frame;
			Vec3 v = -frame.rotation.u;
			this.CameraBearing = v.RotationZ;
			Vec3 v2 = new Vec3(0f, 0f, 1f, -1f);
			this.CameraElevation = MathF.Acos(Vec3.DotProduct(v2, v)) - 1.5707964f;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00010900 File Offset: 0x0000EB00
		protected override void OnFrameTick(float dt)
		{
			if (this.SceneLayer != null)
			{
				bool flag = MBDebug.IsErrorReportModeActive();
				if (flag)
				{
					this._missionState.Paused = MBDebug.IsErrorReportModePauseMission();
				}
				if (base.DebugInput.IsHotKeyPressed("MissionScreenHotkeyFixCamera"))
				{
					this._fixCamera = !this._fixCamera;
				}
				flag = (flag || this._fixCamera);
				if (this.IsPhotoModeEnabled)
				{
					flag = (flag || this.PhotoModeRequiresMouse);
				}
				this.SceneLayer.InputRestrictions.SetMouseVisibility(flag);
			}
			if (this.Mission != null)
			{
				if (this.IsMissionTickable)
				{
					foreach (MissionView missionView in this._missionViews)
					{
						missionView.OnMissionScreenTick(dt);
					}
				}
				this.HandleInputs();
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000109DC File Offset: 0x0000EBDC
		private void ActivateMissionView()
		{
			MBDebug.Print("-------MissionScreen-OnActivate", 0, Debug.DebugColor.White, 17592186044416UL);
			this.Mission.OnMainAgentChanged += this.Mission_OnMainAgentChanged;
			this.Mission.OnBeforeAgentRemoved += this.Mission_OnBeforeAgentRemoved;
			this._cameraBearingDelta = 0f;
			this._cameraElevationDelta = 0f;
			this.SetCameraFrameToMapView();
			this.CheckForUpdateCamera(1E-05f);
			this.Mission.ResetFirstThirdPersonView();
			if (MBEditor.EditModeEnabled && MBEditor.IsEditModeOn)
			{
				MBEditor.EnterEditMissionMode(this.Mission);
			}
			foreach (MissionView missionView in this._missionViews)
			{
				missionView.OnMissionScreenActivate();
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00010ABC File Offset: 0x0000ECBC
		private void Mission_OnMainAgentChanged(object sender, PropertyChangedEventArgs e)
		{
			if (this.Mission.MainAgent != null)
			{
				this._isPlayerAgentAdded = true;
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00010AD2 File Offset: 0x0000ECD2
		private void Mission_OnBeforeAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			if (affectedAgent == this._agentToFollowOverride)
			{
				this._agentToFollowOverride = null;
				return;
			}
			if (affectedAgent == this.Mission.MainAgent)
			{
				this._agentToFollowOverride = affectorAgent;
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00010AFA File Offset: 0x0000ECFA
		public void OnMainAgentWeaponChanged()
		{
			this.ResetMaxCameraZoom();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00010B04 File Offset: 0x0000ED04
		private void ResetMaxCameraZoom()
		{
			if (this.LastFollowedAgent == null || this.LastFollowedAgent != this.Mission.MainAgent)
			{
				this.MaxCameraZoom = 1f;
				return;
			}
			this.MaxCameraZoom = ((Mission.Current != null) ? MathF.Max(1f, Mission.Current.GetMainAgentMaxCameraZoom()) : 1f);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00010B60 File Offset: 0x0000ED60
		protected override void OnDeactivate()
		{
			base.OnDeactivate();
			MBDebug.Print("-------MissionScreen-OnDeactivate", 0, Debug.DebugColor.White, 17592186044416UL);
			if (this.Mission == null)
			{
				return;
			}
			this.Mission.OnMainAgentChanged -= this.Mission_OnMainAgentChanged;
			this.Mission.OnBeforeAgentRemoved -= this.Mission_OnBeforeAgentRemoved;
			foreach (MissionView missionView in this._missionViews)
			{
				missionView.OnMissionScreenDeactivate();
			}
			this._isRenderingStarted = false;
			this._loadingScreenFramesLeft = 15;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00010C14 File Offset: 0x0000EE14
		protected override void OnFinalize()
		{
			MBDebug.Print("-------MissionScreen-OnFinalize", 0, Debug.DebugColor.White, 17592186044416UL);
			Module.CurrentModule.SkinsXMLHasChanged -= this.OnSkinsXMLChanged;
			LoadingWindow.EnableGlobalLoadingWindow();
			if (this.Mission != null)
			{
				this.Mission.InputManager = null;
			}
			this.Mission = null;
			this.OrderFlag = null;
			this.SceneLayer = null;
			this._missionMainAgentController = null;
			this.CombatCamera = null;
			this._customCamera = null;
			this._missionState = null;
			base.OnFinalize();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00010CA0 File Offset: 0x0000EEA0
		private IEnumerable<MissionBehavior> AddDefaultMissionBehaviorsTo(Mission mission, IEnumerable<MissionBehavior> behaviors)
		{
			List<MissionBehavior> list = new List<MissionBehavior>();
			IEnumerable<MissionBehavior> collection = ViewCreatorManager.CreateDefaultMissionBehaviors(mission);
			list.AddRange(collection);
			return behaviors.Concat(list);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00010CC8 File Offset: 0x0000EEC8
		private void OnSkinsXMLChanged()
		{
			foreach (Agent agent in Mission.Current.Agents)
			{
				agent.EquipItemsFromSpawnEquipment(true);
				agent.UpdateAgentProperties();
				agent.AgentVisuals.UpdateSkeletonScale((int)agent.SpawnEquipment.BodyDeformType);
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00010D3C File Offset: 0x0000EF3C
		private void OnSceneRenderingStarted()
		{
			LoadingWindow.DisableGlobalLoadingWindow();
			Utilities.SetScreenTextRenderingState(true);
			foreach (MissionView missionView in this._missionViews)
			{
				missionView.OnSceneRenderingStarted();
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00010D98 File Offset: 0x0000EF98
		[CommandLineFunctionality.CommandLineArgumentFunction("fix_camera_toggle", "mission")]
		public static string ToggleFixedMissionCamera(List<string> strings)
		{
			MissionScreen missionScreen = ScreenManager.TopScreen as MissionScreen;
			if (missionScreen != null)
			{
				MissionScreen.SetFixedMissionCameraActive(!missionScreen._fixCamera);
			}
			return "Done";
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00010DC8 File Offset: 0x0000EFC8
		public static void SetFixedMissionCameraActive(bool active)
		{
			MissionScreen missionScreen = ScreenManager.TopScreen as MissionScreen;
			if (missionScreen != null)
			{
				missionScreen._fixCamera = active;
				missionScreen.SceneLayer.InputRestrictions.SetMouseVisibility(missionScreen._fixCamera);
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00010E00 File Offset: 0x0000F000
		[CommandLineFunctionality.CommandLineArgumentFunction("set_shift_camera_speed", "mission")]
		public static string SetShiftCameraSpeed(List<string> strings)
		{
			MissionScreen missionScreen = ScreenManager.TopScreen as MissionScreen;
			if (missionScreen == null)
			{
				return "No Mission Available";
			}
			int shiftSpeedMultiplier;
			if (strings.Count > 0 && int.TryParse(strings[0], out shiftSpeedMultiplier))
			{
				missionScreen._shiftSpeedMultiplier = shiftSpeedMultiplier;
				return "Done";
			}
			return "Current multiplier is " + missionScreen._shiftSpeedMultiplier.ToString();
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00010E5C File Offset: 0x0000F05C
		[CommandLineFunctionality.CommandLineArgumentFunction("set_camera_position", "mission")]
		public static string SetCameraPosition(List<string> strings)
		{
			if (GameNetwork.IsSessionActive)
			{
				return "Does not work on multiplayer.";
			}
			if (strings.Count < 3)
			{
				return "You need to enter 3 arguments.";
			}
			List<float> list = new List<float>();
			for (int i = 0; i < strings.Count; i++)
			{
				float item = 0f;
				if (!float.TryParse(strings[i], out item))
				{
					return "Argument " + (i + 1) + " is not valid.";
				}
				list.Add(item);
			}
			MissionScreen missionScreen = ScreenManager.TopScreen as MissionScreen;
			if (missionScreen != null)
			{
				missionScreen.IsCheatGhostMode = true;
				missionScreen.LastFollowedAgent = null;
				missionScreen.CombatCamera.Position = new Vec3(list[0], list[1], list[2], -1f);
				return string.Concat(new string[]
				{
					"Camera position has been set to: ",
					strings[0],
					", ",
					strings[1],
					", ",
					strings[2]
				});
			}
			return "Mission screen not found.";
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00010F64 File Offset: 0x0000F164
		private void CheckForUpdateCamera(float dt)
		{
			if (this._fixCamera && !this.IsPhotoModeEnabled)
			{
				return;
			}
			if (this.CustomCamera != null)
			{
				if (this._zoomAmount > 0f)
				{
					this._zoomAmount = MBMath.ClampFloat(this._zoomAmount, 0f, 1f);
					float valueTo = 37f / this.MaxCameraZoom;
					this.CameraViewAngle = MBMath.Lerp(Mission.GetFirstPersonFov(), valueTo, this._zoomAmount, 0.005f);
					this.CustomCamera.SetFovVertical(this._cameraSpecialCurrentFOV * (this.CameraViewAngle / 65f) * 0.017453292f, Screen.AspectRatio, 0.065f, 12500f);
				}
				this.CombatCamera.FillParametersFrom(this.CustomCamera);
				if (this.CustomCamera.Entity != null)
				{
					MatrixFrame globalFrame = this.CustomCamera.Entity.GetGlobalFrame();
					globalFrame.rotation.MakeUnit();
					this.CombatCamera.Frame = globalFrame;
				}
				this.SceneView.SetCamera(this.CombatCamera);
				SoundManager.SetListenerFrame(this.CombatCamera.Frame);
				return;
			}
			bool flag = false;
			using (List<MissionBehavior>.Enumerator enumerator = this.Mission.MissionBehaviors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MissionView missionView;
					if ((missionView = (enumerator.Current as MissionView)) != null)
					{
						flag = (flag || missionView.UpdateOverridenCamera(dt));
					}
				}
			}
			if (!flag)
			{
				this.UpdateCamera(dt);
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000110F0 File Offset: 0x0000F2F0
		private void UpdateDragData()
		{
			if (this._resetDraggingMode)
			{
				this._rightButtonDraggingMode = false;
				this._resetDraggingMode = false;
				return;
			}
			if (this.SceneLayer.Input.IsKeyReleased(InputKey.RightMouseButton))
			{
				this._resetDraggingMode = true;
				return;
			}
			if (this.SceneLayer.Input.IsKeyPressed(InputKey.RightMouseButton))
			{
				this._clickedPositionPixel = this.SceneLayer.Input.GetMousePositionPixel();
				return;
			}
			if (this.SceneLayer.Input.IsKeyDown(InputKey.RightMouseButton) && !this.SceneLayer.Input.IsKeyReleased(InputKey.RightMouseButton) && this.SceneLayer.Input.GetMousePositionPixel().DistanceSquared(this._clickedPositionPixel) > 10f && !this._rightButtonDraggingMode)
			{
				this._rightButtonDraggingMode = true;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000111C4 File Offset: 0x0000F3C4
		private void UpdateCamera(float dt)
		{
			Scene scene = this.Mission.Scene;
			bool photoModeOrbit = scene.GetPhotoModeOrbit();
			float num = this.IsPhotoModeEnabled ? scene.GetPhotoModeFov() : 0f;
			bool flag = this._isGamepadActive && this.PhotoModeRequiresMouse;
			this.UpdateDragData();
			MatrixFrame matrixFrame = MatrixFrame.Identity;
			MissionPeer missionPeer = (GameNetwork.MyPeer != null) ? GameNetwork.MyPeer.GetComponent<MissionPeer>() : null;
			Mission.SpectatorData spectatingData = this.GetSpectatingData(this.CombatCamera.Frame.origin);
			Agent agentToFollow = spectatingData.AgentToFollow;
			IAgentVisual agentVisualToFollow = spectatingData.AgentVisualToFollow;
			SpectatorCameraTypes cameraType = spectatingData.CameraType;
			bool flag2 = this.Mission.CameraIsFirstPerson && agentToFollow != null && agentToFollow == this.Mission.MainAgent;
			float num2 = flag2 ? Mission.GetFirstPersonFov() : 65f;
			if (this.IsPhotoModeEnabled)
			{
				this.CameraViewAngle = num2;
			}
			else
			{
				this._zoomAmount = MBMath.ClampFloat(this._zoomAmount, 0f, 1f);
				float valueTo = 37f / this.MaxCameraZoom;
				this.CameraViewAngle = MBMath.Lerp(num2, valueTo, this._zoomAmount, 0.005f);
			}
			if (this._missionMainAgentController == null)
			{
				this._missionMainAgentController = this.Mission.GetMissionBehavior<MissionMainAgentController>();
			}
			else
			{
				this._missionMainAgentController.IsDisabled = true;
			}
			if (this._missionMainAgentController != null && this.Mission.Mode != MissionMode.Deployment && this.Mission.MainAgent != null && this.Mission.MainAgent.IsCameraAttachable())
			{
				this._missionMainAgentController.IsDisabled = false;
			}
			bool flag3 = this._cameraApplySpecialMovementsInstantly;
			if ((this.IsPhotoModeEnabled && !photoModeOrbit) || (agentToFollow == null && agentVisualToFollow == null))
			{
				float a = -scene.GetPhotoModeRoll();
				matrixFrame.rotation.RotateAboutSide(1.5707964f);
				matrixFrame.rotation.RotateAboutForward(this.CameraBearing);
				matrixFrame.rotation.RotateAboutSide(this.CameraElevation);
				matrixFrame.rotation.RotateAboutUp(a);
				matrixFrame.origin = this.CombatCamera.Frame.origin;
				this._cameraSpeed *= 1f - 5f * dt;
				this._cameraSpeed.x = MBMath.ClampFloat(this._cameraSpeed.x, -20f, 20f);
				this._cameraSpeed.y = MBMath.ClampFloat(this._cameraSpeed.y, -20f, 20f);
				this._cameraSpeed.z = MBMath.ClampFloat(this._cameraSpeed.z, -20f, 20f);
				if (Game.Current.CheatMode)
				{
					if (this.InputManager.IsHotKeyPressed("MissionScreenHotkeyIncreaseCameraSpeed"))
					{
						this._cameraSpeedMultiplier *= 1.5f;
					}
					if (this.InputManager.IsHotKeyPressed("MissionScreenHotkeyDecreaseCameraSpeed"))
					{
						this._cameraSpeedMultiplier *= 0.6666667f;
					}
					if (this.InputManager.IsHotKeyPressed("ResetCameraSpeed"))
					{
						this._cameraSpeedMultiplier = 1f;
					}
					if (this.InputManager.IsControlDown())
					{
						float num3 = this.SceneLayer.Input.GetDeltaMouseScroll() * 0.008333334f;
						if (num3 > 0.01f)
						{
							this._cameraSpeedMultiplier *= 1.25f;
						}
						else if (num3 < -0.01f)
						{
							this._cameraSpeedMultiplier *= 0.8f;
						}
					}
				}
				float num4 = 10f * this._cameraSpeedMultiplier * (this.IsPhotoModeEnabled ? (flag ? 0f : 0.3f) : 1f);
				if (this.Mission.Mode == MissionMode.Deployment)
				{
					float groundHeightAtPosition = scene.GetGroundHeightAtPosition(matrixFrame.origin, BodyFlags.CommonCollisionExcludeFlags);
					num4 *= MathF.Max(1f, 1f + (matrixFrame.origin.z - groundHeightAtPosition - 5f) / 10f);
				}
				if ((!this.IsPhotoModeEnabled && this.SceneLayer.Input.IsGameKeyDown(24)) || (this.IsPhotoModeEnabled && !flag && this.SceneLayer.Input.IsHotKeyDown("FasterCamera")))
				{
					num4 *= (float)this._shiftSpeedMultiplier;
				}
				if (!this._cameraSmoothMode)
				{
					this._cameraSpeed.x = 0f;
					this._cameraSpeed.y = 0f;
					this._cameraSpeed.z = 0f;
				}
				if ((!this.InputManager.IsControlDown() || !this.InputManager.IsAltDown()) && !this.LockCameraMovement)
				{
					bool flag4 = !this._isGamepadActive || this.Mission.Mode != MissionMode.Deployment || Input.IsKeyDown(InputKey.ControllerLTrigger);
					Vec3 vec = Vec3.Zero;
					if (flag4)
					{
						vec.x = this.SceneLayer.Input.GetGameKeyAxis("MovementAxisX");
						vec.y = this.SceneLayer.Input.GetGameKeyAxis("MovementAxisY");
						if (MathF.Abs(vec.x) < 0.2f)
						{
							vec.x = 0f;
						}
						if (MathF.Abs(vec.y) < 0.2f)
						{
							vec.y = 0f;
						}
					}
					if (!this._isGamepadActive || (!this.IsPhotoModeEnabled && this.Mission.Mode != MissionMode.Deployment && !this.IsOrderMenuOpen && !this.IsTransferMenuOpen))
					{
						if (this.SceneLayer.Input.IsGameKeyDown(14))
						{
							vec.z += 1f;
						}
						if (this.SceneLayer.Input.IsGameKeyDown(15))
						{
							vec.z -= 1f;
						}
					}
					else if (this.Mission.Mode == MissionMode.Deployment && this.SceneLayer.IsHitThisFrame)
					{
						if (this.SceneLayer.Input.IsKeyDown(InputKey.ControllerRBumper))
						{
							vec.z += 1f;
						}
						if (this.SceneLayer.Input.IsKeyDown(InputKey.ControllerLBumper))
						{
							vec.z -= 1f;
						}
					}
					if (vec.IsNonZero)
					{
						float val = vec.Normalize();
						vec *= num4 * Math.Min(1f, val);
						this._cameraSpeed += vec;
					}
				}
				if (this.Mission.Mode == MissionMode.Deployment && !this.IsRadialMenuActive)
				{
					Vec3 origin = matrixFrame.origin;
					float x = this._cameraSpeed.x;
					Vec3 vec2 = new Vec3(matrixFrame.rotation.s.AsVec2, 0f, -1f);
					matrixFrame.origin = origin + x * vec2.NormalizedCopy() * dt;
					Vec3 origin2 = matrixFrame.origin;
					float y = this._cameraSpeed.y;
					vec2 = new Vec3(matrixFrame.rotation.u.AsVec2, 0f, -1f);
					matrixFrame.origin = origin2 - y * vec2.NormalizedCopy() * dt;
					matrixFrame.origin.z = matrixFrame.origin.z + this._cameraSpeed.z * dt;
					if (!Game.Current.CheatMode || !this.InputManager.IsControlDown())
					{
						this._cameraDeploymentHeightToAdd += 3f * this.SceneLayer.Input.GetDeltaMouseScroll() / 120f;
						if (this.SceneLayer.Input.IsHotKeyDown("DeploymentCameraIsActive"))
						{
							this._cameraDeploymentHeightToAdd += 0.05f * Input.MouseMoveY;
						}
					}
					if (MathF.Abs(this._cameraDeploymentHeightToAdd) > 0.001f)
					{
						matrixFrame.origin.z = matrixFrame.origin.z + this._cameraDeploymentHeightToAdd * dt * 10f;
						this._cameraDeploymentHeightToAdd = MathF.Lerp(this._cameraDeploymentHeightToAdd, 0f, 1f - MathF.Pow(0.0005f, dt), 1E-05f);
					}
					else
					{
						matrixFrame.origin.z = matrixFrame.origin.z + this._cameraDeploymentHeightToAdd;
						this._cameraDeploymentHeightToAdd = 0f;
					}
				}
				else
				{
					matrixFrame.origin += this._cameraSpeed.x * matrixFrame.rotation.s * dt;
					matrixFrame.origin -= this._cameraSpeed.y * matrixFrame.rotation.u * dt;
					matrixFrame.origin += this._cameraSpeed.z * matrixFrame.rotation.f * dt;
				}
				if (!MBEditor.IsEditModeOn)
				{
					if (!this.Mission.IsPositionInsideBoundaries(matrixFrame.origin.AsVec2))
					{
						matrixFrame.origin.AsVec2 = this.Mission.GetClosestBoundaryPosition(matrixFrame.origin.AsVec2);
					}
					if (!GameNetwork.IsMultiplayer && this.Mission.Mode == MissionMode.Deployment)
					{
						BattleSideEnum side = this.Mission.PlayerTeam.Side;
						IMissionDeploymentPlan deploymentPlan = this.Mission.DeploymentPlan;
						if (deploymentPlan.HasDeploymentBoundaries(side))
						{
							IMissionDeploymentPlan missionDeploymentPlan = deploymentPlan;
							BattleSideEnum battleSide = side;
							Vec2 vec3 = matrixFrame.origin.AsVec2;
							if (!missionDeploymentPlan.IsPositionInsideDeploymentBoundaries(battleSide, vec3))
							{
								IMissionDeploymentPlan missionDeploymentPlan2 = deploymentPlan;
								BattleSideEnum battleSide2 = side;
								vec3 = matrixFrame.origin.AsVec2;
								matrixFrame.origin.AsVec2 = missionDeploymentPlan2.GetClosestDeploymentBoundaryPosition(battleSide2, vec3, false, 0f);
							}
						}
					}
					float groundHeightAtPosition2 = scene.GetGroundHeightAtPosition((this.Mission.Mode == MissionMode.Deployment) ? (matrixFrame.origin + new Vec3(0f, 0f, 100f, -1f)) : matrixFrame.origin, BodyFlags.CommonCollisionExcludeFlags);
					if (!this.IsCheatGhostMode && groundHeightAtPosition2 < 9999f)
					{
						matrixFrame.origin.z = MathF.Max(matrixFrame.origin.z, groundHeightAtPosition2 + 0.5f);
					}
					if (matrixFrame.origin.z > groundHeightAtPosition2 + 80f)
					{
						matrixFrame.origin.z = groundHeightAtPosition2 + 80f;
					}
					if (this._cameraHeightLimit > 0f && matrixFrame.origin.z > this._cameraHeightLimit)
					{
						matrixFrame.origin.z = this._cameraHeightLimit;
					}
					if (matrixFrame.origin.z < -100f)
					{
						matrixFrame.origin.z = -100f;
					}
				}
			}
			else if (flag2 && !this.IsPhotoModeEnabled)
			{
				Agent agent = agentToFollow;
				if (agentToFollow.AgentVisuals != null)
				{
					if (this._cameraAddSpecialMovement)
					{
						if (this.Mission.Mode == MissionMode.Conversation || this.Mission.Mode == MissionMode.Barter)
						{
							MissionMainAgentController missionMainAgentController = this._missionMainAgentController;
							if (((missionMainAgentController != null) ? missionMainAgentController.InteractionComponent.CurrentFocusedObject : null) != null && this._missionMainAgentController.InteractionComponent.CurrentFocusedObject.FocusableObjectType == FocusableObjectType.Agent)
							{
								Vec3 vec4 = (this._missionMainAgentController.InteractionComponent.CurrentFocusedObject as Agent).Position - agentToFollow.Position;
								float cameraSpecialTargetFOV;
								if (65f / this.CameraViewAngle * MathF.Abs(vec4.z) >= 2f)
								{
									float num5 = 160f;
									Vec2 vec3 = vec4.AsVec2;
									cameraSpecialTargetFOV = num5 / vec3.Length;
								}
								else
								{
									float a2 = (this.Mission.Mode == MissionMode.Barter) ? 48.75f : 32.5f;
									float num6 = (this.Mission.Mode == MissionMode.Barter) ? 75f : 50f;
									Vec2 vec3 = vec4.AsVec2;
									cameraSpecialTargetFOV = MathF.Min(a2, num6 / vec3.Length);
								}
								this._cameraSpecialTargetFOV = cameraSpecialTargetFOV;
								goto IL_BF7;
							}
						}
						this._cameraSpecialTargetFOV = 65f;
						IL_BF7:
						if (flag3)
						{
							this._cameraSpecialCurrentFOV = this._cameraSpecialTargetFOV;
						}
					}
					MatrixFrame boneEntitialFrame = agentToFollow.AgentVisuals.GetBoneEntitialFrame(agentToFollow.Monster.ThoraxLookDirectionBoneIndex, true);
					MatrixFrame boneEntitialFrame2 = agentToFollow.AgentVisuals.GetBoneEntitialFrame(agentToFollow.Monster.HeadLookDirectionBoneIndex, true);
					boneEntitialFrame2.origin = boneEntitialFrame2.TransformToParent(agent.Monster.FirstPersonCameraOffsetWrtHead);
					MatrixFrame frame = agentToFollow.AgentVisuals.GetFrame();
					Vec3 vec5 = frame.TransformToParent(boneEntitialFrame2.origin);
					bool flag5;
					if (this.Mission.Mode == MissionMode.Conversation || this.Mission.Mode == MissionMode.Barter)
					{
						MissionMainAgentController missionMainAgentController2 = this._missionMainAgentController;
						if (((missionMainAgentController2 != null) ? missionMainAgentController2.InteractionComponent.CurrentFocusedObject : null) != null)
						{
							flag5 = (this._missionMainAgentController.InteractionComponent.CurrentFocusedObject.FocusableObjectType == FocusableObjectType.Agent);
							goto IL_CC5;
						}
					}
					flag5 = false;
					IL_CC5:
					bool flag6 = flag5;
					if ((agent.GetCurrentAnimationFlag(0) & AnimFlags.anf_lock_camera) != (AnimFlags)0UL || (agent.GetCurrentAnimationFlag(1) & AnimFlags.anf_lock_camera) != (AnimFlags)0UL)
					{
						MatrixFrame matrixFrame2 = frame.TransformToParent(boneEntitialFrame2);
						matrixFrame2.rotation.MakeUnit();
						this.CameraBearing = matrixFrame2.rotation.f.RotationZ;
						this.CameraElevation = matrixFrame2.rotation.f.RotationX;
					}
					else
					{
						if (!flag6)
						{
							if (agentToFollow.IsMainAgent && this._missionMainAgentController != null)
							{
								Vec3 vec2 = this._missionMainAgentController.CustomLookDir;
								if (vec2.IsNonZero)
								{
									goto IL_D68;
								}
							}
							float num7 = MBMath.WrapAngle(this.CameraBearing);
							float num8 = MBMath.WrapAngle(this.CameraElevation);
							float valueTo2;
							float valueTo3;
							this.CalculateNewBearingAndElevationForFirstPerson(agentToFollow, num7, num8, out valueTo2, out valueTo3);
							this.CameraBearing = MBMath.LerpRadians(num7, valueTo2, Math.Min(dt * 12f, 1f), 1E-05f, 0.5f);
							this.CameraElevation = MBMath.LerpRadians(num8, valueTo3, Math.Min(dt * 12f, 1f), 1E-05f, 0.5f);
							goto IL_F51;
						}
						IL_D68:
						Vec3 vec6;
						if (flag6)
						{
							Agent agent2 = this._missionMainAgentController.InteractionComponent.CurrentFocusedObject as Agent;
							Vec3 vec2 = agent2.Position;
							vec2 = new Vec3(vec2.AsVec2, agent2.AgentVisuals.GetGlobalStableEyePoint(agent2.IsHuman).z, -1f) - vec5;
							vec6 = vec2.NormalizedCopy();
							vec2 = new Vec3(vec6.y, -vec6.x, 0f, -1f);
							Vec3 vec7 = vec2.NormalizedCopy();
							vec6 = vec6.RotateAboutAnArbitraryVector(vec7, ((this.Mission.Mode == MissionMode.Conversation) ? -0.003f : -0.0045f) * this._cameraSpecialCurrentFOV);
						}
						else
						{
							vec6 = this._missionMainAgentController.CustomLookDir;
						}
						if (flag3)
						{
							this.CameraBearing = vec6.RotationZ;
							this.CameraElevation = vec6.RotationX;
						}
						else
						{
							Mat3 identity = Mat3.Identity;
							identity.RotateAboutUp(this.CameraBearing);
							identity.RotateAboutSide(this.CameraElevation);
							Vec3 f = identity.f;
							Vec3 vec8 = Vec3.CrossProduct(f, vec6);
							float num9 = vec8.Normalize();
							Vec3 vec9;
							if (num9 < 0.0001f)
							{
								vec9 = vec6;
							}
							else
							{
								vec9 = f;
								vec9 = vec9.RotateAboutAnArbitraryVector(vec8, num9 * dt * 5f);
							}
							this.CameraBearing = vec9.RotationZ;
							this.CameraElevation = vec9.RotationX;
						}
					}
					IL_F51:
					matrixFrame.rotation.RotateAboutSide(1.5707964f);
					matrixFrame.rotation.RotateAboutForward(this.CameraBearing);
					matrixFrame.rotation.RotateAboutSide(this.CameraElevation);
					float actionChannelWeight = agentToFollow.GetActionChannelWeight(1);
					float f2 = MBMath.WrapAngle(this.CameraBearing - agentToFollow.MovementDirectionAsAngle);
					float num10 = 1f - (1f - actionChannelWeight) * MBMath.ClampFloat((MathF.Abs(f2) - 1f) * 0.66f, 0f, 1f);
					float f3 = 0.25f;
					float f4 = 0.15f;
					float f5 = 0.15f;
					Vec3 v = frame.rotation.u * f3;
					Vec3 v2 = frame.rotation.u * f4 + Vec3.Forward * f5;
					v2.RotateAboutX(MBMath.ClampFloat(this.CameraElevation, -0.35f, 0.35f));
					v2.RotateAboutZ(this.CameraBearing);
					Vec3 vec10 = frame.TransformToParent(boneEntitialFrame.origin);
					vec10 += v;
					vec10 += v2;
					if (actionChannelWeight > 0f)
					{
						this._currentViewBlockingBodyCoeff = (this._targetViewBlockingBodyCoeff = 1f);
						this._applySmoothTransitionToVirtualEyeCamera = true;
					}
					else
					{
						Vec3 vec2 = vec5 - vec10;
						Vec3 vec11 = vec2.NormalizedCopy();
						if (Vec3.DotProduct(matrixFrame.rotation.u, vec11) > 0f)
						{
							vec11 = -vec11;
						}
						float num11 = 0.97499996f;
						float num12 = MathF.Lerp(0.55f, 0.7f, MathF.Abs(matrixFrame.rotation.u.z), 1E-05f);
						float num13;
						GameEntity gameEntity;
						if (this.Mission.Scene.RayCastForClosestEntityOrTerrain(vec5 - vec11 * (num11 * num12), vec5 + vec11 * (num11 * (1f - num12)), out num13, out vec2, out gameEntity, 0.01f, BodyFlags.Disabled | BodyFlags.Dynamic | BodyFlags.Ladder | BodyFlags.OnlyCollideWithRaycast | BodyFlags.AILimiter | BodyFlags.Barrier | BodyFlags.Barrier3D | BodyFlags.Ragdoll | BodyFlags.RagdollLimiter | BodyFlags.DroppedItem | BodyFlags.DoNotCollideWithRaycast | BodyFlags.DontCollideWithCamera | BodyFlags.AgentOnly | BodyFlags.MissileOnly))
						{
							float num14 = (num11 - num13) / 0.065f;
							this._targetViewBlockingBodyCoeff = 1f / MathF.Max(1f, num14 * num14 * num14);
						}
						else
						{
							this._targetViewBlockingBodyCoeff = 1f;
						}
						if (this._currentViewBlockingBodyCoeff < this._targetViewBlockingBodyCoeff)
						{
							this._currentViewBlockingBodyCoeff = MathF.Min(this._currentViewBlockingBodyCoeff + dt * 12f, this._targetViewBlockingBodyCoeff);
						}
						else if (this._currentViewBlockingBodyCoeff > this._targetViewBlockingBodyCoeff)
						{
							if (this._applySmoothTransitionToVirtualEyeCamera)
							{
								this._currentViewBlockingBodyCoeff = MathF.Max(this._currentViewBlockingBodyCoeff - dt * 6f, this._targetViewBlockingBodyCoeff);
							}
							else
							{
								this._currentViewBlockingBodyCoeff = this._targetViewBlockingBodyCoeff;
							}
						}
						else
						{
							this._applySmoothTransitionToVirtualEyeCamera = false;
						}
						num10 *= this._currentViewBlockingBodyCoeff;
					}
					matrixFrame.origin.x = MBMath.Lerp(vec10.x, vec5.x, num10, 1E-05f);
					matrixFrame.origin.y = MBMath.Lerp(vec10.y, vec5.y, num10, 1E-05f);
					matrixFrame.origin.z = MBMath.Lerp(vec10.z, vec5.z, actionChannelWeight, 1E-05f);
				}
				else
				{
					matrixFrame = this.CombatCamera.Frame;
				}
			}
			else
			{
				float num15 = 0.6f;
				float num16 = 0f;
				bool flag7 = agentVisualToFollow != null;
				float num17 = 1f;
				bool flag8 = false;
				float num19;
				if (flag7)
				{
					this._cameraSpecialTargetAddedBearing = 0f;
					this._cameraSpecialTargetAddedElevation = 0f;
					this._cameraSpecialTargetPositionToAdd = Vec3.Zero;
					this._cameraSpecialTargetDistanceToAdd = 0f;
					num15 = 1.25f;
					flag3 = (flag3 || agentVisualToFollow != this.LastFollowedAgentVisuals);
					if (agentVisualToFollow.GetEquipment().Horse.Item != null)
					{
						float num18 = (float)agentVisualToFollow.GetEquipment().Horse.Item.HorseComponent.BodyLength * 0.01f;
						num15 += 2f;
						num19 = 1f * num18 + 0.9f * num17 - 0.2f;
					}
					else
					{
						num19 = 1f * num17;
					}
					this.CameraBearing = MBMath.WrapAngle(agentVisualToFollow.GetFrame().rotation.f.RotationZ + 3.1415927f);
					this.CameraElevation = 0.15f;
				}
				else
				{
					flag8 = agentToFollow.HasMount;
					num17 = agentToFollow.AgentScale;
					flag3 = (flag3 || agentToFollow != this.LastFollowedAgent);
					if (this.Mission.Mode == MissionMode.Conversation || this.Mission.Mode == MissionMode.Barter)
					{
						MissionMainAgentController missionMainAgentController3 = this._missionMainAgentController;
						if (((missionMainAgentController3 != null) ? missionMainAgentController3.InteractionComponent.CurrentFocusedObject : null) != null && this._missionMainAgentController.InteractionComponent.CurrentFocusedObject.FocusableObjectType == FocusableObjectType.Agent)
						{
							MissionMainAgentController missionMainAgentController4 = this._missionMainAgentController;
							Agent agent3 = ((missionMainAgentController4 != null) ? missionMainAgentController4.InteractionComponent.CurrentFocusedObject : null) as Agent;
							num19 = (agent3.AgentVisuals.GetGlobalStableEyePoint(true).z + agentToFollow.AgentVisuals.GetGlobalStableEyePoint(true).z) * 0.5f - agentToFollow.Position.z;
							if (agent3.HasMount)
							{
								num15 += 0.1f;
							}
							if (this.Mission.Mode == MissionMode.Barter)
							{
								Vec3 vec2 = agent3.Position;
								Vec2 asVec = vec2.AsVec2;
								vec2 = agentToFollow.Position;
								Vec2 vec12 = asVec - vec2.AsVec2;
								float length = vec12.Length;
								float num20 = MathF.Max(num15 + Mission.CameraAddedDistance, 0.48f) * num17 + length * 0.5f;
								num19 += -0.004f * num20 * this._cameraSpecialCurrentFOV;
								Vec3 globalStableEyePoint = agent3.AgentVisuals.GetGlobalStableEyePoint(agent3.IsHuman);
								Vec3 globalStableEyePoint2 = agentToFollow.AgentVisuals.GetGlobalStableEyePoint(agentToFollow.IsHuman);
								float num21 = vec12.RotationInRadians - MathF.Min(0.47123894f, 0.4f / length);
								this._cameraSpecialTargetAddedBearing = MBMath.WrapAngle(num21 - this.CameraBearing);
								Vec2 vec13 = new Vec2(globalStableEyePoint.z - globalStableEyePoint2.z, MathF.Max(length, 1f));
								float num22 = (flag8 ? -0.03f : 0f) - vec13.RotationInRadians;
								this._cameraSpecialTargetAddedElevation = num22 - this.CameraElevation + MathF.Asin(-0.2f * (num20 - length * 0.5f) / num20);
								goto IL_16A4;
							}
							goto IL_16A4;
						}
					}
					if (flag8)
					{
						num15 += 0.1f;
						Agent mountAgent = agentToFollow.MountAgent;
						Monster monster = mountAgent.Monster;
						num19 = (monster.RiderCameraHeightAdder + monster.BodyCapsulePoint1.z + monster.BodyCapsuleRadius) * mountAgent.AgentScale + agentToFollow.Monster.CrouchEyeHeight * num17;
					}
					else if (agentToFollow.AgentVisuals.GetCurrentRagdollState() == RagdollState.Active)
					{
						num19 = 0.5f;
					}
					else if ((agentToFollow.GetCurrentAnimationFlag(0) & AnimFlags.anf_reset_camera_height) != (AnimFlags)0UL)
					{
						num19 = 0.5f;
					}
					else if (agentToFollow.CrouchMode || agentToFollow.IsSitting())
					{
						num19 = (agentToFollow.Monster.CrouchEyeHeight + 0.2f) * num17;
					}
					else
					{
						num19 = (agentToFollow.Monster.StandingEyeHeight + 0.2f) * num17;
					}
					IL_16A4:
					if ((this.IsViewingCharacter() && (cameraType != SpectatorCameraTypes.LockToTeamMembersView || agentToFollow == this.Mission.MainAgent)) || this.IsPhotoModeEnabled)
					{
						num19 *= 0.5f;
						num15 += 0.5f;
					}
					else if (agentToFollow.HasMount && agentToFollow.IsDoingPassiveAttack && (cameraType != SpectatorCameraTypes.LockToTeamMembersView || agentToFollow == this.Mission.MainAgent))
					{
						num19 *= 1.1f;
					}
					if (this._cameraAddSpecialMovement)
					{
						if (this.Mission.Mode == MissionMode.Conversation || this.Mission.Mode == MissionMode.Barter)
						{
							MissionMainAgentController missionMainAgentController5 = this._missionMainAgentController;
							if (((missionMainAgentController5 != null) ? missionMainAgentController5.InteractionComponent.CurrentFocusedObject : null) != null && this._missionMainAgentController.InteractionComponent.CurrentFocusedObject.FocusableObjectType == FocusableObjectType.Agent)
							{
								Agent agent4 = this._missionMainAgentController.InteractionComponent.CurrentFocusedObject as Agent;
								Vec3 globalStableEyePoint3 = agent4.AgentVisuals.GetGlobalStableEyePoint(true);
								Vec3 globalStableEyePoint4 = agentToFollow.AgentVisuals.GetGlobalStableEyePoint(true);
								Vec3 vec2 = agent4.Position;
								Vec2 asVec2 = vec2.AsVec2;
								vec2 = agentToFollow.Position;
								Vec2 v3 = asVec2 - vec2.AsVec2;
								float length2 = v3.Length;
								this._cameraSpecialTargetPositionToAdd = new Vec3(v3 * 0.5f, 0f, -1f);
								this._cameraSpecialTargetDistanceToAdd = length2 * (flag8 ? 1.3f : 0.8f) - num15;
								float num23 = v3.RotationInRadians - MathF.Min(0.47123894f, 0.48f / length2);
								this._cameraSpecialTargetAddedBearing = MBMath.WrapAngle(num23 - this.CameraBearing);
								Vec2 vec14 = new Vec2(globalStableEyePoint3.z - globalStableEyePoint4.z, MathF.Max(length2, 1f));
								float num24 = (flag8 ? -0.03f : 0f) - vec14.RotationInRadians;
								this._cameraSpecialTargetAddedElevation = num24 - this.CameraElevation;
								this._cameraSpecialTargetFOV = MathF.Min(32.5f, 50f / length2);
								goto IL_18E0;
							}
						}
						this._cameraSpecialTargetPositionToAdd = Vec3.Zero;
						this._cameraSpecialTargetDistanceToAdd = 0f;
						this._cameraSpecialTargetAddedBearing = 0f;
						this._cameraSpecialTargetAddedElevation = 0f;
						this._cameraSpecialTargetFOV = 65f;
						IL_18E0:
						if (flag3)
						{
							this._cameraSpecialCurrentPositionToAdd = this._cameraSpecialTargetPositionToAdd;
							this._cameraSpecialCurrentDistanceToAdd = this._cameraSpecialTargetDistanceToAdd;
							this._cameraSpecialCurrentAddedBearing = this._cameraSpecialTargetAddedBearing;
							this._cameraSpecialCurrentAddedElevation = this._cameraSpecialTargetAddedElevation;
							this._cameraSpecialCurrentFOV = this._cameraSpecialTargetFOV;
						}
					}
					if (this._cameraSpecialCurrentDistanceToAdd != this._cameraSpecialTargetDistanceToAdd)
					{
						float num25 = this._cameraSpecialTargetDistanceToAdd - this._cameraSpecialCurrentDistanceToAdd;
						if (flag3 || MathF.Abs(num25) < 0.0001f)
						{
							this._cameraSpecialCurrentDistanceToAdd = this._cameraSpecialTargetDistanceToAdd;
						}
						else
						{
							float num26 = num25 * 4f * dt;
							this._cameraSpecialCurrentDistanceToAdd += num26;
						}
					}
					num15 += this._cameraSpecialCurrentDistanceToAdd;
				}
				if (flag3)
				{
					this._cameraTargetAddedHeight = num19;
				}
				else
				{
					this._cameraTargetAddedHeight += (num19 - this._cameraTargetAddedHeight) * dt * 6f * num17;
				}
				if (this._cameraSpecialTargetAddedBearing != this._cameraSpecialCurrentAddedBearing)
				{
					float num27 = this._cameraSpecialTargetAddedBearing - this._cameraSpecialCurrentAddedBearing;
					if (flag3 || MathF.Abs(num27) < 0.0001f)
					{
						this._cameraSpecialCurrentAddedBearing = this._cameraSpecialTargetAddedBearing;
					}
					else
					{
						float num28 = num27 * 10f * dt;
						this._cameraSpecialCurrentAddedBearing += num28;
					}
				}
				if (this._cameraSpecialTargetAddedElevation != this._cameraSpecialCurrentAddedElevation)
				{
					float num29 = this._cameraSpecialTargetAddedElevation - this._cameraSpecialCurrentAddedElevation;
					if (flag3 || MathF.Abs(num29) < 0.0001f)
					{
						this._cameraSpecialCurrentAddedElevation = this._cameraSpecialTargetAddedElevation;
					}
					else
					{
						float num30 = num29 * 8f * dt;
						this._cameraSpecialCurrentAddedElevation += num30;
					}
				}
				matrixFrame.rotation.RotateAboutSide(1.5707964f);
				if (agentToFollow != null && !agentToFollow.IsMine && cameraType == SpectatorCameraTypes.LockToTeamMembersView)
				{
					Vec3 lookDirection = agentToFollow.LookDirection;
					Vec2 vec3 = lookDirection.AsVec2;
					matrixFrame.rotation.RotateAboutForward(vec3.RotationInRadians);
					matrixFrame.rotation.RotateAboutSide(MathF.Asin(lookDirection.z));
				}
				else
				{
					matrixFrame.rotation.RotateAboutForward(this.CameraBearing + this._cameraSpecialCurrentAddedBearing);
					matrixFrame.rotation.RotateAboutSide(this.CameraElevation + this._cameraSpecialCurrentAddedElevation);
					if (this.IsPhotoModeEnabled)
					{
						float a3 = -scene.GetPhotoModeRoll();
						matrixFrame.rotation.RotateAboutUp(a3);
					}
				}
				MatrixFrame matrixFrame3 = matrixFrame;
				float num31 = MathF.Max(num15 + Mission.CameraAddedDistance, 0.48f) * num17;
				if (this.Mission.Mode != MissionMode.Conversation && this.Mission.Mode != MissionMode.Barter && agentToFollow != null && agentToFollow.IsActive() && BannerlordConfig.EnableVerticalAimCorrection)
				{
					WeaponComponentData currentUsageItem = agentToFollow.WieldedWeapon.CurrentUsageItem;
					if (currentUsageItem != null && currentUsageItem.IsRangedWeapon)
					{
						MatrixFrame frame2 = this.CombatCamera.Frame;
						frame2.rotation.RotateAboutSide(-this._cameraAddedElevation);
						float num32;
						if (flag8)
						{
							Agent mountAgent2 = agentToFollow.MountAgent;
							Monster monster2 = mountAgent2.Monster;
							num32 = (monster2.RiderCameraHeightAdder + monster2.BodyCapsulePoint1.z + monster2.BodyCapsuleRadius) * mountAgent2.AgentScale + agentToFollow.Monster.CrouchEyeHeight * num17;
						}
						else
						{
							num32 = agentToFollow.Monster.StandingEyeHeight * num17;
						}
						if (currentUsageItem.WeaponFlags.HasAnyFlag(WeaponFlags.UseHandAsThrowBase))
						{
							num32 *= 1.25f;
						}
						float num34;
						if (flag3)
						{
							Vec3 v4 = agentToFollow.Position + matrixFrame.rotation.f * num17 * (0.7f * MathF.Pow(MathF.Cos(1f / ((num31 / num17 - 0.2f) * 30f + 20f)), 3500f));
							v4.z += this._cameraTargetAddedHeight;
							Vec3 vec15 = v4 + matrixFrame.rotation.u * num31;
							float z = vec15.z;
							float num33 = -matrixFrame3.rotation.u.z;
							Vec2 asVec3 = vec15.AsVec2;
							Vec3 vec2 = agentToFollow.Position;
							Vec2 vec3 = asVec3 - vec2.AsVec2;
							num34 = z + num33 * vec3.Length - (agentToFollow.Position.z + num32);
						}
						else
						{
							float z2 = frame2.origin.z;
							float num35 = -frame2.rotation.u.z;
							Vec2 asVec4 = frame2.origin.AsVec2;
							Vec3 vec2 = agentToFollow.Position;
							Vec2 vec3 = asVec4 - vec2.AsVec2;
							num34 = z2 + num35 * vec3.Length - (agentToFollow.Position.z + num32);
						}
						if (num34 > 0f)
						{
							num16 = MathF.Max(-0.15f, -MathF.Asin(MathF.Min(1f, MathF.Sqrt(19.6f * num34) / (float)agentToFollow.WieldedWeapon.GetModifiedMissileSpeedForCurrentUsage())));
						}
						else
						{
							num16 = 0f;
						}
					}
					else
					{
						num16 = ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.MeleeAddedElevationForCrosshair);
					}
				}
				if (flag3 || this.IsPhotoModeEnabled)
				{
					this._cameraAddedElevation = num16;
				}
				else
				{
					this._cameraAddedElevation += (num16 - this._cameraAddedElevation) * dt * 3f;
				}
				if (!this.IsPhotoModeEnabled)
				{
					matrixFrame.rotation.RotateAboutSide(this._cameraAddedElevation);
				}
				bool flag9 = this.IsViewingCharacter() && !GameNetwork.IsSessionActive;
				bool flag10 = agentToFollow != null && agentToFollow.AgentVisuals != null && agentToFollow.AgentVisuals.GetCurrentRagdollState() > RagdollState.Disabled;
				bool flag11 = agentToFollow != null && agentToFollow.IsActive() && agentToFollow.GetCurrentActionType(0) == Agent.ActionCodeType.Mount;
				Vec2 v5 = Vec2.Zero;
				Vec3 vec16;
				Vec3 vec17;
				if (flag7)
				{
					MBAgentVisuals visuals = this.GetPlayerAgentVisuals(missionPeer).GetVisuals();
					vec16 = ((visuals != null) ? visuals.GetGlobalFrame().origin : missionPeer.ControlledAgent.Position);
					vec17 = vec16;
				}
				else
				{
					vec16 = agentToFollow.VisualPosition;
					vec17 = (flag10 ? agentToFollow.AgentVisuals.GetFrame().origin : vec16);
					if (flag8)
					{
						v5 = agentToFollow.MountAgent.GetMovementDirection() * agentToFollow.MountAgent.Monster.RiderBodyCapsuleForwardAdder;
						vec17 += v5.ToVec3(0f);
					}
				}
				if (this._cameraAddSpecialPositionalMovement)
				{
					Vec3 v6 = matrixFrame3.rotation.f * num17 * (0.7f * MathF.Pow(MathF.Cos(1f / ((num31 / num17 - 0.2f) * 30f + 20f)), 3500f));
					if (this.Mission.Mode == MissionMode.Conversation || this.Mission.Mode == MissionMode.Barter)
					{
						this._cameraSpecialCurrentPositionToAdd += v6;
					}
					else
					{
						this._cameraSpecialCurrentPositionToAdd -= v6;
					}
				}
				if (this._cameraSpecialCurrentPositionToAdd != this._cameraSpecialTargetPositionToAdd)
				{
					Vec3 v7 = this._cameraSpecialTargetPositionToAdd - this._cameraSpecialCurrentPositionToAdd;
					if (flag3 || v7.LengthSquared < 1.0000001E-06f)
					{
						this._cameraSpecialCurrentPositionToAdd = this._cameraSpecialTargetPositionToAdd;
					}
					else
					{
						this._cameraSpecialCurrentPositionToAdd += v7 * 4f * dt;
					}
				}
				vec16 += this._cameraSpecialCurrentPositionToAdd;
				vec17 += this._cameraSpecialCurrentPositionToAdd;
				vec17.z += this._cameraTargetAddedHeight;
				int num36 = 0;
				bool flag12 = agentToFollow != null;
				Vec3 supportRaycastPoint = flag12 ? (flag8 ? agentToFollow.MountAgent.GetChestGlobalPosition() : agentToFollow.GetChestGlobalPosition()) : Vec3.Invalid;
				bool flag13;
				do
				{
					Vec3 vec18 = vec17;
					if (this.Mission.Mode != MissionMode.Conversation && this.Mission.Mode != MissionMode.Barter)
					{
						vec18 += matrixFrame3.rotation.f * num17 * (0.7f * MathF.Pow(MathF.Cos(1f / ((num31 / num17 - 0.2f) * 30f + 20f)), 3500f));
					}
					Vec3 o = vec18 + matrixFrame3.rotation.u * num31;
					if (flag10 || flag11)
					{
						float num37 = 0f;
						if (flag11)
						{
							float currentActionProgress = agentToFollow.GetCurrentActionProgress(0);
							num37 = currentActionProgress * currentActionProgress * 20f;
						}
						vec18 = this._cameraTarget + (vec18 - this._cameraTarget) * (5f + num37) * dt;
					}
					flag13 = false;
					MatrixFrame matrixFrame4 = new MatrixFrame(matrixFrame.rotation, o);
					Camera.GetNearPlanePointsStatic(ref matrixFrame4, this.IsPhotoModeEnabled ? (num * 0.017453292f) : (this.CameraViewAngle * 0.017453292f), Screen.AspectRatio, 0.2f, 1f, this._cameraNearPlanePoints);
					Vec3 vec19 = Vec3.Zero;
					for (int i = 0; i < 4; i++)
					{
						vec19 += this._cameraNearPlanePoints[i];
					}
					vec19 *= 0.25f;
					Vec3 vec20 = new Vec3(vec16.AsVec2 + v5, vec18.z, -1f);
					Vec3 v8 = vec20 - vec19;
					for (int j = 0; j < 4; j++)
					{
						this._cameraNearPlanePoints[j] += v8;
					}
					this._cameraBoxPoints[0] = this._cameraNearPlanePoints[3] + matrixFrame4.rotation.u * 0.01f;
					this._cameraBoxPoints[1] = this._cameraNearPlanePoints[0];
					this._cameraBoxPoints[2] = this._cameraNearPlanePoints[3];
					this._cameraBoxPoints[3] = this._cameraNearPlanePoints[2];
					this._cameraBoxPoints[4] = this._cameraNearPlanePoints[1] + matrixFrame4.rotation.u * 0.01f;
					this._cameraBoxPoints[5] = this._cameraNearPlanePoints[0] + matrixFrame4.rotation.u * 0.01f;
					this._cameraBoxPoints[6] = this._cameraNearPlanePoints[1];
					this._cameraBoxPoints[7] = this._cameraNearPlanePoints[2] + matrixFrame4.rotation.u * 0.01f;
					float num38 = (this.IsPhotoModeEnabled && !flag && photoModeOrbit) ? this._zoomAmount : 0f;
					num31 += num38;
					GameEntity gameEntity;
					float num39;
					Vec3 v9;
					if (scene.BoxCastOnlyForCamera(this._cameraBoxPoints, vec20, flag12, supportRaycastPoint, matrixFrame4.rotation.u, num31 + 0.5f, out num39, out v9, out gameEntity, true, true, BodyFlags.Disabled | BodyFlags.Dynamic | BodyFlags.Ladder | BodyFlags.OnlyCollideWithRaycast | BodyFlags.AILimiter | BodyFlags.Barrier | BodyFlags.Barrier3D | BodyFlags.Ragdoll | BodyFlags.RagdollLimiter | BodyFlags.DroppedItem | BodyFlags.DoNotCollideWithRaycast | BodyFlags.DontCollideWithCamera | BodyFlags.AgentOnly | BodyFlags.MissileOnly))
					{
						num39 = MathF.Max(Vec3.DotProduct(matrixFrame4.rotation.u, v9 - vec18), 0.48f * num17);
						if (num39 < num31)
						{
							flag13 = true;
							num31 = num39;
						}
					}
					num36++;
				}
				while (!flag9 && num36 < 5 && flag13);
				num15 = num31 - Mission.CameraAddedDistance;
				if (flag3 || (this.CameraResultDistanceToTarget > num31 && num36 > 1))
				{
					this.CameraResultDistanceToTarget = num31;
				}
				else
				{
					float num40 = MathF.Max(MathF.Abs(Mission.CameraAddedDistance - this._lastCameraAddedDistance) * num17, MathF.Abs((num15 - (this.CameraResultDistanceToTarget - this._lastCameraAddedDistance)) * dt * 3f * num17));
					this.CameraResultDistanceToTarget += MBMath.ClampFloat(num31 - this.CameraResultDistanceToTarget, -num40, num40);
				}
				this._lastCameraAddedDistance = Mission.CameraAddedDistance;
				this._cameraTarget = vec17;
				if (this.Mission.Mode != MissionMode.Conversation && this.Mission.Mode != MissionMode.Barter)
				{
					this._cameraTarget += matrixFrame3.rotation.f * num17 * (0.7f * MathF.Pow(MathF.Cos(1f / ((num31 / num17 - 0.2f) * 30f + 20f)), 3500f));
				}
				matrixFrame.origin = this._cameraTarget + matrixFrame3.rotation.u * this.CameraResultDistanceToTarget;
			}
			if (this._cameraSpecialCurrentFOV != this._cameraSpecialTargetFOV)
			{
				float num41 = this._cameraSpecialTargetFOV - this._cameraSpecialCurrentFOV;
				if (flag3 || MathF.Abs(num41) < 0.001f)
				{
					this._cameraSpecialCurrentFOV = this._cameraSpecialTargetFOV;
				}
				else
				{
					this._cameraSpecialCurrentFOV += num41 * 3f * dt;
				}
			}
			float newDNear = this.Mission.CameraIsFirstPerson ? 0.065f : 0.1f;
			this.CombatCamera.Frame = matrixFrame;
			if (this.IsPhotoModeEnabled)
			{
				float depthOfFieldFocus = 0f;
				float depthOfFieldFocusStart = 0f;
				float depthOfFieldFocusEnd = 0f;
				float num42 = 0f;
				bool isVignetteOn = false;
				scene.GetPhotoModeFocus(ref depthOfFieldFocusStart, ref depthOfFieldFocusEnd, ref depthOfFieldFocus, ref num42, ref isVignetteOn);
				scene.SetDepthOfFieldFocus(depthOfFieldFocus);
				scene.SetDepthOfFieldParameters(depthOfFieldFocusStart, depthOfFieldFocusEnd, isVignetteOn);
			}
			else
			{
				if (this.Mission.Mode == MissionMode.Conversation || this.Mission.Mode == MissionMode.Barter)
				{
					MissionMainAgentController missionMainAgentController6 = this._missionMainAgentController;
					if (((missionMainAgentController6 != null) ? missionMainAgentController6.InteractionComponent.CurrentFocusedObject : null) != null && this._missionMainAgentController.InteractionComponent.CurrentFocusedObject.FocusableObjectType == FocusableObjectType.Agent)
					{
						MissionMainAgentController missionMainAgentController7 = this._missionMainAgentController;
						Agent agent5 = ((missionMainAgentController7 != null) ? missionMainAgentController7.InteractionComponent.CurrentFocusedObject : null) as Agent;
						scene.SetDepthOfFieldParameters(5f, 5f, false);
						Scene scene2 = scene;
						Vec3 vec2 = matrixFrame.origin - agent5.AgentVisuals.GetGlobalStableEyePoint(true);
						scene2.SetDepthOfFieldFocus(vec2.Length);
						goto IL_26EB;
					}
				}
				if (!this._zoomAmount.ApproximatelyEqualsTo(1f, 1E-05f))
				{
					scene.SetDepthOfFieldParameters(0f, 0f, false);
					scene.SetDepthOfFieldFocus(0f);
				}
			}
			IL_26EB:
			this.CombatCamera.SetFovVertical(this.IsPhotoModeEnabled ? (num * 0.017453292f) : (this._cameraSpecialCurrentFOV * (this.CameraViewAngle / 65f) * 0.017453292f), Screen.AspectRatio, newDNear, 12500f);
			this.SceneView.SetCamera(this.CombatCamera);
			Vec3 vec21 = (agentToFollow != null) ? agentToFollow.GetEyeGlobalPosition() : matrixFrame.origin;
			this.Mission.SetCameraFrame(ref matrixFrame, 65f / this.CameraViewAngle, ref vec21);
			if (this.LastFollowedAgent != null && this.LastFollowedAgent != this.Mission.MainAgent && (agentToFollow == this.Mission.MainAgent || agentToFollow == null))
			{
				MissionScreen.OnSpectateAgentDelegate onSpectateAgentFocusOut = this.OnSpectateAgentFocusOut;
				if (onSpectateAgentFocusOut != null)
				{
					onSpectateAgentFocusOut(this.LastFollowedAgent);
				}
			}
			this.LastFollowedAgent = agentToFollow;
			this.LastFollowedAgentVisuals = agentVisualToFollow;
			this._cameraApplySpecialMovementsInstantly = false;
			this._cameraAddSpecialMovement = false;
			this._cameraAddSpecialPositionalMovement = false;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000139A9 File Offset: 0x00011BA9
		public bool IsViewingCharacter()
		{
			return !this.Mission.CameraIsFirstPerson && !this.IsOrderMenuOpen && this.SceneLayer.Input.IsGameKeyDown(25);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000139D4 File Offset: 0x00011BD4
		private void SetCameraFrameToMapView()
		{
			MatrixFrame matrixFrame = MatrixFrame.Identity;
			bool flag = false;
			if (GameNetwork.IsMultiplayer)
			{
				GameEntity gameEntity = this.Mission.Scene.FindEntityWithTag("mp_camera_start_pos");
				if (gameEntity != null)
				{
					matrixFrame = gameEntity.GetGlobalFrame();
					matrixFrame.rotation.Orthonormalize();
					this.CameraBearing = matrixFrame.rotation.f.RotationZ;
					this.CameraElevation = matrixFrame.rotation.f.RotationX - 1.5707964f;
				}
				else
				{
					Debug.FailedAssert("Multiplayer scene does not contain a camera frame", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.View\\Screens\\MissionScreen.cs", "SetCameraFrameToMapView", 2093);
					flag = true;
				}
			}
			else if (this.Mission.Mode == MissionMode.Deployment)
			{
				bool flag2 = this.Mission.PlayerTeam.Side == BattleSideEnum.Attacker;
				GameEntity gameEntity2;
				if (flag2)
				{
					gameEntity2 = (this.Mission.Scene.FindEntityWithTag("strategyCameraAttacker") ?? this.Mission.Scene.FindEntityWithTag("strategyCameraDefender"));
				}
				else
				{
					gameEntity2 = (this.Mission.Scene.FindEntityWithTag("strategyCameraDefender") ?? this.Mission.Scene.FindEntityWithTag("strategyCameraAttacker"));
				}
				if (gameEntity2 != null)
				{
					matrixFrame = gameEntity2.GetGlobalFrame();
					this.CameraBearing = matrixFrame.rotation.f.RotationZ;
					this.CameraElevation = matrixFrame.rotation.f.RotationX - 1.5707964f;
				}
				else if (this.Mission.HasSpawnPath)
				{
					float battleSizeOffset = Mission.GetBattleSizeOffset(100, this.Mission.GetInitialSpawnPath());
					matrixFrame = this.Mission.GetBattleSideInitialSpawnPathFrame(flag2 ? BattleSideEnum.Attacker : BattleSideEnum.Defender, battleSizeOffset).ToGroundMatrixFrame();
					matrixFrame.origin.z = matrixFrame.origin.z + 25f;
					matrixFrame.origin -= 25f * matrixFrame.rotation.f;
					this.CameraBearing = matrixFrame.rotation.f.RotationZ;
					this.CameraElevation = -0.7853982f;
				}
				else
				{
					flag = true;
				}
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				Vec3 vec = new Vec3(float.MaxValue, float.MaxValue, 0f, -1f);
				Vec3 vec2 = new Vec3(float.MinValue, float.MinValue, 0f, -1f);
				if (this.Mission.Boundaries.ContainsKey("walk_area"))
				{
					using (IEnumerator<Vec2> enumerator = this.Mission.Boundaries["walk_area"].GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Vec2 vec3 = enumerator.Current;
							vec.x = MathF.Min(vec.x, vec3.x);
							vec.y = MathF.Min(vec.y, vec3.y);
							vec2.x = MathF.Max(vec2.x, vec3.x);
							vec2.y = MathF.Max(vec2.y, vec3.y);
						}
						goto IL_32A;
					}
				}
				this.Mission.Scene.GetBoundingBox(out vec, out vec2);
				IL_32A:
				Vec3 vec4 = (vec + vec2) * 0.5f;
				matrixFrame.origin = vec4;
				matrixFrame.origin.z = matrixFrame.origin.z + 10000f;
				matrixFrame.origin.z = this.Mission.Scene.GetGroundHeightAtPosition(vec4, BodyFlags.CommonCollisionExcludeFlags) + 10f;
			}
			this.CombatCamera.Frame = matrixFrame;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00013D84 File Offset: 0x00011F84
		private bool HandleUserInputDebug()
		{
			bool result = false;
			if (base.DebugInput.IsHotKeyPressed("MissionScreenHotkeyResetDebugVariables"))
			{
				GameNetwork.ResetDebugVariables();
			}
			if (base.DebugInput.IsHotKeyPressed("FixSkeletons"))
			{
				MBCommon.FixSkeletons();
				MessageManager.DisplayMessage("Skeleton models are reloaded...", 4294901760U);
				result = true;
			}
			return result;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00013DD4 File Offset: 0x00011FD4
		private void HandleUserInput(float dt)
		{
			bool flag = false;
			bool flag2 = this._isGamepadActive && this.PhotoModeRequiresMouse;
			if (this.Mission == null || this.Mission.CurrentState == Mission.State.EndingNextFrame)
			{
				return;
			}
			if (!flag && Game.Current.CheatMode)
			{
				flag = this.HandleUserInputCheatMode(dt);
			}
			if (flag)
			{
				return;
			}
			float num = this.SceneLayer.Input.GetMouseSensitivity();
			if (!this.MouseVisible && this.Mission.MainAgent != null && this.Mission.MainAgent.State == AgentState.Active && this.Mission.MainAgent.IsLookRotationInSlowMotion)
			{
				num *= ManagedParameters.Instance.GetManagedParameter(ManagedParametersEnum.ReducedMouseSensitivityMultiplier);
			}
			float num2 = dt / 0.0009f;
			float num3 = dt / 0.0009f;
			float num4 = 0f;
			float num5 = 0f;
			if ((!MBCommon.IsPaused || this.IsPhotoModeEnabled) && !this.IsRadialMenuActive && this._cameraSpecialTargetFOV > 9f && this.Mission.Mode != MissionMode.Barter)
			{
				if (this.MouseVisible && !this.SceneLayer.Input.IsKeyDown(InputKey.RightMouseButton))
				{
					if (this.Mission.Mode != MissionMode.Conversation)
					{
						if (this.Mission.Mode == MissionMode.Deployment)
						{
							num4 = num2 * this.SceneLayer.Input.GetGameKeyAxis("CameraAxisX");
							num5 = -num3 * this.SceneLayer.Input.GetGameKeyAxis("CameraAxisY");
						}
						else
						{
							if (this.SceneLayer.Input.GetMousePositionRanged().x <= 0.01f)
							{
								num4 = -400f * dt;
							}
							else if (this.SceneLayer.Input.GetMousePositionRanged().x >= 0.99f)
							{
								num4 = 400f * dt;
							}
							if (this.SceneLayer.Input.GetMousePositionRanged().y <= 0.01f)
							{
								num5 = -400f * dt;
							}
							else if (this.SceneLayer.Input.GetMousePositionRanged().y >= 0.99f)
							{
								num5 = 400f * dt;
							}
						}
					}
				}
				else if (!this.SceneLayer.Input.GetIsMouseActive())
				{
					float gameKeyAxis = this.SceneLayer.Input.GetGameKeyAxis("CameraAxisX");
					float gameKeyAxis2 = this.SceneLayer.Input.GetGameKeyAxis("CameraAxisY");
					if (gameKeyAxis > 0.9f || gameKeyAxis < -0.9f)
					{
						num2 = dt / 0.00045f;
					}
					if (gameKeyAxis2 > 0.9f || gameKeyAxis2 < -0.9f)
					{
						num3 = dt / 0.00045f;
					}
					if (this._zoomToggled)
					{
						num2 *= BannerlordConfig.ZoomSensitivityModifier;
						num3 *= BannerlordConfig.ZoomSensitivityModifier;
					}
					num4 = num2 * this.SceneLayer.Input.GetGameKeyAxis("CameraAxisX") + this.SceneLayer.Input.GetMouseMoveX();
					num5 = -num3 * this.SceneLayer.Input.GetGameKeyAxis("CameraAxisY") + this.SceneLayer.Input.GetMouseMoveY();
					if (this._missionMainAgentController.IsPlayerAiming && NativeOptions.GetConfig(NativeOptions.NativeOptionsType.EnableGyroAssistedAim) == 1f)
					{
						float config = NativeOptions.GetConfig(NativeOptions.NativeOptionsType.GyroAimSensitivity);
						float gyroX = Input.GetGyroX();
						Input.GetGyroY();
						float gyroZ = Input.GetGyroZ();
						num4 += config * gyroZ * 12f * -1f;
						num5 += config * gyroX * 12f * -1f;
					}
				}
				else
				{
					num4 = this.SceneLayer.Input.GetMouseMoveX();
					num5 = this.SceneLayer.Input.GetMouseMoveY();
					if (this._zoomAmount > 0.66f)
					{
						num4 *= BannerlordConfig.ZoomSensitivityModifier * this._zoomAmount;
						num5 *= BannerlordConfig.ZoomSensitivityModifier * this._zoomAmount;
					}
				}
			}
			if (NativeConfig.EnableEditMode && base.DebugInput.IsHotKeyPressed("MissionScreenHotkeySwitchCameraSmooth"))
			{
				this._cameraSmoothMode = !this._cameraSmoothMode;
				if (this._cameraSmoothMode)
				{
					MessageManager.DisplayMessage("Camera smooth mode Enabled.", uint.MaxValue);
				}
				else
				{
					MessageManager.DisplayMessage("Camera smooth mode Disabled.", uint.MaxValue);
				}
			}
			float num6 = 0.0035f;
			float num8;
			if (this._cameraSmoothMode)
			{
				num6 *= 0.02f;
				float num7 = 0.02f + dt - 8f * (dt * dt);
				num8 = MathF.Max(0f, 1f - 2f * num7);
			}
			else
			{
				num8 = 0f;
			}
			this._cameraBearingDelta *= num8;
			this._cameraElevationDelta *= num8;
			bool isSessionActive = GameNetwork.IsSessionActive;
			float num9 = num6 * num;
			float num10 = -num4 * num9;
			float num11 = (NativeConfig.InvertMouse ? num5 : (-num5)) * num9;
			if (isSessionActive)
			{
				float num12 = 0.3f + 10f * dt;
				num10 = MBMath.ClampFloat(num10, -num12, num12);
				num11 = MBMath.ClampFloat(num11, -num12, num12);
			}
			this._cameraBearingDelta += num10;
			this._cameraElevationDelta += num11;
			if (isSessionActive)
			{
				float num13 = 0.3f + 10f * dt;
				this._cameraBearingDelta = MBMath.ClampFloat(this._cameraBearingDelta, -num13, num13);
				this._cameraElevationDelta = MBMath.ClampFloat(this._cameraElevationDelta, -num13, num13);
			}
			Agent agentToFollow = this.GetSpectatingData(this.CombatCamera.Frame.origin).AgentToFollow;
			if (this.Mission.CameraIsFirstPerson && agentToFollow != null && agentToFollow.Controller == Agent.ControllerType.Player && agentToFollow.HasMount && ((ManagedOptions.GetConfig(ManagedOptions.ManagedOptionsType.TurnCameraWithHorseInFirstPerson) == 1f && !agentToFollow.WieldedWeapon.IsEmpty && agentToFollow.WieldedWeapon.CurrentUsageItem.IsRangedWeapon) || (ManagedOptions.GetConfig(ManagedOptions.ManagedOptionsType.TurnCameraWithHorseInFirstPerson) == 2f && (agentToFollow.WieldedWeapon.IsEmpty || agentToFollow.WieldedWeapon.CurrentUsageItem.IsMeleeWeapon)) || ManagedOptions.GetConfig(ManagedOptions.ManagedOptionsType.TurnCameraWithHorseInFirstPerson) == 3f))
			{
				this._cameraBearingDelta += agentToFollow.MountAgent.GetTurnSpeed() * dt;
			}
			if (this.InputManager.IsGameKeyDown(28))
			{
				Mission.CameraAddedDistance -= 2.1f * dt;
			}
			if (this.InputManager.IsGameKeyDown(29))
			{
				Mission.CameraAddedDistance += 2.1f * dt;
			}
			Mission.CameraAddedDistance = MBMath.ClampFloat(Mission.CameraAddedDistance, 0.7f, 2.4f);
			this._isGamepadActive = (!Input.IsMouseActive && Input.IsControllerConnected);
			if (this._isGamepadActive)
			{
				Agent mainAgent = this.Mission.MainAgent;
				bool flag3;
				if (mainAgent == null)
				{
					flag3 = false;
				}
				else
				{
					WeaponComponentData currentUsageItem = mainAgent.WieldedWeapon.CurrentUsageItem;
					bool? flag4 = (currentUsageItem != null) ? new bool?(currentUsageItem.IsRangedWeapon) : null;
					bool flag5 = true;
					flag3 = (flag4.GetValueOrDefault() == flag5 & flag4 != null);
				}
				if (!flag3)
				{
					goto IL_6F1;
				}
			}
			bool flag6;
			if (this.CustomCamera == null)
			{
				flag6 = !this.IsRadialMenuActive;
				goto IL_6F2;
			}
			IL_6F1:
			flag6 = false;
			IL_6F2:
			bool flag7 = flag6 || this._forceCanZoom;
			if (flag7)
			{
				float applicationTime = Time.ApplicationTime;
				if (this.SceneLayer.Input.IsHotKeyPressed("ToggleZoom"))
				{
					this._zoomToggleTime = applicationTime;
				}
				if (applicationTime - this._zoomToggleTime > 0.01f && this.SceneLayer.Input.IsHotKeyDown("ToggleZoom"))
				{
					this._zoomToggleTime = float.MaxValue;
					this._zoomToggled = !this._zoomToggled;
				}
			}
			else
			{
				this._zoomToggled = false;
			}
			bool photoModeOrbit = this.Mission.Scene.GetPhotoModeOrbit();
			if (this.IsPhotoModeEnabled)
			{
				if (photoModeOrbit && !flag2)
				{
					this._zoomAmount -= this.SceneLayer.Input.GetDeltaMouseScroll() * 0.002f;
					this._zoomAmount = MBMath.ClampFloat(this._zoomAmount, 0f, 50f);
				}
			}
			else
			{
				if (agentToFollow != null && agentToFollow.IsMine && (this._zoomToggled || (flag7 && this.SceneLayer.Input.IsGameKeyDown(24))))
				{
					this._zoomAmount += 5f * dt;
				}
				else
				{
					this._zoomAmount -= 5f * dt;
				}
				this._zoomAmount = MBMath.ClampFloat(this._zoomAmount, 0f, 1f);
			}
			if (!this.IsPhotoModeEnabled)
			{
				if (this._zoomAmount.ApproximatelyEqualsTo(1f, 1E-05f))
				{
					this.Mission.Scene.SetDepthOfFieldParameters(this._zoomAmount * 160f * 110f, this._zoomAmount * 1500f * 0.3f, false);
				}
				else
				{
					this.Mission.Scene.SetDepthOfFieldParameters(0f, 0f, false);
				}
			}
			float depthOfFieldFocus;
			this.Mission.Scene.RayCastForClosestEntityOrTerrain(this.CombatCamera.Position + this.CombatCamera.Direction * this._cameraRayCastOffset, this.CombatCamera.Position + this.CombatCamera.Direction * 3000f, out depthOfFieldFocus, 0.01f, BodyFlags.CommonFocusRayCastExcludeFlags);
			this.Mission.Scene.SetDepthOfFieldFocus(depthOfFieldFocus);
			Agent mainAgent2 = this.Mission.MainAgent;
			if (mainAgent2 != null && !this.IsPhotoModeEnabled)
			{
				if (this._isPlayerAgentAdded)
				{
					this._isPlayerAgentAdded = false;
					if (this.Mission.Mode != MissionMode.Deployment)
					{
						this.CameraBearing = (this.Mission.CameraIsFirstPerson ? mainAgent2.LookDirection.RotationZ : mainAgent2.MovementDirectionAsAngle);
						this.CameraElevation = (this.Mission.CameraIsFirstPerson ? mainAgent2.LookDirection.RotationX : 0f);
						this._cameraSpecialTargetAddedBearing = 0f;
						this._cameraSpecialTargetAddedElevation = 0f;
						this._cameraSpecialCurrentAddedBearing = 0f;
						this._cameraSpecialCurrentAddedElevation = 0f;
					}
				}
				if (this.Mission.ClearSceneTimerElapsedTime >= 0f)
				{
					bool flag8;
					if (!this.IsViewingCharacter() && this.Mission.Mode != MissionMode.Conversation && this.Mission.Mode != MissionMode.Barter && !mainAgent2.IsLookDirectionLocked)
					{
						MissionMainAgentController missionMainAgentController = this._missionMainAgentController;
						flag8 = (((missionMainAgentController != null) ? missionMainAgentController.LockedAgent : null) == null);
					}
					else
					{
						flag8 = false;
					}
					if (!flag8)
					{
						if (this.Mission.Mode != MissionMode.Barter)
						{
							if (this._missionMainAgentController.LockedAgent != null)
							{
								this.CameraBearing = mainAgent2.LookDirection.RotationZ;
								this.CameraElevation = mainAgent2.LookDirection.RotationX;
							}
							else
							{
								this._cameraSpecialTargetAddedBearing = MBMath.WrapAngle(this._cameraSpecialTargetAddedBearing + this._cameraBearingDelta);
								this._cameraSpecialTargetAddedElevation = MBMath.WrapAngle(this._cameraSpecialTargetAddedElevation + this._cameraElevationDelta);
								this._cameraSpecialCurrentAddedBearing = MBMath.WrapAngle(this._cameraSpecialCurrentAddedBearing + this._cameraBearingDelta);
								this._cameraSpecialCurrentAddedElevation = MBMath.WrapAngle(this._cameraSpecialCurrentAddedElevation + this._cameraElevationDelta);
							}
						}
						float num14 = this.CameraElevation + this._cameraSpecialTargetAddedElevation;
						num14 = MBMath.ClampFloat(num14, -1.3659099f, 1.1219975f);
						this._cameraSpecialTargetAddedElevation = num14 - this.CameraElevation;
						num14 = this.CameraElevation + this._cameraSpecialCurrentAddedElevation;
						num14 = MBMath.ClampFloat(num14, -1.3659099f, 1.1219975f);
						this._cameraSpecialCurrentAddedElevation = num14 - this.CameraElevation;
					}
					else
					{
						this._cameraSpecialTargetAddedBearing = 0f;
						this._cameraSpecialTargetAddedElevation = 0f;
						if (this.Mission.CameraIsFirstPerson && agentToFollow != null && agentToFollow == this.Mission.MainAgent && !this.IsPhotoModeEnabled && !agentToFollow.GetCurrentAnimationFlag(0).HasAnyFlag(AnimFlags.anf_lock_camera) && !agentToFollow.GetCurrentAnimationFlag(1).HasAnyFlag(AnimFlags.anf_lock_camera))
						{
							if (this.Mission.Mode == MissionMode.Conversation || this.Mission.Mode == MissionMode.Barter)
							{
								MissionMainAgentController missionMainAgentController2 = this._missionMainAgentController;
								if (((missionMainAgentController2 != null) ? missionMainAgentController2.InteractionComponent.CurrentFocusedObject : null) != null && this._missionMainAgentController.InteractionComponent.CurrentFocusedObject.FocusableObjectType == FocusableObjectType.Agent)
								{
									goto IL_D60;
								}
							}
							if (this._missionMainAgentController == null || !this._missionMainAgentController.CustomLookDir.IsNonZero)
							{
								float num15 = MBMath.WrapAngle(this.CameraBearing + this._cameraBearingDelta);
								float num16 = MBMath.WrapAngle(this.CameraElevation + this._cameraElevationDelta);
								float num17;
								float num18;
								this.CalculateNewBearingAndElevationForFirstPerson(agentToFollow, num15, num16, out num17, out num18);
								if (num17 != num15)
								{
									this._cameraBearingDelta = (MBMath.IsBetween(MBMath.WrapAngle(this._cameraBearingDelta), 0f, 3.1415927f) ? MBMath.ClampFloat(MBMath.WrapAngle(num17 - this.CameraBearing), 0f, this._cameraBearingDelta) : MBMath.ClampFloat(MBMath.WrapAngle(num17 - this.CameraBearing), this._cameraBearingDelta, 0f));
								}
								if (num18 != num16)
								{
									this._cameraElevationDelta = (MBMath.IsBetween(MBMath.WrapAngle(this._cameraElevationDelta), 0f, 3.1415927f) ? MBMath.ClampFloat(MBMath.WrapAngle(num18 - this.CameraElevation), 0f, this._cameraElevationDelta) : MBMath.ClampFloat(MBMath.WrapAngle(num18 - this.CameraElevation), this._cameraElevationDelta, 0f));
								}
							}
						}
						IL_D60:
						this.CameraBearing += this._cameraBearingDelta;
						this.CameraElevation += this._cameraElevationDelta;
						this.CameraElevation = MBMath.ClampFloat(this.CameraElevation, -1.3659099f, 1.1219975f);
					}
					if (this.LockCameraMovement)
					{
						this._cameraToggleStartTime = float.MaxValue;
						return;
					}
					if (!Input.IsMouseActive)
					{
						float applicationTime2 = Time.ApplicationTime;
						if (this.SceneLayer.Input.IsGameKeyPressed(27))
						{
							if (this.SceneLayer.Input.GetGameKeyAxis("MovementAxisX") <= 0.1f && this.SceneLayer.Input.GetGameKeyAxis("MovementAxisY") <= 0.1f)
							{
								this._cameraToggleStartTime = applicationTime2;
							}
						}
						else if (!this.SceneLayer.Input.IsGameKeyDown(27))
						{
							this._cameraToggleStartTime = float.MaxValue;
						}
						if (this.GetCameraToggleProgress() >= 1f)
						{
							this._cameraToggleStartTime = float.MaxValue;
							this.Mission.CameraIsFirstPerson = !this.Mission.CameraIsFirstPerson;
							this._cameraApplySpecialMovementsInstantly = true;
							return;
						}
					}
					else if (this.SceneLayer.Input.IsGameKeyPressed(27))
					{
						this.Mission.CameraIsFirstPerson = !this.Mission.CameraIsFirstPerson;
						this._cameraApplySpecialMovementsInstantly = true;
						return;
					}
				}
			}
			else
			{
				if (this.IsPhotoModeEnabled && this.Mission.CameraIsFirstPerson)
				{
					this.Mission.CameraIsFirstPerson = false;
				}
				this.CameraBearing += this._cameraBearingDelta;
				this.CameraElevation += this._cameraElevationDelta;
				this.CameraElevation = MBMath.ClampFloat(this.CameraElevation, -1.3659099f, 1.1219975f);
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00014CF2 File Offset: 0x00012EF2
		public float GetCameraToggleProgress()
		{
			if (this._cameraToggleStartTime != 3.4028235E+38f && this.SceneLayer.Input.IsGameKeyDown(27))
			{
				return (Time.ApplicationTime - this._cameraToggleStartTime) / 0.5f;
			}
			return 0f;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00014D30 File Offset: 0x00012F30
		private bool HandleUserInputCheatMode(float dt)
		{
			bool result = false;
			if (!GameNetwork.IsMultiplayer)
			{
				if (this.InputManager.IsHotKeyPressed("EnterSlowMotion"))
				{
					float num;
					if (this.Mission.GetRequestedTimeSpeed(1121, out num))
					{
						this.Mission.RemoveTimeSpeedRequest(1121);
					}
					else
					{
						this.Mission.AddTimeSpeedRequest(new Mission.TimeSpeedRequest(0.1f, 1121));
					}
					result = true;
				}
				float num2;
				if (this.Mission.GetRequestedTimeSpeed(1121, out num2))
				{
					if (this.InputManager.IsHotKeyDown("MissionScreenHotkeyIncreaseSlowMotionFactor"))
					{
						this.Mission.RemoveTimeSpeedRequest(1121);
						num2 = MBMath.ClampFloat(num2 + 0.5f * dt, 0f, 1f);
						this.Mission.AddTimeSpeedRequest(new Mission.TimeSpeedRequest(num2, 1121));
					}
					if (this.InputManager.IsHotKeyDown("MissionScreenHotkeyDecreaseSlowMotionFactor"))
					{
						this.Mission.RemoveTimeSpeedRequest(1121);
						num2 = MBMath.ClampFloat(num2 - 0.5f * dt, 0f, 1f);
						this.Mission.AddTimeSpeedRequest(new Mission.TimeSpeedRequest(num2, 1121));
					}
				}
				if (this.InputManager.IsHotKeyPressed("Pause"))
				{
					this._missionState.Paused = !this._missionState.Paused;
					result = true;
				}
				if (this.InputManager.IsHotKeyPressed("MissionScreenHotkeyHealYourSelf") && this.Mission.MainAgent != null)
				{
					this.Mission.MainAgent.Health = this.Mission.MainAgent.HealthLimit;
					result = true;
				}
				if (this.InputManager.IsHotKeyPressed("MissionScreenHotkeyHealYourHorse"))
				{
					Agent mainAgent = this.Mission.MainAgent;
					if (((mainAgent != null) ? mainAgent.MountAgent : null) != null)
					{
						this.Mission.MainAgent.MountAgent.Health = this.Mission.MainAgent.MountAgent.HealthLimit;
						result = true;
					}
				}
				if (!this.InputManager.IsShiftDown())
				{
					if (!this.InputManager.IsAltDown())
					{
						if (this.InputManager.IsHotKeyPressed("MissionScreenHotkeyKillEnemyAgent"))
						{
							return Mission.Current.KillCheats(false, true, false, false);
						}
					}
					else if (this.InputManager.IsHotKeyPressed("MissionScreenHotkeyKillAllEnemyAgents"))
					{
						return Mission.Current.KillCheats(true, true, false, false);
					}
				}
				else if (!this.InputManager.IsAltDown())
				{
					if (this.InputManager.IsHotKeyPressed("MissionScreenHotkeyKillEnemyHorse"))
					{
						return Mission.Current.KillCheats(false, true, true, false);
					}
				}
				else if (this.InputManager.IsHotKeyPressed("MissionScreenHotkeyKillAllEnemyHorses"))
				{
					return Mission.Current.KillCheats(true, true, true, false);
				}
				if (!this.InputManager.IsShiftDown())
				{
					if (!this.InputManager.IsAltDown())
					{
						if (this.InputManager.IsHotKeyPressed("MissionScreenHotkeyKillFriendlyAgent"))
						{
							return Mission.Current.KillCheats(false, false, false, false);
						}
					}
					else if (this.InputManager.IsHotKeyPressed("MissionScreenHotkeyKillAllFriendlyAgents"))
					{
						return Mission.Current.KillCheats(true, false, false, false);
					}
				}
				else if (!this.InputManager.IsAltDown())
				{
					if (this.InputManager.IsHotKeyPressed("MissionScreenHotkeyKillFriendlyHorse"))
					{
						return Mission.Current.KillCheats(false, false, true, false);
					}
				}
				else if (this.InputManager.IsHotKeyPressed("MissionScreenHotkeyKillAllFriendlyHorses"))
				{
					return Mission.Current.KillCheats(true, false, true, false);
				}
				if (!this.InputManager.IsShiftDown())
				{
					if (this.InputManager.IsHotKeyPressed("MissionScreenHotkeyKillYourSelf"))
					{
						return Mission.Current.KillCheats(false, false, false, true);
					}
				}
				else if (this.InputManager.IsHotKeyPressed("MissionScreenHotkeyKillYourHorse"))
				{
					return Mission.Current.KillCheats(false, false, true, true);
				}
				if ((GameNetwork.IsServerOrRecorder || !GameNetwork.IsMultiplayer) && this.InputManager.IsHotKeyPressed("MissionScreenHotkeyGhostCam"))
				{
					this.IsCheatGhostMode = !this.IsCheatGhostMode;
				}
			}
			if (!GameNetwork.IsSessionActive)
			{
				if (this.InputManager.IsHotKeyPressed("MissionScreenHotkeySwitchAgentToAi"))
				{
					Debug.Print("Cheat: SwitchAgentToAi", 0, Debug.DebugColor.White, 17592186044416UL);
					if (this.Mission.MainAgent != null && this.Mission.MainAgent.IsActive())
					{
						this.Mission.MainAgent.Controller = ((this.Mission.MainAgent.Controller == Agent.ControllerType.Player) ? Agent.ControllerType.AI : Agent.ControllerType.Player);
						result = true;
					}
				}
				if (this.InputManager.IsHotKeyPressed("MissionScreenHotkeyControlFollowedAgent"))
				{
					Debug.Print("Cheat: ControlFollowedAgent", 0, Debug.DebugColor.White, 17592186044416UL);
					if (this.Mission.MainAgent != null)
					{
						if (this.Mission.MainAgent.Controller == Agent.ControllerType.Player)
						{
							this.Mission.MainAgent.Controller = Agent.ControllerType.AI;
							if (this.LastFollowedAgent != null)
							{
								this.LastFollowedAgent.Controller = Agent.ControllerType.Player;
							}
						}
						else
						{
							foreach (Agent agent in this.Mission.Agents)
							{
								if (agent.Controller == Agent.ControllerType.Player)
								{
									agent.Controller = Agent.ControllerType.AI;
								}
							}
							this.Mission.MainAgent.Controller = Agent.ControllerType.Player;
						}
						result = true;
					}
					else
					{
						if (this.LastFollowedAgent != null)
						{
							this.LastFollowedAgent.Controller = Agent.ControllerType.Player;
						}
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00015264 File Offset: 0x00013464
		public void AddMissionView(MissionView missionView)
		{
			this.Mission.AddMissionBehavior(missionView);
			this.RegisterView(missionView);
			missionView.OnMissionScreenInitialize();
			Debug.ReportMemoryBookmark("MissionView Initialized: " + missionView.GetType().Name);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0001529C File Offset: 0x0001349C
		public void ScreenPointToWorldRay(Vec2 screenPoint, out Vec3 rayBegin, out Vec3 rayEnd)
		{
			rayBegin = Vec3.Invalid;
			rayEnd = Vec3.Invalid;
			Vec2 viewportPoint = this.SceneView.ScreenPointToViewportPoint(screenPoint);
			this.CombatCamera.ViewportPointToWorldRay(ref rayBegin, ref rayEnd, viewportPoint);
			float num = -1f;
			foreach (KeyValuePair<string, ICollection<Vec2>> keyValuePair in this.Mission.Boundaries)
			{
				float boundaryRadius = this.Mission.Boundaries.GetBoundaryRadius(keyValuePair.Key);
				if (num < boundaryRadius)
				{
					num = boundaryRadius;
				}
			}
			if (num < 0f)
			{
				num = 30f;
			}
			Vec3 v = rayEnd - rayBegin;
			float a = v.Normalize();
			rayEnd = rayBegin + v * MathF.Min(a, num);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0001538C File Offset: 0x0001358C
		public bool GetProjectedMousePositionOnGround(out Vec3 groundPosition, out Vec3 groundNormal, BodyFlags excludeBodyOwnerFlags, bool checkOccludedSurface)
		{
			return this.SceneView.ProjectedMousePositionOnGround(out groundPosition, out groundNormal, this.MouseVisible, excludeBodyOwnerFlags, checkOccludedSurface);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000153A4 File Offset: 0x000135A4
		public void CancelQuickPositionOrder()
		{
			if (this.OrderFlag != null)
			{
				this.OrderFlag.IsVisible = false;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000153BA File Offset: 0x000135BA
		public bool MissionStartedRendering()
		{
			return this.SceneView != null && this.SceneView.ReadyToRender();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000153D7 File Offset: 0x000135D7
		public Vec3 GetOrderFlagPosition()
		{
			if (this.OrderFlag != null)
			{
				return this.OrderFlag.Position;
			}
			return Vec3.Invalid;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000153F2 File Offset: 0x000135F2
		public MatrixFrame GetOrderFlagFrame()
		{
			return this.OrderFlag.Frame;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00015400 File Offset: 0x00013600
		private void ActivateLoadingScreen()
		{
			if (this.SceneLayer != null && this.SceneLayer.SceneView != null)
			{
				Scene scene = this.SceneLayer.SceneView.GetScene();
				if (scene != null)
				{
					scene.PreloadForRendering();
				}
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00015448 File Offset: 0x00013648
		public void SetRadialMenuActiveState(bool isActive)
		{
			this.IsRadialMenuActive = isActive;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00015451 File Offset: 0x00013651
		public void SetPhotoModeRequiresMouse(bool isRequired)
		{
			this.PhotoModeRequiresMouse = isRequired;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0001545C File Offset: 0x0001365C
		public void SetPhotoModeEnabled(bool isEnabled)
		{
			if (this.IsPhotoModeEnabled != isEnabled && !GameNetwork.IsMultiplayer)
			{
				this.IsPhotoModeEnabled = isEnabled;
				if (isEnabled)
				{
					MBCommon.PauseGameEngine();
					using (List<MissionView>.Enumerator enumerator = this._missionViews.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							MissionView missionView = enumerator.Current;
							missionView.OnPhotoModeActivated();
						}
						goto IL_90;
					}
				}
				MBCommon.UnPauseGameEngine();
				foreach (MissionView missionView2 in this._missionViews)
				{
					missionView2.OnPhotoModeDeactivated();
				}
				IL_90:
				this.Mission.Scene.SetPhotoModeOn(this.IsPhotoModeEnabled);
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0001552C File Offset: 0x0001372C
		public void SetConversationActive(bool isActive)
		{
			if (this.IsConversationActive != isActive && !GameNetwork.IsMultiplayer)
			{
				this.IsConversationActive = isActive;
				foreach (MissionView missionView in this._missionViews)
				{
					if (isActive)
					{
						missionView.OnConversationBegin();
					}
					else
					{
						missionView.OnConversationEnd();
					}
				}
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000155A0 File Offset: 0x000137A0
		public void SetCameraLockState(bool isLocked)
		{
			this.LockCameraMovement = isLocked;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000155A9 File Offset: 0x000137A9
		public void RegisterView(MissionView missionView)
		{
			this._missionViews.Add(missionView);
			missionView.MissionScreen = this;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000155BE File Offset: 0x000137BE
		public void UnregisterView(MissionView missionView)
		{
			this._missionViews.Remove(missionView);
			missionView.MissionScreen = null;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000155D4 File Offset: 0x000137D4
		public IAgentVisual GetPlayerAgentVisuals(MissionPeer lobbyPeer)
		{
			return lobbyPeer.GetAgentVisualForPeer(0);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000155DD File Offset: 0x000137DD
		public void SetAgentToFollow(Agent agent)
		{
			this._agentToFollowOverride = agent;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000155E8 File Offset: 0x000137E8
		public Mission.SpectatorData GetSpectatingData(Vec3 currentCameraPosition)
		{
			Agent agentToFollow = null;
			IAgentVisual agentVisualToFollow = null;
			SpectatorCameraTypes spectatorCameraTypes = SpectatorCameraTypes.Invalid;
			bool flag = this.Mission.MainAgent != null && this.Mission.MainAgent.IsCameraAttachable() && this.Mission.Mode != MissionMode.Deployment;
			bool flag2 = flag || (this.LastFollowedAgent != null && this.LastFollowedAgent.Controller == Agent.ControllerType.Player && this.LastFollowedAgent.IsCameraAttachable());
			MissionPeer missionPeer = (GameNetwork.MyPeer != null) ? GameNetwork.MyPeer.GetComponent<MissionPeer>() : null;
			bool flag3 = missionPeer != null && missionPeer.HasSpawnedAgentVisuals;
			bool flag4 = (this._missionLobbyComponent != null && (this._missionLobbyComponent.MissionType == MultiplayerGameType.Siege || this._missionLobbyComponent.MissionType == MultiplayerGameType.TeamDeathmatch)) || this.Mission.Mode == MissionMode.Deployment;
			SpectatorCameraTypes spectatorCameraTypes2;
			if (!this.IsCheatGhostMode && !flag2 && flag4 && this._agentToFollowOverride != null && this._agentToFollowOverride.IsCameraAttachable() && !flag3)
			{
				agentToFollow = this._agentToFollowOverride;
				spectatorCameraTypes2 = SpectatorCameraTypes.LockToAnyAgent;
			}
			else
			{
				if (this._missionCameraModeLogic != null)
				{
					spectatorCameraTypes = this._missionCameraModeLogic.GetMissionCameraLockMode(flag2);
				}
				if (this.IsCheatGhostMode)
				{
					spectatorCameraTypes2 = SpectatorCameraTypes.Free;
				}
				else if (spectatorCameraTypes != SpectatorCameraTypes.Invalid)
				{
					spectatorCameraTypes2 = spectatorCameraTypes;
				}
				else if (this.Mission.Mode == MissionMode.Deployment)
				{
					spectatorCameraTypes2 = SpectatorCameraTypes.Free;
				}
				else if (flag)
				{
					spectatorCameraTypes2 = SpectatorCameraTypes.LockToMainPlayer;
					agentToFollow = this.Mission.MainAgent;
				}
				else if (flag2)
				{
					spectatorCameraTypes2 = SpectatorCameraTypes.LockToMainPlayer;
					agentToFollow = this.LastFollowedAgent;
				}
				else if (missionPeer != null && this.GetPlayerAgentVisuals(missionPeer) != null && spectatorCameraTypes != SpectatorCameraTypes.Free)
				{
					spectatorCameraTypes2 = SpectatorCameraTypes.LockToPosition;
					agentVisualToFollow = this.GetPlayerAgentVisuals(missionPeer);
				}
				else if (!GameNetwork.IsMultiplayer)
				{
					spectatorCameraTypes2 = SpectatorCameraTypes.Free;
				}
				else
				{
					spectatorCameraTypes2 = (SpectatorCameraTypes)MultiplayerOptions.OptionType.SpectatorCamera.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
				}
				if ((spectatorCameraTypes2 != SpectatorCameraTypes.LockToMainPlayer && spectatorCameraTypes2 != SpectatorCameraTypes.LockToPosition && this.Mission.Mode != MissionMode.Deployment) || (this.IsCheatGhostMode && !this.IsOrderMenuOpen && !this.IsTransferMenuOpen))
				{
					if (this.LastFollowedAgent != null && this.LastFollowedAgent.IsCameraAttachable())
					{
						agentToFollow = this.LastFollowedAgent;
					}
					else if (spectatorCameraTypes2 != SpectatorCameraTypes.Free || (this._gatherCustomAgentListToSpectate != null && this.LastFollowedAgent != null))
					{
						agentToFollow = this.FindNextCameraAttachableAgent(this.LastFollowedAgent, spectatorCameraTypes2, 1, currentCameraPosition);
					}
					bool flag5 = Game.Current.CheatMode && this.InputManager.IsControlDown();
					if (this.InputManager.IsGameKeyReleased(10) || this.InputManager.IsGameKeyReleased(11))
					{
						if (!flag5)
						{
							agentToFollow = this.FindNextCameraAttachableAgent(this.LastFollowedAgent, spectatorCameraTypes2, -1, currentCameraPosition);
						}
					}
					else if ((this.InputManager.IsGameKeyReleased(9) || this.InputManager.IsGameKeyReleased(12)) && !this._rightButtonDraggingMode)
					{
						if (!flag5)
						{
							agentToFollow = this.FindNextCameraAttachableAgent(this.LastFollowedAgent, spectatorCameraTypes2, 1, currentCameraPosition);
						}
					}
					else if ((this.InputManager.IsGameKeyDown(0) || this.InputManager.IsGameKeyDown(1) || this.InputManager.IsGameKeyDown(2) || this.InputManager.IsGameKeyDown(3) || (this.InputManager.GetIsControllerConnected() && (Input.GetKeyState(InputKey.ControllerLStick).y != 0f || Input.GetKeyState(InputKey.ControllerLStick).x != 0f))) && spectatorCameraTypes2 == SpectatorCameraTypes.Free)
					{
						agentToFollow = null;
						agentVisualToFollow = null;
					}
				}
			}
			return new Mission.SpectatorData(agentToFollow, agentVisualToFollow, spectatorCameraTypes2);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00015930 File Offset: 0x00013B30
		private Agent FindNextCameraAttachableAgent(Agent currentAgent, SpectatorCameraTypes cameraLockMode, int iterationDirection, Vec3 currentCameraPosition)
		{
			if (this.Mission.AllAgents == null || this.Mission.AllAgents.Count == 0)
			{
				return null;
			}
			if (MBDebug.IsErrorReportModeActive())
			{
				return null;
			}
			MissionPeer missionPeer = GameNetwork.IsMyPeerReady ? GameNetwork.MyPeer.GetComponent<MissionPeer>() : null;
			List<Agent> list;
			if (this._gatherCustomAgentListToSpectate != null)
			{
				list = this._gatherCustomAgentListToSpectate(currentAgent);
			}
			else
			{
				switch (cameraLockMode)
				{
				case SpectatorCameraTypes.LockToAnyAgent:
					list = (from x in this.Mission.AllAgents
					where x.IsCameraAttachable() || x == currentAgent
					select x).ToList<Agent>();
					break;
				case SpectatorCameraTypes.LockToAnyPlayer:
					list = (from x in this.Mission.AllAgents
					where (x.MissionPeer != null && x.IsCameraAttachable()) || x == currentAgent
					select x).ToList<Agent>();
					break;
				case SpectatorCameraTypes.LockToPlayerFormation:
					list = this.Mission.AllAgents.Where(delegate(Agent x)
					{
						if (x.Formation != null)
						{
							Formation formation = x.Formation;
							MissionPeer missionPeer = missionPeer;
							if (formation == ((missionPeer != null) ? missionPeer.ControlledFormation : null) && x.IsCameraAttachable())
							{
								return true;
							}
						}
						return x == currentAgent;
					}).ToList<Agent>();
					break;
				case SpectatorCameraTypes.LockToTeamMembers:
				case SpectatorCameraTypes.LockToTeamMembersView:
					list = (from x in this.Mission.AllAgents
					where (x.Team == this.Mission.PlayerTeam && x.MissionPeer != null && x.IsCameraAttachable()) || x == currentAgent
					select x).ToList<Agent>();
					break;
				default:
					list = (from x in this.Mission.AllAgents
					where x.IsCameraAttachable() || x == currentAgent
					select x).ToList<Agent>();
					break;
				}
			}
			Agent result;
			if (list.Count - ((currentAgent != null && !currentAgent.IsCameraAttachable()) ? 1 : 0) == 0)
			{
				result = null;
			}
			else if (currentAgent == null)
			{
				Agent agent = null;
				float num = float.MaxValue;
				foreach (Agent agent2 in list)
				{
					float lengthSquared = (currentCameraPosition - agent2.Position).LengthSquared;
					if (num > lengthSquared)
					{
						num = lengthSquared;
						agent = agent2;
					}
				}
				result = agent;
			}
			else
			{
				int num2 = list.IndexOf(currentAgent);
				if (iterationDirection == 1)
				{
					result = list[(num2 + 1) % list.Count];
				}
				else
				{
					result = ((num2 < 0) ? list[list.Count - 1] : list[(num2 + list.Count - 1) % list.Count]);
				}
			}
			return result;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00015B80 File Offset: 0x00013D80
		void IGameStateListener.OnInitialize()
		{
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00015B82 File Offset: 0x00013D82
		void IGameStateListener.OnFinalize()
		{
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00015B84 File Offset: 0x00013D84
		void IGameStateListener.OnActivate()
		{
			if (this._isDeactivated)
			{
				this.ActivateMissionView();
			}
			this._isDeactivated = false;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00015B9C File Offset: 0x00013D9C
		void IGameStateListener.OnDeactivate()
		{
			this._isDeactivated = true;
			Mission mission = this.Mission;
			if (((mission != null) ? mission.MissionBehaviors : null) != null)
			{
				foreach (MissionView missionView in this._missionViews)
				{
					missionView.OnMissionScreenDeactivate();
				}
			}
			this.OnDeactivate();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00015C10 File Offset: 0x00013E10
		void IMissionSystemHandler.OnMissionAfterStarting(Mission mission)
		{
			this.Mission = mission;
			this.Mission.AddListener(this);
			using (List<MissionBehavior>.Enumerator enumerator = this.Mission.MissionBehaviors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MissionView missionView;
					if ((missionView = (enumerator.Current as MissionView)) != null)
					{
						this.RegisterView(missionView);
					}
				}
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00015C84 File Offset: 0x00013E84
		void IMissionSystemHandler.OnMissionLoadingFinished(Mission mission)
		{
			this.Mission = mission;
			this.InitializeMissionView();
			this.ActivateMissionView();
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00015C9C File Offset: 0x00013E9C
		void IMissionSystemHandler.BeforeMissionTick(Mission mission, float realDt)
		{
			if (MBEditor.EditModeEnabled)
			{
				if (base.DebugInput.IsHotKeyReleased("EnterEditMode") && mission == null)
				{
					if (MBEditor.IsEditModeOn)
					{
						MBEditor.LeaveEditMode();
						this._tickEditor = false;
					}
					else
					{
						MBEditor.EnterEditMode(this.SceneView, this.CombatCamera.Frame, this.CameraElevation, this.CameraBearing);
						this._tickEditor = true;
					}
				}
				if (this._tickEditor && MBEditor.IsEditModeOn)
				{
					MBEditor.TickEditMode(realDt);
					return;
				}
			}
			if (mission == null || mission.Scene == null)
			{
				return;
			}
			mission.Scene.SetOwnerThread();
			mission.Scene.SetDynamicShadowmapCascadesRadiusMultiplier(1f);
			if (MBEditor.EditModeEnabled)
			{
				MBCommon.CheckResourceModifications();
			}
			this.HandleUserInput(realDt);
			if (!this._isRenderingStarted && this.MissionStartedRendering())
			{
				Mission.Current.OnRenderingStarted();
				this._isRenderingStarted = true;
			}
			if (this._isRenderingStarted && this._loadingScreenFramesLeft >= 0 && !this._onSceneRenderingStartedCalled)
			{
				if (this._loadingScreenFramesLeft > 0)
				{
					this._loadingScreenFramesLeft--;
					Mission mission2 = Mission.Current;
					Utilities.SetLoadingScreenPercentage((mission2 != null && mission2.HasMissionBehavior<DeploymentMissionController>()) ? ((this._loadingScreenFramesLeft == 0) ? 1f : (0.92f - (float)this._loadingScreenFramesLeft * 0.005f)) : (1f - (float)this._loadingScreenFramesLeft * 0.02f));
				}
				bool flag = this.AreViewsReady();
				if (this._loadingScreenFramesLeft <= 0 && flag && !MBAnimation.IsAnyAnimationLoadingFromDisk())
				{
					this.OnSceneRenderingStarted();
					this._onSceneRenderingStartedCalled = true;
				}
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00015E30 File Offset: 0x00014030
		private bool AreViewsReady()
		{
			bool flag = true;
			foreach (MissionView missionView in this._missionViews)
			{
				bool flag2 = missionView.IsReady();
				flag = (flag && flag2);
			}
			return flag;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00015E88 File Offset: 0x00014088
		private void CameraTick(Mission mission, float realDt)
		{
			if (mission.CurrentState == Mission.State.Continuing)
			{
				this.CheckForUpdateCamera(realDt);
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00015E9A File Offset: 0x0001409A
		void IMissionSystemHandler.UpdateCamera(Mission mission, float realDt)
		{
			this.CameraTick(mission, realDt);
			if (mission.CurrentState == Mission.State.Continuing && !mission.MissionEnded)
			{
				MBWindowManager.PreDisplay();
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00015EBC File Offset: 0x000140BC
		void IMissionSystemHandler.AfterMissionTick(Mission mission, float realDt)
		{
			if ((mission.CurrentState == Mission.State.Continuing || (mission.MissionEnded && mission.CurrentState != Mission.State.Over)) && Game.Current.CheatMode && this.IsCheatGhostMode && Agent.Main != null && this.InputManager.IsHotKeyPressed("MissionScreenHotkeyTeleportMainAgent"))
			{
				MatrixFrame lastFinalRenderCameraFrame = this.Mission.Scene.LastFinalRenderCameraFrame;
				float f;
				if (this.Mission.Scene.RayCastForClosestEntityOrTerrain(lastFinalRenderCameraFrame.origin, lastFinalRenderCameraFrame.origin + -lastFinalRenderCameraFrame.rotation.u * 100f, out f, 0.01f, BodyFlags.CommonCollisionExcludeFlags))
				{
					Vec3 origin = lastFinalRenderCameraFrame.origin + -lastFinalRenderCameraFrame.rotation.u * f;
					Vec2 vec = -lastFinalRenderCameraFrame.rotation.u.AsVec2;
					vec.Normalize();
					MatrixFrame matrixFrame = default(MatrixFrame);
					matrixFrame.origin = origin;
					matrixFrame.rotation.f = new Vec3(vec.x, vec.y, 0f, -1f);
					matrixFrame.rotation.u = new Vec3(0f, 0f, 1f, -1f);
					matrixFrame.rotation.Orthonormalize();
					Agent.Main.TeleportToPosition(matrixFrame.origin);
				}
			}
			if (this.SceneLayer.Input.IsGameKeyPressed(4) && !base.DebugInput.IsAltDown() && MBEditor.EditModeEnabled && MBEditor.IsEditModeOn)
			{
				MBEditor.LeaveEditMissionMode();
			}
			if (mission.Scene == null)
			{
				MBDebug.Print("Mission is null on MissionScreen::OnFrameTick second phase", 0, Debug.DebugColor.White, 17592186044416UL);
				return;
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00016091 File Offset: 0x00014291
		IEnumerable<MissionBehavior> IMissionSystemHandler.OnAddBehaviors(IEnumerable<MissionBehavior> behaviors, Mission mission, string missionName, bool addDefaultMissionBehaviors)
		{
			if (addDefaultMissionBehaviors)
			{
				behaviors = this.AddDefaultMissionBehaviorsTo(mission, behaviors);
			}
			behaviors = ViewCreatorManager.CollectMissionBehaviors(missionName, mission, behaviors);
			return behaviors;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000160AC File Offset: 0x000142AC
		private void HandleInputs()
		{
			if (!MBEditor.IsEditorMissionOn() && this.MissionStartedRendering() && this.SceneLayer.Input.IsHotKeyReleased("ToggleEscapeMenu") && !LoadingWindow.IsLoadingWindowActive)
			{
				this.OnEscape();
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000160E4 File Offset: 0x000142E4
		public void OnEscape()
		{
			if (this.IsMissionTickable)
			{
				foreach (MissionBehavior missionBehavior in (from v in this.Mission.MissionBehaviors
				where v is MissionView
				orderby ((MissionView)v).ViewOrderPriority
				select v).ToList<MissionBehavior>())
				{
					MissionView missionView = missionBehavior as MissionView;
					if (!this.IsMissionTickable)
					{
						break;
					}
					if (missionView.OnEscape())
					{
						break;
					}
				}
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000161A4 File Offset: 0x000143A4
		bool IMissionSystemHandler.RenderIsReady()
		{
			return this.MissionStartedRendering();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000161AC File Offset: 0x000143AC
		void IMissionListener.OnEndMission()
		{
			this._agentToFollowOverride = null;
			this.LastFollowedAgent = null;
			this.LastFollowedAgentVisuals = null;
			foreach (MissionView missionView in this._missionViews.ToArray())
			{
				missionView.OnMissionScreenFinalize();
				this.UnregisterView(missionView);
			}
			CraftedDataViewManager.Clear();
			this.Mission.RemoveListener(this);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0001620A File Offset: 0x0001440A
		void IMissionListener.OnEquipItemsFromSpawnEquipmentBegin(Agent agent, Agent.CreationType creationType)
		{
			agent.ClearEquipment();
			agent.AgentVisuals.ClearVisualComponents(false);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00016220 File Offset: 0x00014420
		void IMissionListener.OnEquipItemsFromSpawnEquipment(Agent agent, Agent.CreationType creationType)
		{
			switch (creationType)
			{
			case Agent.CreationType.FromRoster:
			case Agent.CreationType.FromCharacterObj:
			{
				bool useTeamColor = false;
				Random randomGenerator = null;
				bool randomizeColors = agent.RandomizeColors;
				uint color3;
				uint color4;
				if (randomizeColors)
				{
					int bodyPropertiesSeed = agent.BodyPropertiesSeed;
					randomGenerator = new Random(bodyPropertiesSeed);
					Color color;
					Color color2;
					AgentVisuals.GetRandomClothingColors(bodyPropertiesSeed, Color.FromUint(agent.ClothingColor1), Color.FromUint(agent.ClothingColor2), out color, out color2);
					color3 = color.ToUnsignedInteger();
					color4 = color2.ToUnsignedInteger();
				}
				else
				{
					color3 = agent.ClothingColor1;
					color4 = agent.ClothingColor2;
				}
				for (EquipmentIndex equipmentIndex = EquipmentIndex.NumAllWeaponSlots; equipmentIndex < EquipmentIndex.ArmorItemEndSlot; equipmentIndex++)
				{
					if (!agent.SpawnEquipment[equipmentIndex].IsVisualEmpty)
					{
						ItemObject itemObject = agent.SpawnEquipment[equipmentIndex].CosmeticItem ?? agent.SpawnEquipment[equipmentIndex].Item;
						bool hasGloves = equipmentIndex == EquipmentIndex.Body && agent.SpawnEquipment[EquipmentIndex.Gloves].Item != null;
						bool isFemale = agent.Age >= 14f && agent.IsFemale;
						MetaMesh multiMesh = agent.SpawnEquipment[equipmentIndex].GetMultiMesh(isFemale, hasGloves, true);
						if (multiMesh != null)
						{
							if (randomizeColors)
							{
								multiMesh.SetGlossMultiplier(AgentVisuals.GetRandomGlossFactor(randomGenerator));
							}
							if (!itemObject.IsUsingTableau)
							{
								goto IL_212;
							}
							bool flag;
							if (agent == null)
							{
								flag = (null != null);
							}
							else
							{
								IAgentOriginBase origin = agent.Origin;
								flag = (((origin != null) ? origin.Banner : null) != null);
							}
							if (!flag)
							{
								goto IL_212;
							}
							for (int i = 0; i < multiMesh.MeshCount; i++)
							{
								Mesh currentMesh = multiMesh.GetMeshAtIndex(i);
								Mesh currentMesh3 = currentMesh;
								if (currentMesh3 != null && !currentMesh3.HasTag("dont_use_tableau"))
								{
									Mesh currentMesh2 = currentMesh;
									if (currentMesh2 != null && currentMesh2.HasTag("banner_replacement_mesh"))
									{
										((BannerVisual)agent.Origin.Banner.BannerVisual).GetTableauTextureLarge(delegate(Texture t)
										{
											MissionScreen.ApplyBannerTextureToMesh(currentMesh, t);
										}, true);
										currentMesh.ManualInvalidate();
										break;
									}
								}
								currentMesh.ManualInvalidate();
							}
							IL_287:
							if (itemObject.UsingFacegenScaling)
							{
								multiMesh.UseHeadBoneFaceGenScaling(agent.AgentVisuals.GetSkeleton(), agent.Monster.HeadLookDirectionBoneIndex, agent.AgentVisuals.GetFacegenScalingMatrix());
							}
							Skeleton skeleton = agent.AgentVisuals.GetSkeleton();
							int num = (skeleton != null) ? skeleton.GetComponentCount(GameEntity.ComponentType.ClothSimulator) : -1;
							agent.AgentVisuals.AddMultiMesh(multiMesh, MBAgentVisuals.GetBodyMeshIndex(equipmentIndex));
							multiMesh.ManualInvalidate();
							int num2 = (skeleton != null) ? skeleton.GetComponentCount(GameEntity.ComponentType.ClothSimulator) : -1;
							if (skeleton != null && equipmentIndex == EquipmentIndex.Cape && num2 > num)
							{
								GameEntityComponent componentAtIndex = skeleton.GetComponentAtIndex(GameEntity.ComponentType.ClothSimulator, num2 - 1);
								agent.SetCapeClothSimulator(componentAtIndex);
								goto IL_32E;
							}
							goto IL_32E;
							IL_212:
							if (itemObject.IsUsingTeamColor)
							{
								for (int j = 0; j < multiMesh.MeshCount; j++)
								{
									Mesh meshAtIndex = multiMesh.GetMeshAtIndex(j);
									if (!meshAtIndex.HasTag("no_team_color"))
									{
										meshAtIndex.Color = color3;
										meshAtIndex.Color2 = color4;
										Material material = meshAtIndex.GetMaterial().CreateCopy();
										material.AddMaterialShaderFlag("use_double_colormap_with_mask_texture", false);
										meshAtIndex.SetMaterial(material);
										useTeamColor = true;
									}
									meshAtIndex.ManualInvalidate();
								}
								goto IL_287;
							}
							goto IL_287;
						}
						IL_32E:
						if (equipmentIndex == EquipmentIndex.Body && !string.IsNullOrEmpty(itemObject.ArmBandMeshName))
						{
							MetaMesh copy = MetaMesh.GetCopy(itemObject.ArmBandMeshName, true, true);
							if (copy != null)
							{
								if (randomizeColors)
								{
									copy.SetGlossMultiplier(AgentVisuals.GetRandomGlossFactor(randomGenerator));
								}
								if (itemObject.IsUsingTeamColor)
								{
									for (int k = 0; k < copy.MeshCount; k++)
									{
										Mesh meshAtIndex2 = copy.GetMeshAtIndex(k);
										if (!meshAtIndex2.HasTag("no_team_color"))
										{
											meshAtIndex2.Color = color3;
											meshAtIndex2.Color2 = color4;
											Material material2 = meshAtIndex2.GetMaterial().CreateCopy();
											material2.AddMaterialShaderFlag("use_double_colormap_with_mask_texture", false);
											meshAtIndex2.SetMaterial(material2);
											useTeamColor = true;
										}
										meshAtIndex2.ManualInvalidate();
									}
								}
								agent.AgentVisuals.AddMultiMesh(copy, MBAgentVisuals.GetBodyMeshIndex(equipmentIndex));
								copy.ManualInvalidate();
							}
						}
					}
				}
				ItemObject item = agent.SpawnEquipment[EquipmentIndex.Body].Item;
				if (item != null)
				{
					int lodAtlasIndex = item.LodAtlasIndex;
					if (lodAtlasIndex != -1)
					{
						agent.AgentVisuals.SetLodAtlasShadingIndex(lodAtlasIndex, useTeamColor, agent.ClothingColor1, agent.ClothingColor2);
					}
				}
				break;
			}
			case Agent.CreationType.FromHorseObj:
				MountVisualCreator.AddMountMeshToAgentVisual(agent.AgentVisuals, agent.SpawnEquipment[EquipmentIndex.ArmorItemEndSlot].Item, agent.SpawnEquipment[EquipmentIndex.HorseHarness].Item, agent.HorseCreationKey, agent);
				break;
			}
			ArmorComponent.ArmorMaterialTypes bodyArmorMaterialType = ArmorComponent.ArmorMaterialTypes.None;
			ItemObject item2 = agent.SpawnEquipment[EquipmentIndex.Body].Item;
			if (item2 != null)
			{
				bodyArmorMaterialType = item2.ArmorComponent.MaterialType;
			}
			agent.SetBodyArmorMaterialType(bodyArmorMaterialType);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000166F8 File Offset: 0x000148F8
		void IMissionListener.OnConversationCharacterChanged()
		{
			this._cameraAddSpecialMovement = true;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00016704 File Offset: 0x00014904
		void IMissionListener.OnMissionModeChange(MissionMode oldMissionMode, bool atStart)
		{
			if (this.Mission.Mode == MissionMode.Conversation && oldMissionMode != MissionMode.Conversation)
			{
				this._cameraAddSpecialMovement = true;
				this._cameraApplySpecialMovementsInstantly = atStart;
			}
			else if (this.Mission.Mode == MissionMode.Battle && oldMissionMode == MissionMode.Deployment && this.CombatCamera != null)
			{
				this._cameraAddSpecialMovement = true;
				this._cameraApplySpecialMovementsInstantly = (atStart || this._playerDeploymentCancelled);
				Agent agentToFollow = this.GetSpectatingData(this.CombatCamera.Position).AgentToFollow;
				if (!atStart)
				{
					this.LastFollowedAgent = agentToFollow;
				}
				this._cameraSpecialCurrentAddedElevation = this.CameraElevation;
				if (agentToFollow != null)
				{
					this._cameraSpecialCurrentAddedBearing = MBMath.WrapAngle(this.CameraBearing - agentToFollow.LookDirectionAsAngle);
					this._cameraSpecialCurrentPositionToAdd = this.CombatCamera.Position - agentToFollow.VisualPosition;
					this.CameraBearing = agentToFollow.LookDirectionAsAngle;
				}
				else
				{
					this._cameraSpecialCurrentAddedBearing = 0f;
					this._cameraSpecialCurrentPositionToAdd = Vec3.Zero;
					this.CameraBearing = 0f;
				}
				this.CameraElevation = 0f;
			}
			if (((this.Mission.Mode == MissionMode.Conversation || this.Mission.Mode == MissionMode.Barter) && oldMissionMode != MissionMode.Conversation && oldMissionMode != MissionMode.Barter) || ((oldMissionMode == MissionMode.Conversation || oldMissionMode == MissionMode.Barter) && this.Mission.Mode != MissionMode.Conversation && this.Mission.Mode != MissionMode.Barter))
			{
				this._cameraAddSpecialMovement = true;
				this._cameraAddSpecialPositionalMovement = true;
				this._cameraApplySpecialMovementsInstantly = atStart;
			}
			this._cameraHeightLimit = 0f;
			if (this.Mission.Mode == MissionMode.Deployment)
			{
				GameEntity gameEntity;
				if (this.Mission.PlayerTeam.Side == BattleSideEnum.Attacker)
				{
					gameEntity = (this.Mission.Scene.FindEntityWithTag("strategyCameraAttacker") ?? this.Mission.Scene.FindEntityWithTag("strategyCameraDefender"));
				}
				else
				{
					gameEntity = (this.Mission.Scene.FindEntityWithTag("strategyCameraDefender") ?? this.Mission.Scene.FindEntityWithTag("strategyCameraAttacker"));
				}
				if (gameEntity != null)
				{
					this._cameraHeightLimit = gameEntity.GetGlobalFrame().origin.z;
					return;
				}
			}
			else
			{
				GameEntity gameEntity2 = this.Mission.Scene.FindEntityWithTag("camera_height_limiter");
				if (gameEntity2 != null)
				{
					this._cameraHeightLimit = gameEntity2.GetGlobalFrame().origin.z;
				}
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0001695C File Offset: 0x00014B5C
		void IMissionListener.OnResetMission()
		{
			this._agentToFollowOverride = null;
			this.LastFollowedAgent = null;
			this.LastFollowedAgentVisuals = null;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00016974 File Offset: 0x00014B74
		void IMissionListener.OnInitialDeploymentPlanMade(BattleSideEnum battleSide, bool isFirstPlan)
		{
			if (!GameNetwork.IsMultiplayer && this.Mission.Mode == MissionMode.Deployment && isFirstPlan)
			{
				BattleSideEnum side = this.Mission.PlayerTeam.Side;
				if (side == battleSide)
				{
					DeploymentMissionController missionBehavior = this.Mission.GetMissionBehavior<DeploymentMissionController>();
					bool flag = missionBehavior != null && MissionGameModels.Current.BattleInitializationModel.CanPlayerSideDeployWithOrderOfBattle();
					GameEntity a;
					if (side == BattleSideEnum.Attacker)
					{
						a = (this.Mission.Scene.FindEntityWithTag("strategyCameraAttacker") ?? this.Mission.Scene.FindEntityWithTag("strategyCameraDefender"));
					}
					else
					{
						a = (this.Mission.Scene.FindEntityWithTag("strategyCameraDefender") ?? this.Mission.Scene.FindEntityWithTag("strategyCameraAttacker"));
					}
					if (a == null && flag)
					{
						MatrixFrame battleSideDeploymentFrame = this.Mission.DeploymentPlan.GetBattleSideDeploymentFrame(side);
						MatrixFrame matrixFrame = battleSideDeploymentFrame;
						float f = Math.Max(0.2f * (float)this.Mission.DeploymentPlan.GetTroopCountForSide(side, DeploymentPlanType.Initial), 32f);
						matrixFrame.rotation.RotateAboutSide(-0.5235988f);
						matrixFrame.origin -= f * matrixFrame.rotation.f;
						bool flag2 = false;
						if (this.Mission.IsPositionInsideBoundaries(matrixFrame.origin.AsVec2))
						{
							flag2 = true;
						}
						else
						{
							IEnumerable<KeyValuePair<string, ICollection<Vec2>>> source = from boundary in this.Mission.Boundaries
							where boundary.Key == "walk_area"
							select boundary;
							if (!source.IsEmpty<KeyValuePair<string, ICollection<Vec2>>>())
							{
								List<Vec2> list = source.First<KeyValuePair<string, ICollection<Vec2>>>().Value as List<Vec2>;
								list = (list ?? list.ToList<Vec2>());
								Vec2 rayDir = matrixFrame.rotation.f.AsVec2.Normalized();
								Vec2 asVec = matrixFrame.origin.AsVec2;
								Vec2 xy;
								if (MBMath.IntersectRayWithBoundaryList(asVec, rayDir, list, out xy))
								{
									Vec2 asVec2 = battleSideDeploymentFrame.origin.AsVec2;
									float num = xy.Distance(asVec2);
									float val = asVec.Distance(asVec2);
									float z = num / Math.Max(val, 0.1f) * matrixFrame.origin.z;
									Vec3 origin = new Vec3(xy, z, -1f);
									matrixFrame.origin = origin;
									flag2 = true;
								}
							}
						}
						if (!flag2)
						{
							matrixFrame = battleSideDeploymentFrame;
							matrixFrame.origin.z = matrixFrame.origin.z + 20f;
						}
						this.CombatCamera.Frame = matrixFrame;
						this.CameraBearing = matrixFrame.rotation.f.RotationZ;
						this.CameraElevation = matrixFrame.rotation.f.RotationX;
					}
					this._playerDeploymentCancelled = (missionBehavior != null && !flag);
				}
			}
			foreach (MissionView missionView in this._missionViews)
			{
				missionView.OnInitialDeploymentPlanMadeForSide(battleSide, isFirstPlan);
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00016C88 File Offset: 0x00014E88
		private void CalculateNewBearingAndElevationForFirstPerson(Agent agentToFollow, float cameraBearing, float cameraElevation, out float newBearing, out float newElevation)
		{
			newBearing = cameraBearing;
			newElevation = cameraElevation;
			AnimFlags currentAnimationFlag = agentToFollow.GetCurrentAnimationFlag(0);
			AnimFlags currentAnimationFlag2 = agentToFollow.GetCurrentAnimationFlag(1);
			if (currentAnimationFlag.HasAnyFlag(AnimFlags.anf_lock_movement | AnimFlags.anf_synch_with_ladder_movement) || currentAnimationFlag2.HasAnyFlag(AnimFlags.anf_lock_movement | AnimFlags.anf_synch_with_ladder_movement) || agentToFollow.MovementLockedState == AgentMovementLockedState.FrameLocked)
			{
				MatrixFrame boneEntitialFrame = agentToFollow.AgentVisuals.GetBoneEntitialFrame(agentToFollow.Monster.ThoraxLookDirectionBoneIndex, true);
				MatrixFrame frame = agentToFollow.AgentVisuals.GetFrame();
				float rotationZ = boneEntitialFrame.rotation.f.RotationZ;
				float num = rotationZ + frame.rotation.f.RotationZ;
				float num2 = 66f.ToRadians();
				if (Math.Abs(rotationZ) > num2 * 0.5f - 0.0001f)
				{
					float num3 = Math.Abs(rotationZ) - (num2 * 0.5f - 0.0001f);
					num2 += num3;
					num += num3 * ((rotationZ < 0f) ? 0.5f : -0.5f);
				}
				if (Math.Abs(rotationZ) > num2 * 0.5f - 0.0001f)
				{
					float num4 = Math.Abs(rotationZ) - (num2 * 0.5f - 0.0001f);
					num2 += num4;
					num += num4 * ((rotationZ < 0f) ? 0.5f : -0.5f);
				}
				if (num <= -3.1415927f)
				{
					num += 6.2831855f;
				}
				else if (num > 3.1415927f)
				{
					num -= 6.2831855f;
				}
				newBearing = MBMath.ClampAngle(MBMath.WrapAngle(cameraBearing), num, num2);
				float restrictionRange = 50f.ToRadians();
				newElevation = MBMath.ClampAngle(MBMath.WrapAngle(cameraElevation), frame.rotation.f.RotationX, restrictionRange);
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00016E34 File Offset: 0x00015034
		private static void ApplyBannerTextureToMesh(Mesh armorMesh, Texture bannerTexture)
		{
			if (armorMesh != null)
			{
				Material material = armorMesh.GetMaterial().CreateCopy();
				material.SetTexture(Material.MBTextureType.DiffuseMap2, bannerTexture);
				uint num = (uint)material.GetShader().GetMaterialShaderFlagMask("use_tableau_blending", true);
				ulong shaderFlags = material.GetShaderFlags();
				material.SetShaderFlags(shaderFlags | (ulong)num);
				armorMesh.SetMaterial(material);
			}
		}

		// Token: 0x0400013C RID: 316
		public const int LoadingScreenFramesLeftInitial = 15;

		// Token: 0x0400013E RID: 318
		public Func<BasicCharacterObject> GetSpectatedCharacter;

		// Token: 0x04000141 RID: 321
		private MissionScreen.GatherCustomAgentListToSpectateDelegate _gatherCustomAgentListToSpectate;

		// Token: 0x04000143 RID: 323
		public const float MinCameraAddedDistance = 0.7f;

		// Token: 0x04000144 RID: 324
		public const float MinCameraDistanceHardLimit = 0.48f;

		// Token: 0x04000145 RID: 325
		public const float MaxCameraAddedDistance = 2.4f;

		// Token: 0x04000146 RID: 326
		private const int _cheatTimeSpeedRequestId = 1121;

		// Token: 0x04000147 RID: 327
		private const string AttackerCameraEntityTag = "strategyCameraAttacker";

		// Token: 0x04000148 RID: 328
		private const string DefenderCameraEntityTag = "strategyCameraDefender";

		// Token: 0x04000149 RID: 329
		private const string CameraHeightLimiterTag = "camera_height_limiter";

		// Token: 0x0400014A RID: 330
		private float _cameraRayCastOffset;

		// Token: 0x0400014B RID: 331
		private bool _forceCanZoom;

		// Token: 0x0400014C RID: 332
		private ScreenLayer _emptyUILayer;

		// Token: 0x0400014D RID: 333
		public const float DefaultViewAngle = 65f;

		// Token: 0x0400014F RID: 335
		private Camera _customCamera;

		// Token: 0x04000150 RID: 336
		private Vec3[] _cameraNearPlanePoints = new Vec3[4];

		// Token: 0x04000151 RID: 337
		private Vec3[] _cameraBoxPoints = new Vec3[8];

		// Token: 0x04000152 RID: 338
		private Vec3 _cameraTarget;

		// Token: 0x04000156 RID: 342
		private float _cameraBearingDelta;

		// Token: 0x04000157 RID: 343
		private float _cameraElevationDelta;

		// Token: 0x04000158 RID: 344
		private float _cameraSpecialTargetAddedBearing;

		// Token: 0x04000159 RID: 345
		private float _cameraSpecialCurrentAddedBearing;

		// Token: 0x0400015A RID: 346
		private float _cameraSpecialTargetAddedElevation;

		// Token: 0x0400015B RID: 347
		private float _cameraSpecialCurrentAddedElevation;

		// Token: 0x0400015C RID: 348
		private Vec3 _cameraSpecialTargetPositionToAdd;

		// Token: 0x0400015D RID: 349
		private Vec3 _cameraSpecialCurrentPositionToAdd;

		// Token: 0x0400015E RID: 350
		private float _cameraSpecialTargetDistanceToAdd;

		// Token: 0x0400015F RID: 351
		private float _cameraSpecialCurrentDistanceToAdd;

		// Token: 0x04000160 RID: 352
		private bool _cameraAddSpecialMovement;

		// Token: 0x04000161 RID: 353
		private bool _cameraAddSpecialPositionalMovement;

		// Token: 0x04000162 RID: 354
		private bool _cameraApplySpecialMovementsInstantly;

		// Token: 0x04000163 RID: 355
		private float _cameraSpecialCurrentFOV;

		// Token: 0x04000164 RID: 356
		private float _cameraSpecialTargetFOV;

		// Token: 0x04000165 RID: 357
		private float _cameraTargetAddedHeight;

		// Token: 0x04000166 RID: 358
		private float _cameraDeploymentHeightToAdd;

		// Token: 0x04000167 RID: 359
		private float _lastCameraAddedDistance;

		// Token: 0x04000169 RID: 361
		private float _cameraAddedElevation;

		// Token: 0x0400016A RID: 362
		private float _cameraHeightLimit;

		// Token: 0x0400016B RID: 363
		private float _currentViewBlockingBodyCoeff;

		// Token: 0x0400016C RID: 364
		private float _targetViewBlockingBodyCoeff;

		// Token: 0x0400016D RID: 365
		private bool _applySmoothTransitionToVirtualEyeCamera;

		// Token: 0x0400016F RID: 367
		private Vec3 _cameraSpeed;

		// Token: 0x04000170 RID: 368
		private float _cameraSpeedMultiplier;

		// Token: 0x04000171 RID: 369
		private bool _cameraSmoothMode;

		// Token: 0x04000172 RID: 370
		private bool _fixCamera;

		// Token: 0x04000173 RID: 371
		private int _shiftSpeedMultiplier = 3;

		// Token: 0x04000174 RID: 372
		private bool _tickEditor;

		// Token: 0x04000175 RID: 373
		private bool _playerDeploymentCancelled;

		// Token: 0x0400017A RID: 378
		private const float LookUpLimit = 1.1219975f;

		// Token: 0x0400017B RID: 379
		private const float LookDownLimit = -1.3659099f;

		// Token: 0x0400017C RID: 380
		public const float FirstPersonNearClippingDistance = 0.065f;

		// Token: 0x0400017D RID: 381
		public const float ThirdPersonNearClippingDistance = 0.1f;

		// Token: 0x0400017E RID: 382
		public const float FarClippingDistance = 12500f;

		// Token: 0x0400017F RID: 383
		private const float HoldTimeForCameraToggle = 0.5f;

		// Token: 0x04000180 RID: 384
		private bool _zoomToggled;

		// Token: 0x04000181 RID: 385
		private float _zoomToggleTime = float.MaxValue;

		// Token: 0x04000182 RID: 386
		private float _zoomAmount;

		// Token: 0x04000183 RID: 387
		private float _cameraToggleStartTime = float.MaxValue;

		// Token: 0x04000185 RID: 389
		private bool _displayingDialog;

		// Token: 0x04000186 RID: 390
		private MissionMainAgentController _missionMainAgentController;

		// Token: 0x04000187 RID: 391
		private ICameraModeLogic _missionCameraModeLogic;

		// Token: 0x04000188 RID: 392
		private MissionLobbyComponent _missionLobbyComponent;

		// Token: 0x0400018A RID: 394
		private bool _isPlayerAgentAdded = true;

		// Token: 0x0400018B RID: 395
		private bool _isRenderingStarted;

		// Token: 0x0400018C RID: 396
		private bool _onSceneRenderingStartedCalled;

		// Token: 0x0400018D RID: 397
		private int _loadingScreenFramesLeft = 15;

		// Token: 0x0400018E RID: 398
		private bool _resetDraggingMode;

		// Token: 0x0400018F RID: 399
		private bool _rightButtonDraggingMode;

		// Token: 0x04000190 RID: 400
		private Vec2 _clickedPositionPixel = Vec2.Zero;

		// Token: 0x04000191 RID: 401
		private Agent _agentToFollowOverride;

		// Token: 0x04000192 RID: 402
		private Agent _lastFollowedAgent;

		// Token: 0x04000194 RID: 404
		private MissionMultiplayerGameModeBaseClient _mpGameModeBase;

		// Token: 0x04000195 RID: 405
		private bool _isGamepadActive;

		// Token: 0x04000196 RID: 406
		private List<MissionView> _missionViews;

		// Token: 0x04000198 RID: 408
		private MissionState _missionState;

		// Token: 0x0400019A RID: 410
		private bool _isDeactivated;

		// Token: 0x02000097 RID: 151
		// (Invoke) Token: 0x060004C8 RID: 1224
		public delegate void OnSpectateAgentDelegate(Agent followedAgent);

		// Token: 0x02000098 RID: 152
		// (Invoke) Token: 0x060004CC RID: 1228
		public delegate List<Agent> GatherCustomAgentListToSpectateDelegate(Agent forcedAgentToInclude);
	}
}

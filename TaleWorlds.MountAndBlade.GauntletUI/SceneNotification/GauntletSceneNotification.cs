using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.Scripts;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI.SceneNotification
{
	// Token: 0x0200001E RID: 30
	public class GauntletSceneNotification : GlobalLayer
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00007A07 File Offset: 0x00005C07
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00007A0E File Offset: 0x00005C0E
		public static GauntletSceneNotification Current { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00007A16 File Offset: 0x00005C16
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007A20 File Offset: 0x00005C20
		private GauntletSceneNotification()
		{
			this._dataSource = new SceneNotificationVM(new Action(this.OnPositiveAction), new Action(this.CloseNotification), new Func<string>(this.GetContinueKeyText));
			this._notificationQueue = new Queue<ValueTuple<SceneNotificationData, bool>>();
			this._contextProviders = new List<ISceneNotificationContextProvider>();
			this._gauntletLayer = new GauntletLayer(4600, "GauntletLayer", false);
			this._gauntletLayer.LoadMovie("SceneNotification", this._dataSource);
			base.Layer = this._gauntletLayer;
			MBInformationManager.OnShowSceneNotification += this.OnShowSceneNotification;
			MBInformationManager.OnHideSceneNotification += this.OnHideSceneNotification;
			MBInformationManager.IsAnySceneNotificationActive += this.IsAnySceneNotifiationActive;
			this._gauntletLayer.GamepadNavigationContext.GainNavigationAfterFrames(2, null);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007AF6 File Offset: 0x00005CF6
		private bool IsAnySceneNotifiationActive()
		{
			return this._isActive;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00007AFE File Offset: 0x00005CFE
		public static void Initialize()
		{
			if (GauntletSceneNotification.Current == null)
			{
				GauntletSceneNotification.Current = new GauntletSceneNotification();
				ScreenManager.AddGlobalLayer(GauntletSceneNotification.Current, false);
				ScreenManager.SetSuspendLayer(GauntletSceneNotification.Current.Layer, true);
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00007B2C File Offset: 0x00005D2C
		private void OnHideSceneNotification()
		{
			this.CloseNotification();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00007B34 File Offset: 0x00005D34
		private void OnShowSceneNotification(SceneNotificationData campaignNotification)
		{
			this._notificationQueue.Enqueue(new ValueTuple<SceneNotificationData, bool>(campaignNotification, campaignNotification.PauseActiveState));
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00007B50 File Offset: 0x00005D50
		protected override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (this._dataSource != null)
			{
				SceneNotificationVM dataSource = this._dataSource;
				PopupSceneCameraPath cameraPathScript = this._cameraPathScript;
				dataSource.EndProgress = ((cameraPathScript != null) ? cameraPathScript.GetCameraFade() : 0f);
				PopupSceneCameraPath cameraPathScript2 = this._cameraPathScript;
				if (cameraPathScript2 != null)
				{
					cameraPathScript2.SetIsReady(this._dataSource.IsReady);
				}
			}
			Scene scene = this._scene;
			if (scene == null)
			{
				return;
			}
			scene.Tick(dt);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00007BBA File Offset: 0x00005DBA
		protected override void OnLateTick(float dt)
		{
			base.OnLateTick(dt);
			this.QueueTick();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007BCC File Offset: 0x00005DCC
		private void QueueTick()
		{
			if (!this._isActive && this._notificationQueue.Count > 0)
			{
				SceneNotificationData.RelevantContextType relevantContext = this._notificationQueue.Peek().Item1.RelevantContext;
				if (this.IsGivenContextApplicableToCurrentContext(relevantContext))
				{
					ValueTuple<SceneNotificationData, bool> valueTuple = this._notificationQueue.Dequeue();
					this.CreateSceneNotification(valueTuple.Item1, valueTuple.Item2);
				}
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007C2C File Offset: 0x00005E2C
		private void OnPositiveAction()
		{
			PopupSceneCameraPath cameraPathScript = this._cameraPathScript;
			if (cameraPathScript != null)
			{
				cameraPathScript.SetPositiveState();
			}
			foreach (PopupSceneSpawnPoint popupSceneSpawnPoint in this._sceneCharacterScripts)
			{
				popupSceneSpawnPoint.SetPositiveState();
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007C90 File Offset: 0x00005E90
		private void OpenScene()
		{
			this._scene = Scene.CreateNewScene(true, true, DecalAtlasGroup.Battle, "mono_renderscene");
			SceneInitializationData sceneInitializationData = new SceneInitializationData(true);
			this._scene.Read(this._activeData.SceneID, ref sceneInitializationData, "");
			this._scene.SetClothSimulationState(true);
			this._scene.SetShadow(true);
			this._scene.SetDynamicShadowmapCascadesRadiusMultiplier(0.1f);
			this._agentRendererSceneController = MBAgentRendererSceneController.CreateNewAgentRendererSceneController(this._scene, 32);
			this._agentRendererSceneController.SetEnforcedVisibilityForAllAgents(this._scene);
			this._sceneCharacterScripts = new List<PopupSceneSpawnPoint>();
			this._customPrefabBannerEntities = new Dictionary<string, GameEntity>();
			GameEntity firstEntityWithScriptComponent = this._scene.GetFirstEntityWithScriptComponent<PopupSceneCameraPath>();
			this._cameraPathScript = firstEntityWithScriptComponent.GetFirstScriptOfType<PopupSceneCameraPath>();
			PopupSceneCameraPath cameraPathScript = this._cameraPathScript;
			if (cameraPathScript != null)
			{
				cameraPathScript.Initialize();
			}
			PopupSceneCameraPath cameraPathScript2 = this._cameraPathScript;
			if (cameraPathScript2 != null)
			{
				cameraPathScript2.SetInitialState();
			}
			List<SceneNotificationData.SceneNotificationCharacter> list = this._activeData.GetSceneNotificationCharacters().ToList<SceneNotificationData.SceneNotificationCharacter>();
			List<Banner> list2 = this._activeData.GetBanners().ToList<Banner>();
			if (list != null)
			{
				int num = 1;
				for (int i = 0; i < list.Count; i++)
				{
					SceneNotificationData.SceneNotificationCharacter sceneNotificationCharacter = list[i];
					BasicCharacterObject character = sceneNotificationCharacter.Character;
					if (character == null)
					{
						num++;
					}
					else
					{
						string tag = "spawnpoint_player_" + num.ToString();
						GameEntity gameEntity = this._scene.FindEntitiesWithTag(tag).ToList<GameEntity>().FirstOrDefault<GameEntity>();
						if (gameEntity == null)
						{
							num++;
						}
						else
						{
							PopupSceneSpawnPoint firstScriptOfType = gameEntity.GetFirstScriptOfType<PopupSceneSpawnPoint>();
							MatrixFrame frame = gameEntity.GetFrame();
							Equipment equipment = character.GetFirstEquipment(false);
							if (sceneNotificationCharacter.OverriddenEquipment != null)
							{
								equipment = sceneNotificationCharacter.OverriddenEquipment;
							}
							else if (sceneNotificationCharacter.UseCivilianEquipment)
							{
								equipment = character.GetFirstEquipment(true);
							}
							BodyProperties bodyProperties = character.GetBodyProperties(character.Equipment, -1);
							if (sceneNotificationCharacter.OverriddenBodyProperties != default(BodyProperties))
							{
								bodyProperties = sceneNotificationCharacter.OverriddenBodyProperties;
							}
							uint clothColor = character.Culture.Color;
							uint clothColor2 = character.Culture.Color2;
							if (sceneNotificationCharacter.CustomColor1 != 4294967295U)
							{
								clothColor = sceneNotificationCharacter.CustomColor1;
							}
							if (sceneNotificationCharacter.CustomColor2 != 4294967295U)
							{
								clothColor2 = sceneNotificationCharacter.CustomColor2;
							}
							Monster baseMonsterFromRace = FaceGen.GetBaseMonsterFromRace(character.Race);
							AgentVisuals agentVisuals = AgentVisuals.Create(new AgentVisualsData().UseMorphAnims(true).Equipment(equipment).Race(character.Race).BodyProperties(bodyProperties).SkeletonType(character.IsFemale ? SkeletonType.Female : SkeletonType.Male).Frame(frame).ActionSet(MBGlobals.GetActionSetWithSuffix(baseMonsterFromRace, character.IsFemale, "_facegen")).Scene(this._scene).Monster(baseMonsterFromRace).PrepareImmediately(true).UseTranslucency(true).UseTesselation(true).ClothColor1(clothColor).ClothColor2(clothColor2), "notification_agent_visuals_" + num, false, false, false);
							AgentVisuals agentVisuals2 = null;
							if (sceneNotificationCharacter.UseHorse)
							{
								ItemObject item = equipment[EquipmentIndex.ArmorItemEndSlot].Item;
								string randomMountKeyString = MountCreationKey.GetRandomMountKeyString(item, character.GetMountKeySeed());
								MBActionSet actionSet = MBGlobals.GetActionSet(item.HorseComponent.Monster.ActionSetCode);
								agentVisuals2 = AgentVisuals.Create(new AgentVisualsData().Equipment(equipment).Frame(frame).ActionSet(actionSet).Scene(this._scene).Monster(item.HorseComponent.Monster).Scale(item.ScaleFactor).PrepareImmediately(true).UseTranslucency(true).UseTesselation(true).MountCreationKey(randomMountKeyString), "notification_mount_visuals_" + num, false, false, false);
							}
							firstScriptOfType.InitializeWithAgentVisuals(agentVisuals, agentVisuals2);
							agentVisuals.SetAgentLodZeroOrMaxExternal(true);
							if (agentVisuals2 != null)
							{
								agentVisuals2.SetAgentLodZeroOrMaxExternal(true);
							}
							firstScriptOfType.SetInitialState();
							this._sceneCharacterScripts.Add(firstScriptOfType);
							if (!string.IsNullOrEmpty(firstScriptOfType.BannerTagToUseForAddedPrefab) && firstScriptOfType.AddedPrefabComponent != null)
							{
								this._customPrefabBannerEntities.Add(firstScriptOfType.BannerTagToUseForAddedPrefab, firstScriptOfType.AddedPrefabComponent.GetEntity());
							}
							num++;
						}
					}
				}
			}
			if (list2 != null)
			{
				for (int j = 0; j < list2.Count; j++)
				{
					Banner banner = list2[j];
					string text = "banner_" + (j + 1).ToString();
					GameEntity bannerEntity = this._scene.FindEntityWithTag(text);
					if (bannerEntity != null)
					{
						((BannerVisual)banner.BannerVisual).GetTableauTextureLarge(delegate(Texture t)
						{
							this.OnBannerTableauRenderDone(bannerEntity, t);
						}, true);
					}
					else
					{
						GameEntity entity;
						if (this._customPrefabBannerEntities.TryGetValue(text, out entity))
						{
							((BannerVisual)banner.BannerVisual).GetTableauTextureLarge(delegate(Texture t)
							{
								this.OnBannerTableauRenderDone(entity, t);
							}, true);
						}
					}
				}
			}
			this._dataSource.Scene = this._scene;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000081A8 File Offset: 0x000063A8
		private void OnBannerTableauRenderDone(GameEntity bannerEntity, Texture bannerTexture)
		{
			if (bannerEntity != null)
			{
				foreach (Mesh bannerMesh in bannerEntity.GetAllMeshesWithTag("banner_replacement_mesh"))
				{
					this.ApplyBannerTextureToMesh(bannerMesh, bannerTexture);
				}
				Skeleton skeleton = bannerEntity.Skeleton;
				if (((skeleton != null) ? skeleton.GetAllMeshes() : null) != null)
				{
					Skeleton skeleton2 = bannerEntity.Skeleton;
					foreach (Mesh mesh in ((skeleton2 != null) ? skeleton2.GetAllMeshes() : null))
					{
						if (mesh.HasTag("banner_replacement_mesh"))
						{
							this.ApplyBannerTextureToMesh(mesh, bannerTexture);
						}
					}
				}
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00008274 File Offset: 0x00006474
		private void ApplyBannerTextureToMesh(Mesh bannerMesh, Texture bannerTexture)
		{
			if (bannerMesh != null)
			{
				Material material = bannerMesh.GetMaterial().CreateCopy();
				material.SetTexture(Material.MBTextureType.DiffuseMap2, bannerTexture);
				uint num = (uint)material.GetShader().GetMaterialShaderFlagMask("use_tableau_blending", true);
				ulong shaderFlags = material.GetShaderFlags();
				material.SetShaderFlags(shaderFlags | (ulong)num);
				bannerMesh.SetMaterial(material);
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000082CC File Offset: 0x000064CC
		private void CreateSceneNotification(SceneNotificationData data, bool pauseGameActiveState)
		{
			if (this._isActive)
			{
				return;
			}
			this._isActive = true;
			this._dataSource.CreateNotification(data);
			ScreenManager.SetSuspendLayer(base.Layer, false);
			base.Layer.IsFocusLayer = true;
			ScreenManager.TrySetFocus(base.Layer);
			base.Layer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this._isLastActiveGameStatePaused = pauseGameActiveState;
			if (this._isLastActiveGameStatePaused)
			{
				GameStateManager.Current.RegisterActiveStateDisableRequest(this);
				MBCommon.PauseGameEngine();
			}
			this._activeData = data;
			this._dataSource.EndProgress = 0f;
			this.OpenScene();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00008368 File Offset: 0x00006568
		private void CloseNotification()
		{
			if (!this._isActive)
			{
				return;
			}
			this._dataSource.ForceClose();
			this._isActive = false;
			base.Layer.InputRestrictions.ResetInputRestrictions();
			ScreenManager.SetSuspendLayer(base.Layer, true);
			base.Layer.IsFocusLayer = false;
			ScreenManager.TryLoseFocus(base.Layer);
			if (this._isLastActiveGameStatePaused)
			{
				GameStateManager.Current.UnregisterActiveStateDisableRequest(this);
				MBCommon.UnPauseGameEngine();
			}
			PopupSceneCameraPath cameraPathScript = this._cameraPathScript;
			if (cameraPathScript != null)
			{
				cameraPathScript.Destroy();
			}
			if (this._sceneCharacterScripts != null)
			{
				foreach (PopupSceneSpawnPoint popupSceneSpawnPoint in this._sceneCharacterScripts)
				{
					popupSceneSpawnPoint.Destroy();
				}
				this._sceneCharacterScripts = null;
			}
			MBAgentRendererSceneController.DestructAgentRendererSceneController(this._scene, this._agentRendererSceneController, false);
			this._activeData = null;
			this._scene.ClearAll();
			this._scene = null;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000846C File Offset: 0x0000666C
		private string GetContinueKeyText()
		{
			if (Input.IsGamepadActive)
			{
				TextObject textObject = Module.CurrentModule.GlobalTextManager.FindText("str_click_to_continue_console", null);
				textObject.SetTextVariable("CONSOLE_KEY_NAME", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("ConversationHotKeyCategory", "ContinueKey")));
				return textObject.ToString();
			}
			return Module.CurrentModule.GlobalTextManager.FindText("str_click_to_continue", null).ToString();
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000084D5 File Offset: 0x000066D5
		public void OnFinalize()
		{
			this._dataSource.OnFinalize();
			this._dataSource = null;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000084E9 File Offset: 0x000066E9
		public void RegisterContextProvider(ISceneNotificationContextProvider provider)
		{
			this._contextProviders.Add(provider);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000084F7 File Offset: 0x000066F7
		public bool RemoveContextProvider(ISceneNotificationContextProvider provider)
		{
			return this._contextProviders.Remove(provider);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00008508 File Offset: 0x00006708
		private bool IsGivenContextApplicableToCurrentContext(SceneNotificationData.RelevantContextType givenContextType)
		{
			if (LoadingWindow.IsLoadingWindowActive)
			{
				return false;
			}
			if (givenContextType == SceneNotificationData.RelevantContextType.Any)
			{
				return true;
			}
			for (int i = 0; i < this._contextProviders.Count; i++)
			{
				ISceneNotificationContextProvider sceneNotificationContextProvider = this._contextProviders[i];
				if (sceneNotificationContextProvider != null && !sceneNotificationContextProvider.IsContextAllowed(givenContextType))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040000B6 RID: 182
		private readonly GauntletLayer _gauntletLayer;

		// Token: 0x040000B7 RID: 183
		private readonly Queue<ValueTuple<SceneNotificationData, bool>> _notificationQueue;

		// Token: 0x040000B8 RID: 184
		private readonly List<ISceneNotificationContextProvider> _contextProviders;

		// Token: 0x040000B9 RID: 185
		private SceneNotificationVM _dataSource;

		// Token: 0x040000BA RID: 186
		private SceneNotificationData _activeData;

		// Token: 0x040000BB RID: 187
		private bool _isActive;

		// Token: 0x040000BC RID: 188
		private bool _isLastActiveGameStatePaused;

		// Token: 0x040000BD RID: 189
		private Scene _scene;

		// Token: 0x040000BE RID: 190
		private MBAgentRendererSceneController _agentRendererSceneController;

		// Token: 0x040000BF RID: 191
		private List<PopupSceneSpawnPoint> _sceneCharacterScripts;

		// Token: 0x040000C0 RID: 192
		private PopupSceneCameraPath _cameraPathScript;

		// Token: 0x040000C1 RID: 193
		private Dictionary<string, GameEntity> _customPrefabBannerEntities;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Options;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.Tableaus;

namespace SandBox.View.Map
{
	// Token: 0x0200003B RID: 59
	public class MapConversationTableau
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00013D9C File Offset: 0x00011F9C
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00013DA4 File Offset: 0x00011FA4
		public Texture Texture { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00013DAD File Offset: 0x00011FAD
		private TableauView View
		{
			get
			{
				Texture texture = this.Texture;
				if (texture == null)
				{
					return null;
				}
				return texture.TableauView;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00013DC0 File Offset: 0x00011FC0
		public MapConversationTableau()
		{
			this._changeIdleActionTimer = new Timer(Game.Current.ApplicationTime, 8f, true);
			this._agentVisuals = new List<AgentVisuals>();
			TableauView view = this.View;
			if (view != null)
			{
				view.SetEnable(this._isEnabled);
			}
			this._dataProvider = SandBoxViewSubModule.MapConversationDataProvider;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00013EB4 File Offset: 0x000120B4
		public void SetEnabled(bool enabled)
		{
			if (this._isEnabled != enabled)
			{
				if (enabled)
				{
					TableauView view = this.View;
					if (view != null)
					{
						view.SetEnable(false);
					}
					TableauView view2 = this.View;
					if (view2 != null)
					{
						view2.AddClearTask(true);
					}
					Texture texture = this.Texture;
					if (texture != null)
					{
						texture.ReleaseNextFrame();
					}
					this.Texture = TableauView.AddTableau("MapConvTableau", new RenderTargetComponent.TextureUpdateEventHandler(this.CharacterTableauContinuousRenderFunction), this._tableauScene, this._tableauSizeX, this._tableauSizeY);
					this.Texture.TableauView.SetSceneUsesContour(false);
				}
				else
				{
					TableauView view3 = this.View;
					if (view3 != null)
					{
						view3.SetEnable(false);
					}
					TableauView view4 = this.View;
					if (view4 != null)
					{
						view4.ClearAll(false, false);
					}
				}
				this._isEnabled = enabled;
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00013F74 File Offset: 0x00012174
		public void SetData(object data)
		{
			if (this._data != null)
			{
				this._initialized = false;
				foreach (AgentVisuals agentVisuals in this._agentVisuals)
				{
					agentVisuals.Reset();
				}
				this._agentVisuals.Clear();
			}
			this._data = (data as MapConversationTableauData);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00013FEC File Offset: 0x000121EC
		public void SetTargetSize(int width, int height)
		{
			int num;
			int num2;
			if (width <= 0 || height <= 0)
			{
				num = 10;
				num2 = 10;
			}
			else
			{
				this.RenderScale = NativeOptions.GetConfig(NativeOptions.NativeOptionsType.ResolutionScale) / 100f;
				num = (int)((float)width * this.RenderScale);
				num2 = (int)((float)height * this.RenderScale);
			}
			if (num != this._tableauSizeX || num2 != this._tableauSizeY)
			{
				this._tableauSizeX = num;
				this._tableauSizeY = num2;
				this._cameraRatio = (float)this._tableauSizeX / (float)this._tableauSizeY;
				TableauView view = this.View;
				if (view != null)
				{
					view.SetEnable(false);
				}
				TableauView view2 = this.View;
				if (view2 != null)
				{
					view2.AddClearTask(true);
				}
				Texture texture = this.Texture;
				if (texture != null)
				{
					texture.ReleaseNextFrame();
				}
				this.Texture = TableauView.AddTableau("MapConvTableau", new RenderTargetComponent.TextureUpdateEventHandler(this.CharacterTableauContinuousRenderFunction), this._tableauScene, this._tableauSizeX, this._tableauSizeY);
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000140D4 File Offset: 0x000122D4
		public void OnFinalize(bool clearNextFrame)
		{
			TableauView view = this.View;
			if (view != null)
			{
				view.SetEnable(false);
			}
			this.RemovePreviousAgentsSoundEvent();
			this.StopConversationSoundEvent();
			Camera continuousRenderCamera = this._continuousRenderCamera;
			if (continuousRenderCamera != null)
			{
				continuousRenderCamera.ReleaseCameraEntity();
			}
			this._continuousRenderCamera = null;
			foreach (AgentVisuals agentVisuals in this._agentVisuals)
			{
				agentVisuals.ResetNextFrame();
			}
			this._agentVisuals = null;
			if (clearNextFrame)
			{
				this.View.AddClearTask(true);
				this.Texture.ReleaseNextFrame();
			}
			else
			{
				this.View.ClearAll(false, false);
				this.Texture.Release();
			}
			this.Texture = null;
			IEnumerable<GameEntity> enumerable = this._tableauScene.FindEntitiesWithTag(this._cachedAtmosphereName);
			this._cachedAtmosphereName = "";
			foreach (GameEntity gameEntity in enumerable)
			{
				gameEntity.SetVisibilityExcludeParents(false);
			}
			TableauCacheManager.Current.ReturnCachedMapConversationTableauScene();
			this._tableauScene = null;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00014200 File Offset: 0x00012400
		public void OnTick(float dt)
		{
			if (this._data != null && !this._initialized)
			{
				this.FirstTimeInit();
				MapScreen instance = MapScreen.Instance;
				((instance != null) ? instance.GetMapView<MapConversationView>() : null).ConversationMission.SetConversationTableau(this);
			}
			if (this._conversationSoundEvent != null && !this._conversationSoundEvent.IsPlaying())
			{
				this.RemovePreviousAgentsSoundEvent();
				this._conversationSoundEvent.Stop();
				this._conversationSoundEvent = null;
			}
			if (this._animationFrequencyThreshold > this._animationGap)
			{
				this._animationGap += dt;
			}
			TableauView view = this.View;
			if (view != null)
			{
				if (this._continuousRenderCamera == null)
				{
					this._continuousRenderCamera = Camera.CreateCamera();
				}
				view.SetDoNotRenderThisFrame(false);
			}
			if (this._agentVisuals != null && this._agentVisuals.Count > 0)
			{
				this._agentVisuals[0].TickVisuals();
			}
			if (this._agentVisuals[0].GetEquipment().CalculateEquipmentCode() != this._opponentLeaderEquipmentCache)
			{
				this._initialized = false;
				foreach (AgentVisuals agentVisuals in this._agentVisuals)
				{
					agentVisuals.Reset();
				}
				this._agentVisuals.Clear();
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00014358 File Offset: 0x00012558
		private void FirstTimeInit()
		{
			if (this._tableauScene == null)
			{
				this._tableauScene = TableauCacheManager.Current.GetCachedMapConversationTableauScene();
			}
			string atmosphereNameFromData = this._dataProvider.GetAtmosphereNameFromData(this._data);
			this._tableauScene.SetAtmosphereWithName(atmosphereNameFromData);
			IEnumerable<GameEntity> enumerable = this._tableauScene.FindEntitiesWithTag(atmosphereNameFromData);
			this._cachedAtmosphereName = atmosphereNameFromData;
			foreach (GameEntity gameEntity in enumerable)
			{
				gameEntity.SetVisibilityExcludeParents(true);
			}
			if (this._continuousRenderCamera == null)
			{
				this._continuousRenderCamera = Camera.CreateCamera();
				this._cameraEntity = this._tableauScene.FindEntityWithTag("player_infantry_to_infantry");
				Vec3 vec = default(Vec3);
				this._cameraEntity.GetCameraParamsFromCameraScript(this._continuousRenderCamera, ref vec);
				this._baseCameraFOV = this._continuousRenderCamera.HorizontalFov;
			}
			this.SpawnOpponentLeader();
			PartyBase party = this._data.ConversationPartnerData.Party;
			bool flag;
			if (party == null)
			{
				flag = false;
			}
			else
			{
				TroopRoster memberRoster = party.MemberRoster;
				int? num = (memberRoster != null) ? new int?(memberRoster.TotalManCount) : null;
				int num2 = 1;
				flag = (num.GetValueOrDefault() > num2 & num != null);
			}
			if (flag)
			{
				int num3 = MathF.Min(2, this._data.ConversationPartnerData.Party.MemberRoster.ToFlattenedRoster().Count<FlattenedTroopRosterElement>() - 1);
				IOrderedEnumerable<TroopRosterElement> orderedEnumerable = from t in this._data.ConversationPartnerData.Party.MemberRoster.GetTroopRoster()
				orderby t.Character.Level descending
				select t;
				foreach (TroopRosterElement troopRosterElement in orderedEnumerable)
				{
					CharacterObject character = troopRosterElement.Character;
					if (character != this._data.ConversationPartnerData.Character && !character.IsPlayerCharacter)
					{
						num3--;
						this.SpawnOpponentBodyguardCharacter(character, num3);
					}
					if (num3 == 0)
					{
						break;
					}
				}
				if (num3 == 1)
				{
					num3--;
					TroopRosterElement troopRosterElement2 = orderedEnumerable.FirstOrDefault((TroopRosterElement troop) => !troop.Character.IsHero);
					if (troopRosterElement2.Character != null)
					{
						this.SpawnOpponentBodyguardCharacter(troopRosterElement2.Character, num3);
					}
				}
			}
			this._agentVisuals.ForEach(delegate(AgentVisuals a)
			{
				a.SetAgentLodZeroOrMaxExternal(true);
			});
			this._tableauScene.ForceLoadResources();
			this._cameraRatio = Screen.RealScreenResolutionWidth / Screen.RealScreenResolutionHeight;
			this.SetTargetSize((int)Screen.RealScreenResolutionWidth, (int)Screen.RealScreenResolutionHeight);
			uint num4 = uint.MaxValue;
			num4 &= 4294966271U;
			TableauView view = this.View;
			if (view != null)
			{
				view.SetPostfxConfigParams((int)num4);
			}
			this._tableauScene.FindEntityWithTag(this.RainingEntityTag).SetVisibilityExcludeParents(this._data.IsRaining);
			this._tableauScene.FindEntityWithTag(this.SnowingEntityTag).SetVisibilityExcludeParents(this._data.IsSnowing);
			this._tableauScene.Tick(3f);
			TableauView view2 = this.View;
			if (view2 != null)
			{
				view2.SetEnable(true);
			}
			this._initialized = true;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000146A4 File Offset: 0x000128A4
		private void SpawnOpponentLeader()
		{
			CharacterObject character = this._data.ConversationPartnerData.Character;
			if (character != null)
			{
				GameEntity gameEntity = this._tableauScene.FindEntityWithTag("player_infantry_spawn");
				MapConversationTableau.DefaultConversationAnimationData defaultAnimForCharacter = this.GetDefaultAnimForCharacter(character, false);
				this._opponentLeaderEquipmentCache = null;
				Equipment equipment;
				if (this._data.ConversationPartnerData.IsCivilianEquipmentRequiredForLeader)
				{
					equipment = (this._data.ConversationPartnerData.Character.IsHero ? character.FirstCivilianEquipment : character.CivilianEquipments.ElementAt(this._data.ConversationPartnerData.Character.GetDefaultFaceSeed(0) % character.CivilianEquipments.Count<Equipment>()));
				}
				else
				{
					equipment = (this._data.ConversationPartnerData.Character.IsHero ? character.FirstBattleEquipment : character.BattleEquipments.ElementAt(this._data.ConversationPartnerData.Character.GetDefaultFaceSeed(0) % character.BattleEquipments.Count<Equipment>()));
				}
				equipment = equipment.Clone(false);
				for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex++)
				{
					if (!equipment[equipmentIndex].IsEmpty && equipment[equipmentIndex].Item.Type == ItemObject.ItemTypeEnum.Banner)
					{
						equipment[equipmentIndex] = EquipmentElement.Invalid;
						break;
					}
				}
				int seed = -1;
				if (this._data.ConversationPartnerData.Party != null)
				{
					seed = CharacterHelper.GetPartyMemberFaceSeed(this._data.ConversationPartnerData.Party, character, 0);
				}
				ValueTuple<uint, uint> deterministicColorsForCharacter = CharacterHelper.GetDeterministicColorsForCharacter(character);
				Monster baseMonsterFromRace = TaleWorlds.Core.FaceGen.GetBaseMonsterFromRace(character.Race);
				AgentVisualsData agentVisualsData = new AgentVisualsData();
				Hero heroObject = character.HeroObject;
				AgentVisualsData agentVisualsData2 = agentVisualsData.Banner((heroObject != null) ? heroObject.ClanBanner : null).Equipment(equipment).Race(character.Race);
				Hero heroObject2 = character.HeroObject;
				AgentVisuals agentVisuals = AgentVisuals.Create(agentVisualsData2.BodyProperties((heroObject2 != null) ? heroObject2.BodyProperties : character.GetBodyProperties(equipment, seed)).Frame(gameEntity.GetGlobalFrame()).UseMorphAnims(true).ActionSet(MBGlobals.GetActionSetWithSuffix(baseMonsterFromRace, character.IsFemale, "_warrior")).ActionCode(ActionIndexCache.Create(defaultAnimForCharacter.ActionName)).Scene(this._tableauScene).Monster(baseMonsterFromRace).PrepareImmediately(true).SkeletonType(character.IsFemale ? SkeletonType.Female : SkeletonType.Male).ClothColor1(deterministicColorsForCharacter.Item1).ClothColor2(deterministicColorsForCharacter.Item2), "MapConversationTableau", true, false, false);
				agentVisuals.GetVisuals().GetSkeleton().TickAnimationsAndForceUpdate(0.1f, this._frame, true);
				Vec3 globalStableEyePoint = agentVisuals.GetVisuals().GetGlobalStableEyePoint(true);
				agentVisuals.SetLookDirection(this._cameraEntity.GetGlobalFrame().origin - globalStableEyePoint);
				string defaultFaceIdle = CharacterHelper.GetDefaultFaceIdle(character);
				agentVisuals.GetVisuals().GetSkeleton().SetFacialAnimation(Agent.FacialAnimChannel.Mid, defaultFaceIdle, false, true);
				this._agentVisuals.Add(agentVisuals);
				this._opponentLeaderEquipmentCache = ((equipment != null) ? equipment.CalculateEquipmentCode() : null);
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0001498C File Offset: 0x00012B8C
		private void SpawnOpponentBodyguardCharacter(CharacterObject character, int indexOfBodyguard)
		{
			if (indexOfBodyguard >= 0 && indexOfBodyguard <= 1)
			{
				GameEntity gameEntity = this._tableauScene.FindEntitiesWithTag("player_bodyguard_infantry_spawn").ElementAt(indexOfBodyguard);
				MapConversationTableau.DefaultConversationAnimationData defaultAnimForCharacter = this.GetDefaultAnimForCharacter(character, true);
				int num = (indexOfBodyguard + 10) * 5;
				Equipment equipment;
				if (this._data.ConversationPartnerData.IsCivilianEquipmentRequiredForBodyGuardCharacters)
				{
					equipment = (this._data.ConversationPartnerData.Character.IsHero ? character.FirstCivilianEquipment : character.CivilianEquipments.ElementAt(num % character.CivilianEquipments.Count<Equipment>()));
				}
				else
				{
					equipment = (this._data.ConversationPartnerData.Character.IsHero ? character.FirstBattleEquipment : character.BattleEquipments.ElementAt(num % character.BattleEquipments.Count<Equipment>()));
				}
				int seed = -1;
				if (this._data.ConversationPartnerData.Party != null)
				{
					seed = CharacterHelper.GetPartyMemberFaceSeed(this._data.ConversationPartnerData.Party, this._data.ConversationPartnerData.Character, num);
				}
				Monster baseMonsterFromRace = TaleWorlds.Core.FaceGen.GetBaseMonsterFromRace(character.Race);
				AgentVisualsData agentVisualsData = new AgentVisualsData();
				PartyBase party = this._data.ConversationPartnerData.Party;
				Banner banner;
				if (party == null)
				{
					banner = null;
				}
				else
				{
					Hero leaderHero = party.LeaderHero;
					banner = ((leaderHero != null) ? leaderHero.ClanBanner : null);
				}
				AgentVisualsData agentVisualsData2 = agentVisualsData.Banner(banner).Equipment(equipment).Race(character.Race).BodyProperties(character.GetBodyProperties(equipment, seed)).Frame(gameEntity.GetGlobalFrame()).UseMorphAnims(true).ActionSet(MBGlobals.GetActionSetWithSuffix(baseMonsterFromRace, character.IsFemale, "_warrior")).ActionCode(ActionIndexCache.Create(defaultAnimForCharacter.ActionName)).Scene(this._tableauScene).Monster(baseMonsterFromRace).PrepareImmediately(true).SkeletonType(character.IsFemale ? SkeletonType.Female : SkeletonType.Male);
				PartyBase party2 = this._data.ConversationPartnerData.Party;
				uint? num2;
				if (party2 == null)
				{
					num2 = null;
				}
				else
				{
					Hero leaderHero2 = party2.LeaderHero;
					num2 = ((leaderHero2 != null) ? new uint?(leaderHero2.MapFaction.Color) : null);
				}
				AgentVisualsData agentVisualsData3 = agentVisualsData2.ClothColor1(num2 ?? uint.MaxValue);
				PartyBase party3 = this._data.ConversationPartnerData.Party;
				uint? num3;
				if (party3 == null)
				{
					num3 = null;
				}
				else
				{
					Hero leaderHero3 = party3.LeaderHero;
					num3 = ((leaderHero3 != null) ? new uint?(leaderHero3.MapFaction.Color2) : null);
				}
				AgentVisuals agentVisuals = AgentVisuals.Create(agentVisualsData3.ClothColor2(num3 ?? uint.MaxValue), "MapConversationTableau", true, false, false);
				agentVisuals.GetVisuals().GetSkeleton().TickAnimationsAndForceUpdate(0.1f, this._frame, true);
				Vec3 globalStableEyePoint = agentVisuals.GetVisuals().GetGlobalStableEyePoint(true);
				agentVisuals.SetLookDirection(this._cameraEntity.GetGlobalFrame().origin - globalStableEyePoint);
				string defaultFaceIdle = CharacterHelper.GetDefaultFaceIdle(character);
				agentVisuals.GetVisuals().GetSkeleton().SetFacialAnimation(Agent.FacialAnimChannel.Mid, defaultFaceIdle, false, true);
				this._agentVisuals.Add(agentVisuals);
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00014C90 File Offset: 0x00012E90
		internal void CharacterTableauContinuousRenderFunction(Texture sender, EventArgs e)
		{
			Scene scene = (Scene)sender.UserData;
			this.Texture = sender;
			TableauView tableauView = sender.TableauView;
			if (scene == null)
			{
				tableauView.SetContinuousRendering(false);
				tableauView.SetDeleteAfterRendering(true);
				return;
			}
			scene.EnsurePostfxSystem();
			scene.SetDofMode(true);
			scene.SetMotionBlurMode(false);
			scene.SetBloom(true);
			scene.SetDynamicShadowmapCascadesRadiusMultiplier(0.31f);
			tableauView.SetRenderWithPostfx(true);
			uint num = uint.MaxValue;
			num &= 4294966271U;
			if (tableauView != null)
			{
				tableauView.SetPostfxConfigParams((int)num);
			}
			if (this._continuousRenderCamera != null)
			{
				float num2 = this._cameraRatio / 1.7777778f;
				this._continuousRenderCamera.SetFovHorizontal(num2 * this._baseCameraFOV, this._cameraRatio, 0.2f, 200f);
				tableauView.SetCamera(this._continuousRenderCamera);
				tableauView.SetScene(scene);
				tableauView.SetSceneUsesSkybox(true);
				tableauView.SetDeleteAfterRendering(false);
				tableauView.SetContinuousRendering(true);
				tableauView.SetClearColor(0U);
				tableauView.SetClearGbuffer(true);
				tableauView.DoNotClear(false);
				tableauView.SetFocusedShadowmap(true, ref this._frame.origin, 1.55f);
				scene.ForceLoadResources();
				bool flag = true;
				do
				{
					flag = true;
					foreach (AgentVisuals agentVisuals in this._agentVisuals)
					{
						flag = (flag && agentVisuals.GetVisuals().CheckResources(true));
					}
				}
				while (!flag);
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00014E0C File Offset: 0x0001300C
		private MapConversationTableau.DefaultConversationAnimationData GetDefaultAnimForCharacter(CharacterObject character, bool preferLoopAnimationIfAvailable)
		{
			MapConversationTableau.DefaultConversationAnimationData invalid = MapConversationTableau.DefaultConversationAnimationData.Invalid;
			CultureObject culture = character.Culture;
			if (culture != null && culture.IsBandit)
			{
				invalid.ActionName = "aggressive";
			}
			else
			{
				Hero heroObject = character.HeroObject;
				if (heroObject != null && heroObject.IsWounded)
				{
					PlayerEncounter playerEncounter = PlayerEncounter.Current;
					if (playerEncounter != null && playerEncounter.EncounterState == PlayerEncounterState.CaptureHeroes)
					{
						invalid.ActionName = "weary";
						goto IL_6D;
					}
				}
				invalid.ActionName = CharacterHelper.GetStandingBodyIdle(character);
			}
			IL_6D:
			ConversationAnimData conversationAnimData;
			if (Campaign.Current.ConversationManager.ConversationAnimationManager.ConversationAnims.TryGetValue(invalid.ActionName, out conversationAnimData))
			{
				bool flag = !string.IsNullOrEmpty(conversationAnimData.IdleAnimStart);
				bool flag2 = !string.IsNullOrEmpty(conversationAnimData.IdleAnimLoop);
				invalid.ActionName = (((preferLoopAnimationIfAvailable && flag2) || !flag) ? conversationAnimData.IdleAnimLoop : conversationAnimData.IdleAnimStart);
				invalid.AnimationData = conversationAnimData;
				invalid.AnimationDataValid = true;
			}
			else
			{
				invalid.ActionName = MapConversationTableau.fallbackAnimActName;
				if (Campaign.Current.ConversationManager.ConversationAnimationManager.ConversationAnims.TryGetValue(invalid.ActionName, out conversationAnimData))
				{
					invalid.AnimationData = conversationAnimData;
					invalid.AnimationDataValid = true;
				}
			}
			return invalid;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00014F38 File Offset: 0x00013138
		public void OnConversationPlay(string idleActionId, string idleFaceAnimId, string reactionId, string reactionFaceAnimId, string soundPath)
		{
			if (!this._initialized)
			{
				Debug.FailedAssert("Conversation Tableau shouldn't play before initialization", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.View\\Map\\MapConversationTableau.cs", "OnConversationPlay", 590);
				return;
			}
			if (!Campaign.Current.ConversationManager.SpeakerAgent.Character.IsPlayerCharacter)
			{
				bool flag = false;
				bool flag2 = string.IsNullOrEmpty(idleActionId);
				ConversationAnimData animationData;
				if (flag2)
				{
					MapConversationTableau.DefaultConversationAnimationData defaultAnimForCharacter = this.GetDefaultAnimForCharacter(this._data.ConversationPartnerData.Character, false);
					animationData = defaultAnimForCharacter.AnimationData;
					flag = defaultAnimForCharacter.AnimationDataValid;
				}
				else if (Campaign.Current.ConversationManager.ConversationAnimationManager.ConversationAnims.TryGetValue(idleActionId, out animationData))
				{
					flag = true;
				}
				if (flag)
				{
					if (!string.IsNullOrEmpty(reactionId))
					{
						this._agentVisuals[0].SetAction(ActionIndexCache.Create(animationData.Reactions[reactionId]), 0f, false);
					}
					else if (!flag2 || this._changeIdleActionTimer.Check(Game.Current.ApplicationTime))
					{
						ActionIndexCache actionIndex = ActionIndexCache.Create(animationData.IdleAnimStart);
						if (!this._agentVisuals[0].DoesActionContinueWithCurrentAction(actionIndex))
						{
							this._changeIdleActionTimer.Reset(Game.Current.ApplicationTime);
							this._agentVisuals[0].SetAction(actionIndex, 0f, false);
						}
					}
				}
				if (!string.IsNullOrEmpty(reactionFaceAnimId))
				{
					this._agentVisuals[0].GetVisuals().GetSkeleton().SetFacialAnimation(Agent.FacialAnimChannel.Mid, reactionFaceAnimId, false, false);
				}
				else if (!string.IsNullOrEmpty(idleFaceAnimId))
				{
					this._agentVisuals[0].GetVisuals().GetSkeleton().SetFacialAnimation(Agent.FacialAnimChannel.Mid, idleFaceAnimId, false, true);
				}
			}
			this.RemovePreviousAgentsSoundEvent();
			this.StopConversationSoundEvent();
			if (!string.IsNullOrEmpty(soundPath))
			{
				this.PlayConversationSoundEvent(soundPath);
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000150E5 File Offset: 0x000132E5
		private void RemovePreviousAgentsSoundEvent()
		{
			if (this._conversationSoundEvent != null)
			{
				this._agentVisuals[0].StartRhubarbRecord("", -1);
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00015108 File Offset: 0x00013308
		private void PlayConversationSoundEvent(string soundPath)
		{
			Debug.Print("Conversation sound playing: " + soundPath, 5, Debug.DebugColor.White, 17592186044416UL);
			this._conversationSoundEvent = SoundEvent.CreateEventFromExternalFile("event:/Extra/voiceover", soundPath, this._tableauScene);
			this._conversationSoundEvent.Play();
			int soundId = this._conversationSoundEvent.GetSoundId();
			string rhubarbXmlPathFromSoundPath = this.GetRhubarbXmlPathFromSoundPath(soundPath);
			this._agentVisuals[0].StartRhubarbRecord(rhubarbXmlPathFromSoundPath, soundId);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0001517B File Offset: 0x0001337B
		private void StopConversationSoundEvent()
		{
			if (this._conversationSoundEvent != null)
			{
				this._conversationSoundEvent.Stop();
				this._conversationSoundEvent = null;
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00015198 File Offset: 0x00013398
		private string GetRhubarbXmlPathFromSoundPath(string soundPath)
		{
			int length = soundPath.LastIndexOf('.');
			return soundPath.Substring(0, length) + ".xml";
		}

		// Token: 0x04000114 RID: 276
		private const float MinimumTimeRequiredToChangeIdleAction = 8f;

		// Token: 0x04000116 RID: 278
		private Scene _tableauScene;

		// Token: 0x04000117 RID: 279
		private float _animationFrequencyThreshold = 2.5f;

		// Token: 0x04000118 RID: 280
		private MatrixFrame _frame;

		// Token: 0x04000119 RID: 281
		private GameEntity _cameraEntity;

		// Token: 0x0400011A RID: 282
		private SoundEvent _conversationSoundEvent;

		// Token: 0x0400011B RID: 283
		private Camera _continuousRenderCamera;

		// Token: 0x0400011C RID: 284
		private MapConversationTableauData _data;

		// Token: 0x0400011D RID: 285
		private float _cameraRatio;

		// Token: 0x0400011E RID: 286
		private IMapConversationDataProvider _dataProvider;

		// Token: 0x0400011F RID: 287
		private bool _initialized;

		// Token: 0x04000120 RID: 288
		private Timer _changeIdleActionTimer;

		// Token: 0x04000121 RID: 289
		private int _tableauSizeX;

		// Token: 0x04000122 RID: 290
		private int _tableauSizeY;

		// Token: 0x04000123 RID: 291
		private uint _clothColor1 = new Color(1f, 1f, 1f, 1f).ToUnsignedInteger();

		// Token: 0x04000124 RID: 292
		private uint _clothColor2 = new Color(1f, 1f, 1f, 1f).ToUnsignedInteger();

		// Token: 0x04000125 RID: 293
		private List<AgentVisuals> _agentVisuals;

		// Token: 0x04000126 RID: 294
		private static readonly string fallbackAnimActName = "act_inventory_idle_start";

		// Token: 0x04000127 RID: 295
		private readonly string RainingEntityTag = "raining_entity";

		// Token: 0x04000128 RID: 296
		private readonly string SnowingEntityTag = "snowing_entity";

		// Token: 0x04000129 RID: 297
		private float _animationGap;

		// Token: 0x0400012A RID: 298
		private bool _isEnabled = true;

		// Token: 0x0400012B RID: 299
		private float RenderScale = 1f;

		// Token: 0x0400012C RID: 300
		private const float _baseCameraRatio = 1.7777778f;

		// Token: 0x0400012D RID: 301
		private float _baseCameraFOV = -1f;

		// Token: 0x0400012E RID: 302
		private string _cachedAtmosphereName = "";

		// Token: 0x0400012F RID: 303
		private string _opponentLeaderEquipmentCache;

		// Token: 0x0200007A RID: 122
		private struct DefaultConversationAnimationData
		{
			// Token: 0x040002E9 RID: 745
			public static readonly MapConversationTableau.DefaultConversationAnimationData Invalid = new MapConversationTableau.DefaultConversationAnimationData
			{
				ActionName = "",
				AnimationDataValid = false
			};

			// Token: 0x040002EA RID: 746
			public ConversationAnimData AnimationData;

			// Token: 0x040002EB RID: 747
			public string ActionName;

			// Token: 0x040002EC RID: 748
			public bool AnimationDataValid;
		}
	}
}

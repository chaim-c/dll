using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Helpers;
using SandBox.ViewModelCollection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;

namespace SandBox.View.Map
{
	// Token: 0x02000056 RID: 86
	public class PartyVisual
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0001D588 File Offset: 0x0001B788
		public MapScreen MapScreen
		{
			get
			{
				return MapScreen.Instance;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0001D58F File Offset: 0x0001B78F
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0001D597 File Offset: 0x0001B797
		public GameEntity StrategicEntity { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0001D5A0 File Offset: 0x0001B7A0
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0001D5A8 File Offset: 0x0001B7A8
		public List<GameEntity> TownPhysicalEntities { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0001D5B1 File Offset: 0x0001B7B1
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0001D5B9 File Offset: 0x0001B7B9
		public MatrixFrame CircleLocalFrame { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0001D5C2 File Offset: 0x0001B7C2
		public Vec2 Position
		{
			get
			{
				return this.PartyBase.Position2D;
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001D5CF File Offset: 0x0001B7CF
		public IMapEntity GetMapEntity()
		{
			return this.PartyBase.MapEntity;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0001D5DC File Offset: 0x0001B7DC
		public bool TargetVisibility
		{
			get
			{
				return this.PartyBase.IsVisible;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0001D5EC File Offset: 0x0001B7EC
		private Scene MapScene
		{
			get
			{
				if (this._mapScene == null && Campaign.Current != null && Campaign.Current.MapSceneWrapper != null)
				{
					this._mapScene = ((MapScene)Campaign.Current.MapSceneWrapper).Scene;
				}
				return this._mapScene;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0001D63A File Offset: 0x0001B83A
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0001D642 File Offset: 0x0001B842
		public AgentVisuals HumanAgentVisuals { get; private set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0001D64B File Offset: 0x0001B84B
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x0001D653 File Offset: 0x0001B853
		public AgentVisuals MountAgentVisuals { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0001D65C File Offset: 0x0001B85C
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x0001D664 File Offset: 0x0001B864
		public AgentVisuals CaravanMountAgentVisuals { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0001D66D File Offset: 0x0001B86D
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x0001D675 File Offset: 0x0001B875
		public bool IsEnemy { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0001D67E File Offset: 0x0001B87E
		// (set) Token: 0x060003CB RID: 971 RVA: 0x0001D686 File Offset: 0x0001B886
		public bool IsFriendly { get; private set; }

		// Token: 0x060003CC RID: 972 RVA: 0x0001D690 File Offset: 0x0001B890
		public bool IsEntityMovingVisually()
		{
			if (!this.PartyBase.IsMobile)
			{
				return false;
			}
			if (!this.PartyBase.MobileParty.VisualPosition2DWithoutError.NearlyEquals(this._lastFrameVisualPositionWithoutError, 1E-05f))
			{
				if (Campaign.Current.TimeControlMode != CampaignTimeControlMode.Stop)
				{
					this._lastFrameVisualPositionWithoutError = this.PartyBase.MobileParty.VisualPosition2DWithoutError;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001D6F8 File Offset: 0x0001B8F8
		public PartyVisual(PartyBase partyBase)
		{
			this.PartyBase = partyBase;
			this._siegeRangedMachineEntities = new List<ValueTuple<GameEntity, BattleSideEnum, int, MatrixFrame, GameEntity>>();
			this._siegeMeleeMachineEntities = new List<ValueTuple<GameEntity, BattleSideEnum, int, MatrixFrame, GameEntity>>();
			this._siegeMissileEntities = new List<ValueTuple<GameEntity, BattleSideEnum, int>>();
			this.CircleLocalFrame = MatrixFrame.Identity;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0001D74C File Offset: 0x0001B94C
		private void AddMountToPartyIcon(Vec3 positionOffset, string mountItemId, string harnessItemId, uint contourColor, CharacterObject character)
		{
			ItemObject @object = Game.Current.ObjectManager.GetObject<ItemObject>(mountItemId);
			Monster monster = @object.HorseComponent.Monster;
			ItemObject item = null;
			if (!string.IsNullOrEmpty(harnessItemId))
			{
				item = Game.Current.ObjectManager.GetObject<ItemObject>(harnessItemId);
			}
			Equipment equipment = new Equipment();
			equipment[EquipmentIndex.ArmorItemEndSlot] = new EquipmentElement(@object, null, null, false);
			equipment[EquipmentIndex.HorseHarness] = new EquipmentElement(item, null, null, false);
			AgentVisualsData data = new AgentVisualsData().Equipment(equipment).Scale(@object.ScaleFactor * 0.3f).Frame(new MatrixFrame(Mat3.Identity, positionOffset)).ActionSet(MBGlobals.GetActionSet(monster.ActionSetCode + "_map")).Scene(this.MapScene).Monster(monster).PrepareImmediately(false).UseScaledWeapons(true).HasClippingPlane(true).MountCreationKey(MountCreationKey.GetRandomMountKeyString(@object, character.GetMountKeySeed()));
			this.CaravanMountAgentVisuals = AgentVisuals.Create(data, "PartyIcon " + mountItemId, false, false, false);
			this.CaravanMountAgentVisuals.GetEntity().SetContourColor(new uint?(contourColor), false);
			MatrixFrame m = this.CaravanMountAgentVisuals.GetFrame();
			m.rotation.ApplyScaleLocal(this.CaravanMountAgentVisuals.GetScale());
			m = this.StrategicEntity.GetFrame().TransformToParent(m);
			this.CaravanMountAgentVisuals.GetEntity().SetFrame(ref m);
			float speed = MathF.Min(0.325f * this._speed / 0.3f, 20f);
			this.CaravanMountAgentVisuals.Tick(null, 0.0001f, this.IsEntityMovingVisually(), speed);
			this.CaravanMountAgentVisuals.GetEntity().Skeleton.ForceUpdateBoneFrames();
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001D904 File Offset: 0x0001BB04
		private void AddCharacterToPartyIcon(CharacterObject characterObject, uint contourColor, string bannerKey, int wieldedItemIndex, uint teamColor1, uint teamColor2, ActionIndexCache leaderAction, ActionIndexCache mountAction, float animationStartDuration, ref bool clearBannerEntityCache)
		{
			Equipment equipment = characterObject.Equipment.Clone(false);
			bool flag = !string.IsNullOrEmpty(bannerKey) && (((characterObject.IsPlayerCharacter || characterObject.HeroObject.Clan == Clan.PlayerClan) && Clan.PlayerClan.Tier >= Campaign.Current.Models.ClanTierModel.BannerEligibleTier) || (!characterObject.IsPlayerCharacter && (!characterObject.IsHero || (characterObject.IsHero && characterObject.HeroObject.Clan != Clan.PlayerClan))));
			int leftWieldedItemIndex = 4;
			if (flag)
			{
				ItemObject @object = Game.Current.ObjectManager.GetObject<ItemObject>("campaign_banner_small");
				equipment[EquipmentIndex.ExtraWeaponSlot] = new EquipmentElement(@object, null, null, false);
			}
			Monster baseMonsterFromRace = TaleWorlds.Core.FaceGen.GetBaseMonsterFromRace(characterObject.Race);
			MBActionSet actionSetWithSuffix = MBGlobals.GetActionSetWithSuffix(baseMonsterFromRace, characterObject.IsFemale, flag ? "_map_with_banner" : "_map");
			AgentVisualsData agentVisualsData = new AgentVisualsData().UseMorphAnims(true).Equipment(equipment).BodyProperties(characterObject.GetBodyProperties(characterObject.Equipment, -1)).SkeletonType(characterObject.IsFemale ? SkeletonType.Female : SkeletonType.Male).Scale(0.3f).Frame(this.StrategicEntity.GetFrame()).ActionSet(actionSetWithSuffix).Scene(this.MapScene).Monster(baseMonsterFromRace).PrepareImmediately(false).RightWieldedItemIndex(wieldedItemIndex).HasClippingPlane(true).UseScaledWeapons(true).ClothColor1(teamColor1).ClothColor2(teamColor2).CharacterObjectStringId(characterObject.StringId).AddColorRandomness(!characterObject.IsHero).Race(characterObject.Race);
			if (flag)
			{
				Banner banner = new Banner(bannerKey);
				agentVisualsData.Banner(banner).LeftWieldedItemIndex(leftWieldedItemIndex);
				if (this._cachedBannerEntity.Item1 == bannerKey + "campaign_banner_small")
				{
					agentVisualsData.CachedWeaponEntity(EquipmentIndex.ExtraWeaponSlot, this._cachedBannerEntity.Item2);
				}
			}
			this.HumanAgentVisuals = AgentVisuals.Create(agentVisualsData, "PartyIcon " + characterObject.Name, false, false, false);
			if (flag)
			{
				GameEntity entity = this.HumanAgentVisuals.GetEntity();
				GameEntity child = entity.GetChild(entity.ChildCount - 1);
				if (child.GetComponentCount(GameEntity.ComponentType.ClothSimulator) > 0)
				{
					clearBannerEntityCache = false;
					this._cachedBannerEntity = new ValueTuple<string, GameEntity>(bannerKey + "campaign_banner_small", child);
				}
			}
			if (leaderAction != ActionIndexCache.act_none)
			{
				float actionAnimationDuration = MBActionSet.GetActionAnimationDuration(actionSetWithSuffix, leaderAction);
				if (actionAnimationDuration < 1f)
				{
					this.HumanAgentVisuals.GetVisuals().GetSkeleton().SetAgentActionChannel(0, leaderAction, animationStartDuration, -0.2f, true);
				}
				else
				{
					this.HumanAgentVisuals.GetVisuals().GetSkeleton().SetAgentActionChannel(0, leaderAction, animationStartDuration / actionAnimationDuration, -0.2f, true);
				}
			}
			if (characterObject.HasMount())
			{
				Monster monster = characterObject.Equipment[EquipmentIndex.ArmorItemEndSlot].Item.HorseComponent.Monster;
				MBActionSet actionSet = MBGlobals.GetActionSet(monster.ActionSetCode + "_map");
				AgentVisualsData agentVisualsData2 = new AgentVisualsData().Equipment(characterObject.Equipment).Scale(characterObject.Equipment[EquipmentIndex.ArmorItemEndSlot].Item.ScaleFactor * 0.3f).Frame(MatrixFrame.Identity).ActionSet(actionSet).Scene(this.MapScene).Monster(monster).PrepareImmediately(false).UseScaledWeapons(true).HasClippingPlane(true).MountCreationKey(MountCreationKey.GetRandomMountKeyString(characterObject.Equipment[EquipmentIndex.ArmorItemEndSlot].Item, characterObject.GetMountKeySeed()));
				this.MountAgentVisuals = AgentVisuals.Create(agentVisualsData2, "PartyIcon " + characterObject.Name + " mount", false, false, false);
				if (mountAction != ActionIndexCache.act_none)
				{
					float actionAnimationDuration2 = MBActionSet.GetActionAnimationDuration(actionSet, mountAction);
					if (actionAnimationDuration2 < 1f)
					{
						this.MountAgentVisuals.GetEntity().Skeleton.SetAgentActionChannel(0, mountAction, animationStartDuration, -0.2f, true);
					}
					else
					{
						this.MountAgentVisuals.GetEntity().Skeleton.SetAgentActionChannel(0, mountAction, animationStartDuration / actionAnimationDuration2, -0.2f, true);
					}
				}
				this.MountAgentVisuals.GetEntity().SetContourColor(new uint?(contourColor), false);
				MatrixFrame frame = this.StrategicEntity.GetFrame();
				frame.rotation.ApplyScaleLocal(agentVisualsData2.ScaleData);
				this.MountAgentVisuals.GetEntity().SetFrame(ref frame);
			}
			this.HumanAgentVisuals.GetEntity().SetContourColor(new uint?(contourColor), false);
			MatrixFrame frame2 = this.StrategicEntity.GetFrame();
			frame2.rotation.ApplyScaleLocal(agentVisualsData.ScaleData);
			this.HumanAgentVisuals.GetEntity().SetFrame(ref frame2);
			float num = (this.MountAgentVisuals != null) ? 1.3f : 1f;
			float speed = MathF.Min(0.25f * num * this._speed / 0.3f, 20f);
			if (this.MountAgentVisuals != null)
			{
				this.MountAgentVisuals.Tick(null, 0.0001f, this.IsEntityMovingVisually(), speed);
				this.MountAgentVisuals.GetEntity().Skeleton.ForceUpdateBoneFrames();
			}
			this.HumanAgentVisuals.Tick(this.MountAgentVisuals, 0.0001f, this.IsEntityMovingVisually(), speed);
			this.HumanAgentVisuals.GetEntity().Skeleton.ForceUpdateBoneFrames();
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001DE54 File Offset: 0x0001C054
		private static MetaMesh GetBannerOfCharacter(Banner banner, string bannerMeshName)
		{
			MetaMesh copy = MetaMesh.GetCopy(bannerMeshName, true, false);
			for (int i = 0; i < copy.MeshCount; i++)
			{
				Mesh meshAtIndex = copy.GetMeshAtIndex(i);
				if (!meshAtIndex.HasTag("dont_use_tableau"))
				{
					Material material = meshAtIndex.GetMaterial();
					Material tableauMaterial = null;
					Tuple<Material, BannerCode> key = new Tuple<Material, BannerCode>(material, BannerCode.CreateFrom(banner));
					if (MapScreen.Instance._characterBannerMaterialCache.ContainsKey(key))
					{
						tableauMaterial = MapScreen.Instance._characterBannerMaterialCache[key];
					}
					else
					{
						tableauMaterial = material.CreateCopy();
						Action<Texture> setAction = delegate(Texture tex)
						{
							tableauMaterial.SetTexture(Material.MBTextureType.DiffuseMap2, tex);
							uint num = (uint)tableauMaterial.GetShader().GetMaterialShaderFlagMask("use_tableau_blending", true);
							ulong shaderFlags = tableauMaterial.GetShaderFlags();
							tableauMaterial.SetShaderFlags(shaderFlags | (ulong)num);
						};
						banner.GetTableauTextureLarge(setAction);
						MapScreen.Instance._characterBannerMaterialCache[key] = tableauMaterial;
					}
					meshAtIndex.SetMaterial(tableauMaterial);
				}
			}
			return copy;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001DF31 File Offset: 0x0001C131
		public void Tick(float dt, ref int dirtyPartiesCount, ref PartyVisual[] dirtyPartiesList)
		{
			if (this.PartyBase.IsSettlement)
			{
				this.TickSettlementVisual(dt, ref dirtyPartiesCount, ref dirtyPartiesList);
			}
			else
			{
				this.TickMobilePartyVisual(dt, ref dirtyPartiesCount, ref dirtyPartiesList);
			}
			if (this.PartyBase.LevelMaskIsDirty)
			{
				this.RefreshLevelMask();
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001DF68 File Offset: 0x0001C168
		private void TickSettlementVisual(float dt, ref int dirtyPartiesCount, ref PartyVisual[] dirtyPartiesList)
		{
			if (this.StrategicEntity == null)
			{
				return;
			}
			if (this.PartyBase.IsVisualDirty)
			{
				int num = Interlocked.Increment(ref dirtyPartiesCount);
				dirtyPartiesList[num] = this;
				return;
			}
			double toHours = CampaignTime.Now.ToHours;
			foreach (ValueTuple<GameEntity, BattleSideEnum, int> valueTuple in this._siegeMissileEntities)
			{
				GameEntity item = valueTuple.Item1;
				ISiegeEventSide siegeEventSide = this.PartyBase.Settlement.SiegeEvent.GetSiegeEventSide(valueTuple.Item2);
				int item2 = valueTuple.Item3;
				bool flag = false;
				if (siegeEventSide.SiegeEngineMissiles.Count > item2)
				{
					SiegeEvent.SiegeEngineMissile siegeEngineMissile = siegeEventSide.SiegeEngineMissiles[item2];
					double toHours2 = siegeEngineMissile.CollisionTime.ToHours;
					PartyVisual.SiegeBombardmentData siegeBombardmentData;
					this.CalculateDataAndDurationsForSiegeMachine(siegeEngineMissile.ShooterSlotIndex, siegeEngineMissile.ShooterSiegeEngineType, siegeEventSide.BattleSide, siegeEngineMissile.TargetType, siegeEngineMissile.TargetSlotIndex, out siegeBombardmentData);
					float num2 = siegeBombardmentData.MissileSpeed * MathF.Cos(siegeBombardmentData.LaunchAngle);
					if (toHours > toHours2 - (double)siegeBombardmentData.TotalDuration)
					{
						bool flag2 = toHours - (double)dt > toHours2 - (double)siegeBombardmentData.FlightDuration && toHours - (double)dt < toHours2;
						bool flag3 = toHours > toHours2 - (double)siegeBombardmentData.FlightDuration && toHours < toHours2;
						if (flag3)
						{
							flag = true;
							float num3 = (float)(toHours - (toHours2 - (double)siegeBombardmentData.FlightDuration));
							float num4 = siegeBombardmentData.MissileSpeed * MathF.Sin(siegeBombardmentData.LaunchAngle);
							Vec2 vec = new Vec2(num2 * num3, num4 * num3 - siegeBombardmentData.Gravity * 0.5f * num3 * num3);
							Vec3 vec2 = siegeBombardmentData.LaunchGlobalPosition + siegeBombardmentData.TargetAlignedShooterGlobalFrame.rotation.f.NormalizedCopy() * vec.x + siegeBombardmentData.TargetAlignedShooterGlobalFrame.rotation.u.NormalizedCopy() * vec.y;
							float num5 = num3 + 0.1f;
							Vec2 vec3 = new Vec2(num2 * num5, num4 * num5 - siegeBombardmentData.Gravity * 0.5f * num5 * num5);
							Vec3 v = siegeBombardmentData.LaunchGlobalPosition + siegeBombardmentData.TargetAlignedShooterGlobalFrame.rotation.f.NormalizedCopy() * vec3.x + siegeBombardmentData.TargetAlignedShooterGlobalFrame.rotation.u.NormalizedCopy() * vec3.y;
							Mat3 rotation = item.GetGlobalFrame().rotation;
							rotation.f = v - vec2;
							rotation.Orthonormalize();
							rotation.ApplyScaleLocal(this.MapScreen.PrefabEntityCache.GetScaleForSiegeEngine(siegeEngineMissile.ShooterSiegeEngineType, siegeEventSide.BattleSide));
							MatrixFrame matrixFrame = new MatrixFrame(rotation, vec2);
							item.SetGlobalFrame(matrixFrame);
						}
						item.GetChild(0).SetVisibilityExcludeParents(flag3);
						int soundCodeId = -1;
						if (!flag2 && flag3)
						{
							if (siegeEngineMissile.ShooterSiegeEngineType == DefaultSiegeEngineTypes.Ballista || siegeEngineMissile.ShooterSiegeEngineType == DefaultSiegeEngineTypes.FireBallista)
							{
								soundCodeId = MiscSoundContainer.SoundCodeAmbientNodeSiegeBallistaFire;
							}
							else if (siegeEngineMissile.ShooterSiegeEngineType == DefaultSiegeEngineTypes.Catapult || siegeEngineMissile.ShooterSiegeEngineType == DefaultSiegeEngineTypes.FireCatapult || siegeEngineMissile.ShooterSiegeEngineType == DefaultSiegeEngineTypes.Onager || siegeEngineMissile.ShooterSiegeEngineType == DefaultSiegeEngineTypes.FireOnager)
							{
								soundCodeId = MiscSoundContainer.SoundCodeAmbientNodeSiegeMangonelFire;
							}
							else
							{
								soundCodeId = MiscSoundContainer.SoundCodeAmbientNodeSiegeTrebuchetFire;
							}
						}
						else if (flag2 && !flag3)
						{
							this.StrategicEntity.Scene.CreateBurstParticle(ParticleSystemManager.GetRuntimeIdByName((siegeEngineMissile.TargetType == SiegeBombardTargets.RangedEngines) ? "psys_game_ballista_destruction" : "psys_campaign_boulder_stone_coll"), item.GetGlobalFrame());
							soundCodeId = ((siegeEngineMissile.ShooterSiegeEngineType == DefaultSiegeEngineTypes.Ballista || siegeEngineMissile.ShooterSiegeEngineType == DefaultSiegeEngineTypes.FireBallista) ? MiscSoundContainer.SoundCodeAmbientNodeSiegeBallistaHit : MiscSoundContainer.SoundCodeAmbientNodeSiegeBoulderHit);
						}
						MBSoundEvent.PlaySound(soundCodeId, item.GlobalPosition);
						if (toHours >= toHours2 - (double)(siegeBombardmentData.TotalDuration - siegeBombardmentData.RotationDuration - siegeBombardmentData.ReloadDuration))
						{
							if (toHours < toHours2 - (double)(siegeBombardmentData.TotalDuration - siegeBombardmentData.RotationDuration - siegeBombardmentData.ReloadDuration - siegeBombardmentData.AimingDuration))
							{
								if (siegeEventSide.SiegeEngines.DeployedRangedSiegeEngines[siegeEngineMissile.ShooterSlotIndex] == null || siegeEventSide.SiegeEngines.DeployedRangedSiegeEngines[siegeEngineMissile.ShooterSlotIndex].SiegeEngine != siegeEngineMissile.ShooterSiegeEngineType)
								{
									goto IL_64E;
								}
								using (List<ValueTuple<GameEntity, BattleSideEnum, int, MatrixFrame, GameEntity>>.Enumerator enumerator2 = this._siegeRangedMachineEntities.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										ValueTuple<GameEntity, BattleSideEnum, int, MatrixFrame, GameEntity> valueTuple2 = enumerator2.Current;
										if (!flag && valueTuple2.Item2 == siegeEventSide.BattleSide && valueTuple2.Item3 == siegeEngineMissile.ShooterSlotIndex)
										{
											GameEntity item3 = valueTuple2.Item5;
											if (item3 != null)
											{
												flag = true;
												GameEntity gameEntity = item;
												MatrixFrame matrixFrame2 = item3.GetGlobalFrame();
												matrixFrame2 = matrixFrame2.TransformToParent(item3.Skeleton.GetBoneEntitialFrame(Campaign.Current.Models.SiegeEventModel.GetSiegeEngineMapProjectileBoneIndex(siegeEngineMissile.ShooterSiegeEngineType, siegeEventSide.BattleSide), false));
												gameEntity.SetGlobalFrame(matrixFrame2);
											}
										}
									}
									goto IL_64E;
								}
							}
							if (toHours < toHours2 - (double)(siegeBombardmentData.TotalDuration - siegeBombardmentData.RotationDuration - siegeBombardmentData.ReloadDuration - siegeBombardmentData.AimingDuration - siegeBombardmentData.FireDuration) && !flag3 && siegeEventSide.SiegeEngines.DeployedRangedSiegeEngines[siegeEngineMissile.ShooterSlotIndex] != null && siegeEventSide.SiegeEngines.DeployedRangedSiegeEngines[siegeEngineMissile.ShooterSlotIndex].SiegeEngine == siegeEngineMissile.ShooterSiegeEngineType)
							{
								foreach (ValueTuple<GameEntity, BattleSideEnum, int, MatrixFrame, GameEntity> valueTuple3 in this._siegeRangedMachineEntities)
								{
									if (!flag && valueTuple3.Item2 == siegeEventSide.BattleSide && valueTuple3.Item3 == siegeEngineMissile.ShooterSlotIndex)
									{
										GameEntity item4 = valueTuple3.Item5;
										if (item4 != null)
										{
											flag = true;
											GameEntity gameEntity2 = item;
											MatrixFrame matrixFrame2 = item4.GetGlobalFrame();
											matrixFrame2 = matrixFrame2.TransformToParent(item4.Skeleton.GetBoneEntitialFrame(Campaign.Current.Models.SiegeEventModel.GetSiegeEngineMapProjectileBoneIndex(siegeEngineMissile.ShooterSiegeEngineType, siegeEventSide.BattleSide), false));
											gameEntity2.SetGlobalFrame(matrixFrame2);
										}
									}
								}
							}
						}
					}
				}
				IL_64E:
				item.SetVisibilityExcludeParents(flag);
			}
			foreach (ValueTuple<GameEntity, BattleSideEnum, int, MatrixFrame, GameEntity> valueTuple4 in this._siegeRangedMachineEntities)
			{
				GameEntity item5 = valueTuple4.Item1;
				BattleSideEnum item6 = valueTuple4.Item2;
				int item7 = valueTuple4.Item3;
				GameEntity item8 = valueTuple4.Item5;
				SiegeEngineType siegeEngine = this.PartyBase.Settlement.SiegeEvent.GetSiegeEventSide(item6).SiegeEngines.DeployedRangedSiegeEngines[item7].SiegeEngine;
				if (item8 != null)
				{
					Skeleton skeleton = item8.Skeleton;
					string siegeEngineMapFireAnimationName = Campaign.Current.Models.SiegeEventModel.GetSiegeEngineMapFireAnimationName(siegeEngine, item6);
					string siegeEngineMapReloadAnimationName = Campaign.Current.Models.SiegeEventModel.GetSiegeEngineMapReloadAnimationName(siegeEngine, item6);
					SiegeEvent.RangedSiegeEngine rangedSiegeEngine = this.PartyBase.Settlement.SiegeEvent.GetSiegeEventSide(item6).SiegeEngines.DeployedRangedSiegeEngines[item7].RangedSiegeEngine;
					PartyVisual.SiegeBombardmentData siegeBombardmentData2;
					this.CalculateDataAndDurationsForSiegeMachine(item7, siegeEngine, item6, rangedSiegeEngine.CurrentTargetType, rangedSiegeEngine.CurrentTargetIndex, out siegeBombardmentData2);
					MatrixFrame shooterGlobalFrame = siegeBombardmentData2.ShooterGlobalFrame;
					if (rangedSiegeEngine.PreviousTargetIndex >= 0)
					{
						Vec3 v2;
						if (rangedSiegeEngine.PreviousDamagedTargetType == SiegeBombardTargets.Wall)
						{
							v2 = this._defenderBreachableWallEntitiesCacheForCurrentLevel[rangedSiegeEngine.PreviousTargetIndex].GlobalPosition;
						}
						else
						{
							v2 = ((item6 == BattleSideEnum.Attacker) ? this._defenderRangedEngineSpawnEntitiesCacheForCurrentLevel[rangedSiegeEngine.PreviousTargetIndex].GetGlobalFrame().origin : this._attackerRangedEngineSpawnEntities[rangedSiegeEngine.PreviousTargetIndex].GetGlobalFrame().origin);
						}
						shooterGlobalFrame.rotation.f.AsVec2 = (v2 - shooterGlobalFrame.origin).AsVec2;
						shooterGlobalFrame.rotation.f.NormalizeWithoutChangingZ();
						shooterGlobalFrame.rotation.Orthonormalize();
					}
					item5.SetGlobalFrame(shooterGlobalFrame);
					skeleton.TickAnimations(dt, MatrixFrame.Identity, false);
					double toHours3 = rangedSiegeEngine.NextProjectileCollisionTime.ToHours;
					if (toHours > toHours3 - (double)siegeBombardmentData2.TotalDuration)
					{
						if (toHours < toHours3 - (double)(siegeBombardmentData2.TotalDuration - siegeBombardmentData2.RotationDuration))
						{
							float rotationInRadians = (siegeBombardmentData2.TargetPosition - shooterGlobalFrame.origin).AsVec2.RotationInRadians;
							float rotationInRadians2 = shooterGlobalFrame.rotation.f.AsVec2.RotationInRadians;
							float f = rotationInRadians - rotationInRadians2;
							float num6 = MathF.Abs(f);
							float num7 = (float)(toHours3 - (double)(siegeBombardmentData2.TotalDuration - siegeBombardmentData2.RotationDuration) - toHours);
							if (num6 > num7 * 2f)
							{
								shooterGlobalFrame.rotation.f.AsVec2 = Vec2.FromRotation(rotationInRadians2 + (float)MathF.Sign(f) * (num6 - num7 * 2f));
								shooterGlobalFrame.rotation.f.NormalizeWithoutChangingZ();
								shooterGlobalFrame.rotation.Orthonormalize();
								item5.SetGlobalFrame(shooterGlobalFrame);
							}
						}
						else if (toHours < toHours3 - (double)(siegeBombardmentData2.TotalDuration - siegeBombardmentData2.RotationDuration - siegeBombardmentData2.ReloadDuration))
						{
							item5.SetGlobalFrame(siegeBombardmentData2.TargetAlignedShooterGlobalFrame);
							skeleton.SetAnimationAtChannel(siegeEngineMapReloadAnimationName, 0, 1f, 0f, (float)((toHours - (toHours3 - (double)(siegeBombardmentData2.TotalDuration - siegeBombardmentData2.RotationDuration))) / (double)siegeBombardmentData2.ReloadDuration));
						}
						else if (toHours < toHours3 - (double)(siegeBombardmentData2.TotalDuration - siegeBombardmentData2.RotationDuration - siegeBombardmentData2.ReloadDuration - siegeBombardmentData2.AimingDuration))
						{
							item5.SetGlobalFrame(siegeBombardmentData2.TargetAlignedShooterGlobalFrame);
							skeleton.SetAnimationAtChannel(siegeEngineMapReloadAnimationName, 0, 1f, 0f, 1f);
						}
						else if (toHours < toHours3 - (double)(siegeBombardmentData2.TotalDuration - siegeBombardmentData2.RotationDuration - siegeBombardmentData2.ReloadDuration - siegeBombardmentData2.AimingDuration - siegeBombardmentData2.FireDuration))
						{
							item5.SetGlobalFrame(siegeBombardmentData2.TargetAlignedShooterGlobalFrame);
							skeleton.SetAnimationAtChannel(siegeEngineMapFireAnimationName, 0, 1f, 0f, (float)((toHours - (toHours3 - (double)(siegeBombardmentData2.TotalDuration - siegeBombardmentData2.RotationDuration - siegeBombardmentData2.ReloadDuration - siegeBombardmentData2.AimingDuration))) / (double)siegeBombardmentData2.FireDuration));
						}
						else
						{
							item5.SetGlobalFrame(siegeBombardmentData2.TargetAlignedShooterGlobalFrame);
							skeleton.SetAnimationAtChannel(siegeEngineMapFireAnimationName, 0, 1f, 0f, 1f);
						}
					}
				}
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001EA90 File Offset: 0x0001CC90
		private void TickMobilePartyVisual(float dt, ref int dirtyPartiesCount, ref PartyVisual[] dirtyPartiesList)
		{
			if (this.StrategicEntity == null)
			{
				return;
			}
			if (this.PartyBase.IsVisualDirty && (this._entityAlpha > 0f || this.TargetVisibility))
			{
				int num = Interlocked.Increment(ref dirtyPartiesCount);
				dirtyPartiesList[num] = this;
			}
			this._speed = this.PartyBase.MobileParty.Speed;
			if (this._entityAlpha > 0f && this.HumanAgentVisuals != null && !this.HumanAgentVisuals.GetEquipment()[EquipmentIndex.ExtraWeaponSlot].IsEmpty)
			{
				this.HumanAgentVisuals.SetClothWindToWeaponAtIndex(-this.StrategicEntity.GetGlobalFrame().rotation.f, false, EquipmentIndex.ExtraWeaponSlot);
			}
			float num2 = (this.MountAgentVisuals != null) ? 1.3f : 1f;
			float speed = MathF.Min(0.25f * num2 * this._speed / 0.3f, 20f);
			bool isEntityMoving = this.IsEntityMovingVisually();
			AgentVisuals humanAgentVisuals = this.HumanAgentVisuals;
			if (humanAgentVisuals != null)
			{
				humanAgentVisuals.Tick(this.MountAgentVisuals, dt, isEntityMoving, speed);
			}
			AgentVisuals mountAgentVisuals = this.MountAgentVisuals;
			if (mountAgentVisuals != null)
			{
				mountAgentVisuals.Tick(null, dt, isEntityMoving, speed);
			}
			AgentVisuals caravanMountAgentVisuals = this.CaravanMountAgentVisuals;
			if (caravanMountAgentVisuals != null)
			{
				caravanMountAgentVisuals.Tick(null, dt, isEntityMoving, speed);
			}
			if (this.IsVisibleOrFadingOut())
			{
				MobileParty mobileParty = this.PartyBase.MobileParty;
				MatrixFrame identity = MatrixFrame.Identity;
				identity.origin = this.GetVisualPosition();
				if (mobileParty.Army != null && mobileParty.Army.LeaderParty.AttachedParties.Contains(mobileParty))
				{
					MatrixFrame frame = this.GetFrame();
					Vec2 v = identity.origin.AsVec2 - frame.origin.AsVec2;
					if (v.Length / dt > 20f)
					{
						identity.rotation.RotateAboutUp(this.PartyBase.AverageBearingRotation);
					}
					else if (mobileParty.CurrentSettlement == null)
					{
						float a = MBMath.LerpRadians(frame.rotation.f.AsVec2.RotationInRadians, (v + Vec2.FromRotation(this.PartyBase.AverageBearingRotation) * 0.01f).RotationInRadians, 6f * dt, 0.03f * dt, 10f * dt);
						identity.rotation.RotateAboutUp(a);
					}
					else
					{
						float rotationInRadians = frame.rotation.f.AsVec2.RotationInRadians;
						identity.rotation.RotateAboutUp(rotationInRadians);
					}
				}
				else if (mobileParty.CurrentSettlement == null)
				{
					identity.rotation.RotateAboutUp(this.PartyBase.AverageBearingRotation);
				}
				this.SetFrame(ref identity);
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001ED3C File Offset: 0x0001CF3C
		public Vec3 GetVisualPosition()
		{
			float z = 0f;
			Vec2 zero = Vec2.Zero;
			if (this.PartyBase.IsMobile)
			{
				MobileParty mobileParty = this.PartyBase.MobileParty;
				zero = new Vec2(mobileParty.EventPositionAdder.x + mobileParty.ArmyPositionAdder.x + mobileParty.ErrorPosition.x, mobileParty.EventPositionAdder.y + mobileParty.ArmyPositionAdder.y + mobileParty.ErrorPosition.y);
			}
			Vec2 vec = new Vec2(this.PartyBase.Position2D.x + zero.x, this.PartyBase.Position2D.y + zero.y);
			Campaign.Current.MapSceneWrapper.GetHeightAtPoint(vec, ref z);
			return new Vec3(vec, z, -1f);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001EE10 File Offset: 0x0001D010
		private void CalculateDataAndDurationsForSiegeMachine(int machineSlotIndex, SiegeEngineType machineType, BattleSideEnum side, SiegeBombardTargets targetType, int targetSlotIndex, out PartyVisual.SiegeBombardmentData bombardmentData)
		{
			bombardmentData = default(PartyVisual.SiegeBombardmentData);
			MatrixFrame shooterGlobalFrame = (side == BattleSideEnum.Defender) ? this._defenderRangedEngineSpawnEntitiesCacheForCurrentLevel[machineSlotIndex].GetGlobalFrame() : this._attackerRangedEngineSpawnEntities[machineSlotIndex].GetGlobalFrame();
			shooterGlobalFrame.rotation.MakeUnit();
			bombardmentData.ShooterGlobalFrame = shooterGlobalFrame;
			string siegeEngineMapFireAnimationName = Campaign.Current.Models.SiegeEventModel.GetSiegeEngineMapFireAnimationName(machineType, side);
			string siegeEngineMapReloadAnimationName = Campaign.Current.Models.SiegeEventModel.GetSiegeEngineMapReloadAnimationName(machineType, side);
			bombardmentData.ReloadDuration = MBAnimation.GetAnimationDuration(siegeEngineMapReloadAnimationName) * 0.25f;
			bombardmentData.AimingDuration = 0.25f;
			bombardmentData.RotationDuration = 0.4f;
			bombardmentData.FireDuration = MBAnimation.GetAnimationDuration(siegeEngineMapFireAnimationName) * 0.25f;
			float animationParameter = MBAnimation.GetAnimationParameter1(siegeEngineMapFireAnimationName);
			bombardmentData.MissileLaunchDuration = bombardmentData.FireDuration * animationParameter;
			bombardmentData.MissileSpeed = 14f;
			bombardmentData.Gravity = ((machineType == DefaultSiegeEngineTypes.Ballista || machineType == DefaultSiegeEngineTypes.FireBallista) ? 10f : 40f);
			if (targetType == SiegeBombardTargets.RangedEngines)
			{
				bombardmentData.TargetPosition = ((side == BattleSideEnum.Attacker) ? this._defenderRangedEngineSpawnEntitiesCacheForCurrentLevel[targetSlotIndex].GetGlobalFrame().origin : this._attackerRangedEngineSpawnEntities[targetSlotIndex].GetGlobalFrame().origin);
			}
			else if (targetType == SiegeBombardTargets.Wall)
			{
				bombardmentData.TargetPosition = this._defenderBreachableWallEntitiesCacheForCurrentLevel[targetSlotIndex].GlobalPosition;
			}
			else if (targetSlotIndex == -1)
			{
				bombardmentData.TargetPosition = Vec3.Zero;
			}
			else
			{
				bombardmentData.TargetPosition = ((side == BattleSideEnum.Attacker) ? this._defenderRangedEngineSpawnEntitiesCacheForCurrentLevel[targetSlotIndex].GetGlobalFrame().origin : this._attackerRangedEngineSpawnEntities[targetSlotIndex].GetGlobalFrame().origin);
				bombardmentData.TargetPosition += (bombardmentData.TargetPosition - bombardmentData.ShooterGlobalFrame.origin).NormalizedCopy() * 2f;
				Campaign.Current.MapSceneWrapper.GetHeightAtPoint(bombardmentData.TargetPosition.AsVec2, ref bombardmentData.TargetPosition.z);
			}
			bombardmentData.TargetAlignedShooterGlobalFrame = bombardmentData.ShooterGlobalFrame;
			bombardmentData.TargetAlignedShooterGlobalFrame.rotation.f.AsVec2 = (bombardmentData.TargetPosition - bombardmentData.ShooterGlobalFrame.origin).AsVec2;
			bombardmentData.TargetAlignedShooterGlobalFrame.rotation.f.NormalizeWithoutChangingZ();
			bombardmentData.TargetAlignedShooterGlobalFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
			bombardmentData.LaunchGlobalPosition = bombardmentData.TargetAlignedShooterGlobalFrame.TransformToParent(this.MapScreen.PrefabEntityCache.GetLaunchEntitialFrameForSiegeEngine(machineType, side).origin);
			float lengthSquared = (bombardmentData.LaunchGlobalPosition.AsVec2 - bombardmentData.TargetPosition.AsVec2).LengthSquared;
			float num = MathF.Sqrt(lengthSquared);
			float num2 = bombardmentData.LaunchGlobalPosition.z - bombardmentData.TargetPosition.z;
			float num3 = bombardmentData.MissileSpeed * bombardmentData.MissileSpeed;
			float num4 = num3 * num3;
			float num5 = num4 - bombardmentData.Gravity * (bombardmentData.Gravity * lengthSquared - 2f * num2 * num3);
			if (num5 >= 0f)
			{
				bombardmentData.LaunchAngle = MathF.Atan((num3 - MathF.Sqrt(num5)) / (bombardmentData.Gravity * num));
			}
			else
			{
				bombardmentData.Gravity = 1f;
				num5 = num4 - bombardmentData.Gravity * (bombardmentData.Gravity * lengthSquared - 2f * num2 * num3);
				bombardmentData.LaunchAngle = MathF.Atan((num3 - MathF.Sqrt(num5)) / (bombardmentData.Gravity * num));
			}
			float num6 = bombardmentData.MissileSpeed * MathF.Cos(bombardmentData.LaunchAngle);
			bombardmentData.FlightDuration = num / num6;
			bombardmentData.TotalDuration = bombardmentData.RotationDuration + bombardmentData.ReloadDuration + bombardmentData.AimingDuration + bombardmentData.MissileLaunchDuration + bombardmentData.FlightDuration;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0001F204 File Offset: 0x0001D404
		private void RemoveContourMesh()
		{
			if (this._contourMaskMesh != null)
			{
				this.MapScreen.ContourMaskEntity.RemoveComponentWithMesh(this._contourMaskMesh);
				this._contourMaskMesh = null;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0001F232 File Offset: 0x0001D432
		public void ReleaseResources()
		{
			this.RemoveSiege();
			this.ResetPartyIcon();
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001F240 File Offset: 0x0001D440
		public void ValidateIsDirty(float realDt, float dt)
		{
			if (this.PartyBase.IsSettlement)
			{
				this.RefreshPartyIcon();
				PartyVisualManager.Current.RegisterFadingVisual(this);
				return;
			}
			if (this.PartyBase.MemberRoster.TotalManCount != 0)
			{
				this.RefreshPartyIcon();
				if ((this._entityAlpha < 1f && this.TargetVisibility) || (this._entityAlpha > 0f && !this.TargetVisibility))
				{
					PartyVisualManager.Current.RegisterFadingVisual(this);
					return;
				}
			}
			else
			{
				this.ResetPartyIcon();
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001F2C0 File Offset: 0x0001D4C0
		public void TickFadingState(float realDt, float dt)
		{
			if ((this._entityAlpha < 1f && this.TargetVisibility) || (this._entityAlpha > 0f && !this.TargetVisibility))
			{
				if (this.TargetVisibility)
				{
					if (this._entityAlpha <= 0f)
					{
						this.StrategicEntity.SetVisibilityExcludeParents(true);
						AgentVisuals humanAgentVisuals = this.HumanAgentVisuals;
						if (humanAgentVisuals != null)
						{
							GameEntity entity = humanAgentVisuals.GetEntity();
							if (entity != null)
							{
								entity.SetVisibilityExcludeParents(true);
							}
						}
						AgentVisuals mountAgentVisuals = this.MountAgentVisuals;
						if (mountAgentVisuals != null)
						{
							GameEntity entity2 = mountAgentVisuals.GetEntity();
							if (entity2 != null)
							{
								entity2.SetVisibilityExcludeParents(true);
							}
						}
						AgentVisuals caravanMountAgentVisuals = this.CaravanMountAgentVisuals;
						if (caravanMountAgentVisuals != null)
						{
							GameEntity entity3 = caravanMountAgentVisuals.GetEntity();
							if (entity3 != null)
							{
								entity3.SetVisibilityExcludeParents(true);
							}
						}
					}
					this._entityAlpha = MathF.Min(this._entityAlpha + realDt * 2f, 1f);
					this.StrategicEntity.SetAlpha(this._entityAlpha);
					AgentVisuals humanAgentVisuals2 = this.HumanAgentVisuals;
					if (humanAgentVisuals2 != null)
					{
						GameEntity entity4 = humanAgentVisuals2.GetEntity();
						if (entity4 != null)
						{
							entity4.SetAlpha(this._entityAlpha);
						}
					}
					AgentVisuals mountAgentVisuals2 = this.MountAgentVisuals;
					if (mountAgentVisuals2 != null)
					{
						GameEntity entity5 = mountAgentVisuals2.GetEntity();
						if (entity5 != null)
						{
							entity5.SetAlpha(this._entityAlpha);
						}
					}
					AgentVisuals caravanMountAgentVisuals2 = this.CaravanMountAgentVisuals;
					if (caravanMountAgentVisuals2 != null)
					{
						GameEntity entity6 = caravanMountAgentVisuals2.GetEntity();
						if (entity6 != null)
						{
							entity6.SetAlpha(this._entityAlpha);
						}
					}
					this.StrategicEntity.EntityFlags &= ~EntityFlags.DoNotTick;
					return;
				}
				this._entityAlpha = MathF.Max(this._entityAlpha - realDt * 2f, 0f);
				this.StrategicEntity.SetAlpha(this._entityAlpha);
				AgentVisuals humanAgentVisuals3 = this.HumanAgentVisuals;
				if (humanAgentVisuals3 != null)
				{
					GameEntity entity7 = humanAgentVisuals3.GetEntity();
					if (entity7 != null)
					{
						entity7.SetAlpha(this._entityAlpha);
					}
				}
				AgentVisuals mountAgentVisuals3 = this.MountAgentVisuals;
				if (mountAgentVisuals3 != null)
				{
					GameEntity entity8 = mountAgentVisuals3.GetEntity();
					if (entity8 != null)
					{
						entity8.SetAlpha(this._entityAlpha);
					}
				}
				AgentVisuals caravanMountAgentVisuals3 = this.CaravanMountAgentVisuals;
				if (caravanMountAgentVisuals3 != null)
				{
					GameEntity entity9 = caravanMountAgentVisuals3.GetEntity();
					if (entity9 != null)
					{
						entity9.SetAlpha(this._entityAlpha);
					}
				}
				if (this._entityAlpha <= 0f)
				{
					this.StrategicEntity.SetVisibilityExcludeParents(false);
					AgentVisuals humanAgentVisuals4 = this.HumanAgentVisuals;
					if (humanAgentVisuals4 != null)
					{
						GameEntity entity10 = humanAgentVisuals4.GetEntity();
						if (entity10 != null)
						{
							entity10.SetVisibilityExcludeParents(false);
						}
					}
					AgentVisuals mountAgentVisuals4 = this.MountAgentVisuals;
					if (mountAgentVisuals4 != null)
					{
						GameEntity entity11 = mountAgentVisuals4.GetEntity();
						if (entity11 != null)
						{
							entity11.SetVisibilityExcludeParents(false);
						}
					}
					AgentVisuals caravanMountAgentVisuals4 = this.CaravanMountAgentVisuals;
					if (caravanMountAgentVisuals4 != null)
					{
						GameEntity entity12 = caravanMountAgentVisuals4.GetEntity();
						if (entity12 != null)
						{
							entity12.SetVisibilityExcludeParents(false);
						}
					}
					this.StrategicEntity.EntityFlags |= EntityFlags.DoNotTick;
					return;
				}
			}
			else
			{
				PartyVisualManager.Current.UnRegisterFadingVisual(this);
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001F550 File Offset: 0x0001D750
		public void ResetPartyIcon()
		{
			if (this.StrategicEntity != null)
			{
				this.RemoveContourMesh();
			}
			if (this.HumanAgentVisuals != null)
			{
				this.HumanAgentVisuals.Reset();
				this.HumanAgentVisuals = null;
			}
			if (this.MountAgentVisuals != null)
			{
				this.MountAgentVisuals.Reset();
				this.MountAgentVisuals = null;
			}
			if (this.CaravanMountAgentVisuals != null)
			{
				this.CaravanMountAgentVisuals.Reset();
				this.CaravanMountAgentVisuals = null;
			}
			if (this.StrategicEntity != null)
			{
				if ((this.StrategicEntity.EntityFlags & EntityFlags.Ignore) != (EntityFlags)0U)
				{
					this.StrategicEntity.RemoveFromPredisplayEntity();
				}
				this.StrategicEntity.ClearComponents();
			}
			PartyVisualManager.Current.UnRegisterFadingVisual(this);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001F604 File Offset: 0x0001D804
		private void RefreshPartyIcon()
		{
			if (this.PartyBase.IsVisualDirty)
			{
				this.PartyBase.OnVisualsUpdated();
				bool flag = true;
				bool flag2 = true;
				if (!this.PartyBase.IsSettlement)
				{
					this.ResetPartyIcon();
					MatrixFrame circleLocalFrame = this.CircleLocalFrame;
					circleLocalFrame.origin = Vec3.Zero;
					this.CircleLocalFrame = circleLocalFrame;
				}
				else
				{
					this.RemoveSiege();
					this.StrategicEntity.RemoveAllParticleSystems();
					this.StrategicEntity.EntityFlags |= EntityFlags.DoNotTick;
				}
				MobileParty mobileParty = this.PartyBase.MobileParty;
				if (((mobileParty != null) ? mobileParty.CurrentSettlement : null) != null)
				{
					Dictionary<int, List<GameEntity>> gateBannerEntitiesWithLevels = PartyVisualManager.Current.GetVisualOfParty(this.PartyBase.MobileParty.CurrentSettlement.Party)._gateBannerEntitiesWithLevels;
					if (!this.PartyBase.MobileParty.MapFaction.IsAtWarWith(this.PartyBase.MobileParty.CurrentSettlement.MapFaction) && gateBannerEntitiesWithLevels != null && !gateBannerEntitiesWithLevels.IsEmpty<KeyValuePair<int, List<GameEntity>>>())
					{
						Hero leaderHero = this.PartyBase.LeaderHero;
						if (((leaderHero != null) ? leaderHero.ClanBanner : null) != null)
						{
							string text = this.PartyBase.LeaderHero.ClanBanner.Serialize();
							if (string.IsNullOrEmpty(text))
							{
								goto IL_694;
							}
							int num = 0;
							foreach (MobileParty mobileParty2 in this.PartyBase.MobileParty.CurrentSettlement.Parties)
							{
								if (mobileParty2 == this.PartyBase.MobileParty)
								{
									break;
								}
								Hero leaderHero2 = mobileParty2.LeaderHero;
								if (((leaderHero2 != null) ? leaderHero2.ClanBanner : null) != null)
								{
									num++;
								}
							}
							MatrixFrame matrixFrame = MatrixFrame.Identity;
							int wallLevel = this.PartyBase.MobileParty.CurrentSettlement.Town.GetWallLevel();
							int count = gateBannerEntitiesWithLevels[wallLevel].Count;
							if (count == 0)
							{
								Debug.FailedAssert(string.Format("{0} - has no Banner Entities at level {1}.", this.PartyBase.MobileParty.CurrentSettlement.Name, wallLevel), "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.View\\Map\\PartyVisual.cs", "RefreshPartyIcon", 1060);
								goto IL_694;
							}
							GameEntity gameEntity = gateBannerEntitiesWithLevels[wallLevel][num % count];
							GameEntity child = gameEntity.GetChild(0);
							MatrixFrame matrixFrame2 = (child != null) ? child.GetGlobalFrame() : gameEntity.GetGlobalFrame();
							num /= count;
							int num2 = this.PartyBase.MobileParty.CurrentSettlement.Parties.Count(delegate(MobileParty p)
							{
								Hero leaderHero3 = p.LeaderHero;
								return ((leaderHero3 != null) ? leaderHero3.ClanBanner : null) != null;
							});
							float f = 0.75f / (float)MathF.Max(1, num2 / (count * 2));
							int num3 = (num % 2 == 0) ? -1 : 1;
							Vec3 v = matrixFrame2.rotation.f / 2f * (float)num3;
							if (v.Length < matrixFrame2.rotation.s.Length)
							{
								v = matrixFrame2.rotation.s / 2f * (float)num3;
							}
							matrixFrame.origin = matrixFrame2.origin + v * (float)((num + 1) / 2) * (float)(num % 2 * 2 - 1) * f * (float)num3;
							MatrixFrame globalFrame = this.StrategicEntity.GetGlobalFrame();
							matrixFrame.origin = globalFrame.TransformToLocal(matrixFrame.origin);
							float num4 = MBMath.Map((float)this.PartyBase.NumberOfAllMembers / 400f * ((this.PartyBase.MobileParty.Army != null && this.PartyBase.MobileParty.Army.LeaderParty == this.PartyBase.MobileParty) ? 1.25f : 1f), 0f, 1f, 0.2f, 0.5f);
							matrixFrame = matrixFrame.Elevate(-num4);
							matrixFrame.rotation.ApplyScaleLocal(num4);
							globalFrame = this.StrategicEntity.GetGlobalFrame();
							matrixFrame.rotation = globalFrame.rotation.TransformToLocal(matrixFrame.rotation);
							this.StrategicEntity.AddSphereAsBody(matrixFrame.origin + Vec3.Up * 0.3f, 0.15f, BodyFlags.None);
							flag = false;
							string text2 = "campaign_flag";
							if (this._cachedBannerComponent.Item1 == text + text2)
							{
								this._cachedBannerComponent.Item2.GetFirstMetaMesh().Frame = matrixFrame;
								this.StrategicEntity.AddComponent(this._cachedBannerComponent.Item2);
								goto IL_694;
							}
							MetaMesh bannerOfCharacter = PartyVisual.GetBannerOfCharacter(new Banner(text), text2);
							bannerOfCharacter.Frame = matrixFrame;
							int componentCount = this.StrategicEntity.GetComponentCount(GameEntity.ComponentType.ClothSimulator);
							this.StrategicEntity.AddMultiMesh(bannerOfCharacter, true);
							if (this.StrategicEntity.GetComponentCount(GameEntity.ComponentType.ClothSimulator) > componentCount)
							{
								this._cachedBannerComponent.Item1 = text + text2;
								this._cachedBannerComponent.Item2 = this.StrategicEntity.GetComponentAtIndex(componentCount, GameEntity.ComponentType.ClothSimulator);
								goto IL_694;
							}
							goto IL_694;
						}
					}
					this.StrategicEntity.RemovePhysics(false);
				}
				else
				{
					this.IsEnemy = (this.PartyBase.MapFaction != null && FactionManager.IsAtWarAgainstFaction(this.PartyBase.MapFaction, Hero.MainHero.MapFaction));
					this.IsFriendly = (this.PartyBase.MapFaction != null && FactionManager.IsAlliedWithFaction(this.PartyBase.MapFaction, Hero.MainHero.MapFaction));
					this.InitializePartyCollider(this.PartyBase);
					if (this.PartyBase.IsSettlement)
					{
						if (this.PartyBase.Settlement.IsFortification)
						{
							this.UpdateDefenderSiegeEntitiesCache();
						}
						this.AddSiegeIconComponents(this.PartyBase);
						this.SetSettlementLevelVisibility();
						this.RefreshWallState();
						this.RefreshTownPhysicalEntitiesState(this.PartyBase);
						this.RefreshSiegePreparations(this.PartyBase);
						if (this.PartyBase.Settlement.IsVillage)
						{
							MapEvent mapEvent = this.PartyBase.MapEvent;
							if (mapEvent != null && mapEvent.IsRaid)
							{
								this.StrategicEntity.EntityFlags &= ~EntityFlags.DoNotTick;
								this.StrategicEntity.AddParticleSystemComponent("psys_fire_smoke_env_point");
							}
							else if (this.PartyBase.Settlement.IsRaided)
							{
								this.StrategicEntity.EntityFlags &= ~EntityFlags.DoNotTick;
								this.StrategicEntity.AddParticleSystemComponent("map_icon_village_plunder_fx");
							}
						}
					}
					else
					{
						this.AddMobileIconComponents(this.PartyBase, ref flag2, ref flag2);
					}
				}
				IL_694:
				if (flag)
				{
					this._cachedBannerComponent = new ValueTuple<string, GameEntityComponent>(null, null);
				}
				if (flag2)
				{
					this._cachedBannerEntity = new ValueTuple<string, GameEntity>(null, null);
				}
				this.StrategicEntity.CheckResources(true, false);
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001FCE4 File Offset: 0x0001DEE4
		private void RemoveSiege()
		{
			foreach (ValueTuple<GameEntity, BattleSideEnum, int, MatrixFrame, GameEntity> valueTuple in this._siegeRangedMachineEntities)
			{
				this.StrategicEntity.RemoveChild(valueTuple.Item1, false, false, true, 36);
			}
			foreach (ValueTuple<GameEntity, BattleSideEnum, int> valueTuple2 in this._siegeMissileEntities)
			{
				this.StrategicEntity.RemoveChild(valueTuple2.Item1, false, false, true, 37);
			}
			foreach (ValueTuple<GameEntity, BattleSideEnum, int, MatrixFrame, GameEntity> valueTuple3 in this._siegeMeleeMachineEntities)
			{
				this.StrategicEntity.RemoveChild(valueTuple3.Item1, false, false, true, 38);
			}
			this._siegeRangedMachineEntities.Clear();
			this._siegeMeleeMachineEntities.Clear();
			this._siegeMissileEntities.Clear();
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001FE0C File Offset: 0x0001E00C
		private void RefreshSiegePreparations(PartyBase party)
		{
			List<GameEntity> list = new List<GameEntity>();
			this.StrategicEntity.GetChildrenRecursive(ref list);
			List<GameEntity> list2 = list.FindAll((GameEntity x) => x.HasTag("siege_preparation"));
			bool flag = false;
			if (party.Settlement != null && party.Settlement.IsUnderSiege)
			{
				SiegeEvent.SiegeEngineConstructionProgress siegePreparations = party.Settlement.SiegeEvent.GetSiegeEventSide(BattleSideEnum.Attacker).SiegeEngines.SiegePreparations;
				if (siegePreparations != null && siegePreparations.Progress >= 1f)
				{
					flag = true;
					foreach (GameEntity gameEntity in list2)
					{
						gameEntity.SetVisibilityExcludeParents(true);
					}
				}
			}
			if (!flag)
			{
				foreach (GameEntity gameEntity2 in list2)
				{
					gameEntity2.SetVisibilityExcludeParents(false);
				}
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001FF18 File Offset: 0x0001E118
		private void AddSiegeIconComponents(PartyBase party)
		{
			if (party.Settlement.IsUnderSiege)
			{
				int wallLevel = -1;
				if (party.Settlement.SiegeEvent.BesiegedSettlement.IsTown || party.Settlement.SiegeEvent.BesiegedSettlement.IsCastle)
				{
					wallLevel = party.Settlement.SiegeEvent.BesiegedSettlement.Town.GetWallLevel();
				}
				SiegeEvent.SiegeEngineConstructionProgress[] deployedRangedSiegeEngines = party.Settlement.SiegeEvent.GetSiegeEventSide(BattleSideEnum.Attacker).SiegeEngines.DeployedRangedSiegeEngines;
				for (int i = 0; i < deployedRangedSiegeEngines.Length; i++)
				{
					SiegeEvent.SiegeEngineConstructionProgress siegeEngineConstructionProgress = deployedRangedSiegeEngines[i];
					if (siegeEngineConstructionProgress != null && siegeEngineConstructionProgress.IsActive && i < this._attackerRangedEngineSpawnEntities.Length)
					{
						MatrixFrame globalFrame = this._attackerRangedEngineSpawnEntities[i].GetGlobalFrame();
						globalFrame.rotation.MakeUnit();
						this.AddSiegeMachine(deployedRangedSiegeEngines[i].SiegeEngine, globalFrame, BattleSideEnum.Attacker, wallLevel, i);
					}
				}
				SiegeEvent.SiegeEngineConstructionProgress[] deployedMeleeSiegeEngines = party.Settlement.SiegeEvent.GetSiegeEventSide(BattleSideEnum.Attacker).SiegeEngines.DeployedMeleeSiegeEngines;
				for (int j = 0; j < deployedMeleeSiegeEngines.Length; j++)
				{
					SiegeEvent.SiegeEngineConstructionProgress siegeEngineConstructionProgress2 = deployedMeleeSiegeEngines[j];
					if (siegeEngineConstructionProgress2 != null && siegeEngineConstructionProgress2.IsActive)
					{
						if (deployedMeleeSiegeEngines[j].SiegeEngine == DefaultSiegeEngineTypes.SiegeTower)
						{
							int num = j - this._attackerBatteringRamSpawnEntities.Length;
							if (num >= 0)
							{
								MatrixFrame globalFrame2 = this._attackerSiegeTowerSpawnEntities[num].GetGlobalFrame();
								globalFrame2.rotation.MakeUnit();
								this.AddSiegeMachine(deployedMeleeSiegeEngines[j].SiegeEngine, globalFrame2, BattleSideEnum.Attacker, wallLevel, j);
							}
						}
						else if (deployedMeleeSiegeEngines[j].SiegeEngine == DefaultSiegeEngineTypes.Ram || deployedMeleeSiegeEngines[j].SiegeEngine == DefaultSiegeEngineTypes.ImprovedRam)
						{
							int num2 = j;
							if (num2 >= 0)
							{
								MatrixFrame globalFrame3 = this._attackerBatteringRamSpawnEntities[num2].GetGlobalFrame();
								globalFrame3.rotation.MakeUnit();
								this.AddSiegeMachine(deployedMeleeSiegeEngines[j].SiegeEngine, globalFrame3, BattleSideEnum.Attacker, wallLevel, j);
							}
						}
					}
				}
				SiegeEvent.SiegeEngineConstructionProgress[] deployedRangedSiegeEngines2 = party.Settlement.SiegeEvent.GetSiegeEventSide(BattleSideEnum.Defender).SiegeEngines.DeployedRangedSiegeEngines;
				for (int k = 0; k < deployedRangedSiegeEngines2.Length; k++)
				{
					SiegeEvent.SiegeEngineConstructionProgress siegeEngineConstructionProgress3 = deployedRangedSiegeEngines2[k];
					if (siegeEngineConstructionProgress3 != null && siegeEngineConstructionProgress3.IsActive && k < this._defenderBreachableWallEntitiesCacheForCurrentLevel.Length)
					{
						MatrixFrame globalFrame4 = this._defenderBreachableWallEntitiesCacheForCurrentLevel[k].GetGlobalFrame();
						globalFrame4.rotation.MakeUnit();
						this.AddSiegeMachine(deployedRangedSiegeEngines2[k].SiegeEngine, globalFrame4, BattleSideEnum.Defender, wallLevel, k);
					}
				}
				for (int l = 0; l < 2; l++)
				{
					BattleSideEnum side = (l == 0) ? BattleSideEnum.Attacker : BattleSideEnum.Defender;
					MBReadOnlyList<SiegeEvent.SiegeEngineMissile> siegeEngineMissiles = party.Settlement.SiegeEvent.GetSiegeEventSide(side).SiegeEngineMissiles;
					for (int m = 0; m < siegeEngineMissiles.Count; m++)
					{
						this.AddSiegeMissile(siegeEngineMissiles[m].ShooterSiegeEngineType, this.StrategicEntity.GetGlobalFrame(), side, m);
					}
				}
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000201E8 File Offset: 0x0001E3E8
		private void AddSiegeMachine(SiegeEngineType type, MatrixFrame globalFrame, BattleSideEnum side, int wallLevel, int slotIndex)
		{
			string siegeEngineMapPrefabName = Campaign.Current.Models.SiegeEventModel.GetSiegeEngineMapPrefabName(type, wallLevel, side);
			GameEntity gameEntity = GameEntity.Instantiate(this.MapScene, siegeEngineMapPrefabName, true);
			if (gameEntity != null)
			{
				this.StrategicEntity.AddChild(gameEntity, false);
				MatrixFrame m;
				gameEntity.GetFrame(out m);
				GameEntity gameEntity2 = gameEntity;
				MatrixFrame matrixFrame = globalFrame.TransformToParent(m);
				gameEntity2.SetGlobalFrame(matrixFrame);
				List<GameEntity> list = new List<GameEntity>();
				gameEntity.GetChildrenRecursive(ref list);
				GameEntity gameEntity3 = null;
				if (list.Any((GameEntity entity) => entity.HasTag("siege_machine_mapicon_skeleton")))
				{
					GameEntity gameEntity4 = list.Find((GameEntity entity) => entity.HasTag("siege_machine_mapicon_skeleton"));
					if (gameEntity4.Skeleton != null)
					{
						gameEntity3 = gameEntity4;
						string siegeEngineMapFireAnimationName = Campaign.Current.Models.SiegeEventModel.GetSiegeEngineMapFireAnimationName(type, side);
						gameEntity3.Skeleton.SetAnimationAtChannel(siegeEngineMapFireAnimationName, 0, 1f, 0f, 1f);
					}
				}
				if (type.IsRanged)
				{
					this._siegeRangedMachineEntities.Add(ValueTuple.Create<GameEntity, BattleSideEnum, int, MatrixFrame, GameEntity>(gameEntity, side, slotIndex, globalFrame, gameEntity3));
					return;
				}
				this._siegeMeleeMachineEntities.Add(ValueTuple.Create<GameEntity, BattleSideEnum, int, MatrixFrame, GameEntity>(gameEntity, side, slotIndex, globalFrame, gameEntity3));
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00020330 File Offset: 0x0001E530
		private void AddSiegeMissile(SiegeEngineType type, MatrixFrame globalFrame, BattleSideEnum side, int missileIndex)
		{
			string siegeEngineMapProjectilePrefabName = Campaign.Current.Models.SiegeEventModel.GetSiegeEngineMapProjectilePrefabName(type);
			GameEntity gameEntity = GameEntity.Instantiate(this.MapScene, siegeEngineMapProjectilePrefabName, true);
			if (gameEntity != null)
			{
				this._siegeMissileEntities.Add(ValueTuple.Create<GameEntity, BattleSideEnum, int>(gameEntity, side, missileIndex));
				this.StrategicEntity.AddChild(gameEntity, false);
				this.StrategicEntity.EntityFlags &= ~EntityFlags.DoNotTick;
				MatrixFrame m;
				gameEntity.GetFrame(out m);
				GameEntity gameEntity2 = gameEntity;
				MatrixFrame matrixFrame = globalFrame.TransformToParent(m);
				gameEntity2.SetGlobalFrame(matrixFrame);
				gameEntity.SetVisibilityExcludeParents(false);
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000203C4 File Offset: 0x0001E5C4
		private void AddMobileIconComponents(PartyBase party, ref bool clearBannerComponentCache, ref bool clearBannerEntityCache)
		{
			uint contourColor = FactionManager.IsAtWarAgainstFaction(party.MapFaction, Hero.MainHero.MapFaction) ? 4294905856U : 4278206719U;
			Settlement besiegedSettlement = party.MobileParty.BesiegedSettlement;
			if (((besiegedSettlement != null) ? besiegedSettlement.SiegeEvent : null) != null && party.MobileParty.BesiegedSettlement.SiegeEvent.BesiegerCamp.HasInvolvedPartyForEventType(party, MapEvent.BattleTypes.Siege))
			{
				GameEntity gameEntity = GameEntity.CreateEmpty(this.StrategicEntity.Scene, true);
				gameEntity.AddMultiMesh(MetaMesh.GetCopy("map_icon_siege_camp_tent", true, false), true);
				MatrixFrame identity = MatrixFrame.Identity;
				identity.rotation.ApplyScaleLocal(1.2f);
				gameEntity.SetFrame(ref identity);
				string text = null;
				Hero leaderHero = party.LeaderHero;
				if (((leaderHero != null) ? leaderHero.ClanBanner : null) != null)
				{
					text = party.LeaderHero.ClanBanner.Serialize();
				}
				bool flag = party.MobileParty.Army != null && party.MobileParty.Army.LeaderParty == party.MobileParty;
				MatrixFrame identity2 = MatrixFrame.Identity;
				identity2.origin.z = identity2.origin.z + (flag ? 0.2f : 0.15f);
				identity2.rotation.RotateAboutUp(1.5707964f);
				float scaleAmount = MBMath.Map(party.TotalStrength / 500f * ((party.MobileParty.Army != null && flag) ? 1f : 0.8f), 0f, 1f, 0.15f, 0.5f);
				identity2.rotation.ApplyScaleLocal(scaleAmount);
				if (!string.IsNullOrEmpty(text))
				{
					clearBannerComponentCache = false;
					string text2 = "campaign_flag";
					if (this._cachedBannerComponent.Item1 == text + text2)
					{
						this._cachedBannerComponent.Item2.GetFirstMetaMesh().Frame = identity2;
						this.StrategicEntity.AddComponent(this._cachedBannerComponent.Item2);
					}
					else
					{
						MetaMesh bannerOfCharacter = PartyVisual.GetBannerOfCharacter(new Banner(text), text2);
						bannerOfCharacter.Frame = identity2;
						int componentCount = gameEntity.GetComponentCount(GameEntity.ComponentType.ClothSimulator);
						gameEntity.AddMultiMesh(bannerOfCharacter, true);
						if (gameEntity.GetComponentCount(GameEntity.ComponentType.ClothSimulator) > componentCount)
						{
							this._cachedBannerComponent.Item1 = text + text2;
							this._cachedBannerComponent.Item2 = gameEntity.GetComponentAtIndex(componentCount, GameEntity.ComponentType.ClothSimulator);
						}
					}
				}
				this.StrategicEntity.AddChild(gameEntity, false);
				return;
			}
			if (PartyBaseHelper.GetVisualPartyLeader(party) != null)
			{
				string bannerKey = null;
				Hero leaderHero2 = party.LeaderHero;
				if (((leaderHero2 != null) ? leaderHero2.ClanBanner : null) != null)
				{
					bannerKey = party.LeaderHero.ClanBanner.Serialize();
				}
				ActionIndexCache act_none = ActionIndexCache.act_none;
				ActionIndexCache act_none2 = ActionIndexCache.act_none;
				MapEvent mapEvent = (party.MobileParty.Army != null && party.MobileParty.Army.DoesLeaderPartyAndAttachedPartiesContain(party.MobileParty)) ? party.MobileParty.Army.LeaderParty.MapEvent : party.MapEvent;
				int wieldedItemIndex;
				this.GetMeleeWeaponToWield(party, out wieldedItemIndex);
				if (mapEvent != null && (mapEvent.EventType == MapEvent.BattleTypes.FieldBattle || (mapEvent.EventType == MapEvent.BattleTypes.Raid && party.MapEventSide == mapEvent.AttackerSide) || mapEvent.EventType == MapEvent.BattleTypes.SiegeOutside || mapEvent.EventType == MapEvent.BattleTypes.SallyOut))
				{
					PartyVisual.GetPartyBattleAnimation(party, wieldedItemIndex, out act_none, out act_none2);
				}
				IFaction mapFaction = party.MapFaction;
				uint teamColor = (mapFaction != null) ? mapFaction.Color : 4291609515U;
				IFaction mapFaction2 = party.MapFaction;
				uint teamColor2 = (mapFaction2 != null) ? mapFaction2.Color2 : 4291609515U;
				this.AddCharacterToPartyIcon(PartyBaseHelper.GetVisualPartyLeader(party), contourColor, bannerKey, wieldedItemIndex, teamColor, teamColor2, act_none, act_none2, MBRandom.NondeterministicRandomFloat * 0.7f, ref clearBannerEntityCache);
				if (party.IsMobile)
				{
					string text3;
					string harnessItemId;
					party.MobileParty.GetMountAndHarnessVisualIdsForPartyIcon(out text3, out harnessItemId);
					if (!string.IsNullOrEmpty(text3))
					{
						this.AddMountToPartyIcon(new Vec3(0.3f, -0.25f, 0f, -1f), text3, harnessItemId, contourColor, PartyBaseHelper.GetVisualPartyLeader(party));
					}
				}
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00020794 File Offset: 0x0001E994
		private void GetMeleeWeaponToWield(PartyBase party, out int wieldedItemIndex)
		{
			wieldedItemIndex = -1;
			CharacterObject visualPartyLeader = PartyBaseHelper.GetVisualPartyLeader(party);
			if (visualPartyLeader != null)
			{
				for (int i = 0; i < 5; i++)
				{
					if (visualPartyLeader.Equipment[i].Item != null && visualPartyLeader.Equipment[i].Item.PrimaryWeapon.IsMeleeWeapon)
					{
						wieldedItemIndex = i;
						return;
					}
				}
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000207F4 File Offset: 0x0001E9F4
		private static void GetPartyBattleAnimation(PartyBase party, int wieldedItemIndex, out ActionIndexCache leaderAction, out ActionIndexCache mountAction)
		{
			leaderAction = ActionIndexCache.act_none;
			mountAction = ActionIndexCache.act_none;
			if (party.MobileParty.Army == null || !party.MobileParty.Army.DoesLeaderPartyAndAttachedPartiesContain(party.MobileParty))
			{
				MapEvent mapEvent = party.MapEvent;
			}
			else
			{
				MapEvent mapEvent2 = party.MobileParty.Army.LeaderParty.MapEvent;
			}
			CharacterObject visualPartyLeader = PartyBaseHelper.GetVisualPartyLeader(party);
			MapEvent mapEvent3 = party.MapEvent;
			if (((mapEvent3 != null) ? mapEvent3.MapEventSettlement : null) != null && visualPartyLeader != null && !visualPartyLeader.HasMount())
			{
				leaderAction = PartyVisual._raidOnFoot;
				return;
			}
			if (wieldedItemIndex > -1 && ((visualPartyLeader != null) ? visualPartyLeader.Equipment[wieldedItemIndex].Item : null) != null)
			{
				WeaponComponent weaponComponent = visualPartyLeader.Equipment[wieldedItemIndex].Item.WeaponComponent;
				if (weaponComponent != null && weaponComponent.PrimaryWeapon.IsMeleeWeapon)
				{
					if (visualPartyLeader.HasMount())
					{
						if (visualPartyLeader.Equipment[10].Item.HorseComponent.Monster.MonsterUsage == "camel")
						{
							if (weaponComponent.GetItemType() == ItemObject.ItemTypeEnum.OneHandedWeapon || weaponComponent.GetItemType() == ItemObject.ItemTypeEnum.TwoHandedWeapon)
							{
								leaderAction = PartyVisual._camelSwordAttack;
								mountAction = PartyVisual._swordAttackMount;
							}
							else if (weaponComponent.GetItemType() == ItemObject.ItemTypeEnum.Polearm)
							{
								if (weaponComponent.PrimaryWeapon.SwingDamageType == DamageTypes.Invalid)
								{
									leaderAction = PartyVisual._camelSpearAttack;
									mountAction = PartyVisual._spearAttackMount;
								}
								else if (weaponComponent.PrimaryWeapon.WeaponClass == WeaponClass.TwoHandedPolearm)
								{
									leaderAction = PartyVisual._camel1HandedSwingAttack;
									mountAction = PartyVisual._swingAttackMount;
								}
								else
								{
									leaderAction = PartyVisual._camel2HandedSwingAttack;
									mountAction = PartyVisual._swingAttackMount;
								}
							}
						}
						else if (weaponComponent.GetItemType() == ItemObject.ItemTypeEnum.OneHandedWeapon || weaponComponent.GetItemType() == ItemObject.ItemTypeEnum.TwoHandedWeapon)
						{
							leaderAction = PartyVisual._horseSwordAttack;
							mountAction = PartyVisual._swordAttackMount;
						}
						else if (weaponComponent.GetItemType() == ItemObject.ItemTypeEnum.Polearm)
						{
							if (weaponComponent.PrimaryWeapon.SwingDamageType == DamageTypes.Invalid)
							{
								leaderAction = PartyVisual._horseSpearAttack;
								mountAction = PartyVisual._spearAttackMount;
							}
							else if (weaponComponent.PrimaryWeapon.WeaponClass == WeaponClass.TwoHandedPolearm)
							{
								leaderAction = PartyVisual._horse1HandedSwingAttack;
								mountAction = PartyVisual._swingAttackMount;
							}
							else
							{
								leaderAction = PartyVisual._horse2HandedSwingAttack;
								mountAction = PartyVisual._swingAttackMount;
							}
						}
					}
					else if (weaponComponent.PrimaryWeapon.WeaponClass == WeaponClass.OneHandedAxe || weaponComponent.PrimaryWeapon.WeaponClass == WeaponClass.Mace || weaponComponent.PrimaryWeapon.WeaponClass == WeaponClass.OneHandedSword)
					{
						leaderAction = PartyVisual._attack1H;
					}
					else if (weaponComponent.PrimaryWeapon.WeaponClass == WeaponClass.TwoHandedAxe || weaponComponent.PrimaryWeapon.WeaponClass == WeaponClass.TwoHandedMace || weaponComponent.PrimaryWeapon.WeaponClass == WeaponClass.TwoHandedSword)
					{
						leaderAction = PartyVisual._attack2H;
					}
					else if (weaponComponent.PrimaryWeapon.WeaponClass == WeaponClass.OneHandedPolearm || weaponComponent.PrimaryWeapon.WeaponClass == WeaponClass.TwoHandedPolearm)
					{
						leaderAction = PartyVisual._attackSpear1HOr2H;
					}
				}
			}
			if (leaderAction == ActionIndexCache.act_none)
			{
				if (visualPartyLeader.HasMount())
				{
					if (visualPartyLeader.Equipment[10].Item.HorseComponent.Monster.MonsterUsage == "camel")
					{
						leaderAction = PartyVisual._camelUnarmedAttack;
					}
					else
					{
						leaderAction = PartyVisual._horseUnarmedAttack;
					}
					mountAction = PartyVisual._unarmedAttackMount;
					return;
				}
				leaderAction = PartyVisual._attackUnarmed;
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00020B14 File Offset: 0x0001ED14
		public void RefreshWallState()
		{
			if (this._defenderBreachableWallEntitiesForAllLevels != null)
			{
				PartyBase partyBase = this.PartyBase;
				MBReadOnlyList<float> mbreadOnlyList;
				if (((partyBase != null) ? partyBase.Settlement : null) == null || (this.PartyBase.Settlement != null && !this.PartyBase.Settlement.IsFortification))
				{
					mbreadOnlyList = null;
				}
				else
				{
					mbreadOnlyList = this.PartyBase.Settlement.SettlementWallSectionHitPointsRatioList;
				}
				if (mbreadOnlyList != null)
				{
					if (mbreadOnlyList.Count == 0)
					{
						Debug.FailedAssert(string.Concat(new object[]
						{
							"Town (",
							this.PartyBase.Settlement.Name.ToString(),
							") doesn't have wall entities defined for it's current level(",
							this.PartyBase.Settlement.Town.GetWallLevel(),
							")"
						}), "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.View\\Map\\PartyVisual.cs", "RefreshWallState", 1658);
						return;
					}
					for (int i = 0; i < this._defenderBreachableWallEntitiesForAllLevels.Length; i++)
					{
						bool flag = mbreadOnlyList[i % mbreadOnlyList.Count] <= 0f;
						foreach (GameEntity gameEntity in this._defenderBreachableWallEntitiesForAllLevels[i].GetChildren())
						{
							if (gameEntity.HasTag("map_solid_wall"))
							{
								gameEntity.SetVisibilityExcludeParents(!flag);
							}
							else if (gameEntity.HasTag("map_broken_wall"))
							{
								gameEntity.SetVisibilityExcludeParents(flag);
							}
						}
					}
				}
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00020C90 File Offset: 0x0001EE90
		public void RefreshTownPhysicalEntitiesState(PartyBase party)
		{
			if (((party != null) ? party.Settlement : null) != null && party.Settlement.IsFortification && this.TownPhysicalEntities != null)
			{
				if (PlayerSiege.PlayerSiegeEvent != null && PlayerSiege.PlayerSiegeEvent.BesiegedSettlement == party.Settlement)
				{
					this.TownPhysicalEntities.ForEach(delegate(GameEntity p)
					{
						p.AddBodyFlags(BodyFlags.Disabled, true);
					});
					return;
				}
				this.TownPhysicalEntities.ForEach(delegate(GameEntity p)
				{
					p.RemoveBodyFlags(BodyFlags.Disabled, true);
				});
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00020D31 File Offset: 0x0001EF31
		public void SetLevelMask(uint newMask)
		{
			this._currentLevelMask = newMask;
			this.PartyBase.SetVisualAsDirty();
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00020D48 File Offset: 0x0001EF48
		public void RefreshLevelMask()
		{
			if (this.PartyBase.IsSettlement)
			{
				uint num = 0U;
				if (this.PartyBase.Settlement.IsVillage)
				{
					if (this.PartyBase.Settlement.Village.VillageState == Village.VillageStates.Looted)
					{
						num |= Campaign.Current.MapSceneWrapper.GetSceneLevel("looted");
					}
					else
					{
						num |= Campaign.Current.MapSceneWrapper.GetSceneLevel("civilian");
					}
					num |= PartyVisual.GetLevelOfProduction(this.PartyBase.Settlement);
				}
				else if (this.PartyBase.Settlement.IsTown || this.PartyBase.Settlement.IsCastle)
				{
					if (this.PartyBase.Settlement.Town.GetWallLevel() == 1)
					{
						num |= Campaign.Current.MapSceneWrapper.GetSceneLevel("level_1");
					}
					else if (this.PartyBase.Settlement.Town.GetWallLevel() == 2)
					{
						num |= Campaign.Current.MapSceneWrapper.GetSceneLevel("level_2");
					}
					else if (this.PartyBase.Settlement.Town.GetWallLevel() == 3)
					{
						num |= Campaign.Current.MapSceneWrapper.GetSceneLevel("level_3");
					}
					if (this.PartyBase.Settlement.SiegeEvent != null)
					{
						num |= Campaign.Current.MapSceneWrapper.GetSceneLevel("siege");
					}
					else
					{
						num |= Campaign.Current.MapSceneWrapper.GetSceneLevel("civilian");
					}
				}
				else if (this.PartyBase.Settlement.IsHideout)
				{
					num |= Campaign.Current.MapSceneWrapper.GetSceneLevel("level_1");
				}
				if (this._currentLevelMask != num)
				{
					this.SetLevelMask(num);
				}
			}
			this.PartyBase.OnLevelMaskUpdated();
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00020F1C File Offset: 0x0001F11C
		private static uint GetLevelOfProduction(Settlement settlement)
		{
			uint num = 0U;
			if (settlement.IsVillage)
			{
				if (settlement.Village.Hearth < 200f)
				{
					num |= Campaign.Current.MapSceneWrapper.GetSceneLevel("level_1");
				}
				else if (settlement.Village.Hearth < 600f)
				{
					num |= Campaign.Current.MapSceneWrapper.GetSceneLevel("level_2");
				}
				else
				{
					num |= Campaign.Current.MapSceneWrapper.GetSceneLevel("level_3");
				}
			}
			return num;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00020FA4 File Offset: 0x0001F1A4
		private void SetSettlementLevelVisibility()
		{
			List<GameEntity> list = new List<GameEntity>();
			this.StrategicEntity.GetChildrenRecursive(ref list);
			foreach (GameEntity gameEntity in list)
			{
				if ((gameEntity.GetUpgradeLevelMask() & (GameEntity.UpgradeLevelMask)this._currentLevelMask) == (GameEntity.UpgradeLevelMask)this._currentLevelMask)
				{
					gameEntity.SetVisibilityExcludeParents(true);
					gameEntity.SetPhysicsState(true, true);
				}
				else
				{
					gameEntity.SetVisibilityExcludeParents(false);
					gameEntity.SetPhysicsState(false, true);
				}
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00021034 File Offset: 0x0001F234
		private void InitializePartyCollider(PartyBase party)
		{
			if (this.StrategicEntity != null && party.IsMobile)
			{
				this.StrategicEntity.AddSphereAsBody(new Vec3(0f, 0f, 0f, -1f), 0.5f, BodyFlags.Moveable | BodyFlags.OnlyCollideWithRaycast);
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00021088 File Offset: 0x0001F288
		public void OnPartyRemoved()
		{
			if (this.StrategicEntity != null)
			{
				MapScreen.VisualsOfEntities.Remove(this.StrategicEntity.Pointer);
				foreach (GameEntity gameEntity in this.StrategicEntity.GetChildren())
				{
					MapScreen.VisualsOfEntities.Remove(gameEntity.Pointer);
				}
				this.ReleaseResources();
				this.StrategicEntity.Remove(111);
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0002111C File Offset: 0x0001F31C
		internal void OnMapHoverSiegeEngine(MatrixFrame engineFrame)
		{
			if (PlayerSiege.PlayerSiegeEvent == null)
			{
				return;
			}
			for (int i = 0; i < this._attackerBatteringRamSpawnEntities.Length; i++)
			{
				MatrixFrame globalFrame = this._attackerBatteringRamSpawnEntities[i].GetGlobalFrame();
				if (globalFrame.NearlyEquals(engineFrame, 1E-05f))
				{
					if (this._hoveredSiegeEntityFrame != globalFrame)
					{
						SiegeEvent.SiegeEngineConstructionProgress engineInProgress = PlayerSiege.PlayerSiegeEvent.GetSiegeEventSide(BattleSideEnum.Attacker).SiegeEngines.DeployedMeleeSiegeEngines[i];
						InformationManager.ShowTooltip(typeof(List<TooltipProperty>), new object[]
						{
							SandBoxUIHelper.GetSiegeEngineInProgressTooltip(engineInProgress)
						});
					}
					return;
				}
			}
			for (int j = 0; j < this._attackerSiegeTowerSpawnEntities.Length; j++)
			{
				MatrixFrame globalFrame2 = this._attackerSiegeTowerSpawnEntities[j].GetGlobalFrame();
				if (globalFrame2.NearlyEquals(engineFrame, 1E-05f))
				{
					if (this._hoveredSiegeEntityFrame != globalFrame2)
					{
						SiegeEvent.SiegeEngineConstructionProgress engineInProgress2 = PlayerSiege.PlayerSiegeEvent.GetSiegeEventSide(BattleSideEnum.Attacker).SiegeEngines.DeployedMeleeSiegeEngines[this._attackerBatteringRamSpawnEntities.Length + j];
						InformationManager.ShowTooltip(typeof(List<TooltipProperty>), new object[]
						{
							SandBoxUIHelper.GetSiegeEngineInProgressTooltip(engineInProgress2)
						});
					}
					return;
				}
			}
			for (int k = 0; k < this._attackerRangedEngineSpawnEntities.Length; k++)
			{
				MatrixFrame globalFrame3 = this._attackerRangedEngineSpawnEntities[k].GetGlobalFrame();
				if (globalFrame3.NearlyEquals(engineFrame, 1E-05f))
				{
					if (this._hoveredSiegeEntityFrame != globalFrame3)
					{
						SiegeEvent.SiegeEngineConstructionProgress engineInProgress3 = PlayerSiege.PlayerSiegeEvent.GetSiegeEventSide(BattleSideEnum.Attacker).SiegeEngines.DeployedRangedSiegeEngines[k];
						InformationManager.ShowTooltip(typeof(List<TooltipProperty>), new object[]
						{
							SandBoxUIHelper.GetSiegeEngineInProgressTooltip(engineInProgress3)
						});
					}
					return;
				}
			}
			for (int l = 0; l < this._defenderRangedEngineSpawnEntitiesCacheForCurrentLevel.Length; l++)
			{
				MatrixFrame globalFrame4 = this._defenderRangedEngineSpawnEntitiesCacheForCurrentLevel[l].GetGlobalFrame();
				if (globalFrame4.NearlyEquals(engineFrame, 1E-05f))
				{
					if (this._hoveredSiegeEntityFrame != globalFrame4)
					{
						SiegeEvent.SiegeEngineConstructionProgress engineInProgress4 = PlayerSiege.PlayerSiegeEvent.GetSiegeEventSide(BattleSideEnum.Defender).SiegeEngines.DeployedRangedSiegeEngines[l];
						InformationManager.ShowTooltip(typeof(List<TooltipProperty>), new object[]
						{
							SandBoxUIHelper.GetSiegeEngineInProgressTooltip(engineInProgress4)
						});
					}
					return;
				}
			}
			for (int m = 0; m < this._defenderBreachableWallEntitiesCacheForCurrentLevel.Length; m++)
			{
				MatrixFrame globalFrame5 = this._defenderBreachableWallEntitiesCacheForCurrentLevel[m].GetGlobalFrame();
				if (globalFrame5.NearlyEquals(engineFrame, 1E-05f))
				{
					if (this._hoveredSiegeEntityFrame != globalFrame5 && this.PartyBase.IsSettlement)
					{
						InformationManager.ShowTooltip(typeof(List<TooltipProperty>), new object[]
						{
							SandBoxUIHelper.GetWallSectionTooltip(this.PartyBase.Settlement, m)
						});
					}
					return;
				}
			}
			this._hoveredSiegeEntityFrame = MatrixFrame.Identity;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000213AE File Offset: 0x0001F5AE
		internal void OnMapHoverSiegeEngineEnd()
		{
			this._hoveredSiegeEntityFrame = MatrixFrame.Identity;
			MBInformationManager.HideInformations();
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000213C0 File Offset: 0x0001F5C0
		public void OnStartup()
		{
			bool flag = false;
			if (this.PartyBase.IsMobile)
			{
				this.StrategicEntity = GameEntity.CreateEmpty(this.MapScene, true);
				if (!this.PartyBase.IsVisible)
				{
					this.StrategicEntity.EntityFlags |= EntityFlags.DoNotTick;
				}
			}
			else if (this.PartyBase.IsSettlement)
			{
				this.StrategicEntity = this.MapScene.GetCampaignEntityWithName(this.PartyBase.Id);
				if (this.StrategicEntity == null)
				{
					Campaign.Current.MapSceneWrapper.AddNewEntityToMapScene(this.PartyBase.Settlement.StringId, this.PartyBase.Settlement.Position2D);
					this.StrategicEntity = this.MapScene.GetCampaignEntityWithName(this.PartyBase.Id);
				}
				bool flag2 = false;
				if (this.PartyBase.Settlement.IsFortification)
				{
					List<GameEntity> list = new List<GameEntity>();
					this.StrategicEntity.GetChildrenRecursive(ref list);
					this.PopulateSiegeEngineFrameListsFromChildren(list);
					this.UpdateDefenderSiegeEntitiesCache();
					this.TownPhysicalEntities = list.FindAll((GameEntity x) => x.HasTag("bo_town"));
					List<GameEntity> list2 = new List<GameEntity>();
					Dictionary<int, List<GameEntity>> dictionary = new Dictionary<int, List<GameEntity>>();
					dictionary.Add(1, new List<GameEntity>());
					dictionary.Add(2, new List<GameEntity>());
					dictionary.Add(3, new List<GameEntity>());
					List<MatrixFrame> list3 = new List<MatrixFrame>();
					List<MatrixFrame> list4 = new List<MatrixFrame>();
					foreach (GameEntity gameEntity in list)
					{
						if (gameEntity.HasTag("main_map_city_gate"))
						{
							PartyBase.IsPositionOkForTraveling(gameEntity.GetGlobalFrame().origin.AsVec2);
							flag2 = true;
							list2.Add(gameEntity);
						}
						if (gameEntity.HasTag("map_settlement_circle"))
						{
							this.CircleLocalFrame = gameEntity.GetGlobalFrame();
							flag = true;
							gameEntity.SetVisibilityExcludeParents(false);
							list2.Add(gameEntity);
						}
						if (gameEntity.HasTag("map_banner_placeholder"))
						{
							int upgradeLevelOfEntity = gameEntity.Parent.GetUpgradeLevelOfEntity();
							if (upgradeLevelOfEntity == 0)
							{
								dictionary[1].Add(gameEntity);
								dictionary[2].Add(gameEntity);
								dictionary[3].Add(gameEntity);
							}
							else
							{
								dictionary[upgradeLevelOfEntity].Add(gameEntity);
							}
							list2.Add(gameEntity);
						}
						if (gameEntity.HasTag("map_camp_area_1"))
						{
							list3.Add(gameEntity.GetGlobalFrame());
							list2.Add(gameEntity);
						}
						else if (gameEntity.HasTag("map_camp_area_2"))
						{
							list4.Add(gameEntity.GetGlobalFrame());
							list2.Add(gameEntity);
						}
					}
					this._gateBannerEntitiesWithLevels = dictionary;
					if (this.PartyBase.Settlement.IsFortification)
					{
						this.PartyBase.Settlement.Town.BesiegerCampPositions1 = list3.ToArray();
						this.PartyBase.Settlement.Town.BesiegerCampPositions2 = list4.ToArray();
					}
					foreach (GameEntity gameEntity2 in list2)
					{
						gameEntity2.Remove(112);
					}
				}
				if (!flag2)
				{
					if (!this.PartyBase.Settlement.IsTown)
					{
						bool isCastle = this.PartyBase.Settlement.IsCastle;
					}
					if (!PartyBase.IsPositionOkForTraveling(this.PartyBase.Settlement.GatePosition))
					{
						Vec2 gatePosition = this.PartyBase.Settlement.GatePosition;
					}
				}
			}
			CharacterObject visualPartyLeader = PartyBaseHelper.GetVisualPartyLeader(this.PartyBase);
			if (!flag)
			{
				this.CircleLocalFrame = MatrixFrame.Identity;
				if (this.PartyBase.IsSettlement)
				{
					MatrixFrame circleLocalFrame = this.CircleLocalFrame;
					Mat3 rotation = circleLocalFrame.rotation;
					if (this.PartyBase.Settlement.IsVillage)
					{
						rotation.ApplyScaleLocal(1.75f);
					}
					else if (this.PartyBase.Settlement.IsTown)
					{
						rotation.ApplyScaleLocal(5.75f);
					}
					else if (this.PartyBase.Settlement.IsCastle)
					{
						rotation.ApplyScaleLocal(2.75f);
					}
					else
					{
						rotation.ApplyScaleLocal(1.75f);
					}
					circleLocalFrame.rotation = rotation;
					this.CircleLocalFrame = circleLocalFrame;
				}
				else if ((visualPartyLeader != null && visualPartyLeader.HasMount()) || this.PartyBase.MobileParty.IsCaravan)
				{
					MatrixFrame circleLocalFrame2 = this.CircleLocalFrame;
					Mat3 rotation2 = circleLocalFrame2.rotation;
					rotation2.ApplyScaleLocal(0.4625f);
					circleLocalFrame2.rotation = rotation2;
					this.CircleLocalFrame = circleLocalFrame2;
				}
				else
				{
					MatrixFrame circleLocalFrame3 = this.CircleLocalFrame;
					Mat3 rotation3 = circleLocalFrame3.rotation;
					rotation3.ApplyScaleLocal(0.3725f);
					circleLocalFrame3.rotation = rotation3;
					this.CircleLocalFrame = circleLocalFrame3;
				}
			}
			this.StrategicEntity.SetVisibilityExcludeParents(this.PartyBase.IsVisible);
			AgentVisuals humanAgentVisuals = this.HumanAgentVisuals;
			if (humanAgentVisuals != null)
			{
				GameEntity entity = humanAgentVisuals.GetEntity();
				if (entity != null)
				{
					entity.SetVisibilityExcludeParents(this.PartyBase.IsVisible);
				}
			}
			AgentVisuals mountAgentVisuals = this.MountAgentVisuals;
			if (mountAgentVisuals != null)
			{
				GameEntity entity2 = mountAgentVisuals.GetEntity();
				if (entity2 != null)
				{
					entity2.SetVisibilityExcludeParents(this.PartyBase.IsVisible);
				}
			}
			AgentVisuals caravanMountAgentVisuals = this.CaravanMountAgentVisuals;
			if (caravanMountAgentVisuals != null)
			{
				GameEntity entity3 = caravanMountAgentVisuals.GetEntity();
				if (entity3 != null)
				{
					entity3.SetVisibilityExcludeParents(this.PartyBase.IsVisible);
				}
			}
			this.StrategicEntity.SetReadyToRender(true);
			this.StrategicEntity.SetEntityEnvMapVisibility(false);
			this._entityAlpha = (this.PartyBase.IsVisible ? 1f : 0f);
			this.InitializePartyCollider(this.PartyBase);
			List<GameEntity> list5 = new List<GameEntity>();
			this.StrategicEntity.GetChildrenRecursive(ref list5);
			if (!MapScreen.VisualsOfEntities.ContainsKey(this.StrategicEntity.Pointer))
			{
				MapScreen.VisualsOfEntities.Add(this.StrategicEntity.Pointer, this);
			}
			foreach (GameEntity gameEntity3 in list5)
			{
				if (!MapScreen.VisualsOfEntities.ContainsKey(gameEntity3.Pointer) && !MapScreen.FrameAndVisualOfEngines.ContainsKey(gameEntity3.Pointer))
				{
					MapScreen.VisualsOfEntities.Add(gameEntity3.Pointer, this);
				}
			}
			if (this.PartyBase.IsSettlement)
			{
				this.StrategicEntity.SetAsPredisplayEntity();
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00021A70 File Offset: 0x0001FC70
		private void PopulateSiegeEngineFrameListsFromChildren(List<GameEntity> children)
		{
			this._attackerRangedEngineSpawnEntities = (from e in children.FindAll((GameEntity x) => x.Tags.Any((string t) => t.Contains("map_siege_engine")))
			orderby e.Tags.First((string s) => s.Contains("map_siege_engine"))
			select e).ToArray<GameEntity>();
			foreach (GameEntity gameEntity in this._attackerRangedEngineSpawnEntities)
			{
				if (gameEntity.ChildCount > 0 && !MapScreen.FrameAndVisualOfEngines.ContainsKey(gameEntity.GetChild(0).Pointer))
				{
					MapScreen.FrameAndVisualOfEngines.Add(gameEntity.GetChild(0).Pointer, new Tuple<MatrixFrame, PartyVisual>(gameEntity.GetGlobalFrame(), this));
				}
			}
			this._defenderRangedEngineSpawnEntitiesForAllLevels = (from e in children.FindAll((GameEntity x) => x.Tags.Any((string t) => t.Contains("map_defensive_engine")))
			orderby e.Tags.First((string s) => s.Contains("map_defensive_engine"))
			select e).ToArray<GameEntity>();
			foreach (GameEntity gameEntity2 in this._defenderRangedEngineSpawnEntitiesForAllLevels)
			{
				if (gameEntity2.ChildCount > 0 && !MapScreen.FrameAndVisualOfEngines.ContainsKey(gameEntity2.GetChild(0).Pointer))
				{
					MapScreen.FrameAndVisualOfEngines.Add(gameEntity2.GetChild(0).Pointer, new Tuple<MatrixFrame, PartyVisual>(gameEntity2.GetGlobalFrame(), this));
				}
			}
			this._attackerBatteringRamSpawnEntities = children.FindAll((GameEntity x) => x.HasTag("map_siege_ram")).ToArray();
			foreach (GameEntity gameEntity3 in this._attackerBatteringRamSpawnEntities)
			{
				if (gameEntity3.ChildCount > 0 && !MapScreen.FrameAndVisualOfEngines.ContainsKey(gameEntity3.GetChild(0).Pointer))
				{
					MapScreen.FrameAndVisualOfEngines.Add(gameEntity3.GetChild(0).Pointer, new Tuple<MatrixFrame, PartyVisual>(gameEntity3.GetGlobalFrame(), this));
				}
			}
			this._attackerSiegeTowerSpawnEntities = children.FindAll((GameEntity x) => x.HasTag("map_siege_tower")).ToArray();
			foreach (GameEntity gameEntity4 in this._attackerSiegeTowerSpawnEntities)
			{
				if (gameEntity4.ChildCount > 0 && !MapScreen.FrameAndVisualOfEngines.ContainsKey(gameEntity4.GetChild(0).Pointer))
				{
					MapScreen.FrameAndVisualOfEngines.Add(gameEntity4.GetChild(0).Pointer, new Tuple<MatrixFrame, PartyVisual>(gameEntity4.GetGlobalFrame(), this));
				}
			}
			this._defenderBreachableWallEntitiesForAllLevels = children.FindAll((GameEntity x) => x.HasTag("map_breachable_wall")).ToArray();
			foreach (GameEntity gameEntity5 in this._defenderBreachableWallEntitiesForAllLevels)
			{
				if (gameEntity5.ChildCount > 0 && !MapScreen.FrameAndVisualOfEngines.ContainsKey(gameEntity5.GetChild(0).Pointer))
				{
					MapScreen.FrameAndVisualOfEngines.Add(gameEntity5.GetChild(0).Pointer, new Tuple<MatrixFrame, PartyVisual>(gameEntity5.GetGlobalFrame(), this));
				}
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00021D90 File Offset: 0x0001FF90
		private MatrixFrame GetFrame()
		{
			return this.StrategicEntity.GetFrame();
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00021DA0 File Offset: 0x0001FFA0
		private void SetFrame(ref MatrixFrame frame)
		{
			if (this.StrategicEntity != null && !this.StrategicEntity.GetFrame().NearlyEquals(frame, 1E-05f))
			{
				this.StrategicEntity.SetFrame(ref frame);
				if (this.HumanAgentVisuals != null)
				{
					MatrixFrame matrixFrame = frame;
					matrixFrame.rotation.ApplyScaleLocal(this.HumanAgentVisuals.GetScale());
					this.HumanAgentVisuals.GetEntity().SetFrame(ref matrixFrame);
				}
				if (this.MountAgentVisuals != null)
				{
					MatrixFrame matrixFrame2 = frame;
					matrixFrame2.rotation.ApplyScaleLocal(this.MountAgentVisuals.GetScale());
					this.MountAgentVisuals.GetEntity().SetFrame(ref matrixFrame2);
				}
				if (this.CaravanMountAgentVisuals != null)
				{
					MatrixFrame matrixFrame3 = frame.TransformToParent(this.CaravanMountAgentVisuals.GetFrame());
					matrixFrame3.rotation.ApplyScaleLocal(this.CaravanMountAgentVisuals.GetScale());
					this.CaravanMountAgentVisuals.GetEntity().SetFrame(ref matrixFrame3);
				}
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00021EA0 File Offset: 0x000200A0
		private void UpdateDefenderSiegeEntitiesCache()
		{
			GameEntity.UpgradeLevelMask currentSettlementUpgradeLevelMask = GameEntity.UpgradeLevelMask.None;
			if (this.PartyBase.IsSettlement && this.PartyBase.Settlement.IsFortification)
			{
				if (this.PartyBase.Settlement.Town.GetWallLevel() == 1)
				{
					currentSettlementUpgradeLevelMask = GameEntity.UpgradeLevelMask.Level1;
				}
				else if (this.PartyBase.Settlement.Town.GetWallLevel() == 2)
				{
					currentSettlementUpgradeLevelMask = GameEntity.UpgradeLevelMask.Level2;
				}
				else if (this.PartyBase.Settlement.Town.GetWallLevel() == 3)
				{
					currentSettlementUpgradeLevelMask = GameEntity.UpgradeLevelMask.Level3;
				}
			}
			this._currentSettlementUpgradeLevelMask = currentSettlementUpgradeLevelMask;
			this._defenderRangedEngineSpawnEntitiesCacheForCurrentLevel = (from e in this._defenderRangedEngineSpawnEntitiesForAllLevels
			where (e.GetUpgradeLevelMask() & this._currentSettlementUpgradeLevelMask) == this._currentSettlementUpgradeLevelMask
			select e).ToArray<GameEntity>();
			this._defenderBreachableWallEntitiesCacheForCurrentLevel = (from e in this._defenderBreachableWallEntitiesForAllLevels
			where (e.GetUpgradeLevelMask() & this._currentSettlementUpgradeLevelMask) == this._currentSettlementUpgradeLevelMask
			select e).ToArray<GameEntity>();
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00021F6C File Offset: 0x0002016C
		public MatrixFrame[] GetAttackerTowerSiegeEngineFrames()
		{
			MatrixFrame[] array = new MatrixFrame[this._attackerSiegeTowerSpawnEntities.Length];
			for (int i = 0; i < this._attackerSiegeTowerSpawnEntities.Length; i++)
			{
				array[i] = this._attackerSiegeTowerSpawnEntities[i].GetGlobalFrame();
			}
			return array;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00021FB0 File Offset: 0x000201B0
		public MatrixFrame[] GetAttackerBatteringRamSiegeEngineFrames()
		{
			MatrixFrame[] array = new MatrixFrame[this._attackerBatteringRamSpawnEntities.Length];
			for (int i = 0; i < this._attackerBatteringRamSpawnEntities.Length; i++)
			{
				array[i] = this._attackerBatteringRamSpawnEntities[i].GetGlobalFrame();
			}
			return array;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00021FF4 File Offset: 0x000201F4
		public MatrixFrame[] GetAttackerRangedSiegeEngineFrames()
		{
			MatrixFrame[] array = new MatrixFrame[this._attackerRangedEngineSpawnEntities.Length];
			for (int i = 0; i < this._attackerRangedEngineSpawnEntities.Length; i++)
			{
				array[i] = this._attackerRangedEngineSpawnEntities[i].GetGlobalFrame();
			}
			return array;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00022038 File Offset: 0x00020238
		public MatrixFrame[] GetDefenderRangedSiegeEngineFrames()
		{
			MatrixFrame[] array = new MatrixFrame[this._defenderRangedEngineSpawnEntitiesCacheForCurrentLevel.Length];
			for (int i = 0; i < this._defenderRangedEngineSpawnEntitiesCacheForCurrentLevel.Length; i++)
			{
				array[i] = this._defenderRangedEngineSpawnEntitiesCacheForCurrentLevel[i].GetGlobalFrame();
			}
			return array;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0002207C File Offset: 0x0002027C
		public MatrixFrame[] GetBreachableWallFrames()
		{
			MatrixFrame[] array = new MatrixFrame[this._defenderBreachableWallEntitiesCacheForCurrentLevel.Length];
			for (int i = 0; i < this._defenderBreachableWallEntitiesCacheForCurrentLevel.Length; i++)
			{
				array[i] = this._defenderBreachableWallEntitiesCacheForCurrentLevel[i].GetGlobalFrame();
			}
			return array;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000220BF File Offset: 0x000202BF
		public bool IsVisibleOrFadingOut()
		{
			return this._entityAlpha > 0f;
		}

		// Token: 0x040001F5 RID: 501
		private const string MapSiegeEngineTag = "map_siege_engine";

		// Token: 0x040001F6 RID: 502
		private const string MapBreachableWallTag = "map_breachable_wall";

		// Token: 0x040001F7 RID: 503
		private const string MapDefenderEngineTag = "map_defensive_engine";

		// Token: 0x040001F8 RID: 504
		private const string CircleTag = "map_settlement_circle";

		// Token: 0x040001F9 RID: 505
		private const string BannerPlaceHolderTag = "map_banner_placeholder";

		// Token: 0x040001FA RID: 506
		private const string MapCampArea1Tag = "map_camp_area_1";

		// Token: 0x040001FB RID: 507
		private const string MapCampArea2Tag = "map_camp_area_2";

		// Token: 0x040001FC RID: 508
		private const string MapSiegeEngineRamTag = "map_siege_ram";

		// Token: 0x040001FD RID: 509
		private const string TownPhysicalTag = "bo_town";

		// Token: 0x040001FE RID: 510
		private const string MapSiegeEngineTowerTag = "map_siege_tower";

		// Token: 0x040001FF RID: 511
		private const string MapPreparationTag = "siege_preparation";

		// Token: 0x04000200 RID: 512
		private const string BurnedTag = "looted";

		// Token: 0x04000201 RID: 513
		private const float PartyScale = 0.3f;

		// Token: 0x04000202 RID: 514
		private const float HorseAnimationSpeedFactor = 1.3f;

		// Token: 0x04000203 RID: 515
		private static readonly ActionIndexCache _raidOnFoot = ActionIndexCache.Create("act_map_raid");

		// Token: 0x04000204 RID: 516
		private static readonly ActionIndexCache _camelSwordAttack = ActionIndexCache.Create("act_map_rider_camel_attack_1h");

		// Token: 0x04000205 RID: 517
		private static readonly ActionIndexCache _camelSpearAttack = ActionIndexCache.Create("act_map_rider_camel_attack_1h_spear");

		// Token: 0x04000206 RID: 518
		private static readonly ActionIndexCache _camel1HandedSwingAttack = ActionIndexCache.Create("act_map_rider_camel_attack_1h_swing");

		// Token: 0x04000207 RID: 519
		private static readonly ActionIndexCache _camel2HandedSwingAttack = ActionIndexCache.Create("act_map_rider_camel_attack_2h_swing");

		// Token: 0x04000208 RID: 520
		private static readonly ActionIndexCache _camelUnarmedAttack = ActionIndexCache.Create("act_map_rider_camel_attack_unarmed");

		// Token: 0x04000209 RID: 521
		private static readonly ActionIndexCache _horseSwordAttack = ActionIndexCache.Create("act_map_rider_horse_attack_1h");

		// Token: 0x0400020A RID: 522
		private static readonly ActionIndexCache _horseSpearAttack = ActionIndexCache.Create("act_map_rider_horse_attack_1h_spear");

		// Token: 0x0400020B RID: 523
		private static readonly ActionIndexCache _horse1HandedSwingAttack = ActionIndexCache.Create("act_map_rider_horse_attack_1h_swing");

		// Token: 0x0400020C RID: 524
		private static readonly ActionIndexCache _horse2HandedSwingAttack = ActionIndexCache.Create("act_map_rider_horse_attack_2h_swing");

		// Token: 0x0400020D RID: 525
		private static readonly ActionIndexCache _horseUnarmedAttack = ActionIndexCache.Create("act_map_rider_horse_attack_unarmed");

		// Token: 0x0400020E RID: 526
		private static readonly ActionIndexCache _swordAttackMount = ActionIndexCache.Create("act_map_mount_attack_1h");

		// Token: 0x0400020F RID: 527
		private static readonly ActionIndexCache _spearAttackMount = ActionIndexCache.Create("act_map_mount_attack_spear");

		// Token: 0x04000210 RID: 528
		private static readonly ActionIndexCache _swingAttackMount = ActionIndexCache.Create("act_map_mount_attack_swing");

		// Token: 0x04000211 RID: 529
		private static readonly ActionIndexCache _unarmedAttackMount = ActionIndexCache.Create("act_map_mount_attack_unarmed");

		// Token: 0x04000212 RID: 530
		private static readonly ActionIndexCache _attack1H = ActionIndexCache.Create("act_map_attack_1h");

		// Token: 0x04000213 RID: 531
		private static readonly ActionIndexCache _attack2H = ActionIndexCache.Create("act_map_attack_2h");

		// Token: 0x04000214 RID: 532
		private static readonly ActionIndexCache _attackSpear1HOr2H = ActionIndexCache.Create("act_map_attack_spear_1h_or_2h");

		// Token: 0x04000215 RID: 533
		private static readonly ActionIndexCache _attackUnarmed = ActionIndexCache.Create("act_map_attack_unarmed");

		// Token: 0x04000218 RID: 536
		private readonly List<ValueTuple<GameEntity, BattleSideEnum, int, MatrixFrame, GameEntity>> _siegeRangedMachineEntities;

		// Token: 0x04000219 RID: 537
		private readonly List<ValueTuple<GameEntity, BattleSideEnum, int, MatrixFrame, GameEntity>> _siegeMeleeMachineEntities;

		// Token: 0x0400021A RID: 538
		private readonly List<ValueTuple<GameEntity, BattleSideEnum, int>> _siegeMissileEntities;

		// Token: 0x0400021B RID: 539
		private Dictionary<int, List<GameEntity>> _gateBannerEntitiesWithLevels;

		// Token: 0x0400021C RID: 540
		private GameEntity[] _attackerRangedEngineSpawnEntities;

		// Token: 0x0400021D RID: 541
		private GameEntity[] _attackerBatteringRamSpawnEntities;

		// Token: 0x0400021E RID: 542
		private GameEntity[] _defenderBreachableWallEntitiesCacheForCurrentLevel;

		// Token: 0x0400021F RID: 543
		private GameEntity[] _attackerSiegeTowerSpawnEntities;

		// Token: 0x04000220 RID: 544
		private GameEntity[] _defenderRangedEngineSpawnEntitiesForAllLevels;

		// Token: 0x04000221 RID: 545
		private GameEntity[] _defenderRangedEngineSpawnEntitiesCacheForCurrentLevel;

		// Token: 0x04000222 RID: 546
		private GameEntity[] _defenderBreachableWallEntitiesForAllLevels;

		// Token: 0x04000223 RID: 547
		private ValueTuple<string, GameEntityComponent> _cachedBannerComponent;

		// Token: 0x04000224 RID: 548
		private ValueTuple<string, GameEntity> _cachedBannerEntity;

		// Token: 0x04000225 RID: 549
		private MatrixFrame _hoveredSiegeEntityFrame = MatrixFrame.Identity;

		// Token: 0x04000226 RID: 550
		private GameEntity.UpgradeLevelMask _currentSettlementUpgradeLevelMask;

		// Token: 0x04000228 RID: 552
		private float _speed;

		// Token: 0x04000229 RID: 553
		private float _entityAlpha;

		// Token: 0x0400022A RID: 554
		private Scene _mapScene;

		// Token: 0x0400022B RID: 555
		private Mesh _contourMaskMesh;

		// Token: 0x0400022C RID: 556
		private uint _currentLevelMask;

		// Token: 0x04000232 RID: 562
		public readonly PartyBase PartyBase;

		// Token: 0x04000233 RID: 563
		private Vec2 _lastFrameVisualPositionWithoutError;

		// Token: 0x02000096 RID: 150
		private struct SiegeBombardmentData
		{
			// Token: 0x04000335 RID: 821
			public Vec3 LaunchGlobalPosition;

			// Token: 0x04000336 RID: 822
			public Vec3 TargetPosition;

			// Token: 0x04000337 RID: 823
			public MatrixFrame ShooterGlobalFrame;

			// Token: 0x04000338 RID: 824
			public MatrixFrame TargetAlignedShooterGlobalFrame;

			// Token: 0x04000339 RID: 825
			public float MissileSpeed;

			// Token: 0x0400033A RID: 826
			public float Gravity;

			// Token: 0x0400033B RID: 827
			public float LaunchAngle;

			// Token: 0x0400033C RID: 828
			public float RotationDuration;

			// Token: 0x0400033D RID: 829
			public float ReloadDuration;

			// Token: 0x0400033E RID: 830
			public float AimingDuration;

			// Token: 0x0400033F RID: 831
			public float MissileLaunchDuration;

			// Token: 0x04000340 RID: 832
			public float FireDuration;

			// Token: 0x04000341 RID: 833
			public float FlightDuration;

			// Token: 0x04000342 RID: 834
			public float TotalDuration;
		}
	}
}

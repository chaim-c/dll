using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.Scripts
{
	// Token: 0x0200003E RID: 62
	public class CharacterSpawner : ScriptComponentBehavior
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060002CA RID: 714 RVA: 0x000195D9 File Offset: 0x000177D9
		// (set) Token: 0x060002CB RID: 715 RVA: 0x000195E1 File Offset: 0x000177E1
		public uint ClothColor1 { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060002CC RID: 716 RVA: 0x000195EA File Offset: 0x000177EA
		// (set) Token: 0x060002CD RID: 717 RVA: 0x000195F2 File Offset: 0x000177F2
		public uint ClothColor2 { get; private set; }

		// Token: 0x060002CE RID: 718 RVA: 0x000195FC File Offset: 0x000177FC
		~CharacterSpawner()
		{
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00019624 File Offset: 0x00017824
		protected override void OnInit()
		{
			base.OnInit();
			this.ClothColor1 = uint.MaxValue;
			this.ClothColor2 = uint.MaxValue;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0001963A File Offset: 0x0001783A
		protected void Init()
		{
			this.Active = false;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00019643 File Offset: 0x00017843
		protected override void OnEditorInit()
		{
			base.OnEditorInit();
			if (Game.Current == null)
			{
				this._editorGameManager = new EditorGameManager();
				this.isFinished = !this._editorGameManager.DoLoadingForGameManager();
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00019674 File Offset: 0x00017874
		protected override void OnEditorTick(float dt)
		{
			if (!this.Enabled)
			{
				return;
			}
			if (!this.isFinished && this._editorGameManager != null)
			{
				this.isFinished = !this._editorGameManager.DoLoadingForGameManager();
			}
			if (Game.Current != null && this._agentVisuals == null)
			{
				this.SpawnCharacter();
				base.GameEntity.SetVisibilityExcludeParents(false);
				if (this._agentEntity != null)
				{
					this._agentEntity.SetVisibilityExcludeParents(false);
				}
				if (this._horseEntity != null)
				{
					this._horseEntity.SetVisibilityExcludeParents(false);
				}
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00019703 File Offset: 0x00017903
		protected override void OnRemoved(int removeReason)
		{
			base.OnRemoved(removeReason);
			if (this._agentVisuals != null)
			{
				this._agentVisuals.Reset();
				this._agentVisuals.GetVisuals().ManualInvalidate();
				this._agentVisuals = null;
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00019736 File Offset: 0x00017936
		public void SetCreateFaceImmediately(bool value)
		{
			this.CreateFaceImmediately = value;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00019740 File Offset: 0x00017940
		private void Disable()
		{
			if (this._agentEntity != null && this._agentEntity.Parent == base.GameEntity)
			{
				base.GameEntity.RemoveChild(this._agentEntity, false, false, true, 34);
			}
			if (this._agentVisuals != null)
			{
				this._agentVisuals.Reset();
				this._agentVisuals.GetVisuals().ManualInvalidate();
				this._agentVisuals = null;
			}
			if (this._horseEntity != null && this._horseEntity.Parent == base.GameEntity)
			{
				this._horseEntity.Scene.RemoveEntity(this._horseEntity, 96);
			}
			this.Active = false;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x000197F8 File Offset: 0x000179F8
		protected override void OnEditorVariableChanged(string variableName)
		{
			base.OnEditorVariableChanged(variableName);
			if (variableName == "Enabled")
			{
				if (this.Enabled)
				{
					this.Init();
				}
				else
				{
					this.Disable();
				}
			}
			if (!this.Enabled)
			{
				return;
			}
			if (variableName == "LordName" || variableName == "ActionSetSuffix")
			{
				if (this._agentVisuals != null)
				{
					BasicCharacterObject @object = Game.Current.ObjectManager.GetObject<BasicCharacterObject>(this.LordName);
					if (@object != null)
					{
						this.InitWithCharacter(CharacterCode.CreateFrom(@object), true);
						return;
					}
				}
			}
			else if (variableName == "PoseActionForHorse")
			{
				BasicCharacterObject object2 = Game.Current.ObjectManager.GetObject<BasicCharacterObject>(this.LordName);
				if (object2 != null)
				{
					this.InitWithCharacter(CharacterCode.CreateFrom(object2), true);
					return;
				}
			}
			else if (variableName == "PoseAction")
			{
				if (this._agentVisuals != null)
				{
					BasicCharacterObject object3 = Game.Current.ObjectManager.GetObject<BasicCharacterObject>(this.LordName);
					if (object3 != null)
					{
						this.InitWithCharacter(CharacterCode.CreateFrom(object3), true);
						return;
					}
				}
			}
			else
			{
				if (variableName == "IsWeaponWielded")
				{
					BasicCharacterObject object4 = Game.Current.ObjectManager.GetObject<BasicCharacterObject>(this.LordName);
					this.WieldWeapon(CharacterCode.CreateFrom(object4));
					return;
				}
				if (variableName == "AnimationProgress")
				{
					Skeleton skeleton = this._agentVisuals.GetVisuals().GetSkeleton();
					skeleton.Freeze(false);
					skeleton.TickAnimationsAndForceUpdate(0.001f, this._agentVisuals.GetVisuals().GetGlobalFrame(), false);
					skeleton.SetAnimationParameterAtChannel(0, MBMath.ClampFloat(this.AnimationProgress, 0f, 1f));
					skeleton.SetUptoDate(false);
					skeleton.Freeze(true);
					return;
				}
				if (variableName == "HorseAnimationProgress")
				{
					if (this._horseEntity != null)
					{
						this._horseEntity.Skeleton.Freeze(false);
						this._horseEntity.Skeleton.TickAnimationsAndForceUpdate(0.001f, this._horseEntity.GetGlobalFrame(), false);
						this._horseEntity.Skeleton.SetAnimationParameterAtChannel(0, MBMath.ClampFloat(this.HorseAnimationProgress, 0f, 1f));
						this._horseEntity.Skeleton.SetUptoDate(false);
						this._horseEntity.Skeleton.Freeze(true);
						return;
					}
				}
				else if (variableName == "HasMount")
				{
					if (this.HasMount)
					{
						BasicCharacterObject object5 = Game.Current.ObjectManager.GetObject<BasicCharacterObject>(this.LordName);
						this.SpawnMount(CharacterCode.CreateFrom(object5));
						return;
					}
					if (this._horseEntity != null)
					{
						this._horseEntity.Scene.RemoveEntity(this._horseEntity, 97);
						return;
					}
				}
				else if (variableName == "Active")
				{
					base.GameEntity.SetVisibilityExcludeParents(this.Active);
					if (this._agentEntity != null)
					{
						this._agentEntity.SetVisibilityExcludeParents(this.Active);
					}
					if (this._horseEntity != null)
					{
						this._horseEntity.SetVisibilityExcludeParents(this.Active);
						return;
					}
				}
				else if (variableName == "FaceKeyString")
				{
					BasicCharacterObject object6 = Game.Current.ObjectManager.GetObject<BasicCharacterObject>(this.LordName);
					if (object6 != null)
					{
						this.InitWithCharacter(CharacterCode.CreateFrom(object6), true);
						return;
					}
				}
				else if (variableName == "WieldOffHand")
				{
					BasicCharacterObject object7 = Game.Current.ObjectManager.GetObject<BasicCharacterObject>(this.LordName);
					if (object7 != null)
					{
						this.InitWithCharacter(CharacterCode.CreateFrom(object7), true);
					}
				}
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00019B64 File Offset: 0x00017D64
		public void SetClothColors(uint color1, uint color2)
		{
			this.ClothColor1 = color1;
			this.ClothColor2 = color2;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00019B74 File Offset: 0x00017D74
		public void SpawnCharacter()
		{
			BasicCharacterObject @object = Game.Current.ObjectManager.GetObject<BasicCharacterObject>(this.LordName);
			if (@object != null)
			{
				CharacterCode characterCode = CharacterCode.CreateFrom(@object);
				this.InitWithCharacter(characterCode, true);
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00019BAC File Offset: 0x00017DAC
		public void InitWithCharacter(CharacterCode characterCode, bool useBodyProperties = false)
		{
			base.GameEntity.BreakPrefab();
			if (this._agentEntity != null && this._agentEntity.Parent == base.GameEntity)
			{
				base.GameEntity.RemoveChild(this._agentEntity, false, false, true, 35);
			}
			AgentVisuals agentVisuals = this._agentVisuals;
			if (agentVisuals != null)
			{
				agentVisuals.Reset();
			}
			AgentVisuals agentVisuals2 = this._agentVisuals;
			if (agentVisuals2 != null)
			{
				MBAgentVisuals visuals = agentVisuals2.GetVisuals();
				if (visuals != null)
				{
					visuals.ManualInvalidate();
				}
			}
			if (this._horseEntity != null && this._horseEntity.Parent == base.GameEntity)
			{
				this._horseEntity.Scene.RemoveEntity(this._horseEntity, 98);
			}
			this._agentEntity = GameEntity.CreateEmpty(base.GameEntity.Scene, false);
			this._agentEntity.Name = "TableauCharacterAgentVisualsEntity";
			this._spawnFrame = this._agentEntity.GetFrame();
			BodyProperties bodyProperties = characterCode.BodyProperties;
			if (useBodyProperties)
			{
				BodyProperties.FromString(this.BodyPropertiesString, out bodyProperties);
			}
			if (characterCode.Color1 != 4294967295U)
			{
				this.ClothColor1 = characterCode.Color1;
			}
			if (characterCode.Color2 != 4294967295U)
			{
				this.ClothColor2 = characterCode.Color2;
			}
			Monster baseMonsterFromRace = FaceGen.GetBaseMonsterFromRace(characterCode.Race);
			this._agentVisuals = AgentVisuals.Create(new AgentVisualsData().Equipment(characterCode.CalculateEquipment()).BodyProperties(bodyProperties).Race(characterCode.Race).Frame(this._spawnFrame).Scale(1f).SkeletonType(characterCode.IsFemale ? SkeletonType.Female : SkeletonType.Male).Entity(this._agentEntity).ActionSet(MBGlobals.GetActionSetWithSuffix(baseMonsterFromRace, characterCode.IsFemale, this.ActionSetSuffix)).ActionCode(ActionIndexCache.Create("act_inventory_idle_start")).Scene(base.GameEntity.Scene).Monster(baseMonsterFromRace).PrepareImmediately(this.CreateFaceImmediately).Banner(characterCode.Banner).ClothColor1(this.ClothColor1).ClothColor2(this.ClothColor2).UseMorphAnims(true), "TableauCharacterAgentVisuals", false, false, false);
			this._agentVisuals.SetAction(ActionIndexCache.Create(this.PoseAction), MBMath.ClampFloat(this.AnimationProgress, 0f, 1f), true);
			base.GameEntity.AddChild(this._agentEntity, false);
			this.WieldWeapon(characterCode);
			MatrixFrame identity = MatrixFrame.Identity;
			this._agentVisuals.GetVisuals().SetFrame(ref identity);
			if (this.HasMount)
			{
				this.SpawnMount(characterCode);
			}
			base.GameEntity.SetVisibilityExcludeParents(true);
			this._agentEntity.SetVisibilityExcludeParents(true);
			if (this._horseEntity != null)
			{
				this._horseEntity.SetVisibilityExcludeParents(true);
			}
			Skeleton skeleton = this._agentVisuals.GetVisuals().GetSkeleton();
			skeleton.Freeze(false);
			skeleton.TickAnimationsAndForceUpdate(0.001f, this._agentVisuals.GetVisuals().GetGlobalFrame(), false);
			skeleton.SetUptoDate(false);
			skeleton.Freeze(true);
			this._agentEntity.SetBoundingboxDirty();
			skeleton.Freeze(false);
			skeleton.TickAnimationsAndForceUpdate(0.001f, this._agentVisuals.GetVisuals().GetGlobalFrame(), false);
			skeleton.SetAnimationParameterAtChannel(0, MBMath.ClampFloat(this.AnimationProgress, 0f, 1f));
			skeleton.SetUptoDate(false);
			skeleton.Freeze(true);
			skeleton.ManualInvalidate();
			if (this._horseEntity != null)
			{
				this._horseEntity.Skeleton.Freeze(false);
				this._horseEntity.Skeleton.TickAnimationsAndForceUpdate(0.001f, this._horseEntity.GetGlobalFrame(), false);
				this._horseEntity.Skeleton.SetUptoDate(false);
				this._horseEntity.Skeleton.Freeze(true);
				this._horseEntity.SetBoundingboxDirty();
			}
			if (this._horseEntity != null)
			{
				this._horseEntity.Skeleton.Freeze(false);
				this._horseEntity.Skeleton.TickAnimationsAndForceUpdate(0.001f, this._horseEntity.GetGlobalFrame(), false);
				this._horseEntity.Skeleton.SetAnimationParameterAtChannel(0, MBMath.ClampFloat(this.HorseAnimationProgress, 0f, 1f));
				this._horseEntity.Skeleton.SetUptoDate(false);
				this._horseEntity.Skeleton.Freeze(true);
			}
			base.GameEntity.SetBoundingboxDirty();
			if (!base.GameEntity.Scene.IsEditorScene())
			{
				if (this._agentEntity != null)
				{
					this._agentEntity.ManualInvalidate();
				}
				if (this._horseEntity != null)
				{
					this._horseEntity.ManualInvalidate();
				}
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0001A044 File Offset: 0x00018244
		private void WieldWeapon(CharacterCode characterCode)
		{
			if (this.IsWeaponWielded)
			{
				WeaponFlags p = (WeaponFlags)0UL;
				switch (characterCode.FormationClass)
				{
				case FormationClass.Infantry:
				case FormationClass.Cavalry:
				case FormationClass.NumberOfDefaultFormations:
				case FormationClass.HeavyInfantry:
				case FormationClass.LightCavalry:
				case FormationClass.NumberOfRegularFormations:
				case FormationClass.Bodyguard:
					p = WeaponFlags.MeleeWeapon;
					break;
				case FormationClass.Ranged:
				case FormationClass.HorseArcher:
					p = WeaponFlags.RangedWeapon;
					break;
				}
				int num = -1;
				int num2 = -1;
				Equipment equipment = characterCode.CalculateEquipment();
				for (int i = 0; i < 4; i++)
				{
					ItemObject item = equipment[i].Item;
					if (((item != null) ? item.PrimaryWeapon : null) != null)
					{
						if (num2 == -1 && equipment[i].Item.ItemFlags.HasAnyFlag(ItemFlags.HeldInOffHand) && this.WieldOffHand)
						{
							num2 = i;
						}
						if (num == -1 && equipment[i].Item.PrimaryWeapon.WeaponFlags.HasAnyFlag(p))
						{
							num = i;
						}
					}
				}
				if (num != -1 || num2 != -1)
				{
					AgentVisualsData copyAgentVisualsData = this._agentVisuals.GetCopyAgentVisualsData();
					copyAgentVisualsData.RightWieldedItemIndex(num).LeftWieldedItemIndex(num2).ActionCode(ActionIndexCache.Create(this.PoseAction)).Frame(this._spawnFrame);
					this._agentVisuals.Refresh(false, copyAgentVisualsData, false);
				}
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0001A18C File Offset: 0x0001838C
		private void SpawnMount(CharacterCode characterCode)
		{
			Equipment equipment = characterCode.CalculateEquipment();
			ItemObject item = equipment[EquipmentIndex.ArmorItemEndSlot].Item;
			if (item == null)
			{
				this.HasMount = false;
				return;
			}
			this._horseEntity = GameEntity.CreateEmpty(base.GameEntity.Scene, false);
			this._horseEntity.Name = "MountEntity";
			Monster monster = item.HorseComponent.Monster;
			MBActionSet actionSet = MBActionSet.GetActionSet(monster.ActionSetCode);
			this._horseEntity.CreateAgentSkeleton(actionSet.GetSkeletonName(), false, actionSet, monster.MonsterUsage, monster);
			this._horseEntity.CopyComponentsToSkeleton();
			this._horseEntity.Skeleton.SetAgentActionChannel(0, ActionIndexCache.Create(this.PoseActionForHorse), MBMath.ClampFloat(this.HorseAnimationProgress, 0f, 1f), -0.2f, true);
			base.GameEntity.AddChild(this._horseEntity, false);
			MountVisualCreator.AddMountMeshToEntity(this._horseEntity, equipment[10].Item, equipment[11].Item, MountCreationKey.GetRandomMountKeyString(equipment[10].Item, MBRandom.RandomInt()), null);
			this._horseEntity.SetVisibilityExcludeParents(true);
			this._horseEntity.Skeleton.TickAnimations(0.01f, this._agentVisuals.GetVisuals().GetGlobalFrame(), true);
		}

		// Token: 0x040001FC RID: 508
		public bool Enabled;

		// Token: 0x040001FD RID: 509
		public string PoseAction = "act_walk_idle_unarmed";

		// Token: 0x040001FE RID: 510
		public string LordName = "main_hero_for_perf";

		// Token: 0x040001FF RID: 511
		public string ActionSetSuffix = "_facegen";

		// Token: 0x04000200 RID: 512
		public string PoseActionForHorse = "horse_stand_3";

		// Token: 0x04000201 RID: 513
		public string BodyPropertiesString = "<BodyProperties version=\"4\" age=\"23.16\" weight=\"0.3333\" build=\"0\" key=\"00000C07000000010011111211151111000701000010000000111011000101000000500202111110000000000000000000000000000000000000000000A00000\" />";

		// Token: 0x04000202 RID: 514
		public bool IsWeaponWielded;

		// Token: 0x04000203 RID: 515
		public bool HasMount;

		// Token: 0x04000204 RID: 516
		public bool WieldOffHand = true;

		// Token: 0x04000205 RID: 517
		public float AnimationProgress;

		// Token: 0x04000206 RID: 518
		public float HorseAnimationProgress;

		// Token: 0x04000209 RID: 521
		private MBGameManager _editorGameManager;

		// Token: 0x0400020A RID: 522
		private bool isFinished;

		// Token: 0x0400020B RID: 523
		private bool CreateFaceImmediately = true;

		// Token: 0x0400020C RID: 524
		private AgentVisuals _agentVisuals;

		// Token: 0x0400020D RID: 525
		private GameEntity _agentEntity;

		// Token: 0x0400020E RID: 526
		private GameEntity _horseEntity;

		// Token: 0x0400020F RID: 527
		public bool Active;

		// Token: 0x04000210 RID: 528
		private MatrixFrame _spawnFrame;
	}
}

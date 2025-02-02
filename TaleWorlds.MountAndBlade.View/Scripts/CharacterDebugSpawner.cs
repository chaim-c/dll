using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.Scripts
{
	// Token: 0x0200003D RID: 61
	public class CharacterDebugSpawner : ScriptComponentBehavior
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00018A43 File Offset: 0x00016C43
		// (set) Token: 0x060002BB RID: 699 RVA: 0x00018A4B File Offset: 0x00016C4B
		public uint ClothColor1 { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00018A54 File Offset: 0x00016C54
		// (set) Token: 0x060002BD RID: 701 RVA: 0x00018A5C File Offset: 0x00016C5C
		public uint ClothColor2 { get; private set; }

		// Token: 0x060002BE RID: 702 RVA: 0x00018A65 File Offset: 0x00016C65
		protected override void OnInit()
		{
			base.OnInit();
			this.ClothColor1 = uint.MaxValue;
			this.ClothColor2 = uint.MaxValue;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00018A7B File Offset: 0x00016C7B
		protected override void OnEditorInit()
		{
			base.OnEditorInit();
			if (CharacterDebugSpawner._editorGameManager == null)
			{
				CharacterDebugSpawner._editorGameManager = new EditorGameManager();
			}
			CharacterDebugSpawner._editorGameManagerRefCount++;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00018AA0 File Offset: 0x00016CA0
		protected override void OnEditorTick(float dt)
		{
			if (!CharacterDebugSpawner.isFinished && CharacterDebugSpawner.gameTickFrameNo != Utilities.EngineFrameNo)
			{
				CharacterDebugSpawner.gameTickFrameNo = Utilities.EngineFrameNo;
				CharacterDebugSpawner.isFinished = !CharacterDebugSpawner._editorGameManager.DoLoadingForGameManager();
			}
			if (Game.Current != null && this._agentVisuals == null)
			{
				this.MovementDirection.x = MBRandom.RandomFloatNormal;
				this.MovementDirection.y = MBRandom.RandomFloatNormal;
				this.MovementDirection.Normalize();
				this.MovementSpeed = MBRandom.RandomFloat * 9f + 1f;
				this.PhaseDiff = MBRandom.RandomFloat;
				this.MovementDirectionChange = MBRandom.RandomFloatNormal * 3.1415927f;
				this.Time = 0f;
				this.ActionSetTimer = 0f;
				this.ActionChangeInterval = MBRandom.RandomFloat * 0.5f + 0.5f;
				this.SpawnCharacter();
			}
			MatrixFrame globalFrame = this._agentVisuals.GetVisuals().GetGlobalFrame();
			this._agentVisuals.GetVisuals().SetFrame(ref globalFrame);
			Vec3 vec = new Vec3(this.MovementDirection, 0f, -1f);
			vec.RotateAboutZ(this.MovementDirectionChange * dt);
			this.MovementDirection.x = vec.x;
			this.MovementDirection.y = vec.y;
			float f = this.MovementSpeed * (MathF.Sin(this.PhaseDiff + this.Time) * 0.5f) + 2f;
			Vec2 agentLocalSpeed = this.MovementDirection * f;
			this._agentVisuals.SetAgentLocalSpeed(agentLocalSpeed);
			this.Time += dt;
			if (this.Time - this.ActionSetTimer > this.ActionChangeInterval)
			{
				this.ActionSetTimer = this.Time;
				this._agentVisuals.SetAction(this._actionIndices[MBRandom.RandomInt(this._actionIndices.Length)], 0f, true);
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00018C81 File Offset: 0x00016E81
		protected override void OnRemoved(int removeReason)
		{
			base.OnRemoved(removeReason);
			this.Reset();
			CharacterDebugSpawner._editorGameManagerRefCount--;
			if (CharacterDebugSpawner._editorGameManagerRefCount == 0)
			{
				CharacterDebugSpawner._editorGameManager = null;
				CharacterDebugSpawner.isFinished = false;
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00018CB0 File Offset: 0x00016EB0
		protected override void OnEditorVariableChanged(string variableName)
		{
			base.OnEditorVariableChanged(variableName);
			if (variableName == "LordName")
			{
				BasicCharacterObject @object = Game.Current.ObjectManager.GetObject<BasicCharacterObject>(this.LordName);
				AgentVisualsData copyAgentVisualsData = this._agentVisuals.GetCopyAgentVisualsData();
				copyAgentVisualsData.BodyProperties(@object.GetBodyProperties(null, -1)).SkeletonType(@object.IsFemale ? SkeletonType.Female : SkeletonType.Male).ActionSet(MBGlobals.GetActionSetWithSuffix(copyAgentVisualsData.MonsterData, @object.IsFemale, "_poses")).Equipment(@object.Equipment).UseMorphAnims(true);
				this._agentVisuals.Refresh(false, copyAgentVisualsData, false);
				return;
			}
			if (!(variableName == "PoseAction"))
			{
				if (variableName == "IsWeaponWielded")
				{
					BasicCharacterObject object2 = Game.Current.ObjectManager.GetObject<BasicCharacterObject>(this.LordName);
					this.WieldWeapon(CharacterCode.CreateFrom(object2));
				}
				return;
			}
			AgentVisuals agentVisuals = this._agentVisuals;
			if (agentVisuals == null)
			{
				return;
			}
			agentVisuals.SetAction(this.PoseAction, 0f, true);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00018DA7 File Offset: 0x00016FA7
		public void SetClothColors(uint color1, uint color2)
		{
			this.ClothColor1 = color1;
			this.ClothColor2 = color2;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00018DB8 File Offset: 0x00016FB8
		public void SpawnCharacter()
		{
			BasicCharacterObject @object = Game.Current.ObjectManager.GetObject<BasicCharacterObject>(this.LordName);
			if (@object != null)
			{
				CharacterCode characterCode = CharacterCode.CreateFrom(@object);
				this.InitWithCharacter(characterCode);
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00018DEC File Offset: 0x00016FEC
		public void Reset()
		{
			AgentVisuals agentVisuals = this._agentVisuals;
			if (agentVisuals == null)
			{
				return;
			}
			agentVisuals.Reset();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00018E00 File Offset: 0x00017000
		public void InitWithCharacter(CharacterCode characterCode)
		{
			GameEntity gameEntity = GameEntity.CreateEmpty(base.GameEntity.Scene, false);
			gameEntity.Name = "TableauCharacterAgentVisualsEntity";
			Monster baseMonsterFromRace = FaceGen.GetBaseMonsterFromRace(characterCode.Race);
			this._agentVisuals = AgentVisuals.Create(new AgentVisualsData().Equipment(characterCode.CalculateEquipment()).BodyProperties(characterCode.BodyProperties).Race(characterCode.Race).Frame(gameEntity.GetGlobalFrame()).SkeletonType(characterCode.IsFemale ? SkeletonType.Female : SkeletonType.Male).Entity(gameEntity).ActionSet(MBGlobals.GetActionSetWithSuffix(baseMonsterFromRace, characterCode.IsFemale, "_facegen")).ActionCode(this.act_inventory_idle_start).Scene(base.GameEntity.Scene).Monster(baseMonsterFromRace).PrepareImmediately(this.CreateFaceImmediately).Banner(characterCode.Banner).ClothColor1(this.ClothColor1).ClothColor2(this.ClothColor2), "CharacterDebugSpawner", false, false, false);
			this._agentVisuals.SetAction(this.PoseAction, MBRandom.RandomFloat, true);
			base.GameEntity.AddChild(gameEntity, false);
			this.WieldWeapon(characterCode);
			this._agentVisuals.GetVisuals().GetSkeleton().TickAnimationsAndForceUpdate(0.1f, this._agentVisuals.GetVisuals().GetGlobalFrame(), true);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00018F4C File Offset: 0x0001714C
		public void WieldWeapon(CharacterCode characterCode)
		{
			if (this.IsWeaponWielded)
			{
				int num = -1;
				int num2 = -1;
				Equipment equipment = characterCode.CalculateEquipment();
				for (int i = 0; i < 4; i++)
				{
					ItemObject item = equipment[i].Item;
					if (((item != null) ? item.PrimaryWeapon : null) != null)
					{
						if (num2 == -1 && equipment[i].Item.ItemFlags.HasAnyFlag(ItemFlags.HeldInOffHand))
						{
							num2 = i;
						}
						if (num == -1 && equipment[i].Item.PrimaryWeapon.WeaponFlags.HasAnyFlag(WeaponFlags.WeaponMask))
						{
							num = i;
						}
					}
				}
				if (num != -1 || num2 != -1)
				{
					AgentVisualsData copyAgentVisualsData = this._agentVisuals.GetCopyAgentVisualsData();
					copyAgentVisualsData.RightWieldedItemIndex(num).LeftWieldedItemIndex(num2).ActionCode(this.PoseAction);
					this._agentVisuals.Refresh(false, copyAgentVisualsData, false);
				}
			}
		}

		// Token: 0x040001E8 RID: 488
		private readonly ActionIndexCache[] _actionIndices = new ActionIndexCache[]
		{
			ActionIndexCache.Create("act_start_conversation"),
			ActionIndexCache.Create("act_stand_conversation"),
			ActionIndexCache.Create("act_start_angry_conversation"),
			ActionIndexCache.Create("act_stand_angry_conversation"),
			ActionIndexCache.Create("act_start_sad_conversation"),
			ActionIndexCache.Create("act_stand_sad_conversation"),
			ActionIndexCache.Create("act_start_happy_conversation"),
			ActionIndexCache.Create("act_stand_happy_conversation"),
			ActionIndexCache.Create("act_start_busy_conversation"),
			ActionIndexCache.Create("act_stand_busy_conversation"),
			ActionIndexCache.Create("act_explaining_conversation"),
			ActionIndexCache.Create("act_introduction_conversation"),
			ActionIndexCache.Create("act_wondering_conversation"),
			ActionIndexCache.Create("act_unknown_conversation"),
			ActionIndexCache.Create("act_friendly_conversation"),
			ActionIndexCache.Create("act_offer_conversation"),
			ActionIndexCache.Create("act_negative_conversation"),
			ActionIndexCache.Create("act_affermative_conversation"),
			ActionIndexCache.Create("act_secret_conversation"),
			ActionIndexCache.Create("act_remember_conversation"),
			ActionIndexCache.Create("act_laugh_conversation"),
			ActionIndexCache.Create("act_threat_conversation"),
			ActionIndexCache.Create("act_scared_conversation"),
			ActionIndexCache.Create("act_flirty_conversation"),
			ActionIndexCache.Create("act_thanks_conversation"),
			ActionIndexCache.Create("act_farewell_conversation"),
			ActionIndexCache.Create("act_troop_cavalry_sword"),
			ActionIndexCache.Create("act_inventory_idle_start"),
			ActionIndexCache.Create("act_inventory_idle"),
			ActionIndexCache.Create("act_character_developer_idle"),
			ActionIndexCache.Create("act_inventory_cloth_equip"),
			ActionIndexCache.Create("act_inventory_glove_equip"),
			ActionIndexCache.Create("act_jump"),
			ActionIndexCache.Create("act_jump_loop"),
			ActionIndexCache.Create("act_jump_end"),
			ActionIndexCache.Create("act_jump_end_hard"),
			ActionIndexCache.Create("act_jump_left_stance"),
			ActionIndexCache.Create("act_jump_loop_left_stance"),
			ActionIndexCache.Create("act_jump_end_left_stance"),
			ActionIndexCache.Create("act_jump_end_hard_left_stance"),
			ActionIndexCache.Create("act_jump_forward"),
			ActionIndexCache.Create("act_jump_forward_loop"),
			ActionIndexCache.Create("act_jump_forward_end"),
			ActionIndexCache.Create("act_jump_forward_end_hard"),
			ActionIndexCache.Create("act_jump_forward_left_stance"),
			ActionIndexCache.Create("act_jump_forward_loop_left_stance"),
			ActionIndexCache.Create("act_jump_forward_end_left_stance"),
			ActionIndexCache.Create("act_jump_forward_end_hard_left_stance"),
			ActionIndexCache.Create("act_jump_backward"),
			ActionIndexCache.Create("act_jump_backward_loop"),
			ActionIndexCache.Create("act_jump_backward_end"),
			ActionIndexCache.Create("act_jump_backward_end_hard"),
			ActionIndexCache.Create("act_jump_backward_left_stance"),
			ActionIndexCache.Create("act_jump_backward_loop_left_stance"),
			ActionIndexCache.Create("act_jump_backward_end_left_stance"),
			ActionIndexCache.Create("act_jump_backward_end_hard_left_stance"),
			ActionIndexCache.Create("act_jump_forward_right"),
			ActionIndexCache.Create("act_jump_forward_right_left_stance"),
			ActionIndexCache.Create("act_jump_forward_left"),
			ActionIndexCache.Create("act_jump_forward_left_left_stance"),
			ActionIndexCache.Create("act_jump_right"),
			ActionIndexCache.Create("act_jump_right_loop"),
			ActionIndexCache.Create("act_jump_right_end"),
			ActionIndexCache.Create("act_jump_right_end_hard"),
			ActionIndexCache.Create("act_jump_left"),
			ActionIndexCache.Create("act_jump_left_loop"),
			ActionIndexCache.Create("act_jump_left_end"),
			ActionIndexCache.Create("act_jump_left_end_hard"),
			ActionIndexCache.Create("act_jump_loop_long"),
			ActionIndexCache.Create("act_jump_loop_long_left_stance"),
			ActionIndexCache.Create("act_throne_sit_down_from_front"),
			ActionIndexCache.Create("act_throne_stand_up_to_front"),
			ActionIndexCache.Create("act_throne_sit_idle"),
			ActionIndexCache.Create("act_sit_down_from_front"),
			ActionIndexCache.Create("act_sit_down_from_right"),
			ActionIndexCache.Create("act_sit_down_from_left"),
			ActionIndexCache.Create("act_sit_down_on_floor_1"),
			ActionIndexCache.Create("act_sit_down_on_floor_2"),
			ActionIndexCache.Create("act_sit_down_on_floor_3"),
			ActionIndexCache.Create("act_stand_up_to_front"),
			ActionIndexCache.Create("act_stand_up_to_right"),
			ActionIndexCache.Create("act_stand_up_to_left"),
			ActionIndexCache.Create("act_stand_up_floor_1"),
			ActionIndexCache.Create("act_stand_up_floor_2"),
			ActionIndexCache.Create("act_stand_up_floor_3"),
			ActionIndexCache.Create("act_sit_1"),
			ActionIndexCache.Create("act_sit_2"),
			ActionIndexCache.Create("act_sit_3"),
			ActionIndexCache.Create("act_sit_4"),
			ActionIndexCache.Create("act_sit_5"),
			ActionIndexCache.Create("act_sit_6"),
			ActionIndexCache.Create("act_sit_7"),
			ActionIndexCache.Create("act_sit_8"),
			ActionIndexCache.Create("act_sit_idle_on_floor_1"),
			ActionIndexCache.Create("act_sit_idle_on_floor_2"),
			ActionIndexCache.Create("act_sit_idle_on_floor_3"),
			ActionIndexCache.Create("act_sit_conversation")
		};

		// Token: 0x040001E9 RID: 489
		private readonly ActionIndexCache act_inventory_idle_start = ActionIndexCache.Create("act_inventory_idle_start");

		// Token: 0x040001EA RID: 490
		public readonly ActionIndexCache PoseAction = ActionIndexCache.Create("act_walk_idle_unarmed");

		// Token: 0x040001EB RID: 491
		public string LordName = "main_hero";

		// Token: 0x040001EC RID: 492
		public bool IsWeaponWielded;

		// Token: 0x040001EF RID: 495
		private Vec2 MovementDirection;

		// Token: 0x040001F0 RID: 496
		private float MovementSpeed;

		// Token: 0x040001F1 RID: 497
		private float PhaseDiff;

		// Token: 0x040001F2 RID: 498
		private float Time;

		// Token: 0x040001F3 RID: 499
		private float ActionSetTimer;

		// Token: 0x040001F4 RID: 500
		private float ActionChangeInterval;

		// Token: 0x040001F5 RID: 501
		private float MovementDirectionChange;

		// Token: 0x040001F6 RID: 502
		private static MBGameManager _editorGameManager = null;

		// Token: 0x040001F7 RID: 503
		private static int _editorGameManagerRefCount = 0;

		// Token: 0x040001F8 RID: 504
		private static bool isFinished = false;

		// Token: 0x040001F9 RID: 505
		private static int gameTickFrameNo = -1;

		// Token: 0x040001FA RID: 506
		private bool CreateFaceImmediately = true;

		// Token: 0x040001FB RID: 507
		private AgentVisuals _agentVisuals;
	}
}

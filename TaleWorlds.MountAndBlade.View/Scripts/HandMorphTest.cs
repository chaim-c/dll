using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade.View.Scripts
{
	// Token: 0x0200003F RID: 63
	public class HandMorphTest : ScriptComponentBehavior
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0001A340 File Offset: 0x00018540
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0001A348 File Offset: 0x00018548
		public uint ClothColor1 { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0001A351 File Offset: 0x00018551
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x0001A359 File Offset: 0x00018559
		public uint ClothColor2 { get; private set; }

		// Token: 0x060002E1 RID: 737 RVA: 0x0001A364 File Offset: 0x00018564
		protected override void OnInit()
		{
			base.OnInit();
			this.ClothColor1 = uint.MaxValue;
			this.ClothColor2 = uint.MaxValue;
			if (this._agentVisuals == null && !this.characterSpawned)
			{
				this.SpawnCharacter();
				this.characterSpawned = true;
			}
			MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
			this._agentVisuals.GetVisuals().SetFrame(ref globalFrame);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0001A3C0 File Offset: 0x000185C0
		protected override void OnEditorInit()
		{
			base.OnEditorInit();
			if (Game.Current == null)
			{
				this._editorGameManager = new EditorGameManager();
			}
			this.ClothColor1 = uint.MaxValue;
			this.ClothColor2 = uint.MaxValue;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0001A3E8 File Offset: 0x000185E8
		protected override void OnEditorTick(float dt)
		{
			if (!this.isFinished && this._editorGameManager != null)
			{
				this.isFinished = !this._editorGameManager.DoLoadingForGameManager();
			}
			if (Game.Current != null && this._agentVisuals == null && !this.characterSpawned)
			{
				this.SpawnCharacter();
				this.characterSpawned = true;
			}
			MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
			this._agentVisuals.GetVisuals().SetFrame(ref globalFrame);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0001A45C File Offset: 0x0001865C
		public void SpawnCharacter()
		{
			CharacterCode characterCode = CharacterCode.CreateFrom(MBObjectManager.Instance.GetObject<BasicCharacterObject>("facgen_template_test_char_0"));
			this.InitWithCharacter(characterCode);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0001A485 File Offset: 0x00018685
		public void Reset()
		{
			if (this._agentVisuals != null)
			{
				this._agentVisuals.Reset();
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0001A49C File Offset: 0x0001869C
		public void InitWithCharacter(CharacterCode characterCode)
		{
			this.Reset();
			MatrixFrame frame = base.GameEntity.GetFrame();
			frame.rotation.s.z = 0f;
			frame.rotation.f.z = 0f;
			frame.rotation.s.Normalize();
			frame.rotation.f.Normalize();
			frame.rotation.u = Vec3.CrossProduct(frame.rotation.s, frame.rotation.f);
			characterCode.BodyProperties = new BodyProperties(new DynamicBodyProperties(20f, 0f, 0f), characterCode.BodyProperties.StaticProperties);
			Monster baseMonsterFromRace = FaceGen.GetBaseMonsterFromRace(characterCode.Race);
			this._agentVisuals = AgentVisuals.Create(new AgentVisualsData().Equipment(characterCode.CalculateEquipment()).BodyProperties(characterCode.BodyProperties).Race(characterCode.Race).SkeletonType(characterCode.IsFemale ? SkeletonType.Female : SkeletonType.Male).ActionSet(MBGlobals.GetActionSetWithSuffix(baseMonsterFromRace, characterCode.IsFemale, "_facegen")).ActionCode(this.act_visual_test_morph_animation).Scene(base.GameEntity.Scene).Monster(baseMonsterFromRace).PrepareImmediately(this.CreateFaceImmediately).UseMorphAnims(true).ClothColor1(this.ClothColor1).ClothColor2(this.ClothColor2).Frame(frame), "HandMorphTest", false, false, false);
			this._agentVisuals.SetAction(this.act_defend_up_fist_active, 1f, true);
			MatrixFrame globalFrame = frame;
			this._agentVisuals.GetVisuals().GetSkeleton().TickAnimationsAndForceUpdate(1f, globalFrame, true);
			this._agentVisuals.GetVisuals().SetFrame(ref globalFrame);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0001A65D File Offset: 0x0001885D
		protected override void OnRemoved(int removeReason)
		{
			base.OnRemoved(removeReason);
			this._agentVisuals.Reset();
		}

		// Token: 0x04000211 RID: 529
		private readonly ActionIndexCache act_defend_up_fist_active = ActionIndexCache.Create("act_defend_up_fist_active");

		// Token: 0x04000212 RID: 530
		private readonly ActionIndexCache act_visual_test_morph_animation = ActionIndexCache.Create("act_visual_test_morph_animation");

		// Token: 0x04000215 RID: 533
		private MBGameManager _editorGameManager;

		// Token: 0x04000216 RID: 534
		private bool isFinished;

		// Token: 0x04000217 RID: 535
		private bool characterSpawned;

		// Token: 0x04000218 RID: 536
		private bool CreateFaceImmediately = true;

		// Token: 0x04000219 RID: 537
		private AgentVisuals _agentVisuals;
	}
}

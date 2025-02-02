using System;
using System.Collections.Generic;
using SandBox.View.CharacterCreation;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.GauntletUI.BodyGenerator;
using TaleWorlds.MountAndBlade.ViewModelCollection.EscapeMenu;
using TaleWorlds.ObjectSystem;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.CharacterCreation
{
	// Token: 0x0200003D RID: 61
	[CharacterCreationStageView(typeof(CharacterCreationFaceGeneratorStage))]
	public class CharacterCreationFaceGeneratorView : CharacterCreationStageViewBase
	{
		// Token: 0x0600023B RID: 571 RVA: 0x0000FFF8 File Offset: 0x0000E1F8
		public CharacterCreationFaceGeneratorView(CharacterCreation characterCreation, ControlCharacterCreationStage affirmativeAction, TextObject affirmativeActionText, ControlCharacterCreationStage negativeAction, TextObject negativeActionText, ControlCharacterCreationStage onRefresh, ControlCharacterCreationStageReturnInt getCurrentStageIndexAction, ControlCharacterCreationStageReturnInt getTotalStageCountAction, ControlCharacterCreationStageReturnInt getFurthestIndexAction, ControlCharacterCreationStageWithInt goToIndexAction) : base(affirmativeAction, negativeAction, onRefresh, getTotalStageCountAction, getCurrentStageIndexAction, getFurthestIndexAction, goToIndexAction)
		{
			this._characterCreation = characterCreation;
			MBObjectManager objectManager = Game.Current.ObjectManager;
			string str = "player_char_creation_show_";
			CharacterObject playerCharacter = CharacterObject.PlayerCharacter;
			string str2;
			if (playerCharacter == null)
			{
				str2 = null;
			}
			else
			{
				CultureObject culture = playerCharacter.Culture;
				str2 = ((culture != null) ? culture.StringId : null);
			}
			MBEquipmentRoster @object = objectManager.GetObject<MBEquipmentRoster>(str + str2);
			Equipment dressedEquipment = (@object != null) ? @object.DefaultEquipment : null;
			this._faceGeneratorView = new BodyGeneratorView(new ControlCharacterCreationStage(this.NextStage), affirmativeActionText, new ControlCharacterCreationStage(this.PreviousStage), negativeActionText, Hero.MainHero.CharacterObject, false, null, dressedEquipment, getCurrentStageIndexAction, getTotalStageCountAction, getFurthestIndexAction, goToIndexAction);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0001009F File Offset: 0x0000E29F
		protected override void OnFinalize()
		{
			base.OnFinalize();
			this._faceGeneratorView.OnFinalize();
			this._faceGeneratorView = null;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000100B9 File Offset: 0x0000E2B9
		public override IEnumerable<ScreenLayer> GetLayers()
		{
			return new List<ScreenLayer>
			{
				this._faceGeneratorView.SceneLayer,
				this._faceGeneratorView.GauntletLayer
			};
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000100E2 File Offset: 0x0000E2E2
		public override void PreviousStage()
		{
			this._negativeAction();
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000100F0 File Offset: 0x0000E2F0
		public override void NextStage()
		{
			List<FaceGenChar> newChars = new List<FaceGenChar>
			{
				new FaceGenChar(this._faceGeneratorView.BodyGen.CurrentBodyProperties, this._faceGeneratorView.BodyGen.Race, new Equipment(), this._faceGeneratorView.BodyGen.IsFemale, "act_inventory_idle_start")
			};
			this._characterCreation.ChangeFaceGenChars(newChars);
			this._affirmativeAction();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0001015F File Offset: 0x0000E35F
		public override void Tick(float dt)
		{
			this._faceGeneratorView.OnTick(dt);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0001016D File Offset: 0x0000E36D
		public override int GetVirtualStageCount()
		{
			return 1;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00010170 File Offset: 0x0000E370
		public override void GoToIndex(int index)
		{
			this._goToIndexAction(index);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0001017E File Offset: 0x0000E37E
		public override void LoadEscapeMenuMovie()
		{
			this._escapeMenuDatasource = new EscapeMenuVM(base.GetEscapeMenuItems(this), null);
			this._escapeMenuMovie = this._faceGeneratorView.GauntletLayer.LoadMovie("EscapeMenu", this._escapeMenuDatasource);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000101B4 File Offset: 0x0000E3B4
		public override void ReleaseEscapeMenuMovie()
		{
			this._faceGeneratorView.GauntletLayer.ReleaseMovie(this._escapeMenuMovie);
			this._escapeMenuDatasource = null;
			this._escapeMenuMovie = null;
		}

		// Token: 0x04000130 RID: 304
		private BodyGeneratorView _faceGeneratorView;

		// Token: 0x04000131 RID: 305
		private readonly CharacterCreation _characterCreation;

		// Token: 0x04000132 RID: 306
		private EscapeMenuVM _escapeMenuDatasource;

		// Token: 0x04000133 RID: 307
		private IGauntletMovie _escapeMenuMovie;
	}
}

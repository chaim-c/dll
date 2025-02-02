using System;
using System.Collections.Generic;
using SandBox.GauntletUI.BannerEditor;
using SandBox.View.CharacterCreation;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection.EscapeMenu;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.CharacterCreation
{
	// Token: 0x0200003A RID: 58
	[CharacterCreationStageView(typeof(CharacterCreationBannerEditorStage))]
	public class CharacterCreationBannerEditorView : CharacterCreationStageViewBase
	{
		// Token: 0x0600020C RID: 524 RVA: 0x0000EE08 File Offset: 0x0000D008
		public CharacterCreationBannerEditorView(CharacterCreation characterCreation, ControlCharacterCreationStage affirmativeAction, TextObject affirmativeActionText, ControlCharacterCreationStage negativeAction, TextObject negativeActionText, ControlCharacterCreationStage onRefresh = null, ControlCharacterCreationStageReturnInt getCurrentStageIndexAction = null, ControlCharacterCreationStageReturnInt getTotalStageCountAction = null, ControlCharacterCreationStageReturnInt getFurthestIndexAction = null, ControlCharacterCreationStageWithInt goToIndexAction = null) : this(CharacterObject.PlayerCharacter, Clan.PlayerClan.Banner, affirmativeAction, affirmativeActionText, negativeAction, negativeActionText, onRefresh, getCurrentStageIndexAction, getTotalStageCountAction, getFurthestIndexAction, goToIndexAction)
		{
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000EE3C File Offset: 0x0000D03C
		public CharacterCreationBannerEditorView(BasicCharacterObject character, Banner banner, ControlCharacterCreationStage affirmativeAction, TextObject affirmativeActionText, ControlCharacterCreationStage negativeAction, TextObject negativeActionText, ControlCharacterCreationStage onRefresh = null, ControlCharacterCreationStageReturnInt getCurrentStageIndexAction = null, ControlCharacterCreationStageReturnInt getTotalStageCountAction = null, ControlCharacterCreationStageReturnInt getFurthestIndexAction = null, ControlCharacterCreationStageWithInt goToIndexAction = null) : base(affirmativeAction, negativeAction, onRefresh, getTotalStageCountAction, getCurrentStageIndexAction, getFurthestIndexAction, goToIndexAction)
		{
			this._bannerEditorView = new BannerEditorView(character, banner, new ControlCharacterCreationStage(this.AffirmativeAction), affirmativeActionText, negativeAction, negativeActionText, onRefresh, getCurrentStageIndexAction, getTotalStageCountAction, getFurthestIndexAction, goToIndexAction);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000EE85 File Offset: 0x0000D085
		public override IEnumerable<ScreenLayer> GetLayers()
		{
			return new List<ScreenLayer>
			{
				this._bannerEditorView.SceneLayer,
				this._bannerEditorView.GauntletLayer
			};
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000EEAE File Offset: 0x0000D0AE
		public override void PreviousStage()
		{
			this._bannerEditorView.Exit(true);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000EEBC File Offset: 0x0000D0BC
		public override void NextStage()
		{
			this._bannerEditorView.Exit(false);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000EECA File Offset: 0x0000D0CA
		public override void Tick(float dt)
		{
			if (!this._isFinalized)
			{
				this._bannerEditorView.OnTick(dt);
				if (this._isFinalized)
				{
					return;
				}
				base.HandleEscapeMenu(this, this._bannerEditorView.SceneLayer);
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000EEFB File Offset: 0x0000D0FB
		public override int GetVirtualStageCount()
		{
			return 1;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000EEFE File Offset: 0x0000D0FE
		public override void GoToIndex(int index)
		{
			this._bannerEditorView.GoToIndex(index);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000EF0C File Offset: 0x0000D10C
		protected override void OnFinalize()
		{
			this._bannerEditorView.OnDeactivate();
			this._bannerEditorView.OnFinalize();
			this._isFinalized = true;
			base.OnFinalize();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000EF34 File Offset: 0x0000D134
		private void AffirmativeAction()
		{
			uint primaryColor = this._bannerEditorView.Banner.GetPrimaryColor();
			uint firstIconColor = this._bannerEditorView.Banner.GetFirstIconColor();
			Clan playerClan = Clan.PlayerClan;
			playerClan.Color = primaryColor;
			playerClan.Color2 = firstIconColor;
			playerClan.UpdateBannerColor(primaryColor, firstIconColor);
			(GameStateManager.Current.ActiveState as CharacterCreationState).CurrentCharacterCreationContent.SetPlayerBanner(this._bannerEditorView.Banner);
			this._affirmativeAction();
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000EFAC File Offset: 0x0000D1AC
		public override void LoadEscapeMenuMovie()
		{
			this._escapeMenuDatasource = new EscapeMenuVM(base.GetEscapeMenuItems(this), null);
			this._escapeMenuMovie = this._bannerEditorView.GauntletLayer.LoadMovie("EscapeMenu", this._escapeMenuDatasource);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000EFE2 File Offset: 0x0000D1E2
		public override void ReleaseEscapeMenuMovie()
		{
			this._bannerEditorView.GauntletLayer.ReleaseMovie(this._escapeMenuMovie);
			this._escapeMenuDatasource = null;
			this._escapeMenuMovie = null;
		}

		// Token: 0x0400010B RID: 267
		private readonly BannerEditorView _bannerEditorView;

		// Token: 0x0400010C RID: 268
		private bool _isFinalized;

		// Token: 0x0400010D RID: 269
		private EscapeMenuVM _escapeMenuDatasource;

		// Token: 0x0400010E RID: 270
		private IGauntletMovie _escapeMenuMovie;
	}
}

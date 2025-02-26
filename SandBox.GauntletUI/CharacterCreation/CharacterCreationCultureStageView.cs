﻿using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.View.CharacterCreation;
using SandBox.View.Missions;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.ViewModelCollection.EscapeMenu;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace SandBox.GauntletUI.CharacterCreation
{
	// Token: 0x0200003C RID: 60
	[CharacterCreationStageView(typeof(CharacterCreationCultureStage))]
	public class CharacterCreationCultureStageView : CharacterCreationStageViewBase
	{
		// Token: 0x06000230 RID: 560 RVA: 0x0000FC08 File Offset: 0x0000DE08
		public CharacterCreationCultureStageView(CharacterCreation characterCreation, ControlCharacterCreationStage affirmativeAction, TextObject affirmativeActionText, ControlCharacterCreationStage negativeAction, TextObject negativeActionText, ControlCharacterCreationStage onRefresh, ControlCharacterCreationStageReturnInt getCurrentStageIndexAction, ControlCharacterCreationStageReturnInt getTotalStageCountAction, ControlCharacterCreationStageReturnInt getFurthestIndexAction, ControlCharacterCreationStageWithInt goToIndexAction) : base(affirmativeAction, negativeAction, onRefresh, getCurrentStageIndexAction, getTotalStageCountAction, getFurthestIndexAction, goToIndexAction)
		{
			this._characterCreation = characterCreation;
			this.GauntletLayer = new GauntletLayer(1, "GauntletLayer", true)
			{
				IsFocusLayer = true
			};
			this.GauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this.GauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			ScreenManager.TrySetFocus(this.GauntletLayer);
			this._dataSource = new CharacterCreationCultureStageVM(this._characterCreation, new Action(this.NextStage), affirmativeActionText, new Action(this.PreviousStage), negativeActionText, getCurrentStageIndexAction(), getTotalStageCountAction(), getFurthestIndexAction(), new Action<int>(this.GoToIndex), new Action<CultureObject>(this.OnCultureSelected));
			this._movie = this.GauntletLayer.LoadMovie("CharacterCreationCultureStage", this._dataSource);
			this._dataSource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._dataSource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._characterCreationCategory = spriteData.SpriteCategories["ui_charactercreation"];
			this._characterCreationCategory.Load(resourceContext, uiresourceDepot);
			CharacterCreationContentBase instance = CharacterCreationContentBase.Instance;
			bool flag;
			if (instance == null)
			{
				flag = false;
			}
			else
			{
				flag = instance.CharacterCreationStages.Any((Type c) => c.IsEquivalentTo(typeof(CharacterCreationBannerEditorStage)));
			}
			if (flag)
			{
				this._bannerEditorCategory = spriteData.SpriteCategories["ui_bannericons"];
				this._bannerEditorCategory.Load(resourceContext, uiresourceDepot);
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000FDC4 File Offset: 0x0000DFC4
		protected override void OnFinalize()
		{
			base.OnFinalize();
			this.GauntletLayer = null;
			CharacterCreationCultureStageVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.OnFinalize();
			}
			this._dataSource = null;
			this._characterCreationCategory.Unload();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000FDF8 File Offset: 0x0000DFF8
		private void HandleLayerInput()
		{
			if (this.GauntletLayer.Input.IsHotKeyReleased("Exit"))
			{
				UISoundsHelper.PlayUISound("event:/ui/panels/next");
				this._dataSource.OnPreviousStage();
				return;
			}
			if (this.GauntletLayer.Input.IsHotKeyReleased("Confirm") && this._dataSource.CanAdvance)
			{
				UISoundsHelper.PlayUISound("event:/ui/panels/next");
				this._dataSource.OnNextStage();
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000FE6B File Offset: 0x0000E06B
		public override void Tick(float dt)
		{
			base.Tick(dt);
			if (this._dataSource.IsActive)
			{
				base.HandleEscapeMenu(this, this.GauntletLayer);
				this.HandleLayerInput();
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000FE94 File Offset: 0x0000E094
		public override void NextStage()
		{
			this._characterCreation.Name = NameGenerator.Current.GenerateFirstNameForPlayer(this._dataSource.CurrentSelectedCulture.Culture, Hero.MainHero.IsFemale).ToString();
			this._affirmativeAction();
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000FEE0 File Offset: 0x0000E0E0
		private void OnCultureSelected(CultureObject culture)
		{
			MissionSoundParametersView.SoundParameterMissionCulture soundParameterMissionCulture = MissionSoundParametersView.SoundParameterMissionCulture.None;
			if (culture.StringId == "aserai")
			{
				soundParameterMissionCulture = MissionSoundParametersView.SoundParameterMissionCulture.Aserai;
			}
			else if (culture.StringId == "khuzait")
			{
				soundParameterMissionCulture = MissionSoundParametersView.SoundParameterMissionCulture.Khuzait;
			}
			else if (culture.StringId == "vlandia")
			{
				soundParameterMissionCulture = MissionSoundParametersView.SoundParameterMissionCulture.Vlandia;
			}
			else if (culture.StringId == "sturgia")
			{
				soundParameterMissionCulture = MissionSoundParametersView.SoundParameterMissionCulture.Sturgia;
			}
			else if (culture.StringId == "battania")
			{
				soundParameterMissionCulture = MissionSoundParametersView.SoundParameterMissionCulture.Battania;
			}
			else if (culture.StringId == "empire")
			{
				soundParameterMissionCulture = MissionSoundParametersView.SoundParameterMissionCulture.Empire;
			}
			SoundManager.SetGlobalParameter("MissionCulture", (float)soundParameterMissionCulture);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000FF7D File Offset: 0x0000E17D
		public override void PreviousStage()
		{
			Game.Current.GameStateManager.PopState(0);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000FF8F File Offset: 0x0000E18F
		public override int GetVirtualStageCount()
		{
			return 1;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000FF92 File Offset: 0x0000E192
		public override IEnumerable<ScreenLayer> GetLayers()
		{
			return new List<ScreenLayer>
			{
				this.GauntletLayer
			};
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000FFA5 File Offset: 0x0000E1A5
		public override void LoadEscapeMenuMovie()
		{
			this._escapeMenuDatasource = new EscapeMenuVM(base.GetEscapeMenuItems(this), null);
			this._escapeMenuMovie = this.GauntletLayer.LoadMovie("EscapeMenu", this._escapeMenuDatasource);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000FFD6 File Offset: 0x0000E1D6
		public override void ReleaseEscapeMenuMovie()
		{
			this.GauntletLayer.ReleaseMovie(this._escapeMenuMovie);
			this._escapeMenuDatasource = null;
			this._escapeMenuMovie = null;
		}

		// Token: 0x04000127 RID: 295
		private const string CultureParameterId = "MissionCulture";

		// Token: 0x04000128 RID: 296
		private readonly IGauntletMovie _movie;

		// Token: 0x04000129 RID: 297
		private GauntletLayer GauntletLayer;

		// Token: 0x0400012A RID: 298
		private CharacterCreationCultureStageVM _dataSource;

		// Token: 0x0400012B RID: 299
		private SpriteCategory _characterCreationCategory;

		// Token: 0x0400012C RID: 300
		private SpriteCategory _bannerEditorCategory;

		// Token: 0x0400012D RID: 301
		private readonly CharacterCreation _characterCreation;

		// Token: 0x0400012E RID: 302
		private EscapeMenuVM _escapeMenuDatasource;

		// Token: 0x0400012F RID: 303
		private IGauntletMovie _escapeMenuMovie;
	}
}

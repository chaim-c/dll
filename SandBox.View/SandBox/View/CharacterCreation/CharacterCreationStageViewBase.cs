using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.ViewModelCollection.EscapeMenu;
using TaleWorlds.ScreenSystem;

namespace SandBox.View.CharacterCreation
{
	// Token: 0x0200005D RID: 93
	public abstract class CharacterCreationStageViewBase : ICharacterCreationStageListener
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x00022C78 File Offset: 0x00020E78
		protected CharacterCreationStageViewBase(ControlCharacterCreationStage affirmativeAction, ControlCharacterCreationStage negativeAction, ControlCharacterCreationStage refreshAction, ControlCharacterCreationStageReturnInt getCurrentStageIndexAction, ControlCharacterCreationStageReturnInt getTotalStageCountAction, ControlCharacterCreationStageReturnInt getFurthestIndexAction, ControlCharacterCreationStageWithInt goToIndexAction)
		{
			this._affirmativeAction = affirmativeAction;
			this._negativeAction = negativeAction;
			this._refreshAction = refreshAction;
			this._getTotalStageCountAction = getTotalStageCountAction;
			this._getCurrentStageIndexAction = getCurrentStageIndexAction;
			this._getFurthestIndexAction = getFurthestIndexAction;
			this._goToIndexAction = goToIndexAction;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00022CDF File Offset: 0x00020EDF
		public virtual void SetGenericScene(Scene scene)
		{
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00022CE1 File Offset: 0x00020EE1
		protected virtual void OnRefresh()
		{
			this._refreshAction();
		}

		// Token: 0x06000425 RID: 1061
		public abstract IEnumerable<ScreenLayer> GetLayers();

		// Token: 0x06000426 RID: 1062
		public abstract void NextStage();

		// Token: 0x06000427 RID: 1063
		public abstract void PreviousStage();

		// Token: 0x06000428 RID: 1064 RVA: 0x00022CEE File Offset: 0x00020EEE
		void ICharacterCreationStageListener.OnStageFinalize()
		{
			this.OnFinalize();
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00022CF6 File Offset: 0x00020EF6
		protected virtual void OnFinalize()
		{
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00022CF8 File Offset: 0x00020EF8
		public virtual void Tick(float dt)
		{
		}

		// Token: 0x0600042B RID: 1067
		public abstract int GetVirtualStageCount();

		// Token: 0x0600042C RID: 1068 RVA: 0x00022CFA File Offset: 0x00020EFA
		public virtual void GoToIndex(int index)
		{
			this._goToIndexAction(index);
		}

		// Token: 0x0600042D RID: 1069
		public abstract void LoadEscapeMenuMovie();

		// Token: 0x0600042E RID: 1070
		public abstract void ReleaseEscapeMenuMovie();

		// Token: 0x0600042F RID: 1071 RVA: 0x00022D08 File Offset: 0x00020F08
		public void HandleEscapeMenu(CharacterCreationStageViewBase view, ScreenLayer screenLayer)
		{
			if (screenLayer.Input.IsHotKeyReleased("ToggleEscapeMenu"))
			{
				if (this._isEscapeOpen)
				{
					this.RemoveEscapeMenu(view);
					return;
				}
				this.OpenEscapeMenu(view);
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00022D33 File Offset: 0x00020F33
		private void OpenEscapeMenu(CharacterCreationStageViewBase view)
		{
			view.LoadEscapeMenuMovie();
			this._isEscapeOpen = true;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00022D42 File Offset: 0x00020F42
		private void RemoveEscapeMenu(CharacterCreationStageViewBase view)
		{
			view.ReleaseEscapeMenuMovie();
			this._isEscapeOpen = false;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00022D54 File Offset: 0x00020F54
		public List<EscapeMenuItemVM> GetEscapeMenuItems(CharacterCreationStageViewBase view)
		{
			TextObject characterCreationDisabledReason = GameTexts.FindText("str_pause_menu_disabled_hint", "CharacterCreation");
			List<EscapeMenuItemVM> list = new List<EscapeMenuItemVM>();
			list.Add(new EscapeMenuItemVM(new TextObject("{=5Saniypu}Resume", null), delegate(object o)
			{
				this.RemoveEscapeMenu(view);
			}, null, () => new Tuple<bool, TextObject>(false, TextObject.Empty), true));
			list.Add(new EscapeMenuItemVM(new TextObject("{=PXT6aA4J}Campaign Options", null), delegate(object o)
			{
			}, null, () => new Tuple<bool, TextObject>(true, characterCreationDisabledReason), false));
			list.Add(new EscapeMenuItemVM(new TextObject("{=NqarFr4P}Options", null), delegate(object o)
			{
			}, null, () => new Tuple<bool, TextObject>(true, characterCreationDisabledReason), false));
			list.Add(new EscapeMenuItemVM(new TextObject("{=bV75iwKa}Save", null), delegate(object o)
			{
			}, null, () => new Tuple<bool, TextObject>(true, characterCreationDisabledReason), false));
			list.Add(new EscapeMenuItemVM(new TextObject("{=e0KdfaNe}Save As", null), delegate(object o)
			{
			}, null, () => new Tuple<bool, TextObject>(true, characterCreationDisabledReason), false));
			list.Add(new EscapeMenuItemVM(new TextObject("{=9NuttOBC}Load", null), delegate(object o)
			{
			}, null, () => new Tuple<bool, TextObject>(true, characterCreationDisabledReason), false));
			list.Add(new EscapeMenuItemVM(new TextObject("{=AbEh2y8o}Save And Exit", null), delegate(object o)
			{
			}, null, () => new Tuple<bool, TextObject>(true, characterCreationDisabledReason), false));
			list.Add(new EscapeMenuItemVM(new TextObject("{=RamV6yLM}Exit to Main Menu", null), delegate(object o)
			{
				this.RemoveEscapeMenu(view);
				view.OnFinalize();
				MBGameManager.EndGame();
			}, null, () => new Tuple<bool, TextObject>(false, TextObject.Empty), false));
			return list;
		}

		// Token: 0x04000247 RID: 583
		protected readonly ControlCharacterCreationStage _affirmativeAction;

		// Token: 0x04000248 RID: 584
		protected readonly ControlCharacterCreationStage _negativeAction;

		// Token: 0x04000249 RID: 585
		protected readonly ControlCharacterCreationStage _refreshAction;

		// Token: 0x0400024A RID: 586
		protected readonly ControlCharacterCreationStageReturnInt _getTotalStageCountAction;

		// Token: 0x0400024B RID: 587
		protected readonly ControlCharacterCreationStageReturnInt _getCurrentStageIndexAction;

		// Token: 0x0400024C RID: 588
		protected readonly ControlCharacterCreationStageReturnInt _getFurthestIndexAction;

		// Token: 0x0400024D RID: 589
		protected readonly ControlCharacterCreationStageWithInt _goToIndexAction;

		// Token: 0x0400024E RID: 590
		protected readonly Vec3 _cameraPosition = new Vec3(6.45f, 4.35f, 1.6f, -1f);

		// Token: 0x0400024F RID: 591
		private bool _isEscapeOpen;
	}
}

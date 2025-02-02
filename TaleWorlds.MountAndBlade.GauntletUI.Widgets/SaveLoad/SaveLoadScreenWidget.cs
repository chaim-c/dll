using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.SaveLoad
{
	// Token: 0x02000056 RID: 86
	public class SaveLoadScreenWidget : Widget
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x0000E684 File Offset: 0x0000C884
		public SaveLoadScreenWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000E68D File Offset: 0x0000C88D
		public void SetCurrentSaveTuple(SavedGameTupleButtonWidget tuple)
		{
			if (tuple != null)
			{
				this.LoadButton.IsVisible = true;
				this._currentSelectedTuple = tuple;
				return;
			}
			this.LoadButton.IsEnabled = false;
			this._currentSelectedTuple = null;
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0000E6B9 File Offset: 0x0000C8B9
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x0000E6C1 File Offset: 0x0000C8C1
		[Editor(false)]
		public ButtonWidget LoadButton
		{
			get
			{
				return this._loadButton;
			}
			set
			{
				if (this._loadButton != value)
				{
					this._loadButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "LoadButton");
				}
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0000E6DF File Offset: 0x0000C8DF
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x0000E6E7 File Offset: 0x0000C8E7
		[Editor(false)]
		public bool IsSaving
		{
			get
			{
				return this._isSaving;
			}
			set
			{
				if (this._isSaving != value)
				{
					this._isSaving = value;
					base.OnPropertyChanged(value, "IsSaving");
				}
			}
		}

		// Token: 0x040001FB RID: 507
		private SavedGameTupleButtonWidget _currentSelectedTuple;

		// Token: 0x040001FC RID: 508
		private ButtonWidget _loadButton;

		// Token: 0x040001FD RID: 509
		private bool _isSaving;
	}
}

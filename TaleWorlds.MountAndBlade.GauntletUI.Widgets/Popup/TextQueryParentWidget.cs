using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Popup
{
	// Token: 0x0200005B RID: 91
	public class TextQueryParentWidget : Widget
	{
		// Token: 0x060004CE RID: 1230 RVA: 0x0000EE19 File Offset: 0x0000D019
		public TextQueryParentWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000EE22 File Offset: 0x0000D022
		private void FocusOnTextQuery()
		{
			base.EventManager.SetWidgetFocused(this.TextInputWidget, true);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000EE36 File Offset: 0x0000D036
		protected override void OnConnectedToRoot()
		{
			base.OnConnectedToRoot();
			if (this.TextInputWidget != null)
			{
				this.FocusOnTextQuery();
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000EE4C File Offset: 0x0000D04C
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x0000EE54 File Offset: 0x0000D054
		[Editor(false)]
		public EditableTextWidget TextInputWidget
		{
			get
			{
				return this._editableTextWidget;
			}
			set
			{
				if (value != this._editableTextWidget)
				{
					this._editableTextWidget = value;
					this.FocusOnTextQuery();
				}
			}
		}

		// Token: 0x04000219 RID: 537
		private EditableTextWidget _editableTextWidget;
	}
}

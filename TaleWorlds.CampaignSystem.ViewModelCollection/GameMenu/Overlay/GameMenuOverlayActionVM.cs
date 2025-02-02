using System;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Overlay
{
	// Token: 0x020000A7 RID: 167
	public class GameMenuOverlayActionVM : StringItemWithEnabledAndHintVM
	{
		// Token: 0x060010AC RID: 4268 RVA: 0x00041D4A File Offset: 0x0003FF4A
		public GameMenuOverlayActionVM(Action<object> onExecute, string item, bool isEnabled, object identifier, TextObject hint = null) : base(onExecute, item, isEnabled, identifier, hint)
		{
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x00041D59 File Offset: 0x0003FF59
		// (set) Token: 0x060010AE RID: 4270 RVA: 0x00041D61 File Offset: 0x0003FF61
		[DataSourceProperty]
		public bool IsHiglightEnabled
		{
			get
			{
				return this._isHiglightEnabled;
			}
			set
			{
				if (value != this._isHiglightEnabled)
				{
					this._isHiglightEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsHiglightEnabled");
				}
			}
		}

		// Token: 0x040007BC RID: 1980
		private bool _isHiglightEnabled;
	}
}

using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000037 RID: 55
	public class SceneNotificationDescriptionTextWidget : TextWidget
	{
		// Token: 0x06000306 RID: 774 RVA: 0x00009DDC File Offset: 0x00007FDC
		public SceneNotificationDescriptionTextWidget(UIContext context) : base(context)
		{
			this._defaultAlignment = base.ReadOnlyBrush.TextHorizontalAlignment;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00009DF8 File Offset: 0x00007FF8
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._text.LineCount != this._cachedLineCount)
			{
				this._cachedLineCount = this._text.LineCount;
				if (this._cachedLineCount == 1)
				{
					base.ReadOnlyBrush.TextHorizontalAlignment = this._defaultAlignment;
					return;
				}
				base.ReadOnlyBrush.TextHorizontalAlignment = this.MultiLineAlignment;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00009E5C File Offset: 0x0000805C
		// (set) Token: 0x06000309 RID: 777 RVA: 0x00009E64 File Offset: 0x00008064
		[Editor(false)]
		public TextHorizontalAlignment MultiLineAlignment
		{
			get
			{
				return this._multiLineAlignment;
			}
			set
			{
				this._multiLineAlignment = value;
			}
		}

		// Token: 0x0400013A RID: 314
		private int _cachedLineCount;

		// Token: 0x0400013B RID: 315
		private TextHorizontalAlignment _defaultAlignment;

		// Token: 0x0400013C RID: 316
		private TextHorizontalAlignment _multiLineAlignment;
	}
}

using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Options.Gamepad
{
	// Token: 0x02000072 RID: 114
	public class OptionsGamepadCategoryWidget : Widget
	{
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00012658 File Offset: 0x00010858
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x00012660 File Offset: 0x00010860
		public Widget Playstation4LayoutParentWidget { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00012669 File Offset: 0x00010869
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x00012671 File Offset: 0x00010871
		public Widget Playstation5LayoutParentWidget { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0001267A File Offset: 0x0001087A
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x00012682 File Offset: 0x00010882
		public Widget XboxLayoutParentWidget { get; set; }

		// Token: 0x06000639 RID: 1593 RVA: 0x0001268B File Offset: 0x0001088B
		public OptionsGamepadCategoryWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001269B File Offset: 0x0001089B
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initalized)
			{
				this.SetGamepadLayoutVisibility(this.CurrentGamepadType);
				this._initalized = true;
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000126C0 File Offset: 0x000108C0
		private void SetGamepadLayoutVisibility(int gamepadType)
		{
			this.XboxLayoutParentWidget.IsVisible = false;
			this.Playstation4LayoutParentWidget.IsVisible = false;
			this.Playstation5LayoutParentWidget.IsVisible = false;
			if (gamepadType == 0)
			{
				this.XboxLayoutParentWidget.IsVisible = true;
				return;
			}
			if (gamepadType == 1)
			{
				this.Playstation4LayoutParentWidget.IsVisible = true;
				return;
			}
			if (gamepadType == 2)
			{
				this.Playstation5LayoutParentWidget.IsVisible = true;
				return;
			}
			this.XboxLayoutParentWidget.IsVisible = true;
			Debug.FailedAssert("This kind of gamepad is not visually supported", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\Options\\Gamepad\\OptionsGamepadCategoryWidget.cs", "SetGamepadLayoutVisibility", 47);
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x00012745 File Offset: 0x00010945
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x0001274D File Offset: 0x0001094D
		public int CurrentGamepadType
		{
			get
			{
				return this._currentGamepadType;
			}
			set
			{
				if (this._currentGamepadType != value)
				{
					this._currentGamepadType = value;
				}
			}
		}

		// Token: 0x040002AF RID: 687
		private bool _initalized;

		// Token: 0x040002B0 RID: 688
		private int _currentGamepadType = -1;
	}
}

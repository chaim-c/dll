using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.CharacterCreation.Options
{
	// Token: 0x0200017D RID: 381
	public class CharacterCreationOptionsItemWidget : Widget
	{
		// Token: 0x060013AF RID: 5039 RVA: 0x00035D60 File Offset: 0x00033F60
		public CharacterCreationOptionsItemWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00035D70 File Offset: 0x00033F70
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._isDirty)
			{
				if (this.Type == 0)
				{
					this.ActionOptionWidget.IsVisible = false;
					this.BooleanOptionWidget.IsVisible = true;
					this.SelectionOptionWidget.IsVisible = false;
					this.NumericOptionWidget.IsVisible = false;
				}
				else if (this.Type == 1)
				{
					this.ActionOptionWidget.IsVisible = false;
					this.BooleanOptionWidget.IsVisible = false;
					this.SelectionOptionWidget.IsVisible = false;
					this.NumericOptionWidget.IsVisible = true;
				}
				else if (this.Type == 2)
				{
					this.ActionOptionWidget.IsVisible = false;
					this.BooleanOptionWidget.IsVisible = false;
					this.SelectionOptionWidget.IsVisible = true;
					this.NumericOptionWidget.IsVisible = false;
				}
				else if (this.Type == 3)
				{
					this.ActionOptionWidget.IsVisible = true;
					this.BooleanOptionWidget.IsVisible = false;
					this.SelectionOptionWidget.IsVisible = false;
					this.NumericOptionWidget.IsVisible = false;
				}
				this.ResetNavigationIndices();
				this._isDirty = false;
			}
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00035E88 File Offset: 0x00034088
		private void ResetNavigationIndices()
		{
			if (base.GamepadNavigationIndex == -1)
			{
				return;
			}
			bool flag = false;
			Widget booleanOptionWidget = this.BooleanOptionWidget;
			if (booleanOptionWidget != null && booleanOptionWidget.IsVisible)
			{
				this.BooleanOptionWidget.GamepadNavigationIndex = base.GamepadNavigationIndex;
				flag = true;
			}
			else
			{
				Widget numericOptionWidget = this.NumericOptionWidget;
				if (numericOptionWidget != null && numericOptionWidget.IsVisible)
				{
					this.NumericOptionWidget.GamepadNavigationIndex = base.GamepadNavigationIndex;
					flag = true;
				}
				else
				{
					Widget selectionOptionWidget = this.SelectionOptionWidget;
					if (selectionOptionWidget != null && selectionOptionWidget.IsVisible)
					{
						this.SelectionOptionWidget.GamepadNavigationIndex = base.GamepadNavigationIndex;
						flag = true;
					}
					else
					{
						Widget actionOptionWidget = this.ActionOptionWidget;
						if (actionOptionWidget != null && actionOptionWidget.IsVisible)
						{
							this.ActionOptionWidget.GamepadNavigationIndex = base.GamepadNavigationIndex;
							flag = true;
						}
					}
				}
			}
			if (flag)
			{
				base.GamepadNavigationIndex = -1;
			}
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00035F4D File Offset: 0x0003414D
		protected override void OnGamepadNavigationIndexUpdated(int newIndex)
		{
			base.OnGamepadNavigationIndexUpdated(newIndex);
			this.ResetNavigationIndices();
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x00035F5C File Offset: 0x0003415C
		// (set) Token: 0x060013B4 RID: 5044 RVA: 0x00035F64 File Offset: 0x00034164
		[Editor(false)]
		public int Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (this._type != value)
				{
					this._type = value;
					base.OnPropertyChanged(value, "Type");
					this._isDirty = true;
				}
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x00035F89 File Offset: 0x00034189
		// (set) Token: 0x060013B6 RID: 5046 RVA: 0x00035F91 File Offset: 0x00034191
		[Editor(false)]
		public Widget ActionOptionWidget
		{
			get
			{
				return this._actionOptionWidget;
			}
			set
			{
				if (this._actionOptionWidget != value)
				{
					this._actionOptionWidget = value;
					base.OnPropertyChanged<Widget>(value, "ActionOptionWidget");
				}
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x00035FAF File Offset: 0x000341AF
		// (set) Token: 0x060013B8 RID: 5048 RVA: 0x00035FB7 File Offset: 0x000341B7
		[Editor(false)]
		public Widget NumericOptionWidget
		{
			get
			{
				return this._numericOptionWidget;
			}
			set
			{
				if (this._numericOptionWidget != value)
				{
					this._numericOptionWidget = value;
					base.OnPropertyChanged<Widget>(value, "NumericOptionWidget");
				}
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x00035FD5 File Offset: 0x000341D5
		// (set) Token: 0x060013BA RID: 5050 RVA: 0x00035FDD File Offset: 0x000341DD
		[Editor(false)]
		public Widget SelectionOptionWidget
		{
			get
			{
				return this._selectionOptionWidget;
			}
			set
			{
				if (this._selectionOptionWidget != value)
				{
					this._selectionOptionWidget = value;
					base.OnPropertyChanged<Widget>(value, "SelectionOptionWidget");
				}
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x00035FFB File Offset: 0x000341FB
		// (set) Token: 0x060013BC RID: 5052 RVA: 0x00036003 File Offset: 0x00034203
		[Editor(false)]
		public Widget BooleanOptionWidget
		{
			get
			{
				return this._booleanOptionWidget;
			}
			set
			{
				if (this._booleanOptionWidget != value)
				{
					this._booleanOptionWidget = value;
					base.OnPropertyChanged<Widget>(value, "BooleanOptionWidget");
				}
			}
		}

		// Token: 0x040008F7 RID: 2295
		private bool _isDirty = true;

		// Token: 0x040008F8 RID: 2296
		private int _type;

		// Token: 0x040008F9 RID: 2297
		private Widget _actionOptionWidget;

		// Token: 0x040008FA RID: 2298
		private Widget _numericOptionWidget;

		// Token: 0x040008FB RID: 2299
		private Widget _selectionOptionWidget;

		// Token: 0x040008FC RID: 2300
		private Widget _booleanOptionWidget;
	}
}

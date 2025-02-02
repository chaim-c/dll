using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Options.Gamepad
{
	// Token: 0x02000075 RID: 117
	public class OptionsGamepadVisualWidget : Widget
	{
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00012BFF File Offset: 0x00010DFF
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x00012C07 File Offset: 0x00010E07
		public Widget ParentAreaWidget { get; set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00012C10 File Offset: 0x00010E10
		private float _verticalMarginBetweenKeys
		{
			get
			{
				return 20f;
			}
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00012C17 File Offset: 0x00010E17
		public OptionsGamepadVisualWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00012C38 File Offset: 0x00010E38
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initalized)
			{
				using (List<Widget>.Enumerator enumerator = base.ParentWidget.Children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						OptionsGamepadKeyLocationWidget item;
						if ((item = (enumerator.Current as OptionsGamepadKeyLocationWidget)) != null)
						{
							this._allKeyLocations.Add(item);
						}
					}
				}
				this._initalized = true;
			}
			if (this._isKeysDirty)
			{
				this._allKeyLocations.ForEach(delegate(OptionsGamepadKeyLocationWidget k)
				{
					k.SetKeyProperties(string.Empty, this.ParentAreaWidget);
				});
				foreach (Widget widget in base.Children)
				{
					OptionsGamepadOptionItemListPanel optionItem;
					if ((optionItem = (widget as OptionsGamepadOptionItemListPanel)) != null)
					{
						OptionsGamepadKeyLocationWidget optionsGamepadKeyLocationWidget = this._allKeyLocations.Find((OptionsGamepadKeyLocationWidget l) => l.KeyID == optionItem.KeyId);
						if (optionsGamepadKeyLocationWidget != null)
						{
							optionItem.SetKeyProperties(optionsGamepadKeyLocationWidget, this.ParentAreaWidget);
						}
						else
						{
							optionItem.IsVisible = false;
						}
					}
				}
				this._isKeysDirty = false;
			}
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00012D70 File Offset: 0x00010F70
		private void OnActionTextChanged()
		{
			this._isKeysDirty = true;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00012D7C File Offset: 0x00010F7C
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			this._isKeysDirty = true;
			OptionsGamepadOptionItemListPanel optionsGamepadOptionItemListPanel;
			if ((optionsGamepadOptionItemListPanel = (child as OptionsGamepadOptionItemListPanel)) != null && !this._allChildKeyItems.Contains(optionsGamepadOptionItemListPanel))
			{
				this._allChildKeyItems.Add(optionsGamepadOptionItemListPanel);
				optionsGamepadOptionItemListPanel.OnActionTextChanged += this.OnActionTextChanged;
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00012DD0 File Offset: 0x00010FD0
		protected override void OnChildRemoved(Widget child)
		{
			base.OnChildRemoved(child);
			this._isKeysDirty = true;
			OptionsGamepadOptionItemListPanel optionsGamepadOptionItemListPanel;
			if ((optionsGamepadOptionItemListPanel = (child as OptionsGamepadOptionItemListPanel)) != null && this._allChildKeyItems.Contains(optionsGamepadOptionItemListPanel))
			{
				this._allChildKeyItems.Remove(optionsGamepadOptionItemListPanel);
				optionsGamepadOptionItemListPanel.OnActionTextChanged -= this.OnActionTextChanged;
			}
		}

		// Token: 0x040002C3 RID: 707
		private List<OptionsGamepadKeyLocationWidget> _allKeyLocations = new List<OptionsGamepadKeyLocationWidget>();

		// Token: 0x040002C4 RID: 708
		private List<OptionsGamepadOptionItemListPanel> _allChildKeyItems = new List<OptionsGamepadOptionItemListPanel>();

		// Token: 0x040002C6 RID: 710
		private bool _initalized;

		// Token: 0x040002C7 RID: 711
		private bool _isKeysDirty;
	}
}

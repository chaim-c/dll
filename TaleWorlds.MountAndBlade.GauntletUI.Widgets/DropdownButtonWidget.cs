using System;
using System.Linq;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000018 RID: 24
	public class DropdownButtonWidget : ButtonWidget
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000053AB File Offset: 0x000035AB
		// (set) Token: 0x06000138 RID: 312 RVA: 0x000053B4 File Offset: 0x000035B4
		public Widget DisplayedList
		{
			get
			{
				return this._displayedList;
			}
			set
			{
				if (value != this._displayedList)
				{
					if (this._displayedList != null)
					{
						ListPanel listPanel = this._displayedList.AllChildrenAndThis.FirstOrDefault((Widget x) => x is ListPanel) as ListPanel;
						if (listPanel != null)
						{
							listPanel.SelectEventHandlers.Remove(new Action<Widget>(this.OnListItemSelected));
						}
					}
					this._displayedList = value;
					this._displayedList.IsVisible = false;
					this._isDisplayingList = false;
					ListPanel listPanel2 = this._displayedList.AllChildrenAndThis.FirstOrDefault((Widget x) => x is ListPanel) as ListPanel;
					if (listPanel2 != null)
					{
						listPanel2.SelectEventHandlers.Add(new Action<Widget>(this.OnListItemSelected));
					}
				}
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000548E File Offset: 0x0000368E
		public DropdownButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005497 File Offset: 0x00003697
		private void OnListItemSelected(Widget list)
		{
			this.HideList();
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000054A0 File Offset: 0x000036A0
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._isDisplayingList)
			{
				this.DisplayedList.ScaledPositionXOffset = Mathf.Clamp(base.GlobalPosition.X, 0f, base.EventManager.Root.Size.X * base._inverseScaleToUse - this.DisplayedList.Size.X);
				this.DisplayedList.ScaledPositionYOffset = Mathf.Clamp(base.GlobalPosition.Y + base.Size.Y, 0f, base.EventManager.Root.Size.Y * base._inverseScaleToUse - this.DisplayedList.Size.Y);
				if (base.EventManager.LatestMouseUpWidget == null)
				{
					this.HideList();
					return;
				}
				if (base.EventManager.LatestMouseUpWidget != this && !this.DisplayedList.CheckIsMyChildRecursive(base.EventManager.LatestMouseUpWidget))
				{
					this.HideList();
				}
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000055A8 File Offset: 0x000037A8
		private void DisplayList()
		{
			this.DisplayedList.ParentWidget = base.EventManager.Root;
			this.DisplayedList.IsVisible = true;
			this.DisplayedList.HorizontalAlignment = HorizontalAlignment.Left;
			this.DisplayedList.VerticalAlignment = VerticalAlignment.Top;
			this._isDisplayingList = true;
			base.DoNotUseCustomScaleAndChildren = false;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005600 File Offset: 0x00003800
		private void HideList()
		{
			this.DisplayedList.ParentWidget = this;
			this.DisplayedList.IsVisible = false;
			this.DisplayedList.PositionXOffset = 0f;
			this.DisplayedList.PositionYOffset = 0f;
			this._isDisplayingList = false;
			base.DoNotUseCustomScaleAndChildren = true;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005653 File Offset: 0x00003853
		protected override void OnClick()
		{
			base.OnClick();
			if (this.DisplayedList != null)
			{
				if (!this._isDisplayingList)
				{
					this.DisplayList();
					return;
				}
				this.HideList();
			}
		}

		// Token: 0x04000095 RID: 149
		private Widget _displayedList;

		// Token: 0x04000096 RID: 150
		private bool _isDisplayingList;
	}
}

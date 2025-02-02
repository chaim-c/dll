using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Options
{
	// Token: 0x02000071 RID: 113
	public class OptionsScreenWidget : Widget
	{
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00012470 File Offset: 0x00010670
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x00012478 File Offset: 0x00010678
		public Widget VideoMemoryUsageWidget { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00012481 File Offset: 0x00010681
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x00012489 File Offset: 0x00010689
		public RichTextWidget CurrentOptionDescriptionWidget { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00012492 File Offset: 0x00010692
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x0001249A File Offset: 0x0001069A
		public RichTextWidget CurrentOptionNameWidget { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x000124A3 File Offset: 0x000106A3
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x000124AB File Offset: 0x000106AB
		public Widget CurrentOptionImageWidget { get; set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x000124B4 File Offset: 0x000106B4
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x000124BC File Offset: 0x000106BC
		public TabToggleWidget PerformanceTabToggle { get; set; }

		// Token: 0x0600062E RID: 1582 RVA: 0x000124C5 File Offset: 0x000106C5
		public OptionsScreenWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000124CE File Offset: 0x000106CE
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (!this._initialized)
			{
				this.PerformanceTabToggle.TabControlWidget.OnActiveTabChange += this.OnActiveTabChange;
				this.VideoMemoryUsageWidget.IsVisible = false;
				this._initialized = true;
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001250E File Offset: 0x0001070E
		private void OnActiveTabChange()
		{
			this.VideoMemoryUsageWidget.IsVisible = (this.PerformanceTabToggle.TabControlWidget.ActiveTab.Id == "PerformanceOptionsPage");
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001253A File Offset: 0x0001073A
		protected override void OnDisconnectedFromRoot()
		{
			base.OnDisconnectedFromRoot();
			TabToggleWidget performanceTabToggle = this.PerformanceTabToggle;
			if (((performanceTabToggle != null) ? performanceTabToggle.TabControlWidget : null) != null)
			{
				this.PerformanceTabToggle.TabControlWidget.OnActiveTabChange += this.OnActiveTabChange;
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00012574 File Offset: 0x00010774
		public void SetCurrentOption(Widget currentOptionWidget, Sprite newgraphicsSprite)
		{
			if (this._currentOptionWidget != currentOptionWidget)
			{
				this._currentOptionWidget = currentOptionWidget;
				string text = "";
				string text2 = "";
				if (this._currentOptionWidget != null)
				{
					OptionsItemWidget optionsItemWidget;
					OptionsKeyItemListPanel optionsKeyItemListPanel;
					if ((optionsItemWidget = (this._currentOptionWidget as OptionsItemWidget)) != null)
					{
						text = optionsItemWidget.OptionDescription;
						text2 = optionsItemWidget.OptionTitle;
					}
					else if ((optionsKeyItemListPanel = (this._currentOptionWidget as OptionsKeyItemListPanel)) != null)
					{
						text = optionsKeyItemListPanel.OptionDescription;
						text2 = optionsKeyItemListPanel.OptionTitle;
					}
				}
				if (this.CurrentOptionDescriptionWidget != null)
				{
					this.CurrentOptionDescriptionWidget.Text = text;
				}
				if (this.CurrentOptionDescriptionWidget != null)
				{
					this.CurrentOptionNameWidget.Text = text2;
				}
			}
			if (this.CurrentOptionImageWidget != null && this.CurrentOptionImageWidget.Sprite != newgraphicsSprite)
			{
				this.CurrentOptionImageWidget.Sprite = newgraphicsSprite;
				if (newgraphicsSprite != null)
				{
					float num = this.CurrentOptionImageWidget.SuggestedWidth / (float)newgraphicsSprite.Width;
					this.CurrentOptionImageWidget.SuggestedHeight = (float)newgraphicsSprite.Height * num;
				}
			}
		}

		// Token: 0x040002A5 RID: 677
		private Widget _currentOptionWidget;

		// Token: 0x040002AB RID: 683
		private bool _initialized;
	}
}

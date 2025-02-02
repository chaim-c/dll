using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000011 RID: 17
	public class ContainerPageControlButtonListWidget : ContainerPageControlWidget
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00003C6A File Offset: 0x00001E6A
		public ContainerPageControlButtonListWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003C7E File Offset: 0x00001E7E
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			this.RefreshButtonList();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003C90 File Offset: 0x00001E90
		private void RefreshButtonList()
		{
			if (!this._buttonsInitialized)
			{
				this._pageButtonsList = new List<ButtonWidget>();
				if (base.HasChild(this.PageButtonTemplate))
				{
					base.RemoveChild(this.PageButtonTemplate);
				}
				base.RemoveAllChildren();
				if (this.PageButtonItemsListPanel != null && this.PageButtonTemplate != null && this.EmptyButtonBrush != null && this.FullButtonBrush != null)
				{
					this.PageButtonItemsListPanel.RemoveAllChildren();
					if (base.PageCount == 1)
					{
						base.IsVisible = false;
					}
					else
					{
						for (int i = 0; i < base.PageCount; i++)
						{
							ButtonWidget buttonWidget = new ButtonWidget(base.Context);
							this.PageButtonItemsListPanel.AddChild(buttonWidget);
							buttonWidget.Brush = this.PageButtonTemplate.ReadOnlyBrush;
							buttonWidget.DoNotAcceptEvents = false;
							buttonWidget.SuggestedHeight = this.PageButtonTemplate.SuggestedHeight;
							buttonWidget.SuggestedWidth = this.PageButtonTemplate.SuggestedWidth;
							buttonWidget.MarginLeft = this.PageButtonTemplate.MarginLeft;
							buttonWidget.MarginRight = this.PageButtonTemplate.MarginRight;
							buttonWidget.DoNotPassEventsToChildren = this.PageButtonTemplate.DoNotPassEventsToChildren;
							buttonWidget.ClickEventHandlers.Add(new Action<Widget>(this.OnPageSelection));
							this._pageButtonsList.Add(buttonWidget);
						}
						this.UpdatePageButtonBrushes();
					}
					this._buttonsInitialized = true;
				}
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003DEB File Offset: 0x00001FEB
		protected override void OnInitialized()
		{
			base.OnInitialized();
			this._buttonsInitialized = false;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003DFA File Offset: 0x00001FFA
		protected override void OnContainerItemsUpdated()
		{
			base.OnContainerItemsUpdated();
			this.UpdatePageButtonBrushes();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003E08 File Offset: 0x00002008
		private void OnPageSelection(Widget stageButton)
		{
			int index = this._pageButtonsList.IndexOf(stageButton as ButtonWidget);
			base.GoToPage(index);
			this.UpdatePageButtonBrushes();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003E34 File Offset: 0x00002034
		private void UpdatePageButtonBrushes()
		{
			if (this._pageButtonsList.Count < base.PageCount)
			{
				return;
			}
			for (int i = 0; i < base.PageCount; i++)
			{
				if (i == this._currentPageIndex)
				{
					this._pageButtonsList[i].Brush = base.EventManager.Context.Brushes.First((Brush b) => b.Name == this.FullButtonBrush);
				}
				else
				{
					this._pageButtonsList[i].Brush = base.EventManager.Context.Brushes.First((Brush b) => b.Name == this.EmptyButtonBrush);
				}
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003ED5 File Offset: 0x000020D5
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00003EDD File Offset: 0x000020DD
		[Editor(false)]
		public ButtonWidget PageButtonTemplate
		{
			get
			{
				return this._pageButtonTemplate;
			}
			set
			{
				if (value != this._pageButtonTemplate)
				{
					this._pageButtonTemplate = value;
					base.OnPropertyChanged<ButtonWidget>(value, "PageButtonTemplate");
				}
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003EFB File Offset: 0x000020FB
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00003F03 File Offset: 0x00002103
		[Editor(false)]
		public string FullButtonBrush
		{
			get
			{
				return this._fullButtonBrush;
			}
			set
			{
				if (this._fullButtonBrush != value)
				{
					this._fullButtonBrush = value;
					base.OnPropertyChanged<string>(value, "FullButtonBrush");
				}
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003F26 File Offset: 0x00002126
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00003F2E File Offset: 0x0000212E
		[Editor(false)]
		public string EmptyButtonBrush
		{
			get
			{
				return this._emptyButtonBrush;
			}
			set
			{
				if (this._emptyButtonBrush != value)
				{
					this._emptyButtonBrush = value;
					base.OnPropertyChanged<string>(value, "EmptyButtonBrush");
				}
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003F51 File Offset: 0x00002151
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00003F59 File Offset: 0x00002159
		[Editor(false)]
		public ListPanel PageButtonItemsListPanel
		{
			get
			{
				return this._pageButtonItemsListPanel;
			}
			set
			{
				if (value != this._pageButtonItemsListPanel)
				{
					this._pageButtonItemsListPanel = value;
					base.OnPropertyChanged<ListPanel>(value, "PageButtonItemsListPanel");
				}
			}
		}

		// Token: 0x04000059 RID: 89
		private List<ButtonWidget> _pageButtonsList = new List<ButtonWidget>();

		// Token: 0x0400005A RID: 90
		private bool _buttonsInitialized;

		// Token: 0x0400005B RID: 91
		private ButtonWidget _pageButtonTemplate;

		// Token: 0x0400005C RID: 92
		private ListPanel _pageButtonItemsListPanel;

		// Token: 0x0400005D RID: 93
		private string _fullButtonBrush;

		// Token: 0x0400005E RID: 94
		private string _emptyButtonBrush;
	}
}

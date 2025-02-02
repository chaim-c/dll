using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.CharacterCreation
{
	// Token: 0x0200017C RID: 380
	public class CharacterCreationStageSelectionBarListPanel : ListPanel
	{
		// Token: 0x06001395 RID: 5013 RVA: 0x000358EF File Offset: 0x00033AEF
		public CharacterCreationStageSelectionBarListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00035918 File Offset: 0x00033B18
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			this.RefreshButtonList();
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00035928 File Offset: 0x00033B28
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this.BarFillWidget != null && this.TotalStagesCount != 0 && this._buttonsInitialized && this.CurrentStageIndex != -1)
			{
				this.BarFillWidget.ScaledSuggestedWidth = this.BarCanvasWidget.Size.X - this._stageButtonsList[this._stageButtonsList.Count - 1 - this.CurrentStageIndex].LocalPosition.X;
			}
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x000359A4 File Offset: 0x00033BA4
		private void RefreshButtonList()
		{
			if (!this._buttonsInitialized)
			{
				this._stageButtonsList = new List<ButtonWidget>();
				if (base.HasChild(this.StageButtonTemplate))
				{
					base.RemoveChild(this.StageButtonTemplate);
				}
				base.RemoveAllChildren();
				if (this.StageButtonTemplate != null && this.EmptyButtonBrush != null && this.FullButtonBrush != null && this.FullBrightButtonBrush != null)
				{
					if (this.TotalStagesCount == 0)
					{
						this.BarCanvasWidget.IsVisible = false;
						base.IsVisible = false;
					}
					else
					{
						for (int i = 0; i < this.TotalStagesCount; i++)
						{
							ButtonWidget buttonWidget = new ButtonWidget(base.Context);
							base.AddChild(buttonWidget);
							buttonWidget.Brush = this.StageButtonTemplate.ReadOnlyBrush;
							bool flag = false;
							if (i == this.CurrentStageIndex)
							{
								buttonWidget.Brush = base.EventManager.Context.Brushes.First((Brush b) => b.Name == this.FullBrightButtonBrush);
							}
							else if (i <= this.OpenedStageIndex || (this.OpenedStageIndex == -1 && i < this.CurrentStageIndex))
							{
								buttonWidget.Brush = base.EventManager.Context.Brushes.First((Brush b) => b.Name == this.FullButtonBrush);
								flag = true;
							}
							else
							{
								buttonWidget.Brush = base.EventManager.Context.Brushes.First((Brush b) => b.Name == this.EmptyButtonBrush);
							}
							buttonWidget.DoNotAcceptEvents = !flag;
							buttonWidget.SuggestedHeight = this.StageButtonTemplate.SuggestedHeight;
							buttonWidget.SuggestedWidth = this.StageButtonTemplate.SuggestedWidth;
							buttonWidget.DoNotPassEventsToChildren = this.StageButtonTemplate.DoNotPassEventsToChildren;
							buttonWidget.ClickEventHandlers.Add(new Action<Widget>(this.OnStageSelection));
							this._stageButtonsList.Add(buttonWidget);
						}
					}
					this._buttonsInitialized = true;
				}
			}
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00035B78 File Offset: 0x00033D78
		private void OnStageSelection(Widget stageButton)
		{
			int num = this._stageButtonsList.IndexOf(stageButton as ButtonWidget);
			base.EventFired("OnStageSelection", new object[]
			{
				num
			});
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x00035BB1 File Offset: 0x00033DB1
		// (set) Token: 0x0600139B RID: 5019 RVA: 0x00035BB9 File Offset: 0x00033DB9
		[Editor(false)]
		public ButtonWidget StageButtonTemplate
		{
			get
			{
				return this._stageButtonTemplate;
			}
			set
			{
				if (this._stageButtonTemplate != value)
				{
					this._stageButtonTemplate = value;
					base.OnPropertyChanged<ButtonWidget>(value, "StageButtonTemplate");
					if (value != null)
					{
						base.RemoveChild(value);
					}
				}
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x00035BE1 File Offset: 0x00033DE1
		// (set) Token: 0x0600139D RID: 5021 RVA: 0x00035BE9 File Offset: 0x00033DE9
		[Editor(false)]
		public Widget BarFillWidget
		{
			get
			{
				return this._barFillWidget;
			}
			set
			{
				if (this._barFillWidget != value)
				{
					this._barFillWidget = value;
					base.OnPropertyChanged<Widget>(value, "BarFillWidget");
				}
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x00035C07 File Offset: 0x00033E07
		// (set) Token: 0x0600139F RID: 5023 RVA: 0x00035C0F File Offset: 0x00033E0F
		[Editor(false)]
		public Widget BarCanvasWidget
		{
			get
			{
				return this._barCanvasWidget;
			}
			set
			{
				if (this._barCanvasWidget != value)
				{
					this._barCanvasWidget = value;
					base.OnPropertyChanged<Widget>(value, "BarCanvasWidget");
				}
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x00035C2D File Offset: 0x00033E2D
		// (set) Token: 0x060013A1 RID: 5025 RVA: 0x00035C35 File Offset: 0x00033E35
		[Editor(false)]
		public int CurrentStageIndex
		{
			get
			{
				return this._currentStageIndex;
			}
			set
			{
				if (this._currentStageIndex != value)
				{
					this._currentStageIndex = value;
					base.OnPropertyChanged(value, "CurrentStageIndex");
					this._buttonsInitialized = false;
				}
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x00035C5A File Offset: 0x00033E5A
		// (set) Token: 0x060013A3 RID: 5027 RVA: 0x00035C62 File Offset: 0x00033E62
		[Editor(false)]
		public int TotalStagesCount
		{
			get
			{
				return this._totalStagesCount;
			}
			set
			{
				if (this._totalStagesCount != value)
				{
					this._totalStagesCount = value;
					base.OnPropertyChanged(value, "TotalStagesCount");
				}
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x00035C80 File Offset: 0x00033E80
		// (set) Token: 0x060013A5 RID: 5029 RVA: 0x00035C88 File Offset: 0x00033E88
		[Editor(false)]
		public int OpenedStageIndex
		{
			get
			{
				return this._openedStageIndex;
			}
			set
			{
				if (this._openedStageIndex != value)
				{
					this._openedStageIndex = value;
					base.OnPropertyChanged(value, "OpenedStageIndex");
				}
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x00035CA6 File Offset: 0x00033EA6
		// (set) Token: 0x060013A7 RID: 5031 RVA: 0x00035CAE File Offset: 0x00033EAE
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

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x00035CD1 File Offset: 0x00033ED1
		// (set) Token: 0x060013A9 RID: 5033 RVA: 0x00035CD9 File Offset: 0x00033ED9
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

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00035CFC File Offset: 0x00033EFC
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x00035D04 File Offset: 0x00033F04
		[Editor(false)]
		public string FullBrightButtonBrush
		{
			get
			{
				return this._fullBrightButtonBrush;
			}
			set
			{
				if (this._fullBrightButtonBrush != value)
				{
					this._fullBrightButtonBrush = value;
					base.OnPropertyChanged<string>(value, "FullBrightButtonBrush");
				}
			}
		}

		// Token: 0x040008EC RID: 2284
		private List<ButtonWidget> _stageButtonsList = new List<ButtonWidget>();

		// Token: 0x040008ED RID: 2285
		private bool _buttonsInitialized;

		// Token: 0x040008EE RID: 2286
		private ButtonWidget _stageButtonTemplate;

		// Token: 0x040008EF RID: 2287
		private int _currentStageIndex = -1;

		// Token: 0x040008F0 RID: 2288
		private int _totalStagesCount = -1;

		// Token: 0x040008F1 RID: 2289
		private int _openedStageIndex = -1;

		// Token: 0x040008F2 RID: 2290
		private string _fullButtonBrush;

		// Token: 0x040008F3 RID: 2291
		private string _emptyButtonBrush;

		// Token: 0x040008F4 RID: 2292
		private string _fullBrightButtonBrush;

		// Token: 0x040008F5 RID: 2293
		private Widget _barFillWidget;

		// Token: 0x040008F6 RID: 2294
		private Widget _barCanvasWidget;
	}
}

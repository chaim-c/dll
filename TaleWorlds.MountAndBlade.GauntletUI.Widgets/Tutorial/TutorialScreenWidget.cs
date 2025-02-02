using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Tutorial
{
	// Token: 0x02000049 RID: 73
	public class TutorialScreenWidget : Widget
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000C75C File Offset: 0x0000A95C
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x0000C764 File Offset: 0x0000A964
		public TutorialPanelImageWidget LeftItem { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000C76D File Offset: 0x0000A96D
		// (set) Token: 0x060003DA RID: 986 RVA: 0x0000C775 File Offset: 0x0000A975
		public TutorialPanelImageWidget RightItem { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000C77E File Offset: 0x0000A97E
		// (set) Token: 0x060003DC RID: 988 RVA: 0x0000C786 File Offset: 0x0000A986
		public TutorialPanelImageWidget BottomItem { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000C78F File Offset: 0x0000A98F
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0000C797 File Offset: 0x0000A997
		public TutorialPanelImageWidget TopItem { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
		public TutorialPanelImageWidget LeftTopItem { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000C7B1 File Offset: 0x0000A9B1
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000C7B9 File Offset: 0x0000A9B9
		public TutorialPanelImageWidget RightTopItem { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000C7C2 File Offset: 0x0000A9C2
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000C7CA File Offset: 0x0000A9CA
		public TutorialPanelImageWidget LeftBottomItem { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000C7D3 File Offset: 0x0000A9D3
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000C7DB File Offset: 0x0000A9DB
		public TutorialPanelImageWidget RightBottomItem { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000C7E4 File Offset: 0x0000A9E4
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000C7EC File Offset: 0x0000A9EC
		public TutorialPanelImageWidget CenterItem { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000C7F5 File Offset: 0x0000A9F5
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000C7FD File Offset: 0x0000A9FD
		public TutorialArrowWidget ArrowWidget { get; set; }

		// Token: 0x060003EB RID: 1003 RVA: 0x0000C806 File Offset: 0x0000AA06
		public TutorialScreenWidget(UIContext context) : base(context)
		{
			EventManager.UIEventManager.RegisterEvent<TutorialHighlightItemBrushWidget.HighlightElementToggledEvent>(new Action<TutorialHighlightItemBrushWidget.HighlightElementToggledEvent>(this.OnHighlightElementToggleEvent));
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000C828 File Offset: 0x0000AA28
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initalized)
			{
				this.LeftItem.boolPropertyChanged += this.OnTutorialItemPropertyChanged;
				this.RightItem.boolPropertyChanged += this.OnTutorialItemPropertyChanged;
				this.BottomItem.boolPropertyChanged += this.OnTutorialItemPropertyChanged;
				this.TopItem.boolPropertyChanged += this.OnTutorialItemPropertyChanged;
				this.LeftTopItem.boolPropertyChanged += this.OnTutorialItemPropertyChanged;
				this.RightTopItem.boolPropertyChanged += this.OnTutorialItemPropertyChanged;
				this.LeftBottomItem.boolPropertyChanged += this.OnTutorialItemPropertyChanged;
				this.RightBottomItem.boolPropertyChanged += this.OnTutorialItemPropertyChanged;
				this.CenterItem.boolPropertyChanged += this.OnTutorialItemPropertyChanged;
				this._initalized = true;
			}
			if (this._currentActiveHighligtFrame != null && this._currentActivePanelItem != null)
			{
				Tuple<Widget, Widget> leftAndRightElements = this.GetLeftAndRightElements();
				Tuple<Widget, Widget> topAndBottomElements = this.GetTopAndBottomElements();
				float num = leftAndRightElements.Item1.GlobalPosition.X + leftAndRightElements.Item1.Size.X;
				float x = leftAndRightElements.Item2.GlobalPosition.X;
				float y = topAndBottomElements.Item1.GlobalPosition.Y;
				float y2 = topAndBottomElements.Item2.GlobalPosition.Y;
				float width = MathF.Abs(num - x);
				float height = MathF.Abs(y - y2);
				this.ArrowWidget.ScaledPositionXOffset = num;
				this.ArrowWidget.ScaledPositionYOffset = y;
				this.ArrowWidget.SetArrowProperties(width, height, this.GetIsArrowDirectionIsDownwards(), this.GetIsArrowDirectionIsTowardsRight());
				this.ArrowWidget.IsVisible = true;
				return;
			}
			this.ArrowWidget.IsVisible = false;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		private bool GetIsArrowDirectionIsDownwards()
		{
			if (this._currentActiveHighligtFrame.GlobalPosition.Y < this._currentActivePanelItem.GlobalPosition.Y)
			{
				return this._currentActiveHighligtFrame.GlobalPosition.X < this._currentActivePanelItem.GlobalPosition.X;
			}
			return this._currentActivePanelItem.GlobalPosition.X < this._currentActiveHighligtFrame.GlobalPosition.X;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		private bool GetIsArrowDirectionIsTowardsRight()
		{
			return this._currentActiveHighligtFrame.GlobalPosition.X > this._currentActivePanelItem.GlobalPosition.X;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000CA90 File Offset: 0x0000AC90
		private Tuple<Widget, Widget> GetLeftAndRightElements()
		{
			if (this._currentActiveHighligtFrame.GlobalPosition.X < this._currentActivePanelItem.GlobalPosition.X)
			{
				return new Tuple<Widget, Widget>(this._currentActiveHighligtFrame, this._currentActivePanelItem);
			}
			return new Tuple<Widget, Widget>(this._currentActivePanelItem, this._currentActiveHighligtFrame);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000CAE4 File Offset: 0x0000ACE4
		private Tuple<Widget, Widget> GetTopAndBottomElements()
		{
			if (this._currentActiveHighligtFrame.GlobalPosition.Y < this._currentActivePanelItem.GlobalPosition.Y)
			{
				return new Tuple<Widget, Widget>(this._currentActiveHighligtFrame, this._currentActivePanelItem);
			}
			return new Tuple<Widget, Widget>(this._currentActivePanelItem, this._currentActiveHighligtFrame);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000CB36 File Offset: 0x0000AD36
		private void OnTutorialItemPropertyChanged(PropertyOwnerObject widget, string propertyName, bool propertyValue)
		{
			if (propertyName == "IsDisabled")
			{
				if (propertyValue)
				{
					this._currentActivePanelItem = null;
					this.ArrowWidget.ResetFade();
					return;
				}
				this._currentActivePanelItem = (widget as TutorialPanelImageWidget);
				this.ArrowWidget.DisableFade();
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000CB72 File Offset: 0x0000AD72
		private void OnHighlightElementToggleEvent(TutorialHighlightItemBrushWidget.HighlightElementToggledEvent obj)
		{
			if (obj.IsEnabled)
			{
				this._currentActiveHighligtFrame = obj.HighlightFrameWidget;
				this.ArrowWidget.ResetFade();
				return;
			}
			this.ArrowWidget.DisableFade();
			this._currentActiveHighligtFrame = null;
		}

		// Token: 0x040001AC RID: 428
		private bool _initalized;

		// Token: 0x040001AD RID: 429
		private TutorialHighlightItemBrushWidget _currentActiveHighligtFrame;

		// Token: 0x040001AE RID: 430
		private TutorialPanelImageWidget _currentActivePanelItem;
	}
}

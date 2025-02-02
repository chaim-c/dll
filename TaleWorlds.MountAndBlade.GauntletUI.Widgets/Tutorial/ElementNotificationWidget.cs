using System;
using System.Linq;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Tutorial
{
	// Token: 0x02000043 RID: 67
	public class ElementNotificationWidget : Widget
	{
		// Token: 0x0600038A RID: 906 RVA: 0x0000B368 File Offset: 0x00009568
		public ElementNotificationWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000B37C File Offset: 0x0000957C
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			string elementID = this.ElementID;
			if (elementID != null && elementID.Any<char>() && this.ElementToHighlight == null && !this._doesNotHaveElement)
			{
				this.ElementToHighlight = this.FindElementWithID(base.EventManager.Root, this.ElementID);
				this._doesNotHaveElement = true;
				if (this.ElementToHighlight != null)
				{
					this.TutorialFrameWidget.IsVisible = true;
					this.TutorialFrameWidget.IsHighlightEnabled = true;
					this.TutorialFrameWidget.ParentWidget = this.ElementToHighlight;
					if (this.ElementToHighlight.HeightSizePolicy == SizePolicy.CoverChildren || this.ElementToHighlight.WidthSizePolicy == SizePolicy.CoverChildren)
					{
						this.TutorialFrameWidget.WidthSizePolicy = SizePolicy.Fixed;
						this.TutorialFrameWidget.HeightSizePolicy = SizePolicy.Fixed;
						this._shouldSyncSize = true;
					}
					else
					{
						this.TutorialFrameWidget.WidthSizePolicy = SizePolicy.StretchToParent;
						this.TutorialFrameWidget.HeightSizePolicy = SizePolicy.StretchToParent;
						this._shouldSyncSize = false;
					}
				}
			}
			if (this._shouldSyncSize && this.ElementToHighlight != null && this.ElementToHighlight.Size.X > 1f && this.ElementToHighlight.Size.Y > 1f)
			{
				base.ScaledSuggestedWidth = this.ElementToHighlight.Size.X - 1f;
				base.ScaledSuggestedHeight = this.ElementToHighlight.Size.Y - 1f;
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000B4E8 File Offset: 0x000096E8
		private Widget FindElementWithID(Widget current, string ID)
		{
			if (current != null)
			{
				for (int i = 0; i < current.ChildCount; i++)
				{
					if (current.GetChild(i).Id == ID)
					{
						return current.GetChild(i);
					}
					Widget widget = this.FindElementWithID(current.GetChild(i), ID);
					if (widget != null)
					{
						return widget;
					}
				}
			}
			return null;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000B53A File Offset: 0x0000973A
		private void ResetHighlight()
		{
			if (this.TutorialFrameWidget != null)
			{
				this.TutorialFrameWidget.ParentWidget = this;
				this._doesNotHaveElement = false;
				this.TutorialFrameWidget.IsVisible = false;
				this.TutorialFrameWidget.IsHighlightEnabled = false;
				this.ElementToHighlight = null;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0000B576 File Offset: 0x00009776
		// (set) Token: 0x0600038F RID: 911 RVA: 0x0000B580 File Offset: 0x00009780
		[Editor(false)]
		public string ElementID
		{
			get
			{
				return this._elementID;
			}
			set
			{
				if (this._elementID != value)
				{
					if (this._elementID != string.Empty && value == string.Empty)
					{
						this.ResetHighlight();
					}
					this._elementID = value;
					base.OnPropertyChanged<string>(value, "ElementID");
				}
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000390 RID: 912 RVA: 0x0000B5D3 File Offset: 0x000097D3
		// (set) Token: 0x06000391 RID: 913 RVA: 0x0000B5DB File Offset: 0x000097DB
		[Editor(false)]
		public Widget ElementToHighlight
		{
			get
			{
				return this._elementToHighlight;
			}
			set
			{
				if (this._elementToHighlight != value)
				{
					this._elementToHighlight = value;
					base.OnPropertyChanged<Widget>(value, "ElementToHighlight");
				}
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000B5F9 File Offset: 0x000097F9
		// (set) Token: 0x06000393 RID: 915 RVA: 0x0000B601 File Offset: 0x00009801
		[Editor(false)]
		public TutorialHighlightItemBrushWidget TutorialFrameWidget
		{
			get
			{
				return this._tutorialFrameWidget;
			}
			set
			{
				if (this._tutorialFrameWidget != value)
				{
					this._tutorialFrameWidget = value;
					base.OnPropertyChanged<TutorialHighlightItemBrushWidget>(value, "TutorialFrameWidget");
					if (this._tutorialFrameWidget != null)
					{
						this._tutorialFrameWidget.IsVisible = false;
					}
				}
			}
		}

		// Token: 0x04000176 RID: 374
		private bool _doesNotHaveElement;

		// Token: 0x04000177 RID: 375
		private bool _shouldSyncSize;

		// Token: 0x04000178 RID: 376
		private string _elementID = string.Empty;

		// Token: 0x04000179 RID: 377
		private Widget _elementToHighlight;

		// Token: 0x0400017A RID: 378
		private TutorialHighlightItemBrushWidget _tutorialFrameWidget;
	}
}

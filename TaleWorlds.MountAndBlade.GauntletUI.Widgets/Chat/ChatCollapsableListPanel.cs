using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Chat
{
	// Token: 0x0200016E RID: 366
	public class ChatCollapsableListPanel : ListPanel
	{
		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x000339CC File Offset: 0x00031BCC
		// (set) Token: 0x060012E5 RID: 4837 RVA: 0x000339D4 File Offset: 0x00031BD4
		public bool IsLinesVisible { get; private set; }

		// Token: 0x060012E6 RID: 4838 RVA: 0x000339DD File Offset: 0x00031BDD
		public ChatCollapsableListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x000339E8 File Offset: 0x00031BE8
		private void ToggleLines(bool isVisible)
		{
			for (int i = 0; i < base.ChildCount; i++)
			{
				base.GetChild(i).IsVisible = (i == 0 || isVisible);
			}
			this.IsLinesVisible = isVisible;
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00033A1F File Offset: 0x00031C1F
		protected override void OnMousePressed()
		{
			base.OnMousePressed();
			this.ToggleLines(!this.IsLinesVisible);
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00033A36 File Offset: 0x00031C36
		protected override bool OnPreviewMousePressed()
		{
			return base.OnPreviewMousePressed();
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00033A3E File Offset: 0x00031C3E
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			this.ToggleLines(true);
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00033A4E File Offset: 0x00031C4E
		private void RefreshAlphaValues(float newAlpha)
		{
			this.SetGlobalAlphaRecursively(newAlpha);
			if (newAlpha > 0f)
			{
				ChatLogWidget parentChatLogWidget = this.ParentChatLogWidget;
				if (parentChatLogWidget == null)
				{
					return;
				}
				parentChatLogWidget.RegisterMultiLineElement(this);
				return;
			}
			else
			{
				ChatLogWidget parentChatLogWidget2 = this.ParentChatLogWidget;
				if (parentChatLogWidget2 == null)
				{
					return;
				}
				parentChatLogWidget2.RemoveMultiLineElement(this);
				return;
			}
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00033A84 File Offset: 0x00031C84
		private void UpdateColorValuesOfChildren(Widget widget, Color newColor)
		{
			foreach (Widget widget2 in widget.Children)
			{
				BrushWidget brushWidget;
				if ((brushWidget = (widget2 as BrushWidget)) != null)
				{
					brushWidget.Brush.FontColor = newColor;
				}
				else
				{
					widget2.Color = newColor;
				}
				this.UpdateColorValuesOfChildren(widget2, newColor);
			}
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00033AF8 File Offset: 0x00031CF8
		private void RefreshColorValues(Color newColor)
		{
			this.UpdateColorValuesOfChildren(this, newColor);
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x00033B02 File Offset: 0x00031D02
		// (set) Token: 0x060012EF RID: 4847 RVA: 0x00033B0A File Offset: 0x00031D0A
		[DataSourceProperty]
		public float Alpha
		{
			get
			{
				return this._alpha;
			}
			set
			{
				if (value != this._alpha)
				{
					this._alpha = value;
					base.OnPropertyChanged(value, "Alpha");
					this.RefreshAlphaValues(value);
				}
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x00033B2F File Offset: 0x00031D2F
		// (set) Token: 0x060012F1 RID: 4849 RVA: 0x00033B37 File Offset: 0x00031D37
		[DataSourceProperty]
		public Color LineColor
		{
			get
			{
				return this._lineColor;
			}
			set
			{
				if (value != this._lineColor)
				{
					this._lineColor = value;
					base.OnPropertyChanged(value, "LineColor");
					this.RefreshColorValues(value);
				}
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x00033B61 File Offset: 0x00031D61
		// (set) Token: 0x060012F3 RID: 4851 RVA: 0x00033B69 File Offset: 0x00031D69
		[DataSourceProperty]
		public ChatLogWidget ParentChatLogWidget
		{
			get
			{
				return this._parentChatLogWidget;
			}
			set
			{
				if (value != this._parentChatLogWidget)
				{
					this._parentChatLogWidget = value;
					base.OnPropertyChanged<ChatLogWidget>(value, "ParentChatLogWidget");
				}
			}
		}

		// Token: 0x04000896 RID: 2198
		private float _alpha;

		// Token: 0x04000897 RID: 2199
		private Color _lineColor;

		// Token: 0x04000898 RID: 2200
		private ChatLogWidget _parentChatLogWidget;
	}
}

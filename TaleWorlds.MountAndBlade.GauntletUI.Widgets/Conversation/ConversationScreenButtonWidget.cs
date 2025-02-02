using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Conversation
{
	// Token: 0x02000165 RID: 357
	public class ConversationScreenButtonWidget : ButtonWidget
	{
		// Token: 0x0600129D RID: 4765 RVA: 0x00032DB6 File Offset: 0x00030FB6
		public ConversationScreenButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00032DCC File Offset: 0x00030FCC
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this.AnswerList != null && this.ContinueButton != null)
			{
				this.ContinueButton.IsVisible = (this.AnswerList.ChildCount == 0);
				this.ContinueButton.IsEnabled = (this.AnswerList.ChildCount == 0);
			}
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00032E24 File Offset: 0x00031024
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			foreach (ConversationOptionListPanel conversationOptionListPanel in this._newlyAddedItems)
			{
				conversationOptionListPanel.OptionButtonWidget.ClickEventHandlers.Add(new Action<Widget>(this.OnOptionSelection));
			}
			this._newlyAddedItems.Clear();
			ListPanel answerList = this.AnswerList;
			if (answerList != null && answerList.ChildCount > 0 && this.AnswerList.GetChild(this.AnswerList.ChildCount - 1) != null)
			{
				this.AnswerList.GetChild(this.AnswerList.ChildCount - 1).MarginBottom = 5f;
			}
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00032EF0 File Offset: 0x000310F0
		private void OnOptionSelection(Widget obj)
		{
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00032EF4 File Offset: 0x000310F4
		private void OnOptionRemoved(Widget obj, Widget child)
		{
			ConversationOptionListPanel conversationOptionListPanel;
			if ((conversationOptionListPanel = (obj as ConversationOptionListPanel)) != null)
			{
				conversationOptionListPanel.OptionButtonWidget.ClickEventHandlers.Remove(new Action<Widget>(this.OnOptionSelection));
			}
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00032F28 File Offset: 0x00031128
		private void OnNewOptionAdded(Widget parent, Widget child)
		{
			this._newlyAddedItems.Add(child as ConversationOptionListPanel);
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x00032F3B File Offset: 0x0003113B
		// (set) Token: 0x060012A4 RID: 4772 RVA: 0x00032F44 File Offset: 0x00031144
		[Editor(false)]
		public ListPanel AnswerList
		{
			get
			{
				return this._answerList;
			}
			set
			{
				if (value != this._answerList)
				{
					if (value != null)
					{
						value.ItemAddEventHandlers.Add(new Action<Widget, Widget>(this.OnNewOptionAdded));
						value.ItemRemoveEventHandlers.Add(new Action<Widget, Widget>(this.OnOptionRemoved));
					}
					if (this._answerList != null)
					{
						value.ItemAddEventHandlers.Remove(new Action<Widget, Widget>(this.OnNewOptionAdded));
						value.ItemRemoveEventHandlers.Remove(new Action<Widget, Widget>(this.OnOptionRemoved));
					}
					this._answerList = value;
					base.OnPropertyChanged<ListPanel>(value, "AnswerList");
				}
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x060012A5 RID: 4773 RVA: 0x00032FD6 File Offset: 0x000311D6
		// (set) Token: 0x060012A6 RID: 4774 RVA: 0x00032FDE File Offset: 0x000311DE
		[Editor(false)]
		public ButtonWidget ContinueButton
		{
			get
			{
				return this._continueButton;
			}
			set
			{
				if (value != this._continueButton)
				{
					this._continueButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "ContinueButton");
				}
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x00032FFC File Offset: 0x000311FC
		// (set) Token: 0x060012A8 RID: 4776 RVA: 0x00033004 File Offset: 0x00031204
		[Editor(false)]
		public bool IsPersuasionActive
		{
			get
			{
				return this._isPersuasionActive;
			}
			set
			{
				if (value != this._isPersuasionActive)
				{
					this._isPersuasionActive = value;
					base.OnPropertyChanged(value, "IsPersuasionActive");
				}
			}
		}

		// Token: 0x0400087A RID: 2170
		private List<ConversationOptionListPanel> _newlyAddedItems = new List<ConversationOptionListPanel>();

		// Token: 0x0400087B RID: 2171
		private ListPanel _answerList;

		// Token: 0x0400087C RID: 2172
		private ButtonWidget _continueButton;

		// Token: 0x0400087D RID: 2173
		private bool _isPersuasionActive;
	}
}

using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x0200007A RID: 122
	public class DialogFlow
	{
		// Token: 0x06000F3D RID: 3901 RVA: 0x00048311 File Offset: 0x00046511
		private DialogFlow(string startingToken, int priority = 100)
		{
			this._currentToken = startingToken;
			this.Priority = priority;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x00048334 File Offset: 0x00046534
		private DialogFlow Line(TextObject text, bool byPlayer, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, bool isRepeatable = false)
		{
			string text2 = Campaign.Current.ConversationManager.CreateToken();
			this.AddLine(text, this._currentToken, text2, byPlayer, speakerDelegate, listenerDelegate, isRepeatable, false);
			this._currentToken = text2;
			return this;
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0004836F File Offset: 0x0004656F
		public DialogFlow Variation(string text, params object[] propertiesAndWeights)
		{
			return this.Variation(new TextObject(text, null), propertiesAndWeights);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x00048380 File Offset: 0x00046580
		public DialogFlow Variation(TextObject text, params object[] propertiesAndWeights)
		{
			for (int i = 0; i < propertiesAndWeights.Length; i += 2)
			{
				string tagName = (string)propertiesAndWeights[i];
				int weight = Convert.ToInt32(propertiesAndWeights[i + 1]);
				List<GameTextManager.ChoiceTag> list = new List<GameTextManager.ChoiceTag>();
				list.Add(new GameTextManager.ChoiceTag(tagName, weight));
				this.Lines[this.Lines.Count - 1].AddVariation(text, list);
			}
			return this;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x000483E2 File Offset: 0x000465E2
		public DialogFlow NpcLine(string npcText, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null)
		{
			return this.NpcLine(new TextObject(npcText, null), speakerDelegate, listenerDelegate);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x000483F3 File Offset: 0x000465F3
		public DialogFlow NpcLine(TextObject npcText, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null)
		{
			return this.Line(npcText, false, speakerDelegate, listenerDelegate, false);
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00048400 File Offset: 0x00046600
		public DialogFlow NpcLineWithVariation(string npcText, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null)
		{
			DialogFlow result = this.Line(TextObject.Empty, false, speakerDelegate, listenerDelegate, false);
			List<GameTextManager.ChoiceTag> list = new List<GameTextManager.ChoiceTag>();
			list.Add(new GameTextManager.ChoiceTag("DefaultTag", 1));
			this.Lines[this.Lines.Count - 1].AddVariation(new TextObject(npcText, null), list);
			return result;
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00048458 File Offset: 0x00046658
		public DialogFlow NpcLineWithVariation(TextObject npcText, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null)
		{
			DialogFlow result = this.Line(TextObject.Empty, false, speakerDelegate, listenerDelegate, false);
			List<GameTextManager.ChoiceTag> list = new List<GameTextManager.ChoiceTag>();
			list.Add(new GameTextManager.ChoiceTag("DefaultTag", 1));
			this.Lines[this.Lines.Count - 1].AddVariation(npcText, list);
			return result;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x000484AA File Offset: 0x000466AA
		public DialogFlow PlayerLine(string playerText, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null)
		{
			return this.Line(new TextObject(playerText, null), true, null, listenerDelegate, false);
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x000484BD File Offset: 0x000466BD
		public DialogFlow PlayerLine(TextObject playerText, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null)
		{
			return this.Line(playerText, true, null, listenerDelegate, false);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x000484CA File Offset: 0x000466CA
		private DialogFlow BeginOptions(bool byPlayer)
		{
			this._curDialogFlowContext = new DialogFlowContext(this._currentToken, byPlayer, this._curDialogFlowContext);
			return this;
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x000484E5 File Offset: 0x000466E5
		public DialogFlow BeginPlayerOptions()
		{
			return this.BeginOptions(true);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x000484EE File Offset: 0x000466EE
		public DialogFlow BeginNpcOptions()
		{
			return this.BeginOptions(false);
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x000484F8 File Offset: 0x000466F8
		private DialogFlow Option(TextObject text, bool byPlayer, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, bool isRepeatable = false, bool isSpecialOption = false)
		{
			string text2 = Campaign.Current.ConversationManager.CreateToken();
			this.AddLine(text, this._curDialogFlowContext.Token, text2, byPlayer, speakerDelegate, listenerDelegate, isRepeatable, isSpecialOption);
			this._currentToken = text2;
			return this;
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00048539 File Offset: 0x00046739
		public DialogFlow PlayerOption(string text, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null)
		{
			return this.PlayerOption(new TextObject(text, null), listenerDelegate);
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00048549 File Offset: 0x00046749
		public DialogFlow PlayerOption(TextObject text, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null)
		{
			this.Option(text, true, null, listenerDelegate, false, false);
			return this;
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x00048559 File Offset: 0x00046759
		public DialogFlow PlayerSpecialOption(TextObject text, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null)
		{
			this.Option(text, true, null, listenerDelegate, false, true);
			return this;
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x00048569 File Offset: 0x00046769
		public DialogFlow PlayerRepeatableOption(TextObject text, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null)
		{
			this.Option(text, true, null, listenerDelegate, true, false);
			return this;
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00048579 File Offset: 0x00046779
		public DialogFlow NpcOption(string text, ConversationSentence.OnConditionDelegate conditionDelegate, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null)
		{
			this.Option(new TextObject(text, null), false, speakerDelegate, listenerDelegate, false, false);
			this._lastLine.ConditionDelegate = conditionDelegate;
			return this;
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0004859C File Offset: 0x0004679C
		public DialogFlow NpcOption(TextObject text, ConversationSentence.OnConditionDelegate conditionDelegate, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null)
		{
			this.Option(text, false, speakerDelegate, listenerDelegate, false, false);
			this._lastLine.ConditionDelegate = conditionDelegate;
			return this;
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x000485B9 File Offset: 0x000467B9
		public DialogFlow NpcOptionWithVariation(string text, ConversationSentence.OnConditionDelegate conditionDelegate, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null)
		{
			this.NpcOptionWithVariation(new TextObject(text, null), conditionDelegate, speakerDelegate, listenerDelegate);
			return this;
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x000485D0 File Offset: 0x000467D0
		public DialogFlow NpcOptionWithVariation(TextObject text, ConversationSentence.OnConditionDelegate conditionDelegate, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null)
		{
			this.Option(TextObject.Empty, false, speakerDelegate, listenerDelegate, false, false);
			List<GameTextManager.ChoiceTag> list = new List<GameTextManager.ChoiceTag>();
			list.Add(new GameTextManager.ChoiceTag("DefaultTag", 1));
			this._lastLine.AddVariation(text, list);
			this._lastLine.ConditionDelegate = conditionDelegate;
			return this;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00048620 File Offset: 0x00046820
		private DialogFlow EndOptions(bool byPlayer)
		{
			this._curDialogFlowContext = this._curDialogFlowContext.Parent;
			return this;
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00048634 File Offset: 0x00046834
		public DialogFlow EndPlayerOptions()
		{
			return this.EndOptions(true);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0004863D File Offset: 0x0004683D
		public DialogFlow EndNpcOptions()
		{
			return this.EndOptions(false);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00048646 File Offset: 0x00046846
		public DialogFlow Condition(ConversationSentence.OnConditionDelegate conditionDelegate)
		{
			this._lastLine.ConditionDelegate = conditionDelegate;
			return this;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00048655 File Offset: 0x00046855
		public DialogFlow ClickableCondition(ConversationSentence.OnClickableConditionDelegate clickableConditionDelegate)
		{
			this._lastLine.ClickableConditionDelegate = clickableConditionDelegate;
			return this;
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00048664 File Offset: 0x00046864
		public DialogFlow Consequence(ConversationSentence.OnConsequenceDelegate consequenceDelegate)
		{
			this._lastLine.ConsequenceDelegate = consequenceDelegate;
			return this;
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00048673 File Offset: 0x00046873
		public static DialogFlow CreateDialogFlow(string inputToken = null, int priority = 100)
		{
			return new DialogFlow(inputToken ?? Campaign.Current.ConversationManager.CreateToken(), priority);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00048690 File Offset: 0x00046890
		private DialogFlowLine AddLine(TextObject text, string inputToken, string outputToken, bool byPlayer, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate, bool isRepeatable, bool isSpecialOption = false)
		{
			DialogFlowLine dialogFlowLine = new DialogFlowLine();
			dialogFlowLine.Text = text;
			dialogFlowLine.InputToken = inputToken;
			dialogFlowLine.OutputToken = outputToken;
			dialogFlowLine.ByPlayer = byPlayer;
			dialogFlowLine.SpeakerDelegate = speakerDelegate;
			dialogFlowLine.ListenerDelegate = listenerDelegate;
			dialogFlowLine.IsRepeatable = isRepeatable;
			dialogFlowLine.IsSpecialOption = isSpecialOption;
			this.Lines.Add(dialogFlowLine);
			this._lastLine = dialogFlowLine;
			return dialogFlowLine;
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x000486F4 File Offset: 0x000468F4
		public DialogFlow NpcDefaultOption(string text)
		{
			return this.NpcOption(text, null, null, null);
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00048700 File Offset: 0x00046900
		public DialogFlow GotoDialogState(string input)
		{
			this._lastLine.OutputToken = input;
			this._currentToken = input;
			return this;
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00048716 File Offset: 0x00046916
		public DialogFlow GetOutputToken(out string oState)
		{
			oState = this._lastLine.OutputToken;
			return this;
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00048726 File Offset: 0x00046926
		public DialogFlow GoBackToDialogState(string iState)
		{
			this._currentToken = iState;
			return this;
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x00048730 File Offset: 0x00046930
		public DialogFlow CloseDialog()
		{
			this.GotoDialogState("close_window");
			return this;
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0004873F File Offset: 0x0004693F
		private ConversationSentence AddDialogLine(ConversationSentence dialogLine)
		{
			Campaign.Current.ConversationManager.AddDialogLine(dialogLine);
			return dialogLine;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x00048754 File Offset: 0x00046954
		public ConversationSentence AddPlayerLine(string id, string inputToken, string outputToken, string text, ConversationSentence.OnConditionDelegate conditionDelegate, ConversationSentence.OnConsequenceDelegate consequenceDelegate, object relatedObject, int priority = 100, ConversationSentence.OnClickableConditionDelegate clickableConditionDelegate = null, ConversationSentence.OnPersuasionOptionDelegate persuasionOptionDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null)
		{
			return this.AddDialogLine(new ConversationSentence(id, new TextObject(text, null), inputToken, outputToken, conditionDelegate, clickableConditionDelegate, consequenceDelegate, 1U, priority, 0, 0, relatedObject, false, speakerDelegate, listenerDelegate, persuasionOptionDelegate));
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x00048790 File Offset: 0x00046990
		public ConversationSentence AddDialogLine(string id, string inputToken, string outputToken, string text, ConversationSentence.OnConditionDelegate conditionDelegate, ConversationSentence.OnConsequenceDelegate consequenceDelegate, object relatedObject, int priority = 100, ConversationSentence.OnClickableConditionDelegate clickableConditionDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null)
		{
			return this.AddDialogLine(new ConversationSentence(id, new TextObject(text, null), inputToken, outputToken, conditionDelegate, clickableConditionDelegate, consequenceDelegate, 0U, priority, 0, 0, relatedObject, false, speakerDelegate, listenerDelegate, null));
		}

		// Token: 0x0400051E RID: 1310
		internal readonly List<DialogFlowLine> Lines = new List<DialogFlowLine>();

		// Token: 0x0400051F RID: 1311
		internal readonly int Priority;

		// Token: 0x04000520 RID: 1312
		private string _currentToken;

		// Token: 0x04000521 RID: 1313
		private DialogFlowLine _lastLine;

		// Token: 0x04000522 RID: 1314
		private DialogFlowContext _curDialogFlowContext;
	}
}

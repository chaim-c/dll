﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Helpers;
using TaleWorlds.CampaignSystem.Conversation.Persuasion;
using TaleWorlds.CampaignSystem.Conversation.Tags;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Conversation
{
	// Token: 0x020001EA RID: 490
	public class ConversationManager
	{
		// Token: 0x06001D8E RID: 7566 RVA: 0x0008610A File Offset: 0x0008430A
		public int CreateConversationSentenceIndex()
		{
			int numConversationSentencesCreated = this._numConversationSentencesCreated;
			this._numConversationSentencesCreated++;
			return numConversationSentencesCreated;
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001D8F RID: 7567 RVA: 0x00086120 File Offset: 0x00084320
		public string CurrentSentenceText
		{
			get
			{
				TextObject textObject = this._currentSentenceText;
				if (this.OneToOneConversationCharacter != null)
				{
					textObject = this.FindMatchingTextOrNull(textObject.GetID(), this.OneToOneConversationCharacter);
					if (textObject == null)
					{
						textObject = this._currentSentenceText;
					}
				}
				return MBTextManager.DiscardAnimationTagsAndCheckAnimationTagPositions(textObject.CopyTextObject().ToString());
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x00086169 File Offset: 0x00084369
		private int DialogRepeatCount
		{
			get
			{
				if (this._dialogRepeatObjects.Count > 0)
				{
					return this._dialogRepeatObjects[this._currentRepeatedDialogSetIndex].Count;
				}
				return 1;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x00086191 File Offset: 0x00084391
		public bool IsConversationFlowActive
		{
			get
			{
				return this._isActive;
			}
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x0008619C File Offset: 0x0008439C
		public ConversationManager()
		{
			this._sentences = new List<ConversationSentence>();
			this.stateMap = new Dictionary<string, int>();
			this.stateMap.Add("start", 0);
			this.stateMap.Add("event_triggered", 1);
			this.stateMap.Add("member_chat", 2);
			this.stateMap.Add("prisoner_chat", 3);
			this.stateMap.Add("close_window", 4);
			this._numberOfStateIndices = 5;
			this._isActive = false;
			this._executeDoOptionContinue = false;
			this.InitializeTags();
			this.ConversationAnimationManager = new ConversationAnimationManager();
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x0008626C File Offset: 0x0008446C
		// (set) Token: 0x06001D94 RID: 7572 RVA: 0x00086274 File Offset: 0x00084474
		public List<ConversationSentenceOption> CurOptions { get; protected set; }

		// Token: 0x06001D95 RID: 7573 RVA: 0x00086280 File Offset: 0x00084480
		public void StartNew(int startingToken, bool setActionsInstantly)
		{
			this.ActiveToken = startingToken;
			this._currentSentence = -1;
			this.ResetRepeatedDialogSystem();
			this._lastSelectedDialogObject = null;
			Debug.Print("--------------- Conversation Start --------------- ", 5, Debug.DebugColor.White, 4503599627370496UL);
			Debug.Print(string.Concat(new object[]
			{
				"Conversation character name: ",
				this.OneToOneConversationCharacter.Name,
				"\nid: ",
				this.OneToOneConversationCharacter.StringId,
				"\nculture:",
				this.OneToOneConversationCharacter.Culture,
				"\npersona:",
				this.OneToOneConversationCharacter.GetPersona().Name
			}), 5, Debug.DebugColor.White, 17592186044416UL);
			if (CampaignMission.Current != null)
			{
				foreach (IAgent agent in this.ConversationAgents)
				{
					CampaignMission.Current.OnConversationStart(agent, setActionsInstantly);
				}
			}
			this.ProcessPartnerSentence();
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x0008638C File Offset: 0x0008458C
		private bool ProcessPartnerSentence()
		{
			List<ConversationSentenceOption> sentenceOptions = this.GetSentenceOptions(false, false);
			bool result = false;
			if (sentenceOptions.Count > 0)
			{
				this.ProcessSentence(sentenceOptions[0]);
				result = true;
			}
			IConversationStateHandler handler = this.Handler;
			if (handler != null)
			{
				handler.OnConversationContinue();
			}
			return result;
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x000863D0 File Offset: 0x000845D0
		public void ProcessSentence(ConversationSentenceOption conversationSentenceOption)
		{
			ConversationSentence conversationSentence = this._sentences[conversationSentenceOption.SentenceNo];
			Debug.Print(conversationSentenceOption.DebugInfo, 0, Debug.DebugColor.White, 4503599627370496UL);
			this.ActiveToken = conversationSentence.OutputToken;
			this.UpdateSpeakerAndListenerAgents(conversationSentence);
			if (CampaignMission.Current != null)
			{
				CampaignMission.Current.OnProcessSentence();
			}
			this._lastSelectedDialogObject = conversationSentenceOption.RepeatObject;
			this._currentSentence = conversationSentenceOption.SentenceNo;
			if (Game.Current == null)
			{
				throw new MBNullParameterException("Game");
			}
			this.UpdateCurrentSentenceText();
			int count = this._sentences.Count;
			conversationSentence.RunConsequence(Game.Current);
			if (CampaignMission.Current != null)
			{
				string[] conversationAnimations = MBTextManager.GetConversationAnimations(this._currentSentenceText);
				string soundPath = "";
				VoiceObject voiceObject;
				if (MBTextManager.TryGetVoiceObject(this._currentSentenceText, out voiceObject))
				{
					soundPath = Campaign.Current.Models.VoiceOverModel.GetSoundPathForCharacter((CharacterObject)this.SpeakerAgent.Character, voiceObject);
				}
				CampaignMission.Current.OnConversationPlay(conversationAnimations[0], conversationAnimations[1], conversationAnimations[2], conversationAnimations[3], soundPath);
			}
			if (0 > this._currentSentence || this._currentSentence >= count)
			{
				Debug.FailedAssert("CurrentSentence is not valid.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Conversation\\ConversationManager.cs", "ProcessSentence", 389);
			}
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x00086504 File Offset: 0x00084704
		private void UpdateSpeakerAndListenerAgents(ConversationSentence sentence)
		{
			if (sentence.IsSpeaker != null)
			{
				if (sentence.IsSpeaker(this._mainAgent))
				{
					this.SetSpeakerAgent(this._mainAgent);
					goto IL_8B;
				}
				using (IEnumerator<IAgent> enumerator = this.ConversationAgents.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IAgent agent = enumerator.Current;
						if (sentence.IsSpeaker(agent))
						{
							this.SetSpeakerAgent(agent);
							break;
						}
					}
					goto IL_8B;
				}
			}
			this.SetSpeakerAgent((!sentence.IsPlayer) ? this.ConversationAgents[0] : this._mainAgent);
			IL_8B:
			if (sentence.IsListener != null)
			{
				if (sentence.IsListener(this._mainAgent))
				{
					this.SetListenerAgent(this._mainAgent);
					return;
				}
				using (IEnumerator<IAgent> enumerator = this.ConversationAgents.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IAgent agent2 = enumerator.Current;
						if (sentence.IsListener(agent2))
						{
							this.SetListenerAgent(agent2);
							break;
						}
					}
					return;
				}
			}
			this.SetListenerAgent((!sentence.IsPlayer) ? this._mainAgent : this.ConversationAgents[0]);
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x00086644 File Offset: 0x00084844
		private void SetSpeakerAgent(IAgent agent)
		{
			if (this._speakerAgent != agent)
			{
				this._speakerAgent = agent;
				if (this._speakerAgent != null && this._speakerAgent.Character is CharacterObject)
				{
					StringHelpers.SetCharacterProperties("SPEAKER", agent.Character as CharacterObject, null, false);
				}
			}
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x00086694 File Offset: 0x00084894
		private void SetListenerAgent(IAgent agent)
		{
			if (this._listenerAgent != agent)
			{
				this._listenerAgent = agent;
				if (this._listenerAgent != null && this._listenerAgent.Character is CharacterObject)
				{
					StringHelpers.SetCharacterProperties("LISTENER", agent.Character as CharacterObject, null, false);
				}
			}
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x000866E4 File Offset: 0x000848E4
		public void UpdateCurrentSentenceText()
		{
			TextObject currentSentenceText;
			if (this._currentSentence >= 0)
			{
				currentSentenceText = this._sentences[this._currentSentence].Text;
			}
			else
			{
				if (Campaign.Current == null)
				{
					throw new MBNullParameterException("Campaign");
				}
				currentSentenceText = GameTexts.FindText("str_error_string", null);
			}
			this._currentSentenceText = currentSentenceText;
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x00086738 File Offset: 0x00084938
		public bool IsConversationEnded()
		{
			return this.ActiveToken == 4;
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x00086743 File Offset: 0x00084943
		public void ClearCurrentOptions()
		{
			if (this.CurOptions == null)
			{
				this.CurOptions = new List<ConversationSentenceOption>();
			}
			this.CurOptions.Clear();
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x00086764 File Offset: 0x00084964
		public void AddToCurrentOptions(TextObject text, string id, bool isClickable, TextObject hintText)
		{
			ConversationSentenceOption item = new ConversationSentenceOption
			{
				SentenceNo = 0,
				Text = text,
				Id = id,
				RepeatObject = null,
				DebugInfo = null,
				IsClickable = isClickable,
				HintText = hintText
			};
			this.CurOptions.Add(item);
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x000867C0 File Offset: 0x000849C0
		public void GetPlayerSentenceOptions()
		{
			this.CurOptions = this.GetSentenceOptions(true, true);
			if (this.CurOptions.Count > 0)
			{
				ConversationSentenceOption conversationSentenceOption = this.CurOptions[0];
				foreach (ConversationSentenceOption conversationSentenceOption2 in this.CurOptions)
				{
					if (this._sentences[conversationSentenceOption2.SentenceNo].IsListener != null)
					{
						conversationSentenceOption = conversationSentenceOption2;
						break;
					}
				}
				this.UpdateSpeakerAndListenerAgents(this._sentences[conversationSentenceOption.SentenceNo]);
			}
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x00086868 File Offset: 0x00084A68
		public int GetStateIndex(string str)
		{
			int result;
			if (this.stateMap.ContainsKey(str))
			{
				result = this.stateMap[str];
			}
			else
			{
				result = this._numberOfStateIndices;
				Dictionary<string, int> dictionary = this.stateMap;
				int numberOfStateIndices = this._numberOfStateIndices;
				this._numberOfStateIndices = numberOfStateIndices + 1;
				dictionary.Add(str, numberOfStateIndices);
			}
			return result;
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x000868B7 File Offset: 0x00084AB7
		internal void Build()
		{
			this.SortSentences();
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x000868BF File Offset: 0x00084ABF
		public void DisableSentenceSort()
		{
			this._sortSentenceIsDisabled = true;
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x000868C8 File Offset: 0x00084AC8
		public void EnableSentenceSort()
		{
			this._sortSentenceIsDisabled = false;
			this.SortSentences();
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x000868D7 File Offset: 0x00084AD7
		private void SortSentences()
		{
			this._sentences = (from pair in this._sentences
			orderby pair.Priority descending
			select pair).ToList<ConversationSentence>();
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x00086910 File Offset: 0x00084B10
		private void SortLastSentence()
		{
			int num = this._sentences.Count - 1;
			ConversationSentence conversationSentence = this._sentences[num];
			int priority = conversationSentence.Priority;
			int num2 = num - 1;
			while (num2 >= 0 && this._sentences[num2].Priority < priority)
			{
				this._sentences[num2 + 1] = this._sentences[num2];
				num = num2;
				num2--;
			}
			this._sentences[num] = conversationSentence;
			if (this.CurOptions != null)
			{
				for (int i = 0; i < this.CurOptions.Count; i++)
				{
					if (this.CurOptions[i].SentenceNo >= num)
					{
						ConversationSentenceOption value = this.CurOptions[i];
						value.SentenceNo = this.CurOptions[i].SentenceNo + 1;
						this.CurOptions[i] = value;
					}
				}
			}
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x000869FC File Offset: 0x00084BFC
		private List<ConversationSentenceOption> GetSentenceOptions(bool onlyPlayer, bool processAfterOneOption)
		{
			List<ConversationSentenceOption> list = new List<ConversationSentenceOption>();
			ConversationManager.SetupTextVariables();
			for (int i = 0; i < this._sentences.Count; i++)
			{
				if (this.GetSentenceMatch(i, onlyPlayer))
				{
					ConversationSentence conversationSentence = this._sentences[i];
					int num = 1;
					this._dialogRepeatLines.Clear();
					this._currentRepeatIndex = 0;
					if (conversationSentence.IsRepeatable)
					{
						num = this.DialogRepeatCount;
					}
					for (int j = 0; j < num; j++)
					{
						this._dialogRepeatLines.Add(conversationSentence.Text.CopyTextObject());
						if (conversationSentence.RunCondition())
						{
							conversationSentence.IsClickable = conversationSentence.RunClickableCondition();
							if (conversationSentence.IsWithVariation)
							{
								TextObject content = this.FindMatchingTextOrNull(conversationSentence.Id, this.OneToOneConversationCharacter);
								GameTexts.SetVariable("VARIATION_TEXT_TAGGED_LINE", content);
							}
							string debugInfo = (conversationSentence.IsPlayer ? "P  -> (" : "AI -> (") + conversationSentence.Id + ") - ";
							ConversationSentenceOption item = new ConversationSentenceOption
							{
								SentenceNo = i,
								Text = this.GetCurrentDialogLine(),
								Id = conversationSentence.Id,
								RepeatObject = this.GetCurrentProcessedRepeatObject(),
								DebugInfo = debugInfo,
								IsClickable = conversationSentence.IsClickable,
								HasPersuasion = conversationSentence.HasPersuasion,
								SkillName = conversationSentence.SkillName,
								TraitName = conversationSentence.TraitName,
								IsSpecial = conversationSentence.IsSpecial,
								HintText = conversationSentence.HintText,
								PersuationOptionArgs = conversationSentence.PersuationOptionArgs
							};
							list.Add(item);
							if (conversationSentence.IsRepeatable)
							{
								this._currentRepeatIndex++;
							}
							if (!processAfterOneOption)
							{
								return list;
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x00086BC8 File Offset: 0x00084DC8
		private bool GetSentenceMatch(int sentenceIndex, bool onlyPlayer)
		{
			if (0 > sentenceIndex || sentenceIndex >= this._sentences.Count)
			{
				throw new MBOutOfRangeException("Sentence index is not valid.");
			}
			bool flag = this._sentences[sentenceIndex].InputToken != this.ActiveToken;
			if (!flag && onlyPlayer)
			{
				flag = !this._sentences[sentenceIndex].IsPlayer;
			}
			return !flag;
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x00086C30 File Offset: 0x00084E30
		internal object GetCurrentProcessedRepeatObject()
		{
			if (this._dialogRepeatObjects.Count <= 0)
			{
				return null;
			}
			return this._dialogRepeatObjects[this._currentRepeatedDialogSetIndex][this._currentRepeatIndex];
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x00086C5E File Offset: 0x00084E5E
		internal TextObject GetCurrentDialogLine()
		{
			if (this._dialogRepeatLines.Count <= this._currentRepeatIndex)
			{
				return null;
			}
			return this._dialogRepeatLines[this._currentRepeatIndex];
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x00086C86 File Offset: 0x00084E86
		internal object GetSelectedRepeatObject()
		{
			return this._lastSelectedDialogObject;
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x00086C90 File Offset: 0x00084E90
		internal void SetDialogRepeatCount(IReadOnlyList<object> dialogRepeatObjects, int maxRepeatedDialogsInConversation)
		{
			this._dialogRepeatObjects.Clear();
			bool flag = dialogRepeatObjects.Count > maxRepeatedDialogsInConversation + 1;
			List<object> list = new List<object>(maxRepeatedDialogsInConversation);
			for (int i = 0; i < dialogRepeatObjects.Count; i++)
			{
				object item = dialogRepeatObjects[i];
				if (flag && i % maxRepeatedDialogsInConversation == 0)
				{
					list = new List<object>(maxRepeatedDialogsInConversation);
					this._dialogRepeatObjects.Add(list);
				}
				list.Add(item);
			}
			if (!flag && !list.IsEmpty<object>())
			{
				this._dialogRepeatObjects.Add(list);
			}
			this._currentRepeatedDialogSetIndex = 0;
			this._currentRepeatIndex = 0;
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x00086D1C File Offset: 0x00084F1C
		internal static void DialogRepeatContinueListing()
		{
			Campaign campaign = Campaign.Current;
			ConversationManager conversationManager = (campaign != null) ? campaign.ConversationManager : null;
			if (conversationManager != null)
			{
				conversationManager._currentRepeatedDialogSetIndex++;
				if (conversationManager._currentRepeatedDialogSetIndex >= conversationManager._dialogRepeatObjects.Count)
				{
					conversationManager._currentRepeatedDialogSetIndex = 0;
				}
				conversationManager._currentRepeatIndex = 0;
			}
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x00086D70 File Offset: 0x00084F70
		internal static bool IsThereMultipleRepeatablePages()
		{
			Campaign campaign = Campaign.Current;
			if (campaign == null)
			{
				return false;
			}
			ConversationManager conversationManager = campaign.ConversationManager;
			int? num = (conversationManager != null) ? new int?(conversationManager._dialogRepeatObjects.Count) : null;
			int num2 = 1;
			return num.GetValueOrDefault() > num2 & num != null;
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x00086DC0 File Offset: 0x00084FC0
		private void ResetRepeatedDialogSystem()
		{
			this._currentRepeatedDialogSetIndex = 0;
			this._currentRepeatIndex = 0;
			this._dialogRepeatObjects.Clear();
			this._dialogRepeatLines.Clear();
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x00086DE6 File Offset: 0x00084FE6
		internal ConversationSentence AddDialogLine(ConversationSentence dialogLine)
		{
			this._sentences.Add(dialogLine);
			if (!this._sortSentenceIsDisabled)
			{
				this.SortLastSentence();
			}
			return dialogLine;
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x00086E04 File Offset: 0x00085004
		public void AddDialogFlow(DialogFlow dialogFlow, object relatedObject = null)
		{
			foreach (DialogFlowLine dialogFlowLine in dialogFlow.Lines)
			{
				string text = this.CreateId();
				uint flags = (dialogFlowLine.ByPlayer ? 1U : 0U) | (dialogFlowLine.IsRepeatable ? 2U : 0U) | (dialogFlowLine.IsSpecialOption ? 4U : 0U);
				this.AddDialogLine(new ConversationSentence(text, dialogFlowLine.HasVariation ? new TextObject("{=7AyjDt96}{VARIATION_TEXT_TAGGED_LINE}", null) : dialogFlowLine.Text, dialogFlowLine.InputToken, dialogFlowLine.OutputToken, dialogFlowLine.ConditionDelegate, dialogFlowLine.ClickableConditionDelegate, dialogFlowLine.ConsequenceDelegate, flags, dialogFlow.Priority, 0, 0, relatedObject, dialogFlowLine.HasVariation, dialogFlowLine.SpeakerDelegate, dialogFlowLine.ListenerDelegate, null));
				GameText gameText = Game.Current.GameTextManager.AddGameText(text);
				foreach (KeyValuePair<TextObject, List<GameTextManager.ChoiceTag>> keyValuePair in dialogFlowLine.Variations)
				{
					gameText.AddVariationWithId("", keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x00086F6C File Offset: 0x0008516C
		public ConversationSentence AddDialogLineMultiAgent(string id, string inputToken, string outputToken, TextObject text, ConversationSentence.OnConditionDelegate conditionDelegate, ConversationSentence.OnConsequenceDelegate consequenceDelegate, int agentIndex, int nextAgentIndex, int priority = 100, ConversationSentence.OnClickableConditionDelegate clickableConditionDelegate = null)
		{
			return this.AddDialogLine(new ConversationSentence(id, text, inputToken, outputToken, conditionDelegate, clickableConditionDelegate, consequenceDelegate, 0U, priority, agentIndex, nextAgentIndex, null, false, null, null, null));
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x00086F9B File Offset: 0x0008519B
		internal string CreateToken()
		{
			string result = string.Format("atk:{0}", this._autoToken);
			this._autoToken++;
			return result;
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x00086FC0 File Offset: 0x000851C0
		private string CreateId()
		{
			string result = string.Format("adg:{0}", this._autoId);
			this._autoId++;
			return result;
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x00086FE5 File Offset: 0x000851E5
		internal void SetupGameStringsForConversation()
		{
			StringHelpers.SetCharacterProperties("PLAYER", Hero.MainHero.CharacterObject, null, false);
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x00086FFE File Offset: 0x000851FE
		internal void OnConsequence(ConversationSentence sentence)
		{
			Action<ConversationSentence> consequenceRunned = this.ConsequenceRunned;
			if (consequenceRunned == null)
			{
				return;
			}
			consequenceRunned(sentence);
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x00087011 File Offset: 0x00085211
		internal void OnCondition(ConversationSentence sentence)
		{
			Action<ConversationSentence> conditionRunned = this.ConditionRunned;
			if (conditionRunned == null)
			{
				return;
			}
			conditionRunned(sentence);
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x00087024 File Offset: 0x00085224
		internal void OnClickableCondition(ConversationSentence sentence)
		{
			Action<ConversationSentence> clickableConditionRunned = this.ClickableConditionRunned;
			if (clickableConditionRunned == null)
			{
				return;
			}
			clickableConditionRunned(sentence);
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06001DB8 RID: 7608 RVA: 0x00087038 File Offset: 0x00085238
		// (remove) Token: 0x06001DB9 RID: 7609 RVA: 0x00087070 File Offset: 0x00085270
		public event Action<ConversationSentence> ConsequenceRunned;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06001DBA RID: 7610 RVA: 0x000870A8 File Offset: 0x000852A8
		// (remove) Token: 0x06001DBB RID: 7611 RVA: 0x000870E0 File Offset: 0x000852E0
		public event Action<ConversationSentence> ConditionRunned;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06001DBC RID: 7612 RVA: 0x00087118 File Offset: 0x00085318
		// (remove) Token: 0x06001DBD RID: 7613 RVA: 0x00087150 File Offset: 0x00085350
		public event Action<ConversationSentence> ClickableConditionRunned;

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x00087185 File Offset: 0x00085385
		public IReadOnlyList<IAgent> ConversationAgents
		{
			get
			{
				return this._conversationAgents;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x0008718D File Offset: 0x0008538D
		public IAgent OneToOneConversationAgent
		{
			get
			{
				if (this.ConversationAgents.IsEmpty<IAgent>() || this.ConversationAgents.Count > 1)
				{
					return null;
				}
				return this.ConversationAgents[0];
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x000871B8 File Offset: 0x000853B8
		public IAgent SpeakerAgent
		{
			get
			{
				if (this.ConversationAgents != null)
				{
					return this._speakerAgent;
				}
				return null;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x000871CA File Offset: 0x000853CA
		public IAgent ListenerAgent
		{
			get
			{
				if (this.ConversationAgents != null)
				{
					return this._listenerAgent;
				}
				return null;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x000871DC File Offset: 0x000853DC
		// (set) Token: 0x06001DC3 RID: 7619 RVA: 0x000871E4 File Offset: 0x000853E4
		public bool IsConversationInProgress { get; private set; }

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x000871ED File Offset: 0x000853ED
		public Hero OneToOneConversationHero
		{
			get
			{
				if (this.OneToOneConversationCharacter != null)
				{
					return this.OneToOneConversationCharacter.HeroObject;
				}
				return null;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001DC5 RID: 7621 RVA: 0x00087204 File Offset: 0x00085404
		public CharacterObject OneToOneConversationCharacter
		{
			get
			{
				if (this.OneToOneConversationAgent != null)
				{
					return (CharacterObject)this.OneToOneConversationAgent.Character;
				}
				return null;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x00087220 File Offset: 0x00085420
		public IEnumerable<CharacterObject> ConversationCharacters
		{
			get
			{
				new List<CharacterObject>();
				foreach (IAgent agent in this.ConversationAgents)
				{
					yield return (CharacterObject)agent.Character;
				}
				IEnumerator<IAgent> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x00087230 File Offset: 0x00085430
		public bool IsAgentInConversation(IAgent agent)
		{
			return this.ConversationAgents.Contains(agent);
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x0008723E File Offset: 0x0008543E
		public MobileParty ConversationParty
		{
			get
			{
				return this._conversationParty;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001DC9 RID: 7625 RVA: 0x00087246 File Offset: 0x00085446
		// (set) Token: 0x06001DCA RID: 7626 RVA: 0x0008724E File Offset: 0x0008544E
		public bool NeedsToActivateForMapConversation { get; private set; }

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06001DCB RID: 7627 RVA: 0x00087258 File Offset: 0x00085458
		// (remove) Token: 0x06001DCC RID: 7628 RVA: 0x00087290 File Offset: 0x00085490
		public event Action ConversationSetup;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06001DCD RID: 7629 RVA: 0x000872C8 File Offset: 0x000854C8
		// (remove) Token: 0x06001DCE RID: 7630 RVA: 0x00087300 File Offset: 0x00085500
		public event Action ConversationBegin;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06001DCF RID: 7631 RVA: 0x00087338 File Offset: 0x00085538
		// (remove) Token: 0x06001DD0 RID: 7632 RVA: 0x00087370 File Offset: 0x00085570
		public event Action ConversationEnd;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06001DD1 RID: 7633 RVA: 0x000873A8 File Offset: 0x000855A8
		// (remove) Token: 0x06001DD2 RID: 7634 RVA: 0x000873E0 File Offset: 0x000855E0
		public event Action ConversationEndOneShot;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06001DD3 RID: 7635 RVA: 0x00087418 File Offset: 0x00085618
		// (remove) Token: 0x06001DD4 RID: 7636 RVA: 0x00087450 File Offset: 0x00085650
		public event Action ConversationContinued;

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00087485 File Offset: 0x00085685
		private void SetupConversation()
		{
			this.IsConversationInProgress = true;
			IConversationStateHandler handler = this.Handler;
			if (handler == null)
			{
				return;
			}
			handler.OnConversationInstall();
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x0008749E File Offset: 0x0008569E
		public void BeginConversation()
		{
			this.IsConversationInProgress = true;
			if (this.ConversationSetup != null)
			{
				this.ConversationSetup();
			}
			if (this.ConversationBegin != null)
			{
				this.ConversationBegin();
			}
			this.NeedsToActivateForMapConversation = false;
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x000874D4 File Offset: 0x000856D4
		public void EndConversation()
		{
			Debug.Print("--------------- Conversation End --------------- ", 0, Debug.DebugColor.White, 4503599627370496UL);
			if (CampaignMission.Current != null)
			{
				foreach (IAgent agent in this.ConversationAgents)
				{
					CampaignMission.Current.OnConversationEnd(agent);
				}
			}
			this._conversationParty = null;
			if (this.ConversationEndOneShot != null)
			{
				this.ConversationEndOneShot();
				this.ConversationEndOneShot = null;
			}
			if (this.ConversationEnd != null)
			{
				this.ConversationEnd();
			}
			this.IsConversationInProgress = false;
			foreach (IAgent agent2 in this.ConversationAgents)
			{
				agent2.SetAsConversationAgent(false);
			}
			Campaign.Current.CurrentConversationContext = ConversationContext.Default;
			CampaignEventDispatcher.Instance.OnConversationEnded(this.ConversationCharacters);
			if (ConversationManager.GetPersuasionIsActive())
			{
				ConversationManager.EndPersuasion();
			}
			this._conversationAgents.Clear();
			this._speakerAgent = null;
			this._listenerAgent = null;
			this._mainAgent = null;
			if (this.IsConversationFlowActive)
			{
				this.OnConversationDeactivate();
			}
			IConversationStateHandler handler = this.Handler;
			if (handler == null)
			{
				return;
			}
			handler.OnConversationUninstall();
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x0008761C File Offset: 0x0008581C
		public void DoOption(int optionIndex)
		{
			this.LastSelectedButtonIndex = optionIndex;
			this.ProcessSentence(this.CurOptions[optionIndex]);
			if (this._isActive)
			{
				this.DoOptionContinue();
				return;
			}
			this._executeDoOptionContinue = true;
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x00087650 File Offset: 0x00085850
		public void DoOption(string optionID)
		{
			int count = Campaign.Current.ConversationManager.CurOptions.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.CurOptions[i].Id == optionID)
				{
					this.DoOption(i);
					return;
				}
			}
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x0008769F File Offset: 0x0008589F
		public void DoConversationContinuedCallback()
		{
			if (this.ConversationContinued != null)
			{
				this.ConversationContinued();
			}
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x000876B4 File Offset: 0x000858B4
		public void DoOptionContinue()
		{
			if (this.IsConversationEnded() && this._sentences[this._currentSentence].IsPlayer)
			{
				this.EndConversation();
				return;
			}
			this.ProcessPartnerSentence();
			this.DoConversationContinuedCallback();
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x000876EC File Offset: 0x000858EC
		public void ContinueConversation()
		{
			if (this.CurOptions.Count <= 1)
			{
				if (this.IsConversationEnded())
				{
					this.EndConversation();
					return;
				}
				if (!this.ProcessPartnerSentence() && this.ListenerAgent.Character == Hero.MainHero.CharacterObject)
				{
					this.EndConversation();
					return;
				}
				this.DoConversationContinuedCallback();
				if (CampaignMission.Current != null)
				{
					CampaignMission.Current.OnConversationContinue();
				}
			}
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x00087754 File Offset: 0x00085954
		public void SetupAndStartMissionConversation(IAgent agent, IAgent mainAgent, bool setActionsInstantly)
		{
			CampaignEvents.SetupPreConversation();
			this.SetupConversation();
			this._mainAgent = mainAgent;
			this._conversationAgents.Clear();
			this.AddConversationAgent(agent);
			this._conversationParty = null;
			this.StartNew(0, setActionsInstantly);
			if (!this.IsConversationFlowActive)
			{
				this.OnConversationActivate();
			}
			this.BeginConversation();
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x000877A8 File Offset: 0x000859A8
		public void SetupAndStartMissionConversationWithMultipleAgents(IEnumerable<IAgent> agents, IAgent mainAgent)
		{
			this.SetupConversation();
			this._mainAgent = mainAgent;
			this._conversationAgents.Clear();
			this.AddConversationAgents(agents, true);
			this._conversationParty = null;
			this.StartNew(0, true);
			if (!this.IsConversationFlowActive)
			{
				this.OnConversationActivate();
			}
			this.BeginConversation();
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x000877F8 File Offset: 0x000859F8
		public void SetupAndStartMapConversation(MobileParty party, IAgent agent, IAgent mainAgent)
		{
			this._conversationParty = party;
			CampaignEvents.SetupPreConversation();
			this._mainAgent = mainAgent;
			this._conversationAgents.Clear();
			this.AddConversationAgent(agent);
			this.SetupConversation();
			this.StartNew(0, true);
			this.NeedsToActivateForMapConversation = true;
			if (!this.IsConversationFlowActive)
			{
				this.OnConversationActivate();
			}
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x00087850 File Offset: 0x00085A50
		public void AddConversationAgents(IEnumerable<IAgent> agents, bool setActionsInstantly)
		{
			foreach (IAgent agent in agents)
			{
				if (agent.IsActive() && !this.ConversationAgents.Contains(agent))
				{
					this.AddConversationAgent(agent);
					CampaignMission.Current.OnConversationStart(agent, setActionsInstantly);
				}
			}
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x000878BC File Offset: 0x00085ABC
		private void AddConversationAgent(IAgent agent)
		{
			this._conversationAgents.Add(agent);
			agent.SetAsConversationAgent(true);
			CampaignEventDispatcher.Instance.OnAgentJoinedConversation(agent);
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x000878DC File Offset: 0x00085ADC
		public bool IsConversationAgent(IAgent agent)
		{
			return this.ConversationAgents != null && this.ConversationAgents.Contains(agent);
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x000878F4 File Offset: 0x00085AF4
		public void RemoveRelatedLines(object o)
		{
			this._sentences.RemoveAll((ConversationSentence s) => s.RelatedObject == o);
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001DE4 RID: 7652 RVA: 0x00087926 File Offset: 0x00085B26
		// (set) Token: 0x06001DE5 RID: 7653 RVA: 0x0008792E File Offset: 0x00085B2E
		public IConversationStateHandler Handler { get; set; }

		// Token: 0x06001DE6 RID: 7654 RVA: 0x00087937 File Offset: 0x00085B37
		public void OnConversationDeactivate()
		{
			this._isActive = false;
			IConversationStateHandler handler = this.Handler;
			if (handler == null)
			{
				return;
			}
			handler.OnConversationDeactivate();
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x00087950 File Offset: 0x00085B50
		public void OnConversationActivate()
		{
			this._isActive = true;
			if (this._executeDoOptionContinue)
			{
				this._executeDoOptionContinue = false;
				this.DoOptionContinue();
			}
			IConversationStateHandler handler = this.Handler;
			if (handler == null)
			{
				return;
			}
			handler.OnConversationActivate();
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x00087980 File Offset: 0x00085B80
		public TextObject FindMatchingTextOrNull(string id, CharacterObject character)
		{
			float num = -2.1474836E+09f;
			TextObject result = null;
			GameText gameText = Game.Current.GameTextManager.GetGameText(id);
			if (gameText != null)
			{
				foreach (GameText.GameTextVariation gameTextVariation in gameText.Variations)
				{
					float num2 = this.FindMatchingScore(character, gameTextVariation.Tags);
					if (num2 > num)
					{
						result = gameTextVariation.Text;
						num = num2;
					}
				}
			}
			return result;
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x00087A04 File Offset: 0x00085C04
		private float FindMatchingScore(CharacterObject character, GameTextManager.ChoiceTag[] choiceTags)
		{
			float num = 0f;
			foreach (GameTextManager.ChoiceTag choiceTag in choiceTags)
			{
				if (choiceTag.TagName != "DefaultTag")
				{
					if (this.IsTagApplicable(choiceTag.TagName, character) == choiceTag.IsTagReversed)
					{
						return -2.1474836E+09f;
					}
					uint weight = choiceTag.Weight;
					num += weight;
				}
			}
			return num;
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x00087A70 File Offset: 0x00085C70
		private void InitializeTags()
		{
			this._tags = new Dictionary<string, ConversationTag>();
			string name = typeof(ConversationTag).Assembly.GetName().Name;
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
				bool flag = false;
				if (name == assembly.GetName().Name)
				{
					flag = true;
				}
				else
				{
					AssemblyName[] array = referencedAssemblies;
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].Name == name)
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					foreach (Type type in assembly.GetTypesSafe(null))
					{
						if (type.IsSubclassOf(typeof(ConversationTag)))
						{
							ConversationTag conversationTag = Activator.CreateInstance(type) as ConversationTag;
							this._tags.Add(conversationTag.StringId, conversationTag);
						}
					}
				}
			}
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x00087B94 File Offset: 0x00085D94
		private static void SetupTextVariables()
		{
			StringHelpers.SetCharacterProperties("PLAYER", Hero.MainHero.CharacterObject, null, false);
			int num = 1;
			foreach (CharacterObject character in CharacterObject.ConversationCharacters)
			{
				string str = (num == 1) ? "" : ("_" + num);
				StringHelpers.SetCharacterProperties("CONVERSATION_CHARACTER" + str, character, null, false);
			}
			MBTextManager.SetTextVariable("CURRENT_SETTLEMENT_NAME", (Settlement.CurrentSettlement == null) ? TextObject.Empty : Settlement.CurrentSettlement.Name, false);
			ConversationHelper.ConversationTroopCommentShown = false;
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x00087C4C File Offset: 0x00085E4C
		public IEnumerable<string> GetApplicableTagNames(CharacterObject character)
		{
			foreach (ConversationTag conversationTag in this._tags.Values)
			{
				if (conversationTag.IsApplicableTo(character))
				{
					yield return conversationTag.StringId;
				}
			}
			Dictionary<string, ConversationTag>.ValueCollection.Enumerator enumerator = default(Dictionary<string, ConversationTag>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x00087C64 File Offset: 0x00085E64
		public bool IsTagApplicable(string tagId, CharacterObject character)
		{
			ConversationTag conversationTag;
			if (this._tags.TryGetValue(tagId, out conversationTag))
			{
				return conversationTag.IsApplicableTo(character);
			}
			Debug.FailedAssert("asking for a nonexistent tag", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\Conversation\\ConversationManager.cs", "IsTagApplicable", 1432);
			return false;
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x00087CA4 File Offset: 0x00085EA4
		public void OpenMapConversation(ConversationCharacterData playerCharacterData, ConversationCharacterData conversationPartnerData)
		{
			GameStateManager gameStateManager = GameStateManager.Current;
			(((gameStateManager != null) ? gameStateManager.ActiveState : null) as MapState).OnMapConversationStarts(playerCharacterData, conversationPartnerData);
			PartyBase party = conversationPartnerData.Party;
			this.SetupAndStartMapConversation((party != null) ? party.MobileParty : null, new MapConversationAgent(conversationPartnerData.Character), new MapConversationAgent(CharacterObject.PlayerCharacter));
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x00087CFB File Offset: 0x00085EFB
		public static void StartPersuasion(float goalValue, float successValue, float failValue, float criticalSuccessValue, float criticalFailValue, float initialProgress = -1f, PersuasionDifficulty difficulty = PersuasionDifficulty.Medium)
		{
			ConversationManager._persuasion = new Persuasion(goalValue, successValue, failValue, criticalSuccessValue, criticalFailValue, initialProgress, difficulty);
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x00087D11 File Offset: 0x00085F11
		public static void EndPersuasion()
		{
			ConversationManager._persuasion = null;
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x00087D19 File Offset: 0x00085F19
		public static void PersuasionCommitProgress(PersuasionOptionArgs persuasionOptionArgs)
		{
			ConversationManager._persuasion.CommitProgress(persuasionOptionArgs);
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x00087D26 File Offset: 0x00085F26
		public static void Clear()
		{
			ConversationManager._persuasion = null;
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x00087D2E File Offset: 0x00085F2E
		public void GetPersuasionChanceValues(out float successValue, out float critSuccessValue, out float critFailValue)
		{
			successValue = ConversationManager._persuasion.SuccessValue;
			critSuccessValue = ConversationManager._persuasion.CriticalSuccessValue;
			critFailValue = ConversationManager._persuasion.CriticalFailValue;
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x00087D54 File Offset: 0x00085F54
		public static bool GetPersuasionIsActive()
		{
			return ConversationManager._persuasion != null;
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x00087D5E File Offset: 0x00085F5E
		public static bool GetPersuasionProgressSatisfied()
		{
			return ConversationManager._persuasion.Progress >= ConversationManager._persuasion.GoalValue;
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x00087D79 File Offset: 0x00085F79
		public static bool GetPersuasionIsFailure()
		{
			return ConversationManager._persuasion.Progress < 0f;
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x00087D8C File Offset: 0x00085F8C
		public static float GetPersuasionProgress()
		{
			return ConversationManager._persuasion.Progress;
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x00087D98 File Offset: 0x00085F98
		public static float GetPersuasionGoalValue()
		{
			return ConversationManager._persuasion.GoalValue;
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x00087DA4 File Offset: 0x00085FA4
		public static IEnumerable<Tuple<PersuasionOptionArgs, PersuasionOptionResult>> GetPersuasionChosenOptions()
		{
			return ConversationManager._persuasion.GetChosenOptions();
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x00087DB0 File Offset: 0x00085FB0
		public void GetPersuasionChances(ConversationSentenceOption conversationSentenceOption, out float successChance, out float critSuccessChance, out float critFailChance, out float failChance)
		{
			ConversationSentence conversationSentence = this._sentences[conversationSentenceOption.SentenceNo];
			if (conversationSentenceOption.HasPersuasion)
			{
				Campaign.Current.Models.PersuasionModel.GetChances(conversationSentence.PersuationOptionArgs, out successChance, out critSuccessChance, out critFailChance, out failChance, ConversationManager._persuasion.DifficultyMultiplier);
				return;
			}
			successChance = 0f;
			critSuccessChance = 0f;
			critFailChance = 0f;
			failChance = 0f;
		}

		// Token: 0x04000926 RID: 2342
		private int _currentRepeatedDialogSetIndex;

		// Token: 0x04000927 RID: 2343
		private int _currentRepeatIndex;

		// Token: 0x04000928 RID: 2344
		private int _autoId;

		// Token: 0x04000929 RID: 2345
		private int _autoToken;

		// Token: 0x0400092A RID: 2346
		private int _numConversationSentencesCreated;

		// Token: 0x0400092B RID: 2347
		private List<ConversationSentence> _sentences;

		// Token: 0x0400092C RID: 2348
		private int _numberOfStateIndices;

		// Token: 0x0400092D RID: 2349
		public int ActiveToken;

		// Token: 0x0400092E RID: 2350
		private int _currentSentence;

		// Token: 0x0400092F RID: 2351
		private TextObject _currentSentenceText;

		// Token: 0x04000930 RID: 2352
		public List<Tuple<string, CharacterObject>> DetailedDebugLog = new List<Tuple<string, CharacterObject>>();

		// Token: 0x04000931 RID: 2353
		public string CurrentFaceAnimationRecord;

		// Token: 0x04000932 RID: 2354
		private object _lastSelectedDialogObject;

		// Token: 0x04000933 RID: 2355
		private readonly List<List<object>> _dialogRepeatObjects = new List<List<object>>();

		// Token: 0x04000934 RID: 2356
		private readonly List<TextObject> _dialogRepeatLines = new List<TextObject>();

		// Token: 0x04000935 RID: 2357
		private bool _isActive;

		// Token: 0x04000936 RID: 2358
		private bool _executeDoOptionContinue;

		// Token: 0x04000937 RID: 2359
		public int LastSelectedButtonIndex;

		// Token: 0x04000938 RID: 2360
		public string LastSelectedDialog;

		// Token: 0x04000939 RID: 2361
		public ConversationAnimationManager ConversationAnimationManager;

		// Token: 0x0400093A RID: 2362
		private IAgent _mainAgent;

		// Token: 0x0400093B RID: 2363
		private IAgent _speakerAgent;

		// Token: 0x0400093C RID: 2364
		private IAgent _listenerAgent;

		// Token: 0x0400093D RID: 2365
		private Dictionary<string, ConversationTag> _tags;

		// Token: 0x0400093E RID: 2366
		private bool _sortSentenceIsDisabled;

		// Token: 0x0400093F RID: 2367
		private Dictionary<string, int> stateMap;

		// Token: 0x04000944 RID: 2372
		private List<IAgent> _conversationAgents = new List<IAgent>();

		// Token: 0x04000946 RID: 2374
		public bool CurrentConversationIsFirst;

		// Token: 0x04000947 RID: 2375
		private MobileParty _conversationParty;

		// Token: 0x0400094F RID: 2383
		private static Persuasion _persuasion;

		// Token: 0x02000567 RID: 1383
		public class TaggedString
		{
			// Token: 0x040016CF RID: 5839
			public TextObject Text;

			// Token: 0x040016D0 RID: 5840
			public List<GameTextManager.ChoiceTag> ChoiceTags = new List<GameTextManager.ChoiceTag>();

			// Token: 0x040016D1 RID: 5841
			public int FacialAnimation;
		}
	}
}

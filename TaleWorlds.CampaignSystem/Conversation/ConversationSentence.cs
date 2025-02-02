using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using TaleWorlds.CampaignSystem.Conversation.Persuasion;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Conversation
{
	// Token: 0x020001ED RID: 493
	public class ConversationSentence
	{
		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x00087E20 File Offset: 0x00086020
		// (set) Token: 0x06001E02 RID: 7682 RVA: 0x00087E28 File Offset: 0x00086028
		public TextObject Text { get; private set; }

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x00087E31 File Offset: 0x00086031
		// (set) Token: 0x06001E04 RID: 7684 RVA: 0x00087E39 File Offset: 0x00086039
		public int Index { get; internal set; }

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001E05 RID: 7685 RVA: 0x00087E42 File Offset: 0x00086042
		// (set) Token: 0x06001E06 RID: 7686 RVA: 0x00087E4A File Offset: 0x0008604A
		public string Id { get; private set; }

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x00087E53 File Offset: 0x00086053
		// (set) Token: 0x06001E08 RID: 7688 RVA: 0x00087E5C File Offset: 0x0008605C
		public bool IsPlayer
		{
			get
			{
				return this.GetFlags(ConversationSentence.DialogLineFlags.PlayerLine);
			}
			internal set
			{
				this.set_flags(value, ConversationSentence.DialogLineFlags.PlayerLine);
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x00087E66 File Offset: 0x00086066
		// (set) Token: 0x06001E0A RID: 7690 RVA: 0x00087E6F File Offset: 0x0008606F
		public bool IsRepeatable
		{
			get
			{
				return this.GetFlags(ConversationSentence.DialogLineFlags.RepeatForObjects);
			}
			internal set
			{
				this.set_flags(value, ConversationSentence.DialogLineFlags.RepeatForObjects);
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x00087E79 File Offset: 0x00086079
		// (set) Token: 0x06001E0C RID: 7692 RVA: 0x00087E82 File Offset: 0x00086082
		public bool IsSpecial
		{
			get
			{
				return this.GetFlags(ConversationSentence.DialogLineFlags.SpecialLine);
			}
			internal set
			{
				this.set_flags(value, ConversationSentence.DialogLineFlags.SpecialLine);
			}
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x00087E8C File Offset: 0x0008608C
		private bool GetFlags(ConversationSentence.DialogLineFlags flag)
		{
			return (this._flags & (uint)flag) > 0U;
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x00087E99 File Offset: 0x00086099
		private void set_flags(bool val, ConversationSentence.DialogLineFlags newFlag)
		{
			if (val)
			{
				this._flags |= (uint)newFlag;
				return;
			}
			this._flags &= (uint)(~(uint)newFlag);
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x00087EBC File Offset: 0x000860BC
		// (set) Token: 0x06001E10 RID: 7696 RVA: 0x00087EC4 File Offset: 0x000860C4
		public int Priority { get; private set; }

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x00087ECD File Offset: 0x000860CD
		// (set) Token: 0x06001E12 RID: 7698 RVA: 0x00087ED5 File Offset: 0x000860D5
		public int InputToken { get; private set; }

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001E13 RID: 7699 RVA: 0x00087EDE File Offset: 0x000860DE
		// (set) Token: 0x06001E14 RID: 7700 RVA: 0x00087EE6 File Offset: 0x000860E6
		public int OutputToken { get; private set; }

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001E15 RID: 7701 RVA: 0x00087EEF File Offset: 0x000860EF
		// (set) Token: 0x06001E16 RID: 7702 RVA: 0x00087EF7 File Offset: 0x000860F7
		public object RelatedObject { get; private set; }

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x00087F09 File Offset: 0x00086109
		// (set) Token: 0x06001E17 RID: 7703 RVA: 0x00087F00 File Offset: 0x00086100
		public bool IsWithVariation { get; private set; }

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x00087F11 File Offset: 0x00086111
		// (set) Token: 0x06001E1A RID: 7706 RVA: 0x00087F19 File Offset: 0x00086119
		public PersuasionOptionArgs PersuationOptionArgs { get; private set; }

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x00087F22 File Offset: 0x00086122
		public bool HasPersuasion
		{
			get
			{
				return this._onPersuasionOption != null;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001E1C RID: 7708 RVA: 0x00087F2D File Offset: 0x0008612D
		public string SkillName
		{
			get
			{
				if (!this.HasPersuasion)
				{
					return "";
				}
				return this.PersuationOptionArgs.SkillUsed.ToString();
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001E1D RID: 7709 RVA: 0x00087F4D File Offset: 0x0008614D
		public string TraitName
		{
			get
			{
				if (!this.HasPersuasion)
				{
					return "";
				}
				if (this.PersuationOptionArgs.TraitUsed == null)
				{
					return "";
				}
				return this.PersuationOptionArgs.TraitUsed.ToString();
			}
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x00087F80 File Offset: 0x00086180
		internal ConversationSentence(string idString, TextObject text, string inputToken, string outputToken, ConversationSentence.OnConditionDelegate conditionDelegate, ConversationSentence.OnClickableConditionDelegate clickableConditionDelegate, ConversationSentence.OnConsequenceDelegate consequenceDelegate, uint flags = 0U, int priority = 100, int agentIndex = 0, int nextAgentIndex = 0, object relatedObject = null, bool withVariation = false, ConversationSentence.OnMultipleConversationConsequenceDelegate speakerDelegate = null, ConversationSentence.OnMultipleConversationConsequenceDelegate listenerDelegate = null, ConversationSentence.OnPersuasionOptionDelegate persuasionOptionDelegate = null)
		{
			this.Index = Campaign.Current.ConversationManager.CreateConversationSentenceIndex();
			this.Id = idString;
			this.Text = text;
			this.InputToken = Campaign.Current.ConversationManager.GetStateIndex(inputToken);
			this.OutputToken = Campaign.Current.ConversationManager.GetStateIndex(outputToken);
			this.OnCondition = conditionDelegate;
			this.OnClickableCondition = clickableConditionDelegate;
			this.OnConsequence = consequenceDelegate;
			this._flags = flags;
			this.Priority = priority;
			this.AgentIndex = agentIndex;
			this.NextAgentIndex = nextAgentIndex;
			this.RelatedObject = relatedObject;
			this.IsWithVariation = withVariation;
			this.IsSpeaker = speakerDelegate;
			this.IsListener = listenerDelegate;
			this._onPersuasionOption = persuasionOptionDelegate;
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x0008804A File Offset: 0x0008624A
		internal ConversationSentence(int index)
		{
			this.Index = index;
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x00088060 File Offset: 0x00086260
		public ConversationSentence Variation(params object[] list)
		{
			Game.Current.GameTextManager.AddGameText(this.Id).AddVariation((string)list[0], list.Skip(1).ToArray<object>());
			return this;
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x00088091 File Offset: 0x00086291
		internal void RunConsequence(Game game)
		{
			if (this.OnConsequence != null)
			{
				this.OnConsequence();
			}
			Campaign.Current.ConversationManager.OnConsequence(this);
			if (this.HasPersuasion)
			{
				ConversationManager.PersuasionCommitProgress(this.PersuationOptionArgs);
			}
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x000880CC File Offset: 0x000862CC
		internal bool RunCondition()
		{
			bool flag = true;
			if (this.OnCondition != null)
			{
				flag = this.OnCondition();
			}
			if (flag && this.HasPersuasion)
			{
				this.PersuationOptionArgs = this._onPersuasionOption();
			}
			Campaign.Current.ConversationManager.OnCondition(this);
			return flag;
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x0008811C File Offset: 0x0008631C
		internal bool RunClickableCondition()
		{
			bool result = true;
			if (this.OnClickableCondition != null)
			{
				result = this.OnClickableCondition(out this.HintText);
			}
			Campaign.Current.ConversationManager.OnClickableCondition(this);
			return result;
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x00088158 File Offset: 0x00086358
		public void Deserialize(XmlNode node, Type typeOfConversationCallbacks, ConversationManager conversationManager, int defaultPriority)
		{
			if (node.Attributes == null)
			{
				throw new TWXmlLoadException("node.Attributes != null");
			}
			this.Id = node.Attributes["id"].Value;
			XmlNode xmlNode = node.Attributes["on_condition"];
			if (xmlNode != null)
			{
				string innerText = xmlNode.InnerText;
				this._methodOnCondition = typeOfConversationCallbacks.GetMethod(innerText);
				if (this._methodOnCondition == null)
				{
					throw new MBMethodNameNotFoundException(innerText);
				}
				this.OnCondition = (Delegate.CreateDelegate(typeof(ConversationSentence.OnConditionDelegate), null, this._methodOnCondition) as ConversationSentence.OnConditionDelegate);
			}
			XmlNode xmlNode2 = node.Attributes["on_clickable_condition"];
			if (xmlNode2 != null)
			{
				string innerText2 = xmlNode2.InnerText;
				this._methodOnClickableCondition = typeOfConversationCallbacks.GetMethod(innerText2);
				if (this._methodOnClickableCondition == null)
				{
					throw new MBMethodNameNotFoundException(innerText2);
				}
				this.OnClickableCondition = (Delegate.CreateDelegate(typeof(ConversationSentence.OnClickableConditionDelegate), null, this._methodOnClickableCondition) as ConversationSentence.OnClickableConditionDelegate);
			}
			XmlNode xmlNode3 = node.Attributes["on_consequence"];
			if (xmlNode3 != null)
			{
				string innerText3 = xmlNode3.InnerText;
				this._methodOnConsequence = typeOfConversationCallbacks.GetMethod(innerText3);
				if (this._methodOnConsequence == null)
				{
					throw new MBMethodNameNotFoundException(innerText3);
				}
				this.OnConsequence = (Delegate.CreateDelegate(typeof(ConversationSentence.OnConsequenceDelegate), null, this._methodOnConsequence) as ConversationSentence.OnConsequenceDelegate);
			}
			XmlNode xmlNode4 = node.Attributes["is_player"];
			if (xmlNode4 != null)
			{
				string innerText4 = xmlNode4.InnerText;
				this.IsPlayer = Convert.ToBoolean(innerText4);
			}
			XmlNode xmlNode5 = node.Attributes["is_repeatable"];
			if (xmlNode5 != null)
			{
				string innerText5 = xmlNode5.InnerText;
				this.IsRepeatable = Convert.ToBoolean(innerText5);
			}
			XmlNode xmlNode6 = node.Attributes["is_speacial_option"];
			if (xmlNode6 != null)
			{
				string innerText6 = xmlNode6.InnerText;
				this.IsSpecial = Convert.ToBoolean(innerText6);
			}
			XmlNode xmlNode7 = node.Attributes["text"];
			if (xmlNode7 != null)
			{
				this.Text = new TextObject(xmlNode7.InnerText, null);
			}
			XmlNode xmlNode8 = node.Attributes["istate"];
			if (xmlNode8 != null)
			{
				this.InputToken = conversationManager.GetStateIndex(xmlNode8.InnerText);
			}
			XmlNode xmlNode9 = node.Attributes["ostate"];
			if (xmlNode9 != null)
			{
				this.OutputToken = conversationManager.GetStateIndex(xmlNode9.InnerText);
			}
			XmlNode xmlNode10 = node.Attributes["priority"];
			this.Priority = ((xmlNode10 != null) ? int.Parse(xmlNode10.InnerText) : defaultPriority);
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001E25 RID: 7717 RVA: 0x000883DE File Offset: 0x000865DE
		public static object CurrentProcessedRepeatObject
		{
			get
			{
				return Campaign.Current.ConversationManager.GetCurrentProcessedRepeatObject();
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001E26 RID: 7718 RVA: 0x000883EF File Offset: 0x000865EF
		public static object SelectedRepeatObject
		{
			get
			{
				return Campaign.Current.ConversationManager.GetSelectedRepeatObject();
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001E27 RID: 7719 RVA: 0x00088400 File Offset: 0x00086600
		public static TextObject SelectedRepeatLine
		{
			get
			{
				return Campaign.Current.ConversationManager.GetCurrentDialogLine();
			}
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x00088411 File Offset: 0x00086611
		public static void SetObjectsToRepeatOver(IReadOnlyList<object> objectsToRepeatOver, int maxRepeatedDialogsInConversation = 5)
		{
			Campaign.Current.ConversationManager.SetDialogRepeatCount(objectsToRepeatOver, maxRepeatedDialogsInConversation);
		}

		// Token: 0x0400095C RID: 2396
		public const int DefaultPriority = 100;

		// Token: 0x04000960 RID: 2400
		public int AgentIndex;

		// Token: 0x04000961 RID: 2401
		public int NextAgentIndex;

		// Token: 0x04000962 RID: 2402
		public bool IsClickable = true;

		// Token: 0x04000963 RID: 2403
		public TextObject HintText;

		// Token: 0x04000968 RID: 2408
		private MethodInfo _methodOnCondition;

		// Token: 0x04000969 RID: 2409
		public ConversationSentence.OnConditionDelegate OnCondition;

		// Token: 0x0400096A RID: 2410
		private MethodInfo _methodOnClickableCondition;

		// Token: 0x0400096B RID: 2411
		public ConversationSentence.OnClickableConditionDelegate OnClickableCondition;

		// Token: 0x0400096C RID: 2412
		private MethodInfo _methodOnConsequence;

		// Token: 0x0400096D RID: 2413
		public ConversationSentence.OnConsequenceDelegate OnConsequence;

		// Token: 0x0400096E RID: 2414
		public ConversationSentence.OnMultipleConversationConsequenceDelegate IsSpeaker;

		// Token: 0x0400096F RID: 2415
		public ConversationSentence.OnMultipleConversationConsequenceDelegate IsListener;

		// Token: 0x04000970 RID: 2416
		private uint _flags;

		// Token: 0x04000972 RID: 2418
		private ConversationSentence.OnPersuasionOptionDelegate _onPersuasionOption;

		// Token: 0x0200056C RID: 1388
		public enum DialogLineFlags
		{
			// Token: 0x040016E2 RID: 5858
			PlayerLine = 1,
			// Token: 0x040016E3 RID: 5859
			RepeatForObjects,
			// Token: 0x040016E4 RID: 5860
			SpecialLine = 4
		}

		// Token: 0x0200056D RID: 1389
		// (Invoke) Token: 0x06004564 RID: 17764
		public delegate bool OnConditionDelegate();

		// Token: 0x0200056E RID: 1390
		// (Invoke) Token: 0x06004568 RID: 17768
		public delegate bool OnClickableConditionDelegate(out TextObject explanation);

		// Token: 0x0200056F RID: 1391
		// (Invoke) Token: 0x0600456C RID: 17772
		public delegate PersuasionOptionArgs OnPersuasionOptionDelegate();

		// Token: 0x02000570 RID: 1392
		// (Invoke) Token: 0x06004570 RID: 17776
		public delegate void OnConsequenceDelegate();

		// Token: 0x02000571 RID: 1393
		// (Invoke) Token: 0x06004574 RID: 17780
		public delegate bool OnMultipleConversationConsequenceDelegate(IAgent agent);
	}
}

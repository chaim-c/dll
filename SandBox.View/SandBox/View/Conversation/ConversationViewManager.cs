using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.Screens;

namespace SandBox.View.Conversation
{
	// Token: 0x0200005A RID: 90
	public class ConversationViewManager
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x000225C1 File Offset: 0x000207C1
		public static ConversationViewManager Instance
		{
			get
			{
				return SandBoxViewSubModule.ConversationViewManager;
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000225C8 File Offset: 0x000207C8
		public ConversationViewManager()
		{
			this.FillEventHandlers();
			Campaign.Current.ConversationManager.ConditionRunned += this.OnCondition;
			Campaign.Current.ConversationManager.ConsequenceRunned += this.OnConsequence;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00022618 File Offset: 0x00020818
		private void FillEventHandlers()
		{
			this._conditionEventHandlers = new Dictionary<string, ConversationViewEventHandlerDelegate>();
			this._consequenceEventHandlers = new Dictionary<string, ConversationViewEventHandlerDelegate>();
			Assembly assembly = typeof(ConversationViewEventHandlerDelegate).Assembly;
			this.FillEventHandlersWith(assembly);
			foreach (Assembly assembly2 in GameStateScreenManager.GetViewAssemblies())
			{
				this.FillEventHandlersWith(assembly2);
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00022674 File Offset: 0x00020874
		private void FillEventHandlersWith(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypesSafe(null))
			{
				foreach (MethodInfo method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					object[] customAttributesSafe = method.GetCustomAttributesSafe(typeof(ConversationViewEventHandler), false);
					if (customAttributesSafe != null && customAttributesSafe.Length != 0)
					{
						foreach (ConversationViewEventHandler conversationViewEventHandler in customAttributesSafe)
						{
							ConversationViewEventHandlerDelegate value = Delegate.CreateDelegate(typeof(ConversationViewEventHandlerDelegate), method) as ConversationViewEventHandlerDelegate;
							if (conversationViewEventHandler.Type == ConversationViewEventHandler.EventType.OnCondition)
							{
								if (!this._conditionEventHandlers.ContainsKey(conversationViewEventHandler.Id))
								{
									this._conditionEventHandlers.Add(conversationViewEventHandler.Id, value);
								}
								else
								{
									this._conditionEventHandlers[conversationViewEventHandler.Id] = value;
								}
							}
							else if (conversationViewEventHandler.Type == ConversationViewEventHandler.EventType.OnConsequence)
							{
								if (!this._consequenceEventHandlers.ContainsKey(conversationViewEventHandler.Id))
								{
									this._consequenceEventHandlers.Add(conversationViewEventHandler.Id, value);
								}
								else
								{
									this._consequenceEventHandlers[conversationViewEventHandler.Id] = value;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000227E8 File Offset: 0x000209E8
		private void OnConsequence(ConversationSentence sentence)
		{
			ConversationViewEventHandlerDelegate conversationViewEventHandlerDelegate;
			if (this._consequenceEventHandlers.TryGetValue(sentence.Id, out conversationViewEventHandlerDelegate))
			{
				conversationViewEventHandlerDelegate();
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00022810 File Offset: 0x00020A10
		private void OnCondition(ConversationSentence sentence)
		{
			ConversationViewEventHandlerDelegate conversationViewEventHandlerDelegate;
			if (this._conditionEventHandlers.TryGetValue(sentence.Id, out conversationViewEventHandlerDelegate))
			{
				conversationViewEventHandlerDelegate();
			}
		}

		// Token: 0x0400023C RID: 572
		private Dictionary<string, ConversationViewEventHandlerDelegate> _conditionEventHandlers;

		// Token: 0x0400023D RID: 573
		private Dictionary<string, ConversationViewEventHandlerDelegate> _consequenceEventHandlers;
	}
}

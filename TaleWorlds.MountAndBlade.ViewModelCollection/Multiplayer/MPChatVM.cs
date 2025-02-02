using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Diamond;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Multiplayer
{
	// Token: 0x02000037 RID: 55
	public class MPChatVM : ViewModel, IChatHandler
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x000148DC File Offset: 0x00012ADC
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x000148E4 File Offset: 0x00012AE4
		public ChatChannelType ActiveChannelType
		{
			get
			{
				return this._activeChannelType;
			}
			set
			{
				if ((value == ChatChannelType.All || value == ChatChannelType.Team) && !GameNetwork.IsClient)
				{
					this._activeChannelType = ChatChannelType.NaN;
					this.IsChatDisabled = true;
					return;
				}
				if (value != this._activeChannelType)
				{
					this._activeChannelType = value;
					this.RefreshActiveChannelNameData();
					this.IsChatDisabled = false;
				}
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00014924 File Offset: 0x00012B24
		private string _playerName
		{
			get
			{
				string text = (NetworkMain.GameClient.PlayerData != null) ? NetworkMain.GameClient.Name : new TextObject("{=!}ERROR: MISSING PLAYERDATA", null).ToString();
				NetworkCommunicator myPeer = GameNetwork.MyPeer;
				MissionPeer missionPeer = (myPeer != null) ? myPeer.GetComponent<MissionPeer>() : null;
				if (missionPeer != null && !missionPeer.IsAgentAliveForChatting)
				{
					GameTexts.SetVariable("PLAYER_NAME", "{=!}" + text);
					text = GameTexts.FindText("str_chat_message_dead_player", null).ToString();
				}
				return text;
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000149A4 File Offset: 0x00012BA4
		public MPChatVM()
		{
			this._allMessages = new List<MPChatLineVM>();
			this._requestedMessages = new Queue<MPChatLineVM>();
			this.MessageHistory = new MBBindingList<MPChatLineVM>();
			this.CombatLogHint = new HintViewModel();
			this.IncludeCombatLog = BannerlordConfig.ReportDamage;
			this.IncludeBark = BannerlordConfig.ReportBark;
			InformationManager.DisplayMessageInternal += this.OnDisplayMessageReceived;
			InformationManager.ClearAllMessagesInternal += this.ClearAllMessages;
			ManagedOptions.OnManagedOptionChanged = (ManagedOptions.OnManagedOptionChangedDelegate)Delegate.Combine(ManagedOptions.OnManagedOptionChanged, new ManagedOptions.OnManagedOptionChangedDelegate(this.OnOptionChange));
			this.MaxMessageLength = 100;
			this._recentlySentMessagesTimes = new List<float>();
			this.RefreshValues();
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00014AB4 File Offset: 0x00012CB4
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.CombatLogHint.HintText = new TextObject("{=FRSGOfUJ}Toggle include Combat Log", null);
			this.ToggleCombatLogText = new TextObject("{=rx18kyZb}Combat Log", null).ToString();
			this.ToggleBarkText = new TextObject("{=NuMQvQxg}Shouts", null).ToString();
			this.UpdateHideShowText(this._isInspectingMessages);
			this.UpdateShortcutTexts();
			this.RefreshActiveChannelNameData();
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00014B24 File Offset: 0x00012D24
		private void RefreshActiveChannelNameData()
		{
			if (this.ActiveChannelType == ChatChannelType.NaN)
			{
				this.ActiveChannelNameText = string.Empty;
				this.ActiveChannelColor = Color.White;
				return;
			}
			if (this.ActiveChannelType == ChatChannelType.Custom)
			{
				this.ActiveChannelNameText = "(" + this._currentCustomChatChannel.Name + ")";
				this.ActiveChannelColor = Color.ConvertStringToColor(this._currentCustomChatChannel.RoomColor);
				return;
			}
			string content = GameTexts.FindText("str_multiplayer_chat_channel", this.ActiveChannelType.ToString()).ToString();
			GameTexts.SetVariable("STR", content);
			this.ActiveChannelNameText = GameTexts.FindText("str_STR_in_parentheses", null).ToString();
			this.ActiveChannelColor = this.GetChannelColor(this.ActiveChannelType);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00014BE8 File Offset: 0x00012DE8
		private void OnOptionChange(ManagedOptions.ManagedOptionsType changedManagedOptionsType)
		{
			if (changedManagedOptionsType == ManagedOptions.ManagedOptionsType.ReportDamage)
			{
				this.IncludeCombatLog = BannerlordConfig.ReportDamage;
				return;
			}
			if (changedManagedOptionsType == ManagedOptions.ManagedOptionsType.ReportBark)
			{
				this.IncludeBark = BannerlordConfig.ReportBark;
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00014C0B File Offset: 0x00012E0B
		public void ToggleIncludeCombatLog()
		{
			this.IncludeCombatLog = !this.IncludeCombatLog;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00014C1C File Offset: 0x00012E1C
		public void ExecuteToggleIncludeShouts()
		{
			this.IncludeBark = !this.IncludeBark;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00014C30 File Offset: 0x00012E30
		private void UpdateHideShowText(bool isInspecting)
		{
			TextObject textObject = TextObject.Empty;
			if (this._game != null && isInspecting)
			{
				textObject = this._hideText;
				textObject.SetTextVariable("KEY", this._getToggleChatKeyText() ?? TextObject.Empty);
			}
			this.HideShowText = textObject.ToString();
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00014C84 File Offset: 0x00012E84
		private void UpdateShortcutTexts()
		{
			TextObject cycleChannelsText = this._cycleChannelsText;
			string tag = "KEY";
			Func<TextObject> getCycleChannelsKeyText = this._getCycleChannelsKeyText;
			cycleChannelsText.SetTextVariable(tag, ((getCycleChannelsKeyText != null) ? getCycleChannelsKeyText() : null) ?? TextObject.Empty);
			this.CycleThroughChannelsText = this._cycleChannelsText.ToString();
			if (Input.IsGamepadActive)
			{
				TextObject sendMessageTextObject = this._sendMessageTextObject;
				string tag2 = "KEY";
				Func<TextObject> getSendMessageKeyText = this._getSendMessageKeyText;
				sendMessageTextObject.SetTextVariable(tag2, ((getSendMessageKeyText != null) ? getSendMessageKeyText() : null) ?? TextObject.Empty);
				this.SendMessageText = this._sendMessageTextObject.ToString();
				TextObject cancelSendingTextObject = this._cancelSendingTextObject;
				string tag3 = "KEY";
				Func<TextObject> getCancelSendingKeyText = this._getCancelSendingKeyText;
				cancelSendingTextObject.SetTextVariable(tag3, ((getCancelSendingKeyText != null) ? getCancelSendingKeyText() : null) ?? TextObject.Empty);
				this.CancelSendingText = this._cancelSendingTextObject.ToString();
				return;
			}
			this.SendMessageText = string.Empty;
			this.CancelSendingText = string.Empty;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00014D68 File Offset: 0x00012F68
		public void Tick(float dt)
		{
			while (this._requestedMessages.Count > 0)
			{
				this.AddChatLine(this._requestedMessages.Dequeue());
			}
			float applicationTime = Time.ApplicationTime;
			for (int i = 0; i < this._recentlySentMessagesTimes.Count; i++)
			{
				if (applicationTime - this._recentlySentMessagesTimes[i] >= 15f)
				{
					this._recentlySentMessagesTimes.RemoveAt(i);
				}
			}
			this.CheckChatFading(dt);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00014DDC File Offset: 0x00012FDC
		public void Clear()
		{
			this._allMessages.ForEach(delegate(MPChatLineVM l)
			{
				l.ForceInvisible();
			});
			this.MessageHistory.ToList<MPChatLineVM>().ForEach(delegate(MPChatLineVM l)
			{
				l.ForceInvisible();
			});
			this._allMessages.Clear();
			this.MessageHistory.Clear();
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00014E58 File Offset: 0x00013058
		private void OnDisplayMessageReceived(InformationMessage informationMessage)
		{
			if (this.IsChatAllowedByOptions())
			{
				this.HandleAddChatLineRequest(informationMessage);
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00014E69 File Offset: 0x00013069
		private void ClearAllMessages()
		{
			this.Clear();
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00014E74 File Offset: 0x00013074
		public void UpdateObjects(Game game, Mission mission)
		{
			if (this._game != game)
			{
				if (this._game != null)
				{
					this.ClearGame();
				}
				this._game = game;
				if (this._game != null)
				{
					this.SetGame();
				}
			}
			if (this._mission != mission)
			{
				if (this._mission != null)
				{
					this.ClearMission();
				}
				this._mission = mission;
				if (this._mission != null)
				{
					this.SetMission();
				}
			}
			if (this._game != null)
			{
				ChatBox gameHandler = this._game.GetGameHandler<ChatBox>();
				if (this._chatBox != gameHandler)
				{
					if (this._chatBox != null)
					{
						this.ClearChatBox();
					}
					this._chatBox = gameHandler;
					if (this._chatBox != null)
					{
						this.SetChatBox();
					}
				}
			}
			this.IsOptionsAvailable = (this.IsInspectingMessages && this.IsTypingText);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00014F30 File Offset: 0x00013130
		private void ClearGame()
		{
			this._game = null;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00014F3C File Offset: 0x0001313C
		private void ClearChatBox()
		{
			if (this._chatBox != null)
			{
				this._chatBox.PlayerMessageReceived -= this.OnPlayerMessageReceived;
				this._chatBox.WhisperMessageSent -= this.OnWhisperMessageSent;
				this._chatBox.WhisperMessageReceived -= this.OnWhisperMessageReceived;
				this._chatBox.ErrorWhisperMessageReceived -= this.OnErrorWhisperMessageReceived;
				this._chatBox.ServerMessage -= this.OnServerMessage;
				this._chatBox.ServerAdminMessage -= this.OnServerAdminMessage;
				this._chatBox = null;
			}
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00014FE5 File Offset: 0x000131E5
		private void SetGame()
		{
			this.UpdateHideShowText(this.IsInspectingMessages);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00014FF4 File Offset: 0x000131F4
		private void SetChatBox()
		{
			this._chatBox.PlayerMessageReceived += this.OnPlayerMessageReceived;
			this._chatBox.WhisperMessageSent += this.OnWhisperMessageSent;
			this._chatBox.WhisperMessageReceived += this.OnWhisperMessageReceived;
			this._chatBox.ErrorWhisperMessageReceived += this.OnErrorWhisperMessageReceived;
			this._chatBox.ServerMessage += this.OnServerMessage;
			this._chatBox.ServerAdminMessage += this.OnServerAdminMessage;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001508C File Offset: 0x0001328C
		private void SetMission()
		{
			Game game = Game.Current;
			bool isChatDisabled;
			if (game == null)
			{
				isChatDisabled = false;
			}
			else
			{
				ChatBox gameHandler = game.GetGameHandler<ChatBox>();
				bool? flag = (gameHandler != null) ? new bool?(gameHandler.IsContentRestricted) : null;
				bool flag2 = true;
				isChatDisabled = (flag.GetValueOrDefault() == flag2 & flag != null);
			}
			this.IsChatDisabled = isChatDisabled;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000150DE File Offset: 0x000132DE
		private void ClearMission()
		{
			this._mission = null;
			this.ActiveChannelType = ChatChannelType.NaN;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000150EE File Offset: 0x000132EE
		public override void OnFinalize()
		{
			base.OnFinalize();
			if (this._game != null)
			{
				this.ClearGame();
			}
			if (this._mission != null)
			{
				this.ClearMission();
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00015114 File Offset: 0x00013314
		private void ExecuteSendMessage()
		{
			string text = this.WrittenText;
			if (string.IsNullOrEmpty(text))
			{
				this.WrittenText = string.Empty;
				return;
			}
			if (text.Length > this.MaxMessageLength)
			{
				text = this.WrittenText.Substring(0, this.MaxMessageLength);
			}
			text = Regex.Replace(text.Trim(), "\\s+", " ");
			if (text.StartsWith("/"))
			{
				string[] array = text.Split(new char[]
				{
					' '
				});
				ChatChannelType activeChannelType = ChatChannelType.NaN;
				LobbyClient gameClient = NetworkMain.GameClient;
				if (gameClient != null && gameClient.Connected)
				{
					LobbyClient gameClient2 = NetworkMain.GameClient;
					ChatManager.GetChatRoomResult getChatRoomResult = (gameClient2 != null) ? gameClient2.ChatManager.TryGetChatRoom(array[0]) : null;
					if (getChatRoomResult.Successful)
					{
						activeChannelType = ChatChannelType.Custom;
						this._currentCustomChatChannel = getChatRoomResult.Room;
					}
					else
					{
						string a = array[0].ToLower();
						if (!(a == "/all") && !(a == "/a"))
						{
							if (!(a == "/team") && !(a == "/t"))
							{
								if (!(a == "/ab"))
								{
									if (!(a == "/ac"))
									{
										MPChatLineVM chatLine = new MPChatLineVM(getChatRoomResult.ErrorMessage.ToString(), Color.White, "Social");
										this.AddChatLine(chatLine);
									}
									else if (Mission.Current != null)
									{
										MissionLobbyComponent missionBehavior = Mission.Current.GetMissionBehavior<MissionLobbyComponent>();
										if (missionBehavior != null)
										{
											missionBehavior.RequestAdminMessage(string.Join(" ", array.Skip(1)), false);
										}
									}
								}
								else if (Mission.Current != null)
								{
									MissionLobbyComponent missionBehavior2 = Mission.Current.GetMissionBehavior<MissionLobbyComponent>();
									if (missionBehavior2 != null)
									{
										missionBehavior2.RequestAdminMessage(string.Join(" ", array.Skip(1)), true);
									}
								}
							}
							else
							{
								activeChannelType = ChatChannelType.Team;
							}
						}
						else
						{
							activeChannelType = ChatChannelType.All;
						}
					}
				}
				this.ActiveChannelType = activeChannelType;
			}
			else
			{
				switch (this.ActiveChannelType)
				{
				case ChatChannelType.Private:
				case ChatChannelType.Custom:
					this.SendMessageToLobbyChannel(text);
					goto IL_216;
				case ChatChannelType.All:
				case ChatChannelType.Team:
				case ChatChannelType.Party:
					this.CheckSpamAndSendMessage(this.ActiveChannelType, text);
					goto IL_216;
				}
				Debug.FailedAssert("Player in invalid channel", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\Multiplayer\\MPChatVM.cs", "ExecuteSendMessage", 475);
			}
			IL_216:
			this.WrittenText = "";
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00015344 File Offset: 0x00013544
		private void CheckSpamAndSendMessage(ChatChannelType channelType, string textToSend)
		{
			if (this._recentlySentMessagesTimes.Count >= 5)
			{
				GameTexts.SetVariable("SECONDS", (15f - (Time.ApplicationTime - this._recentlySentMessagesTimes[0])).ToString("0.0"));
				this.AddChatLine(new MPChatLineVM(new TextObject("{=76VR5o8h}You must wait {SECONDS} seconds before sending another message.", null).ToString(), this.GetChannelColor(ChatChannelType.System), "Default"));
				return;
			}
			this._recentlySentMessagesTimes.Add(Time.ApplicationTime);
			this.SendMessageToChannel(this.ActiveChannelType, textToSend);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000153D4 File Offset: 0x000135D4
		private void HandleAddChatLineRequest(InformationMessage informationMessage)
		{
			string information = informationMessage.Information;
			string category = string.IsNullOrEmpty(informationMessage.Category) ? "Default" : informationMessage.Category;
			Color color = informationMessage.Color;
			MPChatLineVM item = new MPChatLineVM(information, color, category);
			this._requestedMessages.Enqueue(item);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00015420 File Offset: 0x00013620
		public void SendMessageToChannel(ChatChannelType channel, string message)
		{
			LobbyClient gameClient = NetworkMain.GameClient;
			if (gameClient != null && gameClient.Connected)
			{
				switch (channel)
				{
				case ChatChannelType.All:
					this._chatBox.SendMessageToAll(message);
					return;
				case ChatChannelType.Team:
					this._chatBox.SendMessageToTeam(message);
					return;
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00015474 File Offset: 0x00013674
		public void SendMessageToLobbyChannel(string message)
		{
			LobbyClient gameClient = NetworkMain.GameClient;
			if (gameClient != null && gameClient.Connected)
			{
				NetworkMain.GameClient.SendChannelMessage(this._currentCustomChatChannel.RoomId, message);
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000154A0 File Offset: 0x000136A0
		void IChatHandler.ReceiveChatMessage(ChatChannelType channel, string sender, string message)
		{
			TextObject textObject = TextObject.Empty;
			if (channel == ChatChannelType.Private)
			{
				textObject = new TextObject("{=6syoutpV}From {WHISPER_TARGET}", null);
				textObject.SetTextVariable("WHISPER_TARGET", sender);
			}
			this.AddMessage(message, sender, channel, textObject);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000154DC File Offset: 0x000136DC
		private void AddMessage(string msg, string author, ChatChannelType type, TextObject customChannelName = null)
		{
			Color channelColor = this.GetChannelColor(type);
			string text = (!TextObject.IsNullOrEmpty(customChannelName)) ? customChannelName.ToString() : type.ToString();
			MPChatLineVM chatLine = new MPChatLineVM(string.Concat(new string[]
			{
				"(",
				text,
				") ",
				author,
				": ",
				msg
			}), channelColor, "Social");
			this.AddChatLine(chatLine);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00015554 File Offset: 0x00013754
		private void AddChatLine(MPChatLineVM chatLine)
		{
			if (NativeConfig.DisableGuiMessages || chatLine == null)
			{
				return;
			}
			this._allMessages.Add(chatLine);
			if (this._allMessages.Count > this._maxHistoryCount * 5)
			{
				this._allMessages.RemoveAt(0);
			}
			if (this.IsMessageIncluded(chatLine))
			{
				this.MessageHistory.Add(chatLine);
				if (this.MessageHistory.Count > this._maxHistoryCount)
				{
					this.MessageHistory.RemoveAt(0);
				}
			}
			this.RefreshVisibility();
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000155D4 File Offset: 0x000137D4
		public void CheckChatFading(float dt)
		{
			foreach (MPChatLineVM mpchatLineVM in this._allMessages)
			{
				mpchatLineVM.HandleFading(dt);
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00015628 File Offset: 0x00013828
		[Conditional("DEBUG")]
		private void CheckFadingOutOrder()
		{
			for (int i = 0; i < this._allMessages.Count - 1; i++)
			{
				MPChatLineVM mpchatLineVM = this._allMessages[i];
				MPChatLineVM mpchatLineVM2 = this._allMessages[i + 1];
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001566C File Offset: 0x0001386C
		private void ChatHistoryFilterToggled()
		{
			this.MessageHistory.Clear();
			int num = 0;
			while (num < this._allMessages.Count && this.MessageHistory.Count < this._maxHistoryCount)
			{
				MPChatLineVM mpchatLineVM = this._allMessages[num];
				if (this.IsMessageIncluded(mpchatLineVM))
				{
					this.MessageHistory.Add(mpchatLineVM);
				}
				num++;
			}
			this.RefreshVisibility();
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000156D5 File Offset: 0x000138D5
		private bool IsMessageIncluded(MPChatLineVM chatLine)
		{
			if (chatLine.Category == "Combat")
			{
				return this.IncludeCombatLog;
			}
			return !(chatLine.Category == "Bark") || this.IncludeBark;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001570A File Offset: 0x0001390A
		public void SetGetKeyTextFromKeyIDFunc(Func<TextObject> getToggleChatKeyText)
		{
			this._getToggleChatKeyText = getToggleChatKeyText;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00015713 File Offset: 0x00013913
		public void SetGetCycleChannelKeyTextFunc(Func<TextObject> getCycleChannelsKeyText)
		{
			this._getCycleChannelsKeyText = getCycleChannelsKeyText;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001571C File Offset: 0x0001391C
		public void SetGetSendMessageKeyTextFunc(Func<TextObject> getSendMessageKeyText)
		{
			this._getSendMessageKeyText = getSendMessageKeyText;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00015725 File Offset: 0x00013925
		public void SetGetCancelSendingKeyTextFunc(Func<TextObject> getCancelSendingKeyText)
		{
			this._getCancelSendingKeyText = getCancelSendingKeyText;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00015730 File Offset: 0x00013930
		private void OnPlayerMessageReceived(NetworkCommunicator player, string message, bool toTeamOnly)
		{
			MissionPeer component = player.GetComponent<MissionPeer>();
			string text = ((component != null) ? component.DisplayedName : null) ?? player.UserName;
			if (component != null && !component.IsAgentAliveForChatting)
			{
				GameTexts.SetVariable("PLAYER_NAME", text);
				text = GameTexts.FindText("str_chat_message_dead_player", null).ToString();
			}
			this.AddMessage(message, text, toTeamOnly ? ChatChannelType.Team : ChatChannelType.All, null);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00015798 File Offset: 0x00013998
		private void OnWhisperMessageReceived(string fromUserName, string message)
		{
			this.AddMessage(message, fromUserName, ChatChannelType.Private, null);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000157A4 File Offset: 0x000139A4
		private void OnErrorWhisperMessageReceived(string toUserName)
		{
			TextObject textObject = new TextObject("{=61isYVW0}Player {USER_NAME} is not found", null);
			textObject.SetTextVariable("USER_NAME", toUserName);
			MPChatLineVM chatLine = new MPChatLineVM(textObject.ToString(), Color.White, "Social");
			this.AddChatLine(chatLine);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000157E5 File Offset: 0x000139E5
		private void OnWhisperMessageSent(string message, string whisperTarget)
		{
			this.AddMessage(message, whisperTarget, ChatChannelType.Private, null);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000157F4 File Offset: 0x000139F4
		private void OnServerMessage(string message)
		{
			MPChatLineVM chatLine = new MPChatLineVM(message, Color.White, "Social");
			this.AddChatLine(chatLine);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001581C File Offset: 0x00013A1C
		private void OnServerAdminMessage(string message)
		{
			MPChatLineVM chatLine = new MPChatLineVM("[Admin]: " + message, Color.ConvertStringToColor("#CC0099FF"), "Social");
			this.AddChatLine(chatLine);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00015850 File Offset: 0x00013A50
		private Color GetChannelColor(ChatChannelType type)
		{
			string color;
			switch (type)
			{
			case ChatChannelType.Private:
				color = "#8C1ABDFF";
				break;
			case ChatChannelType.All:
				color = "#EC943EFF";
				break;
			case ChatChannelType.Team:
				color = "#05C5F7FF";
				break;
			case ChatChannelType.Party:
				color = "#05C587FF";
				break;
			case ChatChannelType.System:
				color = "#FF0000FF";
				break;
			case ChatChannelType.Custom:
				color = "#FF0000FF";
				break;
			default:
				color = "#FFFFFFFF";
				break;
			}
			return Color.ConvertStringToColor(color);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000158B9 File Offset: 0x00013AB9
		public bool IsChatAllowedByOptions()
		{
			if (GameNetwork.IsMultiplayer)
			{
				return BannerlordConfig.EnableMultiplayerChatBox;
			}
			return BannerlordConfig.EnableSingleplayerChatBox && (Mission.Current == null || !BannerlordConfig.HideBattleUI);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000158E2 File Offset: 0x00013AE2
		public void TypeToChannelAll(bool startTyping = false)
		{
			this.ActiveChannelType = ChatChannelType.All;
			if (startTyping)
			{
				this.StartTyping();
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000158F4 File Offset: 0x00013AF4
		public void TypeToChannelTeam(bool startTyping = false)
		{
			this.ActiveChannelType = ChatChannelType.Team;
			if (startTyping)
			{
				this.StartTyping();
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00015906 File Offset: 0x00013B06
		public void StartInspectingMessages()
		{
			this.IsInspectingMessages = true;
			this.IsTypingText = false;
			this.WrittenText = "";
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00015921 File Offset: 0x00013B21
		public void StopInspectingMessages()
		{
			this.IsInspectingMessages = false;
			this.IsTypingText = false;
			this.WrittenText = "";
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001593C File Offset: 0x00013B3C
		public void StartTyping()
		{
			this.IsTypingText = true;
			this.IsInspectingMessages = true;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001594C File Offset: 0x00013B4C
		public void StopTyping(bool resetWrittenText = false)
		{
			this.IsTypingText = false;
			this.IsInspectingMessages = false;
			if (resetWrittenText)
			{
				this.WrittenText = "";
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001596A File Offset: 0x00013B6A
		public void SendCurrentlyTypedMessage()
		{
			this.ExecuteSendMessage();
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00015974 File Offset: 0x00013B74
		private void RefreshVisibility()
		{
			foreach (MPChatLineVM mpchatLineVM in this._allMessages)
			{
				mpchatLineVM.ToggleForceVisible(this.IsTypingText || this.IsInspectingMessages);
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000159D8 File Offset: 0x00013BD8
		public void ExecuteSaveSizes()
		{
			BannerlordConfig.ChatBoxSizeX = this.ChatBoxSizeX;
			BannerlordConfig.ChatBoxSizeY = this.ChatBoxSizeY;
			BannerlordConfig.Save();
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000159F6 File Offset: 0x00013BF6
		public void SetMessageHistoryCapacity(int capacity)
		{
			this._maxHistoryCount = capacity;
			MBBindingList<MPChatLineVM> messageHistory = this.MessageHistory;
			if (messageHistory == null)
			{
				return;
			}
			messageHistory.Clear();
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00015A0F File Offset: 0x00013C0F
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x00015A17 File Offset: 0x00013C17
		[DataSourceProperty]
		public float ChatBoxSizeX
		{
			get
			{
				return this._chatBoxSizeX;
			}
			set
			{
				if (value != this._chatBoxSizeX)
				{
					this._chatBoxSizeX = value;
					base.OnPropertyChangedWithValue(value, "ChatBoxSizeX");
				}
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00015A35 File Offset: 0x00013C35
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x00015A3D File Offset: 0x00013C3D
		[DataSourceProperty]
		public float ChatBoxSizeY
		{
			get
			{
				return this._chatBoxSizeY;
			}
			set
			{
				if (value != this._chatBoxSizeY)
				{
					this._chatBoxSizeY = value;
					base.OnPropertyChangedWithValue(value, "ChatBoxSizeY");
				}
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00015A5B File Offset: 0x00013C5B
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x00015A63 File Offset: 0x00013C63
		[DataSourceProperty]
		public int MaxMessageLength
		{
			get
			{
				return this._maxMessageLength;
			}
			set
			{
				if (value != this._maxMessageLength)
				{
					this._maxMessageLength = value;
					base.OnPropertyChangedWithValue(value, "MaxMessageLength");
				}
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00015A81 File Offset: 0x00013C81
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x00015A89 File Offset: 0x00013C89
		[DataSourceProperty]
		public bool IsTypingText
		{
			get
			{
				return this._isTypingText;
			}
			set
			{
				if (value != this._isTypingText)
				{
					this._isTypingText = value;
					base.OnPropertyChangedWithValue(value, "IsTypingText");
					this.RefreshVisibility();
				}
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00015AAD File Offset: 0x00013CAD
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x00015AB5 File Offset: 0x00013CB5
		[DataSourceProperty]
		public bool IsInspectingMessages
		{
			get
			{
				return this._isInspectingMessages;
			}
			set
			{
				if (value != this._isInspectingMessages)
				{
					this._isInspectingMessages = value;
					this.UpdateHideShowText(this._isInspectingMessages);
					this.UpdateShortcutTexts();
					base.OnPropertyChangedWithValue(value, "IsInspectingMessages");
					this.RefreshVisibility();
				}
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00015AEB File Offset: 0x00013CEB
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x00015AF3 File Offset: 0x00013CF3
		[DataSourceProperty]
		public bool IsChatDisabled
		{
			get
			{
				return this._isChatDisabled;
			}
			set
			{
				if (value != this._isChatDisabled)
				{
					this._isChatDisabled = value;
					base.OnPropertyChangedWithValue(value, "IsChatDisabled");
				}
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00015B11 File Offset: 0x00013D11
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x00015B19 File Offset: 0x00013D19
		[DataSourceProperty]
		public bool ShowHideShowHint
		{
			get
			{
				return this._showHideShowHint;
			}
			set
			{
				if (value != this._showHideShowHint)
				{
					this._showHideShowHint = value;
					base.OnPropertyChangedWithValue(value, "ShowHideShowHint");
				}
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00015B37 File Offset: 0x00013D37
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x00015B3F File Offset: 0x00013D3F
		[DataSourceProperty]
		public bool IsOptionsAvailable
		{
			get
			{
				return this._isOptionsAvailable;
			}
			set
			{
				if (value != this._isOptionsAvailable)
				{
					this._isOptionsAvailable = value;
					base.OnPropertyChangedWithValue(value, "IsOptionsAvailable");
				}
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00015B5D File Offset: 0x00013D5D
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x00015B65 File Offset: 0x00013D65
		[DataSourceProperty]
		public string WrittenText
		{
			get
			{
				return this._writtenText;
			}
			set
			{
				if (value != this._writtenText)
				{
					this._writtenText = value;
					base.OnPropertyChangedWithValue<string>(value, "WrittenText");
				}
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00015B88 File Offset: 0x00013D88
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x00015B90 File Offset: 0x00013D90
		[DataSourceProperty]
		public Color ActiveChannelColor
		{
			get
			{
				return this._activeChannelColor;
			}
			set
			{
				if (value != this._activeChannelColor)
				{
					this._activeChannelColor = value;
					base.OnPropertyChangedWithValue(value, "ActiveChannelColor");
				}
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00015BB3 File Offset: 0x00013DB3
		// (set) Token: 0x060004D6 RID: 1238 RVA: 0x00015BBB File Offset: 0x00013DBB
		[DataSourceProperty]
		public string ActiveChannelNameText
		{
			get
			{
				return this._activeChannelNameText;
			}
			set
			{
				if (value != this._activeChannelNameText)
				{
					this._activeChannelNameText = value;
					base.OnPropertyChangedWithValue<string>(value, "ActiveChannelNameText");
				}
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00015BDE File Offset: 0x00013DDE
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x00015BE6 File Offset: 0x00013DE6
		[DataSourceProperty]
		public string HideShowText
		{
			get
			{
				return this._hideShowText;
			}
			set
			{
				if (value != this._hideShowText)
				{
					this._hideShowText = value;
					base.OnPropertyChangedWithValue<string>(value, "HideShowText");
				}
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00015C09 File Offset: 0x00013E09
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x00015C11 File Offset: 0x00013E11
		[DataSourceProperty]
		public string ToggleCombatLogText
		{
			get
			{
				return this._toggleCombatLogText;
			}
			set
			{
				if (value != this._toggleCombatLogText)
				{
					this._toggleCombatLogText = value;
					base.OnPropertyChangedWithValue<string>(value, "ToggleCombatLogText");
				}
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00015C34 File Offset: 0x00013E34
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x00015C3C File Offset: 0x00013E3C
		[DataSourceProperty]
		public string ToggleBarkText
		{
			get
			{
				return this._toggleBarkText;
			}
			set
			{
				if (value != this._toggleBarkText)
				{
					this._toggleBarkText = value;
					base.OnPropertyChangedWithValue<string>(value, "ToggleBarkText");
				}
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00015C5F File Offset: 0x00013E5F
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x00015C67 File Offset: 0x00013E67
		[DataSourceProperty]
		public string CycleThroughChannelsText
		{
			get
			{
				return this._cycleThroughChannelsText;
			}
			set
			{
				if (value != this._cycleThroughChannelsText)
				{
					this._cycleThroughChannelsText = value;
					base.OnPropertyChangedWithValue<string>(value, "CycleThroughChannelsText");
				}
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00015C8A File Offset: 0x00013E8A
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x00015C92 File Offset: 0x00013E92
		[DataSourceProperty]
		public string SendMessageText
		{
			get
			{
				return this._sendMessageText;
			}
			set
			{
				if (value != this._sendMessageText)
				{
					this._sendMessageText = value;
					base.OnPropertyChangedWithValue<string>(value, "SendMessageText");
				}
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00015CB5 File Offset: 0x00013EB5
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x00015CBD File Offset: 0x00013EBD
		[DataSourceProperty]
		public string CancelSendingText
		{
			get
			{
				return this._cancelSendingText;
			}
			set
			{
				if (value != this._cancelSendingText)
				{
					this._cancelSendingText = value;
					base.OnPropertyChangedWithValue<string>(value, "CancelSendingText");
				}
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00015CE0 File Offset: 0x00013EE0
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x00015CE8 File Offset: 0x00013EE8
		[DataSourceProperty]
		public MBBindingList<MPChatLineVM> MessageHistory
		{
			get
			{
				return this._messageHistory;
			}
			set
			{
				if (value != this._messageHistory)
				{
					this._messageHistory = value;
					base.OnPropertyChangedWithValue<MBBindingList<MPChatLineVM>>(value, "MessageHistory");
				}
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00015D06 File Offset: 0x00013F06
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x00015D0E File Offset: 0x00013F0E
		[DataSourceProperty]
		public HintViewModel CombatLogHint
		{
			get
			{
				return this._combatLogHint;
			}
			set
			{
				if (value != this._combatLogHint)
				{
					this._combatLogHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "CombatLogHint");
				}
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00015D2C File Offset: 0x00013F2C
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x00015D34 File Offset: 0x00013F34
		[DataSourceProperty]
		public bool IncludeCombatLog
		{
			get
			{
				return this._includeCombatLog;
			}
			set
			{
				if (value != this._includeCombatLog)
				{
					this._includeCombatLog = value;
					base.OnPropertyChangedWithValue(value, "IncludeCombatLog");
					this.ChatHistoryFilterToggled();
					BannerlordConfig.ReportDamage = value;
				}
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00015D5E File Offset: 0x00013F5E
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x00015D66 File Offset: 0x00013F66
		[DataSourceProperty]
		public bool IncludeBark
		{
			get
			{
				return this._includeBark;
			}
			set
			{
				if (value != this._includeBark)
				{
					this._includeBark = value;
					base.OnPropertyChangedWithValue(value, "IncludeBark");
					this.ChatHistoryFilterToggled();
					BannerlordConfig.ReportBark = value;
				}
			}
		}

		// Token: 0x04000239 RID: 569
		private readonly TextObject _hideText = new TextObject("{=ou5KJERr}Press '{KEY}' to hide", null);

		// Token: 0x0400023A RID: 570
		private readonly TextObject _cycleChannelsText = new TextObject("{=Dhb2N5JD}Press '{KEY}' to cycle through channels", null);

		// Token: 0x0400023B RID: 571
		private readonly TextObject _sendMessageTextObject = new TextObject("{=f64QfbTO}'{KEY}' to send", null);

		// Token: 0x0400023C RID: 572
		private readonly TextObject _cancelSendingTextObject = new TextObject("{=U1rHNqOk}'{KEY}' to cancel", null);

		// Token: 0x0400023D RID: 573
		public const string DefaultCategory = "Default";

		// Token: 0x0400023E RID: 574
		public const string CombatCategory = "Combat";

		// Token: 0x0400023F RID: 575
		public const string SocialCategory = "Social";

		// Token: 0x04000240 RID: 576
		public const string BarkCategory = "Bark";

		// Token: 0x04000241 RID: 577
		private int _maxHistoryCount = 100;

		// Token: 0x04000242 RID: 578
		private const int _spamDetectionInterval = 15;

		// Token: 0x04000243 RID: 579
		private const int _maxMessagesAllowedPerInterval = 5;

		// Token: 0x04000244 RID: 580
		private List<float> _recentlySentMessagesTimes;

		// Token: 0x04000245 RID: 581
		private readonly List<MPChatLineVM> _allMessages;

		// Token: 0x04000246 RID: 582
		private readonly Queue<MPChatLineVM> _requestedMessages;

		// Token: 0x04000247 RID: 583
		private Func<TextObject> _getToggleChatKeyText;

		// Token: 0x04000248 RID: 584
		private Func<TextObject> _getCycleChannelsKeyText;

		// Token: 0x04000249 RID: 585
		private Func<TextObject> _getSendMessageKeyText;

		// Token: 0x0400024A RID: 586
		private Func<TextObject> _getCancelSendingKeyText;

		// Token: 0x0400024B RID: 587
		private ChatBox _chatBox;

		// Token: 0x0400024C RID: 588
		private Game _game;

		// Token: 0x0400024D RID: 589
		private Mission _mission;

		// Token: 0x0400024E RID: 590
		private ChatRoomInformationForClient _currentCustomChatChannel;

		// Token: 0x0400024F RID: 591
		private ChatChannelType _activeChannelType = ChatChannelType.NaN;

		// Token: 0x04000250 RID: 592
		private float _chatBoxSizeX;

		// Token: 0x04000251 RID: 593
		private float _chatBoxSizeY;

		// Token: 0x04000252 RID: 594
		private int _maxMessageLength;

		// Token: 0x04000253 RID: 595
		private string _writtenText = "";

		// Token: 0x04000254 RID: 596
		private string _activeChannelNameText;

		// Token: 0x04000255 RID: 597
		private string _hideShowText;

		// Token: 0x04000256 RID: 598
		private string _toggleCombatLogText;

		// Token: 0x04000257 RID: 599
		private string _toggleBarkText;

		// Token: 0x04000258 RID: 600
		private string _cycleThroughChannelsText;

		// Token: 0x04000259 RID: 601
		private string _sendMessageText;

		// Token: 0x0400025A RID: 602
		private string _cancelSendingText;

		// Token: 0x0400025B RID: 603
		private MBBindingList<MPChatLineVM> _messageHistory;

		// Token: 0x0400025C RID: 604
		private bool _includeCombatLog;

		// Token: 0x0400025D RID: 605
		private bool _includeBark;

		// Token: 0x0400025E RID: 606
		private bool _isTypingText;

		// Token: 0x0400025F RID: 607
		private bool _isInspectingMessages;

		// Token: 0x04000260 RID: 608
		private bool _isChatDisabled;

		// Token: 0x04000261 RID: 609
		private bool _showHideShowHint;

		// Token: 0x04000262 RID: 610
		private bool _isOptionsAvailable;

		// Token: 0x04000263 RID: 611
		private HintViewModel _combatLogHint;

		// Token: 0x04000264 RID: 612
		private Color _activeChannelColor;
	}
}

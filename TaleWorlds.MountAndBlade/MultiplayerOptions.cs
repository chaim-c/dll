using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002FA RID: 762
	public class MultiplayerOptions
	{
		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06002988 RID: 10632 RVA: 0x0009F7FC File Offset: 0x0009D9FC
		public static MultiplayerOptions Instance
		{
			get
			{
				MultiplayerOptions result;
				if ((result = MultiplayerOptions._instance) == null)
				{
					result = (MultiplayerOptions._instance = new MultiplayerOptions());
				}
				return result;
			}
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x0009F814 File Offset: 0x0009DA14
		public MultiplayerOptions()
		{
			this._default = new MultiplayerOptions.MultiplayerOptionsContainer();
			this._current = new MultiplayerOptions.MultiplayerOptionsContainer();
			this._next = new MultiplayerOptions.MultiplayerOptionsContainer();
			for (MultiplayerOptions.OptionType optionType = MultiplayerOptions.OptionType.ServerName; optionType < MultiplayerOptions.OptionType.NumOfSlots; optionType++)
			{
				this._current.CreateOption(optionType);
				this._default.CreateOption(optionType);
			}
			MBReadOnlyList<MultiplayerGameTypeInfo> multiplayerGameTypes = Module.CurrentModule.GetMultiplayerGameTypes();
			if (multiplayerGameTypes.Count > 0)
			{
				MultiplayerGameTypeInfo multiplayerGameTypeInfo = multiplayerGameTypes[0];
				this._current.UpdateOptionValue(MultiplayerOptions.OptionType.GameType, multiplayerGameTypeInfo.GameType);
				this._current.UpdateOptionValue(MultiplayerOptions.OptionType.PremadeMatchGameMode, multiplayerGameTypes.First((MultiplayerGameTypeInfo info) => info.GameType == "Skirmish").GameType);
				this._current.UpdateOptionValue(MultiplayerOptions.OptionType.Map, multiplayerGameTypeInfo.Scenes.FirstOrDefault<string>());
			}
			this._current.UpdateOptionValue(MultiplayerOptions.OptionType.CultureTeam1, MBObjectManager.Instance.GetObjectTypeList<BasicCultureObject>()[0].StringId);
			this._current.UpdateOptionValue(MultiplayerOptions.OptionType.CultureTeam2, MBObjectManager.Instance.GetObjectTypeList<BasicCultureObject>()[2].StringId);
			this._current.UpdateOptionValue(MultiplayerOptions.OptionType.MaxNumberOfPlayers, 120);
			this._current.UpdateOptionValue(MultiplayerOptions.OptionType.MinNumberOfPlayersForMatchStart, 1);
			this._current.UpdateOptionValue(MultiplayerOptions.OptionType.WarmupTimeLimit, 5);
			this._current.UpdateOptionValue(MultiplayerOptions.OptionType.MapTimeLimit, 30);
			this._current.UpdateOptionValue(MultiplayerOptions.OptionType.RoundTimeLimit, 120);
			this._current.UpdateOptionValue(MultiplayerOptions.OptionType.RoundPreparationTimeLimit, 10);
			this._current.UpdateOptionValue(MultiplayerOptions.OptionType.RoundTotal, 1);
			this._current.UpdateOptionValue(MultiplayerOptions.OptionType.RespawnPeriodTeam1, 3);
			this._current.UpdateOptionValue(MultiplayerOptions.OptionType.RespawnPeriodTeam2, 3);
			this._current.UpdateOptionValue(MultiplayerOptions.OptionType.MinScoreToWinMatch, 120000);
			this._current.UpdateOptionValue(MultiplayerOptions.OptionType.AutoTeamBalanceThreshold, 0);
			this._current.CopyAllValuesTo(this._next);
			this._current.CopyAllValuesTo(this._default);
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x0009F9F3 File Offset: 0x0009DBF3
		public static void Release()
		{
			MultiplayerOptions._instance = null;
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x0009F9FB File Offset: 0x0009DBFB
		public MultiplayerOptions.MultiplayerOption GetOptionFromOptionType(MultiplayerOptions.OptionType optionType, MultiplayerOptions.MultiplayerOptionsAccessMode mode = MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)
		{
			return this.GetContainer(mode).GetOptionFromOptionType(optionType);
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x0009FA0C File Offset: 0x0009DC0C
		public void OnGameTypeChanged(MultiplayerOptions.MultiplayerOptionsAccessMode mode = MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)
		{
			string text = "";
			if (this.CurrentOptionsCategory == MultiplayerOptions.OptionsCategory.Default)
			{
				text = MultiplayerOptions.OptionType.GameType.GetStrValue(mode);
			}
			else if (this.CurrentOptionsCategory == MultiplayerOptions.OptionsCategory.PremadeMatch)
			{
				text = MultiplayerOptions.OptionType.PremadeMatchGameMode.GetStrValue(mode);
			}
			MultiplayerOptions.OptionType.DisableInactivityKick.SetValue(false, MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 2173941516U)
			{
				if (num != 650645549U)
				{
					if (num != 1201370427U)
					{
						if (num == 2173941516U)
						{
							if (text == "Siege")
							{
								this.InitializeForSiege(mode);
							}
						}
					}
					else if (text == "Duel")
					{
						this.InitializeForDuel(mode);
					}
				}
				else if (text == "Skirmish")
				{
					this.InitializeForSkirmish(mode);
				}
			}
			else if (num <= 2508183895U)
			{
				if (num != 2298111883U)
				{
					if (num == 2508183895U)
					{
						if (text == "Battle")
						{
							this.InitializeForBattle(mode);
						}
					}
				}
				else if (text == "Captain")
				{
					this.InitializeForCaptain(mode);
				}
			}
			else if (num != 2569833005U)
			{
				if (num == 4010700071U)
				{
					if (text == "TeamDeathmatch")
					{
						this.InitializeForTeamDeathmatch(mode);
					}
				}
			}
			else if (text == "FreeForAll")
			{
				this.InitializeForFreeForAll(mode);
			}
			MBList<string> mapList = this.GetMapList();
			if (mapList.Count > 0)
			{
				MultiplayerOptions.OptionType.Map.SetValue(mapList[0], MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
			}
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x0009FB79 File Offset: 0x0009DD79
		public void InitializeNextAndDefaultOptionContainers()
		{
			this._current.CopyAllValuesTo(this._next);
			this._current.CopyAllValuesTo(this._default);
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x0009FBA0 File Offset: 0x0009DDA0
		private void InitializeForFreeForAll(MultiplayerOptions.MultiplayerOptionsAccessMode mode)
		{
			string gameModeID = "FreeForAll";
			MultiplayerOptions.OptionType.MaxNumberOfPlayers.SetValue(this.GetNumberOfPlayersForGameMode(gameModeID), mode);
			MultiplayerOptions.OptionType.NumberOfBotsPerFormation.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageMeleeSelfPercent.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageMeleeFriendPercent.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageRangedSelfPercent.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageRangedFriendPercent.SetValue(0, mode);
			MultiplayerOptions.OptionType.SpectatorCamera.SetValue(0, mode);
			MultiplayerOptions.OptionType.MapTimeLimit.SetValue(this.GetRoundTimeLimitInMinutesForGameMode(gameModeID), mode);
			MultiplayerOptions.OptionType.RespawnPeriodTeam1.SetValue(3, mode);
			MultiplayerOptions.OptionType.RespawnPeriodTeam2.SetValue(3, mode);
			MultiplayerOptions.OptionType.GoldGainChangePercentageTeam1.SetValue(0, mode);
			MultiplayerOptions.OptionType.GoldGainChangePercentageTeam2.SetValue(0, mode);
			MultiplayerOptions.OptionType.MinScoreToWinMatch.SetValue(120000, mode);
			MultiplayerOptions.OptionType.AutoTeamBalanceThreshold.SetValue(0, mode);
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x0009FC44 File Offset: 0x0009DE44
		private void InitializeForTeamDeathmatch(MultiplayerOptions.MultiplayerOptionsAccessMode mode)
		{
			string gameModeID = "TeamDeathmatch";
			MultiplayerOptions.OptionType.MaxNumberOfPlayers.SetValue(this.GetNumberOfPlayersForGameMode(gameModeID), mode);
			MultiplayerOptions.OptionType.NumberOfBotsPerFormation.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageMeleeSelfPercent.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageMeleeFriendPercent.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageRangedSelfPercent.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageRangedFriendPercent.SetValue(0, mode);
			MultiplayerOptions.OptionType.SpectatorCamera.SetValue(0, mode);
			MultiplayerOptions.OptionType.MapTimeLimit.SetValue(this.GetRoundTimeLimitInMinutesForGameMode(gameModeID), mode);
			MultiplayerOptions.OptionType.RespawnPeriodTeam1.SetValue(3, mode);
			MultiplayerOptions.OptionType.RespawnPeriodTeam2.SetValue(3, mode);
			MultiplayerOptions.OptionType.GoldGainChangePercentageTeam1.SetValue(0, mode);
			MultiplayerOptions.OptionType.GoldGainChangePercentageTeam2.SetValue(0, mode);
			MultiplayerOptions.OptionType.MinScoreToWinMatch.SetValue(120000, mode);
			MultiplayerOptions.OptionType.AutoTeamBalanceThreshold.SetValue(2, mode);
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x0009FCE8 File Offset: 0x0009DEE8
		private void InitializeForDuel(MultiplayerOptions.MultiplayerOptionsAccessMode mode)
		{
			string gameModeID = "Duel";
			MultiplayerOptions.OptionType.MaxNumberOfPlayers.SetValue(this.GetNumberOfPlayersForGameMode(gameModeID), mode);
			MultiplayerOptions.OptionType.NumberOfBotsPerFormation.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageMeleeSelfPercent.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageMeleeFriendPercent.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageRangedSelfPercent.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageRangedFriendPercent.SetValue(0, mode);
			MultiplayerOptions.OptionType.SpectatorCamera.SetValue(0, mode);
			MultiplayerOptions.OptionType.MapTimeLimit.SetValue(MultiplayerOptions.OptionType.MapTimeLimit.GetMaximumValue(), mode);
			MultiplayerOptions.OptionType.RespawnPeriodTeam1.SetValue(3, mode);
			MultiplayerOptions.OptionType.RespawnPeriodTeam2.SetValue(3, mode);
			MultiplayerOptions.OptionType.GoldGainChangePercentageTeam1.SetValue(0, mode);
			MultiplayerOptions.OptionType.GoldGainChangePercentageTeam2.SetValue(0, mode);
			MultiplayerOptions.OptionType.AutoTeamBalanceThreshold.SetValue(0, mode);
			MultiplayerOptions.OptionType.MinScoreToWinDuel.SetValue(3, mode);
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x0009FD88 File Offset: 0x0009DF88
		private void InitializeForSiege(MultiplayerOptions.MultiplayerOptionsAccessMode mode)
		{
			string gameModeID = "Siege";
			MultiplayerOptions.OptionType.MaxNumberOfPlayers.SetValue(this.GetNumberOfPlayersForGameMode(gameModeID), mode);
			MultiplayerOptions.OptionType.NumberOfBotsPerFormation.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageMeleeSelfPercent.SetValue(50, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageMeleeFriendPercent.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageRangedSelfPercent.SetValue(50, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageRangedFriendPercent.SetValue(0, mode);
			MultiplayerOptions.OptionType.SpectatorCamera.SetValue(0, mode);
			MultiplayerOptions.OptionType.WarmupTimeLimit.SetValue(3, mode);
			MultiplayerOptions.OptionType.MapTimeLimit.SetValue(this.GetRoundTimeLimitInMinutesForGameMode(gameModeID), mode);
			MultiplayerOptions.OptionType.RespawnPeriodTeam1.SetValue(3, mode);
			MultiplayerOptions.OptionType.RespawnPeriodTeam2.SetValue(12, mode);
			MultiplayerOptions.OptionType.GoldGainChangePercentageTeam1.SetValue(30, mode);
			MultiplayerOptions.OptionType.GoldGainChangePercentageTeam2.SetValue(0, mode);
			MultiplayerOptions.OptionType.AutoTeamBalanceThreshold.SetValue(2, mode);
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x0009FE2C File Offset: 0x0009E02C
		private void InitializeForCaptain(MultiplayerOptions.MultiplayerOptionsAccessMode mode)
		{
			string gameModeID = "Captain";
			MultiplayerOptions.OptionType.MaxNumberOfPlayers.SetValue(this.GetNumberOfPlayersForGameMode(gameModeID), mode);
			MultiplayerOptions.OptionType.NumberOfBotsPerFormation.SetValue(25, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageMeleeSelfPercent.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageMeleeFriendPercent.SetValue(50, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageRangedSelfPercent.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageRangedFriendPercent.SetValue(50, mode);
			MultiplayerOptions.OptionType.SpectatorCamera.SetValue(6, mode);
			MultiplayerOptions.OptionType.WarmupTimeLimit.SetValue(5, mode);
			MultiplayerOptions.OptionType.MapTimeLimit.SetValue(5, mode);
			MultiplayerOptions.OptionType.RoundTimeLimit.SetValue(this.GetRoundTimeLimitInMinutesForGameMode(gameModeID) * 60, mode);
			MultiplayerOptions.OptionType.RoundPreparationTimeLimit.SetValue(20, mode);
			MultiplayerOptions.OptionType.RoundTotal.SetValue(this.GetRoundCountForGameMode(gameModeID), mode);
			MultiplayerOptions.OptionType.RespawnPeriodTeam1.SetValue(3, mode);
			MultiplayerOptions.OptionType.RespawnPeriodTeam2.SetValue(3, mode);
			MultiplayerOptions.OptionType.GoldGainChangePercentageTeam1.SetValue(0, mode);
			MultiplayerOptions.OptionType.GoldGainChangePercentageTeam2.SetValue(0, mode);
			MultiplayerOptions.OptionType.AutoTeamBalanceThreshold.SetValue(2, mode);
			MultiplayerOptions.OptionType.AllowPollsToKickPlayers.SetValue(true, mode);
			MultiplayerOptions.OptionType.SingleSpawn.SetValue(true, MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x0009FF04 File Offset: 0x0009E104
		private void InitializeForSkirmish(MultiplayerOptions.MultiplayerOptionsAccessMode mode)
		{
			string gameModeID = "Skirmish";
			MultiplayerOptions.OptionType.MaxNumberOfPlayers.SetValue(this.GetNumberOfPlayersForGameMode(gameModeID), mode);
			MultiplayerOptions.OptionType.NumberOfBotsPerFormation.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageMeleeSelfPercent.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageMeleeFriendPercent.SetValue(50, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageRangedSelfPercent.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageRangedFriendPercent.SetValue(50, mode);
			MultiplayerOptions.OptionType.SpectatorCamera.SetValue(6, mode);
			MultiplayerOptions.OptionType.WarmupTimeLimit.SetValue(5, mode);
			MultiplayerOptions.OptionType.MapTimeLimit.SetValue(5, mode);
			MultiplayerOptions.OptionType.RoundTimeLimit.SetValue(this.GetRoundTimeLimitInMinutesForGameMode(gameModeID) * 60, mode);
			MultiplayerOptions.OptionType.RoundPreparationTimeLimit.SetValue(20, mode);
			MultiplayerOptions.OptionType.RoundTotal.SetValue(this.GetRoundCountForGameMode(gameModeID), mode);
			MultiplayerOptions.OptionType.RespawnPeriodTeam1.SetValue(3, mode);
			MultiplayerOptions.OptionType.RespawnPeriodTeam2.SetValue(3, mode);
			MultiplayerOptions.OptionType.GoldGainChangePercentageTeam1.SetValue(0, mode);
			MultiplayerOptions.OptionType.GoldGainChangePercentageTeam2.SetValue(0, mode);
			MultiplayerOptions.OptionType.AutoTeamBalanceThreshold.SetValue(2, mode);
			MultiplayerOptions.OptionType.AllowPollsToKickPlayers.SetValue(true, mode);
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x0009FFD0 File Offset: 0x0009E1D0
		private void InitializeForBattle(MultiplayerOptions.MultiplayerOptionsAccessMode mode)
		{
			string gameModeID = "Battle";
			MultiplayerOptions.OptionType.MaxNumberOfPlayers.SetValue(this.GetNumberOfPlayersForGameMode(gameModeID), mode);
			MultiplayerOptions.OptionType.NumberOfBotsPerFormation.SetValue(0, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageMeleeSelfPercent.SetValue(25, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageMeleeFriendPercent.SetValue(50, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageRangedSelfPercent.SetValue(25, mode);
			MultiplayerOptions.OptionType.FriendlyFireDamageRangedFriendPercent.SetValue(50, mode);
			MultiplayerOptions.OptionType.SpectatorCamera.SetValue(6, mode);
			MultiplayerOptions.OptionType.WarmupTimeLimit.SetValue(5, mode);
			MultiplayerOptions.OptionType.MapTimeLimit.SetValue(90, mode);
			MultiplayerOptions.OptionType.RoundTimeLimit.SetValue(this.GetRoundTimeLimitInMinutesForGameMode(gameModeID) * 60, mode);
			MultiplayerOptions.OptionType.RoundPreparationTimeLimit.SetValue(20, mode);
			MultiplayerOptions.OptionType.RoundTotal.SetValue(this.GetRoundCountForGameMode(gameModeID), mode);
			MultiplayerOptions.OptionType.RespawnPeriodTeam1.SetValue(3, mode);
			MultiplayerOptions.OptionType.RespawnPeriodTeam2.SetValue(3, mode);
			MultiplayerOptions.OptionType.GoldGainChangePercentageTeam1.SetValue(0, mode);
			MultiplayerOptions.OptionType.GoldGainChangePercentageTeam2.SetValue(0, mode);
			MultiplayerOptions.OptionType.AutoTeamBalanceThreshold.SetValue(2, mode);
			MultiplayerOptions.OptionType.SingleSpawn.SetValue(true, MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x000A00A0 File Offset: 0x0009E2A0
		public int GetNumberOfPlayersForGameMode(string gameModeID)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(gameModeID);
			if (num <= 2173941516U)
			{
				if (num != 650645549U)
				{
					if (num != 1201370427U)
					{
						if (num != 2173941516U)
						{
							return 0;
						}
						if (!(gameModeID == "Siege"))
						{
							return 0;
						}
					}
					else
					{
						if (!(gameModeID == "Duel"))
						{
							return 0;
						}
						return 32;
					}
				}
				else
				{
					if (!(gameModeID == "Skirmish"))
					{
						return 0;
					}
					return 12;
				}
			}
			else if (num <= 2508183895U)
			{
				if (num != 2298111883U)
				{
					if (num != 2508183895U)
					{
						return 0;
					}
					if (!(gameModeID == "Battle"))
					{
						return 0;
					}
				}
				else
				{
					if (!(gameModeID == "Captain"))
					{
						return 0;
					}
					return 12;
				}
			}
			else if (num != 2569833005U)
			{
				if (num != 4010700071U)
				{
					return 0;
				}
				if (!(gameModeID == "TeamDeathmatch"))
				{
					return 0;
				}
			}
			else if (!(gameModeID == "FreeForAll"))
			{
				return 0;
			}
			return 120;
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x000A0184 File Offset: 0x0009E384
		public int GetRoundCountForGameMode(string gameModeID)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(gameModeID);
			if (num <= 2173941516U)
			{
				if (num != 650645549U)
				{
					if (num != 1201370427U)
					{
						if (num != 2173941516U)
						{
							return 0;
						}
						if (!(gameModeID == "Siege"))
						{
							return 0;
						}
					}
					else if (!(gameModeID == "Duel"))
					{
						return 0;
					}
				}
				else
				{
					if (!(gameModeID == "Skirmish"))
					{
						return 0;
					}
					return 5;
				}
			}
			else if (num <= 2508183895U)
			{
				if (num != 2298111883U)
				{
					if (num != 2508183895U)
					{
						return 0;
					}
					if (!(gameModeID == "Battle"))
					{
						return 0;
					}
					return 9;
				}
				else
				{
					if (!(gameModeID == "Captain"))
					{
						return 0;
					}
					return 5;
				}
			}
			else if (num != 2569833005U)
			{
				if (num != 4010700071U)
				{
					return 0;
				}
				if (!(gameModeID == "TeamDeathmatch"))
				{
					return 0;
				}
			}
			else if (!(gameModeID == "FreeForAll"))
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x000A0260 File Offset: 0x0009E460
		public int GetRoundTimeLimitInMinutesForGameMode(string gameModeID)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(gameModeID);
			if (num <= 2173941516U)
			{
				if (num != 650645549U)
				{
					if (num != 1201370427U)
					{
						if (num != 2173941516U)
						{
							return 0;
						}
						if (!(gameModeID == "Siege"))
						{
							return 0;
						}
					}
					else if (!(gameModeID == "Duel"))
					{
						return 0;
					}
				}
				else
				{
					if (!(gameModeID == "Skirmish"))
					{
						return 0;
					}
					return 7;
				}
			}
			else if (num <= 2508183895U)
			{
				if (num != 2298111883U)
				{
					if (num != 2508183895U)
					{
						return 0;
					}
					if (!(gameModeID == "Battle"))
					{
						return 0;
					}
					return 20;
				}
				else
				{
					if (!(gameModeID == "Captain"))
					{
						return 0;
					}
					return 10;
				}
			}
			else if (num != 2569833005U)
			{
				if (num != 4010700071U)
				{
					return 0;
				}
				if (!(gameModeID == "TeamDeathmatch"))
				{
					return 0;
				}
			}
			else if (!(gameModeID == "FreeForAll"))
			{
				return 0;
			}
			return 30;
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x000A0340 File Offset: 0x0009E540
		public void InitializeFromCommandList(List<string> arguments)
		{
			foreach (string command in arguments)
			{
				GameNetwork.HandleConsoleCommand(command);
			}
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x000A038C File Offset: 0x0009E58C
		public void ResetDefaultsToCurrent()
		{
			this._current.CopyAllValuesTo(this._default);
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x000A03A0 File Offset: 0x0009E5A0
		public List<string> GetMultiplayerOptionsTextList(MultiplayerOptions.OptionType optionType)
		{
			List<string> list = new List<string>();
			string str = new TextObject("{=vBkrw5VV}Random", null).ToString();
			string item = "-- " + str + " --";
			switch (optionType)
			{
			case MultiplayerOptions.OptionType.PremadeMatchGameMode:
				return (from q in Module.CurrentModule.GetMultiplayerGameTypes()
				where q.GameType == "Skirmish" || q.GameType == "Captain"
				select GameTexts.FindText("str_multiplayer_official_game_type_name", q.GameType).ToString()).ToList<string>();
			case MultiplayerOptions.OptionType.GameType:
				return (from q in Module.CurrentModule.GetMultiplayerGameTypes()
				select GameTexts.FindText("str_multiplayer_official_game_type_name", q.GameType).ToString()).ToList<string>();
			case MultiplayerOptions.OptionType.PremadeGameType:
				break;
			case MultiplayerOptions.OptionType.Map:
			{
				List<string> list2 = new List<string>();
				if (this.CurrentOptionsCategory == MultiplayerOptions.OptionsCategory.Default)
				{
					list2 = MultiplayerGameTypes.GetGameTypeInfo(MultiplayerOptions.OptionType.GameType.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)).Scenes.ToList<string>();
				}
				else if (this.CurrentOptionsCategory == MultiplayerOptions.OptionsCategory.PremadeMatch)
				{
					list2 = this.GetAvailableClanMatchScenes();
					list.Insert(0, item);
				}
				using (List<string>.Enumerator enumerator = list2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						TextObject textObject;
						string item2;
						if (GameTexts.TryGetText("str_multiplayer_scene_name", out textObject, text))
						{
							item2 = textObject.ToString();
						}
						else
						{
							item2 = text;
						}
						list.Add(item2);
					}
					return list;
				}
				break;
			}
			case MultiplayerOptions.OptionType.CultureTeam1:
			case MultiplayerOptions.OptionType.CultureTeam2:
				list = (from c in MBObjectManager.Instance.GetObjectTypeList<BasicCultureObject>()
				where c.IsMainCulture
				select c into x
				select MultiplayerOptions.GetLocalizedCultureNameFromStringID(x.StringId)).ToList<string>();
				if (this.CurrentOptionsCategory == MultiplayerOptions.OptionsCategory.PremadeMatch)
				{
					list.Insert(0, item);
					return list;
				}
				return list;
			default:
				if (optionType != MultiplayerOptions.OptionType.SpectatorCamera)
				{
					return this.GetMultiplayerOptionsList(optionType);
				}
				return new List<string>
				{
					GameTexts.FindText("str_multiplayer_spectator_camera_type", SpectatorCameraTypes.LockToAnyAgent.ToString()).ToString(),
					GameTexts.FindText("str_multiplayer_spectator_camera_type", SpectatorCameraTypes.LockToAnyPlayer.ToString()).ToString(),
					GameTexts.FindText("str_multiplayer_spectator_camera_type", SpectatorCameraTypes.LockToTeamMembers.ToString()).ToString(),
					GameTexts.FindText("str_multiplayer_spectator_camera_type", SpectatorCameraTypes.LockToTeamMembersView.ToString()).ToString()
				};
			}
			list = new List<string>
			{
				new TextObject("{=H5tiRTya}Practice", null).ToString(),
				new TextObject("{=YNkPy4ta}Clan Match", null).ToString()
			};
			return list;
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x000A0688 File Offset: 0x0009E888
		public List<string> GetMultiplayerOptionsList(MultiplayerOptions.OptionType optionType)
		{
			List<string> list = new List<string>();
			switch (optionType)
			{
			case MultiplayerOptions.OptionType.PremadeMatchGameMode:
				list = (from q in Module.CurrentModule.GetMultiplayerGameTypes()
				select q.GameType).ToList<string>();
				list.Remove("FreeForAll");
				list.Remove("TeamDeathmatch");
				list.Remove("Duel");
				list.Remove("Siege");
				break;
			case MultiplayerOptions.OptionType.GameType:
				list = (from q in Module.CurrentModule.GetMultiplayerGameTypes()
				select q.GameType).ToList<string>();
				list.Remove("FreeForAll");
				break;
			case MultiplayerOptions.OptionType.PremadeGameType:
				list = new List<string>
				{
					PremadeGameType.Practice.ToString(),
					PremadeGameType.Clan.ToString()
				};
				break;
			case MultiplayerOptions.OptionType.Map:
				if (this.CurrentOptionsCategory == MultiplayerOptions.OptionsCategory.Default)
				{
					list = MultiplayerGameTypes.GetGameTypeInfo(MultiplayerOptions.OptionType.GameType.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)).Scenes.ToList<string>();
				}
				else if (this.CurrentOptionsCategory == MultiplayerOptions.OptionsCategory.PremadeMatch)
				{
					MultiplayerOptions.OptionType.PremadeMatchGameMode.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
					list = this.GetAvailableClanMatchScenes();
					list.Insert(0, "RandomSelection");
				}
				break;
			case MultiplayerOptions.OptionType.CultureTeam1:
			case MultiplayerOptions.OptionType.CultureTeam2:
				list = (from c in MBObjectManager.Instance.GetObjectTypeList<BasicCultureObject>()
				where c.IsMainCulture
				select c into x
				select x.StringId).ToList<string>();
				if (this.CurrentOptionsCategory == MultiplayerOptions.OptionsCategory.PremadeMatch)
				{
					list.Insert(0, Parameters.RandomSelectionString);
				}
				break;
			default:
				if (optionType == MultiplayerOptions.OptionType.SpectatorCamera)
				{
					list = new List<string>
					{
						SpectatorCameraTypes.LockToAnyAgent.ToString(),
						SpectatorCameraTypes.LockToAnyPlayer.ToString(),
						SpectatorCameraTypes.LockToTeamMembers.ToString(),
						SpectatorCameraTypes.LockToTeamMembersView.ToString()
					};
				}
				break;
			}
			return list;
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x000A08C8 File Offset: 0x0009EAC8
		private List<string> GetAvailableClanMatchScenes()
		{
			string[] source = new string[0];
			string[] array;
			if (NetworkMain.GameClient.AvailableScenes.ScenesByGameTypes.TryGetValue(MultiplayerOptions.OptionType.PremadeMatchGameMode.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions), out array))
			{
				source = array;
			}
			return source.ToList<string>();
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x000A0904 File Offset: 0x0009EB04
		private MultiplayerOptions.MultiplayerOptionsContainer GetContainer(MultiplayerOptions.MultiplayerOptionsAccessMode mode = MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)
		{
			switch (mode)
			{
			case MultiplayerOptions.MultiplayerOptionsAccessMode.DefaultMapOptions:
				return this._default;
			case MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions:
				return this._current;
			case MultiplayerOptions.MultiplayerOptionsAccessMode.NextMapOptions:
				return this._next;
			default:
				return null;
			}
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x000A0930 File Offset: 0x0009EB30
		public void InitializeAllOptionsFromNext()
		{
			this._next.CopyAllValuesTo(this._current);
			this.UpdateMbMultiplayerData(this._current);
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x000A0950 File Offset: 0x0009EB50
		private void UpdateMbMultiplayerData(MultiplayerOptions.MultiplayerOptionsContainer container)
		{
			container.GetOptionFromOptionType(MultiplayerOptions.OptionType.ServerName).GetValue(out MBMultiplayerData.ServerName);
			if (this.CurrentOptionsCategory == MultiplayerOptions.OptionsCategory.Default)
			{
				container.GetOptionFromOptionType(MultiplayerOptions.OptionType.GameType).GetValue(out MBMultiplayerData.GameType);
			}
			else if (this.CurrentOptionsCategory == MultiplayerOptions.OptionsCategory.PremadeMatch)
			{
				container.GetOptionFromOptionType(MultiplayerOptions.OptionType.PremadeMatchGameMode).GetValue(out MBMultiplayerData.GameType);
			}
			container.GetOptionFromOptionType(MultiplayerOptions.OptionType.Map).GetValue(out MBMultiplayerData.Map);
			container.GetOptionFromOptionType(MultiplayerOptions.OptionType.MaxNumberOfPlayers).GetValue(out MBMultiplayerData.PlayerCountLimit);
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x000A09CC File Offset: 0x0009EBCC
		public MBList<string> GetMapList()
		{
			MultiplayerGameTypeInfo multiplayerGameTypeInfo = null;
			if (this.CurrentOptionsCategory == MultiplayerOptions.OptionsCategory.Default)
			{
				multiplayerGameTypeInfo = MultiplayerGameTypes.GetGameTypeInfo(MultiplayerOptions.OptionType.GameType.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
			}
			else if (this.CurrentOptionsCategory == MultiplayerOptions.OptionsCategory.PremadeMatch)
			{
				multiplayerGameTypeInfo = MultiplayerGameTypes.GetGameTypeInfo(MultiplayerOptions.OptionType.PremadeMatchGameMode.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
			}
			MBList<string> mblist = new MBList<string>();
			if (multiplayerGameTypeInfo.Scenes.Count > 0)
			{
				mblist.Add(multiplayerGameTypeInfo.Scenes[0]);
				MultiplayerOptions.OptionType.Map.SetValue(mblist[0], MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
			}
			return mblist;
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x000A0A40 File Offset: 0x0009EC40
		public string GetValueTextForOptionWithMultipleSelection(MultiplayerOptions.OptionType optionType)
		{
			MultiplayerOptionsProperty optionProperty = optionType.GetOptionProperty();
			MultiplayerOptions.OptionValueType optionValueType = optionProperty.OptionValueType;
			if (optionValueType == MultiplayerOptions.OptionValueType.Enum)
			{
				return Enum.ToObject(optionProperty.EnumType, optionType.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions)).ToString();
			}
			if (optionValueType != MultiplayerOptions.OptionValueType.String)
			{
				return null;
			}
			return optionType.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x000A0A88 File Offset: 0x0009EC88
		public void SetValueForOptionWithMultipleSelectionFromText(MultiplayerOptions.OptionType optionType, string value)
		{
			MultiplayerOptionsProperty optionProperty = optionType.GetOptionProperty();
			MultiplayerOptions.OptionValueType optionValueType = optionProperty.OptionValueType;
			if (optionValueType != MultiplayerOptions.OptionValueType.Enum)
			{
				if (optionValueType == MultiplayerOptions.OptionValueType.String)
				{
					optionType.SetValue(value, MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
				}
			}
			else
			{
				optionType.SetValue((int)Enum.Parse(optionProperty.EnumType, value), MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
			}
			if (optionType == MultiplayerOptions.OptionType.GameType || optionType == MultiplayerOptions.OptionType.PremadeMatchGameMode)
			{
				this.OnGameTypeChanged(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
			}
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x000A0AE0 File Offset: 0x0009ECE0
		private static string GetLocalizedCultureNameFromStringID(string cultureID)
		{
			if (cultureID == "sturgia")
			{
				return new TextObject("{=PjO7oY16}Sturgia", null).ToString();
			}
			if (cultureID == "vlandia")
			{
				return new TextObject("{=FjwRsf1C}Vlandia", null).ToString();
			}
			if (cultureID == "battania")
			{
				return new TextObject("{=0B27RrYJ}Battania", null).ToString();
			}
			if (cultureID == "empire")
			{
				return new TextObject("{=empirefaction}Empire", null).ToString();
			}
			if (cultureID == "khuzait")
			{
				return new TextObject("{=sZLd6VHi}Khuzait", null).ToString();
			}
			if (!(cultureID == "aserai"))
			{
				Debug.FailedAssert("Unidentified culture id: " + cultureID, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Network\\Gameplay\\MultiplayerOptions.cs", "GetLocalizedCultureNameFromStringID", 999);
				return "";
			}
			return new TextObject("{=aseraifaction}Aserai", null).ToString();
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x000A0BC8 File Offset: 0x0009EDC8
		public static bool TryGetOptionTypeFromString(string optionTypeString, out MultiplayerOptions.OptionType optionType, out MultiplayerOptionsProperty optionAttribute)
		{
			optionAttribute = null;
			for (optionType = MultiplayerOptions.OptionType.ServerName; optionType < MultiplayerOptions.OptionType.NumOfSlots; optionType++)
			{
				MultiplayerOptionsProperty optionProperty = optionType.GetOptionProperty();
				if (optionProperty != null && optionType.ToString().Equals(optionTypeString))
				{
					optionAttribute = optionProperty;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400100A RID: 4106
		private const int PlayerCountLimitMin = 1;

		// Token: 0x0400100B RID: 4107
		private const int PlayerCountLimitMax = 1023;

		// Token: 0x0400100C RID: 4108
		private const int PlayerCountLimitForMatchStartMin = 0;

		// Token: 0x0400100D RID: 4109
		private const int PlayerCountLimitForMatchStartMax = 20;

		// Token: 0x0400100E RID: 4110
		private const int MapTimeLimitMin = 1;

		// Token: 0x0400100F RID: 4111
		private const int MapTimeLimitMax = 60;

		// Token: 0x04001010 RID: 4112
		private const int RoundLimitMin = 1;

		// Token: 0x04001011 RID: 4113
		private const int RoundLimitMax = 99;

		// Token: 0x04001012 RID: 4114
		private const int RoundTimeLimitMin = 60;

		// Token: 0x04001013 RID: 4115
		private const int RoundTimeLimitMax = 3600;

		// Token: 0x04001014 RID: 4116
		private const int RoundPreparationTimeLimitMin = 2;

		// Token: 0x04001015 RID: 4117
		private const int RoundPreparationTimeLimitMax = 60;

		// Token: 0x04001016 RID: 4118
		private const int RespawnPeriodMin = 1;

		// Token: 0x04001017 RID: 4119
		private const int RespawnPeriodMax = 60;

		// Token: 0x04001018 RID: 4120
		private const int GoldGainChangePercentageMin = -100;

		// Token: 0x04001019 RID: 4121
		private const int GoldGainChangePercentageMax = 100;

		// Token: 0x0400101A RID: 4122
		private const int PollAcceptThresholdMin = 0;

		// Token: 0x0400101B RID: 4123
		private const int PollAcceptThresholdMax = 10;

		// Token: 0x0400101C RID: 4124
		private const int BotsPerTeamLimitMin = 0;

		// Token: 0x0400101D RID: 4125
		private const int BotsPerTeamLimitMax = 510;

		// Token: 0x0400101E RID: 4126
		private const int BotsPerFormationLimitMin = 0;

		// Token: 0x0400101F RID: 4127
		private const int BotsPerFormationLimitMax = 100;

		// Token: 0x04001020 RID: 4128
		private const int FriendlyFireDamagePercentMin = 0;

		// Token: 0x04001021 RID: 4129
		private const int FriendlyFireDamagePercentMax = 100;

		// Token: 0x04001022 RID: 4130
		private const int GameDefinitionIdMin = -2147483648;

		// Token: 0x04001023 RID: 4131
		private const int GameDefinitionIdMax = 2147483647;

		// Token: 0x04001024 RID: 4132
		private const int MaxScoreToEndDuel = 7;

		// Token: 0x04001025 RID: 4133
		private static MultiplayerOptions _instance;

		// Token: 0x04001026 RID: 4134
		private readonly MultiplayerOptions.MultiplayerOptionsContainer _default;

		// Token: 0x04001027 RID: 4135
		private readonly MultiplayerOptions.MultiplayerOptionsContainer _current;

		// Token: 0x04001028 RID: 4136
		private readonly MultiplayerOptions.MultiplayerOptionsContainer _next;

		// Token: 0x04001029 RID: 4137
		public MultiplayerOptions.OptionsCategory CurrentOptionsCategory;

		// Token: 0x020005BC RID: 1468
		public enum MultiplayerOptionsAccessMode
		{
			// Token: 0x04001E2F RID: 7727
			DefaultMapOptions,
			// Token: 0x04001E30 RID: 7728
			CurrentMapOptions,
			// Token: 0x04001E31 RID: 7729
			NextMapOptions,
			// Token: 0x04001E32 RID: 7730
			NumAccessModes
		}

		// Token: 0x020005BD RID: 1469
		public enum OptionValueType
		{
			// Token: 0x04001E34 RID: 7732
			Bool,
			// Token: 0x04001E35 RID: 7733
			Integer,
			// Token: 0x04001E36 RID: 7734
			Enum,
			// Token: 0x04001E37 RID: 7735
			String
		}

		// Token: 0x020005BE RID: 1470
		public enum OptionType
		{
			// Token: 0x04001E39 RID: 7737
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.String, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Changes the name of the server in the server list", 0, 0, null, false, null)]
			ServerName,
			// Token: 0x04001E3A RID: 7738
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.String, MultiplayerOptionsProperty.ReplicationOccurrence.Immediately, "Welcome messages which is shown to all players when they enter the server.", 0, 0, null, false, null)]
			WelcomeMessage,
			// Token: 0x04001E3B RID: 7739
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.String, MultiplayerOptionsProperty.ReplicationOccurrence.Never, "Sets a password that clients have to enter before connecting to the server.", 0, 0, null, false, null)]
			GamePassword,
			// Token: 0x04001E3C RID: 7740
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.String, MultiplayerOptionsProperty.ReplicationOccurrence.Never, "Sets a password that allows players access to admin tools during the game.", 0, 0, null, false, null)]
			AdminPassword,
			// Token: 0x04001E3D RID: 7741
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.Never, "Sets ID of the private game definition.", -2147483648, 2147483647, null, false, null)]
			GameDefinitionId,
			// Token: 0x04001E3E RID: 7742
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Bool, MultiplayerOptionsProperty.ReplicationOccurrence.Immediately, "Allow players to start polls to kick other players.", 0, 0, null, false, null)]
			AllowPollsToKickPlayers,
			// Token: 0x04001E3F RID: 7743
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Bool, MultiplayerOptionsProperty.ReplicationOccurrence.Immediately, "Allow players to start polls to ban other players.", 0, 0, null, false, null)]
			AllowPollsToBanPlayers,
			// Token: 0x04001E40 RID: 7744
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Bool, MultiplayerOptionsProperty.ReplicationOccurrence.Immediately, "Allow players to start polls to change the current map.", 0, 0, null, false, null)]
			AllowPollsToChangeMaps,
			// Token: 0x04001E41 RID: 7745
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Bool, MultiplayerOptionsProperty.ReplicationOccurrence.Immediately, "Allow players to use their custom banner.", 0, 0, null, false, null)]
			AllowIndividualBanners,
			// Token: 0x04001E42 RID: 7746
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Bool, MultiplayerOptionsProperty.ReplicationOccurrence.Immediately, "Use animation progress dependent blocking.", 0, 0, null, false, null)]
			UseRealisticBlocking,
			// Token: 0x04001E43 RID: 7747
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.String, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Changes the game type.", 0, 0, null, true, null)]
			PremadeMatchGameMode,
			// Token: 0x04001E44 RID: 7748
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.String, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Changes the game type.", 0, 0, null, true, null)]
			GameType,
			// Token: 0x04001E45 RID: 7749
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Enum, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Type of the premade game.", 0, 1, null, true, typeof(PremadeGameType))]
			PremadeGameType,
			// Token: 0x04001E46 RID: 7750
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.String, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Map of the game.", 0, 0, null, true, null)]
			Map,
			// Token: 0x04001E47 RID: 7751
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.String, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Sets culture for team 1", 0, 0, null, true, null)]
			CultureTeam1,
			// Token: 0x04001E48 RID: 7752
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.String, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Sets culture for team 2", 0, 0, null, true, null)]
			CultureTeam2,
			// Token: 0x04001E49 RID: 7753
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Set the maximum amount of player allowed on the server.", 1, 1023, null, false, null)]
			MaxNumberOfPlayers,
			// Token: 0x04001E4A RID: 7754
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.Immediately, "Set the amount of players that are needed to start the first round. If not met, players will just wait.", 0, 20, null, false, null)]
			MinNumberOfPlayersForMatchStart,
			// Token: 0x04001E4B RID: 7755
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.Immediately, "Amount of bots on team 1", 0, 510, null, false, null)]
			NumberOfBotsTeam1,
			// Token: 0x04001E4C RID: 7756
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.Immediately, "Amount of bots on team 2", 0, 510, new string[]
			{
				"Battle",
				"NewBattle",
				"ClassicBattle",
				"Captain",
				"Skirmish",
				"Siege",
				"TeamDeathmatch"
			}, false, null)]
			NumberOfBotsTeam2,
			// Token: 0x04001E4D RID: 7757
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.Immediately, "Amount of bots per formation", 0, 100, new string[]
			{
				"Captain"
			}, false, null)]
			NumberOfBotsPerFormation,
			// Token: 0x04001E4E RID: 7758
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.Immediately, "A percentage of how much melee damage inflicted upon a friend is dealt back to the inflictor.", 0, 100, new string[]
			{
				"Battle",
				"NewBattle",
				"ClassicBattle",
				"Captain",
				"Skirmish",
				"Siege",
				"TeamDeathmatch"
			}, false, null)]
			FriendlyFireDamageMeleeSelfPercent,
			// Token: 0x04001E4F RID: 7759
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.Immediately, "A percentage of how much melee damage inflicted upon a friend is actually dealt.", 0, 100, new string[]
			{
				"Battle",
				"NewBattle",
				"ClassicBattle",
				"Captain",
				"Skirmish",
				"Siege",
				"TeamDeathmatch"
			}, false, null)]
			FriendlyFireDamageMeleeFriendPercent,
			// Token: 0x04001E50 RID: 7760
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.Immediately, "A percentage of how much ranged damage inflicted upon a friend is dealt back to the inflictor.", 0, 100, new string[]
			{
				"Battle",
				"NewBattle",
				"ClassicBattle",
				"Captain",
				"Skirmish",
				"Siege",
				"TeamDeathmatch"
			}, false, null)]
			FriendlyFireDamageRangedSelfPercent,
			// Token: 0x04001E51 RID: 7761
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.Immediately, "A percentage of how much ranged damage inflicted upon a friend is actually dealt.", 0, 100, new string[]
			{
				"Battle",
				"NewBattle",
				"ClassicBattle",
				"Captain",
				"Skirmish",
				"Siege",
				"TeamDeathmatch"
			}, false, null)]
			FriendlyFireDamageRangedFriendPercent,
			// Token: 0x04001E52 RID: 7762
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Enum, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Who can spectators look at, and how.", 0, 7, null, true, typeof(SpectatorCameraTypes))]
			SpectatorCamera,
			// Token: 0x04001E53 RID: 7763
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Maximum duration for the warmup. In minutes.", 1, 60, null, false, null)]
			WarmupTimeLimit,
			// Token: 0x04001E54 RID: 7764
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Maximum duration for the map. In minutes.", 1, 60, null, false, null)]
			MapTimeLimit,
			// Token: 0x04001E55 RID: 7765
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Maximum duration for each round. In seconds.", 60, 3600, new string[]
			{
				"Battle",
				"NewBattle",
				"ClassicBattle",
				"Captain",
				"Skirmish",
				"Siege"
			}, false, null)]
			RoundTimeLimit,
			// Token: 0x04001E56 RID: 7766
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Time available to select class/equipment. In seconds.", 2, 60, new string[]
			{
				"Battle",
				"NewBattle",
				"ClassicBattle",
				"Captain",
				"Skirmish",
				"Siege"
			}, false, null)]
			RoundPreparationTimeLimit,
			// Token: 0x04001E57 RID: 7767
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Maximum amount of rounds before the game ends.", 1, 99, new string[]
			{
				"Battle",
				"NewBattle",
				"ClassicBattle",
				"Captain",
				"Skirmish",
				"Siege"
			}, false, null)]
			RoundTotal,
			// Token: 0x04001E58 RID: 7768
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Wait time after death, before respawning again. In seconds.", 1, 60, new string[]
			{
				"Siege"
			}, false, null)]
			RespawnPeriodTeam1,
			// Token: 0x04001E59 RID: 7769
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Wait time after death, before respawning again. In seconds.", 1, 60, new string[]
			{
				"Siege"
			}, false, null)]
			RespawnPeriodTeam2,
			// Token: 0x04001E5A RID: 7770
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Bool, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Unlimited gold option.", 0, 0, new string[]
			{
				"Battle",
				"Skirmish",
				"Siege",
				"TeamDeathmatch"
			}, false, null)]
			UnlimitedGold,
			// Token: 0x04001E5B RID: 7771
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.Immediately, "Gold gain multiplier from agent deaths.", -100, 100, new string[]
			{
				"Siege",
				"TeamDeathmatch"
			}, false, null)]
			GoldGainChangePercentageTeam1,
			// Token: 0x04001E5C RID: 7772
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.Immediately, "Gold gain multiplier from agent deaths.", -100, 100, new string[]
			{
				"Siege",
				"TeamDeathmatch"
			}, false, null)]
			GoldGainChangePercentageTeam2,
			// Token: 0x04001E5D RID: 7773
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Min score to win match.", 0, 1023000, new string[]
			{
				"TeamDeathmatch"
			}, false, null)]
			MinScoreToWinMatch,
			// Token: 0x04001E5E RID: 7774
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Min score to win duel.", 0, 7, new string[]
			{
				"Duel"
			}, false, null)]
			MinScoreToWinDuel,
			// Token: 0x04001E5F RID: 7775
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.Immediately, "Minimum needed difference in poll results before it is accepted.", 0, 10, null, false, null)]
			PollAcceptThreshold,
			// Token: 0x04001E60 RID: 7776
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Integer, MultiplayerOptionsProperty.ReplicationOccurrence.Immediately, "Maximum player imbalance between team 1 and team 2. Selecting 0 will disable auto team balancing.", 0, 30, null, false, null)]
			AutoTeamBalanceThreshold,
			// Token: 0x04001E61 RID: 7777
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Bool, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Enables mission recording.", 0, 0, null, false, null)]
			EnableMissionRecording,
			// Token: 0x04001E62 RID: 7778
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Bool, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Sets if the game mode uses single spawning.", 0, 0, null, false, null)]
			SingleSpawn,
			// Token: 0x04001E63 RID: 7779
			[MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType.Bool, MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad, "Disables the inactivity kick timer.", 0, 0, null, false, null)]
			DisableInactivityKick,
			// Token: 0x04001E64 RID: 7780
			NumOfSlots
		}

		// Token: 0x020005BF RID: 1471
		public enum OptionsCategory
		{
			// Token: 0x04001E66 RID: 7782
			Default,
			// Token: 0x04001E67 RID: 7783
			PremadeMatch
		}

		// Token: 0x020005C0 RID: 1472
		public class MultiplayerOption
		{
			// Token: 0x06003B06 RID: 15110 RVA: 0x000E7C5A File Offset: 0x000E5E5A
			public static MultiplayerOptions.MultiplayerOption CreateMultiplayerOption(MultiplayerOptions.OptionType optionType)
			{
				return new MultiplayerOptions.MultiplayerOption(optionType);
			}

			// Token: 0x06003B07 RID: 15111 RVA: 0x000E7C62 File Offset: 0x000E5E62
			public static MultiplayerOptions.MultiplayerOption CopyMultiplayerOption(MultiplayerOptions.MultiplayerOption option)
			{
				return new MultiplayerOptions.MultiplayerOption(option.OptionType)
				{
					_intValue = option._intValue,
					_stringValue = option._stringValue
				};
			}

			// Token: 0x06003B08 RID: 15112 RVA: 0x000E7C88 File Offset: 0x000E5E88
			private MultiplayerOption(MultiplayerOptions.OptionType optionType)
			{
				this.OptionType = optionType;
				if (optionType.GetOptionProperty().OptionValueType == MultiplayerOptions.OptionValueType.String)
				{
					this._intValue = MultiplayerOptions.MultiplayerOption.IntegerValue.Invalid;
					this._stringValue = MultiplayerOptions.MultiplayerOption.StringValue.Create();
					return;
				}
				this._intValue = MultiplayerOptions.MultiplayerOption.IntegerValue.Create();
				this._stringValue = MultiplayerOptions.MultiplayerOption.StringValue.Invalid;
			}

			// Token: 0x06003B09 RID: 15113 RVA: 0x000E7CDD File Offset: 0x000E5EDD
			public MultiplayerOptions.MultiplayerOption UpdateValue(bool value)
			{
				this.UpdateValue(value ? 1 : 0);
				return this;
			}

			// Token: 0x06003B0A RID: 15114 RVA: 0x000E7CEE File Offset: 0x000E5EEE
			public MultiplayerOptions.MultiplayerOption UpdateValue(int value)
			{
				this._intValue.UpdateValue(value);
				return this;
			}

			// Token: 0x06003B0B RID: 15115 RVA: 0x000E7CFD File Offset: 0x000E5EFD
			public MultiplayerOptions.MultiplayerOption UpdateValue(string value)
			{
				this._stringValue.UpdateValue(value);
				return this;
			}

			// Token: 0x06003B0C RID: 15116 RVA: 0x000E7D0C File Offset: 0x000E5F0C
			public void GetValue(out bool value)
			{
				value = (this._intValue.Value == 1);
			}

			// Token: 0x06003B0D RID: 15117 RVA: 0x000E7D1E File Offset: 0x000E5F1E
			public void GetValue(out int value)
			{
				value = this._intValue.Value;
			}

			// Token: 0x06003B0E RID: 15118 RVA: 0x000E7D2D File Offset: 0x000E5F2D
			public void GetValue(out string value)
			{
				value = this._stringValue.Value;
			}

			// Token: 0x04001E68 RID: 7784
			public readonly MultiplayerOptions.OptionType OptionType;

			// Token: 0x04001E69 RID: 7785
			private MultiplayerOptions.MultiplayerOption.IntegerValue _intValue;

			// Token: 0x04001E6A RID: 7786
			private MultiplayerOptions.MultiplayerOption.StringValue _stringValue;

			// Token: 0x02000689 RID: 1673
			private struct IntegerValue
			{
				// Token: 0x17000A3D RID: 2621
				// (get) Token: 0x06003DF8 RID: 15864 RVA: 0x000EE108 File Offset: 0x000EC308
				public static MultiplayerOptions.MultiplayerOption.IntegerValue Invalid
				{
					get
					{
						return default(MultiplayerOptions.MultiplayerOption.IntegerValue);
					}
				}

				// Token: 0x17000A3E RID: 2622
				// (get) Token: 0x06003DF9 RID: 15865 RVA: 0x000EE11E File Offset: 0x000EC31E
				// (set) Token: 0x06003DFA RID: 15866 RVA: 0x000EE126 File Offset: 0x000EC326
				public bool IsValid { get; private set; }

				// Token: 0x17000A3F RID: 2623
				// (get) Token: 0x06003DFB RID: 15867 RVA: 0x000EE12F File Offset: 0x000EC32F
				// (set) Token: 0x06003DFC RID: 15868 RVA: 0x000EE137 File Offset: 0x000EC337
				public int Value { get; private set; }

				// Token: 0x06003DFD RID: 15869 RVA: 0x000EE140 File Offset: 0x000EC340
				public static MultiplayerOptions.MultiplayerOption.IntegerValue Create()
				{
					return new MultiplayerOptions.MultiplayerOption.IntegerValue
					{
						IsValid = true
					};
				}

				// Token: 0x06003DFE RID: 15870 RVA: 0x000EE15E File Offset: 0x000EC35E
				public void UpdateValue(int value)
				{
					this.Value = value;
				}
			}

			// Token: 0x0200068A RID: 1674
			private struct StringValue
			{
				// Token: 0x17000A40 RID: 2624
				// (get) Token: 0x06003DFF RID: 15871 RVA: 0x000EE168 File Offset: 0x000EC368
				public static MultiplayerOptions.MultiplayerOption.StringValue Invalid
				{
					get
					{
						return default(MultiplayerOptions.MultiplayerOption.StringValue);
					}
				}

				// Token: 0x17000A41 RID: 2625
				// (get) Token: 0x06003E00 RID: 15872 RVA: 0x000EE17E File Offset: 0x000EC37E
				// (set) Token: 0x06003E01 RID: 15873 RVA: 0x000EE186 File Offset: 0x000EC386
				public bool IsValid { get; private set; }

				// Token: 0x17000A42 RID: 2626
				// (get) Token: 0x06003E02 RID: 15874 RVA: 0x000EE18F File Offset: 0x000EC38F
				// (set) Token: 0x06003E03 RID: 15875 RVA: 0x000EE197 File Offset: 0x000EC397
				public string Value { get; private set; }

				// Token: 0x06003E04 RID: 15876 RVA: 0x000EE1A0 File Offset: 0x000EC3A0
				public static MultiplayerOptions.MultiplayerOption.StringValue Create()
				{
					return new MultiplayerOptions.MultiplayerOption.StringValue
					{
						IsValid = true
					};
				}

				// Token: 0x06003E05 RID: 15877 RVA: 0x000EE1BE File Offset: 0x000EC3BE
				public void UpdateValue(string value)
				{
					this.Value = value;
				}
			}
		}

		// Token: 0x020005C1 RID: 1473
		private class MultiplayerOptionsContainer
		{
			// Token: 0x06003B0F RID: 15119 RVA: 0x000E7D3C File Offset: 0x000E5F3C
			public MultiplayerOptionsContainer()
			{
				this._multiplayerOptions = new MultiplayerOptions.MultiplayerOption[43];
			}

			// Token: 0x06003B10 RID: 15120 RVA: 0x000E7D51 File Offset: 0x000E5F51
			public MultiplayerOptions.MultiplayerOption GetOptionFromOptionType(MultiplayerOptions.OptionType optionType)
			{
				return this._multiplayerOptions[(int)optionType];
			}

			// Token: 0x06003B11 RID: 15121 RVA: 0x000E7D5B File Offset: 0x000E5F5B
			private void CopyOptionFromOther(MultiplayerOptions.OptionType optionType, MultiplayerOptions.MultiplayerOption option)
			{
				this._multiplayerOptions[(int)optionType] = MultiplayerOptions.MultiplayerOption.CopyMultiplayerOption(option);
			}

			// Token: 0x06003B12 RID: 15122 RVA: 0x000E7D6B File Offset: 0x000E5F6B
			public void CreateOption(MultiplayerOptions.OptionType optionType)
			{
				this._multiplayerOptions[(int)optionType] = MultiplayerOptions.MultiplayerOption.CreateMultiplayerOption(optionType);
			}

			// Token: 0x06003B13 RID: 15123 RVA: 0x000E7D7B File Offset: 0x000E5F7B
			public void UpdateOptionValue(MultiplayerOptions.OptionType optionType, int value)
			{
				this._multiplayerOptions[(int)optionType].UpdateValue(value);
			}

			// Token: 0x06003B14 RID: 15124 RVA: 0x000E7D8C File Offset: 0x000E5F8C
			public void UpdateOptionValue(MultiplayerOptions.OptionType optionType, string value)
			{
				this._multiplayerOptions[(int)optionType].UpdateValue(value);
			}

			// Token: 0x06003B15 RID: 15125 RVA: 0x000E7D9D File Offset: 0x000E5F9D
			public void UpdateOptionValue(MultiplayerOptions.OptionType optionType, bool value)
			{
				this._multiplayerOptions[(int)optionType].UpdateValue(value ? 1 : 0);
			}

			// Token: 0x06003B16 RID: 15126 RVA: 0x000E7DB4 File Offset: 0x000E5FB4
			public void CopyAllValuesTo(MultiplayerOptions.MultiplayerOptionsContainer other)
			{
				for (MultiplayerOptions.OptionType optionType = MultiplayerOptions.OptionType.ServerName; optionType < MultiplayerOptions.OptionType.NumOfSlots; optionType++)
				{
					other.CopyOptionFromOther(optionType, this._multiplayerOptions[(int)optionType]);
				}
			}

			// Token: 0x04001E6B RID: 7787
			private readonly MultiplayerOptions.MultiplayerOption[] _multiplayerOptions;
		}
	}
}

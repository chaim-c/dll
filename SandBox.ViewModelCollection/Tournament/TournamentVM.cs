using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Tournaments.MissionLogics;
using SandBox.ViewModelCollection.Input;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.ViewModelCollection.Tournament
{
	// Token: 0x0200000D RID: 13
	public class TournamentVM : ViewModel
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00006158 File Offset: 0x00004358
		public Action DisableUI { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00006160 File Offset: 0x00004360
		public TournamentBehavior Tournament { get; }

		// Token: 0x060000BD RID: 189 RVA: 0x00006168 File Offset: 0x00004368
		public TournamentVM(Action disableUI, TournamentBehavior tournamentBehavior)
		{
			this.DisableUI = disableUI;
			this.CurrentMatch = new TournamentMatchVM();
			this.Round1 = new TournamentRoundVM();
			this.Round2 = new TournamentRoundVM();
			this.Round3 = new TournamentRoundVM();
			this.Round4 = new TournamentRoundVM();
			this._rounds = new List<TournamentRoundVM>
			{
				this.Round1,
				this.Round2,
				this.Round3,
				this.Round4
			};
			this._tournamentWinner = new TournamentParticipantVM();
			this.Tournament = tournamentBehavior;
			this.WinnerIntro = GameTexts.FindText("str_tournament_winner_intro", null).ToString();
			this.BattleRewards = new MBBindingList<TournamentRewardVM>();
			for (int i = 0; i < this._rounds.Count; i++)
			{
				this._rounds[i].Initialize(this.Tournament.Rounds[i], GameTexts.FindText("str_tournament_round", i.ToString()));
			}
			this.Refresh();
			this.Tournament.TournamentEnd += this.OnTournamentEnd;
			this.PrizeVisual = (this.HasPrizeItem ? new ImageIdentifierVM(this.Tournament.TournamentGame.Prize, "") : new ImageIdentifierVM(ImageIdentifierType.Null));
			this.SkipAllRoundsHint = new HintViewModel();
			this.RefreshValues();
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000062E0 File Offset: 0x000044E0
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.LeaveText = GameTexts.FindText("str_tournament_leave", null).ToString();
			this.SkipRoundText = GameTexts.FindText("str_tournament_skip_round", null).ToString();
			this.WatchRoundText = GameTexts.FindText("str_tournament_watch_round", null).ToString();
			this.JoinTournamentText = GameTexts.FindText("str_tournament_join_tournament", null).ToString();
			this.BetText = GameTexts.FindText("str_bet", null).ToString();
			this.AcceptText = GameTexts.FindText("str_accept", null).ToString();
			this.CancelText = GameTexts.FindText("str_cancel", null).ToString();
			this.TournamentWinnerTitle = GameTexts.FindText("str_tournament_winner_title", null).ToString();
			this.BetTitleText = GameTexts.FindText("str_wager", null).ToString();
			GameTexts.SetVariable("MAX_AMOUNT", this.Tournament.GetMaximumBet());
			GameTexts.SetVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
			this.BetDescriptionText = GameTexts.FindText("str_tournament_bet_description", null).ToString();
			this.TournamentPrizeText = GameTexts.FindText("str_tournament_prize", null).ToString();
			this.PrizeItemName = this.Tournament.TournamentGame.Prize.Name.ToString();
			MBTextManager.SetTextVariable("SETTLEMENT_NAME", this.Tournament.Settlement.Name, false);
			this.TournamentTitle = GameTexts.FindText("str_tournament", null).ToString();
			this.CurrentWagerText = GameTexts.FindText("str_tournament_current_wager", null).ToString();
			this.SkipAllRoundsHint.HintText = new TextObject("{=GaOE4bdd}Skip All Rounds", null);
			TournamentRoundVM round = this._round1;
			if (round != null)
			{
				round.RefreshValues();
			}
			TournamentRoundVM round2 = this._round2;
			if (round2 != null)
			{
				round2.RefreshValues();
			}
			TournamentRoundVM round3 = this._round3;
			if (round3 != null)
			{
				round3.RefreshValues();
			}
			TournamentRoundVM round4 = this._round4;
			if (round4 != null)
			{
				round4.RefreshValues();
			}
			TournamentMatchVM currentMatch = this._currentMatch;
			if (currentMatch != null)
			{
				currentMatch.RefreshValues();
			}
			TournamentParticipantVM tournamentWinner = this._tournamentWinner;
			if (tournamentWinner == null)
			{
				return;
			}
			tournamentWinner.RefreshValues();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000064EB File Offset: 0x000046EB
		public void ExecuteBet()
		{
			this._thisRoundBettedAmount += this.WageredDenars;
			this.Tournament.PlaceABet(this.WageredDenars);
			this.RefreshBetProperties();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00006518 File Offset: 0x00004718
		public void ExecuteJoinTournament()
		{
			if (this.PlayerCanJoinMatch())
			{
				this.Tournament.StartMatch();
				this.IsCurrentMatchActive = true;
				this.CurrentMatch.Refresh(true);
				this.CurrentMatch.State = 3;
				this.DisableUI();
				this.IsCurrentMatchActive = true;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00006569 File Offset: 0x00004769
		public void ExecuteSkipRound()
		{
			if (this.IsTournamentIncomplete)
			{
				this.Tournament.SkipMatch(false);
			}
			this.Refresh();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00006588 File Offset: 0x00004788
		public void ExecuteSkipAllRounds()
		{
			int num = 0;
			int num2 = this.Tournament.Rounds.Sum((TournamentRound r) => r.Matches.Length);
			while (!this.CanPlayerJoin)
			{
				TournamentRound currentRound = this.Tournament.CurrentRound;
				if (((currentRound != null) ? currentRound.CurrentMatch : null) == null || num >= num2)
				{
					break;
				}
				this.ExecuteSkipRound();
				num++;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000065F8 File Offset: 0x000047F8
		public void ExecuteWatchRound()
		{
			if (!this.PlayerCanJoinMatch())
			{
				this.Tournament.StartMatch();
				this.IsCurrentMatchActive = true;
				this.CurrentMatch.Refresh(true);
				this.CurrentMatch.State = 3;
				this.DisableUI();
				this.IsCurrentMatchActive = true;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000664C File Offset: 0x0000484C
		public void ExecuteLeave()
		{
			if (this.CurrentMatch != null)
			{
				List<TournamentMatch> list = new List<TournamentMatch>();
				for (int i = this.Tournament.CurrentRoundIndex; i < this.Tournament.Rounds.Length; i++)
				{
					list.AddRange(from x in this.Tournament.Rounds[i].Matches
					where x.State != TournamentMatch.MatchState.Finished
					select x);
				}
				if (list.Any((TournamentMatch x) => x.Participants.Any((TournamentParticipant y) => y.Character == CharacterObject.PlayerCharacter)))
				{
					InformationManager.ShowInquiry(new InquiryData(GameTexts.FindText("str_forfeit", null).ToString(), GameTexts.FindText("str_tournament_forfeit_game", null).ToString(), true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), new Action(this.EndTournamentMission), null, "", 0f, null, null, null), true, false);
					return;
				}
			}
			this.EndTournamentMission();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000675E File Offset: 0x0000495E
		private void EndTournamentMission()
		{
			this.Tournament.EndTournamentViaLeave();
			Mission.Current.EndMission();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00006778 File Offset: 0x00004978
		private void RefreshBetProperties()
		{
			TextObject textObject = new TextObject("{=L9GnQvsq}Stake: {BETTED_DENARS}", null);
			textObject.SetTextVariable("BETTED_DENARS", this.Tournament.BettedDenars);
			this.BettedDenarsText = textObject.ToString();
			TextObject textObject2 = new TextObject("{=xzzSaN4b}Expected: {OVERALL_EXPECTED_DENARS}", null);
			textObject2.SetTextVariable("OVERALL_EXPECTED_DENARS", this.Tournament.OverallExpectedDenars);
			this.OverallExpectedDenarsText = textObject2.ToString();
			TextObject textObject3 = new TextObject("{=yF5fpwNE}Total: {TOTAL}", null);
			textObject3.SetTextVariable("TOTAL", this.Tournament.PlayerDenars);
			this.TotalDenarsText = textObject3.ToString();
			base.OnPropertyChanged("IsBetButtonEnabled");
			this.MaximumBetValue = MathF.Min(this.Tournament.GetMaximumBet() - this._thisRoundBettedAmount, Hero.MainHero.Gold);
			GameTexts.SetVariable("NORMALIZED_EXPECTED_GOLD", (int)(this.Tournament.BetOdd * 100f));
			GameTexts.SetVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
			this.BetOddsText = GameTexts.FindText("str_tournament_bet_odd", null).ToString();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006885 File Offset: 0x00004A85
		private void OnNewRoundStarted(int prevRoundIndex, int currentRoundIndex)
		{
			this._isPlayerParticipating = this.Tournament.IsPlayerParticipating;
			this._thisRoundBettedAmount = 0;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000068A0 File Offset: 0x00004AA0
		public void Refresh()
		{
			this.IsCurrentMatchActive = false;
			this.CurrentMatch = this._rounds[this.Tournament.CurrentRoundIndex].Matches.Find((TournamentMatchVM m) => m.IsValid && m.Match == this.Tournament.CurrentMatch);
			this.ActiveRoundIndex = this.Tournament.CurrentRoundIndex;
			this.CanPlayerJoin = this.PlayerCanJoinMatch();
			base.OnPropertyChanged("IsTournamentIncomplete");
			base.OnPropertyChanged("InitializationOver");
			base.OnPropertyChanged("IsBetButtonEnabled");
			this.HasPrizeItem = (this.Tournament.TournamentGame.Prize != null && !this.IsOver);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006948 File Offset: 0x00004B48
		private void OnTournamentEnd()
		{
			TournamentParticipantVM[] array = this.Round4.Matches.Last((TournamentMatchVM m) => m.IsValid).GetParticipants().ToArray<TournamentParticipantVM>();
			TournamentParticipantVM tournamentParticipantVM = array[0];
			TournamentParticipantVM tournamentParticipantVM2 = array[1];
			this.TournamentWinner = this.Round4.Matches.Last((TournamentMatchVM m) => m.IsValid).GetParticipants().First((TournamentParticipantVM p) => p.Participant == this.Tournament.Winner);
			this.TournamentWinner.Refresh();
			if (this.TournamentWinner.Participant.Character.IsHero)
			{
				Hero heroObject = this.TournamentWinner.Participant.Character.HeroObject;
				this.TournamentWinner.Character.ArmorColor1 = heroObject.MapFaction.Color;
				this.TournamentWinner.Character.ArmorColor2 = heroObject.MapFaction.Color2;
			}
			else
			{
				CultureObject culture = this.TournamentWinner.Participant.Character.Culture;
				this.TournamentWinner.Character.ArmorColor1 = culture.Color;
				this.TournamentWinner.Character.ArmorColor2 = culture.Color2;
			}
			this.IsWinnerHero = this.Tournament.Winner.Character.IsHero;
			if (this.IsWinnerHero)
			{
				this.WinnerBanner = new ImageIdentifierVM(BannerCode.CreateFrom(this.Tournament.Winner.Character.HeroObject.ClanBanner), true);
			}
			if (this.TournamentWinner.Participant.Character.IsPlayerCharacter)
			{
				TournamentParticipantVM tournamentParticipantVM3 = (tournamentParticipantVM == this.TournamentWinner) ? tournamentParticipantVM2 : tournamentParticipantVM;
				GameTexts.SetVariable("TOURNAMENT_FINAL_OPPONENT", tournamentParticipantVM3.Name);
				this.WinnerIntro = GameTexts.FindText("str_tournament_result_won", null).ToString();
				if (this.Tournament.TournamentGame.TournamentWinRenown > 0f)
				{
					GameTexts.SetVariable("RENOWN", this.Tournament.TournamentGame.TournamentWinRenown.ToString("F1"));
					this.BattleRewards.Add(new TournamentRewardVM(GameTexts.FindText("str_tournament_renown", null).ToString()));
				}
				if (this.Tournament.TournamentGame.TournamentWinInfluence > 0f)
				{
					float tournamentWinInfluence = this.Tournament.TournamentGame.TournamentWinInfluence;
					TextObject textObject = GameTexts.FindText("str_tournament_influence", null);
					textObject.SetTextVariable("INFLUENCE", tournamentWinInfluence.ToString("F1"));
					textObject.SetTextVariable("INFLUENCE_ICON", "{=!}<img src=\"General\\Icons\\Influence@2x\" extend=\"7\">");
					this.BattleRewards.Add(new TournamentRewardVM(textObject.ToString()));
				}
				if (this.Tournament.TournamentGame.Prize != null)
				{
					string content = this.Tournament.TournamentGame.Prize.Name.ToString();
					GameTexts.SetVariable("REWARD", content);
					this.BattleRewards.Add(new TournamentRewardVM(GameTexts.FindText("str_tournament_reward", null).ToString(), new ImageIdentifierVM(this.Tournament.TournamentGame.Prize, "")));
				}
				if (this.Tournament.OverallExpectedDenars > 0)
				{
					int overallExpectedDenars = this.Tournament.OverallExpectedDenars;
					TextObject textObject2 = GameTexts.FindText("str_tournament_bet", null);
					textObject2.SetTextVariable("BET", overallExpectedDenars);
					textObject2.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
					this.BattleRewards.Add(new TournamentRewardVM(textObject2.ToString()));
				}
			}
			else if (tournamentParticipantVM.Participant.Character.IsPlayerCharacter || tournamentParticipantVM2.Participant.Character.IsPlayerCharacter)
			{
				TournamentParticipantVM tournamentParticipantVM4 = (tournamentParticipantVM == this.TournamentWinner) ? tournamentParticipantVM : tournamentParticipantVM2;
				GameTexts.SetVariable("TOURNAMENT_FINAL_OPPONENT", tournamentParticipantVM4.Name);
				this.WinnerIntro = GameTexts.FindText("str_tournament_result_eliminated_at_final", null).ToString();
			}
			else
			{
				int num = 3;
				bool flag = this.Round3.GetParticipants().Any((TournamentParticipantVM p) => p.Participant.Character.IsPlayerCharacter);
				bool flag2 = this.Round2.GetParticipants().Any((TournamentParticipantVM p) => p.Participant.Character.IsPlayerCharacter);
				bool flag3 = this.Round1.GetParticipants().Any((TournamentParticipantVM p) => p.Participant.Character.IsPlayerCharacter);
				if (flag)
				{
					num = 3;
				}
				else if (flag2)
				{
					num = 2;
				}
				else if (flag3)
				{
					num = 1;
				}
				bool flag4 = tournamentParticipantVM == this.TournamentWinner;
				GameTexts.SetVariable("TOURNAMENT_FINAL_PARTICIPANT_A", flag4 ? tournamentParticipantVM.Name : tournamentParticipantVM2.Name);
				GameTexts.SetVariable("TOURNAMENT_FINAL_PARTICIPANT_B", flag4 ? tournamentParticipantVM2.Name : tournamentParticipantVM.Name);
				if (this._isPlayerParticipating)
				{
					GameTexts.SetVariable("TOURNAMENT_ELIMINATED_ROUND", num.ToString());
					this.WinnerIntro = GameTexts.FindText("str_tournament_result_eliminated", null).ToString();
				}
				else
				{
					this.WinnerIntro = GameTexts.FindText("str_tournament_result_spectator", null).ToString();
				}
			}
			this.IsOver = true;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006E7E File Offset: 0x0000507E
		private bool PlayerCanJoinMatch()
		{
			if (this.IsTournamentIncomplete)
			{
				return this.Tournament.CurrentMatch.Participants.Any((TournamentParticipant x) => x.Character == CharacterObject.PlayerCharacter);
			}
			return false;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006EC0 File Offset: 0x000050C0
		public void OnAgentRemoved(Agent agent)
		{
			if (this.IsCurrentMatchActive && agent.IsHuman)
			{
				TournamentParticipant participant = this.CurrentMatch.Match.GetParticipant(agent.Origin.UniqueSeed);
				if (participant != null)
				{
					this.CurrentMatch.GetParticipants().First((TournamentParticipantVM p) => p.Participant == participant).IsDead = true;
				}
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006F2E File Offset: 0x0000512E
		public void ExecuteShowPrizeItemTooltip()
		{
			if (this.HasPrizeItem)
			{
				InformationManager.ShowTooltip(typeof(ItemObject), new object[]
				{
					new EquipmentElement(this.Tournament.TournamentGame.Prize, null, null, false)
				});
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00006F6D File Offset: 0x0000516D
		public void ExecuteHidePrizeItemTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00006F74 File Offset: 0x00005174
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM doneInputKey = this.DoneInputKey;
			if (doneInputKey != null)
			{
				doneInputKey.OnFinalize();
			}
			InputKeyItemVM cancelInputKey = this.CancelInputKey;
			if (cancelInputKey == null)
			{
				return;
			}
			cancelInputKey.OnFinalize();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006F9D File Offset: 0x0000519D
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00006FAC File Offset: 0x000051AC
		public void SetCancelInputKey(HotKey hotKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00006FBB File Offset: 0x000051BB
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00006FC3 File Offset: 0x000051C3
		[DataSourceProperty]
		public InputKeyItemVM DoneInputKey
		{
			get
			{
				return this._doneInputKey;
			}
			set
			{
				if (value != this._doneInputKey)
				{
					this._doneInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneInputKey");
				}
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00006FE1 File Offset: 0x000051E1
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00006FE9 File Offset: 0x000051E9
		[DataSourceProperty]
		public InputKeyItemVM CancelInputKey
		{
			get
			{
				return this._cancelInputKey;
			}
			set
			{
				if (value != this._cancelInputKey)
				{
					this._cancelInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CancelInputKey");
				}
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00007007 File Offset: 0x00005207
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000700F File Offset: 0x0000520F
		[DataSourceProperty]
		public string TournamentWinnerTitle
		{
			get
			{
				return this._tournamentWinnerTitle;
			}
			set
			{
				if (value != this._tournamentWinnerTitle)
				{
					this._tournamentWinnerTitle = value;
					base.OnPropertyChangedWithValue<string>(value, "TournamentWinnerTitle");
				}
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00007032 File Offset: 0x00005232
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x0000703A File Offset: 0x0000523A
		[DataSourceProperty]
		public TournamentParticipantVM TournamentWinner
		{
			get
			{
				return this._tournamentWinner;
			}
			set
			{
				if (value != this._tournamentWinner)
				{
					this._tournamentWinner = value;
					base.OnPropertyChangedWithValue<TournamentParticipantVM>(value, "TournamentWinner");
				}
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00007058 File Offset: 0x00005258
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00007060 File Offset: 0x00005260
		[DataSourceProperty]
		public int MaximumBetValue
		{
			get
			{
				return this._maximumBetValue;
			}
			set
			{
				if (value != this._maximumBetValue)
				{
					this._maximumBetValue = value;
					base.OnPropertyChangedWithValue(value, "MaximumBetValue");
					this._wageredDenars = -1;
					this.WageredDenars = 0;
				}
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000708C File Offset: 0x0000528C
		[DataSourceProperty]
		public bool IsBetButtonEnabled
		{
			get
			{
				return this.PlayerCanJoinMatch() && this.Tournament.GetMaximumBet() > this._thisRoundBettedAmount && Hero.MainHero.Gold > 0;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000070B8 File Offset: 0x000052B8
		// (set) Token: 0x060000DD RID: 221 RVA: 0x000070C0 File Offset: 0x000052C0
		[DataSourceProperty]
		public string BetText
		{
			get
			{
				return this._betText;
			}
			set
			{
				if (value != this._betText)
				{
					this._betText = value;
					base.OnPropertyChangedWithValue<string>(value, "BetText");
				}
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000070E3 File Offset: 0x000052E3
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000070EB File Offset: 0x000052EB
		[DataSourceProperty]
		public string BetTitleText
		{
			get
			{
				return this._betTitleText;
			}
			set
			{
				if (value != this._betTitleText)
				{
					this._betTitleText = value;
					base.OnPropertyChangedWithValue<string>(value, "BetTitleText");
				}
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000710E File Offset: 0x0000530E
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00007116 File Offset: 0x00005316
		[DataSourceProperty]
		public string CurrentWagerText
		{
			get
			{
				return this._currentWagerText;
			}
			set
			{
				if (value != this._currentWagerText)
				{
					this._currentWagerText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentWagerText");
				}
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00007139 File Offset: 0x00005339
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00007141 File Offset: 0x00005341
		[DataSourceProperty]
		public string BetDescriptionText
		{
			get
			{
				return this._betDescriptionText;
			}
			set
			{
				if (value != this._betDescriptionText)
				{
					this._betDescriptionText = value;
					base.OnPropertyChangedWithValue<string>(value, "BetDescriptionText");
				}
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00007164 File Offset: 0x00005364
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x0000716C File Offset: 0x0000536C
		[DataSourceProperty]
		public ImageIdentifierVM PrizeVisual
		{
			get
			{
				return this._prizeVisual;
			}
			set
			{
				if (value != this._prizeVisual)
				{
					this._prizeVisual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "PrizeVisual");
				}
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x0000718A File Offset: 0x0000538A
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00007192 File Offset: 0x00005392
		[DataSourceProperty]
		public string PrizeItemName
		{
			get
			{
				return this._prizeItemName;
			}
			set
			{
				if (value != this._prizeItemName)
				{
					this._prizeItemName = value;
					base.OnPropertyChangedWithValue<string>(value, "PrizeItemName");
				}
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000071B5 File Offset: 0x000053B5
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x000071BD File Offset: 0x000053BD
		[DataSourceProperty]
		public string TournamentPrizeText
		{
			get
			{
				return this._tournamentPrizeText;
			}
			set
			{
				if (value != this._tournamentPrizeText)
				{
					this._tournamentPrizeText = value;
					base.OnPropertyChangedWithValue<string>(value, "TournamentPrizeText");
				}
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000071E0 File Offset: 0x000053E0
		// (set) Token: 0x060000EB RID: 235 RVA: 0x000071E8 File Offset: 0x000053E8
		[DataSourceProperty]
		public int WageredDenars
		{
			get
			{
				return this._wageredDenars;
			}
			set
			{
				if (value != this._wageredDenars)
				{
					this._wageredDenars = value;
					base.OnPropertyChangedWithValue(value, "WageredDenars");
					this.ExpectedBetDenars = ((this._wageredDenars == 0) ? 0 : this.Tournament.GetExpectedDenarsForBet(this._wageredDenars));
				}
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00007228 File Offset: 0x00005428
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00007230 File Offset: 0x00005430
		[DataSourceProperty]
		public int ExpectedBetDenars
		{
			get
			{
				return this._expectedBetDenars;
			}
			set
			{
				if (value != this._expectedBetDenars)
				{
					this._expectedBetDenars = value;
					base.OnPropertyChangedWithValue(value, "ExpectedBetDenars");
				}
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000724E File Offset: 0x0000544E
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00007256 File Offset: 0x00005456
		[DataSourceProperty]
		public string BetOddsText
		{
			get
			{
				return this._betOddsText;
			}
			set
			{
				if (value != this._betOddsText)
				{
					this._betOddsText = value;
					base.OnPropertyChangedWithValue<string>(value, "BetOddsText");
				}
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00007279 File Offset: 0x00005479
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00007281 File Offset: 0x00005481
		[DataSourceProperty]
		public string BettedDenarsText
		{
			get
			{
				return this._bettedDenarsText;
			}
			set
			{
				if (value != this._bettedDenarsText)
				{
					this._bettedDenarsText = value;
					base.OnPropertyChangedWithValue<string>(value, "BettedDenarsText");
				}
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000072A4 File Offset: 0x000054A4
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x000072AC File Offset: 0x000054AC
		[DataSourceProperty]
		public string OverallExpectedDenarsText
		{
			get
			{
				return this._overallExpectedDenarsText;
			}
			set
			{
				if (value != this._overallExpectedDenarsText)
				{
					this._overallExpectedDenarsText = value;
					base.OnPropertyChangedWithValue<string>(value, "OverallExpectedDenarsText");
				}
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000072CF File Offset: 0x000054CF
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x000072D7 File Offset: 0x000054D7
		[DataSourceProperty]
		public string CurrentExpectedDenarsText
		{
			get
			{
				return this._currentExpectedDenarsText;
			}
			set
			{
				if (value != this._currentExpectedDenarsText)
				{
					this._currentExpectedDenarsText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentExpectedDenarsText");
				}
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000072FA File Offset: 0x000054FA
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00007302 File Offset: 0x00005502
		[DataSourceProperty]
		public string TotalDenarsText
		{
			get
			{
				return this._totalDenarsText;
			}
			set
			{
				if (value != this._totalDenarsText)
				{
					this._totalDenarsText = value;
					base.OnPropertyChangedWithValue<string>(value, "TotalDenarsText");
				}
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00007325 File Offset: 0x00005525
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x0000732D File Offset: 0x0000552D
		[DataSourceProperty]
		public string AcceptText
		{
			get
			{
				return this._acceptText;
			}
			set
			{
				if (value != this._acceptText)
				{
					this._acceptText = value;
					base.OnPropertyChangedWithValue<string>(value, "AcceptText");
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00007350 File Offset: 0x00005550
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00007358 File Offset: 0x00005558
		[DataSourceProperty]
		public string CancelText
		{
			get
			{
				return this._cancelText;
			}
			set
			{
				if (value != this._cancelText)
				{
					this._cancelText = value;
					base.OnPropertyChangedWithValue<string>(value, "CancelText");
				}
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000737B File Offset: 0x0000557B
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00007383 File Offset: 0x00005583
		[DataSourceProperty]
		public bool IsCurrentMatchActive
		{
			get
			{
				return this._isCurrentMatchActive;
			}
			set
			{
				this._isCurrentMatchActive = value;
				base.OnPropertyChangedWithValue(value, "IsCurrentMatchActive");
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00007398 File Offset: 0x00005598
		// (set) Token: 0x060000FF RID: 255 RVA: 0x000073A0 File Offset: 0x000055A0
		[DataSourceProperty]
		public TournamentMatchVM CurrentMatch
		{
			get
			{
				return this._currentMatch;
			}
			set
			{
				if (value != this._currentMatch)
				{
					TournamentMatchVM currentMatch = this._currentMatch;
					if (currentMatch != null && currentMatch.IsValid)
					{
						this._currentMatch.State = 2;
						this._currentMatch.Refresh(false);
						int num = this._rounds.FindIndex((TournamentRoundVM r) => r.Matches.Any((TournamentMatchVM m) => m.Match == this.Tournament.LastMatch));
						if (num < this.Tournament.Rounds.Length - 1)
						{
							this._rounds[num + 1].Initialize();
						}
					}
					this._currentMatch = value;
					base.OnPropertyChangedWithValue<TournamentMatchVM>(value, "CurrentMatch");
					if (this._currentMatch != null)
					{
						this._currentMatch.State = 1;
					}
				}
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00007449 File Offset: 0x00005649
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00007463 File Offset: 0x00005663
		[DataSourceProperty]
		public bool IsTournamentIncomplete
		{
			get
			{
				return this.Tournament == null || this.Tournament.CurrentMatch != null;
			}
			set
			{
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00007465 File Offset: 0x00005665
		// (set) Token: 0x06000103 RID: 259 RVA: 0x0000746D File Offset: 0x0000566D
		[DataSourceProperty]
		public int ActiveRoundIndex
		{
			get
			{
				return this._activeRoundIndex;
			}
			set
			{
				if (value != this._activeRoundIndex)
				{
					this.OnNewRoundStarted(this._activeRoundIndex, value);
					this._activeRoundIndex = value;
					base.OnPropertyChangedWithValue(value, "ActiveRoundIndex");
					this.RefreshBetProperties();
				}
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000749E File Offset: 0x0000569E
		// (set) Token: 0x06000105 RID: 261 RVA: 0x000074A6 File Offset: 0x000056A6
		[DataSourceProperty]
		public bool CanPlayerJoin
		{
			get
			{
				return this._canPlayerJoin;
			}
			set
			{
				if (value != this._canPlayerJoin)
				{
					this._canPlayerJoin = value;
					base.OnPropertyChangedWithValue(value, "CanPlayerJoin");
				}
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000074C4 File Offset: 0x000056C4
		// (set) Token: 0x06000107 RID: 263 RVA: 0x000074CC File Offset: 0x000056CC
		[DataSourceProperty]
		public bool HasPrizeItem
		{
			get
			{
				return this._hasPrizeItem;
			}
			set
			{
				if (value != this._hasPrizeItem)
				{
					this._hasPrizeItem = value;
					base.OnPropertyChangedWithValue(value, "HasPrizeItem");
				}
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000074EA File Offset: 0x000056EA
		// (set) Token: 0x06000109 RID: 265 RVA: 0x000074F2 File Offset: 0x000056F2
		[DataSourceProperty]
		public string JoinTournamentText
		{
			get
			{
				return this._joinTournamentText;
			}
			set
			{
				if (value != this._joinTournamentText)
				{
					this._joinTournamentText = value;
					base.OnPropertyChangedWithValue<string>(value, "JoinTournamentText");
				}
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00007515 File Offset: 0x00005715
		// (set) Token: 0x0600010B RID: 267 RVA: 0x0000751D File Offset: 0x0000571D
		[DataSourceProperty]
		public string SkipRoundText
		{
			get
			{
				return this._skipRoundText;
			}
			set
			{
				if (value != this._skipRoundText)
				{
					this._skipRoundText = value;
					base.OnPropertyChangedWithValue<string>(value, "SkipRoundText");
				}
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00007540 File Offset: 0x00005740
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00007548 File Offset: 0x00005748
		[DataSourceProperty]
		public string WatchRoundText
		{
			get
			{
				return this._watchRoundText;
			}
			set
			{
				if (value != this._watchRoundText)
				{
					this._watchRoundText = value;
					base.OnPropertyChangedWithValue<string>(value, "WatchRoundText");
				}
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000756B File Offset: 0x0000576B
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00007573 File Offset: 0x00005773
		[DataSourceProperty]
		public string LeaveText
		{
			get
			{
				return this._leaveText;
			}
			set
			{
				if (value != this._leaveText)
				{
					this._leaveText = value;
					base.OnPropertyChangedWithValue<string>(value, "LeaveText");
				}
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00007596 File Offset: 0x00005796
		// (set) Token: 0x06000111 RID: 273 RVA: 0x0000759E File Offset: 0x0000579E
		[DataSourceProperty]
		public TournamentRoundVM Round1
		{
			get
			{
				return this._round1;
			}
			set
			{
				if (value != this._round1)
				{
					this._round1 = value;
					base.OnPropertyChangedWithValue<TournamentRoundVM>(value, "Round1");
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000075BC File Offset: 0x000057BC
		// (set) Token: 0x06000113 RID: 275 RVA: 0x000075C4 File Offset: 0x000057C4
		[DataSourceProperty]
		public TournamentRoundVM Round2
		{
			get
			{
				return this._round2;
			}
			set
			{
				if (value != this._round2)
				{
					this._round2 = value;
					base.OnPropertyChangedWithValue<TournamentRoundVM>(value, "Round2");
				}
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000075E2 File Offset: 0x000057E2
		// (set) Token: 0x06000115 RID: 277 RVA: 0x000075EA File Offset: 0x000057EA
		[DataSourceProperty]
		public TournamentRoundVM Round3
		{
			get
			{
				return this._round3;
			}
			set
			{
				if (value != this._round3)
				{
					this._round3 = value;
					base.OnPropertyChangedWithValue<TournamentRoundVM>(value, "Round3");
				}
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00007608 File Offset: 0x00005808
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00007610 File Offset: 0x00005810
		[DataSourceProperty]
		public TournamentRoundVM Round4
		{
			get
			{
				return this._round4;
			}
			set
			{
				if (value != this._round4)
				{
					this._round4 = value;
					base.OnPropertyChangedWithValue<TournamentRoundVM>(value, "Round4");
				}
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000762E File Offset: 0x0000582E
		[DataSourceProperty]
		public bool InitializationOver
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00007631 File Offset: 0x00005831
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00007639 File Offset: 0x00005839
		[DataSourceProperty]
		public string TournamentTitle
		{
			get
			{
				return this._tournamentTitle;
			}
			set
			{
				if (value != this._tournamentTitle)
				{
					this._tournamentTitle = value;
					base.OnPropertyChangedWithValue<string>(value, "TournamentTitle");
				}
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000765C File Offset: 0x0000585C
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00007664 File Offset: 0x00005864
		[DataSourceProperty]
		public bool IsOver
		{
			get
			{
				return this._isOver;
			}
			set
			{
				if (this._isOver != value)
				{
					this._isOver = value;
					base.OnPropertyChangedWithValue(value, "IsOver");
				}
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00007682 File Offset: 0x00005882
		// (set) Token: 0x0600011E RID: 286 RVA: 0x0000768A File Offset: 0x0000588A
		[DataSourceProperty]
		public string WinnerIntro
		{
			get
			{
				return this._winnerIntro;
			}
			set
			{
				if (value != this._winnerIntro)
				{
					this._winnerIntro = value;
					base.OnPropertyChangedWithValue<string>(value, "WinnerIntro");
				}
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000076AD File Offset: 0x000058AD
		// (set) Token: 0x06000120 RID: 288 RVA: 0x000076B5 File Offset: 0x000058B5
		[DataSourceProperty]
		public MBBindingList<TournamentRewardVM> BattleRewards
		{
			get
			{
				return this._battleRewards;
			}
			set
			{
				if (value != this._battleRewards)
				{
					this._battleRewards = value;
					base.OnPropertyChangedWithValue<MBBindingList<TournamentRewardVM>>(value, "BattleRewards");
				}
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000076D3 File Offset: 0x000058D3
		// (set) Token: 0x06000122 RID: 290 RVA: 0x000076DB File Offset: 0x000058DB
		[DataSourceProperty]
		public bool IsWinnerHero
		{
			get
			{
				return this._isWinnerHero;
			}
			set
			{
				if (value != this._isWinnerHero)
				{
					this._isWinnerHero = value;
					base.OnPropertyChangedWithValue(value, "IsWinnerHero");
				}
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000076F9 File Offset: 0x000058F9
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00007701 File Offset: 0x00005901
		[DataSourceProperty]
		public bool IsBetWindowEnabled
		{
			get
			{
				return this._isBetWindowEnabled;
			}
			set
			{
				if (value != this._isBetWindowEnabled)
				{
					this._isBetWindowEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsBetWindowEnabled");
				}
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000771F File Offset: 0x0000591F
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00007727 File Offset: 0x00005927
		[DataSourceProperty]
		public ImageIdentifierVM WinnerBanner
		{
			get
			{
				return this._winnerBanner;
			}
			set
			{
				if (value != this._winnerBanner)
				{
					this._winnerBanner = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "WinnerBanner");
				}
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00007745 File Offset: 0x00005945
		// (set) Token: 0x06000128 RID: 296 RVA: 0x0000774D File Offset: 0x0000594D
		[DataSourceProperty]
		public HintViewModel SkipAllRoundsHint
		{
			get
			{
				return this._skipAllRoundsHint;
			}
			set
			{
				if (value != this._skipAllRoundsHint)
				{
					this._skipAllRoundsHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "SkipAllRoundsHint");
				}
			}
		}

		// Token: 0x0400004E RID: 78
		private readonly List<TournamentRoundVM> _rounds;

		// Token: 0x0400004F RID: 79
		private int _thisRoundBettedAmount;

		// Token: 0x04000050 RID: 80
		private bool _isPlayerParticipating;

		// Token: 0x04000051 RID: 81
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000052 RID: 82
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000053 RID: 83
		private TournamentRoundVM _round1;

		// Token: 0x04000054 RID: 84
		private TournamentRoundVM _round2;

		// Token: 0x04000055 RID: 85
		private TournamentRoundVM _round3;

		// Token: 0x04000056 RID: 86
		private TournamentRoundVM _round4;

		// Token: 0x04000057 RID: 87
		private int _activeRoundIndex = -1;

		// Token: 0x04000058 RID: 88
		private string _joinTournamentText;

		// Token: 0x04000059 RID: 89
		private string _skipRoundText;

		// Token: 0x0400005A RID: 90
		private string _watchRoundText;

		// Token: 0x0400005B RID: 91
		private string _leaveText;

		// Token: 0x0400005C RID: 92
		private bool _canPlayerJoin;

		// Token: 0x0400005D RID: 93
		private TournamentMatchVM _currentMatch;

		// Token: 0x0400005E RID: 94
		private bool _isCurrentMatchActive;

		// Token: 0x0400005F RID: 95
		private string _betTitleText;

		// Token: 0x04000060 RID: 96
		private string _betDescriptionText;

		// Token: 0x04000061 RID: 97
		private string _betOddsText;

		// Token: 0x04000062 RID: 98
		private string _bettedDenarsText;

		// Token: 0x04000063 RID: 99
		private string _overallExpectedDenarsText;

		// Token: 0x04000064 RID: 100
		private string _currentExpectedDenarsText;

		// Token: 0x04000065 RID: 101
		private string _totalDenarsText;

		// Token: 0x04000066 RID: 102
		private string _acceptText;

		// Token: 0x04000067 RID: 103
		private string _cancelText;

		// Token: 0x04000068 RID: 104
		private string _prizeItemName;

		// Token: 0x04000069 RID: 105
		private string _tournamentPrizeText;

		// Token: 0x0400006A RID: 106
		private string _currentWagerText;

		// Token: 0x0400006B RID: 107
		private int _wageredDenars = -1;

		// Token: 0x0400006C RID: 108
		private int _expectedBetDenars = -1;

		// Token: 0x0400006D RID: 109
		private string _betText;

		// Token: 0x0400006E RID: 110
		private int _maximumBetValue;

		// Token: 0x0400006F RID: 111
		private string _tournamentWinnerTitle;

		// Token: 0x04000070 RID: 112
		private TournamentParticipantVM _tournamentWinner;

		// Token: 0x04000071 RID: 113
		private string _tournamentTitle;

		// Token: 0x04000072 RID: 114
		private bool _isOver;

		// Token: 0x04000073 RID: 115
		private bool _hasPrizeItem;

		// Token: 0x04000074 RID: 116
		private bool _isWinnerHero;

		// Token: 0x04000075 RID: 117
		private bool _isBetWindowEnabled;

		// Token: 0x04000076 RID: 118
		private string _winnerIntro;

		// Token: 0x04000077 RID: 119
		private ImageIdentifierVM _prizeVisual;

		// Token: 0x04000078 RID: 120
		private ImageIdentifierVM _winnerBanner;

		// Token: 0x04000079 RID: 121
		private MBBindingList<TournamentRewardVM> _battleRewards;

		// Token: 0x0400007A RID: 122
		private HintViewModel _skipAllRoundsHint;
	}
}

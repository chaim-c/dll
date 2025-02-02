using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.Tournaments.MissionLogics
{
	// Token: 0x0200002E RID: 46
	public class TournamentBehavior : MissionLogic, ICameraModeLogic
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000870F File Offset: 0x0000690F
		public TournamentGame TournamentGame
		{
			get
			{
				return this._tournamentGame;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00008717 File Offset: 0x00006917
		// (set) Token: 0x0600013A RID: 314 RVA: 0x0000871F File Offset: 0x0000691F
		public TournamentRound[] Rounds { get; private set; }

		// Token: 0x0600013B RID: 315 RVA: 0x00008728 File Offset: 0x00006928
		public SpectatorCameraTypes GetMissionCameraLockMode(bool lockedToMainPlayer)
		{
			if (!this.IsPlayerParticipating)
			{
				return SpectatorCameraTypes.LockToAnyAgent;
			}
			return SpectatorCameraTypes.Invalid;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00008735 File Offset: 0x00006935
		// (set) Token: 0x0600013D RID: 317 RVA: 0x0000873D File Offset: 0x0000693D
		public bool IsPlayerEliminated { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00008746 File Offset: 0x00006946
		// (set) Token: 0x0600013F RID: 319 RVA: 0x0000874E File Offset: 0x0000694E
		public int CurrentRoundIndex { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00008757 File Offset: 0x00006957
		// (set) Token: 0x06000141 RID: 321 RVA: 0x0000875F File Offset: 0x0000695F
		public TournamentMatch LastMatch { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00008768 File Offset: 0x00006968
		public TournamentRound CurrentRound
		{
			get
			{
				return this.Rounds[this.CurrentRoundIndex];
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00008777 File Offset: 0x00006977
		public TournamentRound NextRound
		{
			get
			{
				if (this.CurrentRoundIndex != 3)
				{
					return this.Rounds[this.CurrentRoundIndex + 1];
				}
				return null;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00008793 File Offset: 0x00006993
		public TournamentMatch CurrentMatch
		{
			get
			{
				return this.CurrentRound.CurrentMatch;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000087A0 File Offset: 0x000069A0
		// (set) Token: 0x06000146 RID: 326 RVA: 0x000087A8 File Offset: 0x000069A8
		public TournamentParticipant Winner { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000087B1 File Offset: 0x000069B1
		// (set) Token: 0x06000148 RID: 328 RVA: 0x000087B9 File Offset: 0x000069B9
		public bool IsPlayerParticipating { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000087C2 File Offset: 0x000069C2
		// (set) Token: 0x0600014A RID: 330 RVA: 0x000087CA File Offset: 0x000069CA
		public Settlement Settlement { get; private set; }

		// Token: 0x0600014B RID: 331 RVA: 0x000087D4 File Offset: 0x000069D4
		public TournamentBehavior(TournamentGame tournamentGame, Settlement settlement, ITournamentGameBehavior gameBehavior, bool isPlayerParticipating)
		{
			this.Settlement = settlement;
			this._tournamentGame = tournamentGame;
			this._gameBehavior = gameBehavior;
			this.Rounds = new TournamentRound[4];
			this.CreateParticipants(isPlayerParticipating);
			this.CurrentRoundIndex = -1;
			this.LastMatch = null;
			this.Winner = null;
			this.IsPlayerParticipating = isPlayerParticipating;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000882D File Offset: 0x00006A2D
		public MBList<CharacterObject> GetAllPossibleParticipants()
		{
			return this._tournamentGame.GetParticipantCharacters(this.Settlement, true);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00008844 File Offset: 0x00006A44
		private void CreateParticipants(bool includePlayer)
		{
			this._participants = new TournamentParticipant[this._tournamentGame.MaximumParticipantCount];
			MBList<CharacterObject> participantCharacters = this._tournamentGame.GetParticipantCharacters(this.Settlement, includePlayer);
			participantCharacters.Shuffle<CharacterObject>();
			int num = 0;
			while (num < participantCharacters.Count && num < this._tournamentGame.MaximumParticipantCount)
			{
				this._participants[num] = new TournamentParticipant(participantCharacters[num], default(UniqueTroopDescriptor));
				num++;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000088BC File Offset: 0x00006ABC
		public static void DeleteTournamentSetsExcept(GameEntity selectedSetEntity)
		{
			List<GameEntity> list = Mission.Current.Scene.FindEntitiesWithTag("arena_set").ToList<GameEntity>();
			list.Remove(selectedSetEntity);
			foreach (GameEntity gameEntity in list)
			{
				gameEntity.Remove(93);
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000892C File Offset: 0x00006B2C
		public static void DeleteAllTournamentSets()
		{
			foreach (GameEntity gameEntity in Mission.Current.Scene.FindEntitiesWithTag("arena_set").ToList<GameEntity>())
			{
				gameEntity.Remove(94);
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00008994 File Offset: 0x00006B94
		public override void AfterStart()
		{
			this.CurrentRoundIndex = 0;
			this.CreateTournamentTree();
			this.FillParticipants(this._participants.ToList<TournamentParticipant>());
			this.CalculateBet();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000089BA File Offset: 0x00006BBA
		public override void OnMissionTick(float dt)
		{
			if (this.CurrentMatch != null && this.CurrentMatch.State == TournamentMatch.MatchState.Started && this._gameBehavior.IsMatchEnded())
			{
				this.EndCurrentMatch(false);
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000089E8 File Offset: 0x00006BE8
		public void StartMatch()
		{
			if (this.CurrentMatch.IsPlayerParticipating())
			{
				Campaign.Current.TournamentManager.OnPlayerJoinMatch(this._tournamentGame.GetType());
			}
			this.CurrentMatch.Start();
			base.Mission.SetMissionMode(MissionMode.Tournament, true);
			this._gameBehavior.StartMatch(this.CurrentMatch, this.NextRound == null);
			CampaignEventDispatcher.Instance.OnPlayerStartedTournamentMatch(this.Settlement.Town);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00008A63 File Offset: 0x00006C63
		public void SkipMatch(bool isLeave = false)
		{
			this.CurrentMatch.Start();
			this._gameBehavior.SkipMatch(this.CurrentMatch);
			this.EndCurrentMatch(isLeave);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00008A88 File Offset: 0x00006C88
		private void EndCurrentMatch(bool isLeave)
		{
			this.LastMatch = this.CurrentMatch;
			this.CurrentRound.EndMatch();
			this._gameBehavior.OnMatchEnded();
			if (this.LastMatch.IsPlayerParticipating())
			{
				if (this.LastMatch.Winners.All((TournamentParticipant x) => x.Character != CharacterObject.PlayerCharacter))
				{
					this.OnPlayerEliminated();
				}
				else
				{
					this.OnPlayerWinMatch();
				}
			}
			if (this.NextRound != null)
			{
				for (;;)
				{
					if (!this.LastMatch.Winners.Any((TournamentParticipant x) => !x.IsAssigned))
					{
						break;
					}
					foreach (TournamentParticipant tournamentParticipant in this.LastMatch.Winners)
					{
						if (!tournamentParticipant.IsAssigned)
						{
							this.NextRound.AddParticipant(tournamentParticipant, false);
							tournamentParticipant.IsAssigned = true;
						}
					}
				}
			}
			if (this.CurrentRound.CurrentMatch == null)
			{
				if (this.CurrentRoundIndex < 3)
				{
					int i = this.CurrentRoundIndex;
					this.CurrentRoundIndex = i + 1;
					this.CalculateBet();
					MissionGameModels missionGameModels = MissionGameModels.Current;
					if (missionGameModels == null)
					{
						return;
					}
					AgentStatCalculateModel agentStatCalculateModel = missionGameModels.AgentStatCalculateModel;
					if (agentStatCalculateModel == null)
					{
						return;
					}
					agentStatCalculateModel.SetAILevelMultiplier(1f + (float)this.CurrentRoundIndex / 3f);
					return;
				}
				else
				{
					MissionGameModels missionGameModels2 = MissionGameModels.Current;
					if (missionGameModels2 != null)
					{
						AgentStatCalculateModel agentStatCalculateModel2 = missionGameModels2.AgentStatCalculateModel;
						if (agentStatCalculateModel2 != null)
						{
							agentStatCalculateModel2.ResetAILevelMultiplier();
						}
					}
					this.CalculateBet();
					MBInformationManager.AddQuickInformation(new TextObject("{=tWzLqegB}Tournament is over.", null), 0, null, "");
					this.Winner = this.LastMatch.Winners.FirstOrDefault<TournamentParticipant>();
					if (this.Winner.Character.IsHero)
					{
						if (this.Winner.Character == CharacterObject.PlayerCharacter)
						{
							this.OnPlayerWinTournament();
						}
						Campaign.Current.TournamentManager.GivePrizeToWinner(this._tournamentGame, this.Winner.Character.HeroObject, true);
						Campaign.Current.TournamentManager.AddLeaderboardEntry(this.Winner.Character.HeroObject);
					}
					MBList<CharacterObject> mblist = new MBList<CharacterObject>(this._participants.Length);
					foreach (TournamentParticipant tournamentParticipant2 in this._participants)
					{
						mblist.Add(tournamentParticipant2.Character);
					}
					CampaignEventDispatcher.Instance.OnTournamentFinished(this.Winner.Character, mblist, this.Settlement.Town, this._tournamentGame.Prize);
					if (this.TournamentEnd != null && !isLeave)
					{
						this.TournamentEnd();
					}
				}
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00008D2C File Offset: 0x00006F2C
		public void EndTournamentViaLeave()
		{
			while (this.CurrentMatch != null)
			{
				this.SkipMatch(true);
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00008D40 File Offset: 0x00006F40
		private void OnPlayerEliminated()
		{
			this.IsPlayerEliminated = true;
			this.BetOdd = 0f;
			if (this.BettedDenars > 0)
			{
				GiveGoldAction.ApplyForCharacterToSettlement(null, Settlement.CurrentSettlement, this.BettedDenars, false);
			}
			this.OverallExpectedDenars = 0;
			CampaignEventDispatcher.Instance.OnPlayerEliminatedFromTournament(this.CurrentRoundIndex, this.Settlement.Town);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00008D9C File Offset: 0x00006F9C
		private void OnPlayerWinMatch()
		{
			Campaign.Current.TournamentManager.OnPlayerWinMatch(this._tournamentGame.GetType());
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008DB8 File Offset: 0x00006FB8
		private void OnPlayerWinTournament()
		{
			if (Campaign.Current.GameMode != CampaignGameMode.Campaign)
			{
				return;
			}
			if (Hero.MainHero.MapFaction.IsKingdomFaction && Hero.MainHero.MapFaction.Leader != Hero.MainHero)
			{
				GainKingdomInfluenceAction.ApplyForDefault(Hero.MainHero, 1f);
			}
			if (this.OverallExpectedDenars > 0)
			{
				GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, this.OverallExpectedDenars, false);
			}
			Campaign.Current.TournamentManager.OnPlayerWinTournament(this._tournamentGame.GetType());
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008E40 File Offset: 0x00007040
		private void CreateTournamentTree()
		{
			int num = 16;
			int b = (int)MathF.Log((float)this._tournamentGame.MaxTeamSize, 2f);
			for (int i = 0; i < 4; i++)
			{
				int num2 = (int)MathF.Log((float)num, 2f);
				int num3 = MBRandom.RandomInt(1, MathF.Min(MathF.Min(3, num2), this._tournamentGame.MaxTeamNumberPerMatch));
				int num4 = MathF.Min(num2 - num3, b);
				int num5 = MathF.Ceiling(MathF.Log((float)(1 + MBRandom.RandomInt((int)MathF.Pow(2f, (float)num4))), 2f));
				int x = num2 - (num3 + num5);
				this.Rounds[i] = new TournamentRound(num, MathF.PowTwo32(x), MathF.PowTwo32(num3), num / 2, this._tournamentGame.Mode);
				num /= 2;
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00008F14 File Offset: 0x00007114
		private void FillParticipants(List<TournamentParticipant> participants)
		{
			foreach (TournamentParticipant participant in participants)
			{
				this.Rounds[this.CurrentRoundIndex].AddParticipant(participant, true);
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00008F70 File Offset: 0x00007170
		public override InquiryData OnEndMissionRequest(out bool canPlayerLeave)
		{
			InquiryData result = null;
			canPlayerLeave = false;
			return result;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00008F76 File Offset: 0x00007176
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00008F7E File Offset: 0x0000717E
		public float BetOdd { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00008F87 File Offset: 0x00007187
		public int MaximumBetInstance
		{
			get
			{
				return MathF.Min(150, this.PlayerDenars);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00008F99 File Offset: 0x00007199
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00008FA1 File Offset: 0x000071A1
		public int BettedDenars { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00008FAA File Offset: 0x000071AA
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00008FB2 File Offset: 0x000071B2
		public int OverallExpectedDenars { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00008FBB File Offset: 0x000071BB
		public int PlayerDenars
		{
			get
			{
				return Hero.MainHero.Gold;
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00008FC7 File Offset: 0x000071C7
		public void PlaceABet(int bet)
		{
			this.BettedDenars += bet;
			this.OverallExpectedDenars += this.GetExpectedDenarsForBet(bet);
			GiveGoldAction.ApplyBetweenCharacters(Hero.MainHero, null, bet, true);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00008FF8 File Offset: 0x000071F8
		public int GetExpectedDenarsForBet(int bet)
		{
			return (int)(this.BetOdd * (float)bet);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00009004 File Offset: 0x00007204
		public int GetMaximumBet()
		{
			int num = 150;
			if (Hero.MainHero.GetPerkValue(DefaultPerks.Roguery.DeepPockets))
			{
				num *= (int)DefaultPerks.Roguery.DeepPockets.PrimaryBonus;
			}
			return num;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00009038 File Offset: 0x00007238
		private void CalculateBet()
		{
			if (this.IsPlayerParticipating)
			{
				if (this.CurrentRound.CurrentMatch == null)
				{
					this.BetOdd = 0f;
					return;
				}
				if (this.IsPlayerEliminated || !this.IsPlayerParticipating)
				{
					this.OverallExpectedDenars = 0;
					this.BetOdd = 0f;
					return;
				}
				List<KeyValuePair<Hero, int>> leaderboard = Campaign.Current.TournamentManager.GetLeaderboard();
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < leaderboard.Count; i++)
				{
					if (leaderboard[i].Key == Hero.MainHero)
					{
						num = leaderboard[i].Value;
					}
					if (leaderboard[i].Value > num2)
					{
						num2 = leaderboard[i].Value;
					}
				}
				float num3 = 30f + (float)Hero.MainHero.Level + (float)MathF.Max(0, num * 12 - num2 * 2);
				float num4 = 0f;
				float num5 = 0f;
				float num6 = 0f;
				foreach (TournamentMatch tournamentMatch in this.CurrentRound.Matches)
				{
					foreach (TournamentTeam tournamentTeam in tournamentMatch.Teams)
					{
						float num7 = 0f;
						foreach (TournamentParticipant tournamentParticipant in tournamentTeam.Participants)
						{
							if (tournamentParticipant.Character != CharacterObject.PlayerCharacter)
							{
								int num8 = 0;
								if (tournamentParticipant.Character.IsHero)
								{
									for (int k = 0; k < leaderboard.Count; k++)
									{
										if (leaderboard[k].Key == tournamentParticipant.Character.HeroObject)
										{
											num8 = leaderboard[k].Value;
										}
									}
								}
								num7 += (float)(tournamentParticipant.Character.Level + MathF.Max(0, num8 * 8 - num2 * 2));
							}
						}
						if (tournamentTeam.Participants.Any((TournamentParticipant x) => x.Character == CharacterObject.PlayerCharacter))
						{
							num5 = num7;
							foreach (TournamentTeam tournamentTeam2 in tournamentMatch.Teams)
							{
								if (tournamentTeam != tournamentTeam2)
								{
									foreach (TournamentParticipant tournamentParticipant2 in tournamentTeam2.Participants)
									{
										int num9 = 0;
										if (tournamentParticipant2.Character.IsHero)
										{
											for (int l = 0; l < leaderboard.Count; l++)
											{
												if (leaderboard[l].Key == tournamentParticipant2.Character.HeroObject)
												{
													num9 = leaderboard[l].Value;
												}
											}
										}
										num6 += (float)(tournamentParticipant2.Character.Level + MathF.Max(0, num9 * 8 - num2 * 2));
									}
								}
							}
						}
						num4 += num7;
					}
				}
				float num10 = (num5 + num3) / (num6 + num5 + num3);
				float num11 = num3 / (num5 + num3 + 0.5f * (num4 - (num5 + num6)));
				float num12 = num10 * num11;
				float num13 = MathF.Clamp(MathF.Pow(1f / num12, 0.75f), 1.1f, 4f);
				this.BetOdd = (float)((int)(num13 * 10f)) / 10f;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000168 RID: 360 RVA: 0x00009454 File Offset: 0x00007654
		// (remove) Token: 0x06000169 RID: 361 RVA: 0x0000948C File Offset: 0x0000768C
		public event Action TournamentEnd;

		// Token: 0x0400005E RID: 94
		public const int RoundCount = 4;

		// Token: 0x0400005F RID: 95
		public const int ParticipantCount = 16;

		// Token: 0x04000060 RID: 96
		public const float EndMatchTimerDuration = 6f;

		// Token: 0x04000061 RID: 97
		public const float CheerTimerDuration = 1f;

		// Token: 0x04000062 RID: 98
		private TournamentGame _tournamentGame;

		// Token: 0x04000063 RID: 99
		private ITournamentGameBehavior _gameBehavior;

		// Token: 0x04000065 RID: 101
		private TournamentParticipant[] _participants;

		// Token: 0x0400006D RID: 109
		private const int MaximumBet = 150;

		// Token: 0x0400006E RID: 110
		public const float MaximumOdd = 4f;
	}
}

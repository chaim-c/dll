using System;
using Helpers;
using SandBox.BoardGames.MissionLogics;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace SandBox.BoardGames.AI
{
	// Token: 0x020000D1 RID: 209
	public abstract class BoardGameAIBase
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0004D898 File Offset: 0x0004BA98
		public BoardGameAIBase.AIState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0004D8A2 File Offset: 0x0004BAA2
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x0004D8AA File Offset: 0x0004BAAA
		public Move RecentMoveCalculated { get; private set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0004D8B3 File Offset: 0x0004BAB3
		public bool AbortRequested
		{
			get
			{
				return this.State == BoardGameAIBase.AIState.AbortRequested;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x0004D8BE File Offset: 0x0004BABE
		// (set) Token: 0x06000A97 RID: 2711 RVA: 0x0004D8C6 File Offset: 0x0004BAC6
		private protected BoardGameHelper.AIDifficulty Difficulty { protected get; private set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0004D8CF File Offset: 0x0004BACF
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x0004D8D7 File Offset: 0x0004BAD7
		private protected MissionBoardGameLogic BoardGameHandler { protected get; private set; }

		// Token: 0x06000A9A RID: 2714 RVA: 0x0004D8E0 File Offset: 0x0004BAE0
		protected BoardGameAIBase(BoardGameHelper.AIDifficulty difficulty, MissionBoardGameLogic boardGameHandler)
		{
			this._stateLock = new object();
			this.Difficulty = difficulty;
			this.BoardGameHandler = boardGameHandler;
			this.Initialize();
			this._aiTask = AsyncTask.CreateWithDelegate(new ManagedDelegate
			{
				Instance = new ManagedDelegate.DelegateDefinition(this.UpdateThinkingAboutMoveOnSeparateThread)
			}, true);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0004D937 File Offset: 0x0004BB37
		public virtual Move CalculatePreMovementStageMove()
		{
			Debug.FailedAssert("CalculatePreMovementStageMove is not implemented for " + this.BoardGameHandler.CurrentBoardGame, "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\BoardGames\\AI\\BoardGameAIBase.cs", "CalculatePreMovementStageMove", 60);
			return Move.Invalid;
		}

		// Token: 0x06000A9C RID: 2716
		public abstract Move CalculateMovementStageMove();

		// Token: 0x06000A9D RID: 2717
		protected abstract void InitializeDifficulty();

		// Token: 0x06000A9E RID: 2718 RVA: 0x0004D969 File Offset: 0x0004BB69
		public virtual bool WantsToForfeit()
		{
			return false;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0004D96C File Offset: 0x0004BB6C
		public void OnSetGameOver()
		{
			object stateLock = this._stateLock;
			lock (stateLock)
			{
				BoardGameAIBase.AIState state = this.State;
				if (state != BoardGameAIBase.AIState.ReadyToRun)
				{
					if (state == BoardGameAIBase.AIState.Running)
					{
						this._state = BoardGameAIBase.AIState.AbortRequested;
					}
				}
				else
				{
					this._state = BoardGameAIBase.AIState.AbortRequested;
				}
			}
			this._aiTask.Wait();
			this.Reset();
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0004D9DC File Offset: 0x0004BBDC
		public void Initialize()
		{
			this.Reset();
			this.InitializeDifficulty();
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0004D9EA File Offset: 0x0004BBEA
		public void SetDifficulty(BoardGameHelper.AIDifficulty difficulty)
		{
			this.Difficulty = difficulty;
			this.InitializeDifficulty();
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0004D9F9 File Offset: 0x0004BBF9
		public float HowLongDidAIThinkAboutMove()
		{
			return this._aiDecisionTimer;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0004DA04 File Offset: 0x0004BC04
		public void UpdateThinkingAboutMove(float dt)
		{
			this._aiDecisionTimer += dt;
			object stateLock = this._stateLock;
			lock (stateLock)
			{
				if (this.State == BoardGameAIBase.AIState.NeedsToRun)
				{
					this._state = BoardGameAIBase.AIState.ReadyToRun;
					this._aiTask.Invoke();
				}
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0004DA68 File Offset: 0x0004BC68
		private void UpdateThinkingAboutMoveOnSeparateThread()
		{
			if (this.BoardGameHandler.Board.InPreMovementStage)
			{
				this.CalculatePreMovementStageOnSeparateThread();
				return;
			}
			this.CalculateMovementStageMoveOnSeparateThread();
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0004DA89 File Offset: 0x0004BC89
		public void ResetThinking()
		{
			this._aiDecisionTimer = 0f;
			this._state = BoardGameAIBase.AIState.NeedsToRun;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0004DA9F File Offset: 0x0004BC9F
		public bool CanMakeMove()
		{
			return this.State == BoardGameAIBase.AIState.Done && this._aiDecisionTimer >= 1.5f;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0004DABC File Offset: 0x0004BCBC
		private void Reset()
		{
			this.RecentMoveCalculated = Move.Invalid;
			this.MayForfeit = true;
			this.ResetThinking();
			this.MaxDepth = 0;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0004DAE0 File Offset: 0x0004BCE0
		private void CalculatePreMovementStageOnSeparateThread()
		{
			if (this.OnBeginSeparateThread())
			{
				Move calculatedMove = this.CalculatePreMovementStageMove();
				this.OnExitSeparateThread(calculatedMove);
			}
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0004DB04 File Offset: 0x0004BD04
		private void CalculateMovementStageMoveOnSeparateThread()
		{
			if (this.OnBeginSeparateThread())
			{
				Move calculatedMove = this.CalculateMovementStageMove();
				this.OnExitSeparateThread(calculatedMove);
			}
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0004DB28 File Offset: 0x0004BD28
		private bool OnBeginSeparateThread()
		{
			bool flag = false;
			object stateLock = this._stateLock;
			lock (stateLock)
			{
				if (this.AbortRequested)
				{
					this._state = BoardGameAIBase.AIState.Aborted;
					flag = true;
				}
				else
				{
					this._state = BoardGameAIBase.AIState.Running;
				}
			}
			return !flag;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0004DB88 File Offset: 0x0004BD88
		private void OnExitSeparateThread(Move calculatedMove)
		{
			object stateLock = this._stateLock;
			lock (stateLock)
			{
				if (this.AbortRequested)
				{
					this._state = BoardGameAIBase.AIState.Aborted;
					this.RecentMoveCalculated = Move.Invalid;
				}
				else
				{
					this._state = BoardGameAIBase.AIState.Done;
					this.RecentMoveCalculated = calculatedMove;
				}
			}
		}

		// Token: 0x0400040B RID: 1035
		private const float AIDecisionDuration = 1.5f;

		// Token: 0x0400040C RID: 1036
		protected bool MayForfeit;

		// Token: 0x0400040D RID: 1037
		protected int MaxDepth;

		// Token: 0x0400040E RID: 1038
		private float _aiDecisionTimer;

		// Token: 0x0400040F RID: 1039
		private readonly ITask _aiTask;

		// Token: 0x04000410 RID: 1040
		private readonly object _stateLock;

		// Token: 0x04000411 RID: 1041
		private volatile BoardGameAIBase.AIState _state;

		// Token: 0x020001C5 RID: 453
		public enum AIState
		{
			// Token: 0x04000781 RID: 1921
			NeedsToRun,
			// Token: 0x04000782 RID: 1922
			ReadyToRun,
			// Token: 0x04000783 RID: 1923
			Running,
			// Token: 0x04000784 RID: 1924
			AbortRequested,
			// Token: 0x04000785 RID: 1925
			Aborted,
			// Token: 0x04000786 RID: 1926
			Done
		}
	}
}

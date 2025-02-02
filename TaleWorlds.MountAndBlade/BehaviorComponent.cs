using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200010D RID: 269
	public abstract class BehaviorComponent
	{
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0001B30B File Offset: 0x0001950B
		// (set) Token: 0x06000D19 RID: 3353 RVA: 0x0001B313 File Offset: 0x00019513
		public Formation Formation { get; private set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0001B31C File Offset: 0x0001951C
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x0001B324 File Offset: 0x00019524
		public float BehaviorCoherence { get; set; }

		// Token: 0x06000D1C RID: 3356 RVA: 0x0001B330 File Offset: 0x00019530
		protected BehaviorComponent(Formation formation)
		{
			this.Formation = formation;
			this.PreserveExpireTime = 0f;
			this._navmeshlessTargetPenaltyTime = new Timer(Mission.Current.CurrentTime, 50f, true);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0001B386 File Offset: 0x00019586
		protected BehaviorComponent()
		{
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0001B3A4 File Offset: 0x000195A4
		private void InformSergeantPlayer()
		{
			if (Mission.Current.MainAgent != null && this.Formation.Team.GeneralAgent != null && !this.Formation.Team.IsPlayerGeneral && this.Formation.Team.IsPlayerSergeant && this.Formation.PlayerOwner == Agent.Main)
			{
				TextObject behaviorString = this.GetBehaviorString();
				MBTextManager.SetTextVariable("BEHAVIOR", behaviorString, false);
				MBTextManager.SetTextVariable("PLAYER_NAME", Mission.Current.MainAgent.Name, false);
				MBTextManager.SetTextVariable("TEAM_LEADER", this.Formation.Team.GeneralAgent.Name, false);
				MBInformationManager.AddQuickInformation(new TextObject("{=L91XKoMD}{TEAM_LEADER}: {PLAYER_NAME}, {BEHAVIOR}", null), 4000, this.Formation.Team.GeneralAgent.Character, "");
			}
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0001B48D File Offset: 0x0001968D
		protected virtual void OnBehaviorActivatedAux()
		{
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0001B490 File Offset: 0x00019690
		internal void OnBehaviorActivated()
		{
			if (!this.Formation.Team.IsPlayerGeneral && !this.Formation.Team.IsPlayerSergeant && this.Formation.IsPlayerTroopInFormation && Mission.Current.MainAgent != null)
			{
				TextObject textObject = new TextObject(this.ToString().Replace("MBModule.Behavior", ""), null);
				MBTextManager.SetTextVariable("BEHAVIOUR_NAME_BEGIN", textObject, false);
				textObject = GameTexts.FindText("str_formation_ai_soldier_instruction_text", null);
				MBInformationManager.AddQuickInformation(textObject, 2000, Mission.Current.MainAgent.Character, "");
			}
			if (!GameNetwork.IsMultiplayer)
			{
				this.InformSergeantPlayer();
				this._lastPlayerInformTime = Mission.Current.CurrentTime;
			}
			if (this.Formation.IsAIControlled)
			{
				this.OnBehaviorActivatedAux();
			}
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0001B55D File Offset: 0x0001975D
		public virtual void OnBehaviorCanceled()
		{
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0001B55F File Offset: 0x0001975F
		public virtual void OnAgentRemoved(Agent agent)
		{
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0001B564 File Offset: 0x00019764
		public void RemindSergeantPlayer()
		{
			float currentTime = Mission.Current.CurrentTime;
			if (this == this.Formation.AI.ActiveBehavior && this._lastPlayerInformTime + 60f < currentTime)
			{
				this.InformSergeantPlayer();
				this._lastPlayerInformTime = currentTime;
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0001B5AB File Offset: 0x000197AB
		public virtual void TickOccasionally()
		{
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x0001B5B0 File Offset: 0x000197B0
		// (set) Token: 0x06000D26 RID: 3366 RVA: 0x0001B63C File Offset: 0x0001983C
		public virtual float NavmeshlessTargetPositionPenalty
		{
			get
			{
				if (this._navmeshlessTargetPositionPenalty == 1f)
				{
					return 1f;
				}
				this._navmeshlessTargetPenaltyTime.Check(Mission.Current.CurrentTime);
				float num = this._navmeshlessTargetPenaltyTime.ElapsedTime();
				if (num >= 10f)
				{
					this._navmeshlessTargetPositionPenalty = 1f;
					return 1f;
				}
				if (num <= 5f)
				{
					return this._navmeshlessTargetPositionPenalty;
				}
				return MBMath.Lerp(this._navmeshlessTargetPositionPenalty, 1f, (num - 5f) / 5f, 1E-05f);
			}
			set
			{
				this._navmeshlessTargetPenaltyTime.Reset(Mission.Current.CurrentTime);
				this._navmeshlessTargetPositionPenalty = value;
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0001B65A File Offset: 0x0001985A
		public float GetAIWeight()
		{
			return this.GetAiWeight() * this.NavmeshlessTargetPositionPenalty;
		}

		// Token: 0x06000D28 RID: 3368
		protected abstract float GetAiWeight();

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x0001B669 File Offset: 0x00019869
		// (set) Token: 0x06000D2A RID: 3370 RVA: 0x0001B671 File Offset: 0x00019871
		public MovementOrder CurrentOrder
		{
			get
			{
				return this._currentOrder;
			}
			protected set
			{
				this._currentOrder = value;
				this.IsCurrentOrderChanged = true;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x0001B681 File Offset: 0x00019881
		// (set) Token: 0x06000D2C RID: 3372 RVA: 0x0001B689 File Offset: 0x00019889
		public float PreserveExpireTime { get; set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x0001B692 File Offset: 0x00019892
		// (set) Token: 0x06000D2E RID: 3374 RVA: 0x0001B69A File Offset: 0x0001989A
		public float WeightFactor { get; set; }

		// Token: 0x06000D2F RID: 3375 RVA: 0x0001B6A3 File Offset: 0x000198A3
		public virtual void ResetBehavior()
		{
			this.WeightFactor = 0f;
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0001B6B0 File Offset: 0x000198B0
		public virtual TextObject GetBehaviorString()
		{
			string name = base.GetType().Name;
			return GameTexts.FindText("str_formation_ai_sergeant_instruction_behavior_text", name);
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0001B6D4 File Offset: 0x000198D4
		public virtual void OnValidBehaviorSideChanged()
		{
			this._behaviorSide = this.Formation.AI.Side;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0001B6EC File Offset: 0x000198EC
		protected virtual void CalculateCurrentOrder()
		{
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0001B6F0 File Offset: 0x000198F0
		public void PrecalculateMovementOrder()
		{
			this.CalculateCurrentOrder();
			this.CurrentOrder.GetPosition(this.Formation);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0001B718 File Offset: 0x00019918
		public override bool Equals(object obj)
		{
			return base.GetType() == obj.GetType();
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0001B72B File Offset: 0x0001992B
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0001B733 File Offset: 0x00019933
		public virtual void OnDeploymentFinished()
		{
		}

		// Token: 0x04000310 RID: 784
		protected FormationAI.BehaviorSide _behaviorSide;

		// Token: 0x04000311 RID: 785
		protected const float FormArrangementDistanceToOrderPosition = 10f;

		// Token: 0x04000312 RID: 786
		private const float _playerInformCooldown = 60f;

		// Token: 0x04000313 RID: 787
		protected float _lastPlayerInformTime;

		// Token: 0x04000314 RID: 788
		private Timer _navmeshlessTargetPenaltyTime;

		// Token: 0x04000315 RID: 789
		private float _navmeshlessTargetPositionPenalty = 1f;

		// Token: 0x04000316 RID: 790
		public bool IsCurrentOrderChanged;

		// Token: 0x04000317 RID: 791
		private MovementOrder _currentOrder;

		// Token: 0x04000318 RID: 792
		protected FacingOrder CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
	}
}

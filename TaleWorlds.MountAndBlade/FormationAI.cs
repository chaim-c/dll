using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000138 RID: 312
	public class FormationAI
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000E75 RID: 3701 RVA: 0x000276A8 File Offset: 0x000258A8
		// (remove) Token: 0x06000E76 RID: 3702 RVA: 0x000276E0 File Offset: 0x000258E0
		public event Action<Formation> OnActiveBehaviorChanged;

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x00027715 File Offset: 0x00025915
		// (set) Token: 0x06000E78 RID: 3704 RVA: 0x00027720 File Offset: 0x00025920
		public BehaviorComponent ActiveBehavior
		{
			get
			{
				return this._activeBehavior;
			}
			private set
			{
				if (this._activeBehavior != value)
				{
					BehaviorComponent activeBehavior = this._activeBehavior;
					if (activeBehavior != null)
					{
						activeBehavior.OnBehaviorCanceled();
					}
					BehaviorComponent activeBehavior2 = this._activeBehavior;
					this._activeBehavior = value;
					this._activeBehavior.OnBehaviorActivated();
					this.ActiveBehavior.PreserveExpireTime = Mission.Current.CurrentTime + 10f;
					if (this.OnActiveBehaviorChanged != null && (activeBehavior2 == null || !activeBehavior2.Equals(value)))
					{
						this.OnActiveBehaviorChanged(this._formation);
					}
				}
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x000277A0 File Offset: 0x000259A0
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x000277A8 File Offset: 0x000259A8
		public FormationAI.BehaviorSide Side
		{
			get
			{
				return this._side;
			}
			set
			{
				if (this._side != value)
				{
					this._side = value;
					if (this._side != FormationAI.BehaviorSide.BehaviorSideNotSet)
					{
						foreach (BehaviorComponent behaviorComponent in this._behaviors)
						{
							behaviorComponent.OnValidBehaviorSideChanged();
						}
					}
				}
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x00027814 File Offset: 0x00025A14
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x0002781C File Offset: 0x00025A1C
		public bool IsMainFormation { get; set; }

		// Token: 0x06000E7D RID: 3709 RVA: 0x00027828 File Offset: 0x00025A28
		public FormationAI(Formation formation)
		{
			this._formation = formation;
			float num = 0f;
			if (formation.Team != null)
			{
				float num2 = 0.1f * (float)formation.FormationIndex;
				float num3 = 0f;
				if (formation.Team.TeamIndex >= 0)
				{
					num3 = (float)formation.Team.TeamIndex * 0.5f * 0.1f;
				}
				num = num2 + num3;
			}
			this._tickTimer = new Timer(Mission.Current.CurrentTime + 0.5f * num, 0.5f, true);
			this._specialBehaviorData = new List<FormationAI.BehaviorData>();
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x000278D0 File Offset: 0x00025AD0
		public T SetBehaviorWeight<T>(float w) where T : BehaviorComponent
		{
			using (List<BehaviorComponent>.Enumerator enumerator = this._behaviors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					T t;
					if ((t = (enumerator.Current as T)) != null)
					{
						t.WeightFactor = w;
						return t;
					}
				}
			}
			throw new MBException("Behavior weight could not be set.");
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0002794C File Offset: 0x00025B4C
		public void AddAiBehavior(BehaviorComponent behaviorComponent)
		{
			this._behaviors.Add(behaviorComponent);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0002795C File Offset: 0x00025B5C
		public T GetBehavior<T>() where T : BehaviorComponent
		{
			using (List<BehaviorComponent>.Enumerator enumerator = this._behaviors.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					T result;
					if ((result = (enumerator.Current as T)) != null)
					{
						return result;
					}
				}
			}
			using (List<FormationAI.BehaviorData>.Enumerator enumerator2 = this._specialBehaviorData.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					T result2;
					if ((result2 = (enumerator2.Current.Behavior as T)) != null)
					{
						return result2;
					}
				}
			}
			return default(T);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00027A24 File Offset: 0x00025C24
		public void AddSpecialBehavior(BehaviorComponent behavior, bool purgePreviousSpecialBehaviors = false)
		{
			if (purgePreviousSpecialBehaviors)
			{
				this._specialBehaviorData.Clear();
			}
			this._specialBehaviorData.Add(new FormationAI.BehaviorData
			{
				Behavior = behavior
			});
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00027A4C File Offset: 0x00025C4C
		private bool FindBestBehavior()
		{
			BehaviorComponent behaviorComponent = null;
			float num = float.MinValue;
			foreach (BehaviorComponent behaviorComponent2 in this._behaviors)
			{
				if (behaviorComponent2.WeightFactor > 1E-07f)
				{
					float num2 = behaviorComponent2.GetAIWeight() * behaviorComponent2.WeightFactor;
					if (behaviorComponent2 == this.ActiveBehavior)
					{
						num2 *= MBMath.Lerp(1.2f, 2f, MBMath.ClampFloat((behaviorComponent2.PreserveExpireTime - Mission.Current.CurrentTime) / 5f, 0f, 1f), float.MinValue);
					}
					if (num2 > num)
					{
						if (behaviorComponent2.NavmeshlessTargetPositionPenalty > 0f)
						{
							num2 /= behaviorComponent2.NavmeshlessTargetPositionPenalty;
						}
						behaviorComponent2.PrecalculateMovementOrder();
						num2 *= behaviorComponent2.NavmeshlessTargetPositionPenalty;
						if (num2 > num)
						{
							behaviorComponent = behaviorComponent2;
							num = num2;
						}
					}
				}
			}
			if (behaviorComponent != null)
			{
				this.ActiveBehavior = behaviorComponent;
				if (behaviorComponent != this._behaviors[0])
				{
					this._behaviors.Remove(behaviorComponent);
					this._behaviors.Insert(0, behaviorComponent);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00027B7C File Offset: 0x00025D7C
		private void PreprocessBehaviors()
		{
			if (this._formation.HasAnyEnemyFormationsThatIsNotEmpty())
			{
				FormationAI.BehaviorData behaviorData = this._specialBehaviorData.FirstOrDefault((FormationAI.BehaviorData sd) => !sd.IsPreprocessed);
				if (behaviorData != null)
				{
					behaviorData.Behavior.TickOccasionally();
					float num = behaviorData.Behavior.GetAIWeight();
					if (behaviorData.Behavior == this.ActiveBehavior)
					{
						num *= MBMath.Lerp(1.01f, 1.5f, MBMath.ClampFloat((behaviorData.Behavior.PreserveExpireTime - Mission.Current.CurrentTime) / 5f, 0f, 1f), float.MinValue);
					}
					behaviorData.Weight = num * behaviorData.Preference;
					behaviorData.IsPreprocessed = true;
				}
			}
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00027C44 File Offset: 0x00025E44
		public void Tick()
		{
			if (Mission.Current.AllowAiTicking && (Mission.Current.ForceTickOccasionally || this._tickTimer.Check(Mission.Current.CurrentTime)))
			{
				this.TickOccasionally(this._tickTimer.PreviousDeltaTime);
			}
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00027C94 File Offset: 0x00025E94
		private void TickOccasionally(float dt)
		{
			this._formation.IsAITickedAfterSplit = true;
			if (this.FindBestBehavior())
			{
				if (!this._formation.IsAIControlled)
				{
					if (GameNetwork.IsMultiplayer && Mission.Current.MainAgent != null && !this._formation.Team.IsPlayerGeneral && this._formation.Team.IsPlayerSergeant && this._formation.PlayerOwner == Agent.Main)
					{
						this.ActiveBehavior.RemindSergeantPlayer();
						return;
					}
				}
				else
				{
					this.ActiveBehavior.TickOccasionally();
				}
				return;
			}
			BehaviorComponent behaviorComponent = this.ActiveBehavior;
			if (this._formation.HasAnyEnemyFormationsThatIsNotEmpty())
			{
				this.PreprocessBehaviors();
				foreach (FormationAI.BehaviorData behaviorData in this._specialBehaviorData)
				{
					behaviorData.IsPreprocessed = false;
				}
				if (behaviorComponent is BehaviorStop && this._specialBehaviorData.Count > 0)
				{
					IEnumerable<FormationAI.BehaviorData> source = from sbd in this._specialBehaviorData
					where sbd.Weight > 0f
					select sbd;
					if (source.Any<FormationAI.BehaviorData>())
					{
						behaviorComponent = source.MaxBy((FormationAI.BehaviorData abd) => abd.Weight).Behavior;
					}
				}
				bool isAIControlled = this._formation.IsAIControlled;
				bool flag = false;
				if (this.ActiveBehavior != behaviorComponent)
				{
					BehaviorComponent activeBehavior = this.ActiveBehavior;
					this.ActiveBehavior = behaviorComponent;
					flag = true;
				}
				if (flag || (behaviorComponent != null && behaviorComponent.IsCurrentOrderChanged))
				{
					if (this._formation.IsAIControlled)
					{
						this._formation.SetMovementOrder(behaviorComponent.CurrentOrder);
					}
					behaviorComponent.IsCurrentOrderChanged = false;
				}
			}
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00027E54 File Offset: 0x00026054
		public void OnDeploymentFinished()
		{
			foreach (BehaviorComponent behaviorComponent in this._behaviors)
			{
				behaviorComponent.OnDeploymentFinished();
			}
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00027EA4 File Offset: 0x000260A4
		public void OnAgentRemoved(Agent agent)
		{
			foreach (BehaviorComponent behaviorComponent in this._behaviors)
			{
				behaviorComponent.OnAgentRemoved(agent);
			}
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00027EF8 File Offset: 0x000260F8
		[Conditional("DEBUG")]
		public void DebugMore()
		{
			if (!MBDebug.IsDisplayingHighLevelAI)
			{
				return;
			}
			foreach (FormationAI.BehaviorData behaviorData in from d in this._specialBehaviorData
			orderby d.Behavior.GetType().ToString()
			select d)
			{
				behaviorData.Behavior.GetType().ToString().Replace("MBModule.Behavior", "");
				behaviorData.Weight.ToString("0.00");
				behaviorData.Preference.ToString("0.00");
			}
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00027FAC File Offset: 0x000261AC
		[Conditional("DEBUG")]
		public void DebugScores()
		{
			if (this._formation.PhysicalClass.IsRanged())
			{
				MBDebug.Print("Ranged", 0, Debug.DebugColor.White, 17592186044416UL);
			}
			else if (this._formation.PhysicalClass.IsMeleeCavalry())
			{
				MBDebug.Print("Cavalry", 0, Debug.DebugColor.White, 17592186044416UL);
			}
			else
			{
				MBDebug.Print("Infantry", 0, Debug.DebugColor.White, 17592186044416UL);
			}
			foreach (FormationAI.BehaviorData behaviorData in from d in this._specialBehaviorData
			orderby d.Behavior.GetType().ToString()
			select d)
			{
				string text = behaviorData.Behavior.GetType().ToString().Replace("MBModule.Behavior", "");
				string text2 = behaviorData.Weight.ToString("0.00");
				string text3 = behaviorData.Preference.ToString("0.00");
				MBDebug.Print(string.Concat(new string[]
				{
					text,
					" \t\t w:",
					text2,
					"\t p:",
					text3
				}), 0, Debug.DebugColor.White, 17592186044416UL);
			}
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x000280FC File Offset: 0x000262FC
		public void ResetBehaviorWeights()
		{
			foreach (BehaviorComponent behaviorComponent in this._behaviors)
			{
				behaviorComponent.ResetBehavior();
			}
		}

		// Token: 0x04000396 RID: 918
		private const float BehaviorPreserveTime = 5f;

		// Token: 0x04000398 RID: 920
		private readonly Formation _formation;

		// Token: 0x04000399 RID: 921
		private readonly List<FormationAI.BehaviorData> _specialBehaviorData;

		// Token: 0x0400039A RID: 922
		private readonly List<BehaviorComponent> _behaviors = new List<BehaviorComponent>();

		// Token: 0x0400039B RID: 923
		private BehaviorComponent _activeBehavior;

		// Token: 0x0400039C RID: 924
		private FormationAI.BehaviorSide _side = FormationAI.BehaviorSide.Middle;

		// Token: 0x0400039D RID: 925
		private readonly Timer _tickTimer;

		// Token: 0x02000421 RID: 1057
		public class BehaviorData
		{
			// Token: 0x04001827 RID: 6183
			public BehaviorComponent Behavior;

			// Token: 0x04001828 RID: 6184
			public float Preference = 1f;

			// Token: 0x04001829 RID: 6185
			public float Weight;

			// Token: 0x0400182A RID: 6186
			public bool IsRemovedOnCancel;

			// Token: 0x0400182B RID: 6187
			public bool IsPreprocessed;
		}

		// Token: 0x02000422 RID: 1058
		public enum BehaviorSide
		{
			// Token: 0x0400182D RID: 6189
			Left,
			// Token: 0x0400182E RID: 6190
			Middle,
			// Token: 0x0400182F RID: 6191
			Right,
			// Token: 0x04001830 RID: 6192
			BehaviorSideNotSet,
			// Token: 0x04001831 RID: 6193
			ValidBehaviorSideCount = 3
		}
	}
}

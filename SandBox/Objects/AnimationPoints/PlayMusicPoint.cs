using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace SandBox.Objects.AnimationPoints
{
	// Token: 0x02000045 RID: 69
	public class PlayMusicPoint : AnimationPoint
	{
		// Token: 0x06000292 RID: 658 RVA: 0x00010D39 File Offset: 0x0000EF39
		protected override void OnInit()
		{
			base.OnInit();
			this.KeepOldVisibility = true;
			base.IsDisabledForPlayers = true;
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00010D5C File Offset: 0x0000EF5C
		public void StartLoop(SoundEvent trackEvent)
		{
			this._trackEvent = trackEvent;
			if (base.HasUser && MBActionSet.CheckActionAnimationClipExists(base.UserAgent.ActionSet, this.LoopStartActionCode))
			{
				base.UserAgent.SetActionChannel(0, this.LoopStartActionCode, true, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00010DC7 File Offset: 0x0000EFC7
		public void EndLoop()
		{
			if (this._trackEvent != null)
			{
				this._trackEvent = null;
				this.ChangeInstrument(null);
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00010DDF File Offset: 0x0000EFDF
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			if (base.HasUser)
			{
				return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
			}
			return base.GetTickRequirement();
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00010DF8 File Offset: 0x0000EFF8
		protected override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (this._trackEvent != null && base.HasUser && MBActionSet.CheckActionAnimationClipExists(base.UserAgent.ActionSet, this.LoopStartActionCode))
			{
				base.UserAgent.SetActionChannel(0, this.LoopStartActionCode, this._hasInstrumentAttached, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00010E70 File Offset: 0x0000F070
		public override void OnUseStopped(Agent userAgent, bool isSuccessful, int preferenceIndex)
		{
			base.OnUseStopped(userAgent, isSuccessful, preferenceIndex);
			this.DefaultActionCode = ActionIndexCache.act_none;
			this.EndLoop();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00010E8C File Offset: 0x0000F08C
		public void ChangeInstrument(Tuple<InstrumentData, float> instrument)
		{
			InstrumentData instrumentData = (instrument != null) ? instrument.Item1 : null;
			if (this._instrumentData != instrumentData)
			{
				this._instrumentData = instrumentData;
				if (base.HasUser && base.UserAgent.IsActive())
				{
					if (base.UserAgent.IsSitting())
					{
						this.LoopStartAction = ((instrumentData == null) ? "act_sit_1" : instrumentData.SittingAction);
					}
					else
					{
						this.LoopStartAction = ((instrumentData == null) ? "act_stand_1" : instrumentData.StandingAction);
					}
					this.ActionSpeed = ((instrument != null) ? instrument.Item2 : 1f);
					this.SetActionCodes();
					base.ClearAssignedItems();
					base.UserAgent.SetActionChannel(0, this.LoopStartActionCode, false, (ulong)((long)base.UserAgent.GetCurrentActionPriority(0)), 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
					if (this._instrumentData != null)
					{
						foreach (ValueTuple<HumanBone, string> valueTuple in this._instrumentData.InstrumentEntities)
						{
							AnimationPoint.ItemForBone newItem = new AnimationPoint.ItemForBone(valueTuple.Item1, valueTuple.Item2, true);
							base.AssignItemToBone(newItem);
						}
						base.AddItemsToAgent();
						this._hasInstrumentAttached = !this._instrumentData.IsDataWithoutInstrument;
					}
				}
			}
		}

		// Token: 0x04000138 RID: 312
		private InstrumentData _instrumentData;

		// Token: 0x04000139 RID: 313
		private SoundEvent _trackEvent;

		// Token: 0x0400013A RID: 314
		private bool _hasInstrumentAttached;
	}
}

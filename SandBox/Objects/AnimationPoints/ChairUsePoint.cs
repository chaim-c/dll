using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Objects.AnimationPoints
{
	// Token: 0x02000044 RID: 68
	public class ChairUsePoint : AnimationPoint
	{
		// Token: 0x0600028B RID: 651 RVA: 0x000109B0 File Offset: 0x0000EBB0
		protected override void SetActionCodes()
		{
			base.SetActionCodes();
			this._loopAction = ActionIndexCache.Create(this.LoopStartAction);
			this._pairLoopAction = ActionIndexCache.Create(this.PairLoopStartAction);
			this._nearTableLoopAction = ActionIndexCache.Create(this.NearTableLoopAction);
			this._nearTablePairLoopAction = ActionIndexCache.Create(this.NearTablePairLoopAction);
			this._drinkLoopAction = ActionIndexCache.Create(this.DrinkLoopAction);
			this._drinkPairLoopAction = ActionIndexCache.Create(this.DrinkPairLoopAction);
			this._eatLoopAction = ActionIndexCache.Create(this.EatLoopAction);
			this._eatPairLoopAction = ActionIndexCache.Create(this.EatPairLoopAction);
			this.SetChairAction(this.GetRandomChairAction());
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00010A58 File Offset: 0x0000EC58
		protected override bool ShouldUpdateOnEditorVariableChanged(string variableName)
		{
			return base.ShouldUpdateOnEditorVariableChanged(variableName) || variableName == "NearTable" || variableName == "Drink" || variableName == "Eat" || variableName == "NearTableLoopAction" || variableName == "DrinkLoopAction" || variableName == "EatLoopAction";
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00010ABC File Offset: 0x0000ECBC
		public override void OnUse(Agent userAgent)
		{
			ChairUsePoint.ChairAction chairAction = base.CanAgentUseItem(userAgent) ? this.GetRandomChairAction() : ChairUsePoint.ChairAction.None;
			this.SetChairAction(chairAction);
			base.OnUse(userAgent);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00010AEC File Offset: 0x0000ECEC
		private ChairUsePoint.ChairAction GetRandomChairAction()
		{
			List<ChairUsePoint.ChairAction> list = new List<ChairUsePoint.ChairAction>
			{
				ChairUsePoint.ChairAction.None
			};
			if (this.NearTable && this._nearTableLoopAction != ActionIndexCache.act_none)
			{
				list.Add(ChairUsePoint.ChairAction.LeanOnTable);
			}
			if (this.Drink && this._drinkLoopAction != ActionIndexCache.act_none)
			{
				list.Add(ChairUsePoint.ChairAction.Drink);
			}
			if (this.Eat && this._eatLoopAction != ActionIndexCache.act_none)
			{
				list.Add(ChairUsePoint.ChairAction.Eat);
			}
			return list[new Random().Next(list.Count)];
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00010B80 File Offset: 0x0000ED80
		private void SetChairAction(ChairUsePoint.ChairAction chairAction)
		{
			switch (chairAction)
			{
			case ChairUsePoint.ChairAction.None:
				this.LoopStartActionCode = this._loopAction;
				this.PairLoopStartActionCode = this._pairLoopAction;
				base.SelectedRightHandItem = this.RightHandItem;
				base.SelectedLeftHandItem = this.LeftHandItem;
				return;
			case ChairUsePoint.ChairAction.LeanOnTable:
				this.LoopStartActionCode = this._nearTableLoopAction;
				this.PairLoopStartActionCode = this._nearTablePairLoopAction;
				base.SelectedRightHandItem = string.Empty;
				base.SelectedLeftHandItem = string.Empty;
				return;
			case ChairUsePoint.ChairAction.Drink:
				this.LoopStartActionCode = this._drinkLoopAction;
				this.PairLoopStartActionCode = this._drinkPairLoopAction;
				base.SelectedRightHandItem = this.DrinkRightHandItem;
				base.SelectedLeftHandItem = this.DrinkLeftHandItem;
				return;
			case ChairUsePoint.ChairAction.Eat:
				this.LoopStartActionCode = this._eatLoopAction;
				this.PairLoopStartActionCode = this._eatPairLoopAction;
				base.SelectedRightHandItem = this.EatRightHandItem;
				base.SelectedLeftHandItem = this.EatLeftHandItem;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00010C68 File Offset: 0x0000EE68
		protected override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (base.UserAgent != null && !base.UserAgent.IsAIControlled && base.UserAgent.EventControlFlags.HasAnyFlag(Agent.EventControlFlag.Crouch | Agent.EventControlFlag.Stand))
			{
				base.UserAgent.StopUsingGameObject(true, Agent.StopUsingGameObjectFlags.AutoAttachAfterStoppingUsingGameObject);
			}
		}

		// Token: 0x04000123 RID: 291
		public bool NearTable;

		// Token: 0x04000124 RID: 292
		public string NearTableLoopAction = "";

		// Token: 0x04000125 RID: 293
		public string NearTablePairLoopAction = "";

		// Token: 0x04000126 RID: 294
		public bool Drink;

		// Token: 0x04000127 RID: 295
		public string DrinkLoopAction = "";

		// Token: 0x04000128 RID: 296
		public string DrinkPairLoopAction = "";

		// Token: 0x04000129 RID: 297
		public string DrinkRightHandItem = "";

		// Token: 0x0400012A RID: 298
		public string DrinkLeftHandItem = "";

		// Token: 0x0400012B RID: 299
		public bool Eat;

		// Token: 0x0400012C RID: 300
		public string EatLoopAction = "";

		// Token: 0x0400012D RID: 301
		public string EatPairLoopAction = "";

		// Token: 0x0400012E RID: 302
		public string EatRightHandItem = "";

		// Token: 0x0400012F RID: 303
		public string EatLeftHandItem = "";

		// Token: 0x04000130 RID: 304
		private ActionIndexCache _loopAction;

		// Token: 0x04000131 RID: 305
		private ActionIndexCache _pairLoopAction;

		// Token: 0x04000132 RID: 306
		private ActionIndexCache _nearTableLoopAction;

		// Token: 0x04000133 RID: 307
		private ActionIndexCache _nearTablePairLoopAction;

		// Token: 0x04000134 RID: 308
		private ActionIndexCache _drinkLoopAction;

		// Token: 0x04000135 RID: 309
		private ActionIndexCache _drinkPairLoopAction;

		// Token: 0x04000136 RID: 310
		private ActionIndexCache _eatLoopAction;

		// Token: 0x04000137 RID: 311
		private ActionIndexCache _eatPairLoopAction;

		// Token: 0x02000122 RID: 290
		private enum ChairAction
		{
			// Token: 0x0400052D RID: 1325
			None,
			// Token: 0x0400052E RID: 1326
			LeanOnTable,
			// Token: 0x0400052F RID: 1327
			Drink,
			// Token: 0x04000530 RID: 1328
			Eat
		}
	}
}

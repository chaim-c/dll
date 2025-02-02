using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000215 RID: 533
	public class EditorGameManager : MBGameManager
	{
		// Token: 0x06001D54 RID: 7508 RVA: 0x00066DA0 File Offset: 0x00064FA0
		protected override void DoLoadingForGameManager(GameManagerLoadingSteps gameManagerLoadingStep, out GameManagerLoadingSteps nextStep)
		{
			nextStep = GameManagerLoadingSteps.None;
			switch (gameManagerLoadingStep)
			{
			case GameManagerLoadingSteps.PreInitializeZerothStep:
				MBGameManager.LoadModuleData(false);
				MBGlobals.InitializeReferences();
				Game.CreateGame(new EditorGame(), this).DoLoading();
				nextStep = GameManagerLoadingSteps.FirstInitializeFirstStep;
				return;
			case GameManagerLoadingSteps.FirstInitializeFirstStep:
			{
				bool flag = true;
				foreach (MBSubModuleBase mbsubModuleBase in Module.CurrentModule.SubModules)
				{
					flag = (flag && mbsubModuleBase.DoLoading(Game.Current));
				}
				nextStep = (flag ? GameManagerLoadingSteps.WaitSecondStep : GameManagerLoadingSteps.FirstInitializeFirstStep);
				return;
			}
			case GameManagerLoadingSteps.WaitSecondStep:
				MBGameManager.StartNewGame();
				nextStep = GameManagerLoadingSteps.SecondInitializeThirdState;
				return;
			case GameManagerLoadingSteps.SecondInitializeThirdState:
				nextStep = (Game.Current.DoLoading() ? GameManagerLoadingSteps.PostInitializeFourthState : GameManagerLoadingSteps.SecondInitializeThirdState);
				return;
			case GameManagerLoadingSteps.PostInitializeFourthState:
				nextStep = GameManagerLoadingSteps.FinishLoadingFifthStep;
				return;
			case GameManagerLoadingSteps.FinishLoadingFifthStep:
				nextStep = GameManagerLoadingSteps.None;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x00066E74 File Offset: 0x00065074
		public override void OnAfterCampaignStart(Game game)
		{
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x00066E76 File Offset: 0x00065076
		public override void OnLoadFinished()
		{
			base.OnLoadFinished();
		}
	}
}

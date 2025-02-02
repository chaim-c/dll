using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000283 RID: 643
	public class ReplayMissionLogic : MissionLogic
	{
		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060021C7 RID: 8647 RVA: 0x0007B17F File Offset: 0x0007937F
		// (set) Token: 0x060021C8 RID: 8648 RVA: 0x0007B187 File Offset: 0x00079387
		public string FileName { get; private set; }

		// Token: 0x060021C9 RID: 8649 RVA: 0x0007B190 File Offset: 0x00079390
		public ReplayMissionLogic(bool isMultiplayer, string fileName = "")
		{
			if (!string.IsNullOrEmpty(fileName))
			{
				this.FileName = fileName;
			}
			this._isMultiplayer = isMultiplayer;
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x0007B1AE File Offset: 0x000793AE
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			if (this._isMultiplayer)
			{
				GameNetwork.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add);
			}
			MBCommon.CurrentGameType = MBCommon.GameType.SingleReplay;
			GameNetwork.InitializeClientSide(null, 0, -1, -1);
			base.Mission.Recorder.RestoreRecordFromFile(this.FileName);
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x0007B1E9 File Offset: 0x000793E9
		public override void OnRemoveBehavior()
		{
			if (this._isMultiplayer)
			{
				GameNetwork.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Remove);
				GameNetwork.EndReplay();
			}
			GameNetwork.TerminateClientSide();
			base.Mission.Recorder.ClearRecordBuffers();
			base.OnRemoveBehavior();
		}

		// Token: 0x04000C97 RID: 3223
		private bool _isMultiplayer;
	}
}

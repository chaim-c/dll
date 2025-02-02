using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000267 RID: 615
	public class BasicLeaveMissionLogic : MissionLogic
	{
		// Token: 0x060020BA RID: 8378 RVA: 0x000751DB File Offset: 0x000733DB
		public BasicLeaveMissionLogic() : this(false)
		{
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x000751E4 File Offset: 0x000733E4
		public BasicLeaveMissionLogic(bool askBeforeLeave) : this(askBeforeLeave, 5)
		{
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x000751EE File Offset: 0x000733EE
		public BasicLeaveMissionLogic(bool askBeforeLeave, int minRetreatDistance)
		{
			this._askBeforeLeave = askBeforeLeave;
			this._minRetreatDistance = minRetreatDistance;
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x00075204 File Offset: 0x00073404
		public override bool MissionEnded(ref MissionResult missionResult)
		{
			return base.Mission.MainAgent != null && !base.Mission.MainAgent.IsActive();
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x00075228 File Offset: 0x00073428
		public override InquiryData OnEndMissionRequest(out bool canPlayerLeave)
		{
			canPlayerLeave = true;
			if (base.Mission.MainAgent != null && base.Mission.MainAgent.IsActive() && (float)this._minRetreatDistance > 0f && base.Mission.IsPlayerCloseToAnEnemy((float)this._minRetreatDistance))
			{
				canPlayerLeave = false;
				MBInformationManager.AddQuickInformation(GameTexts.FindText("str_can_not_retreat", null), 0, null, "");
			}
			else if (this._askBeforeLeave)
			{
				return new InquiryData("", GameTexts.FindText("str_give_up_fight", null).ToString(), true, true, GameTexts.FindText("str_ok", null).ToString(), GameTexts.FindText("str_cancel", null).ToString(), new Action(base.Mission.OnEndMissionResult), null, "", 0f, null, null, null);
			}
			return null;
		}

		// Token: 0x04000C18 RID: 3096
		private readonly bool _askBeforeLeave;

		// Token: 0x04000C19 RID: 3097
		private readonly int _minRetreatDistance;
	}
}

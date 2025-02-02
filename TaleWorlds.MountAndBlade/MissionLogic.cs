using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200027E RID: 638
	public abstract class MissionLogic : MissionBehavior
	{
		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06002199 RID: 8601 RVA: 0x0007AE1C File Offset: 0x0007901C
		public override MissionBehaviorType BehaviorType
		{
			get
			{
				return MissionBehaviorType.Logic;
			}
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x0007AE1F File Offset: 0x0007901F
		public virtual InquiryData OnEndMissionRequest(out bool canLeave)
		{
			canLeave = true;
			return null;
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x0007AE25 File Offset: 0x00079025
		public virtual bool MissionEnded(ref MissionResult missionResult)
		{
			return false;
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x0007AE28 File Offset: 0x00079028
		public virtual void OnBattleEnded()
		{
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x0007AE2A File Offset: 0x0007902A
		public virtual void ShowBattleResults()
		{
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x0007AE2C File Offset: 0x0007902C
		public virtual void OnRetreatMission()
		{
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x0007AE2E File Offset: 0x0007902E
		public virtual void OnSurrenderMission()
		{
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x0007AE30 File Offset: 0x00079030
		public virtual void OnAutoDeployTeam(Team team)
		{
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x0007AE32 File Offset: 0x00079032
		public virtual List<EquipmentElement> GetExtraEquipmentElementsForCharacter(BasicCharacterObject character, bool getAllEquipments = false)
		{
			return null;
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x0007AE35 File Offset: 0x00079035
		public virtual void OnMissionResultReady(MissionResult missionResult)
		{
		}
	}
}

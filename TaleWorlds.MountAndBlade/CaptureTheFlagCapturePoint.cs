using System;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200031C RID: 796
	public class CaptureTheFlagCapturePoint
	{
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06002AEC RID: 10988 RVA: 0x000A648A File Offset: 0x000A468A
		// (set) Token: 0x06002AED RID: 10989 RVA: 0x000A6492 File Offset: 0x000A4692
		public float Progress { get; set; }

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06002AEE RID: 10990 RVA: 0x000A649B File Offset: 0x000A469B
		// (set) Token: 0x06002AEF RID: 10991 RVA: 0x000A64A3 File Offset: 0x000A46A3
		public CaptureTheFlagFlagDirection Direction { get; set; }

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06002AF0 RID: 10992 RVA: 0x000A64AC File Offset: 0x000A46AC
		// (set) Token: 0x06002AF1 RID: 10993 RVA: 0x000A64B4 File Offset: 0x000A46B4
		public float Speed { get; set; }

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x000A64BD File Offset: 0x000A46BD
		// (set) Token: 0x06002AF3 RID: 10995 RVA: 0x000A64C5 File Offset: 0x000A46C5
		public MatrixFrame InitialFlagFrame { get; private set; }

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06002AF4 RID: 10996 RVA: 0x000A64CE File Offset: 0x000A46CE
		// (set) Token: 0x06002AF5 RID: 10997 RVA: 0x000A64D6 File Offset: 0x000A46D6
		public GameEntity FlagEntity { get; private set; }

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x000A64DF File Offset: 0x000A46DF
		// (set) Token: 0x06002AF7 RID: 10999 RVA: 0x000A64E7 File Offset: 0x000A46E7
		public SynchedMissionObject FlagHolder { get; private set; }

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06002AF8 RID: 11000 RVA: 0x000A64F0 File Offset: 0x000A46F0
		// (set) Token: 0x06002AF9 RID: 11001 RVA: 0x000A64F8 File Offset: 0x000A46F8
		public GameEntity FlagBottomBoundary { get; private set; }

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x000A6501 File Offset: 0x000A4701
		// (set) Token: 0x06002AFB RID: 11003 RVA: 0x000A6509 File Offset: 0x000A4709
		public GameEntity FlagTopBoundary { get; private set; }

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x000A6512 File Offset: 0x000A4712
		public BattleSideEnum BattleSide { get; }

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06002AFD RID: 11005 RVA: 0x000A651A File Offset: 0x000A471A
		public int Index { get; }

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x000A6522 File Offset: 0x000A4722
		// (set) Token: 0x06002AFF RID: 11007 RVA: 0x000A652A File Offset: 0x000A472A
		public bool UpdateFlag { get; set; }

		// Token: 0x06002B00 RID: 11008 RVA: 0x000A6534 File Offset: 0x000A4734
		public CaptureTheFlagCapturePoint(GameEntity flagPole, BattleSideEnum battleSide, int index)
		{
			this.Reset();
			this.BattleSide = battleSide;
			this.Index = index;
			this.FlagHolder = flagPole.CollectChildrenEntitiesWithTag("score_stand").SingleOrDefault<GameEntity>().GetFirstScriptOfType<SynchedMissionObject>();
			this.FlagEntity = this.FlagHolder.GameEntity.GetChildren().Single((GameEntity q) => q.HasTag("flag"));
			this.FlagHolder.GameEntity.EntityFlags |= EntityFlags.NoOcclusionCulling;
			this.FlagEntity.EntityFlags |= EntityFlags.NoOcclusionCulling;
			this.FlagBottomBoundary = flagPole.GetChildren().Single((GameEntity q) => q.HasTag("flag_raising_bottom"));
			this.FlagTopBoundary = flagPole.GetChildren().Single((GameEntity q) => q.HasTag("flag_raising_top"));
			MatrixFrame globalFrame = this.FlagHolder.GameEntity.GetGlobalFrame();
			globalFrame.origin.z = this.FlagBottomBoundary.GetGlobalFrame().origin.z;
			this.InitialFlagFrame = globalFrame;
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x000A667C File Offset: 0x000A487C
		public void Reset()
		{
			this.Progress = 0f;
			this.Direction = CaptureTheFlagFlagDirection.None;
			this.Speed = 0f;
			this.UpdateFlag = false;
		}
	}
}

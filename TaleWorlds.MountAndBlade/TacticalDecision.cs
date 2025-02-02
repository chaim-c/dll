using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200017E RID: 382
	public struct TacticalDecision
	{
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x0004A8BB File Offset: 0x00048ABB
		// (set) Token: 0x060013AD RID: 5037 RVA: 0x0004A8C3 File Offset: 0x00048AC3
		public TacticComponent DecidingComponent { get; private set; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x0004A8CC File Offset: 0x00048ACC
		// (set) Token: 0x060013AF RID: 5039 RVA: 0x0004A8D4 File Offset: 0x00048AD4
		public byte DecisionCode { get; private set; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x0004A8DD File Offset: 0x00048ADD
		// (set) Token: 0x060013B1 RID: 5041 RVA: 0x0004A8E5 File Offset: 0x00048AE5
		public Formation SubjectFormation { get; private set; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x0004A8EE File Offset: 0x00048AEE
		// (set) Token: 0x060013B3 RID: 5043 RVA: 0x0004A8F6 File Offset: 0x00048AF6
		public Formation TargetFormation { get; private set; }

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x0004A8FF File Offset: 0x00048AFF
		// (set) Token: 0x060013B5 RID: 5045 RVA: 0x0004A907 File Offset: 0x00048B07
		public WorldPosition? TargetPosition { get; private set; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x0004A910 File Offset: 0x00048B10
		// (set) Token: 0x060013B7 RID: 5047 RVA: 0x0004A918 File Offset: 0x00048B18
		public MissionObject TargetObject { get; private set; }

		// Token: 0x060013B8 RID: 5048 RVA: 0x0004A921 File Offset: 0x00048B21
		public TacticalDecision(TacticComponent decidingComponent, byte decisionCode, Formation subjectFormation = null, Formation targetFormation = null, WorldPosition? targetPosition = null, MissionObject targetObject = null)
		{
			this.DecidingComponent = decidingComponent;
			this.DecisionCode = decisionCode;
			this.SubjectFormation = subjectFormation;
			this.TargetFormation = targetFormation;
			this.TargetPosition = targetPosition;
			this.TargetObject = targetObject;
		}
	}
}

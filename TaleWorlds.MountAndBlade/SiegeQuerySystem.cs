using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000178 RID: 376
	public class SiegeQuerySystem
	{
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x00047CE1 File Offset: 0x00045EE1
		public int LeftRegionMemberCount
		{
			get
			{
				return this._leftRegionMemberCount.Value;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x00047CEE File Offset: 0x00045EEE
		public int LeftCloseAttackerCount
		{
			get
			{
				return this._leftCloseAttackerCount.Value;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x00047CFB File Offset: 0x00045EFB
		public int MiddleRegionMemberCount
		{
			get
			{
				return this._middleRegionMemberCount.Value;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x00047D08 File Offset: 0x00045F08
		public int MiddleCloseAttackerCount
		{
			get
			{
				return this._middleCloseAttackerCount.Value;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x00047D15 File Offset: 0x00045F15
		public int RightRegionMemberCount
		{
			get
			{
				return this._rightRegionMemberCount.Value;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x00047D22 File Offset: 0x00045F22
		public int RightCloseAttackerCount
		{
			get
			{
				return this._rightCloseAttackerCount.Value;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x00047D2F File Offset: 0x00045F2F
		public int InsideAttackerCount
		{
			get
			{
				return this._insideAttackerCount.Value;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x00047D3C File Offset: 0x00045F3C
		public int LeftDefenderCount
		{
			get
			{
				return this._leftDefenderCount.Value;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x00047D49 File Offset: 0x00045F49
		public int MiddleDefenderCount
		{
			get
			{
				return this._middleDefenderCount.Value;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x00047D56 File Offset: 0x00045F56
		public int RightDefenderCount
		{
			get
			{
				return this._rightDefenderCount.Value;
			}
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00047D64 File Offset: 0x00045F64
		public SiegeQuerySystem(Team team, IEnumerable<SiegeLane> lanes)
		{
			Mission mission = Mission.Current;
			this._attackerTeam = mission.AttackerTeam;
			Team defenderTeam = mission.DefenderTeam;
			SiegeLane siegeLane = lanes.FirstOrDefault((SiegeLane l) => l.LaneSide == FormationAI.BehaviorSide.Left);
			SiegeLane siegeLane2 = lanes.FirstOrDefault((SiegeLane l) => l.LaneSide == FormationAI.BehaviorSide.Middle);
			SiegeLane siegeLane3 = lanes.FirstOrDefault((SiegeLane l) => l.LaneSide == FormationAI.BehaviorSide.Right);
			Mission mission2 = Mission.Current;
			GameEntity gameEntity = mission2.Scene.FindEntityWithTag("left_defender_origin");
			this.LeftDefenderOrigin = ((gameEntity != null) ? gameEntity.GlobalPosition : (siegeLane.DefenderOrigin.AsVec2.IsNonZero() ? siegeLane.DefenderOrigin.GetGroundVec3() : new Vec3(0f, 0f, 0f, -1f)));
			GameEntity gameEntity2 = mission2.Scene.FindEntityWithTag("left_attacker_origin");
			this.LeftAttackerOrigin = ((gameEntity2 != null) ? gameEntity2.GlobalPosition : (siegeLane.AttackerOrigin.AsVec2.IsNonZero() ? siegeLane.AttackerOrigin.GetGroundVec3() : new Vec3(0f, 0f, 0f, -1f)));
			GameEntity gameEntity3 = mission2.Scene.FindEntityWithTag("middle_defender_origin");
			this.MidDefenderOrigin = ((gameEntity3 != null) ? gameEntity3.GlobalPosition : (siegeLane2.DefenderOrigin.AsVec2.IsNonZero() ? siegeLane2.DefenderOrigin.GetGroundVec3() : new Vec3(0f, 0f, 0f, -1f)));
			GameEntity gameEntity4 = mission2.Scene.FindEntityWithTag("middle_attacker_origin");
			this.MiddleAttackerOrigin = ((gameEntity4 != null) ? gameEntity4.GlobalPosition : (siegeLane2.AttackerOrigin.AsVec2.IsNonZero() ? siegeLane2.AttackerOrigin.GetGroundVec3() : new Vec3(0f, 0f, 0f, -1f)));
			GameEntity gameEntity5 = mission2.Scene.FindEntityWithTag("right_defender_origin");
			this.RightDefenderOrigin = ((gameEntity5 != null) ? gameEntity5.GlobalPosition : (siegeLane3.DefenderOrigin.AsVec2.IsNonZero() ? siegeLane3.DefenderOrigin.GetGroundVec3() : new Vec3(0f, 0f, 0f, -1f)));
			GameEntity gameEntity6 = mission2.Scene.FindEntityWithTag("right_attacker_origin");
			this.RightAttackerOrigin = ((gameEntity6 != null) ? gameEntity6.GlobalPosition : (siegeLane3.AttackerOrigin.AsVec2.IsNonZero() ? siegeLane3.AttackerOrigin.GetGroundVec3() : new Vec3(0f, 0f, 0f, -1f)));
			this.LeftToMidDir = (this.MiddleAttackerOrigin.AsVec2 - this.LeftDefenderOrigin.AsVec2).Normalized();
			this.MidToLeftDir = (this.LeftAttackerOrigin.AsVec2 - this.MidDefenderOrigin.AsVec2).Normalized();
			this.MidToRightDir = (this.RightAttackerOrigin.AsVec2 - this.MidDefenderOrigin.AsVec2).Normalized();
			this.RightToMidDir = (this.MiddleAttackerOrigin.AsVec2 - this.RightDefenderOrigin.AsVec2).Normalized();
			this._leftRegionMemberCount = new QueryData<int>(() => this.LocateAttackers(SiegeQuerySystem.RegionEnum.Left), 5f);
			this._leftCloseAttackerCount = new QueryData<int>(() => this.LocateAttackers(SiegeQuerySystem.RegionEnum.LeftClose), 5f);
			this._middleRegionMemberCount = new QueryData<int>(() => this.LocateAttackers(SiegeQuerySystem.RegionEnum.Middle), 5f);
			this._middleCloseAttackerCount = new QueryData<int>(() => this.LocateAttackers(SiegeQuerySystem.RegionEnum.MiddleClose), 5f);
			this._rightRegionMemberCount = new QueryData<int>(() => this.LocateAttackers(SiegeQuerySystem.RegionEnum.Right), 5f);
			this._rightCloseAttackerCount = new QueryData<int>(() => this.LocateAttackers(SiegeQuerySystem.RegionEnum.RightClose), 5f);
			this._insideAttackerCount = new QueryData<int>(() => this.LocateAttackers(SiegeQuerySystem.RegionEnum.Inside), 5f);
			this._leftDefenderCount = new QueryData<int>(() => mission.GetNearbyAllyAgentsCount(this.LeftDefenderOrigin.AsVec2, 10f, defenderTeam), 5f);
			this._middleDefenderCount = new QueryData<int>(() => mission.GetNearbyAllyAgentsCount(this.MidDefenderOrigin.AsVec2, 10f, defenderTeam), 5f);
			this._rightDefenderCount = new QueryData<int>(() => mission.GetNearbyAllyAgentsCount(this.RightDefenderOrigin.AsVec2, 10f, defenderTeam), 5f);
			this.DefenderLeftToDefenderMidDir = (this.MidDefenderOrigin.AsVec2 - this.LeftDefenderOrigin.AsVec2).Normalized();
			this.DefenderMidToDefenderRightDir = (this.RightDefenderOrigin.AsVec2 - this.MidDefenderOrigin.AsVec2).Normalized();
			this.InitializeTelemetryScopeNames();
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x000482D8 File Offset: 0x000464D8
		private int LocateAttackers(SiegeQuerySystem.RegionEnum region)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			foreach (Agent agent in this._attackerTeam.ActiveAgents)
			{
				Vec2 vec = agent.Position.AsVec2 - this.LeftDefenderOrigin.AsVec2;
				Vec2 vec2 = agent.Position.AsVec2 - this.MidDefenderOrigin.AsVec2;
				Vec2 vec3 = agent.Position.AsVec2 - this.RightDefenderOrigin.AsVec2;
				if (vec.Normalize() < 15f && Math.Abs(agent.Position.z - this.LeftDefenderOrigin.z) <= 3f)
				{
					num2++;
					num++;
				}
				else
				{
					if (vec.DotProduct(this.LeftToMidDir) >= 0f && vec.DotProduct(this.LeftToMidDir.RightVec()) >= 0f)
					{
						num++;
					}
					else if (vec2.DotProduct(this.MidToLeftDir) >= 0f && vec2.DotProduct(this.MidToLeftDir.RightVec()) >= 0f)
					{
						num++;
					}
					if (vec3.Normalize() < 15f && Math.Abs(agent.Position.z - this.RightDefenderOrigin.z) <= 3f)
					{
						num6++;
						num5++;
					}
					else
					{
						if (vec3.DotProduct(this.RightToMidDir) >= 0f && vec3.DotProduct(this.RightToMidDir.LeftVec()) >= 0f)
						{
							num5++;
						}
						else if (vec2.DotProduct(this.MidToRightDir) >= 0f && vec2.DotProduct(this.MidToRightDir.LeftVec()) >= 0f)
						{
							num5++;
						}
						if (vec2.Normalize() < 15f && Math.Abs(agent.Position.z - this.MidDefenderOrigin.z) <= 3f)
						{
							num4++;
							num3++;
						}
						else
						{
							if ((vec2.DotProduct(this.MidToLeftDir) < 0f || vec2.DotProduct(this.MidToLeftDir.RightVec()) < 0f || vec.DotProduct(this.LeftToMidDir) < 0f || vec.DotProduct(this.LeftToMidDir.RightVec()) < 0f) && (vec2.DotProduct(this.MidToRightDir) < 0f || vec2.DotProduct(this.MidToRightDir.LeftVec()) < 0f || vec3.DotProduct(this.RightToMidDir) < 0f || vec3.DotProduct(this.RightToMidDir.LeftVec()) < 0f))
							{
								num3++;
							}
							if (agent.GetCurrentNavigationFaceId() % 10 == 1)
							{
								num7++;
							}
						}
					}
				}
			}
			float currentTime = Mission.Current.CurrentTime;
			this._leftRegionMemberCount.SetValue(num, currentTime);
			this._leftCloseAttackerCount.SetValue(num2, currentTime);
			this._middleRegionMemberCount.SetValue(num3, currentTime);
			this._middleCloseAttackerCount.SetValue(num4, currentTime);
			this._rightRegionMemberCount.SetValue(num5, currentTime);
			this._rightCloseAttackerCount.SetValue(num6, currentTime);
			this._insideAttackerCount.SetValue(num7, currentTime);
			switch (region)
			{
			case SiegeQuerySystem.RegionEnum.Left:
				return num;
			case SiegeQuerySystem.RegionEnum.LeftClose:
				return num2;
			case SiegeQuerySystem.RegionEnum.Middle:
				return num3;
			case SiegeQuerySystem.RegionEnum.MiddleClose:
				return num4;
			case SiegeQuerySystem.RegionEnum.Right:
				return num5;
			case SiegeQuerySystem.RegionEnum.RightClose:
				return num6;
			case SiegeQuerySystem.RegionEnum.Inside:
				return num7;
			default:
				return 0;
			}
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x000486E0 File Offset: 0x000468E0
		public void Expire()
		{
			this._leftRegionMemberCount.Expire();
			this._leftCloseAttackerCount.Expire();
			this._middleRegionMemberCount.Expire();
			this._middleCloseAttackerCount.Expire();
			this._rightRegionMemberCount.Expire();
			this._rightCloseAttackerCount.Expire();
			this._insideAttackerCount.Expire();
			this._leftDefenderCount.Expire();
			this._middleDefenderCount.Expire();
			this._rightDefenderCount.Expire();
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0004875B File Offset: 0x0004695B
		private void InitializeTelemetryScopeNames()
		{
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00048760 File Offset: 0x00046960
		public int DeterminePositionAssociatedSide(Vec3 position)
		{
			float num = position.AsVec2.DistanceSquared(this.LeftDefenderOrigin.AsVec2);
			float num2 = position.AsVec2.DistanceSquared(this.MidDefenderOrigin.AsVec2);
			float num3 = position.AsVec2.DistanceSquared(this.RightDefenderOrigin.AsVec2);
			FormationAI.BehaviorSide behaviorSide;
			if (num < num2 && num < num3)
			{
				behaviorSide = FormationAI.BehaviorSide.Left;
			}
			else if (num3 < num2)
			{
				behaviorSide = FormationAI.BehaviorSide.Right;
			}
			else
			{
				behaviorSide = FormationAI.BehaviorSide.Middle;
			}
			FormationAI.BehaviorSide behaviorSide2 = FormationAI.BehaviorSide.BehaviorSideNotSet;
			switch (behaviorSide)
			{
			case FormationAI.BehaviorSide.Left:
				if ((position.AsVec2 - this.LeftDefenderOrigin.AsVec2).Normalized().DotProduct(this.DefenderLeftToDefenderMidDir) > 0f)
				{
					behaviorSide2 = FormationAI.BehaviorSide.Middle;
				}
				break;
			case FormationAI.BehaviorSide.Middle:
				if ((position.AsVec2 - this.MidDefenderOrigin.AsVec2).Normalized().DotProduct(this.DefenderMidToDefenderRightDir) > 0f)
				{
					behaviorSide2 = FormationAI.BehaviorSide.Right;
				}
				else
				{
					behaviorSide2 = FormationAI.BehaviorSide.Left;
				}
				break;
			case FormationAI.BehaviorSide.Right:
				if ((position.AsVec2 - this.RightDefenderOrigin.AsVec2).Normalized().DotProduct(this.DefenderMidToDefenderRightDir) < 0f)
				{
					behaviorSide2 = FormationAI.BehaviorSide.Middle;
				}
				break;
			}
			int num4 = 1 << (int)behaviorSide;
			if (behaviorSide2 != FormationAI.BehaviorSide.BehaviorSideNotSet)
			{
				num4 |= 1 << (int)behaviorSide2;
			}
			return num4;
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000488DE File Offset: 0x00046ADE
		public static bool AreSidesRelated(FormationAI.BehaviorSide side, int connectedSides)
		{
			return (1 << (int)side & connectedSides) != 0;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x000488EC File Offset: 0x00046AEC
		public static int SideDistance(int connectedSides, int side)
		{
			while (connectedSides != 0 && side != 0)
			{
				connectedSides >>= 1;
				side >>= 1;
			}
			int i = (connectedSides != 0) ? connectedSides : side;
			int num = 0;
			while (i > 0)
			{
				num++;
				if ((i & 1) == 1)
				{
					break;
				}
				i >>= 1;
			}
			return num;
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x0004892A File Offset: 0x00046B2A
		public Vec3 LeftDefenderOrigin { get; }

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x00048932 File Offset: 0x00046B32
		public Vec3 MidDefenderOrigin { get; }

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x0004893A File Offset: 0x00046B3A
		public Vec3 RightDefenderOrigin { get; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x00048942 File Offset: 0x00046B42
		public Vec3 LeftAttackerOrigin { get; }

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600131B RID: 4891 RVA: 0x0004894A File Offset: 0x00046B4A
		public Vec3 MiddleAttackerOrigin { get; }

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x00048952 File Offset: 0x00046B52
		public Vec3 RightAttackerOrigin { get; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600131D RID: 4893 RVA: 0x0004895A File Offset: 0x00046B5A
		public Vec2 LeftToMidDir { get; }

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x00048962 File Offset: 0x00046B62
		public Vec2 MidToLeftDir { get; }

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x0004896A File Offset: 0x00046B6A
		public Vec2 MidToRightDir { get; }

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x00048972 File Offset: 0x00046B72
		public Vec2 RightToMidDir { get; }

		// Token: 0x04000521 RID: 1313
		private const float LaneProximityDistance = 15f;

		// Token: 0x04000522 RID: 1314
		private readonly Team _attackerTeam;

		// Token: 0x04000523 RID: 1315
		public Vec2 DefenderLeftToDefenderMidDir;

		// Token: 0x04000524 RID: 1316
		public Vec2 DefenderMidToDefenderRightDir;

		// Token: 0x04000525 RID: 1317
		private readonly QueryData<int> _leftRegionMemberCount;

		// Token: 0x04000526 RID: 1318
		private readonly QueryData<int> _leftCloseAttackerCount;

		// Token: 0x04000527 RID: 1319
		private readonly QueryData<int> _middleRegionMemberCount;

		// Token: 0x04000528 RID: 1320
		private readonly QueryData<int> _middleCloseAttackerCount;

		// Token: 0x04000529 RID: 1321
		private readonly QueryData<int> _rightRegionMemberCount;

		// Token: 0x0400052A RID: 1322
		private readonly QueryData<int> _rightCloseAttackerCount;

		// Token: 0x0400052B RID: 1323
		private readonly QueryData<int> _insideAttackerCount;

		// Token: 0x0400052C RID: 1324
		private readonly QueryData<int> _leftDefenderCount;

		// Token: 0x0400052D RID: 1325
		private readonly QueryData<int> _middleDefenderCount;

		// Token: 0x0400052E RID: 1326
		private readonly QueryData<int> _rightDefenderCount;

		// Token: 0x0200049E RID: 1182
		private enum RegionEnum
		{
			// Token: 0x04001A63 RID: 6755
			Left,
			// Token: 0x04001A64 RID: 6756
			LeftClose,
			// Token: 0x04001A65 RID: 6757
			Middle,
			// Token: 0x04001A66 RID: 6758
			MiddleClose,
			// Token: 0x04001A67 RID: 6759
			Right,
			// Token: 0x04001A68 RID: 6760
			RightClose,
			// Token: 0x04001A69 RID: 6761
			Inside
		}
	}
}

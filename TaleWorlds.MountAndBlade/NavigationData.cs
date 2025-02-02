using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002D1 RID: 721
	[EngineStruct("Navigation_data", false)]
	[Serializable]
	public struct NavigationData
	{
		// Token: 0x060027C4 RID: 10180 RVA: 0x000994AC File Offset: 0x000976AC
		public NavigationData(Vec3 startPoint, Vec3 endPoint, float agentRadius)
		{
			this.Points = new Vec2[1024];
			this.StartPoint = startPoint;
			this.EndPoint = endPoint;
			this.Points[0] = startPoint.AsVec2;
			this.Points[1] = endPoint.AsVec2;
			this.PointSize = 2;
			this.AgentRadius = agentRadius;
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x0009950C File Offset: 0x0009770C
		[Conditional("DEBUG")]
		public void TickDebug()
		{
			for (int i = 0; i < this.PointSize - 1; i++)
			{
			}
		}

		// Token: 0x04000EAF RID: 3759
		private const int MaxPathSize = 1024;

		// Token: 0x04000EB0 RID: 3760
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
		public Vec2[] Points;

		// Token: 0x04000EB1 RID: 3761
		public Vec3 StartPoint;

		// Token: 0x04000EB2 RID: 3762
		public Vec3 EndPoint;

		// Token: 0x04000EB3 RID: 3763
		public readonly int PointSize;

		// Token: 0x04000EB4 RID: 3764
		public readonly float AgentRadius;
	}
}

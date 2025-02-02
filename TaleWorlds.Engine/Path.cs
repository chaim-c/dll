using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000071 RID: 113
	[EngineClass("rglPath")]
	public sealed class Path : NativeObject
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00008B4A File Offset: 0x00006D4A
		public int NumberOfPoints
		{
			get
			{
				return EngineApplicationInterface.IPath.GetNumberOfPoints(base.Pointer);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00008B5C File Offset: 0x00006D5C
		public float TotalDistance
		{
			get
			{
				return EngineApplicationInterface.IPath.GetTotalLength(base.Pointer);
			}
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00008B6E File Offset: 0x00006D6E
		internal Path(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00008B80 File Offset: 0x00006D80
		public MatrixFrame GetHermiteFrameForDt(float phase, int first_point)
		{
			MatrixFrame identity = MatrixFrame.Identity;
			EngineApplicationInterface.IPath.GetHermiteFrameForDt(base.Pointer, ref identity, phase, first_point);
			return identity;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00008BA8 File Offset: 0x00006DA8
		public MatrixFrame GetFrameForDistance(float distance)
		{
			MatrixFrame identity = MatrixFrame.Identity;
			EngineApplicationInterface.IPath.GetHermiteFrameForDistance(base.Pointer, ref identity, distance);
			return identity;
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00008BD0 File Offset: 0x00006DD0
		public MatrixFrame GetNearestFrameWithValidAlphaForDistance(float distance, bool searchForward = true, float alphaThreshold = 0.5f)
		{
			MatrixFrame identity = MatrixFrame.Identity;
			EngineApplicationInterface.IPath.GetNearestHermiteFrameWithValidAlphaForDistance(base.Pointer, ref identity, distance, searchForward, alphaThreshold);
			return identity;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00008BF9 File Offset: 0x00006DF9
		public void GetFrameAndColorForDistance(float distance, out MatrixFrame frame, out Vec3 color)
		{
			frame = MatrixFrame.Identity;
			EngineApplicationInterface.IPath.GetHermiteFrameAndColorForDistance(base.Pointer, out frame, out color, distance);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00008C19 File Offset: 0x00006E19
		public float GetArcLength(int first_point)
		{
			return EngineApplicationInterface.IPath.GetArcLength(base.Pointer, first_point);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00008C2C File Offset: 0x00006E2C
		public void GetPoints(MatrixFrame[] points)
		{
			EngineApplicationInterface.IPath.GetPoints(base.Pointer, points);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00008C3F File Offset: 0x00006E3F
		public float GetTotalLength()
		{
			return EngineApplicationInterface.IPath.GetTotalLength(base.Pointer);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00008C51 File Offset: 0x00006E51
		public int GetVersion()
		{
			return EngineApplicationInterface.IPath.GetVersion(base.Pointer);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00008C63 File Offset: 0x00006E63
		public void SetFrameOfPoint(int pointIndex, ref MatrixFrame frame)
		{
			EngineApplicationInterface.IPath.SetFrameOfPoint(base.Pointer, pointIndex, ref frame);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00008C77 File Offset: 0x00006E77
		public void SetTangentPositionOfPoint(int pointIndex, int tangentIndex, ref Vec3 position)
		{
			EngineApplicationInterface.IPath.SetTangentPositionOfPoint(base.Pointer, pointIndex, tangentIndex, ref position);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00008C8C File Offset: 0x00006E8C
		public int AddPathPoint(int newNodeIndex)
		{
			return EngineApplicationInterface.IPath.AddPathPoint(base.Pointer, newNodeIndex);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00008C9F File Offset: 0x00006E9F
		public void DeletePathPoint(int nodeIndex)
		{
			EngineApplicationInterface.IPath.DeletePathPoint(base.Pointer, nodeIndex);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00008CB2 File Offset: 0x00006EB2
		public bool HasValidAlphaAtPathPoint(int nodeIndex, float alphaThreshold = 0.5f)
		{
			return EngineApplicationInterface.IPath.HasValidAlphaAtPathPoint(base.Pointer, nodeIndex, alphaThreshold);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00008CC6 File Offset: 0x00006EC6
		public string GetName()
		{
			return EngineApplicationInterface.IPath.GetName(base.Pointer);
		}
	}
}

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x0200001F RID: 31
	internal class ScriptingInterfaceOfIPath : IPath
	{
		// Token: 0x0600031D RID: 797 RVA: 0x00012341 File Offset: 0x00010541
		public int AddPathPoint(UIntPtr ptr, int newNodeIndex)
		{
			return ScriptingInterfaceOfIPath.call_AddPathPointDelegate(ptr, newNodeIndex);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0001234F File Offset: 0x0001054F
		public void DeletePathPoint(UIntPtr ptr, int newNodeIndex)
		{
			ScriptingInterfaceOfIPath.call_DeletePathPointDelegate(ptr, newNodeIndex);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0001235D File Offset: 0x0001055D
		public float GetArcLength(UIntPtr ptr, int firstPoint)
		{
			return ScriptingInterfaceOfIPath.call_GetArcLengthDelegate(ptr, firstPoint);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0001236B File Offset: 0x0001056B
		public void GetHermiteFrameAndColorForDistance(UIntPtr ptr, out MatrixFrame frame, out Vec3 color, float distance)
		{
			ScriptingInterfaceOfIPath.call_GetHermiteFrameAndColorForDistanceDelegate(ptr, out frame, out color, distance);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0001237C File Offset: 0x0001057C
		public void GetHermiteFrameForDistance(UIntPtr ptr, ref MatrixFrame frame, float distance)
		{
			ScriptingInterfaceOfIPath.call_GetHermiteFrameForDistanceDelegate(ptr, ref frame, distance);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0001238B File Offset: 0x0001058B
		public void GetHermiteFrameForDt(UIntPtr ptr, ref MatrixFrame frame, float phase, int firstPoint)
		{
			ScriptingInterfaceOfIPath.call_GetHermiteFrameForDtDelegate(ptr, ref frame, phase, firstPoint);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001239C File Offset: 0x0001059C
		public string GetName(UIntPtr ptr)
		{
			if (ScriptingInterfaceOfIPath.call_GetNameDelegate(ptr) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000123B3 File Offset: 0x000105B3
		public void GetNearestHermiteFrameWithValidAlphaForDistance(UIntPtr ptr, ref MatrixFrame frame, float distance, bool searchForward, float alphaThreshold)
		{
			ScriptingInterfaceOfIPath.call_GetNearestHermiteFrameWithValidAlphaForDistanceDelegate(ptr, ref frame, distance, searchForward, alphaThreshold);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000123C6 File Offset: 0x000105C6
		public int GetNumberOfPoints(UIntPtr ptr)
		{
			return ScriptingInterfaceOfIPath.call_GetNumberOfPointsDelegate(ptr);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000123D4 File Offset: 0x000105D4
		public void GetPoints(UIntPtr ptr, MatrixFrame[] points)
		{
			PinnedArrayData<MatrixFrame> pinnedArrayData = new PinnedArrayData<MatrixFrame>(points, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ScriptingInterfaceOfIPath.call_GetPointsDelegate(ptr, pointer);
			pinnedArrayData.Dispose();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00012405 File Offset: 0x00010605
		public float GetTotalLength(UIntPtr ptr)
		{
			return ScriptingInterfaceOfIPath.call_GetTotalLengthDelegate(ptr);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00012412 File Offset: 0x00010612
		public int GetVersion(UIntPtr ptr)
		{
			return ScriptingInterfaceOfIPath.call_GetVersionDelegate(ptr);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0001241F File Offset: 0x0001061F
		public bool HasValidAlphaAtPathPoint(UIntPtr ptr, int nodeIndex, float alphaThreshold)
		{
			return ScriptingInterfaceOfIPath.call_HasValidAlphaAtPathPointDelegate(ptr, nodeIndex, alphaThreshold);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0001242E File Offset: 0x0001062E
		public void SetFrameOfPoint(UIntPtr ptr, int pointIndex, ref MatrixFrame frame)
		{
			ScriptingInterfaceOfIPath.call_SetFrameOfPointDelegate(ptr, pointIndex, ref frame);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0001243D File Offset: 0x0001063D
		public void SetTangentPositionOfPoint(UIntPtr ptr, int pointIndex, int tangentIndex, ref Vec3 position)
		{
			ScriptingInterfaceOfIPath.call_SetTangentPositionOfPointDelegate(ptr, pointIndex, tangentIndex, ref position);
		}

		// Token: 0x040002B3 RID: 691
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040002B4 RID: 692
		public static ScriptingInterfaceOfIPath.AddPathPointDelegate call_AddPathPointDelegate;

		// Token: 0x040002B5 RID: 693
		public static ScriptingInterfaceOfIPath.DeletePathPointDelegate call_DeletePathPointDelegate;

		// Token: 0x040002B6 RID: 694
		public static ScriptingInterfaceOfIPath.GetArcLengthDelegate call_GetArcLengthDelegate;

		// Token: 0x040002B7 RID: 695
		public static ScriptingInterfaceOfIPath.GetHermiteFrameAndColorForDistanceDelegate call_GetHermiteFrameAndColorForDistanceDelegate;

		// Token: 0x040002B8 RID: 696
		public static ScriptingInterfaceOfIPath.GetHermiteFrameForDistanceDelegate call_GetHermiteFrameForDistanceDelegate;

		// Token: 0x040002B9 RID: 697
		public static ScriptingInterfaceOfIPath.GetHermiteFrameForDtDelegate call_GetHermiteFrameForDtDelegate;

		// Token: 0x040002BA RID: 698
		public static ScriptingInterfaceOfIPath.GetNameDelegate call_GetNameDelegate;

		// Token: 0x040002BB RID: 699
		public static ScriptingInterfaceOfIPath.GetNearestHermiteFrameWithValidAlphaForDistanceDelegate call_GetNearestHermiteFrameWithValidAlphaForDistanceDelegate;

		// Token: 0x040002BC RID: 700
		public static ScriptingInterfaceOfIPath.GetNumberOfPointsDelegate call_GetNumberOfPointsDelegate;

		// Token: 0x040002BD RID: 701
		public static ScriptingInterfaceOfIPath.GetPointsDelegate call_GetPointsDelegate;

		// Token: 0x040002BE RID: 702
		public static ScriptingInterfaceOfIPath.GetTotalLengthDelegate call_GetTotalLengthDelegate;

		// Token: 0x040002BF RID: 703
		public static ScriptingInterfaceOfIPath.GetVersionDelegate call_GetVersionDelegate;

		// Token: 0x040002C0 RID: 704
		public static ScriptingInterfaceOfIPath.HasValidAlphaAtPathPointDelegate call_HasValidAlphaAtPathPointDelegate;

		// Token: 0x040002C1 RID: 705
		public static ScriptingInterfaceOfIPath.SetFrameOfPointDelegate call_SetFrameOfPointDelegate;

		// Token: 0x040002C2 RID: 706
		public static ScriptingInterfaceOfIPath.SetTangentPositionOfPointDelegate call_SetTangentPositionOfPointDelegate;

		// Token: 0x0200030E RID: 782
		// (Invoke) Token: 0x0600113B RID: 4411
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int AddPathPointDelegate(UIntPtr ptr, int newNodeIndex);

		// Token: 0x0200030F RID: 783
		// (Invoke) Token: 0x0600113F RID: 4415
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DeletePathPointDelegate(UIntPtr ptr, int newNodeIndex);

		// Token: 0x02000310 RID: 784
		// (Invoke) Token: 0x06001143 RID: 4419
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetArcLengthDelegate(UIntPtr ptr, int firstPoint);

		// Token: 0x02000311 RID: 785
		// (Invoke) Token: 0x06001147 RID: 4423
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetHermiteFrameAndColorForDistanceDelegate(UIntPtr ptr, out MatrixFrame frame, out Vec3 color, float distance);

		// Token: 0x02000312 RID: 786
		// (Invoke) Token: 0x0600114B RID: 4427
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetHermiteFrameForDistanceDelegate(UIntPtr ptr, ref MatrixFrame frame, float distance);

		// Token: 0x02000313 RID: 787
		// (Invoke) Token: 0x0600114F RID: 4431
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetHermiteFrameForDtDelegate(UIntPtr ptr, ref MatrixFrame frame, float phase, int firstPoint);

		// Token: 0x02000314 RID: 788
		// (Invoke) Token: 0x06001153 RID: 4435
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNameDelegate(UIntPtr ptr);

		// Token: 0x02000315 RID: 789
		// (Invoke) Token: 0x06001157 RID: 4439
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetNearestHermiteFrameWithValidAlphaForDistanceDelegate(UIntPtr ptr, ref MatrixFrame frame, float distance, [MarshalAs(UnmanagedType.U1)] bool searchForward, float alphaThreshold);

		// Token: 0x02000316 RID: 790
		// (Invoke) Token: 0x0600115B RID: 4443
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNumberOfPointsDelegate(UIntPtr ptr);

		// Token: 0x02000317 RID: 791
		// (Invoke) Token: 0x0600115F RID: 4447
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetPointsDelegate(UIntPtr ptr, IntPtr points);

		// Token: 0x02000318 RID: 792
		// (Invoke) Token: 0x06001163 RID: 4451
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetTotalLengthDelegate(UIntPtr ptr);

		// Token: 0x02000319 RID: 793
		// (Invoke) Token: 0x06001167 RID: 4455
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetVersionDelegate(UIntPtr ptr);

		// Token: 0x0200031A RID: 794
		// (Invoke) Token: 0x0600116B RID: 4459
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool HasValidAlphaAtPathPointDelegate(UIntPtr ptr, int nodeIndex, float alphaThreshold);

		// Token: 0x0200031B RID: 795
		// (Invoke) Token: 0x0600116F RID: 4463
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFrameOfPointDelegate(UIntPtr ptr, int pointIndex, ref MatrixFrame frame);

		// Token: 0x0200031C RID: 796
		// (Invoke) Token: 0x06001173 RID: 4467
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetTangentPositionOfPointDelegate(UIntPtr ptr, int pointIndex, int tangentIndex, ref Vec3 position);
	}
}

using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000018 RID: 24
	[ApplicationInterfaceBase]
	internal interface IPath
	{
		// Token: 0x060000F6 RID: 246
		[EngineMethod("get_number_of_points", false)]
		int GetNumberOfPoints(UIntPtr ptr);

		// Token: 0x060000F7 RID: 247
		[EngineMethod("get_hermite_frame_wrt_dt", false)]
		void GetHermiteFrameForDt(UIntPtr ptr, ref MatrixFrame frame, float phase, int firstPoint);

		// Token: 0x060000F8 RID: 248
		[EngineMethod("get_hermite_frame_wrt_distance", false)]
		void GetHermiteFrameForDistance(UIntPtr ptr, ref MatrixFrame frame, float distance);

		// Token: 0x060000F9 RID: 249
		[EngineMethod("get_nearest_hermite_frame_with_valid_alpha_wrt_distance", false)]
		void GetNearestHermiteFrameWithValidAlphaForDistance(UIntPtr ptr, ref MatrixFrame frame, float distance, bool searchForward, float alphaThreshold);

		// Token: 0x060000FA RID: 250
		[EngineMethod("get_hermite_frame_and_color_wrt_distance", false)]
		void GetHermiteFrameAndColorForDistance(UIntPtr ptr, out MatrixFrame frame, out Vec3 color, float distance);

		// Token: 0x060000FB RID: 251
		[EngineMethod("get_arc_length", false)]
		float GetArcLength(UIntPtr ptr, int firstPoint);

		// Token: 0x060000FC RID: 252
		[EngineMethod("get_points", false)]
		void GetPoints(UIntPtr ptr, MatrixFrame[] points);

		// Token: 0x060000FD RID: 253
		[EngineMethod("get_path_length", false)]
		float GetTotalLength(UIntPtr ptr);

		// Token: 0x060000FE RID: 254
		[EngineMethod("get_path_version", false)]
		int GetVersion(UIntPtr ptr);

		// Token: 0x060000FF RID: 255
		[EngineMethod("set_frame_of_point", false)]
		void SetFrameOfPoint(UIntPtr ptr, int pointIndex, ref MatrixFrame frame);

		// Token: 0x06000100 RID: 256
		[EngineMethod("set_tangent_position_of_point", false)]
		void SetTangentPositionOfPoint(UIntPtr ptr, int pointIndex, int tangentIndex, ref Vec3 position);

		// Token: 0x06000101 RID: 257
		[EngineMethod("add_path_point", false)]
		int AddPathPoint(UIntPtr ptr, int newNodeIndex);

		// Token: 0x06000102 RID: 258
		[EngineMethod("delete_path_point", false)]
		void DeletePathPoint(UIntPtr ptr, int newNodeIndex);

		// Token: 0x06000103 RID: 259
		[EngineMethod("has_valid_alpha_at_path_point", false)]
		bool HasValidAlphaAtPathPoint(UIntPtr ptr, int nodeIndex, float alphaThreshold);

		// Token: 0x06000104 RID: 260
		[EngineMethod("get_name", false)]
		string GetName(UIntPtr ptr);
	}
}

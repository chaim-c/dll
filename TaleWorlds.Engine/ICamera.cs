﻿using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000025 RID: 37
	[ApplicationInterfaceBase]
	internal interface ICamera
	{
		// Token: 0x06000204 RID: 516
		[EngineMethod("release", false)]
		void Release(UIntPtr cameraPointer);

		// Token: 0x06000205 RID: 517
		[EngineMethod("set_entity", false)]
		void SetEntity(UIntPtr cameraPointer, UIntPtr entityId);

		// Token: 0x06000206 RID: 518
		[EngineMethod("get_entity", false)]
		GameEntity GetEntity(UIntPtr cameraPointer);

		// Token: 0x06000207 RID: 519
		[EngineMethod("create_camera", false)]
		Camera CreateCamera();

		// Token: 0x06000208 RID: 520
		[EngineMethod("release_camera_entity", false)]
		void ReleaseCameraEntity(UIntPtr cameraPointer);

		// Token: 0x06000209 RID: 521
		[EngineMethod("look_at", false)]
		void LookAt(UIntPtr cameraPointer, Vec3 position, Vec3 target, Vec3 upVector);

		// Token: 0x0600020A RID: 522
		[EngineMethod("screen_space_ray_projection", false)]
		void ScreenSpaceRayProjection(UIntPtr cameraPointer, Vec2 screenPosition, ref Vec3 rayBegin, ref Vec3 rayEnd);

		// Token: 0x0600020B RID: 523
		[EngineMethod("check_entity_visibility", false)]
		bool CheckEntityVisibility(UIntPtr cameraPointer, UIntPtr entityPointer);

		// Token: 0x0600020C RID: 524
		[EngineMethod("set_position", false)]
		void SetPosition(UIntPtr cameraPointer, Vec3 position);

		// Token: 0x0600020D RID: 525
		[EngineMethod("set_view_volume", false)]
		void SetViewVolume(UIntPtr cameraPointer, bool perspective, float dLeft, float dRight, float dBottom, float dTop, float dNear, float dFar);

		// Token: 0x0600020E RID: 526
		[EngineMethod("get_near_plane_points_static", false)]
		void GetNearPlanePointsStatic(ref MatrixFrame cameraFrame, float verticalFov, float aspectRatioXY, float newDNear, float newDFar, Vec3[] nearPlanePoints);

		// Token: 0x0600020F RID: 527
		[EngineMethod("get_near_plane_points", false)]
		void GetNearPlanePoints(UIntPtr cameraPointer, Vec3[] nearPlanePoints);

		// Token: 0x06000210 RID: 528
		[EngineMethod("set_fov_vertical", false)]
		void SetFovVertical(UIntPtr cameraPointer, float verticalFov, float aspectRatio, float newDNear, float newDFar);

		// Token: 0x06000211 RID: 529
		[EngineMethod("get_view_proj_matrix", false)]
		void GetViewProjMatrix(UIntPtr cameraPointer, ref MatrixFrame frame);

		// Token: 0x06000212 RID: 530
		[EngineMethod("set_fov_horizontal", false)]
		void SetFovHorizontal(UIntPtr cameraPointer, float horizontalFov, float aspectRatio, float newDNear, float newDFar);

		// Token: 0x06000213 RID: 531
		[EngineMethod("get_fov_vertical", false)]
		float GetFovVertical(UIntPtr cameraPointer);

		// Token: 0x06000214 RID: 532
		[EngineMethod("get_fov_horizontal", false)]
		float GetFovHorizontal(UIntPtr cameraPointer);

		// Token: 0x06000215 RID: 533
		[EngineMethod("get_aspect_ratio", false)]
		float GetAspectRatio(UIntPtr cameraPointer);

		// Token: 0x06000216 RID: 534
		[EngineMethod("fill_parameters_from", false)]
		void FillParametersFrom(UIntPtr cameraPointer, UIntPtr otherCameraPointer);

		// Token: 0x06000217 RID: 535
		[EngineMethod("render_frustrum", false)]
		void RenderFrustrum(UIntPtr cameraPointer);

		// Token: 0x06000218 RID: 536
		[EngineMethod("set_frame", false)]
		void SetFrame(UIntPtr cameraPointer, ref MatrixFrame frame);

		// Token: 0x06000219 RID: 537
		[EngineMethod("get_frame", false)]
		void GetFrame(UIntPtr cameraPointer, ref MatrixFrame outFrame);

		// Token: 0x0600021A RID: 538
		[EngineMethod("get_near", false)]
		float GetNear(UIntPtr cameraPointer);

		// Token: 0x0600021B RID: 539
		[EngineMethod("get_far", false)]
		float GetFar(UIntPtr cameraPointer);

		// Token: 0x0600021C RID: 540
		[EngineMethod("get_horizontal_fov", false)]
		float GetHorizontalFov(UIntPtr cameraPointer);

		// Token: 0x0600021D RID: 541
		[EngineMethod("viewport_point_to_world_ray", false)]
		void ViewportPointToWorldRay(UIntPtr cameraPointer, ref Vec3 rayBegin, ref Vec3 rayEnd, Vec3 viewportPoint);

		// Token: 0x0600021E RID: 542
		[EngineMethod("world_point_to_viewport_point", false)]
		Vec3 WorldPointToViewportPoint(UIntPtr cameraPointer, ref Vec3 worldPoint);

		// Token: 0x0600021F RID: 543
		[EngineMethod("encloses_point", false)]
		bool EnclosesPoint(UIntPtr cameraPointer, Vec3 pointInWorldSpace);

		// Token: 0x06000220 RID: 544
		[EngineMethod("construct_camera_from_position_elevation_bearing", false)]
		void ConstructCameraFromPositionElevationBearing(Vec3 position, float elevation, float bearing, ref MatrixFrame outFrame);
	}
}

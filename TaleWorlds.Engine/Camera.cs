using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200000E RID: 14
	[EngineClass("rglCamera_object")]
	public sealed class Camera : NativeObject
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002803 File Offset: 0x00000A03
		internal Camera(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002812 File Offset: 0x00000A12
		public static Camera CreateCamera()
		{
			return EngineApplicationInterface.ICamera.CreateCamera();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000281E File Offset: 0x00000A1E
		public void ReleaseCamera()
		{
			EngineApplicationInterface.ICamera.Release(base.Pointer);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002830 File Offset: 0x00000A30
		public void ReleaseCameraEntity()
		{
			EngineApplicationInterface.ICamera.ReleaseCameraEntity(base.Pointer);
			this.ReleaseCamera();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002848 File Offset: 0x00000A48
		~Camera()
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002870 File Offset: 0x00000A70
		public void LookAt(Vec3 position, Vec3 target, Vec3 upVector)
		{
			EngineApplicationInterface.ICamera.LookAt(base.Pointer, position, target, upVector);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002888 File Offset: 0x00000A88
		public void ScreenSpaceRayProjection(Vec2 screenPosition, ref Vec3 rayBegin, ref Vec3 rayEnd)
		{
			EngineApplicationInterface.ICamera.ScreenSpaceRayProjection(base.Pointer, screenPosition, ref rayBegin, ref rayEnd);
			if (this.Entity != null)
			{
				rayBegin = this.Entity.GetGlobalFrame().TransformToParent(rayBegin);
				rayEnd = this.Entity.GetGlobalFrame().TransformToParent(rayEnd);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000028F4 File Offset: 0x00000AF4
		public bool CheckEntityVisibility(GameEntity entity)
		{
			return EngineApplicationInterface.ICamera.CheckEntityVisibility(base.Pointer, entity.Pointer);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000290C File Offset: 0x00000B0C
		public void SetViewVolume(bool perspective, float dLeft, float dRight, float dBottom, float dTop, float dNear, float dFar)
		{
			EngineApplicationInterface.ICamera.SetViewVolume(base.Pointer, perspective, dLeft, dRight, dBottom, dTop, dNear, dFar);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002934 File Offset: 0x00000B34
		public static void GetNearPlanePointsStatic(ref MatrixFrame cameraFrame, float verticalFov, float aspectRatioXY, float newDNear, float newDFar, Vec3[] nearPlanePoints)
		{
			EngineApplicationInterface.ICamera.GetNearPlanePointsStatic(ref cameraFrame, verticalFov, aspectRatioXY, newDNear, newDFar, nearPlanePoints);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002948 File Offset: 0x00000B48
		public void GetNearPlanePoints(Vec3[] nearPlanePoints)
		{
			EngineApplicationInterface.ICamera.GetNearPlanePoints(base.Pointer, nearPlanePoints);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000295B File Offset: 0x00000B5B
		public void SetFovVertical(float verticalFov, float aspectRatioXY, float newDNear, float newDFar)
		{
			EngineApplicationInterface.ICamera.SetFovVertical(base.Pointer, verticalFov, aspectRatioXY, newDNear, newDFar);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002972 File Offset: 0x00000B72
		public void SetFovHorizontal(float horizontalFov, float aspectRatioXY, float newDNear, float newDFar)
		{
			EngineApplicationInterface.ICamera.SetFovHorizontal(base.Pointer, horizontalFov, aspectRatioXY, newDNear, newDFar);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002989 File Offset: 0x00000B89
		public void GetViewProjMatrix(ref MatrixFrame viewProj)
		{
			EngineApplicationInterface.ICamera.GetViewProjMatrix(base.Pointer, ref viewProj);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000299C File Offset: 0x00000B9C
		public float GetFovVertical()
		{
			return EngineApplicationInterface.ICamera.GetFovVertical(base.Pointer);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029AE File Offset: 0x00000BAE
		public float GetFovHorizontal()
		{
			return EngineApplicationInterface.ICamera.GetFovHorizontal(base.Pointer);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029C0 File Offset: 0x00000BC0
		public float GetAspectRatio()
		{
			return EngineApplicationInterface.ICamera.GetAspectRatio(base.Pointer);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000029D2 File Offset: 0x00000BD2
		public void FillParametersFrom(Camera otherCamera)
		{
			EngineApplicationInterface.ICamera.FillParametersFrom(base.Pointer, otherCamera.Pointer);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000029EA File Offset: 0x00000BEA
		public void RenderFrustrum()
		{
			EngineApplicationInterface.ICamera.RenderFrustrum(base.Pointer);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000029FC File Offset: 0x00000BFC
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002A0E File Offset: 0x00000C0E
		public GameEntity Entity
		{
			get
			{
				return EngineApplicationInterface.ICamera.GetEntity(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.ICamera.SetEntity(base.Pointer, value.Pointer);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002A26 File Offset: 0x00000C26
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002A33 File Offset: 0x00000C33
		public Vec3 Position
		{
			get
			{
				return this.Frame.origin;
			}
			set
			{
				EngineApplicationInterface.ICamera.SetPosition(base.Pointer, value);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002A46 File Offset: 0x00000C46
		public Vec3 Direction
		{
			get
			{
				return -this.Frame.rotation.u;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002A60 File Offset: 0x00000C60
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002A88 File Offset: 0x00000C88
		public MatrixFrame Frame
		{
			get
			{
				MatrixFrame result = default(MatrixFrame);
				EngineApplicationInterface.ICamera.GetFrame(base.Pointer, ref result);
				return result;
			}
			set
			{
				EngineApplicationInterface.ICamera.SetFrame(base.Pointer, ref value);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002A9C File Offset: 0x00000C9C
		public float Near
		{
			get
			{
				return EngineApplicationInterface.ICamera.GetNear(base.Pointer);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002AAE File Offset: 0x00000CAE
		public float Far
		{
			get
			{
				return EngineApplicationInterface.ICamera.GetFar(base.Pointer);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public float HorizontalFov
		{
			get
			{
				return EngineApplicationInterface.ICamera.GetHorizontalFov(base.Pointer);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002AD2 File Offset: 0x00000CD2
		public void ViewportPointToWorldRay(ref Vec3 rayBegin, ref Vec3 rayEnd, Vec2 viewportPoint)
		{
			EngineApplicationInterface.ICamera.ViewportPointToWorldRay(base.Pointer, ref rayBegin, ref rayEnd, viewportPoint.ToVec3(0f));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002AF2 File Offset: 0x00000CF2
		public Vec3 WorldPointToViewPortPoint(ref Vec3 worldPoint)
		{
			return EngineApplicationInterface.ICamera.WorldPointToViewportPoint(base.Pointer, ref worldPoint);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002B05 File Offset: 0x00000D05
		public bool EnclosesPoint(Vec3 pointInWorldSpace)
		{
			return EngineApplicationInterface.ICamera.EnclosesPoint(base.Pointer, pointInWorldSpace);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002B18 File Offset: 0x00000D18
		public static MatrixFrame ConstructCameraFromPositionElevationBearing(Vec3 position, float elevation, float bearing)
		{
			MatrixFrame result = default(MatrixFrame);
			EngineApplicationInterface.ICamera.ConstructCameraFromPositionElevationBearing(position, elevation, bearing, ref result);
			return result;
		}
	}
}

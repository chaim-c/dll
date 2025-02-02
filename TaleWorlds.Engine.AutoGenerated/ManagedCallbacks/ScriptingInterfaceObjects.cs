using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ManagedCallbacks
{
	// Token: 0x02000007 RID: 7
	internal static class ScriptingInterfaceObjects
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002C2C File Offset: 0x00000E2C
		public static Dictionary<string, object> GetObjects()
		{
			return new Dictionary<string, object>
			{
				{
					"TaleWorlds.Engine.IAsyncTask",
					new ScriptingInterfaceOfIAsyncTask()
				},
				{
					"TaleWorlds.Engine.IBodyPart",
					new ScriptingInterfaceOfIBodyPart()
				},
				{
					"TaleWorlds.Engine.ICamera",
					new ScriptingInterfaceOfICamera()
				},
				{
					"TaleWorlds.Engine.IClothSimulatorComponent",
					new ScriptingInterfaceOfIClothSimulatorComponent()
				},
				{
					"TaleWorlds.Engine.ICompositeComponent",
					new ScriptingInterfaceOfICompositeComponent()
				},
				{
					"TaleWorlds.Engine.IConfig",
					new ScriptingInterfaceOfIConfig()
				},
				{
					"TaleWorlds.Engine.IDebug",
					new ScriptingInterfaceOfIDebug()
				},
				{
					"TaleWorlds.Engine.IDecal",
					new ScriptingInterfaceOfIDecal()
				},
				{
					"TaleWorlds.Engine.IEngineSizeChecker",
					new ScriptingInterfaceOfIEngineSizeChecker()
				},
				{
					"TaleWorlds.Engine.IGameEntity",
					new ScriptingInterfaceOfIGameEntity()
				},
				{
					"TaleWorlds.Engine.IGameEntityComponent",
					new ScriptingInterfaceOfIGameEntityComponent()
				},
				{
					"TaleWorlds.Engine.IHighlights",
					new ScriptingInterfaceOfIHighlights()
				},
				{
					"TaleWorlds.Engine.IImgui",
					new ScriptingInterfaceOfIImgui()
				},
				{
					"TaleWorlds.Engine.IInput",
					new ScriptingInterfaceOfIInput()
				},
				{
					"TaleWorlds.Engine.ILight",
					new ScriptingInterfaceOfILight()
				},
				{
					"TaleWorlds.Engine.IManagedMeshEditOperations",
					new ScriptingInterfaceOfIManagedMeshEditOperations()
				},
				{
					"TaleWorlds.Engine.IMaterial",
					new ScriptingInterfaceOfIMaterial()
				},
				{
					"TaleWorlds.Engine.IMesh",
					new ScriptingInterfaceOfIMesh()
				},
				{
					"TaleWorlds.Engine.IMeshBuilder",
					new ScriptingInterfaceOfIMeshBuilder()
				},
				{
					"TaleWorlds.Engine.IMetaMesh",
					new ScriptingInterfaceOfIMetaMesh()
				},
				{
					"TaleWorlds.Engine.IMouseManager",
					new ScriptingInterfaceOfIMouseManager()
				},
				{
					"TaleWorlds.Engine.IMusic",
					new ScriptingInterfaceOfIMusic()
				},
				{
					"TaleWorlds.Engine.IParticleSystem",
					new ScriptingInterfaceOfIParticleSystem()
				},
				{
					"TaleWorlds.Engine.IPath",
					new ScriptingInterfaceOfIPath()
				},
				{
					"TaleWorlds.Engine.IPhysicsMaterial",
					new ScriptingInterfaceOfIPhysicsMaterial()
				},
				{
					"TaleWorlds.Engine.IPhysicsShape",
					new ScriptingInterfaceOfIPhysicsShape()
				},
				{
					"TaleWorlds.Engine.IScene",
					new ScriptingInterfaceOfIScene()
				},
				{
					"TaleWorlds.Engine.ISceneView",
					new ScriptingInterfaceOfISceneView()
				},
				{
					"TaleWorlds.Engine.IScreen",
					new ScriptingInterfaceOfIScreen()
				},
				{
					"TaleWorlds.Engine.IScriptComponent",
					new ScriptingInterfaceOfIScriptComponent()
				},
				{
					"TaleWorlds.Engine.IShader",
					new ScriptingInterfaceOfIShader()
				},
				{
					"TaleWorlds.Engine.ISkeleton",
					new ScriptingInterfaceOfISkeleton()
				},
				{
					"TaleWorlds.Engine.ISoundEvent",
					new ScriptingInterfaceOfISoundEvent()
				},
				{
					"TaleWorlds.Engine.ISoundManager",
					new ScriptingInterfaceOfISoundManager()
				},
				{
					"TaleWorlds.Engine.ITableauView",
					new ScriptingInterfaceOfITableauView()
				},
				{
					"TaleWorlds.Engine.ITexture",
					new ScriptingInterfaceOfITexture()
				},
				{
					"TaleWorlds.Engine.ITextureView",
					new ScriptingInterfaceOfITextureView()
				},
				{
					"TaleWorlds.Engine.IThumbnailCreatorView",
					new ScriptingInterfaceOfIThumbnailCreatorView()
				},
				{
					"TaleWorlds.Engine.ITime",
					new ScriptingInterfaceOfITime()
				},
				{
					"TaleWorlds.Engine.ITwoDimensionView",
					new ScriptingInterfaceOfITwoDimensionView()
				},
				{
					"TaleWorlds.Engine.IUtil",
					new ScriptingInterfaceOfIUtil()
				},
				{
					"TaleWorlds.Engine.IVideoPlayerView",
					new ScriptingInterfaceOfIVideoPlayerView()
				},
				{
					"TaleWorlds.Engine.IView",
					new ScriptingInterfaceOfIView()
				}
			};
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002EF0 File Offset: 0x000010F0
		public static void SetFunctionPointer(int id, IntPtr pointer)
		{
			switch (id)
			{
			case 0:
				ScriptingInterfaceOfIAsyncTask.call_CreateWithDelegateDelegate = (ScriptingInterfaceOfIAsyncTask.CreateWithDelegateDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIAsyncTask.CreateWithDelegateDelegate));
				return;
			case 1:
				ScriptingInterfaceOfIAsyncTask.call_InvokeDelegate = (ScriptingInterfaceOfIAsyncTask.InvokeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIAsyncTask.InvokeDelegate));
				return;
			case 2:
				ScriptingInterfaceOfIAsyncTask.call_WaitDelegate = (ScriptingInterfaceOfIAsyncTask.WaitDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIAsyncTask.WaitDelegate));
				return;
			case 3:
				ScriptingInterfaceOfIBodyPart.call_DoSegmentsIntersectDelegate = (ScriptingInterfaceOfIBodyPart.DoSegmentsIntersectDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIBodyPart.DoSegmentsIntersectDelegate));
				return;
			case 4:
				ScriptingInterfaceOfICamera.call_CheckEntityVisibilityDelegate = (ScriptingInterfaceOfICamera.CheckEntityVisibilityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.CheckEntityVisibilityDelegate));
				return;
			case 5:
				ScriptingInterfaceOfICamera.call_ConstructCameraFromPositionElevationBearingDelegate = (ScriptingInterfaceOfICamera.ConstructCameraFromPositionElevationBearingDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.ConstructCameraFromPositionElevationBearingDelegate));
				return;
			case 6:
				ScriptingInterfaceOfICamera.call_CreateCameraDelegate = (ScriptingInterfaceOfICamera.CreateCameraDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.CreateCameraDelegate));
				return;
			case 7:
				ScriptingInterfaceOfICamera.call_EnclosesPointDelegate = (ScriptingInterfaceOfICamera.EnclosesPointDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.EnclosesPointDelegate));
				return;
			case 8:
				ScriptingInterfaceOfICamera.call_FillParametersFromDelegate = (ScriptingInterfaceOfICamera.FillParametersFromDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.FillParametersFromDelegate));
				return;
			case 9:
				ScriptingInterfaceOfICamera.call_GetAspectRatioDelegate = (ScriptingInterfaceOfICamera.GetAspectRatioDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.GetAspectRatioDelegate));
				return;
			case 10:
				ScriptingInterfaceOfICamera.call_GetEntityDelegate = (ScriptingInterfaceOfICamera.GetEntityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.GetEntityDelegate));
				return;
			case 11:
				ScriptingInterfaceOfICamera.call_GetFarDelegate = (ScriptingInterfaceOfICamera.GetFarDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.GetFarDelegate));
				return;
			case 12:
				ScriptingInterfaceOfICamera.call_GetFovHorizontalDelegate = (ScriptingInterfaceOfICamera.GetFovHorizontalDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.GetFovHorizontalDelegate));
				return;
			case 13:
				ScriptingInterfaceOfICamera.call_GetFovVerticalDelegate = (ScriptingInterfaceOfICamera.GetFovVerticalDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.GetFovVerticalDelegate));
				return;
			case 14:
				ScriptingInterfaceOfICamera.call_GetFrameDelegate = (ScriptingInterfaceOfICamera.GetFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.GetFrameDelegate));
				return;
			case 15:
				ScriptingInterfaceOfICamera.call_GetHorizontalFovDelegate = (ScriptingInterfaceOfICamera.GetHorizontalFovDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.GetHorizontalFovDelegate));
				return;
			case 16:
				ScriptingInterfaceOfICamera.call_GetNearDelegate = (ScriptingInterfaceOfICamera.GetNearDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.GetNearDelegate));
				return;
			case 17:
				ScriptingInterfaceOfICamera.call_GetNearPlanePointsDelegate = (ScriptingInterfaceOfICamera.GetNearPlanePointsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.GetNearPlanePointsDelegate));
				return;
			case 18:
				ScriptingInterfaceOfICamera.call_GetNearPlanePointsStaticDelegate = (ScriptingInterfaceOfICamera.GetNearPlanePointsStaticDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.GetNearPlanePointsStaticDelegate));
				return;
			case 19:
				ScriptingInterfaceOfICamera.call_GetViewProjMatrixDelegate = (ScriptingInterfaceOfICamera.GetViewProjMatrixDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.GetViewProjMatrixDelegate));
				return;
			case 20:
				ScriptingInterfaceOfICamera.call_LookAtDelegate = (ScriptingInterfaceOfICamera.LookAtDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.LookAtDelegate));
				return;
			case 21:
				ScriptingInterfaceOfICamera.call_ReleaseDelegate = (ScriptingInterfaceOfICamera.ReleaseDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.ReleaseDelegate));
				return;
			case 22:
				ScriptingInterfaceOfICamera.call_ReleaseCameraEntityDelegate = (ScriptingInterfaceOfICamera.ReleaseCameraEntityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.ReleaseCameraEntityDelegate));
				return;
			case 23:
				ScriptingInterfaceOfICamera.call_RenderFrustrumDelegate = (ScriptingInterfaceOfICamera.RenderFrustrumDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.RenderFrustrumDelegate));
				return;
			case 24:
				ScriptingInterfaceOfICamera.call_ScreenSpaceRayProjectionDelegate = (ScriptingInterfaceOfICamera.ScreenSpaceRayProjectionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.ScreenSpaceRayProjectionDelegate));
				return;
			case 25:
				ScriptingInterfaceOfICamera.call_SetEntityDelegate = (ScriptingInterfaceOfICamera.SetEntityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.SetEntityDelegate));
				return;
			case 26:
				ScriptingInterfaceOfICamera.call_SetFovHorizontalDelegate = (ScriptingInterfaceOfICamera.SetFovHorizontalDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.SetFovHorizontalDelegate));
				return;
			case 27:
				ScriptingInterfaceOfICamera.call_SetFovVerticalDelegate = (ScriptingInterfaceOfICamera.SetFovVerticalDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.SetFovVerticalDelegate));
				return;
			case 28:
				ScriptingInterfaceOfICamera.call_SetFrameDelegate = (ScriptingInterfaceOfICamera.SetFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.SetFrameDelegate));
				return;
			case 29:
				ScriptingInterfaceOfICamera.call_SetPositionDelegate = (ScriptingInterfaceOfICamera.SetPositionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.SetPositionDelegate));
				return;
			case 30:
				ScriptingInterfaceOfICamera.call_SetViewVolumeDelegate = (ScriptingInterfaceOfICamera.SetViewVolumeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.SetViewVolumeDelegate));
				return;
			case 31:
				ScriptingInterfaceOfICamera.call_ViewportPointToWorldRayDelegate = (ScriptingInterfaceOfICamera.ViewportPointToWorldRayDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.ViewportPointToWorldRayDelegate));
				return;
			case 32:
				ScriptingInterfaceOfICamera.call_WorldPointToViewportPointDelegate = (ScriptingInterfaceOfICamera.WorldPointToViewportPointDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICamera.WorldPointToViewportPointDelegate));
				return;
			case 33:
				ScriptingInterfaceOfIClothSimulatorComponent.call_SetMaxDistanceMultiplierDelegate = (ScriptingInterfaceOfIClothSimulatorComponent.SetMaxDistanceMultiplierDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIClothSimulatorComponent.SetMaxDistanceMultiplierDelegate));
				return;
			case 34:
				ScriptingInterfaceOfICompositeComponent.call_AddComponentDelegate = (ScriptingInterfaceOfICompositeComponent.AddComponentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.AddComponentDelegate));
				return;
			case 35:
				ScriptingInterfaceOfICompositeComponent.call_AddMultiMeshDelegate = (ScriptingInterfaceOfICompositeComponent.AddMultiMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.AddMultiMeshDelegate));
				return;
			case 36:
				ScriptingInterfaceOfICompositeComponent.call_AddPrefabEntityDelegate = (ScriptingInterfaceOfICompositeComponent.AddPrefabEntityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.AddPrefabEntityDelegate));
				return;
			case 37:
				ScriptingInterfaceOfICompositeComponent.call_CreateCompositeComponentDelegate = (ScriptingInterfaceOfICompositeComponent.CreateCompositeComponentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.CreateCompositeComponentDelegate));
				return;
			case 38:
				ScriptingInterfaceOfICompositeComponent.call_CreateCopyDelegate = (ScriptingInterfaceOfICompositeComponent.CreateCopyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.CreateCopyDelegate));
				return;
			case 39:
				ScriptingInterfaceOfICompositeComponent.call_GetBoundingBoxDelegate = (ScriptingInterfaceOfICompositeComponent.GetBoundingBoxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.GetBoundingBoxDelegate));
				return;
			case 40:
				ScriptingInterfaceOfICompositeComponent.call_GetFactor1Delegate = (ScriptingInterfaceOfICompositeComponent.GetFactor1Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.GetFactor1Delegate));
				return;
			case 41:
				ScriptingInterfaceOfICompositeComponent.call_GetFactor2Delegate = (ScriptingInterfaceOfICompositeComponent.GetFactor2Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.GetFactor2Delegate));
				return;
			case 42:
				ScriptingInterfaceOfICompositeComponent.call_GetFirstMetaMeshDelegate = (ScriptingInterfaceOfICompositeComponent.GetFirstMetaMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.GetFirstMetaMeshDelegate));
				return;
			case 43:
				ScriptingInterfaceOfICompositeComponent.call_GetFrameDelegate = (ScriptingInterfaceOfICompositeComponent.GetFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.GetFrameDelegate));
				return;
			case 44:
				ScriptingInterfaceOfICompositeComponent.call_GetVectorUserDataDelegate = (ScriptingInterfaceOfICompositeComponent.GetVectorUserDataDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.GetVectorUserDataDelegate));
				return;
			case 45:
				ScriptingInterfaceOfICompositeComponent.call_IsVisibleDelegate = (ScriptingInterfaceOfICompositeComponent.IsVisibleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.IsVisibleDelegate));
				return;
			case 46:
				ScriptingInterfaceOfICompositeComponent.call_ReleaseDelegate = (ScriptingInterfaceOfICompositeComponent.ReleaseDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.ReleaseDelegate));
				return;
			case 47:
				ScriptingInterfaceOfICompositeComponent.call_SetFactor1Delegate = (ScriptingInterfaceOfICompositeComponent.SetFactor1Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.SetFactor1Delegate));
				return;
			case 48:
				ScriptingInterfaceOfICompositeComponent.call_SetFactor2Delegate = (ScriptingInterfaceOfICompositeComponent.SetFactor2Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.SetFactor2Delegate));
				return;
			case 49:
				ScriptingInterfaceOfICompositeComponent.call_SetFrameDelegate = (ScriptingInterfaceOfICompositeComponent.SetFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.SetFrameDelegate));
				return;
			case 50:
				ScriptingInterfaceOfICompositeComponent.call_SetMaterialDelegate = (ScriptingInterfaceOfICompositeComponent.SetMaterialDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.SetMaterialDelegate));
				return;
			case 51:
				ScriptingInterfaceOfICompositeComponent.call_SetVectorArgumentDelegate = (ScriptingInterfaceOfICompositeComponent.SetVectorArgumentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.SetVectorArgumentDelegate));
				return;
			case 52:
				ScriptingInterfaceOfICompositeComponent.call_SetVectorUserDataDelegate = (ScriptingInterfaceOfICompositeComponent.SetVectorUserDataDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.SetVectorUserDataDelegate));
				return;
			case 53:
				ScriptingInterfaceOfICompositeComponent.call_SetVisibilityMaskDelegate = (ScriptingInterfaceOfICompositeComponent.SetVisibilityMaskDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.SetVisibilityMaskDelegate));
				return;
			case 54:
				ScriptingInterfaceOfICompositeComponent.call_SetVisibleDelegate = (ScriptingInterfaceOfICompositeComponent.SetVisibleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfICompositeComponent.SetVisibleDelegate));
				return;
			case 55:
				ScriptingInterfaceOfIConfig.call_ApplyDelegate = (ScriptingInterfaceOfIConfig.ApplyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.ApplyDelegate));
				return;
			case 56:
				ScriptingInterfaceOfIConfig.call_ApplyConfigChangesDelegate = (ScriptingInterfaceOfIConfig.ApplyConfigChangesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.ApplyConfigChangesDelegate));
				return;
			case 57:
				ScriptingInterfaceOfIConfig.call_AutoSaveInMinutesDelegate = (ScriptingInterfaceOfIConfig.AutoSaveInMinutesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.AutoSaveInMinutesDelegate));
				return;
			case 58:
				ScriptingInterfaceOfIConfig.call_CheckGFXSupportStatusDelegate = (ScriptingInterfaceOfIConfig.CheckGFXSupportStatusDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.CheckGFXSupportStatusDelegate));
				return;
			case 59:
				ScriptingInterfaceOfIConfig.call_GetAutoGFXQualityDelegate = (ScriptingInterfaceOfIConfig.GetAutoGFXQualityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetAutoGFXQualityDelegate));
				return;
			case 60:
				ScriptingInterfaceOfIConfig.call_GetCharacterDetailDelegate = (ScriptingInterfaceOfIConfig.GetCharacterDetailDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetCharacterDetailDelegate));
				return;
			case 61:
				ScriptingInterfaceOfIConfig.call_GetCheatModeDelegate = (ScriptingInterfaceOfIConfig.GetCheatModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetCheatModeDelegate));
				return;
			case 62:
				ScriptingInterfaceOfIConfig.call_GetCurrentSoundDeviceIndexDelegate = (ScriptingInterfaceOfIConfig.GetCurrentSoundDeviceIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetCurrentSoundDeviceIndexDelegate));
				return;
			case 63:
				ScriptingInterfaceOfIConfig.call_GetDebugLoginPasswordDelegate = (ScriptingInterfaceOfIConfig.GetDebugLoginPasswordDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetDebugLoginPasswordDelegate));
				return;
			case 64:
				ScriptingInterfaceOfIConfig.call_GetDebugLoginUserNameDelegate = (ScriptingInterfaceOfIConfig.GetDebugLoginUserNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetDebugLoginUserNameDelegate));
				return;
			case 65:
				ScriptingInterfaceOfIConfig.call_GetDefaultRGLConfigDelegate = (ScriptingInterfaceOfIConfig.GetDefaultRGLConfigDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetDefaultRGLConfigDelegate));
				return;
			case 66:
				ScriptingInterfaceOfIConfig.call_GetDesktopResolutionDelegate = (ScriptingInterfaceOfIConfig.GetDesktopResolutionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetDesktopResolutionDelegate));
				return;
			case 67:
				ScriptingInterfaceOfIConfig.call_GetDevelopmentModeDelegate = (ScriptingInterfaceOfIConfig.GetDevelopmentModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetDevelopmentModeDelegate));
				return;
			case 68:
				ScriptingInterfaceOfIConfig.call_GetDisableGuiMessagesDelegate = (ScriptingInterfaceOfIConfig.GetDisableGuiMessagesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetDisableGuiMessagesDelegate));
				return;
			case 69:
				ScriptingInterfaceOfIConfig.call_GetDisableSoundDelegate = (ScriptingInterfaceOfIConfig.GetDisableSoundDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetDisableSoundDelegate));
				return;
			case 70:
				ScriptingInterfaceOfIConfig.call_GetDlssOptionCountDelegate = (ScriptingInterfaceOfIConfig.GetDlssOptionCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetDlssOptionCountDelegate));
				return;
			case 71:
				ScriptingInterfaceOfIConfig.call_GetDlssTechniqueDelegate = (ScriptingInterfaceOfIConfig.GetDlssTechniqueDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetDlssTechniqueDelegate));
				return;
			case 72:
				ScriptingInterfaceOfIConfig.call_GetDoLocalizationCheckAtStartupDelegate = (ScriptingInterfaceOfIConfig.GetDoLocalizationCheckAtStartupDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetDoLocalizationCheckAtStartupDelegate));
				return;
			case 73:
				ScriptingInterfaceOfIConfig.call_GetEnableClothSimulationDelegate = (ScriptingInterfaceOfIConfig.GetEnableClothSimulationDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetEnableClothSimulationDelegate));
				return;
			case 74:
				ScriptingInterfaceOfIConfig.call_GetEnableEditModeDelegate = (ScriptingInterfaceOfIConfig.GetEnableEditModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetEnableEditModeDelegate));
				return;
			case 75:
				ScriptingInterfaceOfIConfig.call_GetInvertMouseDelegate = (ScriptingInterfaceOfIConfig.GetInvertMouseDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetInvertMouseDelegate));
				return;
			case 76:
				ScriptingInterfaceOfIConfig.call_GetLastOpenedSceneDelegate = (ScriptingInterfaceOfIConfig.GetLastOpenedSceneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetLastOpenedSceneDelegate));
				return;
			case 77:
				ScriptingInterfaceOfIConfig.call_GetLocalizationDebugModeDelegate = (ScriptingInterfaceOfIConfig.GetLocalizationDebugModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetLocalizationDebugModeDelegate));
				return;
			case 78:
				ScriptingInterfaceOfIConfig.call_GetMonitorDeviceCountDelegate = (ScriptingInterfaceOfIConfig.GetMonitorDeviceCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetMonitorDeviceCountDelegate));
				return;
			case 79:
				ScriptingInterfaceOfIConfig.call_GetMonitorDeviceNameDelegate = (ScriptingInterfaceOfIConfig.GetMonitorDeviceNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetMonitorDeviceNameDelegate));
				return;
			case 80:
				ScriptingInterfaceOfIConfig.call_GetRefreshRateAtIndexDelegate = (ScriptingInterfaceOfIConfig.GetRefreshRateAtIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetRefreshRateAtIndexDelegate));
				return;
			case 81:
				ScriptingInterfaceOfIConfig.call_GetRefreshRateCountDelegate = (ScriptingInterfaceOfIConfig.GetRefreshRateCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetRefreshRateCountDelegate));
				return;
			case 82:
				ScriptingInterfaceOfIConfig.call_GetResolutionDelegate = (ScriptingInterfaceOfIConfig.GetResolutionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetResolutionDelegate));
				return;
			case 83:
				ScriptingInterfaceOfIConfig.call_GetResolutionAtIndexDelegate = (ScriptingInterfaceOfIConfig.GetResolutionAtIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetResolutionAtIndexDelegate));
				return;
			case 84:
				ScriptingInterfaceOfIConfig.call_GetResolutionCountDelegate = (ScriptingInterfaceOfIConfig.GetResolutionCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetResolutionCountDelegate));
				return;
			case 85:
				ScriptingInterfaceOfIConfig.call_GetRGLConfigDelegate = (ScriptingInterfaceOfIConfig.GetRGLConfigDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetRGLConfigDelegate));
				return;
			case 86:
				ScriptingInterfaceOfIConfig.call_GetRGLConfigForDefaultSettingsDelegate = (ScriptingInterfaceOfIConfig.GetRGLConfigForDefaultSettingsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetRGLConfigForDefaultSettingsDelegate));
				return;
			case 87:
				ScriptingInterfaceOfIConfig.call_GetSoundDeviceCountDelegate = (ScriptingInterfaceOfIConfig.GetSoundDeviceCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetSoundDeviceCountDelegate));
				return;
			case 88:
				ScriptingInterfaceOfIConfig.call_GetSoundDeviceNameDelegate = (ScriptingInterfaceOfIConfig.GetSoundDeviceNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetSoundDeviceNameDelegate));
				return;
			case 89:
				ScriptingInterfaceOfIConfig.call_GetTableauCacheModeDelegate = (ScriptingInterfaceOfIConfig.GetTableauCacheModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetTableauCacheModeDelegate));
				return;
			case 90:
				ScriptingInterfaceOfIConfig.call_GetUIDebugModeDelegate = (ScriptingInterfaceOfIConfig.GetUIDebugModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetUIDebugModeDelegate));
				return;
			case 91:
				ScriptingInterfaceOfIConfig.call_GetUIDoNotUseGeneratedPrefabsDelegate = (ScriptingInterfaceOfIConfig.GetUIDoNotUseGeneratedPrefabsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetUIDoNotUseGeneratedPrefabsDelegate));
				return;
			case 92:
				ScriptingInterfaceOfIConfig.call_GetVideoDeviceCountDelegate = (ScriptingInterfaceOfIConfig.GetVideoDeviceCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetVideoDeviceCountDelegate));
				return;
			case 93:
				ScriptingInterfaceOfIConfig.call_GetVideoDeviceNameDelegate = (ScriptingInterfaceOfIConfig.GetVideoDeviceNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.GetVideoDeviceNameDelegate));
				return;
			case 94:
				ScriptingInterfaceOfIConfig.call_Is120HzAvailableDelegate = (ScriptingInterfaceOfIConfig.Is120HzAvailableDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.Is120HzAvailableDelegate));
				return;
			case 95:
				ScriptingInterfaceOfIConfig.call_IsDlssAvailableDelegate = (ScriptingInterfaceOfIConfig.IsDlssAvailableDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.IsDlssAvailableDelegate));
				return;
			case 96:
				ScriptingInterfaceOfIConfig.call_ReadRGLConfigFilesDelegate = (ScriptingInterfaceOfIConfig.ReadRGLConfigFilesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.ReadRGLConfigFilesDelegate));
				return;
			case 97:
				ScriptingInterfaceOfIConfig.call_RefreshOptionsDataDelegate = (ScriptingInterfaceOfIConfig.RefreshOptionsDataDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.RefreshOptionsDataDelegate));
				return;
			case 98:
				ScriptingInterfaceOfIConfig.call_SaveRGLConfigDelegate = (ScriptingInterfaceOfIConfig.SaveRGLConfigDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.SaveRGLConfigDelegate));
				return;
			case 99:
				ScriptingInterfaceOfIConfig.call_SetAutoConfigWrtHardwareDelegate = (ScriptingInterfaceOfIConfig.SetAutoConfigWrtHardwareDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.SetAutoConfigWrtHardwareDelegate));
				return;
			case 100:
				ScriptingInterfaceOfIConfig.call_SetBrightnessDelegate = (ScriptingInterfaceOfIConfig.SetBrightnessDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.SetBrightnessDelegate));
				return;
			case 101:
				ScriptingInterfaceOfIConfig.call_SetCustomResolutionDelegate = (ScriptingInterfaceOfIConfig.SetCustomResolutionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.SetCustomResolutionDelegate));
				return;
			case 102:
				ScriptingInterfaceOfIConfig.call_SetDefaultGameConfigDelegate = (ScriptingInterfaceOfIConfig.SetDefaultGameConfigDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.SetDefaultGameConfigDelegate));
				return;
			case 103:
				ScriptingInterfaceOfIConfig.call_SetRGLConfigDelegate = (ScriptingInterfaceOfIConfig.SetRGLConfigDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.SetRGLConfigDelegate));
				return;
			case 104:
				ScriptingInterfaceOfIConfig.call_SetSharpenAmountDelegate = (ScriptingInterfaceOfIConfig.SetSharpenAmountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.SetSharpenAmountDelegate));
				return;
			case 105:
				ScriptingInterfaceOfIConfig.call_SetSoundDeviceDelegate = (ScriptingInterfaceOfIConfig.SetSoundDeviceDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIConfig.SetSoundDeviceDelegate));
				return;
			case 106:
				ScriptingInterfaceOfIDebug.call_AbortGameDelegate = (ScriptingInterfaceOfIDebug.AbortGameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.AbortGameDelegate));
				return;
			case 107:
				ScriptingInterfaceOfIDebug.call_AssertMemoryUsageDelegate = (ScriptingInterfaceOfIDebug.AssertMemoryUsageDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.AssertMemoryUsageDelegate));
				return;
			case 108:
				ScriptingInterfaceOfIDebug.call_ClearAllDebugRenderObjectsDelegate = (ScriptingInterfaceOfIDebug.ClearAllDebugRenderObjectsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.ClearAllDebugRenderObjectsDelegate));
				return;
			case 109:
				ScriptingInterfaceOfIDebug.call_ContentWarningDelegate = (ScriptingInterfaceOfIDebug.ContentWarningDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.ContentWarningDelegate));
				return;
			case 110:
				ScriptingInterfaceOfIDebug.call_EchoCommandWindowDelegate = (ScriptingInterfaceOfIDebug.EchoCommandWindowDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.EchoCommandWindowDelegate));
				return;
			case 111:
				ScriptingInterfaceOfIDebug.call_ErrorDelegate = (ScriptingInterfaceOfIDebug.ErrorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.ErrorDelegate));
				return;
			case 112:
				ScriptingInterfaceOfIDebug.call_FailedAssertDelegate = (ScriptingInterfaceOfIDebug.FailedAssertDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.FailedAssertDelegate));
				return;
			case 113:
				ScriptingInterfaceOfIDebug.call_GetDebugVectorDelegate = (ScriptingInterfaceOfIDebug.GetDebugVectorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.GetDebugVectorDelegate));
				return;
			case 114:
				ScriptingInterfaceOfIDebug.call_GetShowDebugInfoDelegate = (ScriptingInterfaceOfIDebug.GetShowDebugInfoDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.GetShowDebugInfoDelegate));
				return;
			case 115:
				ScriptingInterfaceOfIDebug.call_IsErrorReportModeActiveDelegate = (ScriptingInterfaceOfIDebug.IsErrorReportModeActiveDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.IsErrorReportModeActiveDelegate));
				return;
			case 116:
				ScriptingInterfaceOfIDebug.call_IsErrorReportModePauseMissionDelegate = (ScriptingInterfaceOfIDebug.IsErrorReportModePauseMissionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.IsErrorReportModePauseMissionDelegate));
				return;
			case 117:
				ScriptingInterfaceOfIDebug.call_IsTestModeDelegate = (ScriptingInterfaceOfIDebug.IsTestModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.IsTestModeDelegate));
				return;
			case 118:
				ScriptingInterfaceOfIDebug.call_MessageBoxDelegate = (ScriptingInterfaceOfIDebug.MessageBoxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.MessageBoxDelegate));
				return;
			case 119:
				ScriptingInterfaceOfIDebug.call_PostWarningLineDelegate = (ScriptingInterfaceOfIDebug.PostWarningLineDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.PostWarningLineDelegate));
				return;
			case 120:
				ScriptingInterfaceOfIDebug.call_RenderDebugBoxObjectDelegate = (ScriptingInterfaceOfIDebug.RenderDebugBoxObjectDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.RenderDebugBoxObjectDelegate));
				return;
			case 121:
				ScriptingInterfaceOfIDebug.call_RenderDebugBoxObjectWithFrameDelegate = (ScriptingInterfaceOfIDebug.RenderDebugBoxObjectWithFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.RenderDebugBoxObjectWithFrameDelegate));
				return;
			case 122:
				ScriptingInterfaceOfIDebug.call_RenderDebugCapsuleDelegate = (ScriptingInterfaceOfIDebug.RenderDebugCapsuleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.RenderDebugCapsuleDelegate));
				return;
			case 123:
				ScriptingInterfaceOfIDebug.call_RenderDebugDirectionArrowDelegate = (ScriptingInterfaceOfIDebug.RenderDebugDirectionArrowDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.RenderDebugDirectionArrowDelegate));
				return;
			case 124:
				ScriptingInterfaceOfIDebug.call_RenderDebugFrameDelegate = (ScriptingInterfaceOfIDebug.RenderDebugFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.RenderDebugFrameDelegate));
				return;
			case 125:
				ScriptingInterfaceOfIDebug.call_RenderDebugLineDelegate = (ScriptingInterfaceOfIDebug.RenderDebugLineDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.RenderDebugLineDelegate));
				return;
			case 126:
				ScriptingInterfaceOfIDebug.call_RenderDebugRectDelegate = (ScriptingInterfaceOfIDebug.RenderDebugRectDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.RenderDebugRectDelegate));
				return;
			case 127:
				ScriptingInterfaceOfIDebug.call_RenderDebugRectWithColorDelegate = (ScriptingInterfaceOfIDebug.RenderDebugRectWithColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.RenderDebugRectWithColorDelegate));
				return;
			case 128:
				ScriptingInterfaceOfIDebug.call_RenderDebugSphereDelegate = (ScriptingInterfaceOfIDebug.RenderDebugSphereDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.RenderDebugSphereDelegate));
				return;
			case 129:
				ScriptingInterfaceOfIDebug.call_RenderDebugTextDelegate = (ScriptingInterfaceOfIDebug.RenderDebugTextDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.RenderDebugTextDelegate));
				return;
			case 130:
				ScriptingInterfaceOfIDebug.call_RenderDebugText3dDelegate = (ScriptingInterfaceOfIDebug.RenderDebugText3dDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.RenderDebugText3dDelegate));
				return;
			case 131:
				ScriptingInterfaceOfIDebug.call_SetDumpGenerationDisabledDelegate = (ScriptingInterfaceOfIDebug.SetDumpGenerationDisabledDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.SetDumpGenerationDisabledDelegate));
				return;
			case 132:
				ScriptingInterfaceOfIDebug.call_SetErrorReportSceneDelegate = (ScriptingInterfaceOfIDebug.SetErrorReportSceneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.SetErrorReportSceneDelegate));
				return;
			case 133:
				ScriptingInterfaceOfIDebug.call_SetShowDebugInfoDelegate = (ScriptingInterfaceOfIDebug.SetShowDebugInfoDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.SetShowDebugInfoDelegate));
				return;
			case 134:
				ScriptingInterfaceOfIDebug.call_SilentAssertDelegate = (ScriptingInterfaceOfIDebug.SilentAssertDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.SilentAssertDelegate));
				return;
			case 135:
				ScriptingInterfaceOfIDebug.call_WarningDelegate = (ScriptingInterfaceOfIDebug.WarningDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.WarningDelegate));
				return;
			case 136:
				ScriptingInterfaceOfIDebug.call_WriteDebugLineOnScreenDelegate = (ScriptingInterfaceOfIDebug.WriteDebugLineOnScreenDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.WriteDebugLineOnScreenDelegate));
				return;
			case 137:
				ScriptingInterfaceOfIDebug.call_WriteLineDelegate = (ScriptingInterfaceOfIDebug.WriteLineDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDebug.WriteLineDelegate));
				return;
			case 138:
				ScriptingInterfaceOfIDecal.call_CreateCopyDelegate = (ScriptingInterfaceOfIDecal.CreateCopyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDecal.CreateCopyDelegate));
				return;
			case 139:
				ScriptingInterfaceOfIDecal.call_CreateDecalDelegate = (ScriptingInterfaceOfIDecal.CreateDecalDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDecal.CreateDecalDelegate));
				return;
			case 140:
				ScriptingInterfaceOfIDecal.call_GetFactor1Delegate = (ScriptingInterfaceOfIDecal.GetFactor1Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDecal.GetFactor1Delegate));
				return;
			case 141:
				ScriptingInterfaceOfIDecal.call_GetFrameDelegate = (ScriptingInterfaceOfIDecal.GetFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDecal.GetFrameDelegate));
				return;
			case 142:
				ScriptingInterfaceOfIDecal.call_GetMaterialDelegate = (ScriptingInterfaceOfIDecal.GetMaterialDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDecal.GetMaterialDelegate));
				return;
			case 143:
				ScriptingInterfaceOfIDecal.call_SetFactor1Delegate = (ScriptingInterfaceOfIDecal.SetFactor1Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDecal.SetFactor1Delegate));
				return;
			case 144:
				ScriptingInterfaceOfIDecal.call_SetFactor1LinearDelegate = (ScriptingInterfaceOfIDecal.SetFactor1LinearDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDecal.SetFactor1LinearDelegate));
				return;
			case 145:
				ScriptingInterfaceOfIDecal.call_SetFrameDelegate = (ScriptingInterfaceOfIDecal.SetFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDecal.SetFrameDelegate));
				return;
			case 146:
				ScriptingInterfaceOfIDecal.call_SetMaterialDelegate = (ScriptingInterfaceOfIDecal.SetMaterialDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDecal.SetMaterialDelegate));
				return;
			case 147:
				ScriptingInterfaceOfIDecal.call_SetVectorArgumentDelegate = (ScriptingInterfaceOfIDecal.SetVectorArgumentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDecal.SetVectorArgumentDelegate));
				return;
			case 148:
				ScriptingInterfaceOfIDecal.call_SetVectorArgument2Delegate = (ScriptingInterfaceOfIDecal.SetVectorArgument2Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIDecal.SetVectorArgument2Delegate));
				return;
			case 149:
				ScriptingInterfaceOfIEngineSizeChecker.call_GetEngineStructMemberOffsetDelegate = (ScriptingInterfaceOfIEngineSizeChecker.GetEngineStructMemberOffsetDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIEngineSizeChecker.GetEngineStructMemberOffsetDelegate));
				return;
			case 150:
				ScriptingInterfaceOfIEngineSizeChecker.call_GetEngineStructSizeDelegate = (ScriptingInterfaceOfIEngineSizeChecker.GetEngineStructSizeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIEngineSizeChecker.GetEngineStructSizeDelegate));
				return;
			case 151:
				ScriptingInterfaceOfIGameEntity.call_ActivateRagdollDelegate = (ScriptingInterfaceOfIGameEntity.ActivateRagdollDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.ActivateRagdollDelegate));
				return;
			case 152:
				ScriptingInterfaceOfIGameEntity.call_AddAllMeshesOfGameEntityDelegate = (ScriptingInterfaceOfIGameEntity.AddAllMeshesOfGameEntityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.AddAllMeshesOfGameEntityDelegate));
				return;
			case 153:
				ScriptingInterfaceOfIGameEntity.call_AddChildDelegate = (ScriptingInterfaceOfIGameEntity.AddChildDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.AddChildDelegate));
				return;
			case 154:
				ScriptingInterfaceOfIGameEntity.call_AddComponentDelegate = (ScriptingInterfaceOfIGameEntity.AddComponentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.AddComponentDelegate));
				return;
			case 155:
				ScriptingInterfaceOfIGameEntity.call_AddDistanceJointDelegate = (ScriptingInterfaceOfIGameEntity.AddDistanceJointDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.AddDistanceJointDelegate));
				return;
			case 156:
				ScriptingInterfaceOfIGameEntity.call_AddEditDataUserToAllMeshesDelegate = (ScriptingInterfaceOfIGameEntity.AddEditDataUserToAllMeshesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.AddEditDataUserToAllMeshesDelegate));
				return;
			case 157:
				ScriptingInterfaceOfIGameEntity.call_AddLightDelegate = (ScriptingInterfaceOfIGameEntity.AddLightDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.AddLightDelegate));
				return;
			case 158:
				ScriptingInterfaceOfIGameEntity.call_AddMeshDelegate = (ScriptingInterfaceOfIGameEntity.AddMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.AddMeshDelegate));
				return;
			case 159:
				ScriptingInterfaceOfIGameEntity.call_AddMeshToBoneDelegate = (ScriptingInterfaceOfIGameEntity.AddMeshToBoneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.AddMeshToBoneDelegate));
				return;
			case 160:
				ScriptingInterfaceOfIGameEntity.call_AddMultiMeshDelegate = (ScriptingInterfaceOfIGameEntity.AddMultiMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.AddMultiMeshDelegate));
				return;
			case 161:
				ScriptingInterfaceOfIGameEntity.call_AddMultiMeshToSkeletonDelegate = (ScriptingInterfaceOfIGameEntity.AddMultiMeshToSkeletonDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.AddMultiMeshToSkeletonDelegate));
				return;
			case 162:
				ScriptingInterfaceOfIGameEntity.call_AddMultiMeshToSkeletonBoneDelegate = (ScriptingInterfaceOfIGameEntity.AddMultiMeshToSkeletonBoneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.AddMultiMeshToSkeletonBoneDelegate));
				return;
			case 163:
				ScriptingInterfaceOfIGameEntity.call_AddParticleSystemComponentDelegate = (ScriptingInterfaceOfIGameEntity.AddParticleSystemComponentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.AddParticleSystemComponentDelegate));
				return;
			case 164:
				ScriptingInterfaceOfIGameEntity.call_AddPhysicsDelegate = (ScriptingInterfaceOfIGameEntity.AddPhysicsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.AddPhysicsDelegate));
				return;
			case 165:
				ScriptingInterfaceOfIGameEntity.call_AddSphereAsBodyDelegate = (ScriptingInterfaceOfIGameEntity.AddSphereAsBodyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.AddSphereAsBodyDelegate));
				return;
			case 166:
				ScriptingInterfaceOfIGameEntity.call_AddTagDelegate = (ScriptingInterfaceOfIGameEntity.AddTagDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.AddTagDelegate));
				return;
			case 167:
				ScriptingInterfaceOfIGameEntity.call_ApplyAccelerationToDynamicBodyDelegate = (ScriptingInterfaceOfIGameEntity.ApplyAccelerationToDynamicBodyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.ApplyAccelerationToDynamicBodyDelegate));
				return;
			case 168:
				ScriptingInterfaceOfIGameEntity.call_ApplyForceToDynamicBodyDelegate = (ScriptingInterfaceOfIGameEntity.ApplyForceToDynamicBodyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.ApplyForceToDynamicBodyDelegate));
				return;
			case 169:
				ScriptingInterfaceOfIGameEntity.call_ApplyLocalForceToDynamicBodyDelegate = (ScriptingInterfaceOfIGameEntity.ApplyLocalForceToDynamicBodyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.ApplyLocalForceToDynamicBodyDelegate));
				return;
			case 170:
				ScriptingInterfaceOfIGameEntity.call_ApplyLocalImpulseToDynamicBodyDelegate = (ScriptingInterfaceOfIGameEntity.ApplyLocalImpulseToDynamicBodyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.ApplyLocalImpulseToDynamicBodyDelegate));
				return;
			case 171:
				ScriptingInterfaceOfIGameEntity.call_AttachNavigationMeshFacesDelegate = (ScriptingInterfaceOfIGameEntity.AttachNavigationMeshFacesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.AttachNavigationMeshFacesDelegate));
				return;
			case 172:
				ScriptingInterfaceOfIGameEntity.call_BreakPrefabDelegate = (ScriptingInterfaceOfIGameEntity.BreakPrefabDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.BreakPrefabDelegate));
				return;
			case 173:
				ScriptingInterfaceOfIGameEntity.call_BurstEntityParticleDelegate = (ScriptingInterfaceOfIGameEntity.BurstEntityParticleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.BurstEntityParticleDelegate));
				return;
			case 174:
				ScriptingInterfaceOfIGameEntity.call_CallScriptCallbacksDelegate = (ScriptingInterfaceOfIGameEntity.CallScriptCallbacksDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.CallScriptCallbacksDelegate));
				return;
			case 175:
				ScriptingInterfaceOfIGameEntity.call_ChangeMetaMeshOrRemoveItIfNotExistsDelegate = (ScriptingInterfaceOfIGameEntity.ChangeMetaMeshOrRemoveItIfNotExistsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.ChangeMetaMeshOrRemoveItIfNotExistsDelegate));
				return;
			case 176:
				ScriptingInterfaceOfIGameEntity.call_CheckPointWithOrientedBoundingBoxDelegate = (ScriptingInterfaceOfIGameEntity.CheckPointWithOrientedBoundingBoxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.CheckPointWithOrientedBoundingBoxDelegate));
				return;
			case 177:
				ScriptingInterfaceOfIGameEntity.call_CheckResourcesDelegate = (ScriptingInterfaceOfIGameEntity.CheckResourcesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.CheckResourcesDelegate));
				return;
			case 178:
				ScriptingInterfaceOfIGameEntity.call_ClearComponentsDelegate = (ScriptingInterfaceOfIGameEntity.ClearComponentsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.ClearComponentsDelegate));
				return;
			case 179:
				ScriptingInterfaceOfIGameEntity.call_ClearEntityComponentsDelegate = (ScriptingInterfaceOfIGameEntity.ClearEntityComponentsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.ClearEntityComponentsDelegate));
				return;
			case 180:
				ScriptingInterfaceOfIGameEntity.call_ClearOnlyOwnComponentsDelegate = (ScriptingInterfaceOfIGameEntity.ClearOnlyOwnComponentsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.ClearOnlyOwnComponentsDelegate));
				return;
			case 181:
				ScriptingInterfaceOfIGameEntity.call_ComputeTrajectoryVolumeDelegate = (ScriptingInterfaceOfIGameEntity.ComputeTrajectoryVolumeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.ComputeTrajectoryVolumeDelegate));
				return;
			case 182:
				ScriptingInterfaceOfIGameEntity.call_CopyComponentsToSkeletonDelegate = (ScriptingInterfaceOfIGameEntity.CopyComponentsToSkeletonDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.CopyComponentsToSkeletonDelegate));
				return;
			case 183:
				ScriptingInterfaceOfIGameEntity.call_CopyFromPrefabDelegate = (ScriptingInterfaceOfIGameEntity.CopyFromPrefabDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.CopyFromPrefabDelegate));
				return;
			case 184:
				ScriptingInterfaceOfIGameEntity.call_CopyScriptComponentFromAnotherEntityDelegate = (ScriptingInterfaceOfIGameEntity.CopyScriptComponentFromAnotherEntityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.CopyScriptComponentFromAnotherEntityDelegate));
				return;
			case 185:
				ScriptingInterfaceOfIGameEntity.call_CreateAndAddScriptComponentDelegate = (ScriptingInterfaceOfIGameEntity.CreateAndAddScriptComponentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.CreateAndAddScriptComponentDelegate));
				return;
			case 186:
				ScriptingInterfaceOfIGameEntity.call_CreateEmptyDelegate = (ScriptingInterfaceOfIGameEntity.CreateEmptyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.CreateEmptyDelegate));
				return;
			case 187:
				ScriptingInterfaceOfIGameEntity.call_CreateEmptyWithoutSceneDelegate = (ScriptingInterfaceOfIGameEntity.CreateEmptyWithoutSceneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.CreateEmptyWithoutSceneDelegate));
				return;
			case 188:
				ScriptingInterfaceOfIGameEntity.call_CreateFromPrefabDelegate = (ScriptingInterfaceOfIGameEntity.CreateFromPrefabDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.CreateFromPrefabDelegate));
				return;
			case 189:
				ScriptingInterfaceOfIGameEntity.call_CreateFromPrefabWithInitialFrameDelegate = (ScriptingInterfaceOfIGameEntity.CreateFromPrefabWithInitialFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.CreateFromPrefabWithInitialFrameDelegate));
				return;
			case 190:
				ScriptingInterfaceOfIGameEntity.call_DeselectEntityOnEditorDelegate = (ScriptingInterfaceOfIGameEntity.DeselectEntityOnEditorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.DeselectEntityOnEditorDelegate));
				return;
			case 191:
				ScriptingInterfaceOfIGameEntity.call_DisableContourDelegate = (ScriptingInterfaceOfIGameEntity.DisableContourDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.DisableContourDelegate));
				return;
			case 192:
				ScriptingInterfaceOfIGameEntity.call_DisableDynamicBodySimulationDelegate = (ScriptingInterfaceOfIGameEntity.DisableDynamicBodySimulationDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.DisableDynamicBodySimulationDelegate));
				return;
			case 193:
				ScriptingInterfaceOfIGameEntity.call_DisableGravityDelegate = (ScriptingInterfaceOfIGameEntity.DisableGravityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.DisableGravityDelegate));
				return;
			case 194:
				ScriptingInterfaceOfIGameEntity.call_EnableDynamicBodyDelegate = (ScriptingInterfaceOfIGameEntity.EnableDynamicBodyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.EnableDynamicBodyDelegate));
				return;
			case 195:
				ScriptingInterfaceOfIGameEntity.call_FindWithNameDelegate = (ScriptingInterfaceOfIGameEntity.FindWithNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.FindWithNameDelegate));
				return;
			case 196:
				ScriptingInterfaceOfIGameEntity.call_FreezeDelegate = (ScriptingInterfaceOfIGameEntity.FreezeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.FreezeDelegate));
				return;
			case 197:
				ScriptingInterfaceOfIGameEntity.call_GetBodyFlagsDelegate = (ScriptingInterfaceOfIGameEntity.GetBodyFlagsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetBodyFlagsDelegate));
				return;
			case 198:
				ScriptingInterfaceOfIGameEntity.call_GetBodyShapeDelegate = (ScriptingInterfaceOfIGameEntity.GetBodyShapeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetBodyShapeDelegate));
				return;
			case 199:
				ScriptingInterfaceOfIGameEntity.call_GetBoneCountDelegate = (ScriptingInterfaceOfIGameEntity.GetBoneCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetBoneCountDelegate));
				return;
			case 200:
				ScriptingInterfaceOfIGameEntity.call_GetBoneEntitialFrameWithIndexDelegate = (ScriptingInterfaceOfIGameEntity.GetBoneEntitialFrameWithIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetBoneEntitialFrameWithIndexDelegate));
				return;
			case 201:
				ScriptingInterfaceOfIGameEntity.call_GetBoneEntitialFrameWithNameDelegate = (ScriptingInterfaceOfIGameEntity.GetBoneEntitialFrameWithNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetBoneEntitialFrameWithNameDelegate));
				return;
			case 202:
				ScriptingInterfaceOfIGameEntity.call_GetBoundingBoxMaxDelegate = (ScriptingInterfaceOfIGameEntity.GetBoundingBoxMaxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetBoundingBoxMaxDelegate));
				return;
			case 203:
				ScriptingInterfaceOfIGameEntity.call_GetBoundingBoxMinDelegate = (ScriptingInterfaceOfIGameEntity.GetBoundingBoxMinDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetBoundingBoxMinDelegate));
				return;
			case 204:
				ScriptingInterfaceOfIGameEntity.call_GetCameraParamsFromCameraScriptDelegate = (ScriptingInterfaceOfIGameEntity.GetCameraParamsFromCameraScriptDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetCameraParamsFromCameraScriptDelegate));
				return;
			case 205:
				ScriptingInterfaceOfIGameEntity.call_GetCenterOfMassDelegate = (ScriptingInterfaceOfIGameEntity.GetCenterOfMassDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetCenterOfMassDelegate));
				return;
			case 206:
				ScriptingInterfaceOfIGameEntity.call_GetChildDelegate = (ScriptingInterfaceOfIGameEntity.GetChildDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetChildDelegate));
				return;
			case 207:
				ScriptingInterfaceOfIGameEntity.call_GetChildCountDelegate = (ScriptingInterfaceOfIGameEntity.GetChildCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetChildCountDelegate));
				return;
			case 208:
				ScriptingInterfaceOfIGameEntity.call_GetComponentAtIndexDelegate = (ScriptingInterfaceOfIGameEntity.GetComponentAtIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetComponentAtIndexDelegate));
				return;
			case 209:
				ScriptingInterfaceOfIGameEntity.call_GetComponentCountDelegate = (ScriptingInterfaceOfIGameEntity.GetComponentCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetComponentCountDelegate));
				return;
			case 210:
				ScriptingInterfaceOfIGameEntity.call_GetEditModeLevelVisibilityDelegate = (ScriptingInterfaceOfIGameEntity.GetEditModeLevelVisibilityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetEditModeLevelVisibilityDelegate));
				return;
			case 211:
				ScriptingInterfaceOfIGameEntity.call_GetEntityFlagsDelegate = (ScriptingInterfaceOfIGameEntity.GetEntityFlagsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetEntityFlagsDelegate));
				return;
			case 212:
				ScriptingInterfaceOfIGameEntity.call_GetEntityVisibilityFlagsDelegate = (ScriptingInterfaceOfIGameEntity.GetEntityVisibilityFlagsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetEntityVisibilityFlagsDelegate));
				return;
			case 213:
				ScriptingInterfaceOfIGameEntity.call_GetFactorColorDelegate = (ScriptingInterfaceOfIGameEntity.GetFactorColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetFactorColorDelegate));
				return;
			case 214:
				ScriptingInterfaceOfIGameEntity.call_GetFirstEntityWithTagDelegate = (ScriptingInterfaceOfIGameEntity.GetFirstEntityWithTagDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetFirstEntityWithTagDelegate));
				return;
			case 215:
				ScriptingInterfaceOfIGameEntity.call_GetFirstEntityWithTagExpressionDelegate = (ScriptingInterfaceOfIGameEntity.GetFirstEntityWithTagExpressionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetFirstEntityWithTagExpressionDelegate));
				return;
			case 216:
				ScriptingInterfaceOfIGameEntity.call_GetFirstMeshDelegate = (ScriptingInterfaceOfIGameEntity.GetFirstMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetFirstMeshDelegate));
				return;
			case 217:
				ScriptingInterfaceOfIGameEntity.call_GetFrameDelegate = (ScriptingInterfaceOfIGameEntity.GetFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetFrameDelegate));
				return;
			case 218:
				ScriptingInterfaceOfIGameEntity.call_GetGlobalBoxMaxDelegate = (ScriptingInterfaceOfIGameEntity.GetGlobalBoxMaxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetGlobalBoxMaxDelegate));
				return;
			case 219:
				ScriptingInterfaceOfIGameEntity.call_GetGlobalBoxMinDelegate = (ScriptingInterfaceOfIGameEntity.GetGlobalBoxMinDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetGlobalBoxMinDelegate));
				return;
			case 220:
				ScriptingInterfaceOfIGameEntity.call_GetGlobalFrameDelegate = (ScriptingInterfaceOfIGameEntity.GetGlobalFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetGlobalFrameDelegate));
				return;
			case 221:
				ScriptingInterfaceOfIGameEntity.call_GetGlobalScaleDelegate = (ScriptingInterfaceOfIGameEntity.GetGlobalScaleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetGlobalScaleDelegate));
				return;
			case 222:
				ScriptingInterfaceOfIGameEntity.call_GetGuidDelegate = (ScriptingInterfaceOfIGameEntity.GetGuidDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetGuidDelegate));
				return;
			case 223:
				ScriptingInterfaceOfIGameEntity.call_GetLightDelegate = (ScriptingInterfaceOfIGameEntity.GetLightDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetLightDelegate));
				return;
			case 224:
				ScriptingInterfaceOfIGameEntity.call_GetLinearVelocityDelegate = (ScriptingInterfaceOfIGameEntity.GetLinearVelocityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetLinearVelocityDelegate));
				return;
			case 225:
				ScriptingInterfaceOfIGameEntity.call_GetLodLevelForDistanceSqDelegate = (ScriptingInterfaceOfIGameEntity.GetLodLevelForDistanceSqDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetLodLevelForDistanceSqDelegate));
				return;
			case 226:
				ScriptingInterfaceOfIGameEntity.call_GetMassDelegate = (ScriptingInterfaceOfIGameEntity.GetMassDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetMassDelegate));
				return;
			case 227:
				ScriptingInterfaceOfIGameEntity.call_GetMeshBendedPositionDelegate = (ScriptingInterfaceOfIGameEntity.GetMeshBendedPositionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetMeshBendedPositionDelegate));
				return;
			case 228:
				ScriptingInterfaceOfIGameEntity.call_GetNameDelegate = (ScriptingInterfaceOfIGameEntity.GetNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetNameDelegate));
				return;
			case 229:
				ScriptingInterfaceOfIGameEntity.call_GetNextEntityWithTagDelegate = (ScriptingInterfaceOfIGameEntity.GetNextEntityWithTagDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetNextEntityWithTagDelegate));
				return;
			case 230:
				ScriptingInterfaceOfIGameEntity.call_GetNextEntityWithTagExpressionDelegate = (ScriptingInterfaceOfIGameEntity.GetNextEntityWithTagExpressionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetNextEntityWithTagExpressionDelegate));
				return;
			case 231:
				ScriptingInterfaceOfIGameEntity.call_GetNextPrefabDelegate = (ScriptingInterfaceOfIGameEntity.GetNextPrefabDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetNextPrefabDelegate));
				return;
			case 232:
				ScriptingInterfaceOfIGameEntity.call_GetOldPrefabNameDelegate = (ScriptingInterfaceOfIGameEntity.GetOldPrefabNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetOldPrefabNameDelegate));
				return;
			case 233:
				ScriptingInterfaceOfIGameEntity.call_GetParentDelegate = (ScriptingInterfaceOfIGameEntity.GetParentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetParentDelegate));
				return;
			case 234:
				ScriptingInterfaceOfIGameEntity.call_GetPhysicsBoundingBoxMaxDelegate = (ScriptingInterfaceOfIGameEntity.GetPhysicsBoundingBoxMaxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetPhysicsBoundingBoxMaxDelegate));
				return;
			case 235:
				ScriptingInterfaceOfIGameEntity.call_GetPhysicsBoundingBoxMinDelegate = (ScriptingInterfaceOfIGameEntity.GetPhysicsBoundingBoxMinDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetPhysicsBoundingBoxMinDelegate));
				return;
			case 236:
				ScriptingInterfaceOfIGameEntity.call_GetPhysicsDescBodyFlagsDelegate = (ScriptingInterfaceOfIGameEntity.GetPhysicsDescBodyFlagsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetPhysicsDescBodyFlagsDelegate));
				return;
			case 237:
				ScriptingInterfaceOfIGameEntity.call_GetPhysicsMinMaxDelegate = (ScriptingInterfaceOfIGameEntity.GetPhysicsMinMaxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetPhysicsMinMaxDelegate));
				return;
			case 238:
				ScriptingInterfaceOfIGameEntity.call_GetPhysicsStateDelegate = (ScriptingInterfaceOfIGameEntity.GetPhysicsStateDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetPhysicsStateDelegate));
				return;
			case 239:
				ScriptingInterfaceOfIGameEntity.call_GetPrefabNameDelegate = (ScriptingInterfaceOfIGameEntity.GetPrefabNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetPrefabNameDelegate));
				return;
			case 240:
				ScriptingInterfaceOfIGameEntity.call_GetPreviousGlobalFrameDelegate = (ScriptingInterfaceOfIGameEntity.GetPreviousGlobalFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetPreviousGlobalFrameDelegate));
				return;
			case 241:
				ScriptingInterfaceOfIGameEntity.call_GetQuickBoneEntitialFrameDelegate = (ScriptingInterfaceOfIGameEntity.GetQuickBoneEntitialFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetQuickBoneEntitialFrameDelegate));
				return;
			case 242:
				ScriptingInterfaceOfIGameEntity.call_GetRadiusDelegate = (ScriptingInterfaceOfIGameEntity.GetRadiusDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetRadiusDelegate));
				return;
			case 243:
				ScriptingInterfaceOfIGameEntity.call_GetSceneDelegate = (ScriptingInterfaceOfIGameEntity.GetSceneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetSceneDelegate));
				return;
			case 244:
				ScriptingInterfaceOfIGameEntity.call_GetScenePointerDelegate = (ScriptingInterfaceOfIGameEntity.GetScenePointerDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetScenePointerDelegate));
				return;
			case 245:
				ScriptingInterfaceOfIGameEntity.call_GetScriptComponentDelegate = (ScriptingInterfaceOfIGameEntity.GetScriptComponentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetScriptComponentDelegate));
				return;
			case 246:
				ScriptingInterfaceOfIGameEntity.call_GetScriptComponentAtIndexDelegate = (ScriptingInterfaceOfIGameEntity.GetScriptComponentAtIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetScriptComponentAtIndexDelegate));
				return;
			case 247:
				ScriptingInterfaceOfIGameEntity.call_GetScriptComponentCountDelegate = (ScriptingInterfaceOfIGameEntity.GetScriptComponentCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetScriptComponentCountDelegate));
				return;
			case 248:
				ScriptingInterfaceOfIGameEntity.call_GetSkeletonDelegate = (ScriptingInterfaceOfIGameEntity.GetSkeletonDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetSkeletonDelegate));
				return;
			case 249:
				ScriptingInterfaceOfIGameEntity.call_GetTagsDelegate = (ScriptingInterfaceOfIGameEntity.GetTagsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetTagsDelegate));
				return;
			case 250:
				ScriptingInterfaceOfIGameEntity.call_GetUpgradeLevelMaskDelegate = (ScriptingInterfaceOfIGameEntity.GetUpgradeLevelMaskDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetUpgradeLevelMaskDelegate));
				return;
			case 251:
				ScriptingInterfaceOfIGameEntity.call_GetUpgradeLevelMaskCumulativeDelegate = (ScriptingInterfaceOfIGameEntity.GetUpgradeLevelMaskCumulativeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetUpgradeLevelMaskCumulativeDelegate));
				return;
			case 252:
				ScriptingInterfaceOfIGameEntity.call_GetVisibilityExcludeParentsDelegate = (ScriptingInterfaceOfIGameEntity.GetVisibilityExcludeParentsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetVisibilityExcludeParentsDelegate));
				return;
			case 253:
				ScriptingInterfaceOfIGameEntity.call_GetVisibilityLevelMaskIncludingParentsDelegate = (ScriptingInterfaceOfIGameEntity.GetVisibilityLevelMaskIncludingParentsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.GetVisibilityLevelMaskIncludingParentsDelegate));
				return;
			case 254:
				ScriptingInterfaceOfIGameEntity.call_HasBodyDelegate = (ScriptingInterfaceOfIGameEntity.HasBodyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.HasBodyDelegate));
				return;
			case 255:
				ScriptingInterfaceOfIGameEntity.call_HasComplexAnimTreeDelegate = (ScriptingInterfaceOfIGameEntity.HasComplexAnimTreeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.HasComplexAnimTreeDelegate));
				return;
			case 256:
				ScriptingInterfaceOfIGameEntity.call_HasComponentDelegate = (ScriptingInterfaceOfIGameEntity.HasComponentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.HasComponentDelegate));
				return;
			case 257:
				ScriptingInterfaceOfIGameEntity.call_HasFrameChangedDelegate = (ScriptingInterfaceOfIGameEntity.HasFrameChangedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.HasFrameChangedDelegate));
				return;
			case 258:
				ScriptingInterfaceOfIGameEntity.call_HasPhysicsBodyDelegate = (ScriptingInterfaceOfIGameEntity.HasPhysicsBodyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.HasPhysicsBodyDelegate));
				return;
			case 259:
				ScriptingInterfaceOfIGameEntity.call_HasPhysicsDefinitionDelegate = (ScriptingInterfaceOfIGameEntity.HasPhysicsDefinitionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.HasPhysicsDefinitionDelegate));
				return;
			case 260:
				ScriptingInterfaceOfIGameEntity.call_HasSceneDelegate = (ScriptingInterfaceOfIGameEntity.HasSceneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.HasSceneDelegate));
				return;
			case 261:
				ScriptingInterfaceOfIGameEntity.call_HasScriptComponentDelegate = (ScriptingInterfaceOfIGameEntity.HasScriptComponentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.HasScriptComponentDelegate));
				return;
			case 262:
				ScriptingInterfaceOfIGameEntity.call_HasTagDelegate = (ScriptingInterfaceOfIGameEntity.HasTagDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.HasTagDelegate));
				return;
			case 263:
				ScriptingInterfaceOfIGameEntity.call_IsDynamicBodyStationaryDelegate = (ScriptingInterfaceOfIGameEntity.IsDynamicBodyStationaryDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.IsDynamicBodyStationaryDelegate));
				return;
			case 264:
				ScriptingInterfaceOfIGameEntity.call_IsEngineBodySleepingDelegate = (ScriptingInterfaceOfIGameEntity.IsEngineBodySleepingDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.IsEngineBodySleepingDelegate));
				return;
			case 265:
				ScriptingInterfaceOfIGameEntity.call_IsEntitySelectedOnEditorDelegate = (ScriptingInterfaceOfIGameEntity.IsEntitySelectedOnEditorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.IsEntitySelectedOnEditorDelegate));
				return;
			case 266:
				ScriptingInterfaceOfIGameEntity.call_IsFrozenDelegate = (ScriptingInterfaceOfIGameEntity.IsFrozenDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.IsFrozenDelegate));
				return;
			case 267:
				ScriptingInterfaceOfIGameEntity.call_IsGhostObjectDelegate = (ScriptingInterfaceOfIGameEntity.IsGhostObjectDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.IsGhostObjectDelegate));
				return;
			case 268:
				ScriptingInterfaceOfIGameEntity.call_IsGuidValidDelegate = (ScriptingInterfaceOfIGameEntity.IsGuidValidDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.IsGuidValidDelegate));
				return;
			case 269:
				ScriptingInterfaceOfIGameEntity.call_IsVisibleIncludeParentsDelegate = (ScriptingInterfaceOfIGameEntity.IsVisibleIncludeParentsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.IsVisibleIncludeParentsDelegate));
				return;
			case 270:
				ScriptingInterfaceOfIGameEntity.call_PauseParticleSystemDelegate = (ScriptingInterfaceOfIGameEntity.PauseParticleSystemDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.PauseParticleSystemDelegate));
				return;
			case 271:
				ScriptingInterfaceOfIGameEntity.call_PrefabExistsDelegate = (ScriptingInterfaceOfIGameEntity.PrefabExistsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.PrefabExistsDelegate));
				return;
			case 272:
				ScriptingInterfaceOfIGameEntity.call_RecomputeBoundingBoxDelegate = (ScriptingInterfaceOfIGameEntity.RecomputeBoundingBoxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.RecomputeBoundingBoxDelegate));
				return;
			case 273:
				ScriptingInterfaceOfIGameEntity.call_ReleaseEditDataUserToAllMeshesDelegate = (ScriptingInterfaceOfIGameEntity.ReleaseEditDataUserToAllMeshesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.ReleaseEditDataUserToAllMeshesDelegate));
				return;
			case 274:
				ScriptingInterfaceOfIGameEntity.call_RemoveDelegate = (ScriptingInterfaceOfIGameEntity.RemoveDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.RemoveDelegate));
				return;
			case 275:
				ScriptingInterfaceOfIGameEntity.call_RemoveAllChildrenDelegate = (ScriptingInterfaceOfIGameEntity.RemoveAllChildrenDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.RemoveAllChildrenDelegate));
				return;
			case 276:
				ScriptingInterfaceOfIGameEntity.call_RemoveAllParticleSystemsDelegate = (ScriptingInterfaceOfIGameEntity.RemoveAllParticleSystemsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.RemoveAllParticleSystemsDelegate));
				return;
			case 277:
				ScriptingInterfaceOfIGameEntity.call_RemoveChildDelegate = (ScriptingInterfaceOfIGameEntity.RemoveChildDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.RemoveChildDelegate));
				return;
			case 278:
				ScriptingInterfaceOfIGameEntity.call_RemoveComponentDelegate = (ScriptingInterfaceOfIGameEntity.RemoveComponentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.RemoveComponentDelegate));
				return;
			case 279:
				ScriptingInterfaceOfIGameEntity.call_RemoveComponentWithMeshDelegate = (ScriptingInterfaceOfIGameEntity.RemoveComponentWithMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.RemoveComponentWithMeshDelegate));
				return;
			case 280:
				ScriptingInterfaceOfIGameEntity.call_RemoveEnginePhysicsDelegate = (ScriptingInterfaceOfIGameEntity.RemoveEnginePhysicsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.RemoveEnginePhysicsDelegate));
				return;
			case 281:
				ScriptingInterfaceOfIGameEntity.call_RemoveFromPredisplayEntityDelegate = (ScriptingInterfaceOfIGameEntity.RemoveFromPredisplayEntityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.RemoveFromPredisplayEntityDelegate));
				return;
			case 282:
				ScriptingInterfaceOfIGameEntity.call_RemoveMultiMeshDelegate = (ScriptingInterfaceOfIGameEntity.RemoveMultiMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.RemoveMultiMeshDelegate));
				return;
			case 283:
				ScriptingInterfaceOfIGameEntity.call_RemoveMultiMeshFromSkeletonDelegate = (ScriptingInterfaceOfIGameEntity.RemoveMultiMeshFromSkeletonDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.RemoveMultiMeshFromSkeletonDelegate));
				return;
			case 284:
				ScriptingInterfaceOfIGameEntity.call_RemoveMultiMeshFromSkeletonBoneDelegate = (ScriptingInterfaceOfIGameEntity.RemoveMultiMeshFromSkeletonBoneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.RemoveMultiMeshFromSkeletonBoneDelegate));
				return;
			case 285:
				ScriptingInterfaceOfIGameEntity.call_RemovePhysicsDelegate = (ScriptingInterfaceOfIGameEntity.RemovePhysicsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.RemovePhysicsDelegate));
				return;
			case 286:
				ScriptingInterfaceOfIGameEntity.call_RemoveScriptComponentDelegate = (ScriptingInterfaceOfIGameEntity.RemoveScriptComponentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.RemoveScriptComponentDelegate));
				return;
			case 287:
				ScriptingInterfaceOfIGameEntity.call_RemoveTagDelegate = (ScriptingInterfaceOfIGameEntity.RemoveTagDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.RemoveTagDelegate));
				return;
			case 288:
				ScriptingInterfaceOfIGameEntity.call_ResumeParticleSystemDelegate = (ScriptingInterfaceOfIGameEntity.ResumeParticleSystemDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.ResumeParticleSystemDelegate));
				return;
			case 289:
				ScriptingInterfaceOfIGameEntity.call_SelectEntityOnEditorDelegate = (ScriptingInterfaceOfIGameEntity.SelectEntityOnEditorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SelectEntityOnEditorDelegate));
				return;
			case 290:
				ScriptingInterfaceOfIGameEntity.call_SetAlphaDelegate = (ScriptingInterfaceOfIGameEntity.SetAlphaDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetAlphaDelegate));
				return;
			case 291:
				ScriptingInterfaceOfIGameEntity.call_SetAnimationSoundActivationDelegate = (ScriptingInterfaceOfIGameEntity.SetAnimationSoundActivationDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetAnimationSoundActivationDelegate));
				return;
			case 292:
				ScriptingInterfaceOfIGameEntity.call_SetAnimTreeChannelParameterDelegate = (ScriptingInterfaceOfIGameEntity.SetAnimTreeChannelParameterDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetAnimTreeChannelParameterDelegate));
				return;
			case 293:
				ScriptingInterfaceOfIGameEntity.call_SetAsContourEntityDelegate = (ScriptingInterfaceOfIGameEntity.SetAsContourEntityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetAsContourEntityDelegate));
				return;
			case 294:
				ScriptingInterfaceOfIGameEntity.call_SetAsPredisplayEntityDelegate = (ScriptingInterfaceOfIGameEntity.SetAsPredisplayEntityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetAsPredisplayEntityDelegate));
				return;
			case 295:
				ScriptingInterfaceOfIGameEntity.call_SetAsReplayEntityDelegate = (ScriptingInterfaceOfIGameEntity.SetAsReplayEntityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetAsReplayEntityDelegate));
				return;
			case 296:
				ScriptingInterfaceOfIGameEntity.call_SetBodyFlagsDelegate = (ScriptingInterfaceOfIGameEntity.SetBodyFlagsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetBodyFlagsDelegate));
				return;
			case 297:
				ScriptingInterfaceOfIGameEntity.call_SetBodyFlagsRecursiveDelegate = (ScriptingInterfaceOfIGameEntity.SetBodyFlagsRecursiveDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetBodyFlagsRecursiveDelegate));
				return;
			case 298:
				ScriptingInterfaceOfIGameEntity.call_SetBodyShapeDelegate = (ScriptingInterfaceOfIGameEntity.SetBodyShapeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetBodyShapeDelegate));
				return;
			case 299:
				ScriptingInterfaceOfIGameEntity.call_SetBoundingboxDirtyDelegate = (ScriptingInterfaceOfIGameEntity.SetBoundingboxDirtyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetBoundingboxDirtyDelegate));
				return;
			case 300:
				ScriptingInterfaceOfIGameEntity.call_SetClothComponentKeepStateDelegate = (ScriptingInterfaceOfIGameEntity.SetClothComponentKeepStateDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetClothComponentKeepStateDelegate));
				return;
			case 301:
				ScriptingInterfaceOfIGameEntity.call_SetClothComponentKeepStateOfAllMeshesDelegate = (ScriptingInterfaceOfIGameEntity.SetClothComponentKeepStateOfAllMeshesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetClothComponentKeepStateOfAllMeshesDelegate));
				return;
			case 302:
				ScriptingInterfaceOfIGameEntity.call_SetClothMaxDistanceMultiplierDelegate = (ScriptingInterfaceOfIGameEntity.SetClothMaxDistanceMultiplierDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetClothMaxDistanceMultiplierDelegate));
				return;
			case 303:
				ScriptingInterfaceOfIGameEntity.call_SetContourStateDelegate = (ScriptingInterfaceOfIGameEntity.SetContourStateDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetContourStateDelegate));
				return;
			case 304:
				ScriptingInterfaceOfIGameEntity.call_SetCullModeDelegate = (ScriptingInterfaceOfIGameEntity.SetCullModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetCullModeDelegate));
				return;
			case 305:
				ScriptingInterfaceOfIGameEntity.call_SetDampingDelegate = (ScriptingInterfaceOfIGameEntity.SetDampingDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetDampingDelegate));
				return;
			case 306:
				ScriptingInterfaceOfIGameEntity.call_SetEnforcedMaximumLodLevelDelegate = (ScriptingInterfaceOfIGameEntity.SetEnforcedMaximumLodLevelDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetEnforcedMaximumLodLevelDelegate));
				return;
			case 307:
				ScriptingInterfaceOfIGameEntity.call_SetEntityEnvMapVisibilityDelegate = (ScriptingInterfaceOfIGameEntity.SetEntityEnvMapVisibilityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetEntityEnvMapVisibilityDelegate));
				return;
			case 308:
				ScriptingInterfaceOfIGameEntity.call_SetEntityFlagsDelegate = (ScriptingInterfaceOfIGameEntity.SetEntityFlagsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetEntityFlagsDelegate));
				return;
			case 309:
				ScriptingInterfaceOfIGameEntity.call_SetEntityVisibilityFlagsDelegate = (ScriptingInterfaceOfIGameEntity.SetEntityVisibilityFlagsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetEntityVisibilityFlagsDelegate));
				return;
			case 310:
				ScriptingInterfaceOfIGameEntity.call_SetExternalReferencesUsageDelegate = (ScriptingInterfaceOfIGameEntity.SetExternalReferencesUsageDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetExternalReferencesUsageDelegate));
				return;
			case 311:
				ScriptingInterfaceOfIGameEntity.call_SetFactor2ColorDelegate = (ScriptingInterfaceOfIGameEntity.SetFactor2ColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetFactor2ColorDelegate));
				return;
			case 312:
				ScriptingInterfaceOfIGameEntity.call_SetFactorColorDelegate = (ScriptingInterfaceOfIGameEntity.SetFactorColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetFactorColorDelegate));
				return;
			case 313:
				ScriptingInterfaceOfIGameEntity.call_SetFrameDelegate = (ScriptingInterfaceOfIGameEntity.SetFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetFrameDelegate));
				return;
			case 314:
				ScriptingInterfaceOfIGameEntity.call_SetFrameChangedDelegate = (ScriptingInterfaceOfIGameEntity.SetFrameChangedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetFrameChangedDelegate));
				return;
			case 315:
				ScriptingInterfaceOfIGameEntity.call_SetGlobalFrameDelegate = (ScriptingInterfaceOfIGameEntity.SetGlobalFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetGlobalFrameDelegate));
				return;
			case 316:
				ScriptingInterfaceOfIGameEntity.call_SetLocalPositionDelegate = (ScriptingInterfaceOfIGameEntity.SetLocalPositionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetLocalPositionDelegate));
				return;
			case 317:
				ScriptingInterfaceOfIGameEntity.call_SetMassDelegate = (ScriptingInterfaceOfIGameEntity.SetMassDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetMassDelegate));
				return;
			case 318:
				ScriptingInterfaceOfIGameEntity.call_SetMassSpaceInertiaDelegate = (ScriptingInterfaceOfIGameEntity.SetMassSpaceInertiaDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetMassSpaceInertiaDelegate));
				return;
			case 319:
				ScriptingInterfaceOfIGameEntity.call_SetMaterialForAllMeshesDelegate = (ScriptingInterfaceOfIGameEntity.SetMaterialForAllMeshesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetMaterialForAllMeshesDelegate));
				return;
			case 320:
				ScriptingInterfaceOfIGameEntity.call_SetMobilityDelegate = (ScriptingInterfaceOfIGameEntity.SetMobilityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetMobilityDelegate));
				return;
			case 321:
				ScriptingInterfaceOfIGameEntity.call_SetMorphFrameOfComponentsDelegate = (ScriptingInterfaceOfIGameEntity.SetMorphFrameOfComponentsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetMorphFrameOfComponentsDelegate));
				return;
			case 322:
				ScriptingInterfaceOfIGameEntity.call_SetNameDelegate = (ScriptingInterfaceOfIGameEntity.SetNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetNameDelegate));
				return;
			case 323:
				ScriptingInterfaceOfIGameEntity.call_SetPhysicsStateDelegate = (ScriptingInterfaceOfIGameEntity.SetPhysicsStateDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetPhysicsStateDelegate));
				return;
			case 324:
				ScriptingInterfaceOfIGameEntity.call_SetPreviousFrameInvalidDelegate = (ScriptingInterfaceOfIGameEntity.SetPreviousFrameInvalidDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetPreviousFrameInvalidDelegate));
				return;
			case 325:
				ScriptingInterfaceOfIGameEntity.call_SetReadyToRenderDelegate = (ScriptingInterfaceOfIGameEntity.SetReadyToRenderDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetReadyToRenderDelegate));
				return;
			case 326:
				ScriptingInterfaceOfIGameEntity.call_SetRuntimeEmissionRateMultiplierDelegate = (ScriptingInterfaceOfIGameEntity.SetRuntimeEmissionRateMultiplierDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetRuntimeEmissionRateMultiplierDelegate));
				return;
			case 327:
				ScriptingInterfaceOfIGameEntity.call_SetSkeletonDelegate = (ScriptingInterfaceOfIGameEntity.SetSkeletonDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetSkeletonDelegate));
				return;
			case 328:
				ScriptingInterfaceOfIGameEntity.call_SetUpgradeLevelMaskDelegate = (ScriptingInterfaceOfIGameEntity.SetUpgradeLevelMaskDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetUpgradeLevelMaskDelegate));
				return;
			case 329:
				ScriptingInterfaceOfIGameEntity.call_SetVectorArgumentDelegate = (ScriptingInterfaceOfIGameEntity.SetVectorArgumentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetVectorArgumentDelegate));
				return;
			case 330:
				ScriptingInterfaceOfIGameEntity.call_SetVisibilityExcludeParentsDelegate = (ScriptingInterfaceOfIGameEntity.SetVisibilityExcludeParentsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.SetVisibilityExcludeParentsDelegate));
				return;
			case 331:
				ScriptingInterfaceOfIGameEntity.call_UpdateGlobalBoundsDelegate = (ScriptingInterfaceOfIGameEntity.UpdateGlobalBoundsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.UpdateGlobalBoundsDelegate));
				return;
			case 332:
				ScriptingInterfaceOfIGameEntity.call_UpdateTriadFrameForEditorDelegate = (ScriptingInterfaceOfIGameEntity.UpdateTriadFrameForEditorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.UpdateTriadFrameForEditorDelegate));
				return;
			case 333:
				ScriptingInterfaceOfIGameEntity.call_UpdateVisibilityMaskDelegate = (ScriptingInterfaceOfIGameEntity.UpdateVisibilityMaskDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.UpdateVisibilityMaskDelegate));
				return;
			case 334:
				ScriptingInterfaceOfIGameEntity.call_ValidateBoundingBoxDelegate = (ScriptingInterfaceOfIGameEntity.ValidateBoundingBoxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntity.ValidateBoundingBoxDelegate));
				return;
			case 335:
				ScriptingInterfaceOfIGameEntityComponent.call_GetEntityDelegate = (ScriptingInterfaceOfIGameEntityComponent.GetEntityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntityComponent.GetEntityDelegate));
				return;
			case 336:
				ScriptingInterfaceOfIGameEntityComponent.call_GetFirstMetaMeshDelegate = (ScriptingInterfaceOfIGameEntityComponent.GetFirstMetaMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIGameEntityComponent.GetFirstMetaMeshDelegate));
				return;
			case 337:
				ScriptingInterfaceOfIHighlights.call_AddHighlightDelegate = (ScriptingInterfaceOfIHighlights.AddHighlightDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIHighlights.AddHighlightDelegate));
				return;
			case 338:
				ScriptingInterfaceOfIHighlights.call_CloseGroupDelegate = (ScriptingInterfaceOfIHighlights.CloseGroupDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIHighlights.CloseGroupDelegate));
				return;
			case 339:
				ScriptingInterfaceOfIHighlights.call_InitializeDelegate = (ScriptingInterfaceOfIHighlights.InitializeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIHighlights.InitializeDelegate));
				return;
			case 340:
				ScriptingInterfaceOfIHighlights.call_OpenGroupDelegate = (ScriptingInterfaceOfIHighlights.OpenGroupDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIHighlights.OpenGroupDelegate));
				return;
			case 341:
				ScriptingInterfaceOfIHighlights.call_OpenSummaryDelegate = (ScriptingInterfaceOfIHighlights.OpenSummaryDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIHighlights.OpenSummaryDelegate));
				return;
			case 342:
				ScriptingInterfaceOfIHighlights.call_RemoveHighlightDelegate = (ScriptingInterfaceOfIHighlights.RemoveHighlightDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIHighlights.RemoveHighlightDelegate));
				return;
			case 343:
				ScriptingInterfaceOfIHighlights.call_SaveScreenshotDelegate = (ScriptingInterfaceOfIHighlights.SaveScreenshotDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIHighlights.SaveScreenshotDelegate));
				return;
			case 344:
				ScriptingInterfaceOfIHighlights.call_SaveVideoDelegate = (ScriptingInterfaceOfIHighlights.SaveVideoDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIHighlights.SaveVideoDelegate));
				return;
			case 345:
				ScriptingInterfaceOfIImgui.call_BeginDelegate = (ScriptingInterfaceOfIImgui.BeginDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.BeginDelegate));
				return;
			case 346:
				ScriptingInterfaceOfIImgui.call_BeginMainThreadScopeDelegate = (ScriptingInterfaceOfIImgui.BeginMainThreadScopeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.BeginMainThreadScopeDelegate));
				return;
			case 347:
				ScriptingInterfaceOfIImgui.call_BeginWithCloseButtonDelegate = (ScriptingInterfaceOfIImgui.BeginWithCloseButtonDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.BeginWithCloseButtonDelegate));
				return;
			case 348:
				ScriptingInterfaceOfIImgui.call_ButtonDelegate = (ScriptingInterfaceOfIImgui.ButtonDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.ButtonDelegate));
				return;
			case 349:
				ScriptingInterfaceOfIImgui.call_CheckboxDelegate = (ScriptingInterfaceOfIImgui.CheckboxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.CheckboxDelegate));
				return;
			case 350:
				ScriptingInterfaceOfIImgui.call_CollapsingHeaderDelegate = (ScriptingInterfaceOfIImgui.CollapsingHeaderDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.CollapsingHeaderDelegate));
				return;
			case 351:
				ScriptingInterfaceOfIImgui.call_ColumnsDelegate = (ScriptingInterfaceOfIImgui.ColumnsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.ColumnsDelegate));
				return;
			case 352:
				ScriptingInterfaceOfIImgui.call_ComboDelegate = (ScriptingInterfaceOfIImgui.ComboDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.ComboDelegate));
				return;
			case 353:
				ScriptingInterfaceOfIImgui.call_EndDelegate = (ScriptingInterfaceOfIImgui.EndDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.EndDelegate));
				return;
			case 354:
				ScriptingInterfaceOfIImgui.call_EndMainThreadScopeDelegate = (ScriptingInterfaceOfIImgui.EndMainThreadScopeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.EndMainThreadScopeDelegate));
				return;
			case 355:
				ScriptingInterfaceOfIImgui.call_InputFloatDelegate = (ScriptingInterfaceOfIImgui.InputFloatDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.InputFloatDelegate));
				return;
			case 356:
				ScriptingInterfaceOfIImgui.call_InputFloat2Delegate = (ScriptingInterfaceOfIImgui.InputFloat2Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.InputFloat2Delegate));
				return;
			case 357:
				ScriptingInterfaceOfIImgui.call_InputFloat3Delegate = (ScriptingInterfaceOfIImgui.InputFloat3Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.InputFloat3Delegate));
				return;
			case 358:
				ScriptingInterfaceOfIImgui.call_InputFloat4Delegate = (ScriptingInterfaceOfIImgui.InputFloat4Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.InputFloat4Delegate));
				return;
			case 359:
				ScriptingInterfaceOfIImgui.call_InputIntDelegate = (ScriptingInterfaceOfIImgui.InputIntDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.InputIntDelegate));
				return;
			case 360:
				ScriptingInterfaceOfIImgui.call_IsItemHoveredDelegate = (ScriptingInterfaceOfIImgui.IsItemHoveredDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.IsItemHoveredDelegate));
				return;
			case 361:
				ScriptingInterfaceOfIImgui.call_NewFrameDelegate = (ScriptingInterfaceOfIImgui.NewFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.NewFrameDelegate));
				return;
			case 362:
				ScriptingInterfaceOfIImgui.call_NewLineDelegate = (ScriptingInterfaceOfIImgui.NewLineDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.NewLineDelegate));
				return;
			case 363:
				ScriptingInterfaceOfIImgui.call_NextColumnDelegate = (ScriptingInterfaceOfIImgui.NextColumnDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.NextColumnDelegate));
				return;
			case 364:
				ScriptingInterfaceOfIImgui.call_PlotLinesDelegate = (ScriptingInterfaceOfIImgui.PlotLinesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.PlotLinesDelegate));
				return;
			case 365:
				ScriptingInterfaceOfIImgui.call_PopStyleColorDelegate = (ScriptingInterfaceOfIImgui.PopStyleColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.PopStyleColorDelegate));
				return;
			case 366:
				ScriptingInterfaceOfIImgui.call_ProgressBarDelegate = (ScriptingInterfaceOfIImgui.ProgressBarDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.ProgressBarDelegate));
				return;
			case 367:
				ScriptingInterfaceOfIImgui.call_PushStyleColorDelegate = (ScriptingInterfaceOfIImgui.PushStyleColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.PushStyleColorDelegate));
				return;
			case 368:
				ScriptingInterfaceOfIImgui.call_RadioButtonDelegate = (ScriptingInterfaceOfIImgui.RadioButtonDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.RadioButtonDelegate));
				return;
			case 369:
				ScriptingInterfaceOfIImgui.call_RenderDelegate = (ScriptingInterfaceOfIImgui.RenderDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.RenderDelegate));
				return;
			case 370:
				ScriptingInterfaceOfIImgui.call_SameLineDelegate = (ScriptingInterfaceOfIImgui.SameLineDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.SameLineDelegate));
				return;
			case 371:
				ScriptingInterfaceOfIImgui.call_SeparatorDelegate = (ScriptingInterfaceOfIImgui.SeparatorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.SeparatorDelegate));
				return;
			case 372:
				ScriptingInterfaceOfIImgui.call_SetTooltipDelegate = (ScriptingInterfaceOfIImgui.SetTooltipDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.SetTooltipDelegate));
				return;
			case 373:
				ScriptingInterfaceOfIImgui.call_SliderFloatDelegate = (ScriptingInterfaceOfIImgui.SliderFloatDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.SliderFloatDelegate));
				return;
			case 374:
				ScriptingInterfaceOfIImgui.call_SmallButtonDelegate = (ScriptingInterfaceOfIImgui.SmallButtonDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.SmallButtonDelegate));
				return;
			case 375:
				ScriptingInterfaceOfIImgui.call_TextDelegate = (ScriptingInterfaceOfIImgui.TextDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.TextDelegate));
				return;
			case 376:
				ScriptingInterfaceOfIImgui.call_TreeNodeDelegate = (ScriptingInterfaceOfIImgui.TreeNodeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.TreeNodeDelegate));
				return;
			case 377:
				ScriptingInterfaceOfIImgui.call_TreePopDelegate = (ScriptingInterfaceOfIImgui.TreePopDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIImgui.TreePopDelegate));
				return;
			case 378:
				ScriptingInterfaceOfIInput.call_ClearKeysDelegate = (ScriptingInterfaceOfIInput.ClearKeysDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.ClearKeysDelegate));
				return;
			case 379:
				ScriptingInterfaceOfIInput.call_GetClipboardTextDelegate = (ScriptingInterfaceOfIInput.GetClipboardTextDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.GetClipboardTextDelegate));
				return;
			case 380:
				ScriptingInterfaceOfIInput.call_GetControllerTypeDelegate = (ScriptingInterfaceOfIInput.GetControllerTypeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.GetControllerTypeDelegate));
				return;
			case 381:
				ScriptingInterfaceOfIInput.call_GetGyroXDelegate = (ScriptingInterfaceOfIInput.GetGyroXDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.GetGyroXDelegate));
				return;
			case 382:
				ScriptingInterfaceOfIInput.call_GetGyroYDelegate = (ScriptingInterfaceOfIInput.GetGyroYDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.GetGyroYDelegate));
				return;
			case 383:
				ScriptingInterfaceOfIInput.call_GetGyroZDelegate = (ScriptingInterfaceOfIInput.GetGyroZDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.GetGyroZDelegate));
				return;
			case 384:
				ScriptingInterfaceOfIInput.call_GetKeyStateDelegate = (ScriptingInterfaceOfIInput.GetKeyStateDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.GetKeyStateDelegate));
				return;
			case 385:
				ScriptingInterfaceOfIInput.call_GetMouseDeltaZDelegate = (ScriptingInterfaceOfIInput.GetMouseDeltaZDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.GetMouseDeltaZDelegate));
				return;
			case 386:
				ScriptingInterfaceOfIInput.call_GetMouseMoveXDelegate = (ScriptingInterfaceOfIInput.GetMouseMoveXDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.GetMouseMoveXDelegate));
				return;
			case 387:
				ScriptingInterfaceOfIInput.call_GetMouseMoveYDelegate = (ScriptingInterfaceOfIInput.GetMouseMoveYDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.GetMouseMoveYDelegate));
				return;
			case 388:
				ScriptingInterfaceOfIInput.call_GetMousePositionXDelegate = (ScriptingInterfaceOfIInput.GetMousePositionXDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.GetMousePositionXDelegate));
				return;
			case 389:
				ScriptingInterfaceOfIInput.call_GetMousePositionYDelegate = (ScriptingInterfaceOfIInput.GetMousePositionYDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.GetMousePositionYDelegate));
				return;
			case 390:
				ScriptingInterfaceOfIInput.call_GetMouseScrollValueDelegate = (ScriptingInterfaceOfIInput.GetMouseScrollValueDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.GetMouseScrollValueDelegate));
				return;
			case 391:
				ScriptingInterfaceOfIInput.call_GetMouseSensitivityDelegate = (ScriptingInterfaceOfIInput.GetMouseSensitivityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.GetMouseSensitivityDelegate));
				return;
			case 392:
				ScriptingInterfaceOfIInput.call_GetVirtualKeyCodeDelegate = (ScriptingInterfaceOfIInput.GetVirtualKeyCodeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.GetVirtualKeyCodeDelegate));
				return;
			case 393:
				ScriptingInterfaceOfIInput.call_IsAnyTouchActiveDelegate = (ScriptingInterfaceOfIInput.IsAnyTouchActiveDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.IsAnyTouchActiveDelegate));
				return;
			case 394:
				ScriptingInterfaceOfIInput.call_IsControllerConnectedDelegate = (ScriptingInterfaceOfIInput.IsControllerConnectedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.IsControllerConnectedDelegate));
				return;
			case 395:
				ScriptingInterfaceOfIInput.call_IsKeyDownDelegate = (ScriptingInterfaceOfIInput.IsKeyDownDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.IsKeyDownDelegate));
				return;
			case 396:
				ScriptingInterfaceOfIInput.call_IsKeyDownImmediateDelegate = (ScriptingInterfaceOfIInput.IsKeyDownImmediateDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.IsKeyDownImmediateDelegate));
				return;
			case 397:
				ScriptingInterfaceOfIInput.call_IsKeyPressedDelegate = (ScriptingInterfaceOfIInput.IsKeyPressedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.IsKeyPressedDelegate));
				return;
			case 398:
				ScriptingInterfaceOfIInput.call_IsKeyReleasedDelegate = (ScriptingInterfaceOfIInput.IsKeyReleasedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.IsKeyReleasedDelegate));
				return;
			case 399:
				ScriptingInterfaceOfIInput.call_IsMouseActiveDelegate = (ScriptingInterfaceOfIInput.IsMouseActiveDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.IsMouseActiveDelegate));
				return;
			case 400:
				ScriptingInterfaceOfIInput.call_PressKeyDelegate = (ScriptingInterfaceOfIInput.PressKeyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.PressKeyDelegate));
				return;
			case 401:
				ScriptingInterfaceOfIInput.call_SetClipboardTextDelegate = (ScriptingInterfaceOfIInput.SetClipboardTextDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.SetClipboardTextDelegate));
				return;
			case 402:
				ScriptingInterfaceOfIInput.call_SetCursorFrictionValueDelegate = (ScriptingInterfaceOfIInput.SetCursorFrictionValueDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.SetCursorFrictionValueDelegate));
				return;
			case 403:
				ScriptingInterfaceOfIInput.call_SetCursorPositionDelegate = (ScriptingInterfaceOfIInput.SetCursorPositionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.SetCursorPositionDelegate));
				return;
			case 404:
				ScriptingInterfaceOfIInput.call_SetLightbarColorDelegate = (ScriptingInterfaceOfIInput.SetLightbarColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.SetLightbarColorDelegate));
				return;
			case 405:
				ScriptingInterfaceOfIInput.call_SetRumbleEffectDelegate = (ScriptingInterfaceOfIInput.SetRumbleEffectDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.SetRumbleEffectDelegate));
				return;
			case 406:
				ScriptingInterfaceOfIInput.call_SetTriggerFeedbackDelegate = (ScriptingInterfaceOfIInput.SetTriggerFeedbackDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.SetTriggerFeedbackDelegate));
				return;
			case 407:
				ScriptingInterfaceOfIInput.call_SetTriggerVibrationDelegate = (ScriptingInterfaceOfIInput.SetTriggerVibrationDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.SetTriggerVibrationDelegate));
				return;
			case 408:
				ScriptingInterfaceOfIInput.call_SetTriggerWeaponEffectDelegate = (ScriptingInterfaceOfIInput.SetTriggerWeaponEffectDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.SetTriggerWeaponEffectDelegate));
				return;
			case 409:
				ScriptingInterfaceOfIInput.call_UpdateKeyDataDelegate = (ScriptingInterfaceOfIInput.UpdateKeyDataDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIInput.UpdateKeyDataDelegate));
				return;
			case 410:
				ScriptingInterfaceOfILight.call_CreatePointLightDelegate = (ScriptingInterfaceOfILight.CreatePointLightDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfILight.CreatePointLightDelegate));
				return;
			case 411:
				ScriptingInterfaceOfILight.call_EnableShadowDelegate = (ScriptingInterfaceOfILight.EnableShadowDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfILight.EnableShadowDelegate));
				return;
			case 412:
				ScriptingInterfaceOfILight.call_GetFrameDelegate = (ScriptingInterfaceOfILight.GetFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfILight.GetFrameDelegate));
				return;
			case 413:
				ScriptingInterfaceOfILight.call_GetIntensityDelegate = (ScriptingInterfaceOfILight.GetIntensityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfILight.GetIntensityDelegate));
				return;
			case 414:
				ScriptingInterfaceOfILight.call_GetLightColorDelegate = (ScriptingInterfaceOfILight.GetLightColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfILight.GetLightColorDelegate));
				return;
			case 415:
				ScriptingInterfaceOfILight.call_GetRadiusDelegate = (ScriptingInterfaceOfILight.GetRadiusDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfILight.GetRadiusDelegate));
				return;
			case 416:
				ScriptingInterfaceOfILight.call_IsShadowEnabledDelegate = (ScriptingInterfaceOfILight.IsShadowEnabledDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfILight.IsShadowEnabledDelegate));
				return;
			case 417:
				ScriptingInterfaceOfILight.call_ReleaseDelegate = (ScriptingInterfaceOfILight.ReleaseDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfILight.ReleaseDelegate));
				return;
			case 418:
				ScriptingInterfaceOfILight.call_SetFrameDelegate = (ScriptingInterfaceOfILight.SetFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfILight.SetFrameDelegate));
				return;
			case 419:
				ScriptingInterfaceOfILight.call_SetIntensityDelegate = (ScriptingInterfaceOfILight.SetIntensityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfILight.SetIntensityDelegate));
				return;
			case 420:
				ScriptingInterfaceOfILight.call_SetLightColorDelegate = (ScriptingInterfaceOfILight.SetLightColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfILight.SetLightColorDelegate));
				return;
			case 421:
				ScriptingInterfaceOfILight.call_SetLightFlickerDelegate = (ScriptingInterfaceOfILight.SetLightFlickerDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfILight.SetLightFlickerDelegate));
				return;
			case 422:
				ScriptingInterfaceOfILight.call_SetRadiusDelegate = (ScriptingInterfaceOfILight.SetRadiusDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfILight.SetRadiusDelegate));
				return;
			case 423:
				ScriptingInterfaceOfILight.call_SetShadowsDelegate = (ScriptingInterfaceOfILight.SetShadowsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfILight.SetShadowsDelegate));
				return;
			case 424:
				ScriptingInterfaceOfILight.call_SetVisibilityDelegate = (ScriptingInterfaceOfILight.SetVisibilityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfILight.SetVisibilityDelegate));
				return;
			case 425:
				ScriptingInterfaceOfILight.call_SetVolumetricPropertiesDelegate = (ScriptingInterfaceOfILight.SetVolumetricPropertiesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfILight.SetVolumetricPropertiesDelegate));
				return;
			case 426:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_AddFaceDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.AddFaceDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.AddFaceDelegate));
				return;
			case 427:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_AddFaceCorner1Delegate = (ScriptingInterfaceOfIManagedMeshEditOperations.AddFaceCorner1Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.AddFaceCorner1Delegate));
				return;
			case 428:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_AddFaceCorner2Delegate = (ScriptingInterfaceOfIManagedMeshEditOperations.AddFaceCorner2Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.AddFaceCorner2Delegate));
				return;
			case 429:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_AddLineDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.AddLineDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.AddLineDelegate));
				return;
			case 430:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_AddMeshDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshDelegate));
				return;
			case 431:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_AddMeshAuxDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshAuxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshAuxDelegate));
				return;
			case 432:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_AddMeshToBoneDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshToBoneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshToBoneDelegate));
				return;
			case 433:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_AddMeshWithColorDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshWithColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshWithColorDelegate));
				return;
			case 434:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_AddMeshWithFixedNormalsDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshWithFixedNormalsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshWithFixedNormalsDelegate));
				return;
			case 435:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_AddMeshWithFixedNormalsWithHeightGradientColorDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshWithFixedNormalsWithHeightGradientColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshWithFixedNormalsWithHeightGradientColorDelegate));
				return;
			case 436:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_AddMeshWithSkinDataDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshWithSkinDataDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshWithSkinDataDelegate));
				return;
			case 437:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_AddRectDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.AddRectDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.AddRectDelegate));
				return;
			case 438:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_AddRectangle3Delegate = (ScriptingInterfaceOfIManagedMeshEditOperations.AddRectangle3Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.AddRectangle3Delegate));
				return;
			case 439:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_AddRectangleWithInverseUVDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.AddRectangleWithInverseUVDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.AddRectangleWithInverseUVDelegate));
				return;
			case 440:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_AddRectWithZUpDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.AddRectWithZUpDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.AddRectWithZUpDelegate));
				return;
			case 441:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_AddSkinnedMeshWithColorDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.AddSkinnedMeshWithColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.AddSkinnedMeshWithColorDelegate));
				return;
			case 442:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_AddTriangle1Delegate = (ScriptingInterfaceOfIManagedMeshEditOperations.AddTriangle1Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.AddTriangle1Delegate));
				return;
			case 443:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_AddTriangle2Delegate = (ScriptingInterfaceOfIManagedMeshEditOperations.AddTriangle2Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.AddTriangle2Delegate));
				return;
			case 444:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_AddVertexDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.AddVertexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.AddVertexDelegate));
				return;
			case 445:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_ApplyCPUSkinningDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.ApplyCPUSkinningDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.ApplyCPUSkinningDelegate));
				return;
			case 446:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_ClearAllDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.ClearAllDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.ClearAllDelegate));
				return;
			case 447:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_ComputeCornerNormalsDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.ComputeCornerNormalsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.ComputeCornerNormalsDelegate));
				return;
			case 448:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_ComputeCornerNormalsWithSmoothingDataDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.ComputeCornerNormalsWithSmoothingDataDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.ComputeCornerNormalsWithSmoothingDataDelegate));
				return;
			case 449:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_ComputeTangentsDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.ComputeTangentsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.ComputeTangentsDelegate));
				return;
			case 450:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_CreateDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.CreateDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.CreateDelegate));
				return;
			case 451:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_EnsureTransformedVerticesDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.EnsureTransformedVerticesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.EnsureTransformedVerticesDelegate));
				return;
			case 452:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_FinalizeEditingDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.FinalizeEditingDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.FinalizeEditingDelegate));
				return;
			case 453:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_GenerateGridDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.GenerateGridDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.GenerateGridDelegate));
				return;
			case 454:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_GetPositionOfVertexDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.GetPositionOfVertexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.GetPositionOfVertexDelegate));
				return;
			case 455:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_GetVertexColorDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.GetVertexColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.GetVertexColorDelegate));
				return;
			case 456:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_GetVertexColorAlphaDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.GetVertexColorAlphaDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.GetVertexColorAlphaDelegate));
				return;
			case 457:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_InvertFacesWindingOrderDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.InvertFacesWindingOrderDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.InvertFacesWindingOrderDelegate));
				return;
			case 458:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_MoveVerticesAlongNormalDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.MoveVerticesAlongNormalDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.MoveVerticesAlongNormalDelegate));
				return;
			case 459:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_RemoveDuplicatedCornersDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.RemoveDuplicatedCornersDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.RemoveDuplicatedCornersDelegate));
				return;
			case 460:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_RemoveFaceDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.RemoveFaceDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.RemoveFaceDelegate));
				return;
			case 461:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_RescaleMesh2dDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.RescaleMesh2dDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.RescaleMesh2dDelegate));
				return;
			case 462:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_RescaleMesh2dRepeatXDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.RescaleMesh2dRepeatXDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.RescaleMesh2dRepeatXDelegate));
				return;
			case 463:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_RescaleMesh2dRepeatXWithTilingDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.RescaleMesh2dRepeatXWithTilingDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.RescaleMesh2dRepeatXWithTilingDelegate));
				return;
			case 464:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_RescaleMesh2dRepeatYDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.RescaleMesh2dRepeatYDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.RescaleMesh2dRepeatYDelegate));
				return;
			case 465:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_RescaleMesh2dRepeatYWithTilingDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.RescaleMesh2dRepeatYWithTilingDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.RescaleMesh2dRepeatYWithTilingDelegate));
				return;
			case 466:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_RescaleMesh2dWithoutChangingUVDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.RescaleMesh2dWithoutChangingUVDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.RescaleMesh2dWithoutChangingUVDelegate));
				return;
			case 467:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_ReserveFaceCornersDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.ReserveFaceCornersDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.ReserveFaceCornersDelegate));
				return;
			case 468:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_ReserveFacesDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.ReserveFacesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.ReserveFacesDelegate));
				return;
			case 469:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_ReserveVerticesDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.ReserveVerticesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.ReserveVerticesDelegate));
				return;
			case 470:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_ScaleVertices1Delegate = (ScriptingInterfaceOfIManagedMeshEditOperations.ScaleVertices1Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.ScaleVertices1Delegate));
				return;
			case 471:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_ScaleVertices2Delegate = (ScriptingInterfaceOfIManagedMeshEditOperations.ScaleVertices2Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.ScaleVertices2Delegate));
				return;
			case 472:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_SetCornerUVDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.SetCornerUVDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.SetCornerUVDelegate));
				return;
			case 473:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_SetCornerVertexColorDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.SetCornerVertexColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.SetCornerVertexColorDelegate));
				return;
			case 474:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_SetPositionOfVertexDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.SetPositionOfVertexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.SetPositionOfVertexDelegate));
				return;
			case 475:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_SetTangentsOfFaceCornerDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.SetTangentsOfFaceCornerDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.SetTangentsOfFaceCornerDelegate));
				return;
			case 476:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_SetVertexColorDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.SetVertexColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.SetVertexColorDelegate));
				return;
			case 477:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_SetVertexColorAlphaDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.SetVertexColorAlphaDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.SetVertexColorAlphaDelegate));
				return;
			case 478:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_TransformVerticesToLocalDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.TransformVerticesToLocalDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.TransformVerticesToLocalDelegate));
				return;
			case 479:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_TransformVerticesToParentDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.TransformVerticesToParentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.TransformVerticesToParentDelegate));
				return;
			case 480:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_TranslateVerticesDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.TranslateVerticesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.TranslateVerticesDelegate));
				return;
			case 481:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_UpdateOverlappedVertexNormalsDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.UpdateOverlappedVertexNormalsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.UpdateOverlappedVertexNormalsDelegate));
				return;
			case 482:
				ScriptingInterfaceOfIManagedMeshEditOperations.call_WeldDelegate = (ScriptingInterfaceOfIManagedMeshEditOperations.WeldDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIManagedMeshEditOperations.WeldDelegate));
				return;
			case 483:
				ScriptingInterfaceOfIMaterial.call_AddMaterialShaderFlagDelegate = (ScriptingInterfaceOfIMaterial.AddMaterialShaderFlagDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.AddMaterialShaderFlagDelegate));
				return;
			case 484:
				ScriptingInterfaceOfIMaterial.call_CreateCopyDelegate = (ScriptingInterfaceOfIMaterial.CreateCopyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.CreateCopyDelegate));
				return;
			case 485:
				ScriptingInterfaceOfIMaterial.call_GetAlphaBlendModeDelegate = (ScriptingInterfaceOfIMaterial.GetAlphaBlendModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.GetAlphaBlendModeDelegate));
				return;
			case 486:
				ScriptingInterfaceOfIMaterial.call_GetAlphaTestValueDelegate = (ScriptingInterfaceOfIMaterial.GetAlphaTestValueDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.GetAlphaTestValueDelegate));
				return;
			case 487:
				ScriptingInterfaceOfIMaterial.call_GetDefaultMaterialDelegate = (ScriptingInterfaceOfIMaterial.GetDefaultMaterialDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.GetDefaultMaterialDelegate));
				return;
			case 488:
				ScriptingInterfaceOfIMaterial.call_GetFlagsDelegate = (ScriptingInterfaceOfIMaterial.GetFlagsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.GetFlagsDelegate));
				return;
			case 489:
				ScriptingInterfaceOfIMaterial.call_GetFromResourceDelegate = (ScriptingInterfaceOfIMaterial.GetFromResourceDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.GetFromResourceDelegate));
				return;
			case 490:
				ScriptingInterfaceOfIMaterial.call_GetNameDelegate = (ScriptingInterfaceOfIMaterial.GetNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.GetNameDelegate));
				return;
			case 491:
				ScriptingInterfaceOfIMaterial.call_GetOutlineMaterialDelegate = (ScriptingInterfaceOfIMaterial.GetOutlineMaterialDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.GetOutlineMaterialDelegate));
				return;
			case 492:
				ScriptingInterfaceOfIMaterial.call_GetShaderDelegate = (ScriptingInterfaceOfIMaterial.GetShaderDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.GetShaderDelegate));
				return;
			case 493:
				ScriptingInterfaceOfIMaterial.call_GetShaderFlagsDelegate = (ScriptingInterfaceOfIMaterial.GetShaderFlagsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.GetShaderFlagsDelegate));
				return;
			case 494:
				ScriptingInterfaceOfIMaterial.call_GetTextureDelegate = (ScriptingInterfaceOfIMaterial.GetTextureDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.GetTextureDelegate));
				return;
			case 495:
				ScriptingInterfaceOfIMaterial.call_ReleaseDelegate = (ScriptingInterfaceOfIMaterial.ReleaseDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.ReleaseDelegate));
				return;
			case 496:
				ScriptingInterfaceOfIMaterial.call_SetAlphaBlendModeDelegate = (ScriptingInterfaceOfIMaterial.SetAlphaBlendModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.SetAlphaBlendModeDelegate));
				return;
			case 497:
				ScriptingInterfaceOfIMaterial.call_SetAlphaTestValueDelegate = (ScriptingInterfaceOfIMaterial.SetAlphaTestValueDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.SetAlphaTestValueDelegate));
				return;
			case 498:
				ScriptingInterfaceOfIMaterial.call_SetAreaMapScaleDelegate = (ScriptingInterfaceOfIMaterial.SetAreaMapScaleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.SetAreaMapScaleDelegate));
				return;
			case 499:
				ScriptingInterfaceOfIMaterial.call_SetEnableSkinningDelegate = (ScriptingInterfaceOfIMaterial.SetEnableSkinningDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.SetEnableSkinningDelegate));
				return;
			case 500:
				ScriptingInterfaceOfIMaterial.call_SetFlagsDelegate = (ScriptingInterfaceOfIMaterial.SetFlagsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.SetFlagsDelegate));
				return;
			case 501:
				ScriptingInterfaceOfIMaterial.call_SetMeshVectorArgumentDelegate = (ScriptingInterfaceOfIMaterial.SetMeshVectorArgumentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.SetMeshVectorArgumentDelegate));
				return;
			case 502:
				ScriptingInterfaceOfIMaterial.call_SetNameDelegate = (ScriptingInterfaceOfIMaterial.SetNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.SetNameDelegate));
				return;
			case 503:
				ScriptingInterfaceOfIMaterial.call_SetShaderDelegate = (ScriptingInterfaceOfIMaterial.SetShaderDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.SetShaderDelegate));
				return;
			case 504:
				ScriptingInterfaceOfIMaterial.call_SetShaderFlagsDelegate = (ScriptingInterfaceOfIMaterial.SetShaderFlagsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.SetShaderFlagsDelegate));
				return;
			case 505:
				ScriptingInterfaceOfIMaterial.call_SetTextureDelegate = (ScriptingInterfaceOfIMaterial.SetTextureDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.SetTextureDelegate));
				return;
			case 506:
				ScriptingInterfaceOfIMaterial.call_SetTextureAtSlotDelegate = (ScriptingInterfaceOfIMaterial.SetTextureAtSlotDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.SetTextureAtSlotDelegate));
				return;
			case 507:
				ScriptingInterfaceOfIMaterial.call_UsingSkinningDelegate = (ScriptingInterfaceOfIMaterial.UsingSkinningDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMaterial.UsingSkinningDelegate));
				return;
			case 508:
				ScriptingInterfaceOfIMesh.call_AddEditDataUserDelegate = (ScriptingInterfaceOfIMesh.AddEditDataUserDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.AddEditDataUserDelegate));
				return;
			case 509:
				ScriptingInterfaceOfIMesh.call_AddFaceDelegate = (ScriptingInterfaceOfIMesh.AddFaceDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.AddFaceDelegate));
				return;
			case 510:
				ScriptingInterfaceOfIMesh.call_AddFaceCornerDelegate = (ScriptingInterfaceOfIMesh.AddFaceCornerDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.AddFaceCornerDelegate));
				return;
			case 511:
				ScriptingInterfaceOfIMesh.call_AddMeshToMeshDelegate = (ScriptingInterfaceOfIMesh.AddMeshToMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.AddMeshToMeshDelegate));
				return;
			case 512:
				ScriptingInterfaceOfIMesh.call_AddTriangleDelegate = (ScriptingInterfaceOfIMesh.AddTriangleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.AddTriangleDelegate));
				return;
			case 513:
				ScriptingInterfaceOfIMesh.call_AddTriangleWithVertexColorsDelegate = (ScriptingInterfaceOfIMesh.AddTriangleWithVertexColorsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.AddTriangleWithVertexColorsDelegate));
				return;
			case 514:
				ScriptingInterfaceOfIMesh.call_ClearMeshDelegate = (ScriptingInterfaceOfIMesh.ClearMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.ClearMeshDelegate));
				return;
			case 515:
				ScriptingInterfaceOfIMesh.call_ComputeNormalsDelegate = (ScriptingInterfaceOfIMesh.ComputeNormalsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.ComputeNormalsDelegate));
				return;
			case 516:
				ScriptingInterfaceOfIMesh.call_ComputeTangentsDelegate = (ScriptingInterfaceOfIMesh.ComputeTangentsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.ComputeTangentsDelegate));
				return;
			case 517:
				ScriptingInterfaceOfIMesh.call_CreateMeshDelegate = (ScriptingInterfaceOfIMesh.CreateMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.CreateMeshDelegate));
				return;
			case 518:
				ScriptingInterfaceOfIMesh.call_CreateMeshCopyDelegate = (ScriptingInterfaceOfIMesh.CreateMeshCopyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.CreateMeshCopyDelegate));
				return;
			case 519:
				ScriptingInterfaceOfIMesh.call_CreateMeshWithMaterialDelegate = (ScriptingInterfaceOfIMesh.CreateMeshWithMaterialDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.CreateMeshWithMaterialDelegate));
				return;
			case 520:
				ScriptingInterfaceOfIMesh.call_DisableContourDelegate = (ScriptingInterfaceOfIMesh.DisableContourDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.DisableContourDelegate));
				return;
			case 521:
				ScriptingInterfaceOfIMesh.call_GetBaseMeshDelegate = (ScriptingInterfaceOfIMesh.GetBaseMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.GetBaseMeshDelegate));
				return;
			case 522:
				ScriptingInterfaceOfIMesh.call_GetBillboardDelegate = (ScriptingInterfaceOfIMesh.GetBillboardDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.GetBillboardDelegate));
				return;
			case 523:
				ScriptingInterfaceOfIMesh.call_GetBoundingBoxHeightDelegate = (ScriptingInterfaceOfIMesh.GetBoundingBoxHeightDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.GetBoundingBoxHeightDelegate));
				return;
			case 524:
				ScriptingInterfaceOfIMesh.call_GetBoundingBoxMaxDelegate = (ScriptingInterfaceOfIMesh.GetBoundingBoxMaxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.GetBoundingBoxMaxDelegate));
				return;
			case 525:
				ScriptingInterfaceOfIMesh.call_GetBoundingBoxMinDelegate = (ScriptingInterfaceOfIMesh.GetBoundingBoxMinDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.GetBoundingBoxMinDelegate));
				return;
			case 526:
				ScriptingInterfaceOfIMesh.call_GetBoundingBoxWidthDelegate = (ScriptingInterfaceOfIMesh.GetBoundingBoxWidthDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.GetBoundingBoxWidthDelegate));
				return;
			case 527:
				ScriptingInterfaceOfIMesh.call_GetColorDelegate = (ScriptingInterfaceOfIMesh.GetColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.GetColorDelegate));
				return;
			case 528:
				ScriptingInterfaceOfIMesh.call_GetColor2Delegate = (ScriptingInterfaceOfIMesh.GetColor2Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.GetColor2Delegate));
				return;
			case 529:
				ScriptingInterfaceOfIMesh.call_GetEditDataFaceCornerCountDelegate = (ScriptingInterfaceOfIMesh.GetEditDataFaceCornerCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.GetEditDataFaceCornerCountDelegate));
				return;
			case 530:
				ScriptingInterfaceOfIMesh.call_GetEditDataFaceCornerVertexColorDelegate = (ScriptingInterfaceOfIMesh.GetEditDataFaceCornerVertexColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.GetEditDataFaceCornerVertexColorDelegate));
				return;
			case 531:
				ScriptingInterfaceOfIMesh.call_GetFaceCornerCountDelegate = (ScriptingInterfaceOfIMesh.GetFaceCornerCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.GetFaceCornerCountDelegate));
				return;
			case 532:
				ScriptingInterfaceOfIMesh.call_GetFaceCountDelegate = (ScriptingInterfaceOfIMesh.GetFaceCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.GetFaceCountDelegate));
				return;
			case 533:
				ScriptingInterfaceOfIMesh.call_GetLocalFrameDelegate = (ScriptingInterfaceOfIMesh.GetLocalFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.GetLocalFrameDelegate));
				return;
			case 534:
				ScriptingInterfaceOfIMesh.call_GetMaterialDelegate = (ScriptingInterfaceOfIMesh.GetMaterialDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.GetMaterialDelegate));
				return;
			case 535:
				ScriptingInterfaceOfIMesh.call_GetMeshFromResourceDelegate = (ScriptingInterfaceOfIMesh.GetMeshFromResourceDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.GetMeshFromResourceDelegate));
				return;
			case 536:
				ScriptingInterfaceOfIMesh.call_GetNameDelegate = (ScriptingInterfaceOfIMesh.GetNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.GetNameDelegate));
				return;
			case 537:
				ScriptingInterfaceOfIMesh.call_GetRandomMeshWithVdeclDelegate = (ScriptingInterfaceOfIMesh.GetRandomMeshWithVdeclDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.GetRandomMeshWithVdeclDelegate));
				return;
			case 538:
				ScriptingInterfaceOfIMesh.call_GetSecondMaterialDelegate = (ScriptingInterfaceOfIMesh.GetSecondMaterialDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.GetSecondMaterialDelegate));
				return;
			case 539:
				ScriptingInterfaceOfIMesh.call_GetVisibilityMaskDelegate = (ScriptingInterfaceOfIMesh.GetVisibilityMaskDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.GetVisibilityMaskDelegate));
				return;
			case 540:
				ScriptingInterfaceOfIMesh.call_HasTagDelegate = (ScriptingInterfaceOfIMesh.HasTagDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.HasTagDelegate));
				return;
			case 541:
				ScriptingInterfaceOfIMesh.call_HintIndicesDynamicDelegate = (ScriptingInterfaceOfIMesh.HintIndicesDynamicDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.HintIndicesDynamicDelegate));
				return;
			case 542:
				ScriptingInterfaceOfIMesh.call_HintVerticesDynamicDelegate = (ScriptingInterfaceOfIMesh.HintVerticesDynamicDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.HintVerticesDynamicDelegate));
				return;
			case 543:
				ScriptingInterfaceOfIMesh.call_LockEditDataWriteDelegate = (ScriptingInterfaceOfIMesh.LockEditDataWriteDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.LockEditDataWriteDelegate));
				return;
			case 544:
				ScriptingInterfaceOfIMesh.call_PreloadForRenderingDelegate = (ScriptingInterfaceOfIMesh.PreloadForRenderingDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.PreloadForRenderingDelegate));
				return;
			case 545:
				ScriptingInterfaceOfIMesh.call_RecomputeBoundingBoxDelegate = (ScriptingInterfaceOfIMesh.RecomputeBoundingBoxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.RecomputeBoundingBoxDelegate));
				return;
			case 546:
				ScriptingInterfaceOfIMesh.call_ReleaseEditDataUserDelegate = (ScriptingInterfaceOfIMesh.ReleaseEditDataUserDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.ReleaseEditDataUserDelegate));
				return;
			case 547:
				ScriptingInterfaceOfIMesh.call_ReleaseResourcesDelegate = (ScriptingInterfaceOfIMesh.ReleaseResourcesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.ReleaseResourcesDelegate));
				return;
			case 548:
				ScriptingInterfaceOfIMesh.call_SetAsNotEffectedBySeasonDelegate = (ScriptingInterfaceOfIMesh.SetAsNotEffectedBySeasonDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetAsNotEffectedBySeasonDelegate));
				return;
			case 549:
				ScriptingInterfaceOfIMesh.call_SetBillboardDelegate = (ScriptingInterfaceOfIMesh.SetBillboardDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetBillboardDelegate));
				return;
			case 550:
				ScriptingInterfaceOfIMesh.call_SetColorDelegate = (ScriptingInterfaceOfIMesh.SetColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetColorDelegate));
				return;
			case 551:
				ScriptingInterfaceOfIMesh.call_SetColor2Delegate = (ScriptingInterfaceOfIMesh.SetColor2Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetColor2Delegate));
				return;
			case 552:
				ScriptingInterfaceOfIMesh.call_SetColorAlphaDelegate = (ScriptingInterfaceOfIMesh.SetColorAlphaDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetColorAlphaDelegate));
				return;
			case 553:
				ScriptingInterfaceOfIMesh.call_SetColorAndStrokeDelegate = (ScriptingInterfaceOfIMesh.SetColorAndStrokeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetColorAndStrokeDelegate));
				return;
			case 554:
				ScriptingInterfaceOfIMesh.call_SetContourColorDelegate = (ScriptingInterfaceOfIMesh.SetContourColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetContourColorDelegate));
				return;
			case 555:
				ScriptingInterfaceOfIMesh.call_SetCullingModeDelegate = (ScriptingInterfaceOfIMesh.SetCullingModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetCullingModeDelegate));
				return;
			case 556:
				ScriptingInterfaceOfIMesh.call_SetEditDataFaceCornerVertexColorDelegate = (ScriptingInterfaceOfIMesh.SetEditDataFaceCornerVertexColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetEditDataFaceCornerVertexColorDelegate));
				return;
			case 557:
				ScriptingInterfaceOfIMesh.call_SetEditDataPolicyDelegate = (ScriptingInterfaceOfIMesh.SetEditDataPolicyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetEditDataPolicyDelegate));
				return;
			case 558:
				ScriptingInterfaceOfIMesh.call_SetExternalBoundingBoxDelegate = (ScriptingInterfaceOfIMesh.SetExternalBoundingBoxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetExternalBoundingBoxDelegate));
				return;
			case 559:
				ScriptingInterfaceOfIMesh.call_SetLocalFrameDelegate = (ScriptingInterfaceOfIMesh.SetLocalFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetLocalFrameDelegate));
				return;
			case 560:
				ScriptingInterfaceOfIMesh.call_SetMaterialDelegate = (ScriptingInterfaceOfIMesh.SetMaterialDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetMaterialDelegate));
				return;
			case 561:
				ScriptingInterfaceOfIMesh.call_SetMaterialByNameDelegate = (ScriptingInterfaceOfIMesh.SetMaterialByNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetMaterialByNameDelegate));
				return;
			case 562:
				ScriptingInterfaceOfIMesh.call_SetMeshRenderOrderDelegate = (ScriptingInterfaceOfIMesh.SetMeshRenderOrderDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetMeshRenderOrderDelegate));
				return;
			case 563:
				ScriptingInterfaceOfIMesh.call_SetMorphTimeDelegate = (ScriptingInterfaceOfIMesh.SetMorphTimeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetMorphTimeDelegate));
				return;
			case 564:
				ScriptingInterfaceOfIMesh.call_SetNameDelegate = (ScriptingInterfaceOfIMesh.SetNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetNameDelegate));
				return;
			case 565:
				ScriptingInterfaceOfIMesh.call_SetVectorArgumentDelegate = (ScriptingInterfaceOfIMesh.SetVectorArgumentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetVectorArgumentDelegate));
				return;
			case 566:
				ScriptingInterfaceOfIMesh.call_SetVectorArgument2Delegate = (ScriptingInterfaceOfIMesh.SetVectorArgument2Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetVectorArgument2Delegate));
				return;
			case 567:
				ScriptingInterfaceOfIMesh.call_SetVisibilityMaskDelegate = (ScriptingInterfaceOfIMesh.SetVisibilityMaskDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.SetVisibilityMaskDelegate));
				return;
			case 568:
				ScriptingInterfaceOfIMesh.call_UnlockEditDataWriteDelegate = (ScriptingInterfaceOfIMesh.UnlockEditDataWriteDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.UnlockEditDataWriteDelegate));
				return;
			case 569:
				ScriptingInterfaceOfIMesh.call_UpdateBoundingBoxDelegate = (ScriptingInterfaceOfIMesh.UpdateBoundingBoxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMesh.UpdateBoundingBoxDelegate));
				return;
			case 570:
				ScriptingInterfaceOfIMeshBuilder.call_CreateTilingButtonMeshDelegate = (ScriptingInterfaceOfIMeshBuilder.CreateTilingButtonMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMeshBuilder.CreateTilingButtonMeshDelegate));
				return;
			case 571:
				ScriptingInterfaceOfIMeshBuilder.call_CreateTilingWindowMeshDelegate = (ScriptingInterfaceOfIMeshBuilder.CreateTilingWindowMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMeshBuilder.CreateTilingWindowMeshDelegate));
				return;
			case 572:
				ScriptingInterfaceOfIMeshBuilder.call_FinalizeMeshBuilderDelegate = (ScriptingInterfaceOfIMeshBuilder.FinalizeMeshBuilderDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMeshBuilder.FinalizeMeshBuilderDelegate));
				return;
			case 573:
				ScriptingInterfaceOfIMetaMesh.call_AddEditDataUserDelegate = (ScriptingInterfaceOfIMetaMesh.AddEditDataUserDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.AddEditDataUserDelegate));
				return;
			case 574:
				ScriptingInterfaceOfIMetaMesh.call_AddMeshDelegate = (ScriptingInterfaceOfIMetaMesh.AddMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.AddMeshDelegate));
				return;
			case 575:
				ScriptingInterfaceOfIMetaMesh.call_AddMetaMeshDelegate = (ScriptingInterfaceOfIMetaMesh.AddMetaMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.AddMetaMeshDelegate));
				return;
			case 576:
				ScriptingInterfaceOfIMetaMesh.call_AssignClothBodyFromDelegate = (ScriptingInterfaceOfIMetaMesh.AssignClothBodyFromDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.AssignClothBodyFromDelegate));
				return;
			case 577:
				ScriptingInterfaceOfIMetaMesh.call_BatchMultiMeshesDelegate = (ScriptingInterfaceOfIMetaMesh.BatchMultiMeshesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.BatchMultiMeshesDelegate));
				return;
			case 578:
				ScriptingInterfaceOfIMetaMesh.call_BatchMultiMeshesMultipleDelegate = (ScriptingInterfaceOfIMetaMesh.BatchMultiMeshesMultipleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.BatchMultiMeshesMultipleDelegate));
				return;
			case 579:
				ScriptingInterfaceOfIMetaMesh.call_CheckMetaMeshExistenceDelegate = (ScriptingInterfaceOfIMetaMesh.CheckMetaMeshExistenceDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.CheckMetaMeshExistenceDelegate));
				return;
			case 580:
				ScriptingInterfaceOfIMetaMesh.call_CheckResourcesDelegate = (ScriptingInterfaceOfIMetaMesh.CheckResourcesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.CheckResourcesDelegate));
				return;
			case 581:
				ScriptingInterfaceOfIMetaMesh.call_ClearEditDataDelegate = (ScriptingInterfaceOfIMetaMesh.ClearEditDataDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.ClearEditDataDelegate));
				return;
			case 582:
				ScriptingInterfaceOfIMetaMesh.call_ClearMeshesDelegate = (ScriptingInterfaceOfIMetaMesh.ClearMeshesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.ClearMeshesDelegate));
				return;
			case 583:
				ScriptingInterfaceOfIMetaMesh.call_ClearMeshesForLodDelegate = (ScriptingInterfaceOfIMetaMesh.ClearMeshesForLodDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.ClearMeshesForLodDelegate));
				return;
			case 584:
				ScriptingInterfaceOfIMetaMesh.call_ClearMeshesForLowerLodsDelegate = (ScriptingInterfaceOfIMetaMesh.ClearMeshesForLowerLodsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.ClearMeshesForLowerLodsDelegate));
				return;
			case 585:
				ScriptingInterfaceOfIMetaMesh.call_ClearMeshesForOtherLodsDelegate = (ScriptingInterfaceOfIMetaMesh.ClearMeshesForOtherLodsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.ClearMeshesForOtherLodsDelegate));
				return;
			case 586:
				ScriptingInterfaceOfIMetaMesh.call_CopyToDelegate = (ScriptingInterfaceOfIMetaMesh.CopyToDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.CopyToDelegate));
				return;
			case 587:
				ScriptingInterfaceOfIMetaMesh.call_CreateCopyDelegate = (ScriptingInterfaceOfIMetaMesh.CreateCopyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.CreateCopyDelegate));
				return;
			case 588:
				ScriptingInterfaceOfIMetaMesh.call_CreateCopyFromNameDelegate = (ScriptingInterfaceOfIMetaMesh.CreateCopyFromNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.CreateCopyFromNameDelegate));
				return;
			case 589:
				ScriptingInterfaceOfIMetaMesh.call_CreateMetaMeshDelegate = (ScriptingInterfaceOfIMetaMesh.CreateMetaMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.CreateMetaMeshDelegate));
				return;
			case 590:
				ScriptingInterfaceOfIMetaMesh.call_DrawTextWithDefaultFontDelegate = (ScriptingInterfaceOfIMetaMesh.DrawTextWithDefaultFontDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.DrawTextWithDefaultFontDelegate));
				return;
			case 591:
				ScriptingInterfaceOfIMetaMesh.call_GetAllMultiMeshesDelegate = (ScriptingInterfaceOfIMetaMesh.GetAllMultiMeshesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.GetAllMultiMeshesDelegate));
				return;
			case 592:
				ScriptingInterfaceOfIMetaMesh.call_GetBoundingBoxDelegate = (ScriptingInterfaceOfIMetaMesh.GetBoundingBoxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.GetBoundingBoxDelegate));
				return;
			case 593:
				ScriptingInterfaceOfIMetaMesh.call_GetFactor1Delegate = (ScriptingInterfaceOfIMetaMesh.GetFactor1Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.GetFactor1Delegate));
				return;
			case 594:
				ScriptingInterfaceOfIMetaMesh.call_GetFactor2Delegate = (ScriptingInterfaceOfIMetaMesh.GetFactor2Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.GetFactor2Delegate));
				return;
			case 595:
				ScriptingInterfaceOfIMetaMesh.call_GetFrameDelegate = (ScriptingInterfaceOfIMetaMesh.GetFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.GetFrameDelegate));
				return;
			case 596:
				ScriptingInterfaceOfIMetaMesh.call_GetLodMaskForMeshAtIndexDelegate = (ScriptingInterfaceOfIMetaMesh.GetLodMaskForMeshAtIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.GetLodMaskForMeshAtIndexDelegate));
				return;
			case 597:
				ScriptingInterfaceOfIMetaMesh.call_GetMeshAtIndexDelegate = (ScriptingInterfaceOfIMetaMesh.GetMeshAtIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.GetMeshAtIndexDelegate));
				return;
			case 598:
				ScriptingInterfaceOfIMetaMesh.call_GetMeshCountDelegate = (ScriptingInterfaceOfIMetaMesh.GetMeshCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.GetMeshCountDelegate));
				return;
			case 599:
				ScriptingInterfaceOfIMetaMesh.call_GetMeshCountWithTagDelegate = (ScriptingInterfaceOfIMetaMesh.GetMeshCountWithTagDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.GetMeshCountWithTagDelegate));
				return;
			case 600:
				ScriptingInterfaceOfIMetaMesh.call_GetMorphedCopyDelegate = (ScriptingInterfaceOfIMetaMesh.GetMorphedCopyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.GetMorphedCopyDelegate));
				return;
			case 601:
				ScriptingInterfaceOfIMetaMesh.call_GetMultiMeshDelegate = (ScriptingInterfaceOfIMetaMesh.GetMultiMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.GetMultiMeshDelegate));
				return;
			case 602:
				ScriptingInterfaceOfIMetaMesh.call_GetMultiMeshCountDelegate = (ScriptingInterfaceOfIMetaMesh.GetMultiMeshCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.GetMultiMeshCountDelegate));
				return;
			case 603:
				ScriptingInterfaceOfIMetaMesh.call_GetNameDelegate = (ScriptingInterfaceOfIMetaMesh.GetNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.GetNameDelegate));
				return;
			case 604:
				ScriptingInterfaceOfIMetaMesh.call_GetTotalGpuSizeDelegate = (ScriptingInterfaceOfIMetaMesh.GetTotalGpuSizeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.GetTotalGpuSizeDelegate));
				return;
			case 605:
				ScriptingInterfaceOfIMetaMesh.call_GetVectorArgument2Delegate = (ScriptingInterfaceOfIMetaMesh.GetVectorArgument2Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.GetVectorArgument2Delegate));
				return;
			case 606:
				ScriptingInterfaceOfIMetaMesh.call_GetVectorUserDataDelegate = (ScriptingInterfaceOfIMetaMesh.GetVectorUserDataDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.GetVectorUserDataDelegate));
				return;
			case 607:
				ScriptingInterfaceOfIMetaMesh.call_GetVisibilityMaskDelegate = (ScriptingInterfaceOfIMetaMesh.GetVisibilityMaskDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.GetVisibilityMaskDelegate));
				return;
			case 608:
				ScriptingInterfaceOfIMetaMesh.call_HasAnyGeneratedLodsDelegate = (ScriptingInterfaceOfIMetaMesh.HasAnyGeneratedLodsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.HasAnyGeneratedLodsDelegate));
				return;
			case 609:
				ScriptingInterfaceOfIMetaMesh.call_HasAnyLodsDelegate = (ScriptingInterfaceOfIMetaMesh.HasAnyLodsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.HasAnyLodsDelegate));
				return;
			case 610:
				ScriptingInterfaceOfIMetaMesh.call_HasClothDataDelegate = (ScriptingInterfaceOfIMetaMesh.HasClothDataDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.HasClothDataDelegate));
				return;
			case 611:
				ScriptingInterfaceOfIMetaMesh.call_HasVertexBufferOrEditDataOrPackageItemDelegate = (ScriptingInterfaceOfIMetaMesh.HasVertexBufferOrEditDataOrPackageItemDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.HasVertexBufferOrEditDataOrPackageItemDelegate));
				return;
			case 612:
				ScriptingInterfaceOfIMetaMesh.call_MergeMultiMeshesDelegate = (ScriptingInterfaceOfIMetaMesh.MergeMultiMeshesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.MergeMultiMeshesDelegate));
				return;
			case 613:
				ScriptingInterfaceOfIMetaMesh.call_PreloadForRenderingDelegate = (ScriptingInterfaceOfIMetaMesh.PreloadForRenderingDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.PreloadForRenderingDelegate));
				return;
			case 614:
				ScriptingInterfaceOfIMetaMesh.call_PreloadShadersDelegate = (ScriptingInterfaceOfIMetaMesh.PreloadShadersDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.PreloadShadersDelegate));
				return;
			case 615:
				ScriptingInterfaceOfIMetaMesh.call_RecomputeBoundingBoxDelegate = (ScriptingInterfaceOfIMetaMesh.RecomputeBoundingBoxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.RecomputeBoundingBoxDelegate));
				return;
			case 616:
				ScriptingInterfaceOfIMetaMesh.call_ReleaseDelegate = (ScriptingInterfaceOfIMetaMesh.ReleaseDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.ReleaseDelegate));
				return;
			case 617:
				ScriptingInterfaceOfIMetaMesh.call_ReleaseEditDataUserDelegate = (ScriptingInterfaceOfIMetaMesh.ReleaseEditDataUserDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.ReleaseEditDataUserDelegate));
				return;
			case 618:
				ScriptingInterfaceOfIMetaMesh.call_RemoveMeshesWithoutTagDelegate = (ScriptingInterfaceOfIMetaMesh.RemoveMeshesWithoutTagDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.RemoveMeshesWithoutTagDelegate));
				return;
			case 619:
				ScriptingInterfaceOfIMetaMesh.call_RemoveMeshesWithTagDelegate = (ScriptingInterfaceOfIMetaMesh.RemoveMeshesWithTagDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.RemoveMeshesWithTagDelegate));
				return;
			case 620:
				ScriptingInterfaceOfIMetaMesh.call_SetBillboardingDelegate = (ScriptingInterfaceOfIMetaMesh.SetBillboardingDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetBillboardingDelegate));
				return;
			case 621:
				ScriptingInterfaceOfIMetaMesh.call_SetContourColorDelegate = (ScriptingInterfaceOfIMetaMesh.SetContourColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetContourColorDelegate));
				return;
			case 622:
				ScriptingInterfaceOfIMetaMesh.call_SetContourStateDelegate = (ScriptingInterfaceOfIMetaMesh.SetContourStateDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetContourStateDelegate));
				return;
			case 623:
				ScriptingInterfaceOfIMetaMesh.call_SetCullModeDelegate = (ScriptingInterfaceOfIMetaMesh.SetCullModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetCullModeDelegate));
				return;
			case 624:
				ScriptingInterfaceOfIMetaMesh.call_SetEditDataPolicyDelegate = (ScriptingInterfaceOfIMetaMesh.SetEditDataPolicyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetEditDataPolicyDelegate));
				return;
			case 625:
				ScriptingInterfaceOfIMetaMesh.call_SetFactor1Delegate = (ScriptingInterfaceOfIMetaMesh.SetFactor1Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetFactor1Delegate));
				return;
			case 626:
				ScriptingInterfaceOfIMetaMesh.call_SetFactor1LinearDelegate = (ScriptingInterfaceOfIMetaMesh.SetFactor1LinearDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetFactor1LinearDelegate));
				return;
			case 627:
				ScriptingInterfaceOfIMetaMesh.call_SetFactor2Delegate = (ScriptingInterfaceOfIMetaMesh.SetFactor2Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetFactor2Delegate));
				return;
			case 628:
				ScriptingInterfaceOfIMetaMesh.call_SetFactor2LinearDelegate = (ScriptingInterfaceOfIMetaMesh.SetFactor2LinearDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetFactor2LinearDelegate));
				return;
			case 629:
				ScriptingInterfaceOfIMetaMesh.call_SetFactorColorToSubMeshesWithTagDelegate = (ScriptingInterfaceOfIMetaMesh.SetFactorColorToSubMeshesWithTagDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetFactorColorToSubMeshesWithTagDelegate));
				return;
			case 630:
				ScriptingInterfaceOfIMetaMesh.call_SetFrameDelegate = (ScriptingInterfaceOfIMetaMesh.SetFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetFrameDelegate));
				return;
			case 631:
				ScriptingInterfaceOfIMetaMesh.call_SetGlossMultiplierDelegate = (ScriptingInterfaceOfIMetaMesh.SetGlossMultiplierDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetGlossMultiplierDelegate));
				return;
			case 632:
				ScriptingInterfaceOfIMetaMesh.call_SetLodBiasDelegate = (ScriptingInterfaceOfIMetaMesh.SetLodBiasDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetLodBiasDelegate));
				return;
			case 633:
				ScriptingInterfaceOfIMetaMesh.call_SetMaterialDelegate = (ScriptingInterfaceOfIMetaMesh.SetMaterialDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetMaterialDelegate));
				return;
			case 634:
				ScriptingInterfaceOfIMetaMesh.call_SetMaterialToSubMeshesWithTagDelegate = (ScriptingInterfaceOfIMetaMesh.SetMaterialToSubMeshesWithTagDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetMaterialToSubMeshesWithTagDelegate));
				return;
			case 635:
				ScriptingInterfaceOfIMetaMesh.call_SetNumLodsDelegate = (ScriptingInterfaceOfIMetaMesh.SetNumLodsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetNumLodsDelegate));
				return;
			case 636:
				ScriptingInterfaceOfIMetaMesh.call_SetVectorArgumentDelegate = (ScriptingInterfaceOfIMetaMesh.SetVectorArgumentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetVectorArgumentDelegate));
				return;
			case 637:
				ScriptingInterfaceOfIMetaMesh.call_SetVectorArgument2Delegate = (ScriptingInterfaceOfIMetaMesh.SetVectorArgument2Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetVectorArgument2Delegate));
				return;
			case 638:
				ScriptingInterfaceOfIMetaMesh.call_SetVectorUserDataDelegate = (ScriptingInterfaceOfIMetaMesh.SetVectorUserDataDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetVectorUserDataDelegate));
				return;
			case 639:
				ScriptingInterfaceOfIMetaMesh.call_SetVisibilityMaskDelegate = (ScriptingInterfaceOfIMetaMesh.SetVisibilityMaskDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.SetVisibilityMaskDelegate));
				return;
			case 640:
				ScriptingInterfaceOfIMetaMesh.call_UseHeadBoneFaceGenScalingDelegate = (ScriptingInterfaceOfIMetaMesh.UseHeadBoneFaceGenScalingDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMetaMesh.UseHeadBoneFaceGenScalingDelegate));
				return;
			case 641:
				ScriptingInterfaceOfIMouseManager.call_ActivateMouseCursorDelegate = (ScriptingInterfaceOfIMouseManager.ActivateMouseCursorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMouseManager.ActivateMouseCursorDelegate));
				return;
			case 642:
				ScriptingInterfaceOfIMouseManager.call_LockCursorAtCurrentPositionDelegate = (ScriptingInterfaceOfIMouseManager.LockCursorAtCurrentPositionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMouseManager.LockCursorAtCurrentPositionDelegate));
				return;
			case 643:
				ScriptingInterfaceOfIMouseManager.call_LockCursorAtPositionDelegate = (ScriptingInterfaceOfIMouseManager.LockCursorAtPositionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMouseManager.LockCursorAtPositionDelegate));
				return;
			case 644:
				ScriptingInterfaceOfIMouseManager.call_SetMouseCursorDelegate = (ScriptingInterfaceOfIMouseManager.SetMouseCursorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMouseManager.SetMouseCursorDelegate));
				return;
			case 645:
				ScriptingInterfaceOfIMouseManager.call_ShowCursorDelegate = (ScriptingInterfaceOfIMouseManager.ShowCursorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMouseManager.ShowCursorDelegate));
				return;
			case 646:
				ScriptingInterfaceOfIMouseManager.call_UnlockCursorDelegate = (ScriptingInterfaceOfIMouseManager.UnlockCursorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMouseManager.UnlockCursorDelegate));
				return;
			case 647:
				ScriptingInterfaceOfIMusic.call_GetFreeMusicChannelIndexDelegate = (ScriptingInterfaceOfIMusic.GetFreeMusicChannelIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMusic.GetFreeMusicChannelIndexDelegate));
				return;
			case 648:
				ScriptingInterfaceOfIMusic.call_IsClipLoadedDelegate = (ScriptingInterfaceOfIMusic.IsClipLoadedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMusic.IsClipLoadedDelegate));
				return;
			case 649:
				ScriptingInterfaceOfIMusic.call_IsMusicPlayingDelegate = (ScriptingInterfaceOfIMusic.IsMusicPlayingDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMusic.IsMusicPlayingDelegate));
				return;
			case 650:
				ScriptingInterfaceOfIMusic.call_LoadClipDelegate = (ScriptingInterfaceOfIMusic.LoadClipDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMusic.LoadClipDelegate));
				return;
			case 651:
				ScriptingInterfaceOfIMusic.call_PauseMusicDelegate = (ScriptingInterfaceOfIMusic.PauseMusicDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMusic.PauseMusicDelegate));
				return;
			case 652:
				ScriptingInterfaceOfIMusic.call_PlayDelayedDelegate = (ScriptingInterfaceOfIMusic.PlayDelayedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMusic.PlayDelayedDelegate));
				return;
			case 653:
				ScriptingInterfaceOfIMusic.call_PlayMusicDelegate = (ScriptingInterfaceOfIMusic.PlayMusicDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMusic.PlayMusicDelegate));
				return;
			case 654:
				ScriptingInterfaceOfIMusic.call_SetVolumeDelegate = (ScriptingInterfaceOfIMusic.SetVolumeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMusic.SetVolumeDelegate));
				return;
			case 655:
				ScriptingInterfaceOfIMusic.call_StopMusicDelegate = (ScriptingInterfaceOfIMusic.StopMusicDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMusic.StopMusicDelegate));
				return;
			case 656:
				ScriptingInterfaceOfIMusic.call_UnloadClipDelegate = (ScriptingInterfaceOfIMusic.UnloadClipDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIMusic.UnloadClipDelegate));
				return;
			case 657:
				ScriptingInterfaceOfIParticleSystem.call_CreateParticleSystemAttachedToBoneDelegate = (ScriptingInterfaceOfIParticleSystem.CreateParticleSystemAttachedToBoneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIParticleSystem.CreateParticleSystemAttachedToBoneDelegate));
				return;
			case 658:
				ScriptingInterfaceOfIParticleSystem.call_CreateParticleSystemAttachedToEntityDelegate = (ScriptingInterfaceOfIParticleSystem.CreateParticleSystemAttachedToEntityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIParticleSystem.CreateParticleSystemAttachedToEntityDelegate));
				return;
			case 659:
				ScriptingInterfaceOfIParticleSystem.call_GetLocalFrameDelegate = (ScriptingInterfaceOfIParticleSystem.GetLocalFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIParticleSystem.GetLocalFrameDelegate));
				return;
			case 660:
				ScriptingInterfaceOfIParticleSystem.call_GetRuntimeIdByNameDelegate = (ScriptingInterfaceOfIParticleSystem.GetRuntimeIdByNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIParticleSystem.GetRuntimeIdByNameDelegate));
				return;
			case 661:
				ScriptingInterfaceOfIParticleSystem.call_RestartDelegate = (ScriptingInterfaceOfIParticleSystem.RestartDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIParticleSystem.RestartDelegate));
				return;
			case 662:
				ScriptingInterfaceOfIParticleSystem.call_SetEnableDelegate = (ScriptingInterfaceOfIParticleSystem.SetEnableDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIParticleSystem.SetEnableDelegate));
				return;
			case 663:
				ScriptingInterfaceOfIParticleSystem.call_SetLocalFrameDelegate = (ScriptingInterfaceOfIParticleSystem.SetLocalFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIParticleSystem.SetLocalFrameDelegate));
				return;
			case 664:
				ScriptingInterfaceOfIParticleSystem.call_SetParticleEffectByNameDelegate = (ScriptingInterfaceOfIParticleSystem.SetParticleEffectByNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIParticleSystem.SetParticleEffectByNameDelegate));
				return;
			case 665:
				ScriptingInterfaceOfIParticleSystem.call_SetRuntimeEmissionRateMultiplierDelegate = (ScriptingInterfaceOfIParticleSystem.SetRuntimeEmissionRateMultiplierDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIParticleSystem.SetRuntimeEmissionRateMultiplierDelegate));
				return;
			case 666:
				ScriptingInterfaceOfIPath.call_AddPathPointDelegate = (ScriptingInterfaceOfIPath.AddPathPointDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPath.AddPathPointDelegate));
				return;
			case 667:
				ScriptingInterfaceOfIPath.call_DeletePathPointDelegate = (ScriptingInterfaceOfIPath.DeletePathPointDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPath.DeletePathPointDelegate));
				return;
			case 668:
				ScriptingInterfaceOfIPath.call_GetArcLengthDelegate = (ScriptingInterfaceOfIPath.GetArcLengthDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPath.GetArcLengthDelegate));
				return;
			case 669:
				ScriptingInterfaceOfIPath.call_GetHermiteFrameAndColorForDistanceDelegate = (ScriptingInterfaceOfIPath.GetHermiteFrameAndColorForDistanceDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPath.GetHermiteFrameAndColorForDistanceDelegate));
				return;
			case 670:
				ScriptingInterfaceOfIPath.call_GetHermiteFrameForDistanceDelegate = (ScriptingInterfaceOfIPath.GetHermiteFrameForDistanceDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPath.GetHermiteFrameForDistanceDelegate));
				return;
			case 671:
				ScriptingInterfaceOfIPath.call_GetHermiteFrameForDtDelegate = (ScriptingInterfaceOfIPath.GetHermiteFrameForDtDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPath.GetHermiteFrameForDtDelegate));
				return;
			case 672:
				ScriptingInterfaceOfIPath.call_GetNameDelegate = (ScriptingInterfaceOfIPath.GetNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPath.GetNameDelegate));
				return;
			case 673:
				ScriptingInterfaceOfIPath.call_GetNearestHermiteFrameWithValidAlphaForDistanceDelegate = (ScriptingInterfaceOfIPath.GetNearestHermiteFrameWithValidAlphaForDistanceDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPath.GetNearestHermiteFrameWithValidAlphaForDistanceDelegate));
				return;
			case 674:
				ScriptingInterfaceOfIPath.call_GetNumberOfPointsDelegate = (ScriptingInterfaceOfIPath.GetNumberOfPointsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPath.GetNumberOfPointsDelegate));
				return;
			case 675:
				ScriptingInterfaceOfIPath.call_GetPointsDelegate = (ScriptingInterfaceOfIPath.GetPointsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPath.GetPointsDelegate));
				return;
			case 676:
				ScriptingInterfaceOfIPath.call_GetTotalLengthDelegate = (ScriptingInterfaceOfIPath.GetTotalLengthDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPath.GetTotalLengthDelegate));
				return;
			case 677:
				ScriptingInterfaceOfIPath.call_GetVersionDelegate = (ScriptingInterfaceOfIPath.GetVersionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPath.GetVersionDelegate));
				return;
			case 678:
				ScriptingInterfaceOfIPath.call_HasValidAlphaAtPathPointDelegate = (ScriptingInterfaceOfIPath.HasValidAlphaAtPathPointDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPath.HasValidAlphaAtPathPointDelegate));
				return;
			case 679:
				ScriptingInterfaceOfIPath.call_SetFrameOfPointDelegate = (ScriptingInterfaceOfIPath.SetFrameOfPointDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPath.SetFrameOfPointDelegate));
				return;
			case 680:
				ScriptingInterfaceOfIPath.call_SetTangentPositionOfPointDelegate = (ScriptingInterfaceOfIPath.SetTangentPositionOfPointDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPath.SetTangentPositionOfPointDelegate));
				return;
			case 681:
				ScriptingInterfaceOfIPhysicsMaterial.call_GetDynamicFrictionAtIndexDelegate = (ScriptingInterfaceOfIPhysicsMaterial.GetDynamicFrictionAtIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsMaterial.GetDynamicFrictionAtIndexDelegate));
				return;
			case 682:
				ScriptingInterfaceOfIPhysicsMaterial.call_GetFlagsAtIndexDelegate = (ScriptingInterfaceOfIPhysicsMaterial.GetFlagsAtIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsMaterial.GetFlagsAtIndexDelegate));
				return;
			case 683:
				ScriptingInterfaceOfIPhysicsMaterial.call_GetIndexWithNameDelegate = (ScriptingInterfaceOfIPhysicsMaterial.GetIndexWithNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsMaterial.GetIndexWithNameDelegate));
				return;
			case 684:
				ScriptingInterfaceOfIPhysicsMaterial.call_GetMaterialCountDelegate = (ScriptingInterfaceOfIPhysicsMaterial.GetMaterialCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsMaterial.GetMaterialCountDelegate));
				return;
			case 685:
				ScriptingInterfaceOfIPhysicsMaterial.call_GetMaterialNameAtIndexDelegate = (ScriptingInterfaceOfIPhysicsMaterial.GetMaterialNameAtIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsMaterial.GetMaterialNameAtIndexDelegate));
				return;
			case 686:
				ScriptingInterfaceOfIPhysicsMaterial.call_GetRestitutionAtIndexDelegate = (ScriptingInterfaceOfIPhysicsMaterial.GetRestitutionAtIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsMaterial.GetRestitutionAtIndexDelegate));
				return;
			case 687:
				ScriptingInterfaceOfIPhysicsMaterial.call_GetSoftnessAtIndexDelegate = (ScriptingInterfaceOfIPhysicsMaterial.GetSoftnessAtIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsMaterial.GetSoftnessAtIndexDelegate));
				return;
			case 688:
				ScriptingInterfaceOfIPhysicsMaterial.call_GetStaticFrictionAtIndexDelegate = (ScriptingInterfaceOfIPhysicsMaterial.GetStaticFrictionAtIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsMaterial.GetStaticFrictionAtIndexDelegate));
				return;
			case 689:
				ScriptingInterfaceOfIPhysicsShape.call_AddCapsuleDelegate = (ScriptingInterfaceOfIPhysicsShape.AddCapsuleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.AddCapsuleDelegate));
				return;
			case 690:
				ScriptingInterfaceOfIPhysicsShape.call_AddPreloadQueueWithNameDelegate = (ScriptingInterfaceOfIPhysicsShape.AddPreloadQueueWithNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.AddPreloadQueueWithNameDelegate));
				return;
			case 691:
				ScriptingInterfaceOfIPhysicsShape.call_AddSphereDelegate = (ScriptingInterfaceOfIPhysicsShape.AddSphereDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.AddSphereDelegate));
				return;
			case 692:
				ScriptingInterfaceOfIPhysicsShape.call_CapsuleCountDelegate = (ScriptingInterfaceOfIPhysicsShape.CapsuleCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.CapsuleCountDelegate));
				return;
			case 693:
				ScriptingInterfaceOfIPhysicsShape.call_clearDelegate = (ScriptingInterfaceOfIPhysicsShape.clearDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.clearDelegate));
				return;
			case 694:
				ScriptingInterfaceOfIPhysicsShape.call_CreateBodyCopyDelegate = (ScriptingInterfaceOfIPhysicsShape.CreateBodyCopyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.CreateBodyCopyDelegate));
				return;
			case 695:
				ScriptingInterfaceOfIPhysicsShape.call_GetBoundingBoxCenterDelegate = (ScriptingInterfaceOfIPhysicsShape.GetBoundingBoxCenterDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.GetBoundingBoxCenterDelegate));
				return;
			case 696:
				ScriptingInterfaceOfIPhysicsShape.call_GetCapsuleDelegate = (ScriptingInterfaceOfIPhysicsShape.GetCapsuleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.GetCapsuleDelegate));
				return;
			case 697:
				ScriptingInterfaceOfIPhysicsShape.call_GetCapsuleWithMaterialDelegate = (ScriptingInterfaceOfIPhysicsShape.GetCapsuleWithMaterialDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.GetCapsuleWithMaterialDelegate));
				return;
			case 698:
				ScriptingInterfaceOfIPhysicsShape.call_GetDominantMaterialForTriangleMeshDelegate = (ScriptingInterfaceOfIPhysicsShape.GetDominantMaterialForTriangleMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.GetDominantMaterialForTriangleMeshDelegate));
				return;
			case 699:
				ScriptingInterfaceOfIPhysicsShape.call_GetFromResourceDelegate = (ScriptingInterfaceOfIPhysicsShape.GetFromResourceDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.GetFromResourceDelegate));
				return;
			case 700:
				ScriptingInterfaceOfIPhysicsShape.call_GetNameDelegate = (ScriptingInterfaceOfIPhysicsShape.GetNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.GetNameDelegate));
				return;
			case 701:
				ScriptingInterfaceOfIPhysicsShape.call_GetSphereDelegate = (ScriptingInterfaceOfIPhysicsShape.GetSphereDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.GetSphereDelegate));
				return;
			case 702:
				ScriptingInterfaceOfIPhysicsShape.call_GetSphereWithMaterialDelegate = (ScriptingInterfaceOfIPhysicsShape.GetSphereWithMaterialDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.GetSphereWithMaterialDelegate));
				return;
			case 703:
				ScriptingInterfaceOfIPhysicsShape.call_GetTriangleDelegate = (ScriptingInterfaceOfIPhysicsShape.GetTriangleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.GetTriangleDelegate));
				return;
			case 704:
				ScriptingInterfaceOfIPhysicsShape.call_InitDescriptionDelegate = (ScriptingInterfaceOfIPhysicsShape.InitDescriptionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.InitDescriptionDelegate));
				return;
			case 705:
				ScriptingInterfaceOfIPhysicsShape.call_PrepareDelegate = (ScriptingInterfaceOfIPhysicsShape.PrepareDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.PrepareDelegate));
				return;
			case 706:
				ScriptingInterfaceOfIPhysicsShape.call_ProcessPreloadQueueDelegate = (ScriptingInterfaceOfIPhysicsShape.ProcessPreloadQueueDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.ProcessPreloadQueueDelegate));
				return;
			case 707:
				ScriptingInterfaceOfIPhysicsShape.call_SetCapsuleDelegate = (ScriptingInterfaceOfIPhysicsShape.SetCapsuleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.SetCapsuleDelegate));
				return;
			case 708:
				ScriptingInterfaceOfIPhysicsShape.call_SphereCountDelegate = (ScriptingInterfaceOfIPhysicsShape.SphereCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.SphereCountDelegate));
				return;
			case 709:
				ScriptingInterfaceOfIPhysicsShape.call_TransformDelegate = (ScriptingInterfaceOfIPhysicsShape.TransformDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.TransformDelegate));
				return;
			case 710:
				ScriptingInterfaceOfIPhysicsShape.call_TriangleCountInTriangleMeshDelegate = (ScriptingInterfaceOfIPhysicsShape.TriangleCountInTriangleMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.TriangleCountInTriangleMeshDelegate));
				return;
			case 711:
				ScriptingInterfaceOfIPhysicsShape.call_TriangleMeshCountDelegate = (ScriptingInterfaceOfIPhysicsShape.TriangleMeshCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.TriangleMeshCountDelegate));
				return;
			case 712:
				ScriptingInterfaceOfIPhysicsShape.call_UnloadDynamicBodiesDelegate = (ScriptingInterfaceOfIPhysicsShape.UnloadDynamicBodiesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIPhysicsShape.UnloadDynamicBodiesDelegate));
				return;
			case 713:
				ScriptingInterfaceOfIScene.call_AddDecalInstanceDelegate = (ScriptingInterfaceOfIScene.AddDecalInstanceDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.AddDecalInstanceDelegate));
				return;
			case 714:
				ScriptingInterfaceOfIScene.call_AddDirectionalLightDelegate = (ScriptingInterfaceOfIScene.AddDirectionalLightDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.AddDirectionalLightDelegate));
				return;
			case 715:
				ScriptingInterfaceOfIScene.call_AddEntityWithMeshDelegate = (ScriptingInterfaceOfIScene.AddEntityWithMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.AddEntityWithMeshDelegate));
				return;
			case 716:
				ScriptingInterfaceOfIScene.call_AddEntityWithMultiMeshDelegate = (ScriptingInterfaceOfIScene.AddEntityWithMultiMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.AddEntityWithMultiMeshDelegate));
				return;
			case 717:
				ScriptingInterfaceOfIScene.call_AddItemEntityDelegate = (ScriptingInterfaceOfIScene.AddItemEntityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.AddItemEntityDelegate));
				return;
			case 718:
				ScriptingInterfaceOfIScene.call_AddPathDelegate = (ScriptingInterfaceOfIScene.AddPathDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.AddPathDelegate));
				return;
			case 719:
				ScriptingInterfaceOfIScene.call_AddPathPointDelegate = (ScriptingInterfaceOfIScene.AddPathPointDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.AddPathPointDelegate));
				return;
			case 720:
				ScriptingInterfaceOfIScene.call_AddPointLightDelegate = (ScriptingInterfaceOfIScene.AddPointLightDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.AddPointLightDelegate));
				return;
			case 721:
				ScriptingInterfaceOfIScene.call_AttachEntityDelegate = (ScriptingInterfaceOfIScene.AttachEntityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.AttachEntityDelegate));
				return;
			case 722:
				ScriptingInterfaceOfIScene.call_BoxCastDelegate = (ScriptingInterfaceOfIScene.BoxCastDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.BoxCastDelegate));
				return;
			case 723:
				ScriptingInterfaceOfIScene.call_BoxCastOnlyForCameraDelegate = (ScriptingInterfaceOfIScene.BoxCastOnlyForCameraDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.BoxCastOnlyForCameraDelegate));
				return;
			case 724:
				ScriptingInterfaceOfIScene.call_CalculateEffectiveLightingDelegate = (ScriptingInterfaceOfIScene.CalculateEffectiveLightingDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.CalculateEffectiveLightingDelegate));
				return;
			case 725:
				ScriptingInterfaceOfIScene.call_CheckPathEntitiesFrameChangedDelegate = (ScriptingInterfaceOfIScene.CheckPathEntitiesFrameChangedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.CheckPathEntitiesFrameChangedDelegate));
				return;
			case 726:
				ScriptingInterfaceOfIScene.call_CheckPointCanSeePointDelegate = (ScriptingInterfaceOfIScene.CheckPointCanSeePointDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.CheckPointCanSeePointDelegate));
				return;
			case 727:
				ScriptingInterfaceOfIScene.call_CheckResourcesDelegate = (ScriptingInterfaceOfIScene.CheckResourcesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.CheckResourcesDelegate));
				return;
			case 728:
				ScriptingInterfaceOfIScene.call_ClearAllDelegate = (ScriptingInterfaceOfIScene.ClearAllDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.ClearAllDelegate));
				return;
			case 729:
				ScriptingInterfaceOfIScene.call_ClearDecalsDelegate = (ScriptingInterfaceOfIScene.ClearDecalsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.ClearDecalsDelegate));
				return;
			case 730:
				ScriptingInterfaceOfIScene.call_ContainsTerrainDelegate = (ScriptingInterfaceOfIScene.ContainsTerrainDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.ContainsTerrainDelegate));
				return;
			case 731:
				ScriptingInterfaceOfIScene.call_CreateBurstParticleDelegate = (ScriptingInterfaceOfIScene.CreateBurstParticleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.CreateBurstParticleDelegate));
				return;
			case 732:
				ScriptingInterfaceOfIScene.call_CreateNewSceneDelegate = (ScriptingInterfaceOfIScene.CreateNewSceneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.CreateNewSceneDelegate));
				return;
			case 733:
				ScriptingInterfaceOfIScene.call_CreatePathMeshDelegate = (ScriptingInterfaceOfIScene.CreatePathMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.CreatePathMeshDelegate));
				return;
			case 734:
				ScriptingInterfaceOfIScene.call_CreatePathMesh2Delegate = (ScriptingInterfaceOfIScene.CreatePathMesh2Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.CreatePathMesh2Delegate));
				return;
			case 735:
				ScriptingInterfaceOfIScene.call_DeletePathWithNameDelegate = (ScriptingInterfaceOfIScene.DeletePathWithNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.DeletePathWithNameDelegate));
				return;
			case 736:
				ScriptingInterfaceOfIScene.call_DisableStaticShadowsDelegate = (ScriptingInterfaceOfIScene.DisableStaticShadowsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.DisableStaticShadowsDelegate));
				return;
			case 737:
				ScriptingInterfaceOfIScene.call_DoesPathExistBetweenFacesDelegate = (ScriptingInterfaceOfIScene.DoesPathExistBetweenFacesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.DoesPathExistBetweenFacesDelegate));
				return;
			case 738:
				ScriptingInterfaceOfIScene.call_DoesPathExistBetweenPositionsDelegate = (ScriptingInterfaceOfIScene.DoesPathExistBetweenPositionsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.DoesPathExistBetweenPositionsDelegate));
				return;
			case 739:
				ScriptingInterfaceOfIScene.call_EnsurePostfxSystemDelegate = (ScriptingInterfaceOfIScene.EnsurePostfxSystemDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.EnsurePostfxSystemDelegate));
				return;
			case 740:
				ScriptingInterfaceOfIScene.call_FillEntityWithHardBorderPhysicsBarrierDelegate = (ScriptingInterfaceOfIScene.FillEntityWithHardBorderPhysicsBarrierDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.FillEntityWithHardBorderPhysicsBarrierDelegate));
				return;
			case 741:
				ScriptingInterfaceOfIScene.call_FillTerrainHeightDataDelegate = (ScriptingInterfaceOfIScene.FillTerrainHeightDataDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.FillTerrainHeightDataDelegate));
				return;
			case 742:
				ScriptingInterfaceOfIScene.call_FillTerrainPhysicsMaterialIndexDataDelegate = (ScriptingInterfaceOfIScene.FillTerrainPhysicsMaterialIndexDataDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.FillTerrainPhysicsMaterialIndexDataDelegate));
				return;
			case 743:
				ScriptingInterfaceOfIScene.call_FinishSceneSoundsDelegate = (ScriptingInterfaceOfIScene.FinishSceneSoundsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.FinishSceneSoundsDelegate));
				return;
			case 744:
				ScriptingInterfaceOfIScene.call_ForceLoadResourcesDelegate = (ScriptingInterfaceOfIScene.ForceLoadResourcesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.ForceLoadResourcesDelegate));
				return;
			case 745:
				ScriptingInterfaceOfIScene.call_GenerateContactsWithCapsuleDelegate = (ScriptingInterfaceOfIScene.GenerateContactsWithCapsuleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GenerateContactsWithCapsuleDelegate));
				return;
			case 746:
				ScriptingInterfaceOfIScene.call_GetAllColorGradeNamesDelegate = (ScriptingInterfaceOfIScene.GetAllColorGradeNamesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetAllColorGradeNamesDelegate));
				return;
			case 747:
				ScriptingInterfaceOfIScene.call_GetAllEntitiesWithScriptComponentDelegate = (ScriptingInterfaceOfIScene.GetAllEntitiesWithScriptComponentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetAllEntitiesWithScriptComponentDelegate));
				return;
			case 748:
				ScriptingInterfaceOfIScene.call_GetAllFilterNamesDelegate = (ScriptingInterfaceOfIScene.GetAllFilterNamesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetAllFilterNamesDelegate));
				return;
			case 749:
				ScriptingInterfaceOfIScene.call_GetBoundingBoxDelegate = (ScriptingInterfaceOfIScene.GetBoundingBoxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetBoundingBoxDelegate));
				return;
			case 750:
				ScriptingInterfaceOfIScene.call_GetCampaignEntityWithNameDelegate = (ScriptingInterfaceOfIScene.GetCampaignEntityWithNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetCampaignEntityWithNameDelegate));
				return;
			case 751:
				ScriptingInterfaceOfIScene.call_GetEntitiesDelegate = (ScriptingInterfaceOfIScene.GetEntitiesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetEntitiesDelegate));
				return;
			case 752:
				ScriptingInterfaceOfIScene.call_GetEntityCountDelegate = (ScriptingInterfaceOfIScene.GetEntityCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetEntityCountDelegate));
				return;
			case 753:
				ScriptingInterfaceOfIScene.call_GetEntityWithGuidDelegate = (ScriptingInterfaceOfIScene.GetEntityWithGuidDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetEntityWithGuidDelegate));
				return;
			case 754:
				ScriptingInterfaceOfIScene.call_GetFirstEntityWithNameDelegate = (ScriptingInterfaceOfIScene.GetFirstEntityWithNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetFirstEntityWithNameDelegate));
				return;
			case 755:
				ScriptingInterfaceOfIScene.call_GetFirstEntityWithScriptComponentDelegate = (ScriptingInterfaceOfIScene.GetFirstEntityWithScriptComponentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetFirstEntityWithScriptComponentDelegate));
				return;
			case 756:
				ScriptingInterfaceOfIScene.call_GetFloraInstanceCountDelegate = (ScriptingInterfaceOfIScene.GetFloraInstanceCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetFloraInstanceCountDelegate));
				return;
			case 757:
				ScriptingInterfaceOfIScene.call_GetFloraRendererTextureUsageDelegate = (ScriptingInterfaceOfIScene.GetFloraRendererTextureUsageDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetFloraRendererTextureUsageDelegate));
				return;
			case 758:
				ScriptingInterfaceOfIScene.call_GetFogDelegate = (ScriptingInterfaceOfIScene.GetFogDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetFogDelegate));
				return;
			case 759:
				ScriptingInterfaceOfIScene.call_GetGroundHeightAndNormalAtPositionDelegate = (ScriptingInterfaceOfIScene.GetGroundHeightAndNormalAtPositionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetGroundHeightAndNormalAtPositionDelegate));
				return;
			case 760:
				ScriptingInterfaceOfIScene.call_GetGroundHeightAtPositionDelegate = (ScriptingInterfaceOfIScene.GetGroundHeightAtPositionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetGroundHeightAtPositionDelegate));
				return;
			case 761:
				ScriptingInterfaceOfIScene.call_GetHardBoundaryVertexDelegate = (ScriptingInterfaceOfIScene.GetHardBoundaryVertexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetHardBoundaryVertexDelegate));
				return;
			case 762:
				ScriptingInterfaceOfIScene.call_GetHardBoundaryVertexCountDelegate = (ScriptingInterfaceOfIScene.GetHardBoundaryVertexCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetHardBoundaryVertexCountDelegate));
				return;
			case 763:
				ScriptingInterfaceOfIScene.call_GetHeightAtPointDelegate = (ScriptingInterfaceOfIScene.GetHeightAtPointDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetHeightAtPointDelegate));
				return;
			case 764:
				ScriptingInterfaceOfIScene.call_GetIdOfNavMeshFaceDelegate = (ScriptingInterfaceOfIScene.GetIdOfNavMeshFaceDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetIdOfNavMeshFaceDelegate));
				return;
			case 765:
				ScriptingInterfaceOfIScene.call_GetLastFinalRenderCameraFrameDelegate = (ScriptingInterfaceOfIScene.GetLastFinalRenderCameraFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetLastFinalRenderCameraFrameDelegate));
				return;
			case 766:
				ScriptingInterfaceOfIScene.call_GetLastFinalRenderCameraPositionDelegate = (ScriptingInterfaceOfIScene.GetLastFinalRenderCameraPositionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetLastFinalRenderCameraPositionDelegate));
				return;
			case 767:
				ScriptingInterfaceOfIScene.call_GetLastPointOnNavigationMeshFromPositionToDestinationDelegate = (ScriptingInterfaceOfIScene.GetLastPointOnNavigationMeshFromPositionToDestinationDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetLastPointOnNavigationMeshFromPositionToDestinationDelegate));
				return;
			case 768:
				ScriptingInterfaceOfIScene.call_GetLastPointOnNavigationMeshFromWorldPositionToDestinationDelegate = (ScriptingInterfaceOfIScene.GetLastPointOnNavigationMeshFromWorldPositionToDestinationDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetLastPointOnNavigationMeshFromWorldPositionToDestinationDelegate));
				return;
			case 769:
				ScriptingInterfaceOfIScene.call_GetLoadingStateNameDelegate = (ScriptingInterfaceOfIScene.GetLoadingStateNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetLoadingStateNameDelegate));
				return;
			case 770:
				ScriptingInterfaceOfIScene.call_GetModulePathDelegate = (ScriptingInterfaceOfIScene.GetModulePathDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetModulePathDelegate));
				return;
			case 771:
				ScriptingInterfaceOfIScene.call_GetNameDelegate = (ScriptingInterfaceOfIScene.GetNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetNameDelegate));
				return;
			case 772:
				ScriptingInterfaceOfIScene.call_GetNavigationMeshFaceForPositionDelegate = (ScriptingInterfaceOfIScene.GetNavigationMeshFaceForPositionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetNavigationMeshFaceForPositionDelegate));
				return;
			case 773:
				ScriptingInterfaceOfIScene.call_GetNavMeshFaceCenterPositionDelegate = (ScriptingInterfaceOfIScene.GetNavMeshFaceCenterPositionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetNavMeshFaceCenterPositionDelegate));
				return;
			case 774:
				ScriptingInterfaceOfIScene.call_GetNavMeshFaceCountDelegate = (ScriptingInterfaceOfIScene.GetNavMeshFaceCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetNavMeshFaceCountDelegate));
				return;
			case 775:
				ScriptingInterfaceOfIScene.call_GetNavMeshFaceFirstVertexZDelegate = (ScriptingInterfaceOfIScene.GetNavMeshFaceFirstVertexZDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetNavMeshFaceFirstVertexZDelegate));
				return;
			case 776:
				ScriptingInterfaceOfIScene.call_GetNavMeshFaceIndexDelegate = (ScriptingInterfaceOfIScene.GetNavMeshFaceIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetNavMeshFaceIndexDelegate));
				return;
			case 777:
				ScriptingInterfaceOfIScene.call_GetNavMeshFaceIndex3Delegate = (ScriptingInterfaceOfIScene.GetNavMeshFaceIndex3Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetNavMeshFaceIndex3Delegate));
				return;
			case 778:
				ScriptingInterfaceOfIScene.call_GetNodeDataCountDelegate = (ScriptingInterfaceOfIScene.GetNodeDataCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetNodeDataCountDelegate));
				return;
			case 779:
				ScriptingInterfaceOfIScene.call_GetNormalAtDelegate = (ScriptingInterfaceOfIScene.GetNormalAtDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetNormalAtDelegate));
				return;
			case 780:
				ScriptingInterfaceOfIScene.call_GetNorthAngleDelegate = (ScriptingInterfaceOfIScene.GetNorthAngleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetNorthAngleDelegate));
				return;
			case 781:
				ScriptingInterfaceOfIScene.call_GetNumberOfPathsWithNamePrefixDelegate = (ScriptingInterfaceOfIScene.GetNumberOfPathsWithNamePrefixDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetNumberOfPathsWithNamePrefixDelegate));
				return;
			case 782:
				ScriptingInterfaceOfIScene.call_GetPathBetweenAIFaceIndicesDelegate = (ScriptingInterfaceOfIScene.GetPathBetweenAIFaceIndicesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetPathBetweenAIFaceIndicesDelegate));
				return;
			case 783:
				ScriptingInterfaceOfIScene.call_GetPathBetweenAIFacePointersDelegate = (ScriptingInterfaceOfIScene.GetPathBetweenAIFacePointersDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetPathBetweenAIFacePointersDelegate));
				return;
			case 784:
				ScriptingInterfaceOfIScene.call_GetPathDistanceBetweenAIFacesDelegate = (ScriptingInterfaceOfIScene.GetPathDistanceBetweenAIFacesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetPathDistanceBetweenAIFacesDelegate));
				return;
			case 785:
				ScriptingInterfaceOfIScene.call_GetPathDistanceBetweenPositionsDelegate = (ScriptingInterfaceOfIScene.GetPathDistanceBetweenPositionsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetPathDistanceBetweenPositionsDelegate));
				return;
			case 786:
				ScriptingInterfaceOfIScene.call_GetPathsWithNamePrefixDelegate = (ScriptingInterfaceOfIScene.GetPathsWithNamePrefixDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetPathsWithNamePrefixDelegate));
				return;
			case 787:
				ScriptingInterfaceOfIScene.call_GetPathWithNameDelegate = (ScriptingInterfaceOfIScene.GetPathWithNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetPathWithNameDelegate));
				return;
			case 788:
				ScriptingInterfaceOfIScene.call_GetPhotoModeFocusDelegate = (ScriptingInterfaceOfIScene.GetPhotoModeFocusDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetPhotoModeFocusDelegate));
				return;
			case 789:
				ScriptingInterfaceOfIScene.call_GetPhotoModeFovDelegate = (ScriptingInterfaceOfIScene.GetPhotoModeFovDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetPhotoModeFovDelegate));
				return;
			case 790:
				ScriptingInterfaceOfIScene.call_GetPhotoModeOnDelegate = (ScriptingInterfaceOfIScene.GetPhotoModeOnDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetPhotoModeOnDelegate));
				return;
			case 791:
				ScriptingInterfaceOfIScene.call_GetPhotoModeOrbitDelegate = (ScriptingInterfaceOfIScene.GetPhotoModeOrbitDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetPhotoModeOrbitDelegate));
				return;
			case 792:
				ScriptingInterfaceOfIScene.call_GetPhotoModeRollDelegate = (ScriptingInterfaceOfIScene.GetPhotoModeRollDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetPhotoModeRollDelegate));
				return;
			case 793:
				ScriptingInterfaceOfIScene.call_GetPhysicsMinMaxDelegate = (ScriptingInterfaceOfIScene.GetPhysicsMinMaxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetPhysicsMinMaxDelegate));
				return;
			case 794:
				ScriptingInterfaceOfIScene.call_GetRainDensityDelegate = (ScriptingInterfaceOfIScene.GetRainDensityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetRainDensityDelegate));
				return;
			case 795:
				ScriptingInterfaceOfIScene.call_GetRootEntitiesDelegate = (ScriptingInterfaceOfIScene.GetRootEntitiesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetRootEntitiesDelegate));
				return;
			case 796:
				ScriptingInterfaceOfIScene.call_GetRootEntityCountDelegate = (ScriptingInterfaceOfIScene.GetRootEntityCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetRootEntityCountDelegate));
				return;
			case 797:
				ScriptingInterfaceOfIScene.call_GetSceneColorGradeIndexDelegate = (ScriptingInterfaceOfIScene.GetSceneColorGradeIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetSceneColorGradeIndexDelegate));
				return;
			case 798:
				ScriptingInterfaceOfIScene.call_GetSceneFilterIndexDelegate = (ScriptingInterfaceOfIScene.GetSceneFilterIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetSceneFilterIndexDelegate));
				return;
			case 799:
				ScriptingInterfaceOfIScene.call_GetScriptedEntityDelegate = (ScriptingInterfaceOfIScene.GetScriptedEntityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetScriptedEntityDelegate));
				return;
			case 800:
				ScriptingInterfaceOfIScene.call_GetScriptedEntityCountDelegate = (ScriptingInterfaceOfIScene.GetScriptedEntityCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetScriptedEntityCountDelegate));
				return;
			case 801:
				ScriptingInterfaceOfIScene.call_GetSkyboxMeshDelegate = (ScriptingInterfaceOfIScene.GetSkyboxMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetSkyboxMeshDelegate));
				return;
			case 802:
				ScriptingInterfaceOfIScene.call_GetSnowAmountDataDelegate = (ScriptingInterfaceOfIScene.GetSnowAmountDataDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetSnowAmountDataDelegate));
				return;
			case 803:
				ScriptingInterfaceOfIScene.call_GetSnowDensityDelegate = (ScriptingInterfaceOfIScene.GetSnowDensityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetSnowDensityDelegate));
				return;
			case 804:
				ScriptingInterfaceOfIScene.call_GetSoftBoundaryVertexDelegate = (ScriptingInterfaceOfIScene.GetSoftBoundaryVertexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetSoftBoundaryVertexDelegate));
				return;
			case 805:
				ScriptingInterfaceOfIScene.call_GetSoftBoundaryVertexCountDelegate = (ScriptingInterfaceOfIScene.GetSoftBoundaryVertexCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetSoftBoundaryVertexCountDelegate));
				return;
			case 806:
				ScriptingInterfaceOfIScene.call_GetSunDirectionDelegate = (ScriptingInterfaceOfIScene.GetSunDirectionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetSunDirectionDelegate));
				return;
			case 807:
				ScriptingInterfaceOfIScene.call_GetTerrainDataDelegate = (ScriptingInterfaceOfIScene.GetTerrainDataDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetTerrainDataDelegate));
				return;
			case 808:
				ScriptingInterfaceOfIScene.call_GetTerrainHeightDelegate = (ScriptingInterfaceOfIScene.GetTerrainHeightDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetTerrainHeightDelegate));
				return;
			case 809:
				ScriptingInterfaceOfIScene.call_GetTerrainHeightAndNormalDelegate = (ScriptingInterfaceOfIScene.GetTerrainHeightAndNormalDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetTerrainHeightAndNormalDelegate));
				return;
			case 810:
				ScriptingInterfaceOfIScene.call_GetTerrainMemoryUsageDelegate = (ScriptingInterfaceOfIScene.GetTerrainMemoryUsageDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetTerrainMemoryUsageDelegate));
				return;
			case 811:
				ScriptingInterfaceOfIScene.call_GetTerrainMinMaxHeightDelegate = (ScriptingInterfaceOfIScene.GetTerrainMinMaxHeightDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetTerrainMinMaxHeightDelegate));
				return;
			case 812:
				ScriptingInterfaceOfIScene.call_GetTerrainNodeDataDelegate = (ScriptingInterfaceOfIScene.GetTerrainNodeDataDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetTerrainNodeDataDelegate));
				return;
			case 813:
				ScriptingInterfaceOfIScene.call_GetTerrainPhysicsMaterialIndexAtLayerDelegate = (ScriptingInterfaceOfIScene.GetTerrainPhysicsMaterialIndexAtLayerDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetTerrainPhysicsMaterialIndexAtLayerDelegate));
				return;
			case 814:
				ScriptingInterfaceOfIScene.call_GetTimeOfDayDelegate = (ScriptingInterfaceOfIScene.GetTimeOfDayDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetTimeOfDayDelegate));
				return;
			case 815:
				ScriptingInterfaceOfIScene.call_GetTimeSpeedDelegate = (ScriptingInterfaceOfIScene.GetTimeSpeedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetTimeSpeedDelegate));
				return;
			case 816:
				ScriptingInterfaceOfIScene.call_GetUpgradeLevelCountDelegate = (ScriptingInterfaceOfIScene.GetUpgradeLevelCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetUpgradeLevelCountDelegate));
				return;
			case 817:
				ScriptingInterfaceOfIScene.call_GetUpgradeLevelMaskDelegate = (ScriptingInterfaceOfIScene.GetUpgradeLevelMaskDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetUpgradeLevelMaskDelegate));
				return;
			case 818:
				ScriptingInterfaceOfIScene.call_GetUpgradeLevelMaskOfLevelNameDelegate = (ScriptingInterfaceOfIScene.GetUpgradeLevelMaskOfLevelNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetUpgradeLevelMaskOfLevelNameDelegate));
				return;
			case 819:
				ScriptingInterfaceOfIScene.call_GetUpgradeLevelNameOfIndexDelegate = (ScriptingInterfaceOfIScene.GetUpgradeLevelNameOfIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetUpgradeLevelNameOfIndexDelegate));
				return;
			case 820:
				ScriptingInterfaceOfIScene.call_GetWaterLevelDelegate = (ScriptingInterfaceOfIScene.GetWaterLevelDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetWaterLevelDelegate));
				return;
			case 821:
				ScriptingInterfaceOfIScene.call_GetWaterLevelAtPositionDelegate = (ScriptingInterfaceOfIScene.GetWaterLevelAtPositionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetWaterLevelAtPositionDelegate));
				return;
			case 822:
				ScriptingInterfaceOfIScene.call_GetWinterTimeFactorDelegate = (ScriptingInterfaceOfIScene.GetWinterTimeFactorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.GetWinterTimeFactorDelegate));
				return;
			case 823:
				ScriptingInterfaceOfIScene.call_HasTerrainHeightmapDelegate = (ScriptingInterfaceOfIScene.HasTerrainHeightmapDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.HasTerrainHeightmapDelegate));
				return;
			case 824:
				ScriptingInterfaceOfIScene.call_InvalidateTerrainPhysicsMaterialsDelegate = (ScriptingInterfaceOfIScene.InvalidateTerrainPhysicsMaterialsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.InvalidateTerrainPhysicsMaterialsDelegate));
				return;
			case 825:
				ScriptingInterfaceOfIScene.call_IsAnyFaceWithIdDelegate = (ScriptingInterfaceOfIScene.IsAnyFaceWithIdDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.IsAnyFaceWithIdDelegate));
				return;
			case 826:
				ScriptingInterfaceOfIScene.call_IsAtmosphereIndoorDelegate = (ScriptingInterfaceOfIScene.IsAtmosphereIndoorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.IsAtmosphereIndoorDelegate));
				return;
			case 827:
				ScriptingInterfaceOfIScene.call_IsDefaultEditorSceneDelegate = (ScriptingInterfaceOfIScene.IsDefaultEditorSceneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.IsDefaultEditorSceneDelegate));
				return;
			case 828:
				ScriptingInterfaceOfIScene.call_IsEditorSceneDelegate = (ScriptingInterfaceOfIScene.IsEditorSceneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.IsEditorSceneDelegate));
				return;
			case 829:
				ScriptingInterfaceOfIScene.call_IsLineToPointClearDelegate = (ScriptingInterfaceOfIScene.IsLineToPointClearDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.IsLineToPointClearDelegate));
				return;
			case 830:
				ScriptingInterfaceOfIScene.call_IsLineToPointClear2Delegate = (ScriptingInterfaceOfIScene.IsLineToPointClear2Delegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.IsLineToPointClear2Delegate));
				return;
			case 831:
				ScriptingInterfaceOfIScene.call_IsLoadingFinishedDelegate = (ScriptingInterfaceOfIScene.IsLoadingFinishedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.IsLoadingFinishedDelegate));
				return;
			case 832:
				ScriptingInterfaceOfIScene.call_IsMultiplayerSceneDelegate = (ScriptingInterfaceOfIScene.IsMultiplayerSceneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.IsMultiplayerSceneDelegate));
				return;
			case 833:
				ScriptingInterfaceOfIScene.call_LoadNavMeshPrefabDelegate = (ScriptingInterfaceOfIScene.LoadNavMeshPrefabDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.LoadNavMeshPrefabDelegate));
				return;
			case 834:
				ScriptingInterfaceOfIScene.call_MarkFacesWithIdAsLadderDelegate = (ScriptingInterfaceOfIScene.MarkFacesWithIdAsLadderDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.MarkFacesWithIdAsLadderDelegate));
				return;
			case 835:
				ScriptingInterfaceOfIScene.call_MergeFacesWithIdDelegate = (ScriptingInterfaceOfIScene.MergeFacesWithIdDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.MergeFacesWithIdDelegate));
				return;
			case 836:
				ScriptingInterfaceOfIScene.call_OptimizeSceneDelegate = (ScriptingInterfaceOfIScene.OptimizeSceneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.OptimizeSceneDelegate));
				return;
			case 837:
				ScriptingInterfaceOfIScene.call_PauseSceneSoundsDelegate = (ScriptingInterfaceOfIScene.PauseSceneSoundsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.PauseSceneSoundsDelegate));
				return;
			case 838:
				ScriptingInterfaceOfIScene.call_PreloadForRenderingDelegate = (ScriptingInterfaceOfIScene.PreloadForRenderingDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.PreloadForRenderingDelegate));
				return;
			case 839:
				ScriptingInterfaceOfIScene.call_RayCastForClosestEntityOrTerrainDelegate = (ScriptingInterfaceOfIScene.RayCastForClosestEntityOrTerrainDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.RayCastForClosestEntityOrTerrainDelegate));
				return;
			case 840:
				ScriptingInterfaceOfIScene.call_ReadDelegate = (ScriptingInterfaceOfIScene.ReadDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.ReadDelegate));
				return;
			case 841:
				ScriptingInterfaceOfIScene.call_ReadAndCalculateInitialCameraDelegate = (ScriptingInterfaceOfIScene.ReadAndCalculateInitialCameraDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.ReadAndCalculateInitialCameraDelegate));
				return;
			case 842:
				ScriptingInterfaceOfIScene.call_RemoveEntityDelegate = (ScriptingInterfaceOfIScene.RemoveEntityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.RemoveEntityDelegate));
				return;
			case 843:
				ScriptingInterfaceOfIScene.call_ResumeLoadingRenderingsDelegate = (ScriptingInterfaceOfIScene.ResumeLoadingRenderingsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.ResumeLoadingRenderingsDelegate));
				return;
			case 844:
				ScriptingInterfaceOfIScene.call_ResumeSceneSoundsDelegate = (ScriptingInterfaceOfIScene.ResumeSceneSoundsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.ResumeSceneSoundsDelegate));
				return;
			case 845:
				ScriptingInterfaceOfIScene.call_SelectEntitiesCollidedWithDelegate = (ScriptingInterfaceOfIScene.SelectEntitiesCollidedWithDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SelectEntitiesCollidedWithDelegate));
				return;
			case 846:
				ScriptingInterfaceOfIScene.call_SelectEntitiesInBoxWithScriptComponentDelegate = (ScriptingInterfaceOfIScene.SelectEntitiesInBoxWithScriptComponentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SelectEntitiesInBoxWithScriptComponentDelegate));
				return;
			case 847:
				ScriptingInterfaceOfIScene.call_SeparateFacesWithIdDelegate = (ScriptingInterfaceOfIScene.SeparateFacesWithIdDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SeparateFacesWithIdDelegate));
				return;
			case 848:
				ScriptingInterfaceOfIScene.call_SetAberrationOffsetDelegate = (ScriptingInterfaceOfIScene.SetAberrationOffsetDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetAberrationOffsetDelegate));
				return;
			case 849:
				ScriptingInterfaceOfIScene.call_SetAberrationSizeDelegate = (ScriptingInterfaceOfIScene.SetAberrationSizeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetAberrationSizeDelegate));
				return;
			case 850:
				ScriptingInterfaceOfIScene.call_SetAberrationSmoothDelegate = (ScriptingInterfaceOfIScene.SetAberrationSmoothDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetAberrationSmoothDelegate));
				return;
			case 851:
				ScriptingInterfaceOfIScene.call_SetAbilityOfFacesWithIdDelegate = (ScriptingInterfaceOfIScene.SetAbilityOfFacesWithIdDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetAbilityOfFacesWithIdDelegate));
				return;
			case 852:
				ScriptingInterfaceOfIScene.call_SetActiveVisibilityLevelsDelegate = (ScriptingInterfaceOfIScene.SetActiveVisibilityLevelsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetActiveVisibilityLevelsDelegate));
				return;
			case 853:
				ScriptingInterfaceOfIScene.call_SetAntialiasingModeDelegate = (ScriptingInterfaceOfIScene.SetAntialiasingModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetAntialiasingModeDelegate));
				return;
			case 854:
				ScriptingInterfaceOfIScene.call_SetAtmosphereWithNameDelegate = (ScriptingInterfaceOfIScene.SetAtmosphereWithNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetAtmosphereWithNameDelegate));
				return;
			case 855:
				ScriptingInterfaceOfIScene.call_SetBloomDelegate = (ScriptingInterfaceOfIScene.SetBloomDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetBloomDelegate));
				return;
			case 856:
				ScriptingInterfaceOfIScene.call_SetBloomAmountDelegate = (ScriptingInterfaceOfIScene.SetBloomAmountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetBloomAmountDelegate));
				return;
			case 857:
				ScriptingInterfaceOfIScene.call_SetBloomStrengthDelegate = (ScriptingInterfaceOfIScene.SetBloomStrengthDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetBloomStrengthDelegate));
				return;
			case 858:
				ScriptingInterfaceOfIScene.call_SetBrightpassTresholdDelegate = (ScriptingInterfaceOfIScene.SetBrightpassTresholdDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetBrightpassTresholdDelegate));
				return;
			case 859:
				ScriptingInterfaceOfIScene.call_SetClothSimulationStateDelegate = (ScriptingInterfaceOfIScene.SetClothSimulationStateDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetClothSimulationStateDelegate));
				return;
			case 860:
				ScriptingInterfaceOfIScene.call_SetColorGradeBlendDelegate = (ScriptingInterfaceOfIScene.SetColorGradeBlendDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetColorGradeBlendDelegate));
				return;
			case 861:
				ScriptingInterfaceOfIScene.call_SetDLSSModeDelegate = (ScriptingInterfaceOfIScene.SetDLSSModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetDLSSModeDelegate));
				return;
			case 862:
				ScriptingInterfaceOfIScene.call_SetDofFocusDelegate = (ScriptingInterfaceOfIScene.SetDofFocusDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetDofFocusDelegate));
				return;
			case 863:
				ScriptingInterfaceOfIScene.call_SetDofModeDelegate = (ScriptingInterfaceOfIScene.SetDofModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetDofModeDelegate));
				return;
			case 864:
				ScriptingInterfaceOfIScene.call_SetDofParamsDelegate = (ScriptingInterfaceOfIScene.SetDofParamsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetDofParamsDelegate));
				return;
			case 865:
				ScriptingInterfaceOfIScene.call_SetDoNotWaitForLoadingStatesToRenderDelegate = (ScriptingInterfaceOfIScene.SetDoNotWaitForLoadingStatesToRenderDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetDoNotWaitForLoadingStatesToRenderDelegate));
				return;
			case 866:
				ScriptingInterfaceOfIScene.call_SetDrynessFactorDelegate = (ScriptingInterfaceOfIScene.SetDrynessFactorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetDrynessFactorDelegate));
				return;
			case 867:
				ScriptingInterfaceOfIScene.call_SetDynamicShadowmapCascadesRadiusMultiplierDelegate = (ScriptingInterfaceOfIScene.SetDynamicShadowmapCascadesRadiusMultiplierDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetDynamicShadowmapCascadesRadiusMultiplierDelegate));
				return;
			case 868:
				ScriptingInterfaceOfIScene.call_SetEnvironmentMultiplierDelegate = (ScriptingInterfaceOfIScene.SetEnvironmentMultiplierDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetEnvironmentMultiplierDelegate));
				return;
			case 869:
				ScriptingInterfaceOfIScene.call_SetExternalInjectionTextureDelegate = (ScriptingInterfaceOfIScene.SetExternalInjectionTextureDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetExternalInjectionTextureDelegate));
				return;
			case 870:
				ScriptingInterfaceOfIScene.call_SetFogDelegate = (ScriptingInterfaceOfIScene.SetFogDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetFogDelegate));
				return;
			case 871:
				ScriptingInterfaceOfIScene.call_SetFogAdvancedDelegate = (ScriptingInterfaceOfIScene.SetFogAdvancedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetFogAdvancedDelegate));
				return;
			case 872:
				ScriptingInterfaceOfIScene.call_SetFogAmbientColorDelegate = (ScriptingInterfaceOfIScene.SetFogAmbientColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetFogAmbientColorDelegate));
				return;
			case 873:
				ScriptingInterfaceOfIScene.call_SetForcedSnowDelegate = (ScriptingInterfaceOfIScene.SetForcedSnowDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetForcedSnowDelegate));
				return;
			case 874:
				ScriptingInterfaceOfIScene.call_SetGrainAmountDelegate = (ScriptingInterfaceOfIScene.SetGrainAmountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetGrainAmountDelegate));
				return;
			case 875:
				ScriptingInterfaceOfIScene.call_SetHexagonVignetteAlphaDelegate = (ScriptingInterfaceOfIScene.SetHexagonVignetteAlphaDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetHexagonVignetteAlphaDelegate));
				return;
			case 876:
				ScriptingInterfaceOfIScene.call_SetHexagonVignetteColorDelegate = (ScriptingInterfaceOfIScene.SetHexagonVignetteColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetHexagonVignetteColorDelegate));
				return;
			case 877:
				ScriptingInterfaceOfIScene.call_SetHumidityDelegate = (ScriptingInterfaceOfIScene.SetHumidityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetHumidityDelegate));
				return;
			case 878:
				ScriptingInterfaceOfIScene.call_SetLandscapeRainMaskDataDelegate = (ScriptingInterfaceOfIScene.SetLandscapeRainMaskDataDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetLandscapeRainMaskDataDelegate));
				return;
			case 879:
				ScriptingInterfaceOfIScene.call_SetLensDistortionDelegate = (ScriptingInterfaceOfIScene.SetLensDistortionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetLensDistortionDelegate));
				return;
			case 880:
				ScriptingInterfaceOfIScene.call_SetLensFlareAberrationOffsetDelegate = (ScriptingInterfaceOfIScene.SetLensFlareAberrationOffsetDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetLensFlareAberrationOffsetDelegate));
				return;
			case 881:
				ScriptingInterfaceOfIScene.call_SetLensFlareAmountDelegate = (ScriptingInterfaceOfIScene.SetLensFlareAmountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetLensFlareAmountDelegate));
				return;
			case 882:
				ScriptingInterfaceOfIScene.call_SetLensFlareBlurSigmaDelegate = (ScriptingInterfaceOfIScene.SetLensFlareBlurSigmaDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetLensFlareBlurSigmaDelegate));
				return;
			case 883:
				ScriptingInterfaceOfIScene.call_SetLensFlareBlurSizeDelegate = (ScriptingInterfaceOfIScene.SetLensFlareBlurSizeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetLensFlareBlurSizeDelegate));
				return;
			case 884:
				ScriptingInterfaceOfIScene.call_SetLensFlareDiffractionWeightDelegate = (ScriptingInterfaceOfIScene.SetLensFlareDiffractionWeightDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetLensFlareDiffractionWeightDelegate));
				return;
			case 885:
				ScriptingInterfaceOfIScene.call_SetLensFlareDirtWeightDelegate = (ScriptingInterfaceOfIScene.SetLensFlareDirtWeightDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetLensFlareDirtWeightDelegate));
				return;
			case 886:
				ScriptingInterfaceOfIScene.call_SetLensFlareGhostSamplesDelegate = (ScriptingInterfaceOfIScene.SetLensFlareGhostSamplesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetLensFlareGhostSamplesDelegate));
				return;
			case 887:
				ScriptingInterfaceOfIScene.call_SetLensFlareGhostWeightDelegate = (ScriptingInterfaceOfIScene.SetLensFlareGhostWeightDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetLensFlareGhostWeightDelegate));
				return;
			case 888:
				ScriptingInterfaceOfIScene.call_SetLensFlareHaloWeightDelegate = (ScriptingInterfaceOfIScene.SetLensFlareHaloWeightDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetLensFlareHaloWeightDelegate));
				return;
			case 889:
				ScriptingInterfaceOfIScene.call_SetLensFlareHaloWidthDelegate = (ScriptingInterfaceOfIScene.SetLensFlareHaloWidthDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetLensFlareHaloWidthDelegate));
				return;
			case 890:
				ScriptingInterfaceOfIScene.call_SetLensFlareStrengthDelegate = (ScriptingInterfaceOfIScene.SetLensFlareStrengthDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetLensFlareStrengthDelegate));
				return;
			case 891:
				ScriptingInterfaceOfIScene.call_SetLensFlareThresholdDelegate = (ScriptingInterfaceOfIScene.SetLensFlareThresholdDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetLensFlareThresholdDelegate));
				return;
			case 892:
				ScriptingInterfaceOfIScene.call_SetLightDiffuseColorDelegate = (ScriptingInterfaceOfIScene.SetLightDiffuseColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetLightDiffuseColorDelegate));
				return;
			case 893:
				ScriptingInterfaceOfIScene.call_SetLightDirectionDelegate = (ScriptingInterfaceOfIScene.SetLightDirectionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetLightDirectionDelegate));
				return;
			case 894:
				ScriptingInterfaceOfIScene.call_SetLightPositionDelegate = (ScriptingInterfaceOfIScene.SetLightPositionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetLightPositionDelegate));
				return;
			case 895:
				ScriptingInterfaceOfIScene.call_SetMaxExposureDelegate = (ScriptingInterfaceOfIScene.SetMaxExposureDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetMaxExposureDelegate));
				return;
			case 896:
				ScriptingInterfaceOfIScene.call_SetMiddleGrayDelegate = (ScriptingInterfaceOfIScene.SetMiddleGrayDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetMiddleGrayDelegate));
				return;
			case 897:
				ScriptingInterfaceOfIScene.call_SetMieScatterFocusDelegate = (ScriptingInterfaceOfIScene.SetMieScatterFocusDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetMieScatterFocusDelegate));
				return;
			case 898:
				ScriptingInterfaceOfIScene.call_SetMieScatterStrengthDelegate = (ScriptingInterfaceOfIScene.SetMieScatterStrengthDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetMieScatterStrengthDelegate));
				return;
			case 899:
				ScriptingInterfaceOfIScene.call_SetMinExposureDelegate = (ScriptingInterfaceOfIScene.SetMinExposureDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetMinExposureDelegate));
				return;
			case 900:
				ScriptingInterfaceOfIScene.call_SetMotionBlurModeDelegate = (ScriptingInterfaceOfIScene.SetMotionBlurModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetMotionBlurModeDelegate));
				return;
			case 901:
				ScriptingInterfaceOfIScene.call_SetNameDelegate = (ScriptingInterfaceOfIScene.SetNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetNameDelegate));
				return;
			case 902:
				ScriptingInterfaceOfIScene.call_SetOcclusionModeDelegate = (ScriptingInterfaceOfIScene.SetOcclusionModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetOcclusionModeDelegate));
				return;
			case 903:
				ScriptingInterfaceOfIScene.call_SetOwnerThreadDelegate = (ScriptingInterfaceOfIScene.SetOwnerThreadDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetOwnerThreadDelegate));
				return;
			case 904:
				ScriptingInterfaceOfIScene.call_SetPhotoModeFocusDelegate = (ScriptingInterfaceOfIScene.SetPhotoModeFocusDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetPhotoModeFocusDelegate));
				return;
			case 905:
				ScriptingInterfaceOfIScene.call_SetPhotoModeFovDelegate = (ScriptingInterfaceOfIScene.SetPhotoModeFovDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetPhotoModeFovDelegate));
				return;
			case 906:
				ScriptingInterfaceOfIScene.call_SetPhotoModeOnDelegate = (ScriptingInterfaceOfIScene.SetPhotoModeOnDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetPhotoModeOnDelegate));
				return;
			case 907:
				ScriptingInterfaceOfIScene.call_SetPhotoModeOrbitDelegate = (ScriptingInterfaceOfIScene.SetPhotoModeOrbitDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetPhotoModeOrbitDelegate));
				return;
			case 908:
				ScriptingInterfaceOfIScene.call_SetPhotoModeRollDelegate = (ScriptingInterfaceOfIScene.SetPhotoModeRollDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetPhotoModeRollDelegate));
				return;
			case 909:
				ScriptingInterfaceOfIScene.call_SetPhotoModeVignetteDelegate = (ScriptingInterfaceOfIScene.SetPhotoModeVignetteDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetPhotoModeVignetteDelegate));
				return;
			case 910:
				ScriptingInterfaceOfIScene.call_SetPlaySoundEventsAfterReadyToRenderDelegate = (ScriptingInterfaceOfIScene.SetPlaySoundEventsAfterReadyToRenderDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetPlaySoundEventsAfterReadyToRenderDelegate));
				return;
			case 911:
				ScriptingInterfaceOfIScene.call_SetRainDensityDelegate = (ScriptingInterfaceOfIScene.SetRainDensityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetRainDensityDelegate));
				return;
			case 912:
				ScriptingInterfaceOfIScene.call_SetSceneColorGradeDelegate = (ScriptingInterfaceOfIScene.SetSceneColorGradeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetSceneColorGradeDelegate));
				return;
			case 913:
				ScriptingInterfaceOfIScene.call_SetSceneColorGradeIndexDelegate = (ScriptingInterfaceOfIScene.SetSceneColorGradeIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetSceneColorGradeIndexDelegate));
				return;
			case 914:
				ScriptingInterfaceOfIScene.call_SetSceneFilterIndexDelegate = (ScriptingInterfaceOfIScene.SetSceneFilterIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetSceneFilterIndexDelegate));
				return;
			case 915:
				ScriptingInterfaceOfIScene.call_SetShadowDelegate = (ScriptingInterfaceOfIScene.SetShadowDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetShadowDelegate));
				return;
			case 916:
				ScriptingInterfaceOfIScene.call_SetSkyBrightnessDelegate = (ScriptingInterfaceOfIScene.SetSkyBrightnessDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetSkyBrightnessDelegate));
				return;
			case 917:
				ScriptingInterfaceOfIScene.call_SetSkyRotationDelegate = (ScriptingInterfaceOfIScene.SetSkyRotationDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetSkyRotationDelegate));
				return;
			case 918:
				ScriptingInterfaceOfIScene.call_SetSnowDensityDelegate = (ScriptingInterfaceOfIScene.SetSnowDensityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetSnowDensityDelegate));
				return;
			case 919:
				ScriptingInterfaceOfIScene.call_SetStreakAmountDelegate = (ScriptingInterfaceOfIScene.SetStreakAmountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetStreakAmountDelegate));
				return;
			case 920:
				ScriptingInterfaceOfIScene.call_SetStreakIntensityDelegate = (ScriptingInterfaceOfIScene.SetStreakIntensityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetStreakIntensityDelegate));
				return;
			case 921:
				ScriptingInterfaceOfIScene.call_SetStreakStrengthDelegate = (ScriptingInterfaceOfIScene.SetStreakStrengthDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetStreakStrengthDelegate));
				return;
			case 922:
				ScriptingInterfaceOfIScene.call_SetStreakStretchDelegate = (ScriptingInterfaceOfIScene.SetStreakStretchDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetStreakStretchDelegate));
				return;
			case 923:
				ScriptingInterfaceOfIScene.call_SetStreakThresholdDelegate = (ScriptingInterfaceOfIScene.SetStreakThresholdDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetStreakThresholdDelegate));
				return;
			case 924:
				ScriptingInterfaceOfIScene.call_SetStreakTintDelegate = (ScriptingInterfaceOfIScene.SetStreakTintDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetStreakTintDelegate));
				return;
			case 925:
				ScriptingInterfaceOfIScene.call_SetSunDelegate = (ScriptingInterfaceOfIScene.SetSunDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetSunDelegate));
				return;
			case 926:
				ScriptingInterfaceOfIScene.call_SetSunAngleAltitudeDelegate = (ScriptingInterfaceOfIScene.SetSunAngleAltitudeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetSunAngleAltitudeDelegate));
				return;
			case 927:
				ScriptingInterfaceOfIScene.call_SetSunDirectionDelegate = (ScriptingInterfaceOfIScene.SetSunDirectionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetSunDirectionDelegate));
				return;
			case 928:
				ScriptingInterfaceOfIScene.call_SetSunLightDelegate = (ScriptingInterfaceOfIScene.SetSunLightDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetSunLightDelegate));
				return;
			case 929:
				ScriptingInterfaceOfIScene.call_SetSunshaftModeDelegate = (ScriptingInterfaceOfIScene.SetSunshaftModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetSunshaftModeDelegate));
				return;
			case 930:
				ScriptingInterfaceOfIScene.call_SetSunShaftStrengthDelegate = (ScriptingInterfaceOfIScene.SetSunShaftStrengthDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetSunShaftStrengthDelegate));
				return;
			case 931:
				ScriptingInterfaceOfIScene.call_SetSunSizeDelegate = (ScriptingInterfaceOfIScene.SetSunSizeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetSunSizeDelegate));
				return;
			case 932:
				ScriptingInterfaceOfIScene.call_SetTargetExposureDelegate = (ScriptingInterfaceOfIScene.SetTargetExposureDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetTargetExposureDelegate));
				return;
			case 933:
				ScriptingInterfaceOfIScene.call_SetTemperatureDelegate = (ScriptingInterfaceOfIScene.SetTemperatureDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetTemperatureDelegate));
				return;
			case 934:
				ScriptingInterfaceOfIScene.call_SetTerrainDynamicParamsDelegate = (ScriptingInterfaceOfIScene.SetTerrainDynamicParamsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetTerrainDynamicParamsDelegate));
				return;
			case 935:
				ScriptingInterfaceOfIScene.call_SetTimeOfDayDelegate = (ScriptingInterfaceOfIScene.SetTimeOfDayDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetTimeOfDayDelegate));
				return;
			case 936:
				ScriptingInterfaceOfIScene.call_SetTimeSpeedDelegate = (ScriptingInterfaceOfIScene.SetTimeSpeedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetTimeSpeedDelegate));
				return;
			case 937:
				ScriptingInterfaceOfIScene.call_SetUpgradeLevelDelegate = (ScriptingInterfaceOfIScene.SetUpgradeLevelDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetUpgradeLevelDelegate));
				return;
			case 938:
				ScriptingInterfaceOfIScene.call_SetUpgradeLevelVisibilityDelegate = (ScriptingInterfaceOfIScene.SetUpgradeLevelVisibilityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetUpgradeLevelVisibilityDelegate));
				return;
			case 939:
				ScriptingInterfaceOfIScene.call_SetUpgradeLevelVisibilityWithMaskDelegate = (ScriptingInterfaceOfIScene.SetUpgradeLevelVisibilityWithMaskDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetUpgradeLevelVisibilityWithMaskDelegate));
				return;
			case 940:
				ScriptingInterfaceOfIScene.call_SetUseConstantTimeDelegate = (ScriptingInterfaceOfIScene.SetUseConstantTimeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetUseConstantTimeDelegate));
				return;
			case 941:
				ScriptingInterfaceOfIScene.call_SetVignetteInnerRadiusDelegate = (ScriptingInterfaceOfIScene.SetVignetteInnerRadiusDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetVignetteInnerRadiusDelegate));
				return;
			case 942:
				ScriptingInterfaceOfIScene.call_SetVignetteOpacityDelegate = (ScriptingInterfaceOfIScene.SetVignetteOpacityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetVignetteOpacityDelegate));
				return;
			case 943:
				ScriptingInterfaceOfIScene.call_SetVignetteOuterRadiusDelegate = (ScriptingInterfaceOfIScene.SetVignetteOuterRadiusDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetVignetteOuterRadiusDelegate));
				return;
			case 944:
				ScriptingInterfaceOfIScene.call_SetWinterTimeFactorDelegate = (ScriptingInterfaceOfIScene.SetWinterTimeFactorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SetWinterTimeFactorDelegate));
				return;
			case 945:
				ScriptingInterfaceOfIScene.call_StallLoadingRenderingsUntilFurtherNoticeDelegate = (ScriptingInterfaceOfIScene.StallLoadingRenderingsUntilFurtherNoticeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.StallLoadingRenderingsUntilFurtherNoticeDelegate));
				return;
			case 946:
				ScriptingInterfaceOfIScene.call_SwapFaceConnectionsWithIdDelegate = (ScriptingInterfaceOfIScene.SwapFaceConnectionsWithIdDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.SwapFaceConnectionsWithIdDelegate));
				return;
			case 947:
				ScriptingInterfaceOfIScene.call_TakePhotoModePictureDelegate = (ScriptingInterfaceOfIScene.TakePhotoModePictureDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.TakePhotoModePictureDelegate));
				return;
			case 948:
				ScriptingInterfaceOfIScene.call_TickDelegate = (ScriptingInterfaceOfIScene.TickDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.TickDelegate));
				return;
			case 949:
				ScriptingInterfaceOfIScene.call_WorldPositionComputeNearestNavMeshDelegate = (ScriptingInterfaceOfIScene.WorldPositionComputeNearestNavMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.WorldPositionComputeNearestNavMeshDelegate));
				return;
			case 950:
				ScriptingInterfaceOfIScene.call_WorldPositionValidateZDelegate = (ScriptingInterfaceOfIScene.WorldPositionValidateZDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScene.WorldPositionValidateZDelegate));
				return;
			case 951:
				ScriptingInterfaceOfISceneView.call_AddClearTaskDelegate = (ScriptingInterfaceOfISceneView.AddClearTaskDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.AddClearTaskDelegate));
				return;
			case 952:
				ScriptingInterfaceOfISceneView.call_CheckSceneReadyToRenderDelegate = (ScriptingInterfaceOfISceneView.CheckSceneReadyToRenderDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.CheckSceneReadyToRenderDelegate));
				return;
			case 953:
				ScriptingInterfaceOfISceneView.call_ClearAllDelegate = (ScriptingInterfaceOfISceneView.ClearAllDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.ClearAllDelegate));
				return;
			case 954:
				ScriptingInterfaceOfISceneView.call_CreateSceneViewDelegate = (ScriptingInterfaceOfISceneView.CreateSceneViewDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.CreateSceneViewDelegate));
				return;
			case 955:
				ScriptingInterfaceOfISceneView.call_DoNotClearDelegate = (ScriptingInterfaceOfISceneView.DoNotClearDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.DoNotClearDelegate));
				return;
			case 956:
				ScriptingInterfaceOfISceneView.call_GetSceneDelegate = (ScriptingInterfaceOfISceneView.GetSceneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.GetSceneDelegate));
				return;
			case 957:
				ScriptingInterfaceOfISceneView.call_ProjectedMousePositionOnGroundDelegate = (ScriptingInterfaceOfISceneView.ProjectedMousePositionOnGroundDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.ProjectedMousePositionOnGroundDelegate));
				return;
			case 958:
				ScriptingInterfaceOfISceneView.call_RayCastForClosestEntityOrTerrainDelegate = (ScriptingInterfaceOfISceneView.RayCastForClosestEntityOrTerrainDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.RayCastForClosestEntityOrTerrainDelegate));
				return;
			case 959:
				ScriptingInterfaceOfISceneView.call_ReadyToRenderDelegate = (ScriptingInterfaceOfISceneView.ReadyToRenderDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.ReadyToRenderDelegate));
				return;
			case 960:
				ScriptingInterfaceOfISceneView.call_ScreenPointToViewportPointDelegate = (ScriptingInterfaceOfISceneView.ScreenPointToViewportPointDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.ScreenPointToViewportPointDelegate));
				return;
			case 961:
				ScriptingInterfaceOfISceneView.call_SetAcceptGlobalDebugRenderObjectsDelegate = (ScriptingInterfaceOfISceneView.SetAcceptGlobalDebugRenderObjectsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.SetAcceptGlobalDebugRenderObjectsDelegate));
				return;
			case 962:
				ScriptingInterfaceOfISceneView.call_SetCameraDelegate = (ScriptingInterfaceOfISceneView.SetCameraDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.SetCameraDelegate));
				return;
			case 963:
				ScriptingInterfaceOfISceneView.call_SetCleanScreenUntilLoadingDoneDelegate = (ScriptingInterfaceOfISceneView.SetCleanScreenUntilLoadingDoneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.SetCleanScreenUntilLoadingDoneDelegate));
				return;
			case 964:
				ScriptingInterfaceOfISceneView.call_SetClearAndDisableAfterSucessfullRenderDelegate = (ScriptingInterfaceOfISceneView.SetClearAndDisableAfterSucessfullRenderDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.SetClearAndDisableAfterSucessfullRenderDelegate));
				return;
			case 965:
				ScriptingInterfaceOfISceneView.call_SetClearGbufferDelegate = (ScriptingInterfaceOfISceneView.SetClearGbufferDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.SetClearGbufferDelegate));
				return;
			case 966:
				ScriptingInterfaceOfISceneView.call_SetFocusedShadowmapDelegate = (ScriptingInterfaceOfISceneView.SetFocusedShadowmapDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.SetFocusedShadowmapDelegate));
				return;
			case 967:
				ScriptingInterfaceOfISceneView.call_SetForceShaderCompilationDelegate = (ScriptingInterfaceOfISceneView.SetForceShaderCompilationDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.SetForceShaderCompilationDelegate));
				return;
			case 968:
				ScriptingInterfaceOfISceneView.call_SetPointlightResolutionMultiplierDelegate = (ScriptingInterfaceOfISceneView.SetPointlightResolutionMultiplierDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.SetPointlightResolutionMultiplierDelegate));
				return;
			case 969:
				ScriptingInterfaceOfISceneView.call_SetPostfxConfigParamsDelegate = (ScriptingInterfaceOfISceneView.SetPostfxConfigParamsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.SetPostfxConfigParamsDelegate));
				return;
			case 970:
				ScriptingInterfaceOfISceneView.call_SetPostfxFromConfigDelegate = (ScriptingInterfaceOfISceneView.SetPostfxFromConfigDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.SetPostfxFromConfigDelegate));
				return;
			case 971:
				ScriptingInterfaceOfISceneView.call_SetRenderWithPostfxDelegate = (ScriptingInterfaceOfISceneView.SetRenderWithPostfxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.SetRenderWithPostfxDelegate));
				return;
			case 972:
				ScriptingInterfaceOfISceneView.call_SetResolutionScalingDelegate = (ScriptingInterfaceOfISceneView.SetResolutionScalingDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.SetResolutionScalingDelegate));
				return;
			case 973:
				ScriptingInterfaceOfISceneView.call_SetSceneDelegate = (ScriptingInterfaceOfISceneView.SetSceneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.SetSceneDelegate));
				return;
			case 974:
				ScriptingInterfaceOfISceneView.call_SetSceneUsesContourDelegate = (ScriptingInterfaceOfISceneView.SetSceneUsesContourDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.SetSceneUsesContourDelegate));
				return;
			case 975:
				ScriptingInterfaceOfISceneView.call_SetSceneUsesShadowsDelegate = (ScriptingInterfaceOfISceneView.SetSceneUsesShadowsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.SetSceneUsesShadowsDelegate));
				return;
			case 976:
				ScriptingInterfaceOfISceneView.call_SetSceneUsesSkyboxDelegate = (ScriptingInterfaceOfISceneView.SetSceneUsesSkyboxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.SetSceneUsesSkyboxDelegate));
				return;
			case 977:
				ScriptingInterfaceOfISceneView.call_SetShadowmapResolutionMultiplierDelegate = (ScriptingInterfaceOfISceneView.SetShadowmapResolutionMultiplierDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.SetShadowmapResolutionMultiplierDelegate));
				return;
			case 978:
				ScriptingInterfaceOfISceneView.call_TranslateMouseDelegate = (ScriptingInterfaceOfISceneView.TranslateMouseDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.TranslateMouseDelegate));
				return;
			case 979:
				ScriptingInterfaceOfISceneView.call_WorldPointToScreenPointDelegate = (ScriptingInterfaceOfISceneView.WorldPointToScreenPointDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISceneView.WorldPointToScreenPointDelegate));
				return;
			case 980:
				ScriptingInterfaceOfIScreen.call_GetAspectRatioDelegate = (ScriptingInterfaceOfIScreen.GetAspectRatioDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScreen.GetAspectRatioDelegate));
				return;
			case 981:
				ScriptingInterfaceOfIScreen.call_GetDesktopHeightDelegate = (ScriptingInterfaceOfIScreen.GetDesktopHeightDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScreen.GetDesktopHeightDelegate));
				return;
			case 982:
				ScriptingInterfaceOfIScreen.call_GetDesktopWidthDelegate = (ScriptingInterfaceOfIScreen.GetDesktopWidthDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScreen.GetDesktopWidthDelegate));
				return;
			case 983:
				ScriptingInterfaceOfIScreen.call_GetMouseVisibleDelegate = (ScriptingInterfaceOfIScreen.GetMouseVisibleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScreen.GetMouseVisibleDelegate));
				return;
			case 984:
				ScriptingInterfaceOfIScreen.call_GetRealScreenResolutionHeightDelegate = (ScriptingInterfaceOfIScreen.GetRealScreenResolutionHeightDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScreen.GetRealScreenResolutionHeightDelegate));
				return;
			case 985:
				ScriptingInterfaceOfIScreen.call_GetRealScreenResolutionWidthDelegate = (ScriptingInterfaceOfIScreen.GetRealScreenResolutionWidthDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScreen.GetRealScreenResolutionWidthDelegate));
				return;
			case 986:
				ScriptingInterfaceOfIScreen.call_GetUsableAreaPercentagesDelegate = (ScriptingInterfaceOfIScreen.GetUsableAreaPercentagesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScreen.GetUsableAreaPercentagesDelegate));
				return;
			case 987:
				ScriptingInterfaceOfIScreen.call_IsEnterButtonCrossDelegate = (ScriptingInterfaceOfIScreen.IsEnterButtonCrossDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScreen.IsEnterButtonCrossDelegate));
				return;
			case 988:
				ScriptingInterfaceOfIScreen.call_SetMouseVisibleDelegate = (ScriptingInterfaceOfIScreen.SetMouseVisibleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScreen.SetMouseVisibleDelegate));
				return;
			case 989:
				ScriptingInterfaceOfIScriptComponent.call_GetScriptComponentBehaviorDelegate = (ScriptingInterfaceOfIScriptComponent.GetScriptComponentBehaviorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScriptComponent.GetScriptComponentBehaviorDelegate));
				return;
			case 990:
				ScriptingInterfaceOfIScriptComponent.call_SetVariableEditorWidgetStatusDelegate = (ScriptingInterfaceOfIScriptComponent.SetVariableEditorWidgetStatusDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIScriptComponent.SetVariableEditorWidgetStatusDelegate));
				return;
			case 991:
				ScriptingInterfaceOfIShader.call_GetFromResourceDelegate = (ScriptingInterfaceOfIShader.GetFromResourceDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIShader.GetFromResourceDelegate));
				return;
			case 992:
				ScriptingInterfaceOfIShader.call_GetMaterialShaderFlagMaskDelegate = (ScriptingInterfaceOfIShader.GetMaterialShaderFlagMaskDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIShader.GetMaterialShaderFlagMaskDelegate));
				return;
			case 993:
				ScriptingInterfaceOfIShader.call_GetNameDelegate = (ScriptingInterfaceOfIShader.GetNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIShader.GetNameDelegate));
				return;
			case 994:
				ScriptingInterfaceOfIShader.call_ReleaseDelegate = (ScriptingInterfaceOfIShader.ReleaseDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIShader.ReleaseDelegate));
				return;
			case 995:
				ScriptingInterfaceOfISkeleton.call_ActivateRagdollDelegate = (ScriptingInterfaceOfISkeleton.ActivateRagdollDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.ActivateRagdollDelegate));
				return;
			case 996:
				ScriptingInterfaceOfISkeleton.call_AddComponentDelegate = (ScriptingInterfaceOfISkeleton.AddComponentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.AddComponentDelegate));
				return;
			case 997:
				ScriptingInterfaceOfISkeleton.call_AddComponentToBoneDelegate = (ScriptingInterfaceOfISkeleton.AddComponentToBoneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.AddComponentToBoneDelegate));
				return;
			case 998:
				ScriptingInterfaceOfISkeleton.call_AddMeshDelegate = (ScriptingInterfaceOfISkeleton.AddMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.AddMeshDelegate));
				return;
			case 999:
				ScriptingInterfaceOfISkeleton.call_AddMeshToBoneDelegate = (ScriptingInterfaceOfISkeleton.AddMeshToBoneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.AddMeshToBoneDelegate));
				return;
			case 1000:
				ScriptingInterfaceOfISkeleton.call_AddPrefabEntityToBoneDelegate = (ScriptingInterfaceOfISkeleton.AddPrefabEntityToBoneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.AddPrefabEntityToBoneDelegate));
				return;
			case 1001:
				ScriptingInterfaceOfISkeleton.call_ClearComponentsDelegate = (ScriptingInterfaceOfISkeleton.ClearComponentsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.ClearComponentsDelegate));
				return;
			case 1002:
				ScriptingInterfaceOfISkeleton.call_ClearMeshesDelegate = (ScriptingInterfaceOfISkeleton.ClearMeshesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.ClearMeshesDelegate));
				return;
			case 1003:
				ScriptingInterfaceOfISkeleton.call_ClearMeshesAtBoneDelegate = (ScriptingInterfaceOfISkeleton.ClearMeshesAtBoneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.ClearMeshesAtBoneDelegate));
				return;
			case 1004:
				ScriptingInterfaceOfISkeleton.call_CreateFromModelDelegate = (ScriptingInterfaceOfISkeleton.CreateFromModelDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.CreateFromModelDelegate));
				return;
			case 1005:
				ScriptingInterfaceOfISkeleton.call_CreateFromModelWithNullAnimTreeDelegate = (ScriptingInterfaceOfISkeleton.CreateFromModelWithNullAnimTreeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.CreateFromModelWithNullAnimTreeDelegate));
				return;
			case 1006:
				ScriptingInterfaceOfISkeleton.call_ForceUpdateBoneFramesDelegate = (ScriptingInterfaceOfISkeleton.ForceUpdateBoneFramesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.ForceUpdateBoneFramesDelegate));
				return;
			case 1007:
				ScriptingInterfaceOfISkeleton.call_FreezeDelegate = (ScriptingInterfaceOfISkeleton.FreezeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.FreezeDelegate));
				return;
			case 1008:
				ScriptingInterfaceOfISkeleton.call_GetAllMeshesDelegate = (ScriptingInterfaceOfISkeleton.GetAllMeshesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetAllMeshesDelegate));
				return;
			case 1009:
				ScriptingInterfaceOfISkeleton.call_GetAnimationAtChannelDelegate = (ScriptingInterfaceOfISkeleton.GetAnimationAtChannelDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetAnimationAtChannelDelegate));
				return;
			case 1010:
				ScriptingInterfaceOfISkeleton.call_GetAnimationIndexAtChannelDelegate = (ScriptingInterfaceOfISkeleton.GetAnimationIndexAtChannelDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetAnimationIndexAtChannelDelegate));
				return;
			case 1011:
				ScriptingInterfaceOfISkeleton.call_GetBoneBodyDelegate = (ScriptingInterfaceOfISkeleton.GetBoneBodyDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetBoneBodyDelegate));
				return;
			case 1012:
				ScriptingInterfaceOfISkeleton.call_GetBoneChildAtIndexDelegate = (ScriptingInterfaceOfISkeleton.GetBoneChildAtIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetBoneChildAtIndexDelegate));
				return;
			case 1013:
				ScriptingInterfaceOfISkeleton.call_GetBoneChildCountDelegate = (ScriptingInterfaceOfISkeleton.GetBoneChildCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetBoneChildCountDelegate));
				return;
			case 1014:
				ScriptingInterfaceOfISkeleton.call_GetBoneComponentAtIndexDelegate = (ScriptingInterfaceOfISkeleton.GetBoneComponentAtIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetBoneComponentAtIndexDelegate));
				return;
			case 1015:
				ScriptingInterfaceOfISkeleton.call_GetBoneComponentCountDelegate = (ScriptingInterfaceOfISkeleton.GetBoneComponentCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetBoneComponentCountDelegate));
				return;
			case 1016:
				ScriptingInterfaceOfISkeleton.call_GetBoneCountDelegate = (ScriptingInterfaceOfISkeleton.GetBoneCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetBoneCountDelegate));
				return;
			case 1017:
				ScriptingInterfaceOfISkeleton.call_GetBoneEntitialFrameDelegate = (ScriptingInterfaceOfISkeleton.GetBoneEntitialFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetBoneEntitialFrameDelegate));
				return;
			case 1018:
				ScriptingInterfaceOfISkeleton.call_GetBoneEntitialFrameAtChannelDelegate = (ScriptingInterfaceOfISkeleton.GetBoneEntitialFrameAtChannelDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetBoneEntitialFrameAtChannelDelegate));
				return;
			case 1019:
				ScriptingInterfaceOfISkeleton.call_GetBoneEntitialFrameWithIndexDelegate = (ScriptingInterfaceOfISkeleton.GetBoneEntitialFrameWithIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetBoneEntitialFrameWithIndexDelegate));
				return;
			case 1020:
				ScriptingInterfaceOfISkeleton.call_GetBoneEntitialFrameWithNameDelegate = (ScriptingInterfaceOfISkeleton.GetBoneEntitialFrameWithNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetBoneEntitialFrameWithNameDelegate));
				return;
			case 1021:
				ScriptingInterfaceOfISkeleton.call_GetBoneEntitialRestFrameDelegate = (ScriptingInterfaceOfISkeleton.GetBoneEntitialRestFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetBoneEntitialRestFrameDelegate));
				return;
			case 1022:
				ScriptingInterfaceOfISkeleton.call_GetBoneIndexFromNameDelegate = (ScriptingInterfaceOfISkeleton.GetBoneIndexFromNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetBoneIndexFromNameDelegate));
				return;
			case 1023:
				ScriptingInterfaceOfISkeleton.call_GetBoneLocalRestFrameDelegate = (ScriptingInterfaceOfISkeleton.GetBoneLocalRestFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetBoneLocalRestFrameDelegate));
				return;
			case 1024:
				ScriptingInterfaceOfISkeleton.call_GetBoneNameDelegate = (ScriptingInterfaceOfISkeleton.GetBoneNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetBoneNameDelegate));
				return;
			case 1025:
				ScriptingInterfaceOfISkeleton.call_GetComponentAtIndexDelegate = (ScriptingInterfaceOfISkeleton.GetComponentAtIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetComponentAtIndexDelegate));
				return;
			case 1026:
				ScriptingInterfaceOfISkeleton.call_GetComponentCountDelegate = (ScriptingInterfaceOfISkeleton.GetComponentCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetComponentCountDelegate));
				return;
			case 1027:
				ScriptingInterfaceOfISkeleton.call_GetCurrentRagdollStateDelegate = (ScriptingInterfaceOfISkeleton.GetCurrentRagdollStateDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetCurrentRagdollStateDelegate));
				return;
			case 1028:
				ScriptingInterfaceOfISkeleton.call_GetNameDelegate = (ScriptingInterfaceOfISkeleton.GetNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetNameDelegate));
				return;
			case 1029:
				ScriptingInterfaceOfISkeleton.call_GetParentBoneIndexDelegate = (ScriptingInterfaceOfISkeleton.GetParentBoneIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetParentBoneIndexDelegate));
				return;
			case 1030:
				ScriptingInterfaceOfISkeleton.call_GetSkeletonAnimationParameterAtChannelDelegate = (ScriptingInterfaceOfISkeleton.GetSkeletonAnimationParameterAtChannelDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetSkeletonAnimationParameterAtChannelDelegate));
				return;
			case 1031:
				ScriptingInterfaceOfISkeleton.call_GetSkeletonAnimationSpeedAtChannelDelegate = (ScriptingInterfaceOfISkeleton.GetSkeletonAnimationSpeedAtChannelDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetSkeletonAnimationSpeedAtChannelDelegate));
				return;
			case 1032:
				ScriptingInterfaceOfISkeleton.call_GetSkeletonBoneMappingDelegate = (ScriptingInterfaceOfISkeleton.GetSkeletonBoneMappingDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.GetSkeletonBoneMappingDelegate));
				return;
			case 1033:
				ScriptingInterfaceOfISkeleton.call_HasBoneComponentDelegate = (ScriptingInterfaceOfISkeleton.HasBoneComponentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.HasBoneComponentDelegate));
				return;
			case 1034:
				ScriptingInterfaceOfISkeleton.call_HasComponentDelegate = (ScriptingInterfaceOfISkeleton.HasComponentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.HasComponentDelegate));
				return;
			case 1035:
				ScriptingInterfaceOfISkeleton.call_IsFrozenDelegate = (ScriptingInterfaceOfISkeleton.IsFrozenDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.IsFrozenDelegate));
				return;
			case 1036:
				ScriptingInterfaceOfISkeleton.call_RemoveBoneComponentDelegate = (ScriptingInterfaceOfISkeleton.RemoveBoneComponentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.RemoveBoneComponentDelegate));
				return;
			case 1037:
				ScriptingInterfaceOfISkeleton.call_RemoveComponentDelegate = (ScriptingInterfaceOfISkeleton.RemoveComponentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.RemoveComponentDelegate));
				return;
			case 1038:
				ScriptingInterfaceOfISkeleton.call_ResetClothsDelegate = (ScriptingInterfaceOfISkeleton.ResetClothsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.ResetClothsDelegate));
				return;
			case 1039:
				ScriptingInterfaceOfISkeleton.call_ResetFramesDelegate = (ScriptingInterfaceOfISkeleton.ResetFramesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.ResetFramesDelegate));
				return;
			case 1040:
				ScriptingInterfaceOfISkeleton.call_SetBoneLocalFrameDelegate = (ScriptingInterfaceOfISkeleton.SetBoneLocalFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.SetBoneLocalFrameDelegate));
				return;
			case 1041:
				ScriptingInterfaceOfISkeleton.call_SetSkeletonAnimationParameterAtChannelDelegate = (ScriptingInterfaceOfISkeleton.SetSkeletonAnimationParameterAtChannelDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.SetSkeletonAnimationParameterAtChannelDelegate));
				return;
			case 1042:
				ScriptingInterfaceOfISkeleton.call_SetSkeletonAnimationSpeedAtChannelDelegate = (ScriptingInterfaceOfISkeleton.SetSkeletonAnimationSpeedAtChannelDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.SetSkeletonAnimationSpeedAtChannelDelegate));
				return;
			case 1043:
				ScriptingInterfaceOfISkeleton.call_SetSkeletonUptoDateDelegate = (ScriptingInterfaceOfISkeleton.SetSkeletonUptoDateDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.SetSkeletonUptoDateDelegate));
				return;
			case 1044:
				ScriptingInterfaceOfISkeleton.call_SetUsePreciseBoundingVolumeDelegate = (ScriptingInterfaceOfISkeleton.SetUsePreciseBoundingVolumeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.SetUsePreciseBoundingVolumeDelegate));
				return;
			case 1045:
				ScriptingInterfaceOfISkeleton.call_SkeletonModelExistDelegate = (ScriptingInterfaceOfISkeleton.SkeletonModelExistDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.SkeletonModelExistDelegate));
				return;
			case 1046:
				ScriptingInterfaceOfISkeleton.call_TickAnimationsDelegate = (ScriptingInterfaceOfISkeleton.TickAnimationsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.TickAnimationsDelegate));
				return;
			case 1047:
				ScriptingInterfaceOfISkeleton.call_TickAnimationsAndForceUpdateDelegate = (ScriptingInterfaceOfISkeleton.TickAnimationsAndForceUpdateDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.TickAnimationsAndForceUpdateDelegate));
				return;
			case 1048:
				ScriptingInterfaceOfISkeleton.call_UpdateEntitialFramesFromLocalFramesDelegate = (ScriptingInterfaceOfISkeleton.UpdateEntitialFramesFromLocalFramesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISkeleton.UpdateEntitialFramesFromLocalFramesDelegate));
				return;
			case 1049:
				ScriptingInterfaceOfISoundEvent.call_CreateEventDelegate = (ScriptingInterfaceOfISoundEvent.CreateEventDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.CreateEventDelegate));
				return;
			case 1050:
				ScriptingInterfaceOfISoundEvent.call_CreateEventFromExternalFileDelegate = (ScriptingInterfaceOfISoundEvent.CreateEventFromExternalFileDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.CreateEventFromExternalFileDelegate));
				return;
			case 1051:
				ScriptingInterfaceOfISoundEvent.call_CreateEventFromSoundBufferDelegate = (ScriptingInterfaceOfISoundEvent.CreateEventFromSoundBufferDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.CreateEventFromSoundBufferDelegate));
				return;
			case 1052:
				ScriptingInterfaceOfISoundEvent.call_CreateEventFromStringDelegate = (ScriptingInterfaceOfISoundEvent.CreateEventFromStringDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.CreateEventFromStringDelegate));
				return;
			case 1053:
				ScriptingInterfaceOfISoundEvent.call_GetEventIdFromStringDelegate = (ScriptingInterfaceOfISoundEvent.GetEventIdFromStringDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.GetEventIdFromStringDelegate));
				return;
			case 1054:
				ScriptingInterfaceOfISoundEvent.call_GetEventMinMaxDistanceDelegate = (ScriptingInterfaceOfISoundEvent.GetEventMinMaxDistanceDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.GetEventMinMaxDistanceDelegate));
				return;
			case 1055:
				ScriptingInterfaceOfISoundEvent.call_GetTotalEventCountDelegate = (ScriptingInterfaceOfISoundEvent.GetTotalEventCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.GetTotalEventCountDelegate));
				return;
			case 1056:
				ScriptingInterfaceOfISoundEvent.call_IsPausedDelegate = (ScriptingInterfaceOfISoundEvent.IsPausedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.IsPausedDelegate));
				return;
			case 1057:
				ScriptingInterfaceOfISoundEvent.call_IsPlayingDelegate = (ScriptingInterfaceOfISoundEvent.IsPlayingDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.IsPlayingDelegate));
				return;
			case 1058:
				ScriptingInterfaceOfISoundEvent.call_IsValidDelegate = (ScriptingInterfaceOfISoundEvent.IsValidDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.IsValidDelegate));
				return;
			case 1059:
				ScriptingInterfaceOfISoundEvent.call_PauseEventDelegate = (ScriptingInterfaceOfISoundEvent.PauseEventDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.PauseEventDelegate));
				return;
			case 1060:
				ScriptingInterfaceOfISoundEvent.call_PlayExtraEventDelegate = (ScriptingInterfaceOfISoundEvent.PlayExtraEventDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.PlayExtraEventDelegate));
				return;
			case 1061:
				ScriptingInterfaceOfISoundEvent.call_PlaySound2DDelegate = (ScriptingInterfaceOfISoundEvent.PlaySound2DDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.PlaySound2DDelegate));
				return;
			case 1062:
				ScriptingInterfaceOfISoundEvent.call_ReleaseEventDelegate = (ScriptingInterfaceOfISoundEvent.ReleaseEventDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.ReleaseEventDelegate));
				return;
			case 1063:
				ScriptingInterfaceOfISoundEvent.call_ResumeEventDelegate = (ScriptingInterfaceOfISoundEvent.ResumeEventDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.ResumeEventDelegate));
				return;
			case 1064:
				ScriptingInterfaceOfISoundEvent.call_SetEventMinMaxDistanceDelegate = (ScriptingInterfaceOfISoundEvent.SetEventMinMaxDistanceDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.SetEventMinMaxDistanceDelegate));
				return;
			case 1065:
				ScriptingInterfaceOfISoundEvent.call_SetEventParameterAtIndexDelegate = (ScriptingInterfaceOfISoundEvent.SetEventParameterAtIndexDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.SetEventParameterAtIndexDelegate));
				return;
			case 1066:
				ScriptingInterfaceOfISoundEvent.call_SetEventParameterFromStringDelegate = (ScriptingInterfaceOfISoundEvent.SetEventParameterFromStringDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.SetEventParameterFromStringDelegate));
				return;
			case 1067:
				ScriptingInterfaceOfISoundEvent.call_SetEventPositionDelegate = (ScriptingInterfaceOfISoundEvent.SetEventPositionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.SetEventPositionDelegate));
				return;
			case 1068:
				ScriptingInterfaceOfISoundEvent.call_SetEventVelocityDelegate = (ScriptingInterfaceOfISoundEvent.SetEventVelocityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.SetEventVelocityDelegate));
				return;
			case 1069:
				ScriptingInterfaceOfISoundEvent.call_SetSwitchDelegate = (ScriptingInterfaceOfISoundEvent.SetSwitchDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.SetSwitchDelegate));
				return;
			case 1070:
				ScriptingInterfaceOfISoundEvent.call_StartEventDelegate = (ScriptingInterfaceOfISoundEvent.StartEventDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.StartEventDelegate));
				return;
			case 1071:
				ScriptingInterfaceOfISoundEvent.call_StartEventInPositionDelegate = (ScriptingInterfaceOfISoundEvent.StartEventInPositionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.StartEventInPositionDelegate));
				return;
			case 1072:
				ScriptingInterfaceOfISoundEvent.call_StopEventDelegate = (ScriptingInterfaceOfISoundEvent.StopEventDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.StopEventDelegate));
				return;
			case 1073:
				ScriptingInterfaceOfISoundEvent.call_TriggerCueDelegate = (ScriptingInterfaceOfISoundEvent.TriggerCueDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundEvent.TriggerCueDelegate));
				return;
			case 1074:
				ScriptingInterfaceOfISoundManager.call_AddSoundClientWithIdDelegate = (ScriptingInterfaceOfISoundManager.AddSoundClientWithIdDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.AddSoundClientWithIdDelegate));
				return;
			case 1075:
				ScriptingInterfaceOfISoundManager.call_AddXBOXRemoteUserDelegate = (ScriptingInterfaceOfISoundManager.AddXBOXRemoteUserDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.AddXBOXRemoteUserDelegate));
				return;
			case 1076:
				ScriptingInterfaceOfISoundManager.call_ApplyPushToTalkDelegate = (ScriptingInterfaceOfISoundManager.ApplyPushToTalkDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.ApplyPushToTalkDelegate));
				return;
			case 1077:
				ScriptingInterfaceOfISoundManager.call_ClearDataToBeSentDelegate = (ScriptingInterfaceOfISoundManager.ClearDataToBeSentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.ClearDataToBeSentDelegate));
				return;
			case 1078:
				ScriptingInterfaceOfISoundManager.call_ClearXBOXSoundManagerDelegate = (ScriptingInterfaceOfISoundManager.ClearXBOXSoundManagerDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.ClearXBOXSoundManagerDelegate));
				return;
			case 1079:
				ScriptingInterfaceOfISoundManager.call_CompressDataDelegate = (ScriptingInterfaceOfISoundManager.CompressDataDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.CompressDataDelegate));
				return;
			case 1080:
				ScriptingInterfaceOfISoundManager.call_CreateVoiceEventDelegate = (ScriptingInterfaceOfISoundManager.CreateVoiceEventDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.CreateVoiceEventDelegate));
				return;
			case 1081:
				ScriptingInterfaceOfISoundManager.call_DecompressDataDelegate = (ScriptingInterfaceOfISoundManager.DecompressDataDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.DecompressDataDelegate));
				return;
			case 1082:
				ScriptingInterfaceOfISoundManager.call_DeleteSoundClientWithIdDelegate = (ScriptingInterfaceOfISoundManager.DeleteSoundClientWithIdDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.DeleteSoundClientWithIdDelegate));
				return;
			case 1083:
				ScriptingInterfaceOfISoundManager.call_DestroyVoiceEventDelegate = (ScriptingInterfaceOfISoundManager.DestroyVoiceEventDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.DestroyVoiceEventDelegate));
				return;
			case 1084:
				ScriptingInterfaceOfISoundManager.call_FinalizeVoicePlayEventDelegate = (ScriptingInterfaceOfISoundManager.FinalizeVoicePlayEventDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.FinalizeVoicePlayEventDelegate));
				return;
			case 1085:
				ScriptingInterfaceOfISoundManager.call_GetAttenuationPositionDelegate = (ScriptingInterfaceOfISoundManager.GetAttenuationPositionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.GetAttenuationPositionDelegate));
				return;
			case 1086:
				ScriptingInterfaceOfISoundManager.call_GetDataToBeSentAtDelegate = (ScriptingInterfaceOfISoundManager.GetDataToBeSentAtDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.GetDataToBeSentAtDelegate));
				return;
			case 1087:
				ScriptingInterfaceOfISoundManager.call_GetGlobalIndexOfEventDelegate = (ScriptingInterfaceOfISoundManager.GetGlobalIndexOfEventDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.GetGlobalIndexOfEventDelegate));
				return;
			case 1088:
				ScriptingInterfaceOfISoundManager.call_GetListenerFrameDelegate = (ScriptingInterfaceOfISoundManager.GetListenerFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.GetListenerFrameDelegate));
				return;
			case 1089:
				ScriptingInterfaceOfISoundManager.call_GetSizeOfDataToBeSentAtDelegate = (ScriptingInterfaceOfISoundManager.GetSizeOfDataToBeSentAtDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.GetSizeOfDataToBeSentAtDelegate));
				return;
			case 1090:
				ScriptingInterfaceOfISoundManager.call_GetVoiceDataDelegate = (ScriptingInterfaceOfISoundManager.GetVoiceDataDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.GetVoiceDataDelegate));
				return;
			case 1091:
				ScriptingInterfaceOfISoundManager.call_HandleStateChangesDelegate = (ScriptingInterfaceOfISoundManager.HandleStateChangesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.HandleStateChangesDelegate));
				return;
			case 1092:
				ScriptingInterfaceOfISoundManager.call_InitializeVoicePlayEventDelegate = (ScriptingInterfaceOfISoundManager.InitializeVoicePlayEventDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.InitializeVoicePlayEventDelegate));
				return;
			case 1093:
				ScriptingInterfaceOfISoundManager.call_InitializeXBOXSoundManagerDelegate = (ScriptingInterfaceOfISoundManager.InitializeXBOXSoundManagerDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.InitializeXBOXSoundManagerDelegate));
				return;
			case 1094:
				ScriptingInterfaceOfISoundManager.call_LoadEventFileAuxDelegate = (ScriptingInterfaceOfISoundManager.LoadEventFileAuxDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.LoadEventFileAuxDelegate));
				return;
			case 1095:
				ScriptingInterfaceOfISoundManager.call_ProcessDataToBeReceivedDelegate = (ScriptingInterfaceOfISoundManager.ProcessDataToBeReceivedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.ProcessDataToBeReceivedDelegate));
				return;
			case 1096:
				ScriptingInterfaceOfISoundManager.call_ProcessDataToBeSentDelegate = (ScriptingInterfaceOfISoundManager.ProcessDataToBeSentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.ProcessDataToBeSentDelegate));
				return;
			case 1097:
				ScriptingInterfaceOfISoundManager.call_RemoveXBOXRemoteUserDelegate = (ScriptingInterfaceOfISoundManager.RemoveXBOXRemoteUserDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.RemoveXBOXRemoteUserDelegate));
				return;
			case 1098:
				ScriptingInterfaceOfISoundManager.call_ResetDelegate = (ScriptingInterfaceOfISoundManager.ResetDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.ResetDelegate));
				return;
			case 1099:
				ScriptingInterfaceOfISoundManager.call_SetGlobalParameterDelegate = (ScriptingInterfaceOfISoundManager.SetGlobalParameterDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.SetGlobalParameterDelegate));
				return;
			case 1100:
				ScriptingInterfaceOfISoundManager.call_SetListenerFrameDelegate = (ScriptingInterfaceOfISoundManager.SetListenerFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.SetListenerFrameDelegate));
				return;
			case 1101:
				ScriptingInterfaceOfISoundManager.call_SetStateDelegate = (ScriptingInterfaceOfISoundManager.SetStateDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.SetStateDelegate));
				return;
			case 1102:
				ScriptingInterfaceOfISoundManager.call_StartOneShotEventDelegate = (ScriptingInterfaceOfISoundManager.StartOneShotEventDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.StartOneShotEventDelegate));
				return;
			case 1103:
				ScriptingInterfaceOfISoundManager.call_StartOneShotEventWithParamDelegate = (ScriptingInterfaceOfISoundManager.StartOneShotEventWithParamDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.StartOneShotEventWithParamDelegate));
				return;
			case 1104:
				ScriptingInterfaceOfISoundManager.call_StartVoiceRecordDelegate = (ScriptingInterfaceOfISoundManager.StartVoiceRecordDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.StartVoiceRecordDelegate));
				return;
			case 1105:
				ScriptingInterfaceOfISoundManager.call_StopVoiceRecordDelegate = (ScriptingInterfaceOfISoundManager.StopVoiceRecordDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.StopVoiceRecordDelegate));
				return;
			case 1106:
				ScriptingInterfaceOfISoundManager.call_UpdateVoiceToPlayDelegate = (ScriptingInterfaceOfISoundManager.UpdateVoiceToPlayDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.UpdateVoiceToPlayDelegate));
				return;
			case 1107:
				ScriptingInterfaceOfISoundManager.call_UpdateXBOXChatCommunicationFlagsDelegate = (ScriptingInterfaceOfISoundManager.UpdateXBOXChatCommunicationFlagsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.UpdateXBOXChatCommunicationFlagsDelegate));
				return;
			case 1108:
				ScriptingInterfaceOfISoundManager.call_UpdateXBOXLocalUserDelegate = (ScriptingInterfaceOfISoundManager.UpdateXBOXLocalUserDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfISoundManager.UpdateXBOXLocalUserDelegate));
				return;
			case 1109:
				ScriptingInterfaceOfITableauView.call_CreateTableauViewDelegate = (ScriptingInterfaceOfITableauView.CreateTableauViewDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITableauView.CreateTableauViewDelegate));
				return;
			case 1110:
				ScriptingInterfaceOfITableauView.call_SetContinousRenderingDelegate = (ScriptingInterfaceOfITableauView.SetContinousRenderingDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITableauView.SetContinousRenderingDelegate));
				return;
			case 1111:
				ScriptingInterfaceOfITableauView.call_SetDeleteAfterRenderingDelegate = (ScriptingInterfaceOfITableauView.SetDeleteAfterRenderingDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITableauView.SetDeleteAfterRenderingDelegate));
				return;
			case 1112:
				ScriptingInterfaceOfITableauView.call_SetDoNotRenderThisFrameDelegate = (ScriptingInterfaceOfITableauView.SetDoNotRenderThisFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITableauView.SetDoNotRenderThisFrameDelegate));
				return;
			case 1113:
				ScriptingInterfaceOfITableauView.call_SetSortingEnabledDelegate = (ScriptingInterfaceOfITableauView.SetSortingEnabledDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITableauView.SetSortingEnabledDelegate));
				return;
			case 1114:
				ScriptingInterfaceOfITexture.call_CheckAndGetFromResourceDelegate = (ScriptingInterfaceOfITexture.CheckAndGetFromResourceDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.CheckAndGetFromResourceDelegate));
				return;
			case 1115:
				ScriptingInterfaceOfITexture.call_CreateDepthTargetDelegate = (ScriptingInterfaceOfITexture.CreateDepthTargetDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.CreateDepthTargetDelegate));
				return;
			case 1116:
				ScriptingInterfaceOfITexture.call_CreateFromByteArrayDelegate = (ScriptingInterfaceOfITexture.CreateFromByteArrayDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.CreateFromByteArrayDelegate));
				return;
			case 1117:
				ScriptingInterfaceOfITexture.call_CreateFromMemoryDelegate = (ScriptingInterfaceOfITexture.CreateFromMemoryDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.CreateFromMemoryDelegate));
				return;
			case 1118:
				ScriptingInterfaceOfITexture.call_CreateRenderTargetDelegate = (ScriptingInterfaceOfITexture.CreateRenderTargetDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.CreateRenderTargetDelegate));
				return;
			case 1119:
				ScriptingInterfaceOfITexture.call_CreateTextureFromPathDelegate = (ScriptingInterfaceOfITexture.CreateTextureFromPathDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.CreateTextureFromPathDelegate));
				return;
			case 1120:
				ScriptingInterfaceOfITexture.call_GetCurObjectDelegate = (ScriptingInterfaceOfITexture.GetCurObjectDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.GetCurObjectDelegate));
				return;
			case 1121:
				ScriptingInterfaceOfITexture.call_GetFromResourceDelegate = (ScriptingInterfaceOfITexture.GetFromResourceDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.GetFromResourceDelegate));
				return;
			case 1122:
				ScriptingInterfaceOfITexture.call_GetHeightDelegate = (ScriptingInterfaceOfITexture.GetHeightDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.GetHeightDelegate));
				return;
			case 1123:
				ScriptingInterfaceOfITexture.call_GetMemorySizeDelegate = (ScriptingInterfaceOfITexture.GetMemorySizeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.GetMemorySizeDelegate));
				return;
			case 1124:
				ScriptingInterfaceOfITexture.call_GetNameDelegate = (ScriptingInterfaceOfITexture.GetNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.GetNameDelegate));
				return;
			case 1125:
				ScriptingInterfaceOfITexture.call_GetRenderTargetComponentDelegate = (ScriptingInterfaceOfITexture.GetRenderTargetComponentDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.GetRenderTargetComponentDelegate));
				return;
			case 1126:
				ScriptingInterfaceOfITexture.call_GetTableauViewDelegate = (ScriptingInterfaceOfITexture.GetTableauViewDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.GetTableauViewDelegate));
				return;
			case 1127:
				ScriptingInterfaceOfITexture.call_GetWidthDelegate = (ScriptingInterfaceOfITexture.GetWidthDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.GetWidthDelegate));
				return;
			case 1128:
				ScriptingInterfaceOfITexture.call_IsLoadedDelegate = (ScriptingInterfaceOfITexture.IsLoadedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.IsLoadedDelegate));
				return;
			case 1129:
				ScriptingInterfaceOfITexture.call_IsRenderTargetDelegate = (ScriptingInterfaceOfITexture.IsRenderTargetDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.IsRenderTargetDelegate));
				return;
			case 1130:
				ScriptingInterfaceOfITexture.call_LoadTextureFromPathDelegate = (ScriptingInterfaceOfITexture.LoadTextureFromPathDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.LoadTextureFromPathDelegate));
				return;
			case 1131:
				ScriptingInterfaceOfITexture.call_ReleaseDelegate = (ScriptingInterfaceOfITexture.ReleaseDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.ReleaseDelegate));
				return;
			case 1132:
				ScriptingInterfaceOfITexture.call_ReleaseGpuMemoriesDelegate = (ScriptingInterfaceOfITexture.ReleaseGpuMemoriesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.ReleaseGpuMemoriesDelegate));
				return;
			case 1133:
				ScriptingInterfaceOfITexture.call_ReleaseNextFrameDelegate = (ScriptingInterfaceOfITexture.ReleaseNextFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.ReleaseNextFrameDelegate));
				return;
			case 1134:
				ScriptingInterfaceOfITexture.call_RemoveContinousTableauTextureDelegate = (ScriptingInterfaceOfITexture.RemoveContinousTableauTextureDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.RemoveContinousTableauTextureDelegate));
				return;
			case 1135:
				ScriptingInterfaceOfITexture.call_SaveTextureAsAlwaysValidDelegate = (ScriptingInterfaceOfITexture.SaveTextureAsAlwaysValidDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.SaveTextureAsAlwaysValidDelegate));
				return;
			case 1136:
				ScriptingInterfaceOfITexture.call_SaveToFileDelegate = (ScriptingInterfaceOfITexture.SaveToFileDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.SaveToFileDelegate));
				return;
			case 1137:
				ScriptingInterfaceOfITexture.call_SetNameDelegate = (ScriptingInterfaceOfITexture.SetNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.SetNameDelegate));
				return;
			case 1138:
				ScriptingInterfaceOfITexture.call_SetTableauViewDelegate = (ScriptingInterfaceOfITexture.SetTableauViewDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.SetTableauViewDelegate));
				return;
			case 1139:
				ScriptingInterfaceOfITexture.call_TransformRenderTargetToResourceTextureDelegate = (ScriptingInterfaceOfITexture.TransformRenderTargetToResourceTextureDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITexture.TransformRenderTargetToResourceTextureDelegate));
				return;
			case 1140:
				ScriptingInterfaceOfITextureView.call_CreateTextureViewDelegate = (ScriptingInterfaceOfITextureView.CreateTextureViewDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITextureView.CreateTextureViewDelegate));
				return;
			case 1141:
				ScriptingInterfaceOfITextureView.call_SetTextureDelegate = (ScriptingInterfaceOfITextureView.SetTextureDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITextureView.SetTextureDelegate));
				return;
			case 1142:
				ScriptingInterfaceOfIThumbnailCreatorView.call_CancelRequestDelegate = (ScriptingInterfaceOfIThumbnailCreatorView.CancelRequestDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIThumbnailCreatorView.CancelRequestDelegate));
				return;
			case 1143:
				ScriptingInterfaceOfIThumbnailCreatorView.call_ClearRequestsDelegate = (ScriptingInterfaceOfIThumbnailCreatorView.ClearRequestsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIThumbnailCreatorView.ClearRequestsDelegate));
				return;
			case 1144:
				ScriptingInterfaceOfIThumbnailCreatorView.call_CreateThumbnailCreatorViewDelegate = (ScriptingInterfaceOfIThumbnailCreatorView.CreateThumbnailCreatorViewDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIThumbnailCreatorView.CreateThumbnailCreatorViewDelegate));
				return;
			case 1145:
				ScriptingInterfaceOfIThumbnailCreatorView.call_GetNumberOfPendingRequestsDelegate = (ScriptingInterfaceOfIThumbnailCreatorView.GetNumberOfPendingRequestsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIThumbnailCreatorView.GetNumberOfPendingRequestsDelegate));
				return;
			case 1146:
				ScriptingInterfaceOfIThumbnailCreatorView.call_IsMemoryClearedDelegate = (ScriptingInterfaceOfIThumbnailCreatorView.IsMemoryClearedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIThumbnailCreatorView.IsMemoryClearedDelegate));
				return;
			case 1147:
				ScriptingInterfaceOfIThumbnailCreatorView.call_RegisterEntityDelegate = (ScriptingInterfaceOfIThumbnailCreatorView.RegisterEntityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIThumbnailCreatorView.RegisterEntityDelegate));
				return;
			case 1148:
				ScriptingInterfaceOfIThumbnailCreatorView.call_RegisterEntityWithoutTextureDelegate = (ScriptingInterfaceOfIThumbnailCreatorView.RegisterEntityWithoutTextureDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIThumbnailCreatorView.RegisterEntityWithoutTextureDelegate));
				return;
			case 1149:
				ScriptingInterfaceOfIThumbnailCreatorView.call_RegisterSceneDelegate = (ScriptingInterfaceOfIThumbnailCreatorView.RegisterSceneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIThumbnailCreatorView.RegisterSceneDelegate));
				return;
			case 1150:
				ScriptingInterfaceOfITime.call_GetApplicationTimeDelegate = (ScriptingInterfaceOfITime.GetApplicationTimeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITime.GetApplicationTimeDelegate));
				return;
			case 1151:
				ScriptingInterfaceOfITwoDimensionView.call_AddCachedTextMeshDelegate = (ScriptingInterfaceOfITwoDimensionView.AddCachedTextMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITwoDimensionView.AddCachedTextMeshDelegate));
				return;
			case 1152:
				ScriptingInterfaceOfITwoDimensionView.call_AddNewMeshDelegate = (ScriptingInterfaceOfITwoDimensionView.AddNewMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITwoDimensionView.AddNewMeshDelegate));
				return;
			case 1153:
				ScriptingInterfaceOfITwoDimensionView.call_AddNewQuadMeshDelegate = (ScriptingInterfaceOfITwoDimensionView.AddNewQuadMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITwoDimensionView.AddNewQuadMeshDelegate));
				return;
			case 1154:
				ScriptingInterfaceOfITwoDimensionView.call_AddNewTextMeshDelegate = (ScriptingInterfaceOfITwoDimensionView.AddNewTextMeshDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITwoDimensionView.AddNewTextMeshDelegate));
				return;
			case 1155:
				ScriptingInterfaceOfITwoDimensionView.call_BeginFrameDelegate = (ScriptingInterfaceOfITwoDimensionView.BeginFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITwoDimensionView.BeginFrameDelegate));
				return;
			case 1156:
				ScriptingInterfaceOfITwoDimensionView.call_ClearDelegate = (ScriptingInterfaceOfITwoDimensionView.ClearDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITwoDimensionView.ClearDelegate));
				return;
			case 1157:
				ScriptingInterfaceOfITwoDimensionView.call_CreateTwoDimensionViewDelegate = (ScriptingInterfaceOfITwoDimensionView.CreateTwoDimensionViewDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITwoDimensionView.CreateTwoDimensionViewDelegate));
				return;
			case 1158:
				ScriptingInterfaceOfITwoDimensionView.call_EndFrameDelegate = (ScriptingInterfaceOfITwoDimensionView.EndFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfITwoDimensionView.EndFrameDelegate));
				return;
			case 1159:
				ScriptingInterfaceOfIUtil.call_AddCommandLineFunctionDelegate = (ScriptingInterfaceOfIUtil.AddCommandLineFunctionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.AddCommandLineFunctionDelegate));
				return;
			case 1160:
				ScriptingInterfaceOfIUtil.call_AddMainThreadPerformanceQueryDelegate = (ScriptingInterfaceOfIUtil.AddMainThreadPerformanceQueryDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.AddMainThreadPerformanceQueryDelegate));
				return;
			case 1161:
				ScriptingInterfaceOfIUtil.call_AddPerformanceReportTokenDelegate = (ScriptingInterfaceOfIUtil.AddPerformanceReportTokenDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.AddPerformanceReportTokenDelegate));
				return;
			case 1162:
				ScriptingInterfaceOfIUtil.call_AddSceneObjectReportDelegate = (ScriptingInterfaceOfIUtil.AddSceneObjectReportDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.AddSceneObjectReportDelegate));
				return;
			case 1163:
				ScriptingInterfaceOfIUtil.call_CheckIfAssetsAndSourcesAreSameDelegate = (ScriptingInterfaceOfIUtil.CheckIfAssetsAndSourcesAreSameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.CheckIfAssetsAndSourcesAreSameDelegate));
				return;
			case 1164:
				ScriptingInterfaceOfIUtil.call_CheckIfTerrainShaderHeaderGenerationFinishedDelegate = (ScriptingInterfaceOfIUtil.CheckIfTerrainShaderHeaderGenerationFinishedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.CheckIfTerrainShaderHeaderGenerationFinishedDelegate));
				return;
			case 1165:
				ScriptingInterfaceOfIUtil.call_CheckResourceModificationsDelegate = (ScriptingInterfaceOfIUtil.CheckResourceModificationsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.CheckResourceModificationsDelegate));
				return;
			case 1166:
				ScriptingInterfaceOfIUtil.call_CheckSceneForProblemsDelegate = (ScriptingInterfaceOfIUtil.CheckSceneForProblemsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.CheckSceneForProblemsDelegate));
				return;
			case 1167:
				ScriptingInterfaceOfIUtil.call_CheckShaderCompilationDelegate = (ScriptingInterfaceOfIUtil.CheckShaderCompilationDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.CheckShaderCompilationDelegate));
				return;
			case 1168:
				ScriptingInterfaceOfIUtil.call_clear_decal_atlasDelegate = (ScriptingInterfaceOfIUtil.clear_decal_atlasDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.clear_decal_atlasDelegate));
				return;
			case 1169:
				ScriptingInterfaceOfIUtil.call_ClearOldResourcesAndObjectsDelegate = (ScriptingInterfaceOfIUtil.ClearOldResourcesAndObjectsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.ClearOldResourcesAndObjectsDelegate));
				return;
			case 1170:
				ScriptingInterfaceOfIUtil.call_ClearShaderMemoryDelegate = (ScriptingInterfaceOfIUtil.ClearShaderMemoryDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.ClearShaderMemoryDelegate));
				return;
			case 1171:
				ScriptingInterfaceOfIUtil.call_CommandLineArgumentExistsDelegate = (ScriptingInterfaceOfIUtil.CommandLineArgumentExistsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.CommandLineArgumentExistsDelegate));
				return;
			case 1172:
				ScriptingInterfaceOfIUtil.call_CompileAllShadersDelegate = (ScriptingInterfaceOfIUtil.CompileAllShadersDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.CompileAllShadersDelegate));
				return;
			case 1173:
				ScriptingInterfaceOfIUtil.call_CompileTerrainShadersDistDelegate = (ScriptingInterfaceOfIUtil.CompileTerrainShadersDistDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.CompileTerrainShadersDistDelegate));
				return;
			case 1174:
				ScriptingInterfaceOfIUtil.call_CreateSelectionInEditorDelegate = (ScriptingInterfaceOfIUtil.CreateSelectionInEditorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.CreateSelectionInEditorDelegate));
				return;
			case 1175:
				ScriptingInterfaceOfIUtil.call_DebugSetGlobalLoadingWindowStateDelegate = (ScriptingInterfaceOfIUtil.DebugSetGlobalLoadingWindowStateDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.DebugSetGlobalLoadingWindowStateDelegate));
				return;
			case 1176:
				ScriptingInterfaceOfIUtil.call_DeleteEntitiesInEditorSceneDelegate = (ScriptingInterfaceOfIUtil.DeleteEntitiesInEditorSceneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.DeleteEntitiesInEditorSceneDelegate));
				return;
			case 1177:
				ScriptingInterfaceOfIUtil.call_DetachWatchdogDelegate = (ScriptingInterfaceOfIUtil.DetachWatchdogDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.DetachWatchdogDelegate));
				return;
			case 1178:
				ScriptingInterfaceOfIUtil.call_DidAutomatedGIBakeFinishedDelegate = (ScriptingInterfaceOfIUtil.DidAutomatedGIBakeFinishedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.DidAutomatedGIBakeFinishedDelegate));
				return;
			case 1179:
				ScriptingInterfaceOfIUtil.call_DisableCoreGameDelegate = (ScriptingInterfaceOfIUtil.DisableCoreGameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.DisableCoreGameDelegate));
				return;
			case 1180:
				ScriptingInterfaceOfIUtil.call_DisableGlobalEditDataCacherDelegate = (ScriptingInterfaceOfIUtil.DisableGlobalEditDataCacherDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.DisableGlobalEditDataCacherDelegate));
				return;
			case 1181:
				ScriptingInterfaceOfIUtil.call_DisableGlobalLoadingWindowDelegate = (ScriptingInterfaceOfIUtil.DisableGlobalLoadingWindowDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.DisableGlobalLoadingWindowDelegate));
				return;
			case 1182:
				ScriptingInterfaceOfIUtil.call_DoDelayedexitDelegate = (ScriptingInterfaceOfIUtil.DoDelayedexitDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.DoDelayedexitDelegate));
				return;
			case 1183:
				ScriptingInterfaceOfIUtil.call_DoFullBakeAllLevelsAutomatedDelegate = (ScriptingInterfaceOfIUtil.DoFullBakeAllLevelsAutomatedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.DoFullBakeAllLevelsAutomatedDelegate));
				return;
			case 1184:
				ScriptingInterfaceOfIUtil.call_DoFullBakeSingleLevelAutomatedDelegate = (ScriptingInterfaceOfIUtil.DoFullBakeSingleLevelAutomatedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.DoFullBakeSingleLevelAutomatedDelegate));
				return;
			case 1185:
				ScriptingInterfaceOfIUtil.call_DoLightOnlyBakeAllLevelsAutomatedDelegate = (ScriptingInterfaceOfIUtil.DoLightOnlyBakeAllLevelsAutomatedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.DoLightOnlyBakeAllLevelsAutomatedDelegate));
				return;
			case 1186:
				ScriptingInterfaceOfIUtil.call_DoLightOnlyBakeSingleLevelAutomatedDelegate = (ScriptingInterfaceOfIUtil.DoLightOnlyBakeSingleLevelAutomatedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.DoLightOnlyBakeSingleLevelAutomatedDelegate));
				return;
			case 1187:
				ScriptingInterfaceOfIUtil.call_DumpGPUMemoryStatisticsDelegate = (ScriptingInterfaceOfIUtil.DumpGPUMemoryStatisticsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.DumpGPUMemoryStatisticsDelegate));
				return;
			case 1188:
				ScriptingInterfaceOfIUtil.call_EnableGlobalEditDataCacherDelegate = (ScriptingInterfaceOfIUtil.EnableGlobalEditDataCacherDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.EnableGlobalEditDataCacherDelegate));
				return;
			case 1189:
				ScriptingInterfaceOfIUtil.call_EnableGlobalLoadingWindowDelegate = (ScriptingInterfaceOfIUtil.EnableGlobalLoadingWindowDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.EnableGlobalLoadingWindowDelegate));
				return;
			case 1190:
				ScriptingInterfaceOfIUtil.call_EnableSingleGPUQueryPerFrameDelegate = (ScriptingInterfaceOfIUtil.EnableSingleGPUQueryPerFrameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.EnableSingleGPUQueryPerFrameDelegate));
				return;
			case 1191:
				ScriptingInterfaceOfIUtil.call_ExecuteCommandLineCommandDelegate = (ScriptingInterfaceOfIUtil.ExecuteCommandLineCommandDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.ExecuteCommandLineCommandDelegate));
				return;
			case 1192:
				ScriptingInterfaceOfIUtil.call_ExitProcessDelegate = (ScriptingInterfaceOfIUtil.ExitProcessDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.ExitProcessDelegate));
				return;
			case 1193:
				ScriptingInterfaceOfIUtil.call_ExportNavMeshFaceMarksDelegate = (ScriptingInterfaceOfIUtil.ExportNavMeshFaceMarksDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.ExportNavMeshFaceMarksDelegate));
				return;
			case 1194:
				ScriptingInterfaceOfIUtil.call_FindMeshesWithoutLodsDelegate = (ScriptingInterfaceOfIUtil.FindMeshesWithoutLodsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.FindMeshesWithoutLodsDelegate));
				return;
			case 1195:
				ScriptingInterfaceOfIUtil.call_FlushManagedObjectsMemoryDelegate = (ScriptingInterfaceOfIUtil.FlushManagedObjectsMemoryDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.FlushManagedObjectsMemoryDelegate));
				return;
			case 1196:
				ScriptingInterfaceOfIUtil.call_GatherCoreGameReferencesDelegate = (ScriptingInterfaceOfIUtil.GatherCoreGameReferencesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GatherCoreGameReferencesDelegate));
				return;
			case 1197:
				ScriptingInterfaceOfIUtil.call_GenerateTerrainShaderHeadersDelegate = (ScriptingInterfaceOfIUtil.GenerateTerrainShaderHeadersDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GenerateTerrainShaderHeadersDelegate));
				return;
			case 1198:
				ScriptingInterfaceOfIUtil.call_GetApplicationMemoryDelegate = (ScriptingInterfaceOfIUtil.GetApplicationMemoryDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetApplicationMemoryDelegate));
				return;
			case 1199:
				ScriptingInterfaceOfIUtil.call_GetApplicationMemoryStatisticsDelegate = (ScriptingInterfaceOfIUtil.GetApplicationMemoryStatisticsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetApplicationMemoryStatisticsDelegate));
				return;
			case 1200:
				ScriptingInterfaceOfIUtil.call_GetApplicationNameDelegate = (ScriptingInterfaceOfIUtil.GetApplicationNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetApplicationNameDelegate));
				return;
			case 1201:
				ScriptingInterfaceOfIUtil.call_GetAttachmentsPathDelegate = (ScriptingInterfaceOfIUtil.GetAttachmentsPathDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetAttachmentsPathDelegate));
				return;
			case 1202:
				ScriptingInterfaceOfIUtil.call_GetBaseDirectoryDelegate = (ScriptingInterfaceOfIUtil.GetBaseDirectoryDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetBaseDirectoryDelegate));
				return;
			case 1203:
				ScriptingInterfaceOfIUtil.call_GetBenchmarkStatusDelegate = (ScriptingInterfaceOfIUtil.GetBenchmarkStatusDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetBenchmarkStatusDelegate));
				return;
			case 1204:
				ScriptingInterfaceOfIUtil.call_GetBuildNumberDelegate = (ScriptingInterfaceOfIUtil.GetBuildNumberDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetBuildNumberDelegate));
				return;
			case 1205:
				ScriptingInterfaceOfIUtil.call_GetConsoleHostMachineDelegate = (ScriptingInterfaceOfIUtil.GetConsoleHostMachineDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetConsoleHostMachineDelegate));
				return;
			case 1206:
				ScriptingInterfaceOfIUtil.call_GetCoreGameStateDelegate = (ScriptingInterfaceOfIUtil.GetCoreGameStateDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetCoreGameStateDelegate));
				return;
			case 1207:
				ScriptingInterfaceOfIUtil.call_GetCurrentCpuMemoryUsageDelegate = (ScriptingInterfaceOfIUtil.GetCurrentCpuMemoryUsageDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetCurrentCpuMemoryUsageDelegate));
				return;
			case 1208:
				ScriptingInterfaceOfIUtil.call_GetCurrentEstimatedGPUMemoryCostMBDelegate = (ScriptingInterfaceOfIUtil.GetCurrentEstimatedGPUMemoryCostMBDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetCurrentEstimatedGPUMemoryCostMBDelegate));
				return;
			case 1209:
				ScriptingInterfaceOfIUtil.call_GetCurrentProcessIDDelegate = (ScriptingInterfaceOfIUtil.GetCurrentProcessIDDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetCurrentProcessIDDelegate));
				return;
			case 1210:
				ScriptingInterfaceOfIUtil.call_GetCurrentThreadIdDelegate = (ScriptingInterfaceOfIUtil.GetCurrentThreadIdDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetCurrentThreadIdDelegate));
				return;
			case 1211:
				ScriptingInterfaceOfIUtil.call_GetDeltaTimeDelegate = (ScriptingInterfaceOfIUtil.GetDeltaTimeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetDeltaTimeDelegate));
				return;
			case 1212:
				ScriptingInterfaceOfIUtil.call_GetDetailedGPUBufferMemoryStatsDelegate = (ScriptingInterfaceOfIUtil.GetDetailedGPUBufferMemoryStatsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetDetailedGPUBufferMemoryStatsDelegate));
				return;
			case 1213:
				ScriptingInterfaceOfIUtil.call_GetDetailedXBOXMemoryInfoDelegate = (ScriptingInterfaceOfIUtil.GetDetailedXBOXMemoryInfoDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetDetailedXBOXMemoryInfoDelegate));
				return;
			case 1214:
				ScriptingInterfaceOfIUtil.call_GetEditorSelectedEntitiesDelegate = (ScriptingInterfaceOfIUtil.GetEditorSelectedEntitiesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetEditorSelectedEntitiesDelegate));
				return;
			case 1215:
				ScriptingInterfaceOfIUtil.call_GetEditorSelectedEntityCountDelegate = (ScriptingInterfaceOfIUtil.GetEditorSelectedEntityCountDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetEditorSelectedEntityCountDelegate));
				return;
			case 1216:
				ScriptingInterfaceOfIUtil.call_GetEngineFrameNoDelegate = (ScriptingInterfaceOfIUtil.GetEngineFrameNoDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetEngineFrameNoDelegate));
				return;
			case 1217:
				ScriptingInterfaceOfIUtil.call_GetEntitiesOfSelectionSetDelegate = (ScriptingInterfaceOfIUtil.GetEntitiesOfSelectionSetDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetEntitiesOfSelectionSetDelegate));
				return;
			case 1218:
				ScriptingInterfaceOfIUtil.call_GetEntityCountOfSelectionSetDelegate = (ScriptingInterfaceOfIUtil.GetEntityCountOfSelectionSetDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetEntityCountOfSelectionSetDelegate));
				return;
			case 1219:
				ScriptingInterfaceOfIUtil.call_GetExecutableWorkingDirectoryDelegate = (ScriptingInterfaceOfIUtil.GetExecutableWorkingDirectoryDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetExecutableWorkingDirectoryDelegate));
				return;
			case 1220:
				ScriptingInterfaceOfIUtil.call_GetFpsDelegate = (ScriptingInterfaceOfIUtil.GetFpsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetFpsDelegate));
				return;
			case 1221:
				ScriptingInterfaceOfIUtil.call_GetFrameLimiterWithSleepDelegate = (ScriptingInterfaceOfIUtil.GetFrameLimiterWithSleepDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetFrameLimiterWithSleepDelegate));
				return;
			case 1222:
				ScriptingInterfaceOfIUtil.call_GetFullCommandLineStringDelegate = (ScriptingInterfaceOfIUtil.GetFullCommandLineStringDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetFullCommandLineStringDelegate));
				return;
			case 1223:
				ScriptingInterfaceOfIUtil.call_GetFullFilePathOfSceneDelegate = (ScriptingInterfaceOfIUtil.GetFullFilePathOfSceneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetFullFilePathOfSceneDelegate));
				return;
			case 1224:
				ScriptingInterfaceOfIUtil.call_GetFullModulePathDelegate = (ScriptingInterfaceOfIUtil.GetFullModulePathDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetFullModulePathDelegate));
				return;
			case 1225:
				ScriptingInterfaceOfIUtil.call_GetFullModulePathsDelegate = (ScriptingInterfaceOfIUtil.GetFullModulePathsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetFullModulePathsDelegate));
				return;
			case 1226:
				ScriptingInterfaceOfIUtil.call_GetGPUMemoryMBDelegate = (ScriptingInterfaceOfIUtil.GetGPUMemoryMBDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetGPUMemoryMBDelegate));
				return;
			case 1227:
				ScriptingInterfaceOfIUtil.call_GetGpuMemoryOfAllocationGroupDelegate = (ScriptingInterfaceOfIUtil.GetGpuMemoryOfAllocationGroupDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetGpuMemoryOfAllocationGroupDelegate));
				return;
			case 1228:
				ScriptingInterfaceOfIUtil.call_GetGPUMemoryStatsDelegate = (ScriptingInterfaceOfIUtil.GetGPUMemoryStatsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetGPUMemoryStatsDelegate));
				return;
			case 1229:
				ScriptingInterfaceOfIUtil.call_GetLocalOutputPathDelegate = (ScriptingInterfaceOfIUtil.GetLocalOutputPathDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetLocalOutputPathDelegate));
				return;
			case 1230:
				ScriptingInterfaceOfIUtil.call_GetMainFpsDelegate = (ScriptingInterfaceOfIUtil.GetMainFpsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetMainFpsDelegate));
				return;
			case 1231:
				ScriptingInterfaceOfIUtil.call_GetMainThreadIdDelegate = (ScriptingInterfaceOfIUtil.GetMainThreadIdDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetMainThreadIdDelegate));
				return;
			case 1232:
				ScriptingInterfaceOfIUtil.call_GetMemoryUsageOfCategoryDelegate = (ScriptingInterfaceOfIUtil.GetMemoryUsageOfCategoryDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetMemoryUsageOfCategoryDelegate));
				return;
			case 1233:
				ScriptingInterfaceOfIUtil.call_GetModulesCodeDelegate = (ScriptingInterfaceOfIUtil.GetModulesCodeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetModulesCodeDelegate));
				return;
			case 1234:
				ScriptingInterfaceOfIUtil.call_GetNativeMemoryStatisticsDelegate = (ScriptingInterfaceOfIUtil.GetNativeMemoryStatisticsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetNativeMemoryStatisticsDelegate));
				return;
			case 1235:
				ScriptingInterfaceOfIUtil.call_GetNumberOfShaderCompilationsInProgressDelegate = (ScriptingInterfaceOfIUtil.GetNumberOfShaderCompilationsInProgressDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetNumberOfShaderCompilationsInProgressDelegate));
				return;
			case 1236:
				ScriptingInterfaceOfIUtil.call_GetPCInfoDelegate = (ScriptingInterfaceOfIUtil.GetPCInfoDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetPCInfoDelegate));
				return;
			case 1237:
				ScriptingInterfaceOfIUtil.call_GetRendererFpsDelegate = (ScriptingInterfaceOfIUtil.GetRendererFpsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetRendererFpsDelegate));
				return;
			case 1238:
				ScriptingInterfaceOfIUtil.call_GetReturnCodeDelegate = (ScriptingInterfaceOfIUtil.GetReturnCodeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetReturnCodeDelegate));
				return;
			case 1239:
				ScriptingInterfaceOfIUtil.call_GetSingleModuleScenesOfModuleDelegate = (ScriptingInterfaceOfIUtil.GetSingleModuleScenesOfModuleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetSingleModuleScenesOfModuleDelegate));
				return;
			case 1240:
				ScriptingInterfaceOfIUtil.call_GetSteamAppIdDelegate = (ScriptingInterfaceOfIUtil.GetSteamAppIdDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetSteamAppIdDelegate));
				return;
			case 1241:
				ScriptingInterfaceOfIUtil.call_GetSystemLanguageDelegate = (ScriptingInterfaceOfIUtil.GetSystemLanguageDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetSystemLanguageDelegate));
				return;
			case 1242:
				ScriptingInterfaceOfIUtil.call_GetVertexBufferChunkSystemMemoryUsageDelegate = (ScriptingInterfaceOfIUtil.GetVertexBufferChunkSystemMemoryUsageDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetVertexBufferChunkSystemMemoryUsageDelegate));
				return;
			case 1243:
				ScriptingInterfaceOfIUtil.call_GetVisualTestsTestFilesPathDelegate = (ScriptingInterfaceOfIUtil.GetVisualTestsTestFilesPathDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetVisualTestsTestFilesPathDelegate));
				return;
			case 1244:
				ScriptingInterfaceOfIUtil.call_GetVisualTestsValidatePathDelegate = (ScriptingInterfaceOfIUtil.GetVisualTestsValidatePathDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.GetVisualTestsValidatePathDelegate));
				return;
			case 1245:
				ScriptingInterfaceOfIUtil.call_IsBenchmarkQuitedDelegate = (ScriptingInterfaceOfIUtil.IsBenchmarkQuitedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.IsBenchmarkQuitedDelegate));
				return;
			case 1246:
				ScriptingInterfaceOfIUtil.call_IsDetailedSoundLogOnDelegate = (ScriptingInterfaceOfIUtil.IsDetailedSoundLogOnDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.IsDetailedSoundLogOnDelegate));
				return;
			case 1247:
				ScriptingInterfaceOfIUtil.call_IsEditModeEnabledDelegate = (ScriptingInterfaceOfIUtil.IsEditModeEnabledDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.IsEditModeEnabledDelegate));
				return;
			case 1248:
				ScriptingInterfaceOfIUtil.call_IsSceneReportFinishedDelegate = (ScriptingInterfaceOfIUtil.IsSceneReportFinishedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.IsSceneReportFinishedDelegate));
				return;
			case 1249:
				ScriptingInterfaceOfIUtil.call_LoadSkyBoxesDelegate = (ScriptingInterfaceOfIUtil.LoadSkyBoxesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.LoadSkyBoxesDelegate));
				return;
			case 1250:
				ScriptingInterfaceOfIUtil.call_LoadVirtualTextureTilesetDelegate = (ScriptingInterfaceOfIUtil.LoadVirtualTextureTilesetDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.LoadVirtualTextureTilesetDelegate));
				return;
			case 1251:
				ScriptingInterfaceOfIUtil.call_ManagedParallelForDelegate = (ScriptingInterfaceOfIUtil.ManagedParallelForDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.ManagedParallelForDelegate));
				return;
			case 1252:
				ScriptingInterfaceOfIUtil.call_ManagedParallelForWithDtDelegate = (ScriptingInterfaceOfIUtil.ManagedParallelForWithDtDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.ManagedParallelForWithDtDelegate));
				return;
			case 1253:
				ScriptingInterfaceOfIUtil.call_OnLoadingWindowDisabledDelegate = (ScriptingInterfaceOfIUtil.OnLoadingWindowDisabledDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.OnLoadingWindowDisabledDelegate));
				return;
			case 1254:
				ScriptingInterfaceOfIUtil.call_OnLoadingWindowEnabledDelegate = (ScriptingInterfaceOfIUtil.OnLoadingWindowEnabledDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.OnLoadingWindowEnabledDelegate));
				return;
			case 1255:
				ScriptingInterfaceOfIUtil.call_OpenOnscreenKeyboardDelegate = (ScriptingInterfaceOfIUtil.OpenOnscreenKeyboardDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.OpenOnscreenKeyboardDelegate));
				return;
			case 1256:
				ScriptingInterfaceOfIUtil.call_OutputBenchmarkValuesToPerformanceReporterDelegate = (ScriptingInterfaceOfIUtil.OutputBenchmarkValuesToPerformanceReporterDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.OutputBenchmarkValuesToPerformanceReporterDelegate));
				return;
			case 1257:
				ScriptingInterfaceOfIUtil.call_OutputPerformanceReportsDelegate = (ScriptingInterfaceOfIUtil.OutputPerformanceReportsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.OutputPerformanceReportsDelegate));
				return;
			case 1258:
				ScriptingInterfaceOfIUtil.call_PairSceneNameToModuleNameDelegate = (ScriptingInterfaceOfIUtil.PairSceneNameToModuleNameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.PairSceneNameToModuleNameDelegate));
				return;
			case 1259:
				ScriptingInterfaceOfIUtil.call_ProcessWindowTitleDelegate = (ScriptingInterfaceOfIUtil.ProcessWindowTitleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.ProcessWindowTitleDelegate));
				return;
			case 1260:
				ScriptingInterfaceOfIUtil.call_QuitGameDelegate = (ScriptingInterfaceOfIUtil.QuitGameDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.QuitGameDelegate));
				return;
			case 1261:
				ScriptingInterfaceOfIUtil.call_RegisterGPUAllocationGroupDelegate = (ScriptingInterfaceOfIUtil.RegisterGPUAllocationGroupDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.RegisterGPUAllocationGroupDelegate));
				return;
			case 1262:
				ScriptingInterfaceOfIUtil.call_RegisterMeshForGPUMorphDelegate = (ScriptingInterfaceOfIUtil.RegisterMeshForGPUMorphDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.RegisterMeshForGPUMorphDelegate));
				return;
			case 1263:
				ScriptingInterfaceOfIUtil.call_SaveDataAsTextureDelegate = (ScriptingInterfaceOfIUtil.SaveDataAsTextureDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SaveDataAsTextureDelegate));
				return;
			case 1264:
				ScriptingInterfaceOfIUtil.call_SelectEntitiesDelegate = (ScriptingInterfaceOfIUtil.SelectEntitiesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SelectEntitiesDelegate));
				return;
			case 1265:
				ScriptingInterfaceOfIUtil.call_SetAllocationAlwaysValidSceneDelegate = (ScriptingInterfaceOfIUtil.SetAllocationAlwaysValidSceneDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetAllocationAlwaysValidSceneDelegate));
				return;
			case 1266:
				ScriptingInterfaceOfIUtil.call_SetAssertionAtShaderCompileDelegate = (ScriptingInterfaceOfIUtil.SetAssertionAtShaderCompileDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetAssertionAtShaderCompileDelegate));
				return;
			case 1267:
				ScriptingInterfaceOfIUtil.call_SetAssertionsAndWarningsSetExitCodeDelegate = (ScriptingInterfaceOfIUtil.SetAssertionsAndWarningsSetExitCodeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetAssertionsAndWarningsSetExitCodeDelegate));
				return;
			case 1268:
				ScriptingInterfaceOfIUtil.call_SetBenchmarkStatusDelegate = (ScriptingInterfaceOfIUtil.SetBenchmarkStatusDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetBenchmarkStatusDelegate));
				return;
			case 1269:
				ScriptingInterfaceOfIUtil.call_SetCoreGameStateDelegate = (ScriptingInterfaceOfIUtil.SetCoreGameStateDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetCoreGameStateDelegate));
				return;
			case 1270:
				ScriptingInterfaceOfIUtil.call_SetCrashOnAssertsDelegate = (ScriptingInterfaceOfIUtil.SetCrashOnAssertsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetCrashOnAssertsDelegate));
				return;
			case 1271:
				ScriptingInterfaceOfIUtil.call_SetCrashOnWarningsDelegate = (ScriptingInterfaceOfIUtil.SetCrashOnWarningsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetCrashOnWarningsDelegate));
				return;
			case 1272:
				ScriptingInterfaceOfIUtil.call_SetCrashReportCustomStackDelegate = (ScriptingInterfaceOfIUtil.SetCrashReportCustomStackDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetCrashReportCustomStackDelegate));
				return;
			case 1273:
				ScriptingInterfaceOfIUtil.call_SetCrashReportCustomStringDelegate = (ScriptingInterfaceOfIUtil.SetCrashReportCustomStringDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetCrashReportCustomStringDelegate));
				return;
			case 1274:
				ScriptingInterfaceOfIUtil.call_SetDisableDumpGenerationDelegate = (ScriptingInterfaceOfIUtil.SetDisableDumpGenerationDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetDisableDumpGenerationDelegate));
				return;
			case 1275:
				ScriptingInterfaceOfIUtil.call_SetDumpFolderPathDelegate = (ScriptingInterfaceOfIUtil.SetDumpFolderPathDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetDumpFolderPathDelegate));
				return;
			case 1276:
				ScriptingInterfaceOfIUtil.call_SetFixedDtDelegate = (ScriptingInterfaceOfIUtil.SetFixedDtDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetFixedDtDelegate));
				return;
			case 1277:
				ScriptingInterfaceOfIUtil.call_SetForceDrawEntityIDDelegate = (ScriptingInterfaceOfIUtil.SetForceDrawEntityIDDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetForceDrawEntityIDDelegate));
				return;
			case 1278:
				ScriptingInterfaceOfIUtil.call_SetForceVsyncDelegate = (ScriptingInterfaceOfIUtil.SetForceVsyncDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetForceVsyncDelegate));
				return;
			case 1279:
				ScriptingInterfaceOfIUtil.call_SetFrameLimiterWithSleepDelegate = (ScriptingInterfaceOfIUtil.SetFrameLimiterWithSleepDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetFrameLimiterWithSleepDelegate));
				return;
			case 1280:
				ScriptingInterfaceOfIUtil.call_SetGraphicsPresetDelegate = (ScriptingInterfaceOfIUtil.SetGraphicsPresetDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetGraphicsPresetDelegate));
				return;
			case 1281:
				ScriptingInterfaceOfIUtil.call_SetLoadingScreenPercentageDelegate = (ScriptingInterfaceOfIUtil.SetLoadingScreenPercentageDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetLoadingScreenPercentageDelegate));
				return;
			case 1282:
				ScriptingInterfaceOfIUtil.call_SetMessageLineRenderingStateDelegate = (ScriptingInterfaceOfIUtil.SetMessageLineRenderingStateDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetMessageLineRenderingStateDelegate));
				return;
			case 1283:
				ScriptingInterfaceOfIUtil.call_SetPrintCallstackAtCrahsesDelegate = (ScriptingInterfaceOfIUtil.SetPrintCallstackAtCrahsesDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetPrintCallstackAtCrahsesDelegate));
				return;
			case 1284:
				ScriptingInterfaceOfIUtil.call_SetRenderAgentsDelegate = (ScriptingInterfaceOfIUtil.SetRenderAgentsDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetRenderAgentsDelegate));
				return;
			case 1285:
				ScriptingInterfaceOfIUtil.call_SetRenderModeDelegate = (ScriptingInterfaceOfIUtil.SetRenderModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetRenderModeDelegate));
				return;
			case 1286:
				ScriptingInterfaceOfIUtil.call_SetReportModeDelegate = (ScriptingInterfaceOfIUtil.SetReportModeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetReportModeDelegate));
				return;
			case 1287:
				ScriptingInterfaceOfIUtil.call_SetScreenTextRenderingStateDelegate = (ScriptingInterfaceOfIUtil.SetScreenTextRenderingStateDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetScreenTextRenderingStateDelegate));
				return;
			case 1288:
				ScriptingInterfaceOfIUtil.call_SetWatchdogValueDelegate = (ScriptingInterfaceOfIUtil.SetWatchdogValueDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetWatchdogValueDelegate));
				return;
			case 1289:
				ScriptingInterfaceOfIUtil.call_SetWindowTitleDelegate = (ScriptingInterfaceOfIUtil.SetWindowTitleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.SetWindowTitleDelegate));
				return;
			case 1290:
				ScriptingInterfaceOfIUtil.call_StartScenePerformanceReportDelegate = (ScriptingInterfaceOfIUtil.StartScenePerformanceReportDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.StartScenePerformanceReportDelegate));
				return;
			case 1291:
				ScriptingInterfaceOfIUtil.call_TakeScreenshotFromPlatformPathDelegate = (ScriptingInterfaceOfIUtil.TakeScreenshotFromPlatformPathDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.TakeScreenshotFromPlatformPathDelegate));
				return;
			case 1292:
				ScriptingInterfaceOfIUtil.call_TakeScreenshotFromStringPathDelegate = (ScriptingInterfaceOfIUtil.TakeScreenshotFromStringPathDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.TakeScreenshotFromStringPathDelegate));
				return;
			case 1293:
				ScriptingInterfaceOfIUtil.call_TakeSSFromTopDelegate = (ScriptingInterfaceOfIUtil.TakeSSFromTopDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.TakeSSFromTopDelegate));
				return;
			case 1294:
				ScriptingInterfaceOfIUtil.call_ToggleRenderDelegate = (ScriptingInterfaceOfIUtil.ToggleRenderDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIUtil.ToggleRenderDelegate));
				return;
			case 1295:
				ScriptingInterfaceOfIVideoPlayerView.call_CreateVideoPlayerViewDelegate = (ScriptingInterfaceOfIVideoPlayerView.CreateVideoPlayerViewDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIVideoPlayerView.CreateVideoPlayerViewDelegate));
				return;
			case 1296:
				ScriptingInterfaceOfIVideoPlayerView.call_FinalizeDelegate = (ScriptingInterfaceOfIVideoPlayerView.FinalizeDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIVideoPlayerView.FinalizeDelegate));
				return;
			case 1297:
				ScriptingInterfaceOfIVideoPlayerView.call_IsVideoFinishedDelegate = (ScriptingInterfaceOfIVideoPlayerView.IsVideoFinishedDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIVideoPlayerView.IsVideoFinishedDelegate));
				return;
			case 1298:
				ScriptingInterfaceOfIVideoPlayerView.call_PlayVideoDelegate = (ScriptingInterfaceOfIVideoPlayerView.PlayVideoDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIVideoPlayerView.PlayVideoDelegate));
				return;
			case 1299:
				ScriptingInterfaceOfIVideoPlayerView.call_StopVideoDelegate = (ScriptingInterfaceOfIVideoPlayerView.StopVideoDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIVideoPlayerView.StopVideoDelegate));
				return;
			case 1300:
				ScriptingInterfaceOfIView.call_SetAutoDepthTargetCreationDelegate = (ScriptingInterfaceOfIView.SetAutoDepthTargetCreationDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIView.SetAutoDepthTargetCreationDelegate));
				return;
			case 1301:
				ScriptingInterfaceOfIView.call_SetClearColorDelegate = (ScriptingInterfaceOfIView.SetClearColorDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIView.SetClearColorDelegate));
				return;
			case 1302:
				ScriptingInterfaceOfIView.call_SetDebugRenderFunctionalityDelegate = (ScriptingInterfaceOfIView.SetDebugRenderFunctionalityDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIView.SetDebugRenderFunctionalityDelegate));
				return;
			case 1303:
				ScriptingInterfaceOfIView.call_SetDepthTargetDelegate = (ScriptingInterfaceOfIView.SetDepthTargetDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIView.SetDepthTargetDelegate));
				return;
			case 1304:
				ScriptingInterfaceOfIView.call_SetEnableDelegate = (ScriptingInterfaceOfIView.SetEnableDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIView.SetEnableDelegate));
				return;
			case 1305:
				ScriptingInterfaceOfIView.call_SetFileNameToSaveResultDelegate = (ScriptingInterfaceOfIView.SetFileNameToSaveResultDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIView.SetFileNameToSaveResultDelegate));
				return;
			case 1306:
				ScriptingInterfaceOfIView.call_SetFilePathToSaveResultDelegate = (ScriptingInterfaceOfIView.SetFilePathToSaveResultDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIView.SetFilePathToSaveResultDelegate));
				return;
			case 1307:
				ScriptingInterfaceOfIView.call_SetFileTypeToSaveDelegate = (ScriptingInterfaceOfIView.SetFileTypeToSaveDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIView.SetFileTypeToSaveDelegate));
				return;
			case 1308:
				ScriptingInterfaceOfIView.call_SetOffsetDelegate = (ScriptingInterfaceOfIView.SetOffsetDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIView.SetOffsetDelegate));
				return;
			case 1309:
				ScriptingInterfaceOfIView.call_SetRenderOnDemandDelegate = (ScriptingInterfaceOfIView.SetRenderOnDemandDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIView.SetRenderOnDemandDelegate));
				return;
			case 1310:
				ScriptingInterfaceOfIView.call_SetRenderOptionDelegate = (ScriptingInterfaceOfIView.SetRenderOptionDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIView.SetRenderOptionDelegate));
				return;
			case 1311:
				ScriptingInterfaceOfIView.call_SetRenderOrderDelegate = (ScriptingInterfaceOfIView.SetRenderOrderDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIView.SetRenderOrderDelegate));
				return;
			case 1312:
				ScriptingInterfaceOfIView.call_SetRenderTargetDelegate = (ScriptingInterfaceOfIView.SetRenderTargetDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIView.SetRenderTargetDelegate));
				return;
			case 1313:
				ScriptingInterfaceOfIView.call_SetSaveFinalResultToDiskDelegate = (ScriptingInterfaceOfIView.SetSaveFinalResultToDiskDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIView.SetSaveFinalResultToDiskDelegate));
				return;
			case 1314:
				ScriptingInterfaceOfIView.call_SetScaleDelegate = (ScriptingInterfaceOfIView.SetScaleDelegate)Marshal.GetDelegateForFunctionPointer(pointer, typeof(ScriptingInterfaceOfIView.SetScaleDelegate));
				return;
			default:
				return;
			}
		}
	}
}

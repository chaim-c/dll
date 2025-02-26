﻿using System;
using System.Collections.Generic;

namespace TaleWorlds.Engine
{
	// Token: 0x02000013 RID: 19
	internal class EngineApplicationInterface
	{
		// Token: 0x06000085 RID: 133 RVA: 0x000030A0 File Offset: 0x000012A0
		private static T GetObject<T>() where T : class
		{
			object obj;
			if (EngineApplicationInterface._objects.TryGetValue(typeof(T).FullName, out obj))
			{
				return obj as T;
			}
			return default(T);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000030E0 File Offset: 0x000012E0
		internal static void SetObjects(Dictionary<string, object> objects)
		{
			EngineApplicationInterface._objects = objects;
			EngineApplicationInterface.IPath = EngineApplicationInterface.GetObject<IPath>();
			EngineApplicationInterface.IShader = EngineApplicationInterface.GetObject<IShader>();
			EngineApplicationInterface.ITexture = EngineApplicationInterface.GetObject<ITexture>();
			EngineApplicationInterface.IMaterial = EngineApplicationInterface.GetObject<IMaterial>();
			EngineApplicationInterface.IMetaMesh = EngineApplicationInterface.GetObject<IMetaMesh>();
			EngineApplicationInterface.IDecal = EngineApplicationInterface.GetObject<IDecal>();
			EngineApplicationInterface.IClothSimulatorComponent = EngineApplicationInterface.GetObject<IClothSimulatorComponent>();
			EngineApplicationInterface.ICompositeComponent = EngineApplicationInterface.GetObject<ICompositeComponent>();
			EngineApplicationInterface.IPhysicsShape = EngineApplicationInterface.GetObject<IPhysicsShape>();
			EngineApplicationInterface.IBodyPart = EngineApplicationInterface.GetObject<IBodyPart>();
			EngineApplicationInterface.IMesh = EngineApplicationInterface.GetObject<IMesh>();
			EngineApplicationInterface.IMeshBuilder = EngineApplicationInterface.GetObject<IMeshBuilder>();
			EngineApplicationInterface.ICamera = EngineApplicationInterface.GetObject<ICamera>();
			EngineApplicationInterface.ISkeleton = EngineApplicationInterface.GetObject<ISkeleton>();
			EngineApplicationInterface.IGameEntity = EngineApplicationInterface.GetObject<IGameEntity>();
			EngineApplicationInterface.IGameEntityComponent = EngineApplicationInterface.GetObject<IGameEntityComponent>();
			EngineApplicationInterface.IScene = EngineApplicationInterface.GetObject<IScene>();
			EngineApplicationInterface.IScriptComponent = EngineApplicationInterface.GetObject<IScriptComponent>();
			EngineApplicationInterface.ILight = EngineApplicationInterface.GetObject<ILight>();
			EngineApplicationInterface.IAsyncTask = EngineApplicationInterface.GetObject<IAsyncTask>();
			EngineApplicationInterface.IParticleSystem = EngineApplicationInterface.GetObject<IParticleSystem>();
			EngineApplicationInterface.IPhysicsMaterial = EngineApplicationInterface.GetObject<IPhysicsMaterial>();
			EngineApplicationInterface.ISceneView = EngineApplicationInterface.GetObject<ISceneView>();
			EngineApplicationInterface.IView = EngineApplicationInterface.GetObject<IView>();
			EngineApplicationInterface.ITableauView = EngineApplicationInterface.GetObject<ITableauView>();
			EngineApplicationInterface.ITextureView = EngineApplicationInterface.GetObject<ITextureView>();
			EngineApplicationInterface.IVideoPlayerView = EngineApplicationInterface.GetObject<IVideoPlayerView>();
			EngineApplicationInterface.IThumbnailCreatorView = EngineApplicationInterface.GetObject<IThumbnailCreatorView>();
			EngineApplicationInterface.IDebug = EngineApplicationInterface.GetObject<IDebug>();
			EngineApplicationInterface.ITwoDimensionView = EngineApplicationInterface.GetObject<ITwoDimensionView>();
			EngineApplicationInterface.IUtil = EngineApplicationInterface.GetObject<IUtil>();
			EngineApplicationInterface.IEngineSizeChecker = EngineApplicationInterface.GetObject<IEngineSizeChecker>();
			EngineApplicationInterface.IInput = EngineApplicationInterface.GetObject<IInput>();
			EngineApplicationInterface.ITime = EngineApplicationInterface.GetObject<ITime>();
			EngineApplicationInterface.IScreen = EngineApplicationInterface.GetObject<IScreen>();
			EngineApplicationInterface.IMusic = EngineApplicationInterface.GetObject<IMusic>();
			EngineApplicationInterface.IImgui = EngineApplicationInterface.GetObject<IImgui>();
			EngineApplicationInterface.IMouseManager = EngineApplicationInterface.GetObject<IMouseManager>();
			EngineApplicationInterface.IHighlights = EngineApplicationInterface.GetObject<IHighlights>();
			EngineApplicationInterface.ISoundEvent = EngineApplicationInterface.GetObject<ISoundEvent>();
			EngineApplicationInterface.ISoundManager = EngineApplicationInterface.GetObject<ISoundManager>();
			EngineApplicationInterface.IConfig = EngineApplicationInterface.GetObject<IConfig>();
			EngineApplicationInterface.IManagedMeshEditOperations = EngineApplicationInterface.GetObject<IManagedMeshEditOperations>();
		}

		// Token: 0x04000024 RID: 36
		internal static IPath IPath;

		// Token: 0x04000025 RID: 37
		internal static IShader IShader;

		// Token: 0x04000026 RID: 38
		internal static ITexture ITexture;

		// Token: 0x04000027 RID: 39
		internal static IMaterial IMaterial;

		// Token: 0x04000028 RID: 40
		internal static IMetaMesh IMetaMesh;

		// Token: 0x04000029 RID: 41
		internal static IDecal IDecal;

		// Token: 0x0400002A RID: 42
		internal static IClothSimulatorComponent IClothSimulatorComponent;

		// Token: 0x0400002B RID: 43
		internal static ICompositeComponent ICompositeComponent;

		// Token: 0x0400002C RID: 44
		internal static IPhysicsShape IPhysicsShape;

		// Token: 0x0400002D RID: 45
		internal static IBodyPart IBodyPart;

		// Token: 0x0400002E RID: 46
		internal static IParticleSystem IParticleSystem;

		// Token: 0x0400002F RID: 47
		internal static IMesh IMesh;

		// Token: 0x04000030 RID: 48
		internal static IMeshBuilder IMeshBuilder;

		// Token: 0x04000031 RID: 49
		internal static ICamera ICamera;

		// Token: 0x04000032 RID: 50
		internal static ISkeleton ISkeleton;

		// Token: 0x04000033 RID: 51
		internal static IGameEntity IGameEntity;

		// Token: 0x04000034 RID: 52
		internal static IGameEntityComponent IGameEntityComponent;

		// Token: 0x04000035 RID: 53
		internal static IScene IScene;

		// Token: 0x04000036 RID: 54
		internal static IScriptComponent IScriptComponent;

		// Token: 0x04000037 RID: 55
		internal static ILight ILight;

		// Token: 0x04000038 RID: 56
		internal static IAsyncTask IAsyncTask;

		// Token: 0x04000039 RID: 57
		internal static IPhysicsMaterial IPhysicsMaterial;

		// Token: 0x0400003A RID: 58
		internal static ISceneView ISceneView;

		// Token: 0x0400003B RID: 59
		internal static IView IView;

		// Token: 0x0400003C RID: 60
		internal static ITableauView ITableauView;

		// Token: 0x0400003D RID: 61
		internal static ITextureView ITextureView;

		// Token: 0x0400003E RID: 62
		internal static IVideoPlayerView IVideoPlayerView;

		// Token: 0x0400003F RID: 63
		internal static IThumbnailCreatorView IThumbnailCreatorView;

		// Token: 0x04000040 RID: 64
		internal static IDebug IDebug;

		// Token: 0x04000041 RID: 65
		internal static ITwoDimensionView ITwoDimensionView;

		// Token: 0x04000042 RID: 66
		internal static IUtil IUtil;

		// Token: 0x04000043 RID: 67
		internal static IEngineSizeChecker IEngineSizeChecker;

		// Token: 0x04000044 RID: 68
		internal static IInput IInput;

		// Token: 0x04000045 RID: 69
		internal static ITime ITime;

		// Token: 0x04000046 RID: 70
		internal static IScreen IScreen;

		// Token: 0x04000047 RID: 71
		internal static IMusic IMusic;

		// Token: 0x04000048 RID: 72
		internal static IImgui IImgui;

		// Token: 0x04000049 RID: 73
		internal static IMouseManager IMouseManager;

		// Token: 0x0400004A RID: 74
		internal static IHighlights IHighlights;

		// Token: 0x0400004B RID: 75
		internal static ISoundEvent ISoundEvent;

		// Token: 0x0400004C RID: 76
		internal static ISoundManager ISoundManager;

		// Token: 0x0400004D RID: 77
		internal static IConfig IConfig;

		// Token: 0x0400004E RID: 78
		internal static IManagedMeshEditOperations IManagedMeshEditOperations;

		// Token: 0x0400004F RID: 79
		private static Dictionary<string, object> _objects;
	}
}

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x02000012 RID: 18
	internal class ScriptingInterfaceOfIMBEditor : IMBEditor
	{
		// Token: 0x060001DB RID: 475 RVA: 0x0000A668 File Offset: 0x00008868
		public void ActivateSceneEditorPresentation()
		{
			ScriptingInterfaceOfIMBEditor.call_ActivateSceneEditorPresentationDelegate();
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000A674 File Offset: 0x00008874
		public void AddEditorWarning(string msg)
		{
			byte[] array = null;
			if (msg != null)
			{
				int byteCount = ScriptingInterfaceOfIMBEditor._utf8.GetByteCount(msg);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBEditor._utf8.GetBytes(msg, 0, msg.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMBEditor.call_AddEditorWarningDelegate(array);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000A6D0 File Offset: 0x000088D0
		public void AddEntityWarning(UIntPtr entityId, string msg)
		{
			byte[] array = null;
			if (msg != null)
			{
				int byteCount = ScriptingInterfaceOfIMBEditor._utf8.GetByteCount(msg);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBEditor._utf8.GetBytes(msg, 0, msg.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMBEditor.call_AddEntityWarningDelegate(entityId, array);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000A72B File Offset: 0x0000892B
		public bool BorderHelpersEnabled()
		{
			return ScriptingInterfaceOfIMBEditor.call_BorderHelpersEnabledDelegate();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000A737 File Offset: 0x00008937
		public void DeactivateSceneEditorPresentation()
		{
			ScriptingInterfaceOfIMBEditor.call_DeactivateSceneEditorPresentationDelegate();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000A743 File Offset: 0x00008943
		public void EnterEditMissionMode(UIntPtr missionPointer)
		{
			ScriptingInterfaceOfIMBEditor.call_EnterEditMissionModeDelegate(missionPointer);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000A750 File Offset: 0x00008950
		public void EnterEditMode(UIntPtr sceneWidgetPointer, ref MatrixFrame initialCameraFrame, float initialCameraElevation, float initialCameraBearing)
		{
			ScriptingInterfaceOfIMBEditor.call_EnterEditModeDelegate(sceneWidgetPointer, ref initialCameraFrame, initialCameraElevation, initialCameraBearing);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000A761 File Offset: 0x00008961
		public void ExitEditMode()
		{
			ScriptingInterfaceOfIMBEditor.call_ExitEditModeDelegate();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000A770 File Offset: 0x00008970
		public string GetAllPrefabsAndChildWithTag(string tag)
		{
			byte[] array = null;
			if (tag != null)
			{
				int byteCount = ScriptingInterfaceOfIMBEditor._utf8.GetByteCount(tag);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBEditor._utf8.GetBytes(tag, 0, tag.Length, array, 0);
				array[byteCount] = 0;
			}
			if (ScriptingInterfaceOfIMBEditor.call_GetAllPrefabsAndChildWithTagDelegate(array) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000A7D4 File Offset: 0x000089D4
		public SceneView GetEditorSceneView()
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMBEditor.call_GetEditorSceneViewDelegate();
			SceneView result = NativeObject.CreateNativeObjectWrapper<SceneView>(nativeObjectPointer);
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000A814 File Offset: 0x00008A14
		public bool HelpersEnabled()
		{
			return ScriptingInterfaceOfIMBEditor.call_HelpersEnabledDelegate();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000A820 File Offset: 0x00008A20
		public bool IsEditMode()
		{
			return ScriptingInterfaceOfIMBEditor.call_IsEditModeDelegate();
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000A82C File Offset: 0x00008A2C
		public bool IsEditModeEnabled()
		{
			return ScriptingInterfaceOfIMBEditor.call_IsEditModeEnabledDelegate();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000A838 File Offset: 0x00008A38
		public bool IsEntitySelected(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIMBEditor.call_IsEntitySelectedDelegate(entityId);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000A845 File Offset: 0x00008A45
		public bool IsReplayManagerRecording()
		{
			return ScriptingInterfaceOfIMBEditor.call_IsReplayManagerRecordingDelegate();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000A851 File Offset: 0x00008A51
		public bool IsReplayManagerRendering()
		{
			return ScriptingInterfaceOfIMBEditor.call_IsReplayManagerRenderingDelegate();
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000A85D File Offset: 0x00008A5D
		public bool IsReplayManagerReplaying()
		{
			return ScriptingInterfaceOfIMBEditor.call_IsReplayManagerReplayingDelegate();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000A869 File Offset: 0x00008A69
		public void LeaveEditMissionMode()
		{
			ScriptingInterfaceOfIMBEditor.call_LeaveEditMissionModeDelegate();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000A875 File Offset: 0x00008A75
		public void LeaveEditMode()
		{
			ScriptingInterfaceOfIMBEditor.call_LeaveEditModeDelegate();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000A881 File Offset: 0x00008A81
		public void RenderEditorMesh(UIntPtr metaMeshId, ref MatrixFrame frame)
		{
			ScriptingInterfaceOfIMBEditor.call_RenderEditorMeshDelegate(metaMeshId, ref frame);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000A890 File Offset: 0x00008A90
		public void SetLevelVisibility(string cumulated_string)
		{
			byte[] array = null;
			if (cumulated_string != null)
			{
				int byteCount = ScriptingInterfaceOfIMBEditor._utf8.GetByteCount(cumulated_string);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBEditor._utf8.GetBytes(cumulated_string, 0, cumulated_string.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMBEditor.call_SetLevelVisibilityDelegate(array);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000A8EC File Offset: 0x00008AEC
		public void SetUpgradeLevelVisibility(string cumulated_string)
		{
			byte[] array = null;
			if (cumulated_string != null)
			{
				int byteCount = ScriptingInterfaceOfIMBEditor._utf8.GetByteCount(cumulated_string);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBEditor._utf8.GetBytes(cumulated_string, 0, cumulated_string.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMBEditor.call_SetUpgradeLevelVisibilityDelegate(array);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000A946 File Offset: 0x00008B46
		public void TickEditMode(float dt)
		{
			ScriptingInterfaceOfIMBEditor.call_TickEditModeDelegate(dt);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000A953 File Offset: 0x00008B53
		public void TickSceneEditorPresentation(float dt)
		{
			ScriptingInterfaceOfIMBEditor.call_TickSceneEditorPresentationDelegate(dt);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000A960 File Offset: 0x00008B60
		public void UpdateSceneTree()
		{
			ScriptingInterfaceOfIMBEditor.call_UpdateSceneTreeDelegate();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000A96C File Offset: 0x00008B6C
		public void ZoomToPosition(Vec3 pos)
		{
			ScriptingInterfaceOfIMBEditor.call_ZoomToPositionDelegate(pos);
		}

		// Token: 0x0400016F RID: 367
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000170 RID: 368
		public static ScriptingInterfaceOfIMBEditor.ActivateSceneEditorPresentationDelegate call_ActivateSceneEditorPresentationDelegate;

		// Token: 0x04000171 RID: 369
		public static ScriptingInterfaceOfIMBEditor.AddEditorWarningDelegate call_AddEditorWarningDelegate;

		// Token: 0x04000172 RID: 370
		public static ScriptingInterfaceOfIMBEditor.AddEntityWarningDelegate call_AddEntityWarningDelegate;

		// Token: 0x04000173 RID: 371
		public static ScriptingInterfaceOfIMBEditor.BorderHelpersEnabledDelegate call_BorderHelpersEnabledDelegate;

		// Token: 0x04000174 RID: 372
		public static ScriptingInterfaceOfIMBEditor.DeactivateSceneEditorPresentationDelegate call_DeactivateSceneEditorPresentationDelegate;

		// Token: 0x04000175 RID: 373
		public static ScriptingInterfaceOfIMBEditor.EnterEditMissionModeDelegate call_EnterEditMissionModeDelegate;

		// Token: 0x04000176 RID: 374
		public static ScriptingInterfaceOfIMBEditor.EnterEditModeDelegate call_EnterEditModeDelegate;

		// Token: 0x04000177 RID: 375
		public static ScriptingInterfaceOfIMBEditor.ExitEditModeDelegate call_ExitEditModeDelegate;

		// Token: 0x04000178 RID: 376
		public static ScriptingInterfaceOfIMBEditor.GetAllPrefabsAndChildWithTagDelegate call_GetAllPrefabsAndChildWithTagDelegate;

		// Token: 0x04000179 RID: 377
		public static ScriptingInterfaceOfIMBEditor.GetEditorSceneViewDelegate call_GetEditorSceneViewDelegate;

		// Token: 0x0400017A RID: 378
		public static ScriptingInterfaceOfIMBEditor.HelpersEnabledDelegate call_HelpersEnabledDelegate;

		// Token: 0x0400017B RID: 379
		public static ScriptingInterfaceOfIMBEditor.IsEditModeDelegate call_IsEditModeDelegate;

		// Token: 0x0400017C RID: 380
		public static ScriptingInterfaceOfIMBEditor.IsEditModeEnabledDelegate call_IsEditModeEnabledDelegate;

		// Token: 0x0400017D RID: 381
		public static ScriptingInterfaceOfIMBEditor.IsEntitySelectedDelegate call_IsEntitySelectedDelegate;

		// Token: 0x0400017E RID: 382
		public static ScriptingInterfaceOfIMBEditor.IsReplayManagerRecordingDelegate call_IsReplayManagerRecordingDelegate;

		// Token: 0x0400017F RID: 383
		public static ScriptingInterfaceOfIMBEditor.IsReplayManagerRenderingDelegate call_IsReplayManagerRenderingDelegate;

		// Token: 0x04000180 RID: 384
		public static ScriptingInterfaceOfIMBEditor.IsReplayManagerReplayingDelegate call_IsReplayManagerReplayingDelegate;

		// Token: 0x04000181 RID: 385
		public static ScriptingInterfaceOfIMBEditor.LeaveEditMissionModeDelegate call_LeaveEditMissionModeDelegate;

		// Token: 0x04000182 RID: 386
		public static ScriptingInterfaceOfIMBEditor.LeaveEditModeDelegate call_LeaveEditModeDelegate;

		// Token: 0x04000183 RID: 387
		public static ScriptingInterfaceOfIMBEditor.RenderEditorMeshDelegate call_RenderEditorMeshDelegate;

		// Token: 0x04000184 RID: 388
		public static ScriptingInterfaceOfIMBEditor.SetLevelVisibilityDelegate call_SetLevelVisibilityDelegate;

		// Token: 0x04000185 RID: 389
		public static ScriptingInterfaceOfIMBEditor.SetUpgradeLevelVisibilityDelegate call_SetUpgradeLevelVisibilityDelegate;

		// Token: 0x04000186 RID: 390
		public static ScriptingInterfaceOfIMBEditor.TickEditModeDelegate call_TickEditModeDelegate;

		// Token: 0x04000187 RID: 391
		public static ScriptingInterfaceOfIMBEditor.TickSceneEditorPresentationDelegate call_TickSceneEditorPresentationDelegate;

		// Token: 0x04000188 RID: 392
		public static ScriptingInterfaceOfIMBEditor.UpdateSceneTreeDelegate call_UpdateSceneTreeDelegate;

		// Token: 0x04000189 RID: 393
		public static ScriptingInterfaceOfIMBEditor.ZoomToPositionDelegate call_ZoomToPositionDelegate;

		// Token: 0x020001D1 RID: 465
		// (Invoke) Token: 0x060009E9 RID: 2537
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ActivateSceneEditorPresentationDelegate();

		// Token: 0x020001D2 RID: 466
		// (Invoke) Token: 0x060009ED RID: 2541
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddEditorWarningDelegate(byte[] msg);

		// Token: 0x020001D3 RID: 467
		// (Invoke) Token: 0x060009F1 RID: 2545
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddEntityWarningDelegate(UIntPtr entityId, byte[] msg);

		// Token: 0x020001D4 RID: 468
		// (Invoke) Token: 0x060009F5 RID: 2549
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool BorderHelpersEnabledDelegate();

		// Token: 0x020001D5 RID: 469
		// (Invoke) Token: 0x060009F9 RID: 2553
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DeactivateSceneEditorPresentationDelegate();

		// Token: 0x020001D6 RID: 470
		// (Invoke) Token: 0x060009FD RID: 2557
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void EnterEditMissionModeDelegate(UIntPtr missionPointer);

		// Token: 0x020001D7 RID: 471
		// (Invoke) Token: 0x06000A01 RID: 2561
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void EnterEditModeDelegate(UIntPtr sceneWidgetPointer, ref MatrixFrame initialCameraFrame, float initialCameraElevation, float initialCameraBearing);

		// Token: 0x020001D8 RID: 472
		// (Invoke) Token: 0x06000A05 RID: 2565
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ExitEditModeDelegate();

		// Token: 0x020001D9 RID: 473
		// (Invoke) Token: 0x06000A09 RID: 2569
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetAllPrefabsAndChildWithTagDelegate(byte[] tag);

		// Token: 0x020001DA RID: 474
		// (Invoke) Token: 0x06000A0D RID: 2573
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetEditorSceneViewDelegate();

		// Token: 0x020001DB RID: 475
		// (Invoke) Token: 0x06000A11 RID: 2577
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool HelpersEnabledDelegate();

		// Token: 0x020001DC RID: 476
		// (Invoke) Token: 0x06000A15 RID: 2581
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsEditModeDelegate();

		// Token: 0x020001DD RID: 477
		// (Invoke) Token: 0x06000A19 RID: 2585
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsEditModeEnabledDelegate();

		// Token: 0x020001DE RID: 478
		// (Invoke) Token: 0x06000A1D RID: 2589
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsEntitySelectedDelegate(UIntPtr entityId);

		// Token: 0x020001DF RID: 479
		// (Invoke) Token: 0x06000A21 RID: 2593
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsReplayManagerRecordingDelegate();

		// Token: 0x020001E0 RID: 480
		// (Invoke) Token: 0x06000A25 RID: 2597
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsReplayManagerRenderingDelegate();

		// Token: 0x020001E1 RID: 481
		// (Invoke) Token: 0x06000A29 RID: 2601
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsReplayManagerReplayingDelegate();

		// Token: 0x020001E2 RID: 482
		// (Invoke) Token: 0x06000A2D RID: 2605
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void LeaveEditMissionModeDelegate();

		// Token: 0x020001E3 RID: 483
		// (Invoke) Token: 0x06000A31 RID: 2609
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void LeaveEditModeDelegate();

		// Token: 0x020001E4 RID: 484
		// (Invoke) Token: 0x06000A35 RID: 2613
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RenderEditorMeshDelegate(UIntPtr metaMeshId, ref MatrixFrame frame);

		// Token: 0x020001E5 RID: 485
		// (Invoke) Token: 0x06000A39 RID: 2617
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLevelVisibilityDelegate(byte[] cumulated_string);

		// Token: 0x020001E6 RID: 486
		// (Invoke) Token: 0x06000A3D RID: 2621
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetUpgradeLevelVisibilityDelegate(byte[] cumulated_string);

		// Token: 0x020001E7 RID: 487
		// (Invoke) Token: 0x06000A41 RID: 2625
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TickEditModeDelegate(float dt);

		// Token: 0x020001E8 RID: 488
		// (Invoke) Token: 0x06000A45 RID: 2629
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TickSceneEditorPresentationDelegate(float dt);

		// Token: 0x020001E9 RID: 489
		// (Invoke) Token: 0x06000A49 RID: 2633
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void UpdateSceneTreeDelegate();

		// Token: 0x020001EA RID: 490
		// (Invoke) Token: 0x06000A4D RID: 2637
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ZoomToPositionDelegate(Vec3 pos);
	}
}

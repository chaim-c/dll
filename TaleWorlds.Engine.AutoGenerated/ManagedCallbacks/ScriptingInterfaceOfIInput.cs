using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x02000015 RID: 21
	internal class ScriptingInterfaceOfIInput : IInput
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x000101F6 File Offset: 0x0000E3F6
		public void ClearKeys()
		{
			ScriptingInterfaceOfIInput.call_ClearKeysDelegate();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00010202 File Offset: 0x0000E402
		public string GetClipboardText()
		{
			if (ScriptingInterfaceOfIInput.call_GetClipboardTextDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00010218 File Offset: 0x0000E418
		public int GetControllerType()
		{
			return ScriptingInterfaceOfIInput.call_GetControllerTypeDelegate();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00010224 File Offset: 0x0000E424
		public float GetGyroX()
		{
			return ScriptingInterfaceOfIInput.call_GetGyroXDelegate();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00010230 File Offset: 0x0000E430
		public float GetGyroY()
		{
			return ScriptingInterfaceOfIInput.call_GetGyroYDelegate();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0001023C File Offset: 0x0000E43C
		public float GetGyroZ()
		{
			return ScriptingInterfaceOfIInput.call_GetGyroZDelegate();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00010248 File Offset: 0x0000E448
		public Vec2 GetKeyState(InputKey key)
		{
			return ScriptingInterfaceOfIInput.call_GetKeyStateDelegate(key);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00010255 File Offset: 0x0000E455
		public float GetMouseDeltaZ()
		{
			return ScriptingInterfaceOfIInput.call_GetMouseDeltaZDelegate();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00010261 File Offset: 0x0000E461
		public float GetMouseMoveX()
		{
			return ScriptingInterfaceOfIInput.call_GetMouseMoveXDelegate();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0001026D File Offset: 0x0000E46D
		public float GetMouseMoveY()
		{
			return ScriptingInterfaceOfIInput.call_GetMouseMoveYDelegate();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00010279 File Offset: 0x0000E479
		public float GetMousePositionX()
		{
			return ScriptingInterfaceOfIInput.call_GetMousePositionXDelegate();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00010285 File Offset: 0x0000E485
		public float GetMousePositionY()
		{
			return ScriptingInterfaceOfIInput.call_GetMousePositionYDelegate();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00010291 File Offset: 0x0000E491
		public float GetMouseScrollValue()
		{
			return ScriptingInterfaceOfIInput.call_GetMouseScrollValueDelegate();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0001029D File Offset: 0x0000E49D
		public float GetMouseSensitivity()
		{
			return ScriptingInterfaceOfIInput.call_GetMouseSensitivityDelegate();
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000102A9 File Offset: 0x0000E4A9
		public int GetVirtualKeyCode(InputKey key)
		{
			return ScriptingInterfaceOfIInput.call_GetVirtualKeyCodeDelegate(key);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000102B6 File Offset: 0x0000E4B6
		public bool IsAnyTouchActive()
		{
			return ScriptingInterfaceOfIInput.call_IsAnyTouchActiveDelegate();
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000102C2 File Offset: 0x0000E4C2
		public bool IsControllerConnected()
		{
			return ScriptingInterfaceOfIInput.call_IsControllerConnectedDelegate();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000102CE File Offset: 0x0000E4CE
		public bool IsKeyDown(InputKey key)
		{
			return ScriptingInterfaceOfIInput.call_IsKeyDownDelegate(key);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000102DB File Offset: 0x0000E4DB
		public bool IsKeyDownImmediate(InputKey key)
		{
			return ScriptingInterfaceOfIInput.call_IsKeyDownImmediateDelegate(key);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000102E8 File Offset: 0x0000E4E8
		public bool IsKeyPressed(InputKey key)
		{
			return ScriptingInterfaceOfIInput.call_IsKeyPressedDelegate(key);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000102F5 File Offset: 0x0000E4F5
		public bool IsKeyReleased(InputKey key)
		{
			return ScriptingInterfaceOfIInput.call_IsKeyReleasedDelegate(key);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00010302 File Offset: 0x0000E502
		public bool IsMouseActive()
		{
			return ScriptingInterfaceOfIInput.call_IsMouseActiveDelegate();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0001030E File Offset: 0x0000E50E
		public void PressKey(InputKey key)
		{
			ScriptingInterfaceOfIInput.call_PressKeyDelegate(key);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0001031C File Offset: 0x0000E51C
		public void SetClipboardText(string text)
		{
			byte[] array = null;
			if (text != null)
			{
				int byteCount = ScriptingInterfaceOfIInput._utf8.GetByteCount(text);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIInput._utf8.GetBytes(text, 0, text.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIInput.call_SetClipboardTextDelegate(array);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00010376 File Offset: 0x0000E576
		public void SetCursorFrictionValue(float frictionValue)
		{
			ScriptingInterfaceOfIInput.call_SetCursorFrictionValueDelegate(frictionValue);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00010383 File Offset: 0x0000E583
		public void SetCursorPosition(int x, int y)
		{
			ScriptingInterfaceOfIInput.call_SetCursorPositionDelegate(x, y);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00010391 File Offset: 0x0000E591
		public void SetLightbarColor(float red, float green, float blue)
		{
			ScriptingInterfaceOfIInput.call_SetLightbarColorDelegate(red, green, blue);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000103A0 File Offset: 0x0000E5A0
		public void SetRumbleEffect(float[] lowFrequencyLevels, float[] lowFrequencyDurations, int numLowFrequencyElements, float[] highFrequencyLevels, float[] highFrequencyDurations, int numHighFrequencyElements)
		{
			PinnedArrayData<float> pinnedArrayData = new PinnedArrayData<float>(lowFrequencyLevels, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			PinnedArrayData<float> pinnedArrayData2 = new PinnedArrayData<float>(lowFrequencyDurations, false);
			IntPtr pointer2 = pinnedArrayData2.Pointer;
			PinnedArrayData<float> pinnedArrayData3 = new PinnedArrayData<float>(highFrequencyLevels, false);
			IntPtr pointer3 = pinnedArrayData3.Pointer;
			PinnedArrayData<float> pinnedArrayData4 = new PinnedArrayData<float>(highFrequencyDurations, false);
			IntPtr pointer4 = pinnedArrayData4.Pointer;
			ScriptingInterfaceOfIInput.call_SetRumbleEffectDelegate(pointer, pointer2, numLowFrequencyElements, pointer3, pointer4, numHighFrequencyElements);
			pinnedArrayData.Dispose();
			pinnedArrayData2.Dispose();
			pinnedArrayData3.Dispose();
			pinnedArrayData4.Dispose();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00010424 File Offset: 0x0000E624
		public void SetTriggerFeedback(byte leftTriggerPosition, byte leftTriggerStrength, byte rightTriggerPosition, byte rightTriggerStrength)
		{
			ScriptingInterfaceOfIInput.call_SetTriggerFeedbackDelegate(leftTriggerPosition, leftTriggerStrength, rightTriggerPosition, rightTriggerStrength);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00010438 File Offset: 0x0000E638
		public void SetTriggerVibration(float[] leftTriggerAmplitudes, float[] leftTriggerFrequencies, float[] leftTriggerDurations, int numLeftTriggerElements, float[] rightTriggerAmplitudes, float[] rightTriggerFrequencies, float[] rightTriggerDurations, int numRightTriggerElements)
		{
			PinnedArrayData<float> pinnedArrayData = new PinnedArrayData<float>(leftTriggerAmplitudes, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			PinnedArrayData<float> pinnedArrayData2 = new PinnedArrayData<float>(leftTriggerFrequencies, false);
			IntPtr pointer2 = pinnedArrayData2.Pointer;
			PinnedArrayData<float> pinnedArrayData3 = new PinnedArrayData<float>(leftTriggerDurations, false);
			IntPtr pointer3 = pinnedArrayData3.Pointer;
			PinnedArrayData<float> pinnedArrayData4 = new PinnedArrayData<float>(rightTriggerAmplitudes, false);
			IntPtr pointer4 = pinnedArrayData4.Pointer;
			PinnedArrayData<float> pinnedArrayData5 = new PinnedArrayData<float>(rightTriggerFrequencies, false);
			IntPtr pointer5 = pinnedArrayData5.Pointer;
			PinnedArrayData<float> pinnedArrayData6 = new PinnedArrayData<float>(rightTriggerDurations, false);
			IntPtr pointer6 = pinnedArrayData6.Pointer;
			ScriptingInterfaceOfIInput.call_SetTriggerVibrationDelegate(pointer, pointer2, pointer3, numLeftTriggerElements, pointer4, pointer5, pointer6, numRightTriggerElements);
			pinnedArrayData.Dispose();
			pinnedArrayData2.Dispose();
			pinnedArrayData3.Dispose();
			pinnedArrayData4.Dispose();
			pinnedArrayData5.Dispose();
			pinnedArrayData6.Dispose();
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000104F4 File Offset: 0x0000E6F4
		public void SetTriggerWeaponEffect(byte leftStartPosition, byte leftEnd_position, byte leftStrength, byte rightStartPosition, byte rightEndPosition, byte rightStrength)
		{
			ScriptingInterfaceOfIInput.call_SetTriggerWeaponEffectDelegate(leftStartPosition, leftEnd_position, leftStrength, rightStartPosition, rightEndPosition, rightStrength);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0001050C File Offset: 0x0000E70C
		public void UpdateKeyData(byte[] keyData)
		{
			PinnedArrayData<byte> pinnedArrayData = new PinnedArrayData<byte>(keyData, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ManagedArray keyData2 = new ManagedArray(pointer, (keyData != null) ? keyData.Length : 0);
			ScriptingInterfaceOfIInput.call_UpdateKeyDataDelegate(keyData2);
			pinnedArrayData.Dispose();
		}

		// Token: 0x04000189 RID: 393
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x0400018A RID: 394
		public static ScriptingInterfaceOfIInput.ClearKeysDelegate call_ClearKeysDelegate;

		// Token: 0x0400018B RID: 395
		public static ScriptingInterfaceOfIInput.GetClipboardTextDelegate call_GetClipboardTextDelegate;

		// Token: 0x0400018C RID: 396
		public static ScriptingInterfaceOfIInput.GetControllerTypeDelegate call_GetControllerTypeDelegate;

		// Token: 0x0400018D RID: 397
		public static ScriptingInterfaceOfIInput.GetGyroXDelegate call_GetGyroXDelegate;

		// Token: 0x0400018E RID: 398
		public static ScriptingInterfaceOfIInput.GetGyroYDelegate call_GetGyroYDelegate;

		// Token: 0x0400018F RID: 399
		public static ScriptingInterfaceOfIInput.GetGyroZDelegate call_GetGyroZDelegate;

		// Token: 0x04000190 RID: 400
		public static ScriptingInterfaceOfIInput.GetKeyStateDelegate call_GetKeyStateDelegate;

		// Token: 0x04000191 RID: 401
		public static ScriptingInterfaceOfIInput.GetMouseDeltaZDelegate call_GetMouseDeltaZDelegate;

		// Token: 0x04000192 RID: 402
		public static ScriptingInterfaceOfIInput.GetMouseMoveXDelegate call_GetMouseMoveXDelegate;

		// Token: 0x04000193 RID: 403
		public static ScriptingInterfaceOfIInput.GetMouseMoveYDelegate call_GetMouseMoveYDelegate;

		// Token: 0x04000194 RID: 404
		public static ScriptingInterfaceOfIInput.GetMousePositionXDelegate call_GetMousePositionXDelegate;

		// Token: 0x04000195 RID: 405
		public static ScriptingInterfaceOfIInput.GetMousePositionYDelegate call_GetMousePositionYDelegate;

		// Token: 0x04000196 RID: 406
		public static ScriptingInterfaceOfIInput.GetMouseScrollValueDelegate call_GetMouseScrollValueDelegate;

		// Token: 0x04000197 RID: 407
		public static ScriptingInterfaceOfIInput.GetMouseSensitivityDelegate call_GetMouseSensitivityDelegate;

		// Token: 0x04000198 RID: 408
		public static ScriptingInterfaceOfIInput.GetVirtualKeyCodeDelegate call_GetVirtualKeyCodeDelegate;

		// Token: 0x04000199 RID: 409
		public static ScriptingInterfaceOfIInput.IsAnyTouchActiveDelegate call_IsAnyTouchActiveDelegate;

		// Token: 0x0400019A RID: 410
		public static ScriptingInterfaceOfIInput.IsControllerConnectedDelegate call_IsControllerConnectedDelegate;

		// Token: 0x0400019B RID: 411
		public static ScriptingInterfaceOfIInput.IsKeyDownDelegate call_IsKeyDownDelegate;

		// Token: 0x0400019C RID: 412
		public static ScriptingInterfaceOfIInput.IsKeyDownImmediateDelegate call_IsKeyDownImmediateDelegate;

		// Token: 0x0400019D RID: 413
		public static ScriptingInterfaceOfIInput.IsKeyPressedDelegate call_IsKeyPressedDelegate;

		// Token: 0x0400019E RID: 414
		public static ScriptingInterfaceOfIInput.IsKeyReleasedDelegate call_IsKeyReleasedDelegate;

		// Token: 0x0400019F RID: 415
		public static ScriptingInterfaceOfIInput.IsMouseActiveDelegate call_IsMouseActiveDelegate;

		// Token: 0x040001A0 RID: 416
		public static ScriptingInterfaceOfIInput.PressKeyDelegate call_PressKeyDelegate;

		// Token: 0x040001A1 RID: 417
		public static ScriptingInterfaceOfIInput.SetClipboardTextDelegate call_SetClipboardTextDelegate;

		// Token: 0x040001A2 RID: 418
		public static ScriptingInterfaceOfIInput.SetCursorFrictionValueDelegate call_SetCursorFrictionValueDelegate;

		// Token: 0x040001A3 RID: 419
		public static ScriptingInterfaceOfIInput.SetCursorPositionDelegate call_SetCursorPositionDelegate;

		// Token: 0x040001A4 RID: 420
		public static ScriptingInterfaceOfIInput.SetLightbarColorDelegate call_SetLightbarColorDelegate;

		// Token: 0x040001A5 RID: 421
		public static ScriptingInterfaceOfIInput.SetRumbleEffectDelegate call_SetRumbleEffectDelegate;

		// Token: 0x040001A6 RID: 422
		public static ScriptingInterfaceOfIInput.SetTriggerFeedbackDelegate call_SetTriggerFeedbackDelegate;

		// Token: 0x040001A7 RID: 423
		public static ScriptingInterfaceOfIInput.SetTriggerVibrationDelegate call_SetTriggerVibrationDelegate;

		// Token: 0x040001A8 RID: 424
		public static ScriptingInterfaceOfIInput.SetTriggerWeaponEffectDelegate call_SetTriggerWeaponEffectDelegate;

		// Token: 0x040001A9 RID: 425
		public static ScriptingInterfaceOfIInput.UpdateKeyDataDelegate call_UpdateKeyDataDelegate;

		// Token: 0x020001EE RID: 494
		// (Invoke) Token: 0x06000CBB RID: 3259
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearKeysDelegate();

		// Token: 0x020001EF RID: 495
		// (Invoke) Token: 0x06000CBF RID: 3263
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetClipboardTextDelegate();

		// Token: 0x020001F0 RID: 496
		// (Invoke) Token: 0x06000CC3 RID: 3267
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetControllerTypeDelegate();

		// Token: 0x020001F1 RID: 497
		// (Invoke) Token: 0x06000CC7 RID: 3271
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetGyroXDelegate();

		// Token: 0x020001F2 RID: 498
		// (Invoke) Token: 0x06000CCB RID: 3275
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetGyroYDelegate();

		// Token: 0x020001F3 RID: 499
		// (Invoke) Token: 0x06000CCF RID: 3279
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetGyroZDelegate();

		// Token: 0x020001F4 RID: 500
		// (Invoke) Token: 0x06000CD3 RID: 3283
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec2 GetKeyStateDelegate(InputKey key);

		// Token: 0x020001F5 RID: 501
		// (Invoke) Token: 0x06000CD7 RID: 3287
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetMouseDeltaZDelegate();

		// Token: 0x020001F6 RID: 502
		// (Invoke) Token: 0x06000CDB RID: 3291
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetMouseMoveXDelegate();

		// Token: 0x020001F7 RID: 503
		// (Invoke) Token: 0x06000CDF RID: 3295
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetMouseMoveYDelegate();

		// Token: 0x020001F8 RID: 504
		// (Invoke) Token: 0x06000CE3 RID: 3299
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetMousePositionXDelegate();

		// Token: 0x020001F9 RID: 505
		// (Invoke) Token: 0x06000CE7 RID: 3303
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetMousePositionYDelegate();

		// Token: 0x020001FA RID: 506
		// (Invoke) Token: 0x06000CEB RID: 3307
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetMouseScrollValueDelegate();

		// Token: 0x020001FB RID: 507
		// (Invoke) Token: 0x06000CEF RID: 3311
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetMouseSensitivityDelegate();

		// Token: 0x020001FC RID: 508
		// (Invoke) Token: 0x06000CF3 RID: 3315
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetVirtualKeyCodeDelegate(InputKey key);

		// Token: 0x020001FD RID: 509
		// (Invoke) Token: 0x06000CF7 RID: 3319
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsAnyTouchActiveDelegate();

		// Token: 0x020001FE RID: 510
		// (Invoke) Token: 0x06000CFB RID: 3323
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsControllerConnectedDelegate();

		// Token: 0x020001FF RID: 511
		// (Invoke) Token: 0x06000CFF RID: 3327
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsKeyDownDelegate(InputKey key);

		// Token: 0x02000200 RID: 512
		// (Invoke) Token: 0x06000D03 RID: 3331
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsKeyDownImmediateDelegate(InputKey key);

		// Token: 0x02000201 RID: 513
		// (Invoke) Token: 0x06000D07 RID: 3335
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsKeyPressedDelegate(InputKey key);

		// Token: 0x02000202 RID: 514
		// (Invoke) Token: 0x06000D0B RID: 3339
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsKeyReleasedDelegate(InputKey key);

		// Token: 0x02000203 RID: 515
		// (Invoke) Token: 0x06000D0F RID: 3343
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsMouseActiveDelegate();

		// Token: 0x02000204 RID: 516
		// (Invoke) Token: 0x06000D13 RID: 3347
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PressKeyDelegate(InputKey key);

		// Token: 0x02000205 RID: 517
		// (Invoke) Token: 0x06000D17 RID: 3351
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetClipboardTextDelegate(byte[] text);

		// Token: 0x02000206 RID: 518
		// (Invoke) Token: 0x06000D1B RID: 3355
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetCursorFrictionValueDelegate(float frictionValue);

		// Token: 0x02000207 RID: 519
		// (Invoke) Token: 0x06000D1F RID: 3359
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetCursorPositionDelegate(int x, int y);

		// Token: 0x02000208 RID: 520
		// (Invoke) Token: 0x06000D23 RID: 3363
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLightbarColorDelegate(float red, float green, float blue);

		// Token: 0x02000209 RID: 521
		// (Invoke) Token: 0x06000D27 RID: 3367
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetRumbleEffectDelegate(IntPtr lowFrequencyLevels, IntPtr lowFrequencyDurations, int numLowFrequencyElements, IntPtr highFrequencyLevels, IntPtr highFrequencyDurations, int numHighFrequencyElements);

		// Token: 0x0200020A RID: 522
		// (Invoke) Token: 0x06000D2B RID: 3371
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetTriggerFeedbackDelegate(byte leftTriggerPosition, byte leftTriggerStrength, byte rightTriggerPosition, byte rightTriggerStrength);

		// Token: 0x0200020B RID: 523
		// (Invoke) Token: 0x06000D2F RID: 3375
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetTriggerVibrationDelegate(IntPtr leftTriggerAmplitudes, IntPtr leftTriggerFrequencies, IntPtr leftTriggerDurations, int numLeftTriggerElements, IntPtr rightTriggerAmplitudes, IntPtr rightTriggerFrequencies, IntPtr rightTriggerDurations, int numRightTriggerElements);

		// Token: 0x0200020C RID: 524
		// (Invoke) Token: 0x06000D33 RID: 3379
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetTriggerWeaponEffectDelegate(byte leftStartPosition, byte leftEnd_position, byte leftStrength, byte rightStartPosition, byte rightEndPosition, byte rightStrength);

		// Token: 0x0200020D RID: 525
		// (Invoke) Token: 0x06000D37 RID: 3383
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void UpdateKeyDataDelegate(ManagedArray keyData);
	}
}

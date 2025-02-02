using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x0200000E RID: 14
	internal class ScriptingInterfaceOfIDebug : IDebug
	{
		// Token: 0x060000CA RID: 202 RVA: 0x0000D71E File Offset: 0x0000B91E
		public void AbortGame(int ExitCode)
		{
			ScriptingInterfaceOfIDebug.call_AbortGameDelegate(ExitCode);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000D72B File Offset: 0x0000B92B
		public void AssertMemoryUsage(int memoryMB)
		{
			ScriptingInterfaceOfIDebug.call_AssertMemoryUsageDelegate(memoryMB);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000D738 File Offset: 0x0000B938
		public void ClearAllDebugRenderObjects()
		{
			ScriptingInterfaceOfIDebug.call_ClearAllDebugRenderObjectsDelegate();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000D744 File Offset: 0x0000B944
		public bool ContentWarning(string MessageString)
		{
			byte[] array = null;
			if (MessageString != null)
			{
				int byteCount = ScriptingInterfaceOfIDebug._utf8.GetByteCount(MessageString);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIDebug._utf8.GetBytes(MessageString, 0, MessageString.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIDebug.call_ContentWarningDelegate(array);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000D7A0 File Offset: 0x0000B9A0
		public void EchoCommandWindow(string content)
		{
			byte[] array = null;
			if (content != null)
			{
				int byteCount = ScriptingInterfaceOfIDebug._utf8.GetByteCount(content);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIDebug._utf8.GetBytes(content, 0, content.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIDebug.call_EchoCommandWindowDelegate(array);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000D7FC File Offset: 0x0000B9FC
		public bool Error(string MessageString)
		{
			byte[] array = null;
			if (MessageString != null)
			{
				int byteCount = ScriptingInterfaceOfIDebug._utf8.GetByteCount(MessageString);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIDebug._utf8.GetBytes(MessageString, 0, MessageString.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIDebug.call_ErrorDelegate(array);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000D858 File Offset: 0x0000BA58
		public bool FailedAssert(string messageString, string callerFile, string callerMethod, int callerLine)
		{
			byte[] array = null;
			if (messageString != null)
			{
				int byteCount = ScriptingInterfaceOfIDebug._utf8.GetByteCount(messageString);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIDebug._utf8.GetBytes(messageString, 0, messageString.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (callerFile != null)
			{
				int byteCount2 = ScriptingInterfaceOfIDebug._utf8.GetByteCount(callerFile);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIDebug._utf8.GetBytes(callerFile, 0, callerFile.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			byte[] array3 = null;
			if (callerMethod != null)
			{
				int byteCount3 = ScriptingInterfaceOfIDebug._utf8.GetByteCount(callerMethod);
				array3 = ((byteCount3 < 1024) ? CallbackStringBufferManager.StringBuffer2 : new byte[byteCount3 + 1]);
				ScriptingInterfaceOfIDebug._utf8.GetBytes(callerMethod, 0, callerMethod.Length, array3, 0);
				array3[byteCount3] = 0;
			}
			return ScriptingInterfaceOfIDebug.call_FailedAssertDelegate(array, array2, array3, callerLine);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000D942 File Offset: 0x0000BB42
		public Vec3 GetDebugVector()
		{
			return ScriptingInterfaceOfIDebug.call_GetDebugVectorDelegate();
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000D94E File Offset: 0x0000BB4E
		public int GetShowDebugInfo()
		{
			return ScriptingInterfaceOfIDebug.call_GetShowDebugInfoDelegate();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000D95A File Offset: 0x0000BB5A
		public bool IsErrorReportModeActive()
		{
			return ScriptingInterfaceOfIDebug.call_IsErrorReportModeActiveDelegate();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000D966 File Offset: 0x0000BB66
		public bool IsErrorReportModePauseMission()
		{
			return ScriptingInterfaceOfIDebug.call_IsErrorReportModePauseMissionDelegate();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000D972 File Offset: 0x0000BB72
		public bool IsTestMode()
		{
			return ScriptingInterfaceOfIDebug.call_IsTestModeDelegate();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000D980 File Offset: 0x0000BB80
		public int MessageBox(string lpText, string lpCaption, uint uType)
		{
			byte[] array = null;
			if (lpText != null)
			{
				int byteCount = ScriptingInterfaceOfIDebug._utf8.GetByteCount(lpText);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIDebug._utf8.GetBytes(lpText, 0, lpText.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (lpCaption != null)
			{
				int byteCount2 = ScriptingInterfaceOfIDebug._utf8.GetByteCount(lpCaption);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIDebug._utf8.GetBytes(lpCaption, 0, lpCaption.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			return ScriptingInterfaceOfIDebug.call_MessageBoxDelegate(array, array2, uType);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000DA20 File Offset: 0x0000BC20
		public void PostWarningLine(string line)
		{
			byte[] array = null;
			if (line != null)
			{
				int byteCount = ScriptingInterfaceOfIDebug._utf8.GetByteCount(line);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIDebug._utf8.GetBytes(line, 0, line.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIDebug.call_PostWarningLineDelegate(array);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000DA7A File Offset: 0x0000BC7A
		public void RenderDebugBoxObject(Vec3 min, Vec3 max, uint color, bool depthCheck, float time)
		{
			ScriptingInterfaceOfIDebug.call_RenderDebugBoxObjectDelegate(min, max, color, depthCheck, time);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000DA8D File Offset: 0x0000BC8D
		public void RenderDebugBoxObjectWithFrame(Vec3 min, Vec3 max, ref MatrixFrame frame, uint color, bool depthCheck, float time)
		{
			ScriptingInterfaceOfIDebug.call_RenderDebugBoxObjectWithFrameDelegate(min, max, ref frame, color, depthCheck, time);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000DAA2 File Offset: 0x0000BCA2
		public void RenderDebugCapsule(Vec3 p0, Vec3 p1, float radius, uint color, bool depthCheck, float time)
		{
			ScriptingInterfaceOfIDebug.call_RenderDebugCapsuleDelegate(p0, p1, radius, color, depthCheck, time);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000DAB7 File Offset: 0x0000BCB7
		public void RenderDebugDirectionArrow(Vec3 position, Vec3 direction, uint color, bool depthCheck)
		{
			ScriptingInterfaceOfIDebug.call_RenderDebugDirectionArrowDelegate(position, direction, color, depthCheck);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000DAC8 File Offset: 0x0000BCC8
		public void RenderDebugFrame(ref MatrixFrame frame, float lineLength, float time)
		{
			ScriptingInterfaceOfIDebug.call_RenderDebugFrameDelegate(ref frame, lineLength, time);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000DAD7 File Offset: 0x0000BCD7
		public void RenderDebugLine(Vec3 position, Vec3 direction, uint color, bool depthCheck, float time)
		{
			ScriptingInterfaceOfIDebug.call_RenderDebugLineDelegate(position, direction, color, depthCheck, time);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000DAEA File Offset: 0x0000BCEA
		public void RenderDebugRect(float left, float bottom, float right, float top)
		{
			ScriptingInterfaceOfIDebug.call_RenderDebugRectDelegate(left, bottom, right, top);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000DAFB File Offset: 0x0000BCFB
		public void RenderDebugRectWithColor(float left, float bottom, float right, float top, uint color)
		{
			ScriptingInterfaceOfIDebug.call_RenderDebugRectWithColorDelegate(left, bottom, right, top, color);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000DB0E File Offset: 0x0000BD0E
		public void RenderDebugSphere(Vec3 position, float radius, uint color, bool depthCheck, float time)
		{
			ScriptingInterfaceOfIDebug.call_RenderDebugSphereDelegate(position, radius, color, depthCheck, time);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000DB24 File Offset: 0x0000BD24
		public void RenderDebugText(float screenX, float screenY, string str, uint color, float time)
		{
			byte[] array = null;
			if (str != null)
			{
				int byteCount = ScriptingInterfaceOfIDebug._utf8.GetByteCount(str);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIDebug._utf8.GetBytes(str, 0, str.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIDebug.call_RenderDebugTextDelegate(screenX, screenY, array, color, time);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000DB84 File Offset: 0x0000BD84
		public void RenderDebugText3d(Vec3 worldPosition, string str, uint color, int screenPosOffsetX, int screenPosOffsetY, float time)
		{
			byte[] array = null;
			if (str != null)
			{
				int byteCount = ScriptingInterfaceOfIDebug._utf8.GetByteCount(str);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIDebug._utf8.GetBytes(str, 0, str.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIDebug.call_RenderDebugText3dDelegate(worldPosition, array, color, screenPosOffsetX, screenPosOffsetY, time);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000DBE6 File Offset: 0x0000BDE6
		public void SetDumpGenerationDisabled(bool Disabled)
		{
			ScriptingInterfaceOfIDebug.call_SetDumpGenerationDisabledDelegate(Disabled);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
		public void SetErrorReportScene(UIntPtr scenePointer)
		{
			ScriptingInterfaceOfIDebug.call_SetErrorReportSceneDelegate(scenePointer);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000DC00 File Offset: 0x0000BE00
		public void SetShowDebugInfo(int value)
		{
			ScriptingInterfaceOfIDebug.call_SetShowDebugInfoDelegate(value);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000DC10 File Offset: 0x0000BE10
		public bool SilentAssert(string messageString, string callerFile, string callerMethod, int callerLine, bool getDump)
		{
			byte[] array = null;
			if (messageString != null)
			{
				int byteCount = ScriptingInterfaceOfIDebug._utf8.GetByteCount(messageString);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIDebug._utf8.GetBytes(messageString, 0, messageString.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (callerFile != null)
			{
				int byteCount2 = ScriptingInterfaceOfIDebug._utf8.GetByteCount(callerFile);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIDebug._utf8.GetBytes(callerFile, 0, callerFile.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			byte[] array3 = null;
			if (callerMethod != null)
			{
				int byteCount3 = ScriptingInterfaceOfIDebug._utf8.GetByteCount(callerMethod);
				array3 = ((byteCount3 < 1024) ? CallbackStringBufferManager.StringBuffer2 : new byte[byteCount3 + 1]);
				ScriptingInterfaceOfIDebug._utf8.GetBytes(callerMethod, 0, callerMethod.Length, array3, 0);
				array3[byteCount3] = 0;
			}
			return ScriptingInterfaceOfIDebug.call_SilentAssertDelegate(array, array2, array3, callerLine, getDump);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000DCFC File Offset: 0x0000BEFC
		public bool Warning(string MessageString)
		{
			byte[] array = null;
			if (MessageString != null)
			{
				int byteCount = ScriptingInterfaceOfIDebug._utf8.GetByteCount(MessageString);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIDebug._utf8.GetBytes(MessageString, 0, MessageString.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIDebug.call_WarningDelegate(array);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000DD58 File Offset: 0x0000BF58
		public void WriteDebugLineOnScreen(string line)
		{
			byte[] array = null;
			if (line != null)
			{
				int byteCount = ScriptingInterfaceOfIDebug._utf8.GetByteCount(line);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIDebug._utf8.GetBytes(line, 0, line.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIDebug.call_WriteDebugLineOnScreenDelegate(array);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000DDB4 File Offset: 0x0000BFB4
		public void WriteLine(int logLevel, string line, int color, ulong filter)
		{
			byte[] array = null;
			if (line != null)
			{
				int byteCount = ScriptingInterfaceOfIDebug._utf8.GetByteCount(line);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIDebug._utf8.GetBytes(line, 0, line.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIDebug.call_WriteLineDelegate(logLevel, array, color, filter);
		}

		// Token: 0x04000072 RID: 114
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000073 RID: 115
		public static ScriptingInterfaceOfIDebug.AbortGameDelegate call_AbortGameDelegate;

		// Token: 0x04000074 RID: 116
		public static ScriptingInterfaceOfIDebug.AssertMemoryUsageDelegate call_AssertMemoryUsageDelegate;

		// Token: 0x04000075 RID: 117
		public static ScriptingInterfaceOfIDebug.ClearAllDebugRenderObjectsDelegate call_ClearAllDebugRenderObjectsDelegate;

		// Token: 0x04000076 RID: 118
		public static ScriptingInterfaceOfIDebug.ContentWarningDelegate call_ContentWarningDelegate;

		// Token: 0x04000077 RID: 119
		public static ScriptingInterfaceOfIDebug.EchoCommandWindowDelegate call_EchoCommandWindowDelegate;

		// Token: 0x04000078 RID: 120
		public static ScriptingInterfaceOfIDebug.ErrorDelegate call_ErrorDelegate;

		// Token: 0x04000079 RID: 121
		public static ScriptingInterfaceOfIDebug.FailedAssertDelegate call_FailedAssertDelegate;

		// Token: 0x0400007A RID: 122
		public static ScriptingInterfaceOfIDebug.GetDebugVectorDelegate call_GetDebugVectorDelegate;

		// Token: 0x0400007B RID: 123
		public static ScriptingInterfaceOfIDebug.GetShowDebugInfoDelegate call_GetShowDebugInfoDelegate;

		// Token: 0x0400007C RID: 124
		public static ScriptingInterfaceOfIDebug.IsErrorReportModeActiveDelegate call_IsErrorReportModeActiveDelegate;

		// Token: 0x0400007D RID: 125
		public static ScriptingInterfaceOfIDebug.IsErrorReportModePauseMissionDelegate call_IsErrorReportModePauseMissionDelegate;

		// Token: 0x0400007E RID: 126
		public static ScriptingInterfaceOfIDebug.IsTestModeDelegate call_IsTestModeDelegate;

		// Token: 0x0400007F RID: 127
		public static ScriptingInterfaceOfIDebug.MessageBoxDelegate call_MessageBoxDelegate;

		// Token: 0x04000080 RID: 128
		public static ScriptingInterfaceOfIDebug.PostWarningLineDelegate call_PostWarningLineDelegate;

		// Token: 0x04000081 RID: 129
		public static ScriptingInterfaceOfIDebug.RenderDebugBoxObjectDelegate call_RenderDebugBoxObjectDelegate;

		// Token: 0x04000082 RID: 130
		public static ScriptingInterfaceOfIDebug.RenderDebugBoxObjectWithFrameDelegate call_RenderDebugBoxObjectWithFrameDelegate;

		// Token: 0x04000083 RID: 131
		public static ScriptingInterfaceOfIDebug.RenderDebugCapsuleDelegate call_RenderDebugCapsuleDelegate;

		// Token: 0x04000084 RID: 132
		public static ScriptingInterfaceOfIDebug.RenderDebugDirectionArrowDelegate call_RenderDebugDirectionArrowDelegate;

		// Token: 0x04000085 RID: 133
		public static ScriptingInterfaceOfIDebug.RenderDebugFrameDelegate call_RenderDebugFrameDelegate;

		// Token: 0x04000086 RID: 134
		public static ScriptingInterfaceOfIDebug.RenderDebugLineDelegate call_RenderDebugLineDelegate;

		// Token: 0x04000087 RID: 135
		public static ScriptingInterfaceOfIDebug.RenderDebugRectDelegate call_RenderDebugRectDelegate;

		// Token: 0x04000088 RID: 136
		public static ScriptingInterfaceOfIDebug.RenderDebugRectWithColorDelegate call_RenderDebugRectWithColorDelegate;

		// Token: 0x04000089 RID: 137
		public static ScriptingInterfaceOfIDebug.RenderDebugSphereDelegate call_RenderDebugSphereDelegate;

		// Token: 0x0400008A RID: 138
		public static ScriptingInterfaceOfIDebug.RenderDebugTextDelegate call_RenderDebugTextDelegate;

		// Token: 0x0400008B RID: 139
		public static ScriptingInterfaceOfIDebug.RenderDebugText3dDelegate call_RenderDebugText3dDelegate;

		// Token: 0x0400008C RID: 140
		public static ScriptingInterfaceOfIDebug.SetDumpGenerationDisabledDelegate call_SetDumpGenerationDisabledDelegate;

		// Token: 0x0400008D RID: 141
		public static ScriptingInterfaceOfIDebug.SetErrorReportSceneDelegate call_SetErrorReportSceneDelegate;

		// Token: 0x0400008E RID: 142
		public static ScriptingInterfaceOfIDebug.SetShowDebugInfoDelegate call_SetShowDebugInfoDelegate;

		// Token: 0x0400008F RID: 143
		public static ScriptingInterfaceOfIDebug.SilentAssertDelegate call_SilentAssertDelegate;

		// Token: 0x04000090 RID: 144
		public static ScriptingInterfaceOfIDebug.WarningDelegate call_WarningDelegate;

		// Token: 0x04000091 RID: 145
		public static ScriptingInterfaceOfIDebug.WriteDebugLineOnScreenDelegate call_WriteDebugLineOnScreenDelegate;

		// Token: 0x04000092 RID: 146
		public static ScriptingInterfaceOfIDebug.WriteLineDelegate call_WriteLineDelegate;

		// Token: 0x020000DE RID: 222
		// (Invoke) Token: 0x0600087B RID: 2171
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AbortGameDelegate(int ExitCode);

		// Token: 0x020000DF RID: 223
		// (Invoke) Token: 0x0600087F RID: 2175
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AssertMemoryUsageDelegate(int memoryMB);

		// Token: 0x020000E0 RID: 224
		// (Invoke) Token: 0x06000883 RID: 2179
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearAllDebugRenderObjectsDelegate();

		// Token: 0x020000E1 RID: 225
		// (Invoke) Token: 0x06000887 RID: 2183
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool ContentWarningDelegate(byte[] MessageString);

		// Token: 0x020000E2 RID: 226
		// (Invoke) Token: 0x0600088B RID: 2187
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void EchoCommandWindowDelegate(byte[] content);

		// Token: 0x020000E3 RID: 227
		// (Invoke) Token: 0x0600088F RID: 2191
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool ErrorDelegate(byte[] MessageString);

		// Token: 0x020000E4 RID: 228
		// (Invoke) Token: 0x06000893 RID: 2195
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool FailedAssertDelegate(byte[] messageString, byte[] callerFile, byte[] callerMethod, int callerLine);

		// Token: 0x020000E5 RID: 229
		// (Invoke) Token: 0x06000897 RID: 2199
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetDebugVectorDelegate();

		// Token: 0x020000E6 RID: 230
		// (Invoke) Token: 0x0600089B RID: 2203
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetShowDebugInfoDelegate();

		// Token: 0x020000E7 RID: 231
		// (Invoke) Token: 0x0600089F RID: 2207
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsErrorReportModeActiveDelegate();

		// Token: 0x020000E8 RID: 232
		// (Invoke) Token: 0x060008A3 RID: 2211
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsErrorReportModePauseMissionDelegate();

		// Token: 0x020000E9 RID: 233
		// (Invoke) Token: 0x060008A7 RID: 2215
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsTestModeDelegate();

		// Token: 0x020000EA RID: 234
		// (Invoke) Token: 0x060008AB RID: 2219
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int MessageBoxDelegate(byte[] lpText, byte[] lpCaption, uint uType);

		// Token: 0x020000EB RID: 235
		// (Invoke) Token: 0x060008AF RID: 2223
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PostWarningLineDelegate(byte[] line);

		// Token: 0x020000EC RID: 236
		// (Invoke) Token: 0x060008B3 RID: 2227
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RenderDebugBoxObjectDelegate(Vec3 min, Vec3 max, uint color, [MarshalAs(UnmanagedType.U1)] bool depthCheck, float time);

		// Token: 0x020000ED RID: 237
		// (Invoke) Token: 0x060008B7 RID: 2231
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RenderDebugBoxObjectWithFrameDelegate(Vec3 min, Vec3 max, ref MatrixFrame frame, uint color, [MarshalAs(UnmanagedType.U1)] bool depthCheck, float time);

		// Token: 0x020000EE RID: 238
		// (Invoke) Token: 0x060008BB RID: 2235
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RenderDebugCapsuleDelegate(Vec3 p0, Vec3 p1, float radius, uint color, [MarshalAs(UnmanagedType.U1)] bool depthCheck, float time);

		// Token: 0x020000EF RID: 239
		// (Invoke) Token: 0x060008BF RID: 2239
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RenderDebugDirectionArrowDelegate(Vec3 position, Vec3 direction, uint color, [MarshalAs(UnmanagedType.U1)] bool depthCheck);

		// Token: 0x020000F0 RID: 240
		// (Invoke) Token: 0x060008C3 RID: 2243
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RenderDebugFrameDelegate(ref MatrixFrame frame, float lineLength, float time);

		// Token: 0x020000F1 RID: 241
		// (Invoke) Token: 0x060008C7 RID: 2247
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RenderDebugLineDelegate(Vec3 position, Vec3 direction, uint color, [MarshalAs(UnmanagedType.U1)] bool depthCheck, float time);

		// Token: 0x020000F2 RID: 242
		// (Invoke) Token: 0x060008CB RID: 2251
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RenderDebugRectDelegate(float left, float bottom, float right, float top);

		// Token: 0x020000F3 RID: 243
		// (Invoke) Token: 0x060008CF RID: 2255
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RenderDebugRectWithColorDelegate(float left, float bottom, float right, float top, uint color);

		// Token: 0x020000F4 RID: 244
		// (Invoke) Token: 0x060008D3 RID: 2259
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RenderDebugSphereDelegate(Vec3 position, float radius, uint color, [MarshalAs(UnmanagedType.U1)] bool depthCheck, float time);

		// Token: 0x020000F5 RID: 245
		// (Invoke) Token: 0x060008D7 RID: 2263
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RenderDebugTextDelegate(float screenX, float screenY, byte[] str, uint color, float time);

		// Token: 0x020000F6 RID: 246
		// (Invoke) Token: 0x060008DB RID: 2267
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RenderDebugText3dDelegate(Vec3 worldPosition, byte[] str, uint color, int screenPosOffsetX, int screenPosOffsetY, float time);

		// Token: 0x020000F7 RID: 247
		// (Invoke) Token: 0x060008DF RID: 2271
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetDumpGenerationDisabledDelegate([MarshalAs(UnmanagedType.U1)] bool Disabled);

		// Token: 0x020000F8 RID: 248
		// (Invoke) Token: 0x060008E3 RID: 2275
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetErrorReportSceneDelegate(UIntPtr scenePointer);

		// Token: 0x020000F9 RID: 249
		// (Invoke) Token: 0x060008E7 RID: 2279
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetShowDebugInfoDelegate(int value);

		// Token: 0x020000FA RID: 250
		// (Invoke) Token: 0x060008EB RID: 2283
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool SilentAssertDelegate(byte[] messageString, byte[] callerFile, byte[] callerMethod, int callerLine, [MarshalAs(UnmanagedType.U1)] bool getDump);

		// Token: 0x020000FB RID: 251
		// (Invoke) Token: 0x060008EF RID: 2287
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool WarningDelegate(byte[] MessageString);

		// Token: 0x020000FC RID: 252
		// (Invoke) Token: 0x060008F3 RID: 2291
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void WriteDebugLineOnScreenDelegate(byte[] line);

		// Token: 0x020000FD RID: 253
		// (Invoke) Token: 0x060008F7 RID: 2295
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void WriteLineDelegate(int logLevel, byte[] line, int color, ulong filter);
	}
}

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x02000028 RID: 40
	internal class ScriptingInterfaceOfISoundEvent : ISoundEvent
	{
		// Token: 0x060004AE RID: 1198 RVA: 0x00015369 File Offset: 0x00013569
		public int CreateEvent(int fmodEventIndex, UIntPtr scene)
		{
			return ScriptingInterfaceOfISoundEvent.call_CreateEventDelegate(fmodEventIndex, scene);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00015378 File Offset: 0x00013578
		public int CreateEventFromExternalFile(string programmerSoundEventName, string filePath, UIntPtr scene)
		{
			byte[] array = null;
			if (programmerSoundEventName != null)
			{
				int byteCount = ScriptingInterfaceOfISoundEvent._utf8.GetByteCount(programmerSoundEventName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfISoundEvent._utf8.GetBytes(programmerSoundEventName, 0, programmerSoundEventName.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (filePath != null)
			{
				int byteCount2 = ScriptingInterfaceOfISoundEvent._utf8.GetByteCount(filePath);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfISoundEvent._utf8.GetBytes(filePath, 0, filePath.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			return ScriptingInterfaceOfISoundEvent.call_CreateEventFromExternalFileDelegate(array, array2, scene);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00015418 File Offset: 0x00013618
		public int CreateEventFromSoundBuffer(string programmerSoundEventName, byte[] soundBuffer, UIntPtr scene)
		{
			byte[] array = null;
			if (programmerSoundEventName != null)
			{
				int byteCount = ScriptingInterfaceOfISoundEvent._utf8.GetByteCount(programmerSoundEventName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfISoundEvent._utf8.GetBytes(programmerSoundEventName, 0, programmerSoundEventName.Length, array, 0);
				array[byteCount] = 0;
			}
			PinnedArrayData<byte> pinnedArrayData = new PinnedArrayData<byte>(soundBuffer, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ManagedArray soundBuffer2 = new ManagedArray(pointer, (soundBuffer != null) ? soundBuffer.Length : 0);
			int result = ScriptingInterfaceOfISoundEvent.call_CreateEventFromSoundBufferDelegate(array, soundBuffer2, scene);
			pinnedArrayData.Dispose();
			return result;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000154A4 File Offset: 0x000136A4
		public int CreateEventFromString(string eventName, UIntPtr scene)
		{
			byte[] array = null;
			if (eventName != null)
			{
				int byteCount = ScriptingInterfaceOfISoundEvent._utf8.GetByteCount(eventName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfISoundEvent._utf8.GetBytes(eventName, 0, eventName.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfISoundEvent.call_CreateEventFromStringDelegate(array, scene);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00015500 File Offset: 0x00013700
		public int GetEventIdFromString(string eventName)
		{
			byte[] array = null;
			if (eventName != null)
			{
				int byteCount = ScriptingInterfaceOfISoundEvent._utf8.GetByteCount(eventName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfISoundEvent._utf8.GetBytes(eventName, 0, eventName.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfISoundEvent.call_GetEventIdFromStringDelegate(array);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001555A File Offset: 0x0001375A
		public Vec3 GetEventMinMaxDistance(int eventId)
		{
			return ScriptingInterfaceOfISoundEvent.call_GetEventMinMaxDistanceDelegate(eventId);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00015567 File Offset: 0x00013767
		public int GetTotalEventCount()
		{
			return ScriptingInterfaceOfISoundEvent.call_GetTotalEventCountDelegate();
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00015573 File Offset: 0x00013773
		public bool IsPaused(int eventId)
		{
			return ScriptingInterfaceOfISoundEvent.call_IsPausedDelegate(eventId);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00015580 File Offset: 0x00013780
		public bool IsPlaying(int eventId)
		{
			return ScriptingInterfaceOfISoundEvent.call_IsPlayingDelegate(eventId);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001558D File Offset: 0x0001378D
		public bool IsValid(int eventId)
		{
			return ScriptingInterfaceOfISoundEvent.call_IsValidDelegate(eventId);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001559A File Offset: 0x0001379A
		public void PauseEvent(int eventId)
		{
			ScriptingInterfaceOfISoundEvent.call_PauseEventDelegate(eventId);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000155A8 File Offset: 0x000137A8
		public void PlayExtraEvent(int soundId, string eventName)
		{
			byte[] array = null;
			if (eventName != null)
			{
				int byteCount = ScriptingInterfaceOfISoundEvent._utf8.GetByteCount(eventName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfISoundEvent._utf8.GetBytes(eventName, 0, eventName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfISoundEvent.call_PlayExtraEventDelegate(soundId, array);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00015603 File Offset: 0x00013803
		public bool PlaySound2D(int fmodEventIndex)
		{
			return ScriptingInterfaceOfISoundEvent.call_PlaySound2DDelegate(fmodEventIndex);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00015610 File Offset: 0x00013810
		public void ReleaseEvent(int eventId)
		{
			ScriptingInterfaceOfISoundEvent.call_ReleaseEventDelegate(eventId);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001561D File Offset: 0x0001381D
		public void ResumeEvent(int eventId)
		{
			ScriptingInterfaceOfISoundEvent.call_ResumeEventDelegate(eventId);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001562A File Offset: 0x0001382A
		public void SetEventMinMaxDistance(int fmodEventIndex, Vec3 radius)
		{
			ScriptingInterfaceOfISoundEvent.call_SetEventMinMaxDistanceDelegate(fmodEventIndex, radius);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00015638 File Offset: 0x00013838
		public void SetEventParameterAtIndex(int soundId, int parameterIndex, float value)
		{
			ScriptingInterfaceOfISoundEvent.call_SetEventParameterAtIndexDelegate(soundId, parameterIndex, value);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00015648 File Offset: 0x00013848
		public void SetEventParameterFromString(int eventId, string name, float value)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfISoundEvent._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfISoundEvent._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfISoundEvent.call_SetEventParameterFromStringDelegate(eventId, array, value);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000156A4 File Offset: 0x000138A4
		public void SetEventPosition(int eventId, ref Vec3 position)
		{
			ScriptingInterfaceOfISoundEvent.call_SetEventPositionDelegate(eventId, ref position);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000156B2 File Offset: 0x000138B2
		public void SetEventVelocity(int eventId, ref Vec3 velocity)
		{
			ScriptingInterfaceOfISoundEvent.call_SetEventVelocityDelegate(eventId, ref velocity);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000156C0 File Offset: 0x000138C0
		public void SetSwitch(int soundId, string switchGroupName, string newSwitchStateName)
		{
			byte[] array = null;
			if (switchGroupName != null)
			{
				int byteCount = ScriptingInterfaceOfISoundEvent._utf8.GetByteCount(switchGroupName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfISoundEvent._utf8.GetBytes(switchGroupName, 0, switchGroupName.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (newSwitchStateName != null)
			{
				int byteCount2 = ScriptingInterfaceOfISoundEvent._utf8.GetByteCount(newSwitchStateName);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfISoundEvent._utf8.GetBytes(newSwitchStateName, 0, newSwitchStateName.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			ScriptingInterfaceOfISoundEvent.call_SetSwitchDelegate(soundId, array, array2);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001575E File Offset: 0x0001395E
		public bool StartEvent(int eventId)
		{
			return ScriptingInterfaceOfISoundEvent.call_StartEventDelegate(eventId);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001576B File Offset: 0x0001396B
		public bool StartEventInPosition(int eventId, ref Vec3 position)
		{
			return ScriptingInterfaceOfISoundEvent.call_StartEventInPositionDelegate(eventId, ref position);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00015779 File Offset: 0x00013979
		public void StopEvent(int eventId)
		{
			ScriptingInterfaceOfISoundEvent.call_StopEventDelegate(eventId);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00015786 File Offset: 0x00013986
		public void TriggerCue(int eventId)
		{
			ScriptingInterfaceOfISoundEvent.call_TriggerCueDelegate(eventId);
		}

		// Token: 0x0400043B RID: 1083
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x0400043C RID: 1084
		public static ScriptingInterfaceOfISoundEvent.CreateEventDelegate call_CreateEventDelegate;

		// Token: 0x0400043D RID: 1085
		public static ScriptingInterfaceOfISoundEvent.CreateEventFromExternalFileDelegate call_CreateEventFromExternalFileDelegate;

		// Token: 0x0400043E RID: 1086
		public static ScriptingInterfaceOfISoundEvent.CreateEventFromSoundBufferDelegate call_CreateEventFromSoundBufferDelegate;

		// Token: 0x0400043F RID: 1087
		public static ScriptingInterfaceOfISoundEvent.CreateEventFromStringDelegate call_CreateEventFromStringDelegate;

		// Token: 0x04000440 RID: 1088
		public static ScriptingInterfaceOfISoundEvent.GetEventIdFromStringDelegate call_GetEventIdFromStringDelegate;

		// Token: 0x04000441 RID: 1089
		public static ScriptingInterfaceOfISoundEvent.GetEventMinMaxDistanceDelegate call_GetEventMinMaxDistanceDelegate;

		// Token: 0x04000442 RID: 1090
		public static ScriptingInterfaceOfISoundEvent.GetTotalEventCountDelegate call_GetTotalEventCountDelegate;

		// Token: 0x04000443 RID: 1091
		public static ScriptingInterfaceOfISoundEvent.IsPausedDelegate call_IsPausedDelegate;

		// Token: 0x04000444 RID: 1092
		public static ScriptingInterfaceOfISoundEvent.IsPlayingDelegate call_IsPlayingDelegate;

		// Token: 0x04000445 RID: 1093
		public static ScriptingInterfaceOfISoundEvent.IsValidDelegate call_IsValidDelegate;

		// Token: 0x04000446 RID: 1094
		public static ScriptingInterfaceOfISoundEvent.PauseEventDelegate call_PauseEventDelegate;

		// Token: 0x04000447 RID: 1095
		public static ScriptingInterfaceOfISoundEvent.PlayExtraEventDelegate call_PlayExtraEventDelegate;

		// Token: 0x04000448 RID: 1096
		public static ScriptingInterfaceOfISoundEvent.PlaySound2DDelegate call_PlaySound2DDelegate;

		// Token: 0x04000449 RID: 1097
		public static ScriptingInterfaceOfISoundEvent.ReleaseEventDelegate call_ReleaseEventDelegate;

		// Token: 0x0400044A RID: 1098
		public static ScriptingInterfaceOfISoundEvent.ResumeEventDelegate call_ResumeEventDelegate;

		// Token: 0x0400044B RID: 1099
		public static ScriptingInterfaceOfISoundEvent.SetEventMinMaxDistanceDelegate call_SetEventMinMaxDistanceDelegate;

		// Token: 0x0400044C RID: 1100
		public static ScriptingInterfaceOfISoundEvent.SetEventParameterAtIndexDelegate call_SetEventParameterAtIndexDelegate;

		// Token: 0x0400044D RID: 1101
		public static ScriptingInterfaceOfISoundEvent.SetEventParameterFromStringDelegate call_SetEventParameterFromStringDelegate;

		// Token: 0x0400044E RID: 1102
		public static ScriptingInterfaceOfISoundEvent.SetEventPositionDelegate call_SetEventPositionDelegate;

		// Token: 0x0400044F RID: 1103
		public static ScriptingInterfaceOfISoundEvent.SetEventVelocityDelegate call_SetEventVelocityDelegate;

		// Token: 0x04000450 RID: 1104
		public static ScriptingInterfaceOfISoundEvent.SetSwitchDelegate call_SetSwitchDelegate;

		// Token: 0x04000451 RID: 1105
		public static ScriptingInterfaceOfISoundEvent.StartEventDelegate call_StartEventDelegate;

		// Token: 0x04000452 RID: 1106
		public static ScriptingInterfaceOfISoundEvent.StartEventInPositionDelegate call_StartEventInPositionDelegate;

		// Token: 0x04000453 RID: 1107
		public static ScriptingInterfaceOfISoundEvent.StopEventDelegate call_StopEventDelegate;

		// Token: 0x04000454 RID: 1108
		public static ScriptingInterfaceOfISoundEvent.TriggerCueDelegate call_TriggerCueDelegate;

		// Token: 0x0200048D RID: 1165
		// (Invoke) Token: 0x06001737 RID: 5943
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int CreateEventDelegate(int fmodEventIndex, UIntPtr scene);

		// Token: 0x0200048E RID: 1166
		// (Invoke) Token: 0x0600173B RID: 5947
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int CreateEventFromExternalFileDelegate(byte[] programmerSoundEventName, byte[] filePath, UIntPtr scene);

		// Token: 0x0200048F RID: 1167
		// (Invoke) Token: 0x0600173F RID: 5951
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int CreateEventFromSoundBufferDelegate(byte[] programmerSoundEventName, ManagedArray soundBuffer, UIntPtr scene);

		// Token: 0x02000490 RID: 1168
		// (Invoke) Token: 0x06001743 RID: 5955
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int CreateEventFromStringDelegate(byte[] eventName, UIntPtr scene);

		// Token: 0x02000491 RID: 1169
		// (Invoke) Token: 0x06001747 RID: 5959
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetEventIdFromStringDelegate(byte[] eventName);

		// Token: 0x02000492 RID: 1170
		// (Invoke) Token: 0x0600174B RID: 5963
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetEventMinMaxDistanceDelegate(int eventId);

		// Token: 0x02000493 RID: 1171
		// (Invoke) Token: 0x0600174F RID: 5967
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetTotalEventCountDelegate();

		// Token: 0x02000494 RID: 1172
		// (Invoke) Token: 0x06001753 RID: 5971
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsPausedDelegate(int eventId);

		// Token: 0x02000495 RID: 1173
		// (Invoke) Token: 0x06001757 RID: 5975
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsPlayingDelegate(int eventId);

		// Token: 0x02000496 RID: 1174
		// (Invoke) Token: 0x0600175B RID: 5979
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsValidDelegate(int eventId);

		// Token: 0x02000497 RID: 1175
		// (Invoke) Token: 0x0600175F RID: 5983
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PauseEventDelegate(int eventId);

		// Token: 0x02000498 RID: 1176
		// (Invoke) Token: 0x06001763 RID: 5987
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PlayExtraEventDelegate(int soundId, byte[] eventName);

		// Token: 0x02000499 RID: 1177
		// (Invoke) Token: 0x06001767 RID: 5991
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool PlaySound2DDelegate(int fmodEventIndex);

		// Token: 0x0200049A RID: 1178
		// (Invoke) Token: 0x0600176B RID: 5995
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReleaseEventDelegate(int eventId);

		// Token: 0x0200049B RID: 1179
		// (Invoke) Token: 0x0600176F RID: 5999
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ResumeEventDelegate(int eventId);

		// Token: 0x0200049C RID: 1180
		// (Invoke) Token: 0x06001773 RID: 6003
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetEventMinMaxDistanceDelegate(int fmodEventIndex, Vec3 radius);

		// Token: 0x0200049D RID: 1181
		// (Invoke) Token: 0x06001777 RID: 6007
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetEventParameterAtIndexDelegate(int soundId, int parameterIndex, float value);

		// Token: 0x0200049E RID: 1182
		// (Invoke) Token: 0x0600177B RID: 6011
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetEventParameterFromStringDelegate(int eventId, byte[] name, float value);

		// Token: 0x0200049F RID: 1183
		// (Invoke) Token: 0x0600177F RID: 6015
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetEventPositionDelegate(int eventId, ref Vec3 position);

		// Token: 0x020004A0 RID: 1184
		// (Invoke) Token: 0x06001783 RID: 6019
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetEventVelocityDelegate(int eventId, ref Vec3 velocity);

		// Token: 0x020004A1 RID: 1185
		// (Invoke) Token: 0x06001787 RID: 6023
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSwitchDelegate(int soundId, byte[] switchGroupName, byte[] newSwitchStateName);

		// Token: 0x020004A2 RID: 1186
		// (Invoke) Token: 0x0600178B RID: 6027
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool StartEventDelegate(int eventId);

		// Token: 0x020004A3 RID: 1187
		// (Invoke) Token: 0x0600178F RID: 6031
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool StartEventInPositionDelegate(int eventId, ref Vec3 position);

		// Token: 0x020004A4 RID: 1188
		// (Invoke) Token: 0x06001793 RID: 6035
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void StopEventDelegate(int eventId);

		// Token: 0x020004A5 RID: 1189
		// (Invoke) Token: 0x06001797 RID: 6039
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TriggerCueDelegate(int eventId);
	}
}

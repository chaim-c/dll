using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x02000029 RID: 41
	internal class ScriptingInterfaceOfISoundManager : ISoundManager
	{
		// Token: 0x060004C9 RID: 1225 RVA: 0x000157A7 File Offset: 0x000139A7
		public void AddSoundClientWithId(ulong client_id)
		{
			ScriptingInterfaceOfISoundManager.call_AddSoundClientWithIdDelegate(client_id);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000157B4 File Offset: 0x000139B4
		public void AddXBOXRemoteUser(ulong XUID, ulong deviceID, bool canSendMicSound, bool canSendTextSound, bool canSendText, bool canReceiveSound, bool canReceiveText)
		{
			ScriptingInterfaceOfISoundManager.call_AddXBOXRemoteUserDelegate(XUID, deviceID, canSendMicSound, canSendTextSound, canSendText, canReceiveSound, canReceiveText);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000157CB File Offset: 0x000139CB
		public void ApplyPushToTalk(bool pushed)
		{
			ScriptingInterfaceOfISoundManager.call_ApplyPushToTalkDelegate(pushed);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000157D8 File Offset: 0x000139D8
		public void ClearDataToBeSent()
		{
			ScriptingInterfaceOfISoundManager.call_ClearDataToBeSentDelegate();
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000157E4 File Offset: 0x000139E4
		public void ClearXBOXSoundManager()
		{
			ScriptingInterfaceOfISoundManager.call_ClearXBOXSoundManagerDelegate();
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000157F0 File Offset: 0x000139F0
		public void CompressData(ulong clientID, byte[] buffer, int length, byte[] compressedBuffer, ref int compressedBufferLength)
		{
			PinnedArrayData<byte> pinnedArrayData = new PinnedArrayData<byte>(buffer, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ManagedArray buffer2 = new ManagedArray(pointer, (buffer != null) ? buffer.Length : 0);
			PinnedArrayData<byte> pinnedArrayData2 = new PinnedArrayData<byte>(compressedBuffer, false);
			IntPtr pointer2 = pinnedArrayData2.Pointer;
			ManagedArray compressedBuffer2 = new ManagedArray(pointer2, (compressedBuffer != null) ? compressedBuffer.Length : 0);
			ScriptingInterfaceOfISoundManager.call_CompressDataDelegate(clientID, buffer2, length, compressedBuffer2, ref compressedBufferLength);
			pinnedArrayData.Dispose();
			pinnedArrayData2.Dispose();
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00015865 File Offset: 0x00013A65
		public void CreateVoiceEvent()
		{
			ScriptingInterfaceOfISoundManager.call_CreateVoiceEventDelegate();
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00015874 File Offset: 0x00013A74
		public void DecompressData(ulong clientID, byte[] compressedBuffer, int compressedBufferLength, byte[] decompressedBuffer, ref int decompressedBufferLength)
		{
			PinnedArrayData<byte> pinnedArrayData = new PinnedArrayData<byte>(compressedBuffer, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ManagedArray compressedBuffer2 = new ManagedArray(pointer, (compressedBuffer != null) ? compressedBuffer.Length : 0);
			PinnedArrayData<byte> pinnedArrayData2 = new PinnedArrayData<byte>(decompressedBuffer, false);
			IntPtr pointer2 = pinnedArrayData2.Pointer;
			ManagedArray decompressedBuffer2 = new ManagedArray(pointer2, (decompressedBuffer != null) ? decompressedBuffer.Length : 0);
			ScriptingInterfaceOfISoundManager.call_DecompressDataDelegate(clientID, compressedBuffer2, compressedBufferLength, decompressedBuffer2, ref decompressedBufferLength);
			pinnedArrayData.Dispose();
			pinnedArrayData2.Dispose();
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000158E9 File Offset: 0x00013AE9
		public void DeleteSoundClientWithId(ulong client_id)
		{
			ScriptingInterfaceOfISoundManager.call_DeleteSoundClientWithIdDelegate(client_id);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000158F6 File Offset: 0x00013AF6
		public void DestroyVoiceEvent(int id)
		{
			ScriptingInterfaceOfISoundManager.call_DestroyVoiceEventDelegate(id);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00015903 File Offset: 0x00013B03
		public void FinalizeVoicePlayEvent()
		{
			ScriptingInterfaceOfISoundManager.call_FinalizeVoicePlayEventDelegate();
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001590F File Offset: 0x00013B0F
		public void GetAttenuationPosition(out Vec3 result)
		{
			ScriptingInterfaceOfISoundManager.call_GetAttenuationPositionDelegate(out result);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001591C File Offset: 0x00013B1C
		public bool GetDataToBeSentAt(int index, byte[] buffer, ulong[] receivers, ref bool transportGuaranteed)
		{
			PinnedArrayData<byte> pinnedArrayData = new PinnedArrayData<byte>(buffer, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ManagedArray buffer2 = new ManagedArray(pointer, (buffer != null) ? buffer.Length : 0);
			PinnedArrayData<ulong> pinnedArrayData2 = new PinnedArrayData<ulong>(receivers, false);
			IntPtr pointer2 = pinnedArrayData2.Pointer;
			bool result = ScriptingInterfaceOfISoundManager.call_GetDataToBeSentAtDelegate(index, buffer2, pointer2, ref transportGuaranteed);
			pinnedArrayData.Dispose();
			pinnedArrayData2.Dispose();
			return result;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001597C File Offset: 0x00013B7C
		public int GetGlobalIndexOfEvent(string eventFullName)
		{
			byte[] array = null;
			if (eventFullName != null)
			{
				int byteCount = ScriptingInterfaceOfISoundManager._utf8.GetByteCount(eventFullName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfISoundManager._utf8.GetBytes(eventFullName, 0, eventFullName.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfISoundManager.call_GetGlobalIndexOfEventDelegate(array);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000159D6 File Offset: 0x00013BD6
		public void GetListenerFrame(out MatrixFrame result)
		{
			ScriptingInterfaceOfISoundManager.call_GetListenerFrameDelegate(out result);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000159E3 File Offset: 0x00013BE3
		public void GetSizeOfDataToBeSentAt(int index, ref uint byte_count, ref uint numReceivers)
		{
			ScriptingInterfaceOfISoundManager.call_GetSizeOfDataToBeSentAtDelegate(index, ref byte_count, ref numReceivers);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000159F4 File Offset: 0x00013BF4
		public void GetVoiceData(byte[] voiceBuffer, int chunkSize, ref int readBytesLength)
		{
			PinnedArrayData<byte> pinnedArrayData = new PinnedArrayData<byte>(voiceBuffer, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ManagedArray voiceBuffer2 = new ManagedArray(pointer, (voiceBuffer != null) ? voiceBuffer.Length : 0);
			ScriptingInterfaceOfISoundManager.call_GetVoiceDataDelegate(voiceBuffer2, chunkSize, ref readBytesLength);
			pinnedArrayData.Dispose();
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00015A37 File Offset: 0x00013C37
		public void HandleStateChanges()
		{
			ScriptingInterfaceOfISoundManager.call_HandleStateChangesDelegate();
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00015A43 File Offset: 0x00013C43
		public void InitializeVoicePlayEvent()
		{
			ScriptingInterfaceOfISoundManager.call_InitializeVoicePlayEventDelegate();
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00015A4F File Offset: 0x00013C4F
		public void InitializeXBOXSoundManager()
		{
			ScriptingInterfaceOfISoundManager.call_InitializeXBOXSoundManagerDelegate();
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00015A5C File Offset: 0x00013C5C
		public void LoadEventFileAux(string soundBankName, bool decompressSamples)
		{
			byte[] array = null;
			if (soundBankName != null)
			{
				int byteCount = ScriptingInterfaceOfISoundManager._utf8.GetByteCount(soundBankName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfISoundManager._utf8.GetBytes(soundBankName, 0, soundBankName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfISoundManager.call_LoadEventFileAuxDelegate(array, decompressSamples);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00015AB8 File Offset: 0x00013CB8
		public void ProcessDataToBeReceived(ulong senderDeviceID, byte[] data, uint dataSize)
		{
			PinnedArrayData<byte> pinnedArrayData = new PinnedArrayData<byte>(data, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ManagedArray data2 = new ManagedArray(pointer, (data != null) ? data.Length : 0);
			ScriptingInterfaceOfISoundManager.call_ProcessDataToBeReceivedDelegate(senderDeviceID, data2, dataSize);
			pinnedArrayData.Dispose();
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00015AFB File Offset: 0x00013CFB
		public void ProcessDataToBeSent(ref int numData)
		{
			ScriptingInterfaceOfISoundManager.call_ProcessDataToBeSentDelegate(ref numData);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00015B08 File Offset: 0x00013D08
		public void RemoveXBOXRemoteUser(ulong XUID)
		{
			ScriptingInterfaceOfISoundManager.call_RemoveXBOXRemoteUserDelegate(XUID);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00015B15 File Offset: 0x00013D15
		public void Reset()
		{
			ScriptingInterfaceOfISoundManager.call_ResetDelegate();
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00015B24 File Offset: 0x00013D24
		public void SetGlobalParameter(string parameterName, float value)
		{
			byte[] array = null;
			if (parameterName != null)
			{
				int byteCount = ScriptingInterfaceOfISoundManager._utf8.GetByteCount(parameterName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfISoundManager._utf8.GetBytes(parameterName, 0, parameterName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfISoundManager.call_SetGlobalParameterDelegate(array, value);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00015B7F File Offset: 0x00013D7F
		public void SetListenerFrame(ref MatrixFrame frame, ref Vec3 attenuationPosition)
		{
			ScriptingInterfaceOfISoundManager.call_SetListenerFrameDelegate(ref frame, ref attenuationPosition);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00015B90 File Offset: 0x00013D90
		public void SetState(string stateGroup, string state)
		{
			byte[] array = null;
			if (stateGroup != null)
			{
				int byteCount = ScriptingInterfaceOfISoundManager._utf8.GetByteCount(stateGroup);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfISoundManager._utf8.GetBytes(stateGroup, 0, stateGroup.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (state != null)
			{
				int byteCount2 = ScriptingInterfaceOfISoundManager._utf8.GetByteCount(state);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfISoundManager._utf8.GetBytes(state, 0, state.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			ScriptingInterfaceOfISoundManager.call_SetStateDelegate(array, array2);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00015C30 File Offset: 0x00013E30
		public bool StartOneShotEvent(string eventFullName, Vec3 position)
		{
			byte[] array = null;
			if (eventFullName != null)
			{
				int byteCount = ScriptingInterfaceOfISoundManager._utf8.GetByteCount(eventFullName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfISoundManager._utf8.GetBytes(eventFullName, 0, eventFullName.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfISoundManager.call_StartOneShotEventDelegate(array, position);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00015C8C File Offset: 0x00013E8C
		public bool StartOneShotEventWithParam(string eventFullName, Vec3 position, string paramName, float paramValue)
		{
			byte[] array = null;
			if (eventFullName != null)
			{
				int byteCount = ScriptingInterfaceOfISoundManager._utf8.GetByteCount(eventFullName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfISoundManager._utf8.GetBytes(eventFullName, 0, eventFullName.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (paramName != null)
			{
				int byteCount2 = ScriptingInterfaceOfISoundManager._utf8.GetByteCount(paramName);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfISoundManager._utf8.GetBytes(paramName, 0, paramName.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			return ScriptingInterfaceOfISoundManager.call_StartOneShotEventWithParamDelegate(array, position, array2, paramValue);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00015D2C File Offset: 0x00013F2C
		public void StartVoiceRecord()
		{
			ScriptingInterfaceOfISoundManager.call_StartVoiceRecordDelegate();
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00015D38 File Offset: 0x00013F38
		public void StopVoiceRecord()
		{
			ScriptingInterfaceOfISoundManager.call_StopVoiceRecordDelegate();
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00015D44 File Offset: 0x00013F44
		public void UpdateVoiceToPlay(byte[] voiceBuffer, int length, int index)
		{
			PinnedArrayData<byte> pinnedArrayData = new PinnedArrayData<byte>(voiceBuffer, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ManagedArray voiceBuffer2 = new ManagedArray(pointer, (voiceBuffer != null) ? voiceBuffer.Length : 0);
			ScriptingInterfaceOfISoundManager.call_UpdateVoiceToPlayDelegate(voiceBuffer2, length, index);
			pinnedArrayData.Dispose();
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00015D87 File Offset: 0x00013F87
		public void UpdateXBOXChatCommunicationFlags(ulong XUID, bool canSendMicSound, bool canSendTextSound, bool canSendText, bool canReceiveSound, bool canReceiveText)
		{
			ScriptingInterfaceOfISoundManager.call_UpdateXBOXChatCommunicationFlagsDelegate(XUID, canSendMicSound, canSendTextSound, canSendText, canReceiveSound, canReceiveText);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00015D9C File Offset: 0x00013F9C
		public void UpdateXBOXLocalUser()
		{
			ScriptingInterfaceOfISoundManager.call_UpdateXBOXLocalUserDelegate();
		}

		// Token: 0x04000455 RID: 1109
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000456 RID: 1110
		public static ScriptingInterfaceOfISoundManager.AddSoundClientWithIdDelegate call_AddSoundClientWithIdDelegate;

		// Token: 0x04000457 RID: 1111
		public static ScriptingInterfaceOfISoundManager.AddXBOXRemoteUserDelegate call_AddXBOXRemoteUserDelegate;

		// Token: 0x04000458 RID: 1112
		public static ScriptingInterfaceOfISoundManager.ApplyPushToTalkDelegate call_ApplyPushToTalkDelegate;

		// Token: 0x04000459 RID: 1113
		public static ScriptingInterfaceOfISoundManager.ClearDataToBeSentDelegate call_ClearDataToBeSentDelegate;

		// Token: 0x0400045A RID: 1114
		public static ScriptingInterfaceOfISoundManager.ClearXBOXSoundManagerDelegate call_ClearXBOXSoundManagerDelegate;

		// Token: 0x0400045B RID: 1115
		public static ScriptingInterfaceOfISoundManager.CompressDataDelegate call_CompressDataDelegate;

		// Token: 0x0400045C RID: 1116
		public static ScriptingInterfaceOfISoundManager.CreateVoiceEventDelegate call_CreateVoiceEventDelegate;

		// Token: 0x0400045D RID: 1117
		public static ScriptingInterfaceOfISoundManager.DecompressDataDelegate call_DecompressDataDelegate;

		// Token: 0x0400045E RID: 1118
		public static ScriptingInterfaceOfISoundManager.DeleteSoundClientWithIdDelegate call_DeleteSoundClientWithIdDelegate;

		// Token: 0x0400045F RID: 1119
		public static ScriptingInterfaceOfISoundManager.DestroyVoiceEventDelegate call_DestroyVoiceEventDelegate;

		// Token: 0x04000460 RID: 1120
		public static ScriptingInterfaceOfISoundManager.FinalizeVoicePlayEventDelegate call_FinalizeVoicePlayEventDelegate;

		// Token: 0x04000461 RID: 1121
		public static ScriptingInterfaceOfISoundManager.GetAttenuationPositionDelegate call_GetAttenuationPositionDelegate;

		// Token: 0x04000462 RID: 1122
		public static ScriptingInterfaceOfISoundManager.GetDataToBeSentAtDelegate call_GetDataToBeSentAtDelegate;

		// Token: 0x04000463 RID: 1123
		public static ScriptingInterfaceOfISoundManager.GetGlobalIndexOfEventDelegate call_GetGlobalIndexOfEventDelegate;

		// Token: 0x04000464 RID: 1124
		public static ScriptingInterfaceOfISoundManager.GetListenerFrameDelegate call_GetListenerFrameDelegate;

		// Token: 0x04000465 RID: 1125
		public static ScriptingInterfaceOfISoundManager.GetSizeOfDataToBeSentAtDelegate call_GetSizeOfDataToBeSentAtDelegate;

		// Token: 0x04000466 RID: 1126
		public static ScriptingInterfaceOfISoundManager.GetVoiceDataDelegate call_GetVoiceDataDelegate;

		// Token: 0x04000467 RID: 1127
		public static ScriptingInterfaceOfISoundManager.HandleStateChangesDelegate call_HandleStateChangesDelegate;

		// Token: 0x04000468 RID: 1128
		public static ScriptingInterfaceOfISoundManager.InitializeVoicePlayEventDelegate call_InitializeVoicePlayEventDelegate;

		// Token: 0x04000469 RID: 1129
		public static ScriptingInterfaceOfISoundManager.InitializeXBOXSoundManagerDelegate call_InitializeXBOXSoundManagerDelegate;

		// Token: 0x0400046A RID: 1130
		public static ScriptingInterfaceOfISoundManager.LoadEventFileAuxDelegate call_LoadEventFileAuxDelegate;

		// Token: 0x0400046B RID: 1131
		public static ScriptingInterfaceOfISoundManager.ProcessDataToBeReceivedDelegate call_ProcessDataToBeReceivedDelegate;

		// Token: 0x0400046C RID: 1132
		public static ScriptingInterfaceOfISoundManager.ProcessDataToBeSentDelegate call_ProcessDataToBeSentDelegate;

		// Token: 0x0400046D RID: 1133
		public static ScriptingInterfaceOfISoundManager.RemoveXBOXRemoteUserDelegate call_RemoveXBOXRemoteUserDelegate;

		// Token: 0x0400046E RID: 1134
		public static ScriptingInterfaceOfISoundManager.ResetDelegate call_ResetDelegate;

		// Token: 0x0400046F RID: 1135
		public static ScriptingInterfaceOfISoundManager.SetGlobalParameterDelegate call_SetGlobalParameterDelegate;

		// Token: 0x04000470 RID: 1136
		public static ScriptingInterfaceOfISoundManager.SetListenerFrameDelegate call_SetListenerFrameDelegate;

		// Token: 0x04000471 RID: 1137
		public static ScriptingInterfaceOfISoundManager.SetStateDelegate call_SetStateDelegate;

		// Token: 0x04000472 RID: 1138
		public static ScriptingInterfaceOfISoundManager.StartOneShotEventDelegate call_StartOneShotEventDelegate;

		// Token: 0x04000473 RID: 1139
		public static ScriptingInterfaceOfISoundManager.StartOneShotEventWithParamDelegate call_StartOneShotEventWithParamDelegate;

		// Token: 0x04000474 RID: 1140
		public static ScriptingInterfaceOfISoundManager.StartVoiceRecordDelegate call_StartVoiceRecordDelegate;

		// Token: 0x04000475 RID: 1141
		public static ScriptingInterfaceOfISoundManager.StopVoiceRecordDelegate call_StopVoiceRecordDelegate;

		// Token: 0x04000476 RID: 1142
		public static ScriptingInterfaceOfISoundManager.UpdateVoiceToPlayDelegate call_UpdateVoiceToPlayDelegate;

		// Token: 0x04000477 RID: 1143
		public static ScriptingInterfaceOfISoundManager.UpdateXBOXChatCommunicationFlagsDelegate call_UpdateXBOXChatCommunicationFlagsDelegate;

		// Token: 0x04000478 RID: 1144
		public static ScriptingInterfaceOfISoundManager.UpdateXBOXLocalUserDelegate call_UpdateXBOXLocalUserDelegate;

		// Token: 0x020004A6 RID: 1190
		// (Invoke) Token: 0x0600179B RID: 6043
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddSoundClientWithIdDelegate(ulong client_id);

		// Token: 0x020004A7 RID: 1191
		// (Invoke) Token: 0x0600179F RID: 6047
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddXBOXRemoteUserDelegate(ulong XUID, ulong deviceID, [MarshalAs(UnmanagedType.U1)] bool canSendMicSound, [MarshalAs(UnmanagedType.U1)] bool canSendTextSound, [MarshalAs(UnmanagedType.U1)] bool canSendText, [MarshalAs(UnmanagedType.U1)] bool canReceiveSound, [MarshalAs(UnmanagedType.U1)] bool canReceiveText);

		// Token: 0x020004A8 RID: 1192
		// (Invoke) Token: 0x060017A3 RID: 6051
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ApplyPushToTalkDelegate([MarshalAs(UnmanagedType.U1)] bool pushed);

		// Token: 0x020004A9 RID: 1193
		// (Invoke) Token: 0x060017A7 RID: 6055
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearDataToBeSentDelegate();

		// Token: 0x020004AA RID: 1194
		// (Invoke) Token: 0x060017AB RID: 6059
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearXBOXSoundManagerDelegate();

		// Token: 0x020004AB RID: 1195
		// (Invoke) Token: 0x060017AF RID: 6063
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void CompressDataDelegate(ulong clientID, ManagedArray buffer, int length, ManagedArray compressedBuffer, ref int compressedBufferLength);

		// Token: 0x020004AC RID: 1196
		// (Invoke) Token: 0x060017B3 RID: 6067
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void CreateVoiceEventDelegate();

		// Token: 0x020004AD RID: 1197
		// (Invoke) Token: 0x060017B7 RID: 6071
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DecompressDataDelegate(ulong clientID, ManagedArray compressedBuffer, int compressedBufferLength, ManagedArray decompressedBuffer, ref int decompressedBufferLength);

		// Token: 0x020004AE RID: 1198
		// (Invoke) Token: 0x060017BB RID: 6075
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DeleteSoundClientWithIdDelegate(ulong client_id);

		// Token: 0x020004AF RID: 1199
		// (Invoke) Token: 0x060017BF RID: 6079
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DestroyVoiceEventDelegate(int id);

		// Token: 0x020004B0 RID: 1200
		// (Invoke) Token: 0x060017C3 RID: 6083
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void FinalizeVoicePlayEventDelegate();

		// Token: 0x020004B1 RID: 1201
		// (Invoke) Token: 0x060017C7 RID: 6087
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetAttenuationPositionDelegate(out Vec3 result);

		// Token: 0x020004B2 RID: 1202
		// (Invoke) Token: 0x060017CB RID: 6091
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetDataToBeSentAtDelegate(int index, ManagedArray buffer, IntPtr receivers, [MarshalAs(UnmanagedType.U1)] ref bool transportGuaranteed);

		// Token: 0x020004B3 RID: 1203
		// (Invoke) Token: 0x060017CF RID: 6095
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetGlobalIndexOfEventDelegate(byte[] eventFullName);

		// Token: 0x020004B4 RID: 1204
		// (Invoke) Token: 0x060017D3 RID: 6099
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetListenerFrameDelegate(out MatrixFrame result);

		// Token: 0x020004B5 RID: 1205
		// (Invoke) Token: 0x060017D7 RID: 6103
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetSizeOfDataToBeSentAtDelegate(int index, ref uint byte_count, ref uint numReceivers);

		// Token: 0x020004B6 RID: 1206
		// (Invoke) Token: 0x060017DB RID: 6107
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetVoiceDataDelegate(ManagedArray voiceBuffer, int chunkSize, ref int readBytesLength);

		// Token: 0x020004B7 RID: 1207
		// (Invoke) Token: 0x060017DF RID: 6111
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void HandleStateChangesDelegate();

		// Token: 0x020004B8 RID: 1208
		// (Invoke) Token: 0x060017E3 RID: 6115
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void InitializeVoicePlayEventDelegate();

		// Token: 0x020004B9 RID: 1209
		// (Invoke) Token: 0x060017E7 RID: 6119
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void InitializeXBOXSoundManagerDelegate();

		// Token: 0x020004BA RID: 1210
		// (Invoke) Token: 0x060017EB RID: 6123
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void LoadEventFileAuxDelegate(byte[] soundBankName, [MarshalAs(UnmanagedType.U1)] bool decompressSamples);

		// Token: 0x020004BB RID: 1211
		// (Invoke) Token: 0x060017EF RID: 6127
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ProcessDataToBeReceivedDelegate(ulong senderDeviceID, ManagedArray data, uint dataSize);

		// Token: 0x020004BC RID: 1212
		// (Invoke) Token: 0x060017F3 RID: 6131
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ProcessDataToBeSentDelegate(ref int numData);

		// Token: 0x020004BD RID: 1213
		// (Invoke) Token: 0x060017F7 RID: 6135
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemoveXBOXRemoteUserDelegate(ulong XUID);

		// Token: 0x020004BE RID: 1214
		// (Invoke) Token: 0x060017FB RID: 6139
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ResetDelegate();

		// Token: 0x020004BF RID: 1215
		// (Invoke) Token: 0x060017FF RID: 6143
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetGlobalParameterDelegate(byte[] parameterName, float value);

		// Token: 0x020004C0 RID: 1216
		// (Invoke) Token: 0x06001803 RID: 6147
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetListenerFrameDelegate(ref MatrixFrame frame, ref Vec3 attenuationPosition);

		// Token: 0x020004C1 RID: 1217
		// (Invoke) Token: 0x06001807 RID: 6151
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetStateDelegate(byte[] stateGroup, byte[] state);

		// Token: 0x020004C2 RID: 1218
		// (Invoke) Token: 0x0600180B RID: 6155
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool StartOneShotEventDelegate(byte[] eventFullName, Vec3 position);

		// Token: 0x020004C3 RID: 1219
		// (Invoke) Token: 0x0600180F RID: 6159
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool StartOneShotEventWithParamDelegate(byte[] eventFullName, Vec3 position, byte[] paramName, float paramValue);

		// Token: 0x020004C4 RID: 1220
		// (Invoke) Token: 0x06001813 RID: 6163
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void StartVoiceRecordDelegate();

		// Token: 0x020004C5 RID: 1221
		// (Invoke) Token: 0x06001817 RID: 6167
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void StopVoiceRecordDelegate();

		// Token: 0x020004C6 RID: 1222
		// (Invoke) Token: 0x0600181B RID: 6171
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void UpdateVoiceToPlayDelegate(ManagedArray voiceBuffer, int length, int index);

		// Token: 0x020004C7 RID: 1223
		// (Invoke) Token: 0x0600181F RID: 6175
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void UpdateXBOXChatCommunicationFlagsDelegate(ulong XUID, [MarshalAs(UnmanagedType.U1)] bool canSendMicSound, [MarshalAs(UnmanagedType.U1)] bool canSendTextSound, [MarshalAs(UnmanagedType.U1)] bool canSendText, [MarshalAs(UnmanagedType.U1)] bool canReceiveSound, [MarshalAs(UnmanagedType.U1)] bool canReceiveText);

		// Token: 0x020004C8 RID: 1224
		// (Invoke) Token: 0x06001823 RID: 6179
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void UpdateXBOXLocalUserDelegate();
	}
}

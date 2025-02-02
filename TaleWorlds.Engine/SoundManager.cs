using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200008B RID: 139
	public static class SoundManager
	{
		// Token: 0x06000A93 RID: 2707 RVA: 0x0000B987 File Offset: 0x00009B87
		public static void SetListenerFrame(MatrixFrame frame)
		{
			EngineApplicationInterface.ISoundManager.SetListenerFrame(ref frame, ref frame.origin);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0000B99C File Offset: 0x00009B9C
		public static void SetListenerFrame(MatrixFrame frame, Vec3 attenuationPosition)
		{
			EngineApplicationInterface.ISoundManager.SetListenerFrame(ref frame, ref attenuationPosition);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0000B9AC File Offset: 0x00009BAC
		public static MatrixFrame GetListenerFrame()
		{
			MatrixFrame result;
			EngineApplicationInterface.ISoundManager.GetListenerFrame(out result);
			return result;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0000B9C8 File Offset: 0x00009BC8
		public static Vec3 GetAttenuationPosition()
		{
			Vec3 result;
			EngineApplicationInterface.ISoundManager.GetAttenuationPosition(out result);
			return result;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0000B9E2 File Offset: 0x00009BE2
		public static void Reset()
		{
			EngineApplicationInterface.ISoundManager.Reset();
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0000B9EE File Offset: 0x00009BEE
		public static bool StartOneShotEvent(string eventFullName, in Vec3 position, string paramName, float paramValue)
		{
			return EngineApplicationInterface.ISoundManager.StartOneShotEventWithParam(eventFullName, position, paramName, paramValue);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0000BA03 File Offset: 0x00009C03
		public static bool StartOneShotEvent(string eventFullName, in Vec3 position)
		{
			return EngineApplicationInterface.ISoundManager.StartOneShotEvent(eventFullName, position);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0000BA16 File Offset: 0x00009C16
		public static void SetState(string stateGroup, string state)
		{
			EngineApplicationInterface.ISoundManager.SetState(stateGroup, state);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0000BA24 File Offset: 0x00009C24
		public static SoundEvent CreateEvent(string eventFullName, Scene scene)
		{
			return SoundEvent.CreateEventFromString(eventFullName, scene);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0000BA2D File Offset: 0x00009C2D
		public static void LoadEventFileAux(string soundBank, bool decompressSamples)
		{
			if (!SoundManager._loaded)
			{
				EngineApplicationInterface.ISoundManager.LoadEventFileAux(soundBank, decompressSamples);
				SoundManager._loaded = true;
			}
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0000BA48 File Offset: 0x00009C48
		public static void AddSoundClientWithId(ulong clientId)
		{
			EngineApplicationInterface.ISoundManager.AddSoundClientWithId(clientId);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0000BA55 File Offset: 0x00009C55
		public static void DeleteSoundClientWithId(ulong clientId)
		{
			EngineApplicationInterface.ISoundManager.DeleteSoundClientWithId(clientId);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0000BA62 File Offset: 0x00009C62
		public static void SetGlobalParameter(string parameterName, float value)
		{
			EngineApplicationInterface.ISoundManager.SetGlobalParameter(parameterName, value);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0000BA70 File Offset: 0x00009C70
		public static int GetEventGlobalIndex(string eventFullName)
		{
			if (string.IsNullOrEmpty(eventFullName))
			{
				return -1;
			}
			return EngineApplicationInterface.ISoundManager.GetGlobalIndexOfEvent(eventFullName);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0000BA87 File Offset: 0x00009C87
		public static void InitializeVoicePlayEvent()
		{
			EngineApplicationInterface.ISoundManager.InitializeVoicePlayEvent();
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0000BA93 File Offset: 0x00009C93
		public static void CreateVoiceEvent()
		{
			EngineApplicationInterface.ISoundManager.CreateVoiceEvent();
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0000BA9F File Offset: 0x00009C9F
		public static void DestroyVoiceEvent(int id)
		{
			EngineApplicationInterface.ISoundManager.DestroyVoiceEvent(id);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0000BAAC File Offset: 0x00009CAC
		public static void FinalizeVoicePlayEvent()
		{
			EngineApplicationInterface.ISoundManager.FinalizeVoicePlayEvent();
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0000BAB8 File Offset: 0x00009CB8
		public static void StartVoiceRecording()
		{
			EngineApplicationInterface.ISoundManager.StartVoiceRecord();
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0000BAC4 File Offset: 0x00009CC4
		public static void StopVoiceRecording()
		{
			EngineApplicationInterface.ISoundManager.StopVoiceRecord();
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0000BAD0 File Offset: 0x00009CD0
		public static void GetVoiceData(byte[] voiceBuffer, int chunkSize, out int readBytesLength)
		{
			readBytesLength = 0;
			EngineApplicationInterface.ISoundManager.GetVoiceData(voiceBuffer, chunkSize, ref readBytesLength);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0000BAE2 File Offset: 0x00009CE2
		public static void UpdateVoiceToPlay(byte[] voiceBuffer, int length, int index)
		{
			EngineApplicationInterface.ISoundManager.UpdateVoiceToPlay(voiceBuffer, length, index);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0000BAF1 File Offset: 0x00009CF1
		public static void AddXBOXRemoteUser(ulong XUID, ulong deviceID, bool canSendMicSound, bool canSendTextSound, bool canSendText, bool canReceiveSound, bool canReceiveText)
		{
			EngineApplicationInterface.ISoundManager.AddXBOXRemoteUser(XUID, deviceID, canSendMicSound, canSendTextSound, canSendText, canReceiveSound, canReceiveText);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0000BB07 File Offset: 0x00009D07
		public static void InitializeXBOXSoundManager()
		{
			EngineApplicationInterface.ISoundManager.InitializeXBOXSoundManager();
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0000BB13 File Offset: 0x00009D13
		public static void ApplyPushToTalk(bool pushed)
		{
			EngineApplicationInterface.ISoundManager.ApplyPushToTalk(pushed);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0000BB20 File Offset: 0x00009D20
		public static void ClearXBOXSoundManager()
		{
			EngineApplicationInterface.ISoundManager.ClearXBOXSoundManager();
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0000BB2C File Offset: 0x00009D2C
		public static void UpdateXBOXLocalUser()
		{
			EngineApplicationInterface.ISoundManager.UpdateXBOXLocalUser();
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0000BB38 File Offset: 0x00009D38
		public static void UpdateXBOXChatCommunicationFlags(ulong XUID, bool canSendMicSound, bool canSendTextSound, bool canSendText, bool canReceiveSound, bool canReceiveText)
		{
			EngineApplicationInterface.ISoundManager.UpdateXBOXChatCommunicationFlags(XUID, canSendMicSound, canSendTextSound, canSendText, canReceiveSound, canReceiveText);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0000BB4C File Offset: 0x00009D4C
		public static void RemoveXBOXRemoteUser(ulong XUID)
		{
			EngineApplicationInterface.ISoundManager.RemoveXBOXRemoteUser(XUID);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0000BB59 File Offset: 0x00009D59
		public static void ProcessDataToBeReceived(ulong senderDeviceID, byte[] data, uint dataSize)
		{
			EngineApplicationInterface.ISoundManager.ProcessDataToBeReceived(senderDeviceID, data, dataSize);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0000BB68 File Offset: 0x00009D68
		public static void ProcessDataToBeSent(ref int numData)
		{
			EngineApplicationInterface.ISoundManager.ProcessDataToBeSent(ref numData);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0000BB75 File Offset: 0x00009D75
		public static void HandleStateChanges()
		{
			EngineApplicationInterface.ISoundManager.HandleStateChanges();
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0000BB81 File Offset: 0x00009D81
		public static void GetSizeOfDataToBeSentAt(int index, ref uint byteCount, ref uint numReceivers)
		{
			EngineApplicationInterface.ISoundManager.GetSizeOfDataToBeSentAt(index, ref byteCount, ref numReceivers);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0000BB90 File Offset: 0x00009D90
		public static bool GetDataToBeSentAt(int index, byte[] buffer, ulong[] receivers, ref bool transportGuaranteed)
		{
			return EngineApplicationInterface.ISoundManager.GetDataToBeSentAt(index, buffer, receivers, ref transportGuaranteed);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0000BBA0 File Offset: 0x00009DA0
		public static void ClearDataToBeSent()
		{
			EngineApplicationInterface.ISoundManager.ClearDataToBeSent();
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0000BBAC File Offset: 0x00009DAC
		public static void CompressData(int clientID, byte[] buffer, int length, byte[] compressedBuffer, out int compressedBufferLength)
		{
			compressedBufferLength = 0;
			EngineApplicationInterface.ISoundManager.CompressData((ulong)((long)clientID), buffer, length, compressedBuffer, ref compressedBufferLength);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0000BBC3 File Offset: 0x00009DC3
		public static void DecompressData(int clientID, byte[] compressedBuffer, int compressedBufferLength, byte[] decompressedBuffer, out int decompressedBufferLength)
		{
			decompressedBufferLength = 0;
			EngineApplicationInterface.ISoundManager.DecompressData((ulong)((long)clientID), compressedBuffer, compressedBufferLength, decompressedBuffer, ref decompressedBufferLength);
		}

		// Token: 0x040001B2 RID: 434
		private static bool _loaded;
	}
}

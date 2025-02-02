using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200003F RID: 63
	[ApplicationInterfaceBase]
	internal interface ISoundManager
	{
		// Token: 0x06000571 RID: 1393
		[EngineMethod("set_listener_frame", false)]
		void SetListenerFrame(ref MatrixFrame frame, ref Vec3 attenuationPosition);

		// Token: 0x06000572 RID: 1394
		[EngineMethod("get_listener_frame", false)]
		void GetListenerFrame(out MatrixFrame result);

		// Token: 0x06000573 RID: 1395
		[EngineMethod("get_attenuation_position", false)]
		void GetAttenuationPosition(out Vec3 result);

		// Token: 0x06000574 RID: 1396
		[EngineMethod("reset", false)]
		void Reset();

		// Token: 0x06000575 RID: 1397
		[EngineMethod("start_one_shot_event_with_param", false)]
		bool StartOneShotEventWithParam(string eventFullName, Vec3 position, string paramName, float paramValue);

		// Token: 0x06000576 RID: 1398
		[EngineMethod("start_one_shot_event", false)]
		bool StartOneShotEvent(string eventFullName, Vec3 position);

		// Token: 0x06000577 RID: 1399
		[EngineMethod("set_state", false)]
		void SetState(string stateGroup, string state);

		// Token: 0x06000578 RID: 1400
		[EngineMethod("load_event_file_aux", false)]
		void LoadEventFileAux(string soundBankName, bool decompressSamples);

		// Token: 0x06000579 RID: 1401
		[EngineMethod("set_global_parameter", false)]
		void SetGlobalParameter(string parameterName, float value);

		// Token: 0x0600057A RID: 1402
		[EngineMethod("add_sound_client_with_id", false)]
		void AddSoundClientWithId(ulong client_id);

		// Token: 0x0600057B RID: 1403
		[EngineMethod("delete_sound_client_with_id", false)]
		void DeleteSoundClientWithId(ulong client_id);

		// Token: 0x0600057C RID: 1404
		[EngineMethod("get_global_index_of_event", false)]
		int GetGlobalIndexOfEvent(string eventFullName);

		// Token: 0x0600057D RID: 1405
		[EngineMethod("create_voice_event", false)]
		void CreateVoiceEvent();

		// Token: 0x0600057E RID: 1406
		[EngineMethod("destroy_voice_event", false)]
		void DestroyVoiceEvent(int id);

		// Token: 0x0600057F RID: 1407
		[EngineMethod("init_voice_play_event", false)]
		void InitializeVoicePlayEvent();

		// Token: 0x06000580 RID: 1408
		[EngineMethod("finalize_voice_play_event", false)]
		void FinalizeVoicePlayEvent();

		// Token: 0x06000581 RID: 1409
		[EngineMethod("start_voice_record", false)]
		void StartVoiceRecord();

		// Token: 0x06000582 RID: 1410
		[EngineMethod("stop_voice_record", false)]
		void StopVoiceRecord();

		// Token: 0x06000583 RID: 1411
		[EngineMethod("get_voice_data", false)]
		void GetVoiceData(byte[] voiceBuffer, int chunkSize, ref int readBytesLength);

		// Token: 0x06000584 RID: 1412
		[EngineMethod("update_voice_to_play", false)]
		void UpdateVoiceToPlay(byte[] voiceBuffer, int length, int index);

		// Token: 0x06000585 RID: 1413
		[EngineMethod("compress_voice_data", false)]
		void CompressData(ulong clientID, byte[] buffer, int length, byte[] compressedBuffer, ref int compressedBufferLength);

		// Token: 0x06000586 RID: 1414
		[EngineMethod("decompress_voice_data", false)]
		void DecompressData(ulong clientID, byte[] compressedBuffer, int compressedBufferLength, byte[] decompressedBuffer, ref int decompressedBufferLength);

		// Token: 0x06000587 RID: 1415
		[EngineMethod("remove_xbox_remote_user", false)]
		void RemoveXBOXRemoteUser(ulong XUID);

		// Token: 0x06000588 RID: 1416
		[EngineMethod("add_xbox_remote_user", false)]
		void AddXBOXRemoteUser(ulong XUID, ulong deviceID, bool canSendMicSound, bool canSendTextSound, bool canSendText, bool canReceiveSound, bool canReceiveText);

		// Token: 0x06000589 RID: 1417
		[EngineMethod("initialize_xbox_sound_manager", false)]
		void InitializeXBOXSoundManager();

		// Token: 0x0600058A RID: 1418
		[EngineMethod("apply_push_to_talk", false)]
		void ApplyPushToTalk(bool pushed);

		// Token: 0x0600058B RID: 1419
		[EngineMethod("clear_xbox_sound_manager", false)]
		void ClearXBOXSoundManager();

		// Token: 0x0600058C RID: 1420
		[EngineMethod("update_xbox_local_user", false)]
		void UpdateXBOXLocalUser();

		// Token: 0x0600058D RID: 1421
		[EngineMethod("update_xbox_chat_communication_flags", false)]
		void UpdateXBOXChatCommunicationFlags(ulong XUID, bool canSendMicSound, bool canSendTextSound, bool canSendText, bool canReceiveSound, bool canReceiveText);

		// Token: 0x0600058E RID: 1422
		[EngineMethod("process_data_to_be_received", false)]
		void ProcessDataToBeReceived(ulong senderDeviceID, byte[] data, uint dataSize);

		// Token: 0x0600058F RID: 1423
		[EngineMethod("process_data_to_be_sent", false)]
		void ProcessDataToBeSent(ref int numData);

		// Token: 0x06000590 RID: 1424
		[EngineMethod("handle_state_changes", false)]
		void HandleStateChanges();

		// Token: 0x06000591 RID: 1425
		[EngineMethod("get_size_of_data_to_be_sent_at", false)]
		void GetSizeOfDataToBeSentAt(int index, ref uint byte_count, ref uint numReceivers);

		// Token: 0x06000592 RID: 1426
		[EngineMethod("get_data_to_be_sent_at", false)]
		bool GetDataToBeSentAt(int index, byte[] buffer, ulong[] receivers, ref bool transportGuaranteed);

		// Token: 0x06000593 RID: 1427
		[EngineMethod("clear_data_to_be_sent", false)]
		void ClearDataToBeSent();
	}
}

using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000040 RID: 64
	[ApplicationInterfaceBase]
	internal interface ISoundEvent
	{
		// Token: 0x06000594 RID: 1428
		[EngineMethod("create_event_from_string", false)]
		int CreateEventFromString(string eventName, UIntPtr scene);

		// Token: 0x06000595 RID: 1429
		[EngineMethod("get_event_id_from_string", false)]
		int GetEventIdFromString(string eventName);

		// Token: 0x06000596 RID: 1430
		[EngineMethod("play_sound_2d", false)]
		bool PlaySound2D(int fmodEventIndex);

		// Token: 0x06000597 RID: 1431
		[EngineMethod("get_total_event_count", false)]
		int GetTotalEventCount();

		// Token: 0x06000598 RID: 1432
		[EngineMethod("set_event_min_max_distance", false)]
		void SetEventMinMaxDistance(int fmodEventIndex, Vec3 radius);

		// Token: 0x06000599 RID: 1433
		[EngineMethod("create_event", false)]
		int CreateEvent(int fmodEventIndex, UIntPtr scene);

		// Token: 0x0600059A RID: 1434
		[EngineMethod("release_event", false)]
		void ReleaseEvent(int eventId);

		// Token: 0x0600059B RID: 1435
		[EngineMethod("set_event_parameter_from_string", false)]
		void SetEventParameterFromString(int eventId, string name, float value);

		// Token: 0x0600059C RID: 1436
		[EngineMethod("get_event_min_max_distance", false)]
		Vec3 GetEventMinMaxDistance(int eventId);

		// Token: 0x0600059D RID: 1437
		[EngineMethod("set_event_position", false)]
		void SetEventPosition(int eventId, ref Vec3 position);

		// Token: 0x0600059E RID: 1438
		[EngineMethod("set_event_velocity", false)]
		void SetEventVelocity(int eventId, ref Vec3 velocity);

		// Token: 0x0600059F RID: 1439
		[EngineMethod("start_event", false)]
		bool StartEvent(int eventId);

		// Token: 0x060005A0 RID: 1440
		[EngineMethod("start_event_in_position", false)]
		bool StartEventInPosition(int eventId, ref Vec3 position);

		// Token: 0x060005A1 RID: 1441
		[EngineMethod("stop_event", false)]
		void StopEvent(int eventId);

		// Token: 0x060005A2 RID: 1442
		[EngineMethod("pause_event", false)]
		void PauseEvent(int eventId);

		// Token: 0x060005A3 RID: 1443
		[EngineMethod("resume_event", false)]
		void ResumeEvent(int eventId);

		// Token: 0x060005A4 RID: 1444
		[EngineMethod("play_extra_event", false)]
		void PlayExtraEvent(int soundId, string eventName);

		// Token: 0x060005A5 RID: 1445
		[EngineMethod("set_switch", false)]
		void SetSwitch(int soundId, string switchGroupName, string newSwitchStateName);

		// Token: 0x060005A6 RID: 1446
		[EngineMethod("trigger_cue", false)]
		void TriggerCue(int eventId);

		// Token: 0x060005A7 RID: 1447
		[EngineMethod("set_event_parameter_at_index", false)]
		void SetEventParameterAtIndex(int soundId, int parameterIndex, float value);

		// Token: 0x060005A8 RID: 1448
		[EngineMethod("is_playing", false)]
		bool IsPlaying(int eventId);

		// Token: 0x060005A9 RID: 1449
		[EngineMethod("is_paused", false)]
		bool IsPaused(int eventId);

		// Token: 0x060005AA RID: 1450
		[EngineMethod("is_valid", false)]
		bool IsValid(int eventId);

		// Token: 0x060005AB RID: 1451
		[EngineMethod("create_event_from_external_file", false)]
		int CreateEventFromExternalFile(string programmerSoundEventName, string filePath, UIntPtr scene);

		// Token: 0x060005AC RID: 1452
		[EngineMethod("create_event_from_sound_buffer", false)]
		int CreateEventFromSoundBuffer(string programmerSoundEventName, byte[] soundBuffer, UIntPtr scene);
	}
}

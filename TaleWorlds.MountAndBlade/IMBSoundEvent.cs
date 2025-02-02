using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001AE RID: 430
	[ScriptingInterfaceBase]
	internal interface IMBSoundEvent
	{
		// Token: 0x06001789 RID: 6025
		[EngineMethod("create_event_from_external_file", false)]
		int CreateEventFromExternalFile(string programmerSoundEventName, string filePath, UIntPtr scene);

		// Token: 0x0600178A RID: 6026
		[EngineMethod("create_event_from_sound_buffer", false)]
		int CreateEventFromSoundBuffer(string programmerSoundEventName, byte[] soundBuffer, UIntPtr scene);

		// Token: 0x0600178B RID: 6027
		[EngineMethod("play_sound", false)]
		bool PlaySound(int fmodEventIndex, ref Vec3 position);

		// Token: 0x0600178C RID: 6028
		[EngineMethod("play_sound_with_int_param", false)]
		bool PlaySoundWithIntParam(int fmodEventIndex, int paramIndex, float paramVal, ref Vec3 position);

		// Token: 0x0600178D RID: 6029
		[EngineMethod("play_sound_with_str_param", false)]
		bool PlaySoundWithStrParam(int fmodEventIndex, string paramName, float paramVal, ref Vec3 position);

		// Token: 0x0600178E RID: 6030
		[EngineMethod("play_sound_with_param", false)]
		bool PlaySoundWithParam(int soundCodeId, SoundEventParameter parameter, ref Vec3 position);
	}
}

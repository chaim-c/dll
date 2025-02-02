using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001D8 RID: 472
	public static class MBSoundEvent
	{
		// Token: 0x06001AA1 RID: 6817 RVA: 0x0005D0B7 File Offset: 0x0005B2B7
		public static bool PlaySound(int soundCodeId, ref Vec3 position)
		{
			return MBAPI.IMBSoundEvent.PlaySound(soundCodeId, ref position);
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x0005D0C8 File Offset: 0x0005B2C8
		public static bool PlaySound(int soundCodeId, Vec3 position)
		{
			Vec3 vec = position;
			return MBAPI.IMBSoundEvent.PlaySound(soundCodeId, ref vec);
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x0005D0E4 File Offset: 0x0005B2E4
		public static bool PlaySound(int soundCodeId, ref SoundEventParameter parameter, Vec3 position)
		{
			Vec3 vec = position;
			return MBSoundEvent.PlaySound(soundCodeId, ref parameter, ref vec);
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0005D0FC File Offset: 0x0005B2FC
		public static bool PlaySound(string soundPath, ref SoundEventParameter parameter, Vec3 position)
		{
			int eventIdFromString = SoundEvent.GetEventIdFromString(soundPath);
			Vec3 vec = position;
			return MBSoundEvent.PlaySound(eventIdFromString, ref parameter, ref vec);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x0005D119 File Offset: 0x0005B319
		public static bool PlaySound(int soundCodeId, ref SoundEventParameter parameter, ref Vec3 position)
		{
			return MBAPI.IMBSoundEvent.PlaySoundWithParam(soundCodeId, parameter, ref position);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0005D12D File Offset: 0x0005B32D
		public static void PlayEventFromSoundBuffer(string eventId, byte[] soundData, Scene scene)
		{
			MBAPI.IMBSoundEvent.CreateEventFromSoundBuffer(eventId, soundData, (scene != null) ? scene.Pointer : UIntPtr.Zero);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0005D152 File Offset: 0x0005B352
		public static void CreateEventFromExternalFile(string programmerEventName, string soundFilePath, Scene scene)
		{
			MBAPI.IMBSoundEvent.CreateEventFromExternalFile(programmerEventName, soundFilePath, (scene != null) ? scene.Pointer : UIntPtr.Zero);
		}
	}
}

using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001AF RID: 431
	[ScriptingInterfaceBase]
	internal interface IMBVoiceManager
	{
		// Token: 0x0600178F RID: 6031
		[EngineMethod("get_voice_type_index", false)]
		int GetVoiceTypeIndex(string voiceType);

		// Token: 0x06001790 RID: 6032
		[EngineMethod("get_voice_definition_count_with_monster_sound_and_collision_info_class_name", false)]
		int GetVoiceDefinitionCountWithMonsterSoundAndCollisionInfoClassName(string className);

		// Token: 0x06001791 RID: 6033
		[EngineMethod("get_voice_definitions_with_monster_sound_and_collision_info_class_name", false)]
		void GetVoiceDefinitionListWithMonsterSoundAndCollisionInfoClassName(string className, int[] definitionIndices);
	}
}

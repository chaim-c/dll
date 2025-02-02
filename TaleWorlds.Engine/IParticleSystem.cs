using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200001C RID: 28
	[ApplicationInterfaceBase]
	internal interface IParticleSystem
	{
		// Token: 0x0600013C RID: 316
		[EngineMethod("set_enable", false)]
		void SetEnable(UIntPtr psysPointer, bool enable);

		// Token: 0x0600013D RID: 317
		[EngineMethod("set_runtime_emission_rate_multiplier", false)]
		void SetRuntimeEmissionRateMultiplier(UIntPtr pointer, float multiplier);

		// Token: 0x0600013E RID: 318
		[EngineMethod("restart", false)]
		void Restart(UIntPtr psysPointer);

		// Token: 0x0600013F RID: 319
		[EngineMethod("set_local_frame", false)]
		void SetLocalFrame(UIntPtr pointer, ref MatrixFrame newFrame);

		// Token: 0x06000140 RID: 320
		[EngineMethod("get_local_frame", false)]
		void GetLocalFrame(UIntPtr pointer, ref MatrixFrame frame);

		// Token: 0x06000141 RID: 321
		[EngineMethod("get_runtime_id_by_name", false)]
		int GetRuntimeIdByName(string particleSystemName);

		// Token: 0x06000142 RID: 322
		[EngineMethod("create_particle_system_attached_to_bone", false)]
		ParticleSystem CreateParticleSystemAttachedToBone(int runtimeId, UIntPtr skeletonPtr, sbyte boneIndex, ref MatrixFrame boneLocalFrame);

		// Token: 0x06000143 RID: 323
		[EngineMethod("create_particle_system_attached_to_entity", false)]
		ParticleSystem CreateParticleSystemAttachedToEntity(int runtimeId, UIntPtr entityPtr, ref MatrixFrame boneLocalFrame);

		// Token: 0x06000144 RID: 324
		[EngineMethod("set_particle_effect_by_name", false)]
		void SetParticleEffectByName(UIntPtr pointer, string effectName);
	}
}

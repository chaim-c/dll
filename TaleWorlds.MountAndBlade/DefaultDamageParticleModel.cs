using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001F6 RID: 502
	public class DefaultDamageParticleModel : DamageParticleModel
	{
		// Token: 0x06001C09 RID: 7177 RVA: 0x00060FF8 File Offset: 0x0005F1F8
		public DefaultDamageParticleModel()
		{
			this._bloodStartHitParticleIndex = ParticleSystemManager.GetRuntimeIdByName("psys_game_blood_sword_enter");
			this._bloodContinueHitParticleIndex = ParticleSystemManager.GetRuntimeIdByName("psys_game_blood_sword_inside");
			this._bloodEndHitParticleIndex = ParticleSystemManager.GetRuntimeIdByName("psys_game_blood_sword_exit");
			this._sweatStartHitParticleIndex = ParticleSystemManager.GetRuntimeIdByName("psys_game_sweat_sword_enter");
			this._sweatContinueHitParticleIndex = ParticleSystemManager.GetRuntimeIdByName("psys_game_sweat_sword_enter");
			this._sweatEndHitParticleIndex = ParticleSystemManager.GetRuntimeIdByName("psys_game_sweat_sword_enter");
			this._missileHitParticleIndex = ParticleSystemManager.GetRuntimeIdByName("psys_game_blood_sword_enter");
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x000610AC File Offset: 0x0005F2AC
		public override void GetMeleeAttackBloodParticles(Agent attacker, Agent victim, in Blow blow, in AttackCollisionData collisionData, out HitParticleResultData particleResultData)
		{
			particleResultData.StartHitParticleIndex = this._bloodStartHitParticleIndex;
			particleResultData.ContinueHitParticleIndex = this._bloodContinueHitParticleIndex;
			particleResultData.EndHitParticleIndex = this._bloodEndHitParticleIndex;
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x000610D5 File Offset: 0x0005F2D5
		public override void GetMeleeAttackSweatParticles(Agent attacker, Agent victim, in Blow blow, in AttackCollisionData collisionData, out HitParticleResultData particleResultData)
		{
			particleResultData.StartHitParticleIndex = this._sweatStartHitParticleIndex;
			particleResultData.ContinueHitParticleIndex = this._sweatContinueHitParticleIndex;
			particleResultData.EndHitParticleIndex = this._sweatEndHitParticleIndex;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x000610FE File Offset: 0x0005F2FE
		public override int GetMissileAttackParticle(Agent attacker, Agent victim, in Blow blow, in AttackCollisionData collisionData)
		{
			return this._missileHitParticleIndex;
		}

		// Token: 0x0400090C RID: 2316
		private int _bloodStartHitParticleIndex = -1;

		// Token: 0x0400090D RID: 2317
		private int _bloodContinueHitParticleIndex = -1;

		// Token: 0x0400090E RID: 2318
		private int _bloodEndHitParticleIndex = -1;

		// Token: 0x0400090F RID: 2319
		private int _sweatStartHitParticleIndex = -1;

		// Token: 0x04000910 RID: 2320
		private int _sweatContinueHitParticleIndex = -1;

		// Token: 0x04000911 RID: 2321
		private int _sweatEndHitParticleIndex = -1;

		// Token: 0x04000912 RID: 2322
		private int _missileHitParticleIndex = -1;
	}
}

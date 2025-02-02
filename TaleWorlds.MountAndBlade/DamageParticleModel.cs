using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001F4 RID: 500
	public abstract class DamageParticleModel : GameModel
	{
		// Token: 0x06001C03 RID: 7171
		public abstract void GetMeleeAttackBloodParticles(Agent attacker, Agent victim, in Blow blow, in AttackCollisionData collisionData, out HitParticleResultData particleResultData);

		// Token: 0x06001C04 RID: 7172
		public abstract void GetMeleeAttackSweatParticles(Agent attacker, Agent victim, in Blow blow, in AttackCollisionData collisionData, out HitParticleResultData particleResultData);

		// Token: 0x06001C05 RID: 7173
		public abstract int GetMissileAttackParticle(Agent attacker, Agent victim, in Blow blow, in AttackCollisionData collisionData);
	}
}

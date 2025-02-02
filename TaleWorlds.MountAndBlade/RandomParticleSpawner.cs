using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000328 RID: 808
	public class RandomParticleSpawner : ScriptComponentBehavior
	{
		// Token: 0x06002B45 RID: 11077 RVA: 0x000A7976 File Offset: 0x000A5B76
		private void InitScript()
		{
			this._timeUntilNextParticleSpawn = this.spawnInterval;
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x000A7984 File Offset: 0x000A5B84
		private void CheckSpawnParticle(float dt)
		{
			this._timeUntilNextParticleSpawn -= dt;
			if (this._timeUntilNextParticleSpawn <= 0f)
			{
				int childCount = base.GameEntity.ChildCount;
				if (childCount > 0)
				{
					int index = MBRandom.RandomInt(childCount);
					GameEntity child = base.GameEntity.GetChild(index);
					int componentCount = child.GetComponentCount(GameEntity.ComponentType.ParticleSystemInstanced);
					for (int i = 0; i < componentCount; i++)
					{
						((ParticleSystem)child.GetComponentAtIndex(i, GameEntity.ComponentType.ParticleSystemInstanced)).Restart();
					}
				}
				this._timeUntilNextParticleSpawn += this.spawnInterval;
			}
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x000A7A0E File Offset: 0x000A5C0E
		protected internal override void OnInit()
		{
			base.OnInit();
			this.InitScript();
			base.SetScriptComponentToTick(this.GetTickRequirement());
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x000A7A28 File Offset: 0x000A5C28
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
			this.OnInit();
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x000A7A36 File Offset: 0x000A5C36
		public override ScriptComponentBehavior.TickRequirement GetTickRequirement()
		{
			return ScriptComponentBehavior.TickRequirement.Tick | base.GetTickRequirement();
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x000A7A40 File Offset: 0x000A5C40
		protected internal override void OnTick(float dt)
		{
			this.CheckSpawnParticle(dt);
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x000A7A49 File Offset: 0x000A5C49
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			this.CheckSpawnParticle(dt);
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000A7A59 File Offset: 0x000A5C59
		protected internal override bool MovesEntity()
		{
			return true;
		}

		// Token: 0x040010C6 RID: 4294
		private float _timeUntilNextParticleSpawn;

		// Token: 0x040010C7 RID: 4295
		public float spawnInterval = 3f;
	}
}

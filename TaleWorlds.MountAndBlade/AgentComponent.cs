using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020000FB RID: 251
	public abstract class AgentComponent
	{
		// Token: 0x06000BAC RID: 2988 RVA: 0x00015B42 File Offset: 0x00013D42
		protected AgentComponent(Agent agent)
		{
			this.Agent = agent;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00015B51 File Offset: 0x00013D51
		public virtual void Initialize()
		{
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00015B53 File Offset: 0x00013D53
		public virtual void OnTickAsAI(float dt)
		{
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00015B55 File Offset: 0x00013D55
		public virtual float GetMoraleAddition()
		{
			return 0f;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00015B5C File Offset: 0x00013D5C
		public virtual float GetMoraleDecreaseConstant()
		{
			return 1f;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00015B63 File Offset: 0x00013D63
		public virtual void OnItemPickup(SpawnedItemEntity item)
		{
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00015B65 File Offset: 0x00013D65
		public virtual void OnWeaponDrop(MissionWeapon droppedWeapon)
		{
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00015B67 File Offset: 0x00013D67
		public virtual void OnStopUsingGameObject()
		{
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00015B69 File Offset: 0x00013D69
		public virtual void OnWeaponHPChanged(ItemObject item, int hitPoints)
		{
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00015B6B File Offset: 0x00013D6B
		public virtual void OnRetreating()
		{
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00015B6D File Offset: 0x00013D6D
		public virtual void OnMount(Agent mount)
		{
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00015B6F File Offset: 0x00013D6F
		public virtual void OnDismount(Agent mount)
		{
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00015B71 File Offset: 0x00013D71
		public virtual void OnHit(Agent affectorAgent, int damage, in MissionWeapon affectorWeapon)
		{
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00015B73 File Offset: 0x00013D73
		public virtual void OnDisciplineChanged()
		{
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00015B75 File Offset: 0x00013D75
		public virtual void OnAgentRemoved()
		{
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00015B77 File Offset: 0x00013D77
		public virtual void OnComponentRemoved()
		{
		}

		// Token: 0x040002A8 RID: 680
		protected readonly Agent Agent;
	}
}

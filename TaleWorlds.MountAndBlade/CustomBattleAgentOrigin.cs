using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000100 RID: 256
	public class CustomBattleAgentOrigin : IAgentOriginBase
	{
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x00016306 File Offset: 0x00014506
		// (set) Token: 0x06000C7E RID: 3198 RVA: 0x0001630E File Offset: 0x0001450E
		public CustomBattleCombatant CustomBattleCombatant { get; private set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00016317 File Offset: 0x00014517
		IBattleCombatant IAgentOriginBase.BattleCombatant
		{
			get
			{
				return this.CustomBattleCombatant;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0001631F File Offset: 0x0001451F
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x00016327 File Offset: 0x00014527
		public BasicCharacterObject Troop { get; private set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00016330 File Offset: 0x00014530
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x00016338 File Offset: 0x00014538
		public int Rank { get; private set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x00016341 File Offset: 0x00014541
		public Banner Banner
		{
			get
			{
				return this.CustomBattleCombatant.Banner;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0001634E File Offset: 0x0001454E
		public bool IsUnderPlayersCommand
		{
			get
			{
				return this._isPlayerSide;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x00016356 File Offset: 0x00014556
		public uint FactionColor
		{
			get
			{
				return this.CustomBattleCombatant.BasicCulture.Color;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x00016368 File Offset: 0x00014568
		public uint FactionColor2
		{
			get
			{
				return this.CustomBattleCombatant.BasicCulture.Color2;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0001637A File Offset: 0x0001457A
		public int Seed
		{
			get
			{
				return this.Troop.GetDefaultFaceSeed(this.Rank);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x00016390 File Offset: 0x00014590
		public int UniqueSeed
		{
			get
			{
				return this._descriptor.UniqueSeed;
			}
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x000163AC File Offset: 0x000145AC
		public CustomBattleAgentOrigin(CustomBattleCombatant customBattleCombatant, BasicCharacterObject characterObject, CustomBattleTroopSupplier troopSupplier, bool isPlayerSide, int rank = -1, UniqueTroopDescriptor uniqueNo = default(UniqueTroopDescriptor))
		{
			this.CustomBattleCombatant = customBattleCombatant;
			this.Troop = characterObject;
			this._descriptor = ((!uniqueNo.IsValid) ? new UniqueTroopDescriptor(Game.Current.NextUniqueTroopSeed) : uniqueNo);
			this.Rank = ((rank == -1) ? MBRandom.RandomInt(10000) : rank);
			this._troopSupplier = troopSupplier;
			this._isPlayerSide = isPlayerSide;
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00016417 File Offset: 0x00014617
		public void SetWounded()
		{
			if (!this._isRemoved)
			{
				this._troopSupplier.OnTroopWounded();
				this._isRemoved = true;
			}
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00016433 File Offset: 0x00014633
		public void SetKilled()
		{
			if (!this._isRemoved)
			{
				this._troopSupplier.OnTroopKilled();
				this._isRemoved = true;
			}
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0001644F File Offset: 0x0001464F
		public void SetRouted()
		{
			if (!this._isRemoved)
			{
				this._troopSupplier.OnTroopRouted();
				this._isRemoved = true;
			}
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0001646B File Offset: 0x0001466B
		public void OnAgentRemoved(float agentHealth)
		{
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0001646D File Offset: 0x0001466D
		void IAgentOriginBase.OnScoreHit(BasicCharacterObject victim, BasicCharacterObject captain, int damage, bool isFatal, bool isTeamKill, WeaponComponentData attackerWeapon)
		{
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0001646F File Offset: 0x0001466F
		public void SetBanner(Banner banner)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040002B4 RID: 692
		private readonly UniqueTroopDescriptor _descriptor;

		// Token: 0x040002B5 RID: 693
		private readonly bool _isPlayerSide;

		// Token: 0x040002B6 RID: 694
		private CustomBattleTroopSupplier _troopSupplier;

		// Token: 0x040002B7 RID: 695
		private bool _isRemoved;
	}
}

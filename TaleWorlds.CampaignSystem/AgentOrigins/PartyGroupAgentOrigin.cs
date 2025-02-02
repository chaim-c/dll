using System;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.TroopSuppliers;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.AgentOrigins
{
	// Token: 0x02000421 RID: 1057
	public class PartyGroupAgentOrigin : IAgentOriginBase
	{
		// Token: 0x06004019 RID: 16409 RVA: 0x0013C09C File Offset: 0x0013A29C
		internal PartyGroupAgentOrigin(PartyGroupTroopSupplier supplier, UniqueTroopDescriptor descriptor, int rank)
		{
			this._supplier = supplier;
			this._descriptor = descriptor;
			this._rank = rank;
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x0600401A RID: 16410 RVA: 0x0013C0B9 File Offset: 0x0013A2B9
		public PartyBase Party
		{
			get
			{
				return this._supplier.GetParty(this._descriptor);
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x0600401B RID: 16411 RVA: 0x0013C0CC File Offset: 0x0013A2CC
		public IBattleCombatant BattleCombatant
		{
			get
			{
				return this.Party;
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x0600401C RID: 16412 RVA: 0x0013C0D4 File Offset: 0x0013A2D4
		public Banner Banner
		{
			get
			{
				if (this.Party.LeaderHero == null)
				{
					return this.Party.MapFaction.Banner;
				}
				return this.Party.LeaderHero.ClanBanner;
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x0600401D RID: 16413 RVA: 0x0013C104 File Offset: 0x0013A304
		public int UniqueSeed
		{
			get
			{
				return this._descriptor.UniqueSeed;
			}
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x0600401E RID: 16414 RVA: 0x0013C11F File Offset: 0x0013A31F
		public CharacterObject Troop
		{
			get
			{
				return this._supplier.GetTroop(this._descriptor);
			}
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x0600401F RID: 16415 RVA: 0x0013C132 File Offset: 0x0013A332
		BasicCharacterObject IAgentOriginBase.Troop
		{
			get
			{
				return this.Troop;
			}
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06004020 RID: 16416 RVA: 0x0013C13A File Offset: 0x0013A33A
		public UniqueTroopDescriptor TroopDesc
		{
			get
			{
				return this._descriptor;
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06004021 RID: 16417 RVA: 0x0013C142 File Offset: 0x0013A342
		public int Rank
		{
			get
			{
				return this._rank;
			}
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06004022 RID: 16418 RVA: 0x0013C14A File Offset: 0x0013A34A
		public bool IsUnderPlayersCommand
		{
			get
			{
				return this.Troop == Hero.MainHero.CharacterObject || PartyGroupAgentOrigin.IsPartyUnderPlayerCommand(this.Party);
			}
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06004023 RID: 16419 RVA: 0x0013C16B File Offset: 0x0013A36B
		public uint FactionColor
		{
			get
			{
				return this.Party.MapFaction.Color;
			}
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06004024 RID: 16420 RVA: 0x0013C17D File Offset: 0x0013A37D
		public uint FactionColor2
		{
			get
			{
				return this.Party.MapFaction.Color2;
			}
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06004025 RID: 16421 RVA: 0x0013C18F File Offset: 0x0013A38F
		public int Seed
		{
			get
			{
				return CharacterHelper.GetPartyMemberFaceSeed(this.Party, this.Troop, this.Rank);
			}
		}

		// Token: 0x06004026 RID: 16422 RVA: 0x0013C1A8 File Offset: 0x0013A3A8
		public void SetWounded()
		{
			if (!this._isRemoved)
			{
				this._supplier.OnTroopWounded(this._descriptor);
				this._isRemoved = true;
			}
		}

		// Token: 0x06004027 RID: 16423 RVA: 0x0013C1CC File Offset: 0x0013A3CC
		public void SetKilled()
		{
			if (!this._isRemoved)
			{
				this._supplier.OnTroopKilled(this._descriptor);
				if (this.Troop.IsHero)
				{
					KillCharacterAction.ApplyByBattle(this.Troop.HeroObject, null, true);
				}
				this._isRemoved = true;
			}
		}

		// Token: 0x06004028 RID: 16424 RVA: 0x0013C218 File Offset: 0x0013A418
		public void SetRouted()
		{
			if (!this._isRemoved)
			{
				this._supplier.OnTroopRouted(this._descriptor);
				this._isRemoved = true;
			}
		}

		// Token: 0x06004029 RID: 16425 RVA: 0x0013C23A File Offset: 0x0013A43A
		public void OnAgentRemoved(float agentHealth)
		{
			if (this.Troop.IsHero)
			{
				this.Troop.HeroObject.HitPoints = MathF.Max(1, MathF.Round(agentHealth));
			}
		}

		// Token: 0x0600402A RID: 16426 RVA: 0x0013C265 File Offset: 0x0013A465
		void IAgentOriginBase.OnScoreHit(BasicCharacterObject victim, BasicCharacterObject captain, int damage, bool isFatal, bool isTeamKill, WeaponComponentData attackerWeapon)
		{
			this._supplier.OnTroopScoreHit(this._descriptor, victim, damage, isFatal, isTeamKill, attackerWeapon);
		}

		// Token: 0x0600402B RID: 16427 RVA: 0x0013C280 File Offset: 0x0013A480
		public void SetBanner(Banner banner)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x0013C288 File Offset: 0x0013A488
		public static bool IsPartyUnderPlayerCommand(PartyBase party)
		{
			if (party == PartyBase.MainParty)
			{
				return true;
			}
			if (party.Side != PartyBase.MainParty.Side)
			{
				return false;
			}
			bool flag = party.Owner == Hero.MainHero;
			IFaction mapFaction = party.MapFaction;
			bool flag2 = ((mapFaction != null) ? mapFaction.Leader : null) == Hero.MainHero;
			bool flag3 = party.MobileParty != null && party.MobileParty.DefaultBehavior == AiBehavior.EscortParty && party.MobileParty.TargetParty == MobileParty.MainParty;
			bool flag4 = party.MobileParty != null && party.MobileParty.Army != null && party.MobileParty.Army.LeaderParty == MobileParty.MainParty;
			Settlement mapEventSettlement = party.MapEvent.MapEventSettlement;
			bool flag5 = mapEventSettlement != null && mapEventSettlement.OwnerClan.Leader == Hero.MainHero;
			return flag || flag2 || flag3 || flag4 || flag5;
		}

		// Token: 0x040012B4 RID: 4788
		private readonly PartyGroupTroopSupplier _supplier;

		// Token: 0x040012B5 RID: 4789
		private readonly UniqueTroopDescriptor _descriptor;

		// Token: 0x040012B6 RID: 4790
		private readonly int _rank;

		// Token: 0x040012B7 RID: 4791
		private bool _isRemoved;
	}
}

﻿using System;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.AgentOrigins
{
	// Token: 0x02000422 RID: 1058
	public class SimpleAgentOrigin : IAgentOriginBase
	{
		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x0600402D RID: 16429 RVA: 0x0013C366 File Offset: 0x0013A566
		public BasicCharacterObject Troop
		{
			get
			{
				return this._troop;
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x0600402E RID: 16430 RVA: 0x0013C370 File Offset: 0x0013A570
		public bool IsUnderPlayersCommand
		{
			get
			{
				PartyBase party = this.Party;
				return party != null && (party == PartyBase.MainParty || party.Owner == Hero.MainHero || party.MapFaction.Leader == Hero.MainHero);
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x0600402F RID: 16431 RVA: 0x0013C3B2 File Offset: 0x0013A5B2
		public uint FactionColor
		{
			get
			{
				if (this.Party != null)
				{
					return this.Party.MapFaction.Color;
				}
				if (this._troop.IsHero)
				{
					return this._troop.HeroObject.MapFaction.Color;
				}
				return 0U;
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06004030 RID: 16432 RVA: 0x0013C3F1 File Offset: 0x0013A5F1
		public uint FactionColor2
		{
			get
			{
				if (this.Party != null)
				{
					return this.Party.MapFaction.Color2;
				}
				if (this._troop.IsHero)
				{
					return this._troop.HeroObject.MapFaction.Color2;
				}
				return 0U;
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06004031 RID: 16433 RVA: 0x0013C430 File Offset: 0x0013A630
		public int Seed
		{
			get
			{
				if (this.Party != null)
				{
					return CharacterHelper.GetPartyMemberFaceSeed(this.Party, this._troop, this.Rank);
				}
				return CharacterHelper.GetDefaultFaceSeed(this._troop, this.Rank);
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06004032 RID: 16434 RVA: 0x0013C463 File Offset: 0x0013A663
		public PartyBase Party
		{
			get
			{
				if (!this._troop.IsHero || this._troop.HeroObject.PartyBelongedTo == null)
				{
					return null;
				}
				return this._troop.HeroObject.PartyBelongedTo.Party;
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06004033 RID: 16435 RVA: 0x0013C49B File Offset: 0x0013A69B
		public IBattleCombatant BattleCombatant
		{
			get
			{
				return this.Party;
			}
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06004034 RID: 16436 RVA: 0x0013C4A3 File Offset: 0x0013A6A3
		public Banner Banner
		{
			get
			{
				return this._banner;
			}
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06004035 RID: 16437 RVA: 0x0013C4AB File Offset: 0x0013A6AB
		// (set) Token: 0x06004036 RID: 16438 RVA: 0x0013C4B3 File Offset: 0x0013A6B3
		public int Rank { get; private set; }

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06004037 RID: 16439 RVA: 0x0013C4BC File Offset: 0x0013A6BC
		public int UniqueSeed
		{
			get
			{
				return this._descriptor.UniqueSeed;
			}
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x0013C4C9 File Offset: 0x0013A6C9
		public SimpleAgentOrigin(BasicCharacterObject troop, int rank = -1, Banner banner = null, UniqueTroopDescriptor descriptor = default(UniqueTroopDescriptor))
		{
			this._troop = (CharacterObject)troop;
			this._descriptor = descriptor;
			this.Rank = ((rank == -1) ? MBRandom.RandomInt(10000) : rank);
			this._banner = banner;
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x0013C503 File Offset: 0x0013A703
		public void SetWounded()
		{
		}

		// Token: 0x0600403A RID: 16442 RVA: 0x0013C505 File Offset: 0x0013A705
		public void SetKilled()
		{
			if (this._troop.IsHero)
			{
				KillCharacterAction.ApplyByBattle(this._troop.HeroObject, null, true);
			}
		}

		// Token: 0x0600403B RID: 16443 RVA: 0x0013C526 File Offset: 0x0013A726
		public void SetRouted()
		{
		}

		// Token: 0x0600403C RID: 16444 RVA: 0x0013C528 File Offset: 0x0013A728
		public void OnAgentRemoved(float agentHealth)
		{
		}

		// Token: 0x0600403D RID: 16445 RVA: 0x0013C52C File Offset: 0x0013A72C
		void IAgentOriginBase.OnScoreHit(BasicCharacterObject victim, BasicCharacterObject captain, int damage, bool isFatal, bool isTeamKill, WeaponComponentData attackerWeapon)
		{
			if (isTeamKill)
			{
				CharacterObject troop = this._troop;
				int num;
				Campaign.Current.Models.CombatXpModel.GetXpFromHit(troop, (CharacterObject)captain, (CharacterObject)victim, this.Party, damage, isFatal, CombatXpModel.MissionTypeEnum.Battle, out num);
				if (troop.IsHero && attackerWeapon != null)
				{
					SkillObject skillForWeapon = Campaign.Current.Models.CombatXpModel.GetSkillForWeapon(attackerWeapon, false);
					troop.HeroObject.AddSkillXp(skillForWeapon, (float)num);
				}
			}
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x0013C5A3 File Offset: 0x0013A7A3
		public void SetBanner(Banner banner)
		{
			this._banner = banner;
		}

		// Token: 0x040012B8 RID: 4792
		private CharacterObject _troop;

		// Token: 0x040012B9 RID: 4793
		private Banner _banner;

		// Token: 0x040012BB RID: 4795
		private UniqueTroopDescriptor _descriptor;
	}
}

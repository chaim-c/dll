using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000304 RID: 772
	public abstract class MPPerkEffectBase
	{
		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x060029E2 RID: 10722 RVA: 0x000A1D2C File Offset: 0x0009FF2C
		public virtual bool IsTickRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x060029E3 RID: 10723 RVA: 0x000A1D2F File Offset: 0x0009FF2F
		// (set) Token: 0x060029E4 RID: 10724 RVA: 0x000A1D37 File Offset: 0x0009FF37
		public bool IsDisabledInWarmup { get; protected set; }

		// Token: 0x060029E5 RID: 10725 RVA: 0x000A1D40 File Offset: 0x0009FF40
		public virtual void OnUpdate(Agent agent, bool newState)
		{
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x000A1D44 File Offset: 0x0009FF44
		public virtual void OnTick(MissionPeer peer, int tickCount)
		{
			if (MultiplayerOptions.OptionType.NumberOfBotsPerFormation.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions) > 0)
			{
				MBReadOnlyList<IFormationUnit> mbreadOnlyList;
				if (peer == null)
				{
					mbreadOnlyList = null;
				}
				else
				{
					Formation controlledFormation = peer.ControlledFormation;
					mbreadOnlyList = ((controlledFormation != null) ? controlledFormation.Arrangement.GetAllUnits() : null);
				}
				MBReadOnlyList<IFormationUnit> mbreadOnlyList2 = mbreadOnlyList;
				if (mbreadOnlyList2 == null)
				{
					return;
				}
				using (List<IFormationUnit>.Enumerator enumerator = mbreadOnlyList2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Agent agent;
						if ((agent = (enumerator.Current as Agent)) != null && agent.IsActive())
						{
							this.OnTick(agent, tickCount);
						}
					}
					return;
				}
			}
			if (peer != null)
			{
				Agent controlledAgent = peer.ControlledAgent;
				bool? flag = (controlledAgent != null) ? new bool?(controlledAgent.IsActive()) : null;
				bool flag2 = true;
				if (flag.GetValueOrDefault() == flag2 & flag != null)
				{
					this.OnTick(peer.ControlledAgent, tickCount);
				}
			}
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x000A1E1C File Offset: 0x000A001C
		public virtual void OnTick(Agent agent, int tickCount)
		{
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x000A1E1E File Offset: 0x000A001E
		public virtual float GetDamage(WeaponComponentData attackerWeapon, DamageTypes damageType, bool isAlternativeAttack)
		{
			return 0f;
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x000A1E25 File Offset: 0x000A0025
		public virtual float GetMountDamage(WeaponComponentData attackerWeapon, DamageTypes damageType, bool isAlternativeAttack)
		{
			return 0f;
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x000A1E2C File Offset: 0x000A002C
		public virtual float GetDamageTaken(WeaponComponentData attackerWeapon, DamageTypes damageType)
		{
			return 0f;
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000A1E33 File Offset: 0x000A0033
		public virtual float GetMountDamageTaken(WeaponComponentData attackerWeapon, DamageTypes damageType)
		{
			return 0f;
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x000A1E3A File Offset: 0x000A003A
		public virtual float GetSpeedBonusEffectiveness(Agent attacker, WeaponComponentData attackerWeapon, DamageTypes damageType)
		{
			return 0f;
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x000A1E41 File Offset: 0x000A0041
		public virtual float GetShieldDamage(bool isCorrectSideBlock)
		{
			return 0f;
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000A1E48 File Offset: 0x000A0048
		public virtual float GetShieldDamageTaken(bool isCorrectSideBlock)
		{
			return 0f;
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x000A1E4F File Offset: 0x000A004F
		public virtual float GetRangedAccuracy()
		{
			return 0f;
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x000A1E56 File Offset: 0x000A0056
		public virtual float GetThrowingWeaponSpeed(WeaponComponentData attackerWeapon)
		{
			return 0f;
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x000A1E5D File Offset: 0x000A005D
		public virtual float GetDamageInterruptionThreshold()
		{
			return 0f;
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x000A1E64 File Offset: 0x000A0064
		public virtual float GetMountManeuver()
		{
			return 0f;
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x000A1E6B File Offset: 0x000A006B
		public virtual float GetMountSpeed()
		{
			return 0f;
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x000A1E72 File Offset: 0x000A0072
		public virtual float GetRangedHeadShotDamage()
		{
			return 0f;
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x000A1E79 File Offset: 0x000A0079
		public virtual int GetGoldOnKill(float attackerValue, float victimValue)
		{
			return 0;
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x000A1E7C File Offset: 0x000A007C
		public virtual int GetGoldOnAssist()
		{
			return 0;
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x000A1E7F File Offset: 0x000A007F
		public virtual int GetRewardedGoldOnAssist()
		{
			return 0;
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x000A1E82 File Offset: 0x000A0082
		public virtual bool GetIsTeamRewardedOnDeath()
		{
			return false;
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x000A1E85 File Offset: 0x000A0085
		public virtual void CalculateRewardedGoldOnDeath(Agent agent, List<ValueTuple<MissionPeer, int>> teamMembers)
		{
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x000A1E87 File Offset: 0x000A0087
		public virtual float GetDrivenPropertyBonus(DrivenProperty drivenProperty, float baseValue)
		{
			return 0f;
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x000A1E8E File Offset: 0x000A008E
		public virtual float GetEncumbrance(bool isOnBody)
		{
			return 0f;
		}

		// Token: 0x060029FC RID: 10748
		protected abstract void Deserialize(XmlNode node);
	}
}

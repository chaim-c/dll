using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001E9 RID: 489
	public struct CombatLogData
	{
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0005E0F8 File Offset: 0x0005C2F8
		private bool IsValidForPlayer
		{
			get
			{
				return this.IsImportant && (this.IsAttackerPlayer || this.IsVictimPlayer);
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001B1E RID: 6942 RVA: 0x0005E114 File Offset: 0x0005C314
		private bool IsImportant
		{
			get
			{
				return this.TotalDamage > 0 || this.CrushedThrough || this.Chamber;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x0005E12F File Offset: 0x0005C32F
		private bool IsAttackerPlayer
		{
			get
			{
				if (!this.IsAttackerAgentHuman)
				{
					return this.DoesAttackerAgentHaveRiderAgent && this.IsAttackerAgentRiderAgentMine;
				}
				return this.IsAttackerAgentMine;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001B20 RID: 6944 RVA: 0x0005E150 File Offset: 0x0005C350
		private bool IsVictimPlayer
		{
			get
			{
				if (!this.IsVictimAgentHuman)
				{
					return this.DoesVictimAgentHaveRiderAgent && this.IsVictimAgentRiderAgentMine;
				}
				return this.IsVictimAgentMine;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001B21 RID: 6945 RVA: 0x0005E171 File Offset: 0x0005C371
		private bool IsAttackerMount
		{
			get
			{
				return this.IsAttackerAgentMount;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001B22 RID: 6946 RVA: 0x0005E179 File Offset: 0x0005C379
		private bool IsVictimMount
		{
			get
			{
				return this.IsVictimAgentMount;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x0005E181 File Offset: 0x0005C381
		public int TotalDamage
		{
			get
			{
				return this.InflictedDamage + this.ModifiedDamage;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001B24 RID: 6948 RVA: 0x0005E190 File Offset: 0x0005C390
		// (set) Token: 0x06001B25 RID: 6949 RVA: 0x0005E198 File Offset: 0x0005C398
		public float AttackProgress { get; internal set; }

		// Token: 0x06001B26 RID: 6950 RVA: 0x0005E1A4 File Offset: 0x0005C3A4
		public List<ValueTuple<string, uint>> GetLogString()
		{
			CombatLogData._logStringCache.Clear();
			if (this.IsValidForPlayer && ManagedOptions.GetConfig(ManagedOptions.ManagedOptionsType.ReportDamage) > 0f)
			{
				if (this.IsRangedAttack && this.IsAttackerPlayer && this.BodyPartHit == BoneBodyPartType.Head)
				{
					CombatLogData._logStringCache.Add(ValueTuple.Create<string, uint>(GameTexts.FindText("ui_head_shot", null).ToString(), 4289612505U));
				}
				if (this.IsFriendlyFire)
				{
					CombatLogData._logStringCache.Add(ValueTuple.Create<string, uint>(GameTexts.FindText("combat_log_friendly_fire", null).ToString(), 4289612505U));
				}
				if (this.CrushedThrough && !this.IsFriendlyFire)
				{
					if (this.IsAttackerPlayer)
					{
						CombatLogData._logStringCache.Add(ValueTuple.Create<string, uint>(GameTexts.FindText("combat_log_crushed_through_attacker", null).ToString(), 4289612505U));
					}
					else
					{
						CombatLogData._logStringCache.Add(ValueTuple.Create<string, uint>(GameTexts.FindText("combat_log_crushed_through_victim", null).ToString(), 4289612505U));
					}
				}
				if (this.Chamber)
				{
					CombatLogData._logStringCache.Add(ValueTuple.Create<string, uint>(GameTexts.FindText("combat_log_chamber_blocked", null).ToString(), 4289612505U));
				}
				uint item = 4290563554U;
				GameTexts.SetVariable("DAMAGE", this.TotalDamage);
				string variableName = "DAMAGE_TYPE";
				string id = "combat_log_damage_type";
				int num = (int)this.DamageType;
				GameTexts.SetVariable(variableName, GameTexts.FindText(id, num.ToString()));
				MBStringBuilder mbstringBuilder = default(MBStringBuilder);
				mbstringBuilder.Initialize(16, "GetLogString");
				if (this.IsVictimAgentSameAsAttackerAgent)
				{
					mbstringBuilder.Append<TextObject>(GameTexts.FindText("ui_received_number_damage_fall", null));
					item = 4292917946U;
				}
				else if (this.IsVictimMount)
				{
					if (this.IsVictimRiderAgentSameAsAttackerAgent)
					{
						mbstringBuilder.Append<TextObject>(GameTexts.FindText("ui_received_number_damage_fall_to_horse", null));
						item = 4292917946U;
					}
					else
					{
						mbstringBuilder.Append<TextObject>(GameTexts.FindText(this.IsAttackerPlayer ? "ui_delivered_number_damage_to_horse" : "ui_horse_received_number_damage", null));
						item = (this.IsAttackerPlayer ? 4210351871U : 4292917946U);
					}
				}
				else if (this.IsVictimEntity)
				{
					mbstringBuilder.Append<TextObject>(GameTexts.FindText("ui_delivered_number_damage_to_entity", null));
				}
				else if (this.IsAttackerMount)
				{
					mbstringBuilder.Append<TextObject>(GameTexts.FindText(this.IsAttackerPlayer ? "ui_horse_charged_for_number_damage" : "ui_received_number_damage", null));
					item = (this.IsAttackerPlayer ? 4210351871U : 4292917946U);
				}
				else if (this.TotalDamage > 0)
				{
					mbstringBuilder.Append<TextObject>(GameTexts.FindText(this.IsAttackerPlayer ? "ui_delivered_number_damage" : "ui_received_number_damage", null));
					item = (this.IsAttackerPlayer ? 4210351871U : 4292917946U);
				}
				if (this.BodyPartHit != BoneBodyPartType.None)
				{
					string variableName2 = "BODY_PART";
					string id2 = "body_part_type";
					num = (int)this.BodyPartHit;
					GameTexts.SetVariable(variableName2, GameTexts.FindText(id2, num.ToString()));
					mbstringBuilder.Append<string>("<Detail>");
					mbstringBuilder.Append<TextObject>(GameTexts.FindText("combat_log_detail_body_part", null));
					mbstringBuilder.Append<string>("</Detail>");
				}
				if (this.HitSpeed > 1E-05f)
				{
					GameTexts.SetVariable("SPEED", MathF.Round(this.HitSpeed, 2));
					mbstringBuilder.Append<string>("<Detail>");
					mbstringBuilder.Append<TextObject>(this.IsRangedAttack ? GameTexts.FindText("combat_log_detail_missile_speed", null) : GameTexts.FindText("combat_log_detail_move_speed", null));
					mbstringBuilder.Append<string>("</Detail>");
				}
				if (this.IsRangedAttack)
				{
					GameTexts.SetVariable("DISTANCE", MathF.Round(this.Distance, 1));
					mbstringBuilder.Append<string>("<Detail>");
					mbstringBuilder.Append<TextObject>(GameTexts.FindText("combat_log_detail_distance", null));
					mbstringBuilder.Append<string>("</Detail>");
				}
				if (this.AbsorbedDamage > 0)
				{
					GameTexts.SetVariable("ABSORBED_DAMAGE", this.AbsorbedDamage);
					mbstringBuilder.Append<string>("<Detail>");
					mbstringBuilder.Append<TextObject>(GameTexts.FindText("combat_log_detail_absorbed_damage", null));
					mbstringBuilder.Append<string>("</Detail>");
				}
				if (this.ModifiedDamage != 0)
				{
					GameTexts.SetVariable("MODIFIED_DAMAGE", MathF.Abs(this.ModifiedDamage));
					mbstringBuilder.Append<string>("<Detail>");
					if (this.ModifiedDamage > 0)
					{
						mbstringBuilder.Append<TextObject>(GameTexts.FindText("combat_log_detail_extra_damage", null));
					}
					else if (this.ModifiedDamage < 0)
					{
						mbstringBuilder.Append<TextObject>(GameTexts.FindText("combat_log_detail_reduced_damage", null));
					}
					mbstringBuilder.Append<string>("</Detail>");
				}
				CombatLogData._logStringCache.Add(ValueTuple.Create<string, uint>(mbstringBuilder.ToStringAndRelease(), item));
			}
			return CombatLogData._logStringCache;
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x0005E628 File Offset: 0x0005C828
		public CombatLogData(bool isVictimAgentSameAsAttackerAgent, bool isAttackerAgentHuman, bool isAttackerAgentMine, bool doesAttackerAgentHaveRiderAgent, bool isAttackerAgentRiderAgentMine, bool isAttackerAgentMount, bool isVictimAgentHuman, bool isVictimAgentMine, bool isVictimAgentDead, bool doesVictimAgentHaveRiderAgent, bool isVictimAgentRiderAgentIsMine, bool isVictimAgentMount, bool isVictimEntity, bool isVictimRiderAgentSameAsAttackerAgent, bool crushedThrough, bool chamber, float distance)
		{
			this.IsVictimAgentSameAsAttackerAgent = isVictimAgentSameAsAttackerAgent;
			this.IsAttackerAgentHuman = isAttackerAgentHuman;
			this.IsAttackerAgentMine = isAttackerAgentMine;
			this.DoesAttackerAgentHaveRiderAgent = doesAttackerAgentHaveRiderAgent;
			this.IsAttackerAgentRiderAgentMine = isAttackerAgentRiderAgentMine;
			this.IsAttackerAgentMount = isAttackerAgentMount;
			this.IsVictimAgentHuman = isVictimAgentHuman;
			this.IsVictimAgentMine = isVictimAgentMine;
			this.DoesVictimAgentHaveRiderAgent = doesVictimAgentHaveRiderAgent;
			this.IsVictimAgentRiderAgentMine = isVictimAgentRiderAgentIsMine;
			this.IsVictimAgentMount = isVictimAgentMount;
			this.IsVictimEntity = isVictimEntity;
			this.IsVictimRiderAgentSameAsAttackerAgent = isVictimRiderAgentSameAsAttackerAgent;
			this.IsFatalDamage = isVictimAgentDead;
			this.DamageType = DamageTypes.Blunt;
			this.CrushedThrough = crushedThrough;
			this.Chamber = chamber;
			this.IsRangedAttack = false;
			this.IsFriendlyFire = false;
			this.VictimAgentName = null;
			this.HitSpeed = 0f;
			this.InflictedDamage = 0;
			this.AbsorbedDamage = 0;
			this.ModifiedDamage = 0;
			this.AttackProgress = 0f;
			this.BodyPartHit = BoneBodyPartType.None;
			this.Distance = distance;
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0005E708 File Offset: 0x0005C908
		public void SetVictimAgent(Agent victimAgent)
		{
			if (((victimAgent != null) ? victimAgent.MissionPeer : null) != null)
			{
				this.VictimAgentName = victimAgent.MissionPeer.DisplayedName;
				return;
			}
			this.VictimAgentName = ((victimAgent != null) ? victimAgent.Name : null);
		}

		// Token: 0x040008A9 RID: 2217
		private const string DetailTagStart = "<Detail>";

		// Token: 0x040008AA RID: 2218
		private const string DetailTagEnd = "</Detail>";

		// Token: 0x040008AB RID: 2219
		private const uint DamageReceivedColor = 4292917946U;

		// Token: 0x040008AC RID: 2220
		private const uint DamageDealedColor = 4210351871U;

		// Token: 0x040008AD RID: 2221
		private static List<ValueTuple<string, uint>> _logStringCache = new List<ValueTuple<string, uint>>();

		// Token: 0x040008AE RID: 2222
		public readonly bool IsVictimAgentSameAsAttackerAgent;

		// Token: 0x040008AF RID: 2223
		public readonly bool IsVictimRiderAgentSameAsAttackerAgent;

		// Token: 0x040008B0 RID: 2224
		public readonly bool IsAttackerAgentHuman;

		// Token: 0x040008B1 RID: 2225
		public readonly bool IsAttackerAgentMine;

		// Token: 0x040008B2 RID: 2226
		public readonly bool DoesAttackerAgentHaveRiderAgent;

		// Token: 0x040008B3 RID: 2227
		public readonly bool IsAttackerAgentRiderAgentMine;

		// Token: 0x040008B4 RID: 2228
		public readonly bool IsAttackerAgentMount;

		// Token: 0x040008B5 RID: 2229
		public readonly bool IsVictimAgentHuman;

		// Token: 0x040008B6 RID: 2230
		public readonly bool IsVictimAgentMine;

		// Token: 0x040008B7 RID: 2231
		public readonly bool DoesVictimAgentHaveRiderAgent;

		// Token: 0x040008B8 RID: 2232
		public readonly bool IsVictimAgentRiderAgentMine;

		// Token: 0x040008B9 RID: 2233
		public readonly bool IsVictimAgentMount;

		// Token: 0x040008BA RID: 2234
		public bool IsVictimEntity;

		// Token: 0x040008BB RID: 2235
		public DamageTypes DamageType;

		// Token: 0x040008BC RID: 2236
		public bool CrushedThrough;

		// Token: 0x040008BD RID: 2237
		public bool Chamber;

		// Token: 0x040008BE RID: 2238
		public bool IsRangedAttack;

		// Token: 0x040008BF RID: 2239
		public bool IsFriendlyFire;

		// Token: 0x040008C0 RID: 2240
		public bool IsFatalDamage;

		// Token: 0x040008C1 RID: 2241
		public BoneBodyPartType BodyPartHit;

		// Token: 0x040008C2 RID: 2242
		public string VictimAgentName;

		// Token: 0x040008C3 RID: 2243
		public float HitSpeed;

		// Token: 0x040008C4 RID: 2244
		public int InflictedDamage;

		// Token: 0x040008C5 RID: 2245
		public int AbsorbedDamage;

		// Token: 0x040008C6 RID: 2246
		public int ModifiedDamage;

		// Token: 0x040008C8 RID: 2248
		public float Distance;
	}
}

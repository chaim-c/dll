using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200007E RID: 126
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class CombatLogNetworkMessage : GameNetworkMessage
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000844D File Offset: 0x0000664D
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x00008455 File Offset: 0x00006655
		public int AttackerAgentIndex { get; private set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000845E File Offset: 0x0000665E
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x00008466 File Offset: 0x00006666
		public int VictimAgentIndex { get; private set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000846F File Offset: 0x0000666F
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x00008477 File Offset: 0x00006677
		public bool IsVictimEntity { get; private set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00008480 File Offset: 0x00006680
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x00008488 File Offset: 0x00006688
		public DamageTypes DamageType { get; private set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00008491 File Offset: 0x00006691
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x00008499 File Offset: 0x00006699
		public bool CrushedThrough { get; private set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x000084A2 File Offset: 0x000066A2
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x000084AA File Offset: 0x000066AA
		public bool Chamber { get; private set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x000084B3 File Offset: 0x000066B3
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x000084BB File Offset: 0x000066BB
		public bool IsRangedAttack { get; private set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x000084C4 File Offset: 0x000066C4
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x000084CC File Offset: 0x000066CC
		public bool IsFriendlyFire { get; private set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x000084D5 File Offset: 0x000066D5
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x000084DD File Offset: 0x000066DD
		public bool IsFatalDamage { get; private set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x000084E6 File Offset: 0x000066E6
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x000084EE File Offset: 0x000066EE
		public BoneBodyPartType BodyPartHit { get; private set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x000084F7 File Offset: 0x000066F7
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x000084FF File Offset: 0x000066FF
		public float HitSpeed { get; private set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00008508 File Offset: 0x00006708
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x00008510 File Offset: 0x00006710
		public float Distance { get; private set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00008519 File Offset: 0x00006719
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x00008521 File Offset: 0x00006721
		public int InflictedDamage { get; private set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0000852A File Offset: 0x0000672A
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x00008532 File Offset: 0x00006732
		public int AbsorbedDamage { get; private set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000853B File Offset: 0x0000673B
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x00008543 File Offset: 0x00006743
		public int ModifiedDamage { get; private set; }

		// Token: 0x060004AA RID: 1194 RVA: 0x0000854C File Offset: 0x0000674C
		public CombatLogNetworkMessage()
		{
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00008554 File Offset: 0x00006754
		public CombatLogNetworkMessage(int attackerAgentIndex, int victimAgentIndex, GameEntity hitEntity, CombatLogData combatLogData)
		{
			this.AttackerAgentIndex = attackerAgentIndex;
			this.VictimAgentIndex = victimAgentIndex;
			this.IsVictimEntity = (hitEntity != null);
			this.DamageType = combatLogData.DamageType;
			this.CrushedThrough = combatLogData.CrushedThrough;
			this.Chamber = combatLogData.Chamber;
			this.IsRangedAttack = combatLogData.IsRangedAttack;
			this.IsFriendlyFire = combatLogData.IsFriendlyFire;
			this.IsFatalDamage = combatLogData.IsFatalDamage;
			this.BodyPartHit = combatLogData.BodyPartHit;
			this.HitSpeed = combatLogData.HitSpeed;
			this.Distance = combatLogData.Distance;
			this.InflictedDamage = combatLogData.InflictedDamage;
			this.AbsorbedDamage = combatLogData.AbsorbedDamage;
			this.ModifiedDamage = combatLogData.ModifiedDamage;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00008620 File Offset: 0x00006820
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.AttackerAgentIndex);
			GameNetworkMessage.WriteAgentIndexToPacket(this.VictimAgentIndex);
			GameNetworkMessage.WriteBoolToPacket(this.IsVictimEntity);
			GameNetworkMessage.WriteIntToPacket((int)this.DamageType, CompressionBasic.AgentHitDamageTypeCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(this.CrushedThrough);
			GameNetworkMessage.WriteBoolToPacket(this.Chamber);
			GameNetworkMessage.WriteBoolToPacket(this.IsRangedAttack);
			GameNetworkMessage.WriteBoolToPacket(this.IsFriendlyFire);
			GameNetworkMessage.WriteBoolToPacket(this.IsFatalDamage);
			GameNetworkMessage.WriteIntToPacket((int)this.BodyPartHit, CompressionBasic.AgentHitBodyPartCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this.HitSpeed, CompressionBasic.AgentHitRelativeSpeedCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this.Distance, CompressionBasic.AgentHitRelativeSpeedCompressionInfo);
			this.AbsorbedDamage = MBMath.ClampInt(this.AbsorbedDamage, 0, 2000);
			this.InflictedDamage = MBMath.ClampInt(this.InflictedDamage, 0, 2000);
			this.ModifiedDamage = MBMath.ClampInt(this.ModifiedDamage, -2000, 2000);
			GameNetworkMessage.WriteIntToPacket(this.AbsorbedDamage, CompressionBasic.AgentHitDamageCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.InflictedDamage, CompressionBasic.AgentHitDamageCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.ModifiedDamage, CompressionBasic.AgentHitModifiedDamageCompressionInfo);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00008740 File Offset: 0x00006940
		protected override bool OnRead()
		{
			bool result = true;
			this.AttackerAgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.VictimAgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.IsVictimEntity = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.DamageType = (DamageTypes)GameNetworkMessage.ReadIntFromPacket(CompressionBasic.AgentHitDamageTypeCompressionInfo, ref result);
			this.CrushedThrough = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.Chamber = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.IsRangedAttack = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.IsFriendlyFire = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.IsFatalDamage = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.BodyPartHit = (BoneBodyPartType)GameNetworkMessage.ReadIntFromPacket(CompressionBasic.AgentHitBodyPartCompressionInfo, ref result);
			this.HitSpeed = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.AgentHitRelativeSpeedCompressionInfo, ref result);
			this.Distance = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.AgentHitRelativeSpeedCompressionInfo, ref result);
			this.AbsorbedDamage = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.AgentHitDamageCompressionInfo, ref result);
			this.InflictedDamage = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.AgentHitDamageCompressionInfo, ref result);
			this.ModifiedDamage = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.AgentHitModifiedDamageCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00008837 File Offset: 0x00006A37
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.AgentsDetailed;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000883F File Offset: 0x00006A3F
		protected override string OnGetLogFormat()
		{
			return "Agent got hit.";
		}
	}
}

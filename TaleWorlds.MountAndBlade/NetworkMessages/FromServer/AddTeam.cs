using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000071 RID: 113
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class AddTeam : GameNetworkMessage
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x00007799 File Offset: 0x00005999
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x000077A1 File Offset: 0x000059A1
		public int TeamIndex { get; private set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x000077AA File Offset: 0x000059AA
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x000077B2 File Offset: 0x000059B2
		public BattleSideEnum Side { get; private set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x000077BB File Offset: 0x000059BB
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x000077C3 File Offset: 0x000059C3
		public uint Color { get; private set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x000077CC File Offset: 0x000059CC
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x000077D4 File Offset: 0x000059D4
		public uint Color2 { get; private set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x000077DD File Offset: 0x000059DD
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x000077E5 File Offset: 0x000059E5
		public string BannerCode { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x000077EE File Offset: 0x000059EE
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x000077F6 File Offset: 0x000059F6
		public bool IsPlayerGeneral { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x000077FF File Offset: 0x000059FF
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x00007807 File Offset: 0x00005A07
		public bool IsPlayerSergeant { get; private set; }

		// Token: 0x0600040B RID: 1035 RVA: 0x00007810 File Offset: 0x00005A10
		public AddTeam(int teamIndex, BattleSideEnum side, uint color, uint color2, string bannerCode, bool isPlayerGeneral, bool isPlayerSergeant)
		{
			this.TeamIndex = teamIndex;
			this.Side = side;
			this.Color = color;
			this.Color2 = color2;
			this.BannerCode = bannerCode;
			this.IsPlayerGeneral = isPlayerGeneral;
			this.IsPlayerSergeant = isPlayerSergeant;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000784D File Offset: 0x00005A4D
		public AddTeam()
		{
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00007858 File Offset: 0x00005A58
		protected override bool OnRead()
		{
			bool result = true;
			this.TeamIndex = GameNetworkMessage.ReadTeamIndexFromPacket(ref result);
			this.Side = (BattleSideEnum)GameNetworkMessage.ReadIntFromPacket(CompressionMission.TeamSideCompressionInfo, ref result);
			this.Color = GameNetworkMessage.ReadUintFromPacket(CompressionBasic.ColorCompressionInfo, ref result);
			this.Color2 = GameNetworkMessage.ReadUintFromPacket(CompressionBasic.ColorCompressionInfo, ref result);
			this.BannerCode = GameNetworkMessage.ReadStringFromPacket(ref result);
			this.IsPlayerGeneral = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.IsPlayerSergeant = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000078D4 File Offset: 0x00005AD4
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteTeamIndexToPacket(this.TeamIndex);
			GameNetworkMessage.WriteIntToPacket((int)this.Side, CompressionMission.TeamSideCompressionInfo);
			GameNetworkMessage.WriteUintToPacket(this.Color, CompressionBasic.ColorCompressionInfo);
			GameNetworkMessage.WriteUintToPacket(this.Color2, CompressionBasic.ColorCompressionInfo);
			GameNetworkMessage.WriteStringToPacket(this.BannerCode);
			GameNetworkMessage.WriteBoolToPacket(this.IsPlayerGeneral);
			GameNetworkMessage.WriteBoolToPacket(this.IsPlayerSergeant);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000793D File Offset: 0x00005B3D
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Mission;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00007945 File Offset: 0x00005B45
		protected override string OnGetLogFormat()
		{
			return "Add team with side: " + this.Side;
		}
	}
}

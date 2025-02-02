using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000D5 RID: 213
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class NotificationMessage : GameNetworkMessage
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0000E847 File Offset: 0x0000CA47
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x0000E84F File Offset: 0x0000CA4F
		public int Message { get; private set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0000E858 File Offset: 0x0000CA58
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x0000E860 File Offset: 0x0000CA60
		public int ParameterOne { get; private set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0000E869 File Offset: 0x0000CA69
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x0000E871 File Offset: 0x0000CA71
		public int ParameterTwo { get; private set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0000E87A File Offset: 0x0000CA7A
		private bool HasParameterOne
		{
			get
			{
				return this.ParameterOne != -1;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0000E888 File Offset: 0x0000CA88
		private bool HasParameterTwo
		{
			get
			{
				return this.ParameterOne != -1;
			}
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0000E896 File Offset: 0x0000CA96
		public NotificationMessage(int message, int param1, int param2)
		{
			this.Message = message;
			this.ParameterOne = param1;
			this.ParameterTwo = param2;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0000E8B3 File Offset: 0x0000CAB3
		public NotificationMessage()
		{
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0000E8BC File Offset: 0x0000CABC
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.Message, CompressionMission.MultiplayerNotificationCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(this.HasParameterOne);
			if (this.HasParameterOne)
			{
				GameNetworkMessage.WriteIntToPacket(this.ParameterOne, CompressionMission.MultiplayerNotificationParameterCompressionInfo);
				GameNetworkMessage.WriteBoolToPacket(this.HasParameterTwo);
				if (this.HasParameterTwo)
				{
					GameNetworkMessage.WriteIntToPacket(this.ParameterTwo, CompressionMission.MultiplayerNotificationParameterCompressionInfo);
				}
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0000E920 File Offset: 0x0000CB20
		protected override bool OnRead()
		{
			bool result = true;
			this.ParameterOne = (this.ParameterTwo = -1);
			this.Message = GameNetworkMessage.ReadIntFromPacket(CompressionMission.MultiplayerNotificationCompressionInfo, ref result);
			if (GameNetworkMessage.ReadBoolFromPacket(ref result))
			{
				this.ParameterOne = GameNetworkMessage.ReadIntFromPacket(CompressionMission.MultiplayerNotificationParameterCompressionInfo, ref result);
				if (GameNetworkMessage.ReadBoolFromPacket(ref result))
				{
					this.ParameterTwo = GameNetworkMessage.ReadIntFromPacket(CompressionMission.MultiplayerNotificationParameterCompressionInfo, ref result);
				}
			}
			return result;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0000E988 File Offset: 0x0000CB88
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Messaging;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0000E98C File Offset: 0x0000CB8C
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Receiving message: ",
				this.Message,
				this.HasParameterOne ? (" With first parameter: " + this.ParameterOne) : "",
				this.HasParameterTwo ? (" and second parameter: " + this.ParameterTwo) : ""
			});
		}
	}
}

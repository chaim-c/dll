using System;
using TaleWorlds.Engine.Options;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000037 RID: 55
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class SyncRelevantGameOptionsToServer : GameNetworkMessage
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00003F85 File Offset: 0x00002185
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00003F8D File Offset: 0x0000218D
		public bool SendMeBloodEvents { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00003F96 File Offset: 0x00002196
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00003F9E File Offset: 0x0000219E
		public bool SendMeSoundEvents { get; private set; }

		// Token: 0x060001BA RID: 442 RVA: 0x00003FA7 File Offset: 0x000021A7
		public SyncRelevantGameOptionsToServer()
		{
			this.SendMeBloodEvents = true;
			this.SendMeSoundEvents = true;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00003FBD File Offset: 0x000021BD
		public void InitializeOptions()
		{
			this.SendMeBloodEvents = BannerlordConfig.ShowBlood;
			this.SendMeSoundEvents = (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.SoundVolume) > 0.01f && NativeOptions.GetConfig(NativeOptions.NativeOptionsType.MasterVolume) > 0.01f);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00003FF0 File Offset: 0x000021F0
		protected override bool OnRead()
		{
			bool result = true;
			this.SendMeBloodEvents = GameNetworkMessage.ReadBoolFromPacket(ref result);
			this.SendMeSoundEvents = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000401A File Offset: 0x0000221A
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteBoolToPacket(this.SendMeBloodEvents);
			GameNetworkMessage.WriteBoolToPacket(this.SendMeSoundEvents);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00004032 File Offset: 0x00002232
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.General;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00004036 File Offset: 0x00002236
		protected override string OnGetLogFormat()
		{
			return "SyncRelevantGameOptionsToServer";
		}
	}
}

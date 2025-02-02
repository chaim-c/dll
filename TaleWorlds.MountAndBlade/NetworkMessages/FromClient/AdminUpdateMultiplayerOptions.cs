using System;
using System.Collections.Generic;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000014 RID: 20
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class AdminUpdateMultiplayerOptions : GameNetworkMessage
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002A82 File Offset: 0x00000C82
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00002A8A File Offset: 0x00000C8A
		public List<AdminUpdateMultiplayerOptions.AdminMultiplayerOptionInfo> Options { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002A93 File Offset: 0x00000C93
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00002A9B File Offset: 0x00000C9B
		public int OptionCount { get; private set; }

		// Token: 0x06000090 RID: 144 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public AdminUpdateMultiplayerOptions()
		{
			this.Options = new List<AdminUpdateMultiplayerOptions.AdminMultiplayerOptionInfo>();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002AB7 File Offset: 0x00000CB7
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002ABF File Offset: 0x00000CBF
		protected override string OnGetLogFormat()
		{
			return "Admin requesting update multiplayer options on server";
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002AC8 File Offset: 0x00000CC8
		protected override bool OnRead()
		{
			bool result = true;
			this.OptionCount = GameNetworkMessage.ReadIntFromPacket(new CompressionInfo.Integer(0, 43, true), ref result);
			for (int i = 0; i < this.OptionCount; i++)
			{
				AdminUpdateMultiplayerOptions.AdminMultiplayerOptionInfo item = this.ReadOptionInfoFromPacket(ref result);
				this.Options.Add(item);
			}
			return result;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002B14 File Offset: 0x00000D14
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.Options.Count, new CompressionInfo.Integer(0, 43, true));
			for (int i = 0; i < this.Options.Count; i++)
			{
				this.WriteOptionInfoToPacket(this.Options[i]);
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002B64 File Offset: 0x00000D64
		public void AddMultiplayerOption(MultiplayerOptions.OptionType optionType, MultiplayerOptions.MultiplayerOptionsAccessMode accessMode, bool value)
		{
			AdminUpdateMultiplayerOptions.AdminMultiplayerOptionInfo adminMultiplayerOptionInfo = new AdminUpdateMultiplayerOptions.AdminMultiplayerOptionInfo(optionType, accessMode);
			adminMultiplayerOptionInfo.SetValue(value);
			this.Options.Add(adminMultiplayerOptionInfo);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002B8C File Offset: 0x00000D8C
		public void AddMultiplayerOption(MultiplayerOptions.OptionType optionType, MultiplayerOptions.MultiplayerOptionsAccessMode accessMode, int value)
		{
			AdminUpdateMultiplayerOptions.AdminMultiplayerOptionInfo adminMultiplayerOptionInfo = new AdminUpdateMultiplayerOptions.AdminMultiplayerOptionInfo(optionType, accessMode);
			adminMultiplayerOptionInfo.SetValue(value);
			this.Options.Add(adminMultiplayerOptionInfo);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002BB4 File Offset: 0x00000DB4
		public void AddMultiplayerOption(MultiplayerOptions.OptionType optionType, MultiplayerOptions.MultiplayerOptionsAccessMode accessMode, string value)
		{
			AdminUpdateMultiplayerOptions.AdminMultiplayerOptionInfo adminMultiplayerOptionInfo = new AdminUpdateMultiplayerOptions.AdminMultiplayerOptionInfo(optionType, accessMode);
			adminMultiplayerOptionInfo.SetValue(value);
			this.Options.Add(adminMultiplayerOptionInfo);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002BDC File Offset: 0x00000DDC
		private AdminUpdateMultiplayerOptions.AdminMultiplayerOptionInfo ReadOptionInfoFromPacket(ref bool bufferReadValid)
		{
			int optionType = GameNetworkMessage.ReadIntFromPacket(new CompressionInfo.Integer(0, 43, true), ref bufferReadValid);
			MultiplayerOptions.MultiplayerOptionsAccessMode accessMode = (MultiplayerOptions.MultiplayerOptionsAccessMode)GameNetworkMessage.ReadIntFromPacket(new CompressionInfo.Integer(0, 3, true), ref bufferReadValid);
			AdminUpdateMultiplayerOptions.AdminMultiplayerOptionInfo adminMultiplayerOptionInfo = new AdminUpdateMultiplayerOptions.AdminMultiplayerOptionInfo((MultiplayerOptions.OptionType)optionType, accessMode);
			MultiplayerOptionsProperty optionProperty = ((MultiplayerOptions.OptionType)optionType).GetOptionProperty();
			switch (optionProperty.OptionValueType)
			{
			case MultiplayerOptions.OptionValueType.Bool:
			{
				bool value = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
				adminMultiplayerOptionInfo.SetValue(value);
				break;
			}
			case MultiplayerOptions.OptionValueType.Integer:
			case MultiplayerOptions.OptionValueType.Enum:
			{
				int value2 = GameNetworkMessage.ReadIntFromPacket(new CompressionInfo.Integer(optionProperty.BoundsMin, optionProperty.BoundsMax, true), ref bufferReadValid);
				adminMultiplayerOptionInfo.SetValue(value2);
				break;
			}
			case MultiplayerOptions.OptionValueType.String:
			{
				string value3 = GameNetworkMessage.ReadStringFromPacket(ref bufferReadValid);
				adminMultiplayerOptionInfo.SetValue(value3);
				break;
			}
			}
			return adminMultiplayerOptionInfo;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002C7C File Offset: 0x00000E7C
		private void WriteOptionInfoToPacket(AdminUpdateMultiplayerOptions.AdminMultiplayerOptionInfo optionInfo)
		{
			GameNetworkMessage.WriteIntToPacket((int)optionInfo.OptionType, new CompressionInfo.Integer(0, 43, true));
			GameNetworkMessage.WriteIntToPacket((int)optionInfo.AccessMode, new CompressionInfo.Integer(0, 3, true));
			MultiplayerOptionsProperty optionProperty = optionInfo.OptionType.GetOptionProperty();
			switch (optionProperty.OptionValueType)
			{
			case MultiplayerOptions.OptionValueType.Bool:
				GameNetworkMessage.WriteBoolToPacket(optionInfo.BoolValue);
				return;
			case MultiplayerOptions.OptionValueType.Integer:
			case MultiplayerOptions.OptionValueType.Enum:
				GameNetworkMessage.WriteIntToPacket(optionInfo.IntValue, new CompressionInfo.Integer(optionProperty.BoundsMin, optionProperty.BoundsMax, true));
				return;
			case MultiplayerOptions.OptionValueType.String:
				GameNetworkMessage.WriteStringToPacket(optionInfo.StringValue);
				return;
			default:
				return;
			}
		}

		// Token: 0x020003D4 RID: 980
		public class AdminMultiplayerOptionInfo
		{
			// Token: 0x17000934 RID: 2356
			// (get) Token: 0x060033AE RID: 13230 RVA: 0x000D6210 File Offset: 0x000D4410
			public MultiplayerOptions.OptionType OptionType { get; }

			// Token: 0x17000935 RID: 2357
			// (get) Token: 0x060033AF RID: 13231 RVA: 0x000D6218 File Offset: 0x000D4418
			public MultiplayerOptions.MultiplayerOptionsAccessMode AccessMode { get; }

			// Token: 0x17000936 RID: 2358
			// (get) Token: 0x060033B0 RID: 13232 RVA: 0x000D6220 File Offset: 0x000D4420
			// (set) Token: 0x060033B1 RID: 13233 RVA: 0x000D6228 File Offset: 0x000D4428
			public string StringValue { get; private set; }

			// Token: 0x17000937 RID: 2359
			// (get) Token: 0x060033B2 RID: 13234 RVA: 0x000D6231 File Offset: 0x000D4431
			// (set) Token: 0x060033B3 RID: 13235 RVA: 0x000D6239 File Offset: 0x000D4439
			public bool BoolValue { get; private set; }

			// Token: 0x17000938 RID: 2360
			// (get) Token: 0x060033B4 RID: 13236 RVA: 0x000D6242 File Offset: 0x000D4442
			// (set) Token: 0x060033B5 RID: 13237 RVA: 0x000D624A File Offset: 0x000D444A
			public int IntValue { get; private set; }

			// Token: 0x060033B6 RID: 13238 RVA: 0x000D6253 File Offset: 0x000D4453
			public AdminMultiplayerOptionInfo(MultiplayerOptions.OptionType optionType, MultiplayerOptions.MultiplayerOptionsAccessMode accessMode)
			{
				this.OptionType = optionType;
				this.AccessMode = accessMode;
			}

			// Token: 0x060033B7 RID: 13239 RVA: 0x000D6269 File Offset: 0x000D4469
			internal void SetValue(string value)
			{
				this.StringValue = value;
			}

			// Token: 0x060033B8 RID: 13240 RVA: 0x000D6272 File Offset: 0x000D4472
			internal void SetValue(bool value)
			{
				this.BoolValue = value;
			}

			// Token: 0x060033B9 RID: 13241 RVA: 0x000D627B File Offset: 0x000D447B
			internal void SetValue(int value)
			{
				this.IntValue = value;
			}
		}
	}
}

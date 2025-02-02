using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000060 RID: 96
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class MultiplayerOptionsInitial : GameNetworkMessage
	{
		// Token: 0x0600035D RID: 861 RVA: 0x0000679C File Offset: 0x0000499C
		public MultiplayerOptionsInitial()
		{
			this._optionList = new List<MultiplayerOptions.MultiplayerOption>();
			for (MultiplayerOptions.OptionType optionType = MultiplayerOptions.OptionType.ServerName; optionType < MultiplayerOptions.OptionType.NumOfSlots; optionType++)
			{
				if (optionType.GetOptionProperty().Replication == MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad)
				{
					this._optionList.Add(MultiplayerOptions.Instance.GetOptionFromOptionType(optionType, MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
				}
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000067EC File Offset: 0x000049EC
		public MultiplayerOptions.MultiplayerOption GetOption(MultiplayerOptions.OptionType optionType)
		{
			return this._optionList.First((MultiplayerOptions.MultiplayerOption x) => x.OptionType == optionType);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00006820 File Offset: 0x00004A20
		protected override bool OnRead()
		{
			bool result = true;
			this._optionList = new List<MultiplayerOptions.MultiplayerOption>();
			for (MultiplayerOptions.OptionType optionType = MultiplayerOptions.OptionType.ServerName; optionType < MultiplayerOptions.OptionType.NumOfSlots; optionType++)
			{
				MultiplayerOptionsProperty optionProperty = optionType.GetOptionProperty();
				if (optionProperty.Replication == MultiplayerOptionsProperty.ReplicationOccurrence.AtMapLoad)
				{
					MultiplayerOptions.MultiplayerOption multiplayerOption = MultiplayerOptions.MultiplayerOption.CreateMultiplayerOption(optionType);
					switch (optionProperty.OptionValueType)
					{
					case MultiplayerOptions.OptionValueType.Bool:
						multiplayerOption.UpdateValue(GameNetworkMessage.ReadBoolFromPacket(ref result));
						break;
					case MultiplayerOptions.OptionValueType.Integer:
					case MultiplayerOptions.OptionValueType.Enum:
						multiplayerOption.UpdateValue(GameNetworkMessage.ReadIntFromPacket(new CompressionInfo.Integer(optionProperty.BoundsMin, optionProperty.BoundsMax, true), ref result));
						break;
					case MultiplayerOptions.OptionValueType.String:
						multiplayerOption.UpdateValue(GameNetworkMessage.ReadStringFromPacket(ref result));
						break;
					}
					this._optionList.Add(multiplayerOption);
				}
			}
			return result;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000068D4 File Offset: 0x00004AD4
		protected override void OnWrite()
		{
			foreach (MultiplayerOptions.MultiplayerOption multiplayerOption in this._optionList)
			{
				MultiplayerOptions.OptionType optionType = multiplayerOption.OptionType;
				MultiplayerOptionsProperty optionProperty = optionType.GetOptionProperty();
				switch (optionProperty.OptionValueType)
				{
				case MultiplayerOptions.OptionValueType.Bool:
					GameNetworkMessage.WriteBoolToPacket(optionType.GetBoolValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
					break;
				case MultiplayerOptions.OptionValueType.Integer:
				case MultiplayerOptions.OptionValueType.Enum:
					GameNetworkMessage.WriteIntToPacket(optionType.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions), new CompressionInfo.Integer(optionProperty.BoundsMin, optionProperty.BoundsMax, true));
					break;
				case MultiplayerOptions.OptionValueType.String:
					GameNetworkMessage.WriteStringToPacket(optionType.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
					break;
				}
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00006988 File Offset: 0x00004B88
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00006990 File Offset: 0x00004B90
		protected override string OnGetLogFormat()
		{
			return "Receiving initial multiplayer options.";
		}

		// Token: 0x040000A4 RID: 164
		private List<MultiplayerOptions.MultiplayerOption> _optionList;
	}
}

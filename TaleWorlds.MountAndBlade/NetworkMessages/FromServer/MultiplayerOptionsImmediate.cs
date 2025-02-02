using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200005F RID: 95
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class MultiplayerOptionsImmediate : GameNetworkMessage
	{
		// Token: 0x06000357 RID: 855 RVA: 0x000065A0 File Offset: 0x000047A0
		public MultiplayerOptionsImmediate()
		{
			this._optionList = new List<MultiplayerOptions.MultiplayerOption>();
			for (MultiplayerOptions.OptionType optionType = MultiplayerOptions.OptionType.ServerName; optionType < MultiplayerOptions.OptionType.NumOfSlots; optionType++)
			{
				if (optionType.GetOptionProperty().Replication == MultiplayerOptionsProperty.ReplicationOccurrence.Immediately)
				{
					this._optionList.Add(MultiplayerOptions.Instance.GetOptionFromOptionType(optionType, MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
				}
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000065F0 File Offset: 0x000047F0
		public MultiplayerOptions.MultiplayerOption GetOption(MultiplayerOptions.OptionType optionType)
		{
			return this._optionList.First((MultiplayerOptions.MultiplayerOption x) => x.OptionType == optionType);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00006624 File Offset: 0x00004824
		protected override bool OnRead()
		{
			bool result = true;
			this._optionList = new List<MultiplayerOptions.MultiplayerOption>();
			for (MultiplayerOptions.OptionType optionType = MultiplayerOptions.OptionType.ServerName; optionType < MultiplayerOptions.OptionType.NumOfSlots; optionType++)
			{
				MultiplayerOptionsProperty optionProperty = optionType.GetOptionProperty();
				if (optionProperty.Replication == MultiplayerOptionsProperty.ReplicationOccurrence.Immediately)
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

		// Token: 0x0600035A RID: 858 RVA: 0x000066D8 File Offset: 0x000048D8
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

		// Token: 0x0600035B RID: 859 RVA: 0x0000678C File Offset: 0x0000498C
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00006794 File Offset: 0x00004994
		protected override string OnGetLogFormat()
		{
			return "Receiving runtime multiplayer options.";
		}

		// Token: 0x040000A3 RID: 163
		private List<MultiplayerOptions.MultiplayerOption> _optionList;
	}
}

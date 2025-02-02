using System;
using System.Collections.Generic;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200005E RID: 94
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class MultiplayerOptionsDefault : GameNetworkMessage
	{
		// Token: 0x06000352 RID: 850 RVA: 0x0000640C File Offset: 0x0000460C
		public MultiplayerOptionsDefault()
		{
			this._optionList = new List<MultiplayerOptions.OptionType>();
			for (MultiplayerOptions.OptionType optionType = MultiplayerOptions.OptionType.ServerName; optionType < MultiplayerOptions.OptionType.NumOfSlots; optionType++)
			{
				if (optionType.GetOptionProperty().Replication != MultiplayerOptionsProperty.ReplicationOccurrence.Never)
				{
					this._optionList.Add(optionType);
				}
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00006450 File Offset: 0x00004650
		protected override bool OnRead()
		{
			bool result = true;
			for (int i = 0; i < this._optionList.Count; i++)
			{
				MultiplayerOptions.OptionType optionType = this._optionList[i];
				MultiplayerOptionsProperty optionProperty = optionType.GetOptionProperty();
				switch (optionProperty.OptionValueType)
				{
				case MultiplayerOptions.OptionValueType.Bool:
				{
					bool value = GameNetworkMessage.ReadBoolFromPacket(ref result);
					optionType.SetValue(value, MultiplayerOptions.MultiplayerOptionsAccessMode.DefaultMapOptions);
					break;
				}
				case MultiplayerOptions.OptionValueType.Integer:
				case MultiplayerOptions.OptionValueType.Enum:
				{
					int value2 = GameNetworkMessage.ReadIntFromPacket(new CompressionInfo.Integer(optionProperty.BoundsMin, optionProperty.BoundsMax, true), ref result);
					optionType.SetValue(value2, MultiplayerOptions.MultiplayerOptionsAccessMode.DefaultMapOptions);
					break;
				}
				case MultiplayerOptions.OptionValueType.String:
				{
					string value3 = GameNetworkMessage.ReadStringFromPacket(ref result);
					optionType.SetValue(value3, MultiplayerOptions.MultiplayerOptionsAccessMode.DefaultMapOptions);
					break;
				}
				}
			}
			return result;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00006500 File Offset: 0x00004700
		protected override void OnWrite()
		{
			for (int i = 0; i < this._optionList.Count; i++)
			{
				MultiplayerOptions.OptionType optionType = this._optionList[i];
				MultiplayerOptionsProperty optionProperty = optionType.GetOptionProperty();
				switch (optionProperty.OptionValueType)
				{
				case MultiplayerOptions.OptionValueType.Bool:
					GameNetworkMessage.WriteBoolToPacket(optionType.GetBoolValue(MultiplayerOptions.MultiplayerOptionsAccessMode.DefaultMapOptions));
					break;
				case MultiplayerOptions.OptionValueType.Integer:
				case MultiplayerOptions.OptionValueType.Enum:
					GameNetworkMessage.WriteIntToPacket(optionType.GetIntValue(MultiplayerOptions.MultiplayerOptionsAccessMode.DefaultMapOptions), new CompressionInfo.Integer(optionProperty.BoundsMin, optionProperty.BoundsMax, true));
					break;
				case MultiplayerOptions.OptionValueType.String:
					GameNetworkMessage.WriteStringToPacket(optionType.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.DefaultMapOptions));
					break;
				}
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00006590 File Offset: 0x00004790
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00006598 File Offset: 0x00004798
		protected override string OnGetLogFormat()
		{
			return "Receiving default multiplayer options.";
		}

		// Token: 0x040000A2 RID: 162
		private readonly List<MultiplayerOptions.OptionType> _optionList;
	}
}

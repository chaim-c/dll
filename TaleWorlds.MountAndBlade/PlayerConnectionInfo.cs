using System;
using System.Collections.Generic;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000311 RID: 785
	public class PlayerConnectionInfo
	{
		// Token: 0x06002AA5 RID: 10917 RVA: 0x000A5425 File Offset: 0x000A3625
		public PlayerConnectionInfo(PlayerId playerID)
		{
			this.PlayerID = playerID;
			this._parameters = new Dictionary<string, object>();
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000A543F File Offset: 0x000A363F
		public void AddParameter(string name, object parameter)
		{
			if (!this._parameters.ContainsKey(name))
			{
				this._parameters.Add(name, parameter);
			}
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000A545C File Offset: 0x000A365C
		public T GetParameter<T>(string name) where T : class
		{
			if (this._parameters.ContainsKey(name))
			{
				return this._parameters[name] as T;
			}
			return default(T);
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x000A5497 File Offset: 0x000A3697
		// (set) Token: 0x06002AA9 RID: 10921 RVA: 0x000A549F File Offset: 0x000A369F
		public int SessionKey { get; set; }

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002AAA RID: 10922 RVA: 0x000A54A8 File Offset: 0x000A36A8
		// (set) Token: 0x06002AAB RID: 10923 RVA: 0x000A54B0 File Offset: 0x000A36B0
		public string Name { get; set; }

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06002AAC RID: 10924 RVA: 0x000A54B9 File Offset: 0x000A36B9
		// (set) Token: 0x06002AAD RID: 10925 RVA: 0x000A54C1 File Offset: 0x000A36C1
		public NetworkCommunicator NetworkPeer { get; set; }

		// Token: 0x04001062 RID: 4194
		private Dictionary<string, object> _parameters;

		// Token: 0x04001066 RID: 4198
		public readonly PlayerId PlayerID;
	}
}

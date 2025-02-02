﻿using System;
using System.Threading.Tasks;

namespace TaleWorlds.Diamond
{
	// Token: 0x0200000D RID: 13
	public interface IClient
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000040 RID: 64
		bool IsInCriticalState { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000041 RID: 65
		long AliveCheckTimeInMiliSeconds { get; }

		// Token: 0x06000042 RID: 66
		void HandleMessage(Message message);

		// Token: 0x06000043 RID: 67
		void OnConnected();

		// Token: 0x06000044 RID: 68
		void OnCantConnect();

		// Token: 0x06000045 RID: 69
		void OnDisconnected();

		// Token: 0x06000046 RID: 70
		Task<bool> CheckConnection();

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000047 RID: 71
		ILoginAccessProvider AccessProvider { get; }
	}
}

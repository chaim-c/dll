using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002B3 RID: 691
	public enum ConnectionClosedReason
	{
		// Token: 0x0400085B RID: 2139
		Unknown,
		// Token: 0x0400085C RID: 2140
		ClosedByLocalUser,
		// Token: 0x0400085D RID: 2141
		ClosedByPeer,
		// Token: 0x0400085E RID: 2142
		TimedOut,
		// Token: 0x0400085F RID: 2143
		TooManyConnections,
		// Token: 0x04000860 RID: 2144
		InvalidMessage,
		// Token: 0x04000861 RID: 2145
		InvalidData,
		// Token: 0x04000862 RID: 2146
		ConnectionFailed,
		// Token: 0x04000863 RID: 2147
		ConnectionClosed,
		// Token: 0x04000864 RID: 2148
		NegotiationFailed,
		// Token: 0x04000865 RID: 2149
		UnexpectedError
	}
}

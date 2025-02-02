using System;

namespace Epic.OnlineServices
{
	// Token: 0x0200000D RID: 13
	internal interface IGettable<T> where T : struct
	{
		// Token: 0x06000081 RID: 129
		void Get(out T other);
	}
}

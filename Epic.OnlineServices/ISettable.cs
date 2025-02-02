using System;

namespace Epic.OnlineServices
{
	// Token: 0x0200000E RID: 14
	internal interface ISettable<T> where T : struct
	{
		// Token: 0x06000082 RID: 130
		void Set(ref T other);

		// Token: 0x06000083 RID: 131
		void Set(ref T? other);
	}
}

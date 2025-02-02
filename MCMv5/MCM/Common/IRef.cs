using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MCM.Common
{
	// Token: 0x02000017 RID: 23
	[NullableContext(2)]
	public interface IRef : INotifyPropertyChanged
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006F RID: 111
		[Nullable(1)]
		Type Type { [NullableContext(1)] get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000070 RID: 112
		// (set) Token: 0x06000071 RID: 113
		object Value { get; set; }
	}
}

using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x02000117 RID: 279
	[ExcludeFromCodeCoverage]
	internal sealed class KeyValue<TKey, TValue>
	{
		// Token: 0x060006A4 RID: 1700 RVA: 0x00014FF7 File Offset: 0x000131F7
		public KeyValue(TKey key, TValue value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x040001EF RID: 495
		public readonly TKey Key;

		// Token: 0x040001F0 RID: 496
		public readonly TValue Value;
	}
}

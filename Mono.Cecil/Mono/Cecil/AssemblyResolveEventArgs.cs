using System;

namespace Mono.Cecil
{
	// Token: 0x02000090 RID: 144
	public sealed class AssemblyResolveEventArgs : EventArgs
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00014C6D File Offset: 0x00012E6D
		public AssemblyNameReference AssemblyReference
		{
			get
			{
				return this.reference;
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00014C75 File Offset: 0x00012E75
		public AssemblyResolveEventArgs(AssemblyNameReference reference)
		{
			this.reference = reference;
		}

		// Token: 0x040003DD RID: 989
		private readonly AssemblyNameReference reference;
	}
}

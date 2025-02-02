using System;

namespace TaleWorlds.DotNet
{
	// Token: 0x02000023 RID: 35
	public class ManagedDelegate : DotNetObject
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003F04 File Offset: 0x00002104
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003F0C File Offset: 0x0000210C
		public ManagedDelegate.DelegateDefinition Instance
		{
			get
			{
				return this._instance;
			}
			set
			{
				this._instance = value;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003F1D File Offset: 0x0000211D
		[LibraryCallback]
		public void InvokeAux()
		{
			this.Instance();
		}

		// Token: 0x04000048 RID: 72
		private ManagedDelegate.DelegateDefinition _instance;

		// Token: 0x0200003E RID: 62
		// (Invoke) Token: 0x0600015E RID: 350
		public delegate void DelegateDefinition();
	}
}

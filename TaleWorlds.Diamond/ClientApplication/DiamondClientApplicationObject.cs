using System;
using TaleWorlds.Library;

namespace TaleWorlds.Diamond.ClientApplication
{
	// Token: 0x0200005E RID: 94
	public abstract class DiamondClientApplicationObject
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00006900 File Offset: 0x00004B00
		public DiamondClientApplication Application
		{
			get
			{
				return this._application;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00006908 File Offset: 0x00004B08
		public ApplicationVersion ApplicationVersion
		{
			get
			{
				return this.Application.ApplicationVersion;
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00006915 File Offset: 0x00004B15
		protected DiamondClientApplicationObject(DiamondClientApplication application)
		{
			this._application = application;
		}

		// Token: 0x040000CE RID: 206
		private DiamondClientApplication _application;
	}
}

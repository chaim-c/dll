using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000234 RID: 564
	public class BannerBuilderState : GameState
	{
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x0006E380 File Offset: 0x0006C580
		public override bool IsMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x0006E383 File Offset: 0x0006C583
		public string DefaultBannerKey { get; }

		// Token: 0x06001EE3 RID: 7907 RVA: 0x0006E38B File Offset: 0x0006C58B
		public BannerBuilderState()
		{
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x0006E393 File Offset: 0x0006C593
		public BannerBuilderState(string defaultBannerKey)
		{
			this.DefaultBannerKey = defaultBannerKey;
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x0006E3A2 File Offset: 0x0006C5A2
		protected override void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x0006E3AA File Offset: 0x0006C5AA
		protected override void OnFinalize()
		{
			base.OnFinalize();
		}
	}
}

using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.ComponentInterfaces
{
	// Token: 0x020003CC RID: 972
	public abstract class FormationArrangementModel : GameModel
	{
		// Token: 0x06003391 RID: 13201
		public abstract List<FormationArrangementModel.ArrangementPosition> GetBannerBearerPositions(Formation formation, int maxCount);

		// Token: 0x02000669 RID: 1641
		public struct ArrangementPosition
		{
			// Token: 0x17000A31 RID: 2609
			// (get) Token: 0x06003D98 RID: 15768 RVA: 0x000ED98B File Offset: 0x000EBB8B
			public bool IsValid
			{
				get
				{
					return this.FileIndex > -1 && this.RankIndex > -1;
				}
			}

			// Token: 0x17000A32 RID: 2610
			// (get) Token: 0x06003D99 RID: 15769 RVA: 0x000ED9A4 File Offset: 0x000EBBA4
			public static FormationArrangementModel.ArrangementPosition Invalid
			{
				get
				{
					return default(FormationArrangementModel.ArrangementPosition);
				}
			}

			// Token: 0x06003D9A RID: 15770 RVA: 0x000ED9BA File Offset: 0x000EBBBA
			public ArrangementPosition(int fileIndex = -1, int rankIndex = -1)
			{
				this.FileIndex = fileIndex;
				this.RankIndex = rankIndex;
			}

			// Token: 0x04002134 RID: 8500
			public readonly int FileIndex;

			// Token: 0x04002135 RID: 8501
			public readonly int RankIndex;
		}
	}
}

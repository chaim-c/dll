using System;
using System.Collections.Generic;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000039 RID: 57
	internal class TwoDimensionDrawLayer
	{
		// Token: 0x0600028E RID: 654 RVA: 0x00009E98 File Offset: 0x00008098
		public TwoDimensionDrawLayer()
		{
			this._drawData = new List<TwoDimensionDrawData>(2);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00009EAC File Offset: 0x000080AC
		public void Reset()
		{
			this._drawData.Clear();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00009EB9 File Offset: 0x000080B9
		public void AddDrawData(TwoDimensionDrawData drawData)
		{
			this._drawData.Add(drawData);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00009EC8 File Offset: 0x000080C8
		public void DrawTo(TwoDimensionContext twoDimensionContext, int layer)
		{
			for (int i = 0; i < this._drawData.Count; i++)
			{
				this._drawData[i].DrawTo(twoDimensionContext, layer);
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009F04 File Offset: 0x00008104
		public bool IsIntersects(Rectangle rectangle)
		{
			for (int i = 0; i < this._drawData.Count; i++)
			{
				if (this._drawData[i].IsIntersects(rectangle))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400014A RID: 330
		private List<TwoDimensionDrawData> _drawData;
	}
}

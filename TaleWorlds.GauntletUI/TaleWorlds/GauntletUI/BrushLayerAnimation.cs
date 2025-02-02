using System;
using TaleWorlds.Library;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000012 RID: 18
	public class BrushLayerAnimation
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00006B3C File Offset: 0x00004D3C
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00006B44 File Offset: 0x00004D44
		public string LayerName { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00006B4D File Offset: 0x00004D4D
		public MBReadOnlyList<BrushAnimationProperty> Collections
		{
			get
			{
				return this._collections;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006B55 File Offset: 0x00004D55
		public BrushLayerAnimation()
		{
			this.LayerName = null;
			this._collections = new MBList<BrushAnimationProperty>();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006B6F File Offset: 0x00004D6F
		internal void RemoveAnimationProperty(BrushAnimationProperty property)
		{
			this._collections.Remove(property);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006B7E File Offset: 0x00004D7E
		public void AddAnimationProperty(BrushAnimationProperty property)
		{
			this._collections.Add(property);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006B8C File Offset: 0x00004D8C
		private void FillFrom(BrushLayerAnimation brushLayerAnimation)
		{
			this.LayerName = brushLayerAnimation.LayerName;
			this._collections = new MBList<BrushAnimationProperty>();
			foreach (BrushAnimationProperty brushAnimationProperty in brushLayerAnimation._collections)
			{
				BrushAnimationProperty item = brushAnimationProperty.Clone();
				this._collections.Add(item);
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006C00 File Offset: 0x00004E00
		public BrushLayerAnimation Clone()
		{
			BrushLayerAnimation brushLayerAnimation = new BrushLayerAnimation();
			brushLayerAnimation.FillFrom(this);
			return brushLayerAnimation;
		}

		// Token: 0x04000069 RID: 105
		private MBList<BrushAnimationProperty> _collections;
	}
}

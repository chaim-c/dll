using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000032 RID: 50
	public class ParallaxContainerWidget : Widget
	{
		// Token: 0x060002DE RID: 734 RVA: 0x0000961E File Offset: 0x0000781E
		public ParallaxContainerWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00009634 File Offset: 0x00007834
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			using (List<ParallaxItemBrushWidget>.Enumerator enumerator = this._parallaxItems.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					switch (enumerator.Current.InitialDirection)
					{
					}
				}
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000096A8 File Offset: 0x000078A8
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			ParallaxItemBrushWidget item;
			if ((item = (child as ParallaxItemBrushWidget)) != null)
			{
				this._parallaxItems.Add(item);
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000096D4 File Offset: 0x000078D4
		protected override void OnChildRemoved(Widget child)
		{
			base.OnChildRemoved(child);
			ParallaxItemBrushWidget item;
			if ((item = (child as ParallaxItemBrushWidget)) != null)
			{
				this._parallaxItems.Remove(item);
			}
		}

		// Token: 0x0400012C RID: 300
		private List<ParallaxItemBrushWidget> _parallaxItems = new List<ParallaxItemBrushWidget>();
	}
}

using System;
using System.Numerics;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x02000057 RID: 87
	public class BasicContainer : Container
	{
		// Token: 0x0600058E RID: 1422 RVA: 0x00017A78 File Offset: 0x00015C78
		public BasicContainer(UIContext context) : base(context)
		{
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00017A81 File Offset: 0x00015C81
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x00017A89 File Offset: 0x00015C89
		public override Predicate<Widget> AcceptDropPredicate { get; set; }

		// Token: 0x06000591 RID: 1425 RVA: 0x00017A92 File Offset: 0x00015C92
		public override Vector2 GetDropGizmoPosition(Vector2 draggedWidgetPosition)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00017A99 File Offset: 0x00015C99
		public override int GetIndexForDrop(Vector2 draggedWidgetPosition)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00017AA0 File Offset: 0x00015CA0
		public override bool IsDragHovering { get; }

		// Token: 0x06000594 RID: 1428 RVA: 0x00017AA8 File Offset: 0x00015CA8
		public override void OnChildSelected(Widget widget)
		{
			int intValue = -1;
			for (int i = 0; i < base.ChildCount; i++)
			{
				if (widget == base.GetChild(i))
				{
					intValue = i;
				}
			}
			base.IntValue = intValue;
		}
	}
}

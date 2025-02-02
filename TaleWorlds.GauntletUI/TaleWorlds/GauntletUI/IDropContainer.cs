using System;
using System.Numerics;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000029 RID: 41
	public interface IDropContainer
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600031E RID: 798
		// (set) Token: 0x0600031F RID: 799
		Predicate<Widget> AcceptDropPredicate { get; set; }

		// Token: 0x06000320 RID: 800
		Vector2 GetDropGizmoPosition(Vector2 draggedWidgetPosition);
	}
}

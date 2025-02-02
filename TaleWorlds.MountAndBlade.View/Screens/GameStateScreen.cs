using System;

namespace TaleWorlds.MountAndBlade.View.Screens
{
	// Token: 0x02000030 RID: 48
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class GameStateScreen : Attribute
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000FA00 File Offset: 0x0000DC00
		public Type GameStateType { get; private set; }

		// Token: 0x060001CF RID: 463 RVA: 0x0000FA09 File Offset: 0x0000DC09
		public GameStateScreen(Type gameStateType)
		{
			this.GameStateType = gameStateType;
		}
	}
}

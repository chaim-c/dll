using System;
using TaleWorlds.Engine;

namespace SandBox.BoardGames.Objects
{
	// Token: 0x020000CD RID: 205
	public class BoardGameDecal : ScriptComponentBehavior
	{
		// Token: 0x06000A55 RID: 2645 RVA: 0x0004C8BA File Offset: 0x0004AABA
		protected override void OnInit()
		{
			base.OnInit();
			this.SetAlpha(0f);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0004C8CD File Offset: 0x0004AACD
		public void SetAlpha(float alpha)
		{
			base.GameEntity.SetAlpha(alpha);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0004C8DB File Offset: 0x0004AADB
		protected override bool MovesEntity()
		{
			return false;
		}
	}
}

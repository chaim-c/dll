using System;
using TaleWorlds.Engine;

namespace SandBox.BoardGames.Pawns
{
	// Token: 0x020000C9 RID: 201
	public class PawnMuTorere : PawnBase
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0004C335 File Offset: 0x0004A535
		// (set) Token: 0x06000A33 RID: 2611 RVA: 0x0004C33D File Offset: 0x0004A53D
		public int X { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0004C346 File Offset: 0x0004A546
		public override bool IsPlaced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0004C349 File Offset: 0x0004A549
		public PawnMuTorere(GameEntity entity, bool playerOne) : base(entity, playerOne)
		{
			this.X = -1;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0004C35A File Offset: 0x0004A55A
		public override void Reset()
		{
			base.Reset();
			this.X = -1;
		}
	}
}

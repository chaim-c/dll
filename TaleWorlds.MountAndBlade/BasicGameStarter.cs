using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001E0 RID: 480
	public class BasicGameStarter : IGameStarter
	{
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x0005D4D6 File Offset: 0x0005B6D6
		IEnumerable<GameModel> IGameStarter.Models
		{
			get
			{
				return this._models;
			}
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0005D4DE File Offset: 0x0005B6DE
		public BasicGameStarter()
		{
			this._models = new List<GameModel>();
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x0005D4F1 File Offset: 0x0005B6F1
		void IGameStarter.AddModel(GameModel gameModel)
		{
			this._models.Add(gameModel);
		}

		// Token: 0x04000860 RID: 2144
		private List<GameModel> _models;
	}
}

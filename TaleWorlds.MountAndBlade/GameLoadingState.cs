using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000236 RID: 566
	public class GameLoadingState : GameState
	{
		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x0006E3CA File Offset: 0x0006C5CA
		public override bool IsMusicMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x0006E3D5 File Offset: 0x0006C5D5
		public void SetLoadingParameters(MBGameManager gameLoader)
		{
			Game.OnGameCreated += this.OnGameCreated;
			this._gameLoader = gameLoader;
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x0006E3EF File Offset: 0x0006C5EF
		protected override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (!this._loadingFinished)
			{
				this._loadingFinished = this._gameLoader.DoLoadingForGameManager();
				return;
			}
			GameStateManager.Current = Game.Current.GameStateManager;
			this._gameLoader.OnLoadFinished();
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x0006E42C File Offset: 0x0006C62C
		private void OnGameCreated()
		{
			Game.OnGameCreated -= this.OnGameCreated;
			Game.Current.OnItemDeserializedEvent += delegate(ItemObject itemObject)
			{
				if (itemObject.Type == ItemObject.ItemTypeEnum.HandArmor)
				{
					Utilities.RegisterMeshForGPUMorph(itemObject.MultiMeshName);
				}
			};
		}

		// Token: 0x04000B5E RID: 2910
		private bool _loadingFinished;

		// Token: 0x04000B5F RID: 2911
		private MBGameManager _gameLoader;
	}
}

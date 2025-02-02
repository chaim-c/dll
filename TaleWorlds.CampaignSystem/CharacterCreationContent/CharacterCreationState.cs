using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.CharacterCreationContent
{
	// Token: 0x020001DE RID: 478
	public class CharacterCreationState : PlayerGameState
	{
		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x0007FA2E File Offset: 0x0007DC2E
		// (set) Token: 0x06001C6F RID: 7279 RVA: 0x0007FA36 File Offset: 0x0007DC36
		public CharacterCreation CharacterCreation
		{
			get
			{
				return this._characterCreation;
			}
			private set
			{
				this._characterCreation = value;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x0007FA3F File Offset: 0x0007DC3F
		// (set) Token: 0x06001C71 RID: 7281 RVA: 0x0007FA47 File Offset: 0x0007DC47
		public ICharacterCreationStateHandler Handler
		{
			get
			{
				return this._handler;
			}
			set
			{
				this._handler = value;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x0007FA50 File Offset: 0x0007DC50
		// (set) Token: 0x06001C73 RID: 7283 RVA: 0x0007FA58 File Offset: 0x0007DC58
		public CharacterCreationStageBase CurrentStage { get; private set; }

		// Token: 0x06001C74 RID: 7284 RVA: 0x0007FA64 File Offset: 0x0007DC64
		public CharacterCreationState(CharacterCreationContentBase baseContent)
		{
			this.CharacterCreation = new CharacterCreation();
			this.CurrentCharacterCreationContent = baseContent;
			this.CurrentCharacterCreationContent.Initialize(this.CharacterCreation);
			this._stages = new List<KeyValuePair<int, Type>>();
			int key = 0;
			foreach (Type type in this.CurrentCharacterCreationContent.CharacterCreationStages)
			{
				if (type.IsSubclassOf(typeof(CharacterCreationStageBase)))
				{
					this._stages.Add(new KeyValuePair<int, Type>(key, type));
				}
				else
				{
					Debug.FailedAssert("Invalid character creation stage type: " + type.Name, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CharacterCreationContent\\CharacterCreationState.cs", ".ctor", 54);
				}
			}
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x0007FB34 File Offset: 0x0007DD34
		public CharacterCreationState()
		{
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x0007FB43 File Offset: 0x0007DD43
		protected override void OnInitialize()
		{
			base.OnInitialize();
			Game.Current.GameStateManager.RegisterActiveStateDisableRequest(this);
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x0007FB5B File Offset: 0x0007DD5B
		protected override void OnActivate()
		{
			base.OnActivate();
			if (this._stageIndex == -1 && this.CharacterCreation != null)
			{
				this.NextStage();
			}
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x0007FB7C File Offset: 0x0007DD7C
		public void FinalizeCharacterCreation()
		{
			this.CharacterCreation.ApplyFinalEffects();
			Game.Current.GameStateManager.UnregisterActiveStateDisableRequest(this);
			Game.Current.GameStateManager.CleanAndPushState(Game.Current.GameStateManager.CreateState<MapState>(), 0);
			PartyBase.MainParty.SetVisualAsDirty();
			ICharacterCreationStateHandler handler = this._handler;
			if (handler != null)
			{
				handler.OnCharacterCreationFinalized();
			}
			this.CurrentCharacterCreationContent.OnCharacterCreationFinalized();
			CampaignEventDispatcher.Instance.OnCharacterCreationIsOver();
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x0007FBF4 File Offset: 0x0007DDF4
		public void NextStage()
		{
			this._stageIndex++;
			CharacterCreationStageBase currentStage = this.CurrentStage;
			if (currentStage != null)
			{
				currentStage.OnFinalize();
			}
			this._furthestStageIndex = MathF.Max(this._furthestStageIndex, this._stageIndex);
			if (this._stageIndex == this._stages.Count)
			{
				this.FinalizeCharacterCreation();
				return;
			}
			Type value = this._stages[this._stageIndex].Value;
			this.CreateStage(value);
			this.Refresh();
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x0007FC78 File Offset: 0x0007DE78
		public void PreviousStage()
		{
			CharacterCreationStageBase currentStage = this.CurrentStage;
			if (currentStage != null)
			{
				currentStage.OnFinalize();
			}
			this._stageIndex--;
			Type value = this._stages[this._stageIndex].Value;
			this.CreateStage(value);
			this.Refresh();
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x0007FCCB File Offset: 0x0007DECB
		private void CreateStage(Type type)
		{
			this.CurrentStage = (Activator.CreateInstance(type, new object[]
			{
				this
			}) as CharacterCreationStageBase);
			ICharacterCreationStateHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnStageCreated(this.CurrentStage);
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0007FD00 File Offset: 0x0007DF00
		public void Refresh()
		{
			if (this.CurrentStage == null || this._stageIndex < 0 || this._stageIndex >= this._stages.Count)
			{
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CharacterCreationContent\\CharacterCreationState.cs", "Refresh", 139);
				return;
			}
			ICharacterCreationStateHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnRefresh();
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x0007FD5B File Offset: 0x0007DF5B
		public int GetTotalStagesCount()
		{
			return this._stages.Count;
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x0007FD68 File Offset: 0x0007DF68
		public int GetIndexOfCurrentStage()
		{
			return this._stageIndex;
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x0007FD70 File Offset: 0x0007DF70
		public int GetFurthestIndex()
		{
			return this._furthestStageIndex;
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x0007FD78 File Offset: 0x0007DF78
		public void GoToStage(int stageIndex)
		{
			if (stageIndex >= 0 && stageIndex < this._stages.Count && stageIndex != this._stageIndex && stageIndex <= this._furthestStageIndex)
			{
				this._stageIndex = stageIndex + 1;
				this.PreviousStage();
			}
		}

		// Token: 0x040008E8 RID: 2280
		private CharacterCreation _characterCreation;

		// Token: 0x040008E9 RID: 2281
		private ICharacterCreationStateHandler _handler;

		// Token: 0x040008EA RID: 2282
		private readonly List<KeyValuePair<int, Type>> _stages;

		// Token: 0x040008EB RID: 2283
		private int _stageIndex = -1;

		// Token: 0x040008ED RID: 2285
		private int _furthestStageIndex;

		// Token: 0x040008EE RID: 2286
		public readonly CharacterCreationContentBase CurrentCharacterCreationContent;
	}
}

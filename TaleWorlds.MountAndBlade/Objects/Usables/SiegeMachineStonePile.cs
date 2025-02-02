using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Objects.Siege;

namespace TaleWorlds.MountAndBlade.Objects.Usables
{
	// Token: 0x0200038A RID: 906
	public class SiegeMachineStonePile : UsableMachine, ISpawnable
	{
		// Token: 0x06003194 RID: 12692 RVA: 0x000CC97F File Offset: 0x000CAB7F
		protected internal override void OnInit()
		{
			base.OnInit();
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x000CC987 File Offset: 0x000CAB87
		public override TextObject GetActionTextForStandingPoint(UsableMissionObject usableGameObject)
		{
			if (usableGameObject.GameEntity.HasTag(this.AmmoPickUpTag))
			{
				TextObject textObject = new TextObject("{=jfcceEoE}{PILE_TYPE} Pile", null);
				textObject.SetTextVariable("PILE_TYPE", new TextObject("{=1CPdu9K0}Stone", null));
				return textObject;
			}
			return TextObject.Empty;
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x000CC9C4 File Offset: 0x000CABC4
		public override string GetDescriptionText(GameEntity gameEntity = null)
		{
			if (gameEntity.HasTag(this.AmmoPickUpTag))
			{
				TextObject textObject = new TextObject("{=bNYm3K6b}{KEY} Pick Up", null);
				textObject.SetTextVariable("KEY", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("CombatHotKeyCategory", 13)));
				return textObject.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06003197 RID: 12695 RVA: 0x000CCA12 File Offset: 0x000CAC12
		public void SetSpawnedFromSpawner()
		{
			this._spawnedFromSpawner = true;
		}

		// Token: 0x06003198 RID: 12696 RVA: 0x000CCA1B File Offset: 0x000CAC1B
		public override OrderType GetOrder(BattleSideEnum side)
		{
			return OrderType.None;
		}

		// Token: 0x04001539 RID: 5433
		private bool _spawnedFromSpawner;
	}
}

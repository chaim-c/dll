using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.View.Scripts
{
	// Token: 0x0200003B RID: 59
	public class PopupSceneSwitchItemSequence : PopupSceneSequence
	{
		// Token: 0x060002AF RID: 687 RVA: 0x0001886D File Offset: 0x00016A6D
		public override void OnInitialState()
		{
			this.AttachItem(this.InitialItem, this.InitialBodyPart);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00018881 File Offset: 0x00016A81
		public override void OnPositiveState()
		{
			this.AttachItem(this.PositiveItem, this.PositiveBodyPart);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00018895 File Offset: 0x00016A95
		public override void OnNegativeState()
		{
			this.AttachItem(this.NegativeItem, this.NegativeBodyPart);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000188AC File Offset: 0x00016AAC
		private EquipmentIndex StringToEquipmentIndex(PopupSceneSwitchItemSequence.BodyPartIndex part)
		{
			switch (part)
			{
			case PopupSceneSwitchItemSequence.BodyPartIndex.None:
				return EquipmentIndex.None;
			case PopupSceneSwitchItemSequence.BodyPartIndex.Weapon0:
				return EquipmentIndex.WeaponItemBeginSlot;
			case PopupSceneSwitchItemSequence.BodyPartIndex.Weapon1:
				return EquipmentIndex.Weapon1;
			case PopupSceneSwitchItemSequence.BodyPartIndex.Weapon2:
				return EquipmentIndex.Weapon2;
			case PopupSceneSwitchItemSequence.BodyPartIndex.Weapon3:
				return EquipmentIndex.Weapon3;
			case PopupSceneSwitchItemSequence.BodyPartIndex.ExtraWeaponSlot:
				return EquipmentIndex.ExtraWeaponSlot;
			case PopupSceneSwitchItemSequence.BodyPartIndex.Head:
				return EquipmentIndex.NumAllWeaponSlots;
			case PopupSceneSwitchItemSequence.BodyPartIndex.Body:
				return EquipmentIndex.Body;
			case PopupSceneSwitchItemSequence.BodyPartIndex.Leg:
				return EquipmentIndex.Leg;
			case PopupSceneSwitchItemSequence.BodyPartIndex.Gloves:
				return EquipmentIndex.Gloves;
			case PopupSceneSwitchItemSequence.BodyPartIndex.Cape:
				return EquipmentIndex.Cape;
			case PopupSceneSwitchItemSequence.BodyPartIndex.Horse:
				return EquipmentIndex.ArmorItemEndSlot;
			case PopupSceneSwitchItemSequence.BodyPartIndex.HorseHarness:
				return EquipmentIndex.HorseHarness;
			default:
				return EquipmentIndex.None;
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00018914 File Offset: 0x00016B14
		private void AttachItem(string itemName, PopupSceneSwitchItemSequence.BodyPartIndex bodyPart)
		{
			if (this._agentVisuals == null)
			{
				return;
			}
			EquipmentIndex equipmentIndex = this.StringToEquipmentIndex(bodyPart);
			if (equipmentIndex != EquipmentIndex.None)
			{
				AgentVisualsData copyAgentVisualsData = this._agentVisuals.GetCopyAgentVisualsData();
				Equipment equipment = this._agentVisuals.GetEquipment().Clone(false);
				if (itemName == "")
				{
					equipment.AddEquipmentToSlotWithoutAgent(equipmentIndex, default(EquipmentElement));
				}
				else
				{
					equipment.AddEquipmentToSlotWithoutAgent(equipmentIndex, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>(itemName), null, null, false));
				}
				copyAgentVisualsData.RightWieldedItemIndex(0).LeftWieldedItemIndex(-1).Equipment(equipment);
				this._agentVisuals.Refresh(false, copyAgentVisualsData, false);
			}
		}

		// Token: 0x040001E0 RID: 480
		public string InitialItem;

		// Token: 0x040001E1 RID: 481
		public string PositiveItem;

		// Token: 0x040001E2 RID: 482
		public string NegativeItem;

		// Token: 0x040001E3 RID: 483
		public PopupSceneSwitchItemSequence.BodyPartIndex InitialBodyPart;

		// Token: 0x040001E4 RID: 484
		public PopupSceneSwitchItemSequence.BodyPartIndex PositiveBodyPart;

		// Token: 0x040001E5 RID: 485
		public PopupSceneSwitchItemSequence.BodyPartIndex NegativeBodyPart;

		// Token: 0x020000A5 RID: 165
		public enum BodyPartIndex
		{
			// Token: 0x04000366 RID: 870
			None,
			// Token: 0x04000367 RID: 871
			Weapon0,
			// Token: 0x04000368 RID: 872
			Weapon1,
			// Token: 0x04000369 RID: 873
			Weapon2,
			// Token: 0x0400036A RID: 874
			Weapon3,
			// Token: 0x0400036B RID: 875
			ExtraWeaponSlot,
			// Token: 0x0400036C RID: 876
			Head,
			// Token: 0x0400036D RID: 877
			Body,
			// Token: 0x0400036E RID: 878
			Leg,
			// Token: 0x0400036F RID: 879
			Gloves,
			// Token: 0x04000370 RID: 880
			Cape,
			// Token: 0x04000371 RID: 881
			Horse,
			// Token: 0x04000372 RID: 882
			HorseHarness
		}
	}
}

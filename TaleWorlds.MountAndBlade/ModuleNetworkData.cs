using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002F6 RID: 758
	public static class ModuleNetworkData
	{
		// Token: 0x06002955 RID: 10581 RVA: 0x0009EA8C File Offset: 0x0009CC8C
		public static EquipmentElement ReadItemReferenceFromPacket(MBObjectManager objectManager, ref bool bufferReadValid)
		{
			MBObjectBase mbobjectBase = GameNetworkMessage.ReadObjectReferenceFromPacket(objectManager, CompressionBasic.GUIDCompressionInfo, ref bufferReadValid);
			bool flag = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
			MBObjectBase mbobjectBase2 = null;
			if (flag)
			{
				mbobjectBase2 = GameNetworkMessage.ReadObjectReferenceFromPacket(objectManager, CompressionBasic.GUIDCompressionInfo, ref bufferReadValid);
			}
			ItemObject item = mbobjectBase as ItemObject;
			return new EquipmentElement(item, null, mbobjectBase2 as ItemObject, false);
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x0009EAD0 File Offset: 0x0009CCD0
		public static void WriteItemReferenceToPacket(EquipmentElement equipElement)
		{
			GameNetworkMessage.WriteObjectReferenceToPacket(equipElement.Item, CompressionBasic.GUIDCompressionInfo);
			if (equipElement.CosmeticItem != null)
			{
				GameNetworkMessage.WriteBoolToPacket(true);
				GameNetworkMessage.WriteObjectReferenceToPacket(equipElement.CosmeticItem, CompressionBasic.GUIDCompressionInfo);
				return;
			}
			GameNetworkMessage.WriteBoolToPacket(false);
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x0009EB08 File Offset: 0x0009CD08
		public static MissionWeapon ReadWeaponReferenceFromPacket(MBObjectManager objectManager, ref bool bufferReadValid)
		{
			if (GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid))
			{
				return MissionWeapon.Invalid;
			}
			MBObjectBase mbobjectBase = GameNetworkMessage.ReadObjectReferenceFromPacket(objectManager, CompressionBasic.GUIDCompressionInfo, ref bufferReadValid);
			int num = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.ItemDataValueCompressionInfo, ref bufferReadValid);
			int num2 = GameNetworkMessage.ReadIntFromPacket(CompressionMission.WeaponReloadPhaseCompressionInfo, ref bufferReadValid);
			short currentUsageIndex = (short)GameNetworkMessage.ReadIntFromPacket(CompressionMission.WeaponUsageIndexCompressionInfo, ref bufferReadValid);
			bool flag = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
			Banner banner = null;
			if (flag)
			{
				string bannerKey = GameNetworkMessage.ReadBannerCodeFromPacket(ref bufferReadValid);
				if (bufferReadValid)
				{
					banner = new Banner(bannerKey);
				}
			}
			ItemObject primaryItem = mbobjectBase as ItemObject;
			bool flag2 = GameNetworkMessage.ReadBoolFromPacket(ref bufferReadValid);
			MissionWeapon? ammoWeapon = null;
			if (bufferReadValid && flag2)
			{
				MBObjectBase mbobjectBase2 = GameNetworkMessage.ReadObjectReferenceFromPacket(objectManager, CompressionBasic.GUIDCompressionInfo, ref bufferReadValid);
				int num3 = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.ItemDataValueCompressionInfo, ref bufferReadValid);
				ItemObject primaryItem2 = mbobjectBase2 as ItemObject;
				ammoWeapon = new MissionWeapon?(new MissionWeapon(primaryItem2, null, banner, (short)num3));
			}
			return new MissionWeapon(primaryItem, null, banner, (short)num, (short)num2, ammoWeapon)
			{
				CurrentUsageIndex = (int)currentUsageIndex
			};
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x0009EBE0 File Offset: 0x0009CDE0
		public static void WriteWeaponReferenceToPacket(MissionWeapon weapon)
		{
			GameNetworkMessage.WriteBoolToPacket(weapon.IsEmpty);
			if (!weapon.IsEmpty)
			{
				GameNetworkMessage.WriteObjectReferenceToPacket(weapon.Item, CompressionBasic.GUIDCompressionInfo);
				GameNetworkMessage.WriteIntToPacket((int)weapon.RawDataForNetwork, CompressionBasic.ItemDataValueCompressionInfo);
				GameNetworkMessage.WriteIntToPacket((int)weapon.ReloadPhase, CompressionMission.WeaponReloadPhaseCompressionInfo);
				GameNetworkMessage.WriteIntToPacket(weapon.CurrentUsageIndex, CompressionMission.WeaponUsageIndexCompressionInfo);
				bool flag = weapon.Banner != null;
				GameNetworkMessage.WriteBoolToPacket(flag);
				if (flag)
				{
					GameNetworkMessage.WriteBannerCodeToPacket(weapon.Banner.Serialize());
				}
				MissionWeapon ammoWeapon = weapon.AmmoWeapon;
				bool flag2 = !ammoWeapon.IsEmpty;
				GameNetworkMessage.WriteBoolToPacket(flag2);
				if (flag2)
				{
					GameNetworkMessage.WriteObjectReferenceToPacket(ammoWeapon.Item, CompressionBasic.GUIDCompressionInfo);
					GameNetworkMessage.WriteIntToPacket((int)ammoWeapon.RawDataForNetwork, CompressionBasic.ItemDataValueCompressionInfo);
				}
			}
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x0009ECA8 File Offset: 0x0009CEA8
		public static MissionWeapon ReadMissileWeaponReferenceFromPacket(MBObjectManager objectManager, ref bool bufferReadValid)
		{
			MBObjectBase mbobjectBase = GameNetworkMessage.ReadObjectReferenceFromPacket(objectManager, CompressionBasic.GUIDCompressionInfo, ref bufferReadValid);
			short currentUsageIndex = (short)GameNetworkMessage.ReadIntFromPacket(CompressionMission.WeaponUsageIndexCompressionInfo, ref bufferReadValid);
			ItemObject primaryItem = mbobjectBase as ItemObject;
			return new MissionWeapon(primaryItem, null, null, 1)
			{
				CurrentUsageIndex = (int)currentUsageIndex
			};
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x0009ECE8 File Offset: 0x0009CEE8
		public static void WriteMissileWeaponReferenceToPacket(MissionWeapon weapon)
		{
			GameNetworkMessage.WriteObjectReferenceToPacket(weapon.Item, CompressionBasic.GUIDCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(weapon.CurrentUsageIndex, CompressionMission.WeaponUsageIndexCompressionInfo);
		}
	}
}

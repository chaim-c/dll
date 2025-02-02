using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001AC RID: 428
	[ScriptingInterfaceBase]
	internal interface IMBGameEntityExtensions
	{
		// Token: 0x06001782 RID: 6018
		[EngineMethod("create_from_weapon", false)]
		GameEntity CreateFromWeapon(UIntPtr scenePointer, in WeaponData weaponData, WeaponStatsData[] weaponStatsData, int weaponStatsDataLength, in WeaponData ammoWeaponData, WeaponStatsData[] ammoWeaponStatsData, int ammoWeaponStatsDataLength, bool showHolsterWithWeapon);

		// Token: 0x06001783 RID: 6019
		[EngineMethod("fade_out", false)]
		void FadeOut(UIntPtr entityPointer, float interval, bool isRemovingFromScene);

		// Token: 0x06001784 RID: 6020
		[EngineMethod("fade_in", false)]
		void FadeIn(UIntPtr entityPointer, bool resetAlpha);

		// Token: 0x06001785 RID: 6021
		[EngineMethod("hide_if_not_fading_out", false)]
		void HideIfNotFadingOut(UIntPtr entityPointer);
	}
}

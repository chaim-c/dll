using System;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000225 RID: 549
	public static class GameKeyTextExtensions
	{
		// Token: 0x06001E85 RID: 7813 RVA: 0x0006CE5F File Offset: 0x0006B05F
		public static TextObject GetHotKeyGameText(this GameTextManager gameTextManager, string categoryName, string hotKeyId)
		{
			return gameTextManager.GetHotKeyGameTextFromKeyID(HotKeyManager.GetHotKeyId(categoryName, hotKeyId));
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x0006CE6E File Offset: 0x0006B06E
		public static TextObject GetHotKeyGameText(this GameTextManager gameTextManager, string categoryName, int gameKeyId)
		{
			return gameTextManager.GetHotKeyGameTextFromKeyID(HotKeyManager.GetHotKeyId(categoryName, gameKeyId));
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x0006CE7D File Offset: 0x0006B07D
		public static TextObject GetHotKeyGameTextFromKeyID(this GameTextManager gameTextManager, string keyId)
		{
			return gameTextManager.FindText("str_game_key_text", keyId.ToLower());
		}
	}
}

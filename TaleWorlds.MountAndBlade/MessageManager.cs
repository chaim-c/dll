using System;
using System.Diagnostics;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001DF RID: 479
	public static class MessageManager
	{
		// Token: 0x06001AE2 RID: 6882 RVA: 0x0005D3FA File Offset: 0x0005B5FA
		public static void DisplayMessage(string message)
		{
			MBAPI.IMBMessageManager.DisplayMessage(message);
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x0005D407 File Offset: 0x0005B607
		public static void DisplayMessage(string message, uint color)
		{
			MBAPI.IMBMessageManager.DisplayMessageWithColor(message, color);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x0005D418 File Offset: 0x0005B618
		[Conditional("DEBUG")]
		public static void DisplayDebugMessage(string message)
		{
			if (message.Length > 4 && message.Substring(0, 4).Equals("[DEBUG]"))
			{
				message = message.Substring(4);
			}
			MBAPI.IMBMessageManager.DisplayMessageWithColor("[DEBUG]: " + message, 4294936712U);
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x0005D468 File Offset: 0x0005B668
		public static void DisplayMultilineMessage(string message, uint color)
		{
			if (message.Contains("\n"))
			{
				string[] array = message.Split(new char[]
				{
					'\n'
				});
				for (int i = 0; i < array.Length; i++)
				{
					MBAPI.IMBMessageManager.DisplayMessageWithColor(array[i], color);
				}
				return;
			}
			MBAPI.IMBMessageManager.DisplayMessageWithColor(message, color);
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0005D4BD File Offset: 0x0005B6BD
		public static void EraseMessageLines()
		{
			MBAPI.IMBWindowManager.EraseMessageLines();
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x0005D4C9 File Offset: 0x0005B6C9
		public static void SetMessageManager(MessageManagerBase messageManager)
		{
			MBAPI.IMBMessageManager.SetMessageManager(messageManager);
		}
	}
}

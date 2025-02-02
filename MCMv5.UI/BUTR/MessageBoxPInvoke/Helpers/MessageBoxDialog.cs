using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BUTR.MessageBoxPInvoke.Helpers
{
	// Token: 0x02000064 RID: 100
	[NullableContext(1)]
	[Nullable(0)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal static class MessageBoxDialog
	{
		// Token: 0x060003C0 RID: 960
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("USER32.dll", EntryPoint = "MessageBoxW", ExactSpelling = true, SetLastError = true)]
		internal static extern MESSAGEBOX_RESULT MessageBox(IntPtr hWnd, IntPtr lpText, IntPtr lpCaption, MESSAGEBOX_STYLE uType);

		// Token: 0x060003C1 RID: 961 RVA: 0x0000FB08 File Offset: 0x0000DD08
		internal static MESSAGEBOX_RESULT MessageBox(IntPtr hWnd, string lpText, string lpCaption, MESSAGEBOX_STYLE uType)
		{
			IntPtr lpTextLocal = Marshal.StringToHGlobalUni(lpText);
			IntPtr lpCaptionLocal = Marshal.StringToHGlobalUni(lpCaption);
			return MessageBoxDialog.MessageBox(hWnd, lpTextLocal, lpCaptionLocal, uType);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000FB31 File Offset: 0x0000DD31
		public static MessageBoxResult Show(string text)
		{
			return (MessageBoxResult)MessageBoxDialog.MessageBox(IntPtr.Zero, text, "\0", MESSAGEBOX_STYLE.MB_OK);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000FB44 File Offset: 0x0000DD44
		public static MessageBoxResult Show(string text, string caption)
		{
			return (MessageBoxResult)MessageBoxDialog.MessageBox(IntPtr.Zero, text, caption, MESSAGEBOX_STYLE.MB_OK);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000FB53 File Offset: 0x0000DD53
		public static MessageBoxResult Show(string text, string caption, MessageBoxButtons buttons)
		{
			return (MessageBoxResult)MessageBoxDialog.MessageBox(IntPtr.Zero, text, caption, (MESSAGEBOX_STYLE)buttons);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000FB62 File Offset: 0x0000DD62
		public static MessageBoxResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			return (MessageBoxResult)MessageBoxDialog.MessageBox(IntPtr.Zero, text, caption, (MESSAGEBOX_STYLE)(buttons | (MessageBoxButtons)icon));
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000FB73 File Offset: 0x0000DD73
		public static MessageBoxResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton button)
		{
			return (MessageBoxResult)MessageBoxDialog.MessageBox(IntPtr.Zero, text, caption, (MESSAGEBOX_STYLE)(buttons | (MessageBoxButtons)icon | (MessageBoxButtons)button));
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000FB87 File Offset: 0x0000DD87
		public static MessageBoxResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton button, MessageBoxModal modal)
		{
			return (MessageBoxResult)MessageBoxDialog.MessageBox(IntPtr.Zero, text, caption, (MESSAGEBOX_STYLE)(buttons | (MessageBoxButtons)icon | (MessageBoxButtons)button | (MessageBoxButtons)modal));
		}
	}
}

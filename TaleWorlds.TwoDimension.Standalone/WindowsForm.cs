using System;
using System.Collections.Generic;
using System.Diagnostics;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension.Standalone.Native.Windows;

namespace TaleWorlds.TwoDimension.Standalone
{
	// Token: 0x0200000D RID: 13
	public class WindowsForm
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000046B4 File Offset: 0x000028B4
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x000046BC File Offset: 0x000028BC
		public int Width { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000046C5 File Offset: 0x000028C5
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x000046CD File Offset: 0x000028CD
		public int Height { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000046D6 File Offset: 0x000028D6
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x000046DE File Offset: 0x000028DE
		public string Text { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000046E7 File Offset: 0x000028E7
		// (set) Token: 0x060000CA RID: 202 RVA: 0x000046EF File Offset: 0x000028EF
		public IntPtr Handle { get; set; }

		// Token: 0x060000CB RID: 203 RVA: 0x000046F8 File Offset: 0x000028F8
		public WindowsForm(int x, int y, int width, int height, ResourceDepot resourceDepot, bool borderlessWindow = false, bool enableWindowBlur = false, string name = null) : this(x, y, width, height, resourceDepot, IntPtr.Zero, borderlessWindow, enableWindowBlur, name)
		{
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004720 File Offset: 0x00002920
		public WindowsForm(int x, int y, int width, int height, ResourceDepot resourceDepot, IntPtr parent, bool borderlessWindow = false, bool enableWindowBlur = false, string name = null)
		{
			this.Handle = IntPtr.Zero;
			WindowsForm.classNameCount++;
			this.Width = width;
			this.Height = height;
			this.Text = "Form";
			this.windowClassName = "Form" + WindowsForm.classNameCount;
			this.wc = default(WindowClass);
			this._windowProcedure = new WndProc(this.WndProc);
			this.wc.style = 0U;
			this.wc.lpfnWndProc = this._windowProcedure;
			this.wc.cbClsExtra = 0;
			this.wc.cbWndExtra = 0;
			this.wc.hCursor = User32.LoadCursorFromFile(resourceDepot.GetFilePath("mb_cursor.cur"));
			this.wc.hInstance = Kernel32.GetModuleHandle(null);
			this.wc.lpszMenuName = null;
			this.wc.lpszClassName = this.windowClassName;
			this.wc.hbrBackground = Gdi32.CreateSolidBrush(IntPtr.Zero);
			User32.RegisterClass(ref this.wc);
			if (string.IsNullOrEmpty(name))
			{
				name = "Gauntlet UI: " + Process.GetCurrentProcess().Id;
			}
			WindowStyle dwStyle;
			if (parent != IntPtr.Zero)
			{
				dwStyle = (WindowStyle.WS_CHILD | WindowStyle.WS_VISIBLE);
			}
			else if (!borderlessWindow)
			{
				dwStyle = WindowStyle.OverlappedWindow;
			}
			else
			{
				dwStyle = (WindowStyle)2416443392U;
			}
			this.Handle = User32.CreateWindowEx(0, this.windowClassName, name, dwStyle, x, y, width, height, parent, IntPtr.Zero, Kernel32.GetModuleHandle(null), IntPtr.Zero);
			if (enableWindowBlur)
			{
				DwmBlurBehind dwmBlurBehind = default(DwmBlurBehind);
				dwmBlurBehind.dwFlags = (BlurBehindConstraints.Enable | BlurBehindConstraints.BlurRegion);
				dwmBlurBehind.hRgnBlur = Gdi32.CreateRectRgn(0, 0, -1, -1);
				dwmBlurBehind.fEnable = true;
				Dwmapi.DwmEnableBlurBehindWindow(this.Handle, ref dwmBlurBehind);
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000048FC File Offset: 0x00002AFC
		public WindowsForm(int width, int height, ResourceDepot resourceDepot) : this(100, 100, width, height, resourceDepot, false, false, null)
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004919 File Offset: 0x00002B19
		public void SetParent(IntPtr parentHandle)
		{
			User32.SetParent(this.Handle, parentHandle);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004928 File Offset: 0x00002B28
		public void Show()
		{
			User32.ShowWindow(this.Handle, WindowShowStyle.Show);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004937 File Offset: 0x00002B37
		public void Hide()
		{
			User32.ShowWindow(this.Handle, WindowShowStyle.Hide);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004946 File Offset: 0x00002B46
		public void Destroy()
		{
			this.Hide();
			User32.DestroyWindow(this.Handle);
			User32.UnregisterClass(this.windowClassName, IntPtr.Zero);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000496B File Offset: 0x00002B6B
		public void AddMessageHandler(WindowsFormMessageHandler messageHandler)
		{
			this._messageHandlers.Add(messageHandler);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000497C File Offset: 0x00002B7C
		private IntPtr WndProc(IntPtr hWnd, uint message, IntPtr wParam, IntPtr lParam)
		{
			long wParam2 = wParam.ToInt64();
			long num = lParam.ToInt64();
			if (message == 5U)
			{
				int width = (int)num % 65536;
				int height = (int)(num / 65536L);
				this.Width = width;
				this.Height = height;
			}
			foreach (WindowsFormMessageHandler windowsFormMessageHandler in this._messageHandlers)
			{
				windowsFormMessageHandler((WindowMessage)message, wParam2, num);
			}
			return User32.DefWindowProc(hWnd, message, wParam, lParam);
		}

		// Token: 0x04000045 RID: 69
		private static int classNameCount;

		// Token: 0x04000046 RID: 70
		private WindowClass wc;

		// Token: 0x04000047 RID: 71
		private string windowClassName;

		// Token: 0x04000048 RID: 72
		private WndProc _windowProcedure;

		// Token: 0x0400004C RID: 76
		private List<WindowsFormMessageHandler> _messageHandlers = new List<WindowsFormMessageHandler>();
	}
}

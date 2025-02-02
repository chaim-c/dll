using System;
using System.Runtime.InteropServices;

namespace TaleWorlds.TwoDimension.Standalone.Native.Windows
{
	// Token: 0x0200001A RID: 26
	public static class Kernel32
	{
		// Token: 0x060000F8 RID: 248
		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr LoadLibrary(string lpFileName);

		// Token: 0x060000F9 RID: 249
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr GetModuleHandle(string lpModuleName);

		// Token: 0x060000FA RID: 250
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern int GetLastError();

		// Token: 0x060000FB RID: 251
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		public static extern IntPtr GetConsoleWindow();

		// Token: 0x060000FC RID: 252
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		public static extern int GetUserGeoID(Kernel32.GeoTypeId type);

		// Token: 0x0200003C RID: 60
		public enum GeoTypeId
		{
			// Token: 0x040002D2 RID: 722
			Nation = 16,
			// Token: 0x040002D3 RID: 723
			Region = 14
		}
	}
}

using System;
using System.Collections.Concurrent;

namespace TaleWorlds.Library.Http
{
	// Token: 0x020000A9 RID: 169
	public static class HttpDriverManager
	{
		// Token: 0x06000628 RID: 1576 RVA: 0x00014205 File Offset: 0x00012405
		public static void AddHttpDriver(string name, IHttpDriver driver)
		{
			if (HttpDriverManager._httpDrivers.Count == 0)
			{
				HttpDriverManager._defaultHttpDriver = name;
			}
			HttpDriverManager._httpDrivers[name] = driver;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00014225 File Offset: 0x00012425
		public static void SetDefault(string name)
		{
			if (HttpDriverManager.GetHttpDriver(name) != null)
			{
				HttpDriverManager._defaultHttpDriver = name;
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00014238 File Offset: 0x00012438
		public static IHttpDriver GetHttpDriver(string name)
		{
			IHttpDriver httpDriver;
			HttpDriverManager._httpDrivers.TryGetValue(name, out httpDriver);
			if (httpDriver == null)
			{
				Debug.Print("HTTP driver not found:" + (name ?? "not set"), 0, Debug.DebugColor.White, 17592186044416UL);
			}
			return httpDriver;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001427C File Offset: 0x0001247C
		public static IHttpDriver GetDefaultHttpDriver()
		{
			if (HttpDriverManager._defaultHttpDriver == null)
			{
				HttpDriverManager.AddHttpDriver("DotNet", new DotNetHttpDriver());
			}
			return HttpDriverManager.GetHttpDriver(HttpDriverManager._defaultHttpDriver);
		}

		// Token: 0x040001CA RID: 458
		private static ConcurrentDictionary<string, IHttpDriver> _httpDrivers = new ConcurrentDictionary<string, IHttpDriver>();

		// Token: 0x040001CB RID: 459
		private static string _defaultHttpDriver;
	}
}

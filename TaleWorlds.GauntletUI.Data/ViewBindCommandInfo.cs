using System;
using TaleWorlds.Library;

namespace TaleWorlds.GauntletUI.Data
{
	// Token: 0x0200000B RID: 11
	internal class ViewBindCommandInfo
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000042C0 File Offset: 0x000024C0
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000042C8 File Offset: 0x000024C8
		internal GauntletView Owner { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000042D1 File Offset: 0x000024D1
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000042D9 File Offset: 0x000024D9
		internal string Command { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000042E2 File Offset: 0x000024E2
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000042EA File Offset: 0x000024EA
		internal BindingPath Path { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000042F3 File Offset: 0x000024F3
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000042FB File Offset: 0x000024FB
		internal string Parameter { get; private set; }

		// Token: 0x06000091 RID: 145 RVA: 0x00004304 File Offset: 0x00002504
		internal ViewBindCommandInfo(GauntletView view, string command, BindingPath path, string parameter)
		{
			this.Owner = view;
			this.Command = command;
			this.Path = path;
			this.Parameter = parameter;
		}
	}
}

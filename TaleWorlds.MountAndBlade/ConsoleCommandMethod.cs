using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000313 RID: 787
	[AttributeUsage(AttributeTargets.Method)]
	public class ConsoleCommandMethod : Attribute
	{
		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x000A552C File Offset: 0x000A372C
		// (set) Token: 0x06002AB1 RID: 10929 RVA: 0x000A5534 File Offset: 0x000A3734
		public string CommandName { get; private set; }

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x000A553D File Offset: 0x000A373D
		// (set) Token: 0x06002AB3 RID: 10931 RVA: 0x000A5545 File Offset: 0x000A3745
		public string Description { get; private set; }

		// Token: 0x06002AB4 RID: 10932 RVA: 0x000A554E File Offset: 0x000A374E
		public ConsoleCommandMethod(string commandName, string description)
		{
			this.CommandName = commandName;
			this.Description = description;
		}
	}
}

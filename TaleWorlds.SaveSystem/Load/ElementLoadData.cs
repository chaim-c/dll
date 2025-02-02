using System;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Load
{
	// Token: 0x02000035 RID: 53
	internal class ElementLoadData : VariableLoadData
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000909B File Offset: 0x0000729B
		// (set) Token: 0x060001EC RID: 492 RVA: 0x000090A3 File Offset: 0x000072A3
		public ContainerLoadData ContainerLoadData { get; private set; }

		// Token: 0x060001ED RID: 493 RVA: 0x000090AC File Offset: 0x000072AC
		internal ElementLoadData(ContainerLoadData containerLoadData, IReader reader) : base(containerLoadData.Context, reader)
		{
			this.ContainerLoadData = containerLoadData;
		}
	}
}

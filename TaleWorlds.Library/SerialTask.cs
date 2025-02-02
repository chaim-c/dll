using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000086 RID: 134
	public class SerialTask : ITask
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x0000FC00 File Offset: 0x0000DE00
		public SerialTask(SerialTask.DelegateDefinition function)
		{
			this._instance = function;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000FC0F File Offset: 0x0000DE0F
		void ITask.Invoke()
		{
			this._instance();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000FC1C File Offset: 0x0000DE1C
		void ITask.Wait()
		{
		}

		// Token: 0x04000162 RID: 354
		private SerialTask.DelegateDefinition _instance;

		// Token: 0x020000D4 RID: 212
		// (Invoke) Token: 0x06000712 RID: 1810
		public delegate void DelegateDefinition();
	}
}

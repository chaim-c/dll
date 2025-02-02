using System;

namespace SandBox.View.Map
{
	// Token: 0x02000042 RID: 66
	public class MapEncyclopediaView : MapView
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00015B6D File Offset: 0x00013D6D
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00015B75 File Offset: 0x00013D75
		public bool IsEncyclopediaOpen { get; protected set; }

		// Token: 0x0600024C RID: 588 RVA: 0x00015B7E File Offset: 0x00013D7E
		public virtual void CloseEncyclopedia()
		{
		}
	}
}

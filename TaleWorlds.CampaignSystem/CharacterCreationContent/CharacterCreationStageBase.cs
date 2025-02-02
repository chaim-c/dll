using System;

namespace TaleWorlds.CampaignSystem.CharacterCreationContent
{
	// Token: 0x020001DD RID: 477
	public abstract class CharacterCreationStageBase
	{
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001C69 RID: 7273 RVA: 0x0007F9F4 File Offset: 0x0007DBF4
		public CharacterCreationState CharacterCreationState { get; }

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x0007F9FC File Offset: 0x0007DBFC
		// (set) Token: 0x06001C6B RID: 7275 RVA: 0x0007FA04 File Offset: 0x0007DC04
		public ICharacterCreationStageListener Listener { get; set; }

		// Token: 0x06001C6C RID: 7276 RVA: 0x0007FA0D File Offset: 0x0007DC0D
		protected CharacterCreationStageBase(CharacterCreationState state)
		{
			this.CharacterCreationState = state;
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x0007FA1C File Offset: 0x0007DC1C
		protected internal virtual void OnFinalize()
		{
			ICharacterCreationStageListener listener = this.Listener;
			if (listener == null)
			{
				return;
			}
			listener.OnStageFinalize();
		}
	}
}

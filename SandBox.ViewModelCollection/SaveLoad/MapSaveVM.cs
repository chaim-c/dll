using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SandBox.ViewModelCollection.SaveLoad
{
	// Token: 0x0200000E RID: 14
	public class MapSaveVM : ViewModel
	{
		// Token: 0x0600012D RID: 301 RVA: 0x000077D0 File Offset: 0x000059D0
		public MapSaveVM(Action<bool> onActiveStateChange)
		{
			this._onActiveStateChange = onActiveStateChange;
			CampaignEvents.OnSaveStartedEvent.AddNonSerializedListener(this, new Action(this.OnSaveStarted));
			CampaignEvents.OnSaveOverEvent.AddNonSerializedListener(this, new Action<bool, string>(this.OnSaveOver));
			this.RefreshValues();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00007820 File Offset: 0x00005A20
		public override void RefreshValues()
		{
			base.RefreshValues();
			TextObject textObject = TextObject.Empty;
			textObject = new TextObject("{=cp2XDjeq}Saving...", null);
			this.SavingText = textObject.ToString();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007851 File Offset: 0x00005A51
		private void OnSaveOver(bool isSuccessful, string saveName)
		{
			this.IsActive = false;
			Action<bool> onActiveStateChange = this._onActiveStateChange;
			if (onActiveStateChange == null)
			{
				return;
			}
			onActiveStateChange(false);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000786B File Offset: 0x00005A6B
		private void OnSaveStarted()
		{
			this.IsActive = true;
			Action<bool> onActiveStateChange = this._onActiveStateChange;
			if (onActiveStateChange == null)
			{
				return;
			}
			onActiveStateChange(true);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00007885 File Offset: 0x00005A85
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEvents.OnSaveStartedEvent.ClearListeners(this);
			CampaignEvents.OnSaveOverEvent.ClearListeners(this);
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000078A3 File Offset: 0x00005AA3
		// (set) Token: 0x06000133 RID: 307 RVA: 0x000078AB File Offset: 0x00005AAB
		[DataSourceProperty]
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (value != this._isActive)
				{
					this._isActive = value;
					base.OnPropertyChangedWithValue(value, "IsActive");
				}
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000078C9 File Offset: 0x00005AC9
		// (set) Token: 0x06000135 RID: 309 RVA: 0x000078D1 File Offset: 0x00005AD1
		[DataSourceProperty]
		public string SavingText
		{
			get
			{
				return this._savingText;
			}
			set
			{
				if (value != this._savingText)
				{
					this._savingText = value;
					base.OnPropertyChangedWithValue<string>(value, "SavingText");
				}
			}
		}

		// Token: 0x0400007B RID: 123
		private readonly Action<bool> _onActiveStateChange;

		// Token: 0x0400007C RID: 124
		private string _savingText;

		// Token: 0x0400007D RID: 125
		private bool _isActive;
	}
}

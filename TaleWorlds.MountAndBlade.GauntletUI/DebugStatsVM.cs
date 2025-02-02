using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x02000007 RID: 7
	internal class DebugStatsVM : ViewModel
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00003CDC File Offset: 0x00001EDC
		public DebugStatsVM()
		{
			this.GameVersion = ApplicationVersion.FromParametersFile(null).ToString();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003D09 File Offset: 0x00001F09
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00003D11 File Offset: 0x00001F11
		[DataSourceProperty]
		public string GameVersion
		{
			get
			{
				return this._gameVersion;
			}
			set
			{
				if (value != this._gameVersion)
				{
					this._gameVersion = value;
					base.OnPropertyChangedWithValue<string>(value, "GameVersion");
				}
			}
		}

		// Token: 0x04000031 RID: 49
		private string _gameVersion;
	}
}

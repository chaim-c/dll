using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD
{
	// Token: 0x0200004B RID: 75
	public class ReloadPhaseItemVM : ViewModel
	{
		// Token: 0x06000641 RID: 1601 RVA: 0x000199F3 File Offset: 0x00017BF3
		public ReloadPhaseItemVM(float progress, float relativeDurationToMaxDuration)
		{
			this.Update(progress, relativeDurationToMaxDuration);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00019A03 File Offset: 0x00017C03
		public void Update(float progress, float relativeDurationToMaxDuration)
		{
			this.Progress = progress;
			this.RelativeDurationToMaxDuration = relativeDurationToMaxDuration;
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x00019A13 File Offset: 0x00017C13
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x00019A1B File Offset: 0x00017C1B
		[DataSourceProperty]
		public float Progress
		{
			get
			{
				return this._progress;
			}
			set
			{
				if (value != this._progress)
				{
					this._progress = value;
					base.OnPropertyChangedWithValue(value, "Progress");
				}
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x00019A39 File Offset: 0x00017C39
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x00019A41 File Offset: 0x00017C41
		[DataSourceProperty]
		public float RelativeDurationToMaxDuration
		{
			get
			{
				return this._relativeDurationToMaxDuration;
			}
			set
			{
				if (value != this._relativeDurationToMaxDuration)
				{
					this._relativeDurationToMaxDuration = value;
					base.OnPropertyChangedWithValue(value, "RelativeDurationToMaxDuration");
				}
			}
		}

		// Token: 0x040002FA RID: 762
		private float _progress;

		// Token: 0x040002FB RID: 763
		private float _relativeDurationToMaxDuration;
	}
}

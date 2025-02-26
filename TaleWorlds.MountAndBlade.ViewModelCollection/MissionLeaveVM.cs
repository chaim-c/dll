﻿using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection
{
	// Token: 0x02000008 RID: 8
	public class MissionLeaveVM : ViewModel
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00004007 File Offset: 0x00002207
		public MissionLeaveVM(Func<float> getMissionEndTimer, Func<float> getMissionEndTimeInSeconds)
		{
			this._getMissionEndTimer = getMissionEndTimer;
			this._getMissionEndTimeInSeconds = getMissionEndTimeInSeconds;
			this.RefreshValues();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004023 File Offset: 0x00002223
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.LeaveText = GameTexts.FindText("str_leaving", null).ToString();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004041 File Offset: 0x00002241
		public void Tick(float dt)
		{
			this.CurrentTime = this._getMissionEndTimer();
			this.MaxTime = this._getMissionEndTimeInSeconds();
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00004065 File Offset: 0x00002265
		// (set) Token: 0x06000099 RID: 153 RVA: 0x0000406D File Offset: 0x0000226D
		[DataSourceProperty]
		public string LeaveText
		{
			get
			{
				return this._leaveText;
			}
			set
			{
				if (value != this._leaveText)
				{
					this._leaveText = value;
					base.OnPropertyChangedWithValue<string>(value, "LeaveText");
				}
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004090 File Offset: 0x00002290
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00004098 File Offset: 0x00002298
		[DataSourceProperty]
		public float MaxTime
		{
			get
			{
				return this._maxTime;
			}
			set
			{
				if (value != this._maxTime)
				{
					this._maxTime = value;
					base.OnPropertyChangedWithValue(value, "MaxTime");
				}
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000040B6 File Offset: 0x000022B6
		// (set) Token: 0x0600009D RID: 157 RVA: 0x000040BE File Offset: 0x000022BE
		[DataSourceProperty]
		public float CurrentTime
		{
			get
			{
				return this._currentTime;
			}
			set
			{
				if (value != this._currentTime)
				{
					this._currentTime = value;
					base.OnPropertyChangedWithValue(value, "CurrentTime");
				}
			}
		}

		// Token: 0x0400003E RID: 62
		private Func<float> _getMissionEndTimer;

		// Token: 0x0400003F RID: 63
		private Func<float> _getMissionEndTimeInSeconds;

		// Token: 0x04000040 RID: 64
		private float _maxTime;

		// Token: 0x04000041 RID: 65
		private float _currentTime;

		// Token: 0x04000042 RID: 66
		private string _leaveText;
	}
}

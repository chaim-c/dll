using System;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD
{
	// Token: 0x0200004A RID: 74
	public class MissionSpectatorControlVM : ViewModel
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x000197B9 File Offset: 0x000179B9
		public MissionSpectatorControlVM(Mission mission)
		{
			this._mission = mission;
			this.RefreshValues();
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x000197DF File Offset: 0x000179DF
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.PrevCharacterText = new TextObject("{=BANC61K5}Previous Character", null).ToString();
			this.NextCharacterText = new TextObject("{=znKxunbQ}Next Character", null).ToString();
			this.UpdateStatusText();
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00019819 File Offset: 0x00017A19
		public void OnSpectatedAgentFocusIn(Agent followedAgent)
		{
			MissionPeer missionPeer = followedAgent.MissionPeer;
			this.SpectatedAgentName = (((missionPeer != null) ? missionPeer.DisplayedName : null) ?? followedAgent.Name);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001983D File Offset: 0x00017A3D
		public void OnSpectatedAgentFocusOut(Agent followedAgent)
		{
			this.SpectatedAgentName = TextObject.Empty.ToString();
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001984F File Offset: 0x00017A4F
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM prevCharacterKey = this.PrevCharacterKey;
			if (prevCharacterKey != null)
			{
				prevCharacterKey.OnFinalize();
			}
			InputKeyItemVM nextCharacterKey = this.NextCharacterKey;
			if (nextCharacterKey == null)
			{
				return;
			}
			nextCharacterKey.OnFinalize();
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00019878 File Offset: 0x00017A78
		public void SetMainAgentStatus(bool isDead)
		{
			if (this._isMainHeroDead != isDead)
			{
				this._isMainHeroDead = isDead;
				this.UpdateStatusText();
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00019890 File Offset: 0x00017A90
		private void UpdateStatusText()
		{
			if (this._isMainHeroDead)
			{
				this.StatusText = this._deadTextObject.ToString();
				return;
			}
			this.StatusText = string.Empty;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x000198B7 File Offset: 0x00017AB7
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x000198BF File Offset: 0x00017ABF
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x000198DD File Offset: 0x00017ADD
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x000198E5 File Offset: 0x00017AE5
		[DataSourceProperty]
		public string PrevCharacterText
		{
			get
			{
				return this._prevCharacterText;
			}
			set
			{
				if (value != this._prevCharacterText)
				{
					this._prevCharacterText = value;
					base.OnPropertyChangedWithValue<string>(value, "PrevCharacterText");
				}
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00019908 File Offset: 0x00017B08
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x00019910 File Offset: 0x00017B10
		[DataSourceProperty]
		public string NextCharacterText
		{
			get
			{
				return this._nextCharacterText;
			}
			set
			{
				if (value != this._nextCharacterText)
				{
					this._nextCharacterText = value;
					base.OnPropertyChangedWithValue<string>(value, "NextCharacterText");
				}
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00019933 File Offset: 0x00017B33
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x0001993B File Offset: 0x00017B3B
		[DataSourceProperty]
		public string StatusText
		{
			get
			{
				return this._statusText;
			}
			set
			{
				if (value != this._statusText)
				{
					this._statusText = value;
					base.OnPropertyChangedWithValue<string>(value, "StatusText");
				}
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001995E File Offset: 0x00017B5E
		public void SetPrevCharacterInputKey(GameKey gameKey)
		{
			this.PrevCharacterKey = InputKeyItemVM.CreateFromGameKey(gameKey, false);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001996D File Offset: 0x00017B6D
		public void SetNextCharacterInputKey(GameKey gameKey)
		{
			this.NextCharacterKey = InputKeyItemVM.CreateFromGameKey(gameKey, false);
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0001997C File Offset: 0x00017B7C
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x00019984 File Offset: 0x00017B84
		[DataSourceProperty]
		public string SpectatedAgentName
		{
			get
			{
				return this._spectatedAgentName;
			}
			set
			{
				if (value != this._spectatedAgentName)
				{
					this._spectatedAgentName = value;
					base.OnPropertyChangedWithValue<string>(value, "SpectatedAgentName");
				}
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x000199A7 File Offset: 0x00017BA7
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x000199AF File Offset: 0x00017BAF
		[DataSourceProperty]
		public InputKeyItemVM PrevCharacterKey
		{
			get
			{
				return this._prevCharacterKey;
			}
			set
			{
				if (value != this._prevCharacterKey)
				{
					this._prevCharacterKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "PrevCharacterKey");
				}
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x000199CD File Offset: 0x00017BCD
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x000199D5 File Offset: 0x00017BD5
		[DataSourceProperty]
		public InputKeyItemVM NextCharacterKey
		{
			get
			{
				return this._nextCharacterKey;
			}
			set
			{
				if (value != this._nextCharacterKey)
				{
					this._nextCharacterKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "NextCharacterKey");
				}
			}
		}

		// Token: 0x040002F0 RID: 752
		private readonly Mission _mission;

		// Token: 0x040002F1 RID: 753
		private bool _isMainHeroDead;

		// Token: 0x040002F2 RID: 754
		private readonly TextObject _deadTextObject = GameTexts.FindText("str_battle_hero_dead", null);

		// Token: 0x040002F3 RID: 755
		private bool _isEnabled;

		// Token: 0x040002F4 RID: 756
		private string _prevCharacterText;

		// Token: 0x040002F5 RID: 757
		private string _nextCharacterText;

		// Token: 0x040002F6 RID: 758
		private string _statusText;

		// Token: 0x040002F7 RID: 759
		private string _spectatedAgentName;

		// Token: 0x040002F8 RID: 760
		private InputKeyItemVM _prevCharacterKey;

		// Token: 0x040002F9 RID: 761
		private InputKeyItemVM _nextCharacterKey;
	}
}

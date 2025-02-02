using System;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection.Information
{
	// Token: 0x02000014 RID: 20
	public class GameNotificationItemVM : ViewModel
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00003D14 File Offset: 0x00001F14
		public GameNotificationItemVM(string notificationText, int extraTimeInMs, BasicCharacterObject announcerCharacter, string soundId)
		{
			this.GameNotificationText = notificationText;
			this.NotificationSoundId = soundId;
			this.Announcer = ((announcerCharacter != null) ? new ImageIdentifierVM(CharacterCode.CreateFrom(announcerCharacter)) : new ImageIdentifierVM(ImageIdentifierType.Null));
			this.CharacterNameText = ((announcerCharacter != null) ? announcerCharacter.Name.ToString() : "");
			this.ExtraTimeInMs = extraTimeInMs;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00003D74 File Offset: 0x00001F74
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00003D7C File Offset: 0x00001F7C
		[DataSourceProperty]
		public ImageIdentifierVM Announcer
		{
			get
			{
				return this._announcer;
			}
			set
			{
				if (value != this._announcer)
				{
					this._announcer = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Announcer");
				}
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00003D9A File Offset: 0x00001F9A
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00003DA2 File Offset: 0x00001FA2
		[DataSourceProperty]
		public int ExtraTimeInMs
		{
			get
			{
				return this._extraTimeInMs;
			}
			set
			{
				if (value != this._extraTimeInMs)
				{
					this._extraTimeInMs = value;
					base.OnPropertyChangedWithValue(value, "ExtraTimeInMs");
				}
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00003DC0 File Offset: 0x00001FC0
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00003DC8 File Offset: 0x00001FC8
		[DataSourceProperty]
		public string GameNotificationText
		{
			get
			{
				return this._gameNotificationText;
			}
			set
			{
				if (value != this._gameNotificationText)
				{
					this._gameNotificationText = value;
					base.OnPropertyChangedWithValue<string>(value, "GameNotificationText");
				}
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00003DEB File Offset: 0x00001FEB
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00003DF3 File Offset: 0x00001FF3
		[DataSourceProperty]
		public string CharacterNameText
		{
			get
			{
				return this._characterNameText;
			}
			set
			{
				if (value != this._characterNameText)
				{
					this._characterNameText = value;
					base.OnPropertyChangedWithValue<string>(value, "CharacterNameText");
				}
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00003E16 File Offset: 0x00002016
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00003E1E File Offset: 0x0000201E
		[DataSourceProperty]
		public string NotificationSoundId
		{
			get
			{
				return this._notificationSoundId;
			}
			set
			{
				if (value != this._notificationSoundId)
				{
					this._notificationSoundId = value;
					base.OnPropertyChangedWithValue<string>(value, "NotificationSoundId");
				}
			}
		}

		// Token: 0x04000060 RID: 96
		private string _gameNotificationText;

		// Token: 0x04000061 RID: 97
		private string _characterNameText;

		// Token: 0x04000062 RID: 98
		private string _notificationSoundId;

		// Token: 0x04000063 RID: 99
		private ImageIdentifierVM _announcer;

		// Token: 0x04000064 RID: 100
		private int _extraTimeInMs;
	}
}

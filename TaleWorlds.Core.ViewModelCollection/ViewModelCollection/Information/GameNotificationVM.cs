using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection.Information
{
	// Token: 0x02000015 RID: 21
	public class GameNotificationVM : ViewModel
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00003E44 File Offset: 0x00002044
		private float CurrentNotificationOnScreenTime
		{
			get
			{
				float num = 1f;
				num += (float)this.CurrentNotification.ExtraTimeInMs / 1000f;
				int numberOfWords = this.GetNumberOfWords(this.CurrentNotification.GameNotificationText);
				if (numberOfWords > 4)
				{
					num += (float)(numberOfWords - 4) / 5f;
				}
				return num + 1f / (float)(this._items.Count + 1);
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003EA8 File Offset: 0x000020A8
		public GameNotificationVM()
		{
			MBInformationManager.FiringQuickInformation += this.AddGameNotification;
			this._items = new List<GameNotificationItemVM>();
			this.CurrentNotification = new GameNotificationItemVM("NULL", 0, null, "NULL");
			this.GotNotification = false;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003EF5 File Offset: 0x000020F5
		public void ClearNotifications()
		{
			this._items.Clear();
			this.GotNotification = false;
			this._timer = this.CurrentNotificationOnScreenTime * 2f;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003F1C File Offset: 0x0000211C
		public void Tick(float dt)
		{
			this._timer += dt;
			if (this.GotNotification && this._timer >= this.CurrentNotificationOnScreenTime)
			{
				this._timer = 0f;
				if (this._items.Count > 0)
				{
					this.CurrentNotification = this._items[0];
					this._items.RemoveAt(0);
					return;
				}
				this.GotNotification = false;
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003F8C File Offset: 0x0000218C
		public void AddGameNotification(string notificationText, int extraTimeInMs, BasicCharacterObject announcerCharacter, string soundId)
		{
			GameNotificationItemVM gameNotificationItemVM = new GameNotificationItemVM(notificationText, extraTimeInMs, announcerCharacter, soundId);
			if (!this._items.Any((GameNotificationItemVM i) => i.GameNotificationText == notificationText) && (!this.GotNotification || this.CurrentNotification.GameNotificationText != notificationText))
			{
				if (this.GotNotification)
				{
					this._items.Add(gameNotificationItemVM);
					return;
				}
				this.CurrentNotification = gameNotificationItemVM;
				this.TotalTime = this.CurrentNotificationOnScreenTime;
				this.GotNotification = true;
				this._timer = 0f;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004029 File Offset: 0x00002229
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00004034 File Offset: 0x00002234
		public GameNotificationItemVM CurrentNotification
		{
			get
			{
				return this._currentNotification;
			}
			set
			{
				if (this._currentNotification != value)
				{
					this._currentNotification = value;
					int notificationId = this.NotificationId;
					this.NotificationId = notificationId + 1;
					base.OnPropertyChangedWithValue<GameNotificationItemVM>(value, "CurrentNotification");
					if (value != null)
					{
						Action<GameNotificationItemVM> receiveNewNotification = this.ReceiveNewNotification;
						if (receiveNewNotification == null)
						{
							return;
						}
						receiveNewNotification(this.CurrentNotification);
					}
				}
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00004086 File Offset: 0x00002286
		// (set) Token: 0x0600010A RID: 266 RVA: 0x0000408E File Offset: 0x0000228E
		[DataSourceProperty]
		public bool GotNotification
		{
			get
			{
				return this._gotNotification;
			}
			set
			{
				if (value != this._gotNotification)
				{
					this._gotNotification = value;
					base.OnPropertyChangedWithValue(value, "GotNotification");
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000040AC File Offset: 0x000022AC
		// (set) Token: 0x0600010C RID: 268 RVA: 0x000040B4 File Offset: 0x000022B4
		[DataSourceProperty]
		public int NotificationId
		{
			get
			{
				return this._notificationId;
			}
			set
			{
				if (value != this._notificationId)
				{
					this._notificationId = value;
					base.OnPropertyChangedWithValue(value, "NotificationId");
				}
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000040D2 File Offset: 0x000022D2
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000040DA File Offset: 0x000022DA
		[DataSourceProperty]
		public float TotalTime
		{
			get
			{
				return this._totalTime;
			}
			set
			{
				if (value != this._totalTime)
				{
					this._totalTime = value;
					base.OnPropertyChangedWithValue(value, "TotalTime");
				}
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600010F RID: 271 RVA: 0x000040F8 File Offset: 0x000022F8
		// (remove) Token: 0x06000110 RID: 272 RVA: 0x00004130 File Offset: 0x00002330
		public event Action<GameNotificationItemVM> ReceiveNewNotification;

		// Token: 0x06000111 RID: 273 RVA: 0x00004168 File Offset: 0x00002368
		private int GetNumberOfWords(string text)
		{
			string text2 = text.Trim();
			int num = 0;
			int i = 0;
			while (i < text2.Length)
			{
				while (i < text2.Length && !char.IsWhiteSpace(text2[i]))
				{
					i++;
				}
				num++;
				while (i < text2.Length && char.IsWhiteSpace(text2[i]))
				{
					i++;
				}
			}
			return num;
		}

		// Token: 0x04000065 RID: 101
		private readonly List<GameNotificationItemVM> _items;

		// Token: 0x04000066 RID: 102
		private bool _gotNotification;

		// Token: 0x04000067 RID: 103
		private const float MinimumDisplayTimeInSeconds = 1f;

		// Token: 0x04000068 RID: 104
		private const float ExtraDisplayTimeInSeconds = 1f;

		// Token: 0x04000069 RID: 105
		private float _timer;

		// Token: 0x0400006A RID: 106
		private int _notificationId;

		// Token: 0x0400006B RID: 107
		private GameNotificationItemVM _currentNotification;

		// Token: 0x0400006C RID: 108
		private float _totalTime;
	}
}

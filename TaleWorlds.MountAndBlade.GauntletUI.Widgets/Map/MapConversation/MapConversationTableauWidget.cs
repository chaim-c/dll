using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map.MapConversation
{
	// Token: 0x02000117 RID: 279
	public class MapConversationTableauWidget : TextureWidget
	{
		// Token: 0x06000E9B RID: 3739 RVA: 0x000289B9 File Offset: 0x00026BB9
		public MapConversationTableauWidget(UIContext context) : base(context)
		{
			base.TextureProviderName = "MapConversationTextureProvider";
			this._isRenderRequestedPreviousFrame = false;
			base.UpdateTextureWidget();
			base.EventManager.AddAfterFinalizedCallback(new Action(this.OnEventManagerIsFinalized));
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x000289F1 File Offset: 0x00026BF1
		private void OnEventManagerIsFinalized()
		{
			if (!this._setForClearNextFrame)
			{
				TextureProvider textureProvider = base.TextureProvider;
				if (textureProvider == null)
				{
					return;
				}
				textureProvider.Clear(false);
			}
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00028A0C File Offset: 0x00026C0C
		protected override void OnDisconnectedFromRoot()
		{
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x00028A0E File Offset: 0x00026C0E
		// (set) Token: 0x06000E9F RID: 3743 RVA: 0x00028A16 File Offset: 0x00026C16
		[Editor(false)]
		public object Data
		{
			get
			{
				return this._data;
			}
			set
			{
				if (value != this._data)
				{
					this._data = value;
					base.OnPropertyChanged<object>(value, "Data");
					base.SetTextureProviderProperty("Data", value);
				}
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x00028A40 File Offset: 0x00026C40
		// (set) Token: 0x06000EA1 RID: 3745 RVA: 0x00028A48 File Offset: 0x00026C48
		[Editor(false)]
		public bool IsTableauEnabled
		{
			get
			{
				return this._isTableauEnabled;
			}
			set
			{
				if (value != this._isTableauEnabled)
				{
					this._isTableauEnabled = value;
					base.OnPropertyChanged(value, "IsTableauEnabled");
					base.SetTextureProviderProperty("IsEnabled", value);
					if (this._isTableauEnabled)
					{
						this._isRenderRequestedPreviousFrame = true;
					}
				}
			}
		}

		// Token: 0x040006B7 RID: 1719
		private object _data;

		// Token: 0x040006B8 RID: 1720
		private bool _isTableauEnabled;
	}
}

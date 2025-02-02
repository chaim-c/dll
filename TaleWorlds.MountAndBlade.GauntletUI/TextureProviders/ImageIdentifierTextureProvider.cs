using System;
using System.Linq;
using TaleWorlds.Avatar.PlayerServices;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.Tableaus;
using TaleWorlds.ObjectSystem;
using TaleWorlds.PlayerServices;
using TaleWorlds.PlayerServices.Avatar;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.TextureProviders
{
	// Token: 0x02000019 RID: 25
	public class ImageIdentifierTextureProvider : TextureProvider
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00006E4C File Offset: 0x0000504C
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00006E54 File Offset: 0x00005054
		public bool IsBig
		{
			get
			{
				return this._isBig;
			}
			set
			{
				if (this._isBig != value)
				{
					this._isBig = value;
					this._textureRequiresRefreshing = true;
				}
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00006E6D File Offset: 0x0000506D
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00006E75 File Offset: 0x00005075
		public bool IsReleased
		{
			get
			{
				return this._isReleased;
			}
			set
			{
				if (this._isReleased != value)
				{
					this._isReleased = value;
					if (this._isReleased)
					{
						this.ReleaseCache();
					}
				}
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00006E95 File Offset: 0x00005095
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00006E9D File Offset: 0x0000509D
		public string ImageId
		{
			get
			{
				return this._imageId;
			}
			set
			{
				if (this._imageId != value)
				{
					this._imageId = value;
					this._textureRequiresRefreshing = true;
				}
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00006EBB File Offset: 0x000050BB
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00006EC3 File Offset: 0x000050C3
		public string AdditionalArgs
		{
			get
			{
				return this._additionalArgs;
			}
			set
			{
				if (this._additionalArgs != value)
				{
					this._additionalArgs = value;
					this._textureRequiresRefreshing = true;
				}
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00006EE1 File Offset: 0x000050E1
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00006EE9 File Offset: 0x000050E9
		public int ImageTypeCode
		{
			get
			{
				return this._imageTypeCode;
			}
			set
			{
				if (this._imageTypeCode != value)
				{
					this._imageTypeCode = value;
					this._textureRequiresRefreshing = true;
				}
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006F02 File Offset: 0x00005102
		public ImageIdentifierTextureProvider()
		{
			this._textureRequiresRefreshing = true;
			this._receivedAvatarData = null;
			this._timeSinceAvatarFail = 6f;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006F24 File Offset: 0x00005124
		private void CheckTexture()
		{
			if (this._textureRequiresRefreshing && (!this._creatingTexture || this.ImageTypeCode == 4))
			{
				if (this._receivedAvatarData != null)
				{
					if (this._receivedAvatarData.Status == AvatarData.DataStatus.Ready)
					{
						AvatarData receivedAvatarData = this._receivedAvatarData;
						this._receivedAvatarData = null;
						this.OnAvatarLoaded(this.ImageId + "." + this.AdditionalArgs, receivedAvatarData);
					}
					else if (this._receivedAvatarData.Status == AvatarData.DataStatus.Failed)
					{
						this._receivedAvatarData = null;
						this.OnTextureCreated(null);
						this._textureRequiresRefreshing = true;
						this._timeSinceAvatarFail = 0f;
					}
				}
				else if (this.ImageTypeCode != 0)
				{
					if (this._timeSinceAvatarFail > 5f)
					{
						this.CreateImageWithId(this.ImageId, this.ImageTypeCode, this.AdditionalArgs);
					}
				}
				else
				{
					this.OnTextureCreated(null);
				}
			}
			if (this._handleNewlyCreatedTexture)
			{
				TaleWorlds.Engine.Texture b = null;
				TaleWorlds.TwoDimension.Texture providedTexture = this._providedTexture;
				EngineTexture engineTexture;
				if ((engineTexture = (((providedTexture != null) ? providedTexture.PlatformTexture : null) as EngineTexture)) != null)
				{
					b = engineTexture.Texture;
				}
				if (this._texture != b)
				{
					if (this._texture != null)
					{
						EngineTexture platformTexture = new EngineTexture(this._texture);
						this._providedTexture = new TaleWorlds.TwoDimension.Texture(platformTexture);
					}
					else
					{
						this._providedTexture = null;
					}
				}
				this._handleNewlyCreatedTexture = false;
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00007069 File Offset: 0x00005269
		public override TaleWorlds.TwoDimension.Texture GetTexture(TwoDimensionContext twoDimensionContext, string name)
		{
			this.CheckTexture();
			return this._providedTexture;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00007077 File Offset: 0x00005277
		public override void Tick(float dt)
		{
			base.Tick(dt);
			this._timeSinceAvatarFail += dt;
			this.CheckTexture();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00007094 File Offset: 0x00005294
		public void CreateImageWithId(string id, int typeAsInt, string additionalArgs)
		{
			if (string.IsNullOrEmpty(id))
			{
				if (typeAsInt == 5)
				{
					CharacterCode characterCode = this._characterCode;
					if (characterCode == null || characterCode.IsEmpty)
					{
						this.OnTextureCreated(TableauCacheManager.Current.GetCachedHeroSilhouetteTexture());
						return;
					}
				}
				this.OnTextureCreated(null);
				return;
			}
			switch (typeAsInt)
			{
			case 0:
				this.OnTextureCreated(null);
				return;
			case 1:
				this._itemObject = MBObjectManager.Instance.GetObject<ItemObject>(id);
				Debug.Print("Render Requested: " + id, 0, Debug.DebugColor.White, 17592186044416UL);
				if (this._itemObject == null)
				{
					Debug.FailedAssert("WRONG Item IMAGE IDENTIFIER ID", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\TextureProviders\\ImageIdentifierTextureProvider.cs", "CreateImageWithId", 214);
					this.OnTextureCreated(null);
					return;
				}
				this._creatingTexture = true;
				TableauCacheManager.Current.BeginCreateItemTexture(this._itemObject, additionalArgs, new Action<TaleWorlds.Engine.Texture>(this.OnTextureCreated));
				return;
			case 2:
				this._craftingPiece = MBObjectManager.Instance.GetObject<CraftingPiece>(id.Split(new char[]
				{
					'$'
				})[0]);
				if (this._craftingPiece == null)
				{
					Debug.FailedAssert("WRONG CraftingPiece IMAGE IDENTIFIER ID", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\TextureProviders\\ImageIdentifierTextureProvider.cs", "CreateImageWithId", 225);
					this.OnTextureCreated(null);
					return;
				}
				this._creatingTexture = true;
				TableauCacheManager.Current.BeginCreateCraftingPieceTexture(this._craftingPiece, id.Split(new char[]
				{
					'$'
				})[1], new Action<TaleWorlds.Engine.Texture>(this.OnTextureCreated));
				return;
			case 3:
				this._bannerCode = BannerCode.CreateFrom(id);
				this._creatingTexture = true;
				TableauCacheManager.Current.BeginCreateBannerTexture(this._bannerCode, new Action<TaleWorlds.Engine.Texture>(this.OnTextureCreated), false, false);
				return;
			case 4:
			{
				this._creatingTexture = true;
				PlayerId playerId = PlayerId.FromString(id);
				int forcedIndex;
				if (!additionalArgs.IsEmpty<char>())
				{
					forcedIndex = int.Parse(additionalArgs);
				}
				else
				{
					NetworkCommunicator networkCommunicator = GameNetwork.NetworkPeers.FirstOrDefault((NetworkCommunicator np) => np.VirtualPlayer.Id == playerId);
					forcedIndex = ((networkCommunicator != null) ? networkCommunicator.ForcedAvatarIndex : -1);
				}
				AvatarData playerAvatar = AvatarServices.GetPlayerAvatar(playerId, forcedIndex);
				if (playerAvatar != null)
				{
					this._receivedAvatarData = playerAvatar;
					return;
				}
				this._timeSinceAvatarFail = 0f;
				return;
			}
			case 5:
				this._characterCode = CharacterCode.CreateFrom(id);
				if (FaceGen.GetMaturityTypeWithAge(this._characterCode.BodyProperties.Age) <= BodyMeshMaturityType.Child)
				{
					this.OnTextureCreated(null);
					return;
				}
				this._creatingTexture = true;
				TableauCacheManager.Current.BeginCreateCharacterTexture(this._characterCode, new Action<TaleWorlds.Engine.Texture>(this.OnTextureCreated), this.IsBig);
				return;
			case 6:
				this._bannerCode = BannerCode.CreateFrom(id);
				this._creatingTexture = true;
				TableauCacheManager.Current.BeginCreateBannerTexture(this._bannerCode, new Action<TaleWorlds.Engine.Texture>(this.OnTextureCreated), true, this.IsBig);
				return;
			default:
				Debug.FailedAssert("WRONG IMAGE IDENTIFIER ID", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\TextureProviders\\ImageIdentifierTextureProvider.cs", "CreateImageWithId", 284);
				return;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00007358 File Offset: 0x00005558
		private void OnAvatarLoaded(string avatarID, AvatarData avatarData)
		{
			if (avatarData != null)
			{
				this._creatingTexture = true;
				TaleWorlds.Engine.Texture texture = TableauCacheManager.Current.CreateAvatarTexture(avatarID, avatarData.Image, avatarData.Width, avatarData.Height, avatarData.Type);
				this.OnTextureCreated(texture);
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000739A File Offset: 0x0000559A
		public override void Clear(bool clearNextFrame)
		{
			base.Clear(clearNextFrame);
			this._providedTexture = null;
			this._textureRequiresRefreshing = true;
			this._itemObject = null;
			this._craftingPiece = null;
			this._bannerCode = null;
			this._characterCode = null;
			this._creatingTexture = false;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000073D4 File Offset: 0x000055D4
		public void ReleaseCache()
		{
			switch (this.ImageTypeCode)
			{
			case 0:
			case 4:
				break;
			case 1:
				if (this._itemObject != null)
				{
					TableauCacheManager.Current.ReleaseTextureWithId(this._itemObject);
					return;
				}
				break;
			case 2:
				if (this._craftingPiece != null)
				{
					TableauCacheManager.Current.ReleaseTextureWithId(this._craftingPiece, this.ImageId.Split(new char[]
					{
						'$'
					})[1]);
					return;
				}
				break;
			case 3:
				if (this._bannerCode != null)
				{
					TableauCacheManager.Current.ReleaseTextureWithId(this._bannerCode, false, false);
					return;
				}
				break;
			case 5:
				if (this._characterCode != null && FaceGen.GetMaturityTypeWithAge(this._characterCode.BodyProperties.Age) > BodyMeshMaturityType.Child)
				{
					TableauCacheManager.Current.ReleaseTextureWithId(this._characterCode, this.IsBig);
					return;
				}
				break;
			case 6:
				TableauCacheManager.Current.ReleaseTextureWithId(this._bannerCode, true, this.IsBig);
				break;
			default:
				return;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000074CA File Offset: 0x000056CA
		private void OnTextureCreated(TaleWorlds.Engine.Texture texture)
		{
			this._texture = texture;
			this._textureRequiresRefreshing = false;
			this._handleNewlyCreatedTexture = true;
			this._creatingTexture = false;
		}

		// Token: 0x04000090 RID: 144
		private const float AvatarFailWaitTime = 5f;

		// Token: 0x04000091 RID: 145
		private ItemObject _itemObject;

		// Token: 0x04000092 RID: 146
		private CraftingPiece _craftingPiece;

		// Token: 0x04000093 RID: 147
		private BannerCode _bannerCode;

		// Token: 0x04000094 RID: 148
		private CharacterCode _characterCode;

		// Token: 0x04000095 RID: 149
		private TaleWorlds.Engine.Texture _texture;

		// Token: 0x04000096 RID: 150
		private TaleWorlds.TwoDimension.Texture _providedTexture;

		// Token: 0x04000097 RID: 151
		private string _imageId;

		// Token: 0x04000098 RID: 152
		private string _additionalArgs;

		// Token: 0x04000099 RID: 153
		private int _imageTypeCode;

		// Token: 0x0400009A RID: 154
		private bool _isBig;

		// Token: 0x0400009B RID: 155
		private bool _isReleased;

		// Token: 0x0400009C RID: 156
		private AvatarData _receivedAvatarData;

		// Token: 0x0400009D RID: 157
		private float _timeSinceAvatarFail;

		// Token: 0x0400009E RID: 158
		private bool _textureRequiresRefreshing;

		// Token: 0x0400009F RID: 159
		private bool _handleNewlyCreatedTexture;

		// Token: 0x040000A0 RID: 160
		private bool _creatingTexture;
	}
}

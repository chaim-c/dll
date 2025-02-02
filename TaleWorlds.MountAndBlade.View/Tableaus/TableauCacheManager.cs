using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.Scripts;
using TaleWorlds.ObjectSystem;
using TaleWorlds.PlayerServices.Avatar;

namespace TaleWorlds.MountAndBlade.View.Tableaus
{
	// Token: 0x02000026 RID: 38
	public class TableauCacheManager
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000CBC2 File Offset: 0x0000ADC2
		// (set) Token: 0x06000182 RID: 386 RVA: 0x0000CBC9 File Offset: 0x0000ADC9
		public static TableauCacheManager Current { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000CBD1 File Offset: 0x0000ADD1
		// (set) Token: 0x06000184 RID: 388 RVA: 0x0000CBD9 File Offset: 0x0000ADD9
		public MatrixFrame InventorySceneCameraFrame { get; private set; }

		// Token: 0x06000185 RID: 389 RVA: 0x0000CBE4 File Offset: 0x0000ADE4
		private void InitializeThumbnailCreator()
		{
			this._thumbnailCreatorView = ThumbnailCreatorView.CreateThumbnailCreatorView();
			ThumbnailCreatorView.renderCallback = (ThumbnailCreatorView.OnThumbnailRenderCompleteDelegate)Delegate.Combine(ThumbnailCreatorView.renderCallback, new ThumbnailCreatorView.OnThumbnailRenderCompleteDelegate(this.OnThumbnailRenderComplete));
			foreach (Scene scene in BannerlordTableauManager.TableauCharacterScenes)
			{
				this._thumbnailCreatorView.RegisterScene(scene, true);
			}
			this._bannerScene = Scene.CreateNewScene(true, false, DecalAtlasGroup.All, "mono_renderscene");
			this._bannerScene.DisableStaticShadows(true);
			this._bannerScene.SetName("TableauCacheManager.BannerScene");
			this._bannerScene.SetDefaultLighting();
			SceneInitializationData sceneInitializationData = new SceneInitializationData(true);
			sceneInitializationData.InitPhysicsWorld = false;
			sceneInitializationData.DoNotUseLoadingScreen = true;
			this._inventoryScene = Scene.CreateNewScene(true, false, DecalAtlasGroup.Battle, "mono_renderscene");
			this._inventoryScene.Read("inventory_character_scene", ref sceneInitializationData, "");
			this._inventoryScene.SetShadow(true);
			this._inventoryScene.DisableStaticShadows(true);
			this.InventorySceneCameraFrame = this._inventoryScene.FindEntityWithTag("camera_instance").GetGlobalFrame();
			this._inventorySceneAgentRenderer = MBAgentRendererSceneController.CreateNewAgentRendererSceneController(this._inventoryScene, 32);
			this._thumbnailCreatorView.RegisterScene(this._bannerScene, false);
			this.bannerTableauGPUAllocationIndex = Utilities.RegisterGPUAllocationGroup("BannerTableauCache");
			this.itemTableauGPUAllocationIndex = Utilities.RegisterGPUAllocationGroup("ItemTableauCache");
			this.characterTableauGPUAllocationIndex = Utilities.RegisterGPUAllocationGroup("CharacterTableauCache");
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000CD45 File Offset: 0x0000AF45
		public bool IsCachedInventoryTableauSceneUsed()
		{
			return this._inventorySceneBeingUsed;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000CD4D File Offset: 0x0000AF4D
		public Scene GetCachedInventoryTableauScene()
		{
			this._inventorySceneBeingUsed = true;
			return this._inventoryScene;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000CD5C File Offset: 0x0000AF5C
		public void ReturnCachedInventoryTableauScene()
		{
			this._inventorySceneBeingUsed = false;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000CD65 File Offset: 0x0000AF65
		public bool IsCachedMapConversationTableauSceneUsed()
		{
			return this._mapConversationSceneBeingUsed;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000CD6D File Offset: 0x0000AF6D
		public Scene GetCachedMapConversationTableauScene()
		{
			this._mapConversationSceneBeingUsed = true;
			return this._mapConversationScene;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000CD7C File Offset: 0x0000AF7C
		public void ReturnCachedMapConversationTableauScene()
		{
			this._mapConversationSceneBeingUsed = false;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000CD85 File Offset: 0x0000AF85
		public static int GetNumberOfPendingRequests()
		{
			if (TableauCacheManager.Current != null)
			{
				return TableauCacheManager.Current._thumbnailCreatorView.GetNumberOfPendingRequests();
			}
			return 0;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000CD9F File Offset: 0x0000AF9F
		public static bool IsNativeMemoryCleared()
		{
			return TableauCacheManager.Current != null && TableauCacheManager.Current._thumbnailCreatorView.IsMemoryCleared();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000CDBC File Offset: 0x0000AFBC
		public static void InitializeManager()
		{
			TableauCacheManager.Current = new TableauCacheManager();
			TableauCacheManager.Current._renderCallbacks = new Dictionary<string, TableauCacheManager.RenderDetails>();
			TableauCacheManager.Current.InitializeThumbnailCreator();
			TableauCacheManager.Current._avatarVisuals = new ThumbnailCache(100, TableauCacheManager.Current._thumbnailCreatorView);
			TableauCacheManager.Current._itemVisuals = new ThumbnailCache(100, TableauCacheManager.Current._thumbnailCreatorView);
			TableauCacheManager.Current._craftingPieceVisuals = new ThumbnailCache(100, TableauCacheManager.Current._thumbnailCreatorView);
			TableauCacheManager.Current._characterVisuals = new ThumbnailCache(100, TableauCacheManager.Current._thumbnailCreatorView);
			TableauCacheManager.Current._bannerVisuals = new ThumbnailCache(100, TableauCacheManager.Current._thumbnailCreatorView);
			TableauCacheManager.Current._bannerCamera = TableauCacheManager.CreateDefaultBannerCamera();
			TableauCacheManager.Current._nineGridBannerCamera = TableauCacheManager.CreateNineGridBannerCamera();
			TableauCacheManager.Current._heroSilhouetteTexture = Texture.GetFromResource("hero_silhouette");
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000CEA8 File Offset: 0x0000B0A8
		public static void InitializeSandboxValues()
		{
			SceneInitializationData sceneInitializationData = new SceneInitializationData(true);
			sceneInitializationData.InitPhysicsWorld = false;
			TableauCacheManager.Current._mapConversationScene = Scene.CreateNewScene(true, false, DecalAtlasGroup.All, "mono_renderscene");
			TableauCacheManager.Current._mapConversationScene.SetName("MapConversationTableau");
			TableauCacheManager.Current._mapConversationScene.DisableStaticShadows(true);
			TableauCacheManager.Current._mapConversationScene.Read("scn_conversation_tableau", ref sceneInitializationData, "");
			TableauCacheManager.Current._mapConversationScene.SetShadow(true);
			TableauCacheManager.Current._mapConversationSceneAgentRenderer = MBAgentRendererSceneController.CreateNewAgentRendererSceneController(TableauCacheManager.Current._mapConversationScene, 32);
			Utilities.LoadVirtualTextureTileset("WorldMap");
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000CF50 File Offset: 0x0000B150
		public static void ReleaseSandboxValues()
		{
			MBAgentRendererSceneController.DestructAgentRendererSceneController(TableauCacheManager.Current._mapConversationScene, TableauCacheManager.Current._mapConversationSceneAgentRenderer, false);
			TableauCacheManager.Current._mapConversationSceneAgentRenderer = null;
			TableauCacheManager.Current._mapConversationScene.ClearAll();
			TableauCacheManager.Current._mapConversationScene.ManualInvalidate();
			TableauCacheManager.Current._mapConversationScene = null;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000CFAC File Offset: 0x0000B1AC
		public static void ClearManager()
		{
			Debug.Print("TableauCacheManager::ClearManager", 0, Debug.DebugColor.White, 17592186044416UL);
			if (TableauCacheManager.Current != null)
			{
				TableauCacheManager.Current._renderCallbacks = null;
				TableauCacheManager.Current._avatarVisuals = null;
				TableauCacheManager.Current._itemVisuals = null;
				TableauCacheManager.Current._craftingPieceVisuals = null;
				TableauCacheManager.Current._characterVisuals = null;
				TableauCacheManager.Current._bannerVisuals = null;
				Camera bannerCamera = TableauCacheManager.Current._bannerCamera;
				if (bannerCamera != null)
				{
					bannerCamera.ReleaseCamera();
				}
				TableauCacheManager.Current._bannerCamera = null;
				Camera nineGridBannerCamera = TableauCacheManager.Current._nineGridBannerCamera;
				if (nineGridBannerCamera != null)
				{
					nineGridBannerCamera.ReleaseCamera();
				}
				TableauCacheManager.Current._nineGridBannerCamera = null;
				MBAgentRendererSceneController.DestructAgentRendererSceneController(TableauCacheManager.Current._inventoryScene, TableauCacheManager.Current._inventorySceneAgentRenderer, true);
				Scene inventoryScene = TableauCacheManager.Current._inventoryScene;
				if (inventoryScene != null)
				{
					inventoryScene.ManualInvalidate();
				}
				TableauCacheManager.Current._inventoryScene = null;
				Scene bannerScene = TableauCacheManager.Current._bannerScene;
				if (bannerScene != null)
				{
					bannerScene.ClearDecals();
				}
				Scene bannerScene2 = TableauCacheManager.Current._bannerScene;
				if (bannerScene2 != null)
				{
					bannerScene2.ClearAll();
				}
				Scene bannerScene3 = TableauCacheManager.Current._bannerScene;
				if (bannerScene3 != null)
				{
					bannerScene3.ManualInvalidate();
				}
				TableauCacheManager.Current._bannerScene = null;
				ThumbnailCreatorView.renderCallback = (ThumbnailCreatorView.OnThumbnailRenderCompleteDelegate)Delegate.Remove(ThumbnailCreatorView.renderCallback, new ThumbnailCreatorView.OnThumbnailRenderCompleteDelegate(TableauCacheManager.Current.OnThumbnailRenderComplete));
				TableauCacheManager.Current._thumbnailCreatorView.ClearRequests();
				TableauCacheManager.Current._thumbnailCreatorView = null;
				TableauCacheManager.Current = null;
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000D124 File Offset: 0x0000B324
		private string ByteWidthToString(int bytes)
		{
			double num = Math.Log((double)bytes);
			if (bytes == 0)
			{
				num = 0.0;
			}
			int num2 = (int)(num / Math.Log(1024.0));
			char c = " KMGTPE"[num2];
			return ((double)bytes / Math.Pow(1024.0, (double)num2)).ToString("0.00") + " " + c.ToString() + "      ";
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000D19C File Offset: 0x0000B39C
		private void PrintTextureToImgui(string name, ThumbnailCache cache)
		{
			int totalMemorySize = cache.GetTotalMemorySize();
			Imgui.Text(name);
			Imgui.NextColumn();
			Imgui.Text(cache.Count.ToString());
			Imgui.NextColumn();
			Imgui.Text(this.ByteWidthToString(totalMemorySize));
			Imgui.NextColumn();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000D1E4 File Offset: 0x0000B3E4
		public void OnImguiProfilerTick()
		{
			Imgui.BeginMainThreadScope();
			Imgui.Begin("Tableau Cache Manager");
			Imgui.Columns(3, "", true);
			Imgui.Text("Name");
			Imgui.NextColumn();
			Imgui.Text("Count");
			Imgui.NextColumn();
			Imgui.Text("Memory");
			Imgui.NextColumn();
			Imgui.Separator();
			this.PrintTextureToImgui("Items", this._itemVisuals);
			this.PrintTextureToImgui("Banners", this._bannerVisuals);
			this.PrintTextureToImgui("Characters", this._characterVisuals);
			this.PrintTextureToImgui("Avatars", this._avatarVisuals);
			this.PrintTextureToImgui("Crafting Pieces", this._craftingPieceVisuals);
			Imgui.Text("Render Callbacks");
			Imgui.NextColumn();
			Imgui.Text(this._renderCallbacks.Count<KeyValuePair<string, TableauCacheManager.RenderDetails>>().ToString());
			Imgui.NextColumn();
			Imgui.Text("-");
			Imgui.NextColumn();
			Imgui.End();
			Imgui.EndMainThreadScope();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000D2D8 File Offset: 0x0000B4D8
		private void OnThumbnailRenderComplete(string renderId, Texture renderTarget)
		{
			Texture texture = null;
			if (this._itemVisuals.GetValue(renderId, out texture))
			{
				this._itemVisuals.Add(renderId, renderTarget);
			}
			else if (this._craftingPieceVisuals.GetValue(renderId, out texture))
			{
				this._craftingPieceVisuals.Add(renderId, renderTarget);
			}
			else if (this._characterVisuals.GetValue(renderId, out texture))
			{
				this._characterVisuals.Add(renderId, renderTarget);
			}
			else if (!this._avatarVisuals.GetValue(renderId, out texture) && !this._bannerVisuals.GetValue(renderId, out texture))
			{
				renderTarget.Release();
			}
			if (this._renderCallbacks.ContainsKey(renderId))
			{
				foreach (Action<Texture> action in this._renderCallbacks[renderId].Actions)
				{
					if (action != null)
					{
						action(renderTarget);
					}
				}
				this._renderCallbacks.Remove(renderId);
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000D3E0 File Offset: 0x0000B5E0
		public Texture CreateAvatarTexture(string avatarID, byte[] avatarBytes, uint width, uint height, AvatarData.ImageType imageType)
		{
			Texture texture;
			this._avatarVisuals.GetValue(avatarID, out texture);
			if (texture == null)
			{
				if (imageType == AvatarData.ImageType.Image)
				{
					texture = Texture.CreateFromMemory(avatarBytes);
				}
				else if (imageType == AvatarData.ImageType.Raw)
				{
					texture = Texture.CreateFromByteArray(avatarBytes, (int)width, (int)height);
				}
				this._avatarVisuals.Add(avatarID, texture);
			}
			this._avatarVisuals.AddReference(avatarID);
			return texture;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000D43C File Offset: 0x0000B63C
		public void BeginCreateItemTexture(ItemObject itemObject, string additionalArgs, Action<Texture> setAction)
		{
			string text = itemObject.StringId;
			if (itemObject.Type == ItemObject.ItemTypeEnum.Shield)
			{
				text = text + "_" + additionalArgs;
			}
			Texture obj;
			if (this._itemVisuals.GetValue(text, out obj))
			{
				if (this._renderCallbacks.ContainsKey(text))
				{
					this._renderCallbacks[text].Actions.Add(setAction);
				}
				else if (setAction != null)
				{
					setAction(obj);
				}
				this._itemVisuals.AddReference(text);
				return;
			}
			Camera camera = null;
			int num = 2;
			int width = 256;
			int height = 120;
			GameEntity gameEntity = this.CreateItemBaseEntity(itemObject, BannerlordTableauManager.TableauCharacterScenes[num], ref camera);
			this._thumbnailCreatorView.RegisterEntityWithoutTexture(BannerlordTableauManager.TableauCharacterScenes[num], camera, gameEntity, width, height, this.itemTableauGPUAllocationIndex, text, "item_tableau_" + text);
			gameEntity.ManualInvalidate();
			this._itemVisuals.Add(text, null);
			this._itemVisuals.AddReference(text);
			if (!this._renderCallbacks.ContainsKey(text))
			{
				this._renderCallbacks.Add(text, new TableauCacheManager.RenderDetails(new List<Action<Texture>>()));
			}
			this._renderCallbacks[text].Actions.Add(setAction);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000D568 File Offset: 0x0000B768
		public void BeginCreateCraftingPieceTexture(CraftingPiece craftingPiece, string type, Action<Texture> setAction)
		{
			string text = craftingPiece.StringId + "$" + type;
			Texture obj;
			if (this._craftingPieceVisuals.GetValue(text, out obj))
			{
				if (this._renderCallbacks.ContainsKey(text))
				{
					this._renderCallbacks[text].Actions.Add(setAction);
				}
				else if (setAction != null)
				{
					setAction(obj);
				}
				this._craftingPieceVisuals.AddReference(text);
				return;
			}
			Camera camera = null;
			int num = 2;
			int width = 256;
			int height = 180;
			GameEntity gameEntity = this.CreateCraftingPieceBaseEntity(craftingPiece, type, BannerlordTableauManager.TableauCharacterScenes[num], ref camera);
			this._thumbnailCreatorView.RegisterEntityWithoutTexture(BannerlordTableauManager.TableauCharacterScenes[num], camera, gameEntity, width, height, this.itemTableauGPUAllocationIndex, text, "craft_tableau");
			gameEntity.ManualInvalidate();
			this._craftingPieceVisuals.Add(text, null);
			this._craftingPieceVisuals.AddReference(text);
			if (!this._renderCallbacks.ContainsKey(text))
			{
				this._renderCallbacks.Add(text, new TableauCacheManager.RenderDetails(new List<Action<Texture>>()));
			}
			this._renderCallbacks[text].Actions.Add(setAction);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000D684 File Offset: 0x0000B884
		public void BeginCreateCharacterTexture(CharacterCode characterCode, Action<Texture> setAction, bool isBig)
		{
			if (MBObjectManager.Instance == null)
			{
				return;
			}
			characterCode.BodyProperties = new BodyProperties(new DynamicBodyProperties((float)((int)characterCode.BodyProperties.Age), (float)((int)characterCode.BodyProperties.Weight), (float)((int)characterCode.BodyProperties.Build)), characterCode.BodyProperties.StaticProperties);
			string text = characterCode.CreateNewCodeString();
			text += (isBig ? "1" : "0");
			Texture obj;
			if (this._characterVisuals.GetValue(text, out obj))
			{
				if (this._renderCallbacks.ContainsKey(text))
				{
					this._renderCallbacks[text].Actions.Add(setAction);
				}
				else if (setAction != null)
				{
					setAction(obj);
				}
				this._characterVisuals.AddReference(text);
				return;
			}
			Camera camera = null;
			int num = isBig ? 0 : 4;
			GameEntity gameEntity = this.CreateCharacterBaseEntity(characterCode, BannerlordTableauManager.TableauCharacterScenes[num], ref camera, isBig);
			gameEntity = this.FillEntityWithPose(characterCode, gameEntity, BannerlordTableauManager.TableauCharacterScenes[num]);
			int width = 256;
			int height = isBig ? 120 : 184;
			this._thumbnailCreatorView.RegisterEntityWithoutTexture(BannerlordTableauManager.TableauCharacterScenes[num], camera, gameEntity, width, height, this.characterTableauGPUAllocationIndex, text, "character_tableau_" + this._characterCount.ToString());
			gameEntity.ManualInvalidate();
			this._characterCount++;
			this._characterVisuals.Add(text, null);
			this._characterVisuals.AddReference(text);
			if (!this._renderCallbacks.ContainsKey(text))
			{
				this._renderCallbacks.Add(text, new TableauCacheManager.RenderDetails(new List<Action<Texture>>()));
			}
			this._renderCallbacks[text].Actions.Add(setAction);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000D838 File Offset: 0x0000BA38
		public Texture GetCachedHeroSilhouetteTexture()
		{
			return this._heroSilhouetteTexture;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000D840 File Offset: 0x0000BA40
		public Texture BeginCreateBannerTexture(BannerCode bannerCode, Action<Texture> setAction, bool isTableauOrNineGrid = false, bool isLarge = false)
		{
			int width = 512;
			int height = 512;
			Camera cam = this._bannerCamera;
			string str = "BannerThumbnail";
			if (isTableauOrNineGrid)
			{
				cam = this._nineGridBannerCamera;
				if (isLarge)
				{
					width = 1024;
					height = 1024;
					str = "BannerTableauLarge";
				}
				else
				{
					str = "BannerTableauSmall";
				}
			}
			string text = str + ":" + bannerCode.Code;
			Texture texture;
			if (this._bannerVisuals.GetValue(text, out texture))
			{
				if (this._renderCallbacks.ContainsKey(text))
				{
					this._renderCallbacks[text].Actions.Add(setAction);
				}
				else if (setAction != null)
				{
					setAction(texture);
				}
				this._bannerVisuals.AddReference(text);
				return texture;
			}
			MatrixFrame identity = MatrixFrame.Identity;
			Banner banner = bannerCode.CalculateBanner();
			if (Game.Current == null)
			{
				banner.SetBannerVisual(((IBannerVisualCreator)new BannerVisualCreator()).CreateBannerVisual(banner));
			}
			MetaMesh metaMesh = banner.ConvertToMultiMesh();
			GameEntity gameEntity = this._bannerScene.AddItemEntity(ref identity, metaMesh);
			metaMesh.ManualInvalidate();
			gameEntity.SetVisibilityExcludeParents(false);
			Texture texture2 = Texture.CreateRenderTarget(str + this._bannerCount.ToString(), width, height, true, false, true, true);
			this._thumbnailCreatorView.RegisterEntity(this._bannerScene, cam, texture2, gameEntity, this.bannerTableauGPUAllocationIndex, text);
			this._bannerVisuals.Add(text, texture2);
			this._bannerVisuals.AddReference(text);
			this._bannerCount++;
			if (!this._renderCallbacks.ContainsKey(text))
			{
				this._renderCallbacks.Add(text, new TableauCacheManager.RenderDetails(new List<Action<Texture>>()));
			}
			this._renderCallbacks[text].Actions.Add(setAction);
			return texture2;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
		public void Tick()
		{
			ThumbnailCache avatarVisuals = this._avatarVisuals;
			if (avatarVisuals != null)
			{
				avatarVisuals.Tick();
			}
			ThumbnailCache itemVisuals = this._itemVisuals;
			if (itemVisuals != null)
			{
				itemVisuals.Tick();
			}
			ThumbnailCache craftingPieceVisuals = this._craftingPieceVisuals;
			if (craftingPieceVisuals != null)
			{
				craftingPieceVisuals.Tick();
			}
			ThumbnailCache characterVisuals = this._characterVisuals;
			if (characterVisuals != null)
			{
				characterVisuals.Tick();
			}
			ThumbnailCache bannerVisuals = this._bannerVisuals;
			if (bannerVisuals == null)
			{
				return;
			}
			bannerVisuals.Tick();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000DA5C File Offset: 0x0000BC5C
		public void ReleaseTextureWithId(CraftingPiece craftingPiece, string type)
		{
			string key = craftingPiece.StringId + "$" + type;
			this._craftingPieceVisuals.MarkForDeletion(key);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000DA88 File Offset: 0x0000BC88
		public void ReleaseTextureWithId(CharacterCode characterCode, bool isBig)
		{
			characterCode.BodyProperties = new BodyProperties(new DynamicBodyProperties((float)((int)characterCode.BodyProperties.Age), (float)((int)characterCode.BodyProperties.Weight), (float)((int)characterCode.BodyProperties.Build)), characterCode.BodyProperties.StaticProperties);
			string text = characterCode.CreateNewCodeString();
			text += (isBig ? "1" : "0");
			this._characterVisuals.MarkForDeletion(text);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000DB04 File Offset: 0x0000BD04
		public void ReleaseTextureWithId(ItemObject itemObject)
		{
			string stringId = itemObject.StringId;
			this._itemVisuals.MarkForDeletion(stringId);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000DB28 File Offset: 0x0000BD28
		public void ReleaseTextureWithId(BannerCode bannerCode, bool isTableau = false, bool isLarge = false)
		{
			string str = "BannerThumbnail";
			if (isTableau)
			{
				if (isLarge)
				{
					str = "BannerTableauLarge";
				}
				else
				{
					str = "BannerTableauSmall";
				}
			}
			string key = str + ":" + bannerCode.Code;
			this._bannerVisuals.MarkForDeletion(key);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000DB70 File Offset: 0x0000BD70
		public void ForceReleaseBanner(BannerCode bannerCode, bool isTableau = false, bool isLarge = false)
		{
			string str = "BannerThumbnail";
			if (isTableau)
			{
				if (isLarge)
				{
					str = "BannerTableauLarge";
				}
				else
				{
					str = "BannerTableauSmall";
				}
			}
			string key = str + ":" + bannerCode.Code;
			this._bannerVisuals.ForceDelete(key);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000DBB8 File Offset: 0x0000BDB8
		private void GetItemPoseAndCameraForCraftedItem(ItemObject item, Scene scene, ref Camera camera, ref MatrixFrame itemFrame, ref MatrixFrame itemFrame1, ref MatrixFrame itemFrame2)
		{
			if (camera == null)
			{
				camera = Camera.CreateCamera();
			}
			itemFrame = MatrixFrame.Identity;
			WeaponClass weaponClass = item.WeaponDesign.Template.WeaponDescriptions[0].WeaponClass;
			Vec3 u = itemFrame.rotation.u;
			Vec3 v = itemFrame.origin - u * (item.WeaponDesign.CraftedWeaponLength * 0.5f);
			Vec3 v2 = v + u * item.WeaponDesign.CraftedWeaponLength;
			Vec3 v3 = v - u * item.WeaponDesign.BottomPivotOffset;
			int num = 0;
			Vec3 v4 = default(Vec3);
			foreach (float num2 in item.WeaponDesign.TopPivotOffsets)
			{
				if (num2 > MathF.Abs(1E-05f))
				{
					Vec3 vec = v + u * num2;
					if (num == 1)
					{
						v4 = vec;
					}
					num++;
				}
			}
			if (weaponClass == WeaponClass.OneHandedSword || weaponClass == WeaponClass.TwoHandedSword)
			{
				GameEntity gameEntity = scene.FindEntityWithTag("sword_camera");
				Vec3 vec2 = default(Vec3);
				gameEntity.GetCameraParamsFromCameraScript(camera, ref vec2);
				gameEntity.SetVisibilityExcludeParents(false);
				Vec3 v5 = itemFrame.TransformToLocal(v3);
				MatrixFrame identity = MatrixFrame.Identity;
				identity.origin = -v5;
				GameEntity gameEntity2 = scene.FindEntityWithTag("sword");
				gameEntity2.SetVisibilityExcludeParents(false);
				itemFrame = gameEntity2.GetGlobalFrame();
				itemFrame = itemFrame.TransformToParent(identity);
			}
			if (weaponClass == WeaponClass.OneHandedAxe || weaponClass == WeaponClass.TwoHandedAxe)
			{
				GameEntity gameEntity3 = scene.FindEntityWithTag("axe_camera");
				Vec3 vec3 = default(Vec3);
				gameEntity3.GetCameraParamsFromCameraScript(camera, ref vec3);
				gameEntity3.SetVisibilityExcludeParents(false);
				Vec3 v6 = itemFrame.TransformToLocal(v4);
				MatrixFrame identity2 = MatrixFrame.Identity;
				identity2.origin = -v6;
				GameEntity gameEntity4 = scene.FindEntityWithTag("axe");
				gameEntity4.SetVisibilityExcludeParents(false);
				itemFrame = gameEntity4.GetGlobalFrame();
				itemFrame = itemFrame.TransformToParent(identity2);
			}
			if (weaponClass == WeaponClass.Dagger)
			{
				GameEntity gameEntity5 = scene.FindEntityWithTag("sword_camera");
				Vec3 vec4 = default(Vec3);
				gameEntity5.GetCameraParamsFromCameraScript(camera, ref vec4);
				gameEntity5.SetVisibilityExcludeParents(false);
				Vec3 v7 = itemFrame.TransformToLocal(v3);
				MatrixFrame identity3 = MatrixFrame.Identity;
				identity3.origin = -v7;
				GameEntity gameEntity6 = scene.FindEntityWithTag("sword");
				gameEntity6.SetVisibilityExcludeParents(false);
				itemFrame = gameEntity6.GetGlobalFrame();
				itemFrame = itemFrame.TransformToParent(identity3);
			}
			if (weaponClass == WeaponClass.ThrowingAxe)
			{
				GameEntity gameEntity7 = scene.FindEntityWithTag("throwing_axe_camera");
				Vec3 vec5 = default(Vec3);
				gameEntity7.GetCameraParamsFromCameraScript(camera, ref vec5);
				gameEntity7.SetVisibilityExcludeParents(false);
				Vec3 v8 = v + u * item.PrimaryWeapon.CenterOfMass;
				Vec3 v9 = itemFrame.TransformToLocal(v8);
				MatrixFrame identity4 = MatrixFrame.Identity;
				identity4.origin = -v9 * 2.5f;
				GameEntity gameEntity8 = scene.FindEntityWithTag("throwing_axe");
				gameEntity8.SetVisibilityExcludeParents(false);
				itemFrame = gameEntity8.GetGlobalFrame();
				itemFrame = itemFrame.TransformToParent(identity4);
				gameEntity8 = scene.FindEntityWithTag("throwing_axe_1");
				gameEntity8.SetVisibilityExcludeParents(false);
				itemFrame1 = gameEntity8.GetGlobalFrame();
				itemFrame1 = itemFrame1.TransformToParent(identity4);
				gameEntity8 = scene.FindEntityWithTag("throwing_axe_2");
				gameEntity8.SetVisibilityExcludeParents(false);
				itemFrame2 = gameEntity8.GetGlobalFrame();
				itemFrame2 = itemFrame2.TransformToParent(identity4);
			}
			if (weaponClass == WeaponClass.Javelin)
			{
				GameEntity gameEntity9 = scene.FindEntityWithTag("javelin_camera");
				Vec3 vec6 = default(Vec3);
				gameEntity9.GetCameraParamsFromCameraScript(camera, ref vec6);
				gameEntity9.SetVisibilityExcludeParents(false);
				Vec3 v10 = itemFrame.TransformToLocal(v4);
				MatrixFrame identity5 = MatrixFrame.Identity;
				identity5.origin = -v10 * 2.2f;
				GameEntity gameEntity10 = scene.FindEntityWithTag("javelin");
				gameEntity10.SetVisibilityExcludeParents(false);
				itemFrame = gameEntity10.GetGlobalFrame();
				itemFrame = itemFrame.TransformToParent(identity5);
				gameEntity10 = scene.FindEntityWithTag("javelin_1");
				gameEntity10.SetVisibilityExcludeParents(false);
				itemFrame1 = gameEntity10.GetGlobalFrame();
				itemFrame1 = itemFrame1.TransformToParent(identity5);
				gameEntity10 = scene.FindEntityWithTag("javelin_2");
				gameEntity10.SetVisibilityExcludeParents(false);
				itemFrame2 = gameEntity10.GetGlobalFrame();
				itemFrame2 = itemFrame2.TransformToParent(identity5);
			}
			if (weaponClass == WeaponClass.ThrowingKnife)
			{
				GameEntity gameEntity11 = scene.FindEntityWithTag("javelin_camera");
				Vec3 vec7 = default(Vec3);
				gameEntity11.GetCameraParamsFromCameraScript(camera, ref vec7);
				gameEntity11.SetVisibilityExcludeParents(false);
				Vec3 v11 = itemFrame.TransformToLocal(v2);
				MatrixFrame identity6 = MatrixFrame.Identity;
				identity6.origin = -v11 * 1.4f;
				GameEntity gameEntity12 = scene.FindEntityWithTag("javelin");
				gameEntity12.SetVisibilityExcludeParents(false);
				itemFrame = gameEntity12.GetGlobalFrame();
				itemFrame = itemFrame.TransformToParent(identity6);
				gameEntity12 = scene.FindEntityWithTag("javelin_1");
				gameEntity12.SetVisibilityExcludeParents(false);
				itemFrame1 = gameEntity12.GetGlobalFrame();
				itemFrame1 = itemFrame1.TransformToParent(identity6);
				gameEntity12 = scene.FindEntityWithTag("javelin_2");
				gameEntity12.SetVisibilityExcludeParents(false);
				itemFrame2 = gameEntity12.GetGlobalFrame();
				itemFrame2 = itemFrame2.TransformToParent(identity6);
			}
			if (weaponClass == WeaponClass.TwoHandedPolearm || weaponClass == WeaponClass.OneHandedPolearm || weaponClass == WeaponClass.LowGripPolearm || weaponClass == WeaponClass.Mace || weaponClass == WeaponClass.TwoHandedMace)
			{
				GameEntity gameEntity13 = scene.FindEntityWithTag("spear_camera");
				Vec3 vec8 = default(Vec3);
				gameEntity13.GetCameraParamsFromCameraScript(camera, ref vec8);
				gameEntity13.SetVisibilityExcludeParents(false);
				Vec3 v12 = itemFrame.TransformToLocal(v4);
				MatrixFrame identity7 = MatrixFrame.Identity;
				identity7.origin = -v12;
				GameEntity gameEntity14 = scene.FindEntityWithTag("spear");
				gameEntity14.SetVisibilityExcludeParents(false);
				itemFrame = gameEntity14.GetGlobalFrame();
				itemFrame = itemFrame.TransformToParent(identity7);
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000E1C4 File Offset: 0x0000C3C4
		private void GetItemPoseAndCamera(ItemObject item, Scene scene, ref Camera camera, ref MatrixFrame itemFrame, ref MatrixFrame itemFrame1, ref MatrixFrame itemFrame2)
		{
			if (item.IsCraftedWeapon)
			{
				this.GetItemPoseAndCameraForCraftedItem(item, scene, ref camera, ref itemFrame, ref itemFrame1, ref itemFrame2);
				return;
			}
			string text = "";
			TableauCacheManager.CustomPoseParameters customPoseParameters = new TableauCacheManager.CustomPoseParameters
			{
				CameraTag = "goods_cam",
				DistanceModifier = 6f,
				FrameTag = "goods_frame"
			};
			if (item.WeaponComponent != null)
			{
				WeaponClass weaponClass = item.WeaponComponent.PrimaryWeapon.WeaponClass;
				if (weaponClass - WeaponClass.OneHandedSword <= 1)
				{
					text = "sword";
				}
			}
			else
			{
				ItemObject.ItemTypeEnum type = item.Type;
				if (type != ItemObject.ItemTypeEnum.HeadArmor)
				{
					if (type == ItemObject.ItemTypeEnum.BodyArmor)
					{
						text = "armor";
					}
				}
				else
				{
					text = "helmet";
				}
			}
			if (item.Type == ItemObject.ItemTypeEnum.Shield)
			{
				text = "shield";
			}
			if (item.Type == ItemObject.ItemTypeEnum.Crossbow)
			{
				text = "crossbow";
			}
			if (item.Type == ItemObject.ItemTypeEnum.Bow)
			{
				text = "bow";
			}
			if (item.Type == ItemObject.ItemTypeEnum.LegArmor)
			{
				text = "boot";
			}
			if (item.Type == ItemObject.ItemTypeEnum.Horse)
			{
				text = ((HorseComponent)item.ItemComponent).Monster.MonsterUsage;
			}
			if (item.Type == ItemObject.ItemTypeEnum.HorseHarness)
			{
				text = "horse";
			}
			if (item.Type == ItemObject.ItemTypeEnum.Cape)
			{
				text = "cape";
			}
			if (item.Type == ItemObject.ItemTypeEnum.HandArmor)
			{
				text = "glove";
			}
			if (item.Type == ItemObject.ItemTypeEnum.Arrows)
			{
				text = "arrow";
			}
			if (item.Type == ItemObject.ItemTypeEnum.Bolts)
			{
				text = "bolt";
			}
			if (item.Type == ItemObject.ItemTypeEnum.Banner)
			{
				customPoseParameters = new TableauCacheManager.CustomPoseParameters
				{
					CameraTag = "banner_cam",
					DistanceModifier = 1.5f,
					FrameTag = "banner_frame",
					FocusAlignment = TableauCacheManager.CustomPoseParameters.Alignment.Top
				};
			}
			if (item.Type == ItemObject.ItemTypeEnum.Animal)
			{
				customPoseParameters = new TableauCacheManager.CustomPoseParameters
				{
					CameraTag = customPoseParameters.CameraTag,
					DistanceModifier = 3f,
					FrameTag = customPoseParameters.FrameTag
				};
			}
			if (item.StringId == "iron" || item.StringId == "hardwood" || item.StringId == "charcoal" || item.StringId == "ironIngot1" || item.StringId == "ironIngot2" || item.StringId == "ironIngot3" || item.StringId == "ironIngot4" || item.StringId == "ironIngot5" || item.StringId == "ironIngot6" || item.ItemCategory == DefaultItemCategories.Silver)
			{
				text = "craftmat";
			}
			if (!string.IsNullOrEmpty(text))
			{
				string tag = text + "_cam";
				string tag2 = text + "_frame";
				GameEntity gameEntity = scene.FindEntityWithTag(tag);
				if (gameEntity != null)
				{
					camera = Camera.CreateCamera();
					Vec3 vec = default(Vec3);
					gameEntity.GetCameraParamsFromCameraScript(camera, ref vec);
				}
				GameEntity gameEntity2 = scene.FindEntityWithTag(tag2);
				if (gameEntity2 != null)
				{
					itemFrame = gameEntity2.GetGlobalFrame();
					gameEntity2.SetVisibilityExcludeParents(false);
				}
			}
			else
			{
				GameEntity gameEntity3 = scene.FindEntityWithTag(customPoseParameters.CameraTag);
				if (gameEntity3 != null)
				{
					camera = Camera.CreateCamera();
					Vec3 vec2 = default(Vec3);
					gameEntity3.GetCameraParamsFromCameraScript(camera, ref vec2);
				}
				GameEntity gameEntity4 = scene.FindEntityWithTag(customPoseParameters.FrameTag);
				if (gameEntity4 != null)
				{
					itemFrame = gameEntity4.GetGlobalFrame();
					gameEntity4.SetVisibilityExcludeParents(false);
					gameEntity4.UpdateGlobalBounds();
					MatrixFrame globalFrame = gameEntity4.GetGlobalFrame();
					MetaMesh itemMeshForInventory = new ItemRosterElement(item, 0, null).GetItemMeshForInventory(false);
					Vec3 vec3 = new Vec3(1000000f, 1000000f, 1000000f, -1f);
					Vec3 vec4 = new Vec3(-1000000f, -1000000f, -1000000f, -1f);
					if (itemMeshForInventory != null)
					{
						MatrixFrame identity = MatrixFrame.Identity;
						for (int num = 0; num != itemMeshForInventory.MeshCount; num++)
						{
							Vec3 boundingBoxMin = itemMeshForInventory.GetMeshAtIndex(num).GetBoundingBoxMin();
							Vec3 boundingBoxMax = itemMeshForInventory.GetMeshAtIndex(num).GetBoundingBoxMax();
							Vec3[] array = new Vec3[]
							{
								globalFrame.TransformToParent(new Vec3(boundingBoxMin.x, boundingBoxMin.y, boundingBoxMin.z, -1f)),
								globalFrame.TransformToParent(new Vec3(boundingBoxMin.x, boundingBoxMin.y, boundingBoxMax.z, -1f)),
								globalFrame.TransformToParent(new Vec3(boundingBoxMin.x, boundingBoxMax.y, boundingBoxMin.z, -1f)),
								globalFrame.TransformToParent(new Vec3(boundingBoxMin.x, boundingBoxMax.y, boundingBoxMax.z, -1f)),
								globalFrame.TransformToParent(new Vec3(boundingBoxMax.x, boundingBoxMin.y, boundingBoxMin.z, -1f)),
								globalFrame.TransformToParent(new Vec3(boundingBoxMax.x, boundingBoxMin.y, boundingBoxMax.z, -1f)),
								globalFrame.TransformToParent(new Vec3(boundingBoxMax.x, boundingBoxMax.y, boundingBoxMin.z, -1f)),
								globalFrame.TransformToParent(new Vec3(boundingBoxMax.x, boundingBoxMax.y, boundingBoxMax.z, -1f))
							};
							for (int i = 0; i < 8; i++)
							{
								vec3 = Vec3.Vec3Min(vec3, array[i]);
								vec4 = Vec3.Vec3Max(vec4, array[i]);
							}
						}
					}
					Vec3 v = (vec3 + vec4) * 0.5f;
					Vec3 v2 = gameEntity4.GetGlobalFrame().TransformToLocal(v);
					MatrixFrame globalFrame2 = gameEntity4.GetGlobalFrame();
					globalFrame2.origin -= v2;
					itemFrame = globalFrame2;
					MatrixFrame frame = camera.Frame;
					float f = (vec4 - vec3).Length * customPoseParameters.DistanceModifier;
					frame.origin += frame.rotation.u * f;
					if (customPoseParameters.FocusAlignment == TableauCacheManager.CustomPoseParameters.Alignment.Top)
					{
						frame.origin += new Vec3(0f, 0f, (vec4 - vec3).Z * 0.3f, -1f);
					}
					else if (customPoseParameters.FocusAlignment == TableauCacheManager.CustomPoseParameters.Alignment.Bottom)
					{
						frame.origin -= new Vec3(0f, 0f, (vec4 - vec3).Z * 0.3f, -1f);
					}
					camera.Frame = frame;
				}
			}
			if (camera == null)
			{
				camera = Camera.CreateCamera();
				camera.SetViewVolume(false, -1f, 1f, -0.5f, 0.5f, 0.01f, 100f);
				MatrixFrame identity2 = MatrixFrame.Identity;
				identity2.origin -= identity2.rotation.u * 7f;
				identity2.rotation.u = identity2.rotation.u * -1f;
				camera.Frame = identity2;
			}
			if (item.Type == ItemObject.ItemTypeEnum.Shield)
			{
				GameEntity gameEntity5 = scene.FindEntityWithTag("shield_cam");
				MatrixFrame holsterFrameByIndex = MBItem.GetHolsterFrameByIndex(MBItem.GetItemHolsterIndex(item.ItemHolsters[0]));
				itemFrame.rotation = holsterFrameByIndex.rotation;
				MatrixFrame frame2 = itemFrame.TransformToParent(gameEntity5.GetFrame());
				camera.Frame = frame2;
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000E9AC File Offset: 0x0000CBAC
		private GameEntity AddItem(Scene scene, ItemObject item, MatrixFrame itemFrame, MatrixFrame itemFrame1, MatrixFrame itemFrame2)
		{
			ItemRosterElement rosterElement = new ItemRosterElement(item, 0, null);
			MetaMesh itemMeshForInventory = rosterElement.GetItemMeshForInventory(false);
			if (item.IsCraftedWeapon)
			{
				MatrixFrame frame = itemMeshForInventory.Frame;
				frame.Elevate(-item.WeaponDesign.CraftedWeaponLength / 2f);
				itemMeshForInventory.Frame = frame;
			}
			GameEntity gameEntity = null;
			if (itemMeshForInventory != null && rosterElement.EquipmentElement.Item.ItemType == ItemObject.ItemTypeEnum.HandArmor)
			{
				gameEntity = GameEntity.CreateEmpty(scene, true);
				AnimationSystemData animationSystemData = Game.Current.DefaultMonster.FillAnimationSystemData(MBActionSet.GetActionSet(Game.Current.DefaultMonster.ActionSetCode), 1f, false);
				gameEntity.CreateSkeletonWithActionSet(ref animationSystemData);
				gameEntity.SetFrame(ref itemFrame);
				gameEntity.Skeleton.SetAgentActionChannel(0, this.act_tableau_hand_armor_pose, 0f, -0.2f, true);
				gameEntity.AddMultiMeshToSkeleton(itemMeshForInventory);
				gameEntity.Skeleton.TickAnimationsAndForceUpdate(0.01f, itemFrame, true);
			}
			else if (itemMeshForInventory != null)
			{
				if (item.WeaponComponent != null)
				{
					WeaponClass weaponClass = item.WeaponComponent.PrimaryWeapon.WeaponClass;
					if (weaponClass == WeaponClass.ThrowingAxe || weaponClass == WeaponClass.ThrowingKnife || weaponClass == WeaponClass.Javelin || weaponClass == WeaponClass.Bolt)
					{
						gameEntity = GameEntity.CreateEmpty(scene, true);
						MetaMesh metaMesh = itemMeshForInventory.CreateCopy();
						metaMesh.Frame = itemFrame;
						gameEntity.AddMultiMesh(metaMesh, true);
						MetaMesh metaMesh2 = itemMeshForInventory.CreateCopy();
						metaMesh2.Frame = itemFrame1;
						gameEntity.AddMultiMesh(metaMesh2, true);
						MetaMesh metaMesh3 = itemMeshForInventory.CreateCopy();
						metaMesh3.Frame = itemFrame2;
						gameEntity.AddMultiMesh(metaMesh3, true);
					}
					else
					{
						gameEntity = scene.AddItemEntity(ref itemFrame, itemMeshForInventory);
					}
				}
				else
				{
					gameEntity = scene.AddItemEntity(ref itemFrame, itemMeshForInventory);
					if (item.Type == ItemObject.ItemTypeEnum.HorseHarness && item.ArmorComponent != null)
					{
						MetaMesh copy = MetaMesh.GetCopy(item.ArmorComponent.ReinsMesh, true, true);
						if (copy != null)
						{
							gameEntity.AddMultiMesh(copy, true);
						}
					}
				}
			}
			else
			{
				MBDebug.ShowWarning("[DEBUG]Item with " + rosterElement.EquipmentElement.Item.StringId + "[DEBUG] string id cannot be found");
			}
			gameEntity.SetVisibilityExcludeParents(false);
			return gameEntity;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000EBBC File Offset: 0x0000CDBC
		private void GetPoseParamsFromCharacterCode(CharacterCode characterCode, out string poseName, out bool hasHorse)
		{
			hasHorse = false;
			if (characterCode.IsHero)
			{
				int num = MBRandom.NondeterministicRandomInt % 8;
				poseName = "lord_" + num;
				return;
			}
			poseName = "troop_villager";
			int num2 = -1;
			int num3 = -1;
			Equipment equipment = characterCode.CalculateEquipment();
			switch (characterCode.FormationClass)
			{
			case FormationClass.Infantry:
			case FormationClass.Cavalry:
			case FormationClass.NumberOfDefaultFormations:
			case FormationClass.HeavyInfantry:
			case FormationClass.LightCavalry:
			case FormationClass.HeavyCavalry:
			case FormationClass.NumberOfRegularFormations:
			case FormationClass.Bodyguard:
				for (int i = 0; i < 4; i++)
				{
					ItemObject item = equipment[i].Item;
					if (((item != null) ? item.PrimaryWeapon : null) != null)
					{
						if (num3 == -1 && equipment[i].Item.ItemFlags.HasAnyFlag(ItemFlags.HeldInOffHand))
						{
							num3 = i;
						}
						if (num2 == -1 && equipment[i].Item.PrimaryWeapon.WeaponFlags.HasAnyFlag(WeaponFlags.MeleeWeapon))
						{
							num2 = i;
						}
					}
				}
				break;
			case FormationClass.Ranged:
			case FormationClass.HorseArcher:
				for (int j = 0; j < 4; j++)
				{
					ItemObject item2 = equipment[j].Item;
					if (((item2 != null) ? item2.PrimaryWeapon : null) != null)
					{
						if (num3 == -1 && equipment[j].Item.ItemFlags.HasAnyFlag(ItemFlags.HeldInOffHand))
						{
							num3 = j;
						}
						if (num2 == -1 && equipment[j].Item.PrimaryWeapon.WeaponFlags.HasAnyFlag(WeaponFlags.RangedWeapon))
						{
							num2 = j;
						}
					}
				}
				break;
			}
			if (num2 != -1)
			{
				switch (equipment[num2].Item.PrimaryWeapon.WeaponClass)
				{
				case WeaponClass.OneHandedSword:
				case WeaponClass.OneHandedAxe:
					if (num3 == -1)
					{
						poseName = "troop_infantry_sword1h";
					}
					else if (equipment[num3].Item.PrimaryWeapon.IsShield)
					{
						poseName = "troop_infantry_sword1h";
					}
					break;
				case WeaponClass.TwoHandedSword:
				case WeaponClass.TwoHandedAxe:
				case WeaponClass.TwoHandedMace:
					poseName = "troop_infantry_sword2h";
					break;
				case WeaponClass.OneHandedPolearm:
				case WeaponClass.TwoHandedPolearm:
					poseName = "troop_spear";
					break;
				case WeaponClass.LowGripPolearm:
				case WeaponClass.Javelin:
					poseName = "troop_spear";
					break;
				case WeaponClass.Bow:
					poseName = "troop_bow";
					break;
				case WeaponClass.Crossbow:
					poseName = "troop_crossbow";
					break;
				}
			}
			if (!equipment[EquipmentIndex.ArmorItemEndSlot].IsEmpty)
			{
				if (num2 != -1)
				{
					HorseComponent horseComponent = equipment[EquipmentIndex.ArmorItemEndSlot].Item.HorseComponent;
					bool flag;
					if (horseComponent == null)
					{
						flag = false;
					}
					else
					{
						Monster monster = horseComponent.Monster;
						int? num4 = (monster != null) ? new int?(monster.FamilyType) : null;
						int num5 = 2;
						flag = (num4.GetValueOrDefault() == num5 & num4 != null);
					}
					bool flag2 = flag;
					ItemObject.ItemTypeEnum type = equipment[num2].Item.Type;
					if (type != ItemObject.ItemTypeEnum.OneHandedWeapon)
					{
						if (type == ItemObject.ItemTypeEnum.Bow)
						{
							poseName = "troop_cavalry_archer";
						}
						else
						{
							poseName = "troop_cavalry_lance";
						}
					}
					else if (num3 == -1)
					{
						poseName = "troop_cavalry_sword";
					}
					else if (equipment[num3].Item.PrimaryWeapon.IsShield)
					{
						poseName = "troop_cavalry_sword";
					}
					if (flag2)
					{
						poseName = "camel_" + poseName;
					}
				}
				hasHorse = true;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000EF10 File Offset: 0x0000D110
		private GameEntity CreateCraftingPieceBaseEntity(CraftingPiece craftingPiece, string ItemType, Scene scene, ref Camera camera)
		{
			MatrixFrame matrixFrame = MatrixFrame.Identity;
			bool flag = false;
			string tag = "craftingPiece_cam";
			string tag2 = "craftingPiece_frame";
			if (craftingPiece.PieceType == CraftingPiece.PieceTypes.Blade)
			{
				if (ItemType == "OneHandedAxe" || ItemType == "ThrowingAxe")
				{
					tag = "craft_axe_camera";
					tag2 = "craft_axe";
				}
				else if (ItemType == "TwoHandedAxe")
				{
					tag = "craft_big_axe_camera";
					tag2 = "craft_big_axe";
				}
				else if (ItemType == "Dagger" || ItemType == "ThrowingKnife" || ItemType == "TwoHandedPolearm" || ItemType == "Pike" || ItemType == "Javelin")
				{
					tag = "craft_spear_blade_camera";
					tag2 = "craft_spear_blade";
				}
				else if (ItemType == "Mace" || ItemType == "TwoHandedMace")
				{
					tag = "craft_mace_camera";
					tag2 = "craft_mace";
				}
				else
				{
					tag = "craft_blade_camera";
					tag2 = "craft_blade";
				}
				flag = true;
			}
			else if (craftingPiece.PieceType == CraftingPiece.PieceTypes.Pommel)
			{
				tag = "craft_pommel_camera";
				tag2 = "craft_pommel";
				flag = true;
			}
			else if (craftingPiece.PieceType == CraftingPiece.PieceTypes.Guard)
			{
				tag = "craft_guard_camera";
				tag2 = "craft_guard";
				flag = true;
			}
			else if (craftingPiece.PieceType == CraftingPiece.PieceTypes.Handle)
			{
				tag = "craft_handle_camera";
				tag2 = "craft_handle";
				flag = true;
			}
			bool flag2 = false;
			if (flag)
			{
				GameEntity gameEntity = scene.FindEntityWithTag(tag);
				if (gameEntity != null)
				{
					camera = Camera.CreateCamera();
					Vec3 vec = default(Vec3);
					gameEntity.GetCameraParamsFromCameraScript(camera, ref vec);
				}
				GameEntity gameEntity2 = scene.FindEntityWithTag(tag2);
				if (gameEntity2 != null)
				{
					matrixFrame = gameEntity2.GetGlobalFrame();
					gameEntity2.SetVisibilityExcludeParents(false);
					flag2 = true;
				}
			}
			else
			{
				GameEntity gameEntity3 = scene.FindEntityWithTag("old_system_item_frame");
				if (gameEntity3 != null)
				{
					matrixFrame = gameEntity3.GetGlobalFrame();
					gameEntity3.SetVisibilityExcludeParents(false);
				}
			}
			if (camera == null)
			{
				camera = Camera.CreateCamera();
				camera.SetViewVolume(false, -1f, 1f, -0.5f, 0.5f, 0.01f, 100f);
				MatrixFrame identity = MatrixFrame.Identity;
				identity.origin -= identity.rotation.u * 7f;
				identity.rotation.u = identity.rotation.u * -1f;
				camera.Frame = identity;
			}
			if (!flag2)
			{
				matrixFrame = craftingPiece.GetCraftingPieceFrameForInventory();
			}
			MetaMesh copy = MetaMesh.GetCopy(craftingPiece.MeshName, true, false);
			GameEntity gameEntity4 = null;
			if (copy != null)
			{
				gameEntity4 = scene.AddItemEntity(ref matrixFrame, copy);
			}
			else
			{
				MBDebug.ShowWarning("[DEBUG]craftingPiece with " + craftingPiece.StringId + "[DEBUG] string id cannot be found");
			}
			gameEntity4.SetVisibilityExcludeParents(false);
			return gameEntity4;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000F1D0 File Offset: 0x0000D3D0
		private GameEntity CreateItemBaseEntity(ItemObject item, Scene scene, ref Camera camera)
		{
			MatrixFrame identity = MatrixFrame.Identity;
			MatrixFrame identity2 = MatrixFrame.Identity;
			MatrixFrame identity3 = MatrixFrame.Identity;
			this.GetItemPoseAndCamera(item, scene, ref camera, ref identity, ref identity2, ref identity3);
			return this.AddItem(scene, item, identity, identity2, identity3);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000F20C File Offset: 0x0000D40C
		private GameEntity CreateCharacterBaseEntity(CharacterCode characterCode, Scene scene, ref Camera camera, bool isBig)
		{
			string str;
			bool flag;
			this.GetPoseParamsFromCharacterCode(characterCode, out str, out flag);
			string tag = str + "_pose";
			string tag2 = isBig ? (str + "_cam") : (str + "_cam_small");
			GameEntity gameEntity = scene.FindEntityWithTag(tag);
			if (gameEntity == null)
			{
				return null;
			}
			gameEntity.SetVisibilityExcludeParents(true);
			GameEntity gameEntity2 = GameEntity.CopyFromPrefab(gameEntity);
			gameEntity2.Name = gameEntity.Name + "Instance";
			gameEntity2.RemoveTag(tag);
			scene.AttachEntity(gameEntity2, false);
			gameEntity2.SetVisibilityExcludeParents(true);
			gameEntity.SetVisibilityExcludeParents(false);
			GameEntity gameEntity3 = scene.FindEntityWithTag(tag2);
			Vec3 vec = default(Vec3);
			camera = Camera.CreateCamera();
			if (gameEntity3 != null)
			{
				gameEntity3.GetCameraParamsFromCameraScript(camera, ref vec);
				camera.Frame = gameEntity3.GetGlobalFrame();
			}
			return gameEntity2;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000F2EC File Offset: 0x0000D4EC
		private GameEntity FillEntityWithPose(CharacterCode characterCode, GameEntity poseEntity, Scene scene)
		{
			if (characterCode.IsEmpty)
			{
				Debug.FailedAssert("Trying to fill entity with empty character code", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.View\\Tableaus\\TableauCacheManager.cs", "FillEntityWithPose", 1536);
				return poseEntity;
			}
			if (string.IsNullOrEmpty(characterCode.EquipmentCode))
			{
				Debug.FailedAssert("Trying to fill entity with invalid equipment code", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.View\\Tableaus\\TableauCacheManager.cs", "FillEntityWithPose", 1542);
				return poseEntity;
			}
			if (FaceGen.GetBaseMonsterFromRace(characterCode.Race) == null)
			{
				Debug.FailedAssert("There are no monster data for the race: " + characterCode.Race, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.View\\Tableaus\\TableauCacheManager.cs", "FillEntityWithPose", 1549);
				return poseEntity;
			}
			if (TableauCacheManager.Current != null && poseEntity != null)
			{
				string text;
				bool flag;
				this.GetPoseParamsFromCharacterCode(characterCode, out text, out flag);
				CharacterSpawner characterSpawner = poseEntity.GetScriptComponents<CharacterSpawner>().First<CharacterSpawner>();
				characterSpawner.SetCreateFaceImmediately(false);
				characterSpawner.InitWithCharacter(characterCode, false);
			}
			return poseEntity;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000F3B1 File Offset: 0x0000D5B1
		public static Camera CreateDefaultBannerCamera()
		{
			return TableauCacheManager.CreateCamera(0.33333334f, 0.6666667f, -0.6666667f, -0.33333334f, 0.001f, 510f);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000F3D6 File Offset: 0x0000D5D6
		public static Camera CreateNineGridBannerCamera()
		{
			return TableauCacheManager.CreateCamera(0f, 1f, -1f, 0f, 0.001f, 510f);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000F3FC File Offset: 0x0000D5FC
		private static Camera CreateCamera(float left, float right, float bottom, float top, float near, float far)
		{
			Camera camera = Camera.CreateCamera();
			MatrixFrame identity = MatrixFrame.Identity;
			identity.origin.z = 400f;
			camera.Frame = identity;
			camera.LookAt(new Vec3(0f, 0f, 400f, -1f), new Vec3(0f, 0f, 0f, -1f), new Vec3(0f, 1f, 0f, -1f));
			camera.SetViewVolume(false, left, right, bottom, top, near, far);
			return camera;
		}

		// Token: 0x0400010D RID: 269
		private ThumbnailCreatorView _thumbnailCreatorView;

		// Token: 0x0400010E RID: 270
		private Scene _bannerScene;

		// Token: 0x0400010F RID: 271
		private Scene _inventoryScene;

		// Token: 0x04000110 RID: 272
		private bool _inventorySceneBeingUsed;

		// Token: 0x04000111 RID: 273
		private MBAgentRendererSceneController _inventorySceneAgentRenderer;

		// Token: 0x04000112 RID: 274
		private Scene _mapConversationScene;

		// Token: 0x04000113 RID: 275
		private bool _mapConversationSceneBeingUsed;

		// Token: 0x04000114 RID: 276
		private MBAgentRendererSceneController _mapConversationSceneAgentRenderer;

		// Token: 0x04000116 RID: 278
		private Camera _bannerCamera;

		// Token: 0x04000117 RID: 279
		private Camera _nineGridBannerCamera;

		// Token: 0x04000118 RID: 280
		private readonly ActionIndexCache act_tableau_hand_armor_pose = ActionIndexCache.Create("act_tableau_hand_armor_pose");

		// Token: 0x04000119 RID: 281
		private int _characterCount;

		// Token: 0x0400011A RID: 282
		private int _bannerCount;

		// Token: 0x0400011B RID: 283
		private Dictionary<string, TableauCacheManager.RenderDetails> _renderCallbacks;

		// Token: 0x0400011C RID: 284
		private ThumbnailCache _avatarVisuals;

		// Token: 0x0400011D RID: 285
		private ThumbnailCache _itemVisuals;

		// Token: 0x0400011E RID: 286
		private ThumbnailCache _craftingPieceVisuals;

		// Token: 0x0400011F RID: 287
		private ThumbnailCache _characterVisuals;

		// Token: 0x04000120 RID: 288
		private ThumbnailCache _bannerVisuals;

		// Token: 0x04000121 RID: 289
		private int bannerTableauGPUAllocationIndex;

		// Token: 0x04000122 RID: 290
		private int itemTableauGPUAllocationIndex;

		// Token: 0x04000123 RID: 291
		private int characterTableauGPUAllocationIndex;

		// Token: 0x04000124 RID: 292
		private Texture _heroSilhouetteTexture;

		// Token: 0x02000095 RID: 149
		private struct RenderDetails
		{
			// Token: 0x17000079 RID: 121
			// (get) Token: 0x060004C4 RID: 1220 RVA: 0x000262CC File Offset: 0x000244CC
			// (set) Token: 0x060004C5 RID: 1221 RVA: 0x000262D4 File Offset: 0x000244D4
			public List<Action<Texture>> Actions { get; private set; }

			// Token: 0x060004C6 RID: 1222 RVA: 0x000262DD File Offset: 0x000244DD
			public RenderDetails(List<Action<Texture>> setActionList)
			{
				this.Actions = setActionList;
			}
		}

		// Token: 0x02000096 RID: 150
		private struct CustomPoseParameters
		{
			// Token: 0x04000331 RID: 817
			public string CameraTag;

			// Token: 0x04000332 RID: 818
			public string FrameTag;

			// Token: 0x04000333 RID: 819
			public float DistanceModifier;

			// Token: 0x04000334 RID: 820
			public TableauCacheManager.CustomPoseParameters.Alignment FocusAlignment;

			// Token: 0x020000BF RID: 191
			public enum Alignment
			{
				// Token: 0x040003C3 RID: 963
				Center,
				// Token: 0x040003C4 RID: 964
				Top,
				// Token: 0x040003C5 RID: 965
				Bottom
			}
		}
	}
}

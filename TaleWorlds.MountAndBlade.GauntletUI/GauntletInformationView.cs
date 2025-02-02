using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x0200000B RID: 11
	public class GauntletInformationView : GlobalLayer
	{
		// Token: 0x0600004D RID: 77 RVA: 0x000040F0 File Offset: 0x000022F0
		private GauntletInformationView()
		{
			this._layerAsGauntletLayer = new GauntletLayer(100000, "GauntletLayer", false);
			InformationManager.OnShowTooltip += this.OnShowTooltip;
			InformationManager.OnHideTooltip += this.OnHideTooltip;
			base.Layer = this._layerAsGauntletLayer;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004148 File Offset: 0x00002348
		public static void Initialize()
		{
			if (GauntletInformationView._current == null)
			{
				GauntletInformationView._current = new GauntletInformationView();
				ScreenManager.AddGlobalLayer(GauntletInformationView._current, false);
				PropertyBasedTooltipVM.AddKeyType("MapClick", () => GauntletInformationView._current.GetKey("MapHotKeyCategory", "MapClick"));
				PropertyBasedTooltipVM.AddKeyType("FollowModifier", () => GauntletInformationView._current.GetKey("MapHotKeyCategory", "MapFollowModifier"));
				PropertyBasedTooltipVM.AddKeyType("ExtendModifier", () => GauntletInformationView._current.GetExtendTooltipKeyText());
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000041F0 File Offset: 0x000023F0
		protected override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (this._dataSource != null && (Input.IsKeyDown(InputKey.LeftAlt) || Input.IsKeyDown(InputKey.RightAlt) || Input.IsKeyDown(InputKey.ControllerLBumper)))
			{
				this._gamepadTooltipExtendTimer += dt;
			}
			else
			{
				this._gamepadTooltipExtendTimer = 0f;
			}
			if (this._dataSource != null)
			{
				this._dataSource.Tick(dt);
				this._dataSource.IsExtended = (Input.IsGamepadActive ? (this._gamepadTooltipExtendTimer > 0.18f) : (this._gamepadTooltipExtendTimer > 0f));
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000428C File Offset: 0x0000248C
		private string GetExtendTooltipKeyText()
		{
			if (Input.IsControllerConnected && !Input.IsMouseActive)
			{
				return this.GetKey("MapHotKeyCategory", "MapFollowModifier");
			}
			return Game.Current.GameTextManager.FindText("str_game_key_text", "anyalt").ToString();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000042DC File Offset: 0x000024DC
		private string GetKey(string categoryId, string keyId)
		{
			return Game.Current.GameTextManager.GetHotKeyGameText(categoryId, keyId).ToString();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000042F4 File Offset: 0x000024F4
		private string GetKey(string categoryId, int keyId)
		{
			return Game.Current.GameTextManager.GetHotKeyGameText(categoryId, keyId).ToString();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000430C File Offset: 0x0000250C
		private void OnShowTooltip(Type type, object[] args)
		{
			this.OnHideTooltip();
			ValueTuple<Type, object, string> valueTuple;
			if (InformationManager.RegisteredTypes.TryGetValue(type, out valueTuple))
			{
				try
				{
					this._dataSource = (Activator.CreateInstance(valueTuple.Item1, new object[]
					{
						type,
						args
					}) as TooltipBaseVM);
					this._movie = this._layerAsGauntletLayer.LoadMovie(valueTuple.Item3, this._dataSource);
					return;
				}
				catch (Exception arg)
				{
					Debug.FailedAssert(string.Format("Failed to display tooltip of type: {0}. Exception: {1}", type.FullName, arg), "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\GauntletInformationView.cs", "OnShowTooltip", 102);
					return;
				}
			}
			Debug.FailedAssert("Unable to show tooltip. Either the given type or the corresponding tooltip type is not added to TooltipMappingProvider. Given type: " + type.FullName, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\GauntletInformationView.cs", "OnShowTooltip", 107);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000043C8 File Offset: 0x000025C8
		private void OnHideTooltip()
		{
			TooltipBaseVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.OnFinalize();
			}
			if (this._movie != null)
			{
				this._layerAsGauntletLayer.ReleaseMovie(this._movie);
			}
			this._dataSource = null;
			this._movie = null;
		}

		// Token: 0x0400003E RID: 62
		private TooltipBaseVM _dataSource;

		// Token: 0x0400003F RID: 63
		private IGauntletMovie _movie;

		// Token: 0x04000040 RID: 64
		private GauntletLayer _layerAsGauntletLayer;

		// Token: 0x04000041 RID: 65
		private static GauntletInformationView _current;

		// Token: 0x04000042 RID: 66
		private const float _tooltipExtendTreshold = 0.18f;

		// Token: 0x04000043 RID: 67
		private float _gamepadTooltipExtendTimer;
	}
}

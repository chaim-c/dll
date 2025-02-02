using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.ScreenSystem;

namespace SandBox.View.CharacterCreation
{
	// Token: 0x0200005B RID: 91
	[GameStateScreen(typeof(CharacterCreationState))]
	public class CharacterCreationScreen : ScreenBase, ICharacterCreationStateHandler, IGameStateListener
	{
		// Token: 0x06000414 RID: 1044 RVA: 0x00022838 File Offset: 0x00020A38
		public CharacterCreationScreen(CharacterCreationState characterCreationState)
		{
			this._characterCreationStateState = characterCreationState;
			characterCreationState.Handler = this;
			this._stageViews = new Dictionary<Type, Type>();
			Assembly[] viewAssemblies = this.GetViewAssemblies();
			foreach (Type type in this.CollectUnorderedStages(viewAssemblies))
			{
				CharacterCreationStageViewAttribute characterCreationStageViewAttribute;
				if (typeof(CharacterCreationStageViewBase).IsAssignableFrom(type) && (characterCreationStageViewAttribute = (type.GetCustomAttributesSafe(typeof(CharacterCreationStageViewAttribute), true).FirstOrDefault<object>() as CharacterCreationStageViewAttribute)) != null)
				{
					if (this._stageViews.ContainsKey(characterCreationStageViewAttribute.StageType))
					{
						this._stageViews[characterCreationStageViewAttribute.StageType] = type;
					}
					else
					{
						this._stageViews.Add(characterCreationStageViewAttribute.StageType, type);
					}
				}
			}
			this._cultureAmbientSoundEvent = SoundEvent.CreateEventFromString("event:/mission/ambient/special/charactercreation", null);
			this._cultureAmbientSoundEvent.Play();
			this.CreateGenericScene();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00022934 File Offset: 0x00020B34
		private void CreateGenericScene()
		{
			this._genericScene = Scene.CreateNewScene(true, false, DecalAtlasGroup.All, "mono_renderscene");
			SceneInitializationData sceneInitializationData = default(SceneInitializationData);
			sceneInitializationData.InitPhysicsWorld = false;
			this._genericScene.Read("character_menu_new", ref sceneInitializationData, "");
			this._agentRendererSceneController = MBAgentRendererSceneController.CreateNewAgentRendererSceneController(this._genericScene, 32);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0002298E File Offset: 0x00020B8E
		private void StopSound()
		{
			SoundManager.SetGlobalParameter("MissionCulture", 0f);
			SoundEvent cultureAmbientSoundEvent = this._cultureAmbientSoundEvent;
			if (cultureAmbientSoundEvent != null)
			{
				cultureAmbientSoundEvent.Stop();
			}
			this._cultureAmbientSoundEvent = null;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000229B7 File Offset: 0x00020BB7
		void ICharacterCreationStateHandler.OnCharacterCreationFinalized()
		{
			LoadingWindow.EnableGlobalLoadingWindow();
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000229C0 File Offset: 0x00020BC0
		void ICharacterCreationStateHandler.OnRefresh()
		{
			if (this._shownLayers != null)
			{
				foreach (ScreenLayer layer in this._shownLayers.ToArray<ScreenLayer>())
				{
					base.RemoveLayer(layer);
				}
			}
			if (this._currentStageView != null)
			{
				this._shownLayers = this._currentStageView.GetLayers();
				if (this._shownLayers != null)
				{
					foreach (ScreenLayer layer2 in this._shownLayers.ToArray<ScreenLayer>())
					{
						base.AddLayer(layer2);
					}
				}
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00022A40 File Offset: 0x00020C40
		void ICharacterCreationStateHandler.OnStageCreated(CharacterCreationStageBase stage)
		{
			Type type;
			if (this._stageViews.TryGetValue(stage.GetType(), out type))
			{
				this._currentStageView = (Activator.CreateInstance(type, new object[]
				{
					this._characterCreationStateState.CharacterCreation,
					new ControlCharacterCreationStage(this._characterCreationStateState.NextStage),
					new TextObject("{=Rvr1bcu8}Next", null),
					new ControlCharacterCreationStage(this._characterCreationStateState.PreviousStage),
					new TextObject("{=WXAaWZVf}Previous", null),
					new ControlCharacterCreationStage(this._characterCreationStateState.Refresh),
					new ControlCharacterCreationStageReturnInt(this._characterCreationStateState.GetIndexOfCurrentStage),
					new ControlCharacterCreationStageReturnInt(this._characterCreationStateState.GetTotalStagesCount),
					new ControlCharacterCreationStageReturnInt(this._characterCreationStateState.GetFurthestIndex),
					new ControlCharacterCreationStageWithInt(this._characterCreationStateState.GoToStage)
				}) as CharacterCreationStageViewBase);
				stage.Listener = this._currentStageView;
				this._currentStageView.SetGenericScene(this._genericScene);
				return;
			}
			this._currentStageView = null;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00022B59 File Offset: 0x00020D59
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			if (LoadingWindow.IsLoadingWindowActive)
			{
				LoadingWindow.DisableGlobalLoadingWindow();
			}
			CharacterCreationStageViewBase currentStageView = this._currentStageView;
			if (currentStageView == null)
			{
				return;
			}
			currentStageView.Tick(dt);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00022B7F File Offset: 0x00020D7F
		void IGameStateListener.OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00022B87 File Offset: 0x00020D87
		void IGameStateListener.OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00022B8F File Offset: 0x00020D8F
		void IGameStateListener.OnInitialize()
		{
			base.OnInitialize();
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00022B97 File Offset: 0x00020D97
		void IGameStateListener.OnFinalize()
		{
			base.OnFinalize();
			this.StopSound();
			MBAgentRendererSceneController.DestructAgentRendererSceneController(this._genericScene, this._agentRendererSceneController, false);
			this._agentRendererSceneController = null;
			this._genericScene.ClearAll();
			this._genericScene = null;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00022BD0 File Offset: 0x00020DD0
		private IEnumerable<Type> CollectUnorderedStages(Assembly[] assemblies)
		{
			Assembly[] array = assemblies;
			for (int i = 0; i < array.Length; i++)
			{
				List<Type> typesSafe = array[i].GetTypesSafe(null);
				foreach (Type type in typesSafe)
				{
					if (typeof(CharacterCreationStageViewBase).IsAssignableFrom(type) && type.GetCustomAttributesSafe(typeof(CharacterCreationStageViewAttribute), true).FirstOrDefault<object>() is CharacterCreationStageViewAttribute)
					{
						yield return type;
					}
				}
				List<Type>.Enumerator enumerator = default(List<Type>.Enumerator);
			}
			array = null;
			yield break;
			yield break;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00022BE0 File Offset: 0x00020DE0
		private Assembly[] GetViewAssemblies()
		{
			List<Assembly> list = new List<Assembly>();
			Assembly assembly = typeof(CharacterCreationStageViewAttribute).Assembly;
			foreach (Assembly assembly2 in AppDomain.CurrentDomain.GetAssemblies())
			{
				AssemblyName[] referencedAssemblies = assembly2.GetReferencedAssemblies();
				for (int j = 0; j < referencedAssemblies.Length; j++)
				{
					if (referencedAssemblies[j].ToString() == assembly.GetName().ToString())
					{
						list.Add(assembly2);
						break;
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x0400023E RID: 574
		private const string CultureParameterId = "MissionCulture";

		// Token: 0x0400023F RID: 575
		private readonly CharacterCreationState _characterCreationStateState;

		// Token: 0x04000240 RID: 576
		private IEnumerable<ScreenLayer> _shownLayers;

		// Token: 0x04000241 RID: 577
		private CharacterCreationStageViewBase _currentStageView;

		// Token: 0x04000242 RID: 578
		private readonly Dictionary<Type, Type> _stageViews;

		// Token: 0x04000243 RID: 579
		private SoundEvent _cultureAmbientSoundEvent;

		// Token: 0x04000244 RID: 580
		private Scene _genericScene;

		// Token: 0x04000245 RID: 581
		private MBAgentRendererSceneController _agentRendererSceneController;
	}
}

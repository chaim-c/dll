﻿using System;
using System.Collections.Generic;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.GauntletUI.PrefabSystem;
using TaleWorlds.MountAndBlade.ViewModelCollection;
using TaleWorlds.MountAndBlade.ViewModelCollection.EscapeMenu;
using TaleWorlds.MountAndBlade.ViewModelCollection.FaceGenerator;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD;
using TaleWorlds.MountAndBlade.ViewModelCollection.InitialMenu;
using TaleWorlds.MountAndBlade.ViewModelCollection.Inquiries;
using TaleWorlds.MountAndBlade.ViewModelCollection.Order;

namespace TaleWorlds.MountAndBlade.GauntletUI.AutoGenerated1
{
	// Token: 0x0200003E RID: 62
	public class GeneratedUIPrefabCreator : IGeneratedUIPrefabCreator
	{
		// Token: 0x06000D38 RID: 3384 RVA: 0x000628CC File Offset: 0x00060ACC
		public GeneratedPrefabInstantiationResult CreateFaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM(UIContext context, Dictionary<string, object> data)
		{
			FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM = new FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM(context);
			faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM.CreateWidgets();
			faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM.SetIds();
			faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM.SetAttributes();
			GeneratedPrefabInstantiationResult generatedPrefabInstantiationResult = new GeneratedPrefabInstantiationResult(faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM);
			GeneratedGauntletMovie data2 = new GeneratedGauntletMovie("FaceGen", faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM);
			object obj = data["DataSource"];
			faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM.SetDataSource((FaceGenVM)obj);
			generatedPrefabInstantiationResult.AddData("Movie", data2);
			return generatedPrefabInstantiationResult;
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00062928 File Offset: 0x00060B28
		public GeneratedPrefabInstantiationResult CreateOrderBar__TaleWorlds_MountAndBlade_ViewModelCollection_Order_MissionOrderVM(UIContext context, Dictionary<string, object> data)
		{
			OrderBar__TaleWorlds_MountAndBlade_ViewModelCollection_Order_MissionOrderVM orderBar__TaleWorlds_MountAndBlade_ViewModelCollection_Order_MissionOrderVM = new OrderBar__TaleWorlds_MountAndBlade_ViewModelCollection_Order_MissionOrderVM(context);
			orderBar__TaleWorlds_MountAndBlade_ViewModelCollection_Order_MissionOrderVM.CreateWidgets();
			orderBar__TaleWorlds_MountAndBlade_ViewModelCollection_Order_MissionOrderVM.SetIds();
			orderBar__TaleWorlds_MountAndBlade_ViewModelCollection_Order_MissionOrderVM.SetAttributes();
			GeneratedPrefabInstantiationResult generatedPrefabInstantiationResult = new GeneratedPrefabInstantiationResult(orderBar__TaleWorlds_MountAndBlade_ViewModelCollection_Order_MissionOrderVM);
			GeneratedGauntletMovie data2 = new GeneratedGauntletMovie("OrderBar", orderBar__TaleWorlds_MountAndBlade_ViewModelCollection_Order_MissionOrderVM);
			object obj = data["DataSource"];
			orderBar__TaleWorlds_MountAndBlade_ViewModelCollection_Order_MissionOrderVM.SetDataSource((MissionOrderVM)obj);
			generatedPrefabInstantiationResult.AddData("Movie", data2);
			return generatedPrefabInstantiationResult;
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00062984 File Offset: 0x00060B84
		public GeneratedPrefabInstantiationResult CreateInitialScreen__TaleWorlds_MountAndBlade_ViewModelCollection_InitialMenu_InitialMenuVM(UIContext context, Dictionary<string, object> data)
		{
			InitialScreen__TaleWorlds_MountAndBlade_ViewModelCollection_InitialMenu_InitialMenuVM initialScreen__TaleWorlds_MountAndBlade_ViewModelCollection_InitialMenu_InitialMenuVM = new InitialScreen__TaleWorlds_MountAndBlade_ViewModelCollection_InitialMenu_InitialMenuVM(context);
			initialScreen__TaleWorlds_MountAndBlade_ViewModelCollection_InitialMenu_InitialMenuVM.CreateWidgets();
			initialScreen__TaleWorlds_MountAndBlade_ViewModelCollection_InitialMenu_InitialMenuVM.SetIds();
			initialScreen__TaleWorlds_MountAndBlade_ViewModelCollection_InitialMenu_InitialMenuVM.SetAttributes();
			GeneratedPrefabInstantiationResult generatedPrefabInstantiationResult = new GeneratedPrefabInstantiationResult(initialScreen__TaleWorlds_MountAndBlade_ViewModelCollection_InitialMenu_InitialMenuVM);
			GeneratedGauntletMovie data2 = new GeneratedGauntletMovie("InitialScreen", initialScreen__TaleWorlds_MountAndBlade_ViewModelCollection_InitialMenu_InitialMenuVM);
			object obj = data["DataSource"];
			initialScreen__TaleWorlds_MountAndBlade_ViewModelCollection_InitialMenu_InitialMenuVM.SetDataSource((InitialMenuVM)obj);
			generatedPrefabInstantiationResult.AddData("Movie", data2);
			return generatedPrefabInstantiationResult;
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x000629E0 File Offset: 0x00060BE0
		public GeneratedPrefabInstantiationResult CreateEscapeMenu__TaleWorlds_MountAndBlade_ViewModelCollection_EscapeMenu_EscapeMenuVM(UIContext context, Dictionary<string, object> data)
		{
			EscapeMenu__TaleWorlds_MountAndBlade_ViewModelCollection_EscapeMenu_EscapeMenuVM escapeMenu__TaleWorlds_MountAndBlade_ViewModelCollection_EscapeMenu_EscapeMenuVM = new EscapeMenu__TaleWorlds_MountAndBlade_ViewModelCollection_EscapeMenu_EscapeMenuVM(context);
			escapeMenu__TaleWorlds_MountAndBlade_ViewModelCollection_EscapeMenu_EscapeMenuVM.CreateWidgets();
			escapeMenu__TaleWorlds_MountAndBlade_ViewModelCollection_EscapeMenu_EscapeMenuVM.SetIds();
			escapeMenu__TaleWorlds_MountAndBlade_ViewModelCollection_EscapeMenu_EscapeMenuVM.SetAttributes();
			GeneratedPrefabInstantiationResult generatedPrefabInstantiationResult = new GeneratedPrefabInstantiationResult(escapeMenu__TaleWorlds_MountAndBlade_ViewModelCollection_EscapeMenu_EscapeMenuVM);
			GeneratedGauntletMovie data2 = new GeneratedGauntletMovie("EscapeMenu", escapeMenu__TaleWorlds_MountAndBlade_ViewModelCollection_EscapeMenu_EscapeMenuVM);
			object obj = data["DataSource"];
			escapeMenu__TaleWorlds_MountAndBlade_ViewModelCollection_EscapeMenu_EscapeMenuVM.SetDataSource((EscapeMenuVM)obj);
			generatedPrefabInstantiationResult.AddData("Movie", data2);
			return generatedPrefabInstantiationResult;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00062A3C File Offset: 0x00060C3C
		public GeneratedPrefabInstantiationResult CreateMainAgentHUD__TaleWorlds_MountAndBlade_ViewModelCollection_MissionAgentStatusVM(UIContext context, Dictionary<string, object> data)
		{
			MainAgentHUD__TaleWorlds_MountAndBlade_ViewModelCollection_MissionAgentStatusVM mainAgentHUD__TaleWorlds_MountAndBlade_ViewModelCollection_MissionAgentStatusVM = new MainAgentHUD__TaleWorlds_MountAndBlade_ViewModelCollection_MissionAgentStatusVM(context);
			mainAgentHUD__TaleWorlds_MountAndBlade_ViewModelCollection_MissionAgentStatusVM.CreateWidgets();
			mainAgentHUD__TaleWorlds_MountAndBlade_ViewModelCollection_MissionAgentStatusVM.SetIds();
			mainAgentHUD__TaleWorlds_MountAndBlade_ViewModelCollection_MissionAgentStatusVM.SetAttributes();
			GeneratedPrefabInstantiationResult generatedPrefabInstantiationResult = new GeneratedPrefabInstantiationResult(mainAgentHUD__TaleWorlds_MountAndBlade_ViewModelCollection_MissionAgentStatusVM);
			GeneratedGauntletMovie data2 = new GeneratedGauntletMovie("MainAgentHUD", mainAgentHUD__TaleWorlds_MountAndBlade_ViewModelCollection_MissionAgentStatusVM);
			object obj = data["DataSource"];
			mainAgentHUD__TaleWorlds_MountAndBlade_ViewModelCollection_MissionAgentStatusVM.SetDataSource((MissionAgentStatusVM)obj);
			generatedPrefabInstantiationResult.AddData("Movie", data2);
			return generatedPrefabInstantiationResult;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00062A98 File Offset: 0x00060C98
		public GeneratedPrefabInstantiationResult CreateGameNotificationUI__TaleWorlds_Core_ViewModelCollection_Information_GameNotificationVM(UIContext context, Dictionary<string, object> data)
		{
			GameNotificationUI__TaleWorlds_Core_ViewModelCollection_Information_GameNotificationVM gameNotificationUI__TaleWorlds_Core_ViewModelCollection_Information_GameNotificationVM = new GameNotificationUI__TaleWorlds_Core_ViewModelCollection_Information_GameNotificationVM(context);
			gameNotificationUI__TaleWorlds_Core_ViewModelCollection_Information_GameNotificationVM.CreateWidgets();
			gameNotificationUI__TaleWorlds_Core_ViewModelCollection_Information_GameNotificationVM.SetIds();
			gameNotificationUI__TaleWorlds_Core_ViewModelCollection_Information_GameNotificationVM.SetAttributes();
			GeneratedPrefabInstantiationResult generatedPrefabInstantiationResult = new GeneratedPrefabInstantiationResult(gameNotificationUI__TaleWorlds_Core_ViewModelCollection_Information_GameNotificationVM);
			GeneratedGauntletMovie data2 = new GeneratedGauntletMovie("GameNotificationUI", gameNotificationUI__TaleWorlds_Core_ViewModelCollection_Information_GameNotificationVM);
			object obj = data["DataSource"];
			gameNotificationUI__TaleWorlds_Core_ViewModelCollection_Information_GameNotificationVM.SetDataSource((GameNotificationVM)obj);
			generatedPrefabInstantiationResult.AddData("Movie", data2);
			return generatedPrefabInstantiationResult;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00062AF4 File Offset: 0x00060CF4
		public GeneratedPrefabInstantiationResult CreateHintTooltip__TaleWorlds_Core_ViewModelCollection_Information_HintVM(UIContext context, Dictionary<string, object> data)
		{
			HintTooltip__TaleWorlds_Core_ViewModelCollection_Information_HintVM hintTooltip__TaleWorlds_Core_ViewModelCollection_Information_HintVM = new HintTooltip__TaleWorlds_Core_ViewModelCollection_Information_HintVM(context);
			hintTooltip__TaleWorlds_Core_ViewModelCollection_Information_HintVM.CreateWidgets();
			hintTooltip__TaleWorlds_Core_ViewModelCollection_Information_HintVM.SetIds();
			hintTooltip__TaleWorlds_Core_ViewModelCollection_Information_HintVM.SetAttributes();
			GeneratedPrefabInstantiationResult generatedPrefabInstantiationResult = new GeneratedPrefabInstantiationResult(hintTooltip__TaleWorlds_Core_ViewModelCollection_Information_HintVM);
			GeneratedGauntletMovie data2 = new GeneratedGauntletMovie("HintTooltip", hintTooltip__TaleWorlds_Core_ViewModelCollection_Information_HintVM);
			object obj = data["DataSource"];
			hintTooltip__TaleWorlds_Core_ViewModelCollection_Information_HintVM.SetDataSource((HintVM)obj);
			generatedPrefabInstantiationResult.AddData("Movie", data2);
			return generatedPrefabInstantiationResult;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00062B50 File Offset: 0x00060D50
		public GeneratedPrefabInstantiationResult CreateSingleQueryPopup__TaleWorlds_MountAndBlade_ViewModelCollection_Inquiries_SingleQueryPopUpVM(UIContext context, Dictionary<string, object> data)
		{
			SingleQueryPopup__TaleWorlds_MountAndBlade_ViewModelCollection_Inquiries_SingleQueryPopUpVM singleQueryPopup__TaleWorlds_MountAndBlade_ViewModelCollection_Inquiries_SingleQueryPopUpVM = new SingleQueryPopup__TaleWorlds_MountAndBlade_ViewModelCollection_Inquiries_SingleQueryPopUpVM(context);
			singleQueryPopup__TaleWorlds_MountAndBlade_ViewModelCollection_Inquiries_SingleQueryPopUpVM.CreateWidgets();
			singleQueryPopup__TaleWorlds_MountAndBlade_ViewModelCollection_Inquiries_SingleQueryPopUpVM.SetIds();
			singleQueryPopup__TaleWorlds_MountAndBlade_ViewModelCollection_Inquiries_SingleQueryPopUpVM.SetAttributes();
			GeneratedPrefabInstantiationResult generatedPrefabInstantiationResult = new GeneratedPrefabInstantiationResult(singleQueryPopup__TaleWorlds_MountAndBlade_ViewModelCollection_Inquiries_SingleQueryPopUpVM);
			GeneratedGauntletMovie data2 = new GeneratedGauntletMovie("SingleQueryPopup", singleQueryPopup__TaleWorlds_MountAndBlade_ViewModelCollection_Inquiries_SingleQueryPopUpVM);
			object obj = data["DataSource"];
			singleQueryPopup__TaleWorlds_MountAndBlade_ViewModelCollection_Inquiries_SingleQueryPopUpVM.SetDataSource((SingleQueryPopUpVM)obj);
			generatedPrefabInstantiationResult.AddData("Movie", data2);
			return generatedPrefabInstantiationResult;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00062BAC File Offset: 0x00060DAC
		public GeneratedPrefabInstantiationResult CreateCrosshair__TaleWorlds_MountAndBlade_ViewModelCollection_HUD_CrosshairVM(UIContext context, Dictionary<string, object> data)
		{
			Crosshair__TaleWorlds_MountAndBlade_ViewModelCollection_HUD_CrosshairVM crosshair__TaleWorlds_MountAndBlade_ViewModelCollection_HUD_CrosshairVM = new Crosshair__TaleWorlds_MountAndBlade_ViewModelCollection_HUD_CrosshairVM(context);
			crosshair__TaleWorlds_MountAndBlade_ViewModelCollection_HUD_CrosshairVM.CreateWidgets();
			crosshair__TaleWorlds_MountAndBlade_ViewModelCollection_HUD_CrosshairVM.SetIds();
			crosshair__TaleWorlds_MountAndBlade_ViewModelCollection_HUD_CrosshairVM.SetAttributes();
			GeneratedPrefabInstantiationResult generatedPrefabInstantiationResult = new GeneratedPrefabInstantiationResult(crosshair__TaleWorlds_MountAndBlade_ViewModelCollection_HUD_CrosshairVM);
			GeneratedGauntletMovie data2 = new GeneratedGauntletMovie("Crosshair", crosshair__TaleWorlds_MountAndBlade_ViewModelCollection_HUD_CrosshairVM);
			object obj = data["DataSource"];
			crosshair__TaleWorlds_MountAndBlade_ViewModelCollection_HUD_CrosshairVM.SetDataSource((CrosshairVM)obj);
			generatedPrefabInstantiationResult.AddData("Movie", data2);
			return generatedPrefabInstantiationResult;
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00062C08 File Offset: 0x00060E08
		public void CollectGeneratedPrefabDefinitions(GeneratedPrefabContext generatedPrefabContext)
		{
			generatedPrefabContext.AddGeneratedPrefab("FaceGen", "TaleWorlds.MountAndBlade.ViewModelCollection.FaceGenerator.FaceGenVM", new CreateGeneratedWidget(this.CreateFaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM));
			generatedPrefabContext.AddGeneratedPrefab("OrderBar", "TaleWorlds.MountAndBlade.ViewModelCollection.Order.MissionOrderVM", new CreateGeneratedWidget(this.CreateOrderBar__TaleWorlds_MountAndBlade_ViewModelCollection_Order_MissionOrderVM));
			generatedPrefabContext.AddGeneratedPrefab("InitialScreen", "TaleWorlds.MountAndBlade.ViewModelCollection.InitialMenu.InitialMenuVM", new CreateGeneratedWidget(this.CreateInitialScreen__TaleWorlds_MountAndBlade_ViewModelCollection_InitialMenu_InitialMenuVM));
			generatedPrefabContext.AddGeneratedPrefab("EscapeMenu", "TaleWorlds.MountAndBlade.ViewModelCollection.EscapeMenu.EscapeMenuVM", new CreateGeneratedWidget(this.CreateEscapeMenu__TaleWorlds_MountAndBlade_ViewModelCollection_EscapeMenu_EscapeMenuVM));
			generatedPrefabContext.AddGeneratedPrefab("MainAgentHUD", "TaleWorlds.MountAndBlade.ViewModelCollection.MissionAgentStatusVM", new CreateGeneratedWidget(this.CreateMainAgentHUD__TaleWorlds_MountAndBlade_ViewModelCollection_MissionAgentStatusVM));
			generatedPrefabContext.AddGeneratedPrefab("GameNotificationUI", "TaleWorlds.Core.ViewModelCollection.Information.GameNotificationVM", new CreateGeneratedWidget(this.CreateGameNotificationUI__TaleWorlds_Core_ViewModelCollection_Information_GameNotificationVM));
			generatedPrefabContext.AddGeneratedPrefab("HintTooltip", "TaleWorlds.Core.ViewModelCollection.Information.HintVM", new CreateGeneratedWidget(this.CreateHintTooltip__TaleWorlds_Core_ViewModelCollection_Information_HintVM));
			generatedPrefabContext.AddGeneratedPrefab("SingleQueryPopup", "TaleWorlds.MountAndBlade.ViewModelCollection.Inquiries.SingleQueryPopUpVM", new CreateGeneratedWidget(this.CreateSingleQueryPopup__TaleWorlds_MountAndBlade_ViewModelCollection_Inquiries_SingleQueryPopUpVM));
			generatedPrefabContext.AddGeneratedPrefab("Crosshair", "TaleWorlds.MountAndBlade.ViewModelCollection.HUD.CrosshairVM", new CreateGeneratedWidget(this.CreateCrosshair__TaleWorlds_MountAndBlade_ViewModelCollection_HUD_CrosshairVM));
		}
	}
}

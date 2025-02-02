using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000223 RID: 547
	public enum GameKeyDefinition
	{
		// Token: 0x04000A3E RID: 2622
		Up,
		// Token: 0x04000A3F RID: 2623
		Down,
		// Token: 0x04000A40 RID: 2624
		Left,
		// Token: 0x04000A41 RID: 2625
		Right,
		// Token: 0x04000A42 RID: 2626
		Leave,
		// Token: 0x04000A43 RID: 2627
		ShowIndicators,
		// Token: 0x04000A44 RID: 2628
		InitiateAllChat,
		// Token: 0x04000A45 RID: 2629
		InitiateTeamChat,
		// Token: 0x04000A46 RID: 2630
		FinalizeChat,
		// Token: 0x04000A47 RID: 2631
		Attack,
		// Token: 0x04000A48 RID: 2632
		Defend,
		// Token: 0x04000A49 RID: 2633
		EquipPrimaryWeapon,
		// Token: 0x04000A4A RID: 2634
		EquipSecondaryWeapon,
		// Token: 0x04000A4B RID: 2635
		Action,
		// Token: 0x04000A4C RID: 2636
		Jump,
		// Token: 0x04000A4D RID: 2637
		Crouch,
		// Token: 0x04000A4E RID: 2638
		Kick,
		// Token: 0x04000A4F RID: 2639
		ToggleWeaponMode,
		// Token: 0x04000A50 RID: 2640
		EquipWeapon1,
		// Token: 0x04000A51 RID: 2641
		EquipWeapon2,
		// Token: 0x04000A52 RID: 2642
		EquipWeapon3,
		// Token: 0x04000A53 RID: 2643
		EquipWeapon4,
		// Token: 0x04000A54 RID: 2644
		DropWeapon,
		// Token: 0x04000A55 RID: 2645
		SheathWeapon,
		// Token: 0x04000A56 RID: 2646
		Zoom,
		// Token: 0x04000A57 RID: 2647
		ViewCharacter,
		// Token: 0x04000A58 RID: 2648
		LockTarget,
		// Token: 0x04000A59 RID: 2649
		CameraToggle,
		// Token: 0x04000A5A RID: 2650
		MissionScreenHotkeyCameraZoomIn,
		// Token: 0x04000A5B RID: 2651
		MissionScreenHotkeyCameraZoomOut,
		// Token: 0x04000A5C RID: 2652
		ToggleWalkMode,
		// Token: 0x04000A5D RID: 2653
		Cheer,
		// Token: 0x04000A5E RID: 2654
		Taunt,
		// Token: 0x04000A5F RID: 2655
		PushToTalk,
		// Token: 0x04000A60 RID: 2656
		EquipmentSwitch,
		// Token: 0x04000A61 RID: 2657
		ShowMouse,
		// Token: 0x04000A62 RID: 2658
		BannerWindow,
		// Token: 0x04000A63 RID: 2659
		CharacterWindow,
		// Token: 0x04000A64 RID: 2660
		InventoryWindow,
		// Token: 0x04000A65 RID: 2661
		EncyclopediaWindow,
		// Token: 0x04000A66 RID: 2662
		KingdomWindow,
		// Token: 0x04000A67 RID: 2663
		ClanWindow,
		// Token: 0x04000A68 RID: 2664
		QuestsWindow,
		// Token: 0x04000A69 RID: 2665
		PartyWindow,
		// Token: 0x04000A6A RID: 2666
		FacegenWindow,
		// Token: 0x04000A6B RID: 2667
		MapMoveUp,
		// Token: 0x04000A6C RID: 2668
		MapMoveDown,
		// Token: 0x04000A6D RID: 2669
		MapMoveRight,
		// Token: 0x04000A6E RID: 2670
		MapMoveLeft,
		// Token: 0x04000A6F RID: 2671
		PartyMoveUp,
		// Token: 0x04000A70 RID: 2672
		PartyMoveDown,
		// Token: 0x04000A71 RID: 2673
		PartyMoveRight,
		// Token: 0x04000A72 RID: 2674
		PartyMoveLeft,
		// Token: 0x04000A73 RID: 2675
		QuickSave,
		// Token: 0x04000A74 RID: 2676
		MapFastMove,
		// Token: 0x04000A75 RID: 2677
		MapZoomIn,
		// Token: 0x04000A76 RID: 2678
		MapZoomOut,
		// Token: 0x04000A77 RID: 2679
		MapRotateLeft,
		// Token: 0x04000A78 RID: 2680
		MapRotateRight,
		// Token: 0x04000A79 RID: 2681
		MapTimeStop,
		// Token: 0x04000A7A RID: 2682
		MapTimeNormal,
		// Token: 0x04000A7B RID: 2683
		MapTimeFastForward,
		// Token: 0x04000A7C RID: 2684
		MapTimeTogglePause,
		// Token: 0x04000A7D RID: 2685
		MapCameraFollowMode,
		// Token: 0x04000A7E RID: 2686
		MapToggleFastForward,
		// Token: 0x04000A7F RID: 2687
		MapTrackSettlement,
		// Token: 0x04000A80 RID: 2688
		MapGoToEncylopedia,
		// Token: 0x04000A81 RID: 2689
		ViewOrders,
		// Token: 0x04000A82 RID: 2690
		SelectOrder1,
		// Token: 0x04000A83 RID: 2691
		SelectOrder2,
		// Token: 0x04000A84 RID: 2692
		SelectOrder3,
		// Token: 0x04000A85 RID: 2693
		SelectOrder4,
		// Token: 0x04000A86 RID: 2694
		SelectOrder5,
		// Token: 0x04000A87 RID: 2695
		SelectOrder6,
		// Token: 0x04000A88 RID: 2696
		SelectOrder7,
		// Token: 0x04000A89 RID: 2697
		SelectOrder8,
		// Token: 0x04000A8A RID: 2698
		SelectOrderReturn,
		// Token: 0x04000A8B RID: 2699
		EveryoneHear,
		// Token: 0x04000A8C RID: 2700
		Group0Hear,
		// Token: 0x04000A8D RID: 2701
		Group1Hear,
		// Token: 0x04000A8E RID: 2702
		Group2Hear,
		// Token: 0x04000A8F RID: 2703
		Group3Hear,
		// Token: 0x04000A90 RID: 2704
		Group4Hear,
		// Token: 0x04000A91 RID: 2705
		Group5Hear,
		// Token: 0x04000A92 RID: 2706
		Group6Hear,
		// Token: 0x04000A93 RID: 2707
		Group7Hear,
		// Token: 0x04000A94 RID: 2708
		HoldOrder,
		// Token: 0x04000A95 RID: 2709
		SelectNextGroup,
		// Token: 0x04000A96 RID: 2710
		SelectPreviousGroup,
		// Token: 0x04000A97 RID: 2711
		ToggleGroupSelection,
		// Token: 0x04000A98 RID: 2712
		HideUI,
		// Token: 0x04000A99 RID: 2713
		CameraRollLeft,
		// Token: 0x04000A9A RID: 2714
		CameraRollRight,
		// Token: 0x04000A9B RID: 2715
		TakePicture,
		// Token: 0x04000A9C RID: 2716
		TakePictureWithAdditionalPasses,
		// Token: 0x04000A9D RID: 2717
		ToggleCameraFollowMode,
		// Token: 0x04000A9E RID: 2718
		ToggleMouse,
		// Token: 0x04000A9F RID: 2719
		ToggleVignette,
		// Token: 0x04000AA0 RID: 2720
		ToggleCharacters,
		// Token: 0x04000AA1 RID: 2721
		IncreaseFocus,
		// Token: 0x04000AA2 RID: 2722
		DecreaseFocus,
		// Token: 0x04000AA3 RID: 2723
		IncreaseFocusStart,
		// Token: 0x04000AA4 RID: 2724
		DecreaseFocusStart,
		// Token: 0x04000AA5 RID: 2725
		IncreaseFocusEnd,
		// Token: 0x04000AA6 RID: 2726
		DecreaseFocusEnd,
		// Token: 0x04000AA7 RID: 2727
		Reset,
		// Token: 0x04000AA8 RID: 2728
		AcceptPoll,
		// Token: 0x04000AA9 RID: 2729
		DeclinePoll,
		// Token: 0x04000AAA RID: 2730
		TotalGameKeyCount
	}
}

using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000199 RID: 409
	[EngineStruct("Anim_flags", false)]
	[Flags]
	public enum AnimFlags : ulong
	{
		// Token: 0x04000705 RID: 1797
		amf_priority_continue = 1UL,
		// Token: 0x04000706 RID: 1798
		amf_priority_jump = 2UL,
		// Token: 0x04000707 RID: 1799
		amf_priority_ride = 2UL,
		// Token: 0x04000708 RID: 1800
		amf_priority_crouch = 2UL,
		// Token: 0x04000709 RID: 1801
		amf_priority_attack = 10UL,
		// Token: 0x0400070A RID: 1802
		amf_priority_cancel = 12UL,
		// Token: 0x0400070B RID: 1803
		amf_priority_defend = 14UL,
		// Token: 0x0400070C RID: 1804
		amf_priority_defend_parry = 15UL,
		// Token: 0x0400070D RID: 1805
		amf_priority_throw = 15UL,
		// Token: 0x0400070E RID: 1806
		amf_priority_blocked = 15UL,
		// Token: 0x0400070F RID: 1807
		amf_priority_parried = 15UL,
		// Token: 0x04000710 RID: 1808
		amf_priority_kick = 33UL,
		// Token: 0x04000711 RID: 1809
		amf_priority_jump_end = 33UL,
		// Token: 0x04000712 RID: 1810
		amf_priority_reload = 60UL,
		// Token: 0x04000713 RID: 1811
		amf_priority_mount = 64UL,
		// Token: 0x04000714 RID: 1812
		amf_priority_equip = 70UL,
		// Token: 0x04000715 RID: 1813
		amf_priority_rear = 74UL,
		// Token: 0x04000716 RID: 1814
		amf_priority_upperbody_while_kick = 75UL,
		// Token: 0x04000717 RID: 1815
		amf_priority_striked = 80UL,
		// Token: 0x04000718 RID: 1816
		amf_priority_fall_from_horse = 81UL,
		// Token: 0x04000719 RID: 1817
		amf_priority_die = 95UL,
		// Token: 0x0400071A RID: 1818
		amf_priority_mask = 255UL,
		// Token: 0x0400071B RID: 1819
		anf_disable_agent_agent_collisions = 256UL,
		// Token: 0x0400071C RID: 1820
		anf_ignore_all_collisions = 512UL,
		// Token: 0x0400071D RID: 1821
		anf_ignore_static_body_collisions = 1024UL,
		// Token: 0x0400071E RID: 1822
		anf_use_last_step_point_as_data = 2048UL,
		// Token: 0x0400071F RID: 1823
		anf_make_bodyfall_sound = 4096UL,
		// Token: 0x04000720 RID: 1824
		anf_client_prediction = 8192UL,
		// Token: 0x04000721 RID: 1825
		anf_keep = 16384UL,
		// Token: 0x04000722 RID: 1826
		anf_restart = 32768UL,
		// Token: 0x04000723 RID: 1827
		anf_client_owner_prediction = 65536UL,
		// Token: 0x04000724 RID: 1828
		anf_make_walk_sound = 131072UL,
		// Token: 0x04000725 RID: 1829
		anf_disable_hand_ik = 262144UL,
		// Token: 0x04000726 RID: 1830
		anf_stick_item_to_left_hand = 524288UL,
		// Token: 0x04000727 RID: 1831
		anf_blends_according_to_look_slope = 1048576UL,
		// Token: 0x04000728 RID: 1832
		anf_synch_with_horse = 2097152UL,
		// Token: 0x04000729 RID: 1833
		anf_use_left_hand_during_attack = 4194304UL,
		// Token: 0x0400072A RID: 1834
		anf_lock_camera = 8388608UL,
		// Token: 0x0400072B RID: 1835
		anf_lock_movement = 16777216UL,
		// Token: 0x0400072C RID: 1836
		anf_synch_with_movement = 33554432UL,
		// Token: 0x0400072D RID: 1837
		anf_enable_hand_spring_ik = 67108864UL,
		// Token: 0x0400072E RID: 1838
		anf_enable_hand_blend_ik = 134217728UL,
		// Token: 0x0400072F RID: 1839
		anf_synch_with_ladder_movement = 268435456UL,
		// Token: 0x04000730 RID: 1840
		anf_do_not_keep_track_of_sound = 536870912UL,
		// Token: 0x04000731 RID: 1841
		anf_reset_camera_height = 1073741824UL,
		// Token: 0x04000732 RID: 1842
		anf_disable_alternative_randomization = 2147483648UL,
		// Token: 0x04000733 RID: 1843
		anf_disable_auto_increment_progress = 4294967296UL,
		// Token: 0x04000734 RID: 1844
		anf_switch_item_between_hands = 8589934592UL,
		// Token: 0x04000735 RID: 1845
		anf_enforce_lowerbody = 68719476736UL,
		// Token: 0x04000736 RID: 1846
		anf_enforce_all = 137438953472UL,
		// Token: 0x04000737 RID: 1847
		anf_cyclic = 274877906944UL,
		// Token: 0x04000738 RID: 1848
		anf_enforce_root_rotation = 549755813888UL,
		// Token: 0x04000739 RID: 1849
		anf_allow_head_movement = 1099511627776UL,
		// Token: 0x0400073A RID: 1850
		anf_disable_foot_ik = 2199023255552UL,
		// Token: 0x0400073B RID: 1851
		anf_affected_by_movement = 4398046511104UL,
		// Token: 0x0400073C RID: 1852
		anf_update_bounding_volume = 8796093022208UL,
		// Token: 0x0400073D RID: 1853
		anf_align_with_ground = 17592186044416UL,
		// Token: 0x0400073E RID: 1854
		anf_ignore_slope = 35184372088832UL,
		// Token: 0x0400073F RID: 1855
		anf_displace_position = 70368744177664UL,
		// Token: 0x04000740 RID: 1856
		anf_enable_left_hand_ik = 140737488355328UL,
		// Token: 0x04000741 RID: 1857
		anf_ignore_scale_on_root_position = 281474976710656UL,
		// Token: 0x04000742 RID: 1858
		anf_blend_main_item_bone_entitially = 562949953421312UL,
		// Token: 0x04000743 RID: 1859
		anf_animation_layer_flags_mask = 4503530907893760UL,
		// Token: 0x04000744 RID: 1860
		anf_animation_layer_flags_bits = 36UL,
		// Token: 0x04000745 RID: 1861
		anf_randomization_weight_1 = 1152921504606846976UL,
		// Token: 0x04000746 RID: 1862
		anf_randomization_weight_2 = 2305843009213693952UL,
		// Token: 0x04000747 RID: 1863
		anf_randomization_weight_4 = 4611686018427387904UL,
		// Token: 0x04000748 RID: 1864
		anf_randomization_weight_8 = 9223372036854775808UL,
		// Token: 0x04000749 RID: 1865
		anf_randomization_weight_mask = 17293822569102704640UL
	}
}

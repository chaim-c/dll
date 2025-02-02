using System;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI;
using TaleWorlds.MountAndBlade.View.Tableaus;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.TextureProviders
{
	// Token: 0x02000018 RID: 24
	public class CharacterTableauTextureProvider : TextureProvider
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00006BE9 File Offset: 0x00004DE9
		public float CustomAnimationProgressRatio
		{
			get
			{
				return this._characterTableau.GetCustomAnimationProgressRatio();
			}
		}

		// Token: 0x1700001D RID: 29
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00006BF6 File Offset: 0x00004DF6
		public string BannerCodeText
		{
			set
			{
				this._characterTableau.SetBannerCode(value);
			}
		}

		// Token: 0x1700001E RID: 30
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00006C04 File Offset: 0x00004E04
		public string BodyProperties
		{
			set
			{
				this._characterTableau.SetBodyProperties(value);
			}
		}

		// Token: 0x1700001F RID: 31
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00006C12 File Offset: 0x00004E12
		public int StanceIndex
		{
			set
			{
				this._characterTableau.SetStanceIndex(value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00006C20 File Offset: 0x00004E20
		public bool IsFemale
		{
			set
			{
				this._characterTableau.SetIsFemale(value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00006C2E File Offset: 0x00004E2E
		public int Race
		{
			set
			{
				this._characterTableau.SetRace(value);
			}
		}

		// Token: 0x17000022 RID: 34
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00006C3C File Offset: 0x00004E3C
		public bool IsBannerShownInBackground
		{
			set
			{
				this._characterTableau.SetIsBannerShownInBackground(value);
			}
		}

		// Token: 0x17000023 RID: 35
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00006C4A File Offset: 0x00004E4A
		public bool IsEquipmentAnimActive
		{
			set
			{
				this._characterTableau.SetIsEquipmentAnimActive(value);
			}
		}

		// Token: 0x17000024 RID: 36
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00006C58 File Offset: 0x00004E58
		public string EquipmentCode
		{
			set
			{
				this._characterTableau.SetEquipmentCode(value);
			}
		}

		// Token: 0x17000025 RID: 37
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00006C66 File Offset: 0x00004E66
		public string IdleAction
		{
			set
			{
				this._characterTableau.SetIdleAction(value);
			}
		}

		// Token: 0x17000026 RID: 38
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00006C74 File Offset: 0x00004E74
		public string IdleFaceAnim
		{
			set
			{
				this._characterTableau.SetIdleFaceAnim(value);
			}
		}

		// Token: 0x17000027 RID: 39
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00006C82 File Offset: 0x00004E82
		public bool CurrentlyRotating
		{
			set
			{
				this._characterTableau.RotateCharacter(value);
			}
		}

		// Token: 0x17000028 RID: 40
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00006C90 File Offset: 0x00004E90
		public string MountCreationKey
		{
			set
			{
				this._characterTableau.SetMountCreationKey(value);
			}
		}

		// Token: 0x17000029 RID: 41
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00006C9E File Offset: 0x00004E9E
		public uint ArmorColor1
		{
			set
			{
				this._characterTableau.SetArmorColor1(value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00006CAC File Offset: 0x00004EAC
		public uint ArmorColor2
		{
			set
			{
				this._characterTableau.SetArmorColor2(value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00006CBA File Offset: 0x00004EBA
		public string CharStringId
		{
			set
			{
				this._characterTableau.SetCharStringID(value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00006CC8 File Offset: 0x00004EC8
		public bool TriggerCharacterMountPlacesSwap
		{
			set
			{
				this._characterTableau.TriggerCharacterMountPlacesSwap();
			}
		}

		// Token: 0x1700002D RID: 45
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00006CD5 File Offset: 0x00004ED5
		public float CustomRenderScale
		{
			set
			{
				this._characterTableau.SetCustomRenderScale(value);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00006CE3 File Offset: 0x00004EE3
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00006CF6 File Offset: 0x00004EF6
		public bool IsPlayingCustomAnimations
		{
			get
			{
				CharacterTableau characterTableau = this._characterTableau;
				return characterTableau != null && characterTableau.IsRunningCustomAnimation;
			}
			set
			{
				if (value)
				{
					this._characterTableau.StartCustomAnimation();
					return;
				}
				this._characterTableau.StopCustomAnimation();
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00006D12 File Offset: 0x00004F12
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00006D1F File Offset: 0x00004F1F
		public bool ShouldLoopCustomAnimation
		{
			get
			{
				return this._characterTableau.ShouldLoopCustomAnimation;
			}
			set
			{
				this._characterTableau.ShouldLoopCustomAnimation = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00006D2D File Offset: 0x00004F2D
		public int LeftHandWieldedEquipmentIndex
		{
			set
			{
				this._characterTableau.SetLeftHandWieldedEquipmentIndex(value);
			}
		}

		// Token: 0x17000031 RID: 49
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00006D3B File Offset: 0x00004F3B
		public int RightHandWieldedEquipmentIndex
		{
			set
			{
				this._characterTableau.SetRightHandWieldedEquipmentIndex(value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00006D49 File Offset: 0x00004F49
		public float CustomAnimationWaitDuration
		{
			set
			{
				this._characterTableau.CustomAnimationWaitDuration = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00006D57 File Offset: 0x00004F57
		public string CustomAnimation
		{
			set
			{
				this._characterTableau.SetCustomAnimation(value);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00006D77 File Offset: 0x00004F77
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00006D65 File Offset: 0x00004F65
		public bool IsHidden
		{
			get
			{
				return this._isHidden;
			}
			set
			{
				if (this._isHidden != value)
				{
					this._isHidden = value;
				}
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006D7F File Offset: 0x00004F7F
		public CharacterTableauTextureProvider()
		{
			this._characterTableau = new CharacterTableau();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006D92 File Offset: 0x00004F92
		public override void Clear(bool clearNextFrame)
		{
			this._characterTableau.OnFinalize();
			base.Clear(clearNextFrame);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006DA8 File Offset: 0x00004FA8
		private void CheckTexture()
		{
			if (this._texture != this._characterTableau.Texture)
			{
				this._texture = this._characterTableau.Texture;
				if (this._texture != null)
				{
					EngineTexture platformTexture = new EngineTexture(this._texture);
					this._providedTexture = new TaleWorlds.TwoDimension.Texture(platformTexture);
					return;
				}
				this._providedTexture = null;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006E0C File Offset: 0x0000500C
		public override TaleWorlds.TwoDimension.Texture GetTexture(TwoDimensionContext twoDimensionContext, string name)
		{
			this.CheckTexture();
			return this._providedTexture;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00006E1A File Offset: 0x0000501A
		public override void SetTargetSize(int width, int height)
		{
			base.SetTargetSize(width, height);
			this._characterTableau.SetTargetSize(width, height);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006E31 File Offset: 0x00005031
		public override void Tick(float dt)
		{
			base.Tick(dt);
			this.CheckTexture();
			this._characterTableau.OnTick(dt);
		}

		// Token: 0x0400008C RID: 140
		private CharacterTableau _characterTableau;

		// Token: 0x0400008D RID: 141
		private TaleWorlds.Engine.Texture _texture;

		// Token: 0x0400008E RID: 142
		private TaleWorlds.TwoDimension.Texture _providedTexture;

		// Token: 0x0400008F RID: 143
		private bool _isHidden;
	}
}

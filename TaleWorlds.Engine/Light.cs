using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000052 RID: 82
	[EngineClass("rglLight")]
	public sealed class Light : GameEntityComponent
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0000523F File Offset: 0x0000343F
		public bool IsValid
		{
			get
			{
				return base.Pointer != UIntPtr.Zero;
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00005251 File Offset: 0x00003451
		internal Light(UIntPtr pointer) : base(pointer)
		{
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0000525A File Offset: 0x0000345A
		public static Light CreatePointLight(float lightRadius)
		{
			return EngineApplicationInterface.ILight.CreatePointLight(lightRadius);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00005268 File Offset: 0x00003468
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x00005288 File Offset: 0x00003488
		public MatrixFrame Frame
		{
			get
			{
				MatrixFrame result;
				EngineApplicationInterface.ILight.GetFrame(base.Pointer, out result);
				return result;
			}
			set
			{
				EngineApplicationInterface.ILight.SetFrame(base.Pointer, ref value);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0000529C File Offset: 0x0000349C
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x000052AE File Offset: 0x000034AE
		public Vec3 LightColor
		{
			get
			{
				return EngineApplicationInterface.ILight.GetLightColor(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.ILight.SetLightColor(base.Pointer, value);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x000052C1 File Offset: 0x000034C1
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x000052D3 File Offset: 0x000034D3
		public float Intensity
		{
			get
			{
				return EngineApplicationInterface.ILight.GetIntensity(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.ILight.SetIntensity(base.Pointer, value);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x000052E6 File Offset: 0x000034E6
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x000052F8 File Offset: 0x000034F8
		public float Radius
		{
			get
			{
				return EngineApplicationInterface.ILight.GetRadius(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.ILight.SetRadius(base.Pointer, value);
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0000530B File Offset: 0x0000350B
		public void SetShadowType(Light.ShadowType type)
		{
			EngineApplicationInterface.ILight.SetShadows(base.Pointer, (int)type);
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0000531E File Offset: 0x0000351E
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x00005330 File Offset: 0x00003530
		public bool ShadowEnabled
		{
			get
			{
				return EngineApplicationInterface.ILight.IsShadowEnabled(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.ILight.EnableShadow(base.Pointer, value);
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00005343 File Offset: 0x00003543
		public void SetLightFlicker(float magnitude, float interval)
		{
			EngineApplicationInterface.ILight.SetLightFlicker(base.Pointer, magnitude, interval);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00005357 File Offset: 0x00003557
		public void SetVolumetricProperties(bool volumetricLightEnabled, float volumeParameters)
		{
			EngineApplicationInterface.ILight.SetVolumetricProperties(base.Pointer, volumetricLightEnabled, volumeParameters);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0000536B File Offset: 0x0000356B
		public void Dispose()
		{
			if (this.IsValid)
			{
				this.Release();
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00005381 File Offset: 0x00003581
		public void SetVisibility(bool value)
		{
			EngineApplicationInterface.ILight.SetVisibility(base.Pointer, value);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00005394 File Offset: 0x00003594
		private void Release()
		{
			EngineApplicationInterface.ILight.Release(base.Pointer);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x000053A8 File Offset: 0x000035A8
		~Light()
		{
			this.Dispose();
		}

		// Token: 0x020000B9 RID: 185
		public enum ShadowType
		{
			// Token: 0x040003BE RID: 958
			NoShadow,
			// Token: 0x040003BF RID: 959
			StaticShadow,
			// Token: 0x040003C0 RID: 960
			DynamicShadow,
			// Token: 0x040003C1 RID: 961
			Count
		}
	}
}

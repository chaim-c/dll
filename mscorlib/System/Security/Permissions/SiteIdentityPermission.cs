﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x02000308 RID: 776
	[ComVisible(true)]
	[Serializable]
	public sealed class SiteIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x06002747 RID: 10055 RVA: 0x0008E4A0 File Offset: 0x0008C6A0
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_serializedPermission != null)
			{
				this.FromXml(SecurityElement.FromString(this.m_serializedPermission));
				this.m_serializedPermission = null;
				return;
			}
			if (this.m_site != null)
			{
				this.m_unrestricted = false;
				this.m_sites = new SiteString[1];
				this.m_sites[0] = this.m_site;
				this.m_site = null;
			}
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x0008E500 File Offset: 0x0008C700
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermission = this.ToXml().ToString();
				if (this.m_sites != null && this.m_sites.Length == 1)
				{
					this.m_site = this.m_sites[0];
				}
			}
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x0008E54E File Offset: 0x0008C74E
		[OnSerialized]
		private void OnSerialized(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermission = null;
				this.m_site = null;
			}
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x0008E56D File Offset: 0x0008C76D
		public SiteIdentityPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.m_unrestricted = true;
				return;
			}
			if (state == PermissionState.None)
			{
				this.m_unrestricted = false;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x0008E59B File Offset: 0x0008C79B
		public SiteIdentityPermission(string site)
		{
			this.Site = site;
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x0600274D RID: 10061 RVA: 0x0008E5CD File Offset: 0x0008C7CD
		// (set) Token: 0x0600274C RID: 10060 RVA: 0x0008E5AA File Offset: 0x0008C7AA
		public string Site
		{
			get
			{
				if (this.m_sites == null)
				{
					return "";
				}
				if (this.m_sites.Length == 1)
				{
					return this.m_sites[0].ToString();
				}
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
			}
			set
			{
				this.m_unrestricted = false;
				this.m_sites = new SiteString[1];
				this.m_sites[0] = new SiteString(value);
			}
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x0008E608 File Offset: 0x0008C808
		public override IPermission Copy()
		{
			SiteIdentityPermission siteIdentityPermission = new SiteIdentityPermission(PermissionState.None);
			siteIdentityPermission.m_unrestricted = this.m_unrestricted;
			if (this.m_sites != null)
			{
				siteIdentityPermission.m_sites = new SiteString[this.m_sites.Length];
				for (int i = 0; i < this.m_sites.Length; i++)
				{
					siteIdentityPermission.m_sites[i] = this.m_sites[i].Copy();
				}
			}
			return siteIdentityPermission;
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x0008E66C File Offset: 0x0008C86C
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.m_unrestricted && (this.m_sites == null || this.m_sites.Length == 0);
			}
			SiteIdentityPermission siteIdentityPermission = target as SiteIdentityPermission;
			if (siteIdentityPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			if (siteIdentityPermission.m_unrestricted)
			{
				return true;
			}
			if (this.m_unrestricted)
			{
				return false;
			}
			if (this.m_sites != null)
			{
				foreach (SiteString siteString in this.m_sites)
				{
					bool flag = false;
					if (siteIdentityPermission.m_sites != null)
					{
						foreach (SiteString operand in siteIdentityPermission.m_sites)
						{
							if (siteString.IsSubsetOf(operand))
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x0008E744 File Offset: 0x0008C944
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SiteIdentityPermission siteIdentityPermission = target as SiteIdentityPermission;
			if (siteIdentityPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			if (this.m_unrestricted && siteIdentityPermission.m_unrestricted)
			{
				return new SiteIdentityPermission(PermissionState.None)
				{
					m_unrestricted = true
				};
			}
			if (this.m_unrestricted)
			{
				return siteIdentityPermission.Copy();
			}
			if (siteIdentityPermission.m_unrestricted)
			{
				return this.Copy();
			}
			if (this.m_sites == null || siteIdentityPermission.m_sites == null || this.m_sites.Length == 0 || siteIdentityPermission.m_sites.Length == 0)
			{
				return null;
			}
			List<SiteString> list = new List<SiteString>();
			foreach (SiteString siteString in this.m_sites)
			{
				foreach (SiteString operand in siteIdentityPermission.m_sites)
				{
					SiteString siteString2 = siteString.Intersect(operand);
					if (siteString2 != null)
					{
						list.Add(siteString2);
					}
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return new SiteIdentityPermission(PermissionState.None)
			{
				m_sites = list.ToArray()
			};
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x0008E868 File Offset: 0x0008CA68
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				if ((this.m_sites == null || this.m_sites.Length == 0) && !this.m_unrestricted)
				{
					return null;
				}
				return this.Copy();
			}
			else
			{
				SiteIdentityPermission siteIdentityPermission = target as SiteIdentityPermission;
				if (siteIdentityPermission == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
					{
						base.GetType().FullName
					}));
				}
				if (this.m_unrestricted || siteIdentityPermission.m_unrestricted)
				{
					return new SiteIdentityPermission(PermissionState.None)
					{
						m_unrestricted = true
					};
				}
				if (this.m_sites == null || this.m_sites.Length == 0)
				{
					if (siteIdentityPermission.m_sites == null || siteIdentityPermission.m_sites.Length == 0)
					{
						return null;
					}
					return siteIdentityPermission.Copy();
				}
				else
				{
					if (siteIdentityPermission.m_sites == null || siteIdentityPermission.m_sites.Length == 0)
					{
						return this.Copy();
					}
					List<SiteString> list = new List<SiteString>();
					foreach (SiteString item in this.m_sites)
					{
						list.Add(item);
					}
					foreach (SiteString siteString in siteIdentityPermission.m_sites)
					{
						bool flag = false;
						foreach (SiteString obj in list)
						{
							if (siteString.Equals(obj))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							list.Add(siteString);
						}
					}
					return new SiteIdentityPermission(PermissionState.None)
					{
						m_sites = list.ToArray()
					};
				}
			}
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x0008E9EC File Offset: 0x0008CBEC
		public override void FromXml(SecurityElement esd)
		{
			this.m_unrestricted = false;
			this.m_sites = null;
			CodeAccessPermission.ValidateElement(esd, this);
			string text = esd.Attribute("Unrestricted");
			if (text != null && string.Compare(text, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_unrestricted = true;
				return;
			}
			string text2 = esd.Attribute("Site");
			List<SiteString> list = new List<SiteString>();
			if (text2 != null)
			{
				list.Add(new SiteString(text2));
			}
			ArrayList children = esd.Children;
			if (children != null)
			{
				foreach (object obj in children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					text2 = securityElement.Attribute("Site");
					if (text2 != null)
					{
						list.Add(new SiteString(text2));
					}
				}
			}
			if (list.Count != 0)
			{
				this.m_sites = list.ToArray();
			}
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x0008EAD8 File Offset: 0x0008CCD8
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.SiteIdentityPermission");
			if (this.m_unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else if (this.m_sites != null)
			{
				if (this.m_sites.Length == 1)
				{
					securityElement.AddAttribute("Site", this.m_sites[0].ToString());
				}
				else
				{
					for (int i = 0; i < this.m_sites.Length; i++)
					{
						SecurityElement securityElement2 = new SecurityElement("Site");
						securityElement2.AddAttribute("Site", this.m_sites[i].ToString());
						securityElement.AddChild(securityElement2);
					}
				}
			}
			return securityElement;
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x0008EB76 File Offset: 0x0008CD76
		int IBuiltInPermission.GetTokenIndex()
		{
			return SiteIdentityPermission.GetTokenIndex();
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x0008EB7D File Offset: 0x0008CD7D
		internal static int GetTokenIndex()
		{
			return 11;
		}

		// Token: 0x04000F41 RID: 3905
		[OptionalField(VersionAdded = 2)]
		private bool m_unrestricted;

		// Token: 0x04000F42 RID: 3906
		[OptionalField(VersionAdded = 2)]
		private SiteString[] m_sites;

		// Token: 0x04000F43 RID: 3907
		[OptionalField(VersionAdded = 2)]
		private string m_serializedPermission;

		// Token: 0x04000F44 RID: 3908
		private SiteString m_site;
	}
}

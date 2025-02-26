﻿using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Util;
using System.Text;
using System.Threading;

namespace System.Security.Policy
{
	// Token: 0x02000365 RID: 869
	[ComVisible(true)]
	[Serializable]
	public sealed class PolicyLevel
	{
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06002ADA RID: 10970 RVA: 0x0009EAD4 File Offset: 0x0009CCD4
		private static object InternalSyncObject
		{
			get
			{
				if (PolicyLevel.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref PolicyLevel.s_InternalSyncObject, value, null);
				}
				return PolicyLevel.s_InternalSyncObject;
			}
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x0009EB00 File Offset: 0x0009CD00
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_label != null)
			{
				this.DeriveTypeFromLabel();
			}
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x0009EB10 File Offset: 0x0009CD10
		private void DeriveTypeFromLabel()
		{
			if (this.m_label.Equals(Environment.GetResourceString("Policy_PL_User")))
			{
				this.m_type = PolicyLevelType.User;
				return;
			}
			if (this.m_label.Equals(Environment.GetResourceString("Policy_PL_Machine")))
			{
				this.m_type = PolicyLevelType.Machine;
				return;
			}
			if (this.m_label.Equals(Environment.GetResourceString("Policy_PL_Enterprise")))
			{
				this.m_type = PolicyLevelType.Enterprise;
				return;
			}
			if (this.m_label.Equals(Environment.GetResourceString("Policy_PL_AppDomain")))
			{
				this.m_type = PolicyLevelType.AppDomain;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Policy_Default"));
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x0009EBA8 File Offset: 0x0009CDA8
		private string DeriveLabelFromType()
		{
			switch (this.m_type)
			{
			case PolicyLevelType.User:
				return Environment.GetResourceString("Policy_PL_User");
			case PolicyLevelType.Machine:
				return Environment.GetResourceString("Policy_PL_Machine");
			case PolicyLevelType.Enterprise:
				return Environment.GetResourceString("Policy_PL_Enterprise");
			case PolicyLevelType.AppDomain:
				return Environment.GetResourceString("Policy_PL_AppDomain");
			default:
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
				{
					(int)this.m_type
				}));
			}
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x0009EC23 File Offset: 0x0009CE23
		private PolicyLevel()
		{
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x0009EC2B File Offset: 0x0009CE2B
		[SecurityCritical]
		internal PolicyLevel(PolicyLevelType type) : this(type, PolicyLevel.GetLocationFromType(type))
		{
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x0009EC3A File Offset: 0x0009CE3A
		internal PolicyLevel(PolicyLevelType type, string path) : this(type, path, ConfigId.None)
		{
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x0009EC48 File Offset: 0x0009CE48
		internal PolicyLevel(PolicyLevelType type, string path, ConfigId configId)
		{
			this.m_type = type;
			this.m_path = path;
			this.m_loaded = (path == null);
			if (this.m_path == null)
			{
				this.m_rootCodeGroup = this.CreateDefaultAllGroup();
				this.SetFactoryPermissionSets();
				this.SetDefaultFullTrustAssemblies();
			}
			this.m_configId = configId;
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x0009EC9C File Offset: 0x0009CE9C
		[SecurityCritical]
		internal static string GetLocationFromType(PolicyLevelType type)
		{
			switch (type)
			{
			case PolicyLevelType.User:
				return Config.UserDirectory + "security.config";
			case PolicyLevelType.Machine:
				return Config.MachineDirectory + "security.config";
			case PolicyLevelType.Enterprise:
				return Config.MachineDirectory + "enterprisesec.config";
			default:
				return null;
			}
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x0009ECEE File Offset: 0x0009CEEE
		[SecuritySafeCritical]
		[Obsolete("AppDomain policy levels are obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		public static PolicyLevel CreateAppDomainLevel()
		{
			return new PolicyLevel(PolicyLevelType.AppDomain);
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06002AE4 RID: 10980 RVA: 0x0009ECF6 File Offset: 0x0009CEF6
		public string Label
		{
			get
			{
				if (this.m_label == null)
				{
					this.m_label = this.DeriveLabelFromType();
				}
				return this.m_label;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06002AE5 RID: 10981 RVA: 0x0009ED12 File Offset: 0x0009CF12
		[ComVisible(false)]
		public PolicyLevelType Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06002AE6 RID: 10982 RVA: 0x0009ED1A File Offset: 0x0009CF1A
		internal ConfigId ConfigId
		{
			get
			{
				return this.m_configId;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06002AE7 RID: 10983 RVA: 0x0009ED22 File Offset: 0x0009CF22
		internal string Path
		{
			get
			{
				return this.m_path;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06002AE8 RID: 10984 RVA: 0x0009ED2A File Offset: 0x0009CF2A
		public string StoreLocation
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
			get
			{
				return PolicyLevel.GetLocationFromType(this.m_type);
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06002AE9 RID: 10985 RVA: 0x0009ED37 File Offset: 0x0009CF37
		// (set) Token: 0x06002AEA RID: 10986 RVA: 0x0009ED45 File Offset: 0x0009CF45
		public CodeGroup RootCodeGroup
		{
			[SecuritySafeCritical]
			get
			{
				this.CheckLoaded();
				return this.m_rootCodeGroup;
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("RootCodeGroup");
				}
				this.CheckLoaded();
				this.m_rootCodeGroup = value.Copy();
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06002AEB RID: 10987 RVA: 0x0009ED68 File Offset: 0x0009CF68
		public IList NamedPermissionSets
		{
			[SecuritySafeCritical]
			get
			{
				this.CheckLoaded();
				this.LoadAllPermissionSets();
				ArrayList arrayList = new ArrayList(this.m_namedPermissionSets.Count);
				foreach (object obj in this.m_namedPermissionSets)
				{
					arrayList.Add(((NamedPermissionSet)obj).Copy());
				}
				return arrayList;
			}
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x0009EDC0 File Offset: 0x0009CFC0
		public CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			return this.RootCodeGroup.ResolveMatchingCodeGroups(evidence);
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x0009EDDC File Offset: 0x0009CFDC
		[Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
		public void AddFullTrustAssembly(StrongName sn)
		{
			if (sn == null)
			{
				throw new ArgumentNullException("sn");
			}
			this.AddFullTrustAssembly(new StrongNameMembershipCondition(sn.PublicKey, sn.Name, sn.Version));
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x0009EE0C File Offset: 0x0009D00C
		[SecuritySafeCritical]
		[Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
		public void AddFullTrustAssembly(StrongNameMembershipCondition snMC)
		{
			if (snMC == null)
			{
				throw new ArgumentNullException("snMC");
			}
			this.CheckLoaded();
			IEnumerator enumerator = this.m_fullTrustAssemblies.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (((StrongNameMembershipCondition)enumerator.Current).Equals(snMC))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_AssemblyAlreadyFullTrust"));
				}
			}
			ArrayList fullTrustAssemblies = this.m_fullTrustAssemblies;
			lock (fullTrustAssemblies)
			{
				this.m_fullTrustAssemblies.Add(snMC);
			}
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x0009EEA0 File Offset: 0x0009D0A0
		[Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
		public void RemoveFullTrustAssembly(StrongName sn)
		{
			if (sn == null)
			{
				throw new ArgumentNullException("assembly");
			}
			this.RemoveFullTrustAssembly(new StrongNameMembershipCondition(sn.PublicKey, sn.Name, sn.Version));
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x0009EED0 File Offset: 0x0009D0D0
		[SecuritySafeCritical]
		[Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
		public void RemoveFullTrustAssembly(StrongNameMembershipCondition snMC)
		{
			if (snMC == null)
			{
				throw new ArgumentNullException("snMC");
			}
			this.CheckLoaded();
			object obj = null;
			IEnumerator enumerator = this.m_fullTrustAssemblies.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (((StrongNameMembershipCondition)enumerator.Current).Equals(snMC))
				{
					obj = enumerator.Current;
					break;
				}
			}
			if (obj == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_AssemblyNotFullTrust"));
			}
			ArrayList fullTrustAssemblies = this.m_fullTrustAssemblies;
			lock (fullTrustAssemblies)
			{
				this.m_fullTrustAssemblies.Remove(obj);
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06002AF1 RID: 10993 RVA: 0x0009EF74 File Offset: 0x0009D174
		[Obsolete("Because all GAC assemblies always get full trust, the full trust list is no longer meaningful. You should install any assemblies that are used in security policy in the GAC to ensure they are trusted.")]
		public IList FullTrustAssemblies
		{
			[SecuritySafeCritical]
			get
			{
				this.CheckLoaded();
				return new ArrayList(this.m_fullTrustAssemblies);
			}
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x0009EF88 File Offset: 0x0009D188
		[SecuritySafeCritical]
		public void AddNamedPermissionSet(NamedPermissionSet permSet)
		{
			if (permSet == null)
			{
				throw new ArgumentNullException("permSet");
			}
			this.CheckLoaded();
			this.LoadAllPermissionSets();
			lock (this)
			{
				IEnumerator enumerator = this.m_namedPermissionSets.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (((NamedPermissionSet)enumerator.Current).Name.Equals(permSet.Name))
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_DuplicateName"));
					}
				}
				NamedPermissionSet namedPermissionSet = (NamedPermissionSet)permSet.Copy();
				namedPermissionSet.IgnoreTypeLoadFailures = true;
				this.m_namedPermissionSets.Add(namedPermissionSet);
			}
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x0009F03C File Offset: 0x0009D23C
		public NamedPermissionSet RemoveNamedPermissionSet(NamedPermissionSet permSet)
		{
			if (permSet == null)
			{
				throw new ArgumentNullException("permSet");
			}
			return this.RemoveNamedPermissionSet(permSet.Name);
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x0009F058 File Offset: 0x0009D258
		[SecuritySafeCritical]
		public NamedPermissionSet RemoveNamedPermissionSet(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.CheckLoaded();
			this.LoadAllPermissionSets();
			int num = -1;
			for (int i = 0; i < PolicyLevel.s_reservedNamedPermissionSets.Length; i++)
			{
				if (PolicyLevel.s_reservedNamedPermissionSets[i].Equals(name))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_ReservedNPMS", new object[]
					{
						name
					}));
				}
			}
			ArrayList namedPermissionSets = this.m_namedPermissionSets;
			for (int j = 0; j < namedPermissionSets.Count; j++)
			{
				if (((NamedPermissionSet)namedPermissionSets[j]).Name.Equals(name))
				{
					num = j;
					break;
				}
			}
			if (num == -1)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NoNPMS"));
			}
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.m_rootCodeGroup);
			for (int k = 0; k < arrayList.Count; k++)
			{
				CodeGroup codeGroup = (CodeGroup)arrayList[k];
				if (codeGroup.PermissionSetName != null && codeGroup.PermissionSetName.Equals(name))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_NPMSInUse", new object[]
					{
						name
					}));
				}
				IEnumerator enumerator = codeGroup.Children.GetEnumerator();
				if (enumerator != null)
				{
					while (enumerator.MoveNext())
					{
						object value = enumerator.Current;
						arrayList.Add(value);
					}
				}
			}
			NamedPermissionSet result = (NamedPermissionSet)namedPermissionSets[num];
			namedPermissionSets.RemoveAt(num);
			return result;
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x0009F1BC File Offset: 0x0009D3BC
		[SecuritySafeCritical]
		public NamedPermissionSet ChangeNamedPermissionSet(string name, PermissionSet pSet)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (pSet == null)
			{
				throw new ArgumentNullException("pSet");
			}
			for (int i = 0; i < PolicyLevel.s_reservedNamedPermissionSets.Length; i++)
			{
				if (PolicyLevel.s_reservedNamedPermissionSets[i].Equals(name))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_ReservedNPMS", new object[]
					{
						name
					}));
				}
			}
			NamedPermissionSet namedPermissionSetInternal = this.GetNamedPermissionSetInternal(name);
			if (namedPermissionSetInternal == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NoNPMS"));
			}
			NamedPermissionSet result = (NamedPermissionSet)namedPermissionSetInternal.Copy();
			namedPermissionSetInternal.Reset();
			namedPermissionSetInternal.SetUnrestricted(pSet.IsUnrestricted());
			foreach (object obj in pSet)
			{
				namedPermissionSetInternal.SetPermission(((IPermission)obj).Copy());
			}
			if (pSet is NamedPermissionSet)
			{
				namedPermissionSetInternal.Description = ((NamedPermissionSet)pSet).Description;
			}
			return result;
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x0009F29C File Offset: 0x0009D49C
		[SecuritySafeCritical]
		public NamedPermissionSet GetNamedPermissionSet(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			NamedPermissionSet namedPermissionSetInternal = this.GetNamedPermissionSetInternal(name);
			if (namedPermissionSetInternal != null)
			{
				return new NamedPermissionSet(namedPermissionSetInternal);
			}
			return null;
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x0009F2CC File Offset: 0x0009D4CC
		[SecuritySafeCritical]
		public void Recover()
		{
			if (this.m_configId == ConfigId.None)
			{
				throw new PolicyException(Environment.GetResourceString("Policy_RecoverNotFileBased"));
			}
			lock (this)
			{
				if (!Config.RecoverData(this.m_configId))
				{
					throw new PolicyException(Environment.GetResourceString("Policy_RecoverNoConfigFile"));
				}
				this.m_loaded = false;
				this.m_rootCodeGroup = null;
				this.m_namedPermissionSets = null;
				this.m_fullTrustAssemblies = new ArrayList();
			}
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x0009F358 File Offset: 0x0009D558
		[SecuritySafeCritical]
		public void Reset()
		{
			this.SetDefault();
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x0009F360 File Offset: 0x0009D560
		[SecuritySafeCritical]
		public PolicyStatement Resolve(Evidence evidence)
		{
			return this.Resolve(evidence, 0, null);
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x0009F36C File Offset: 0x0009D56C
		[SecuritySafeCritical]
		public SecurityElement ToXml()
		{
			this.CheckLoaded();
			this.LoadAllPermissionSets();
			SecurityElement securityElement = new SecurityElement("PolicyLevel");
			securityElement.AddAttribute("version", "1");
			Hashtable hashtable = new Hashtable();
			lock (this)
			{
				SecurityElement securityElement2 = new SecurityElement("NamedPermissionSets");
				foreach (object obj in this.m_namedPermissionSets)
				{
					securityElement2.AddChild(this.NormalizeClassDeep(((NamedPermissionSet)obj).ToXml(), hashtable));
				}
				SecurityElement child = this.NormalizeClassDeep(this.m_rootCodeGroup.ToXml(this), hashtable);
				SecurityElement securityElement3 = new SecurityElement("FullTrustAssemblies");
				foreach (object obj2 in this.m_fullTrustAssemblies)
				{
					securityElement3.AddChild(this.NormalizeClassDeep(((StrongNameMembershipCondition)obj2).ToXml(), hashtable));
				}
				SecurityElement securityElement4 = new SecurityElement("SecurityClasses");
				IDictionaryEnumerator enumerator2 = hashtable.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					SecurityElement securityElement5 = new SecurityElement("SecurityClass");
					securityElement5.AddAttribute("Name", (string)enumerator2.Value);
					securityElement5.AddAttribute("Description", (string)enumerator2.Key);
					securityElement4.AddChild(securityElement5);
				}
				securityElement.AddChild(securityElement4);
				securityElement.AddChild(securityElement2);
				securityElement.AddChild(child);
				securityElement.AddChild(securityElement3);
			}
			return securityElement;
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x0009F4F8 File Offset: 0x0009D6F8
		public void FromXml(SecurityElement e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			lock (this)
			{
				ArrayList arrayList = new ArrayList();
				SecurityElement securityElement = e.SearchForChildByTag("SecurityClasses");
				Hashtable hashtable;
				if (securityElement != null)
				{
					hashtable = new Hashtable();
					foreach (object obj in securityElement.Children)
					{
						SecurityElement securityElement2 = (SecurityElement)obj;
						if (securityElement2.Tag.Equals("SecurityClass"))
						{
							string text = securityElement2.Attribute("Name");
							string text2 = securityElement2.Attribute("Description");
							if (text != null && text2 != null)
							{
								hashtable.Add(text, text2);
							}
						}
					}
				}
				else
				{
					hashtable = null;
				}
				SecurityElement securityElement3 = e.SearchForChildByTag("FullTrustAssemblies");
				if (securityElement3 != null && securityElement3.InternalChildren != null)
				{
					string assemblyQualifiedName = typeof(StrongNameMembershipCondition).AssemblyQualifiedName;
					IEnumerator enumerator2 = securityElement3.Children.GetEnumerator();
					while (enumerator2.MoveNext())
					{
						StrongNameMembershipCondition strongNameMembershipCondition = new StrongNameMembershipCondition();
						strongNameMembershipCondition.FromXml((SecurityElement)enumerator2.Current);
						arrayList.Add(strongNameMembershipCondition);
					}
				}
				this.m_fullTrustAssemblies = arrayList;
				ArrayList arrayList2 = new ArrayList();
				SecurityElement securityElement4 = e.SearchForChildByTag("NamedPermissionSets");
				SecurityElement securityElement5 = null;
				if (securityElement4 != null && securityElement4.InternalChildren != null)
				{
					securityElement5 = this.UnnormalizeClassDeep(securityElement4, hashtable);
					foreach (string name in PolicyLevel.s_reservedNamedPermissionSets)
					{
						this.FindElement(securityElement5, name);
					}
				}
				if (securityElement5 == null)
				{
					securityElement5 = new SecurityElement("NamedPermissionSets");
				}
				arrayList2.Add(BuiltInPermissionSets.FullTrust);
				arrayList2.Add(BuiltInPermissionSets.Everything);
				arrayList2.Add(BuiltInPermissionSets.SkipVerification);
				arrayList2.Add(BuiltInPermissionSets.Execution);
				arrayList2.Add(BuiltInPermissionSets.Nothing);
				arrayList2.Add(BuiltInPermissionSets.Internet);
				arrayList2.Add(BuiltInPermissionSets.LocalIntranet);
				foreach (object obj2 in arrayList2)
				{
					PermissionSet permissionSet = (PermissionSet)obj2;
					permissionSet.IgnoreTypeLoadFailures = true;
				}
				this.m_namedPermissionSets = arrayList2;
				this.m_permSetElement = securityElement5;
				SecurityElement securityElement6 = e.SearchForChildByTag("CodeGroup");
				if (securityElement6 == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXMLElement", new object[]
					{
						"CodeGroup",
						base.GetType().FullName
					}));
				}
				CodeGroup codeGroup = XMLUtil.CreateCodeGroup(this.UnnormalizeClassDeep(securityElement6, hashtable));
				if (codeGroup == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXMLElement", new object[]
					{
						"CodeGroup",
						base.GetType().FullName
					}));
				}
				codeGroup.FromXml(securityElement6, this);
				this.m_rootCodeGroup = codeGroup;
			}
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x0009F7FC File Offset: 0x0009D9FC
		[SecurityCritical]
		internal static PermissionSet GetBuiltInSet(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			if (name.Equals("FullTrust"))
			{
				return BuiltInPermissionSets.FullTrust;
			}
			if (name.Equals("Nothing"))
			{
				return BuiltInPermissionSets.Nothing;
			}
			if (name.Equals("Execution"))
			{
				return BuiltInPermissionSets.Execution;
			}
			if (name.Equals("SkipVerification"))
			{
				return BuiltInPermissionSets.SkipVerification;
			}
			if (name.Equals("Internet"))
			{
				return BuiltInPermissionSets.Internet;
			}
			if (name.Equals("LocalIntranet"))
			{
				return BuiltInPermissionSets.LocalIntranet;
			}
			return null;
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x0009F888 File Offset: 0x0009DA88
		[SecurityCritical]
		internal NamedPermissionSet GetNamedPermissionSetInternal(string name)
		{
			this.CheckLoaded();
			object internalSyncObject = PolicyLevel.InternalSyncObject;
			lock (internalSyncObject)
			{
				foreach (object obj in this.m_namedPermissionSets)
				{
					NamedPermissionSet namedPermissionSet = (NamedPermissionSet)obj;
					if (namedPermissionSet.Name.Equals(name))
					{
						return namedPermissionSet;
					}
				}
				if (this.m_permSetElement != null)
				{
					SecurityElement securityElement = this.FindElement(this.m_permSetElement, name);
					if (securityElement != null)
					{
						NamedPermissionSet namedPermissionSet2 = new NamedPermissionSet();
						namedPermissionSet2.Name = name;
						this.m_namedPermissionSets.Add(namedPermissionSet2);
						try
						{
							namedPermissionSet2.FromXml(securityElement, false, true);
						}
						catch
						{
							this.m_namedPermissionSets.Remove(namedPermissionSet2);
							return null;
						}
						if (namedPermissionSet2.Name != null)
						{
							return namedPermissionSet2;
						}
						this.m_namedPermissionSets.Remove(namedPermissionSet2);
					}
				}
			}
			return null;
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x0009F9A8 File Offset: 0x0009DBA8
		[SecurityCritical]
		internal PolicyStatement Resolve(Evidence evidence, int count, byte[] serializedEvidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			PolicyStatement policyStatement = null;
			if (serializedEvidence != null)
			{
				policyStatement = this.CheckCache(count, serializedEvidence);
			}
			if (policyStatement == null)
			{
				this.CheckLoaded();
				bool flag = this.m_fullTrustAssemblies != null && PolicyLevel.IsFullTrustAssembly(this.m_fullTrustAssemblies, evidence);
				bool flag2;
				if (flag)
				{
					policyStatement = new PolicyStatement(new PermissionSet(true), PolicyStatementAttribute.Nothing);
					flag2 = true;
				}
				else
				{
					ArrayList arrayList = this.GenericResolve(evidence, out flag2);
					policyStatement = new PolicyStatement();
					policyStatement.PermissionSet = null;
					foreach (object obj in arrayList)
					{
						PolicyStatement policy = ((CodeGroupStackFrame)obj).policy;
						if (policy != null)
						{
							policyStatement.GetPermissionSetNoCopy().InplaceUnion(policy.GetPermissionSetNoCopy());
							policyStatement.Attributes |= policy.Attributes;
							if (policy.HasDependentEvidence)
							{
								foreach (IDelayEvaluatedEvidence delayEvaluatedEvidence in policy.DependentEvidence)
								{
									delayEvaluatedEvidence.MarkUsed();
								}
							}
						}
					}
				}
				if (flag2)
				{
					this.Cache(count, evidence.RawSerialize(), policyStatement);
				}
			}
			return policyStatement;
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x0009FAD8 File Offset: 0x0009DCD8
		[SecurityCritical]
		private void CheckLoaded()
		{
			if (!this.m_loaded)
			{
				object internalSyncObject = PolicyLevel.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (!this.m_loaded)
					{
						this.LoadPolicyLevel();
					}
				}
			}
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x0009FB28 File Offset: 0x0009DD28
		private static byte[] ReadFile(string fileName)
		{
			byte[] result;
			using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				int num = (int)fileStream.Length;
				byte[] array = new byte[num];
				num = fileStream.Read(array, 0, num);
				fileStream.Close();
				result = array;
			}
			return result;
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x0009FB7C File Offset: 0x0009DD7C
		[SecurityCritical]
		private void LoadPolicyLevel()
		{
			Exception ex = null;
			CodeAccessPermission.Assert(true);
			if (File.InternalExists(this.m_path))
			{
				Encoding utf = Encoding.UTF8;
				SecurityElement securityElement;
				try
				{
					string @string = utf.GetString(PolicyLevel.ReadFile(this.m_path));
					securityElement = SecurityElement.FromString(@string);
				}
				catch (Exception ex2)
				{
					string text;
					if (!string.IsNullOrEmpty(ex2.Message))
					{
						text = ex2.Message;
					}
					else
					{
						text = ex2.GetType().AssemblyQualifiedName;
					}
					ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParseEx", new object[]
					{
						this.Label,
						text
					}));
					goto IL_1BD;
				}
				if (securityElement == null)
				{
					ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", new object[]
					{
						this.Label
					}));
				}
				else
				{
					SecurityElement securityElement2 = securityElement.SearchForChildByTag("mscorlib");
					if (securityElement2 == null)
					{
						ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", new object[]
						{
							this.Label
						}));
					}
					else
					{
						SecurityElement securityElement3 = securityElement2.SearchForChildByTag("security");
						if (securityElement3 == null)
						{
							ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", new object[]
							{
								this.Label
							}));
						}
						else
						{
							SecurityElement securityElement4 = securityElement3.SearchForChildByTag("policy");
							if (securityElement4 == null)
							{
								ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", new object[]
								{
									this.Label
								}));
							}
							else
							{
								SecurityElement securityElement5 = securityElement4.SearchForChildByTag("PolicyLevel");
								if (securityElement5 != null)
								{
									try
									{
										this.FromXml(securityElement5);
										goto IL_1B5;
									}
									catch (Exception)
									{
										ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", new object[]
										{
											this.Label
										}));
										goto IL_1BD;
									}
									goto IL_193;
									IL_1B5:
									this.m_loaded = true;
									return;
								}
								IL_193:
								ex = this.LoadError(Environment.GetResourceString("Error_SecurityPolicyFileParse", new object[]
								{
									this.Label
								}));
							}
						}
					}
				}
			}
			IL_1BD:
			this.SetDefault();
			this.m_loaded = true;
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x0009FD74 File Offset: 0x0009DF74
		[SecurityCritical]
		private Exception LoadError(string message)
		{
			if (this.m_type != PolicyLevelType.User && this.m_type != PolicyLevelType.Machine && this.m_type != PolicyLevelType.Enterprise)
			{
				return new ArgumentException(message);
			}
			Config.WriteToEventLog(message);
			return null;
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x0009FDA0 File Offset: 0x0009DFA0
		[SecurityCritical]
		private void Cache(int count, byte[] serializedEvidence, PolicyStatement policy)
		{
			if (this.m_configId == ConfigId.None)
			{
				return;
			}
			if (serializedEvidence == null)
			{
				return;
			}
			byte[] data = new SecurityDocument(policy.ToXml(null, true)).m_data;
			Config.AddCacheEntry(this.m_configId, count, serializedEvidence, data);
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x0009FDDC File Offset: 0x0009DFDC
		[SecurityCritical]
		private PolicyStatement CheckCache(int count, byte[] serializedEvidence)
		{
			if (this.m_configId == ConfigId.None)
			{
				return null;
			}
			if (serializedEvidence == null)
			{
				return null;
			}
			byte[] data;
			if (!Config.GetCacheEntry(this.m_configId, count, serializedEvidence, out data))
			{
				return null;
			}
			PolicyStatement policyStatement = new PolicyStatement();
			SecurityDocument doc = new SecurityDocument(data);
			policyStatement.FromXml(doc, 0, null, true);
			return policyStatement;
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x0009FE24 File Offset: 0x0009E024
		[SecurityCritical]
		private static bool IsFullTrustAssembly(ArrayList fullTrustAssemblies, Evidence evidence)
		{
			if (fullTrustAssemblies.Count == 0)
			{
				return false;
			}
			if (evidence != null)
			{
				lock (fullTrustAssemblies)
				{
					foreach (object obj in fullTrustAssemblies)
					{
						StrongNameMembershipCondition strongNameMembershipCondition = (StrongNameMembershipCondition)obj;
						if (strongNameMembershipCondition.Check(evidence))
						{
							if (Environment.GetCompatibilityFlag(CompatibilityFlag.FullTrustListAssembliesInGac))
							{
								if (new ZoneMembershipCondition().Check(evidence))
								{
									return true;
								}
							}
							else if (new GacMembershipCondition().Check(evidence))
							{
								return true;
							}
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x0009FEBC File Offset: 0x0009E0BC
		private CodeGroup CreateDefaultAllGroup()
		{
			UnionCodeGroup unionCodeGroup = new UnionCodeGroup();
			unionCodeGroup.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new AllMembershipCondition().ToXml()), this);
			unionCodeGroup.Name = Environment.GetResourceString("Policy_AllCode_Name");
			unionCodeGroup.Description = Environment.GetResourceString("Policy_AllCode_DescriptionFullTrust");
			return unionCodeGroup;
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x0009FF10 File Offset: 0x0009E110
		[SecurityCritical]
		private CodeGroup CreateDefaultMachinePolicy()
		{
			UnionCodeGroup unionCodeGroup = new UnionCodeGroup();
			unionCodeGroup.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Nothing", new AllMembershipCondition().ToXml()), this);
			unionCodeGroup.Name = Environment.GetResourceString("Policy_AllCode_Name");
			unionCodeGroup.Description = Environment.GetResourceString("Policy_AllCode_DescriptionNothing");
			UnionCodeGroup unionCodeGroup2 = new UnionCodeGroup();
			unionCodeGroup2.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new ZoneMembershipCondition(SecurityZone.MyComputer).ToXml()), this);
			unionCodeGroup2.Name = Environment.GetResourceString("Policy_MyComputer_Name");
			unionCodeGroup2.Description = Environment.GetResourceString("Policy_MyComputer_Description");
			StrongNamePublicKeyBlob blob = new StrongNamePublicKeyBlob("002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293");
			UnionCodeGroup unionCodeGroup3 = new UnionCodeGroup();
			unionCodeGroup3.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new StrongNameMembershipCondition(blob, null, null).ToXml()), this);
			unionCodeGroup3.Name = Environment.GetResourceString("Policy_Microsoft_Name");
			unionCodeGroup3.Description = Environment.GetResourceString("Policy_Microsoft_Description");
			unionCodeGroup2.AddChildInternal(unionCodeGroup3);
			blob = new StrongNamePublicKeyBlob("00000000000000000400000000000000");
			UnionCodeGroup unionCodeGroup4 = new UnionCodeGroup();
			unionCodeGroup4.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "FullTrust", new StrongNameMembershipCondition(blob, null, null).ToXml()), this);
			unionCodeGroup4.Name = Environment.GetResourceString("Policy_Ecma_Name");
			unionCodeGroup4.Description = Environment.GetResourceString("Policy_Ecma_Description");
			unionCodeGroup2.AddChildInternal(unionCodeGroup4);
			unionCodeGroup.AddChildInternal(unionCodeGroup2);
			CodeGroup codeGroup = new UnionCodeGroup();
			codeGroup.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "LocalIntranet", new ZoneMembershipCondition(SecurityZone.Intranet).ToXml()), this);
			codeGroup.Name = Environment.GetResourceString("Policy_Intranet_Name");
			codeGroup.Description = Environment.GetResourceString("Policy_Intranet_Description");
			codeGroup.AddChildInternal(new NetCodeGroup(new AllMembershipCondition())
			{
				Name = Environment.GetResourceString("Policy_IntranetNet_Name"),
				Description = Environment.GetResourceString("Policy_IntranetNet_Description")
			});
			codeGroup.AddChildInternal(new FileCodeGroup(new AllMembershipCondition(), FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery)
			{
				Name = Environment.GetResourceString("Policy_IntranetFile_Name"),
				Description = Environment.GetResourceString("Policy_IntranetFile_Description")
			});
			unionCodeGroup.AddChildInternal(codeGroup);
			CodeGroup codeGroup2 = new UnionCodeGroup();
			codeGroup2.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Internet", new ZoneMembershipCondition(SecurityZone.Internet).ToXml()), this);
			codeGroup2.Name = Environment.GetResourceString("Policy_Internet_Name");
			codeGroup2.Description = Environment.GetResourceString("Policy_Internet_Description");
			codeGroup2.AddChildInternal(new NetCodeGroup(new AllMembershipCondition())
			{
				Name = Environment.GetResourceString("Policy_InternetNet_Name"),
				Description = Environment.GetResourceString("Policy_InternetNet_Description")
			});
			unionCodeGroup.AddChildInternal(codeGroup2);
			CodeGroup codeGroup3 = new UnionCodeGroup();
			codeGroup3.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Nothing", new ZoneMembershipCondition(SecurityZone.Untrusted).ToXml()), this);
			codeGroup3.Name = Environment.GetResourceString("Policy_Untrusted_Name");
			codeGroup3.Description = Environment.GetResourceString("Policy_Untrusted_Description");
			unionCodeGroup.AddChildInternal(codeGroup3);
			CodeGroup codeGroup4 = new UnionCodeGroup();
			codeGroup4.FromXml(PolicyLevel.CreateCodeGroupElement("UnionCodeGroup", "Internet", new ZoneMembershipCondition(SecurityZone.Trusted).ToXml()), this);
			codeGroup4.Name = Environment.GetResourceString("Policy_Trusted_Name");
			codeGroup4.Description = Environment.GetResourceString("Policy_Trusted_Description");
			codeGroup4.AddChildInternal(new NetCodeGroup(new AllMembershipCondition())
			{
				Name = Environment.GetResourceString("Policy_TrustedNet_Name"),
				Description = Environment.GetResourceString("Policy_TrustedNet_Description")
			});
			unionCodeGroup.AddChildInternal(codeGroup4);
			return unionCodeGroup;
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000A0298 File Offset: 0x0009E498
		private static SecurityElement CreateCodeGroupElement(string codeGroupType, string permissionSetName, SecurityElement mshipElement)
		{
			SecurityElement securityElement = new SecurityElement("CodeGroup");
			securityElement.AddAttribute("class", "System.Security." + codeGroupType + ", mscorlib, Version={VERSION}, Culture=neutral, PublicKeyToken=b77a5c561934e089");
			securityElement.AddAttribute("version", "1");
			securityElement.AddAttribute("PermissionSetName", permissionSetName);
			securityElement.AddChild(mshipElement);
			return securityElement;
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000A02F0 File Offset: 0x0009E4F0
		private void SetDefaultFullTrustAssemblies()
		{
			this.m_fullTrustAssemblies = new ArrayList();
			StrongNamePublicKeyBlob blob = new StrongNamePublicKeyBlob("00000000000000000400000000000000");
			for (int i = 0; i < PolicyLevel.EcmaFullTrustAssemblies.Length; i++)
			{
				StrongNameMembershipCondition value = new StrongNameMembershipCondition(blob, PolicyLevel.EcmaFullTrustAssemblies[i], new Version("4.0.0.0"));
				this.m_fullTrustAssemblies.Add(value);
			}
			StrongNamePublicKeyBlob blob2 = new StrongNamePublicKeyBlob("002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293");
			for (int j = 0; j < PolicyLevel.MicrosoftFullTrustAssemblies.Length; j++)
			{
				StrongNameMembershipCondition value2 = new StrongNameMembershipCondition(blob2, PolicyLevel.MicrosoftFullTrustAssemblies[j], new Version("4.0.0.0"));
				this.m_fullTrustAssemblies.Add(value2);
			}
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x000A0394 File Offset: 0x0009E594
		[SecurityCritical]
		private void SetDefault()
		{
			lock (this)
			{
				string path = PolicyLevel.GetLocationFromType(this.m_type) + ".default";
				if (File.InternalExists(path))
				{
					PolicyLevel policyLevel = new PolicyLevel(this.m_type, path);
					this.m_rootCodeGroup = policyLevel.RootCodeGroup;
					this.m_namedPermissionSets = (ArrayList)policyLevel.NamedPermissionSets;
					this.m_fullTrustAssemblies = (ArrayList)policyLevel.FullTrustAssemblies;
					this.m_loaded = true;
				}
				else
				{
					this.m_namedPermissionSets = null;
					this.m_rootCodeGroup = null;
					this.m_permSetElement = null;
					this.m_rootCodeGroup = ((this.m_type == PolicyLevelType.Machine) ? this.CreateDefaultMachinePolicy() : this.CreateDefaultAllGroup());
					this.SetFactoryPermissionSets();
					this.SetDefaultFullTrustAssemblies();
					this.m_loaded = true;
				}
			}
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x000A0470 File Offset: 0x0009E670
		private void SetFactoryPermissionSets()
		{
			object internalSyncObject = PolicyLevel.InternalSyncObject;
			lock (internalSyncObject)
			{
				this.m_namedPermissionSets = new ArrayList();
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.FullTrust);
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.Everything);
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.Nothing);
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.SkipVerification);
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.Execution);
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.Internet);
				this.m_namedPermissionSets.Add(BuiltInPermissionSets.LocalIntranet);
			}
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x000A052C File Offset: 0x0009E72C
		private SecurityElement FindElement(SecurityElement element, string name)
		{
			foreach (object obj in element.Children)
			{
				SecurityElement securityElement = (SecurityElement)obj;
				if (securityElement.Tag.Equals("PermissionSet"))
				{
					string text = securityElement.Attribute("Name");
					if (text != null && text.Equals(name))
					{
						element.InternalChildren.Remove(securityElement);
						return securityElement;
					}
				}
			}
			return null;
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000A0594 File Offset: 0x0009E794
		[SecurityCritical]
		private void LoadAllPermissionSets()
		{
			if (this.m_permSetElement != null && this.m_permSetElement.InternalChildren != null)
			{
				object internalSyncObject = PolicyLevel.InternalSyncObject;
				lock (internalSyncObject)
				{
					while (this.m_permSetElement != null && this.m_permSetElement.InternalChildren.Count != 0)
					{
						SecurityElement securityElement = (SecurityElement)this.m_permSetElement.Children[this.m_permSetElement.InternalChildren.Count - 1];
						this.m_permSetElement.InternalChildren.RemoveAt(this.m_permSetElement.InternalChildren.Count - 1);
						if (securityElement.Tag.Equals("PermissionSet") && securityElement.Attribute("class").Equals("System.Security.NamedPermissionSet"))
						{
							NamedPermissionSet namedPermissionSet = new NamedPermissionSet();
							namedPermissionSet.FromXmlNameOnly(securityElement);
							if (namedPermissionSet.Name != null)
							{
								this.m_namedPermissionSets.Add(namedPermissionSet);
								try
								{
									namedPermissionSet.FromXml(securityElement, false, true);
								}
								catch
								{
									this.m_namedPermissionSets.Remove(namedPermissionSet);
								}
							}
						}
					}
					this.m_permSetElement = null;
				}
			}
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x000A06CC File Offset: 0x0009E8CC
		[SecurityCritical]
		private ArrayList GenericResolve(Evidence evidence, out bool allConst)
		{
			CodeGroupStack codeGroupStack = new CodeGroupStack();
			CodeGroup rootCodeGroup = this.m_rootCodeGroup;
			if (rootCodeGroup == null)
			{
				throw new PolicyException(Environment.GetResourceString("Policy_NonFullTrustAssembly"));
			}
			CodeGroupStackFrame codeGroupStackFrame = new CodeGroupStackFrame();
			codeGroupStackFrame.current = rootCodeGroup;
			codeGroupStackFrame.parent = null;
			codeGroupStack.Push(codeGroupStackFrame);
			ArrayList arrayList = new ArrayList();
			bool flag = false;
			allConst = true;
			Exception ex = null;
			while (!codeGroupStack.IsEmpty())
			{
				codeGroupStackFrame = codeGroupStack.Pop();
				FirstMatchCodeGroup firstMatchCodeGroup = codeGroupStackFrame.current as FirstMatchCodeGroup;
				UnionCodeGroup unionCodeGroup = codeGroupStackFrame.current as UnionCodeGroup;
				if (!(codeGroupStackFrame.current.MembershipCondition is IConstantMembershipCondition) || (unionCodeGroup == null && firstMatchCodeGroup == null))
				{
					allConst = false;
				}
				try
				{
					codeGroupStackFrame.policy = PolicyManager.ResolveCodeGroup(codeGroupStackFrame.current, evidence);
				}
				catch (Exception ex2)
				{
					if (ex == null)
					{
						ex = ex2;
					}
				}
				if (codeGroupStackFrame.policy != null)
				{
					if ((codeGroupStackFrame.policy.Attributes & PolicyStatementAttribute.Exclusive) != PolicyStatementAttribute.Nothing)
					{
						if (flag)
						{
							throw new PolicyException(Environment.GetResourceString("Policy_MultipleExclusive"));
						}
						arrayList.RemoveRange(0, arrayList.Count);
						arrayList.Add(codeGroupStackFrame);
						flag = true;
					}
					if (!flag)
					{
						arrayList.Add(codeGroupStackFrame);
					}
				}
			}
			if (ex != null)
			{
				throw ex;
			}
			return arrayList;
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000A07FC File Offset: 0x0009E9FC
		private static string GenerateFriendlyName(string className, Hashtable classes)
		{
			if (classes.ContainsKey(className))
			{
				return (string)classes[className];
			}
			Type type = System.Type.GetType(className, false, false);
			if (type != null && !type.IsVisible)
			{
				type = null;
			}
			if (type == null)
			{
				return className;
			}
			if (!classes.ContainsValue(type.Name))
			{
				classes.Add(className, type.Name);
				return type.Name;
			}
			if (!classes.ContainsValue(type.FullName))
			{
				classes.Add(className, type.FullName);
				return type.FullName;
			}
			classes.Add(className, type.AssemblyQualifiedName);
			return type.AssemblyQualifiedName;
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x000A08A0 File Offset: 0x0009EAA0
		private SecurityElement NormalizeClassDeep(SecurityElement elem, Hashtable classes)
		{
			this.NormalizeClass(elem, classes);
			if (elem.InternalChildren != null && elem.InternalChildren.Count > 0)
			{
				foreach (object obj in elem.Children)
				{
					this.NormalizeClassDeep((SecurityElement)obj, classes);
				}
			}
			return elem;
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x000A08F8 File Offset: 0x0009EAF8
		private SecurityElement NormalizeClass(SecurityElement elem, Hashtable classes)
		{
			if (elem.m_lAttributes == null || elem.m_lAttributes.Count == 0)
			{
				return elem;
			}
			int count = elem.m_lAttributes.Count;
			for (int i = 0; i < count; i += 2)
			{
				string text = (string)elem.m_lAttributes[i];
				if (text.Equals("class"))
				{
					string className = (string)elem.m_lAttributes[i + 1];
					elem.m_lAttributes[i + 1] = PolicyLevel.GenerateFriendlyName(className, classes);
					break;
				}
			}
			return elem;
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000A0980 File Offset: 0x0009EB80
		private SecurityElement UnnormalizeClassDeep(SecurityElement elem, Hashtable classes)
		{
			this.UnnormalizeClass(elem, classes);
			if (elem.InternalChildren != null && elem.InternalChildren.Count > 0)
			{
				foreach (object obj in elem.Children)
				{
					this.UnnormalizeClassDeep((SecurityElement)obj, classes);
				}
			}
			return elem;
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000A09D8 File Offset: 0x0009EBD8
		private SecurityElement UnnormalizeClass(SecurityElement elem, Hashtable classes)
		{
			if (classes == null || elem.m_lAttributes == null || elem.m_lAttributes.Count == 0)
			{
				return elem;
			}
			int count = elem.m_lAttributes.Count;
			int i = 0;
			while (i < count)
			{
				string text = (string)elem.m_lAttributes[i];
				if (text.Equals("class"))
				{
					string key = (string)elem.m_lAttributes[i + 1];
					string text2 = (string)classes[key];
					if (text2 != null)
					{
						elem.m_lAttributes[i + 1] = text2;
						break;
					}
					break;
				}
				else
				{
					i += 2;
				}
			}
			return elem;
		}

		// Token: 0x0400117B RID: 4475
		private ArrayList m_fullTrustAssemblies;

		// Token: 0x0400117C RID: 4476
		private ArrayList m_namedPermissionSets;

		// Token: 0x0400117D RID: 4477
		private CodeGroup m_rootCodeGroup;

		// Token: 0x0400117E RID: 4478
		private string m_label;

		// Token: 0x0400117F RID: 4479
		[OptionalField(VersionAdded = 2)]
		private PolicyLevelType m_type;

		// Token: 0x04001180 RID: 4480
		private ConfigId m_configId;

		// Token: 0x04001181 RID: 4481
		private bool m_useDefaultCodeGroupsOnReset;

		// Token: 0x04001182 RID: 4482
		private bool m_generateQuickCacheOnLoad;

		// Token: 0x04001183 RID: 4483
		private bool m_caching;

		// Token: 0x04001184 RID: 4484
		private bool m_throwOnLoadError;

		// Token: 0x04001185 RID: 4485
		private Encoding m_encoding;

		// Token: 0x04001186 RID: 4486
		private bool m_loaded;

		// Token: 0x04001187 RID: 4487
		private SecurityElement m_permSetElement;

		// Token: 0x04001188 RID: 4488
		private string m_path;

		// Token: 0x04001189 RID: 4489
		private static object s_InternalSyncObject;

		// Token: 0x0400118A RID: 4490
		private static readonly string[] s_reservedNamedPermissionSets = new string[]
		{
			"FullTrust",
			"Nothing",
			"Execution",
			"SkipVerification",
			"Internet",
			"LocalIntranet",
			"Everything"
		};

		// Token: 0x0400118B RID: 4491
		private static string[] EcmaFullTrustAssemblies = new string[]
		{
			"mscorlib.resources",
			"System",
			"System.resources",
			"System.Xml",
			"System.Xml.resources",
			"System.Windows.Forms",
			"System.Windows.Forms.resources",
			"System.Data",
			"System.Data.resources"
		};

		// Token: 0x0400118C RID: 4492
		private static string[] MicrosoftFullTrustAssemblies = new string[]
		{
			"System.Security",
			"System.Security.resources",
			"System.Drawing",
			"System.Drawing.resources",
			"System.Messaging",
			"System.Messaging.resources",
			"System.ServiceProcess",
			"System.ServiceProcess.resources",
			"System.DirectoryServices",
			"System.DirectoryServices.resources",
			"System.Deployment",
			"System.Deployment.resources"
		};
	}
}

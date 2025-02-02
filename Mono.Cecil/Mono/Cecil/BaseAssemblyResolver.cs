using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000093 RID: 147
	public abstract class BaseAssemblyResolver : IAssemblyResolver
	{
		// Token: 0x060004EC RID: 1260 RVA: 0x00014CB0 File Offset: 0x00012EB0
		public void AddSearchDirectory(string directory)
		{
			this.directories.Add(directory);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00014CBE File Offset: 0x00012EBE
		public void RemoveSearchDirectory(string directory)
		{
			this.directories.Remove(directory);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00014CD0 File Offset: 0x00012ED0
		public string[] GetSearchDirectories()
		{
			string[] array = new string[this.directories.size];
			Array.Copy(this.directories.items, array, array.Length);
			return array;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00014D03 File Offset: 0x00012F03
		public virtual AssemblyDefinition Resolve(string fullName)
		{
			return this.Resolve(fullName, new ReaderParameters());
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00014D11 File Offset: 0x00012F11
		public virtual AssemblyDefinition Resolve(string fullName, ReaderParameters parameters)
		{
			if (fullName == null)
			{
				throw new ArgumentNullException("fullName");
			}
			return this.Resolve(AssemblyNameReference.Parse(fullName), parameters);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060004F1 RID: 1265 RVA: 0x00014D30 File Offset: 0x00012F30
		// (remove) Token: 0x060004F2 RID: 1266 RVA: 0x00014D68 File Offset: 0x00012F68
		public event AssemblyResolveEventHandler ResolveFailure;

		// Token: 0x060004F3 RID: 1267 RVA: 0x00014DA0 File Offset: 0x00012FA0
		protected BaseAssemblyResolver()
		{
			this.directories = new Collection<string>(2)
			{
				".",
				"bin"
			};
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00014DD7 File Offset: 0x00012FD7
		private AssemblyDefinition GetAssembly(string file, ReaderParameters parameters)
		{
			if (parameters.AssemblyResolver == null)
			{
				parameters.AssemblyResolver = this;
			}
			return ModuleDefinition.ReadModule(file, parameters).Assembly;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00014DF4 File Offset: 0x00012FF4
		public virtual AssemblyDefinition Resolve(AssemblyNameReference name)
		{
			return this.Resolve(name, new ReaderParameters());
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00014E04 File Offset: 0x00013004
		public virtual AssemblyDefinition Resolve(AssemblyNameReference name, ReaderParameters parameters)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (parameters == null)
			{
				parameters = new ReaderParameters();
			}
			AssemblyDefinition assemblyDefinition = this.SearchDirectory(name, this.directories, parameters);
			if (assemblyDefinition != null)
			{
				return assemblyDefinition;
			}
			if (name.IsRetargetable)
			{
				name = new AssemblyNameReference(name.Name, new Version(0, 0, 0, 0))
				{
					PublicKeyToken = Empty<byte>.Array
				};
			}
			string directoryName = Path.GetDirectoryName(typeof(object).Module.FullyQualifiedName);
			if (BaseAssemblyResolver.IsZero(name.Version))
			{
				assemblyDefinition = this.SearchDirectory(name, new string[]
				{
					directoryName
				}, parameters);
				if (assemblyDefinition != null)
				{
					return assemblyDefinition;
				}
			}
			if (name.Name == "mscorlib")
			{
				assemblyDefinition = this.GetCorlib(name, parameters);
				if (assemblyDefinition != null)
				{
					return assemblyDefinition;
				}
			}
			assemblyDefinition = this.GetAssemblyInGac(name, parameters);
			if (assemblyDefinition != null)
			{
				return assemblyDefinition;
			}
			assemblyDefinition = this.SearchDirectory(name, new string[]
			{
				directoryName
			}, parameters);
			if (assemblyDefinition != null)
			{
				return assemblyDefinition;
			}
			if (this.ResolveFailure != null)
			{
				assemblyDefinition = this.ResolveFailure(this, name);
				if (assemblyDefinition != null)
				{
					return assemblyDefinition;
				}
			}
			throw new AssemblyResolutionException(name);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00014F18 File Offset: 0x00013118
		private AssemblyDefinition SearchDirectory(AssemblyNameReference name, IEnumerable<string> directories, ReaderParameters parameters)
		{
			string[] array = new string[]
			{
				".exe",
				".dll"
			};
			foreach (string path in directories)
			{
				foreach (string str in array)
				{
					string text = Path.Combine(path, name.Name + str);
					if (File.Exists(text))
					{
						return this.GetAssembly(text, parameters);
					}
				}
			}
			return null;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00014FC4 File Offset: 0x000131C4
		private static bool IsZero(Version version)
		{
			return version == null || (version.Major == 0 && version.Minor == 0 && version.Build == 0 && version.Revision == 0);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00014FF4 File Offset: 0x000131F4
		private AssemblyDefinition GetCorlib(AssemblyNameReference reference, ReaderParameters parameters)
		{
			Version version = reference.Version;
			AssemblyName name = typeof(object).Assembly.GetName();
			if (name.Version == version || BaseAssemblyResolver.IsZero(version))
			{
				return this.GetAssembly(typeof(object).Module.FullyQualifiedName, parameters);
			}
			string path = Directory.GetParent(Directory.GetParent(typeof(object).Module.FullyQualifiedName).FullName).FullName;
			if (!BaseAssemblyResolver.on_mono)
			{
				switch (version.Major)
				{
				case 1:
					if (version.MajorRevision == 3300)
					{
						path = Path.Combine(path, "v1.0.3705");
						goto IL_170;
					}
					path = Path.Combine(path, "v1.0.5000.0");
					goto IL_170;
				case 2:
					path = Path.Combine(path, "v2.0.50727");
					goto IL_170;
				case 4:
					path = Path.Combine(path, "v4.0.30319");
					goto IL_170;
				}
				throw new NotSupportedException("Version not supported: " + version);
			}
			if (version.Major == 1)
			{
				path = Path.Combine(path, "1.0");
			}
			else if (version.Major == 2)
			{
				if (version.MajorRevision == 5)
				{
					path = Path.Combine(path, "2.1");
				}
				else
				{
					path = Path.Combine(path, "2.0");
				}
			}
			else
			{
				if (version.Major != 4)
				{
					throw new NotSupportedException("Version not supported: " + version);
				}
				path = Path.Combine(path, "4.0");
			}
			IL_170:
			string text = Path.Combine(path, "mscorlib.dll");
			if (File.Exists(text))
			{
				return this.GetAssembly(text, parameters);
			}
			return null;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00015190 File Offset: 0x00013390
		private static Collection<string> GetGacPaths()
		{
			if (BaseAssemblyResolver.on_mono)
			{
				return BaseAssemblyResolver.GetDefaultMonoGacPaths();
			}
			Collection<string> collection = new Collection<string>(2);
			string environmentVariable = Environment.GetEnvironmentVariable("WINDIR");
			if (environmentVariable == null)
			{
				return collection;
			}
			collection.Add(Path.Combine(environmentVariable, "assembly"));
			collection.Add(Path.Combine(environmentVariable, Path.Combine("Microsoft.NET", "assembly")));
			return collection;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000151F0 File Offset: 0x000133F0
		private static Collection<string> GetDefaultMonoGacPaths()
		{
			Collection<string> collection = new Collection<string>(1);
			string currentMonoGac = BaseAssemblyResolver.GetCurrentMonoGac();
			if (currentMonoGac != null)
			{
				collection.Add(currentMonoGac);
			}
			string environmentVariable = Environment.GetEnvironmentVariable("MONO_GAC_PREFIX");
			if (string.IsNullOrEmpty(environmentVariable))
			{
				return collection;
			}
			string[] array = environmentVariable.Split(new char[]
			{
				Path.PathSeparator
			});
			foreach (string text in array)
			{
				if (!string.IsNullOrEmpty(text))
				{
					string text2 = Path.Combine(Path.Combine(Path.Combine(text, "lib"), "mono"), "gac");
					if (Directory.Exists(text2) && !collection.Contains(currentMonoGac))
					{
						collection.Add(text2);
					}
				}
			}
			return collection;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000152A6 File Offset: 0x000134A6
		private static string GetCurrentMonoGac()
		{
			return Path.Combine(Directory.GetParent(Path.GetDirectoryName(typeof(object).Module.FullyQualifiedName)).FullName, "gac");
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000152D8 File Offset: 0x000134D8
		private AssemblyDefinition GetAssemblyInGac(AssemblyNameReference reference, ReaderParameters parameters)
		{
			if (reference.PublicKeyToken == null || reference.PublicKeyToken.Length == 0)
			{
				return null;
			}
			if (this.gac_paths == null)
			{
				this.gac_paths = BaseAssemblyResolver.GetGacPaths();
			}
			if (BaseAssemblyResolver.on_mono)
			{
				return this.GetAssemblyInMonoGac(reference, parameters);
			}
			return this.GetAssemblyInNetGac(reference, parameters);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00015324 File Offset: 0x00013524
		private AssemblyDefinition GetAssemblyInMonoGac(AssemblyNameReference reference, ReaderParameters parameters)
		{
			for (int i = 0; i < this.gac_paths.Count; i++)
			{
				string gac = this.gac_paths[i];
				string assemblyFile = BaseAssemblyResolver.GetAssemblyFile(reference, string.Empty, gac);
				if (File.Exists(assemblyFile))
				{
					return this.GetAssembly(assemblyFile, parameters);
				}
			}
			return null;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00015374 File Offset: 0x00013574
		private AssemblyDefinition GetAssemblyInNetGac(AssemblyNameReference reference, ReaderParameters parameters)
		{
			string[] array = new string[]
			{
				"GAC_MSIL",
				"GAC_32",
				"GAC_64",
				"GAC"
			};
			string[] array2 = new string[]
			{
				string.Empty,
				"v4.0_"
			};
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < array.Length; j++)
				{
					string text = Path.Combine(this.gac_paths[i], array[j]);
					string assemblyFile = BaseAssemblyResolver.GetAssemblyFile(reference, array2[i], text);
					if (Directory.Exists(text) && File.Exists(assemblyFile))
					{
						return this.GetAssembly(assemblyFile, parameters);
					}
				}
			}
			return null;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00015428 File Offset: 0x00013628
		private static string GetAssemblyFile(AssemblyNameReference reference, string prefix, string gac)
		{
			StringBuilder stringBuilder = new StringBuilder().Append(prefix).Append(reference.Version).Append("__");
			for (int i = 0; i < reference.PublicKeyToken.Length; i++)
			{
				stringBuilder.Append(reference.PublicKeyToken[i].ToString("x2"));
			}
			return Path.Combine(Path.Combine(Path.Combine(gac, reference.Name), stringBuilder.ToString()), reference.Name + ".dll");
		}

		// Token: 0x040003DF RID: 991
		private static readonly bool on_mono = Type.GetType("Mono.Runtime") != null;

		// Token: 0x040003E0 RID: 992
		private readonly Collection<string> directories;

		// Token: 0x040003E1 RID: 993
		private Collection<string> gac_paths;
	}
}

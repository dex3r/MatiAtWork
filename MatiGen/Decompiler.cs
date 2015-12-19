using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Utils;
using System.IO;
using Mono.Cecil;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Ast;
using System.Reflection.Emit;
using System.Reflection;
using TypeAttributes = System.Reflection.TypeAttributes;
using MethodAttributes = System.Reflection.MethodAttributes;
using System.Threading;

namespace MatiGen
{
    public sealed class Decompiler : IDecompiler
    {
        private ITempDirManager tempManager;
        private string dllFileName;
        private string dllFileDir;
        private string dllFileFullPathDir;
        private string dllFileRelativePath;

        private string dllFileFullPath;

        private object decompileLock = new object();

        public Decompiler(ITempDirManager tempManager)
        {
            this.tempManager = tempManager;
            this.dllFileName = "DecompilerIntermediate.dll";
            this.dllFileDir = "Decompiler\\";
            this.dllFileFullPathDir = Path.Combine(tempManager.GetTempDirectoryPath(), dllFileDir);
            Directory.CreateDirectory(this.dllFileFullPathDir);

            this.dllFileRelativePath = Path.Combine(dllFileDir, dllFileName);
            this.dllFileFullPath = Path.Combine(tempManager.GetTempDirectoryPath(), dllFileRelativePath);
        }

        public string DecompileExpression(LambdaExpression expression)
        {
            lock (decompileLock)
            {
                AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain
                    .DefineDynamicAssembly(new AssemblyName("GeneratedAssembly"), AssemblyBuilderAccess.Save, dllFileFullPathDir);

                ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("GeneratedModule", dllFileName, true);
                TypeBuilder typeBuilder = moduleBuilder.DefineType("GeneratedType", TypeAttributes.Class | TypeAttributes.Public);

                MethodBuilder methodBuilder = typeBuilder
                    .DefineMethod("GeneratedMethod", MethodAttributes.Public | MethodAttributes.Static);

                expression.CompileToMethod(methodBuilder);

                typeBuilder.CreateType();
                assemblyBuilder.Save(dllFileName);

                AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(dllFileFullPath);

                // This code assumes that there is only one module in given assembly
                ModuleDefinition moduleDefinition = assemblyDefinition.Modules.First();

                DecompilerSettings decompilerSettings = new DecompilerSettings();

                DecompilerContext decompilerContext = new DecompilerContext(moduleDefinition)
                {
                    CancellationToken = CancellationToken.None,
                    // This code assumes that there is only one type in given module
                    CurrentType = moduleDefinition.Types.First(),
                    Settings = decompilerSettings
                };
                AstBuilder astBuilder = new AstBuilder(decompilerContext);

                astBuilder.AddAssembly(moduleDefinition, false);
                astBuilder.RunTransformations();

                using (StringWriter stringWriter = new StringWriter())
                {
                    ITextOutput textOutput = new ICSharpCode.Decompiler.PlainTextOutput(stringWriter);
                    astBuilder.GenerateCode(textOutput);

                    return stringWriter.ToString();
                }
            }
        }
    }
}

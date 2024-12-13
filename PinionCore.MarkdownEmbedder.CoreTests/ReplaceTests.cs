using PinionCore.MarkdownEmbedder.Explainers;
using System.ComponentModel.DataAnnotations;

namespace PinionCore.MarkdownEmbedder.Core.Tests
{
    [TestClass()]
    public class ReplaceTests
    {
        [TestMethod()]
        public void XmlReplaceTest()
        {
            var replacer = new XmlReplacer() as IReplacer;

            var element = new Element { Name = "csproj", Content = @"replaced" };
            var result = replacer.Replace(@"<Project Sdk=""Microsoft.NET.Sdk""> 
    <PropertyGroup>
        <TargetFramework>net9.0</TargetFramework>
        <LangVersion>latest</LangVersion>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
    </PropertyGroup>
    <ItemGroup>
        <PackageReference Include=""Microsoft.NET.Test.Sdk"" Version=""17.11.1"" />
        <PackageReference Include=""MSTest"" Version=""3.6.1"" />
    </ItemGroup>
    <!-- <Replace Name=""csproj"">-->
    <ItemGroup>
        <ProjectReference Include=""..\PinionCore.MarkdownEmbedder.Core\PinionCore.MarkdownEmbedder.Core.csproj"" />
        <ProjectReference Include=""..\PinionCore.MarkdownEmbedder.Explainers\PinionCore.MarkdownEmbedder.Explainers.csproj"" />
    </ItemGroup>
    <!-- </Replace >-->
    <ItemGroup>
    <!-- <Replace Name=""csproj"">-->
        <Using Include=""Microsoft.VisualStudio.TestTools.UnitTesting"" />
    <!-- </Replace >-->
    </ItemGroup>
</Project>", element);

            Assert.AreEqual(@"<Project Sdk=""Microsoft.NET.Sdk""> 
    <PropertyGroup>
        <TargetFramework>net9.0</TargetFramework>
        <LangVersion>latest</LangVersion>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
    </PropertyGroup>
    <ItemGroup>
        <PackageReference Include=""Microsoft.NET.Test.Sdk"" Version=""17.11.1"" />
        <PackageReference Include=""MSTest"" Version=""3.6.1"" />
    </ItemGroup>
    <!-- <Replace Name=""csproj"">-->replaced<!-- </Replace >-->
    <ItemGroup>
    <!-- <Replace Name=""csproj"">-->replaced<!-- </Replace >-->
    </ItemGroup>
</Project>", result);
        }
    }
}
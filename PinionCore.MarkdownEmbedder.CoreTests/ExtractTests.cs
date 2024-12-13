namespace PinionCore.MarkdownEmbedder.Core.Tests
{
    [TestClass()]
    public class ExtractTests
    {
        

        [TestMethod()]
        public void XmlExtractTest()
        {

            var xml = new Explainers.XmlExtracter() as IExtracter;
            var elements = xml.Extract(@"<Project Sdk=""Microsoft.NET.Sdk"">

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
<!-- <Extract Name=""csproj"">-->
  <ItemGroup>
    <ProjectReference Include=""..\PinionCore.MarkdownEmbedder.Core\PinionCore.MarkdownEmbedder.Core.csproj"" />
    <ProjectReference Include=""..\PinionCore.MarkdownEmbedder.Explainers\PinionCore.MarkdownEmbedder.Explainers.csproj"" />
  </ItemGroup>
	<!-- </Extract >-->

  <ItemGroup>
    <Using Include=""Microsoft.VisualStudio.TestTools.UnitTesting"" />
  </ItemGroup>

</Project>
");
            var element= elements.Single();
            
            Assert.AreEqual("csproj", element.Name);
            Assert.AreEqual(@"
  <ItemGroup>
    <ProjectReference Include=""..\PinionCore.MarkdownEmbedder.Core\PinionCore.MarkdownEmbedder.Core.csproj"" />
    <ProjectReference Include=""..\PinionCore.MarkdownEmbedder.Explainers\PinionCore.MarkdownEmbedder.Explainers.csproj"" />
  </ItemGroup>
	", element.Content);
        }
    }
}
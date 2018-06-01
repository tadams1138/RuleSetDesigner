using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RuleSetDesignerLauncher
{
    [TestClass]
    public class RuleSetDialogFactoryTests
    {
        private static readonly string FullPath = TestHelper.GetFullPath("DummyAssembly.dll");
        private readonly TypeInfo _typeInfo;
        private AppDomain _loaderDomain;

        public RuleSetDialogFactoryTests()
        {
            _typeInfo = new TypeInfo { FilePath = FullPath, FullName = "DummyAssembly.RuleSetContext" };
        }

        [TestInitialize]
        public void Initialize()
        {
            _loaderDomain = AppDomainFactory.CreateWithShadowCopy();
        }

        [TestCleanup]
        public void Cleanup()
        {
            AppDomain.Unload(_loaderDomain);
        }

        [TestMethod]
        public void GivenTypeInfo_ReturnsDialog()
        {
            // Arrange

            // Act
            var result = GetRuleSetDialog(_typeInfo);

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void GivenTypeInfo_DoesNotLockFile()
        {
            // Arrange
            var dummyAssemblyPath = TestHelper.GetFullPath("DummyAssembly.dll");
            var assemblyToTest = TestHelper.GetFullPath($"{Guid.NewGuid()}.dll");
            File.Copy(dummyAssemblyPath, assemblyToTest);
            _typeInfo.FilePath = assemblyToTest;

            // Act
            GetRuleSetDialog(_typeInfo);

            // Assert
            File.Delete(assemblyToTest);
        }

        [TestMethod]
        public void GivenTypeInfo_NotLoadedIntoCurrentAppDomain()
        {
            // Arrange

            // Act
            GetRuleSetDialog(_typeInfo);

            // Assert
            AppDomain.CurrentDomain.GetAssemblies().Any(x => x.FullName.Contains("DummyAssembly")).Should().BeFalse();
        }

        private Form GetRuleSetDialog(TypeInfo typeInfo)
        {
            return RuleSetDialogFactory.Get(_loaderDomain, typeInfo, "test fileName");
        }
    }
}
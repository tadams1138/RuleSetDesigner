using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RuleSetDesignerLauncher
{
    [TestClass]
    public class TypeInfoFactoryTests
    {
        [TestMethod]
        public void GivenInvalidAssemblyName_ReturnsEmptyList()
        {
            // Arrange

            // Act
            var results = TypeInfoFactory.Get(new[] { TestHelper.GetFullPath("InvalidAssembly.dll") });

            // Assert
            results.Should().NotBeNull().And.BeEmpty();
        }

        [TestMethod]
        public void GivenAssembly_ReturnTypeInfo()
        {
            // Arrange
            var fullPath = TestHelper.GetFullPath("DummyAssembly.dll");

            // Act
            var results = TypeInfoFactory.Get(new[] { fullPath });

            // Assert
            results.Should().NotBeNull().And.Contain(x => x.FullName == "DummyAssembly.RuleSetContext" && x.FilePath == fullPath);
        }

        [TestMethod]
        public void GivenAssembly_DoesNotLockFile()
        {
            // Arrange
            var dummyAssemblyPath = TestHelper.GetFullPath("DummyAssembly.dll");
            var assemblyToTest = TestHelper.GetFullPath($"{Guid.NewGuid()}.dll");
            File.Copy(dummyAssemblyPath, assemblyToTest);

            // Act
            TypeInfoFactory.Get(new[] { assemblyToTest });

            // Assert
            File.Delete(assemblyToTest);
        }

        [TestMethod]
        public void GivenAssembly_NotLoadedIntoCurrentAppDomain()
        {
            // Arrange
            var fullPath = TestHelper.GetFullPath("DummyAssembly.dll");

            // Act
            TypeInfoFactory.Get(new[] { fullPath });

            // Assert
            AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies().Any(x => x.FullName.Contains("DummyAssembly")).Should().BeFalse();
        }
    }
}

using NUnit.Framework;
using System;
using Bee.Core;
using Unity.Build.Classic.Private;
using Unity.Build.MockPlatform;
using Unity.Build.MockPlatform.Classic;
using UnityEditor;
using UnityEngine;

namespace Unity.Build.MockPlatform
{
    class TestPlatformClassicNonIncrementalPipeline_WrongNamespace : ClassicNonIncrementalPipelineBase
    {
        protected override RunResult OnRun(RunContext context) => throw new NotImplementedException();
        protected override BuildTarget BuildTarget { get; } = BuildTarget.NoTarget;
        public override Platform Platform { get; } = Classic.MockPlatform.Instance;
    }
}

namespace Unity.Build.MockPlatform.Classic
{
    [HideInInspector]
    class MockPlatform : Platform
    {
        public override bool HasPosix => false;
        public static MockPlatform Instance = new MockPlatform();
    }

    class TestPlatformClassicNonIncrementalPipeline : ClassicNonIncrementalPipelineBase
    {
        protected override RunResult OnRun(RunContext context) => throw new NotImplementedException();
        protected override BuildTarget BuildTarget { get; } = BuildTarget.NoTarget;
        public override Platform Platform { get; } = MockPlatform.Instance;
    }
}

namespace Unity.Build.Classic.Tests
{
    /// <summary>
    /// BuildPipelineSelector should only pick pipelines from namespace Unity.Build.*Platform*.Classic
    /// If pipeline class namespace contains "Test" word, ignore it
    /// </summary>
    [TestFixture]
    public class ClassicBuildPipelineTests
    {
        [Test]
        public void BuildPipelineSelectorTests()
        {
            Assert.IsTrue(BuildPipelineSelector.IsBuildPipelineValid(new TestPlatformClassicNonIncrementalPipeline(), MockPlatform.Classic.MockPlatform.Instance));
            Assert.IsFalse(BuildPipelineSelector.IsBuildPipelineValid(new TestPlatformClassicNonIncrementalPipeline_WrongNamespace(), MockPlatform.Classic.MockPlatform.Instance));

            var selector = new BuildPipelineSelector();
            Assert.AreEqual(selector.SelectFor(MockPlatform.Classic.MockPlatform.Instance, false).GetType(), typeof(TestPlatformClassicNonIncrementalPipeline));
        }
    }
}

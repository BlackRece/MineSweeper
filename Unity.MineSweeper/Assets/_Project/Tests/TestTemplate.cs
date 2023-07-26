using System.Collections;

using NUnit.Framework;

using UnityEngine.TestTools;

namespace _Project.Tests {
    [TestFixture]
    public class TestTemplate {
        [SetUp]
        public void Setup() {
        }
        
        // A Test behaves as an ordinary method
        [Test]
        public void WhenOnAHappyPath() {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator ThenHappyPathPasses() {
            Assert.True(true);
            yield return null;
        }
        
        [TearDown]
        public void Teardown() {
        }
    }
}
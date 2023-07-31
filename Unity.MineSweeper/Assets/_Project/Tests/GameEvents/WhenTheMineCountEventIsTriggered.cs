using BlackRece.MineSweeper;

using NUnit.Framework;

using UnityEngine;

namespace GivenAGameEvent {
    // TODO: Write tests for the GameEvents class
    [TestFixture]
    public class WhenTheMineCountEventIsTriggered {
        [SetUp]
        public void Setup() {
            GameEvents.EvtMineCount += MineCountHandler;
        }
        
        public int MineCountHandler(Vector2Int position) {
            return 42;
        }
        
        [Test]
        public void ThenHappyPathPasses() {
            // Arrange
            Vector2Int testPosition = new Vector2Int(1, 1);
            int expectedResult = 42;
            int actualResult = 0;

            // Register a test handler for the event
            /*GameEvents.EvtMineCount += (position) =>
            {
                Assert.AreEqual(testPosition, position, "Position parameter should match");
                return expectedResult;
            };
            */

            // Act
            actualResult = GameEvents.OnMineCount(testPosition);

            // Assert
            Assert.AreEqual(expectedResult, actualResult, "Returned result should match expected result");
        }
        
        [TearDown]
        public void Teardown() {
            GameEvents.EvtMineCount -= MineCountHandler;
        }
    }
}
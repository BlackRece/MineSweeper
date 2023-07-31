using System.Collections;

using BlackRece.MineSweeper;

using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;

namespace GivenAGameCell {
    [TestFixture]
    public class WhenCellIsFlagged {
        private GameObject _cellGameObject;
        private GameCell _gameCell;
        
        [SetUp]
        public void Setup() {
            _cellGameObject = new GameObject();
            _gameCell = _cellGameObject.AddComponent<GameCell>();
            
            _gameCell.ToggleFlag();
        }
        
        [Test]
        public void ThenCellIsNotRevealed() {
            Assert.That(_gameCell.IsRevealed, Is.False, "Cell can not be revealed while flagged.");
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
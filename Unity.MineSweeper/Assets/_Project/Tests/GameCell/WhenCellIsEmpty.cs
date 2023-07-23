using System.Collections;

using BlackRece.MineSweeper;

using NUnit.Framework;

using UnityEngine;
using UnityEngine.TestTools;

namespace GivenAGameCell {
    public class WhenCellIsEmpty {
        private GameObject _cellGameObject;
        private GameCell _gameCell;

        [SetUp]
        public void Setup() {
            _cellGameObject = new GameObject();
            _gameCell = _cellGameObject.AddComponent<GameCell>();
        }

        [Test]
        public void ThenHasMineReturnsFalse() {
            Assert.IsFalse(_gameCell.HasMine);
        }

        [Test]
        public void ThenIsRevealedReturnsFalse() {
            Assert.IsFalse(_gameCell.IsRevealed);
        }

        [Test]
        public void ThenMineCountReturnsZero() {
            Assert.AreEqual(0, _gameCell.MineCount);
        }

        [TearDown]
        public void Teardown() {
            Object.DestroyImmediate(_cellGameObject);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator GivenAGameCellWithEnumeratorPasses() {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
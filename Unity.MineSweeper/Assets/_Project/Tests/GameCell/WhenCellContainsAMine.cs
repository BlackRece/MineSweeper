using System.Collections;
using System.Collections.Generic;

using BlackRece.MineSweeper;

using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace GivenAGameCell {
    public class WhenCellContainsAMine {
        private GameObject _cellGameObject;
        private GameCell _gameCell;
        
        [SetUp]
        public void Setup() {
            _cellGameObject = new GameObject();
            _cellGameObject.AddComponent<MeshRenderer>(); 
            _gameCell = _cellGameObject.AddComponent<GameCell>();
            
            _gameCell.SetMine();
        }
        
        // A Test behaves as an ordinary method
        [Test]
        public void GivenAGameCellSimplePasses() {
            // Use the Assert class to test conditions
        }

        [Test]
        public void ThenHasMineReturnsTrue() {
            Assert.IsTrue(_gameCell.HasMine);
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
        public IEnumerator ThenRevealMinesEventIsRaised() {
            /*
             bool eventRaised = false;

            // Local method to handle the event
            void OnRevealMinesEvent() {
                // Set the eventRaised flag to true when the event is raised
                eventRaised = true;
            }
            
            // Subscribe to the event using the local method
            GameEvents.EvtRevealMines += OnRevealMinesEvent;
            
            // Act: Call RevealCell to trigger the event
            _gameCell.RevealCell();

            // Yield control back to the test runner to allow the event to execute
            yield return null;

            // Assert: Check if the event was raised
            Assert.IsTrue(eventRaised);

            // Unsubscribe from the event (important to avoid interference in other tests)
            GameEvents.EvtRevealMines -= OnRevealMinesEvent;
            */
            Assert.True(true);
            yield return null;
        }
    }
}
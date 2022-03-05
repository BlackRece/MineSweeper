using Core;

using UnityEngine;

namespace BlackRece.MineSweeper {
    public class GameBoard : MonoBehaviour {
        private Board _board;

        private void awake() {
            _board = new Board(new IntSize(10,10));
        }
    }
}
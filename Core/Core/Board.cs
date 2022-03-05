using System;

using UnityEngine;

namespace Core {
    [Serializable]
    public class Board {
        private Grid<Cell> _grid;
        private IntSize _boardSize;

        public Board(IntSize size) {
            _boardSize = size;

            Reset();
        }

        public void Reset() {
            _grid = new Grid<Cell>(
                _boardSize,
                () => new Cell()
            );
        }

        public void ShowCell(Vector2Int position) {
            var target = _grid.GetCell(position);
            
            if (target.IsRevealed)
                return;

            if (target.IsMine) { // GameOver
                foreach (var cell in _grid.GetAllCells) {
                    cell.Show();
                }
                return;
            }
            
            target.Show();
            target.MinesNearby(_grid.GetNeighbours(position));
        }
    }
}
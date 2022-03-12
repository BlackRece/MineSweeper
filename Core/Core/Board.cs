using System;
using System.Collections.Generic;

using UnityEngine;

using Random = UnityEngine.Random;

namespace BlackRece.Core {
    [Serializable]
    public sealed class Board {
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

        public void SetUp(int amountOfMines) {
            if (amountOfMines < 1)
                return;

            var positions = _grid.GetAllPositions;

            for (var i = 0; i < amountOfMines; i++) {
                var mine = Random.Range(0, positions.Count);

                var cell = _grid.GetCell(positions[mine]);
                cell.IsMine = true;
                
                positions.RemoveAt(mine);
            }
        }

        public List<Vector2Int> GetCellPositions => _grid.GetAllPositions;
    }
}
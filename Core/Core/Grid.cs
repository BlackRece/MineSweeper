using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Core {
    public interface IGrid<TCell> {
        TCell GetCell(Vector2Int position);
        void SetCell(Vector2Int position, TCell cell);
        List<TCell> GetNeighbours(Vector2Int target);
    }

    public sealed class Grid<TCell> : IGrid<TCell> {
        private readonly Dictionary<Vector2Int, TCell> _grid;

        public Grid(IntSize size, Func<TCell> createCell) {
            _grid = new Dictionary<Vector2Int, TCell>();
            
            for (var x = 0; x < size.Width; x++) {
                for (var y = 0; y < size.Height; y++) {
                    var position = new Vector2Int(x,y);
                    _grid.Add(position, createCell());
                }
            }
        }

        public List<TCell> GetAllCells => _grid.Values.ToList();

        public TCell GetCell(Vector2Int position) {
            if (_grid.ContainsKey(position))
                return _grid[position];

            throw new IndexOutOfRangeException(
                $"position ({position}) was outside of grid.");
        }

        public void SetCell(Vector2Int position, TCell cell) {
            if (_grid.ContainsKey(position))
                _grid[position] = cell;

            throw new IndexOutOfRangeException(
                $"position ({position}) was outside of grid.");
        }

        public List<TCell> GetNeighbours(Vector2Int target) {
            if (_grid.ContainsKey(target))
                throw new IndexOutOfRangeException(
                    $"position ({target}) was outside of grid.");

            var neighbours = new List<TCell>();
            
            for (var x = -1; x < 1; x++) {
                for (var y = -1; y < 1; y++) {
                    var position = new Vector2Int(target.x + x, target.y + y);
                    
                    if(!_grid.ContainsKey(position))
                        continue;
                    
                    neighbours.Add(_grid[position]);
                }
            }

            return neighbours;
        }
    }
}
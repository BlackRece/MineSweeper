using System;
using System.Collections.Generic;

using BlackRece.Core;

using UnityEngine;

namespace BlackRece.MineSweeper {
    public sealed class GameBoard : MonoBehaviour {
        [SerializeField] private GameObject _cellPrefab = null;
        
        [SerializeField] [Range(10, 500)] private int _width, _height; 
        private IntSize _size = null;

        [SerializeField] private int _mines;
        
        private Board _board;
        private Dictionary<Vector2Int, GameObject> _UIBoard;

        private void Awake() {
            _size = new IntSize(_width, _height);
            _UIBoard = new Dictionary<Vector2Int, GameObject>();

            if (_cellPrefab == null)
                throw new ArgumentNullException(nameof(_cellPrefab),"Missing Cell prefab");
        }

        private void Start() {
            _board = new Board(_size);

            RenderGameBoard();
            
            _board.SetUp(_mines);

            GameEvents.evtCellClick += ShowCellOnGrid;
        }

        private void RenderGameBoard() {
            foreach (var boardPosition in _board.GetCellPositions) {
                var cellGO = Instantiate(_cellPrefab, transform);
                cellGO.transform.position = new Vector3(boardPosition.x, 0f, boardPosition.y);
                _UIBoard.Add(boardPosition, cellGO);

                var cell = cellGO.GetComponent<GameCell>();
                cell.SetBoardPosition(boardPosition);
            }
        }

        private void ShowCellOnGrid(Vector2Int position) {
            _board.ShowCell(position);

            var cell = _UIBoard[position].GetComponent<GameCell>();
            cell.ShowMineCount();
        }
    }

    public sealed class GameEvents : ScriptableObject {
        public static event Action<Vector2Int> evtCellClick;

        public static void OnCellClick(Vector2Int position) => evtCellClick?.Invoke(position);
    }
}
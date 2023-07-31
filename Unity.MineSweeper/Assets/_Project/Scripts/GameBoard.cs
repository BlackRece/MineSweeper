using System;
using System.Collections.Generic;
using System.Linq;

using BlackRece.Core;

using UnityEngine;
using Random = UnityEngine.Random;

namespace BlackRece.MineSweeper {
    public sealed class GameBoard : MonoBehaviour {
        [SerializeField] private GameObject _cellPrefab = null;
        [SerializeField] private GameObject _minePrefab = null;
        
        [SerializeField] [Range(10, 500)] private int _width, _height; 
        private IntSize _size = null;

        [SerializeField] private int _mines;
        
        private Board _board;
        private Dictionary<Vector2Int, GameObject> _UIBoard;

        private float _clickDelay;
        private float _clickTimer;
        
        private void Awake() {
            _size = new IntSize(_width, _height);
            _UIBoard = new Dictionary<Vector2Int, GameObject>();

            _clickDelay = 0.1f;
            _clickTimer = 0f;
            
            if (_cellPrefab == null)
                throw new ArgumentNullException(nameof(_cellPrefab),"Missing Cell prefab");
        }

        private void Start() {
            _board = new Board(_size);

            RenderGameBoard();
            SetupMines(_mines);

            GameEvents.EvtMineCount += GetMineCount;
            GameEvents.EvtRevealCell += RevealNeighbours;
            GameEvents.EvtRevealMines += RevealMines;
            GameEvents.EvtFlagCell += ToggleFlag;
        }

        private void Update() {
            if(_clickTimer > 0f)
                _clickTimer -= Time.deltaTime;
        }

        private void ToggleFlag(Vector2Int obj) {
            if(_clickTimer > 0f)
                return;

            _clickTimer = _clickDelay;
            _UIBoard[obj].GetComponent<GameCell>()
                .ToggleFlag();
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

        // Show cell on grid - called when player clicks on cell
        private int GetMineCount(Vector2Int position) {
            var mineCount = 0;

            foreach (var cellPos in GetNeighbourPositions(position)) {
                if(_UIBoard[cellPos].GetComponent<GameCell>().HasMine)
                    mineCount++;
            }
            
            return mineCount;
        }
        
        private void RevealNeighbours(Vector2Int position) {
            var neighbourPositions = GetNeighbourPositions(position);
            foreach (var cellPosition in neighbourPositions) {
                var cell = _UIBoard[cellPosition].GetComponent<GameCell>();
                if(cell.IsRevealed || cell.HasMine || cell.IsFlagged)
                    continue;
                
                cell.SetMineCount(GetMineCount(cellPosition));
                cell.RevealCell();
            }
        }
        
        private void RevealMines() {
            foreach (GameObject cellGO in _UIBoard.Values) {
                var cell = cellGO.GetComponent<GameCell>();
                if(!cell.HasMine)
                    cell.RevealCell();
            }
        }

        private void SetupMines(int mineAmount) {
            if (mineAmount > _UIBoard.Count || mineAmount < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(mineAmount),
            "Mine amount is greater than the number of cells");

            var minePositions = new Vector2Int();
            for(var i = 0; i < mineAmount; i++) {
                do {
                    minePositions.x = Random.Range(0, _width);
                    minePositions.y = Random.Range(0, _height);
                } while (_UIBoard[minePositions].GetComponent<GameCell>().HasMine);

                _UIBoard[minePositions].GetComponent<GameCell>().SetMine();
            }
        }

        public void Reset() {
            foreach (var cellGO in _UIBoard.Values) {
                cellGO.GetComponent<GameCell>().Reset();
            }
            
            SetupMines(_mines);
        }

        private List<Vector2Int> GetNeighbourPositions(Vector2Int position) {
            var neighbourPositions = new List<Vector2Int> {
                new Vector2Int(position.x - 1, position.y - 1),
                new Vector2Int(position.x - 1, position.y),
                new Vector2Int(position.x - 1, position.y + 1),
                new Vector2Int(position.x, position.y - 1),
                new Vector2Int(position.x, position.y + 1),
                new Vector2Int(position.x + 1, position.y - 1),
                new Vector2Int(position.x + 1, position.y),
                new Vector2Int(position.x + 1, position.y + 1)
            };
            
            var validNeighbours = neighbourPositions
                .Where(neighbourPosition => _UIBoard
                    .ContainsKey(neighbourPosition))
                .ToList();

            return validNeighbours;
        }
    }
}
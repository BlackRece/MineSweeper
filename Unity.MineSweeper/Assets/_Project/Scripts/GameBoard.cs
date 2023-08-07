using System;
using System.Collections.Generic;
using System.Linq;

using BlackRece.Core;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;

namespace BlackRece.MineSweeper {
    public sealed class GameBoard : MonoBehaviour {
        // game state enum
        private enum GameState {
            Playing = 0,
            Won = 1,
            Lost = 2
        }
        
        [SerializeField] private GameObject _cellPrefab = null;
        [SerializeField] private GameObject _minePrefab = null;
        
        [SerializeField] [Range(10, 500)] private int _width, _height; 
        private IntSize _size = null;
        
        // TODO: move to a UI class
        [SerializeField] private TMP_InputField _mineCountInput = null;
        [SerializeField] private Slider _mineCountSlider = null;
        [SerializeField] private TMP_Text _gameStateText = null;

        [SerializeField] private int _mines;
        private const int MAX_MINES = 100;
        
        private Board _board;
        private Dictionary<Vector2Int, GameObject> _boardGOs;
        private Dictionary<Vector2Int, GameObject> _mineGOs;
        private GameState _gameState;

        private float _clickDelay;
        private float _clickTimer;
        
        private void Awake() {
            _size = new IntSize(_width, _height);
            _boardGOs = new Dictionary<Vector2Int, GameObject>();
            _mineGOs = new Dictionary<Vector2Int, GameObject>();

            _clickDelay = 0.1f;
            _clickTimer = 0f;
            
            if (_cellPrefab == null)
                throw new ArgumentNullException(nameof(_cellPrefab),"Missing Cell prefab");
            
            _gameStateText.gameObject.SetActive(false);
        }

        private void Start() {
            _board = new Board(_size);
            
            _mineCountInput.text = _mines.ToString();

            RenderGameBoard();
            SetupMines(_mines);
            
            _gameState = GameState.Playing;

            GameEvents.EvtMineCount += GetMineCount;
            GameEvents.EvtRevealCell += RevealNeighbours;
            GameEvents.EvtRevealMines += RevealMines;
            GameEvents.EvtFlagCell += ToggleFlag;
            GameEvents.EvtResetBoard += ResetBoard;
        }

        private void Update() {
            if(_clickTimer > 0f)
                _clickTimer -= Time.deltaTime;
            
            UI_DisplayGameState();

            // TODO: Expensive, but works. Find a better way to do this
            if(_gameState == GameState.Playing)
                CheckBoard();
        }
        
        private void ToggleFlag(Vector2Int obj) {
            if(_clickTimer > 0f)
                return;

            _clickTimer = _clickDelay;
            _boardGOs[obj].GetComponent<GameCell>()
                .ToggleFlag();
        }

        private void RenderGameBoard() {
            foreach (Vector2Int boardPosition in _board.GetCellPositions) {
                var gamePosition = new Vector3(boardPosition.x, 0f, boardPosition.y);
                
                var cellGO = Instantiate(_cellPrefab, transform);
                cellGO.transform.position = gamePosition;
                _boardGOs.Add(boardPosition, cellGO);
                
                var cell = cellGO.GetComponent<GameCell>();
                cell.SetBoardPosition(boardPosition);

                if (!_mineGOs.ContainsKey(boardPosition)) {
                    var mineGO = Instantiate(_minePrefab, transform);
                    mineGO.transform.position = gamePosition;
                    _mineGOs.Add(boardPosition, mineGO);
                }
                _mineGOs[boardPosition].SetActive(false);
            }
        }

        // Show cell on grid - called when player clicks on cell
        private int GetMineCount(Vector2Int position) {
            var mineCount = 0;

            foreach (var cellPos in GetNeighbourPositions(position)) {
                if(_boardGOs[cellPos].GetComponent<GameCell>().HasMine)
                    mineCount++;
            }
            
            return mineCount;
        }
        
        private void RevealNeighbours(Vector2Int position) {
            var neighbourPositions = GetNeighbourPositions(position);
            foreach (var cellPosition in neighbourPositions) {
                var cell = _boardGOs[cellPosition].GetComponent<GameCell>();
                if(cell.IsRevealed || cell.HasMine || cell.IsFlagged)
                    continue;
                
                cell.SetMineCount(GetMineCount(cellPosition));
                cell.RevealCell();
            }
        }
        
        private void CheckBoard() {
            var unrevealedCells = _boardGOs.Values
                .Select(cell => cell.GetComponent<GameCell>())
                .Where(cell => !cell.IsRevealed)
                .ToList();

            if (unrevealedCells.Count == _mines) 
                _gameState = GameState.Won;
        }
        
        private void RevealMines() {
            foreach (GameObject cellGO in _boardGOs.Values) {
                var cell = cellGO.GetComponent<GameCell>();
                if(cell.HasMine) {
                    cellGO.SetActive(false);
                    _mineGOs[cell.BoardPosition].SetActive(true);
                    continue;
                }
                
                cell.RevealCell();
            }
            
            _gameState = GameState.Lost;
        }

        private void SetupMines(int mineAmount) {
            if (mineAmount > _boardGOs.Count || mineAmount < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(mineAmount),
            "Mine amount is greater than the number of cells");

            foreach (GameObject mineGO in _mineGOs.Values) 
                mineGO.SetActive(false);
            
            var minePositions = new Vector2Int();
            for(var i = 0; i < mineAmount; i++) {
                do {
                    minePositions.x = Random.Range(0, _width);
                    minePositions.y = Random.Range(0, _height);
                } while (_boardGOs[minePositions].GetComponent<GameCell>().HasMine);

                _boardGOs[minePositions].GetComponent<GameCell>().SetMine();
            }
        }

        public void ResetBoard() {
            foreach (var cellGO in _boardGOs.Values) {
                cellGO.GetComponent<GameCell>().Reset();
                cellGO.SetActive(true);
            }

            SetupMines(_mines);
            _gameState = GameState.Playing;
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
                .Where(neighbourPosition => _boardGOs
                    .ContainsKey(neighbourPosition))
                .ToList();

            return validNeighbours;
        }

        // TODO: move to a UI class
        private void UI_MineCount(int mineCount) {
            _mines = mineCount < 1 || mineCount >= _boardGOs.Count 
                ? _mines 
                : mineCount;
            
            _mineCountInput.text = _mines.ToString();
            _mineCountSlider.value = _mines;
        }
        
        public void UI_MineInput(string mineCount) {
            if (int.TryParse(mineCount, out var mineAmount)) 
                UI_MineCount(mineAmount);
        }
        
        public void UI_MineSlider(float mineCount) {
            UI_MineCount((int)mineCount);
        }

        private void UI_DisplayGameState() {
            _gameStateText.gameObject.SetActive(_gameState != GameState.Playing);
            
            switch (_gameState) {
                case GameState.Won:
                    _gameStateText.text = "You Win!";
                    break;
                case GameState.Lost:
                    _gameStateText.text = "You Lose!";
                    break;
                default:
                    _gameStateText.text = "";
                    break;
            }
        }
    }
}
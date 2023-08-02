using System;

using BlackRece.Core;

using TMPro;

using UnityEngine;

namespace BlackRece.MineSweeper {
    public sealed class GameCell : MonoBehaviour {
        private const float _minePercent = 7f / 100f;
        private readonly Color _eight = Color.red;
        private readonly Color _one = Color.green;
        private readonly Color _black = Color.black;
        
        [SerializeField] private TMP_Text _label;
        
        private Vector2Int _boardPosition;
        public Vector2Int BoardPosition => _boardPosition;
        private int _nearbyMines;
        public int MineCount => _nearbyMines;

        private bool _bIsFlagged;
        public bool IsFlagged => _bIsFlagged;

        private bool _bIsRevealed;
        public bool IsRevealed => _bIsRevealed;

        private bool _bHasMine;
        private Renderer _renderer;
        public bool HasMine => _bHasMine;
        
        public void SetMine() => _bHasMine = true;
        
        public void SetBoardPosition(Vector2Int position) => _boardPosition = position;

        public void SetMineCount(int mineCount) => _nearbyMines = mineCount;

        private void Awake() {
            _renderer = gameObject.GetComponent<Renderer>();
            
            Reset();
        }

        private void Update() {
            if (Input.GetMouseButtonDown(1)) 
                OnRightMouseButtonDown();
        }

        private void OnMouseDown() {
            if(_bIsRevealed || _bIsFlagged)
                return;
            
            
            // get mine count
            _nearbyMines = GameEvents.OnMineCount(_boardPosition);
            
            if (_bHasMine) 
                ShowMine();
            else
                RevealCell();
        }
        
        private void OnRightMouseButtonDown() {
            Vector3 mousePos = Input.mousePosition;
            var ray = Camera.main.ScreenPointToRay(mousePos);
            if (!Physics.Raycast(ray, out var hit))
                return;
            
            if (hit.collider.gameObject.TryGetComponent<GameCell>(out var cell))
                GameEvents.OnFlagCell(cell.BoardPosition);
        }

        public void RevealCell() {
            _bIsRevealed = true;
            
            _renderer.material.color = Color.grey;

            if (_nearbyMines == 0) 
                // reveal neighbours
                GameEvents.OnRevealCell(_boardPosition);
            else
                ShowCount();
        }

        private void ShowMine() {
            _label.gameObject.SetActive(_bHasMine);
            _label.text = "*";
            _label.color = _black;
            
            GameEvents.OnRevealMines();
        }
        
        private void ShowCount() {
            _label.gameObject.SetActive(_nearbyMines != 0);
            _label.text = _nearbyMines.ToString();
            _label.color = Color.Lerp(_one, _eight, (float) _nearbyMines / 8);
        }

        public void ToggleFlag() {
            if(_bIsRevealed)
                return;
            
            _bIsFlagged = !_bIsFlagged;
            
            _renderer.material.color = _bIsFlagged ? Color.cyan : Color.white;
            
            _label.gameObject.SetActive(_bIsFlagged);
            _label.color = Color.blue;
            _label.text = _bIsFlagged ? "?" : "";
        }

        public void Reset() {
            _bIsRevealed = false;
            _bIsFlagged = false;
            _bHasMine = false;
            _nearbyMines = 0;
            _label.gameObject.SetActive(false);
            _renderer.material.color = Color.white;
        }
    }
}
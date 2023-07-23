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
        private int _nearbyMines;
        public int MineCount => _nearbyMines;

        private bool _bIsRevealed;
        public bool IsRevealed => _bIsRevealed;

        private bool _bHasMine;
        public bool HasMine => _bHasMine;
        
        public void SetMine() => _bHasMine = true;
        
        public void SetBoardPosition(Vector2Int position) => _boardPosition = position;

        public void SetMineCount(int mineCount) => _nearbyMines = mineCount;

        private void Awake() {
            _bIsRevealed = false;
            _bHasMine = false;
            _nearbyMines = 0;
        }

        private void OnMouseDown() {
            if(_bIsRevealed)
                return;
            
            // get mine count
            _nearbyMines = GameEvents.OnMineCount(_boardPosition);
            
            if (_bHasMine) 
                ShowMine();
            else
                RevealCell();
        }

        public void RevealCell() {
            _bIsRevealed = true;
            
            gameObject.GetComponent<Renderer>().material.color = Color.grey;

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
    }
}
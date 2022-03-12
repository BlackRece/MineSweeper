using BlackRece.Core;

using TMPro;

using UnityEngine;

namespace BlackRece.MineSweeper {
    public sealed class GameCell : MonoBehaviour {
        private const float _minePercent = 7f / 100f;
        private readonly Color _eight = Color.red;
        private readonly Color _one = Color.green;
        
        private TMP_Text _label;
        
        private Vector2Int _boardPosition;

        private Cell _cell;
        public bool IsRevealed => _cell.IsRevealed;
        public bool IsMine => _cell.IsMine;
        

        private void Awake() {
            _cell = new Cell();
            _label = GetComponent<TMP_Text>();
        }

        private void OnMouseDown() {
            if(IsRevealed)
                return;
            
            GameEvents.OnCellClick(_boardPosition);
        }

        public void ShowMineCount() {
            var mineRating = _minePercent * _cell.NearbyMines; 

            _label.enabled = true;
            if (_cell.IsMine) {
                _label.color = _eight;
                _label.text = "*";
                return;
            }
            
            var result = Color.Lerp(_one, _eight, mineRating);
            _label.text = _cell.NearbyMines.ToString();
            _label.color = result;
        }

        public void SetBoardPosition(Vector2Int position) {
            _boardPosition = position;
        }
    }
}
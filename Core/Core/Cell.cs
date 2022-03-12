using System.Collections.Generic;

namespace BlackRece.Core {
    public sealed class Cell {
        private bool _isMine;
        public bool IsMine {
            get => _isMine;
            set => _isMine = value;
        }

        private bool _isRevealed;
        public bool IsRevealed => _isRevealed;

        private int _nearbyMines;
        public int NearbyMines => _nearbyMines;
        
        public Cell() {
            _isMine = false;

            _nearbyMines = -1;
            _isRevealed = false;
        }

        public void Show() => _isRevealed = true;
        
        public int MinesNearby(List<Cell> cells) {
            _nearbyMines = 0;
            
            foreach (var cell in cells) {
                if(!cell.IsMine)
                    continue;

                _nearbyMines++;
            }

            return _nearbyMines;
        }
    }
}
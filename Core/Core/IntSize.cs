using System;

namespace BlackRece.Core {
    [Serializable]
    public sealed class IntSize {
        private int _height;
        public int Height {
            get => _height;
            set => _height = value;
        }

        private int _width;
        public int Width {
            get => _width;
            set => _width = value;
        }

        public IntSize() { _width = 0; _height = 0; }
        public IntSize(int width, int height) {
            _width = width;
            _height = height;
        }
    }
}
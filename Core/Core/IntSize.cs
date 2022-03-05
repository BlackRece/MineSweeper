using System;

namespace Core {
    [Serializable]
    public sealed class IntSize {
        private int _width, _height;

        public int Height {
            get => _height;
            set => _height = value;
        }

        public int Width {
            get => _width;
            set => _width = value;
        }

        public IntSize() { }
        public IntSize(int width, int height) {
            _width = width;
            _height = height;
        }
        
    }
}
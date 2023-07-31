using System;

using UnityEngine;

namespace BlackRece.MineSweeper {
    public sealed class GameEvents : ScriptableObject {
        public static event Func<Vector2Int, int> EvtMineCount;
        public static event Action<Vector2Int> EvtRevealCell;
        public static event Action EvtRevealMines;
        public static event Action<Vector2Int> EvtFlagCell; 

        public static int OnMineCount(Vector2Int position) => EvtMineCount?.Invoke(position) ?? 0;
        public static void OnRevealCell(Vector2Int position) => EvtRevealCell?.Invoke(position);
        public static void OnRevealMines() => EvtRevealMines?.Invoke();
        public static void OnFlagCell(Vector2Int position) => EvtFlagCell?.Invoke(position);
    }
}
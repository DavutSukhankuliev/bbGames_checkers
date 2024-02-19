using System;
using UnityEngine;

namespace Checkers.Board
{
    [CreateAssetMenu(fileName = "BoardConfig", menuName = "Checkers/BoardConfig", order = 0)]
    public class BoardConfig : ScriptableObject
    {
        [SerializeField] private int _x;
        [SerializeField] private int _y;
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Material _cellMaterial;

        public int X => _x;
        public int Y => _y;
        public Mesh Mesh => _mesh;
        public Material CellMaterial => _cellMaterial;

        private void OnValidate()
        {
            if (_x < 0) _x = Math.Abs(_x);
            if (_y < 0) _y = Math.Abs(_y);
        }
    }
}
using Unity.VisualScripting;
using UnityEngine;

namespace Checkers.Board
{
    public class BoardController
    {
        private readonly BoardCellView.Pool _pool;
        private readonly BoardConfig _config;

        private BoardCellView[,] _cells;
        private Mesh _mesh;

        public BoardController(
            BoardCellView.Pool pool,
            BoardConfig config)
        {
            _pool = pool;
            _config = config;

            SpawnAllCells(1, _config.X, _config.Y);
        }

        private void SpawnAllCells(float cellSize, int countX, int countY)
        {
            _cells = new BoardCellView[_config.X, _config.Y];
            for (int x = 0; x < countX; x++)
                for (int y = 0; y < countY; y++)
                    _cells[x, y] = SpawnSingleTile(cellSize, x, y);
        }

        private BoardCellView SpawnSingleTile(float cellSize, int x = 0, int y = 0)
        {
            var protocol = new SceneObjectProtocol(new Vector3(x, 0, y));
            var view = _pool.Spawn(protocol);
            view.name = $"X: {x} Y: {y}";

            if (_mesh == null)
                _mesh = new Mesh();

            view.MeshFilter.mesh = _mesh;
            view.MeshRenderer.material = _config.CellMaterial;

            Vector3[] vertices = new Vector3[4];
            vertices[0] = new Vector3(cellSize, 0, cellSize);
            vertices[1] = new Vector3(cellSize, 0, cellSize + 1);
            vertices[2] = new Vector3(cellSize + 1, 0, cellSize);
            vertices[3] = new Vector3(cellSize + 1, 0, cellSize + 1);

            int[] tris = new int[] { 0, 1, 2, 1, 3, 2 };

            _mesh.vertices = vertices;
            _mesh.triangles = tris;
            
            _mesh.RecalculateNormals();

            view.BoxCollider = view.AddComponent<BoxCollider>();
            
            return view;
        }
    }
}
using UnityEngine;
using Zenject;

namespace Checkers.Board
{
    public class BoardInstaller : MonoInstaller<BoardInstaller>
    {
        [SerializeField] private BoardCellView _prefab;
        [SerializeField] private BoardConfig _config;
        
        public override void InstallBindings()
        {
            Container
                .Bind<BoardConfig>()
                .FromInstance(_config)
                .AsSingle();

            Container
                .BindMemoryPool<BoardCellView, BoardCellView.Pool>()
                .FromComponentInNewPrefab(_prefab)
                .UnderTransformGroup("Board");

            Container
                .Bind<BoardController>()
                .AsSingle()
                .NonLazy();
        }
    }
}
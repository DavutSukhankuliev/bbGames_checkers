using System;
using UnityEngine;
using Zenject;

namespace Checkers.Board
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class BoardCellView : MonoBehaviour, IDisposable
    {
        [field: SerializeField] public MeshFilter MeshFilter { get; set; }
        [field: SerializeField] public MeshRenderer MeshRenderer { get; set; }
        [field: NonSerialized] public BoxCollider BoxCollider { get; set; }


        private IMemoryPool _pool;

        private void OnValidate()
        {
            if (MeshFilter != null || MeshRenderer != null)
                return;
            
            MeshFilter = GetComponent<MeshFilter>();
            MeshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnDespawned()
        {
            transform.position = Vector3.zero;
            gameObject.SetActive(false);
        }

        private void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
        }

        private void ReInit(SceneObjectProtocol protocol = new SceneObjectProtocol())
        {
            gameObject.SetActive(true);
            transform.localPosition = protocol.Position;
        }

        public void Dispose()
        {
            _pool = null;
        }
        
        public class Pool : MemoryPool<SceneObjectProtocol, BoardCellView>
        {
            protected override void Reinitialize(SceneObjectProtocol protocol, BoardCellView item)
            {
                item.ReInit(protocol);
            }

            protected override void OnDespawned(BoardCellView item)
            {
                item.OnDespawned();
                base.OnDespawned(item);
            }

            protected override void OnSpawned(BoardCellView item)
            {
                base.OnSpawned(item);
                item.OnSpawned(this);
            }
        }
    }
}

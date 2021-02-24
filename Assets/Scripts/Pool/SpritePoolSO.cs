using UnityEngine;
using System;
using System.Collections;
using Bolza.Factory;

namespace Bolza.Pool {

    public class SpritePoolSO : PoolSO<GameObject> {
        [SerializeField]
        private SpriteFactorySO _factory;

        public override IFactory<GameObject> Factory {
            get {
                return _factory;
            }
            set {
                _factory = value as SpriteFactorySO;
            }
        }

        private Transform _poolRoot;
        private Transform PoolRoot {
            get {
                if (_poolRoot == null) {
                    _poolRoot = new GameObject(name).transform;
                    // _poolRoot.SetParent(_parent);
                }
                return _poolRoot;
            }
        }

        public override GameObject Request() {
            GameObject member = base.Request();
            member.gameObject.SetActive(true);
            return member;
        }

        public override void Return(GameObject member) {
            member.transform.SetParent(PoolRoot.transform);
            member.gameObject.SetActive(false);
            base.Return(member);

        }

        protected override GameObject Create() {
            GameObject newMember = base.Create();
            newMember.transform.SetParent(PoolRoot.transform);
            newMember.gameObject.SetActive(false);
            return newMember;
        }

        public override void OnDisable() {
            base.OnDisable();
            if (_poolRoot != null) {
#if UNITY_EDITOR
                DestroyImmediate(_poolRoot.gameObject);
#else
				Destroy(_poolRoot.gameObject);
#endif
            }
        }
    }
}

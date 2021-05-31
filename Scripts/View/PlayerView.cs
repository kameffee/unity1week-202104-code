using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace kameffee.unity1week202104.View
{
    public class PlayerView : MonoBehaviour, IPlayerView, IBaseCampConsumer
    {
        [SerializeField]
        private Rigidbody2D rigidbody;

        [SerializeField]
        private Collider2D collider2D;

        [SerializeField]
        private PlayerSettings playerSettings;

        [SerializeField]
        private Light2D light2D;

        public IObservable<IItemView> OnHitPickUpItem => onHitPickUpItem;
        private readonly Subject<IItemView> onHitPickUpItem = new Subject<IItemView>();

        public IObservable<IItemView> OnFocusItem =>
            this.OnTriggerEnter2DAsObservable()
                .Select(collider2D => collider2D.gameObject.GetComponent<IItemView>())
                .Where(itemView => itemView != null);

        public IObservable<IItemView> OnEnterItem =>
            rigidbody.OnTriggerEnter2DAsObservable()
                .Select(collider => collider.GetComponent<IItemView>())
                .Where(view => view != null);

        public IObservable<IItemView> OnExitItem =>
            rigidbody.OnTriggerExit2DAsObservable()
                .Select(collider => collider.GetComponent<IItemView>())
                .Where(view => view != null);

        public IReadOnlyReactiveProperty<bool> OnLightArea => onLightArea;
        private ReactiveProperty<bool> onLightArea = new ReactiveProperty<bool>();

        public IObservable<IBaseCampView> OnEnterBaseCamp => onEnterBaseCamp;
        private readonly Subject<IBaseCampView> onEnterBaseCamp = new Subject<IBaseCampView>();

        public IObservable<IBaseCampView> OnExitBaseCamp => onExitBaseCamp;
        private readonly Subject<IBaseCampView> onExitBaseCamp = new Subject<IBaseCampView>();

        /// 地面についているか
        public IReadOnlyReactiveProperty<bool> IsGround => isGround;

        private readonly ReactiveProperty<bool> isGround = new ReactiveProperty<bool>();

        void IBaseCampConsumer.OnEnterBaseCamp(IBaseCampView baseCampView) => onEnterBaseCamp.OnNext(baseCampView);

        void IBaseCampConsumer.OnExitBaseCamp(IBaseCampView baseCampView) => onExitBaseCamp.OnNext(baseCampView);

        private void Start()
        {
            rigidbody.OnCollisionExit2DAsObservable()
                .Subscribe(collision2D => CheckOnGround(collision2D)).AddTo(this);

            collider2D.OnTriggerEnter2DAsObservable()
                .Where(collider2D1 => collider2D1.CompareTag("LightArea"))
                .Subscribe(_ => onLightArea.Value = true)
                .AddTo(this);
            collider2D.OnTriggerExit2DAsObservable()
                .Where(collider2D1 => collider2D1.CompareTag("LightArea"))
                .Subscribe(_ => onLightArea.Value = false)
                .AddTo(this);
        }

        [Serializable]
        public class RayCastData
        {
            public float distance = 0.5f;

            public Vector3 rayPosition;

            public Vector2 direction = Vector2.down;
        }

        [Header("Ray settings")]
        [SerializeField]
        private LayerMask layerMask;

        [SerializeField]
        private List<RayCastData> raycastList;

        private RaycastHit2D hit; // レイが何かに当たった時の情報
        private Ray2D ray;

        void Update()
        {
            isGround.Value = CheckGround();
        }

        public bool CheckGround()
        {
            foreach (var rayCastData in raycastList)
            {
                hit = Physics2D.Raycast(
                    transform.position + rayCastData.rayPosition,
                    rayCastData.direction,
                    rayCastData.distance,
                    layerMask);

                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Ground"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = isGround.Value ? Color.red : Color.green;

            foreach (var rayCastData in raycastList)
            {
                Gizmos.DrawRay(transform.position + rayCastData.rayPosition,
                    rayCastData.direction.normalized * rayCastData.distance);
            }
        }

        public void Move(Vector2 vector)
        {
            rigidbody.velocity = new Vector2(vector.x * playerSettings.moveSpeed, rigidbody.velocity.y);
        }

        public void Jump()
        {
            var vector = rigidbody.velocity;
            vector.y += playerSettings.jumpPower;
            rigidbody.velocity = vector;
        }

        private void CheckOnGround(Collision2D collision2D)
        {
            bool onGround = false;
            foreach (var contactPoint in collision2D.contacts)
            {
                if (contactPoint.rigidbody.CompareTag("Ground"))
                {
                    onGround = true;
                    break;
                }
            }

            isGround.Value = onGround;
        }

        public void SetPosition(Vector3 position) => transform.position = position;
        public void SetActiveLight(bool isActive) => light2D.enabled = isActive;
    }
}
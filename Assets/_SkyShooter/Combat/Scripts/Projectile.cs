using SkyShooter.Enemies;
using UnityEngine;

namespace SkyShooter.Combat
{
    /// <summary>
    /// 移動区間を球で検査して高速弾の衝突を判定する。 | 이동 구간을 구 형태로 검사해 고속탄 충돌을 판정한다.
    /// 弾本体にはCollider/Rigidbodyを付けない。 | 총알 본체에는 Collider/Rigidbody를 붙이지 않는다.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class Projectile : MonoBehaviour
    {
        [SerializeField, Min(0.001f)]
        [Tooltip("衝突半径（ワールド単位）。直径0.3の球なら0.15。 | 충돌 반경(월드 단위). 지름 0.3인 구는 0.15.")]
        private float hitRadius = 0.15f;

        [SerializeField]
        private LayerMask hitLayers = Physics.DefaultRaycastLayers;

        private Transform owner;
        private Vector3 direction;
        private int damage;
        private float speed;
        private float remainingRange;
        private bool initialized;
        private bool consumed;

        public void Initialize(Transform shooter, Vector3 shotDirection, int shotDamage,
            float shotSpeed, float range)
        {
            owner = shooter;
            direction = shotDirection.normalized;
            damage = shotDamage;
            speed = shotSpeed;
            remainingRange = range;
            initialized = true;
        }

        private void Update()
        {
            if (consumed || Time.deltaTime <= 0f)
            {
                return;
            }

            if (!initialized || remainingRange <= 0f || direction.sqrMagnitude < 0.001f)
            {
                Destroy(gameObject);
                return;
            }

            float distance = Mathf.Min(speed * Time.deltaTime, remainingRange);
            if (distance <= 0f)
            {
                return;
            }

            // 結果の順番に依存せず、最も近い有効な衝突を選ぶ。 | 결과 순서에 의존하지 않고 가장 가까운 유효 충돌을 선택한다.
            RaycastHit[] hits = Physics.SphereCastAll(transform.position,
                Mathf.Max(0.001f, hitRadius), direction, distance,
                hitLayers, QueryTriggerInteraction.Ignore);
            Collider closestCollider = null;
            float closestDistance = float.PositiveInfinity;

            foreach (RaycastHit hit in hits)
            {
                if (IsIgnored(hit.collider) || hit.distance >= closestDistance)
                {
                    continue;
                }

                closestCollider = hit.collider;
                closestDistance = hit.distance;
            }

            if (closestCollider != null)
            {
                // 初期重なりの場合hit.pointは使わず、距離で位置を計算する。 | 초기 겹침에서는 hit.point 대신 거리로 위치를 계산한다.
                transform.position += direction * Mathf.Max(0f, closestDistance);
                Hit(closestCollider);
                return;
            }

            transform.position += direction * distance;
            remainingRange -= distance;
            if (remainingRange <= 0f)
            {
                Destroy(gameObject);
            }
        }

        private bool IsIgnored(Collider other)
        {
            if (other == null || other.transform == transform || other.transform.IsChildOf(transform))
            {
                return true;
            }

            // 発射したPlayerと、その子オブジェクトには当てない。 | 발사한 Player와 그 자식 오브젝트는 제외한다.
            return owner != null && (other.transform == owner || other.transform.IsChildOf(owner));
        }

        private void Hit(Collider other)
        {
            if (consumed)
            {
                return;
            }

            consumed = true;
            EnemyStats enemy = other.GetComponentInParent<EnemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // 敵にも壁にも当たった時点で弾を消す。 | 적 또는 벽에 닿으면 총알을 제거한다.
            Destroy(gameObject);
        }
    }
}

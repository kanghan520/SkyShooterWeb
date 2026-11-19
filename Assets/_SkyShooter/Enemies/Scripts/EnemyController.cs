using UnityEngine;
using SkyShooter.Player;

namespace SkyShooter.Enemies
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterController), typeof(EnemyStats))]
    public sealed class EnemyController : MonoBehaviour
    {
        [SerializeField, Min(0.01f)]
        private float moveSpeed = 2f;

        [SerializeField, Min(0f)]
        private float stoppingDistance = 1.1f;

        [SerializeField, Min(0.01f)]
        private float entryArrivalDistance = 0.25f;

        [SerializeField, Min(0f)]
        private float gravity = 20f;

        private CharacterController controller;
        private EnemyStats stats;
        private Transform player;

        private Vector3 entryPosition;
        private bool entering;
        private bool initialized;
        private float verticalSpeed;

        // 攻撃力 | 공격력
        [SerializeField, Min(1)]
        private int attackDamage = 10;

        // 攻撃範囲 | 공격 범위
        [SerializeField, Min(0f)]
        private float attackDistance = 1.3f;

        private PlayerStats playerStats;
        
        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            stats = GetComponent<EnemyStats>();
        }

        public void Initialize(
            Transform target, Vector3 gateEntryPosition)
        {
            player = target;
            playerStats = target.GetComponentInParent<PlayerStats>();
            entryPosition = gateEntryPosition;

            entering = true;
            initialized = true;
            verticalSpeed = 0f;
        }

        private void Update()
        {
            if (!initialized || !controller.enabled
                || stats.IsDead || Time.deltaTime <= 0f)
            {
                return;
            }

            Vector3 offset = Vector3.zero;
            float stopAt = stoppingDistance;

            if (entering)
            {
                // 最初は門の内側へ移動する。 | 처음에는 문 안쪽으로 이동한다.
                offset = entryPosition - transform.position;
                offset.y = 0f;

                stopAt = entryArrivalDistance;

                if (offset.magnitude <= entryArrivalDistance + 0.02f)
                {
                    entering = false;
                }
            }

            if (!entering)
            {
                // 進入後はプレイヤーを追跡する。 | 진입 후 플레이어를 추적한다.
                offset = player != null
                    ? player.position - transform.position
                    : Vector3.zero;

                offset.y = 0f;
                stopAt = stoppingDistance;
            }

            float distance = offset.magnitude;
            Vector3 displacement = Vector3.zero;

            if (distance > stopAt)
            {
                Vector3 direction = offset / distance;

                // 目的地を通り越さない。 | 목적지를 지나치지 않게 한다.
                float moveDistance = Mathf.Min(
                    moveSpeed * Time.deltaTime,
                    distance - stopAt);

                displacement = direction * moveDistance;

                transform.rotation = Quaternion.LookRotation(
                    direction, Vector3.up);
            }

            if (controller.isGrounded && verticalSpeed < 0f)
            {
                verticalSpeed = -2f;
            }

            verticalSpeed -= gravity * Time.deltaTime;
            displacement.y = verticalSpeed * Time.deltaTime;

            controller.Move(displacement);
            // 攻撃範囲の判定 | 공격 범위 판정
            if (!entering && player != null && playerStats != null)
            {
                Vector3 attackOffset = player.position - transform.position;
                attackOffset.y = 0f;

                if (attackOffset.magnitude <= attackDistance)
                {
                    playerStats.TakeDamage(attackDamage);
                }
            }
        }
    }
}
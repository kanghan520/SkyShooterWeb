using UnityEngine;

namespace SkyShooter.Player
{
    [DisallowMultipleComponent]
    public sealed class PlayerStats : MonoBehaviour
    {
        // 最大HP | 최대 HP
        [SerializeField, Min(1)]
        private int maxHp = 100;

        // 現在のHP | 현재 HP
        [SerializeField]
        private int currentHp;

        // 無敵時間 | 무적 시간
        [SerializeField, Min(0f)]
        private float invincibleTime = 1f;

        private float nextDamageTime;
        private bool pausedByDeath;

        public int MaxHp => maxHp;
        public int CurrentHp => currentHp;
        public bool IsDead => currentHp <= 0;

        private void Awake()
        {
            currentHp = maxHp;
        }

        // ダメージ処理 | 피해 처리
        public void TakeDamage(int damage)
        {
            if (IsDead || damage <= 0 ||
                Time.time < nextDamageTime)
            {
                return;
            }

            currentHp = Mathf.Max(0, currentHp - damage);

            nextDamageTime = Time.time + invincibleTime;

            Debug.Log($"HP: {currentHp}/{maxHp}");

            if (IsDead)
            {
                Die();
            }
        }

        // ゲーム終了 | 게임 종료
        private void Die()
        {
            Debug.Log("GAME OVER");

            pausedByDeath = true;
            Time.timeScale = 0f;
        }

        private void OnDestroy()
        {
            if (pausedByDeath)
            {
                Time.timeScale = 1f;
            }
        }
    }
}
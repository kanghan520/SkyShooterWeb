using UnityEngine;

namespace SkyShooter.Enemies
{
    /// <summary>
    /// テスト用の敵HPと撃破を処理する。 | 테스트 적의 HP와 처치를 처리한다.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class EnemyStats : MonoBehaviour
    {
        [SerializeField, Min(1)]
        private int maxHp = 3;

        // 再生中にInspectorで確認できる。 | 재생 중 Inspector에서 확인할 수 있다.
        [SerializeField]
        private int currentHp;

        public int CurrentHp => currentHp;
        public bool IsDead { get; private set; }

        private void Awake()
        {
            maxHp = Mathf.Max(1, maxHp);
            currentHp = maxHp;
            IsDead = false;
        }

        public void TakeDamage(int damage)
        {
            if (IsDead || damage <= 0)
            {
                return;
            }

            currentHp = Mathf.Max(0, currentHp - damage);
            Debug.Log($"敵に命中！ 残りHP: {currentHp}/{maxHp}", this);
            if (currentHp == 0)
            {
                IsDead = true;
                Debug.Log("敵を倒しました！", this);
                Destroy(gameObject);
            }
        }

        // TODO: スコア・ドロップ・敵の移動を後の段階で接続する。 | 점수·아이템 드롭·적 이동은 이후 연결한다.
    }
}

using SkyShooter.Combat;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyShooter.Player
{
    /// <summary>
    /// Q長押しで基本攻撃を連射する。 | Q를 누르고 있으면 기본 공격을 연사한다.
    /// Player本体に追加する。 | Player 본체에 추가한다.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerController))]
    public sealed class PlayerCombat : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Projectウィンドウの弾プレハブを指定。 | 프로젝트 창의 총알 프리팹을 지정.")]
        private Projectile projectilePrefab;

        [SerializeField, Min(0.01f)]
        [Tooltip("Qの発射間隔（秒） | Q 발사 간격(초)")]
        private float fireInterval = 0.25f;

        [SerializeField, Min(1)]
        [Tooltip("1発の攻撃力 | 한 발의 공격력")]
        private int attack = 1;

        [SerializeField, Min(0.01f)]
        [Tooltip("弾速（Unity単位/秒） | 총알 속도(Unity 단위/초)")]
        private float projectileSpeed = 20f;

        [SerializeField, Min(0.01f)]
        [Tooltip("最大射程（Unity単位） | 최대 사거리(Unity 단위)")]
        private float projectileRange = 12f;

        [SerializeField]
        [Tooltip("Player中心からの発射高さの補正 | Player 중심 기준 발사 높이 보정")]
        private float shotHeightOffset = 0.2f;

        private PlayerController movement;
        private float nextFireTime;

        private void Awake()
        {
            movement = GetComponent<PlayerController>();
            if (projectilePrefab == null)
            {
                Debug.LogWarning("弾プレハブが未設定です。Player CombatのProjectile Prefabに設定してください。", this);
            }
        }

        private void OnEnable()
        {
            nextFireTime = 0f;
        }

        private void LateUpdate()
        {
            // 移動処理の後に向きと位置を読み取る。 | 이동 처리 후 방향과 위치를 읽는다.
            Keyboard keyboard = Keyboard.current;
            if (Time.timeScale <= 0f || !Application.isFocused || keyboard == null
                || !keyboard.qKey.isPressed || projectilePrefab == null)
            {
                return;
            }

            if (Time.time < nextFireTime)
            {
                return;
            }

            Vector3 direction = movement.FacingDirection;
            Vector3 origin = transform.position + Vector3.up * shotHeightOffset;
            Projectile projectile = Instantiate(projectilePrefab, origin,
                Quaternion.LookRotation(direction, Vector3.up));
            projectile.Initialize(transform, direction, Mathf.Max(1, attack),
                Mathf.Max(0.01f, projectileSpeed), Mathf.Max(0.01f, projectileRange));

            // 低FPSでも発射を一度にまとめない。 | 낮은 FPS에서도 밀린 총알을 한꺼번에 발사하지 않는다.
            nextFireTime = Time.time + Mathf.Max(0.01f, fireInterval);
        }

        // TODO: W/E/R、能力値・ゲーム状態との連携を追加する。 | W/E/R, 능력치·게임 상태 연결을 추가한다.
    }
}

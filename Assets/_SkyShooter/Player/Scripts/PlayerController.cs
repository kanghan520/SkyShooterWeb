using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyShooter.Player
{
    /// <summary>
    /// 矢印キーでXZ平面を移動する。 | 방향키로 XZ 평면을 이동한다.
    /// CharacterControllerで床・壁との衝突を処理する。 | CharacterController로 바닥·벽 충돌을 처리한다.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField, Min(0f)]
        [Tooltip("移動速度（Unity単位/秒） | 이동 속도(Unity 단위/초)")]
        private float moveSpeed = 6f;

        [SerializeField, Min(0f)]
        [Tooltip("重力の強さ | 중력 크기")]
        private float gravity = 20f;

        // 今後、Q/W/Eの攻撃方向として参照する。 | 이후 Q/W/E 공격 방향으로 참조한다.
        public Vector3 FacingDirection { get; private set; } = Vector3.forward;

        private CharacterController controller;
        private float verticalSpeed;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            FacingDirection = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
            if (FacingDirection.sqrMagnitude < 0.001f)
            {
                FacingDirection = Vector3.forward;
            }
        }

        private void OnEnable()
        {
            // 再有効化時に落下速度をリセットする。 | 다시 활성화할 때 낙하 속도를 초기화한다.
            verticalSpeed = 0f;
        }

        private void Update()
        {
            if (!controller.enabled || Time.deltaTime <= 0f)
            {
                return;
            }

            Vector3 input = ReadMovement();

            // 斜め移動でも同じ速度にする。 | 대각선 이동도 같은 속도로 만든다.
            Vector3 direction = Vector3.ClampMagnitude(input, 1f);
            if (direction.sqrMagnitude > 0.001f)
            {
                FacingDirection = direction.normalized;
                transform.rotation = Quaternion.LookRotation(FacingDirection, Vector3.up);
            }
            // 入力がない時は向きを変えない。 | 입력이 없으면 바라보는 방향을 유지한다.

            if (controller.isGrounded && verticalSpeed < 0f)
            {
                verticalSpeed = -2f;
            }

            // Move自体には重力がないため、垂直速度を別途計算する。 | Move 자체에는 중력이 없어 수직 속도를 따로 계산한다.
            verticalSpeed -= gravity * Time.deltaTime;
            Vector3 velocity = direction * moveSpeed;
            velocity.y = verticalSpeed;

            // 位置を直接変更せず、衝突付きで移動する。 | 위치를 직접 바꾸지 않고 충돌을 처리하며 이동한다.
            controller.Move(velocity * Time.deltaTime);
        }

        private static Vector3 ReadMovement()
        {
            Keyboard keyboard = Keyboard.current;
            if (keyboard == null || !Application.isFocused)
            {
                return Vector3.zero;
            }

            // Wは散弾用に予約。移動には矢印キーのみを使う。 | W는 산탄용으로 예약. 이동에는 방향키만 사용한다.
            float x = (keyboard.rightArrowKey.isPressed ? 1f : 0f)
                    - (keyboard.leftArrowKey.isPressed ? 1f : 0f);
            float z = (keyboard.upArrowKey.isPressed ? 1f : 0f)
                    - (keyboard.downArrowKey.isPressed ? 1f : 0f);
            return new Vector3(x, 0f, z);
        }
    }
}

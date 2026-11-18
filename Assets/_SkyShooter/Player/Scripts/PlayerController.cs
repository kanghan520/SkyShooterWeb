using UnityEngine;
using UnityEngine.InputSystem;

namespace SkyShooter.Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField, Min(0f)]
        private float moveSpeed = 6f;

        [SerializeField, Min(0f)]
        private float gravity = 20f;

        // 部屋の半分の大きさ（X/Z）。 | 방 크기의 절반(X/Z).
        [SerializeField]
        private Vector2 arenaHalfSize = new Vector2(12f, 8f);

        // 境界の余白。 | 경계에서 확보할 여유 거리.
        [SerializeField, Min(0f)]
        private float boundaryPadding = 0.6f;

        public Vector3 FacingDirection { get; private set; }
            = Vector3.forward;

        private CharacterController controller;
        private float verticalSpeed;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();

            FacingDirection = Vector3.ProjectOnPlane(
                transform.forward, Vector3.up).normalized;

            if (FacingDirection.sqrMagnitude < 0.001f)
            {
                FacingDirection = Vector3.forward;
            }
        }

        private void OnEnable()
        {
            verticalSpeed = 0f;
        }

        private void Update()
        {
            if (!controller.enabled || Time.deltaTime <= 0f)
            {
                return;
            }

            // 斜め移動の速度を補正する。 | 대각선 이동 속도를 보정한다.
            Vector3 direction = Vector3.ClampMagnitude(
                ReadMovement(), 1f);

            if (direction.sqrMagnitude > 0.001f)
            {
                FacingDirection = direction.normalized;

                transform.rotation = Quaternion.LookRotation(
                    FacingDirection, Vector3.up);
            }

            if (controller.isGrounded && verticalSpeed < 0f)
            {
                verticalSpeed = -2f;
            }

            verticalSpeed -= gravity * Time.deltaTime;

            Vector3 desired = transform.position
                + direction * moveSpeed * Time.deltaTime;

            float maxX = Mathf.Max(
                0f, arenaHalfSize.x - boundaryPadding);

            float maxZ = Mathf.Max(
                0f, arenaHalfSize.y - boundaryPadding);

            // プレイヤーを部屋の中に留める。 | 플레이어를 방 안에 유지한다.
            desired.x = Mathf.Clamp(desired.x, -maxX, maxX);
            desired.z = Mathf.Clamp(desired.z, -maxZ, maxZ);

            Vector3 displacement = desired - transform.position;
            displacement.y = verticalSpeed * Time.deltaTime;

            controller.Move(displacement);
        }

        private static Vector3 ReadMovement()
        {
            Keyboard keyboard = Keyboard.current;

            if (keyboard == null || !Application.isFocused)
            {
                return Vector3.zero;
            }

            float x = (keyboard.rightArrowKey.isPressed ? 1f : 0f)
                - (keyboard.leftArrowKey.isPressed ? 1f : 0f);

            float z = (keyboard.upArrowKey.isPressed ? 1f : 0f)
                - (keyboard.downArrowKey.isPressed ? 1f : 0f);

            return new Vector3(x, 0f, z);
        }
    }
}
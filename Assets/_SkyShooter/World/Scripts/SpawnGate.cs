using UnityEngine;

namespace SkyShooter.World
{
    [DisallowMultipleComponent]
    public sealed class SpawnGate : MonoBehaviour
    {
        [SerializeField]
        private Transform spawnPoint;

        [SerializeField]
        private Transform entryPoint;

        [SerializeField, Min(0.1f)]
        private float spawnClearanceRadius = 0.7f;

        public bool IsConfigured =>
            spawnPoint != null && entryPoint != null;

        public Vector3 SpawnPosition => spawnPoint.position;
        public Vector3 EntryPosition => entryPoint.position;

        public bool IsSpawnClear()
        {
            if (!IsConfigured)
            {
                return false;
            }

            // 出現位置が空いているか確認する。 | 생성 위치가 비어 있는지 확인한다.
            return !Physics.CheckSphere(
                spawnPoint.position,
                spawnClearanceRadius,
                Physics.DefaultRaycastLayers,
                QueryTriggerInteraction.Ignore);
        }

        private void OnDrawGizmosSelected()
        {
            if (!IsConfigured)
            {
                return;
            }

            // 黄色は出現位置、緑色は進入先。 | 노란색은 생성 위치, 초록색은 진입 목적지.
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(
                spawnPoint.position, spawnClearanceRadius);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(entryPoint.position, 0.25f);

            Gizmos.DrawLine(
                spawnPoint.position, entryPoint.position);
        }
    }
}
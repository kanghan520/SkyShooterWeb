using System.Collections.Generic;
using SkyShooter.World;
using UnityEngine;

namespace SkyShooter.Enemies
{
    [DisallowMultipleComponent]
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private EnemyController enemyPrefab;

        [SerializeField]
        private Transform player;

        [SerializeField]
        private SpawnGate[] gates = new SpawnGate[4];

        [SerializeField, Min(0.1f)]
        private float spawnInterval = 1.5f;

        [SerializeField, Min(0f)]
        private float initialDelay = 2f;

        [SerializeField, Min(1)]
        private int maxAliveEnemies = 12;

        private readonly List<EnemyController> aliveEnemies =
            new List<EnemyController>();

        private float nextSpawnTime;
        private bool blockedWarningShown;

        private void Start()
        {
            bool hasGate = false;

            if (gates != null)
            {
                foreach (SpawnGate gate in gates)
                {
                    if (gate != null && gate.IsConfigured)
                    {
                        hasGate = true;
                    }
                }
            }

            if (enemyPrefab == null || player == null || !hasGate
                || !enemyPrefab.gameObject.activeSelf)
            {
                Debug.LogError(
                    "敵の出現設定を確認してください。"
                    + "Enemy Prefab、Player、出入口の設定が必要です。",
                    this);

                enabled = false;
                return;
            }

            nextSpawnTime = Time.time + initialDelay;
        }

        private void Update()
        {
            if (Time.timeScale <= 0f || Time.time < nextSpawnTime)
            {
                return;
            }

            nextSpawnTime = Time.time
                + Mathf.Max(0.1f, spawnInterval);

            // 倒された敵をリストから除外する。 | 처치된 적을 목록에서 제외한다.
            aliveEnemies.RemoveAll(enemy => enemy == null);

            if (aliveEnemies.Count >= Mathf.Max(1, maxAliveEnemies)
                || player == null)
            {
                return;
            }

            // 無作為の門から順番に確認する。 | 무작위 문부터 차례로 확인한다.
            int start = Random.Range(0, gates.Length);

            for (int i = 0; i < gates.Length; i++)
            {
                SpawnGate gate =
                    gates[(start + i) % gates.Length];

                if (gate == null || !gate.IsSpawnClear())
                {
                    continue;
                }

                EnemyController enemy = Instantiate(
                    enemyPrefab,
                    gate.SpawnPosition,
                    Quaternion.identity);

                enemy.Initialize(player, gate.EntryPosition);
                aliveEnemies.Add(enemy);

                blockedWarningShown = false;
                return;
            }

            if (!blockedWarningShown)
            {
                Debug.LogWarning(
                    "出現場所が塞がっています。"
                    + "古い壁と出入口の配置を確認してください。",
                    this);

                blockedWarningShown = true;
            }
        }
    }
}
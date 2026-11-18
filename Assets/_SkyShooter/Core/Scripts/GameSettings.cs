using UnityEngine;

namespace SkyShooter.Core
{
    // 初期調整値。各機能への接続は今後実装する。 | 초기 조정값. 각 기능 연결은 이후 구현한다.
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Sky Shooter/ゲーム設定")]
    public sealed class GameSettings : ScriptableObject
    {
        // Unityのワールド単位を使用。元のpx/秒とは異なる。 | Unity 월드 단위 사용. 기존 px/초와 다르다.
        [Min(0.01f)] public float playerMoveSpeed = 6f;
        [Min(1)] public int playerMaxHp = 5;
        [Min(1)] public int initialAttack = 1;
        [Min(1)] public int maxAttack = 5;
        [Min(0f)] public float invincibleSeconds = 1f;
        // Qの連射間隔。スキルのクールタイムとは別管理。 | Q 연사 간격. 스킬 대기시간과 별도 관리.
        [Min(0.01f)] public float basicFireInterval = 0.25f;
        [Min(0.01f)] public float enemySpawnInterval = 1f;
        [Min(1)] public int maxActiveEnemies = 30;
    }
}

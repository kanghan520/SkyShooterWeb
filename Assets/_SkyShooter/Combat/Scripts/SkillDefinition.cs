using UnityEngine;

namespace SkyShooter.Combat
{
    // スキルの設定データのみ。攻撃処理は未実装。 | 스킬 설정 데이터만 담당. 공격 처리는 미구현.
    [CreateAssetMenu(fileName = "SkillDefinition", menuName = "Sky Shooter/スキル設定")]
    public sealed class SkillDefinition : ScriptableObject
    {
        public SkillType type;
        public string displayNameJa = "通常攻撃";
        [Min(0f)] public float damageMultiplier = 1f;
        [Min(0f)] public float cooldownSeconds = 0f;
        // Q/Wの射程、Eの投擲距離。Rはradiusを使用。 | Q/W 사거리, E 투척 거리. R은 radius 사용.
        [Min(0f)] public float range = 10f;
        // E/Rの攻撃半径。 | E/R 공격 반경.
        [Min(0f)] public float radius = 3f;
        [Min(0f)] public float detonationDelaySeconds = 0.5f;
        [Min(1)] public int pelletCount = 5;
        [Range(0f, 180f)] public float spreadAngle = 45f;
    }
}

namespace SkyShooter.UI
{
    // ゲーム画面の基本言語は日本語。キー名はQ/W/E/Rのまま表示する。 | 게임 기본 언어는 일본어. 키는 Q/W/E/R로 표시한다.
    // HUD等への接続は今後実装する。表示文はここで一元管理する。 | HUD 연결은 이후 구현. 표시 문구는 여기서 통합 관리한다.
    public static class JapaneseLabels
    {
        public const string GameTitle = "スカイシューター";
        public const string Start = "ゲーム開始";
        public const string Pause = "一時停止";
        public const string Resume = "再開";
        public const string Retry = "もう一度遊ぶ";
        public const string GameOver = "ゲームオーバー";
        public const string Loading = "読み込み中…";
        public const string ClickToStart = "画面をクリックして開始";
        public const string Hp = "HP";
        public const string Attack = "攻撃力";
        public const string Score = "スコア";
        public const string Level = "レベル";
        public const string Basic = "通常攻撃";
        public const string Shotgun = "散弾";
        public const string Bomb = "爆弾";
        public const string Radial = "範囲攻撃";
        public const string Ready = "使用可能";
        public const string Cooldown = "再使用まで";
        public const string SecondsFormat = "{0:0.0}秒";
        public const string Ranking = "ランキング TOP 5";
        public const string NoRecords = "まだ記録がありません";
        public const string Controls = "移動：矢印キー　攻撃：Q / W / E / R";
    }
}

# 기본 일본어 UI 문구

실제 문구의 기준 파일은 `Assets/_SkyShooter/UI/Scripts/JapaneseLabels.cs`다.
아래 표는 설명용이다. 문구를 바꿀 때 기준 파일도 수정한다. HUD 연결과 폰트는 이후 작업이다.

| 키 | 게임 표시 | 한국어 의미 |
|---|---|---|
| `GameTitle` | スカイシューター | 게임 제목(가제) |
| `Start` | ゲーム開始 | 게임 시작 |
| `Pause` | 一時停止 | 일시정지 |
| `Resume` | 再開 | 재개 |
| `Retry` | もう一度遊ぶ | 다시 플레이 |
| `GameOver` | ゲームオーバー | 게임오버 |
| `Loading` | 読み込み中… | 불러오는 중 |
| `ClickToStart` | 画面をクリックして開始 | 화면을 클릭하여 시작 |
| `Hp` | HP | 체력 |
| `Attack` | 攻撃力 | 공격력 |
| `Score` | スコア | 점수 |
| `Level` | レベル | 레벨 |
| `Basic` | 通常攻撃 | 기본 공격 |
| `Shotgun` | 散弾 | 산탄 |
| `Bomb` | 爆弾 | 폭탄 |
| `Radial` | 範囲攻撃 | 범위 공격 |
| `Ready` | 使用可能 | 사용 가능 |
| `Cooldown` | 再使用まで | 재사용까지 |
| `SecondsFormat` | {0:0.0}秒 | 초 표시 형식 |
| `Ranking` | ランキング TOP 5 | 상위 5개 기록 |
| `NoRecords` | まだ記録がありません | 기록 없음 |
| `Controls` | 移動：矢印キー　攻撃：Q / W / E / R | 조작 안내 |

키 이름 Q/W/E/R과 HP는 식별하기 쉽게 그대로 표시한다. 게임 화면에는 후리가나를 자동으로 넣지 않는다.
SkillDefinition의 displayNameJa에는 해당 공격의 일본어 이름을 설정한다. 일반 상태 문구는 JapaneseLabels를 사용한다.

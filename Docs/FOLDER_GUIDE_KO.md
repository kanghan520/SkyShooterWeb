# 폴더와 파일 안내

플레이어는 Player, 적은 Enemies처럼 기능별로 묶었다. 기능 안에서 Scripts·Prefabs·Models를 나누므로 관련 파일을 한곳에서 찾을 수 있다.
폴더·파일·C# 식별자는 영문, 게임 표시 문구는 일본어, 코드 주석은 일본어 | 한국어를 사용한다.

## 폴더

| 경로 | 역할 |
|---|---|
| `Assets/_SkyShooter/Scenes/` | 타이틀·게임 씬 |
| `Assets/_SkyShooter/Core/Scripts/` | 게임 진행·설정 |
| `Assets/_SkyShooter/Core/Settings/` | 게임 설정 에셋 |
| `Assets/_SkyShooter/Player/Scripts/` | 이동·공격·능력치 |
| `Assets/_SkyShooter/Player/Prefabs/` | 플레이어 프리팹 |
| `Assets/_SkyShooter/Player/Models/` | 플레이어 모델 |
| `Assets/_SkyShooter/Player/Animations/` | 플레이어 애니메이션 |
| `Assets/_SkyShooter/Enemies/Scripts/` | 적 행동·출현 관리 |
| `Assets/_SkyShooter/Enemies/Prefabs/` | 적 프리팹 |
| `Assets/_SkyShooter/Enemies/Models/` | 적 모델 |
| `Assets/_SkyShooter/Enemies/Animations/` | 적 애니메이션 |
| `Assets/_SkyShooter/Combat/Scripts/` | 총알·폭발·스킬 설정 |
| `Assets/_SkyShooter/Combat/Prefabs/` | 총알·폭탄 프리팹 |
| `Assets/_SkyShooter/Combat/Definitions/` | QWER 설정 에셋 |
| `Assets/_SkyShooter/Combat/VFX/` | 공격 이펙트 |
| `Assets/_SkyShooter/World/Scripts/` | 출입구 |
| `Assets/_SkyShooter/World/Prefabs/` | 바닥·벽·출입구 프리팹 |
| `Assets/_SkyShooter/World/Materials/` | 맵 재질 |
| `Assets/_SkyShooter/UI/Scripts/` | HUD·일본어 표시 |
| `Assets/_SkyShooter/UI/Prefabs/` | HP 바·스킬 슬롯 |
| `Assets/_SkyShooter/UI/Icons/` | 스킬 아이콘 |
| `Assets/_SkyShooter/UI/Fonts/` | 일본어 지원 폰트 |
| `Assets/_SkyShooter/Items/Scripts/` | 회복·강화 아이템 |
| `Assets/_SkyShooter/Items/Prefabs/` | 아이템 프리팹 |
| `Assets/_SkyShooter/Save/Scripts/` | 기기 내 기록 |
| `Assets/_SkyShooter/Audio/BGM/` | 배경음악 |
| `Assets/_SkyShooter/Audio/SFX/` | 효과음 |
| `Assets/_SkyShooter/Shared/Materials/` | 공용 재질 |
| `Assets/_SkyShooter/Shared/Textures/` | 공용 텍스처 |

## C# 파일

아래 경로는 `Assets/_SkyShooter/` 기준이다. 빈 클래스의 TODO는 구현할 역할을 나타내며 동작 구현을 의미하지 않는다.

| 파일 | 역할 | 현재 상태 |
|---|---|---|
| `Core/Scripts/GameManager.cs` | 게임 상태·점수·난이도를 관리한다. | 동작 미구현 |
| `Player/Scripts/PlayerController.cs` | 방향키 이동과 바라보는 방향을 담당한다. | 동작 미구현 |
| `Player/Scripts/PlayerCombat.cs` | QWER 입력과 공격 실행을 담당한다. | 동작 미구현 |
| `Player/Scripts/PlayerStats.cs` | 플레이어 HP·공격력·무적 시간을 담당한다. | 동작 미구현 |
| `Enemies/Scripts/EnemyController.cs` | 적의 진입과 이동을 담당한다. | 동작 미구현 |
| `Enemies/Scripts/EnemyStats.cs` | 적 HP·처치 점수를 담당한다. | 동작 미구현 |
| `Enemies/Scripts/EnemySpawner.cs` | 네 방향 출입구에서 적을 생성한다. | 동작 미구현 |
| `World/Scripts/SpawnGate.cs` | 적 출현 위치와 진입 목적지를 나타낸다. | 동작 미구현 |
| `Combat/Scripts/Projectile.cs` | 기본탄·산탄의 이동과 명중을 담당한다. | 동작 미구현 |
| `Combat/Scripts/Bomb.cs` | E의 착탄 지점과 폭발을 담당한다. | 동작 미구현 |
| `Combat/Scripts/AreaAttack.cs` | R의 원형 범위 공격을 담당한다. | 동작 미구현 |
| `UI/Scripts/HUDController.cs` | HP·공격력·점수를 화면에 표시한다. | 동작 미구현 |
| `UI/Scripts/SkillSlotUI.cs` | QWER 아이콘과 남은 시간을 표시한다. | 동작 미구현 |
| `Items/Scripts/ItemPickup.cs` | 회복·공격력 증가 아이템 획득을 담당한다. | 동작 미구현 |
| `Core/Scripts/GamePhase.cs` | 준비·플레이·일시정지·종료 상태 정의 | 정의 포함·게임 연결 전 |
| `Core/Scripts/GameSettings.cs` | 초기 게임 설정 데이터 | 정의 포함·게임 연결 전 |
| `Combat/Scripts/SkillType.cs` | QWER 공격 종류 정의 | 정의 포함·게임 연결 전 |
| `Combat/Scripts/SkillDefinition.cs` | 스킬별 수치와 일본어 이름 데이터 | 정의 포함·게임 연결 전 |
| `UI/Scripts/JapaneseLabels.cs` | 기본 일본어 표시 문구 | 정의 포함·게임 연결 전 |
| `Save/Scripts/RecordStore.cs` | 기기 내 상위 5개 저장 역할 | 동작 미구현 |

## Unity에서 이후 생성할 파일

| 경로 | 생성 시점 |
|---|---|
| `Scenes/Game.unity` | 첫 맵 작업 |
| `Scenes/Title.unity` | 타이틀 화면 분리가 필요해질 때 |
| `Core/Settings/GameSettings.asset` | 초기 수치 설정 |
| `Player/Prefabs/Player.prefab` | 이동·충돌 구성을 저장할 때 |
| `Enemies/Prefabs/Enemy_Basic.prefab` | 기본 적 완성 후 |
| `World/Prefabs/SpawnGate.prefab` | 출입구 완성 후 |
| `Combat/Prefabs/Projectile.prefab` | Q 공격 구현 |
| `Combat/Prefabs/Bomb.prefab` | E 공격 구현 |
| `Combat/Definitions/Q_Basic.asset` 등 4개 | QWER 설정 데이터 생성 |
| `UI/Prefabs/HUD.prefab` | 하단 정보창 구성 |
| `Player/PlayerControls.inputactions` | Input System 키 연결 |

위 파일들은 현재 압축파일에 없다. 이름만 있는 빈 .unity/.prefab/.asset 파일을 만들지 않고 Unity에서 올바른 에셋으로 생성한다.
Input Actions에는 방향키 Move와 Q/W/E/R 공격을 나눈다. W는 산탄이므로 WASD 이동 바인딩은 추가하지 않는다.
빈 폴더의 `.gitkeep`는 Git에서 폴더를 유지하기 위한 표시다. Unity가 만드는 `.meta`는 버전 관리에 함께 보관한다.

## 추가 자료

외부 에셋은 `Assets/ThirdParty` 등 원본 구역에 보관하고 필요할 때 프로젝트에서 참조한다. 출처·라이선스도 함께 기록한다.
현재 ThirdParty 에셋과 일본어 폰트는 포함되어 있지 않다.

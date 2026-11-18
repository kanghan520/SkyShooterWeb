# Sky Shooter — Unity Web 시작 구성

기본 표시 언어: 일본어 / 제작: Unity + C# / 실행 목표: PC 웹브라우저.

## 현재 포함된 범위

폴더 구성, C# 클래스 뼈대, 초기 설정 데이터 클래스, 일본어 표시 문구, 단계별 안내를 포함한다.
이 압축파일은 **새 Unity 프로젝트에 복사하는 소스 시작 구성**이다.
게임 기능, 씬, 프리팹, 모델, 폰트, Input Actions는 아직 구현·생성하지 않았다.
스크립트를 복사하는 것만으로 플레이 가능한 게임이 실행되지는 않는다.
원본 Sky_Shooter.zip의 HTML/Flask/DB 파일은 변경하거나 포함하지 않았다.

## 1. Unity 프로젝트 만들기

1. Unity Hub에서 사용할 Unity 6 버전에 Web 빌드 지원 모듈을 설치한다. 버전에 따라 Web Build Support / WebGL Build Support로 표시될 수 있다.
2. Universal 3D 템플릿으로 새 프로젝트를 만든다. 프로젝트 이름 예: SkyShooterWeb.
3. 초기 프로젝트 생성과 패키지 설정이 끝나면 Unity Editor를 닫는다.
4. 이 압축파일 안의 `Assets/_SkyShooter` 폴더를 새 프로젝트의 `Assets` 폴더 안으로 복사한다.
5. `Docs`와 `README_KO.md`를 새 프로젝트 최상위에 복사한다. `.editorconfig`, `.gitignore`, `.vscode/extensions.json`은 기존 설정이 없다면 복사하고, 있다면 필요한 항목을 병합한다.
6. Unity Hub에서 **방금 Hub로 생성한 프로젝트**를 다시 연다. `Assets/_SkyShooter`가 보이는지 확인한다.

주의: 이 시작 구성 폴더 자체를 Unity Hub의 프로젝트로 추가하지 않는다. 실제 프로젝트의 `Packages`와 `ProjectSettings`는 Unity가 생성하고 관리한다.

## 2. VS Code 연결

- VS Code에 Microsoft의 Unity 확장을 설치한다.
- Unity Package Manager에서 Visual Studio Editor 연동 패키지를 확인한다.
- Unity의 External Tools에서 외부 스크립트 편집기를 Visual Studio Code로 선택한다.
- VS Code에서는 `Assets`만 열지 말고 실제 Unity 프로젝트 최상위 폴더를 연다.
- `.csproj`, `.sln`과 `.meta`는 Unity가 생성한다. 직접 빈 파일로 만들지 않는다.
- 현재 C# 뼈대는 UnityEngine만 참조한다. Input System, uGUI, TextMeshPro 연결은 각 기능 구현 단계에서 진행한다.

## 3. 첫 작업

1. Unity에서 새 씬을 만든 뒤 `Assets/_SkyShooter/Scenes/Game.unity`로 저장한다.
2. 바닥, 벽, 조명, 카메라, 임시 플레이어를 배치한다.
3. `PlayerController.cs`부터 방향키 8방향 이동을 구현한다.
4. 이동이 되면 Web 빌드의 Build And Run으로 브라우저에서 확인한다.
5. Q 기본 공격, 적·출입구, W/E/R 순서로 확장한다.

## 4. 설정 에셋 만들기 — Unity에서 진행

- Project 창 Create → Sky Shooter → ゲーム設定: `Core/Settings/GameSettings.asset`으로 저장한다.
- Create → Sky Shooter → スキル設定: `Combat/Definitions`에 Q_Basic, W_Shotgun, E_Bomb, R_Radial을 생성한다.
- 설정 에셋은 데이터이므로, 이후 스크립트에서 참조를 연결해야 실제 게임에 반영된다.
- Inspector 값은 첫 테스트용이다. GameSettings 이동 속도는 Unity 월드 단위이며 기존 JavaScript의 px/초와 다르다.
- Q는 GameSettings.basicFireInterval로 연사 간격을 관리한다. Q 스킬의 cooldownSeconds는 0으로 둔다.
- W/E/R 대기시간 제안은 각각 4/8/20초다. range·radius·위력은 플레이 테스트로 결정한다.

## 5. 일본어 표시

`UI/Scripts/JapaneseLabels.cs`가 공통 일본어 문구의 기준이다.
일본어 폰트와 TextMeshPro Font Asset은 아직 포함하지 않았다. 기본 라틴 문자 폰트만 사용하면 일본어가 사각형으로 표시될 수 있다.
라이선스를 확인한 일본어 지원 폰트를 `UI/Fonts`에 넣고, Font Asset과 필요한 일본어 글리프 또는 폴백을 설정한다.
브라우저 빌드에서도 메뉴·스킬명·숫자를 확인한다. 실제 게임 문구에는 후리가나를 자동으로 붙이지 않는다.

## 6. 저장과 배포

- 초기 게임 실행에는 Flask가 필수는 아니다. 웹 빌드 파일을 제공하는 정적 호스팅을 사용한다.
- 개인 기록의 브라우저 저장과 온라인 공용 랭킹을 구분한다. 온라인 랭킹을 만들 때 기존 Flask API를 검토한다.
- `Builds/Web`은 나중에 실제 프로젝트에서 생성하는 웹 빌드 출력 경로다. 현재 압축파일에는 빌드가 없다.
- 생성된 HTML 더블클릭 대신 Build And Run 또는 웹서버로 테스트한다. 배포 시 빌드 압축 방식에 맞는 서버 설정도 확인한다.

## 확인 범위

압축파일 무결성과 소스 파일 구성을 확인했다. 이 생성 환경에는 Unity Editor가 없어 Unity 내 임포트·컴파일·브라우저 실행은 아직 검증하지 않았다.
기능 구현 전 뼈대이므로 빈 MonoBehaviour를 오브젝트에 연결해도 게임 동작은 발생하지 않는다.

자세한 폴더별 역할: `Docs/FOLDER_GUIDE_KO.md` / 작업 순서: `Docs/ROADMAP_KO.md` / 일본어 문구: `Docs/UI_TEXT_JA_KO.md`.

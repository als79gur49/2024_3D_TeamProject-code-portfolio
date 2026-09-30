# 딸깍, 건축 — 코드 검토용 포트폴리오 사본

사람과 LLM이 팀 프로젝트의 구조와 구현을 읽기 위한 **실행·빌드 불가능한 소스 스냅샷**입니다. 완전한 Unity 프로젝트가 아니며 Unity로 열어 실행하거나 테스트할 수 없습니다.

- 원본: 비공개 GitHub 저장소 `als79gur49/2024_3D_TeamProject` (저장소 ID `862361414`)
- 기준: `main`, 커밋 `81e231be42c696d0763b8c9700f17e54c16c86f5`
- 해당 커밋의 루트 트리 SHA: `dab46bf44ad59c4d9a194038338ad84c70325d2a`. `branches/main`, `git/refs/heads/main`, Git commit 응답으로 커밋과 트리를 구분해 확인했습니다.
- 로컬 원본: 없음. 연결된 GitHub 읽기 기능으로 선택 파일만 취득했습니다.
- 원본 파일 352개 중 30개 포함: C# 28개, 의존성·에디터 버전 문맥 2개. 322개 제외. 생성 문서는 별도 5개입니다.
- 원본의 README, AGENTS.md, .agents 및 SKILL.md는 전체 트리에서 발견되지 않았습니다. 별도 Editor·테스트 코드도 발견되지 않았습니다.

## 프로젝트와 담당 역할

- 개발 시기: **2024.09~10 (2024년 2학기 전기)**
- 팀 구성: **윤재현(기획), 권민혁(프로그래밍), 한기승(프로그래밍)**
- 주요 기술: **C#, Unity**
- 권민혁 담당: **블록 이동·충돌, 카메라, 사운드 구현, 팀원 작업 통합**

권민혁 담당 영역은 `PlayableMove`, 충돌 계열, `CameraController`, `SoundManager`를 중심으로 설명합니다. `PlayableMove.cs`와 `StageManager.cs`에는 한기승의 수정도 확인되어 공동 수정 파일로 표시했습니다. UI·메뉴·시스템의 수동 통합 반입은 초기 작성자 확인과 구분합니다. [기여 기록](CONTRIBUTIONS.md)에 정확한 커밋 링크와 기능별 근거·한계를 정리했습니다.

감사 범위는 main 31개 커밋, 5개 브랜치, 고유 커밋 37개입니다. main 작성자별 커밋 수 29:2는 기여율이 아니며 개인 기여 비율이나 단독 저작권으로 환산하지 않습니다.

## 프로젝트와 읽는 순서

저장소 이름은 3D지만 선택 코드에서는 Rigidbody2D와 Collider2D를 이용해 움직이는 블록을 쌓는 게임 흐름을 확인할 수 있습니다. 씬과 Inspector 연결은 제외되어 코드만으로 실제 게임의 모든 설정을 알 수 없습니다.

| 경로 | 역할 |
| --- | --- |
| `Assets/Scripts/Spawn/BuildingSpawner.cs` | 가중치 기반 블록 선택, 생성 지연, 이동 속도 설정 |
| `Assets/Scripts/Block/PlayableMove.cs` | 수평 이동·반사, 입력에 따른 낙하, 블록 정지 |
| `Assets/Scripts/Block/BlockCollider.cs`, `TopBlockCollider.cs`, `SideBlockCollider.cs`, `BottomBlockCollider.cs` | 충돌 방향별 상속 구조와 쌓기·파괴 처리 |
| `Assets/Scripts/Block/BlockManager.cs`, `BlockInfo.cs` | 블록 스택·높이와 개별 블록 상태·파괴 |
| `Assets/Scripts/StageManager.cs` | 목표 높이·체력 판정, 체력 이벤트, 성공·실패 UI 호출 |
| `Assets/Scripts/InGameUI.cs`, `System/Timer.cs`, `BestTimeMenu.cs` | 체력·클리어 UI, 시간 측정, PlayerPrefs 최고 기록 |
| `Assets/Scripts/System/` | 게임 관리자, 스테이지 잠금 해제, 저장값 초기화 |
| `Assets/Scripts/SoundManager.cs`, `Button/`, `ChangeScenes.cs`, `ClickOnContent.cs` | 오디오 API, 버튼·메뉴·씬 전환 |
| `Assets/Scripts/etc_/` | 카메라·배경 추종, 반사·낙하 파괴, 씬별 BGM 연동 |
| `ReviewContext/Packages/manifest.json` | 원본 패키지 목록을 읽는 문맥 자료; 패키지 구현은 없음 |
| `ReviewContext/ProjectSettings/ProjectVersion.txt` | 원본 Unity 2022.3.7f1 정보; 빌드 설정은 없음 |

대표 흐름은 `BuildingSpawner → PlayableMove → TopBlockCollider → BlockManager → StageManager → InGameUI`입니다. 원본 코드는 리팩터링하지 않았고, 빈 메서드와 기존 API 참조도 보존했습니다. `BottomBlockCollider`는 비어 있는 override가 있지만 게임별 충돌 상속 관계를 설명하므로 포함했습니다.

## 포함 판단과 출처 한계

C# 확장자만으로 포함하지 않았습니다. 파일 내용을 읽어 게임별 클래스 간 참조·로직을 확인했습니다. 빈 Start/Update만 있는 `etc_/BackgroundChanger.cs`는 기본 템플릿에 가까워 제외했습니다. TMP 리소스·셰이더 등 외부 패키지 구현은 포함하지 않았습니다.

포함 코드는 팀 프로젝트의 검토 자료입니다. 제공된 커밋 감사로 확인된 기여 변경, 공동 수정, 초기 작성자 미확인 파일을 `MANIFEST.csv`와 `CONTRIBUTIONS.md`에서 구분합니다. 변경 근거는 파일 전체의 단독 작성·권리 귀속을 증명하지 않습니다. 사용자는 2026-10-01 KST에 팀원들이 현재 코드와 이름의 새 GitHub 공개 저장소 게시에 동의했으며 외부 차용 코드가 없다고 확인했습니다. 이는 사용자 확인 기록이며 독립적인 법적 검증이나 모든 파일의 단독 저작 입증은 아닙니다. 새로운 라이선스를 부여하지 않습니다. 외부 패키지 고지나 구현 패턴이 검색되지 않았다는 사실만으로 독자 작성 코드를 입증하지 않습니다.

## 제외한 자료와 의존성

`Assets/Fonts/malgunbd.ttf`, `malgunbd SDF.asset`, 관련 meta를 포함한 Fonts 폴더 전체와 TextMesh Pro의 폰트·파생 SDF·리소스·셰이더·문서를 제외했습니다. 음원·텍스처·씬·프리팹·머티리얼·Unity meta·빌드 설정·캐시·바이너리·원본 .git 이력도 포함하지 않았습니다.

UnityEngine, Unity UI, TMPro, Unity.VisualScripting, Unity.Collections 및 조건부 UnityEditor API 참조는 원본 코드에 남아 있습니다. 엔진과 TMP 패키지 구현, 외부 종속성 및 Inspector 연결은 제공되지 않습니다. DOTween 참조나 구현은 이번 선택 코드에서 발견되지 않았습니다. 폰트 대체나 API 수정은 수행하지 않았습니다.

## 패키지·에셋 버전 근거

기준은 위 고정 커밋입니다. [Packages/manifest.json](https://github.com/als79gur49/2024_3D_TeamProject/blob/81e231be42c696d0763b8c9700f17e54c16c86f5/Packages/manifest.json)의 직접 의존성 선언과 [Packages/packages-lock.json](https://github.com/als79gur49/2024_3D_TeamProject/blob/81e231be42c696d0763b8c9700f17e54c16c86f5/Packages/packages-lock.json)의 최상위 `dependencies[패키지].version` 잠금 기록을 구분합니다. resolved는 커밋에 저장된 잠금 버전이며 현재 PC 설치·실행 검증을 뜻하지 않습니다. 하위 패키지가 요구하는 버전을 최종 resolved로 대신 쓰지 않았습니다. 링크는 비공개 원본 접근 권한이 필요합니다.

| 직접 패키지 | manifest 선언 | lock resolved |
| --- | --- | --- |
| `com.unity.collab-proxy` | 2.5.1 | 2.5.1 |
| `com.unity.feature.2d` | 2.0.1 | 2.0.1 |
| `com.unity.ide.rider` | 3.0.28 | 3.0.28 |
| `com.unity.ide.visualstudio` | 2.0.22 | 2.0.22 |
| `com.unity.test-framework` | 1.1.33 | 1.1.33 |
| `com.unity.textmeshpro` | 3.0.6 | 3.0.6 |
| `com.unity.timeline` | 1.7.6 | 1.7.6 |
| `com.unity.ugui` | 1.0.0 | 1.0.0 |
| `com.unity.visualscripting` | 1.9.4 | 1.9.4 |

2D Feature 등의 전이 의존성(직접 선언 없음, lock resolved):

- `com.unity.2d.animation`: 9.0.3 (registry, depth 1)
- `com.unity.2d.aseprite`: 1.0.0 (registry, depth 1)
- `com.unity.2d.common`: 8.0.1 (registry, depth 2)
- `com.unity.2d.pixel-perfect`: 5.0.3 (registry, depth 1)
- `com.unity.2d.psdimporter`: 8.0.2 (registry, depth 1)
- `com.unity.2d.sprite`: 1.0.0 (builtin, depth 1)
- `com.unity.2d.spriteshape`: 9.0.2 (registry, depth 1)
- `com.unity.2d.tilemap`: 1.0.0 (builtin, depth 1)
- `com.unity.2d.tilemap.extras`: 3.1.1 (registry, depth 1)
- `com.unity.burst`: 1.8.7 (registry, depth 3)
- `com.unity.collections`: 1.2.4 (registry, depth 2)
- `com.unity.ext.nunit`: 1.0.6 (registry, depth 1)
- `com.unity.mathematics`: 1.2.6 (registry, depth 2)

`com.unity.modules.*` 엔진 모듈은 manifest 31개·lock 32개 모두 `1.0.0`입니다. lock에만 있는 전이 모듈은 `com.unity.modules.subsystems`입니다. 이 값은 별도 Asset Store 판매 버전이 아닙니다. 전체 패키지별 선언·resolved·source·depth는 `VALIDATION.json`의 `package_version_audit.packages`에 기록했습니다.

- Unity 에디터: `2022.3.7f1` (revision `b16b3b16c7a0`), 근거 `ReviewContext/ProjectSettings/ProjectVersion.txt` / 원본 `ProjectSettings/ProjectVersion.txt`.
- TextMesh Pro: UPM 선언·lock 모두 `3.0.6`. 제외한 `Assets/TextMesh Pro` 리소스·문서·셰이더의 개별 배포 버전은 별도 버전 파일 근거가 없어 확인 불가입니다. 파일 이름의 연도만으로 설치 버전을 추정하지 않습니다.
- DOTween / DOTween Pro: 이 커밋의 전체 트리(잘림 없음), manifest·lock, 선택 코드에서 `DOTween`, `DG.Tweening`, `Demigiant` 근거가 발견되지 않았습니다. 이 저장소에 포함됐다는 근거 없음 / 설치 버전 확인 불가로 기록합니다. 다른 프로젝트나 현재 판매 버전은 사용하지 않았습니다.
- 제외한 폰트·음원·텍스처 등의 개별 에셋 버전은 확인 불가입니다. 제외 payload를 추가로 내려받거나 버전·라이선스를 추정하지 않았습니다.

## 검증과 검토 시 유의점

30개 원본 파일은 저장소 트리의 크기·Git blob SHA-1과 모두 일치하며, 로컬 SHA-256도 기록하고 재확인했습니다. 연결 도구의 텍스트 인코딩 변환을 역변환한 경우에도 원본 blob 해시와 일치하는 바이트만 채택했습니다. 일부 원본 파일은 UTF-8이 아닙니다. 한글이 깨져 보이면 에디터에서 CP949 등 원본 인코딩을 확인하세요. 읽기 편의를 위한 재인코딩은 하지 않았습니다.

`MANIFEST.csv`는 포함 파일 30개의 경로·기여 기록·출처 한계·원본 blob 해시·SHA-256과 제외 파일 322개의 종류별 개수를 제공합니다. 제외 리소스의 개인 이름이 있는 경로는 공개 문서에서 생략했습니다. `VALIDATION.json`은 검사 결과와 한계를 기록합니다. 비밀키·토큰·자격 증명 패턴 검사에서 후보가 없었으나 패턴 검사는 모든 비밀정보의 부재를 보증하지 않습니다. 제외한 파일의 내용은 내려받지 않았습니다.

Unity 실행·테스트·컴파일 검증은 수행하지 않았습니다. 원본과 원격 이력을 수정하지 않았고 저장소 초기화, push, 공개, 공유 변경도 하지 않았습니다. `.gitignore`는 선택 파일과 생성 문서만 허용하는 목록이므로 새 파일을 추가할 때에는 출처·비밀정보·필요성을 검토하고 허용 목록을 갱신해야 합니다. Gitignore는 보안 경계가 아니며 강제 추가는 차단하지 못합니다.

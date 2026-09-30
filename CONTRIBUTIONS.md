# 기여 기록 — 딸깍, 건축

개발 시기: 2024.09~10, 2024년 2학기 전기. 팀 구성은 윤재현(기획), 권민혁(프로그래밍), 한기승(프로그래밍)입니다. 권민혁의 담당 역할은 블록 이동·충돌, 카메라, 사운드 구현과 팀원 작업 통합입니다. 프로젝트 소개는 사용자 제공 사실이며, 아래 변경 근거는 제공된 GitHub 커밋 감사 결과입니다.

## 감사 범위와 해석

main 31개 커밋, 5개 브랜치에서 고유 커밋 37개를 확인한 감사입니다. main의 작성자별 커밋 수 29:2는 기여율이 아닙니다. 커밋 수·추가 줄 수·통합 커밋 작성자를 개인의 실제 작업량이나 단독 저작권으로 환산하지 않습니다.

`als79gur49 = 권민혁`, `naymell = 한기승`의 연결은 [당시 README 커밋](https://github.com/als79gur49/2024_3D_TeamProject/commit/93b60ee2d62761ae9c8e3b12f4f3bebfbb6a7683)에서 확인된 감사 근거입니다. 현재 스냅샷 main에 README가 없다는 사실과 과거 README의 존재는 모순되지 않습니다. 링크는 비공개 원본이므로 접근 권한이 없는 독자는 열 수 없습니다.

## 권민혁의 담당 기능과 변경 근거

| 기능 | 대표 파일 및 확인 범위 | 커밋 근거 |
| --- | --- | --- |
| 블록 이동·생성·충돌 | `Block/PlayableMove.cs`, `Spawn/BuildingSpawner.cs`, 블록 충돌 계열의 추가·구현 | [초기 이동·생성·충돌 추가](https://github.com/als79gur49/2024_3D_TeamProject/commit/3d3794fcd7ac18449148717d32a8bd320edab736) |
| 방향별 충돌 구조 | `Block/BlockCollider.cs`, `TopBlockCollider.cs`, `SideBlockCollider.cs`, `BottomBlockCollider.cs` | [충돌 분리](https://github.com/als79gur49/2024_3D_TeamProject/commit/7e35cec2fc0f0869f3ec3e0c0e12251effcb872a) |
| 충돌 보호·배경 | 충돌 중복 처리 등 보호 조건 및 배경 관련 변경 | [보호 조건·배경 변경](https://github.com/als79gur49/2024_3D_TeamProject/commit/c94b75373e46714e3256836973b50751364c1343) |
| 카메라 | `etc_/CameraController.cs`: 블록 상태에 따른 목표 높이와 위치 보간 | [카메라 구현](https://github.com/als79gur49/2024_3D_TeamProject/commit/9260607c582d6a6b9565d0e9645b2b40431740fc) |
| 사운드 | `SoundManager.cs`: BGM·효과음 재생과 AudioMixer 볼륨 제어 | [사운드 구현](https://github.com/als79gur49/2024_3D_TeamProject/commit/82510566d760788a8864e3aba19eac5cc0796135) |
| 버튼 사운드 | 버튼 동작과 사운드 재생의 연결 | [버튼 사운드 변경](https://github.com/als79gur49/2024_3D_TeamProject/commit/baf3e92e78fde74ab5188516cfc5626e7dc271c6) |
| 팀 작업 통합 | 게임 로직과 팀원 UI·시스템 작업의 통합 | [통합 변경](https://github.com/als79gur49/2024_3D_TeamProject/commit/8830eeb694037224b4f39d04c912a21e7adeaa6d) |

현재 코드에서 `TopBlockCollider`는 블록 정지·스택 등록·효과음·카메라 갱신을 연결합니다. `BlockManager → StageManager → InGameUI`는 높이·체력 판정과 화면 표시를 연결합니다. 이러한 호출 구조는 통합 설명의 근거이며 각 연결 대상 파일 전체의 단독 작성 증거는 아닙니다.

## 한기승의 확인된 변경과 공동 수정

- [f9808a61](https://github.com/als79gur49/2024_3D_TeamProject/commit/f9808a61f5724accc6c067f8505c0366c9422c99): `PlayableMove.cs`의 종료 UI 상태 입력 제한, `InGameUI.cs`의 체력 이벤트·표시, `StageManager.cs`의 체력·종료 UI 연결, `QuitButton.cs` 추가.
- [62574142](https://github.com/als79gur49/2024_3D_TeamProject/commit/625741429805e1dd77eb46a76573b210023f54bd): `GameManager.cs`·`LevelLock.cs`의 재클리어 잠금 해제 및 생명주기·null 처리, `QuitButton.cs`의 사운드 재생 후 종료, `BestTimeMenu.cs`의 표시 문구 변경.

`PlayableMove.cs`와 `StageManager.cs`는 **공동 수정 파일**로 설명합니다. `QuitButton.cs`는 한기승의 추가·후속 변경 근거가 있으므로 권민혁의 단독 구현으로 소개하지 않습니다.

## 초기 구현의 작성자가 확인되지 않은 자료

[수동 통합 a8617d9b](https://github.com/als79gur49/2024_3D_TeamProject/commit/a8617d9b4de25e7d9f96f06b3b002e04aa89f44b)는 UI·메뉴·시스템 스크립트 12개를 반입했습니다. 통합 커밋 작성자만으로 반입된 파일의 초기 작성자를 확정하지 않습니다.

`ButtonNo.cs`, `ChangeScenes.cs`, `ClickOnContent.cs`, `StageReset.cs`, `Timer.cs`의 초기 작성자는 미확인입니다. `BestTimeMenu.cs`, `InGameUI.cs`, `GameManager.cs`, `LevelLock.cs`도 초기 구현 작성자는 미확인이고, 위 한기승의 후속 변경만 구분해 기록합니다. 그 밖의 파일에서 기능상 담당 범위와 대응하는 사실도 파일 전체의 저작자를 입증하지 않습니다. 파일별 상태와 근거 링크는 `MANIFEST.csv`를 참고하세요.

## 공개 권한과 출처의 별도 한계

커밋 감사는 기여 설명의 근거를 보강하며 모든 코드의 권리 귀속이나 단독 저작을 확정하지 않습니다. 사용자는 2026-10-01 KST에 팀원들이 현재 코드와 이름의 새 GitHub 공개 저장소 게시에 동의했으며 외부 차용 코드가 없다고 확인했습니다. 이는 사용자 확인에 근거한 게시 동의·출처 설명이며 독립적인 법적 검증이 아닙니다. 공동 수정과 초기 작성자 미확인 기록은 유지하며 새로운 라이선스를 부여하지 않습니다. 원본 저장소는 비공개로 유지됩니다.

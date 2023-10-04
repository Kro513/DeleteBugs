![header](https://capsule-render.vercel.app/api?type=waving&color=gradient&customColorList=2&height=300&section=header&text=DeleteBug&fontSize=90&fontColor=2D2727)
 https://teamsparta.notion.site/02-2-b1b44f2e5f1d437c9dab0c05f24807d4

해충 디펜스 (DeleteBug)

 ## :one: 프로젝트 소개

프로젝트 소개 : 내 집을 곤충의 습격으로부터 구하는 게임이다. 다양한 타워를 건설하고 전략적으로 배치하여 집을 곤충 무리로부터 방어한다. 곤충의 다양한 종류와 체력, 속도 빈도가 증가하며, 플레이어의 지휘력과 전략 능력을 시험하는 중요한 요소를 제공하는 역동적인 게임이다.

### :raising_hand: 멤버구성

- 김형중
    
    타워 종류, 상점
    
    1. FlySwatter(파리채)Tower 스크립트 작성
    2. Muscipula(파리지옥)Tower 스크립트 작성
- 우성훈
    
    곤충 구현
    
- 신해준
    
    게임 UI ,Player체력, 타일맵, 사운드,일시정지판넬
    
- 김윤경
    
    타워 종류, 상점
    
- 이두희
    
    게임 시작 종료화면




## :clipboard: 필수구현사항 목록

###  1,게임맵 생성 및 배치
타일맵을 이용하여 맵을 생성배치


###  2,타워 생성
타워인터페이스를 이용해 설치 구역에 타워를 클릭하면 타워가 설치 됩니다.

###  3,적의움직임과스폰
적 캐릭터가 경로를 따라 이동하는 로직을 구현하여 방어지점까지 이동합니다.
일정한 속도로 적을 스폰시키는 스포너를 만들었습니다.

###  4,타워공격로직
타워가 적을 감지하고 공격하는 로직을 따릅니다.

###  5,적의 체력과 타워의 공격력

###  6,자원관리-플레이어가 자원을 획득,소모
플레이어가 웨이브가 끝날때 골드를 받고
골드로 타워를 구매할수 있도록합니다.

###  7,게임 진행상태표시
체력,자원,라운드 현재 시간 표시

###  8,게임오버및 승리조건-
일반 몬스터는 체력을 1씩 깍아 최대체력10을 전부 깍으면 게임오버가 됩니다.
바퀴벌레가 보스 몬스터로서
처치하게 된다면 승리하게 되고
바퀴벌레가 목표지점으로 도달하게되면 무조건 게임오버하게됩니다.




## :clipboard: 선택 구현사항
1,사운드 및 그래픽 향상
게임시작,클리어,게임오버,골드소모시,체력이 깍일경우 사운드가 작동하도록 했습니다.

2,다양한 적 유형
모기,개미,바퀴벌레등의 적의 유형을 추가했습니다.

3,다양한 타워유형
파리지옥,파리채,살충제등의 타워유형을 나눠서 설계했습니다.

## :no_entry:미구현 목록
게임저장 불러오기

레벨 디자인

타워 업그레이드

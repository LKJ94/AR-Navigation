# 프로젝트 소개
Google Map API에서 Static Map을 이용하여 2D 지도를 구현하고 Places API 와 Direction API를 이용해서 목적지의 정보와 현재위치와 목적지 사이에 경로를 탐색,
사용자를 목적지까지 안내
AR 기능을 이용해서 카메라에 목적지까지의 경로를 시각적으로 보여줄 수 있게 함

# 목차
- 역할 분담
- 기능 구현
- 느낀점

# 역할분담
Google Map API 및 전체 프로젝트 담당 - 이기준 [Github] : <https://github.com/LKJ94>

AR 기능 담당 - 홍승모

UI 담당 - 채병락

# 기능구현
AR - Google API 중 지리정보 생성자(Geospatial Creator)를 이용해서 앵커를 지도에 설치하면 AR 카메라로 해당 위치의 앵커가 보이는 기능 구현

UI - AR Navigation을 Android Galaxy Tab에서 빌드하기 위해 필요한 UI들 제작

Google Map API :
## 1. Google Static Map 
### Google Static Map

<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/%EC%8A%A4%ED%81%AC%EB%A6%B0%EC%83%B7%202024-05-16%20212217.png" alt="Project Logo" width="1200" height="800"/>
<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/Screenshot_20240509_113825_TestAR.jpg" alt="Project Logo" width="800" height="800"/>

Google Map API에서 URL의 양식에 맞게 구성하고 해당 이미지를 Raw Image를 통해 보여줌

### Drag & Zoom

Mercator 투영 기법을 이용해서 지도를 이미지 사이즈에 맞게 설정

<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/%EC%8A%A4%ED%81%AC%EB%A6%B0%EC%83%B7%202024-05-16%20212743.png" alt="Project Logo" width="800" height="800"/>

드래그 및 확대/축소 기능 구현
<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/%EC%8A%A4%ED%81%AC%EB%A6%B0%EC%83%B7%202024-05-16%20212829.png" alt="Project Logo" width="1200" height="800"/>
<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/%EC%8A%A4%ED%81%AC%EB%A6%B0%EC%83%B7%202024-05-16%20212836.png" alt="Project Logo" width="1200" height="800"/>

## 2. Places API

 ### Autocomplete API

자동완성 기능을 이용해서 (2글자 이상부터) 장소의 이름을 검색하면 비슷한 이름의 장소들을 나열, 해당 장소를 검색했을 경우 장소의 geometry (경도, 위도) 데이터 값을 가지게 됨
<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/%EC%8A%A4%ED%81%AC%EB%A6%B0%EC%83%B7%202024-05-16%20213013.png" alt="Project Logo" width="1200" height="800"/>
<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/Screenshot_20240509_113901_TestAR.jpg" alt="Project Logo" width="800" height="800"/>

 ### Details API

검색한 장소의 위치값을 가져옴 (목적지 설정)
<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/Screenshot_20240509_113912_TestAR.jpg" alt="Project Logo" width="800" height="800"/>

목적지의 세부 정보 (이름, 주소, 번호, 웹사이트 등등)
<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/Screenshot_20240509_113923_TestAR.jpg" alt="Project Logo" width="800" height="800"/>
<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/%EC%8A%A4%ED%81%AC%EB%A6%B0%EC%83%B7%202024-05-16%20213022.png" alt="Project Logo" width="1200" height="800"/>
<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/%EC%8A%A4%ED%81%AC%EB%A6%B0%EC%83%B7%202024-05-16%20213029.png" alt="Project Logo" width="1200" height="800"/>

## 3. Direction API

### 목적지 설정 후 현재 위치와 목적지 사이의 경로 탐색
<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/%EC%8A%A4%ED%81%AC%EB%A6%B0%EC%83%B7%202024-05-16%20213043.png" alt="Project Logo" width="1200" height="800"/>
<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/Screenshot_20240509_113933_TestAR.jpg" alt="Project Logo" width="800" height="800"/>

### 탐색한 경로 길안내 시작
<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/%EC%8A%A4%ED%81%AC%EB%A6%B0%EC%83%B7%202024-05-16%20213050.png" alt="Project Logo" width="1200" height="800"/>
<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/%EC%8A%A4%ED%81%AC%EB%A6%B0%EC%83%B7%202024-05-16%20213123.png" alt="Project Logo" width="1200" height="800"/>
<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/Screenshot_20240509_113956_TestAR.jpg" alt="Project Logo" width="800" height="800"/>

## 4. 그 외

### 검색 기록 저장 및 버튼 동적 할당
<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/%EC%8A%A4%ED%81%AC%EB%A6%B0%EC%83%B7%202024-05-16%20213147.png" alt="Project Logo" width="1200" height="800"/>
<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/%EC%8A%A4%ED%81%AC%EB%A6%B0%EC%83%B7%202024-05-16%20213135.png" alt="Project Logo" width="1200" height="800"/>

### Script 구조 

1차 프로젝트 때 스크립트 정리와 데이터 구조로 고생을 많이 해서 이번에는 MVC 패턴으로 구조를 짜고 시작함, 거기에 Singleton으로 서로를 참조하게 만들어서 기능 구현하는데 편리했음

'M' 
<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/%EC%8A%A4%ED%81%AC%EB%A6%B0%EC%83%B7%202024-05-16%20212908.png" alt="Project Logo" width="1200" height="800"/>

'V'
<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/%EC%8A%A4%ED%81%AC%EB%A6%B0%EC%83%B7%202024-05-16%20212934.png" alt="Project Logo" width="1200" height="800"/>

'C'
<img src="https://github.com/LKJ94/AR-Navigation/blob/main/TestAR/DescriptionImages/%EC%8A%A4%ED%81%AC%EB%A6%B0%EC%83%B7%202024-05-16%20212938.png" alt="Project Logo" width="1200" height="800"/>

# 느낀점
구글 Map API 와 유니티로 Navigation을 만드는 과정이 좀 험난했습니다. JavaScript를 이용한 방법도 있었지만 팀원이 3명이라 각자 역할 맡아서 R&D하기 바빴고 의욕없는 팀원들 이끌면서
주어진 기능들만이라도 완성해보자라는 마음으로 프로젝트를 시작했습니다. 

그로인해 대부분의 기능구현을 제가 맡게 되었고 API를 이용해서 유니티에 구현하는 것이 처음이기에 이것 저것 Document를 보면서 기능구현하기에 급급했고 다른 팀원들을 더 세심하게 봐주지 못한 게 아쉬움으로 남았습니다. UI의 디자인 부분이라던가 AR 컨텐츠 부분에서 더 나은 방향으로 나아갈 수 있었지만 그렇게 마무리하지 못해서 아쉬웠고 제 본래 파트인 Static Map 부분에서도 AR에서 앵커를 코드로 설정할 수 없어서 일일히 경로를 그렸던 점, 정적 지도 로딩이라서 드래그나 확대시 맵이 끊기는 점, 경로 또한 대중교통 루트로만 사용가능했던 제한적인 상황에서 다른 방향으로 더 나아가지 못한 점이 아쉬웠습니다.

하지만 이렇게 다소 불친절하고 낯선 환경에서 프로젝트를 끝마칠 수 있던 지난 날의 경험들은 뜻깊었습니다. 절대 못할 것 같은 프로젝트에서 하나씩 기능이 구현되면서 남은 기간동안 프로젝트를 진행하는데에 큰 힘이 되었습니다. 다음 프로젝트에서는 이러한 단점들을 좀 더 발전시켜서 더 나은 모습이 되었으면 좋겠습니다.

# 감사합니다!


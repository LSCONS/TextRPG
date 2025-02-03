using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    //구현해야 하는 것
    //5. 레벨이 오를수록 상점에 나타나는 아이템의 양이 증가
    //2. 사망 시 재시작 가능
    //1. 레벨 업 기능 추가
    //경험치 슬롯 추가
    //던전의 난이도마다 흭득 경험치 랜덤값으로 입력
    //기본 상태 창에서도 현재 경험치 출력

    //4. 다양한 값들 프로퍼티로 정의
    //3. 플레이어 상태 저장 기능 추가

    public class GameManager
    {
        private static GameManager _instance;
        private static readonly object _lock = new object(); // 멀티스레딩 안전

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                { _instance = new GameManager(); }

                return _instance;
            }
        }

        // private 생성자로 외부에서 new 방지
        private GameManager() { }


        public void StartGame()
        {
            List<Item> playerItemList = new List<Item>();                                   //플레이어의 인벤토리 아이템 리스트
            ItemInstanceManager itemInstanceManager = new ItemInstanceManager();            //아이템을 무작위로 생성할 때 사용할 변수
            JobType playerJobs = new JobType(); //플레이어의 직업을 저장할 변수

            string playerInput = "";
            bool isReStartGame = false;//***위치 조정 필요

            Player.PlayerAbilityStatus.playerName = MenuManager.InputPlayerNameMenu(null); //플레이어 이름 지정
            playerJobs = (JobType)MenuManager.InputPlayerSelectJobMenu(null); //플레이어 직업 지정
            Player.PlayerAbilityStatus.playerJob = playerJobs.ToString();   //직업 이름 플레이어에 대입
            Player.PlayerAbilityStatus.playerNowLevel = 1;                  //플레이어 레벨 초기화
            Player.InputPlayerJobAbility(playerJobs.ToString());            //정한 직업의 기본 능력치를 입력

            while (true)
            {
                if (isReStartGame == true)
                {
                    //게임 데이터 초기화 및 이름, 직업 선택도 초기화***
                }



                Console.Clear();
                Console.WriteLine(TextManager.StartGameTxt());
                Console.WriteLine(TextManager.SelectMainMenuTxt());
                Console.Write(TextManager.SelectNumberTxt(null));
                playerInput = Console.ReadLine();


                switch (playerInput)
                {
                    case "1"://상태 창 입장
                        MenuManager.InputStatusMenu(playerItemList, null);
                        break;

                    case "2"://인벤토리 창 입장
                        MenuManager.InputInventoryMenu(playerItemList, null);
                        break;

                    case "3"://상점 창 입장
                        MenuManager.InputShopMenu(playerItemList, null);
                        break;

                    case "4"://던전 창 입장
                        MenuManager.InputDungeonMenu(null);
                        break;

                    case "5"://휴식 창 입장
                        MenuManager.InputRestMenu(null);
                        break;


                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }

        }
    }
}

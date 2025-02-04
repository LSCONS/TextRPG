using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TextRPG.Player.PlayerAbilityStatus;
using NAudio.Wave;

namespace TextRPG
{
    //구현해야 하는 것

    //창 변경시 효과음 소리

    //배경음악 추가

    //한글자씩 출력되는 기능 구현

    //던전 내부에서 특수 이벤트 추가

    //플레이어가 보낼 수 있는 최대 턴 추가
    public class GameManager
    {
        private static GameManager _instance;

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

        static IWavePlayer bgmPlayer;
        static AudioFileReader bgmReader;

        public void StartGame()
        {
            DataManager.PlayerDataLoad();
            string? message = null;

            while (true)
            {
                if( PlayerName == null ||
                    PlayerJob == null ||
                    PlayerName == "" || 
                    PlayerJob == "")
                {
                    DataManager.PlayerDataReStart();
                }

                if (PlayerInventoryItem == null) PlayerInventoryItem = new List<Item>();

                Console.Clear();
                Console.WriteLine(TextManager.StartGameTxt());
                Console.WriteLine(TextManager.SelectMainMenuTxt());
                Console.Write(TextManager.SelectNumberTxt(message));
                string playerInput = Console.ReadLine();

                message = null;

                switch (playerInput)
                {
                    case "1"://상태 창 입장
                        MenuManager.InputStatusMenu(PlayerInventoryItem, null);
                        break;

                    case "2"://인벤토리 창 입장
                        MenuManager.InputInventoryMenu(PlayerInventoryItem, null);
                        break;

                    case "3"://상점 창 입장
                        MenuManager.InputShopMenu(PlayerInventoryItem, null);
                        break;

                    case "4"://던전 창 입장
                        MenuManager.InputDungeonMenu(null);
                        break;

                    case "5"://휴식 창 입장
                        MenuManager.InputRestMenu(null);
                        break;

                    default:
                        message = "잘못된 입력입니다";
                        break;
                }
            }

        }
    }
}

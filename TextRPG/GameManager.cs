using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TextRPG.Player.PlayerAbilityStatus;

namespace TextRPG
{
    //구현해야 하는 것
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



        public void StartGame()
        {
            DataManager.PlayerDataLoad();
            MenuManager.MainMenu(null);
        }
    }
}

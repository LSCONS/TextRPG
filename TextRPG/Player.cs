using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Player
    {
        //플레이어의 기본적인 능력치 데이터가 들어있는 클래스
        public static class PlayerAbilityStatus
        {
            public static string playerName = "르탄이";
            public static string playerJob = "도적";

            public static int playerNowLevel = 0;
            public static int playerLevelDefaultRequestValue = 5;
            public static int playerLevelRequestValue = 5;
            public static int playerLecelNowValue = 0;
            public static int playerMaxHP = 0;
            public static int playerNowHP = 0;
            public static int playerNowATK = 0;
            public static int playerNowDEF = 0;
            public static int playerNowGold = 0;

            public static Item weaponItem;
            public static Item armorItem;
        }

        public static void InputPlayerJobAbility(string job)
        {
            int hp = 0;
            int ATK = 0;
            int DEF = 0;
            int gold = 0;


            switch(job)
            {
                case "전사":
                    hp = 120;
                    ATK = 8;
                    DEF = 8;
                    gold = 200;
                    break;

                case "도적":
                    hp = 100;
                    ATK = 10;
                    DEF = 5;
                    gold = 500;
                    break;

                case "궁수":
                    hp = 120;
                    ATK = 12;
                    DEF = 5;
                    gold = 200;
                    break;

                default:
                    Console.Error.WriteLine("직업을 찾을 수 없습니다.");
                    break;
            }

            PlayerAbilityStatus.playerNowATK = ATK;
            PlayerAbilityStatus.playerNowDEF = DEF;
            PlayerAbilityStatus.playerMaxHP = hp;
            PlayerAbilityStatus.playerNowHP = hp;
            PlayerAbilityStatus.playerNowGold = gold;
        }
    }


    internal enum JobType
    {
        전사 = 1,
        도적 = 2,
        궁수 = 3
    }

}

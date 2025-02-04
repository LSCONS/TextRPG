using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Player
    {
        //플레이어의 기본적인 능력치 데이터가 들어있는 클래스
        public static class PlayerAbilityStatus
        {
            public static string PlayerName { get; set; } = "";     //플레이어 이름
            public static string PlayerJob { get; set; } = "";      //플레이어 직업
            public static int PlayerNowLevel { get; set; } = 0;     //플레이어 현재 레벨
            public static int PlayerLevelDefaultRequestValue { get; set; } = 5;     //플레이어 레벨 업 기본 요구 경험치
            public static int PlayerLevelRequestValue { get; set; } = 5;            //플레이어 레벨 업 현재 요구 경험치
            public static int PlayerLevelNowValue { get; set; } = 0;                //플레이어 현재 경험치
            public static int PlayerMaxHP { get; set; } = 0;        //플레이어 최대 체력
            public static int PlayerNowHP { get; set; } = 0;        //플레이어 현재 체력
            public static int PlayerNowATK { get; set; } = 0;       //플레이어 현재 공격력
            public static int PlayerNowDEF { get; set; } = 0;       //플레이어 현재 방어력
            public static int PlayerNowGold { get; set; } = 0;      //플레이어 현재 골드
            public static Item PlayerWeaponItem { get; set; }       //플레이어 장착 무기
            public static Item PlayerArmorItem { get; set; }        //플레이어 장착 방어구
            public static List<Item> PlayerInventoryItem { get; set; }       //플레이어 인벤토리 아이템 리스트
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
                    hp = 80;
                    ATK = 12;
                    DEF = 5;
                    gold = 200;
                    break;

                default:
                    Console.Error.WriteLine("직업을 찾을 수 없습니다.");
                    break;
            }

            PlayerAbilityStatus.PlayerNowATK = ATK;
            PlayerAbilityStatus.PlayerNowDEF = DEF;
            PlayerAbilityStatus.PlayerMaxHP = hp;
            PlayerAbilityStatus.PlayerNowHP = hp;
            PlayerAbilityStatus.PlayerNowGold = gold;
        }
    }


    internal enum JobType
    {
        전사 = 1,
        도적 = 2,
        궁수 = 3
    }

}

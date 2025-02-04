using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TextRPG.Player.PlayerAbilityStatus;

namespace TextRPG
{
    //다양한 텍스트 저장
    internal class TextManager
    {
        //시작 할 때 출력할 텍스트
        public static string StartGameTxt()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("스파르타 마을에 오신 여러분 환영합니다.");
            result.AppendLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

            return result.ToString();
        }


        //메인 메뉴를 출력할 텍스트
        public static string SelectMainMenuTxt()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("1. 상태 보기");
            result.AppendLine("2. 인벤토리");
            result.AppendLine("3. 상점");
            result.AppendLine("4. 던전 입장");
            result.AppendLine("5. 휴식하기");

            return result.ToString();
        }


        //번호 선택을 알리는 텍스트
        public static string SelectNumberTxt(string? message)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("원하시는 행동을 입력해주세요.");
            if (message != null)
            {
                result.AppendLine(message);
            }
            result.Append(">> ");

            return result.ToString();
        }


        //플레이어의 스테이터스를 출력할 텍스트
        public static string PlayerAbilityStatusTxt(List<Item> playerItemList)
        {
            string itemTxt = "";
            StringBuilder result = new StringBuilder();

            int sumATK = 0;
            int sumDEF = 0;
            int sumHP = 0;

            foreach (Item item in playerItemList)
            {
                if (item.UseNow)
                {
                    sumATK += item.ItemATK;
                    sumDEF += item.ItemDEF;
                    sumHP += item.ItemHP;
                }
            }

            result.AppendLine("상태 보기");
            result.AppendLine("캐릭터의 정보가 표시됩니다");
            result.AppendLine();
            result.AppendLine($"이름 : {PlayerName}");
            result.AppendLine($"레벨 : {PlayerNowLevel}");
            result.AppendLine($"직업 : {PlayerJob}");
            result.AppendLine($"공격력 : {PlayerNowATK + sumATK} (+{sumATK})");
            result.AppendLine($"방어력 : {PlayerNowDEF + sumDEF} (+{sumDEF})");
            result.AppendLine($"최대체력 : {PlayerMaxHP + sumHP} (+{sumHP})");
            result.AppendLine($"현재체력 : {PlayerNowHP + sumHP} (+{sumHP})");
            result.AppendLine($"골드 : {PlayerNowGold} G");
            result.AppendLine($"경험치 : ( {PlayerLevelNowValue} / {PlayerLevelRequestValue} )");
            result.AppendLine();
            result.AppendLine("0. 나가기");

            return result.ToString();
        }


        //플레이어의 인벤토리를 출력할 텍스트
        public static string PlayerInventoryTxt(List<Item> playerItemList)
        {
            StringBuilder itemTxt = new StringBuilder();
            StringBuilder result = new StringBuilder();

            foreach (Item item in playerItemList)
            {
                itemTxt.AppendLine("-" + item.ItemEquippedSettingTxt);
            }

            result.AppendLine("인벤토리");
            result.AppendLine("보유 중인 아이템을 관리할 수 있습니다.");
            result.AppendLine();
            result.AppendLine("[아이템 목록]");
            result.AppendLine(itemTxt.ToString());
            result.AppendLine("1. 장착 관리");
            result.AppendLine("0. 나가기");

            return result.ToString();
        }


        //플레이어의 장비 창을 출력할 텍스트
        public static string PlayerEquippedSettingTxt(List<Item> playerItemList)
        {
            StringBuilder itemTxt = new StringBuilder();
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < playerItemList.Count; i++)
            {
                itemTxt.AppendLine($"- {i + 1}{playerItemList[i].ItemEquippedSettingTxt}");
            }

            result.AppendLine("인벤토리 - 장착 관리");
            result.AppendLine("보유 중인 아이템을 관리할 수  있습니다.");
            result.AppendLine();
            result.AppendLine("[아이템 목록]");
            result.AppendLine(itemTxt.ToString());
            result.AppendLine("0. 나가기");

            return result.ToString();
        }


        //상점 창을 출력할 텍스트
        public static string ShopMenuTxt(List<Item> playerItemList)
        {
            StringBuilder itemTxt = new StringBuilder();
            foreach (Item item in ItemInstanceManager.items)
            {
                itemTxt.AppendLine(item.ItemEquippedSettingTxt);
            }


            StringBuilder result = new StringBuilder();
            result.AppendLine("상점");
            result.AppendLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            result.AppendLine();
            result.AppendLine("[보유 골드]");
            result.AppendLine($"{PlayerNowGold} G");
            result.AppendLine();
            result.AppendLine("[아이템 목록]");
            result.AppendLine(itemTxt.ToString());
            result.AppendLine();
            result.AppendLine("1. 아이템 구매");
            result.AppendLine("2. 아이템 판매");
            result.AppendLine("0. 나가기");

            return result.ToString();
        }


        //상점에서 구입창에 들어가면 출력할 텍스트
        public static string ShopBuyTxt(List<Item> playerItemList)
        {
            StringBuilder itemTxt = new StringBuilder();
            for (int i = 0; i < ItemInstanceManager.items.Count; i++)
            {
                itemTxt.AppendLine($"- {i + 1}" + ItemInstanceManager.items[i].ItemEquippedSettingTxt);
            }


            StringBuilder result = new StringBuilder();
            result.AppendLine("상점");
            result.AppendLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            result.AppendLine();
            result.AppendLine("[보유 골드]");
            result.AppendLine($"{PlayerNowGold} G");
            result.AppendLine();
            result.AppendLine("[아이템 목록]");
            result.AppendLine(itemTxt.ToString());
            result.AppendLine();
            result.AppendLine("0. 나가기");

            return result.ToString();
        }


        //상점에서 판매창에 들어가면 출력할 텍스트
        public static string ShopSellTxt(List<Item> playerItemList)
        {
            StringBuilder itemTxt = new StringBuilder();
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < playerItemList.Count; i++)
            {
                itemTxt.AppendLine($"- {i + 1}{playerItemList[i].ItemEquippedSettingTxt}");
            }

            result.AppendLine("상점 - 아이템 판매");
            result.AppendLine("필요없는 아이템을 판매할 수 있습니다.");
            result.AppendLine();
            result.AppendLine("[보유 골드]");
            result.AppendLine($"{PlayerNowGold} G");
            result.AppendLine();
            result.AppendLine("[아이템 목록]");
            result.AppendLine(itemTxt.ToString());
            result.AppendLine("0. 나가기");

            return result.ToString();
        }


        //휴식 창에 들어가면 출력할 텍스트
        public static string RestMenuTxt()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("휴식하기");
            result.AppendLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {PlayerNowGold} G)");
            result.AppendLine($"최대 체력 : {PlayerMaxHP}");
            result.AppendLine($"현재 체력 : {PlayerNowHP}");
            result.AppendLine();
            result.AppendLine("1. 휴식하기");
            result.AppendLine("0. 나가기");

            return result.ToString();
        }


        //던전에 입장하면 출력할 텍스트
        public static string DungeonMenuTxt()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("던전입장");
            result.AppendLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            result.AppendLine();
            result.AppendLine("1. 쉬운 던전 \\ 방어력 5 이상 권장");
            result.AppendLine("2. 일반 던전 \\ 방어력 11 이상 권장");
            result.AppendLine("3. 어려운 던전 \\ 방어력 17 이상 권장");
            result.AppendLine("0. 나가기");

            return result.ToString();
        }


        //던전 클리어 시 출력할 텍스트
        public static string DungeonClearTxt(int dungeonLevel, int damage, int rewardGold, int levelValue, int beforeLevel, int beforeLevelRequest)
        {
            StringBuilder result = new StringBuilder();

            string dungeonMessage = DungeonLevelSwitch(dungeonLevel);
            int playerHP = PlayerNowHP;
            int playerGold = PlayerNowGold;
            int playerLvValue = PlayerLevelNowValue;
            int playerLvRequest = PlayerLevelRequestValue;

            result.AppendLine("던전 클리어");
            result.AppendLine("축하합니다!!");
            result.AppendLine($"{dungeonMessage} 을 클리어 하였습니다.");
            result.AppendLine();
            result.AppendLine("[탐험 결과]");
            result.AppendLine($"레벨 {beforeLevel} -> {PlayerNowLevel}");
            result.AppendLine($"체력 {playerHP + damage} -> {playerHP}");
            result.AppendLine($"골드 {playerGold - rewardGold} -> {playerGold}");
            result.AppendLine($"경험치 ( {playerLvValue - levelValue} / {beforeLevelRequest} ) -> ( {playerLvValue} / {playerLvRequest} )");
            result.AppendLine();
            result.AppendLine("0. 나가기");

            return result.ToString();
        }


        //던전 레벨을 int로 받아온 후 해당 던전의 이름을 string으로 반환하는 메서드
        public static string DungeonLevelSwitch(int dungeonLevel)
        {
            string dungeonMessage = "";

            switch (dungeonLevel)
            {
                case 1:
                    dungeonMessage = "일반 던전";
                    break;

                case 2:
                    dungeonMessage = "보통 던전";
                    break;

                case 3:
                    dungeonMessage = "어려운 던전";
                    break;
            }

            return dungeonMessage;
        }


        //던전 실패 시 출력할 텍스트
        public static string DungeonFailedTxt(int dungeonLevel, int damage)
        {
            StringBuilder result = new StringBuilder();

            string dungeonMessage = DungeonLevelSwitch(dungeonLevel);

            result.AppendLine("던전 클리어 실패");
            result.AppendLine($"{dungeonMessage} 탐험에 실패했습니다.");
            result.AppendLine();
            result.AppendLine("[탐험 결과]");
            result.AppendLine($"체력 {PlayerNowHP + damage} -> {PlayerNowHP}");
            result.AppendLine($"골드 {PlayerNowGold}");
            result.AppendLine();
            result.AppendLine("0. 나가기");

            return result.ToString();
        }


        //사망 시 출력할 텍스트
        public static string PlayerDieTxt(string name, string job, int level, int hp, int ATK, int DEF, int levelValue, int requestLevelValue, int gold)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("플레이어 사망");
            result.AppendLine("플레이어가 사망했습니다.");
            result.AppendLine();
            result.AppendLine("[플레이어 정보]");
            result.AppendLine($"이름 : {name}");
            result.AppendLine($"직업 : {job}");
            result.AppendLine($"레벨 : {level}");
            result.AppendLine($"최대 체력 : {hp}");
            result.AppendLine($"공격력 : {ATK}");
            result.AppendLine($"방어력 : {DEF}");
            result.AppendLine($"경험치 : ({levelValue} -> {requestLevelValue})");
            result.AppendLine($"골드 {gold}");
            result.AppendLine();
            result.AppendLine("0. 다시하기");

            return result.ToString();
        }


        //플레이어의 이름을 입력할 때 사용할 텍스트
        public static string InputPlayerNameTxt(string? message)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("당신의 이름을 입력해 주세요.");
            if (message != null)
            {
                result.AppendLine(message);
            }
            result.Append(">>");

            return result.ToString();
        }


        //플레이어의 직업을 선택할 때 사용할 텍스트
        public static string InputPlayerJobTxt()
        {
            StringBuilder result = new StringBuilder();
            StringBuilder jobList = new StringBuilder();

            //모든 직업의 수와 이름을 가져와 텍스트로 정렬
            foreach (JobType job in Enum.GetValues(typeof(JobType)))
            {
                jobList.AppendLine($"{(int)job}. {Enum.GetName(typeof(JobType), job)}");
            }

            result.AppendLine("당신의 직업을 선택해 주세요");
            result.AppendLine();
            result.Append(jobList.ToString());

            return result.ToString();
        }


        //플레이어가 입력한 내용이 맞는지 확인할 때 사용할 텍스트
        public static string CheckToPlayerInput(string? message)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("입력한 사항이 확실합니까?");
            result.AppendLine();
            result.AppendLine($"당신의 선택 -> [{message}]");
            result.AppendLine();
            result.AppendLine("1. 확정");
            result.AppendLine("0. 돌아가기");

            return result.ToString();
        }


        //플레이어가 레벨 업을 달성할 시 출력할 텍스트
        public static string PlayerLevelUpTxt()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("레벨 업");
            result.AppendLine("플레이어의 레벨이 상승했습니다.");
            result.AppendLine();
            result.AppendLine($"최대체력 : {PlayerMaxHP} -> 추가"); //***추가 텍스트 추가
            result.AppendLine($"현재체력 : {PlayerNowHP} -> 추가");
            result.AppendLine($"공격력 : {PlayerNowATK}  -> 추가");
            result.AppendLine($"방어력 : {PlayerNowDEF}  -> 추가");
            result.AppendLine();
            result.AppendLine("0. 나가기");


            return result.ToString();
        }
    }
}

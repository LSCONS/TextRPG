using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TextRPG.Player.PlayerAbilityStatus;

namespace TextRPG
{
    //다양한 텍스트 저장
    public class TextManager
    {
        //던전의 종류 및 권장 방어도를 입력
        static Dictionary<int, (string, int)> DungeonType = new Dictionary<int, (string, int)>
        {
            {1, ("쉬운 던전", 5) },
            {2, ("보통 던전", 11) },
            {3, ("어려운 던전", 17) },
        };

        #region 시작 및 메인 메뉴 텍스트 관리 /*시작 /*메인메뉴
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
            result.AppendLine("0. 다시 시작하기");

            return result.ToString();
        }


        //메인 메뉴에서 플레이어의 스테이터스를 출력할 텍스트
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


        //메인 메뉴에서 플레이어의 인벤토리를 출력할 텍스트
        public static string PlayerInventoryTxt(List<Item> playerItemList)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("인벤토리");
            result.AppendLine("보유 중인 아이템을 관리할 수 있습니다.");
            result.AppendLine();
            result.AppendLine("[아이템 목록]");
            result.Append(SortItemList(playerItemList, false));
            result.AppendLine();
            result.AppendLine("1. 장착 관리");
            result.AppendLine("0. 나가기");

            return result.ToString();
        }


        //메인 메뉴에서 상점 창으로 넘어갈 때 출력할 텍스트
        public static string ShopMenuTxt(List<Item> playerItemList)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("상점");
            result.AppendLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            result.AppendLine();
            result.AppendLine("[보유 골드]");
            result.AppendLine($"{PlayerNowGold} G");
            result.AppendLine();
            result.AppendLine("[아이템 목록]");
            result.Append(SortItemList(ItemInstanceManager.items, true));
            result.AppendLine();
            result.AppendLine("1. 아이템 구매");
            result.AppendLine("2. 아이템 판매");
            result.AppendLine("0. 나가기");

            return result.ToString();
        }


        //메인 메뉴에서 휴식 창에 들어가면 출력할 텍스트
        public static string RestMenuTxt()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("휴식하기");
            result.AppendLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {PlayerNowGold} G)");
            result.AppendLine();
            result.AppendLine($"최대 체력 : {PlayerMaxHP}");
            result.AppendLine($"현재 체력 : {PlayerNowHP}");
            result.AppendLine();
            result.AppendLine("1. 휴식하기");
            result.AppendLine("0. 나가기");

            return result.ToString();
        }


        //메인 메뉴에서 던전에 입장하면 출력할 텍스트
        public static string DungeonMenuTxt()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("던전입장");
            result.AppendLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            result.AppendLine();
            result.AppendLine($"최대체력 : {PlayerMaxHP}");
            result.AppendLine($"현재체력 : {PlayerNowHP}");
            result.AppendLine();
            result.AppendLine("========================================");
            result.AppendLine("번호    장소             권장 방어력");
            result.AppendLine("========================================");

            for (int i = 1; i < DungeonType.Count + 1; i++)
            {
                string num = SortPadRightItemList("- "+ i.ToString(), 7);
                string name = SortPadRightItemList(DungeonType[i].Item1, 16);
                string recommandArmor = SortPadRightItemList(DungeonType[i].Item2.ToString(), 8);

                result.AppendLine($"{num} {name} {recommandArmor}");
            }
            result.AppendLine("========================================");
            result.AppendLine();
            result.AppendLine("0. 나가기");

            return result.ToString();
        }


        //메인 메뉴에서 새로 시작하기를 누르면 출력할 텍스트
        public static string PlayerReStartMenuTxt()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("새로 시작하기");
            result.AppendLine();
            result.AppendLine("[플레이어 정보]");
            result.AppendLine($"이름 : {PlayerName}");
            result.AppendLine($"레벨 : {PlayerNowLevel}");
            result.AppendLine($"직업 : {PlayerJob}");
            result.AppendLine();
            result.AppendLine("1. 다시 시작하기");
            result.AppendLine("0. 취소");

            return result.ToString();
        }
        #endregion


        #region 상점 아이템 구매 및 판매 관리 /*구매 /*판매
        //상점에서 구입창에 들어가면 출력할 텍스트
        public static string ShopBuyTxt(List<Item> playerItemList)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("상점 - 아이템 구매");
            result.AppendLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            result.AppendLine();
            result.AppendLine("[보유 골드]");
            result.AppendLine($"{PlayerNowGold} G");
            result.AppendLine();
            result.AppendLine("[아이템 목록]");
            result.Append(SortItemList(ItemInstanceManager.items, true));
            result.AppendLine();
            result.AppendLine("0. 나가기");

            return result.ToString();
        }


        //상점에서 판매창에 들어가면 출력할 텍스트
        public static string ShopSellTxt(List<Item> playerItemList)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("상점 - 아이템 판매");
            result.AppendLine("필요없는 아이템을 판매할 수 있습니다.");
            result.AppendLine();
            result.AppendLine("[보유 골드]");
            result.AppendLine($"{PlayerNowGold} G");
            result.AppendLine();
            result.AppendLine("[아이템 목록]");
            result.Append(SortItemList(playerItemList, false));
            result.AppendLine("0. 나가기");

            return result.ToString();
        }
        #endregion


        #region 던전 클리어 및 실패 등 처리 관리 /*클리어 /*실패 /*사망
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


        //던전에서 사망 시 출력할 텍스트
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
        #endregion


        #region 플레이어 관련 출력 관리 /*장비 /*이름 /*직업 /*입력내용 확인 /*레벨 업
        //플레이어의 장비 창을 출력할 텍스트
        public static string PlayerEquippedSettingTxt(List<Item> playerItemList)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("인벤토리 - 장착 관리");
            result.AppendLine("보유 중인 아이템을 관리할 수  있습니다.");
            result.AppendLine();
            result.AppendLine("[아이템 목록]");
            result.Append(SortItemList(playerItemList, false));
            result.AppendLine();
            result.AppendLine("0. 나가기");

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
        public static string PlayerLevelUpTxt(int beforeMaxHP, int beforeATK, int beforeDEF, int beforeNowHP)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("레벨 업");
            result.AppendLine("플레이어의 레벨이 상승했습니다.");
            result.AppendLine();
            result.AppendLine($"최대체력 : {beforeMaxHP} -> {PlayerMaxHP}");
            result.AppendLine($"현재체력 : {beforeNowHP} -> {PlayerNowHP}");
            result.AppendLine($"공격력 : {beforeATK} -> {PlayerNowATK}");
            result.AppendLine($"방어력 : {beforeDEF} -> {PlayerNowDEF}");
            result.AppendLine();
            result.AppendLine("0. 나가기");

            return result.ToString();
        }

        #endregion


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


        //입력 받은 아이템 리스트를 가지고 정렬을 한 뒤 string으로 반환하는 메서드
        public static string SortItemList(List<Item> itemList, bool isBuy)
        {
            StringBuilder itemTxt = new StringBuilder();
            itemTxt.AppendLine("======================================================================================================");
            itemTxt.AppendLine($"번호     이름        종류       공격력     방어력     {(isBuy ? "구매가격" : "판매가격")}     설명 ");
            itemTxt.AppendLine("======================================================================================================");

            for (int i = 0; i < itemList.Count; i++)
            {
                string num = (i + 1).ToString();
                string name = SortPadRightItemList(itemList[i].ItemName, 11);
                string type = SortPadRightItemList(itemList[i].ItemType, 10);
                string ATK = SortPadRightItemList(itemList[i].ItemATK.ToString(), 10);
                string DEF = SortPadRightItemList(itemList[i].ItemDEF.ToString(), 10);
                string gold = "";
                string information = SortPadRightItemList(itemList[i].ItemInformationTxt, 30);
                bool itemIsBuy = itemList[i].IsBuy;

                if (isBuy == true)  { gold = itemIsBuy ? SortPadRightItemList("구매 완료", 12) : SortPadRightItemList(itemList[i].ItemBuyGold.ToString() + " G", 12); }
                else                { gold = SortPadRightItemList(itemList[i].ItemSellGold.ToString() + " G", 12); }


                //플에이어가 착용하고 있는지 확인
                if (itemList[i].UseNow == true) { itemTxt.AppendLine($"- {num, -2} {"[E]",-2} {name} {type} {ATK} {DEF} {gold} {information}");  }
                else                            { itemTxt.AppendLine($"- {num,-6} {name} {type} {ATK} {DEF} {gold} {information}"); }


            }
            itemTxt.AppendLine("======================================================================================================");

            return itemTxt.ToString();
        }


        //해당 텍스트에 한글이 얼마나 들어있는지 확인하고 정렬의 수를 조절하는 메서드
        static string SortPadRightItemList(string input, int defaultLength)
        {
            int countKOR = input.Count(x => x >= 0xAC00 && x <= 0xD7A3);
            return input.PadRight(defaultLength - countKOR, ' ');
        }
    }
}

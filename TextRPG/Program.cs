using System;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TextRPG
{

    internal class Program
    {
        //구현해야 하는 것
        //레벨이 오를수록 상점에 나타나는 아이템의 양이 증가
        //아이템 착용시 PlayerNowDEF에 적용 구현
        //시작 시 이름 입력 가능
        //직업 선택 가능
        //사망 시 재시작 가능


        //게임 시작
        static void Main(string[] args)
        {
            List<Item> playerItemList = new List<Item>();
            ItemInstanceManager itemInstanceManager = new ItemInstanceManager();

            playerItemList = StartSetting(playerItemList);

            string playerInput = "";
            bool isReStartGame = false;//***위치 조정 필요

            PlayerStatus.playerName = InputPlayerNameMenu(null);

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
                        InputStatusMenu(playerItemList, null);
                        break;

                    case "2"://인벤토리 창 입장
                        InputInventoryMenu(playerItemList, null);
                        break;

                    case "3"://상점 창 입장
                        InputShopMenu(playerItemList, null, itemInstanceManager);
                        break;

                    case "4"://던전 창 입장
                        InputDungeonMenu(null);
                        break;

                    case "5"://휴식 창 입장
                        InputRestMenu(null);
                        break;


                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }

        }


        //시작 임시 세팅
        static List<Item> StartSetting(List<Item> playerItemList)
        {
            //*** 추후 해당 아이템을 저장 및 로드하는 기능 추가
            Item longSword = new Item("롱 소드", "무기", false, 10, 0, 0, 50, "그냥 검이다.");
            Item ironarmor = new Item("철 갑옷", "방어구", false, 0, 7, 7, 30, "그냥 갑옷이다.");
            Item oldSword = new Item("낡은 검", "무기", false, 3, 0, 0, 10, "없는 것 보단 나은 검이다");
            playerItemList.Add(longSword);
            playerItemList.Add(ironarmor);
            playerItemList.Add(oldSword);

            return playerItemList;
        }


        //플레이어 이름 입력 창으로 들어간 경우
        static string InputPlayerNameMenu(string? message)
        {
            string playerName = "";

            while (true)
            {
                Console.Clear();
                Console.Write(TextManager.InputPlayerNameTxt(message));
                playerName = Console.ReadLine();

                if(playerName.Replace(" ", "") == "")
                {
                    message = "다시 입력해주세요";
                }
                else
                {
                    break;
                }
            }

            if (CheckedPlayerInput(null, playerName) == false)
            {
                playerName = InputPlayerNameMenu(null);
            }

            return playerName;
        }


        //플레이어 직업 선택 창으로 들어간 경우
        static void InputPlayerSelectJobMenu(string? message)
        {
            
        }


        //플레이어가 입력한 것을 확정 지을지 묻는 메소드
        static bool CheckedPlayerInput(string? message, string playerInput)
        {
            Console.Clear();
            Console.WriteLine(TextManager.CheckToPlayerInput(playerInput));
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerNum = Console.ReadLine();

            switch (playerNum)
            {
                case "0"://돌아가기
                    return false;

                case "1"://확정
                    return true;

                default:
                    return CheckedPlayerInput("잘못된 입력입니다.", playerInput);

            }

        }


        //메인 메뉴에서 상태 창으로 들어간 경우
        static void InputStatusMenu(List<Item> playerItemList, string? message)
        {
            Console.Clear();
            Console.WriteLine(TextManager.PlayerStatusTxt(playerItemList));
            Console.WriteLine();
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = Console.ReadLine();

            switch (playerInput)
            {
                case "0":
                    break;

                default:
                    InputStatusMenu(playerItemList, "잘못된 입력입니다.");
                    break;

            }
        }


        //메인 메뉴에서 인벤토리로 들어간 경우
        static void InputInventoryMenu(List<Item> playerItemList, string? message)
        {
            Console.Clear();
            Console.WriteLine(TextManager.PlayerInventoryTxt(playerItemList));
            Console.WriteLine();
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = Console.ReadLine();

            switch (playerInput)
            {
                case "1":
                    SelectEquiqqedNum(playerItemList, null);
                    break;

                case "0":
                    break;

                default:
                    InputInventoryMenu(playerItemList, "잘못된 입력입니다.");
                    break;

            }
        }


        //인벤토리에서 아이템 장착으로 들어간 경우
        static void SelectEquiqqedNum(List<Item> playerItemList, string? message)
        {
            Console.Clear();
            Console.WriteLine(TextManager.PlayerEquippedSettingTxt(playerItemList));
            Console.WriteLine();
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = Console.ReadLine();

            if (playerInput == "0")
            { }
            else if (int.TryParse(playerInput, out int value) && playerItemList.Count >= value)
            {
                Console.Clear();

                Item item = playerItemList[value - 1];

                if (item.itemType == "무기")
                {
                    if (PlayerStatus.weaponItem == null)
                    {
                        PlayerStatus.weaponItem = item;
                        item.ItemChangeToUse();
                    }
                    else if (PlayerStatus.weaponItem != item)
                    {
                        PlayerStatus.weaponItem.ItemChangeToUse();
                        PlayerStatus.weaponItem = item;
                        item.ItemChangeToUse();
                    }
                    else if (PlayerStatus.weaponItem == item)
                    {
                        PlayerStatus.weaponItem.ItemChangeToUse();
                        PlayerStatus.weaponItem = null;
                    }
                }
                else if (item.itemType == "방어구")
                {
                    if (PlayerStatus.armorItem == null)
                    {
                        PlayerStatus.armorItem = item;
                        item.ItemChangeToUse();
                    }
                    else if (PlayerStatus.armorItem != item)
                    {
                        PlayerStatus.armorItem.ItemChangeToUse();
                        PlayerStatus.armorItem = item;
                        item.ItemChangeToUse();
                    }
                    else if (PlayerStatus.armorItem == item)
                    {
                        PlayerStatus.armorItem.ItemChangeToUse();
                        PlayerStatus.armorItem = null;
                    }
                }


                SelectEquiqqedNum(playerItemList, null);
            }
            else
            {
                SelectEquiqqedNum(playerItemList, "잘못된 입력입니다.");
            }
        }


        //메인 메뉴에서 상점으로 들어간 경우
        static void InputShopMenu(List<Item> playerItemList, string? message, ItemInstanceManager itemInstanceManager)
        {
            Console.Clear();
            itemInstanceManager.InstanceItem(5);
            Console.WriteLine(TextManager.ShopMenuTxt(playerItemList, itemInstanceManager));
            Console.WriteLine();
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = Console.ReadLine();

            switch (playerInput)
            {
                case "1":
                    InputShopBuy(playerItemList, null, itemInstanceManager);
                    break;

                case "2":
                    InputShopSell(playerItemList, null, itemInstanceManager);
                    break;

                case "0":
                    break;

                default:
                    InputShopMenu(playerItemList, "잘못된 입력입니다.", itemInstanceManager);
                    break;

            }
        }


        //상점에서 아이템 구매로 들어간 경우
        static void InputShopBuy(List<Item> playerItemList, string? message, ItemInstanceManager itemInstanceManager)
        {
            Console.Clear();
            Console.WriteLine(TextManager.ShopBuyTxt(playerItemList, itemInstanceManager));
            Console.WriteLine();
            Console.Write(TextManager.SelectNumberTxt(message));

            string playerInput = Console.ReadLine();

            if (playerInput == "0")
            { }
            else if (int.TryParse(playerInput, out int value) && itemInstanceManager.items.Count >= value)
            {
                Item item = itemInstanceManager.items[value - 1];
                if (PlayerStatus.playerDefaultGold >= item.itemBuyGold)
                {
                    //상점 리스트 -> 플레이어 인벤토리 리스트로 아이템 이동
                    PlayerStatus.playerDefaultGold -= item.itemBuyGold;
                    playerItemList.Add(item);
                    itemInstanceManager.items.Remove(item);
                    InputShopBuy(playerItemList, "구입이 완료되었습니다.", itemInstanceManager);
                }
                else
                {
                    InputShopBuy(playerItemList, "골드가 부족합니다.", itemInstanceManager);
                }
            }
            else
            {
                InputShopBuy(playerItemList, "잘못된 입력입니다.", itemInstanceManager);
            }
        }


        //상점에서 아이템 판매로 들어간 경우
        static void InputShopSell(List<Item> playerItemList, string? message, ItemInstanceManager itemInstanceManager)
        {

            Console.Clear();
            Console.WriteLine(TextManager.ShopSellTxt(playerItemList));
            Console.WriteLine();
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = Console.ReadLine();

            if (playerInput == "0")
            { }
            else if (int.TryParse(playerInput, out int value) && itemInstanceManager.items.Count >= value)
            {
                Item item = playerItemList[value - 1];
                if (item.useNow == false)
                {
                    PlayerStatus.playerDefaultGold += item.itemSellGold;
                    playerItemList.Remove(item);
                    InputShopSell(playerItemList, "판매 완료했습니다.", itemInstanceManager);
                }
                else
                {
                    InputShopSell(playerItemList, "장비를 해제해 주세요", itemInstanceManager);
                }
            }
            else
            {
                InputShopSell(playerItemList, "잘못된 입력입니다.", itemInstanceManager);
            }
        }


        //메인 메뉴에서 던전 입장으로 들어간 경우
        static void InputDungeonMenu(string? message)
        {

            Console.Clear();
            Console.WriteLine(TextManager.DungeonMenuTxt());
            Console.WriteLine();
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = Console.ReadLine();

            switch (playerInput)
            {
                case "0":
                    break;

                case "1"://쉬운 던전 입장
                    GoToDungeon(null, 1);
                    break;

                case "2"://일반 던전 입장
                    GoToDungeon(null, 2);
                    break;

                case "3"://어려운 던전 입장
                    GoToDungeon(null, 3);
                    break;

                default:
                    InputDungeonMenu("잘못된 입력입니다.");
                    break;

            }
        }


        //던전에 입장한 경우
        static void GoToDungeon(string? message, int dungeonLevel)
        {
            //던전 입장 시 체력 소모 로직 구현
            //1. 권장 방어력과 현재 방어력에 따라 체력 소모 반영
            //2. 디폴트 체력 소모는 20 ~ 35의 랜덤 값
            //3. 현재 방어력 - 권장 방어력의 수치만큼 체력 소모에서 뺌.
            //4. 던전의 권장 방어력보다 현재 방어력이 낮을 경우 실패율 40% 즉, 기본 성공 확률이 60%
            //5. 실패할 경우 체력 소모 50% 감소 및 보상 없음.
            //6. 체력 감소 후 현재 체력이 0이 된 경우 플레이어 사망 처리.

            Random random = new Random();
            int rand = random.Next(20, 36);
            int recommendArmor = 0;
            int sumDamage = 0;
            int rewardGold = 0;

            switch (dungeonLevel)
            {
                case 1:
                    recommendArmor = 5;
                    rewardGold = 1000;
                    break;

                case 2:
                    recommendArmor = 11;
                    rewardGold = 1700;
                    break;

                case 3:
                    recommendArmor = 17;
                    rewardGold = 2500;
                    break;
            }
            sumDamage = rand + recommendArmor - PlayerStatus.playerDefaultDEF;

            rand = random.Next(PlayerStatus.playerDefaultATK, PlayerStatus.playerDefaultATK * 2);
            rewardGold = rewardGold * rand / 100;

            rand = random.Next(0, 10);

            //플레이어의 방어력이 권장 방어력보다 높은 경우 성공
            //기본 성공 확률 60%의 확률로 성공
            if (PlayerStatus.playerDefaultDEF >= recommendArmor || rand >= 4)
            {
                //던전 성공***
                //체력 소모
                //현재 체력 - 체력 소모가 0 이하일 경우 사망 처리
                //골드 증가
                //던전 성공 텍스트 출력

                //현재 체력이 0 이하가 될 경우 사망 처리
                if (PlayerStatus.playerNowHP - sumDamage <= 0)
                {
                    PlayerStatus.playerNowHP = 0;
                    PlayerDieMenu(null);
                }
                //던전 성공
                else
                {
                    PlayerStatus.playerNowHP -= sumDamage;
                    PlayerStatus.playerDefaultGold += rewardGold;
                    DungeonClearMenu(null, dungeonLevel, sumDamage, rewardGold);
                }
            }
            //던전에 실패한 경우
            else
            {
                sumDamage = sumDamage / 2;

                //현재 체력이 0 이하가 될 경우 사망 처리
                if (PlayerStatus.playerNowHP - sumDamage <= 0)
                {
                    PlayerStatus.playerNowHP = 0;
                    PlayerDieMenu(null);
                }
                //던전 실패
                else
                {
                    PlayerStatus.playerNowHP -= sumDamage;
                    DungeonFaildMenu(null, dungeonLevel, sumDamage);
                }
            }
        }


        //사망 처리 창으로 넘어가는 메서드
        static void PlayerDieMenu(string? message)
        {
            Console.Clear();
            Console.WriteLine(TextManager.PlayerDieTxt());
            Console.WriteLine();
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = Console.ReadLine();

            switch (playerInput)
            {
                case "0":
                    break;

                case "1"://게임 다시 시작하기
                    break;

                default:
                    InputRestMenu("잘못된 입력입니다.");
                    break;

            }
        }


        //탐험 성공 창으로 넘어가는 메서드
        static void DungeonClearMenu(string? message, int dungeonLevel, int damage, int rewardGold)
        {
            Console.Clear();
            Console.WriteLine(TextManager.DungeonClearTxt(dungeonLevel, damage, rewardGold));
            Console.WriteLine();
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = Console.ReadLine();

            switch (playerInput)
            {
                case "0":
                    break;

                default:
                    DungeonClearMenu("잘못된 입력입니다.", dungeonLevel, damage, rewardGold);
                    break;

            }
        }


        //탐험 실패 창으로 넘어가는 메서드
        static void DungeonFaildMenu(string? message, int dungeonLevel, int damage)
        {
            Console.Clear();
            Console.WriteLine(TextManager.DungeonFailedTxt(dungeonLevel, damage));
            Console.WriteLine();
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = Console.ReadLine();

            switch (playerInput)
            {
                case "0":
                    break;

                default:
                    DungeonFaildMenu("잘못된 입력입니다.", dungeonLevel, damage);
                    break;

            }
        }


        //메인 메뉴에서 휴식으로 입장한 경우
        static void InputRestMenu(string? message)
        {
            Console.Clear();
            Console.WriteLine(TextManager.RestMenuTxt());
            Console.WriteLine();
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = Console.ReadLine();

            switch (playerInput)
            {
                case "0":
                    break;

                case "1":
                    if (PlayerStatus.playerDefaultGold >= 500)
                    {
                        PlayerStatus.playerNowHP = PlayerStatus.playerMaxHP;
                        PlayerStatus.playerDefaultGold -= 500;
                        InputRestMenu("휴식을 완료했습니다.");
                    }
                    else
                    {
                        InputRestMenu("Gold 가 부족합니다.");
                    }
                    break;

                default:
                    InputRestMenu("잘못된 입력입니다.");
                    break;

            }
        }
    }


    //플레이어의 기본적인 데이터가 들어있는 클래스
    public static class PlayerStatus
    {
        public static string playerName = "르탄이";
        public static string playerJob = "도적";

        public static int playerDefalutLevel = 1;
        public static int playerMaxHP = 100;
        public static int playerDefaultATK = 10;
        public static int playerDefaultDEF = 5;
        public static int playerDefaultGold = 2000;

        public static int playerNowLevel;
        public static int playerNowHP = 100;
        public static int playerNowATK;
        public static int playerNowDEF;
        public static int playerNowGold;

        public static Item weaponItem;
        public static Item armorItem;

    }


    //다양한 텍스트 저장
    public static class TextManager
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
        public static string PlayerStatusTxt(List<Item> playerItemList)
        {
            string itemTxt = "";
            StringBuilder result = new StringBuilder();

            int sumATK = 0;
            int sumDEF = 0;
            int sumHP = 0;

            foreach (Item item in playerItemList)
            {
                if (item.useNow)
                {
                    sumATK += item.itemATK;
                    sumDEF += item.itemDEF;
                    sumHP += item.itemHP;
                }
            }

            result.AppendLine("상태 보기");
            result.AppendLine("캐릭터의 정보가 표시됩니다");
            result.AppendLine();
            result.AppendLine($"이름 : {PlayerStatus.playerName}");
            result.AppendLine($"Lv. {PlayerStatus.playerDefalutLevel}");
            result.AppendLine($"chad ( {PlayerStatus.playerJob} )");
            result.AppendLine($"공격력 : {PlayerStatus.playerDefaultATK + sumATK} (+{sumATK})");
            result.AppendLine($"방어력 : {PlayerStatus.playerDefaultDEF + sumDEF} (+{sumDEF})");
            result.AppendLine($"체 력 : {PlayerStatus.playerMaxHP + sumHP} (+{sumHP})");
            result.AppendLine($"Gold : {PlayerStatus.playerDefaultGold} G");
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
                itemTxt.AppendLine("-" + item.itemEquippedSettingTxt);
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
                itemTxt.AppendLine($"- {i + 1}{playerItemList[i].itemEquippedSettingTxt}");
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
        public static string ShopMenuTxt(List<Item> playerItemList, ItemInstanceManager itemInstanceManager)
        {
            StringBuilder itemTxt = new StringBuilder();
            foreach (Item item in itemInstanceManager.items)
            {
                itemTxt.AppendLine(item.itemEquippedSettingTxt);
            }


            StringBuilder result = new StringBuilder();
            result.AppendLine("상점");
            result.AppendLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            result.AppendLine();
            result.AppendLine("[보유 골드]");
            result.AppendLine($"{PlayerStatus.playerDefaultGold} G");
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
        public static string ShopBuyTxt(List<Item> playerItemList, ItemInstanceManager itemInstanceManager)
        {
            StringBuilder itemTxt = new StringBuilder();
            for (int i = 0; i < itemInstanceManager.items.Count; i++)
            {
                itemTxt.AppendLine($"- {i + 1}" + itemInstanceManager.items[i].itemEquippedSettingTxt);
            }


            StringBuilder result = new StringBuilder();
            result.AppendLine("상점");
            result.AppendLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            result.AppendLine();
            result.AppendLine("[보유 골드]");
            result.AppendLine($"{PlayerStatus.playerDefaultGold} G");
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
                itemTxt.AppendLine($"- {i + 1}{playerItemList[i].itemEquippedSettingTxt}");
            }

            result.AppendLine("상점 - 아이템 판매");
            result.AppendLine("필요없는 아이템을 판매할 수 있습니다.");
            result.AppendLine();
            result.AppendLine("[보유 골드]");
            result.AppendLine($"{PlayerStatus.playerDefaultGold} G");
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
            result.AppendLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {PlayerStatus.playerDefaultGold} G)");
            result.AppendLine($"최대 체력 : {PlayerStatus.playerMaxHP}");
            result.AppendLine($"현재 체력 : {PlayerStatus.playerNowHP}");
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
        public static string DungeonClearTxt(int dungeonLevel, int damage, int rewardGold)
        {
            StringBuilder result = new StringBuilder();

            string dungeonMessage = DungeonLevelSwitch(dungeonLevel);

            result.AppendLine("던전 클리어");
            result.AppendLine("축하합니다!!");
            result.AppendLine($"{dungeonMessage} 을 클리어 하였습니다.");
            result.AppendLine();
            result.AppendLine("[탐험 결과]");
            result.AppendLine($"체력 {PlayerStatus.playerNowHP} -> {PlayerStatus.playerNowHP - damage}");
            result.AppendLine($"골드 {PlayerStatus.playerDefaultGold} -> {PlayerStatus.playerDefaultGold + rewardGold}");
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
            result.AppendLine($"체력 {PlayerStatus.playerNowHP} -> {PlayerStatus.playerNowHP - damage}");
            result.AppendLine($"골드 {PlayerStatus.playerDefaultGold}");
            result.AppendLine();
            result.AppendLine("0. 나가기");

            return result.ToString();
        }


        //사망 시 출력할 텍스트
        public static string PlayerDieTxt()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("플레이어 사망");
            result.AppendLine("플레이어가 사망했습니다.");
            result.AppendLine();
            result.AppendLine("[탐험 결과]");
            result.AppendLine($"골드 {PlayerStatus.playerDefaultGold}");//***추후 플레이어 스테이터스 추가
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

            result.AppendLine("당신의 직업을 선택해 주세요");
            result.AppendLine();
            result.AppendLine("1. 전사");
            result.AppendLine("2. 도적");
            result.AppendLine("3. 궁수");

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
    }

    //아이템 필수 정보
    public class Item
    {
        public readonly string itemName;
        public string itemType;
        public bool useNow;
        public int itemATK;
        public int itemDEF;
        public int itemHP;
        public int itemBuyGold;
        public int itemSellGold;
        public readonly string itemInformationTxt;
        public string itemEquippedSettingTxt;

        private float itemSellMultiple = 0.8f;


        //Item을 생성하기 위해 선언해야 하는 생성자
        public Item(string itemName, string itemType, bool useNow, int itemATK, int itemDEF, int itemHP, int itemBuyGold, string itemInformationTxt)
        {
            this.itemName = itemName;
            this.itemType = itemType;
            this.useNow = useNow;
            this.itemATK = itemATK;
            this.itemDEF = itemDEF;
            this.itemHP = itemHP;
            this.itemBuyGold = itemBuyGold;
            this.itemSellGold = (int)(itemBuyGold * itemSellMultiple);
            this.itemInformationTxt = itemInformationTxt;
            this.itemEquippedSettingTxt = InputItemEquippedSettingTxt(useNow);
        }


        //현재 useNow 값에 따라 설명 텍스트를 변경하는 메서드
        public string InputItemEquippedSettingTxt(bool useNow)
        {
            string result = "";

            if (useNow)
            {
                result = $" [E]{itemName} | 공격력 +{itemATK} | 방어력 +{itemDEF} | {itemInformationTxt} | 판매 가치 : {itemSellGold}";
            }
            else
            {
                result = $" {itemName} | 공격력 +{itemATK} | 방어력 +{itemDEF} | {itemInformationTxt} | 판매 가치 : {itemSellGold}";
            }

            return result;
        }


        //장비 탈착 변셩 시 텍스트 교체
        public void ItemChangeToUse()
        {
            useNow = !useNow;

            itemEquippedSettingTxt = InputItemEquippedSettingTxt(useNow);
        }
    }


    //아이템 생성 관련 클래스
    public class ItemInstanceManager
    {
        public List<Item> items = new List<Item>();
        Random random = new Random();


        //생성한 아이템 값에 랜덤한 수치를 더해 무작위성을 추가하는 메서드
        public Item RearrangeValue(Item item, int value)
        {
            Random random = new Random();
            int rand = random.Next(-value, value + 1);

            if (item.itemType == "무기")
            { item.itemATK += rand; }
            else if (item.itemType == "방어구")
            { item.itemDEF += rand; }

            //아이템 설명 텍스트 초기화
            item.itemEquippedSettingTxt = item.InputItemEquippedSettingTxt(false);

            return item;
        }


        //아이템을 랜덤으로 생성하는 메소드
        public void InstanceItem(int num)
        {
            if (items.Count == 0)
            {
                for (int i = 0; i < num; i++)
                {
                    int rand = random.Next(0, 10);
                    Item item;

                    if (rand == 0)
                    {
                        //상급 아이템 생성
                        item = CreateItemFromData(highItemData);
                        item = RearrangeValue(item, 5);
                    }
                    else if (rand <= 3)
                    {
                        //중급 아이템 생성
                        item = CreateItemFromData(nomalItemData);
                        item = RearrangeValue(item, 3);
                    }
                    else
                    {
                        //하급 아이템 생성
                        item = CreateItemFromData(lowItemData);
                        item = RearrangeValue(item, 1);
                    }
                    items.Add(item);
                }
                //아이템 이름대로 정렬
                items.Sort((x, y) => x.itemName.CompareTo(y.itemName));
            }
        }


        //무작위로 생성한 List<Item>을 초기화하는 메서드
        public void ResetItemsList()
        {
            items.Clear();
        }


        //저장되어 있는 Dictionary 아이템 값을 Item클래스에 맞춰서 반환하는 메서드***(추후 개선 필요)
        private Item CreateItemFromData(Dictionary<string, (string type, int itemATK, int itemDEF, int itemHP, int itemBuyGold, string information)> itemData)
        {
            List<string> keys = new List<string>(itemData.Keys);
            string selectedKey = keys[random.Next(keys.Count)];
            var data = itemData[selectedKey];
            return new Item(selectedKey, data.type, false, data.itemATK, data.itemDEF, data.itemHP, data.itemBuyGold, data.information);
        }


        //아이템들을 저장하고 있는 변수들.
        public Dictionary<string, (string type, int itemATK, int itemDEF, int itemHP, int itemBuyGold, string information)> lowItemData =
    new Dictionary<string, (string type, int itemATK, int itemDEF, int itemHP, int itemBuyGold, string information)>
            {
                {"나무 검", ("무기",3, 0, 0, 100, "없는 것 보다 나은 검이다.") },
                {"나무 막대기", ("무기", 2, 0, 0, 30, "없으나 마나 한 막대기다.") },
                {"낡은 조끼",("방어구", 0, 2, 0, 30, "없으나 마나 한 옷이다") },
                {"조끼", ("방어구", 0, 3, 0, 30, "없는 것 보다 나은 옷이다.") }
            };
        public Dictionary<string, (string type, int itemATK, int itemDEF, int itemHP, int itemBuyGold, string information)> nomalItemData =
    new Dictionary<string, (string type, int itemATK, int itemDEF, int itemHP, int itemBuyGold, string information)>
    {
                {"철 검", ("무기",8, 0, 0, 300, "좋은 검이다.") },
                {"철 방망이", ("무기", 5, 0, 0, 200, "쓸만한 방망이다.") },
                {"철 갑옷",("방어구", 0, 8, 0, 300, "좋은 방어구다") },
                {"가죽 갑옷", ("방어구", 0, 5, 0, 200, "쓸만한 방어구다.") }
    };
        public Dictionary<string, (string type, int itemATK, int itemDEF, int itemHP, int itemBuyGold, string information)> highItemData =
    new Dictionary<string, (string type, int itemATK, int itemDEF, int itemHP, int itemBuyGold, string information)>
    {
                {"전설의 검", ("무기",15, 0, 0, 1000, "전설의 검이다.") },
                {"강철 검", ("무기", 12, 0, 0, 500, "철 검보다 단단한 검이다.") },
                {"전설의 갑옷",("방어구", 0, 15, 0, 1000, "전설의 갑옷이다") },
                {"강철 갑옷", ("방어구", 0, 12, 0, 500, "철 갑옷보다 단단한 갑옷이다.") }
    };

    }
}

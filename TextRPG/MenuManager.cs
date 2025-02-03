using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TextRPG.Player.PlayerAbilityStatus;

namespace TextRPG
{
    internal class MenuManager
    {
        //플레이어 이름 입력 창으로 들어간 경우
        public static string InputPlayerNameMenu(string? message)
        {
            string playerName = "";

            while (true)
            {
                Console.Clear();
                Console.Write(TextManager.InputPlayerNameTxt(message));
                playerName = Console.ReadLine();

                if (playerName.Replace(" ", "") == "")
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
        public static int InputPlayerSelectJobMenu(string? message)
        {
            string playerInput = "";
            int jobsNumber = 0;
            string jobName = "";

            while (true)
            {
                Console.Clear();
                Console.WriteLine(TextManager.InputPlayerJobTxt());
                Console.Write(TextManager.SelectNumberTxt(message));
                playerInput = Console.ReadLine();

                if (int.TryParse(playerInput, out jobsNumber))
                {
                    //해당 입력값이 직업에 정의되어있는지 확인
                    if (Enum.IsDefined(typeof(JobType), jobsNumber))
                    {
                        jobName = Enum.GetName(typeof(JobType), jobsNumber);
                        break;
                    }
                }

                message = "잘못된 입력입니다";
            }

            //입력한 값을 확정할 것인지 추가 확인
            if (CheckedPlayerInput(null, jobName) == false)
            {
                jobsNumber = InputPlayerSelectJobMenu(null);
            }
            return jobsNumber;
        }


        //플레이어가 입력한 것을 확정 지을지 묻는 메소드
        public static bool CheckedPlayerInput(string? message, string playerInput)
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
        public static void InputStatusMenu(List<Item> playerItemList, string? message)
        {
            Console.Clear();
            Console.WriteLine(TextManager.PlayerAbilityStatusTxt(playerItemList));
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
        public static void InputInventoryMenu(List<Item> playerItemList, string? message)
        {
            Console.Clear();
            Console.WriteLine(TextManager.PlayerInventoryTxt(playerItemList));
            Console.WriteLine();
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = Console.ReadLine();

            switch (playerInput)
            {
                case "1":
                    SelectEquiqqedNumMenu(playerItemList, null);
                    break;

                case "0":
                    break;

                default:
                    InputInventoryMenu(playerItemList, "잘못된 입력입니다.");
                    break;

            }
        }


        //인벤토리에서 아이템 장착으로 들어간 경우
        public static void SelectEquiqqedNumMenu(List<Item> playerItemList, string? message)
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
                    if (weaponItem == null)
                    {
                        weaponItem = item;
                        item.ItemChangeToUse();
                    }
                    else if (weaponItem != item)
                    {
                        weaponItem.ItemChangeToUse();
                        weaponItem = item;
                        item.ItemChangeToUse();
                    }
                    else if (weaponItem == item)
                    {
                        weaponItem.ItemChangeToUse();
                        weaponItem = null;
                    }
                }
                else if (item.itemType == "방어구")
                {
                    if (armorItem == null)
                    {
                        armorItem = item;
                        item.ItemChangeToUse();
                    }
                    else if (armorItem != item)
                    {
                        armorItem.ItemChangeToUse();
                        armorItem = item;
                        item.ItemChangeToUse();
                    }
                    else if (armorItem == item)
                    {
                        armorItem.ItemChangeToUse();
                        armorItem = null;
                    }
                }


                SelectEquiqqedNumMenu(playerItemList, null);
            }
            else
            {
                SelectEquiqqedNumMenu(playerItemList, "잘못된 입력입니다.");
            }
        }


        //메인 메뉴에서 상점으로 들어간 경우
        public static void InputShopMenu(List<Item> playerItemList, string? message)
        {
            Console.Clear();
            ItemInstanceManager.InstanceItem(5);
            Console.WriteLine(TextManager.ShopMenuTxt(playerItemList));
            Console.WriteLine();
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = Console.ReadLine();

            switch (playerInput)
            {
                case "1":
                    InputShopBuyMenu(playerItemList, null);
                    break;

                case "2":
                    InputShopSellMenu(playerItemList, null);
                    break;

                case "0":
                    break;

                default:
                    InputShopMenu(playerItemList, "잘못된 입력입니다.");
                    break;

            }
        }


        //상점에서 아이템 구매로 들어간 경우
        public static void InputShopBuyMenu(List<Item> playerItemList, string? message)
        {
            Console.Clear();
            Console.WriteLine(TextManager.ShopBuyTxt(playerItemList));
            Console.WriteLine();
            Console.Write(TextManager.SelectNumberTxt(message));

            string playerInput = Console.ReadLine();

            if (playerInput == "0")
            { }
            else if (int.TryParse(playerInput, out int value) && ItemInstanceManager.items.Count >= value)
            {
                Item item = ItemInstanceManager.items[value - 1];
                if (playerNowGold >= item.itemBuyGold)
                {
                    //상점 리스트 -> 플레이어 인벤토리 리스트로 아이템 이동
                    playerNowGold -= item.itemBuyGold;
                    playerItemList.Add(item);
                    ItemInstanceManager.items.Remove(item);
                    InputShopBuyMenu(playerItemList, "구입이 완료되었습니다.");
                }
                else
                {
                    InputShopBuyMenu(playerItemList, "골드가 부족합니다.");
                }
            }
            else
            {
                InputShopBuyMenu(playerItemList, "잘못된 입력입니다.");
            }
        }


        //상점에서 아이템 판매로 들어간 경우
        public static void InputShopSellMenu(List<Item> playerItemList, string? message)
        {

            Console.Clear();
            Console.WriteLine(TextManager.ShopSellTxt(playerItemList));
            Console.WriteLine();
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = Console.ReadLine();

            if (playerInput == "0")
            { }
            else if (int.TryParse(playerInput, out int value) && ItemInstanceManager.items.Count >= value)
            {
                Item item = playerItemList[value - 1];
                if (item.useNow == false)
                {
                    playerNowGold += item.itemSellGold;
                    playerItemList.Remove(item);
                    InputShopSellMenu(playerItemList, "판매 완료했습니다.");
                }
                else
                {
                    InputShopSellMenu(playerItemList, "장비를 해제해 주세요");
                }
            }
            else
            {
                InputShopSellMenu(playerItemList, "잘못된 입력입니다.");
            }
        }


        //메인 메뉴에서 던전 입장으로 들어간 경우
        public static void InputDungeonMenu(string? message)
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
                    SelectDungeonLevelMenu(null, 1);
                    break;

                case "2"://일반 던전 입장
                    SelectDungeonLevelMenu(null, 2);
                    break;

                case "3"://어려운 던전 입장
                    SelectDungeonLevelMenu(null, 3);
                    break;

                default:
                    InputDungeonMenu("잘못된 입력입니다.");
                    break;

            }
        }


        //던전에 입장한 경우
        public static void SelectDungeonLevelMenu(string? message, int dungeonLevel)
        {
            Random random = new Random();
            int rand = random.Next(20, 36);
            int levelValue = 0;
            int recommendArmor = 0;
            int sumDamage = 0;
            int rewardGold = 0;

            switch (dungeonLevel)
            {
                case 1:
                    recommendArmor = 5;
                    rewardGold = 1000;
                    levelValue = random.Next(2, 4);
                    break;

                case 2:
                    recommendArmor = 11;
                    rewardGold = 1700;
                    levelValue = random.Next(4, 7);
                    break;

                case 3:
                    recommendArmor = 17;
                    rewardGold = 2500;
                    levelValue = random.Next(7, 11);
                    break;
            }
            sumDamage = rand + recommendArmor - playerNowDEF;

            rand = random.Next(playerNowATK, playerNowATK * 2);
            rewardGold = rewardGold * rand / 100;

            rand = random.Next(0, 10);

            //플레이어의 방어력이 권장 방어력보다 높은 경우 성공
            //기본 성공 확률 60%의 확률로 성공
            if (playerNowDEF >= recommendArmor || rand >= 4)
            {
                //현재 체력이 0 이하가 될 경우 사망 처리
                if (playerNowHP - sumDamage <= 0)
                {
                    playerNowHP = 0;
                    PlayerDieMenu(null);
                }
                //던전 성공
                else
                {
                    playerNowHP -= sumDamage;
                    playerNowGold += rewardGold;
                    playerLecelNowValue += levelValue;
                    DungeonClearMenu(null, dungeonLevel, sumDamage, rewardGold, levelValue);

                    //성공 이후 플레이어의 레벨이 요구치를 충족했을 경우
                    if(playerLevelRequestValue <= playerLecelNowValue)
                    {
                        playerNowLevel += 1;
                        PlayerLevelUpMenu(null);

                        playerLevelRequestValue = playerLevelDefaultRequestValue + (int)(playerLevelRequestValue * 1.2f);
                    }
                }
            }
            //던전에 실패한 경우
            else
            {
                sumDamage = sumDamage / 2;

                //현재 체력이 0 이하가 될 경우 사망 처리
                if (playerNowHP - sumDamage <= 0)
                {
                    playerNowHP = 0;
                    PlayerDieMenu(null);
                }
                //던전 실패
                else
                {
                    playerNowHP -= sumDamage;
                    DungeonFaildMenu(null, dungeonLevel, sumDamage);
                }
            }
        }


        //사망 처리 창으로 넘어가는 메서드
        public static void PlayerDieMenu(string? message)
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
        public static void DungeonClearMenu(string? message, int dungeonLevel, int damage, int rewardGold, int levelValue)
        {
            Console.Clear();
            Console.WriteLine(TextManager.DungeonClearTxt(dungeonLevel, damage, rewardGold, levelValue));
            Console.WriteLine();
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = Console.ReadLine();

            switch (playerInput)
            {
                case "0":
                    break;

                default:
                    DungeonClearMenu("잘못된 입력입니다.", dungeonLevel, damage, rewardGold, levelValue);
                    break;

            }
        }


        //탐험 실패 창으로 넘어가는 메서드
        public static void DungeonFaildMenu(string? message, int dungeonLevel, int damage)
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
        public static void InputRestMenu(string? message)
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
                    if (playerNowGold >= 500)
                    {
                        playerNowHP = playerMaxHP;
                        playerNowGold -= 500;
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


        //레벨 업 창으로 넘어간 경우
        public static void PlayerLevelUpMenu(string? message)
        {
            Console.Clear();
            Console.WriteLine(TextManager.PlayerLevelUpTxt());
            Console.WriteLine();
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = Console.ReadLine();

            switch (playerInput)
            {
                case "0":
                    break;
                    
                default:
                    PlayerLevelUpMenu("잘못된 입력입니다.");
                    break;

            }
        }
    }
}

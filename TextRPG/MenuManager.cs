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
        #region 플레이어 관련 메뉴 /*이름 /*직업 /*입력 확정 /*상태 /*인벤토리 /*장착 /*레벨 업

        //플레이어 이름 입력 창으로 들어간 경우
        public static string InputPlayerNameMenu(string? message)
        {
            string playerName = "";

            while (true)
            {
                Console.Clear();
                Console.Write(TextManager.InputPlayerNameTxt(message));
                playerName = InputNumber();

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
                playerInput = InputNumber();

                //해당 입력값이 직업에 정의되어있는지 확인
                if (int.TryParse(playerInput, out jobsNumber) &&
                    Enum.IsDefined(typeof(JobType), jobsNumber))
                {
                    jobName = Enum.GetName(typeof(JobType), jobsNumber);
                    break;
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
            string playerNum = InputNumber();

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


        //메인 메뉴에서 플레이어 상태 창으로 들어간 경우
        public static void InputStatusMenu(List<Item> playerItemList, string? message)
        {
            Console.Clear();
            Console.WriteLine(TextManager.PlayerAbilityStatusTxt(playerItemList));
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = InputNumber();

            switch (playerInput)
            {
                case "0":
                    break;

                default:
                    InputStatusMenu(playerItemList, "잘못된 입력입니다.");
                    break;

            }
        }


        //메인 메뉴에서 플레이어 인벤토리로 들어간 경우
        public static void InputInventoryMenu(List<Item> playerItemList, string? message)
        {
            Console.Clear();
            Console.WriteLine(TextManager.PlayerInventoryTxt(playerItemList));
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = InputNumber();

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


        //플레이어 인벤토리에서 아이템 장착으로 들어간 경우
        public static void SelectEquiqqedNumMenu(List<Item> playerItemList, string? message)
        {
            Console.Clear();
            Console.WriteLine(TextManager.PlayerEquippedSettingTxt(playerItemList));
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = InputNumber();

            if (playerInput == "0")
            { }
            else if (int.TryParse(playerInput, out int value) && playerItemList.Count >= value)
            {
                Console.Clear();

                Item item = playerItemList[value - 1];

                if (item.ItemType == "무기")
                {
                    if (PlayerWeaponItem == null)
                    {
                        PlayerWeaponItem = item;
                        item.ItemChangeToUse();
                    }
                    else if (PlayerWeaponItem != item)
                    {
                        PlayerWeaponItem.ItemChangeToUse();
                        PlayerWeaponItem = item;
                        item.ItemChangeToUse();
                    }
                    else if (PlayerWeaponItem == item)
                    {
                        PlayerWeaponItem.ItemChangeToUse();
                        PlayerWeaponItem = null;
                    }
                }
                else if (item.ItemType == "방어구")
                {
                    if (PlayerArmorItem == null)
                    {
                        PlayerArmorItem = item;
                        item.ItemChangeToUse();
                    }
                    else if (PlayerArmorItem != item)
                    {
                        PlayerArmorItem.ItemChangeToUse();
                        PlayerArmorItem = item;
                        item.ItemChangeToUse();
                    }
                    else if (PlayerArmorItem == item)
                    {
                        PlayerArmorItem.ItemChangeToUse();
                        PlayerArmorItem = null;
                    }
                }

                AudioManager.PlayItemEquippedSE();//장비 장착 사운드 실행
                SelectEquiqqedNumMenu(playerItemList, null);
            }
            else
            {
                SelectEquiqqedNumMenu(playerItemList, "잘못된 입력입니다.");
            }
        }


        //레벨 업 창으로 넘어간 경우
        public static void PlayerLevelUpMenu(string? message, int beforeMaxHP, int beforeATK, int beforeDEF, int beforeNowHP)
        {
            Console.Clear();
            Console.WriteLine(TextManager.PlayerLevelUpTxt(beforeMaxHP, beforeATK, beforeDEF, beforeNowHP));
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = InputNumber();

            switch (playerInput)
            {
                case "0":
                    break;

                default:
                    PlayerLevelUpMenu("잘못된 입력입니다.", beforeMaxHP, beforeATK, beforeDEF, beforeNowHP);
                    break;

            }
        }
        #endregion


        #region 상점 관련 /*상점 입장 /*아이템 판매 /*아이템 구매
        //메인 메뉴에서 상점으로 들어간 경우
        public static void InputShopMenu(List<Item> playerItemList, string? message)
        {
            Console.Clear();
            Console.WriteLine(TextManager.ShopMenuTxt(playerItemList));
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = InputNumber();

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
            Console.Write(TextManager.SelectNumberTxt(message));

            string playerInput = InputNumber();

            if (playerInput == "0") { }
            else if (int.TryParse(playerInput, out int value) && ItemInstanceManager.items.Count >= value)
            {
                Item item = ItemInstanceManager.items[value - 1];
                if (PlayerNowGold >= item.ItemBuyGold)
                {
                    //상점 리스트 -> 플레이어 인벤토리 리스트로 아이템 이동
                    PlayerNowGold -= item.ItemBuyGold;
                    playerItemList.Add(item);
                    
                    ItemInstanceManager.items.Remove(item);
                    DataManager.PlayerDataSave();       //데이터 저장
                    AudioManager.PlayItemBuyOrSellSE();//구매 소리 출력
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
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = InputNumber();

            if (playerInput == "0")
            { }
            else if (int.TryParse(playerInput, out int value) && playerItemList.Count >= value)
            {
                Item item = playerItemList[value - 1];
                if (item.UseNow == false)
                {
                    PlayerNowGold += item.ItemSellGold;
                    playerItemList.Remove(item);
                    DataManager.PlayerDataSave();       //데이터 저장
                    AudioManager.PlayItemBuyOrSellSE();//판매 소리 출력
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
        #endregion


        #region 던전 관련 /*던전 입장 /*성공 /*실패 /*사망
        //메인 메뉴에서 던전 입장으로 들어간 경우
        public static void InputDungeonMenu(string? message)
        {

            Console.Clear();
            Console.WriteLine(TextManager.DungeonMenuTxt());
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = InputNumber();

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
            sumDamage = rand + recommendArmor - PlayerNowDEF;

            rand = random.Next(PlayerNowATK, PlayerNowATK * 2);
            rewardGold = rewardGold * rand / 100;

            rand = random.Next(0, 10);

            //플레이어의 방어력이 권장 방어력보다 높은 경우 성공
            //기본 성공 확률 60%의 확률로 성공
            if (PlayerNowDEF >= recommendArmor || rand >= 4)
            {
                //현재 체력이 0 이하가 될 경우 사망 처리
                if (PlayerNowHP - sumDamage <= 0)
                {
                    PlayerDieAction();
                }
                //던전 성공
                else
                {
                    PlayerNowHP -= sumDamage;
                    PlayerNowGold += rewardGold;
                    PlayerLevelNowValue += levelValue;

                    int beforeMaxHP = PlayerMaxHP;
                    int beforeATK = PlayerNowATK;
                    int beforeDEF = PlayerNowDEF;
                    int beforeNowLevel = PlayerNowLevel;
                    int beforeLevelRequestValue = PlayerLevelRequestValue;
                    int beforeNowHP = PlayerNowHP;


                    //성공 이후 플레이어의 레벨이 요구치를 충족했을 경우
                    if (PlayerLevelRequestValue <= PlayerLevelNowValue)
                    {
                        Player.PlayerLevelUp();
                        PlayerLevelRequestValue = PlayerLevelDefaultRequestValue + (int)(PlayerLevelRequestValue * 1.2f);
                    }
                    DataManager.PlayerDataSave();

                    //상점 아이템 초기화
                    ItemInstanceManager.InstanceItem();

                    AudioManager.PlayDungeonClearSE(); //던전 클리어 음성 출력
                    DungeonClearMenu(null, dungeonLevel, sumDamage, rewardGold, levelValue, beforeNowLevel, beforeLevelRequestValue);

                    if (beforeLevelRequestValue <= PlayerLevelNowValue)
                    {
                        AudioManager.PlayLevelUpSE(); //레벨업 음성 출력
                        PlayerLevelUpMenu(null, beforeMaxHP, beforeATK, beforeDEF, beforeNowHP);
                    }
                }
            }
            //던전에 실패한 경우
            else
            {
                sumDamage = sumDamage / 2;

                //현재 체력이 0 이하가 될 경우 사망 처리
                if (PlayerNowHP - sumDamage <= 0)
                {
                    PlayerDieAction();
                }
                //던전 실패
                else
                {
                    PlayerNowHP -= sumDamage;
                    DataManager.PlayerDataSave();
                    AudioManager.PlayDungeonFailedSE(); //던전 실패 음성 출력
                    DungeonFaildMenu(null, dungeonLevel, sumDamage);
                }
            }
        }


        //사망 처리 창으로 넘어가는 메서드
        public static void PlayerDieMenu(string? message, string name, string job, int level, int hp, int ATK, int DEF, int levelValue, int requestLevelValue, int gold)
        {
            Console.Clear();
            Console.WriteLine(TextManager.PlayerDieTxt(name, job, level, hp, ATK, DEF, levelValue, requestLevelValue, gold));
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = InputNumber();

            switch (playerInput)
            {
                case "0": //게임 다시 시작하기
                    break;

                default:
                    PlayerDieMenu("잘못된 입력입니다.", name, job, level, hp, ATK, DEF, levelValue, requestLevelValue, gold);
                    break;

            }
        }


        //플레이어가 사망한 후 데이터 처리 및 다음 메뉴로 넘어가는 메서드
        public static void PlayerDieAction()
        {
            PlayerNowHP = 0;
            string name = PlayerName;
            string job = PlayerJob;
            int level = PlayerNowLevel;
            int hp = PlayerMaxHP;
            int ATK = PlayerNowATK;
            int DEF = PlayerNowDEF;
            int tempLevelValue = PlayerLevelNowValue;
            int tempLevelRequestValue = PlayerLevelRequestValue;
            int gold = PlayerNowGold;

            DataManager.PlayerDataClear(); //플레이어 데이터 초기화 후 세이브
            DataManager.PlayerDataSave();
            AudioManager.PlayPlayerDieSE(); //플레이어 사망 음성 출력
            PlayerDieMenu(null, name, job, level, hp, ATK, DEF, tempLevelValue, tempLevelRequestValue, gold);
        }


        //탐험 성공 창으로 넘어가는 메서드
        public static void DungeonClearMenu(string? message, int dungeonLevel, int damage, int rewardGold, int levelValue, int beforeLevel, int beforeLevelRequest)
        {
            Console.Clear();
            Console.WriteLine(TextManager.DungeonClearTxt(dungeonLevel, damage, rewardGold, levelValue, beforeLevel, beforeLevelRequest));
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = InputNumber();

            switch (playerInput)
            {
                case "0":
                    break;

                default:
                    DungeonClearMenu("잘못된 입력입니다.", dungeonLevel, damage, rewardGold, levelValue, beforeLevel, beforeLevelRequest);
                    break;

            }
        }


        //탐험 실패 창으로 넘어가는 메서드
        public static void DungeonFaildMenu(string? message, int dungeonLevel, int damage)
        {
            Console.Clear();
            Console.WriteLine(TextManager.DungeonFailedTxt(dungeonLevel, damage));
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = InputNumber();

            switch (playerInput)
            {
                case "0":
                    break;

                default:
                    DungeonFaildMenu("잘못된 입력입니다.", dungeonLevel, damage);
                    break;

            }
        }
        #endregion


        //메인 메뉴 입장
        public static void MainMenu(string? message)
        {
            while (true)
            {
                if (PlayerName == null ||
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
                string playerInput = InputNumber();

                message = null;

                switch (playerInput)
                {
                    case "1"://상태 창 입장
                        InputStatusMenu(PlayerInventoryItem, null);
                        break;

                    case "2"://인벤토리 창 입장
                        InputInventoryMenu(PlayerInventoryItem, null);
                        break;

                    case "3"://상점 창 입장
                        InputShopMenu(PlayerInventoryItem, null);
                        break;

                    case "4"://던전 창 입장
                        InputDungeonMenu(null);
                        break;

                    case "5"://휴식 창 입장
                        InputRestMenu(null);
                        break;

                    case "0"://플레이어 재시작
                        PlayerReStartMenu(null);
                        break;

                    default:
                        message = "잘못된 입력입니다";
                        break;
                }
            }
        }


        //메인 메뉴에서 새로하기를 입력한 경우
        public static void PlayerReStartMenu(string? message)
        {
            Console.Clear();
            Console.WriteLine(TextManager.PlayerReStartMenuTxt());
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = InputNumber();

            switch (playerInput)
            {
                case "0"://취소
                    break;

                case "1"://다시 시작하기
                    if(CheckedPlayerInput(null, "다시 시작하기") == true)
                    {
                        DataManager.PlayerDataReStart();
                    }
                    break;

                default:
                    PlayerReStartMenu("잘못된 입력입니다.");
                    break;
            }
        }

        public static string InputNumber()
        {
            string playerInput = Console.ReadLine();
            AudioManager.PlayMoveMenuSE(); //무언가 입력했을 경우 출력할 오디오
            return playerInput;
        }


        //메인 메뉴에서 휴식으로 입장한 경우
        public static void InputRestMenu(string? message)
        {
            Console.Clear();
            Console.WriteLine(TextManager.RestMenuTxt());
            Console.Write(TextManager.SelectNumberTxt(message));
            string playerInput = InputNumber();

            switch (playerInput)
            {
                case "0":
                    break;

                case "1":
                    if (PlayerNowGold >= 500)
                    {
                        PlayerNowHP = PlayerMaxHP;
                        PlayerNowGold -= 500;
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
}

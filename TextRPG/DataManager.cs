using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using static TextRPG.Player.PlayerAbilityStatus;

namespace TextRPG
{
    //해당 json을 확인하고 null이거나 오류가 발생할 경우 플레이어 데이터를 초기화시킴.
    internal class DataManager
    {
        //내부에 저장되어있는 플레이어 데이터를 외부로 저장하는 메서드
        public static void PlayerDataSave()
        {
            var playerData = new PlayerAbilityStatusData
            {
                Name = PlayerName,
                Job = PlayerJob,
                NowLevel = PlayerNowLevel,
                LevelDefaultRequestValue = PlayerLevelDefaultRequestValue,
                LevelRequestValue = PlayerLevelRequestValue,
                LevelNowValue = PlayerLevelNowValue,
                MaxHP = PlayerMaxHP,
                NowHP = PlayerNowHP,
                NowATK = PlayerNowATK,
                NowDEF = PlayerNowDEF,
                NowGold = PlayerNowGold,
                InventoryItem = PlayerInventoryItem,
                ShopItem = ItemInstanceManager.items
            };

            //C#의 PascalCase를 Json의 camelCase로 자동 변환
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string json = JsonSerializer.Serialize(playerData, options);
            File.WriteAllText("player_data.json", json);
        }


        //저장되어있는 데이터를 로드할 때 사용하는 메서드
        public static void PlayerDataLoad()
        {
            try
            {
                string json = File.ReadAllText("player_data.json");

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var playerData = JsonSerializer.Deserialize<PlayerAbilityStatusData>(json, options);

                if (playerData != null && 
                    playerData.Name != null && 
                    playerData.Job != null &&
                    playerData.Name != "" &&
                    playerData.Job != "")
                {
                    PlayerName = playerData.Name;
                    PlayerJob = playerData.Job;
                    PlayerMaxHP = playerData.MaxHP;
                    PlayerNowHP = playerData.NowHP;
                    PlayerNowATK = playerData.NowATK;
                    PlayerNowDEF = playerData.NowDEF;
                    PlayerNowGold = playerData.NowGold;
                    PlayerNowLevel = playerData.NowLevel;
                    PlayerLevelNowValue = playerData.LevelNowValue;
                    PlayerLevelDefaultRequestValue = playerData.LevelDefaultRequestValue;
                    PlayerLevelRequestValue = playerData.LevelRequestValue;
                    PlayerInventoryItem = playerData.InventoryItem;
                    ItemInstanceManager.items = playerData.ShopItem;

                    PlayerEquippedUseItem();
                }
                else
                {
                    PlayerDataReStart();
                }
            }
            catch
            {
                PlayerDataReStart();
            }

        }


        //내부에 저장되어있는 플레이어 데이터를 삭제할 때 사용하는 메서드
        public static void PlayerDataClear()
        {
            PlayerName = "";     //플레이어 이름
            PlayerJob = "";      //플레이어 직업
            PlayerNowLevel = 0;     //플레이어 현재 레벨
            PlayerLevelDefaultRequestValue = 10;     //플레이어 레벨 업 기본 요구 경험치
            PlayerLevelRequestValue = 10;            //플레이어 레벨 업 현재 요구 경험치
            PlayerLevelNowValue = 0;                //플레이어 현재 경험치
            PlayerMaxHP = 0;        //플레이어 최대 체력
            PlayerNowHP = 0;        //플레이어 현재 체력
            PlayerNowATK = 0;       //플레이어 현재 공격력
            PlayerNowGold = 0;      //플레이어 현재 골드
            PlayerWeaponItem = null;       //플레이어 장착 무기
            PlayerArmorItem = null;       //플레이어 장착 방어구
            PlayerInventoryItem = new List<Item>();      //플레이어 인벤토리 아이템 리스트
        }


        //내부에 저장되어있는 플레이어 데이터를 재정의할 때 사용할 메서드
        public static void PlayerDataReset()
        {
            JobType playerJobs = new JobType(); //플레이어의 직업을 저장할 변수

            PlayerName = MenuManager.InputPlayerNameMenu(null); //플레이어 이름 지정
            playerJobs = (JobType)MenuManager.InputPlayerSelectJobMenu(null); //플레이어 직업 지정
            PlayerJob = playerJobs.ToString();   //직업 이름 플레이어에 대입
            PlayerNowLevel = 1;                  //플레이어 레벨 초기화
            Player.InputPlayerJobAbility(playerJobs.ToString());            //정한 직업의 기본 능력치를 입력
            PlayerInventoryItem = new List<Item>();


            PlayerDataSave();           //선택한 이름, 직업 저장
        }


        //플레이어의 인벤토리 아이템을 검사해서 UseNow가 True인 아이템을 장착 처리 시켜주는 메서드
        public static void PlayerEquippedUseItem()
        {
            foreach(Item item in PlayerInventoryItem)
            {
                if(item.UseNow == true)
                {
                    if (item.ItemType == "무기")
                    {
                        PlayerWeaponItem = item;
                    }
                    else if (item.ItemType == "방어구")
                    {
                        PlayerArmorItem = item;
                    }
                }
            }
        }


        //플레이어의 데이터를 모두 초기화하고 다시 시작할 때 사용할 메서드
        public static void PlayerDataReStart()
        {
            PlayerDataClear();
            PlayerDataReset();
            ItemInstanceManager.InstanceItem(5);
        }
    }

    


    //JSON 변환을 위한 일반 클래스 정의
    public class PlayerAbilityStatusData
    {
        public string Name { get; set; }
        public string Job { get; set; }

        public int NowLevel { get; set; }
        public int LevelDefaultRequestValue { get; set; }
        public int LevelRequestValue { get; set; }
        public int LevelNowValue { get; set; }
        public int MaxHP { get; set; }
        public int NowHP { get; set; }
        public int NowATK { get; set; }
        public int NowDEF { get; set; }
        public int NowGold { get; set; }

        public List<Item> InventoryItem { get; set; }
        public List<Item> ShopItem { get; set; }
    }
}

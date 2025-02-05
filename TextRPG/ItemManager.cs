using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public enum Rarity
    {
        Low,
        Normal,
        High
    }


    //아이템 필수 정보
    public class Item
    {
        public string ItemName { get; init; }               //아이템 이름
        public Rarity Rarity { get; set; }                  //아이템 레어도
        public string ItemType { get; set; }                //아이템 타입
        public bool UseNow { get; set; }                    //아이템의 사용 여부
        public int ItemATK { get; set; }                    //아이템의 공격력
        public int ItemDEF { get; set; }                    //아이템의 방어력
        public int ItemHP { get; set; }                     //아이템의 체력(미구현 항목***)
        public int ItemBuyGold { get; set; }                //아이템의 구매 가격
        public int ItemSellGold { get; set; }               //아이템의 판매 가격
        public string ItemInformationTxt { get; init; }     //아이템의 정보

        private float itemSellMultiple = 0.85f;              //아이템의 판매 가격을 정할 배율


        //Item을 생성하기 위해 선언해야 하는 생성자
        public Item(string itemName, Rarity rarity,string itemType, bool useNow, int itemATK, int itemDEF, int itemHP, int itemBuyGold, string itemInformationTxt)
        {
            this.ItemName = itemName;
            this.Rarity = rarity;
            this.ItemType = itemType;
            this.UseNow = useNow;
            this.ItemATK = itemATK;
            this.ItemDEF = itemDEF;
            this.ItemHP = itemHP;
            this.ItemBuyGold = itemBuyGold;
            this.ItemSellGold = (int)(itemBuyGold * itemSellMultiple);
            this.ItemInformationTxt = itemInformationTxt;
        }


        //장비를 입거나 벗을 경우 실행할 메서드
        public void ItemChangeToUse()
        {
            UseNow = !UseNow;

            //아이템 착용
            if (UseNow == true)
            {
                Player.PlayerAbilityStatus.PlayerNowATK += ItemATK;
                Player.PlayerAbilityStatus.PlayerNowDEF += ItemDEF;
                Player.PlayerAbilityStatus.PlayerNowHP += ItemHP;
            }
            else if (UseNow == false)
            {
                Player.PlayerAbilityStatus.PlayerNowATK -= ItemATK;
                Player.PlayerAbilityStatus.PlayerNowDEF -= ItemDEF;
                Player.PlayerAbilityStatus.PlayerNowHP -= ItemHP;
            }
            DataManager.PlayerDataSave();
        }
    }

}

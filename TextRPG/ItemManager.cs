using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    //아이템 필수 정보
    public class Item
    {
        public string ItemName { get; init; }
        public string ItemType { get; set; }
        public bool UseNow { get; set; }
        public int ItemATK { get; set; }
        public int ItemDEF { get; set; }
        public int ItemHP { get; set; }
        public int ItemBuyGold { get; set; }
        public int ItemSellGold { get; set; }
        public string ItemInformationTxt { get; init; }
        public string ItemEquippedSettingTxt { get; set; }

        private float itemSellMultiple = 0.8f;


        //Item을 생성하기 위해 선언해야 하는 생성자
        public Item(string itemName, string itemType, bool useNow, int itemATK, int itemDEF, int itemHP, int itemBuyGold, string itemInformationTxt)
        {
            this.ItemName = itemName;
            this.ItemType = itemType;
            this.UseNow = useNow;
            this.ItemATK = itemATK;
            this.ItemDEF = itemDEF;
            this.ItemHP = itemHP;
            this.ItemBuyGold = itemBuyGold;
            this.ItemSellGold = (int)(itemBuyGold * itemSellMultiple);
            this.ItemInformationTxt = itemInformationTxt;
            this.ItemEquippedSettingTxt = InputItemEquippedSettingTxt(useNow);
        }


        //현재 useNow 값에 따라 설명 텍스트를 변경하는 메서드
        public string InputItemEquippedSettingTxt(bool useNow)
        {
            string result = "";

            if (useNow)
            {
                result = $" [E]{ItemName} | 공격력 +{ItemATK} | 방어력 +{ItemDEF} | {ItemInformationTxt} | 판매 가치 : {ItemSellGold}";
            }
            else
            {
                result = $" {ItemName} | 공격력 +{ItemATK} | 방어력 +{ItemDEF} | {ItemInformationTxt} | 판매 가치 : {ItemSellGold}";
            }

            return result;
        }


        //장비를 입거나 벗을 경우 실행할 메서드
        public void ItemChangeToUse()
        {
            UseNow = !UseNow;

            //아이템 착용
            if(UseNow == true)
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

            ItemEquippedSettingTxt = InputItemEquippedSettingTxt(UseNow);
            DataManager.PlayerDataSave();
        }
    }
}

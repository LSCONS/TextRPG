using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    //아이템 필수 정보
    internal class Item
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


        //장비를 입거나 벗을 경우 실행할 메서드
        public void ItemChangeToUse()
        {
            useNow = !useNow;

            //아이템 착용
            if(useNow == true)
            {
                Player.PlayerAbilityStatus.playerNowATK += itemATK;
                Player.PlayerAbilityStatus.playerNowDEF += itemDEF;
                Player.PlayerAbilityStatus.playerNowHP += itemHP;
            }
            else if (useNow == false)
            {
                Player.PlayerAbilityStatus.playerNowATK -= itemATK;
                Player.PlayerAbilityStatus.playerNowDEF -= itemDEF;
                Player.PlayerAbilityStatus.playerNowHP -= itemHP;
            }

            itemEquippedSettingTxt = InputItemEquippedSettingTxt(useNow);
        }
    }
}

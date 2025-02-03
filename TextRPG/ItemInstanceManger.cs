using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{

    //아이템 생성 관련 클래스
    internal class ItemInstanceManager
    {
        public static List<Item> items = new List<Item>();
        static Random random = new Random();


        //생성한 아이템 값에 랜덤한 수치를 더해 무작위성을 추가하는 메서드
        public static Item RearrangeValue(Item item, int value)
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
        public static void InstanceItem(int num)
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
        public static void ResetItemsList()
        {
            items.Clear();
        }


        //저장되어 있는 Dictionary 아이템 값을 Item클래스에 맞춰서 반환하는 메서드***(추후 개선 필요)
        public static Item CreateItemFromData(Dictionary<string, (string type, int itemATK, int itemDEF, int itemHP, int itemBuyGold, string information)> itemData)
        {
            List<string> keys = new List<string>(itemData.Keys);
            string selectedKey = keys[random.Next(keys.Count)];
            var data = itemData[selectedKey];
            return new Item(selectedKey, data.type, false, data.itemATK, data.itemDEF, data.itemHP, data.itemBuyGold, data.information);
        }


        //아이템들을 저장하고 있는 변수들.
        public static Dictionary<string, (string type, int itemATK, int itemDEF, int itemHP, int itemBuyGold, string information)> lowItemData =
    new Dictionary<string, (string type, int itemATK, int itemDEF, int itemHP, int itemBuyGold, string information)>
            {
                {"나무 검", ("무기",3, 0, 0, 100, "없는 것 보다 나은 검이다.") },
                {"나무 막대기", ("무기", 2, 0, 0, 30, "없으나 마나 한 막대기다.") },
                {"낡은 조끼",("방어구", 0, 2, 0, 30, "없으나 마나 한 옷이다") },
                {"조끼", ("방어구", 0, 3, 0, 30, "없는 것 보다 나은 옷이다.") }
            };
        public static Dictionary<string, (string type, int itemATK, int itemDEF, int itemHP, int itemBuyGold, string information)> nomalItemData =
    new Dictionary<string, (string type, int itemATK, int itemDEF, int itemHP, int itemBuyGold, string information)>
    {
                {"철 검", ("무기",8, 0, 0, 300, "좋은 검이다.") },
                {"철 방망이", ("무기", 5, 0, 0, 200, "쓸만한 방망이다.") },
                {"철 갑옷",("방어구", 0, 8, 0, 300, "좋은 방어구다") },
                {"가죽 갑옷", ("방어구", 0, 5, 0, 200, "쓸만한 방어구다.") }
    };
        public static Dictionary<string, (string type, int itemATK, int itemDEF, int itemHP, int itemBuyGold, string information)> highItemData =
    new Dictionary<string, (string type, int itemATK, int itemDEF, int itemHP, int itemBuyGold, string information)>
    {
                {"전설의 검", ("무기",15, 0, 0, 1000, "전설의 검이다.") },
                {"강철 검", ("무기", 12, 0, 0, 500, "철 검보다 단단한 검이다.") },
                {"전설의 갑옷",("방어구", 0, 15, 0, 1000, "전설의 갑옷이다") },
                {"강철 갑옷", ("방어구", 0, 12, 0, 500, "철 갑옷보다 단단한 갑옷이다.") }
    };

    }
}

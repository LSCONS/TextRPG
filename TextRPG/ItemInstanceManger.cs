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
        public static List<Item> items = new List<Item>();  //상점에 출력할 아이템들
        static Random random = new Random();


        //생성한 아이템 값에 랜덤한 수치를 더해 무작위성을 추가하는 메서드
        public static Item RearrangeValue(Item item, int value)
        {
            Random random = new Random();
            int rand = random.Next(-value, value + 1);

            if (item.ItemType == "무기")
            { item.ItemATK += rand; }
            else if (item.ItemType == "방어구")
            { item.ItemDEF += rand; }

            return item;
        }


        //아이템을 랜덤으로 생성하는 메소드
        public static void InstanceItem(int num)
        {
            items = new List<Item>();

            for (int i = 0; i <num; i++)
            {
                Rarity rarity = RandomRarity();
                items.Add(GetRandomItem(rarity));
            }

            //아이템 이름대로 정렬 후 저장
            items.Sort((x, y) => x.ItemName.CompareTo(y.ItemName));
            DataManager.PlayerDataSave(); //저장
        }


        //레어도에 따라 랜덤으로 뽑은 Rarity를 반환하는 메서드
        static Rarity RandomRarity()
        {
            int sumCount = rarityAllCount.Values.Sum();
            int rand = random.Next(0, sumCount);
            int i = 0;

            //i의 값에 등장 확률을 차례대로 더한다.
            //해당 과정 중 rand값이 해당 i의 값보다 작다면 해당 레어도를 반환.
            foreach(var rarity in rarityAllCount)
            {
                i += rarity.Value;
                if (rand < i) return rarity.Key;
            }
            return Rarity.Low;
        }

        
        //입력 받은 Rarity에 따라 특정 아이템을 뽑아 반환하는 메서드
        static Item GetRandomItem(Rarity rarity)
        {
            var itemList = allItem.Where(x => x.Rarity == rarity).ToList();
            
            int i = random.Next(itemList.Count);

            return itemList[i];
        }


        //모든 등급의 등장 수치를 정할 Dictionary
        static Dictionary<Rarity, int> rarityAllCount = new Dictionary<Rarity, int> 
        {
            { Rarity.Low, 6 },
            { Rarity.Normal, 3 },
            { Rarity.High, 1 },
        };


        //아이템들을 저장하고 있는 변수들.
        public static List<Item> allItem = new List<Item>
            {
    // ===== 하급 (lowItemData) =====
    new Item("나무 검",     Rarity.Low, "무기",   false, 3,  0, 0, 100, "없는 것 보다 나은 검이다."),
    new Item("나무 막대기", Rarity.Low, "무기",   false, 2,  0, 0, 30,  "없으나 마나 한 막대기다."),
    new Item("낡은 조끼",   Rarity.Low, "방어구", false, 0,  2, 0, 30,  "없으나 마나 한 옷이다"),
    new Item("조끼",        Rarity.Low, "방어구", false, 0,  3, 0, 30,  "없는 것 보다 나은 옷이다."),

    // ===== 중급 (nomalItemData) =====
    new Item("철 검",       Rarity.Normal, "무기",   false, 8,  0, 0, 300, "좋은 검이다."),
    new Item("철 방망이",   Rarity.Normal, "무기",   false, 5,  0, 0, 200, "쓸만한 방망이다."),
    new Item("철 갑옷",     Rarity.Normal, "방어구", false, 0,  8, 0, 300, "좋은 방어구다"),
    new Item("가죽 갑옷",   Rarity.Normal, "방어구", false, 0,  5, 0, 200, "쓸만한 방어구다."),

    // ===== 상급 (highItemData) =====
    new Item("전설의 검",   Rarity.High, "무기",   false, 15, 0, 0, 1000, "전설의 검이다."),
    new Item("강철 검",     Rarity.High, "무기",   false, 12, 0, 0, 500,  "철 검보다 단단한 검이다."),
    new Item("전설의 갑옷", Rarity.High, "방어구", false, 0,  15,0, 1000, "전설의 갑옷이다"),
    new Item("강철 갑옷",   Rarity.High, "방어구", false, 0,  12,0, 500,  "철 갑옷보다 단단한 갑옷이다.")
            };

    }
}

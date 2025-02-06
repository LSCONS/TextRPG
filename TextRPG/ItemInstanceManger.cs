using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    //아이템 생성 관련 클래스
    public class ItemInstanceManager
    {
        public static List<Item> items = new List<Item>();  //상점에 출력할 아이템들
        static Random random = new Random();


        //모든 등급의 등장 수치를 정할 Dictionary
        static Dictionary<Rarity, int> rarityAllCount = new Dictionary<Rarity, int>
        {
            { Rarity.Low, 6 },
            { Rarity.Normal, 3 },
            { Rarity.High, 1 },
        };


        //생성한 아이템 값에 랜덤한 수치를 더해 무작위성을 추가하는 메서드
        public static Item RearrangeValue(Item item)
        {
            Random random = new Random();
            int range = 0;
            int ATK = 0;
            int DEF = 0;

            switch (item.Rarity)
            {
                case Rarity.Low:
                    range = 1; 
                    break;

                case Rarity.Normal:
                    range = 3;
                    break;

                case Rarity.High:
                    range = 8;
                    break;
            }

            int rand = random.Next(-range, range +1);
            ATK = item.ItemATK;
            DEF = item.ItemDEF;


            if (item.ItemType == "무기")        { ATK += rand; }
            else if (item.ItemType == "방어구") { DEF += rand; }

            return new Item(item.ItemName, item.Rarity, item.ItemType, item.UseNow, ATK, DEF, item.ItemHP, item.ItemBuyGold, item.ItemInformationTxt, false);
        }


        //상점 아이템 리스트를 초기화시키고 랜덤으로 (플레이어 레벨 + 2)만큼 생성하는 메소드
        public static void InstanceItem()
        {
            items = new List<Item>();

            for (int i = 0; i < Player.PlayerAbilityStatus.PlayerNowLevel + 2; i++)
            {
                Rarity rarity = RandomRarity();
                Item item = GetRandomItem(rarity);
                item = RearrangeValue(item);        //무작위 수치 부여
                items.Add(item);
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
            foreach (var rarity in rarityAllCount)
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


        //아이템들을 저장하고 있는 변수들.
        public static List<Item> allItem = new List<Item>
        {
            // ===== 하급 (lowItemData) =====
            new Item("나무 검",     Rarity.Low, "무기",   false, 3,  0, 0, 100, "없는 것 보다 나은 검이다.", false),
            new Item("나무 막대기", Rarity.Low, "무기",   false, 2,  0, 0, 30,  "없으나 마나 한 막대기다.", false),
            new Item("낡은 조끼",   Rarity.Low, "방어구", false, 0,  2, 0, 30,  "없으나 마나 한 옷이다", false),
            new Item("조끼",        Rarity.Low, "방어구", false, 0,  3, 0, 30,  "없는 것 보다 나은 옷이다.", false),
            new Item("돌도끼",      Rarity.Low, "무기",   false, 4,  0, 0, 120, "단단한 돌로 만든 도끼다.", false),
            new Item("짧은 창",     Rarity.Low, "무기",   false, 5,  0, 0, 150, "손쉬운 창이다.", false),
            new Item("녹슨 검",     Rarity.Low, "무기",   false, 3,  0, 0, 90,  "오래된 검이다.", false),
            new Item("작은 방패",   Rarity.Low, "방어구", false, 0,  2, 0, 50,  "가벼운 방패다.", false),
            new Item("가죽 장갑",   Rarity.Low, "방어구", false, 0,  1, 0, 20,  "손을 보호하는 장갑이다.", false),
            new Item("무릎 보호대", Rarity.Low, "방어구", false, 0,  2, 0, 40,  "무릎을 보호한다.", false),
            new Item("천 갑옷",     Rarity.Low, "방어구", false, 0,  4, 0, 80,  "얇은 천으로 만든 갑옷이다.", false),
            new Item("낡은 신발",   Rarity.Low, "방어구", false, 0,  1, 0, 30,  "오래된 신발이다.", false),
            new Item("철제 너클",   Rarity.Low, "무기",   false, 6,  0, 0, 160, "주먹을 강화하는 무기다.", false),
            new Item("부러진 창",   Rarity.Low, "무기",   false, 3,  0, 0, 70,  "부러져서 짧아진 창이다.", false),

            // ===== 중급 (nomalItemData) =====
            new Item("철 검",       Rarity.Normal, "무기",   false, 8,  0, 0, 300, "좋은 검이다.", false),
            new Item("철 방망이",   Rarity.Normal, "무기",   false, 5,  0, 0, 200, "쓸만한 방망이다.", false),
            new Item("철 갑옷",     Rarity.Normal, "방어구", false, 0,  8, 0, 300, "좋은 방어구다", false),
            new Item("가죽 갑옷",   Rarity.Normal, "방어구", false, 0,  5, 0, 200, "쓸만한 방어구다.", false),
            new Item("은 도끼",     Rarity.Normal, "무기",   false, 10, 0, 0, 350, "무게감 있는 도끼다.", false),
            new Item("긴 창",       Rarity.Normal, "무기",   false, 9,  0, 0, 320, "긴 창이라서 강하다.", false),
            new Item("강철 망치",   Rarity.Normal, "무기",   false, 7,  0, 0, 270, "무거운 강철 망치다.", false),
            new Item("큰 방패",     Rarity.Normal, "방어구", false, 0,  6, 0, 250, "튼튼한 방패다.", false),
            new Item("강철 신발",   Rarity.Normal, "방어구", false, 0,  5, 0, 220, "단단한 신발이다.", false),
            new Item("사슬 갑옷",   Rarity.Normal, "방어구", false, 0,  9, 0, 350, "사슬로 만든 갑옷이다.", false),

            // ===== 상급 (highItemData) =====
            new Item("명검 엑스칼",  Rarity.High, "무기",   false, 15, 0, 0, 1000, "전설 속의 명검이다.", false),
            new Item("강화된 대검",  Rarity.High, "무기",   false, 12, 0, 0, 500,  "단단한 대검이다.", false),
            new Item("신성한 갑옷",  Rarity.High, "방어구", false, 0,  15, 0, 1000, "성스러운 기운이 감돈다.", false),
            new Item("강화된 갑옷",  Rarity.High, "방어구", false, 0,  12, 0, 500,  "매우 튼튼한 갑옷이다.", false),
            new Item("폭룡의 검",    Rarity.High, "무기",   false, 18, 0, 0, 1200, "폭풍 같은 용의 힘을 담았다.", false),
            new Item("미스릴 갑옷",  Rarity.High, "방어구", false, 0,  18, 0, 1200, "희귀한 미스릴로 제작됨.", false)
        };
    }
}

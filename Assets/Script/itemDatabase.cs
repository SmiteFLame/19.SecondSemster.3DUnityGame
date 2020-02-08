using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();
   // public Sprite[] sp = Resources.LoadAll<Sprite>("ItemIcons/" + "34x34icons180709");
    //public Sprite[] sprites;

    void Start()
    {
      //  sprites = Resources.LoadAll<Sprite>("ItemIcons/34x34icons180709");

        // items.Add(new Item(이름, 아이템아이디, 설명, 공격력, 공속, 방어력, 회피력, 아이템 속성여부));
        items.Add(new Item("5_0", "Iron Sword", 1001, "This sword is normal style sword", 10, 1, 0, 0, Item.ItemType.Weapon));
        // 위와 같이 원하는 아이템을 모두 추가해줍니다.(수업 중 예시니까 조금만 만들거임 ' ㅅ'
        items.Add(new Item("8_0", "Iron Spear", 1011, "This spear is normal style spear", 12, 2, 0, 0, Item.ItemType.Weapon));

        items.Add(new Item("7_1", "Boxing Gloves", 2001, "This Gloves is fast gloves", 10, 1, 0, 1, Item.ItemType.Weapon));
        items.Add(new Item("7_2", "Drill Gloves", 2002, "This Gloves is Drill gloves", 13, 2, 0, 1, Item.ItemType.Weapon));

        items.Add(new Item("2_7", "Red Potion", 4001, "This potion is restores hp(+50)", 0, 0, 0, 0, Item.ItemType.Use));
        items.Add(new Item("2_8", "Orange Potion", 4011, "This potion is increase sight (+5)", 0, 0, 0, 0, Item.ItemType.Use));
        // 원하는 만큼 만들어주면 됨(물론 나중에 디비가 생긴다면 이거 안해도 되는건데, 우리는 디비 없이 할거니까 해야됨
        // 아무튼 해야됨 ㅇㅇㅇㅇㅋ
    }

}

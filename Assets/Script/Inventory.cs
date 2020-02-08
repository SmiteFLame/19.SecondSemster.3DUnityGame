using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    // 인벤토리를 리스트로 만듭니다.
    private itemDatabase db;
    // 아이템 데이터베이스는 db로 축약해서 사용합니다.

    public int slotX, slotY;    // 인벤토리 가로 세로 속성 설정 위한 변수
    public List<Item> slots = new List<Item>(); // 인벤토리 속성 변수

    private bool showInventory = false;
    // I버튼을 누르면 활성화/비활성화 되는 부울 변수
    public GUISkin skin;

    private bool showTooltip;
    private string tooltip;
    // 툴팁 추가를 위한 부울 변수와 스트링 변수

    private bool dragItem;
    // 아이템을 드래그 한 것인지에 대한 여부 부울 변수
    private Item draggedItem;
    // 드래그하고 있는 아이템을 담을 임시 그릇
    private int prevIndex;
    // 선택했던 아이템의 전 위치를 저장하기 위한 변수

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < slotX * slotY; i++)
        {
            slots.Add(new Item());
            // 아이템 슬롯칸에 빈 오브젝트 추가하기
            inventory.Add(new Item());
            // 인벤토리에 추가
        }
        db = GameObject.FindGameObjectWithTag("Item Database").GetComponent<itemDatabase>();
        if (db == null)
        {
            Debug.Log("db가져오기실패");
        }
        // 디비 변수에 "Item Database" 태그를 가진 오브젝트를 연결합니다.
        // 그리고 그 중 가져오는 컴포넌트는 "itemDatabse"라는 속성입니다.

        AddItem(1001);
        // 아이템ID를 호출하도록 한다.
        AddItem(1011);
        AddItem(2001);
        // 테스트용 명령어
        AddItem(4001);
        // 물약 추가하기
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        // 만약 Inventory(I)버튼이 눌리면 아래 내용을 실행합니다.
        {
            showInventory = !showInventory;
            // showInventory 앞에 느낌표는 낫(Not)연산자이며, 참>거짓, 거짓>참으로 바꿔주는 연산자입니다.
            // 누를때마다 참>거짓>참>거짓으로 바뀌겠죠
        }
    }
    void OnGUI()
    {
        tooltip = "";
        GUI.skin = skin;
        // Skin 을 skin 답게 ' ㅅ'
        if (showInventory)
        {
            DrawInventory();
            if (showTooltip)
            {
                GUI.Box(new Rect(Event.current.mousePosition.x + 5, Event.current.mousePosition.y + 2, 200, 200), tooltip, skin.GetStyle("tooltip"));
                // 아이템 설명창을 마우스의 좌표에 컨트롤 되게 설정하였으며, GUI skin을 응용하여 설정하였음
            }
        }
        if (dragItem)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x - 5, Event.current.mousePosition.y - 5, 50, 50), draggedItem.itemIcon);
        }

    }

    void DrawInventory()
    {
        int k = 0;
        Event e = Event.current;
        // 마우스 상태 공동 사용을 위한 변수로 지정
        for (int j = 0; j < slotY; j++)
        {

            for (int i = 0; i < slotX; i++)
            {
                Rect slotRect = new Rect(i * 52 + 100, j * 52 + 30, 50, 50);
                // 박스 분할하기
                GUI.Box(slotRect, "", skin.GetStyle("slot background"));
                // 각 박스의 생성 위치를 설정해주는 곳입니다. skin.GetStyle은 이전에 만들었던 skin을 불러오는 것임

                // 기능 추가하기
                slots[k] = inventory[k];

                if (slots[k].itemName != null)
                {
                    GUI.DrawTexture(slotRect, slots[k].itemIcon);
                    if (slotRect.Contains(e.mousePosition))
                    // 만약 마우스가 해당 인벤토리 창-버튼-위로 올라온다면,
                    {
                        tooltip = CreateTooltip(slots[k]);
                        // 툴팁 만드는 함수를 실행하며,
                        // 보내는 속성은 i번째 슬롯입니다.
                        showTooltip = true;
                        // 툴팁을 만들고, showTooltip을 true로 만들어서 활성화 시켜줍니다.
                        if (e.button == 0 && e.type == EventType.MouseDrag && !dragItem)
                        // 마우스 상태가 0이면서 동시에 '마우스 드래그' 상태인 조건
                        {
                            dragItem = true;
                            // 드래그 아이템을 참으로
                            prevIndex = k;
                            // 선택했던 위치를 저장함
                            draggedItem = slots[k];
                            // 아이템변수에 현재 슬롯 아이템을 저장함
                            inventory[k] = new Item();
                        }
                        if (e.type == EventType.MouseUp && dragItem)
                        // 마우스 업 하고 드래그 하고 있는 아이템이 존재한다면,
                        {
                            inventory[prevIndex] = inventory[k];
                            // 아이템의 전 위치에 현재 아이템을 놓고
                            inventory[k] = draggedItem;
                            // 현재 아이템의 위치에 드래그하고 있는 아이템을 놓고
                            dragItem = false;
                            // 드래그 옵션은 false로 종료하고
                            draggedItem = null;
                            // 드래그 중인 아이템은 없는걸로 하고
                        }
                        if (e.isMouse && e.type == EventType.MouseDown && e.button == 1)
                        // 마우스의 상태가 클릭이면서 동시에 해당 클릭 버튼이 오른쪽 버튼이라면,
                        {
                            if (inventory[k].itemType == Item.ItemType.Use)
                            // 만약 클릭한 아이템의 속성이 use속성이라면, 
                            {
                                switch (inventory[k].itemID)
                                // 인벤토리의 아이템ID가 각각의 상태라면,
                                {
                                    case 4001:
                                        // 4001은 힐링포션
                                        Debug.Log("heal +50");
                                        break;
                                    case 4011:
                                        // 4011은 시야 포션
                                        Debug.Log("increase sight + 1");
                                        break;
                                    default:
                                        // 그 밖의 use 아이템은 아직 활성화가 안됨
                                        Debug.Log("I don't know, what is use this?");
                                        break;
                                }
                                inventory[k] = new Item();
                            }
                        }
                    }
                }
                else
                {
                    if (slotRect.Contains(e.mousePosition))
                    {
                        if (e.type == EventType.MouseUp && dragItem)
                        {
                            // 위와 같지만, 빈 공간으로의 아이템 옮기기 기능입니다 ' ㅅ' //
                            inventory[k] = draggedItem;
                            dragItem = false;
                            draggedItem = null;
                        }
                    }
                }

                if (tooltip == "")
                {
                    showTooltip = false;
                }


                k++;
                // 갯수 증가
            }
        }
    }

    string CreateTooltip(Item item)
    {
        tooltip = "Item name: <color=#a10000><b>" + item.itemName + "</b></color>\nItem Damage: <color=#ffffff>" + item.itemPower + "</color>\nItem Speed: <color=#ffffff>" + item.itemSpeed + "</color>";
        /* html 태그가 어느정도 먹힘
         * <color=#000000> 말 </color>
         * <b> 두껍게 </b>
         * ... emdemdemd
         */

        return tooltip;
    }

    void AddItem(int id)
    // 본 함수에서 id를 받아서
    {
        for (int i = 0; i < inventory.Count; i++)
        // 전체 인벤토리를 모두 찾습니다
        {
            Debug.Log("인벤토리찾기");
            if (inventory[i].itemName == null)
            // 인벤토리가 빈자리면 
            {

                for (int j = 0; j < db.items.Count; j++)
                // 추가한 값까지 모두 찾은 다음에
                {
                    if (db.items[j].itemID == id)
                    {
                        // 디비의 아이템의 ID와 입력한 ID가 같다면,
                        inventory[i] = db.items[j];
                        Debug.Log(db.items.Count);
                        // 빈 인벤토리에 db에 저장된 아이템을 적용하고
                        return;
                        // 함수를 마무리합니다.
                    }
                }
            }
        }
    }

    bool inventoryContains(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            return (inventory[i].itemID == id);
        }
        return false;
    }

    void RemoveItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemID == id)
            {
                inventory[i] = new Item();
                break;
            }
        }
    }
}

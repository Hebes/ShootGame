using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;
enum EUI_Inventory
{
    ItemSlotContainer,
    ItemSlotTemplate,
}

/// <summary>
/// 背包系统
/// </summary>
public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    private Player_GetItem player_GetItem;

    public void SetPlayer(Player_GetItem player_GetItem) => this.player_GetItem = player_GetItem;

    private void Awake()
    {
        itemSlotContainer = transform.Find(EUI_Inventory.ItemSlotContainer.ToString());
        itemSlotTemplate = itemSlotContainer.Find(EUI_Inventory.ItemSlotTemplate.ToString());
    }

    /// <summary>
    /// 设置库存
    /// </summary>
    /// <param name="inventory"></param>
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    /// <summary>
    /// 刷新库存物品
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Inventory_OnItemListChanged(object sender, EventArgs e)
    {
        RefreshInventoryItems();
    }
    /// <summary>
    /// 刷新库存物品
    /// </summary>
    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate)
                continue;
            Destroy(child.gameObject);
        }

        //int x = 0;
        //int y = 0;
        //float itemSlotCellSize = 100f;

        foreach (Item item in inventory.GetItemList)
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                //Use item
                inventory.UserItem(item);
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                //Drp item
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                inventory.RemoveItem(item);
                ItemWorld.DropItem(player_GetItem.GetPosition(), duplicateItem);
                
            };


            //itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI text = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
            text.text = item.amount > 1 ? item.amount.ToString() : string.Empty;

            //x++;
            //if (x >= 4)
            //{
            //    x = 0;
            //    y--;
            //}

        }
    }
}

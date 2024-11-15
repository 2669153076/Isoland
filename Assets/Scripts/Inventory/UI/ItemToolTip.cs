using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemToolTip : MonoBehaviour
{
    public Text itemNameText;

    public void UpdateItemName(E_ItemName itemName)
    {
        itemNameText.text = itemName switch
        {
            E_ItemName.Key => "信箱钥匙",
            E_ItemName.Ticket => "一张船票",
            _=>""

        };
    }
}

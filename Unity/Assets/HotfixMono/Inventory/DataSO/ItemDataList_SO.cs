using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [CreateAssetMenu(fileName = "ItemDataList_SO",menuName = "Inventory/ItemDataList")]
    public class ItemDataList_SO : ScriptableObject
    {
        public List<ItemDetails> ItemDetailsList;
    }
}
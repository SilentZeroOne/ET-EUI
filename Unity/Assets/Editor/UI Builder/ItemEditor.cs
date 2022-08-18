using System;
using System.Collections.Generic;
using System.Linq;
using ET;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Object = UnityEngine.Object;

public class ItemEditor : EditorWindow
{
    private ItemDataList_SO _dataBase;
    private List<ItemDetails> _itemList;
    private ListView _itemListView;
    private VisualTreeAsset _itemRowTemplete;
    private VisualElement _itemDetailView;
    private Sprite _defaultIcon;
    private ItemDetails _activeItem;

    [MenuItem("Farm/ItemEditor")]
    public static void ShowExample()
    {
        ItemEditor wnd = GetWindow<ItemEditor>();
        wnd.titleContent = new GUIContent("ItemEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        //VisualElements objects can contain other VisualElement following a tree hierarchy.
        // VisualElement label = new Label("Hello World! From C#");
        // root.Add(label);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI Builder/ItemEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);
        
        //加载数据
        LoadDataBase();

        _defaultIcon = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Res/M Studio/Art/Items/Icons/icon_Game.png");

        root.Q<Button>("AddButton").clicked += this.OnAddButtonClick;
        root.Q<Button>("DeleteButton").clicked += this.OnDeleteButtonClick;
        
        // The "makeItem" function will be called as needed
        // when the ListView needs more items to render
        _itemRowTemplete = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI Builder/ItemRowTemplate.uxml");
        _itemDetailView = this.rootVisualElement.Q<VisualElement>("ItemDetails");

        GenerateListView();

        this._itemDetailView.visible = false;
    }

    private void LoadDataBase()
    {
        var dataArray = AssetDatabase.FindAssets("ItemDataList_SO");
        if (dataArray.Length > 1)
        {
            var path = AssetDatabase.GUIDToAssetPath(dataArray[0]);
            this._dataBase = AssetDatabase.LoadAssetAtPath<ItemDataList_SO>(path);
            _itemList = this._dataBase.ItemDetailsList;
            
            EditorUtility.SetDirty(this._dataBase);
        }
    }

    private void GenerateListView()
    {
        Func<VisualElement> makeItem = () => _itemRowTemplete.CloneTree();

        // As the user scrolls through the list, the ListView object
        // will recycle elements created by the "makeItem"
        // and invoke the "bindItem" callback to associate
        // the element with the matching data item (specified as an index in the list)
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            if (i < this._itemList.Count)
            {
                e.Q<VisualElement>("Icon").style.backgroundImage = _itemList[i].ItemIcon == null?
                        this._defaultIcon.texture : _itemList[i].ItemIcon.texture;
                e.Q<Label>("Name").text = string.IsNullOrEmpty(this._itemList[i].ItemName)? "No Item" : this._itemList[i].ItemName;
            }
        };

        _itemListView = rootVisualElement.Q<ListView>();
        _itemListView.fixedItemHeight = 60;
        _itemListView.makeItem = makeItem;
        _itemListView.bindItem = bindItem;
        _itemListView.itemsSource = this._itemList;
        _itemListView.selectionType = SelectionType.Multiple;
        
        this._itemListView.onSelectionChange += this.OnItemSelect;
    }

    private void OnItemSelect(IEnumerable<object> items)
    {
        _activeItem = items.First() as ItemDetails;
        ShowItemDetails();
        this._itemDetailView.visible = true;
    }

    private void ShowItemDetails()
    {
        _itemDetailView.MarkDirtyRepaint();

        var row1Container = _itemDetailView.Q<VisualElement>("Row1").Q<VisualElement>("Container");
        var row2Container = _itemDetailView.Q<VisualElement>("Row2").Q<VisualElement>("Container");

        row1Container.Q<VisualElement>("Icon").style.backgroundImage = _activeItem.ItemIcon.texture;
        row1Container.Q<ObjectField>("ItemIcon").value = _activeItem.ItemIcon;
        row1Container.Q<ObjectField>("ItemIcon").RegisterValueChangedCallback(evt =>
        {
            _activeItem.ItemIcon = evt.newValue as Sprite;
            row1Container.Q<VisualElement>("Icon").style.backgroundImage =
                    _activeItem.ItemIcon == null? this._defaultIcon.texture : _activeItem.ItemIcon.texture;
            this._itemListView.Rebuild();
        });

        row1Container.Q<ObjectField>("ItemOnWorldSprite").value = _activeItem.ItemOnWorldSprite;
        row1Container.Q<ObjectField>("ItemOnWorldSprite").RegisterValueChangedCallback(evt =>
        {
            _activeItem.ItemOnWorldSprite = evt.newValue as Sprite;
        });

        row1Container.Q<IntegerField>("ItemId").value = _activeItem.ItemId;
        row1Container.Q<IntegerField>("ItemId").RegisterValueChangedCallback(evt =>
        {
            _activeItem.ItemId = evt.newValue;
        });
        
        row1Container.Q<TextField>("ItemName").value = _activeItem.ItemName;
        row1Container.Q<TextField>("ItemName").RegisterValueChangedCallback(evt =>
        {
            _activeItem.ItemName = evt.newValue;
            this._itemListView.Rebuild();
        });
        
        var itemTypeEnum = row1Container.Q<EnumField>("ItemType");
        itemTypeEnum.Init(MonoItemType.Seed);
        itemTypeEnum.value = _activeItem.ItemType;
        itemTypeEnum.RegisterValueChangedCallback(evt =>
        {
            _activeItem.ItemType = evt.newValue is MonoItemType type? type : MonoItemType.Seed;
        });

        row2Container.Q<TextField>("ItemDescription").value = _activeItem.ItemDescription;
        row2Container.Q<TextField>("ItemDescription").RegisterValueChangedCallback(evt =>
        {
            _activeItem.ItemDescription = evt.newValue;
        });

        var row3 = _itemDetailView.Q<VisualElement>("Row3");
        var left = row3.Q<VisualElement>("Left");
        var right = row3.Q<VisualElement>("Right");
        left.Q<IntegerField>("ItemUseRadius").value = _activeItem.ItemUseRadius;
        left.Q<IntegerField>("ItemUseRadius").RegisterValueChangedCallback(evt =>
        {
            _activeItem.ItemUseRadius = evt.newValue;
        });
        
        left.Q<Toggle>("CanPickedUp").value = _activeItem.CanPickedUp;
        left.Q<Toggle>("CanPickedUp").RegisterValueChangedCallback(evt =>
        {
            _activeItem.CanPickedUp = evt.newValue;
        });
        
        left.Q<Toggle>("CanCarried").value = _activeItem.CanCarried;
        left.Q<Toggle>("CanCarried").RegisterValueChangedCallback(evt =>
        {
            _activeItem.CanCarried = evt.newValue;
        });
        
        left.Q<Toggle>("CanDropped").value = _activeItem.CanDropped;
        left.Q<Toggle>("CanDropped").RegisterValueChangedCallback(evt =>
        {
            _activeItem.CanDropped = evt.newValue;
        });

        right.Q<IntegerField>("ItemPrice").value = _activeItem.ItemPrice;
        right.Q<IntegerField>("ItemPrice").RegisterValueChangedCallback(evt =>
        {
            _activeItem.ItemPrice = evt.newValue;
        });
        
        right.Q<Slider>("SellPercentage").value = _activeItem.SellPercentage;
        right.Q<Slider>("SellPercentage").RegisterValueChangedCallback(evt =>
        {
            _activeItem.SellPercentage = evt.newValue;
        });
    }

    private void OnAddButtonClick()
    {
        ItemDetails newItem = new();
        newItem.ItemId = 1001 + this._itemList.Count;
        newItem.ItemName = "New Item";
        newItem.ItemIcon = this._defaultIcon;
        this._itemList.Add(newItem);
        this._itemListView.Rebuild();
    }

    private void OnDeleteButtonClick()
    {
        _itemList.Remove(this._activeItem);
        _itemListView.Rebuild();
        _itemDetailView.visible = false;
    }
}
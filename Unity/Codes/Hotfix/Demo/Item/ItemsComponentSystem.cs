using System.IO;

namespace ET
{
    public class ItemsComponentAwakeSystem: AwakeSystem<ItemsComponent>
    {
        public override void Awake(ItemsComponent self)
        {
            self.CurrentSceneName = self.GetParent<Scene>().Name;
        }
    }

    public class ItemsComponentDestroySystem: DestroySystem<ItemsComponent>
    {
        public override void Destroy(ItemsComponent self)
        {
            self.SaveItemsComponent();
        }
    }

    [FriendClass(typeof(ItemsComponent))]
    [FriendClassAttribute(typeof(ET.Item))]
    public static class ItemsComponentSystem
    {
        public static void AddItem(this ItemsComponent self, Item item)
        {
            if (!self.ItemList.Contains(item))
            {
                self.ItemList.Add(item);
                if (item.Parent != self)
                {
                    self.AddChild(item);
                }
                    
            }
        }

        public static Item AddItem(this ItemsComponent self, int itemId)
        {
            Item item = self.AddChild<Item, int>(itemId);
            self.ItemList.Add(item);
            return item;
        }

        public static void RemoveItem(this ItemsComponent self, Item item)
        {
            if (self.ItemList.Contains(item))
            {
                self.ItemList.Remove(item);
            }
            
            self.SaveItemsComponent();
        }

        #region Save/Load

        public static ItemListProto ToProto(this ItemsComponent self)
        {
            ItemListProto proto = new ItemListProto();
            foreach (var item in self.ItemList)
            {
                ItemInfo info = item.ToProto();
                info.WorldPosition = new ProtoVector3(item.Position);
                proto.ItemInfos.Add(info);
            }

            return proto;
        }

        public static void FromProto(this ItemsComponent self, ItemListProto proto)
        {
            foreach (var item in proto.ItemInfos)
            {
                Item temp = self.AddChildWithId<Item>(item.ItemId);
                self.AddItem(temp.FromProto(item));
                ItemFactory.Create(temp);
            }
        }

        public static void SaveItemsComponent(this ItemsComponent self, string path = null)
        {
            //path = self.SavePath;
            if (string.IsNullOrEmpty(path))
            {
#if NOT_UNITY
                path = "";
#else
                path = Path.Combine(PathHelper.SavingPath, self.CurrentSceneName + "_ItemsSave.sav");
#endif
            }

            Log.Debug($"Save ItemsComponent in {path}");
            ProtobufHelper.SaveTo(self.ToProto(), path);
        }

        public static async ETTask<bool> LoadItemsComponent(this ItemsComponent self)
        {
#if NOT_UNITY
            await ETTask.CompletedTask;
            return false;
#else
            string path = Path.Combine(PathHelper.SavingPath, self.CurrentSceneName + "_ItemsSave.sav");

            byte[] bytes = await FileReadHelper.DownloadData(path);
            ItemListProto proto = ProtobufHelper.Deserialize<ItemListProto>(bytes);
            if (proto != null)
            {
                self.FromProto(proto);
                return true;
            }

            return false;
#endif
        }

        #endregion
    }
}
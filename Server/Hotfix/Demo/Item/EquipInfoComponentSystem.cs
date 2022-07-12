namespace ET
{

    public class EquipInfoComponentAwakeSystem: AwakeSystem<EquipInfoComponent>
    {
        public override void Awake(EquipInfoComponent self)
        {
            self.GenerateEntries();
        }
    }
    
    public class EquipInfoComponentDestroySystem : DestroySystem<EquipInfoComponent>
    {
        public override void Destroy(EquipInfoComponent self)
        {
            self.isInited = false;
            self.Score = 0;
            foreach (var entry in self.EntryList)
            {
                entry?.Dispose();
            }
            self.EntryList.Clear();
        }
    }
    
    public class EquipInfoComponentDeserializeSystem : DeserializeSystem<EquipInfoComponent>
    {
        public override void Deserialize(EquipInfoComponent self)
        {
            foreach (var entity in self.Children.Values)
            {
                self.EntryList.Add(entity as AttributeEntry);
            }
        }
    }

    [FriendClass(typeof(AttributeEntry))]
    [FriendClass(typeof(EquipInfoComponent))]
    [FriendClass(typeof(Item))]
    public static class EquipInfoComponentSystem
    {
        public static void GenerateEntries(this EquipInfoComponent self)
        {
            if (self.isInited)
            {
                return;
            }

            self.isInited = true;
            self.CreateEntry();
        }

        public static void CreateEntry(this EquipInfoComponent self)
        {
            Item item = self.GetParent<Item>();
            ItemConfig itemConfig = item.Config;
            EntryRandomConfig entryRandomConfig = EntryRandomConfigCategory.Instance.Get(itemConfig.EntryRandomId);

            //创建普通词条
            int entryCount = RandomHelper.RandomNumber(entryRandomConfig.EntryRandMinCount + item.Quality,
                entryRandomConfig.EntryRandMaxCount + item.Quality);
            for (int i = 0; i < entryCount; i++)
            {
                EntryConfig entryConfig =
                        EntryConfigCategory.Instance.GetRandomEntryConfigByLevel((int)EntryType.Common, entryRandomConfig.EntryLevel);

                if (entryConfig == null)
                {
                    continue;
                }
                
                AttributeEntry entry = self.AddChild<AttributeEntry>();
                entry.Key = entryConfig.AttributeType;
                entry.Type = EntryType.Common;
                entry.Value = RandomHelper.RandomNumber(entryConfig.AttributeMinValue, entryConfig.AttributeMaxValue + item.Quality);
                self.EntryList.Add(entry);
                self.Score += entryConfig.EntryScore;
            }

            //创建特殊词条
            entryCount = RandomHelper.RandomNumber(entryRandomConfig.SpecialEntryRandMinCount, entryRandomConfig.SpecialEntryRandMaxCount);
            for (int i = 0; i < entryCount; i++)
            {
                EntryConfig entryConfig =
                        EntryConfigCategory.Instance.GetRandomEntryConfigByLevel((int)EntryType.Special, entryRandomConfig.SpecialEntryLevel);

                if (entryConfig == null)
                {
                    continue;
                }
                
                AttributeEntry entry = self.AddChild<AttributeEntry>();
                entry.Key = entryConfig.AttributeType;
                entry.Type = EntryType.Special;
                entry.Value = RandomHelper.RandomNumber(entryConfig.AttributeMinValue, entryConfig.AttributeMaxValue);
                self.EntryList.Add(entry);
                self.Score += entryConfig.EntryScore;
            }
        }

        public static EquipInfoProto ToMessage(this EquipInfoComponent self)
        {
            EquipInfoProto message = new EquipInfoProto() { Id = self.Id, Score = self.Score };
            for (int i = 0; i < self.EntryList.Count; i++)
            {
                message.AttributeEntryProtoList.Add(self.EntryList[i].ToMessage());
            }
            return message;
        }
    }
}
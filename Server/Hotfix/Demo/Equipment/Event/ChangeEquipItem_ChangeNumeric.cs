using ET.EventType;

namespace ET
{
    [FriendClass(typeof(AttributeEntry))]
    [FriendClass(typeof(EquipInfoComponent))]
    public class ChangeEquipItem_ChangeNumeric : AEvent<ChangeEquipItem>
    {
        protected override void Run(ChangeEquipItem a)
        {
            EquipInfoComponent equipInfoComponent = a.Item.GetComponent<EquipInfoComponent>();
            if (equipInfoComponent == null)
            {
                return;
            }
            
            NumericComponent numericComponent = a.Unit.GetComponent<NumericComponent>();
            foreach (var entry in equipInfoComponent.EntryList)
            {
                if (a.EquipOp == EquipOp.Load)
                {
                    numericComponent[entry.Key] += entry.Value;
                }
                else if (a.EquipOp == EquipOp.UnLoad)
                {
                    numericComponent[entry.Key] -= entry.Value;
                }
            }
        }
    }
}
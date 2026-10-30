using System;
using System.Collections.Generic;

namespace Radial_Menu
{
    public class Domain
    {
        public IReadOnlyList<Item_Data> Items => items;
        public readonly int Slots_Count;
        public int Selected_Item_Index {get; private set;}
        public Item_Data Selected_Item => items[Selected_Item_Index];
        public Item_Data Prev_Selected_Item {get; private set;}
        public event Action<int> On_Item_Selected;
        public bool Enabled {get; private set;} = true;
        public event Action<bool> On_Enable;
        
        private readonly List<Item_Data> items;

        public Domain(List<Item_Data> items, int min_slots)
        {
            if(items.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(items), items, "There has to be at least one item.");

            if(min_slots < 1)
                throw new ArgumentOutOfRangeException(nameof(min_slots), min_slots, "There has to be at least one slot.");

            this.items = new List<Item_Data>(items);

            Slots_Count = Math.Max(this.items.Count, min_slots);

            Selected_Item_Index = 0;
            Prev_Selected_Item = Selected_Item;
        }

        public void Enable(bool enabled)
        {
            if(this.Enabled == enabled)
                return;

            this.Enabled = enabled;
            On_Enable?.Invoke(this.Enabled);
        }

        public void Select_Item(int index)
        {
            if(index < 0 || index >= Items.Count)
                throw new ArgumentOutOfRangeException(nameof(index), index, "Index is out of range");

            if(Enabled == false)
                return;

            Prev_Selected_Item = Selected_Item;
            Selected_Item_Index = index;
            On_Item_Selected?.Invoke(index);
        }

        public void Select_Next_Item(int increment)
        {
            if(Enabled == false)
                return;

            int new_item_index = (Selected_Item_Index + increment) % Items.Count;

            if(new_item_index < 0)
                new_item_index = Items.Count + new_item_index;

            Prev_Selected_Item = Selected_Item;
            Selected_Item_Index = new_item_index;
            On_Item_Selected?.Invoke(new_item_index);
        }

        public void Trigger_Selected_Item()
        {
            if(Enabled == false)
                return;

            Selected_Item.Callback?.Invoke();
        }
    }
}
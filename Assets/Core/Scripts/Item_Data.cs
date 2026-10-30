using System;
using UnityEngine;

namespace Radial_Menu
{
    public class Item_Data
    {
        public Sprite Icon {get; private set;}
        public Action Callback {get; private set;}

        public Item_Data(Sprite icon, Action callback)
        {
            Icon = icon;
            Callback = callback;
        }
    }
}
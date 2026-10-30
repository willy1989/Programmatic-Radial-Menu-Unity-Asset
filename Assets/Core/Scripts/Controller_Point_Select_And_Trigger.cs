using UnityEngine;
using UnityEngine.InputSystem;

namespace Radial_Menu
{
    public class Controller_Point_Select_And_Trigger : Controller
    {   
        private InputAction select_action;
        private InputAction toggle_menu_action;

        public Controller_Point_Select_And_Trigger(Domain radial_menu_domain, InputAction select_action, InputAction trigger_action, InputAction toggle_menu_action)
            :base(radial_menu_domain)
        {
            this.select_action = select_action;
            this.toggle_menu_action = toggle_menu_action;
        }

        public override void Listen_Input()
        {
            Toggle_Menu();
            Select_Item();
            Trigger_Item();
        }

        private void Toggle_Menu()
        {
            if(toggle_menu_action.WasPerformedThisFrame() == true)
                Radial_Menu_Domain.Enable(!Radial_Menu_Domain.Enabled);
        }

        private void Select_Item()
        {
            float slot_angle = 360f/Radial_Menu_Domain.Slots_Count;

            int slot_index = Mathf.FloorToInt(Clock_Angle() / slot_angle);

            if(slot_index < 0 || slot_index >= Radial_Menu_Domain.Items.Count)
                return;

            Radial_Menu_Domain.Select_Item(slot_index);
        }

        private void Trigger_Item()
        {
            if(Radial_Menu_Domain.Selected_Item != Radial_Menu_Domain.Prev_Selected_Item)
                Radial_Menu_Domain.Trigger_Selected_Item();
        }

        private float Clock_Angle()
        {
            Vector2 offset_from_center = select_action.ReadValue<Vector2>() - new Vector2(Screen.width / 2f, Screen.height / 2f);

            // Swapped (x, y) arguments measure clockwise from 12 o'clock, unlike the usual (y, x) form.
            float signed_clock_angle = Mathf.Atan2(offset_from_center.x, offset_from_center.y) * Mathf.Rad2Deg;
            
            // normalize (-180, 180] to [0, 360)
            return Mathf.Repeat(signed_clock_angle, 360f);   
        }
    }
}
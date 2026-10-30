using UnityEngine;
using UnityEngine.InputSystem;

namespace Radial_Menu
{
    public class Controller_Cycle_Select_Then_Trigger : Controller
    {
        private InputAction select_action;
        private InputAction trigger_action;
        private InputAction toggle_menu_action;
        
        public Controller_Cycle_Select_Then_Trigger(Domain radial_menu_domain, InputAction select_action, InputAction trigger_action, InputAction toggle_menu_action)
            :base(radial_menu_domain)
        {
            this.select_action = select_action;
            this.trigger_action = trigger_action;
            this.toggle_menu_action = toggle_menu_action;
        }

        public override void Listen_Input()
        {
            if(select_action.WasPerformedThisFrame() == true)
            {
                int item_index_increment = Mathf.RoundToInt(select_action.ReadValue<float>());
                Radial_Menu_Domain.Select_Next_Item(item_index_increment);
            }

            if(trigger_action.WasPerformedThisFrame() == true)
                Radial_Menu_Domain.Trigger_Selected_Item();

            if(toggle_menu_action.WasPerformedThisFrame() == true)
                Radial_Menu_Domain.Enable(!Radial_Menu_Domain.Enabled);
        }
    }
}
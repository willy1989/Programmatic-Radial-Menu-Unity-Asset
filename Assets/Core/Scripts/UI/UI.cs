using System;
using UnityEngine;
using UnityEngine.UI;

namespace Radial_Menu
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private RectTransform content_container;
        [SerializeField] private Image highlight;
        [SerializeField] private Line_UI line_prefab;
        [SerializeField] private Icon_UI icon_prefab;

        private Domain radial_menu_domain;
        private float slot_angle;

        public void Construct(Domain radial_menu_domain)
        {
            if(this.radial_menu_domain != null)
                throw new InvalidOperationException("Radial_Menu_UI was already constructed. It can only be constructed once.");

            this.radial_menu_domain = radial_menu_domain;

            Configure_Highlight();

            slot_angle = 360f / this.radial_menu_domain.Slots_Count;
            
            Build_UI();
            Set_Visibility(this.radial_menu_domain.Enabled);
            Highlight_Item(this.radial_menu_domain.Selected_Item_Index);

            this.radial_menu_domain.On_Enable += Set_Visibility;
            this.radial_menu_domain.On_Item_Selected += Highlight_Item;
        }

        private void OnDestroy()
        {
            if(radial_menu_domain == null)
                return;

            radial_menu_domain.On_Enable -= Set_Visibility;
            radial_menu_domain.On_Item_Selected -= Highlight_Item;
        }

        private void Set_Visibility(bool on)
        {
            content_container.gameObject.SetActive(on);
        }

        private void Configure_Highlight()
        {
            highlight.type = Image.Type.Filled;
            highlight.fillMethod = Image.FillMethod.Radial360;
            highlight.fillOrigin = (int)Image.Origin360.Top;
            highlight.fillClockwise = true;
        }

        private void Build_UI()
        {
            highlight.fillAmount = 1f / radial_menu_domain.Slots_Count;

            for(int i = 0; i < radial_menu_domain.Slots_Count; i++)
                Instantiate(line_prefab, content_container).Set_Rotation(slot_angle * i);

            for(int i = 0; i < radial_menu_domain.Items.Count; i++)
            {
                Icon_UI icon = Instantiate(icon_prefab, content_container);
                icon.Set_Icon(radial_menu_domain.Items[i].Icon);
                icon.Set_Rotation(euler_angle_z: -slot_angle * (i + 0.5f));
            }
        }

        private void Highlight_Item(int item_index)
        {
            float z_euler = -slot_angle * item_index;

            highlight.rectTransform.localRotation = Quaternion.Euler(0f, 0f, z_euler);
        }
    }
}
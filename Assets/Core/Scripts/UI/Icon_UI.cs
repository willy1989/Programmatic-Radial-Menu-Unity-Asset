using UnityEngine;
using UnityEngine.UI;

namespace Radial_Menu
{
    public class Icon_UI : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private RectTransform container_rectTransform;
        [SerializeField] private RectTransform icon_rectTransform;

        public void Set_Icon(Sprite sprite)
        {
            image.sprite = sprite;
        }

        // Is there a better solution to keep the icon facing upwards?
        public void Set_Rotation(float euler_angle_z)
        {
            container_rectTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, euler_angle_z));
            icon_rectTransform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -euler_angle_z));
        }
    }
}
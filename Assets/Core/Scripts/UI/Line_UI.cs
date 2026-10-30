using UnityEngine;

namespace Radial_Menu
{
    public class Line_UI : MonoBehaviour
    {
        [SerializeField] private RectTransform rectransform;

        public void Set_Rotation(float angle_degrees)
        {
            rectransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 360f - angle_degrees));
        }
    }
}
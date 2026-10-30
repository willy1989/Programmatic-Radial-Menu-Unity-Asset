using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Radial_Menu;

public class Radial_Menu_Example : MonoBehaviour
{
    [SerializeField] private InputActionAsset input_action_asset;
    [SerializeField] private UI radial_menu_ui;
    [SerializeField] private Sprite[] icons;

    private InputActionMap radial_menu_map;
    private Controller radial_menu_controller;

    private readonly int radial_menu_min_slots = 4;

    private void Start()
    {
        List<Item_Data> items = new List<Item_Data>();

        foreach(Sprite icon in icons)
            items.Add(new Item_Data(icon, () => Debug.Log("Triggered " + icon.name)));

        Domain domain = new Domain(items, radial_menu_min_slots);

        radial_menu_map = input_action_asset.FindActionMap("radial_menu", throwIfNotFound: true);

        radial_menu_controller = new Controller_Point_Select_Then_Trigger(
        domain,
        radial_menu_map.FindAction("select_item_point", throwIfNotFound: true),
        radial_menu_map.FindAction("trigger_item", throwIfNotFound: true),
        radial_menu_map.FindAction("toggle_menu", throwIfNotFound: true));

        radial_menu_ui.Construct(domain);

        radial_menu_map.Enable();
    }

    private void Update() => radial_menu_controller.Listen_Input();
    private void OnDestroy() => radial_menu_map?.Disable();
}
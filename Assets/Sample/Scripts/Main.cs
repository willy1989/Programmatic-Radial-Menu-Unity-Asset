using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Radial_Menu;
using System;

public class Main : MonoBehaviour
{
    [SerializeField] private InputActionAsset input_action_asset;
    [SerializeField] private UI radial_menu_ui;
    [SerializeField] private Sprite[] weapon_icons;
    private InputActionMap radial_menu_map;
    private Controller radial_menu_controller;
    
    private readonly int radial_menu_min_slots = 4;

    private void Start()
    {
        Weapon[] weapons =
        {
            new Weapon("Stiletto", 3),
            new Weapon("Axe", 5),
            new Weapon("Gladius", 7),
            new Weapon("Kusarigama", 4),
            new Weapon("Spear", 8)
        };

        Weapon_Manager weapon_manager = new Weapon_Manager(weapons);

        List<Item_Data> item_data_list = Create_Item_Data_List(weapons, weapon_manager, weapon_icons);
        Domain radial_menu_domain = new Domain(item_data_list, radial_menu_min_slots);

        radial_menu_map = input_action_asset.FindActionMap("radial_menu", throwIfNotFound: true);
        radial_menu_controller = Create_Controller(radial_menu_domain, radial_menu_map);
        radial_menu_ui.Construct(radial_menu_domain);

        radial_menu_map.Enable();
    }

    private void Update()
    {
        radial_menu_controller.Listen_Input();
    }

    private void OnDestroy()
    {
        radial_menu_map?.Disable();
    }

    private List<Item_Data> Create_Item_Data_List(Weapon[] weapons, Weapon_Manager weapon_manager, Sprite[] icons)
    {
        if(icons.Length != weapons.Length)
            throw new ArgumentException("Expected one icon per weapon, got " + icons.Length + " icons for " + weapons.Length + " weapons.");

        List<Item_Data> item_data_list = new List<Item_Data>();

        for(int i = 0; i < weapons.Length; i++)
        {
            int index = i;
            item_data_list.Add(new Item_Data(icons[index], () => weapon_manager.Equip_Weapon(index)));
        }

        return item_data_list;
    }

    private Controller Create_Controller(Domain radial_menu_domain, InputActionMap map)
    {
        return new Controller_Point_Select_Then_Trigger(
            radial_menu_domain,
            map.FindAction("select_item_point", throwIfNotFound: true),
            map.FindAction("trigger_item", throwIfNotFound: true),
            map.FindAction("toggle_menu", throwIfNotFound: true));
    }
}
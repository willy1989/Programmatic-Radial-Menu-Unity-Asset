namespace Radial_Menu
{
    public abstract class Controller
    {
        protected Domain Radial_Menu_Domain;

        protected Controller(Domain radial_menu_domain)
        {
            Radial_Menu_Domain = radial_menu_domain;
        }

        public abstract void Listen_Input();
    }
}
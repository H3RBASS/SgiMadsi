namespace SgiMadsi.Shared.Layout;

public partial class Navbar
{
    private bool MostrarMenu;

    private async Task ToggleMenu()
    {
        MostrarMenu = !MostrarMenu;
    }
}

namespace KiraNet.GutsMVC.Implement
{
    public interface IControllerBulider
    {
        Controller ControllerBuild();
        void ControllerRelease();
    }
}
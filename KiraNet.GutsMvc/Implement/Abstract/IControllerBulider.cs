namespace KiraNet.GutsMvc.Implement
{
    public interface IControllerBulider
    {
        Controller ControllerBuild();
        void ControllerRelease();
    }
}
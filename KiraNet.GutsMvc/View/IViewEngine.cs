using KiraNet.GutsMVC.Implement;

namespace KiraNet.GutsMVC.View
{
    public interface IViewEngine
    {
    //    IView FindPartialView(ControllerContext controllerContext, string partialViewName);

        IView FindView(ControllerContext controllerContext, string folderName, string viewName);

        //void ReleaseView(ControllerContext controllerContext, IView view);
    }
}

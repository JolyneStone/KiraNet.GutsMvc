using KiraNet.GutsMvc.Implement;

namespace KiraNet.GutsMvc.View
{
    public interface IViewEngine
    {
    //    IView FindPartialView(ControllerContext controllerContext, string partialViewName);

        IView CreateView(ControllerContext controllerContext, string folderName, string viewName);

        //void ReleaseView(ControllerContext controllerContext, IView view);
    }
}

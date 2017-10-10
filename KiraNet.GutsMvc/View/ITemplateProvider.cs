using System.Threading.Tasks;

namespace KiraNet.GutsMvc.View
{
    public interface ITemplateProvider<Template>
        where Template : class
    {
        Task<Template> CompileTemplate(string viewName, ViewContext viewContext);
    }
}

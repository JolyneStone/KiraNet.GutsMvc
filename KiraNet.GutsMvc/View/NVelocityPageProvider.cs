using Commons.Collections;
using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;
using System;
using System.IO;

namespace KiraNet.GutsMVC.View
{
    public class NVelocityPageProvider : IPageProvider
    {
        private VelocityEngine _vltEngine;
        public NVelocityPageProvider(string folderName)
        {
            // 创建模板引擎
            _vltEngine = new VelocityEngine();


            ExtendedProperties props = new ExtendedProperties();

            // 使用文件型模板
            props.AddProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            props.AddProperty(RuntimeConstants.INPUT_ENCODING, "utf-8");
            props.AddProperty(RuntimeConstants.OUTPUT_ENCODING, "utf-8");
            // 模板存放目录 注：一个Controller一个目录
            props.AddProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, Path.Combine(ViewPath.Path, folderName));
            // 支持缓存，提高性能
            props.AddProperty(RuntimeConstants.FILE_RESOURCE_LOADER_CACHE, true);
            // 设置缓存每次检查的时间间隔
            props.AddProperty("file.resource.loader.modificationCheckInterval", "30");

            _vltEngine.Init(props);
        }

        public string CompilePage(string viewName, ViewContext viewContext)
        {
            if (string.IsNullOrWhiteSpace(viewName))
            {
                throw new ArgumentException("message", nameof(viewName));
            }

            var vltContext = new VelocityContext();

            // 传入View所需要的参数
            vltContext.Put("Model", viewContext.Model);

            // NVelocity不支持索引，这也是弃用了TempData和ViewBag的原因
            // 因此我把ViewData的每个键值对都作为变量传入模板中
            if(viewContext.ViewData != null)
            {
                foreach(var keyValuePair in viewContext.ViewData)
                {
                    vltContext.Put(keyValuePair.Key, keyValuePair.Value);
                }
            }

            Template vltTemplate;
            try
            {
                vltTemplate = _vltEngine.GetTemplate(viewName + ".html");
            }
            catch
            {
                vltTemplate = _vltEngine.GetTemplate(viewName + ".htm");
            }
            StringWriter vltWriter = new StringWriter();

            // 根据模板的上下文，将模板生成的内容写进vltWriter中
            vltTemplate.Merge(vltContext, vltWriter);

            string html = vltWriter.GetStringBuilder()?.ToString();
            if (String.IsNullOrWhiteSpace(html))
            {
                html = String.Empty;
            }

            return html;
        }
    }
}

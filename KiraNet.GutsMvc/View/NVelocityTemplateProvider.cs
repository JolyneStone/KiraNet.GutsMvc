using Commons.Collections;
using NVelocity;
using NVelocity.App;
using NVelocity.Exception;
using NVelocity.Runtime;
using System;
using System.IO;
using System.Threading.Tasks;

namespace KiraNet.GutsMvc.View
{
    internal class NVelocityTemplateProvider : ITemplateProvider<StringWriter>
    {
        private VelocityEngine _vltEngine;
        public NVelocityTemplateProvider(string folderName)
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

        public async Task<StringWriter> CompileTemplate(string viewName, ViewContext viewContext)
        {
            if (string.IsNullOrWhiteSpace(viewName))
            {
                throw new ArgumentException("message", nameof(viewName));
            }
            var vltContext = new VelocityContext();

            PutVariable(viewContext, vltContext);

            Template vltTemplate;
            try
            {
                vltTemplate = _vltEngine.GetTemplate(viewName + ".html");
            }
            catch (ResourceNotFoundException)
            {
                try
                {
                    vltTemplate = _vltEngine.GetTemplate(viewName + ".htm");
                }
                catch(ResourceNotFoundException)
                {
                    throw new FileNotFoundException($"无法找到或加载指定视图{viewName}");
                }
            }

            StringWriter vltWriter = new StringWriter();

            // 根据模板的上下文，将模板生成的内容写进vltWriter中
            await Task.Run(() => vltTemplate.Merge(vltContext, vltWriter));

            //string html = vltWriter.GetStringBuilder()?.ToString();
            //if (String.IsNullOrWhiteSpace(html))
            //{
            //    html = String.Empty;
            //}

            return vltWriter;
        }

        /// <summary>
        /// 传入模板所需要的参数
        /// </summary>
        /// <param name="viewContext"></param>
        /// <param name="vltContext"></param>
        private void PutVariable(ViewContext viewContext, VelocityContext vltContext)
        {
            vltContext.Put("Model", viewContext.Model);

            // NVelocity不支持索引，这也是弃用了TempData和ViewBag的原因
            // 因此我把ViewData的每个键值对都作为变量传入模板中
            if (viewContext.ViewData != null)
            {
                foreach (var keyValuePair in viewContext.ViewData)
                {
                    vltContext.Put(keyValuePair.Key, keyValuePair.Value);
                }
            }

            if (viewContext.TempData != null)
            {
                foreach (var keyValuePair in viewContext.TempData)
                {
                    vltContext.Put(keyValuePair.Key, keyValuePair.Value);
                }
            }

            if (viewContext.ViewBag != null)
            {
                vltContext.Put("ViewBag", viewContext.ViewBag);
            }

            vltContext.Put("Context", viewContext);
        }
    }
}

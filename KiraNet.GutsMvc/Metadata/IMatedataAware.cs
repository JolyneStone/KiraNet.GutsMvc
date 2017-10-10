
namespace KiraNet.GutsMvc.Metadata
{
    /// <summary>
    /// 两个实现了该接口的特性分别是AdditionalMetadataAttribute、AllowHtmlAttribute
    /// </summary>
    public interface IMatedataAware
    {
        /// <summary>
        /// 用于定制Model元数据
        /// </summary>
        /// <param name="metadata"></param>
        void OnMetadataCreated(ModelMetadata metadata);
    }
}

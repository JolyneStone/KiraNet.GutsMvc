using KiraNet.GutsMVC.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace KiraNet.GutsMVC.Metadata
{
    /// <summary>
    /// 表示元数据类
    /// </summary>
    public class ModelMetadata
    {
        private IEnumerable<ModelMetadata> _properties;
        private object _model;
        private Func<object> _accessor;

        public ModelMetadata(
            ModelMetadataProvider provider,
            Type containerType,
            Func<object> modelAccessor,
            Type modelType,
            string propertyName)
        {
            Provider = provider ?? throw new ArgumentNullException("provider");
            ModelType = modelType ?? throw new ArgumentNullException("modelType");
            ContainerType = containerType;
            _accessor = modelAccessor;
            IsRequired = !TypeHelper.TypeAllowsNullValue(modelType);
            PropertyName = propertyName;

            var type = containerType ?? modelType;
            var displayColumnAttribute = type.GetTypeInfo().GetCustomAttribute<DisplayColumnAttribute>();
            if (displayColumnAttribute != null)
            {
                _displayColumn = displayColumnAttribute;
            }
        }


        protected DisplayColumnAttribute _displayColumn;

        /// <summary>
        /// 表示属性名，如果该元数据是一个属性的话
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// 表示一个获取Model对象的委托
        /// </summary>
        public Func<object> ModelAccessor { get; set; }

        /// <summary>
        /// 描述目标数据类型本身的ModelMetadata和描述其数据成员的ModelMetadata是一种包含关系
        /// 我们将前者称之为后者的容器（Container）
        /// </summary>
        public virtual IEnumerable<ModelMetadata> Properties
        {
            get
            {
                if (_properties == null)
                {
                    _properties = Provider.GetMetadataForProperties(Model, ModelType);
                }

                return _properties;
            }
        }

        /// <summary>
        /// 表示Model自身的数据类型
        /// </summary>
        public Type ModelType { get; }

        /// <summary>
        /// 指示是否是一个复杂类型，复杂类型的Properties应该不为空
        /// <!--
        /// 复杂类型和简单类型的区分：
        /// 判断某个类型是否是复杂类型的条件只有一个——它是否支持源自字符串的类型转换。
        /// 由此，所有的基元类型和可空值类型都是简单类型
        /// 一个自定义的数据类型在默认情况下总是一个复杂类型，除非我们在类型上应用一个TypeConverterAttribute特性并指定一个支持字符串转换的TypeConverter类型
        /// -->
        /// </summary>
        public bool IsComplexType
        {
            get
            {
                return !TypeDescriptor.GetConverter(ModelType).CanConvertFrom(typeof(string));
            }
        }

        /// <summary>
        /// 指示是否是一个可空值类型
        /// </summary>
        public virtual bool IsNullableValueType
        {
            get
            {
                return TypeHelper.IsNullableValueType(ModelType);
            }
        }

        /// <summary>
        /// 表示其容器类型，对于源容器，该属性为Null
        /// </summary>
        public Type ContainerType { get; }

        /// <summary>
        /// 表示具体的数据对象
        /// </summary>
        public object Model
        {
            get
            {
                if(_accessor != null)
                {
                    _model = _accessor();
                    _accessor = null;
                }

                return _model;
            }
            set
            {
                _model = value;
                _accessor = null;
                _properties = null;
            }
        }

        /// <summary>
        /// 用于存储一些自定义属性的名称和值
        /// 我们可以在数据类型或者其数据成员上应用AdditionalMetadataAttribute特性为对应的ModelMetadata对象添加相应的自定义属性
        /// </summary>
        public virtual Dictionary<string, object> AdditionalValues { get; }

        /// <summary>
        /// ModelMetadata提供者
        /// </summary>
        protected virtual ModelMetadataProvider Provider { get; set; }

        /// <summary>
        /// 表示默认模板名称
        /// 何谓模板？其实就是各种HtmlHelper的模板方法或者是TagHelper方法又或者是某些特性中所指定的模板
        /// </summary>
        public virtual string TemplateHint { get; set; }

        /// <summary>
        /// 表示目标元素是否需要显示在界面上，一般由HiddenInputAttribute的Display属性决定
        /// </summary>
        public virtual bool HideSurroundingHtml { get; set; }

        /// <summary>
        /// 表示显示的数据类型的名称，对应DataTypeAttribute特性中设置的数据类型
        /// 如果DataTypeAttribute通过一个DataType枚举表示数据类型，则该属性返回已字符串表示的DataType枚举值
        /// 如果DataTypeAttribute的DataType属性为DataType.Custom，则表示数据类型为用户定制类型。此时，该属性为字符串形式的自定义数据类型
        /// </summary>
        public virtual string DataTypeName { get; set; }

        /// <summary>
        /// 指示是否将传入的空字符串转换成Null，对应DisplayFormatAttribute的同名属性
        /// </summary>
        public virtual bool ConvertEmptyStringToNull { get; set; }

        /// <summary>
        /// 针对空值的显示文本，对应DisplayFormatAttribute的同名属性
        /// </summary>
        public virtual string NullDisplayText { get; set; }

        /// <summary>
        /// 表示格式化字符串，对应DisplayFormatAttribute的同名属性
        /// </summary>
        public virtual string DisplayFormatString { get; set; }

        /// <summary>
        /// 表示编辑模式下的格式化字符串，如果DisplayFormatAttribute的ApplyFormatInEditMode属性为ture，则其DisplayFormatString将被赋值给该属性
        /// </summary>
        public virtual string EditFormatString { get; set; }

        /// <summary>
        /// 指示数据对象是否应该出现在显示模式下的HTML中，对应ScaffoldColumnAttribute的Scaffold属性
        /// </summary>
        public virtual bool ShowForDisplay { get; set; } = true;

        /// <summary>
        /// 指示数据对象是否应该出现在编辑模式下的HTML中，对应ScaffoldColumnAttribute的Scaffold属性
        /// </summary>
        public virtual bool ShowForEdit { get; set; } = true;

        /// <summary>
        /// 指示是否是只读类型，对应EditableAttribute和ReadonlyAttribute，但EditableAttribute拥有更高的优先级
        /// </summary>
        public virtual bool IsReadOnly { get; set; }

        /// <summary>
        /// 指示该数据元素是必需的。对应RequiredAttribute
        /// </summary>
        public virtual bool IsRequired { get; set; }

        /// <summary>
        /// 指示是否进行请求数据的验证，默认为ture。对应AllowHtmlAttribute
        /// 验证是指检查是否有任何HTML标记被包含其中
        /// </summary>
        public virtual bool RequestValidationEnabled { get; set; } = true;

        /// <summary>
        /// 获取或设置可用作水印的值
        /// </summary>
        public virtual string Watermark { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值表示当前元数据的顺序。默认值为10000
        /// </summary>
        public virtual int Order { get; set; } = 10000;

        /// <summary>
        /// 获取或设置短的展示名称
        /// </summary>
        public virtual string ShortDisplayName { get; set; }

        public virtual string Description { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual bool HasNonDefaultEditFormat { get; set; }

        protected virtual string GetSimpleDisplayText()
        {
            // DisplayColumnAttribute属性是特殊的，因为只有它是作用在Model类之上的
            // 因此需要在经过一些特殊处理
            if (Model != null)
            {
                if (_displayColumn != null && !String.IsNullOrEmpty(_displayColumn.DisplayColumn))
                {
                    PropertyInfo displayColumnProperty = ModelType.GetProperty(
                        _displayColumn.DisplayColumn,
                        BindingFlags.Public |
                        BindingFlags.IgnoreCase |
                        BindingFlags.Instance);

                    if (displayColumnProperty == null)
                    {
                        throw new InvalidOperationException($"参数{nameof(_displayColumn)}不能为空");
                    }
                    if (displayColumnProperty.GetGetMethod() == null)
                    {
                        throw new InvalidOperationException($"{nameof(displayColumnProperty.Name)}没有公有Get方法");
                    }

                    object simpleDisplayTextValue = displayColumnProperty.GetValue(Model, new object[0]);
                    if (simpleDisplayTextValue != null)
                    {
                        return simpleDisplayTextValue.ToString();
                    }
                }
            }


            if (Model == null)
            {
                return NullDisplayText;
            }

            string toStringResult = Convert.ToString(Model, CultureInfo.CurrentCulture);
            if (toStringResult == null)
            {
                return String.Empty;
            }

            if (!toStringResult.Equals(Model.GetType().FullName, StringComparison.Ordinal))
            {
                return toStringResult;
            }

            ModelMetadata firstProperty = Properties.FirstOrDefault();
            if (firstProperty == null)
            {
                return String.Empty;
            }

            if (firstProperty.Model == null)
            {
                return firstProperty.NullDisplayText;
            }

            return Convert.ToString(firstProperty.Model, CultureInfo.CurrentCulture);
        }


        protected virtual bool ComputeConvertEmptyStringToNull()
        {
            return ConvertEmptyStringToNull;
        }

        protected virtual string ComputeDataTypeName()
        {
            return DataTypeName;
        }

        protected virtual string ComputeDescription()
        {
            return Description;
        }

        protected virtual string ComputeDisplayFormatString()
        {
            return DisplayFormatString;
        }

        protected virtual string ComputeDisplayName()
        {
            return DisplayName;
        }

        protected virtual string ComputeEditFormatString()
        {
            return EditFormatString;
        }

        protected virtual bool ComputeHasNonDefaultEditFormat()
        {
            return HasNonDefaultEditFormat;
        }

        protected virtual bool ComputeHideSurroundingHtml()
        {
            return HideSurroundingHtml;
        }

        protected virtual bool ComputeIsReadOnly()
        {
            return IsReadOnly;
        }

        protected virtual bool ComputeIsRequired()
        {
            return IsRequired;
        }

        protected virtual string ComputeNullDisplayText()
        {
            return NullDisplayText;
        }

        protected virtual int ComputeOrder()
        {
            return Order;
        }

        protected virtual string ComputeShortDisplayName()
        {
            return ShortDisplayName;
        }

        protected virtual bool ComputeShowForDisplay()
        {
            return ShowForDisplay;
        }

        protected virtual bool ComputeShowForEdit()
        {
            return ShowForEdit;
        }

        protected virtual string ComputeSimpleDisplayText()
        {
            return GetSimpleDisplayText();
        }

        protected virtual string ComputeTemplateHint()
        {
            return TemplateHint;
        }

        protected virtual string ComputeWatermark()
        {
            return Watermark;
        }
    }
}

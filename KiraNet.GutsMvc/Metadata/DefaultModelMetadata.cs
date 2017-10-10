using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KiraNet.GutsMvc.Metadata
{
    /// <summary>
    /// 用于基于注解特性的声明式元数据
    /// </summary>
    public class DefaultModelMetadata : ModelMetadata
    {

        private string _dataTypeName;
        private string _description;
        private string _displayFormatString;
        private string _displayName;
        private string _editFormatString;
        private string _nullDisplayText;
        private int _order;
        private string _shortDisplayName;
        private string _templateHint;
        private string _watermark;

        //private bool _convertEmptyStringToNull;
        //private bool _showForDisplay;
        //private bool _showForEdit;
        //private bool _hasNonDefaultEditFormat;
        //private bool _hideSurroundingHtml;
        //private bool _isReadOnly;
        //private bool _isRequired;
        //private bool _convertEmptyStringToNullComputed;
        //private bool _dataTypeNameComputed;
        //private bool _descriptionComputed;
        //private bool _displayFormatStringComputed;
        //private bool _displayNameComputed;
        //private bool _editFormatStringComputed;
        //private bool _hasNonDefaultEditFormatComputed;
        //private bool _hideSurroundingHtmlComputed;
        //private bool _isReadOnlyComputed;
        //private bool _isRequiredComputed;
        //private bool _nullDisplayTextComputed;
        //private bool _orderComputed;
        //private bool _shortDisplayNameComputed;
        //private bool _showForDisplayComputed;
        //private bool _showForEditComputed;
        //private bool _templateHintComputed;
        //private bool _watermarkComputed;
        private bool _isEditFormatStringFromCache;

        public DefaultModelMetadata(
            DefaultModelMetadataProvider provider,
            Type containerType,
            Func<object> modelAccessor,
            Type modelType,
            string propertyName) :
            base(provider, containerType, modelAccessor, modelType, propertyName)
        {
        }

        public DefaultModelMetadata(
            DefaultModelMetadata prototype,
            Func<object> modelAccessor) :
            base(prototype.Provider, prototype.ContainerType, modelAccessor, prototype.ModelType, prototype.PropertyName)
        {
            PrototypeCache = prototype.PrototypeCache;
        }

        public DefaultModelMetadata(
            DefaultModelMetadataProvider provider,
            Type containerType,
            Type modelType,
            string propertyName,
            IEnumerable<Attribute> attributes)
            : this(provider, containerType, modelType, propertyName, new MetadataAttributes(attributes.ToArray()))
        {
        }

        protected DefaultModelMetadata(DefaultModelMetadataProvider provider, Type containerType, Type modelType, string propertyName, MetadataAttributes prototypeCache)
            : base(provider, containerType, null /* modelAccessor */, modelType, propertyName)
        {
            PrototypeCache = prototypeCache;
        }


        protected MetadataAttributes PrototypeCache { get; set; }

        public override string DataTypeName { get => ComputeDataTypeName(); set => _dataTypeName = value ?? String.Empty; }
        public override string Description { get => ComputeDescription(); set => _description = value ?? String.Empty; }
        public override string DisplayFormatString { get => ComputeDisplayFormatString(); set => _displayFormatString = value ?? String.Empty; }
        public override string DisplayName { get => ComputeDisplayName(); set => _displayName = value ?? String.Empty; }
        public override string EditFormatString { get => ComputeEditFormatString(); set => _editFormatString = value ?? String.Empty; }
        public override bool HasNonDefaultEditFormat { get => ComputeHasNonDefaultEditFormat(); set => base.HasNonDefaultEditFormat = value; }
        public override bool HideSurroundingHtml { get => ComputeHideSurroundingHtml(); set => base.HideSurroundingHtml = value; }
        public override bool IsReadOnly { get => ComputeIsReadOnly(); set => base.IsReadOnly = value; }
        public override bool IsRequired { get => ComputeIsRequired(); set => base.IsRequired = value; }
        public override string NullDisplayText { get => ComputeNullDisplayText(); set => _nullDisplayText = value ?? String.Empty; }
        public override string ShortDisplayName { get => ComputeShortDisplayName(); set => _shortDisplayName = value ?? String.Empty; }
        public override int Order { get => ComputeOrder(); set => _order = value; }
        public override bool ShowForDisplay { get => ComputeShowForDisplay(); set => base.ShowForDisplay = value; }
        public override bool ShowForEdit { get => ComputeShowForEdit(); set => base.ShowForEdit = value; }
        public override string TemplateHint { get => ComputeTemplateHint(); set => _templateHint = value; }
        public override string Watermark { get => ComputeWatermark(); set => _watermark = value ?? String.Empty; }
        public override bool ConvertEmptyStringToNull
        {
            get
            {
                return PrototypeCache.DisplayFormat != null
                  ? PrototypeCache.DisplayFormat.ConvertEmptyStringToNull
                  : base.ConvertEmptyStringToNull;
            }
            set
            {
                base.ConvertEmptyStringToNull = value;
            }
        }

        protected override string ComputeDataTypeName()
        {
            if (String.IsNullOrWhiteSpace(_dataTypeName))
            {
                if (PrototypeCache.DataType != null)
                {
                    return _dataTypeName = PrototypeCache.DataType.GetDataTypeName();
                }

                if (PrototypeCache.DisplayFormat != null && !PrototypeCache.DisplayFormat.HtmlEncode)
                {
                    return _dataTypeName = DataType.Html.ToString();
                }

                return base.DataTypeName;
            }

            return _dataTypeName;
        }

        protected override string ComputeDescription()
        {
            if (String.IsNullOrWhiteSpace(_description))
            {
                _description = PrototypeCache.Display != null
                            ? PrototypeCache.Display.GetDescription()
                            : base.Description;

                return _description;
            }

            return _description;
        }

        protected override string ComputeDisplayFormatString()
        {
            if (String.IsNullOrWhiteSpace(_displayFormatString))
            {
                _displayFormatString = PrototypeCache.DisplayFormat != null
                    ? PrototypeCache.DisplayFormat.DataFormatString
                    : base.DisplayFormatString;

                return _displayFormatString;
            }

            return _displayFormatString;
        }

        protected override string ComputeDisplayName()
        {
            if (String.IsNullOrWhiteSpace(_displayName))
            {
                string result = null;

                if (PrototypeCache.Display != null)
                {
                    result = PrototypeCache.Display.GetName();
                }

                if (result == null && PrototypeCache.DisplayName != null)
                {
                    result = PrototypeCache.DisplayName.DisplayName;
                }

                _displayName = result ?? base.DisplayName;
            }

            return _displayName;
        }

        protected override string ComputeEditFormatString()
        {
            if (String.IsNullOrWhiteSpace(_editFormatString))
            {
                if (PrototypeCache.DisplayFormat != null && PrototypeCache.DisplayFormat.ApplyFormatInEditMode)
                {
                    _isEditFormatStringFromCache = true;
                    return _editFormatString = PrototypeCache.DisplayFormat.DataFormatString;
                }

                return _editFormatString = base.EditFormatString;
            }

            return _editFormatString;
        }

        protected override bool ComputeHasNonDefaultEditFormat()
        {
            if (!String.IsNullOrEmpty(EditFormatString) && _isEditFormatStringFromCache)
            {
                if (PrototypeCache.DataType == null)
                {
                    return true;
                }

                if (PrototypeCache.DataType.DisplayFormat != PrototypeCache.DisplayFormat)
                {
                    return true;
                }

                if (PrototypeCache.DataType.GetType() != typeof(DataTypeAttribute))
                {
                    return true;
                }
            }

            return base.HasNonDefaultEditFormat;
        }

        protected override bool ComputeHideSurroundingHtml()
        {
            return PrototypeCache.HiddenInput != null
                       ? !PrototypeCache.HiddenInput.DisplayValue
                       : base.HideSurroundingHtml;
        }

        protected override bool ComputeIsReadOnly()
        {
            if (PrototypeCache.Editable != null)
            {
                return !PrototypeCache.Editable.AllowEdit;
            }

            if (PrototypeCache.ReadOnly != null)
            {
                return PrototypeCache.ReadOnly.IsReadOnly;
            }

            return base.IsReadOnly;
        }

        protected override bool ComputeIsRequired()
        {
            return PrototypeCache.Required != null
                       ? true
                       : base.IsRequired;
        }

        protected override string ComputeNullDisplayText()
        {
            if (String.IsNullOrWhiteSpace(_nullDisplayText))
            {
                _nullDisplayText = PrototypeCache.DisplayFormat != null
                           ? PrototypeCache.DisplayFormat.NullDisplayText
                           : base.NullDisplayText;
            }

            return _nullDisplayText;
        }

        protected override int ComputeOrder()
        {
            if (_order == 10000)
            {
                int? result = null;

                if (PrototypeCache.Display != null)
                {
                    result = PrototypeCache.Display.GetOrder();
                }

                _order = result ?? base.Order;
            }

            return _order;
        }

        protected override string ComputeShortDisplayName()
        {
            if (String.IsNullOrWhiteSpace(_shortDisplayName))
            {
                _shortDisplayName = PrototypeCache.Display != null
                           ? PrototypeCache.Display.GetShortName()
                           : base.ShortDisplayName;
            }

            return _shortDisplayName;
        }

        protected override bool ComputeShowForDisplay()
        {
            return PrototypeCache.ScaffoldColumn != null
                       ? PrototypeCache.ScaffoldColumn.Scaffold
                       : base.ShowForDisplay;
        }

        protected override bool ComputeShowForEdit()
        {
            return PrototypeCache.ScaffoldColumn != null
                       ? PrototypeCache.ScaffoldColumn.Scaffold
                       : base.ShowForEdit;
        }

        protected override string ComputeTemplateHint()
        {
            if (String.IsNullOrWhiteSpace(_templateHint))
            {
                if (PrototypeCache.UIHint != null)
                {
                    _templateHint = PrototypeCache.UIHint.UIHint;
                }
                else if (PrototypeCache.HiddenInput != null)
                {
                    _templateHint = "HiddenInput";
                }
                else
                {
                    _templateHint = base.TemplateHint;
                }
            }

            return _templateHint;
        }

        protected override string ComputeWatermark()
        {
            if (String.IsNullOrWhiteSpace(_watermark))
            {
                _watermark = PrototypeCache.Display != null
                           ? PrototypeCache.Display.GetPrompt()
                           : base.Watermark;
            }

            return _watermark;
        }
    }
}

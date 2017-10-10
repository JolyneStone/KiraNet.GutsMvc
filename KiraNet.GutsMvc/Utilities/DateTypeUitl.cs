//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

//namespace KiraNet.GutsMvc
//{
//    internal static class DataTypeUtil
//    {
//        private static readonly string CreditCardTypeName = DataType.CreditCard.ToString();
//        private static readonly string CurrencyTypeName = DataType.Currency.ToString();
//        private static readonly string DateTypeName = DataType.Date.ToString();
//        private static readonly string DateTimeTypeName = DataType.DateTime.ToString();
//        private static readonly string DurationTypeName = DataType.Duration.ToString();
//        private static readonly string EmailAddressTypeName = DataType.EmailAddress.ToString();
//        internal static readonly string HtmlTypeName = DataType.Html.ToString();
//        private static readonly string ImageUrlTypeName = DataType.ImageUrl.ToString();
//        private static readonly string MultiLineTextTypeName = DataType.MultilineText.ToString();
//        private static readonly string PasswordTypeName = DataType.Password.ToString();
//        private static readonly string PhoneNumberTypeName = DataType.PhoneNumber.ToString();
//        private static readonly string PostalCodeTypeName = DataType.PostalCode.ToString();
//        private static readonly string TextTypeName = DataType.Text.ToString();
//        private static readonly string TimeTypeName = DataType.Time.ToString();
//        private static readonly string UploadTypeName = DataType.Upload.ToString();
//        private static readonly string UrlTypeName = DataType.Url.ToString();

//        private static readonly Lazy<Dictionary<object, string>> _dataTypeToName = new Lazy<Dictionary<object, string>>(CreateDataTypeToName, isThreadSafe: true);

//        internal static string ToDataTypeName(this DataTypeAttribute attribute, Func<DataTypeAttribute, Boolean> isDataType = null)
//        {
//            if (isDataType == null)
//            {
//                isDataType = t => t.GetType().Equals(typeof(DataTypeAttribute));
//            }

//            if (isDataType(attribute))
//            {
//                string name = KnownDataTypeToString(attribute.DataType);
//                if (name == null)
//                {
//                    _dataTypeToName.Value.TryGetValue(attribute.DataType, out name);
//                }

//                if (name != null)
//                {
//                    return name;
//                }
//            }

//            return attribute.GetDataTypeName();
//        }

//        private static string KnownDataTypeToString(DataType dataType)
//        {
//            switch (dataType)
//            {
//                case DataType.CreditCard:
//                    return CreditCardTypeName;
//                case DataType.Currency:
//                    return CurrencyTypeName;
//                case DataType.Date:
//                    return DateTypeName;
//                case DataType.DateTime:
//                    return DateTimeTypeName;
//                case DataType.Duration:
//                    return DurationTypeName;
//                case DataType.EmailAddress:
//                    return EmailAddressTypeName;
//                case DataType.Html:
//                    return HtmlTypeName;
//                case DataType.ImageUrl:
//                    return ImageUrlTypeName;
//                case DataType.MultilineText:
//                    return MultiLineTextTypeName;
//                case DataType.Password:
//                    return PasswordTypeName;
//                case DataType.PhoneNumber:
//                    return PhoneNumberTypeName;
//                case DataType.PostalCode:
//                    return PostalCodeTypeName;
//                case DataType.Text:
//                    return TextTypeName;
//                case DataType.Time:
//                    return TimeTypeName;
//                case DataType.Upload:
//                    return UploadTypeName;
//                case DataType.Url:
//                    return UrlTypeName;
//            }

//            return null;
//        }

//        private static Dictionary<object, string> CreateDataTypeToName()
//        {
//            Dictionary<object, string> dataTypeToName = new Dictionary<object, string>();
//            foreach (DataType dataTypeValue in Enum.GetValues(typeof(DataType)))
//            {
//                if (dataTypeValue != DataType.Custom && KnownDataTypeToString(dataTypeValue) == null)
//                {
//                    string name = Enum.GetName(typeof(DataType), dataTypeValue);
//                    dataTypeToName[dataTypeValue] = name;
//                }
//            }

//            return dataTypeToName;
//        }
//    }
//}

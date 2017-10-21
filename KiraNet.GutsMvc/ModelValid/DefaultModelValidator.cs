using KiraNet.GutsMvc.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace KiraNet.GutsMvc.ModelValid
{
    public class DefaultModelValidator : ModelValidator
    {
        public override bool IsRequired { get; set; } = true;
        public override ModelValidDictionary Validate(object model, Type modelType, string modelName)
        {
            if (IsRequired == false)
            {
                return null;
            }

            if (modelName == null)
            {
                modelName = "";
            }

            ModelValidDictionary modelValidDictionary = new ModelValidDictionary();
            if (modelType == null && model == null)
            {
                var validFailure = new ValidFailure()
                {
                    ErrorMessage = "model与modelType都为空"
                };

                var modelValid = new ModelValid();
                modelValid.ValidResults.Add(validFailure);

                modelValidDictionary.Add(modelName, modelValid);
                return modelValidDictionary;
            }

            if (modelType == null && model != null)
            {
                modelType = model.GetType();
            }

            ValidateModel(modelValidDictionary, model, modelType, modelName);

            return modelValidDictionary;
        }

        private void ValidateModel(ModelValidDictionary modelValidDictionary, object model, Type modelType, string modelName)
        {
            var modelValid = GetModelValid(model, modelType, modelName);
            if (modelValid != null && modelValid.ValidResults != null && modelValid.ValidResults.Any())
            {
                modelValidDictionary.Add(modelName, modelValid);
            }

            if (model == null)
            {
                return;
            }

            if (modelType == typeof(string) ||
                TypeHelper.IsPrimitiveType(modelType) ||
                TypeHelper.IsNullableValueType(modelType))
            {
                return;
            }

            var properties = modelType.GetProperties(
                BindingFlags.GetProperty |
                BindingFlags.SetProperty |
                BindingFlags.Instance |
                BindingFlags.Public
                );

            if (properties != null || properties.Any())
            {
                foreach (var property in properties)
                {
                    ValidProperty(property, model, modelValidDictionary, modelName);
                }
            }

            if (modelType != typeof(string) &&
                typeof(IEnumerable<>).IsAssignableFrom(modelType))
            {
                Expression callExpr = Expression.Call(
                    Expression.Constant(this),
                    typeof(DefaultModelValidator)
                        .GetMethod("ValidEnumerable",
                            BindingFlags.Static |
                            BindingFlags.NonPublic)
                        .MakeGenericMethod(modelType
                        .GetInterface(typeof(IEnumerable<>).FullName)
                        .GetGenericArguments()[0]),
                    Expression.Constant(model, typeof(object)),
                    Expression.Constant(modelValidDictionary, typeof(ModelValidDictionary)),
                    Expression.Constant(modelName, typeof(string))
                    );

                Expression.Lambda<Action>(callExpr).Compile()();
            }
        }

        private void ValidEnumerable<T>(object model, ModelValidDictionary modelValidDictionary, string modelName)
        {
            var enumerables = model as IEnumerable<T>;
            if (enumerables == null)
            {
                return;
            }

            int index = 0;
            var modelType = typeof(T);
            foreach (var x in enumerables)
            {
                ValidateModel(modelValidDictionary, x, modelType, $"{modelName}.[{index}]");
                index++;
            }
        }

        private void ValidProperty(PropertyInfo property, object model, ModelValidDictionary modelValidDictionary, string modelName)
        {
            var propertyName = modelName + "." + property.Name;
            var validitions = property.GetCustomAttributes<ValidationAttribute>();
            var modelValid = new ModelValid();
            ValidValidAttribute(new ValidationContext(model), property.GetValue(model), validitions, modelValid, propertyName);

            if (modelValid != null &&
                modelValid.ValidResults != null &&
                modelValid.ValidResults.Any())
            {
                modelValidDictionary.Add(propertyName, modelValid);
            }

            ValidateModel(
                modelValidDictionary,
                model != null ? property.GetValue(model) : null,
                property.PropertyType,
                propertyName);
        }

        private static ModelValid GetModelValid(object model, Type modelType, string modelName)
        {
            var modelValid = new ModelValid();
            ValidValidAttribute(new ValidationContext(model), model, modelType, modelValid, modelName);
            ValidValidatableObject(model, modelType, modelValid, modelName);
            ValidDataErrorInfo(model, modelType, modelValid, modelName);

            return modelValid;
        }


        private static void ValidValidAttribute(ValidationContext validationContext, object model, Type modelType, ModelValid modelValid, string modelName)
        {
            var validations = modelType.GetCustomAttributes<ValidationAttribute>();
            ValidValidAttribute(validationContext, model, validations, modelValid, modelName);
        }

        private static void ValidValidAttribute(ValidationContext validationContext, object model, IEnumerable<ValidationAttribute> validations, ModelValid modelValid, string modelName)
        {
            if (validations == null || !validations.Any())
            {
                return;
            }
            foreach (var validation in validations)
            {
                bool result = false;
                Exception exception = null;
                try
                {
                    //result = validation.IsValid(model);
                    result = validation.GetValidationResult(model, validationContext) == ValidationResult.Success;
                }
                catch (Exception ex)
                {
                    exception = ex;
                }

                if (result)
                {
                    continue;
                }


                modelValid.ValidResults.Add(new ValidFailure()
                {
                    ErrorMessage = validation.ErrorMessage ?? validation.ToString(),
                    ErrorMessageResourceName = validation.ErrorMessageResourceName,
                    ErrorMessageResourceType = validation.ErrorMessageResourceType,
                    ErrorMessageString = validation.FormatErrorMessage(""),
                    Exception = exception,
                    Name = modelName
                });
            }
        }

        private static void ValidValidatableObject(object model, Type modelType, ModelValid modelValid, string modelName)
        {
            IValidatableObject validatableObject = model as IValidatableObject;

            if (validatableObject == null)
            {
                return;
            }

            ValidationContext validationContext = new ValidationContext(model);
            foreach (var validResult in validatableObject.Validate(validationContext))
            {
                modelValid.ValidResults.Add(new ValidFailure()
                {
                    ErrorMessage = validResult.ErrorMessage,
                    MemberNames = validResult.MemberNames,
                    Name = modelName
                });
            }
        }

        public static void ValidDataErrorInfo(object model, Type modelType, ModelValid modelValid, string modelName = "")
        {
            IDataErrorInfo dataErrorInfo = model as IDataErrorInfo;
            if (dataErrorInfo == null)
            {
                return;
            }

            foreach (var propertyName in modelType.GetProperties(
                BindingFlags.GetProperty |
                BindingFlags.SetProperty |
                BindingFlags.Instance |
                BindingFlags.Public)
                .Select(x => x.Name))
            {
                var errorMessage = dataErrorInfo[propertyName];
                if (!String.IsNullOrWhiteSpace(errorMessage))
                {
                    modelValid.ValidResults.Add(new ValidFailure
                    {
                        ErrorMessage = errorMessage,
                        ErrorMessageResourceName = propertyName,
                        Name = modelName + "." + propertyName
                    });
                }
            }
        }
    }
}

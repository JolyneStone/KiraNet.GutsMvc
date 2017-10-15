using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace KiraNet.GutsMvc.ModelValid
{
    public class ModelState : IModelState
    {
        private ModelValidator _modelValidator;
        public ModelState(IServiceProvider services)
        {
            _modelValidator = services.GetService<ModelValidator>();
        }

        public IEnumerable<ModelWrapper> ModelWrappers { get; set; }
        public ModelValidDictionary ModelValidDictionary { get; set; }

        public bool IsValid
        {
            get
            {
                if (ModelValidDictionary == null)
                {
                    ModelValidDictionary = new ModelValidDictionary();
                    foreach (var wrapper in ModelWrappers)
                    {
                        ModelValidDictionary.AddRange(_modelValidator.Validate(wrapper.Model, wrapper.ModelType, wrapper.ModelName));
                    }
                }

                return ModelValidDictionary.Count == 0;
            }
        }
    }
}

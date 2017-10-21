﻿using KiraNet.GutsMvc.Implement;
using System;

namespace KiraNet.GutsMvc.Infrastructure
{
    public sealed class FormValueProviderFactory : IValueProviderFactory
    {
        public IValueProvider CreateValueProvider(ControllerContext controllerContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException(nameof(controllerContext));
            }

            try
            {
                if(controllerContext.HttpContext.Request.Form == null)
                {
                    return null;
                }

                return new FormValueProvider(controllerContext.HttpContext.Request.Form);
            }
            catch
            {
                return null;
            }
        }
    }
}

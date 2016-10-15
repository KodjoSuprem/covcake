using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace covCake
{
    public class TrimerDefaultModelBinder : DefaultModelBinder
    {

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // un truc comme sa :-/ ....
            /*
            foreach (string key in bindingContext.ValueProvider.Keys.ToArray())
            {
        
                ValueProviderResult value = bindingContext.ValueProvider[key];
                if(value.RawValue is string)
                    bindingContext.ValueProvider[key] = new ValueProviderResult(((string)value.RawValue).Trim(),value.AttemptedValue,value.Culture);
                
            }*/
            return base.BindModel(controllerContext, bindingContext);
        }

       

        protected override void SetProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor, object value)
        {
            if (bindingContext.ModelType.Namespace == "covCake.Models")
            {
                if (value is string)
                    value = ((string)value).Trim();
            }
            base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value);
         }
          
    }
}

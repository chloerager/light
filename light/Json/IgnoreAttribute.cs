using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace light
{
   [AttributeUsage(AttributeTargets.ReturnValue | AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
   public class IgnoreAttribute : Attribute
   {

   }
}

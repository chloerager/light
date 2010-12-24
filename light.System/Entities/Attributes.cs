using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace light.System.Entities
{
   /// <summary>
   ///  字段属性
   /// </summary>
   [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
   public class FieldAttribute : Attribute
   {
      public FieldAttribute(string name,bool autoCreated = false ,bool primaryKey = false)
      {
         Name = name;
         PrimaryKey = primaryKey;
         AutoCreated = autoCreated;
      }

      /// <summary>
      ///  字段的名字
      /// </summary>
      public string Name { get; set; }

      /// <summary>
      ///  是否是主键
      /// </summary>
      public bool PrimaryKey { get; set; }

      /// <summary>
      ///  自动创建
      /// </summary>
      public bool AutoCreated { get; set; }
   }

   /// <summary>
   ///  表属性
   /// </summary>
   [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
   public class TableAttribute : Attribute
   {
      public TableAttribute(string name)
      {
         Name = name;
      }

      public string Name { get; set; }
   }
}

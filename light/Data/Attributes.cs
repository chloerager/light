using System;

namespace light.Data
{
   /// <summary>
   ///  字段属性
   /// </summary>
   [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
   public class FieldAttribute : Attribute
   {
      public FieldAttribute(string name,bool autoCreated = false ,bool primaryKey = false,bool identity=false,bool allowNulls=false)
      {
         Name = name;
         PrimaryKey = primaryKey;
         AutoCreated = autoCreated;
         Identity = identity;
         AllowNulls = allowNulls;
      }

      public FieldAttribute(object defaultValue, string name, bool autoCreated = false, bool primaryKey = false, bool identity = false, bool allowNulls = false)
      {
         Name = name;
         PrimaryKey = primaryKey;
         AutoCreated = autoCreated;
         Identity = identity;
         AllowNulls = allowNulls;
         DefaultValue = defaultValue;
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

      /// <summary>
      ///  自增字段
      /// </summary>
      public bool Identity { get; set; }

      /// <summary>
      ///  允许Null值
      /// </summary>
      public bool AllowNulls { get; set; }

      /// <summary>
      ///  只是在读的时候尝试赋值
      /// </summary>
      public bool ReadOnly { get; set; }

      /// <summary>
      ///  默认值
      /// </summary>
      public object DefaultValue { get; set; }
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

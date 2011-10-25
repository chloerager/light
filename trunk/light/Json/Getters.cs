using System;
using System.Collections.Generic;

namespace light
{
    internal class Getters
    {
        public string Name;
        public JSON.GenericGetter Getter;
        public Type propertyType;
    }

    public class DatasetSchema
    {
        public IList<string> Info { get; set; }
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace light
{
   /// <summary>
   ///  在Dictionary的基础上添加了并发锁。
   /// </summary>
   /// <typeparam name="TKey"></typeparam>
   /// <typeparam name="TValue"></typeparam>
   internal class SafeDictionary<TKey, TValue>
   {
      private readonly Dictionary<TKey, TValue> _Dictionary = new Dictionary<TKey, TValue>();

      public bool TryGetValue(TKey key, out TValue value)
      {
         return _Dictionary.TryGetValue(key, out value);
      }

      public bool ContainsKey(TKey key)
      {
         return _Dictionary.ContainsKey(key);
      }

      public TValue this[TKey key]
      {
         get
         {
            return _Dictionary[key];
         }
      }
      public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
      {
         return ((ICollection<KeyValuePair<TKey, TValue>>)_Dictionary).GetEnumerator();
      }

      private readonly object _Padlock = new object();
      public void Add(TKey key, TValue value)
      {
         lock (_Padlock)
         {
            if (_Dictionary.ContainsKey(key) == false) _Dictionary.Add(key, value);
         }
      }
   }
}

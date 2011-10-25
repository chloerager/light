using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace light.Entities
{
   public interface IUserStatusEntity
   {
      /// <summary>
      ///  是否有角色扩展
      /// </summary>
      bool hasRoleEx { get; }
   }
}

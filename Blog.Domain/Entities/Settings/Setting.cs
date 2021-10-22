using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.Entities.Base;

namespace Blog.Domain.Entities.Settings
{
    public class Setting:Entity
    {
        #region Fields

        #endregion
        #region Constrcutors
        #endregion
        #region Properties

        public string Key { get; set; }
        public string Value { get; set; }
        #endregion
        #region Navigation Properties
        #endregion
    }
}

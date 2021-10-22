using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.Entities.Base;

namespace Blog.Domain.Entities.Skills
{
    public class Skill:Entity
    {
        #region Fields

        #endregion
        #region Constrcutors
        #endregion
        #region Properties
        public string Name { get; set; }
        public int Percentage { get; set; }
        public string Description { get; set; }
        #endregion
        #region Navigation Properties
        #endregion
    }
}

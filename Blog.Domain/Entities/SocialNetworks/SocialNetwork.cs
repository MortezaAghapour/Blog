using Blog.Domain.Entities.Base;

namespace Blog.Domain.Entities.SocialNetworks
{
    public class SocialNetwork:Entity
    {
        #region Fields

        #endregion
        #region Constrcutors
        #endregion
        #region Properties

        public string Name { get; set; }
        public string Address { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }

        #endregion
        #region Navigation Properties
        #endregion
    }
}

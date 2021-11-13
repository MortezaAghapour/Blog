using Blog.Domain.Entities.Base;

namespace Blog.Domain.Entities.Sliders
{
    public class Slider : Entity
    {
        #region Fields

        #endregion
        #region Constrcutors
        #endregion
        #region Properties

        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }

        #endregion
        #region Navigation Properties
        #endregion
    }
}

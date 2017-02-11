using System.Drawing;
using System.Globalization;

namespace Powell.Vehicles
{
    using static NumberStyles;

    // TODO: TBD: from a database mapping perspective, potentially refers to either Model (really, "ModelYear") or Vehicle (i.e. for those times when after market paint is applied)
    // TODO: TBD: and then, "Paint" may not be as simple as one color; i.e. for fancy pearlescent colors, two tone, etc... for academic purposes, this is fine, though...
    public class Paint : DomainObject
    {
        public virtual string Name { get; set; }

        // TODO: TBD: front end user interface should use a color picker, color wheel, etc, of some sort...
        /// <summary>
        /// Domain usage should get or set the Color.
        /// </summary>
        /// <see cref="Value"/>
        public virtual Color Color { get; set; }

        /// <summary>
        /// Front end usage should set the Value.
        /// </summary>
        /// <see cref="Color"/>
        public virtual string Value
        {
            get { return $"#{Color.R:X2}{Color.G:X2}{Color.B:X2}"; }
            set
            {
                // TODO: TBD: assumes a valid value: may put exception handling here.
                var r = byte.Parse(value.Substring(1, 2), HexNumber);
                var g = byte.Parse(value.Substring(3, 2), HexNumber);
                var b = byte.Parse(value.Substring(5, 2), HexNumber);
                Color = Color.FromArgb(r, g, b);
            }
        }

        public Paint()
        {
            Initialize();
        }

        private void Initialize()
        {
            // Starting from the built-in colors.
            Color = Color.Navy;
        }
    }
}

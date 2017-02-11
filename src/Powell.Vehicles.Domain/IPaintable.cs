namespace Powell.Vehicles
{
    // TODO: TBD: consider when mapping, Paintable objects refer to Paint... whose object of being Painted may be something of DomainObject ...
    public interface IPaintable
    {
        /// <summary>
        /// Gets or sets the Paint.
        /// </summary>
        Paint Paint { get; set; }
    }
}

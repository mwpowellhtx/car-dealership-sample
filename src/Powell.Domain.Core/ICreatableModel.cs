using System;

namespace Powell
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICreatableModel
    {
        /// <summary>
        /// Gets or sets when CreatedOn.
        /// </summary>
        DateTime CreatedOn { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class CreatableModelExtensionMethods
    {
        //TODO: this would be a prime opportunity to establish a common system clock in one place ... (or at least far fewer) ...
        //TODO: consider whether this could be an internal API ...
        /// <summary>
        /// Sets the <paramref name="model"/> value for <see cref="ICreatableModel.CreatedOn"/>
        /// to the current <see cref="DateTime.UtcNow"/>.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TModel Created<TModel>(this TModel model)
            where TModel : ICreatableModel
        {
            model.CreatedOn = DateTime.UtcNow;
            return model;
        }
    }
}

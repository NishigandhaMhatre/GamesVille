using System;

namespace Game.Models
{
    /// <summary>
    /// Base model for records that get saved
    /// </summary>
    public class BaseModel<T> : DefaultModel
    {

        // Location to the image for the item.  Will come from the server as a fully qualified URI example:  https://developer.android.com/images/robot-tiny.png
        public string ImageURI { get; set; }

        /// <summary>
        /// Updates the data based on the new data passed
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        public virtual bool Update(T newData)
        {
            return true;
        }
    }
}
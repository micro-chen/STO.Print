namespace STO.Print.Utilities.Collections
{
    /// <summary>Defines a generalized clone method that a 
    /// value type or class implements to create a 
    /// type-specific clone method to supports cloning.</summary>
    /// <typeparam name="T">The type of object to clone.</typeparam>
    public interface ICloneable<T>
    {
        /// <summary>Creates a new object that is a copy of the current instance.</summary>
        /// <returns>A new object that is a copy of this instance. </returns>
        T Clone();
    }
}


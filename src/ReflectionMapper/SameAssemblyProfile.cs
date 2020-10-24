using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ReflectionMapper
{
    /// <summary>
    /// looks for classes that implement <seealso cref="IMap{MapFrom}" within the same assembly/>
    /// </summary>
    public abstract class SameAssemblyProfile : BaseProfile
    {
        protected SameAssemblyProfile() : base(Assembly.GetCallingAssembly())
        {
        }
    }
}
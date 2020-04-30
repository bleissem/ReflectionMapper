using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ReflectionMapper
{
    public abstract class DefaultReflectionMapperProfile : ReflectionMapperProfile
    {
        protected DefaultReflectionMapperProfile() : base(Assembly.GetCallingAssembly())
        {
        }
    }
}
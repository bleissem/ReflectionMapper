using AutoMapper;
using System;

namespace ReflectionMapper
{
    public abstract class MapConfig<MapTo, MapFrom>
        where MapTo : class where MapFrom : class
    {
        public virtual Action<IMappingExpression<MapFrom, MapTo>> AlterReadMapping()
        {
            return null;
        }

        public virtual Action<IMappingExpression<MapTo, MapFrom>> AlterSaveMapping()
        {
            return null;
        }
    }
}
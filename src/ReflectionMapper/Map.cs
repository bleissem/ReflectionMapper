using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionMapper
{
    public abstract class Map<MapTo, MapFrom> : IMap<MapFrom>
        where MapTo : class
        where MapFrom : class
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
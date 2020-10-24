using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionMapper
{
    /// <summary>
    /// Maps with configuration when Reading or Saving
    /// </summary>
    /// <typeparam name="MapTo"></typeparam>
    /// <typeparam name="MapFrom"></typeparam>
    public abstract class MapConfig<MapTo, MapFrom> : IMap<MapFrom>
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
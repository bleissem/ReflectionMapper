using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionMapper
{
    public abstract class Map<MapTo, MapFrom> : MapConfig<MapTo, MapFrom> where MapTo : class
        where MapFrom : class, IMap<MapFrom>
    {
    }
}
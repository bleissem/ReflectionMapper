using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionMapper
{
    public abstract class Map<MapTo, MapFrom> : MapConfig<MapTo, MapFrom> where MapTo : class, IMap<MapFrom>
        where MapFrom : class
    {
    }
}
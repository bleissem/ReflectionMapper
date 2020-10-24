using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionMapper.Tests
{
    internal class MapTo : IMap<MapFrom>
    {
        public int Id
        {
            get;
            set;
        }
    }
}
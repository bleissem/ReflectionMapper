using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionMapper.Tests
{
    internal class MapTo2 : IMap<MapFrom2>
    {
        public string Prop { get; set; }
        public string AnotherProp { get; set; }
    }
}
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReflectionMapper.Tests
{
    internal class MappingWithConfig : MapConfig<MapTo2, MapFrom2>
    {
        public override Action<IMappingExpression<MapFrom2, MapTo2>> AlterReadMapping()
        {
            return cfg => cfg
            .ForMember(source => source.Prop, options => options.MapFrom(destination => $"A{destination.Prop}"))
            .ForMember(source => source.AnotherProp, options => options.MapFrom(destination => "42"));
        }
    }
}
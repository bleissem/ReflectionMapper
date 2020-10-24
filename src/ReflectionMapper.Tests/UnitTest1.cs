using System;
using Xunit;

namespace ReflectionMapper.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void MapIMapTo()
        {
            AutoMapper.Mapper mapper = new AutoMapper.Mapper(new AutoMapper.MapperConfiguration(config =>
            {
                config.AddProfile<TestProfile>();
            }));

            MapFrom mapFrom = new MapFrom()
            {
                Id = 3
            };

            var mapto = mapper.Map<MapTo>(mapFrom);
            Assert.Equal(3, mapto.Id);
        }

        [Fact]
        public void MapMappingWithConfigReading()
        {
            AutoMapper.Mapper mapper = new AutoMapper.Mapper(new AutoMapper.MapperConfiguration(config =>
            {
                config.AddProfile<TestProfile>();
            }));

            MapFrom2 mapFrom = new MapFrom2()
            {
                Prop = "Z"
            };

            var mapto = mapper.Map<MapTo2>(mapFrom);
            Assert.Equal("AZ", mapto.Prop);
            Assert.Equal("42", mapto.AnotherProp);
        }

        [Fact]
        public void MapMappingWithConfigSaving()
        {
            AutoMapper.Mapper mapper = new AutoMapper.Mapper(new AutoMapper.MapperConfiguration(config =>
            {
                config.AddProfile<TestProfile>();
            }));

            MapTo2 mapTo = new MapTo2()
            {
                Prop = "Y",
                AnotherProp = "X"
            };

            var mapBack = mapper.Map<MapFrom2>(mapTo);
            Assert.Equal("X", mapBack.Prop);
        }
    }
}
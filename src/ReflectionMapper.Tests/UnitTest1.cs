using System;
using Xunit;

namespace ReflectionMapper.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
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
    }
}
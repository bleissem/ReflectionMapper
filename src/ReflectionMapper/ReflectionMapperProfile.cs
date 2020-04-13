using AutoMapper;
using ReflectionMapper.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReflectionMapper
{
    public abstract class ReflectionMapperProfile : Profile
    {
        public ReflectionMapperProfile(params Assembly[] assemblies)
        {
            IEnumerable<Type> assembyTypes = assemblies.SelectMany(assembly => assembly.GetTypes());

            MethodInfo createMap = this.GetType().GetMethods().Where(method => method.Name == nameof(base.CreateMap) && method.IsGenericMethod && method.GetParameters().Length == 0).First();

            Type mapConfigType = typeof(MapConfig<,>);

            foreach (MapType mapType in GetMapTypes(assembyTypes))
            {
                Type dto = mapType.DTO;
                Type entity = mapType.Entity;
                var dtoEntityConfig = assembyTypes.Where(x => x.IsSubclassOf(mapConfigType.MakeGenericType(dto, entity))).FirstOrDefault();

                if (dtoEntityConfig != null)
                {
                    object dto2Entity = createMap.MakeGenericMethod(dto, entity).Invoke(this, null);
                    object entity2DTO = createMap.MakeGenericMethod(entity, dto).Invoke(this, null);
                    object mapToConfigDto = Activator.CreateInstance(dtoEntityConfig);

                    Type invokeMapToConfigType = typeof(MapConfig<,>).MakeGenericType(dto, entity);
                    IInvokeMapToConfig invokeMapping = (IInvokeMapToConfig)Activator.CreateInstance(invokeMapToConfigType, new [] { mapToConfigDto, dto2Entity, entity2DTO });
                    invokeMapping.Invoke();
                }
            }
        }

        protected ReflectionMapperProfile()
        {
        }

        private static IEnumerable<MapType> GetMapTypes(IEnumerable<Type> assemblyTypes)
        {
            return assemblyTypes
                .Where(s => s.GetInterfaces().Any(interfaces => interfaces.IsGenericType && interfaces.GetGenericTypeDefinition() == typeof(IMap<>)))
                .Select(dto => new MapType
                {
                    DTO = dto,
                    Entity = dto.GetInterfaces().Where(sg => sg.IsGenericType && sg.GetGenericTypeDefinition() == typeof(IMap<>))
                .Select(classArgutments => classArgutments.GenericTypeArguments.First()).First()
                });
        }
    }
}
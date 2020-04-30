﻿using AutoMapper;
using ReflectionMapper.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReflectionMapper
{
    public abstract class ReflectionMapperProfile : Profile
    {
        protected ReflectionMapperProfile(params Assembly[] assemblies)
        {
            IEnumerable<Type> assembyTypes = assemblies.SelectMany(assembly => assembly.GetTypes());

            foreach (MapType mapType in GetMapTypes(assembyTypes))
            {
                Type dto = mapType.DTO;
                Type entity = mapType.Entity;
                (object dto2Entity, object entity2DTO) = Mapping(dto, entity);
                Config(assembyTypes, dto2Entity, entity2DTO, dto, entity);
            }
        }

        private (object dto2Entity, object entity2DTO) Mapping(Type dto, Type entity)
        {
            MethodInfo createMap = this.GetType().GetMethods().Where(method => method.Name == nameof(base.CreateMap) && method.IsGenericMethod && method.GetParameters().Length == 0).First();

            object dto2Entity = createMap.MakeGenericMethod(dto, entity).Invoke(this, null);
            object entity2DTO = createMap.MakeGenericMethod(entity, dto).Invoke(this, null);
            return (dto2Entity, entity2DTO);
        }

        private void Config(IEnumerable<Type> assembyTypes, object dto2Entity, object entity2DTO, Type dto, Type entity)
        {
            Type mapConfigType = typeof(MapConfig<,>);
            Type dtoEntityConfig = assembyTypes.Where(x => x.IsSubclassOf(mapConfigType.MakeGenericType(dto, entity))).FirstOrDefault();
            if (dtoEntityConfig != null)
            {
                object mapToConfig = Activator.CreateInstance(dtoEntityConfig);

                Type invokeMapToConfigType = typeof(InvokeMapToConfig<,>).MakeGenericType(dto, entity);
                IInvokeMapToConfig invokeMapping = (IInvokeMapToConfig)Activator.CreateInstance(invokeMapToConfigType, new[] { mapToConfig,  dto2Entity, entity2DTO });
                invokeMapping.Invoke();
            }
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
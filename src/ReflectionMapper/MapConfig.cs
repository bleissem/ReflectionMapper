using AutoMapper;
using System;

namespace ReflectionMapper
{
    public abstract class MapConfig<TDTO, TEntity>
        where TDTO : class where TEntity : class
    {
        public virtual Action<IMappingExpression<TEntity, TDTO>> AlterReadMapping { get { return null; } }

        public virtual Action<IMappingExpression<TDTO, TEntity>> AlterSaveMapping { get { return null; } }
    }
}
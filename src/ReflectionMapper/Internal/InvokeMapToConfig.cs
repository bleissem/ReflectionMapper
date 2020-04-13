using AutoMapper;

namespace ReflectionMapper.Internal
{
    internal class InvokeMapToConfig<TDTO, TEntity> : IInvokeMapToConfig
        where TEntity : class
        where TDTO : class, IMap<TEntity>
    {
        private readonly MapConfig<TDTO, TEntity> _mapToConfig;
        private readonly IMappingExpression<TDTO, TEntity> _dto2Entity;
        private readonly IMappingExpression<TEntity, TDTO> _entity2DTO;

        public InvokeMapToConfig(MapConfig<TDTO, TEntity> mapConfig, IMappingExpression<TDTO, TEntity> dto2Entity, IMappingExpression<TEntity, TDTO> entity2DTO)
        {
            _dto2Entity = dto2Entity;
            _entity2DTO = entity2DTO;
            _mapToConfig = mapConfig;
        }

        public void Invoke()
        {
            _mapToConfig?.AlterReadMapping?.Invoke(_entity2DTO);
            _mapToConfig?.AlterSaveMapping?.Invoke(_dto2Entity);
        }
    }
}
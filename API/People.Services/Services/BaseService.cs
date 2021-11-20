using AutoMapper;
using People.Domain.DTOs.Requests;
using People.Domain.Entities;
using People.Domain.Interfaces;
using People.Domain.Interfaces.Repositories;
using People.Domain.Interfaces.Services;
using People.Domain.Mappings.DTOEntities;
using System.Linq.Expressions;

namespace People.Services.Services
{
    public class BaseService<K, T, Z> : IBaseService<K, T>
        where K : IComparable, IConvertible, IEquatable<K>
        where T : BaseRequestDTO<K>
        where Z : BaseEntity<K>
    {
        private readonly IBaseRepository<K, Z> baseRepository;
        private readonly ILogService logService;
        private readonly IMapper mapper;
        private readonly IApiNotification apiNotification;
        public BaseService(IBaseRepository<K, Z> baseRepository, ILogService logService, IApiNotification apiNotification)
        {
            this.baseRepository = baseRepository;
            this.logService = logService;
            this.apiNotification = apiNotification;
            mapper = DTOEntityMap.MapDTOEntity();
        }

        private Z ToEntity(T dto)
        {
            var entity = mapper.Map<Z>(dto);
            if (entity == null)
            {
                logService.LogDebug("[BaseService] Mapper dto to entity is null");
                throw new ArgumentException("Mapper dto to entity is null", nameof(dto));
            }
            return entity!;
        }

        private T ToDto(Z entity)
        {
            var dto = mapper.Map<T>(entity);
            if (dto == null)
            {
                logService.LogDebug("[BaseService] Mapper entity to dto is null");
                throw new ArgumentException("Mapper entity to dto is null", nameof(dto));
            }
            return dto!;
        }

        private IList<T> ToDtoList(IList<Z> entity)
        {
            var dtos = mapper.Map<IList<T>>(entity);
            if (dtos == null)
            {
                logService.LogDebug("[BaseService] Mapper list entity to list dto is null");
                throw new ArgumentException("Mapper list entity to list dto is null", nameof(dtos));
            }
            return dtos!;
        }

        public async Task<T> Create(T dto)
        {
            var entity = ToEntity(dto);

            if (entity == null)
            {
                apiNotification.StatusCode = 400;
                apiNotification.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(dto), "Dados inválidos"));
                return null;
            }

            if (entity.Id.GetType() == typeof(string))
            {
                var propId = entity.GetType().GetProperty(nameof(entity.Id));
                propId?.SetValue(entity, Guid.NewGuid().ToString());
            }
            else
            {
                apiNotification.StatusCode = 400;
                throw new NotImplementedException("Definition of Id type to generate value not implemented.");
            }

            //TODO RECUPERAR USUARIO ATUAL LOGADO
            var propCreatedUserId = entity.GetType().GetProperty(nameof(entity.CreatedUserId));
            propCreatedUserId?.SetValue(entity, "");

            var propUpdatedUserId = entity.GetType().GetProperty(nameof(entity.UpdatedUserId));
            propUpdatedUserId?.SetValue(entity, "");

            var propDateCreated = entity.GetType().GetProperty(nameof(entity.DateCreated));
            propDateCreated?.SetValue(entity, DateTime.Now);

            var propDateUpdated = entity.GetType().GetProperty(nameof(entity.DateUpdated));
            propDateUpdated?.SetValue(entity, DateTime.Now);

            var response = await baseRepository.Create(entity);
            apiNotification.StatusCode = response != null ? 201 : 422;
            return ToDto(response);
        }

        public async Task<bool> Delete(K id)
        {
            var success = await baseRepository.Delete(id);
            apiNotification.StatusCode = success ? 200 : 404;
            return success;
        }

        public async Task<IList<T>> FindAll()
        {
            var response = await baseRepository.FindAll();
            apiNotification.StatusCode = 200;
            return ToDtoList(response);
        }

        public async Task<IList<T>> FindAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Expression<Func<T, bool>>?, Expression<Func<Z, bool>>?>();
                cfg.CreateMap<Func<IQueryable<T>, IOrderedQueryable<T>>?, Func<IQueryable<Z>, IOrderedQueryable<Z>>?>();
            });

            var mapLambda = config.CreateMapper();
            var filterEntity = mapLambda.Map<Expression<Func<Z, bool>>?>(filter);
            var orderByEntity = mapLambda.Map<Func<IQueryable<Z>, IOrderedQueryable<Z>>?>(orderBy);

            var response = await baseRepository.FindAll(filterEntity, orderByEntity);
            apiNotification.StatusCode = 200;
            return ToDtoList(response);
        }

        public async Task<T> FindById(K id)
        {
            var response = await baseRepository.FindById(id);
            apiNotification.StatusCode = (response == null ? 404 : 200);
            return ToDto(response);
        }

        public async Task<T> Update(T dto)
        {
            var entity = ToEntity(dto);

            if (entity == null)
            {
                apiNotification.StatusCode = 400;
                apiNotification.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(dto), "Dados inválidos"));
                return null;
            }
            
            //TODO RECUPERAR USUARIO ATUAL LOGADO
            var propUpdatedUserId = entity.GetType().GetProperty(nameof(entity.UpdatedUserId));
            propUpdatedUserId?.SetValue(entity, "");

            var propDateUpdated = entity.GetType().GetProperty(nameof(entity.DateUpdated));
            propDateUpdated?.SetValue(entity, DateTime.Now);

            var response = await baseRepository.Update(entity);
            apiNotification.StatusCode = response != null ? 200 : 422;
            return ToDto(response);
        }
    }
}

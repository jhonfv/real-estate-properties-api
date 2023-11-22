using AutoMapper;
using properties.Application.Common;
using properties.Application.DTOs;
using properties.Application.Interfaces;
using properties.Domain.Entities;
using properties.Domain.Interfaces;

namespace properties.Application.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _repository;
        private readonly IMapper _mapper;

        public OwnerService(IOwnerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<OwnerDTO>> CreateAsync(OwnerDTO ownerDTO)
        {
            var owner = _mapper.Map<Owner>(ownerDTO);
            var newOwner =await _repository.CreateAsync(owner);
            if(newOwner != null)
            {
                var ownerResponse = _mapper.Map<OwnerDTO>(newOwner);
                return new Result<OwnerDTO>().Success(ownerResponse);
            }
            else
            {
                return new Result<OwnerDTO>().Failed(new List<string> { "Owner not insert"}, code:400);
            }
        }

        public async Task<Result<IEnumerable<OwnerDTO>>> getAllAsync()
        {
            var owners =await _repository.GetAllAsync();
            if(owners == null || owners.Count() ==0)
            {
                return new Result<IEnumerable<OwnerDTO>>().Failed(new List<string>{ "No content avalible" }, code:204);
            }
            else
            {
                var reponse = _mapper.Map<IEnumerable<OwnerDTO>>(owners);
                return new Result<IEnumerable<OwnerDTO>>().Success(reponse);
            }
        }

    }
}

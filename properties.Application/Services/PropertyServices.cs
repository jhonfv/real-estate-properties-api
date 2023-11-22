using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using properties.Application.Common;
using properties.Application.DTOs.Properties;
using properties.Application.Interfaces;
using properties.Domain.Entities;
using properties.Domain.Filters;
using properties.Domain.Interfaces;

namespace properties.Application.Services
{
    public class PropertyServices : IPropertyServices
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IFakeCDNExternalService _fakeCDNExternalService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public PropertyServices(IPropertyRepository propertyRepository, IMapper mapper, IFakeCDNExternalService fakeCDNExternalService, IConfiguration configuration)
        {
            _mapper = mapper;
            _propertyRepository = propertyRepository;
            _fakeCDNExternalService = fakeCDNExternalService;
            _configuration = configuration;
        }
        public async Task<Result<PropertyDTO>> createAsync(CreatePropertyDTO createPropertyDTO)
        {
            var property = _mapper.Map<Property>(createPropertyDTO);
            var newProperty =await _propertyRepository.CreateAsync(property);
            if(newProperty != null)
            {
                var internalCode = $"P{newProperty.IdProperty.ToString().PadLeft(5,'0')}";
                newProperty.CodeInternal = internalCode ;
            }
            else
            {
                return new Result<PropertyDTO>().Failed(new List<string> { "Not insert property" }, code: 400);
            }

            //Actualzar internlCode
            var propertyUpdate =await _propertyRepository.UpdateAsync(newProperty);
            
            if(propertyUpdate != null)
            {
                var propertyResponse = _mapper.Map<PropertyDTO>(propertyUpdate);
                return new Result<PropertyDTO>().Success(propertyResponse);
            }
            else
            {
                return new Result<PropertyDTO>().Failed(new List<string> { $"Not udpate codeInternal for property {newProperty.IdProperty}" }, code: 400);
            }
            

        }
        
        public async Task<Result<PropertyDTO>> changePriceAsync(double price, int idProperty)
        {
            var propertyChange = new Property()
            {
                IdProperty = idProperty,
                Price = price,
            };
            var responseChange = await _propertyRepository.ChangePriceAsync(propertyChange);
            if (responseChange != null)
            {
                var propertyDto = _mapper.Map<PropertyDTO>(responseChange);
                return new Result<PropertyDTO>().Success(propertyDto);
            }
            else
            {
                return new Result<PropertyDTO>().Failed(new List<string> { "Price not change"});
            }
        }

        public async Task<Result<PropertyDTO>> UpdateAsync(PropertyDTO propertyDTO)
        {
            var propertyUpdate = _mapper.Map<Property>(propertyDTO);
            var responseUpdate =await _propertyRepository.UpdateAsync(propertyUpdate);
            if(responseUpdate != null)
            {
                var propertyResponse = _mapper.Map<PropertyDTO>(propertyUpdate);
                return new Result<PropertyDTO>().Success(propertyResponse);
            }
            else
            {
                return new Result<PropertyDTO>().Failed(new List<string>() {$"Property ${propertyDTO.IdProperty} not update"});
            }
        }

        public async Task<Result<PropertyImageDTO>> addImageAsync(IFormFile image, int idProperty)
        {
            var pathImage = await _fakeCDNExternalService.saveImageAsync(image);
            if(pathImage != null)
            {
                var propertyImage= new PropertyImage()
                {
                    Enabled= true,
                    FilePath=pathImage,
                    IdProperty=idProperty
                };

                propertyImage =await _propertyRepository.AddImageAsync(propertyImage);
                if(propertyImage != null)
                {
                    var newImageProperty = _mapper.Map<PropertyImageDTO>(propertyImage);
                    newImageProperty.FilePath =$"{_configuration.GetSection("FakeCDN").Value}/{newImageProperty.FilePath}";
                    return new Result<PropertyImageDTO>().Success(newImageProperty);
                }
                else
                {
                    return new Result<PropertyImageDTO>().Failed(new List<string>() { $"Image no add" });
                }
            }
            else
            {
                return new Result<PropertyImageDTO>().Failed(new List<string>() { $"Image no Upload" });
            }
        }

        public async Task<Result<IEnumerable<PropertyDTO>>> getByFiltersAsync(FilterPropertyDTO filterPropertyDTO)
        {
            var filters = _mapper.Map<FilterProperty>(filterPropertyDTO);

            var propertyList = await _propertyRepository.getByFiltersAsync(filters);

            if (propertyList != null)
            {
                var propertyResponse = _mapper.Map<IEnumerable<PropertyDTO>>(propertyList);
                return new Result<IEnumerable<PropertyDTO>>().Success(propertyResponse);
            }
            else
            {
                return new Result<IEnumerable<PropertyDTO>>().Failed(new List<string>() { $"Error get properties" });
            }
        }
    }
}

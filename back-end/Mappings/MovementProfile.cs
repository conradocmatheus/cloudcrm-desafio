using AutoMapper;
using back_end.DTOs.MovementDTOs;
using back_end.Models;

namespace back_end.Mappings;

public class MovementProfile : Profile
{
    public MovementProfile()
    {
        // Mapeamento de CreateMovementDto para Movement
        CreateMap<CreateMovementDto, Movement>()
            .ForMember(dest => dest.MovementProducts, opt => opt.Ignore());

        // Mapeamento de Movement para MovementDto
        CreateMap<Movement, MovementDto>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src =>
                src.MovementProducts.Select(mp => new MovementProductDto
                {
                    Id = mp.ProductId,
                    Name = mp.Product.Name,
                    Quantity = mp.Quantity,
                    Price = mp.Product.Price
                }).ToList()));

        // Mapeamento de MovementProduct para MovementProductDto
        CreateMap<MovementProduct, MovementProductDto>();

        // Mapping pro GetAllMovements
        CreateMap<Movement, GetAllMovementsDto>()
            .ForMember(dest => dest.MovementProductIds,
                opt => opt.MapFrom(src => src.MovementProducts.Select(mp => mp.ProductId)))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
        
        CreateMap<Movement, GetAllMovementsWithUserInfoDto>()
            .ForMember(dest => dest.MovementProductIds,
                opt => opt.MapFrom(src => src.MovementProducts.Select(mp => mp.ProductId)))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
        
        // Mapping pro UserInfoDto
        CreateMap<User, UserInfoDto>();
        
        // Mapping pro ExportMovementDto
        CreateMap<Movement, ExportMovementDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src =>
                src.MovementProducts.Select(mp => new ExportMovementDto.ProductExportDto
                {
                    ProductId = mp.Product.Id,
                    ProductName = mp.Product.Name
                }).ToList()));
    }
}
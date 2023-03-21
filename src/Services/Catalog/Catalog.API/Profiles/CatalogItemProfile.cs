using AutoMapper;
using Catalog.API.Models;
using Catalog.API.ViewModels;

namespace Catalog.API.Profiles;

public class CatalogItemProfile : Profile
{
    public CatalogItemProfile()
    {
        CreateMap<CatalogItem, ItemViewModel>();
    }
}
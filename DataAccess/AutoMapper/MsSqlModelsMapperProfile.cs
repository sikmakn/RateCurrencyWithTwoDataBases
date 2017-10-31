using AutoMapper;
using DataAccess.DataBase;
using DataAccess.ModelsForServices;

namespace DataAccess.AutoMapper
{
    public class MsSqlModelsMapperProfile: Profile
    {
        public MsSqlModelsMapperProfile()
        {
            MsSqlToServiceModels();
            ServiceModelToMsSql();
        }

        private void MsSqlToServiceModels()
        {
            CreateMap<City, CityServiceModel>()
                .ForMember(sM => sM.Id, opt => opt.MapFrom(c => c.Id.ToString()));

            CreateMap<Currency, CurrencyServiceModel>()
                .ForMember(sM => sM.Id, opt => opt.MapFrom(c => c.Id.ToString()));

            CreateMap<BankDepartment, BankDepartmentServiceModel>()
                .ForMember(sM => sM.Id, opt => opt.MapFrom(bD => bD.Id.ToString()))
                .ForMember(sM => sM.City, opt => opt.MapFrom(c => Mapper.Map<City, CityServiceModel>(c.City)));

            CreateMap<CurrencyRateByTime, CurrencyRateByTimeServiceModel>()
                .ForMember(sM => sM.Id, opt => opt.MapFrom(cR => cR.Id.ToString()))
                .ForMember(sM => sM.Currency,
                    opt => opt.MapFrom(c => Mapper.Map<Currency, CurrencyServiceModel>(c.Currency)))
                .ForMember(sM => sM.BankDepartmentId, opt => opt.MapFrom(c => c.BankDepartmentId.ToString()));
        }

        private void ServiceModelToMsSql()
        {
            CreateMap<CityServiceModel, City>()
                .ForMember(c => c.Id, opt => opt.MapFrom(sM => sM.Id == null ? 0 : int.Parse(sM.Id)));

            CreateMap<CurrencyServiceModel, Currency>()
                .ForMember(c => c.Id, opt => opt.MapFrom(sM => sM.Id == null ? 0 : int.Parse(sM.Id)));

            CreateMap<BankDepartmentServiceModel, BankDepartment>()
                .ForMember(b => b.Id, opt => opt.MapFrom(sM => sM.Id == null ? 0 : int.Parse(sM.Id)))
                .ForMember(b => b.City, opt => opt.MapFrom(sM => Mapper.Map<CityServiceModel, City>(sM.City)));

            CreateMap<CurrencyRateByTimeServiceModel, CurrencyRateByTime>()
                .ForMember(c => c.Id, opt => opt.MapFrom(sM => sM.Id == null ? 0 : int.Parse(sM.Id)))
                .ForMember(c => c.BankDepartmentId,
                    opt => opt.MapFrom(sM => sM.BankDepartmentId == null ? 0 : int.Parse(sM.BankDepartmentId)))
                .ForMember(c => c.Currency,
                    opt => opt.MapFrom(sM => Mapper.Map<CurrencyServiceModel, Currency>(sM.Currency)));
        }
    }
}

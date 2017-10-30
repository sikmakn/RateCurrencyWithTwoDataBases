using System.Collections.Generic;
using AutoMapper;
using DataAccess.DataBase;
using DataAccess.ModelsForServices;

namespace DataAccess.AutoMapper
{
    public class MsSqlModelsMapperProfile: Profile
    {
        public MsSqlModelsMapperProfile()
        {
            CreateMap<City, CityServiceModel>()
                .ForMember(cSM => cSM.Id, opt => opt.MapFrom(c => c.Id.ToString()));

            CreateMap<Currency, CurrencyServiceModel>()
                .ForMember(cSM => cSM.Id, opt => opt.MapFrom(c => c.Id.ToString()));

            //CreateMap<CurrencyRateByTime, CurrencyRateByTimeServiceModel>()
            //    .ForMember(sM => sM.Id, opt => opt.MapFrom(cR => cR.Id.ToString()))
            //    .ForMember(sM => sM.Currency, opt => opt.MapFrom(cR => cR.CurrencyId.ToString()))
            //    .ForMember(sM => sM.BankDepartment, opt => opt.MapFrom(cR => cR.BankDepartmentId.ToString()))
                //.ForMember(sM => sM.BankDepartment,
                //    opt => opt.MapFrom(cR =>
                //        Mapper.Map<BankDepartment, BankDepartmentServiceModel>(cR.BankDepartment)));

            //CreateMap<BankDepartment, BankDepartmentServiceModel>()
            //    .ForMember(sM => sM.Id, opt => opt.MapFrom(bD => bD.Id.ToString()))
            //    .ForMember(sM => sM.BankId, opt => opt.MapFrom(bD => bD.CityId.ToString()))
            //    .ForMember(sM => sM.BankId, opt => opt.MapFrom(bD => bD.BankId.ToString()))
            //    .ForMember(sM => sM.CurrencyRateByTime,
            //        opt => opt.MapFrom(bd =>
            //            Mapper.Map<ICollection<CurrencyRateByTime>, ICollection<CurrencyRateByTimeServiceModel>>(
            //                bd.CurrencyRateByTime)));

            //CreateMap<Bank, BankServiceModel>()
            //    .ForMember(sM => sM.Id, opt => opt.MapFrom(b => b.Id.ToString()))
            //    .ForMember(sM => sM.BankDepartment,
            //        opt => opt.MapFrom(b =>
            //            Mapper.Map<ICollection<BankDepartment>, ICollection<BankDepartmentServiceModel>>(
            //                b.BankDepartment)));
        }
    }
}

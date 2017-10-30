using AutoMapper;

namespace DataAccess.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(x => x.AddProfiles(new[]
            {
                typeof(MsSqlModelsMapperProfile),
                typeof(MongoModelsMapperProfile)
            }));
        }
    }
}

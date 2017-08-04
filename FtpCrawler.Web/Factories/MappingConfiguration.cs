namespace FtpCrawler.Web.Factories
{
    public class MappingConfiguration
    {
        public static AutoMapper.MapperConfiguration CreateConfiguration()
        {
            AutoMapper.MapperConfiguration config = new AutoMapper.MapperConfiguration(cfg =>
            {
                //convention to be used here is first the db model, then the DTS model

                cfg.CreateMap<FtpCrawler.Web.Models.FtpServerModel, FtpCrawler.Data.Models.FtpServer>().ForMember(x => x.Id, opts => opts.MapFrom(y => y.EntryId)).ReverseMap();
            });

            return config;
        }
    }
}
namespace FtpCrawler.Web.Factories
{
    public class MappingConfiguration
    {
        public static AutoMapper.MapperConfiguration CreateConfiguration()
        {
            AutoMapper.MapperConfiguration config = new AutoMapper.MapperConfiguration(cfg =>
            {
                //convention to be used here is first the db model, then the DTS model

                cfg.CreateMap<FtpCrawler.Data.Models.FtpServer, FtpCrawler.Web.Models.FtpServerModel>().ForMember(x => x.Online, opt => opt.Ignore()).ForMember(x => x.EntryId, opts => opts.MapFrom(y => y.Id)).ReverseMap();
            });

            return config;
        }
    }
}
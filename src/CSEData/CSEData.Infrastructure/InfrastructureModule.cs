using Autofac;
using CSEData.Application.Features.Services;
using CSEData.Infrastructure.Features.Services;

namespace CSEData.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<CompanyService>()
                .As<ICompanyService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PriceService>()
                .As<IPriceService>()
                .InstancePerLifetimeScope();
        }
    }
}

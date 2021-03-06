using Model.Concrete;
using Model.Repository;
using Model.Service;
using Web.Models;
using Web.Service;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Web.App_Start.NinjectWebCommon), "Stop")]

namespace Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ApplicationDbContext>().To<ApplicationDbContext>();
            kernel.Bind<EFDbContext>().To<EFDbContext>();

            kernel.Bind<IProductRepository>().To<EFProductRepository>();
            kernel.Bind<ProductService>().To<ProductService>();

            kernel.Bind<IPostRepository>().To<EFPostRepository>();
            kernel.Bind<PostService>().To<PostService>();

            kernel.Bind<ITagRepository>().To<EFTagRepository>();
            kernel.Bind<TagService>().To<TagService>();

            kernel.Bind<IOrderRepository>().To<EFOrderRepository>();
            kernel.Bind<OrderService>().To<OrderService>();

            kernel.Bind<ShoppingCartService>().To<ShoppingCartService>();
            kernel.Bind<PriceCalculator>().To<PriceCalculator>();

            kernel.Bind<IPaymentRepository>().To<EFPaymentRepository>();

            kernel.Bind<ISelectedProductRepository>().To< EFSelectedProductRepository>();

        }
    }
}

﻿using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace GoSmokeBackend.Dependencies
{
    public class CastleInstaller
    {
        public static IWindsorContainer Install()
        {
            var container = new WindsorContainer().Install(FromAssembly.This());
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
            return container;
        }
    }
}

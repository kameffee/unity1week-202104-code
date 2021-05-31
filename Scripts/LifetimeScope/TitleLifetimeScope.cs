using kameffee.unity1week202104.Domain;
using kameffee.unity1week202104.Presenter;
using kameffee.unity1week202104.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace kameffee.unity1week202104.LifetimeScope
{
    public class TitleLifetimeScope : VContainer.Unity.LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<TitleModel>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<TitleView>().As<ITitleView>();
            builder.RegisterEntryPoint<TitlePresenter>();
        }
    }
}
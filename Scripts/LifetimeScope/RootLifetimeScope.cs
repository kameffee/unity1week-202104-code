using kameffee.unity1week202104.Domain;
using kameffee.unity1week202104.Presenter;
using kameffee.unity1week202104.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace kameffee.unity1week202104.LifetimeScope
{
    public class RootLifetimeScope : VContainer.Unity.LifetimeScope
    {
        [SerializeField]
        private FadeCanvas fadeCanvasPrefab;
        
        protected override void Configure(IContainerBuilder builder)
        {
            // フェード
            builder.RegisterComponentInNewPrefab(fadeCanvasPrefab, Lifetime.Singleton).As<IFadeView>();
            builder.Register<FadeModel>(Lifetime.Singleton);
            builder.RegisterEntryPoint<FadePresenter>(Lifetime.Singleton);
        }
    }
}
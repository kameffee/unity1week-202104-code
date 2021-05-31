using System.Collections;
using System.Collections.Generic;
using System.Net;
using Cinemachine;
using kameffee.unity1week202104.Domain;
using kameffee.unity1week202104.Presenter;
using kameffee.unity1week202104.UseCase;
using kameffee.unity1week202104.View;
using UnityEditor;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace kameffee.unity1week202104.LifetimeScope
{
    public class MainLifetimeScope : VContainer.Unity.LifetimeScope
    {
        [Header("Camera")]
        [SerializeField]
        private Camera mainCamera;

        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;

        [Header("Prefab")]
        [SerializeField]
        private PickupPopupView pickupPopupViewPrefab;

        [SerializeField]
        private GameOverCanvas gameOverCanvasPrefab;

        [SerializeField]
        private PlayableList playableList;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance<Camera>(mainCamera);
            builder.RegisterInstance<CinemachineVirtualCamera>(virtualCamera);

            builder.RegisterInstance<PlayableList>(playableList);

            // Cinema Scope
            builder.RegisterComponentInHierarchy<CinemaScopeView>().AsImplementedInterfaces();
            builder.Register<CinemaScopeModel>(Lifetime.Scoped);
            builder.RegisterEntryPoint<CinemaScopePresenter>();

            // Input
            builder.Register<PlayerInput>(Lifetime.Scoped).As<IPlayerInput>();

            // Player
            builder.RegisterComponentInHierarchy<PlayerView>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<PlayerAnimation>();
            builder.Register<PlayerModel>(Lifetime.Scoped);
            builder.Register<ItemPouch>(Lifetime.Scoped);
            builder.RegisterEntryPoint<PlayerPresenter>(Lifetime.Scoped);

            // Creature
            builder.Register<CreatureModel>(Lifetime.Transient);
            builder.RegisterEntryPoint<FieldCreatureContainer>(Lifetime.Scoped)
                .WithParameter((IReadOnlyList<CreaturePresenter>)FindObjectsOfType<CreaturePresenter>());

            // Status
            builder.RegisterComponentInHierarchy<AirBombStatusView>().AsImplementedInterfaces();
            builder.RegisterEntryPoint<PlayerStatusPresenter>();
            builder.RegisterComponentInHierarchy<ItemStatusView>().As<IItemStatusView>();
            builder.RegisterEntryPoint<ItemStatusPresenter>(Lifetime.Singleton);

            // Air
            builder.Register<AirBombeModel>(Lifetime.Scoped).WithParameter(AirBombeModel.MaxAir);

            // Item
            builder.Register<ItemModel>(Lifetime.Transient);
            // builder.RegisterComponentInHierarchy<ItemView>().AsImplementedInterfaces();
            // builder.RegisterEntryPoint<ItemPresenter>(Lifetime.Transient);

            builder.Register<FieldItemContainer>(Lifetime.Scoped);

            // Goal
            builder.RegisterComponentInHierarchy<GoalPoint>().As<IGoalPoint>();

            // Pickup popup
            builder.RegisterFactory<Vector3, IPickupPopupView>(
                conteiner => { return position => Instantiate(pickupPopupViewPrefab, position, Quaternion.identity); },
                Lifetime.Scoped);
            builder.Register<PickupPopupPresenter>(Lifetime.Transient);
            builder.Register<PickupPopup>(Lifetime.Transient);

            // ゲームオーバー
            builder.RegisterInstance<GameOverCanvas>(Instantiate(gameOverCanvasPrefab));
            builder.Register<GameOverPerformer>(Lifetime.Scoped);
            builder.Register<RetryUseCase>(Lifetime.Scoped);

            // ベースキャンプ
            builder.Register<BaseCampModel>(Lifetime.Transient);

            // Game cycle
            builder.RegisterEntryPoint<GameCycle>();
        }
    }
}
using CodeBase.Infrastructure.Services.TextureProvider;
using CodeBase.Infrastructure.Services.TextureProvider.TextureLoader;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class LoadTexturesState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly TextureLoader _textureLoader;
        private readonly ITexturesProvider _texturesProvider;

        public LoadTexturesState(
            GameStateMachine stateMachine,
            TextureLoader textureLoader,
            ITexturesProvider texturesProvider)
        {
            _stateMachine = stateMachine;
            _textureLoader = textureLoader;
            _texturesProvider = texturesProvider;
        }

        public void Enter()
        {
            _textureLoader.Load(onLoaded: OnLoaded);
        }

        public void Exit()
        {
            
        }

        private void OnLoaded()
        {
            _texturesProvider.Textures = _textureLoader.Textures;
            _stateMachine.Enter<LoadLevelState, string>(SceneNames.Main);
        }
    }
}
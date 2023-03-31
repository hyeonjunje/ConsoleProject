using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProject.Scene
{
    enum EScene
    {
        None,
        Intro,
        Info,
        Credit,
        Main,
        Ending,
    }


    class SceneManager : Singleton<SceneManager>
    {
        private Dictionary<EScene, BaseScene> _sceneDictionary = new Dictionary<EScene, BaseScene>();
        public BaseScene CurrentScene { get; private set; }

        public SceneManager()
        {
            _sceneDictionary[EScene.Intro] = new IntroScene();
            _sceneDictionary[EScene.Main] = new MainScene();
            _sceneDictionary[EScene.Info] = new InfoScene();
            _sceneDictionary[EScene.Credit] = new CreditScene();
            _sceneDictionary[EScene.Ending] = new EndingScene();
        }

        // 씬을 바꾸면 이전 씬 End()함수 하고 씬을 바꾸고 바꾼 Scene에 start()함수를 실행
        public void ChangeScene(EScene scene)
        {
            // 키값이 없으면 return
            if(!_sceneDictionary.ContainsKey(scene))
            {
                return;
            }

            // 같은 씬으로 이동하면 return
            if (CurrentScene == _sceneDictionary[scene])
            {
                return;
            }

            // 이전 씬이 null이 아닐때만 이전 씬의 End() 실행
            if (CurrentScene != null)
            {
                // End 함수는 씬 초기화 작업
                CurrentScene.End();
            }

            CurrentScene = _sceneDictionary[scene];

            // start 함수는 씬은 각 씬의 화면을 그려줌
            CurrentScene.Start();
        }


        public BaseScene GetScene(EScene scene)
        {
            return _sceneDictionary[scene];
        }
    }
}

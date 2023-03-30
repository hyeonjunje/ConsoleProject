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


    class SceneManager
    {
        private static SceneManager _instance = null;
        public static SceneManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new SceneManager();
                }

                return _instance;
            }
        }

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
                CurrentScene.End();
            }

            CurrentScene = _sceneDictionary[scene];

            CurrentScene.Start();
        }


        public BaseScene GetScene(EScene scene)
        {
            return _sceneDictionary[scene];
        }
    }
}

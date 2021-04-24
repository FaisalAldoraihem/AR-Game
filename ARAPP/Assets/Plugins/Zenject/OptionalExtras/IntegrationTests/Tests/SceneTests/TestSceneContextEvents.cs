using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Zenject.Tests
{

    public class WaitForSceneLoaded : CustomYieldInstruction
    {
        readonly string sceneName;
        readonly float timeout;
        readonly float startTime;
        bool timedOut;

        public bool TimedOut => timedOut;

        public override bool keepWaiting
        {
            get
            {
                var scene = SceneManager.GetSceneByName(sceneName);
                var sceneLoaded = scene.IsValid() && scene.isLoaded;

                if (Time.realtimeSinceStartup - startTime >= timeout)
                {
                    timedOut = true;
                }

                return !sceneLoaded && !timedOut;
            }
        }

        public WaitForSceneLoaded(string newSceneName, float newTimeout = 10)
        {
            sceneName = newSceneName;
            timeout = newTimeout;
            startTime = Time.realtimeSinceStartup;
        }
    }
    public class TestSceneContextEvents : SceneTestFixture
    {
        [UnityTest]
        public IEnumerator TestMainScene()
        {
            var waitForScene = new WaitForSceneLoaded("Main");
            yield return waitForScene;
            Assert.IsFalse(waitForScene.TimedOut, "Scene " + "Main" + " was never loaded");
        }
        public IEnumerator TestMainMenueScene()
        {
            var waitForScene = new WaitForSceneLoaded("MainMenue");
            yield return waitForScene;
            Assert.IsFalse(waitForScene.TimedOut, "Scene " + "MainMenue" + " was never loaded");
        }
        public IEnumerator TestARScene()
        {
            var waitForScene = new WaitForSceneLoaded("AR");
            yield return waitForScene;
            Assert.IsFalse(waitForScene.TimedOut, "Scene " + "AR" + " was never loaded");
        }
        public IEnumerator TestMultipleChoiceScene()
        {
            var waitForScene = new WaitForSceneLoaded("MultipleChoice");
            yield return waitForScene;
            Assert.IsFalse(waitForScene.TimedOut, "Scene " + "MultipleChoice" + " was never loaded");
        }
    }


}

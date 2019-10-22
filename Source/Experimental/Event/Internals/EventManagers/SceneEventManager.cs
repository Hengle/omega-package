using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Omega.Tools.Experimental.Event.Internals.EventManagers
{
    internal sealed class SceneEventManager<TEvent> : UniversalEventManager<TEvent>
    {
        public SceneEventManager()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                EditorSceneManager.activeSceneChangedInEditMode += ActiveSceneChangeHandler;
            else
#endif
                SceneManager.activeSceneChanged += ActiveSceneChangeHandler;
        }

        private void ActiveSceneChangeHandler(Scene previousScene, Scene newScene)
        {
            GetOutFromEventManagerDispatcher();
            
#if UNITY_EDITOR
            if (!Application.isPlaying)
                EditorSceneManager.activeSceneChangedInEditMode -= ActiveSceneChangeHandler;
            else
#endif
                SceneManager.activeSceneChanged -= ActiveSceneChangeHandler;
        }

        private void GetOutFromEventManagerDispatcher()
        {
            if (EventManagerDispatcher<TEvent>.GetEventManagerHardInternal() == this)
                EventManagerDispatcher<TEvent>.RemoveEventManagerInternal();
        }
    }
}
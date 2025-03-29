

namespace TestEngine.Models
{
    public static class SceneManager
    {
        static List<Scene> scenes = new List<Scene>();
        static int _firstScene = 0;
        static int _currentScene = 0;

        public static void AddScene(Scene scene)
        {
            scenes.Add(scene);
        }

        public static Scene GetScene(int id)
        {
            return scenes[id];
        }

        public static void RemoveScene(int id)
        {
            Scene? scene = scenes[id];
            if (scene != null)
            {
                scenes.Remove(scene);
                
            }
        }
    }
}
using System.Data.Common;
using TestEngine.Exceptions;

namespace TestEngine.Models
{
    public enum SceneSource
    {
        FromTxtFile, // ????
        FromCsvFile, // ????
        FromXmlFile,
        FromDB,
        FromMemory, // ??????
        FromScene
    }
    public class SceneSettings
    {
        public string Name {get;set;}
        public Node? MainNode {get;set;}

        // FromFile / Scene (Memory?)
        public SceneSettings(SceneSource sceneSource, string path)
        {
            if (sceneSource == SceneSource.FromXmlFile)
            {

            } else {
                throw new WrongSourceException($"Given source type: {nameof(sceneSource)}");
            }
        }

        public SceneSettings(SceneSource sceneSource, DbConnection connection)
        {
            if (sceneSource == SceneSource.FromDB)
            {

            } else {
                throw new WrongSourceException($"Given source type: {nameof(sceneSource)}");
            }
        }

        public SceneSettings(SceneSource sceneSource, Scene scene)
        {
            if (sceneSource == SceneSource.FromScene)
            {

            }
            else
            {
                throw new WrongSourceException($"Given source type: {nameof(sceneSource)}");
            }
        }
    }
}
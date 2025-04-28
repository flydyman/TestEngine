namespace TestEngine.Resources;

public class AnimationEntity2D: IDisposable
{
    public string Name { get; set; }
    public int AnimationSpeed { get; set; } = 5; // frames?
    public bool IsLooped { get; set; } = true;
    public List<Texture2D> Textures { get; set; } = new List<Texture2D>();

    public AnimationEntity2D(string name)
    {
        Name = name;
    }

    public void Dispose()
    {
        foreach (Texture2D texture in Textures)
        {
            texture.Dispose();
        }
        GC.SuppressFinalize(this);
    }
}
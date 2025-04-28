namespace TestEngine.Resources;

public class AnimationBank2D : IDisposable
{
    public string BankName { get; set; }
    public List<AnimationEntity2D> Animations { get; set; } = new List<AnimationEntity2D>();

    public AnimationBank2D(string name)
    {
        BankName = name;
    }

    public void Dispose()
    {
        foreach (AnimationEntity2D animation in Animations)
        {
            animation.Dispose();
        }
        GC.SuppressFinalize(this);
    }
}
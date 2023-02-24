using DG.Tweening;

public static class TweeningExtensions
{
    public static void KillIfActive(this Tweener tweener)
    {
        if(tweener != null && tweener.IsActive())
        {
            tweener.Kill();
        }
    }
}

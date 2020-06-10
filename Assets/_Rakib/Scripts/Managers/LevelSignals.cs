namespace Rakib
{
    public class LevelSignals
    {
    }
    
    public class LevelLoadSignal{}
    public class LevelStartSignal{}
    public class LevelCompleteSignal{}
    public class LevelFailSignal{}
    public class LevelLoadNextSignal{}
    public class LevelLoadSameSignal{}

    public class ProgressUpdateSignal
    {
        public int Avatar;
        public float Progress;
    }
    
    public class PlayerUpdateSignal
    {
        public float SlideFactor;
        public float ZPosition;
    }
    
    public class LevelStatusSignal
    {
        public string Status;
    }

}
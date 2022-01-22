namespace FakerLib.Test
{
    public class B
    {
        public bool TestBoolean;

        public char TestChar;

        public string TestCity { get; private set; }

        private B(string testCity)
        {
            TestCity = testCity;
        }
    }
}

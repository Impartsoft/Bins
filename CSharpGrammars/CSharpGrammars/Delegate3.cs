namespace CSharpGrammars
{
    class DelegateTest3
    {
        public delegate void GetHello();
        public event GetHello SayHelloEvent;

        public void SayHello()
        {
            SayHelloEvent = DelegateTest3_SayHelloEvent;
            SayHelloEvent += DelegateTest3_SayHelloEvent;
        }

        private void DelegateTest3_SayHelloEvent()
        {
            throw new System.NotImplementedException();
        }
    }
}

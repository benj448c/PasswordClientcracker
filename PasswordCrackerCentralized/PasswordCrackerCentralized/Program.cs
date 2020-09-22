namespace PasswordCrackerCentralized
{
    class Program
    {
        static void Main()
        {
            Cracking cracker = new Cracking();
            TcpClientCracker client = new TcpClientCracker();
            client.Start("127.0.0.1", 7007);
        }
    }
}

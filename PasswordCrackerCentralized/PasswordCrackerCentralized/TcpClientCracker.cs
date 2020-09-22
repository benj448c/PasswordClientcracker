using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PasswordCrackerCentralized.model;

namespace PasswordCrackerCentralized
{
    public class TcpClientCracker
    {
        public void Start(string host, int port)
        {
            TcpClient socket = new TcpClient(host, port);

            Does(socket);
        }

        private List<UserInfoClearText> DoHack(WorkInfo work)
        {


            Cracking crack  = new Cracking();

            return crack.RunCracking(work);

        }

        private void Does(TcpClient socket)
        {
            bool hackInProgress = true;

            using (socket)
            {
                NetworkStream ns = socket.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);
                sw.AutoFlush = true;

                while (hackInProgress)
                {
                    Console.WriteLine("listening");
                    string message = Console.ReadLine();

                    switch (message)
                    {
                        case "hack":
                            sw.WriteLine("hack");
                            string jsonstring = sr.ReadLine();
                            Console.WriteLine(jsonstring);
                            Task.Run(() =>
                            {
                                System.Console.WriteLine("Starting hack!!");
                                WorkInfo work = JsonConvert.DeserializeObject<WorkInfo>(jsonstring);
                                sw.WriteLine(JsonConvert.SerializeObject(DoHack(work)));
                                sw.WriteLine("completed");
                                sw.WriteLine(work.Id);
                            });
                            break;
                    }
                }


            }
        }
    }
}
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

        private async Task<List<UserInfoClearText>> DoHack(WorkInfo work)
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
                Console.WriteLine("listening");
                string message = Console.ReadLine();
                while (hackInProgress)
                {
                    
                    switch (message)
                    {
                        case "hack":
                            sw.WriteLine("hack");
                            string jsonstring = sr.ReadLine();
                            Task.Run(async () =>
                            {
                                System.Console.WriteLine("Starting hack!!");
                                WorkInfo work = JsonConvert.DeserializeObject<WorkInfo>(jsonstring);
                                if (work.WordList.Count > 0)
                                {
                                    Console.WriteLine(work.WordList.Count + work.WordList[0]);
                                    List<UserInfoClearText> list = await DoHack(work);
                                    sw.WriteLine(JsonConvert.SerializeObject(list));
                                    sw.WriteLine("completed");
                                    sw.WriteLine(work.Id);
                                    message = "hack";
                                    Console.WriteLine("new hack");
                                }
                                else
                                {
                                    Console.WriteLine("no more dicts");
                                    hackInProgress = false;
                                }
                            });
                            break;
                    }
                    message = null;
                }


            }
        }
    }
}
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
            Console.WriteLine("Disconnecting");
            socket?.Close();
        }

        private async Task<List<UserInfoClearText>> DoHack(WorkInfo work)
        {

            Cracking crack = new Cracking();

            List<UserInfoClearText> result = await Task.Run(() => crack.RunCracking(work));

            return result;



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
                    if (message != null)
                    {
                        Console.WriteLine("sending - " + message);
                    }




                    switch (message)
                    {
                        case "hack":
                            message = null;
                            if (socket.Connected == false)
                            {
                                Console.WriteLine("connection lost");
                                hackInProgress = false;
                            }
                            else if (sr.BaseStream != null)
                            {
                                sw.WriteLine("hack");
                                try
                                {
                                    string jsonstring = sr.ReadLine();


                                    Task.Run(async () =>
                                    {
                                        System.Console.WriteLine("Starting hack!!");
                                        WorkInfo work = JsonConvert.DeserializeObject<WorkInfo>(jsonstring);
                                        if (work.WordList.Count > 0)
                                        {
                                            Console.WriteLine(work.WordList.Count + work.WordList[0]);
                                            List<UserInfoClearText> list = await DoHack(work);
                                            sw.WriteLine("passwordfound");
                                            sw.WriteLine(JsonConvert.SerializeObject(list));
                                            sw.WriteLine("completed");
                                            sw.WriteLine(work.Id);
                                            Console.WriteLine("new hack");
                                            message = "hack";

                                        }
                                        else
                                        {
                                            Console.WriteLine("no more dicts");
                                            hackInProgress = false;
                                        }
                                    });
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }

                            }

                            break;
                    }
                }
            }
        }
    }
}
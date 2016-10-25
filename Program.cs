using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Protocol;
using Thrift.Transport;
using System.IO;
using RigourTech;
using System.Threading;

namespace EventAnaliyServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //IList<int> test = new List<int>() {10,8,15,46,8,16,6 };

            //Console.WriteLine(test.LastOrDefault(n => n > 10));
            //Console.ReadLine();
            //return;

            TSocket socket = new TSocket("127.0.0.1", 8807);
            TBinaryProtocol pro = new TBinaryProtocol(socket);
            TennisDataAnaliy.Client client = new TennisDataAnaliy.Client(pro);

            bool isopen = false;
            while (!isopen)
            {
                try
                {
                    socket.Open();
                    isopen = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("等待服务启动...");
                    Thread.Sleep(1000);
                }
            }

            Console.WriteLine("可以开始测试了");
            TMatch match = new TMatch();
            match.Court = new TennisCourt();
            match.Court.Width = 10970;
            match.Court.Height = 23770;
            match.MatchID = Guid.NewGuid().ToString();
            match.StartTime = DateTime.Now.ToString();
            match.Users = new List<userInfo>();
            match.Users.Add(new userInfo() {  UserID=Guid.NewGuid().ToString(), Direction=1,Hand=0});
            match.Users.Add(new userInfo() { UserID = Guid.NewGuid().ToString(), Direction = -1, Hand = 0 });
            if (client.StartMatch(match))
            {
                Console.WriteLine("开始新的比赛成功");
            }
            else
            {
                Console.WriteLine("开始比赛失败");
            }

            string path = @"E:\网球\03网球鹰眼顶级赛事版\02doc\00-项目文档\Log.txt";
            StreamReader reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                string trace = reader.ReadLine();
                client.InsertPathToDB(trace);
            }
            Console.WriteLine("轨迹数据发送完毕");
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Protocol;
using Thrift.Transport;
using System.IO;
namespace EventAnaliyServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TSocket socket = new TSocket("127.0.0.1", 1899);
            TBinaryProtocol pro = new TBinaryProtocol(socket);
            TennisDataAnaliy.TennisDataAnaliy.Client client = new TennisDataAnaliy.TennisDataAnaliy.Client(pro);
            socket.Open();
            Console.WriteLine("可以开始测试了");
            TennisDataAnaliy.userInfo user = new TennisDataAnaliy.userInfo();
            user.UserID = 1;
           // user.Rect = "(0,0),(100,100)";
            client.UpdatePlayers(new List<TennisDataAnaliy.userInfo>() { user});
            Console.WriteLine("用户信息设置完成");
            TennisDataAnaliy.RunningMode model = new TennisDataAnaliy.RunningMode();
            model.Model = "train";
            model.TargetMarkNumber = "8";
            client.SetRunningModel(model);
            Console.WriteLine("工作模式设置完成");
            string path = @"E:\网球\03网球鹰眼顶级赛事版\02doc\00-项目文档\Log.txt";
            StreamReader reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
               string trace= reader.ReadLine();
                client.InsertPathToDB(trace);
            }
            Console.WriteLine("轨迹数据发送完毕");
        }
    }
}

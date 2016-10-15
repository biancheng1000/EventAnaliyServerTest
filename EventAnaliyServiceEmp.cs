using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TennisDataAnaliy;

namespace DataProviderService
{
    public class EventAnaliyServiceEmp: TennisDataAnaliy.TennisDataAnaliy.Iface
    {

        #region Iface
        /// <summary>
        /// 接收来自底层的数据
        /// </summary>
        /// <param name="path"></param>
        public void InsertPathToDB(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 接收来自界面传递来的用户信息
        /// </summary>
        /// <param name="useinfo"></param>
        public void SetPlayers(List<userInfo> useinfo)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 接收来自客户端当前的模式
        /// 比赛 训练（需要目标区域号码）
        /// </summary>
        /// <param name="model"></param>
        public void SetRunningModel(RunningMode model)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 接收到底层坐标信息进行事件分析
        /// <summary>
        /// 匹配人员坐标
        /// </summary>
        /// <param name="SubjectString"></param>
        private void MatchPerson(string SubjectString)
        {
            string ResultString = null;
            try
            {
                ResultString = Regex.Match(SubjectString, "[0]\\|[0-9]*\\|[0-9]*\\.[0-9]*,[0-9]*\\.[0-9]*,[0-9]*\\.[0-9]*").Value;
            }
            catch (ArgumentException ex)
            {
                // Syntax error in the regular expression
            }
        }
        /// <summary>
        /// 匹配数据
        /// </summary>
        /// <param name="SubjectString"></param>
        private void matchSpeed(string SubjectString)
        {
            string ResultString = null;
            try
            {
                ResultString = Regex.Match(SubjectString, "[2]\\|[0-9]*\\.[0-9]*").Value;
            }
            catch (ArgumentException ex)
            {
                // Syntax error in the regular expression
            }
        }

        /// <summary>
        /// 匹配轨迹数据
        /// </summary>
        /// <param name="subjectstring"></param>
        private void MatchTracks(string subjectstring)
        {
            string ResultString = null;
            try
            {
                MatchCollection results = Regex.Matches(subjectstring, "[0-9]*,[0-9]*\\.[0-9]*,[0-9]*\\.[0-9]*,[0-9]*\\.[0-9]*,[0-9]*\\.[0-9]*(;¦|)");
            }
            catch (ArgumentException ex)
            {
                
            }
        }
        #endregion

    }
}

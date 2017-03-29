using SF.Entitys;
using System.Threading.Tasks;

namespace SF.Core.Common.Message.Sms
{
    public interface ISmsSender
    {
        Task SendSmsAsync(ISiteContext site, string number, string message);
    }
}

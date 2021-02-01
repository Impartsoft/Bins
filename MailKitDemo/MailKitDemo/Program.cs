using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using System.IO;
using System.Linq;

namespace MailKitDemo
{
    class Program
    {
        private const string MAIL_NAME = "";
        private const string MAIL_PASSWORD = "";
        private const string DIRECTORY = @"D:\";
        static void Main(string[] args)
        {
            using (var client = new ImapClient())
            {
                client.Connect("imap.exmail.qq.com", 993, SecureSocketOptions.SslOnConnect);

                client.Authenticate(MAIL_NAME, MAIL_PASSWORD);

                client.Inbox.Open(FolderAccess.ReadWrite);

                // 读取方式一：可以预先筛选邮件
                // search for messages where the Subject header contains either "MimeKit" or "MailKit"
                // var query = SearchQuery.SubjectContains("MimeKit").Or(SearchQuery.SubjectContains("MailKit"));
                // var uids = client.Inbox.Search(query);

                // 读取方式二：读取所有邮件
                var uids = client.Inbox.Search(SearchQuery.All);
                foreach (var uid in uids)
                {
                    var message = client.Inbox.GetMessage(uid);

                    // 操作邮件一：读取邮件标题
                    string subject = message.Subject;
                    if (!subject.Contains("MimeKitDemo"))
                        return;

                    // 操作邮件二：读取正文
                    string body = message.TextBody ?? string.Empty;
                    if (!body.Contains("MimeKitDemoBody"))
                        return;

                    // 操作邮件三：下载邮件附件
                    var attachments = message.Attachments;
                    if (attachments.Any())
                    {
                        foreach (var attachment in attachments)
                            DownloadAttachment(attachment);
                    }

                    // 操作邮件四：移动邮件（移动至删除文件夹）
                    client.Inbox.MoveTo(uid, client.GetFolder(SpecialFolder.Trash));

                    // 操作邮件五：删除邮件 - 将邮件标记为删除、最后删除
                    client.Inbox.AddFlags(uid, MessageFlags.Deleted, true);
                    client.Inbox.Expunge();
                }

                client.Disconnect(true);
            }
        }

        /// <summary>
        /// 下载邮件附件
        /// </summary>
        private static void DownloadAttachment(MimeEntity attachment)
        {
            if (attachment is MessagePart)
            {
                var fileName = attachment.ContentDisposition?.FileName;
                var rfc822 = (MessagePart)attachment;

                if (string.IsNullOrEmpty(fileName))
                    fileName = "attached-message.eml";

                var path = Path.Combine(DIRECTORY, fileName);
                using (var stream = File.Create(path))
                    rfc822.Message.WriteTo(stream);
            }
            else
            {
                var part = (MimePart)attachment;
                var fileName = part.FileName;

                var path = Path.Combine(DIRECTORY, fileName);
                using (var stream = File.Create(path))
                    part.Content.DecodeTo(stream);
            }
        }
    }
}

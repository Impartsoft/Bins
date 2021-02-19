## 0.介绍
> MimeKit and MailKit are popular fully-featured email frameworks for .NET
框架支持版本如下
> Supports .NET 4.5, .NET 4.6, .NET 4.7, .NET 4.8, .NET 5.0, .NETStandard 2.0, Xamarin.Android, Xamarin.iOS, Windows Phone 8.1, and more.

MailKit是最流行且最强大的.NET邮件处理框架之一，下面为大家简单介绍MailKit的使用方式（IMAP为例）

## 1. 参考资料
> Github https://github.com/jstedfast/MailKit
> 官方Doc http://www.mimekit.net/docs/html/Introduction.htm

## 2.核心内容（IMAP为例）
- #### 连接邮箱

##### 加密 
```
client.Connect("imap.exmail.qq.com", 993, SecureSocketOptions.SslOnConnect);
```
##### 不加密
```
client.Connect("imap.exmail.qq.com", 143, SecureSocketOptions.None);
```

- #### 登入邮箱
``` 
client.Authenticate(MAIL_NAME, MAIL_PASSWORD);
```

- #### 打开邮件文件夹 
```
client.Inbox.Open(FolderAccess.ReadWrite);
```

- #### 读取文件

##### 读取方式一：可以预先筛选邮件 
```
   search for messages where the Subject header contains either "MimeKit" or "MailKit"
   var query = SearchQuery.SubjectContains("MimeKit").Or(SearchQuery.SubjectContains("MailKit"));
   var uids = client.Inbox.Search(query);
```
##### 读取方式二：读取所有邮件 
```
   var uids = client.Inbox.Search(SearchQuery.All); 
```

- #### 邮件操作

##### 操作邮件一：读取邮件标题 
```
    string subject = message.Subject;
    if (!subject.Contains("MimeKitDemo"))
        return;
```

##### 操作邮件二：读取正文 
 ```
    string body = message.TextBody ?? string.Empty;
    if (!body.Contains("MimeKitDemoBody"))
        return;
```
##### 操作邮件三：下载邮件附件 
```
    var attachments = message.Attachments;
    if (attachments.Any())
    {
        foreach (var attachment in attachments)
            DownloadAttachment(attachment);
    }

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
    
```
##### 操作邮件四：移动邮件（移动至删除文件夹） 
```
 
    client.Inbox.MoveTo(uid, client.GetFolder(SpecialFolder.Trash));

```

##### 操作邮件五：删除邮件 - 将邮件标记为删除、最后删除 

```
    client.Inbox.AddFlags(uid, MessageFlags.Deleted, true);
    client.Inbox.Expunge();

```
 

## 3.样例源码地址

 > https://github.com/Impartsoft/Bins/tree/main/MailKitDemo


```
欢迎大家批评指正，共同学习，共同进步！
作者：Iannnnnnnnnnnnn
出处：https://www.cnblogs.com/Iannnnnnnnnnnnn
本文版权归作者和博客园共有，欢迎转载，但未经作者同意必须保留此段声明，且在文章页面明显位置给出原文连接，否则保留追究法律责任的权利。
```

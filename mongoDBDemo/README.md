## 0.介绍
> MongoDB is a document database with the scalability and flexibility that you want with the querying and indexing that you need


本文记录MongoDB的简单使用，可以直接调试样例源码。

## 1. 参考资料
> 官方Doc https://docs.mongodb.com/drivers/csharp/

> MongoDB Runoob教程 https://www.runoob.com/mongodb/mongodb-window-install.html

> 博客 https://www.cnblogs.com/yan7/p/8603640.html

## 2.核心内容


##### 连接mongoDB
```

var client = new MongoClient("mongodb://localhost:27017");
var database = client.GetDatabase("db");

// 文档对象
return database.GetCollection<BsonDocument>("runoob");
   
```

##### 连接mongoDB
```

var client = new MongoClient("mongodb://localhost:27017");
var database = client.GetDatabase("db");

return database.GetCollection<BsonDocument>("runoob");
   
```

##### 写入数据 - 单条写入
```
collection.InsertOne(new BsonDocument("Name1", "Jack4"));
   
```


##### 写入数据 - 多条写入
```
collection.InsertMany(bsonDocuments);
   
```

##### 查询 
```
// 查找所有
var result = collection.Find(Builders<BsonDocument>.Filter.Empty).ToList();

// 匹配Name 等于 Poth
var result = collection.Find(Builders<BsonDocument>.Filter.Eq("Name", "Poth");).ToList();

// 匹配Sum 大于 4
var result = collection.Find(Builders<BsonDocument>.Filter.Gt("Sum", 4)).ToList();

// 匹配Sum 小于 4
var result = collection.Find(Builders<BsonDocument>.Filter.Lt("Sum", 4)).ToList();

```
>  更多匹配方式可参考
https://mongodb.github.io/mongo-csharp-driver/2.11/apidocs/html/T_MongoDB_Driver_FilterDefinitionBuilder_1.htm

## 3.样例源码地址
调试Demo可以先参考MongoDB Runoob安装教程，部署MongoDB之后再进行调试
 > https://github.com/Impartsoft/Bins/tree/main/mongoDBDemo
 
---


> 欢迎大家批评指正，共同学习，共同进步！
> 作者：Iannnnnnnnnnnnn
> 出处：https://www.cnblogs.com/Iannnnnnnnnnnnn
> 本文版权归作者和博客园共有，欢迎转载，但未经作者同意必须保留此段声明，且在文章页面明显位置给出原文连接，否则保留追究法律责任的权利。
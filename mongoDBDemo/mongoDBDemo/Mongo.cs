using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

namespace mongoDBDemo
{
    class Mongo
    {
        /// <summary>
        /// .net使用MongoDB的学习笔记
        /// 作者：Iannnnnnnnnnnnn
        /// 博客：https://www.cnblogs.com/Iannnnnnnnnnnnn/p/14427538.html
        /// </summary>
        private static IMongoCollection<BsonDocument> GetMongoCollection()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("db");

            // 文档对象
            return database.GetCollection<BsonDocument>("runoob");
        }

        public void MongoTest()
        {
            var collection = GetMongoCollection();

            // 写入数据 - 单条写入
            collection.InsertOne(new BsonDocument("Name1", "Jack4"));

            // 写入数据 - 多条写入
            var bsonDocuments = new[]
            {
                new BsonDocument{
                    { "DepartmentName","开发部"},
                    { "People",new  BsonArray
                        {
                            new BsonDocument{ { "Name", "狗娃" },{"Age",20 } },
                            new BsonDocument{ { "Name", "狗剩" },{"Age",22 } },
                            new BsonDocument{ { "Name", "铁蛋" },{"Age",24 } }
                        }
                    },
                    { "Sum",18 },
                    { "dim_cm", new BsonArray { 14, 21 } }
                },
                 new BsonDocument{
                    { "People",new  BsonArray
                        {
                            new BsonDocument{ { "Name", "张三" },{"Age",11 } },
                            new BsonDocument{ { "Name", "李四" },{"Age",34 } },
                            new BsonDocument{ { "Name", "王五" },{"Age",33 } }
                        }
                    },
                     { "Sum",4 },
                     { "dim_cm", new BsonArray { 14, 21 } }

                },
                  new BsonDocument{
                    { "People",new  BsonArray
                        {
                            new BsonDocument{ { "Name", "闫" },{"Age",20 } },
                            new BsonDocument{ { "Name", "王" },{"Age",22 } },
                            new BsonDocument{ { "Name", "赵" },{"Age",24 } }
                        }
                    },
                    { "Sum",2 },
                    { "dim_cm", new BsonArray { 22.85, 30 } }
                }
            };

            collection.;


            collection.InsertMany(bsonDocuments);

            // 查找所有
            var result = collection.Find(Builders<BsonDocument>.Filter.Empty).ToList();

            // 查询部门是开发部的信息
            var filter = Builders<BsonDocument>.Filter.Eq("DepartmentName", "开发部");
            result = collection.Find(filter).ToList();

            // 获取Sum大于4的数据
            filter = Builders<BsonDocument>.Filter.Gt("Sum", 4);
            result = collection.Find(filter).ToList();

            // And约束  
            filter = Builders<BsonDocument>.Filter.And(Builders<BsonDocument>.Filter.Gt("Sum", 2), Builders<BsonDocument>.Filter.Eq("DepartmentName", "开发部"));
            result = collection.Find(filter).ToList();

            // 查询指定值
            // Include 包含某元素    Exclude  不包含某元素
            ProjectionDefinition<BsonDocument> projection = Builders<BsonDocument>.Projection.Include("DepartmentName").Exclude("_id");
            result = collection.Find(Builders<BsonDocument>.Filter.Empty).Project(projection).ToList();

            // 排序
            // Ascending 正序    Descending 倒序
            SortDefinition<BsonDocument> sort = Builders<BsonDocument>.Sort.Ascending("Sum");
            result = collection.Find(Builders<BsonDocument>.Filter.Empty).Sort(sort).ToList();
        }
    }
}

using System.Collections.Generic;

namespace CSRedisDemo
{
    public class Student
    {
        public Student(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; set; }

        public int Age { get; set; }
    }

    public class Test
    {
        public static IList<Student> AllStudents = new List<Student>
        {
            new Student("Mark",2),
            new Student("William",13),
            new Student("Lilith",30),
        };
    }
}
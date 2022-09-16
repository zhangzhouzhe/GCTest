using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace GCTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var s1 = new Staff("s1", 1);
            //var s2 = new Staff("s2", 2);

            //Console.WriteLine("创建两个对象 进入 Gen0");

            //s1 = null;

            //GC.Collect();


            //Console.WriteLine("s1回收  s2进入 Gen1");
            //GC.Collect();

            //var s3 = new Staff("s3", 3);

            //var s4 = GetAll(200);

            //Console.WriteLine("s3进入 Gen0,s2进入 Gen2");


            var s = new StaffCollection();
            var names = s.Where(a => a.Name == "123").Where(b => b.Name == "123");


            for (int i = 0; i < 6; i++)
            {
                var name = names.FirstOrDefault();
            }
         


            Console.ReadLine();




        }

        public static List<Staff> GetAll(long count)
        {
            var result = new List<Staff>();
            for (int i = 0; i < count; i++)
            {
                result.Add(new Staff("test", 1));
            }
            return result;
        }

    }



    public class Staff
    {
        public Staff(string name, int age)
        {
            Name = name;
            Age = age;
        }
        public string Name { get; set; }
        public int Age { get; set; }

    }




    public class StaffCollection : IEnumerable<Staff>
    {
        public StaffCollection()
        {
            _staffs = new Staff[] {
                new Staff("1",1),
                new Staff("2",2)
               
            };
        }
        public Staff this[int index] => _staffs[index];
        public int Count => _staffs.Length;
        private Staff[] _staffs;


        IEnumerator<Staff> IEnumerable<Staff>.GetEnumerator()
        {
            return new StaffEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new StaffEnumerator(this);
        }
    }

    public class StaffEnumerator : IEnumerator<Staff>
    {
        private int _index;
        private readonly StaffCollection _staffCollection;

        public StaffEnumerator(StaffCollection staffCollection)
        {
            _staffCollection = staffCollection;
            _index = -1;
        }

        public Staff Current
        {
            get
            {
                var result = _staffCollection[_index];
                return result;
            }
        }


        object System.Collections.IEnumerator.Current
        {
            get
            {
                var result = _staffCollection[_index];
                return result;
            }
        }

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            _index++;
            return _index < _staffCollection.Count;
        }

        public void Reset()
        {
            _index = -1;
        }
    }
}

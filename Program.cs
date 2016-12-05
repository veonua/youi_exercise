using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("youiTestTests")]
namespace youiTest
{
    public class Program
    {
        /// <summary>
        /// Frequency of the first and last names ordered by frequency and then alphabetically.
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> task1(ICollection<Name> names)
        {
            return (from x in names
                    group x by new { x.first, x.last } into grp
                    let count = grp.Count()
                    orderby count descending, grp.Key.first, grp.Key.last
                    select new
                    {
                        grp.Key.first,
                        grp.Key.last,
                        count
                    });
        }

        /// <summary>
        /// Frequency of the first and last names (concatinated) ordered by frequency and then alphabetically.
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> task1_2(ICollection<Name> names)
        {
            return (from x in names
                    select x.first).Concat(
                    from y in names
                    select y.last).
                    GroupBy(x => x)
                    .Select(group => new { name = group.Key, count = group.Count() })
                    .OrderByDescending(x=> x.count);
        }

        /// <summary>
        /// the addresses sorted alphabetically by street name.
        /// </summary>
        /// <param name="addresses"></param>
        /// <param name="distinct"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> task2(ICollection<Address> addresses, bool distinct)
        {
            var query2 = (from x in addresses orderby x.streetName select x.streetName);
            return distinct ? query2.Distinct() : query2;
        }

        static void Main(string[] args)
        {
            var parser = new TextFieldParser(args[0]);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(new string[] { "," });

            if (!parser.EndOfData)
            {
                string[] row = parser.ReadFields();
                Debug.Assert(row[0] == "FirstName");
                Debug.Assert(row[2] == "Address");
            }

            ICollection<Name> names = new List<Name>();
            ICollection<Address> addresses = new List<Address>();

            while (!parser.EndOfData)
            {
                string[] row = parser.ReadFields();
                names.Add(new Name(row[0], row[1]));
                addresses.Add(new Address(row[2]));
            }
            
            foreach (var t in task1(names))
            {
                System.Console.WriteLine(t.first+","+ t.last + "," +t.count);
            }

            System.Console.WriteLine("-------------1.2------------");

            foreach (var t in task1_2(names))
            {
                System.Console.WriteLine(t.name + "," + t.count);
            }

            System.Console.WriteLine("-------------2------------");

            foreach (var t in task2(addresses, true))
            {
                System.Console.WriteLine(t);
            }

            System.Console.ReadKey();
        }
    }

    public class Name
    {
        public readonly string first;
        public readonly string last;

        public Name(string first, string last)
        {
            this.first = first;
            this.last = last;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            Name nameObj = (obj as Name);
            if (nameObj == null) return false;

            return string.Equals(first, nameObj.first) && string.Equals(last, nameObj.last);
        }

        public override int GetHashCode()
        {
            return (first + "#" + last).GetHashCode();
        }
    }

    public class Address
    {
        public readonly string streetName;

        public Address(string v)
        {
            streetName = Regex.Match(v, @"\d*\W*(.+)").Groups[1].Value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            Address addressObj = (obj as Address);
            if (addressObj == null) return false;

            return string.Equals(streetName, addressObj.streetName);
        }

        public override int GetHashCode()
        {
            return streetName.GetHashCode();
        }
    }
}

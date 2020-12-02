using System;
using System.Collections.Generic;

/*
 * Benefits of Records:
 *  - Simple to set up
 *  - Thread-safe (it's immutable)
 *  - Easy/safe to share
 *
 * When to use records:
 *  - Capturing external data that doesnt change WheatherService, SWAPI.dev
 *  - API calls
 *  - Processing data
 *  - Anytime when you work with readonly data
 *
 *  When not to use records:
 *   - When you need to change any data (Entity Framework)
*/

namespace RecordDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Record1 record1A = new("Ana", "Helena");
            Record1 record1B = new("Ana", "Helena");
            Record1 record1C = new("Joao", "Pereira");

            Class1 class1A = new("Ana", "Helena");
            Class1 class1B = new("Ana", "Helena");
            Class1 class1C = new("Joao", "Pereira");

            Console.WriteLine("Record Type:");
            Console.WriteLine($"To String: {record1A}"); //it overrides the ToString method
            Console.WriteLine($"Are the two objects equal? {Equals(record1A, record1B)}"); //it compares the values inside the object
            Console.WriteLine($"Are the two objects reference equal? {ReferenceEquals(record1A, record1B)}"); //it compares the values inside the object
            Console.WriteLine($"Are the two objects ==? {record1A == record1B}");
            Console.WriteLine($"Are the two objects !=? {record1A != record1C}");
            Console.WriteLine($"Hashcode of {nameof(record1A)}: { record1A.GetHashCode() }");
            Console.WriteLine($"Hashcode of {nameof(record1B)}: { record1B.GetHashCode() }");
            Console.WriteLine($"Hashcode of {nameof(record1C)}: { record1C.GetHashCode() }");

            Console.WriteLine();
            Console.WriteLine("***************************************************************************");
            Console.WriteLine();

            Console.WriteLine("Class Type:");
            Console.WriteLine($"To String: {class1A}");
            Console.WriteLine($"Are the two objects equal? {Equals(class1A, class1B)}");
            Console.WriteLine($"Are the two objects reference equal? {ReferenceEquals(class1A, class1B)}"); //it compares the values inside the object
            Console.WriteLine($"Are the two objects ==? {class1A == class1B}");
            Console.WriteLine($"Are the two objects !=? {class1A != class1C}");
            Console.WriteLine($"Hashcode of {nameof(class1A)}: { class1A.GetHashCode() }");
            Console.WriteLine($"Hashcode of {nameof(class1B)}: { class1B.GetHashCode() }");
            Console.WriteLine($"Hashcode of {nameof(class1C)}: { class1C.GetHashCode() }");

            Console.WriteLine();
            Console.WriteLine("***************************************************************************");
            Console.WriteLine();

            //Deconstructor
            var (fn, ln) = record1A;
            Console.WriteLine($"The value of fn is {fn} and the value of ln is {ln}");

            Console.WriteLine();

            //copy of the record1A with the FirstName = "Maria"
            Record1 record1D = record1A with
            {
                FirstName = "Maria"
            };
            Console.WriteLine($"Maria's Record { record1D }");

            Console.WriteLine();

            Record2 record2A = new Record2("Ana", "Helena");
            Console.WriteLine($"{nameof(record2A)} value: {record2A}");
            Console.WriteLine($"{nameof(record2A)} fn: {record2A.FirstName}   ln: {record2A.LastName}");
            Console.WriteLine(record2A.SayHello());
        }
    }

    //a Record is just a fancy class
    //it's a pre build code to make your life easier
    //Immutable - the values cannot be changed (aka a readonly class)
    public record Record1(string FirstName, string LastName);
    public record User1(int Id, string FirstName, string LastName) : Record1(FirstName, LastName);

    public class DiscoveryModel
    {
        public User1 LookupUser { get; set; }
        public int IncidentsFound { get; set; }
        public List<string> Incidents { get; set; }
    }

    public record Record2(string FirstName, string LastName)
    {
        private string _firstName = FirstName;

        public string FirstName
        {
            get { return _firstName.Substring(0, 1); }
            init { }
        }

        public string FullName => $"{FirstName} {LastName}";

        public string SayHello()
        {
            return $"Hello {FirstName}";
        }
    }

    public class Class1
    {
        public string FirstName { get; init; }
        public string LastName { get; init; } //=> init means that you can only set its value in a constructor or when you create the class using the {} syntax

        public Class1(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void Deconstruct(out string FirstName, out string LastName)
        {
            FirstName = this.FirstName;
            LastName = this.LastName;
        }
    }

    //*****************************************
    //DO NOT DO ANY OF THE BELOW
    //*****************************************

    public record Record3 //No constructor so no deconstructor
    {
        public string FirstName { get; set; } //the set makes this record mutable (BAD!!!)
        public string LasName { get; set; } //the set makes this record mutable (BAD!!!)
    }

    //Dont just make clones all over to update state
}
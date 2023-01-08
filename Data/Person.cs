using Reflection.CustomAttributes;

namespace Reflection.Data
{
    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public int ZipCode { get; set; }

        [MethodForRun(RunCount = 3)]
        public void Print()
        {
            Console.WriteLine($"{FirstName} {LastName}");
        }

        [MethodForRun(RunCount = 3)]
        public void SayHi()
        {
            Console.WriteLine($"Hi {FirstName}");
        }

        public void Move(int newZipCode)
        {
            ZipCode = newZipCode;
            Console.WriteLine($"{FirstName} {LastName} has been move to new zip code");
        }
    }
}

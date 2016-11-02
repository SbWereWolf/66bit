using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ListDllClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" please input directory full path : ");
            var path = Console.ReadLine();

            FileInfo[] dlls = null ;
            if (path != null)
            {
                //path = @"C:\WORK\VacancyTask\AppleTest_Structure\BusinessLogic\bin\Debug\";
                var folder = new DirectoryInfo(path);
                dlls = folder.GetFiles("*.dll");
            }

            if (dlls != null )
            {
                foreach (var dll in dlls)
                {
                    if (dll != null)
                    {
                        var dllPath = dll.FullName;
                        try
                        {
                            Assembly asm = Assembly.LoadFrom(dllPath);
                            Console.WriteLine($@" file : {dll.Name}");
                            if (asm?.DefinedTypes != null)
                                foreach (var typeInfo in asm.DefinedTypes)
                                {
                                    if (typeInfo != null && typeInfo.IsClass)
                                    {
                                        Console.WriteLine($@" - Namespace : {typeInfo.Namespace} , Class : {typeInfo.Name} ");
                                        var list = typeInfo.DeclaredMethods?.Where(x => x != null).Where(x => x.IsPublic || x.IsFamily);
                                        if (list != null)
                                        {
                                            foreach (var methodInfo in list)
                                            {
                                                Console.WriteLine($@" => Method : {methodInfo.Name}");
                                            }
                                        }
                                    }
                                }
                        }
                        catch
                        {
                            Console.WriteLine("Can't Load Assembly");
                        }
                    }
                }
            }


            Console.Read();
        }


    }
}

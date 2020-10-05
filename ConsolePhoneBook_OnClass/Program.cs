using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary0916;

namespace ConsolePhoneBook_OnClass
{
	class Program 
	{
		static void Main(string[] args)
		{
			
			PhoneBookManager manager = PhoneBookManager.CreateInstance();

			manager.ReadData();
			
			while (true)
			{
				try
				{
					manager.ShowMenu();
					int choice = Utility.ConvertInt(Console.ReadLine());

					switch (choice)
					{
						case 1: manager.InputData(); break;
						case 2: manager.ListData(); break;
						case 3: manager.SearchData(); break;
						case 4: manager.DeleteData(); break;
						case 5: manager.SortData(); break;
						case 6: manager.SaveDate();
								Console.WriteLine("프로그램을 종료합니다."); return;
						default: throw new Exception("\n1 ~ 6까지의 숫자만 입력해주세요"); //break;
					}
				}
				catch (Exception err)
				{
					Console.WriteLine(err.Message);
				}
			}
		}
	}
}

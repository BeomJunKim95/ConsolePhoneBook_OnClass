using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary0916;

namespace ConsolePhoneBook_OnClass
{
	class Program
	{
		static void Main(string[] args)
		{
			PhoneBookManager manager = new PhoneBookManager();
			
			while (true)
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
					case 6: Console.WriteLine("프로그램을 종료합니다."); return;
					default: Console.WriteLine("\n1 ~ 6까지의 숫자만 입력해주세요"); break;
						
				}
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePhoneBook_OnClass
{
	class MenuChoiceException : Exception
	{
		int wrongChoice;
		
		public MenuChoiceException(string message) : base(message) { }
		public MenuChoiceException(int choice):base("다시 메뉴를 선택해주세요")
		{
			wrongChoice = choice;
		}
		public void ShowWrongChoice()
		{
			Console.WriteLine($"{this.wrongChoice}에 해당되는 메뉴는 없습니다.");
		}
		
	}
	class InputException : Exception
	{
		string wrongInput;

		public InputException(string input):base(input)
		{
			wrongInput = input;
		}
		public void ShowWrongInput()
		{
			Console.WriteLine($"{this.wrongInput}은 필수입력 입니다.");
		}
	}
}

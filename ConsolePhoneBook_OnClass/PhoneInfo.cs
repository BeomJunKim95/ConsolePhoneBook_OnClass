using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePhoneBook_OnClass
{
	public class PhoneInfo
	{
		string name; //필수
		string phoneNumber; //필수
		string birth; //선택

		public string Name 
		{ get { return name; } }

		public PhoneInfo(string name, string phoneNumber)
		{
			this.name = name;
			this.phoneNumber = phoneNumber;
		}

		public PhoneInfo(string name, string phoneNumber, string birth)
		{
			this.name = name;
			this.phoneNumber = phoneNumber;
			this.birth = birth;
		}

		public virtual void ShowPhoneInfo()
		{
			Console.WriteLine("이름 : {0}\n번호 : {1}", Name, phoneNumber);

			if (string.IsNullOrEmpty(birth) == false)
				Console.WriteLine("생일 : {0}\n", birth);
		}
			
		//ToString()를 override해서 PhoneManager에서 사용해보기
		public override string ToString()
		{
			string val = $"이름 : {Name}\n번호 : {phoneNumber}\n";
			if (string.IsNullOrEmpty(birth) == false)
				val += $"생일 : {birth}\n";
			
			return val;
		}

		

	}
	public class PhoneUnivInfo : PhoneInfo
	{
		string major;
		int year;
		public PhoneUnivInfo(string name, string phoneNumber, string birth, string major, int year):base(name,phoneNumber,birth)
		{
			this.major = major;
			this.year = year;
		}

		public override void ShowPhoneInfo()
		{
			base.ShowPhoneInfo();
			Console.WriteLine($"학과 : {major}\n학년 : {year}\n");
		}

		public override string ToString()
		{
			string str = $"학과 : {major}\n학년 : {year}\n";
			return base.ToString() + str;

		}
	}

	public class PhoneCompanyInfo : PhoneInfo
	{
		string company;
		public PhoneCompanyInfo(string name, string phoneNumber, string birth, string company) : base(name, phoneNumber, birth)
		{
			this.company = company;
		}

		public override void ShowPhoneInfo()
		{
			base.ShowPhoneInfo();
			Console.WriteLine($"회사 : {company}\n");
		}

		public override string ToString()
		{
			string str = $"회사 : {company}\n";
			return base.ToString() + str;
		}
	}
}

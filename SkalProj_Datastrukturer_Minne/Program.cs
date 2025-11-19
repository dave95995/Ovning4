using System;

namespace SkalProj_Datastrukturer_Minne
{
	/*
		1. Hur fungerar stacken och heapen? Förklara gärna med exempel eller skiss på dess grundläggande funktion?

			Stacken är ett minnesområde som används för lokala variabler och funktionsanrop. Minnet hanteras enligt datastrukturen stack (Last In – First Out).
			När ett funktionsanrop sker så skapas en stackframe som innehåller returadress, funktionsparametrar och lokala variabler.
			Denna stackframe pushas på stacken och när funktionen returnerar poppas den bort och minnet frigörs automatiskt.
			Stacken har en fast storlek och kan därför orsaka stack overflow, exempelvis vid mycket djupa rekursioner.
			Däremot erbjuder den mycket snabb, effektiv och automatisk minneshantering.

			Heapen är ett minnesområde som används för dynamisk lagring och där lagras objekt och variabler av referenstyp eller som behöver leva längre än ett enskilt funktionsanrop.
			Till skillnad från stacken har heapen ingen fast storlek utan kan växa dynamiskt beroende på hur mycket tillgängliga minne det finns.
			Minneshanteringen är mer komplex och långsammare än i stacken eftersom data inte lagras i en bestämd ordning och allokering kräver mer arbete.
			Minnet på heapen frigörs inte automatiskt när en funktion avslutas utan hanteras antingen av en garbage collector (som i C# och Java)
			eller manuellt av programmeraren som i t.ex C.

		2. Vad är Value Types respektive Reference Types och vad skiljer dem åt?

			Hos en värdetyp innehåller själva variablen datan direkt. Exempel på detta är enkla typer som int, float, bool, char och struct.
			En referenstyp innehåller istället en referens (en minnesadress) som pekar på var datan ligger på heapen. Exempel är klasser, string, arrayer, listor och andra objekttyper.

			För referenstyper kan två variabler peka på samma objekt vilket innebär att om du ändrar objektet via den ena variabeln så påverkas den andra också.
			Hos värdetyper har däremot har varje variabel sin egen kopia av datan vilket betyder att ändringar på en variabel inte påverkar någon annan.

		3. Följande metoder (se bild nedan) genererar olika svar. Den första returnerar 3, den andra returnerar 4, varför?

			ReturnValue()

			Det som kan förvirra är new int() som man tänker skapar nya objekt(referenstyp).

					int x = new int();

					https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/expressions#12817-the-new-operator
					"The new operator implies creation of an instance of a type, but does not necessarily imply allocation of memory.
					In particular, instances of value types require no additional memory beyond the variables in which they reside,
					and no allocations occur when new is used to create instances of value types."

				Alltså skapas en vanlig System.Int32 som är av value type och y tilldelas en kopia av x's värde och därför returnerar ReturnValue() 3.

			ReturnValue2()

				x är av referenstyp till ett MyInt objekt.
				Efter y=x så pekar y på samma objekt som x
				y.MyValue = 4 ändrar värdet på samma objekt som x refererar till

				Därför returnerar funktionen värdet 4.

	*/

	internal class Program
	{
		/// <summary>
		/// The main method, vill handle the menues for the program
		/// </summary>
		/// <param name="args"></param>
		private static void Main()
		{
			while (true)
			{
				Console.WriteLine("Please navigate through the menu by inputting the number \n(1, 2, 3 ,4, 0) of your choice"
					+ "\n1. Examine a List"
					+ "\n2. Examine a Queue"
					+ "\n3. Examine a Stack"
					+ "\n4. CheckParenthesis"
					+ "\n0. Exit the application");
				char input = ' '; //Creates the character input to be used with the switch-case below.
				try
				{
					input = Console.ReadLine()![0]; //Tries to set input to the first char in an input line
				}
				catch (IndexOutOfRangeException) //If the input line is empty, we ask the users for some input.
				{
					Console.Clear();
					Console.WriteLine("Please enter some input!");
				}
				switch (input)
				{
					case '1':
						ExamineList();
						break;

					case '2':
						ExamineQueue();
						break;

					case '3':
						ExamineStack();
						break;

					case '4':
						CheckParanthesis();
						break;
					/*
                     * Extend the menu to include the recursive
                     * and iterative exercises.
                     */
					case '0':
						Environment.Exit(0);
						break;

					default:
						Console.WriteLine("Please enter some valid input (0, 1, 2, 3, 4)");
						break;
				}
			}
		}

		/// <summary>
		/// Examines the datastructure List
		/// </summary>
		private static void ExamineList()
		{
			/*
             * Loop this method untill the user inputs something to exit to main menue.
             * Create a switch statement with cases '+' and '-'
             * '+': Add the rest of the input to the list (The user could write +Adam and "Adam" would be added to the list)
             * '-': Remove the rest of the input from the list (The user could write -Adam and "Adam" would be removed from the list)
             * In both cases, look at the count and capacity of the list
             * As a default case, tell them to use only + or -
             * Below you can see some inspirational code to begin working.
            */

			//List<string> theList = new List<string>();
			//string input = Console.ReadLine();
			//char nav = input[0];
			//string value = input.substring(1);

			//switch(nav){...}
		}

		/// <summary>
		/// Examines the datastructure Queue
		/// </summary>
		private static void ExamineQueue()
		{
			/*
             * Loop this method untill the user inputs something to exit to main menue.
             * Create a switch with cases to enqueue items or dequeue items
             * Make sure to look at the queue after Enqueueing and Dequeueing to see how it behaves
            */
		}

		/// <summary>
		/// Examines the datastructure Stack
		/// </summary>
		private static void ExamineStack()
		{
			/*
             * Loop this method until the user inputs something to exit to main menue.
             * Create a switch with cases to push or pop items
             * Make sure to look at the stack after pushing and and poping to see how it behaves
            */
		}

		private static void CheckParanthesis()
		{
			/*
             * Use this method to check if the paranthesis in a string is Correct or incorrect.
             * Example of correct: (()), {}, [({})],  List<int> list = new List<int>() { 1, 2, 3, 4 };
             * Example of incorrect: (()]), [), {[()}],  List<int> list = new List<int>() { 1, 2, 3, 4 );
             */
		}
	}
}
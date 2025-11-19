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
					+ "\n5. CheckRekursion"
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
					case '5':
						CheckRekursion();
						break;

					case '0':
						Environment.Exit(0);
						break;

					default:
						Console.WriteLine("Please enter some valid input (0, 1, 2, 3, 4)");
						break;
				}
			}
		}
		private static int RecursiveEven(int n)
		{
			// Det första jämna talet är 2
			if (n == 1)
			{
				return 2;
			}
			return (RecursiveEven(n - 1) + 2);
		}

		private static int Fib(int n)
		{
			// Basfallen
			if (n <= 0) return 0;
			if (n == 1) return 1;
			return Fib(n - 1) + Fib(n - 2);
		}


		private static void CheckRekursion()
		{
			Console.Write("Vilket fibonaccital? ");
			if (int.TryParse(Console.ReadLine() , out int n))
			{
				Console.WriteLine("Svar: " + Fib(n));
			}
			
		}

		/// <summary>
		/// Examines the datastructure List
		/// </summary>
		private static void ExamineList()
		{
			/*

			2. När ökar listans kapacitet? (Alltså den underliggande arrayens storlek)

				När Count når Capacity, alltså när listan är full.

			3. Med hur mycket ökar kapaciteten?

				Den dubbleras. (Från List.cs)
					int newCapacity = _items.Length == 0 ? DefaultCapacity : 2 * _items.Length;

			4. Varför ökar inte listans kapacitet i samma takt som element läggs till?

				För att öka listans kapacitet måste en ny array skapas och
				till den nya arrayen måste alla element kopieras från den gamla.
				Detta har tidskomplexiteten O(n) och det är något man vill undvika göra ofta och det är därför
				listan växer i större steg.

			5. Minskar kapaciteten när element tas bort ur listan?

				Nej, detta beror på samma sak som ovan, man vill inte kopiera i onödan.

			6. När är det då fördelaktigt att använda en egendefinierad array istället för en lista?

				När man vet hur stor arrayen ska vara från start och att storleken inte ändras.
				När minnet är väldigt begränsat, dels tar objektet mer minne än en lista men också för listans
				Capacity växer exponentiellt.
				Det går snabbare att hantera en array direkt än via en klass.

				När jag modellerar ett 2d grid med rader och kolumner är det lättare att använda vanliga arrayer.

			*/

			/*
             * Loop this method untill the user inputs something to exit to main menue.
             * Create a switch statement with cases '+' and '-'
             * '+': Add the rest of the input to the list (The user could write +Adam and "Adam" would be added to the list)
             * '-': Remove the rest of the input from the list (The user could write -Adam and "Adam" would be removed from the list)
             * In both cases, look at the count and capacity of the list
             * As a default case, tell them to use only + or -
             * Below you can see some inspirational code to begin working.
            */

			List<string> theList = new List<string>();

			while (true)
			{
				Console.Write("+<word> or -<word> or quit: ");
				string input = Console.ReadLine();

				if (input == "quit")
				{
					break;
				}

				char nav = input[0];
				string value = input.Substring(1);

				switch (nav)
				{
					case '+':
						theList.Add(value);
						break;

					case '-':
						theList.Remove(value);
						break;

					default:
						Console.WriteLine("Bad input");
						break;
				}

				Console.WriteLine("List: [" + string.Join(", ", theList) + "]");
				Console.WriteLine($"Count: {theList.Count}, Capacity: {theList.Capacity}");
				Console.WriteLine();
			}
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

			Queue<string> theQueue = new Queue<string>();

			while (true)
			{
				Console.WriteLine("Enter word to add, del to remove or quit");

				Console.Write("Input: ");
				string input = Console.ReadLine();

				if (input == "quit")
				{
					break;
				}

				switch (input)
				{
					case "del":
						if (theQueue.Count > 0)
							theQueue.Dequeue();
						break;

					default:
						theQueue.Enqueue(input);
						break;
				}

				Console.WriteLine("First in -> [" + string.Join(", ", theQueue) + "] <- Last in");
				Console.WriteLine();
			}
		}

		/// <summary>
		/// Examines the datastructure Stack
		/// </summary>
		private static void ExamineStack()
		{
			/*

			1. Simulera ännu en gång ICA-kön på papper. Denna gång med en stack. Varför är det inte så smart att använda en stack i det här fallet?

			För då kommer den som senaste anlände till kön att få gå först.

			*/

			/*
             * Loop this method until the user inputs something to exit to main menue.
             * Create a switch with cases to push or pop items
             * Make sure to look at the stack after pushing and and poping to see how it behaves
            */

			Stack<string> theStack = new Stack<string>();
			while (true)
			{
				Console.Write("Enter: push <value>, or pop to remove, or quit: ");
				string input = Console.ReadLine();
				if (input == "quit") break;

				string[] parts = input.Split(" ");

				switch (parts[0])
				{
					case "push":
						theStack.Push(parts[1]);
						break;

					case "pop":
						theStack.Pop();
						break;

					default:
						Console.WriteLine("Invalid command");
						break;
				}

				Console.WriteLine("Last in -> [" + string.Join(", ", theStack) + "] <- First in");
			}
		}

		/// <summary>
		/// Return a string reversed using a stack
		/// </summary>
		public static String ReverseText(String text)
		{
			String result = string.Empty;
			Stack<char> theStack = new Stack<char>();
			foreach (char c in text)
			{
				theStack.Push(c);
			}
			// Sist in blir först ut.
			while (theStack.Count > 0)
			{
				result += theStack.Pop();
			}
			return result;
		}

		private static void CheckParanthesis()
		{
			/*
				1.Skapa med hjälp av er nya kunskap funktionalitet för att kontrollera en välformad
				sträng på papper.Du ska använda dig av någon eller några av de datastrukturer vi
				precis gått igenom.Vilken datastruktur använder du?

				Jag använder en stack:
						Öppningsparenteser läggs på stacken(push)
						Hittar jag en stängande parentes måste den matcha den senast tillagda öppnande parentesen(pop)

			*/


			/*
             * Use this method to check if the paranthesis in a string is Correct or incorrect.
             * Example of correct: (()), {}, [({})],  List<int> list = new List<int>() { 1, 2, 3, 4 };
             * Example of incorrect: (()]), [), {[()}],  List<int> list = new List<int>() { 1, 2, 3, 4 );
             */

			/*
			string[] tests = {
							"(())",
							"{}",
							"[({})]",
							"List<int> list = new List<int>() { 1, 2, 3, 4 };",
							"(()])",
							"[)",
							"{[()}]",
							"List<int> list = new List<int>() { 1, 2, 3, 4 );",
						};

			foreach (string test in tests)
			{
				bool result = CheckParanthesis(test);
				Console.WriteLine($"{result}\t{test}");
			}
			*/

			Console.Write("Enter a string: ");
			String input = Console.ReadLine();
			if (CheckParanthesis(input))
			{
				Console.WriteLine("Correct");
			}
			else
			{
				Console.WriteLine("Incorrect");
			}
		}

		private static bool CheckParanthesis(string input)
		{
			// Här är ordningen viktig för vi jämnför index senare
			string leftParens = "({[";
			string rightParens = ")}]";

			Stack<char> order = new Stack<char>();

			foreach (char ch in input)
			{
				// Öppningsparanteser
				int leftIndex = leftParens.IndexOf(ch);
				if (leftIndex != -1)
				{
					order.Push(ch);
					continue;
				}

				// Stängningsparanteser
				int rightIndex = rightParens.IndexOf(ch);
				if (rightIndex != -1)
				{
					// Ett öppningstecken måste komma innan stäningstecken
					if (order.Count == 0)
						return false;

					// Öppningstecken
					char top = order.Pop();

					leftIndex = leftParens.IndexOf(top);
					// Motsvarande stängningstecken
					if (leftIndex != rightIndex)
					{
						return false;
					}
				}
			}
			// Det får inte finnas några ostängda tecken kvar
			return order.Count == 0;
		}
	}
}
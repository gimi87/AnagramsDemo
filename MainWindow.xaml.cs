using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace AnagramsDemo
{
	/// <summary>Interaction logic for MainWindow.xaml</summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			input.Text = string.Join(Environment.NewLine,
				"enlist",
				"skins",
				"inlets",
				"fresher",
				"boaters",
				"listen",
				"boaster",
				"silent",
				"borates",
				"tac",
				"refresh",
				"sinks",
				"knits",
				"stink",
				"sort",
				"cat",
				"rots"
			);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			output.Clear();

			//Collect anagrams
			IEnumerable<IGrouping<int, IGrouping<string, string>>> anagramsOccurences =
				CollectAndGroupAnagrams(Regex.Split(input.Text, @"\r?\n").Where(str => !string.IsNullOrWhiteSpace(str)));

			//Then decide what to do with the collection
			output.Text =
				string.Join(Environment.NewLine, anagramsOccurences.OrderByDescending(i => i.Key).Select(anagrams => //order by number of occurences descending
				string.Join(Environment.NewLine, anagrams.OrderByDescending(j => j.Key.Length).Select(words => //order by length of word descending
				string.Join(" ", words.OrderBy(word => word)))))); //order words ascending
		}

		private IEnumerable<IGrouping<string, string>> CollectAnagrams(IEnumerable<string> words) => words
			.GroupBy(w => string.Concat(w.OrderBy(x => x))); //This will group all anagrams by sorted characters in words

		private IEnumerable<IGrouping<int, IGrouping<string, string>>> GroupAnagrams(IEnumerable<IGrouping<string, string>> anagrams) => anagrams
			.GroupBy(i => i.Count()); //This will group by number of occurencces of words

		private IEnumerable<IGrouping<int, IGrouping<string, string>>> CollectAndGroupAnagrams(IEnumerable<string> words)
			=> GroupAnagrams(CollectAnagrams(words));
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Grammer_Helper_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
         
        }

        public void DefButton_Click(object sender, RoutedEventArgs e)
        {
            TypeBut.Content = "Definition";
            ConfirmButton.Content = "Type a word and click me.";
            ConfirmButton.Visibility = Visibility.Visible;
            Output.Visibility = Visibility.Hidden;
            DefButton.Visibility = Visibility.Hidden;
            SubPredButton.Visibility = Visibility.Hidden;
            PartOfGramBtn.Visibility = Visibility.Hidden;
        }

        public void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmButton.Visibility = Visibility.Hidden;
            PartOfGramBtn.Visibility = Visibility.Visible;
            SubPredButton.Visibility = Visibility.Visible;
            DefButton.Visibility = Visibility.Visible;
            if (TypeBut.Content == "Definition")
            {
                string word1 = Input.Text;

                Output.Visibility = Visibility.Visible;
                Output.Text = FindDefinition(Input.Text.ToUpper());
            }
            if (TypeBut.Content == "SubPred")
            {
                Output.Text = GetSubPred(Input.Text.ToUpper());
                Output.Visibility = Visibility.Visible;
                SubPredButton.Visibility = Visibility.Visible;
            }
            if(TypeBut.Content == "PartOfGram")
            {
                Output.Text = GetPartOfGrammar(Input.Text.ToUpper());
                Output.Visibility= Visibility.Visible;

            }
        }
        private void SubPredButton_Click(object sender, RoutedEventArgs e)
        {
            TypeBut.Content = "SubPred";
            ConfirmButton.Content = "Type a Sentence and click me.";
            ConfirmButton.Visibility = Visibility.Visible;
            SubPredButton.Visibility = Visibility.Hidden;
            DefButton.Visibility = Visibility.Hidden;
            PartOfGramBtn.Visibility = Visibility.Hidden;
            Output.Visibility = Visibility.Hidden;
        }





        // this thing accepts a word and gives its definition from a specific document it has to know the location of.
        public static string FindDefinition(string word)
        {
            //Please put in the correct filepath to the text document to get definitions.
            string Filepath = @"C:\grammer.txt";

            List<string> AllWords = File.ReadAllLines(Filepath).ToList();
            string Grammar = "nope";

            Boolean HaveWeFoundIt = false;

            int ticker = 0;

            while (HaveWeFoundIt == false)
            {

                if ((AllWords[ticker].ToUpper()).StartsWith(word) == true)
                {
                    Grammar = AllWords[ticker];
                    HaveWeFoundIt = true;
                }
                else
                {
                    if (72324 == ticker)
                    {
                        Grammar = "we didnt find it :(";
                        HaveWeFoundIt = true;
                    }
                }
                ticker++;
                Console.WriteLine(ticker);
            }
            //This part will look for the word in the whole line instaead of just at the start, such as if you wanted to find Validation instaed of Validate.
            if (Grammar == "we didnt find it :(")
            {
                ticker = 0;
                HaveWeFoundIt = false;
                while (HaveWeFoundIt == false)
                {
                    if ((" " + AllWords[ticker] + " ").ToUpper().Contains(word) == true)
                    {
                        Grammar = AllWords[ticker];
                        HaveWeFoundIt = true;
                    }
                    else
                    {
                        if (72324 == ticker)
                        {
                            Grammar = "we didnt find it :(";
                            HaveWeFoundIt = true;
                        }
                    }
                    ticker++;
                    Console.WriteLine(ticker);

                }
            }
            return Grammar;
        }

        // This method breaks down a string into a list of words by looking for spaces
        public static String[] BreakDown(string broke)
        {
            string broke2 = broke.TrimStart();
            string[] FinishedList = broke2.Split(' ');
           


           return FinishedList;
        }
        // This Method Gets the part of Grammar for a single word
        public static string GetPartOfGrammar(string Gram)
        {
            string BigGrammar = "";
            string Def = FindDefinition(Gram);
            int numLetters = Gram.Length;
            for(int h = numLetters; h < (numLetters + 10); h++)
            {
                BigGrammar = BigGrammar + Def[h];
            }
            string part = BigGrammar;
            if ((part.Contains("—v.") == true) || (part.Contains(" v. ") == true))
            {
                BigGrammar = "ITS A VERB";
            }
            if (part.Contains("—n.") == true || part.Contains(" n. ") == true)
            {
                BigGrammar = "ITS A NOUN";
            }
            if (part.Contains("—adj.") == true || part.Contains(" adj. ") == true)
            {
                BigGrammar = "ITS A Adjective";
            }
            if (part.Contains("—adv.") == true || part.Contains(" adv. ") == true)
            {
                BigGrammar = "ITS A ADVERB";
            }
            if (part.Contains("—prep.") == true || part.Contains(" prep. ") == true)
            {
                BigGrammar = "ITS A Preposition";
            }
            if (part.Contains("—conj.") == true || part.Contains(" conj. ") == true)
            {
                BigGrammar = "ITS A Conjuction";
            }
            if (part.Contains("—abbr.") == true || part.Contains(" abbr. ") == true)
            {
                BigGrammar = "ITS A abrieviation i know i spelled it wrong";
            }
            return BigGrammar;
        }
        // This method uses the two previous methods to breakdown a sentence and find each words part of grammar
        // Then it identifies the subject and predicate and returns them.
        public static string GetSubPred(string sentence)
        {
            string Answer = "";
            string Subject = "";
            String Predicate = "";
            bool foundSub = false;
            bool foundPred = false;
            string[] WordList = BreakDown(sentence);

            for(int b = 0; b < WordList.Length; b++)
            {
                if (GetPartOfGrammar(WordList[b]).Contains("NOUN") == true && foundSub == false)
                {
                    foundSub = true;
                    Subject = WordList[b];
                }
                if (GetPartOfGrammar(WordList[b]).Contains("VERB") == true && foundPred == false)
                {
                    foundPred = true;
                    Predicate = WordList[b];
                }

            }
            Answer = "The Subject is " + Subject + " and the Predicate is " + Predicate + ", does that help?";

            return Answer;
        }

        private void PartOfGramBtn_Click(object sender, RoutedEventArgs e)
        {
            Output.Visibility = Visibility.Hidden;
            PartOfGramBtn.Visibility = Visibility.Hidden;
            TypeBut.Content = "PartOfGram";
            ConfirmButton.Content = "Type a word.";
            ConfirmButton.Visibility = Visibility.Visible;
            SubPredButton.Visibility = Visibility.Hidden;
            DefButton.Visibility = Visibility.Hidden;
            PartOfGramBtn.Visibility = Visibility.Hidden;
        }
    }
    //hope this helps
    public class Isaclass
    {
        public string oops = "sorry for the trouble";
    }
}

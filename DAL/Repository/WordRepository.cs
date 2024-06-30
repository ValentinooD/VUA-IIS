using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class WordRepository : IWordRepository
    {
        private static IList<Word> words = new List<Word>()
        {
            new Word() {String = "car", Meaning="Vehicle that drives on 4 wheels", Invented = 2001},
            new Word() {String = "cat", Meaning="A silly little animal", Invented = 1920},
            new Word() {String = "bar", Meaning="A staff", Invented = 123},
            new Word() {String = "bat", Meaning="An animal that prevents crime in the city", Invented = -100}
        };

        public WordRepository() { }

        public void AddWords(params Word[] list)
        {
            foreach (var item in list)
            {
                words.Add(item);
            }
        }

        public void AddWords(IList<Word> list)
        {
            foreach (var item in list)
            {
                words.Add(item);
            }
        }

        public Word? GetWord(string word)
        {
            return words.FirstOrDefault(s => s.String.ToLower().Equals(word.ToLower()));
        }

        public IList<Word> GetWords()
        {
            return words;
        }
    }
}

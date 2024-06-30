using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IWordRepository
    {
        public IList<Word> GetWords();
        public Word? GetWord(string word);

        public void AddWords(IList<Word> words);
        public void AddWords(params Word[] words);
    }
}

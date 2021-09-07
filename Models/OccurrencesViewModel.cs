
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssignmentGit.Models
{
    public class OccurrencesViewModel
    {
        public OccurrencesViewModel()
        {
            WordOccured = new Dictionary<string, int>();
            DisplayActualMessagesList = new List<string>();
            DisplaySortedWordsMessagesList = new List<string>();

        }
        public Dictionary<string,int> WordOccured { get; set; }
        public List<string> DisplayActualMessagesList { get; set; }
        public List<string> DisplaySortedWordsMessagesList { get; set; }

    }



}
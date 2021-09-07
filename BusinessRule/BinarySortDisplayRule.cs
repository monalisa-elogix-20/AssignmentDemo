using AssignmentGit.Helper;
using AssignmentGit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssignmentGit.BusinessRule
{
    public class BinarySortDisplayRule
    {
        public  Dictionary<int, List<bstModel>> MakeSortedCommentsByASCIIDictionary(List<string> CommentsStr)
        {

            Dictionary<int, List<bstModel>> SumASCIIDictionary = new Dictionary<int, List<bstModel>>();
            Dictionary<int, List<bstModel>> SortedSumASCIIDictionary = new Dictionary<int, List<bstModel>>();

            SumASCIIDictionary = GetSumAsCIIByWordsDictionary(CommentsStr);

            foreach (var item in SumASCIIDictionary)
            {
                var newSortedArr = BinarySearchHelper.createSortedByBinarySearchList(item.Value, SumASCIIDictionary.Count);
                SortedSumASCIIDictionary.Add(item.Key, newSortedArr);
            }
            return SortedSumASCIIDictionary;
        }
        private  Dictionary<int, List<bstModel>> GetSumAsCIIByWordsDictionary(List<string> CommentsStr)
        {
            List<bstModel> WordList = new List<bstModel>();
            Dictionary<int, List<bstModel>> MessageList = new Dictionary<int, List<bstModel>>();

            long[] ACIIArr = new long[2];
            int j = 0;
            foreach (var Comment in CommentsStr)
            {
                String[] Words = Comment.Trim().Split(' ');
                ACIIArr = new long[Words.Length + 1];
                int x = 0;
                String EachWrd = string.Empty;
                WordList = new List<bstModel>();
                foreach (var W in Words)
                {
                    if (!W.Equals(String.Empty))
                    {
                        long sum = 0;
                        EachWrd = W;

                        for (int i = 0; i < W.Length; i++)
                        {
                            sum += W[i];

                        }
                        ACIIArr[x] = sum;
                        x++;
                        WordList.Add(new bstModel { Word = EachWrd, AscIIVal = sum });
                    }
                }

                MessageList.Add(j, WordList);
                j++;


            }

            return MessageList;
        }
    }
}
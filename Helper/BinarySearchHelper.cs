using AssignmentGit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentGit.Helper
{
   public static class BinarySearchHelper
    {

        public static long[] createSortedByBinarySearch(long[] arr, int len)
        {

            List<long> b = new List<long>();
            long[] SortedASCIIArr = new long[len];

            for (int j = 0; j < len; j++)
            {
                // if b is empty any element can be at
                // first place
                if (b.Count == 0)
                    b.Add(arr[j]);
                else
                {

                    // Perform Binary Search to find the correct
                    // position of current element in the
                    // new array
                    int start = 0, end = b.Count - 1;

                    // let the element should be at first index
                    int pos = 0;

                    while (start <= end)
                    {

                        int mid = start + (end - start) / 2;

                        // if a[j] is already present in the new array
                        if (b[mid] == arr[j])
                        {
                            // add a[j] at mid+1. you can add it at mid
                            b.Insert((Math.Max(0, mid + 1)), arr[j]);
                            break;
                        }

                        // if a[j] is lesser than b[mid] go right side
                        else if (b[mid] > arr[j])

                            // means pos should be between start and mid-1
                            pos = end = mid - 1;
                        else

                            // else pos should be between mid+1 and end
                            pos = start = mid + 1;

                        // if a[j] is the largest push it at last
                        if (start > end)
                        {
                            pos = start;
                            b.Insert(Math.Max(0, pos), arr[j]);

                            // here Max(0, pos) is used because sometimes
                            // pos can be negative as smallest duplicates
                            // can be present in the array
                            break;
                        }
                    }
                }
            }

            SortedASCIIArr = b.ToArray();
            return SortedASCIIArr;
        }

        public static List<bstModel> createSortedByBinarySearchList(List<bstModel> BTModel, int len)
        {

            List<bstModel> bS = new List<bstModel>();

            List<bstModel> SortedASCIIArrS = new List<bstModel>();

            foreach (var item in BTModel)
            {
                if (SortedASCIIArrS.Count == 0)
                {
                    SortedASCIIArrS.Add(new bstModel { Word = item.Word, AscIIVal = item.AscIIVal });
             
                }
                else
                {

                    // Perform Binary Search to find the correct
                    // position of current element in the
                    // new array
                    int start = 0, end = SortedASCIIArrS.Count - 1;

                    // let the element should be at first index
                    int pos = 0;

                    while (start <= end)
                    {

                        int mid = start + (end - start) / 2;

                        // if item.Word, item.AscIIVal is already present in the new array
                        if ((SortedASCIIArrS[mid].Word == item.Word) && (SortedASCIIArrS[mid].AscIIVal == item.AscIIVal))
                        {
                            // add item at mid+1. you can add it at mid

                            SortedASCIIArrS.Insert((Math.Max(0, mid + 1)), new bstModel { Word = item.Word, AscIIVal = item.AscIIVal });
                            break;
                        }

                        // if item.AscIIVal is lesser than SortedASCIIArrS[mid].AscIIVal go right side
                        else if (SortedASCIIArrS[mid].AscIIVal > item.AscIIVal)

                            // means pos should be between start and mid-1
                            pos = end = mid - 1;
                        else

                            // else pos should be between mid+1 and end
                            pos = start = mid + 1;

                        // if item is the largest push it at last
                        if (start > end)
                        {
                            pos = start;
                            //b.Insert(Math.Max(0, pos), arr[j]);
                            SortedASCIIArrS.Insert((Math.Max(0, pos)), new bstModel { Word = item.Word, AscIIVal = item.AscIIVal });
                            // here Max(0, pos) is used because sometimes
                            // pos can be negative as smallest duplicates
                            // can be present in the array
                            break;
                        }
                    }
                }
            }


            return SortedASCIIArrS;
        }
    }
}

using AssignmentGit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssignmentGit.BusinessRule;
using Octokit;
using System.Text;

namespace AssignmentGit.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult ResultView(GitAuthDetailsObjectModel model)
        {

            
            var modelData = new OccurrencesViewModel();
            try
            {
                //Output1 : (Display each comment by Sorted words(ASCII Value) using BST)
                //--------------------------------------------------------------------------------------------
                ApiOptions options = new ApiOptions
                {
                    StartPage = 1,
                    PageSize = 100,
                    PageCount = 1
                };

                var ghClient = new GitHubClient(new ProductHeaderValue(model.Reponame), new Uri(model.Url));
                if (!string.IsNullOrEmpty(model.Uname) && !string.IsNullOrEmpty(model.Token))
                {
                    //var basicAuth = new Credentials( model.Token, AuthenticationType.Bearer);
                    var basicAuth = new Credentials(model.Uname, model.Token, AuthenticationType.Basic);
                    ghClient.Connection.Credentials = basicAuth;
                    //ghClient.Credentials = basicAuth;
                }

                var repositoryCommt = ghClient.Repository.Commit.GetAll(model.Uname, model.Projname, options).Result;
                var commitMessages = repositoryCommt.Where(x => !string.IsNullOrEmpty(x.Commit.Message)).Select(x => x?.Commit?.Message).ToList();


                //Output1 : (Display each comment by Sorted words(ASCII Value) using BST)
                //------------------------------------------------------------------------
                var bRule = new BinarySortDisplayRule();
                var result = bRule.MakeSortedCommentsByASCIIDictionary(commitMessages);
                var resultSrlz = result.Select(x => x.Value).Select(x => string.Join(" ", x.Select(v => v.Word)));

                //-------------------------------End Of Code-------------------------------

                //Output2 : (Word Occurences display)
                //---------------------------------------------------------------------------
                var occourencesCountHelper = new OccourencesCountRule();
                var wordCountData = occourencesCountHelper.GetAllWordsCount(commitMessages) ?? new Dictionary<string, int>();

                //--------------------------------End Of Code-------------------------------

                modelData.WordOccured = wordCountData;
                modelData.DisplayActualMessagesList = commitMessages;
                modelData.DisplaySortedWordsMessagesList = resultSrlz.Where(x => !string.IsNullOrEmpty(x)).ToList();

                //Output3 : (Storing Word Occurences data in tempdata for CSV download)
                //---------------------------------------------------------------------------
                var objectDict = wordCountData.OrderBy(obj => obj.Key).ToDictionary(obj => obj.Key, obj => obj.Value);
                var CSVStr = String.Join(
                 Environment.NewLine,
                 objectDict.Select(d => $"{d.Key},{d.Value},")
                );

                TempData["CsvData"] = CSVStr;
                //--------------------------------End Of Code-------------------------------

            }
            catch (Exception ex)
            {

                ViewBag.Message = ex.Message;
            }
            return PartialView(modelData);
        }

        public FileContentResult DownloadCSV()
        {
            var csvStringData = (string)(TempData["CsvData"] ?? "No data found !");
            TempData.Keep("CsvData");
            return File(new System.Text.UTF8Encoding().GetBytes(csvStringData), "text/csv", "report.csv");
        }


    }
}
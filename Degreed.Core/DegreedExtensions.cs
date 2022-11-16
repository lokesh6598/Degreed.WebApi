using System.Text.RegularExpressions;

namespace Degreed.Core
{
    public static class DegreedExtensions
    {
        #region Public Methods

        /// <summary>
        /// This method will heightlight the search word with brackets on received input
        /// </summary>
        /// <param name="joke"></param>
        /// <param name="matchingTerm"></param>
        /// <returns></returns>
        public static string HighlightSearchWord(this string joke, string matchingTerm)
        {
            return Regex.Replace(joke, matchingTerm, @"'[" + matchingTerm + @"]'", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// This method will cound the number of words on received input
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static int CountNumberOfWords(this string content)
        {
            int numberOfWords = 0;
            var words = content.Split(' ');
            foreach (var word in words)
            {
                numberOfWords++;
            }
            return numberOfWords;
        }
        #endregion
    }
}

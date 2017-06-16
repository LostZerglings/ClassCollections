using System;

namespace Program
{
    class GetTextBetweenWords
    {
        public static string textbetween(string source, string startingWord, string endingWord)
        {
            // If the source string contains the start word and the end word we want to get the text between them. 
            //
            // Otherwise we want to return an empty string since nothing was found.

            if (source.Contains(startingWord) && source.Contains(endingWord))
            {
                // In order to get the text between the two values we need to get the index of each word.
                //
                // IndexOf will get us the value, we just need to supply it with where to look.
                //
                // Once we do that add the length of the search criteria (start or end) to ensure that the starting and ending words are not included.

                int startingIndex = source.IndexOf(startingWord, 0) + startingWord.Length;

                int endingIndex = source.IndexOf(endingWord, startingIndex);

                // We can then return the text between by using substring to identy the index range we want.
                //
                // We need to specify the starting index and the length of the content we want to return (the range of characters between the start index and the ending index.
                //
                // To get that length simply subtract the ending index from the starting index. 

                return source.Substring(startingIndex, endingIndex - startingIndex);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}

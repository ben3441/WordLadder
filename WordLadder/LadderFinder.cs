using System.Collections.Concurrent;

namespace WordLadder;

internal static class LadderFinder
{
    internal const int NotSetMin = -1;

    internal static IReadOnlyCollection<List<string>> Find(Dictionary<string, List<string>> connectingWordsLookup, string startWord, string endWord)
    {
        var laddersBruteForce = new ConcurrentStack<List<string>>();
        var currentMinLength = NotSetMin;

        var startingSets = connectingWordsLookup[startWord]
            .Select(connectingWord => new List<string> {startWord, connectingWord});

        Parallel.ForEach(startingSets, RecursivelySearchWordCombinationsForLadders);

        void RecursivelySearchWordCombinationsForLadders(IList<string> currentLadder)
        {
            var connectors = connectingWordsLookup[currentLadder.Last()];

            bool currentLadderAlreadyLongerThanShortestFound =
                currentMinLength != NotSetMin && currentLadder.Count + 1 > currentMinLength;

            if (currentLadderAlreadyLongerThanShortestFound)
                return; 

            foreach (var connector in connectors)
            {
                if (currentLadder.Contains(connector))
                    continue;

                var extendedLadder = new List<string>(currentLadder) { connector };

                if (connector == endWord)
                {
                    UpdateMinLengthSafelyIfRequired(extendedLadder.Count);

                    void UpdateMinLengthSafelyIfRequired(int newMin)
                    {
                        if (currentMinLength != NotSetMin && newMin >= currentMinLength) 
                            return;
                        while (currentMinLength == NotSetMin || newMin < currentMinLength)
                        {
                            if (currentMinLength == Interlocked.CompareExchange(ref currentMinLength, newMin, currentMinLength))
                                break;
                        }
                    }

                    laddersBruteForce.Push(extendedLadder);
                    break;
                }
                RecursivelySearchWordCombinationsForLadders(extendedLadder);
            }
        }

        return laddersBruteForce.ToList();
    }
}
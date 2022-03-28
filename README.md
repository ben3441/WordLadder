## WordLadder

The application is split between a console host and a library it uses which implements the word ladder algorithm. The idea being that the library is a pure word ladder solver unaware of how it is used and so reusable elsewhere.

The initial algorithm the application went through to solve the word ladder was:
1. Trim word set to words of same length as start and end words
2. Build lookup for each word in word set to words that differ from it by one character 
3. From start word search through all connecting word combinations to find ladders
4. From ladders find smallest and return.

This worked well with the small data sets I'd used to test drive the word ladder algorithm code but proved extremely slow with the provided word set.

To improve the performance of the application it was changed so that:
-  Connecting words in the lookup are ordered by similarity to end word, so most likely paths are checked first
- Parallelise ladder search at first recursive branch
- Track shortest ladder found so far and halt searches that go over that length

These changes improved performance massively and to the point I was happy with it and so stopped optimising, however further improvements that could be made are:
- Cleverer parallelisation scheme, current approach is limited by the number of connecting words the start word has
- Track words that lead to dead ends and break out of searches early when found
- Use of HashSet instead of List when building ladders to speed up the frequent .Contains() calls, the downside of this is you loose the order of the words in a HashSet which whilst not a requirement in the doc is useful so finding/implementing a collection that maintains adding order but has O(1) speed for .Contains() operations

Tests have been added for both the console app and the library, in both cases I think these tests add a valuable safety net proving the code works without leaking implementation details - they were very useful when refactoring to higher performance.

The classes in the implementation are almost entirely static, not something I stick with in general development but for this problem seems simplest solution.

I have heavily used the Result/Result<T> type from the package CSharpFunctionalExtensions throughout to simplify control flow/error reporting for operations which may fail
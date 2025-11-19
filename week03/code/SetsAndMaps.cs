using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE
        // create a new HashSet
        var pairs = new HashSet<string>();
        var notPairs = new HashSet<string>();

        foreach (var word in words)
        {
            // reverse the characters in the word
            var charArray = word.ToCharArray();
            Array.Reverse(charArray);
            var reversedWord = new string(charArray);

            // checking if the reversed word is in set
            if (notPairs.Contains(reversedWord))
            {
                pairs.Add($"{word} & {reversedWord}");
            }
            else
            {
                // checking for each cases
                if (word[0] != word[1])
                {
                    notPairs.Add(word);
                }
            }
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // TODO Problem 2 - ADD YOUR CODE HERE
            // 1. I'll check if the length of the fields is greater or equals to 4
            if (fields.Length >= 4)
            {
                // Trimming any extra space
                var degree = fields[3].Trim();
                // add the degree to the dictionary or increment the count by 1
                if (!degrees.ContainsKey(degree))
                {
                    degrees[degree] = 1;
                }
                else
                {
                    degrees[degree]++;
                }
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // TODO Problem 3 - ADD YOUR CODE HERE
        // 1. Getting rid of any white spaces by using the replace method
        // I use the REPLACE method instead of the TRIM method because unlike 
        // The TRIM method that remove leading and trailing whitespace, REPLACE
        // removes all spaces, including within the string
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        // 2. If the lengths differ, they cannot be anagrams
        if (word1.Length != word2.Length)
        {
            return false;
        }

        // 3. A dictionary to count letter frequencies in word1
        var letterCounts = new Dictionary<char, int>();

        // 4. Count letters in word1
        foreach (var letter in word1)
        {
            if (!letterCounts.ContainsKey(letter))
            {
                letterCounts[letter] = 1;
            }
            else
            {
                letterCounts[letter]++;
            }
        }

        // 5. Validate letters in word2
        foreach (var letter in word2)
        {
            if (!letterCounts.ContainsKey(letter) || letterCounts[letter] == 0)
            {
                return false;
            }

            letterCounts[letter]--;
        }

        // if all counts are zero, the words are anagrams
        return true;

    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.

        // Prepare the list for formatted strings
        var earthquakeSummaries = new List<string>();

        // Ensure the FeatureCollection is valid and contains features
        if (featureCollection?.Features != null)
        {
            foreach (var feature in featureCollection.Features)
            {
                // Extract magnitude and location
                var mag = feature.Properties.Mag;
                var place = feature.Properties.Place;

                // Add a formatted string for each valid earthquake
                if (mag.HasValue && !string.IsNullOrWhiteSpace(place))
                {
                    earthquakeSummaries.Add($"{place} - Mag {mag:F2}");
                }
            }
        }

        // Return the results as an array
        return earthquakeSummaries.ToArray();


    }
}
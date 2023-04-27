# BingCustomSearchLibrary
This Library converts the jsons from Bing Custom Search Api to Response Objects
<br><br>
USAGE: 
<br>
var searcher = new CustomSearch("YOUR_SUBSCRIPTION_KEY","CUSTOM_CONFIGURATION_ID");
<br>
var results = searcher.GetWebResults("YOUR_SEARCH_TERMS");
<br>
Console.WriteLine(results.name);
<br>
Console.WriteLine(results.url);
<br>
Console.WriteLine(results.snippet);
<br><br>
OR:
<br>
var results = searcher.GetImageResults("YOUR_SEARCH_TERMS");
<br>
OR:
<br>
var results = searcher.GetVideoResults("YOUR_SEARCH_TERMS");

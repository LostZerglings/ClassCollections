# ClassCollections
Collection of useful methods and classes

SimpleLogger.cs

Summary: 
A simple logging class that receives the log as a string, appends the user who logged it and the date and time it occurred, then writes it to a file. 

Usage: 
Log.writelog("log text goes here");




GetTextBetweenWords.cs

Summary:
A simple text class that allows you to select the text between two words in a string.

Usage:
string result = GetTextBetweenWords.textbetween("Hello strange world","Hello","world");
result now contains the word "strange"

RateChart
=========
This program is a Windows Form Application written in C#.

The program firstly reads from database the EURUSD exchange rate within a specific period according to user’s input, then draw the chart using GDI.

The program draws only a simple line chart, but can be modified to other forms such as candlestick chart without too much effort.

The entry is in ***Chart/Program.cs***  
Layout of the window and chart is defined in ***Chart/Form1.cs***  
Data structure is defined in ***Chart/Rate.cs***  
***Chart/ReadData.cs*** connects to the database

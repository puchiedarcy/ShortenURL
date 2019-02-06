ShorterURL is a code library that takes in URLs and shortens them as much as possible.
When given back the short URL, it can expand it back to the original full URL.
This project also keeps tracks of the number of "clicks" a short URL has, which is assumed to correspond to the number of times a short URL is asked to be expanded.
This goal of this library is to always generate the shortest possible URL. The ShortHashMaker class has comments on how that is achieved.


This solution has three projects.
ShorterURL.Lib - Contains the business logic of shortening and expanding URLs and well as their storage (in this case, simply in-memory for simplicity).
ShorterURL.Lib.Tests - Test cases for the major classes on ShorterURL.Lib
ShorterURL.Web - A very simple ASP MVC web layer for interacting with ShorterURL.Lib via a browser.


This solution was made in Visuao Studio 2017 Community Edition and should compile and run with no other external dependencies.


The three main classes in ShorterURL.Lib that contian all the business logic for shortening and expanding URLs are:
* ShortenURL - Contains the Shorten and Expand methods for full and short URLs
* ShortenedURLRepository - Holds shortened URLs in memory for basic (yet temporary) persistence
* ShortHashMaker - Will compute the next shortest hash available for a short URL to use
* ShortenedURL - POCO for holding related data together.

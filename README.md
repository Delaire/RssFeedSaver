# RssFeedSaver
This is a RSS feed saver, it allows you to save your feeds to your Azure DB 

This is an easy to use RSS Feed saver, all you need to do is setup you Azure DB and get the feed of the site that yo wish to save

#What you need to do before you are ready to save the feed data
this project has been setup for savign the feed of http://www.theverge.com/rss/current

if you wish to use another feedurl that is fine you will just need to go into RssFeedSaver/RssFeedApi/Service/ParseData.cs
and starting line 60 modify the code so that it saves the correct information for you specific feed.
(look for this comment: //TODO: to be customized for each FEED)

This solution is proposed as is.

TODO:
Add unit testing.

 
